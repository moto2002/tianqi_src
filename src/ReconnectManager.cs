using Package;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using XNetwork;

public class ReconnectManager : IReconnectManager
{
	protected static readonly int AutoReconnectTimes = 3;

	protected static ReconnectManager instance;

	protected bool hasInit;

	protected bool enableReconnect = true;

	protected bool isProcessingReconnect;

	protected IPEndPoint currentReconnectIEEndPoint;

	protected ServerType currentReconnectServiceType;

	protected List<ReconnectMessage> waitingReconnectList = new List<ReconnectMessage>();

	protected bool isReconnectingDataServer;

	protected IDataServerReconnectHandler dataServerReconnectHandler = DataServerCityReconnectHandler.Instance;

	protected int reconnectDataServerCount;

	protected bool isSendingDataServerVerify;

	protected WaitingConnectUI waitConnectUI;

	protected uint dataServerWaitTimer;

	protected uint chatServerWaitTimer;

	public static ReconnectManager Instance
	{
		get
		{
			if (ReconnectManager.instance == null)
			{
				ReconnectManager.instance = new ReconnectManager();
			}
			return ReconnectManager.instance;
		}
	}

	public bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		set
		{
			this.hasInit = value;
		}
	}

	public bool EnableReconnect
	{
		get
		{
			return this.enableReconnect;
		}
		set
		{
			this.enableReconnect = value;
			if (!value)
			{
				this.IsProcessingReconnect = false;
				this.IsReconnectingDataServer = false;
				this.ReconnectDataServerCount = 0;
				this.IsSendingDataServerVerify = false;
				this.waitingReconnectList.Clear();
				this.TryShowWaitReconnectResultUI(false, 0);
				if (DialogBoxUIView.Instance != null)
				{
					DialogBoxUIView.Instance.Show(false);
				}
				TimerHeap.DelTimer(this.dataServerWaitTimer);
				TimerHeap.DelTimer(this.chatServerWaitTimer);
			}
		}
	}

	protected bool IsProcessingReconnect
	{
		get
		{
			return this.isProcessingReconnect;
		}
		set
		{
			this.isProcessingReconnect = value;
		}
	}

	public bool IsReconnectingDataServer
	{
		get
		{
			return this.isReconnectingDataServer;
		}
		protected set
		{
			this.isReconnectingDataServer = value;
			if (value)
			{
				this.IsSendingDataServerVerify = false;
			}
		}
	}

	public IDataServerReconnectHandler DataServerReconnectHandler
	{
		protected get
		{
			return this.dataServerReconnectHandler;
		}
		set
		{
			Debug.Log("DataServerReconnectHandler: " + value.GetType().get_Name());
			this.dataServerReconnectHandler = value;
			EventDispatcher.Broadcast(HeartbeatManagerEvent.ForceSendHeartbeat);
		}
	}

	protected int ReconnectDataServerCount
	{
		get
		{
			return this.reconnectDataServerCount;
		}
		set
		{
			this.reconnectDataServerCount = value;
		}
	}

	public bool IsSendingDataServerVerify
	{
		get
		{
			return this.isSendingDataServerVerify;
		}
		protected set
		{
			this.isSendingDataServerVerify = value;
		}
	}

	public void Init()
	{
		this.HasInit = true;
		NetworkManager.Instance.RegistReconnectHandler(new NetworkService.OnReconnect(this.GetReconnectResult));
		NetworkManager.AddListenEvent<RoleReconnectRes>(new NetCallBackMethod<RoleReconnectRes>(this.OnVerifyReconnectDataServerRes));
	}

	public void Release()
	{
		NetworkManager.RemoveListenEvent<RoleReconnectRes>(new NetCallBackMethod<RoleReconnectRes>(this.OnVerifyReconnectDataServerRes));
		NetworkManager.Instance.RegistReconnectHandler(null);
		this.hasInit = false;
		this.enableReconnect = true;
		this.isProcessingReconnect = false;
		this.isReconnectingDataServer = false;
		this.dataServerReconnectHandler = DataServerCityReconnectHandler.Instance;
		this.reconnectDataServerCount = 0;
		this.isSendingDataServerVerify = false;
		this.currentReconnectIEEndPoint = null;
		this.waitingReconnectList.Clear();
		this.waitConnectUI = null;
		TimerHeap.DelTimer(this.dataServerWaitTimer);
		TimerHeap.DelTimer(this.chatServerWaitTimer);
		ReconnectManager.instance = null;
	}

	public void BeginReconnect(IPEndPoint ipEndPoint, ServerType serverType, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
	{
		Debug.Log("BeginReconnect");
		if (!this.HasInit)
		{
			return;
		}
		if (!this.EnableReconnect)
		{
			return;
		}
		this.TryAddToWaitingReconnectList(ipEndPoint, serverType, onConnectSuccessCallBack, onConnectFailedCallBack);
		if (!this.IsProcessingReconnect)
		{
			this.IsProcessingReconnect = true;
			this.currentReconnectIEEndPoint = ipEndPoint;
			this.currentReconnectServiceType = serverType;
			Loom.Current.QueueOnMainThread(delegate
			{
				switch (serverType)
				{
				case ServerType.Data:
					if (TokenManager.Instance.Token == null)
					{
						this.ConfirmReconnectDataNoToken();
					}
					else
					{
						this.DataServerReconnectHandler.Begin();
						this.ReconnectToDataServer(ipEndPoint, this.DataServerReconnectHandler.BeginCount);
					}
					break;
				case ServerType.Chat:
					this.ReconnectToChatServer(ipEndPoint);
					break;
				case ServerType.LoginInner:
					this.ReconnectToLoginInner();
					break;
				case ServerType.LoginOuter:
					this.ReconnectToLoginOuter();
					break;
				}
			});
		}
	}

	protected void TryAddToWaitingReconnectList(IPEndPoint ipEndPoint, ServerType serverType, Action onConnectSuccessCallBack, Action onConnectFailedCallBack)
	{
		Debug.Log("TryAddToWaitingReconnectList");
		for (int i = 0; i < this.waitingReconnectList.get_Count(); i++)
		{
			if (this.waitingReconnectList.get_Item(i).ipEndPoint == ipEndPoint && this.waitingReconnectList.get_Item(i).serverType == serverType)
			{
				return;
			}
		}
		ReconnectMessage reconnectMessage = default(ReconnectMessage);
		reconnectMessage.ipEndPoint = ipEndPoint;
		reconnectMessage.serverType = serverType;
		reconnectMessage.onConnectSuccessCallBack = onConnectSuccessCallBack;
		reconnectMessage.onConnectFailedCallBack = onConnectFailedCallBack;
		this.waitingReconnectList.Add(reconnectMessage);
		Debug.Log("After AddToWaitingReconnectList: " + this.waitingReconnectList.get_Count());
	}

	protected void CheckWaitingReconnectList()
	{
		this.waitingReconnectList.RemoveAll((ReconnectMessage item) => item.ipEndPoint == this.currentReconnectIEEndPoint && item.serverType == this.currentReconnectServiceType);
		if (this.waitingReconnectList.get_Count() > 0)
		{
			this.BeginReconnect(this.waitingReconnectList.get_Item(0).ipEndPoint, this.waitingReconnectList.get_Item(0).serverType, this.waitingReconnectList.get_Item(0).onConnectSuccessCallBack, this.waitingReconnectList.get_Item(0).onConnectFailedCallBack);
		}
		Debug.Log("CheckWaitingReconnectList: " + this.waitingReconnectList.get_Count());
	}

	protected void GetReconnectResult(ServerType serverType, bool isSuccess)
	{
		if (serverType != ServerType.Data)
		{
			if (serverType != ServerType.Chat)
			{
			}
		}
		else
		{
			this.GetReconnectDataServerResult(isSuccess);
		}
	}

	protected void ReconnectToLoginOuter()
	{
		this.IsProcessingReconnect = false;
		this.CheckWaitingReconnectList();
		LoginManager.Instance.StopGetDataServerInfoTimer(ServerType.LoginOuter);
		NetworkManager.Instance.ConnectLoginOuterServer();
	}

	protected void ReconnectToLoginInner()
	{
		this.IsProcessingReconnect = false;
		this.CheckWaitingReconnectList();
		LoginManager.Instance.StopGetDataServerInfoTimer(ServerType.LoginInner);
		NetworkManager.Instance.ConnectLoginInnerServer();
	}

	protected void ReconnectToDataServer(IPEndPoint ipEndPoint, int currentReconnectCount)
	{
		Debug.Log("ReconnectToDataServer");
		if (!this.EnableReconnect)
		{
			return;
		}
		if (this.IsSendingDataServerVerify)
		{
			return;
		}
		Debug.Log("开始重连");
		if (Application.get_isPlaying())
		{
			this.IsReconnectingDataServer = true;
			this.ReconnectDataServerCount = currentReconnectCount;
			this.TryShowWaitReconnectResultUI(true, currentReconnectCount);
			NetworkManager.Instance.ConnectDataServer(ipEndPoint, new Action(this.OnDataServerReconnectSuccess), new Action(this.OnDataServerReconnectFailed));
		}
	}

	protected void OnDataServerReconnectSuccess()
	{
		Debug.Log("OnDataServerReconnectSuccess");
		this.GetReconnectDataServerResult(true);
	}

	protected void OnDataServerReconnectFailed()
	{
		Debug.Log("OnDataServerReconnectFailed");
		this.AnyReconnectDataFailed();
	}

	protected void GetReconnectDataServerResult(bool isSuccess)
	{
		Debug.Log("OnGetReconnectResult: " + isSuccess);
		if (!this.EnableReconnect)
		{
			return;
		}
		if (isSuccess)
		{
			this.VerifyReconnectDataServer();
		}
		else
		{
			this.HandleReconnectDataServerFailed();
		}
	}

	protected void HandleReconnectDataServerFailed()
	{
		Debug.Log("HandleReconnectDataServerFailed");
		if (this.ReconnectDataServerCount < ReconnectManager.AutoReconnectTimes)
		{
			this.AutoReconnectData();
		}
		else if (this.ReconnectDataServerCount == ReconnectManager.AutoReconnectTimes)
		{
			this.ConfirmReconnectData();
		}
		else
		{
			this.ChooseReconnectData();
		}
	}

	protected void VerifyReconnectDataServer()
	{
		Debug.Log("VerifyReconnect");
		if (this.IsSendingDataServerVerify)
		{
			return;
		}
		string token = TokenManager.Instance.Token;
		if (token == null)
		{
			this.HandleReconnectDataServerFailed();
		}
		else
		{
			this.IsReconnectingDataServer = false;
			this.IsSendingDataServerVerify = true;
			NetworkManager.Instance.SendVerify(new RoleReconnectReq
			{
				roleId = EntityWorld.Instance.EntSelf.ID,
				token = token
			}, ServerType.Data);
		}
	}

	protected void OnVerifyReconnectDataServerRes(short state, RoleReconnectRes down = null)
	{
		Debug.Log("OnVerifyReconnectRes: " + state);
		if (!this.EnableReconnect)
		{
			return;
		}
		if (!this.IsSendingDataServerVerify)
		{
			return;
		}
		this.IsProcessingReconnect = false;
		this.IsReconnectingDataServer = false;
		this.ReconnectDataServerCount = 0;
		this.IsSendingDataServerVerify = false;
		this.TryShowWaitReconnectResultUI(false, 0);
		if (state != 0)
		{
			Debug.Log("验证失败: " + state);
			NetworkManager.Instance.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621273, false), null);
			return;
		}
		Debug.Log("验证成功，连上场景服");
		this.DataServerReconnectHandler.SuccessEnd();
		this.CheckWaitingReconnectList();
	}

	public void VerifyReqNotConnected()
	{
		if (!this.HasInit)
		{
			return;
		}
		if (!this.EnableReconnect)
		{
			return;
		}
		if (!this.IsSendingDataServerVerify)
		{
			return;
		}
		this.IsReconnectingDataServer = true;
		this.IsSendingDataServerVerify = false;
		this.AnyReconnectDataFailed();
	}

	public void VerifyResNotConnected()
	{
		if (!this.HasInit)
		{
			return;
		}
		if (!this.EnableReconnect)
		{
			return;
		}
		if (!this.IsSendingDataServerVerify)
		{
			return;
		}
		this.IsReconnectingDataServer = true;
		this.IsSendingDataServerVerify = false;
		this.AnyReconnectDataFailed();
	}

	public void BeginSynchronizeServerBattle()
	{
		WaitUI.OpenUI(0u);
		EntityWorld.Instance.EntSelf.IsSynchronizingServerBattle = true;
	}

	public void EndSynchronizeServerBattle()
	{
		WaitUI.CloseUI(0u);
		NetworkManager.Instance.SendCacheData(ServerType.Data, new BattleReconnCacheConfirmReq());
		InstanceManager.IsAIThinking = true;
		XInputManager.EnabledLogic = true;
	}

	protected void AnyReconnectDataFailed()
	{
		Debug.Log("连接服务器失败，请稍后重连");
		if (this.DataServerReconnectHandler.NextTime == 0u)
		{
			this.GetReconnectDataServerResult(false);
		}
		else
		{
			this.dataServerWaitTimer = TimerHeap.AddTimer(this.DataServerReconnectHandler.NextTime, 0, delegate
			{
				this.GetReconnectDataServerResult(false);
			});
		}
	}

	protected void ReconnectToChatServer(IPEndPoint ipEndPoint)
	{
		TimerHeap.DelTimer(this.chatServerWaitTimer);
		this.TryShowWaitReconnectResultUI(true, 0);
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621274, false), 1f, 1f);
		this.chatServerWaitTimer = TimerHeap.AddTimer(5000u, 0, new Action(this.AnyReconnectChatFailed));
		NetworkManager.Instance.ConnectChatServer(ipEndPoint, new Action(this.OnChatServerReconnectSuccess), new Action(this.AnyReconnectChatFailed));
	}

	protected void OnChatServerReconnectSuccess()
	{
		this.VerifyReconnectChat();
	}

	protected void VerifyReconnectChat()
	{
		LoginManager.Instance.SendLoginChat(new Action(this.OnVerifyReconnectChatSuccess), new Action(this.AnyReconnectChatFailed));
	}

	protected void OnVerifyReconnectChatSuccess()
	{
		TimerHeap.DelTimer(this.chatServerWaitTimer);
		this.IsProcessingReconnect = false;
		this.TryShowWaitReconnectResultUI(false, 0);
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621274, false), 1f, 1f);
		this.CheckWaitingReconnectList();
	}

	protected void AnyReconnectChatFailed()
	{
		TimerHeap.DelTimer(this.chatServerWaitTimer);
		this.IsProcessingReconnect = false;
		this.TryShowWaitReconnectResultUI(false, 0);
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621274, false), 1f, 1f);
		this.CheckWaitingReconnectList();
	}

	protected void TryShowWaitReconnectResultUI(bool isShow, int currentReconnectCount = 0)
	{
		if (isShow)
		{
			if (currentReconnectCount == 0)
			{
				return;
			}
			EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", true);
			if (this.waitConnectUI == null)
			{
				this.waitConnectUI = (UIManagerControl.Instance.OpenUI("WaitingConnect", UINodesManager.T3RootOfSpecial, false, UIType.NonPush) as WaitingConnectUI);
			}
			this.waitConnectUI.Show(true);
			if (currentReconnectCount <= ReconnectManager.AutoReconnectTimes)
			{
				this.waitConnectUI.SetLineName(currentReconnectCount);
			}
			else
			{
				this.waitConnectUI.SetLineName(0);
			}
		}
		else
		{
			EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", false);
			if (this.waitConnectUI == null)
			{
				this.waitConnectUI = (UIManagerControl.Instance.OpenUI("WaitingConnect", UINodesManager.T3RootOfSpecial, false, UIType.NonPush) as WaitingConnectUI);
			}
			this.waitConnectUI.Show(false);
		}
	}

	protected void AutoReconnectData()
	{
		this.ReconnectToDataServer(this.currentReconnectIEEndPoint, this.DataServerReconnectHandler.GetNextCount(this.ReconnectDataServerCount));
	}

	protected void ConfirmReconnectData()
	{
		this.TryShowWaitReconnectResultUI(false, 0);
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505147, false), delegate
		{
			this.ReconnectToDataServer(this.currentReconnectIEEndPoint, this.ReconnectDataServerCount + 1);
		}, GameDataUtils.GetChineseContent(621276, false), "button_orange_1");
	}

	protected void ChooseReconnectData()
	{
		this.TryShowWaitReconnectResultUI(false, 0);
		NetworkDialogUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505147, false), delegate
		{
			ClientApp.Instance.ReInit();
		}, delegate
		{
			this.ReconnectToDataServer(this.currentReconnectIEEndPoint, this.ReconnectDataServerCount + 1);
		}, GameDataUtils.GetChineseContent(621275, false), GameDataUtils.GetChineseContent(621276, false), "button_orange_1", "button_yellow_1");
	}

	protected void ConfirmReconnectDataNoToken()
	{
		this.TryShowWaitReconnectResultUI(false, 0);
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621277, false), delegate
		{
			ClientApp.Instance.ReInit();
		}, GameDataUtils.GetChineseContent(621275, false), "button_orange_1");
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("EnableReconnect: ");
		stringBuilder.Append(this.EnableReconnect);
		stringBuilder.Append("  ");
		stringBuilder.Append("IsProcessingReconnect: ");
		stringBuilder.Append(this.IsProcessingReconnect);
		stringBuilder.Append("  ");
		stringBuilder.Append("waitingReconnectList:[");
		for (int i = 0; i < this.waitingReconnectList.get_Count(); i++)
		{
			stringBuilder.Append(this.waitingReconnectList.get_Item(i).serverType + " " + this.waitingReconnectList.get_Item(i).serverType);
			stringBuilder.Append(",");
		}
		stringBuilder.Append("]  ");
		stringBuilder.Append("IsReconnectingDataServer: ");
		stringBuilder.Append(this.IsReconnectingDataServer);
		stringBuilder.Append("  ");
		stringBuilder.Append("DataServerReconnectHandler: ");
		stringBuilder.Append(this.DataServerReconnectHandler);
		stringBuilder.Append("  ");
		stringBuilder.Append("ReconnectDataServerCount: ");
		stringBuilder.Append(this.ReconnectDataServerCount);
		stringBuilder.Append("  ");
		stringBuilder.Append("IsSendingDataServerVerify: ");
		stringBuilder.Append(this.IsSendingDataServerVerify);
		stringBuilder.Append("  ");
		stringBuilder.Append("\n");
		return stringBuilder.ToString();
	}
}
