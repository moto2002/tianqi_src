using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class TramcarManager : BaseSubSystemManager
{
	private bool mLootRefreshLock;

	private uint mLootRefreshLockId;

	private bool mInviteLock;

	private uint mInviteLockId;

	private long mLastFriendId;

	public int BeLootLimit;

	public int LootDeduct;

	public int UseDiamond;

	public int BastTramcarVip;

	public int LootTimeLimit;

	public int LootFightTime;

	public string[] TRAMCAR_NAME;

	private static TramcarManager instance;

	public int CurMapId
	{
		get;
		private set;
	}

	public int CurQuality
	{
		get;
		private set;
	}

	public int CurLootQulity
	{
		get;
		private set;
	}

	public bool IsProtecting
	{
		get;
		private set;
	}

	public int LootCountDown
	{
		get;
		private set;
	}

	public bool IsDontShowAgainBeInvite
	{
		get;
		set;
	}

	public List<InviteProtectNty> InviteMessage
	{
		get;
		private set;
	}

	public bool LootRefreshLock
	{
		get
		{
			return this.mLootRefreshLock;
		}
		private set
		{
			this.mLootRefreshLock = value;
			if (this.mLootRefreshLockId != 0u)
			{
				TimerHeap.DelTimer(this.mLootRefreshLockId);
				this.mLootRefreshLockId = 0u;
			}
			if (this.mLootRefreshLock)
			{
				this.mLootRefreshLockId = TimerHeap.AddTimer(5000u, 0, delegate
				{
					this.mLootRefreshLock = false;
				});
			}
		}
	}

	public bool InviteLock
	{
		get
		{
			return this.mInviteLock;
		}
		private set
		{
			this.mInviteLock = value;
			if (this.mInviteLockId != 0u)
			{
				TimerHeap.DelTimer(this.mInviteLockId);
				this.mInviteLockId = 0u;
			}
			if (this.mInviteLock)
			{
				this.mInviteLockId = TimerHeap.AddTimer(1000u, 0, delegate
				{
					this.mInviteLock = false;
				});
			}
		}
	}

	public ProtectFightInfo FightInfo
	{
		get;
		private set;
	}

	public List<DropItem> TramcarRewards
	{
		get;
		private set;
	}

	public List<TramcarRoomInfo> TramcarLootInfos
	{
		get;
		private set;
	}

	public List<FriendProtectFightInfo> TramcarFriendInfos
	{
		get;
		private set;
	}

	public static TramcarManager Instance
	{
		get
		{
			if (TramcarManager.instance == null)
			{
				TramcarManager.instance = new TramcarManager();
			}
			return TramcarManager.instance;
		}
	}

	private TramcarManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.InviteMessage = new List<InviteProtectNty>();
		this.SetTaskOtherData();
		if (this.TRAMCAR_NAME == null)
		{
			this.TRAMCAR_NAME = new string[]
			{
				string.Empty,
				GameDataUtils.GetChineseContent(513667, false),
				GameDataUtils.GetChineseContent(513668, false),
				GameDataUtils.GetChineseContent(513669, false),
				GameDataUtils.GetChineseContent(513670, false),
				GameDataUtils.GetChineseContent(513671, false)
			};
		}
	}

	public override void Release()
	{
		this.CurMapId = 0;
		this.CurQuality = 0;
		this.CurLootQulity = 0;
		this.IsProtecting = false;
		this.LootCountDown = 0;
		this.TramcarRewards = null;
		this.FightInfo = null;
		this.TramcarLootInfos = null;
		this.TramcarFriendInfos = null;
		this.InviteMessage.Clear();
		this.IsDontShowAgainBeInvite = false;
		this.ClearTaskOtherData();
	}

	private void SetTaskOtherData()
	{
		this.BeLootLimit = this.GetIntOtherData("oncefightlimits");
		this.LootDeduct = this.GetIntOtherData("RewardReductionPCT");
		this.UseDiamond = this.GetIntOtherData("diamond");
		this.BastTramcarVip = this.GetIntOtherData("viplimits");
		this.LootTimeLimit = this.GetIntOtherData("fightCD");
		this.LootFightTime = this.GetIntOtherData("fighttime");
	}

	private void ClearTaskOtherData()
	{
		this.BeLootLimit = 0;
		this.LootDeduct = 0;
		this.UseDiamond = 0;
		this.BastTramcarVip = 0;
		this.LootTimeLimit = 0;
		this.LootFightTime = 0;
	}

	private int GetIntOtherData(string key)
	{
		float num = 0f;
		HuSongKuangCheXinXi huSongKuangCheXinXi = DataReader<HuSongKuangCheXinXi>.Get(key);
		if (huSongKuangCheXinXi != null && float.TryParse(huSongKuangCheXinXi.value.get_Item(0), ref num))
		{
			return (int)num;
		}
		return 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ProtectFightPush>(new NetCallBackMethod<ProtectFightPush>(this.OnProtectFightPush));
		NetworkManager.AddListenEvent<ResultProtectFightNty>(new NetCallBackMethod<ResultProtectFightNty>(this.OnResultProtectFightNty));
		NetworkManager.AddListenEvent<DungeonPanelInfoPush>(new NetCallBackMethod<DungeonPanelInfoPush>(this.OnDungeonPanelInfoPush));
		NetworkManager.AddListenEvent<InviteProtectNty>(new NetCallBackMethod<InviteProtectNty>(this.OnInviteProtectNty));
		NetworkManager.AddListenEvent<OpenTramcarPanelRes>(new NetCallBackMethod<OpenTramcarPanelRes>(this.OnOpenTramcarPanelRes));
		NetworkManager.AddListenEvent<OpenGrabPanelRes>(new NetCallBackMethod<OpenGrabPanelRes>(this.OnOpenGrabPanelRes));
		NetworkManager.AddListenEvent<EnterGrabRes>(new NetCallBackMethod<EnterGrabRes>(this.OnEnterGrabRes));
		NetworkManager.AddListenEvent<RefreshTramcarRes>(new NetCallBackMethod<RefreshTramcarRes>(this.OnRefreshTramcarRes));
		NetworkManager.AddListenEvent<GetFriendProtectListRes>(new NetCallBackMethod<GetFriendProtectListRes>(this.OnGetFriendProtectListRes));
		NetworkManager.AddListenEvent<InviteFriendRes>(new NetCallBackMethod<InviteFriendRes>(this.OnInviteFriendRes));
		NetworkManager.AddListenEvent<ProtectFightRes>(new NetCallBackMethod<ProtectFightRes>(this.OnProtectFightRes));
		NetworkManager.AddListenEvent<ExitProtectFightRes>(new NetCallBackMethod<ExitProtectFightRes>(this.OnExitProtectFightRes));
		NetworkManager.AddListenEvent<FriendProtectAnswerRes>(new NetCallBackMethod<FriendProtectAnswerRes>(this.OnFriendProtectAnswerRes));
		NetworkManager.AddListenEvent<DelFriendHelpRes>(new NetCallBackMethod<DelFriendHelpRes>(this.OnDelFriendHelpRes));
	}

	private void OnProtectFightPush(short state, ProtectFightPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.FightInfo = down.info;
		this.CurQuality = down.info.curQuality;
		EventDispatcher.Broadcast(EventNames.TramcarProtectFightNty);
		if (this.mLastFriendId != this.FightInfo.helpRoleId && this.FightInfo.helpRoleId != 0L)
		{
			EventDispatcher.Broadcast(EventNames.InviteFriendSuccess);
		}
		this.mLastFriendId = this.FightInfo.helpRoleId;
	}

	private void OnInviteProtectNty(short state, InviteProtectNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.LogFormat("接收到好友[{0}]护送矿车的邀请", new object[]
		{
			down.roleName
		});
		if (!this.InviteMessage.Exists((InviteProtectNty e) => e.roleId == down.roleId))
		{
			this.InviteMessage.Add(down);
		}
		EventDispatcher.Broadcast(EventNames.TramcarInviteFriendNty);
	}

	private void OnResultProtectFightNty(short state, ResultProtectFightNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			TramcarInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnDungeonPanelInfoPush(short state, DungeonPanelInfoPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast<int>(EventNames.TramcarLootChangeNty, down.beGrabTimes);
	}

	public void SendOpenTramcarPanelReq(int mapid)
	{
		WaitUI.OpenUI(6000u);
		Debug.LogFormat("请求[{0}]矿车列表", new object[]
		{
			mapid
		});
		NetworkManager.Send(new OpenTramcarPanelReq
		{
			mapId = mapid
		}, ServerType.Data);
	}

	private void OnOpenTramcarPanelRes(short state, OpenTramcarPanelRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.TramcarRewards = down.item;
		this.CurQuality = down.curQuality;
		this.CurMapId = down.mapId;
		EventDispatcher.Broadcast(EventNames.OpenTramcarUI);
		Debug.LogFormat("护送矿车地图ID:{0}, 矿车品质:{1}", new object[]
		{
			this.CurMapId,
			this.CurQuality
		});
	}

	public void SendOpenGrabPanelReq(int mapid)
	{
		if (!this.LootRefreshLock)
		{
			WaitUI.OpenUI(6000u);
			Debug.LogFormat("请求抢夺[{0}]矿车列表", new object[]
			{
				mapid
			});
			this.LootRefreshLock = true;
			NetworkManager.Send(new OpenGrabPanelReq
			{
				mapId = mapid
			}, ServerType.Data);
		}
	}

	private void OnOpenGrabPanelRes(short state, OpenGrabPanelRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.TramcarLootInfos = down.infoList;
		this.LootCountDown = down.lastGrabTime;
		EventDispatcher.Broadcast(EventNames.OpenTramcarLootList);
		Debug.Log("返回抢夺矿车列表");
	}

	public void SendEnterGrabReq(int map_id, long role_id, int quality)
	{
		Debug.LogFormat("请求抢夺地图[{0}]玩家[{1}]的矿车", new object[]
		{
			map_id,
			role_id
		});
		this.CurLootQulity = quality;
		this.IsProtecting = false;
		NetworkManager.Send(new EnterGrabReq
		{
			mapId = map_id,
			roleId = role_id
		}, ServerType.Data);
	}

	private void OnEnterGrabRes(short state, EnterGrabRes down = null)
	{
		if (state != 0)
		{
			this.CurLootQulity = 0;
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("返回抢夺玩家的矿车");
	}

	public void SendRefreshTramcarReq(int quality = 0, bool isUseDiamond = false)
	{
		Debug.Log((quality <= 0) ? "请求刷新矿车" : ("请求刷新矿车品质:" + quality));
		NetworkManager.Send(new RefreshTramcarReq
		{
			targetQuality = quality,
			useDiamond = isUseDiamond
		}, ServerType.Data);
	}

	private void OnRefreshTramcarRes(short state, RefreshTramcarRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
		}
		if (down != null && down.curQuality > 0)
		{
			int curQuality = this.CurQuality;
			this.CurQuality = down.curQuality;
			EventDispatcher.Broadcast<bool>(EventNames.RefreshTramcarUI, curQuality != this.CurQuality);
			Debug.LogFormat("护送矿车地图ID:{0}, 矿车品质:{1}", new object[]
			{
				this.CurMapId,
				this.CurQuality
			});
		}
	}

	public void SendGetFriendProtectListReq()
	{
		WaitUI.OpenUI(6000u);
		Debug.Log("请求矿车好友列表");
		NetworkManager.Send(new GetFriendProtectListReq(), ServerType.Data);
	}

	private void OnGetFriendProtectListRes(short state, GetFriendProtectListRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.TramcarFriendInfos = down.infoList;
		EventDispatcher.Broadcast(EventNames.OpenTramcarFriendList);
		Debug.Log("返回矿车好友列表");
	}

	public void SendInviteFriendReq(long role_id)
	{
		if (!this.InviteLock)
		{
			this.InviteLock = true;
			NetworkManager.Send(new InviteFriendReq
			{
				roleId = role_id
			}, ServerType.Data);
			Debug.Log("请求邀请好友:" + role_id);
		}
	}

	private void OnInviteFriendRes(short state, InviteFriendRes down = null)
	{
		if (down != null && down.cdTime > 0)
		{
			UIManagerControl.Instance.ShowToastText(string.Format("CD中，{0}秒后可邀请！", down.cdTime));
		}
		Debug.Log("返回邀请好友");
	}

	public void SendProtectFightReq(int map_id)
	{
		WaitUI.OpenUI(10000u);
		Debug.LogFormat("请求进入护送矿车[{0}]副本", new object[]
		{
			map_id
		});
		this.CurLootQulity = 0;
		this.IsProtecting = false;
		NetworkManager.Send(new ProtectFightReq
		{
			mapId = map_id
		}, ServerType.Data);
	}

	private void OnProtectFightRes(short state, ProtectFightRes down = null)
	{
		if (state != 0)
		{
			WaitUI.CloseUI(0u);
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("返回进入护送矿车副本");
	}

	public void SendExitProtectFightReq()
	{
		Debug.Log("请求退出护送矿车副本");
		NetworkManager.Send(new ExitProtectFightReq(), ServerType.Data);
	}

	private void OnExitProtectFightRes(short state, ExitProtectFightRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("返回退出护送矿车副本");
	}

	public void SendFriendProtectAnswerReq(long roleId, bool isDeal, string roleName = "")
	{
		Debug.LogFormat("您[{0}]了好友[{1}]的邀请护送", new object[]
		{
			(!isDeal) ? "拒绝" : "接受",
			roleName
		});
		this.CurLootQulity = 0;
		this.IsProtecting = isDeal;
		NetworkManager.Send(new FriendProtectAnswerReq
		{
			inviteRoleId = roleId,
			answerFlag = isDeal
		}, ServerType.Data);
	}

	private void OnFriendProtectAnswerRes(short state, FriendProtectAnswerRes down = null)
	{
		if (state != 0)
		{
			this.IsProtecting = false;
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.IsProtecting)
		{
			this.InviteMessage.Clear();
		}
		EventDispatcher.Broadcast<bool>(EventNames.TramcarInviteAnswerRes, this.IsProtecting);
		Debug.Log("返回答复好友邀请护送");
	}

	public void SendDelFriendHelpReq()
	{
		Debug.Log("请求剔除护送矿车队友");
		NetworkManager.Send(new DelFriendHelpReq(), ServerType.Data);
	}

	private void OnDelFriendHelpRes(short state, DelFriendHelpRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.FightInfo.helpRoleId = 0L;
		EventDispatcher.Broadcast(EventNames.TramcarProtectFightNty);
		Debug.Log("返回剔除护送矿车队友");
	}
}
