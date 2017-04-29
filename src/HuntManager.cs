using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class HuntManager : BaseSubSystemManager
{
	public List<RoomUiInfo> NormalRoomInfos;

	public List<RoomUiInfo> ChaosRoomInfos;

	public List<RoomUiInfo> VipRoomInfos;

	private int[] mTempId = new int[4];

	private uint mNormalRoomCDId;

	private uint mChaosRoomCDId;

	private uint mVipRoomCDId;

	public string OpenTime;

	public string CloseTime;

	public int BuyMinTime;

	public int RefreshTime = 5;

	public int RoomMaxPlayer;

	public int BuyTimePrice;

	public int CanBuyTimes;

	private static HuntManager instance;

	public int RemainTime
	{
		get;
		private set;
	}

	public int DayBuyTimes
	{
		get;
		private set;
	}

	public int MapId
	{
		get;
		private set;
	}

	public int AreaId
	{
		get;
		private set;
	}

	public int RoomId
	{
		get;
		private set;
	}

	public int EnterTick
	{
		get;
		private set;
	}

	public int AreaType
	{
		get;
		private set;
	}

	public int NormalRoomCD
	{
		get;
		private set;
	}

	public int ChaosRoomCD
	{
		get;
		private set;
	}

	public int VipRoomCD
	{
		get;
		private set;
	}

	public static HuntManager Instance
	{
		get
		{
			if (HuntManager.instance == null)
			{
				HuntManager.instance = new HuntManager();
			}
			return HuntManager.instance;
		}
	}

	public string RoomName
	{
		get
		{
			return this.GetRoomName(this.MapId, this.AreaId, this.RoomId);
		}
	}

	public List<RoomUiInfo> CurRoomInfos
	{
		get
		{
			if (this.AreaType == 1)
			{
				return this.NormalRoomInfos;
			}
			if (this.AreaType == 2)
			{
				return this.ChaosRoomInfos;
			}
			if (this.AreaType == 3)
			{
				return this.VipRoomInfos;
			}
			return null;
		}
	}

	public int CurRoomCD
	{
		get
		{
			if (this.AreaType == 1)
			{
				return this.NormalRoomCD;
			}
			if (this.AreaType == 2)
			{
				return this.ChaosRoomCD;
			}
			if (this.AreaType == 3)
			{
				return this.VipRoomCD;
			}
			return 0;
		}
		set
		{
			this.SetCDByType(this.AreaType, value);
		}
	}

	public uint CurRoomCDId
	{
		get
		{
			if (this.AreaType == 1)
			{
				return this.mNormalRoomCDId;
			}
			if (this.AreaType == 2)
			{
				return this.mChaosRoomCDId;
			}
			if (this.AreaType == 3)
			{
				return this.mVipRoomCDId;
			}
			return 0u;
		}
	}

	private HuntManager()
	{
	}

	public override void Release()
	{
		this.RemainTime = 0;
		this.DayBuyTimes = 0;
		this.MapId = 0;
		this.AreaId = 0;
		this.RoomId = 0;
		this.EnterTick = 0;
		this.NormalRoomInfos = null;
		this.ChaosRoomInfos = null;
		this.VipRoomInfos = null;
		for (int i = 0; i < this.mTempId.Length; i++)
		{
			this.mTempId[i] = 0;
		}
		this.ClearCD();
		this.ClearOtherData();
	}

	public override void Init()
	{
		base.Init();
		this.SetOtherData();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<HookMiscPush>(new NetCallBackMethod<HookMiscPush>(this.OnHookMiscPush));
		NetworkManager.AddListenEvent<GetMapUiInfoRes>(new NetCallBackMethod<GetMapUiInfoRes>(this.OnGetMapUiInfoRes));
		NetworkManager.AddListenEvent<EnterMapUiRes>(new NetCallBackMethod<EnterMapUiRes>(this.OnEnterMapUiRes));
		NetworkManager.AddListenEvent<OpenRoomRes>(new NetCallBackMethod<OpenRoomRes>(this.OnOpenRoomRes));
		NetworkManager.AddListenEvent<BuyHookTimeRes>(new NetCallBackMethod<BuyHookTimeRes>(this.OnBuyHookTimeRes));
		NetworkManager.AddListenEvent<EnterRoomRes>(new NetCallBackMethod<EnterRoomRes>(this.OnEnterRoomRes));
		NetworkManager.AddListenEvent<ExitRoomRes>(new NetCallBackMethod<ExitRoomRes>(this.OnExitRoomRes));
	}

	private void OnHookMiscPush(short state, HookMiscPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.RemainTime = down.remainTimeSec;
		this.DayBuyTimes = down.dayBuyTimes;
		EventDispatcher.Broadcast(EventNames.HuntInfosPush);
	}

	public void SendGetMapUiInfoReq()
	{
		NetworkManager.Send(new GetMapUiInfoReq(), ServerType.Data);
	}

	private void OnGetMapUiInfoRes(short state, GetMapUiInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void SendEnterMapUiReq(int map_id, int area_id, int area_type)
	{
		this.mTempId[0] = map_id;
		this.mTempId[1] = area_id;
		this.mTempId[3] = area_type;
		this.SetCDByType(area_type, this.RefreshTime);
		NetworkManager.Send(new EnterMapUiReq
		{
			mapId = map_id,
			areaId = area_id
		}, ServerType.Data);
		Debug.Log("请求区域[<color=#ffffff>" + area_id + "</color>]房间列表");
	}

	private void OnEnterMapUiRes(short state, EnterMapUiRes down = null)
	{
		if (state != 0)
		{
			this.CurRoomCD = 0;
			TimerHeap.DelTimer(this.CurRoomCDId);
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.MapId = this.mTempId[0];
		this.AreaId = this.mTempId[1];
		this.AreaType = this.mTempId[3];
		switch (this.AreaType)
		{
		case 1:
			this.NormalRoomInfos = down.roomUiInfo;
			TimerHeap.DelTimer(this.mNormalRoomCDId);
			this.mNormalRoomCDId = TimerHeap.AddTimer(1000u, 1000, new Action(this.NormalRoomTick));
			break;
		case 2:
			this.ChaosRoomInfos = down.roomUiInfo;
			TimerHeap.DelTimer(this.mChaosRoomCDId);
			this.mChaosRoomCDId = TimerHeap.AddTimer(1000u, 1000, new Action(this.ChaosRoomTick));
			break;
		case 3:
			this.VipRoomInfos = down.roomUiInfo;
			TimerHeap.DelTimer(this.mVipRoomCDId);
			this.mVipRoomCDId = TimerHeap.AddTimer(1000u, 1000, new Action(this.VipRoomTick));
			break;
		}
		Debug.Log(string.Concat(new object[]
		{
			"返回区域[<color=#ffffff>",
			this.MapId,
			"-",
			this.AreaId,
			"</color>]房间列表"
		}));
		EventDispatcher.Broadcast(EventNames.GetHuntRoomList);
	}

	public void SendOpenRoomReq(int map_id, int area_id, int room_id)
	{
		this.mTempId[0] = map_id;
		this.mTempId[1] = area_id;
		this.mTempId[2] = room_id;
		NetworkManager.Send(new OpenRoomReq
		{
			mapId = map_id,
			areaId = area_id,
			roomId = room_id
		}, ServerType.Data);
		Debug.Log("请求打开房间[<color=#ffffff>" + room_id + "</color>]");
	}

	private void OnOpenRoomRes(short state, OpenRoomRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.MapId = this.mTempId[0];
		this.AreaId = this.mTempId[1];
		this.RoomId = this.mTempId[2];
	}

	public void SendBuyHookTimeReq()
	{
		NetworkManager.Send(new BuyHookTimeReq(), ServerType.Data);
		Debug.Log("请求购买挂机时间");
	}

	private void OnBuyHookTimeRes(short state, BuyHookTimeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void SendEnterRoomReq(int area_id, int room_id)
	{
		this.mTempId[1] = area_id;
		this.mTempId[2] = room_id;
		NetworkManager.Send(new EnterRoomReq
		{
			area = area_id,
			roomId = room_id
		}, ServerType.Data);
		Debug.Log("请求进入房间[<color=#ffffff>" + room_id + "</color>]");
	}

	private void OnEnterRoomRes(short state, EnterRoomRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.AreaId = this.mTempId[1];
		this.RoomId = this.mTempId[2];
		this.EnterTick = down.deadlineTime;
		Debug.Log("进入房间[<color=#ffffff>" + this.RoomId + "</color>]成功!");
	}

	public void SendExitRoomReq()
	{
		NetworkManager.Send(new ExitRoomReq(), ServerType.Data);
		Debug.Log("请求退出房间[<color=#ffffff>" + this.RoomId + "</color>]");
	}

	private void OnExitRoomRes(short state, ExitRoomRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("退出房间[<color=#ffffff>" + this.RoomId + "</color>]成功!");
	}

	private void SetOtherData()
	{
		this.OpenTime = this.GetStringOtherData("startTime");
		this.CloseTime = this.GetStringOtherData("endTime");
		this.RefreshTime = this.GetIntOtherData("refreshInterval");
		this.RoomMaxPlayer = this.GetIntOtherData("roomsLimit");
		this.BuyTimePrice = this.GetIntOtherData("buyPrice");
		this.CanBuyTimes = this.GetIntOtherData("buyTime");
		this.BuyMinTime = this.GetIntOtherData("buyMin");
	}

	private void ClearOtherData()
	{
		this.OpenTime = string.Empty;
		this.CloseTime = string.Empty;
		this.RefreshTime = 0;
		this.RoomMaxPlayer = 0;
		this.BuyTimePrice = 0;
		this.CanBuyTimes = 0;
		this.BuyMinTime = 0;
	}

	public int GetIntOtherData(string key)
	{
		float num = 0f;
		GuaJiJiChuSheZhi guaJiJiChuSheZhi = DataReader<GuaJiJiChuSheZhi>.Get(key);
		if (guaJiJiChuSheZhi != null && float.TryParse(guaJiJiChuSheZhi.value, ref num))
		{
			return (int)num;
		}
		return 0;
	}

	public string GetStringOtherData(string key)
	{
		GuaJiJiChuSheZhi guaJiJiChuSheZhi = DataReader<GuaJiJiChuSheZhi>.Get(key);
		if (guaJiJiChuSheZhi != null)
		{
			return guaJiJiChuSheZhi.value;
		}
		return string.Empty;
	}

	private void NormalRoomTick()
	{
		this.NormalRoomCD--;
		if (this.NormalRoomCD <= 0)
		{
			this.NormalRoomCD = 0;
			TimerHeap.DelTimer(this.mNormalRoomCDId);
			this.mNormalRoomCDId = 0u;
		}
	}

	private void ChaosRoomTick()
	{
		this.ChaosRoomCD--;
		if (this.ChaosRoomCD <= 0)
		{
			this.ChaosRoomCD = 0;
			TimerHeap.DelTimer(this.mChaosRoomCDId);
			this.mChaosRoomCDId = 0u;
		}
	}

	private void VipRoomTick()
	{
		this.VipRoomCD--;
		if (this.VipRoomCD <= 0)
		{
			this.VipRoomCD = 0;
			TimerHeap.DelTimer(this.mVipRoomCDId);
			this.mVipRoomCDId = 0u;
		}
	}

	private void SetCDByType(int type, int cd)
	{
		if (type == 1)
		{
			this.NormalRoomCD = cd;
		}
		else if (type == 2)
		{
			this.ChaosRoomCD = cd;
		}
		else if (type == 3)
		{
			this.VipRoomCD = cd;
		}
	}

	public string GetRoomName(int map, int area, int room)
	{
		string text = this.GetRoomName(map, area);
		if (room > 0)
		{
			text += string.Format(GameDataUtils.GetChineseContent(511607, false), room % area);
		}
		return text;
	}

	public string GetRoomName(int map, int area)
	{
		string text = string.Empty;
		GuaJiDiTuPeiZhi guaJiDiTuPeiZhi = DataReader<GuaJiDiTuPeiZhi>.Get(map);
		if (guaJiDiTuPeiZhi != null)
		{
			text = text + GameDataUtils.GetChineseContent(guaJiDiTuPeiZhi.name, false) + " ";
		}
		GuaJiQuYuPeiZhi guaJiQuYuPeiZhi = DataReader<GuaJiQuYuPeiZhi>.Get(area);
		if (guaJiQuYuPeiZhi != null)
		{
			text = text + GameDataUtils.GetChineseContent(511603 + guaJiQuYuPeiZhi.areaType, false) + " ";
		}
		return text;
	}

	public void SetAreaData(GuaJiQuYuPeiZhi areaData)
	{
		this.MapId = areaData.setMap;
		this.AreaId = areaData.id;
		this.AreaType = areaData.areaType;
	}

	public void ClearCD()
	{
		this.AreaType = 0;
		this.NormalRoomCD = 0;
		this.ChaosRoomCD = 0;
		this.VipRoomCD = 0;
		TimerHeap.DelTimer(this.mNormalRoomCDId);
		this.mNormalRoomCDId = 0u;
		TimerHeap.DelTimer(this.mChaosRoomCDId);
		this.mChaosRoomCDId = 0u;
		TimerHeap.DelTimer(this.mVipRoomCDId);
		this.mVipRoomCDId = 0u;
	}
}
