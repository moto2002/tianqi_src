using Package;
using System;
using System.Net;
using UnityEngine;
using XNetwork;

public class NetworkManager
{
	protected enum KickType
	{
		Exception,
		FreezeID,
		ServerMaintenance
	}

	public const string OuterDNSName = "tqzm.453e.com";

	public const string OuterIP = "60.12.154.218";

	public const int OuterPort = 7600;

	public const string InnerIP = "172.19.8.194";

	public const int InnerPort = 7700;

	protected static NetworkManager instance;

	protected bool hasShowForceQuit;

	protected float handlePacketIntervel;

	protected float handlePacketCurrentTime;

	protected Type heartbeatPacketType = typeof(HeartBeatReq);

	protected HeartBeatReq heartbeatPacket = new HeartBeatReq();

	protected Type clientHeartbeatPacketType = typeof(PingReq);

	protected PingReq clientHeartbeatPacket = new PingReq();

	protected bool isEnableAck = true;

	protected bool isEnablePing = true;

	public static NetworkManager Instance
	{
		get
		{
			if (NetworkManager.instance == null)
			{
				NetworkManager.instance = new NetworkManager();
			}
			return NetworkManager.instance;
		}
	}

	public uint NetworkTimeoutTime
	{
		get
		{
			return 3000u;
		}
	}

	protected bool HasShowForceQuit
	{
		get
		{
			return this.hasShowForceQuit;
		}
		set
		{
			this.hasShowForceQuit = value;
		}
	}

	public float HandlePacketIntervel
	{
		get
		{
			return this.handlePacketIntervel;
		}
		set
		{
			this.handlePacketIntervel = value;
		}
	}

	protected float HandlePacketCurrentTime
	{
		get
		{
			return this.handlePacketCurrentTime;
		}
		set
		{
			this.handlePacketCurrentTime = value;
		}
	}

	public bool IsEnableAck
	{
		get
		{
			return this.isEnableAck;
		}
		set
		{
			this.isEnableAck = value;
		}
	}

	public bool IsEnablePing
	{
		get
		{
			return this.isEnablePing;
		}
		set
		{
			this.isEnablePing = value;
		}
	}

	public bool IsExistPing
	{
		get
		{
			return NetworkService.Instance.IsDataServerConnected;
		}
	}

	public int PingValue
	{
		get
		{
			return NetworkService.Instance.PingValue;
		}
	}

	protected NetworkManager()
	{
	}

	public void Init()
	{
		TokenManager.Instance.Init();
		this.HasShowForceQuit = false;
		NetworkService.Instance.OnErrorHandler = new NetworkService.OnError(this.OnNetSocketOnError);
		NetworkService.Instance.OnDisconnectHandler = new NetworkService.OnDisconnect(this.OnNetSocketDisconnect);
		this.TrueAddListenEvent<AbnormalDisconnectNty>(new NetCallBackMethod<AbnormalDisconnectNty>(this.OnServerNotifyDisconnect));
		this.TrueAddListenEvent<KickNty>(new NetCallBackMethod<KickNty>(this.OnKickNty));
		this.TrueAddListenEvent<AckNty>(new NetCallBackMethod<AckNty>(this.OnAckNty));
	}

	public void Release()
	{
		TokenManager.Instance.Release();
		this.TrueRemoveListenEvent<AbnormalDisconnectNty>(new NetCallBackMethod<AbnormalDisconnectNty>(this.OnServerNotifyDisconnect));
		this.TrueRemoveListenEvent<KickNty>(new NetCallBackMethod<KickNty>(this.OnKickNty));
		this.TrueRemoveListenEvent<AckNty>(new NetCallBackMethod<AckNty>(this.OnAckNty));
		NetworkService.Instance.OnErrorHandler = null;
		NetworkService.Instance.OnDisconnectHandler = null;
		this.isEnableAck = true;
		this.isEnablePing = true;
		this.HasShowForceQuit = false;
	}

	public static void AddListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
	{
		NetworkManager.Instance.TrueAddListenEvent<T>(callBack);
	}

	public static void RemoveListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
	{
		NetworkManager.Instance.TrueRemoveListenEvent<T>(callBack);
	}

	protected void TrueAddListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
	{
		NetworkService.Instance.AddListenEvent<T>(callBack);
	}

	protected void TrueRemoveListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
	{
		NetworkService.Instance.RemoveListenEvent<T>(callBack);
	}

	public void ConnectLoginServer()
	{
		Debug.Log("开始连接");
		LoginManager.Instance.GetServerListFile(LoginManager.AddressType.DOMAIN);
	}

	public void ConnectLoginOuterServer()
	{
		NetworkService.Instance.ConnectByDomain("tqzm.453e.com", 7600, ServerType.LoginOuter, delegate
		{
			LoginManager.Instance.GetDataServerList(ServerType.LoginOuter);
		}, delegate
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621260, false), 1f, 1f);
		});
	}

	public void ConnectLoginInnerServer()
	{
		NetworkService.Instance.ConnectByIP("172.19.8.194", 7700, ServerType.LoginInner, delegate
		{
			LoginManager.Instance.GetDataServerList(ServerType.LoginInner);
		}, delegate
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621260, false), 1f, 1f);
		});
	}

	public void ConnectDataServer(string ipString, int port, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
	{
		Debug.Log("ConnectDataServer");
		NetworkService.Instance.ConnectByIP(ipString, port, ServerType.Data, onConnectSuccessCallBack, onConnectFailedCallBack);
	}

	public void ConnectDataServer(IPEndPoint ipEndPoint, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
	{
		Debug.Log("ConnectDataServer");
		NetworkService.Instance.ConnectByIPEndPoint(ipEndPoint, ServerType.Data, onConnectSuccessCallBack, onConnectFailedCallBack);
	}

	public void ConnectChatServer(string ipString, int port, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
	{
		Debug.Log("ConnectChatServer");
		NetworkService.Instance.ConnectByIP(ipString, port, ServerType.Chat, onConnectSuccessCallBack, onConnectFailedCallBack);
	}

	public void ConnectChatServer(IPEndPoint ipEndPoint, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
	{
		Debug.Log("ConnectChatServer");
		NetworkService.Instance.ConnectByIPEndPoint(ipEndPoint, ServerType.Chat, onConnectSuccessCallBack, onConnectFailedCallBack);
	}

	public void ShutDownLoginServer()
	{
		Debug.Log("ShutDownLoginServer");
		this.ShutDownServer(ServerType.LoginInner);
		this.ShutDownServer(ServerType.LoginOuter);
	}

	public void ShutDownServer(ServerType serviceType = ServerType.Data)
	{
		Debug.Log("ShutDownServer: " + serviceType);
		NetworkService.Instance.Disconnect(serviceType);
	}

	public void ShutDownAllServer()
	{
		Debug.Log("ShutDownAllServer");
		NetworkService.Instance.DisconnectAll();
	}

	public void ShutDownAndReconnectAllServer()
	{
		Debug.Log("ShutDownAndReconnectAllServer");
		NetworkService.Instance.DisconnectAndReconnectAll();
	}

	protected void OnNetSocketOnError(ServerType serverType, string msg)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnNetSocketOnError",
			serverType,
			" ",
			msg
		}));
		if (serverType == ServerType.Data)
		{
			this.InfrastructureError();
		}
	}

	protected void OnNetSocketDisconnect(ServerType serverType)
	{
		Debug.Log("OnNetSocketDisconnect: " + serverType);
		if (serverType == ServerType.Data)
		{
			this.InfrastructureError();
		}
	}

	protected void InfrastructureError()
	{
		Loom.Current.QueueOnMainThread(delegate
		{
			this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621291, false), null);
		}, 0.5f);
	}

	protected void OnServerNotifyDisconnect(short state, AbnormalDisconnectNty down = null)
	{
		Debug.Log("OnGetServiceDisconnect: " + state);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		NetworkManager.Send(new AbnormalDisconnectAck(), ServerType.Data);
		AbnormalDisconnectNty.AbnormalType abnormalType = down.abnormalType;
		if (abnormalType != AbnormalDisconnectNty.AbnormalType.ClientTimeCheckFailure)
		{
			if (abnormalType != AbnormalDisconnectNty.AbnormalType.BackendMaintain)
			{
				if (abnormalType != AbnormalDisconnectNty.AbnormalType.ServerStop)
				{
					this.NetworkForceQuitApp(string.Format(GameDataUtils.GetChineseContent(this.GetServerNotifyDisconnectDesc(down.abnormalType), false), down.abnormalType), null);
				}
				else
				{
					this.ServerStop(down.countDownTime);
				}
			}
			else
			{
				this.ServerConfirmNotice(down.msg, null);
			}
		}
		else
		{
			TimeManager.Instance.IsOpenDebug = true;
			this.NetworkForceQuitApp(string.Format(GameDataUtils.GetChineseContent(this.GetServerNotifyDisconnectDesc(down.abnormalType), false), down.abnormalType), null);
		}
	}

	protected int GetServerNotifyDisconnectDesc(AbnormalDisconnectNty.AbnormalType abnormalType)
	{
		switch (abnormalType)
		{
		case AbnormalDisconnectNty.AbnormalType.Unknown:
			return 621151;
		case AbnormalDisconnectNty.AbnormalType.AnotherDeviceLogin:
			return 621152;
		case AbnormalDisconnectNty.AbnormalType.ServerLogicError:
			return 621153;
		case AbnormalDisconnectNty.AbnormalType.ServerStop:
			return 621154;
		case AbnormalDisconnectNty.AbnormalType.ClientSendPacketTooQuick:
			return 621156;
		case AbnormalDisconnectNty.AbnormalType.ServerNotRecPacketTooLong:
			return 621157;
		case AbnormalDisconnectNty.AbnormalType.ServerCachePacketTooMuch:
			return 621158;
		case AbnormalDisconnectNty.AbnormalType.SecureError:
			return 621159;
		case AbnormalDisconnectNty.AbnormalType.ClientTimeCheckFailure:
			return 621292;
		}
		return 621259;
	}

	protected void ServerStop(int second)
	{
		ReconnectManager.Instance.EnableReconnect = false;
		CloseServerUI closeServerUI = UIManagerControl.Instance.OpenUI("CloseServerTips", UINodesManager.T4RootOfSpecial, false, UIType.NonPush) as CloseServerUI;
		if (second > 5)
		{
			closeServerUI.SetTime(second - 5, delegate
			{
				UIManagerControl.Instance.HideUI("CloseServerTips");
				this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621308, false), null);
			});
		}
		else
		{
			this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621308, false), null);
		}
	}

	protected void OnKickNty(short state, KickNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		switch (down.typeId)
		{
		case 0:
			this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621262, false), GameDataUtils.GetChineseContent(621265, false));
			break;
		case 1:
			this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621263, false), GameDataUtils.GetChineseContent(621265, false));
			break;
		case 2:
			this.NetworkForceQuitApp(GameDataUtils.GetChineseContent(621306, false), GameDataUtils.GetChineseContent(621265, false));
			break;
		}
	}

	public static void Send(object data, ServerType serverType = ServerType.Data)
	{
		if (serverType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
		{
			Debug.Log("Send");
		}
		NetworkManager.Instance.TrueSend(data, serverType);
	}

	public static void Send(Type dataType, object data, ServerType serverType = ServerType.Data)
	{
		if (serverType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
		{
			Debug.Log("Send");
		}
		NetworkManager.Instance.TrueSend(dataType, data, serverType);
	}

	protected void TrueSend(object data, ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(data.GetType(), data, serverType, PacketType.Data, false);
	}

	protected void TrueSend(Type dataType, object data, ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(dataType, data, serverType, PacketType.Data, false);
	}

	public void SendHeartbeat(ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(this.heartbeatPacketType, this.heartbeatPacket, serverType, PacketType.Heartbeat, false);
	}

	public void SendClientHeartbeat(ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(this.heartbeatPacketType, this.heartbeatPacket, serverType, PacketType.Heartbeat, false);
	}

	public void SendVerify(object data, ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(data.GetType(), data, serverType, PacketType.Verify, false);
	}

	public void SendCacheData(ServerType serverType, object ackData = null)
	{
		NetworkService.Instance.SendCacheData(serverType, ackData);
	}

	public static void VitalSend(object data, ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(data.GetType(), data, serverType, PacketType.Data, NetworkManager.Instance.IsEnableAck);
	}

	public static void VitalSend(Type dataType, object data, ServerType serverType = ServerType.Data)
	{
		NetworkService.Instance.BeginSend(dataType, data, serverType, PacketType.Data, NetworkManager.Instance.IsEnableAck);
	}

	protected void OnAckNty(short state, AckNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			this.NetworkForceQuitApp("网络错误，请重新连接", "确认号断开");
			return;
		}
	}

	public void Update(float time)
	{
		if (this.HandlePacketIntervel == 0f)
		{
			NetworkService.Instance.ProcessPackets(time);
		}
		else
		{
			if (this.HandlePacketCurrentTime < this.HandlePacketIntervel)
			{
				this.HandlePacketCurrentTime += time;
				return;
			}
			NetworkService.Instance.ProcessPackets(this.HandlePacketCurrentTime);
			this.HandlePacketCurrentTime = 0f;
		}
	}

	public void RegistReconnectHandler(NetworkService.OnReconnect reconnectHandler)
	{
		NetworkService.Instance.OnReconnectHandler = reconnectHandler;
	}

	public void InitPing()
	{
		if (this.IsEnablePing)
		{
			NetworkService.Instance.InitPing();
		}
	}

	public void ServerConfirmNotice(string msg, string title = null)
	{
		if (string.IsNullOrEmpty(title))
		{
			title = GameDataUtils.GetChineseContent(621264, false);
		}
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(title, msg, delegate
		{
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
	}

	public void NetworkForceReInit(string msg, string title = null)
	{
		this.ShutDownAllServer();
		if (string.IsNullOrEmpty(title))
		{
			title = GameDataUtils.GetChineseContent(621264, false);
		}
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(title, msg, delegate
		{
			ClientApp.Instance.ReInit();
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
	}

	public void NetworkForceQuitApp(string msg, string title = null)
	{
		Debug.Log("NetworkForceQuitApp: title: " + title + " msg: " + msg);
		if (this.HasShowForceQuit)
		{
			return;
		}
		this.HasShowForceQuit = true;
		this.ShutDownAllServer();
		if (string.IsNullOrEmpty(title))
		{
			title = GameDataUtils.GetChineseContent(621264, false);
		}
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(title, msg, delegate
		{
			ClientApp.Instance.ReInit();
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
	}
}
