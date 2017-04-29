using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class CityManager
{
	protected const int GuildFieldSceneID = 4000;

	private static CityManager instance;

	protected List<int> allCityScenes = new List<int>();

	protected List<int> openCityID = new List<int>();

	private bool mNeedDelayEnterNPC;

	private uint mNeedDelayEnterNPCLockId;

	public bool NeedSwitchCity;

	public int UsedFlyShoeFreeTime;

	public bool DontShowFlyShoeAgain;

	public static CityManager Instance
	{
		get
		{
			if (CityManager.instance == null)
			{
				CityManager.instance = new CityManager();
			}
			return CityManager.instance;
		}
	}

	public int CurrentCityID
	{
		get
		{
			if (this.IsMainCityScene(MySceneManager.Instance.CurSceneID))
			{
				return this.SceneIDToMainCityID(MySceneManager.Instance.CurSceneID);
			}
			if (this.IsGuildFieldScene(MySceneManager.Instance.CurSceneID))
			{
				return this.SceneIDToGuildFieldID(MySceneManager.Instance.CurSceneID);
			}
			return 0;
		}
	}

	public ZhuChengPeiZhi CurrentCityData
	{
		get
		{
			int currentCityID = this.CurrentCityID;
			return (currentCityID != 0) ? DataReader<ZhuChengPeiZhi>.Get(currentCityID) : null;
		}
	}

	public bool NeedDelayEnterNPC
	{
		get
		{
			return this.mNeedDelayEnterNPC;
		}
		set
		{
			this.mNeedDelayEnterNPC = value;
			if (this.mNeedDelayEnterNPCLockId != 0u)
			{
				TimerHeap.DelTimer(this.mNeedDelayEnterNPCLockId);
				this.mNeedDelayEnterNPCLockId = 0u;
			}
			if (this.mNeedDelayEnterNPC)
			{
				this.mNeedDelayEnterNPCLockId = TimerHeap.AddTimer(30000u, 0, delegate
				{
					this.mNeedDelayEnterNPC = false;
				});
			}
		}
	}

	protected CityManager()
	{
	}

	public void Init()
	{
		this.allCityScenes.Add(10001);
		for (int i = 0; i < DataReader<ZhuChengPeiZhi>.DataList.get_Count(); i++)
		{
			this.allCityScenes.Add(DataReader<ZhuChengPeiZhi>.DataList.get_Item(i).scene);
		}
		this.allCityScenes.Add((int)float.Parse(DataReader<GlobalParams>.Get("starSceneId").value));
		this.AddListeners();
	}

	public void Release()
	{
		this.RemoveListeners();
		this.allCityScenes.Clear();
		this.openCityID.Clear();
		this.UsedFlyShoeFreeTime = 0;
		this.NeedDelayEnterNPC = false;
		this.DontShowFlyShoeAgain = false;
	}

	protected void AddListeners()
	{
		NetworkManager.AddListenEvent<OpenMainCityNty>(new NetCallBackMethod<OpenMainCityNty>(this.OpenMainCityNty));
		NetworkManager.AddListenEvent<NewMainCityNty>(new NetCallBackMethod<NewMainCityNty>(this.NewMainCityNty));
		EventDispatcher.AddListener(CityManagerEvent.EnterIntegrationHearth, new Callback(this.EnterIntegrationHearth));
		EventDispatcher.AddListener<int>(CityManagerEvent.ChangeCityByIntegrationHearth, new Callback<int>(this.ChangeCityByIntegrationHearth));
		EventDispatcher.AddListener<int>(HearthNPCBehavior.OnEnterNPC, new Callback<int>(this.EnterOneWayHearth));
		NetworkManager.AddListenEvent<EnterMainCityRes>(new NetCallBackMethod<EnterMainCityRes>(this.ChangeCityByIntegrationHearthRes));
		NetworkManager.AddListenEvent<MapTransportRes>(new NetCallBackMethod<MapTransportRes>(this.ChangeCityByOneWayHearthRes));
		EventDispatcher.AddListener(CityManagerEvent.EnterGuildField, new Callback(this.EnterGuildFieldReq));
		EventDispatcher.AddListener(CityManagerEvent.ExitGuildField, new Callback(this.ExitGuildFieldReq));
		NetworkManager.AddListenEvent<EnterGuildFieldRes>(new NetCallBackMethod<EnterGuildFieldRes>(this.EnterGuildFieldRes));
		NetworkManager.AddListenEvent<LeaveGuildFieldRes>(new NetCallBackMethod<LeaveGuildFieldRes>(this.ExitGuildFieldRes));
		NetworkManager.AddListenEvent<FlyShoeInfoNty>(new NetCallBackMethod<FlyShoeInfoNty>(this.OnFlyShoeInfoNty));
		NetworkManager.AddListenEvent<FlyShoeTransportRes>(new NetCallBackMethod<FlyShoeTransportRes>(this.OnFlyShoeTransportRes));
	}

	protected void RemoveListeners()
	{
		NetworkManager.RemoveListenEvent<OpenMainCityNty>(new NetCallBackMethod<OpenMainCityNty>(this.OpenMainCityNty));
		NetworkManager.RemoveListenEvent<NewMainCityNty>(new NetCallBackMethod<NewMainCityNty>(this.NewMainCityNty));
		EventDispatcher.RemoveListener(CityManagerEvent.EnterIntegrationHearth, new Callback(this.EnterIntegrationHearth));
		EventDispatcher.RemoveListener<int>(CityManagerEvent.ChangeCityByIntegrationHearth, new Callback<int>(this.ChangeCityByIntegrationHearth));
		EventDispatcher.RemoveListener<int>(HearthNPCBehavior.OnEnterNPC, new Callback<int>(this.EnterOneWayHearth));
		NetworkManager.RemoveListenEvent<EnterMainCityRes>(new NetCallBackMethod<EnterMainCityRes>(this.ChangeCityByIntegrationHearthRes));
		NetworkManager.RemoveListenEvent<MapTransportRes>(new NetCallBackMethod<MapTransportRes>(this.ChangeCityByOneWayHearthRes));
		EventDispatcher.RemoveListener(CityManagerEvent.EnterGuildField, new Callback(this.EnterGuildFieldReq));
		EventDispatcher.RemoveListener(CityManagerEvent.ExitGuildField, new Callback(this.ExitGuildFieldReq));
		NetworkManager.RemoveListenEvent<EnterGuildFieldRes>(new NetCallBackMethod<EnterGuildFieldRes>(this.EnterGuildFieldRes));
		NetworkManager.RemoveListenEvent<LeaveGuildFieldRes>(new NetCallBackMethod<LeaveGuildFieldRes>(this.ExitGuildFieldRes));
		NetworkManager.RemoveListenEvent<FlyShoeInfoNty>(new NetCallBackMethod<FlyShoeInfoNty>(this.OnFlyShoeInfoNty));
		NetworkManager.RemoveListenEvent<FlyShoeTransportRes>(new NetCallBackMethod<FlyShoeTransportRes>(this.OnFlyShoeTransportRes));
	}

	protected void OpenMainCityNty(short state, OpenMainCityNty down = null)
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
		for (int i = 0; i < down.ids.get_Count(); i++)
		{
			if (!this.openCityID.Contains(down.ids.get_Item(i)))
			{
				this.openCityID.Add(down.ids.get_Item(i));
			}
		}
	}

	protected void NewMainCityNty(short state, NewMainCityNty down = null)
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
		if (!this.openCityID.Contains(down.id))
		{
			this.openCityID.Add(down.id);
		}
	}

	protected void EnterIntegrationHearth()
	{
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.IsNavNeedChangeScene)
		{
			CitySelectUI citySelectUI = UIManagerControl.Instance.OpenUI("CitySelectUI", null, true, UIType.NonPush) as CitySelectUI;
			citySelectUI.UpdateData(this.CurrentCityID, this.openCityID);
			citySelectUI.OnUnitClick(EntityWorld.Instance.EntSelf.NavToScene);
		}
		else
		{
			CitySelectUI citySelectUI2 = UIManagerControl.Instance.OpenUI("CitySelectUI", null, true, UIType.NonPush) as CitySelectUI;
			citySelectUI2.UpdateData(this.CurrentCityID, this.openCityID);
		}
	}

	protected void ChangeCityByIntegrationHearth(int sceneID)
	{
		if (!MySceneManager.IsSceneResourceAvailable(sceneID))
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(3060001, false), GameDataUtils.GetChineseContent(3060002, false), delegate
			{
				UIManagerControl.Instance.HideUI("CitySelectUI");
				OperateActivityManager.Instance.CheckUpdateMaxLevel();
			}, GameDataUtils.GetChineseContent(3060003, false), "button_orange_1", null);
			return;
		}
		NetworkManager.Send(new EnterMainCityReq
		{
			cityId = sceneID
		}, ServerType.Data);
	}

	protected void ChangeCityByIntegrationHearthRes(short state, EnterMainCityRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	protected void EnterOneWayHearth(int hearthID)
	{
		this.ChangeCityByOneWayHearth(hearthID);
	}

	protected void ChangeCityByOneWayHearth(int hearthID)
	{
		if (!MySceneManager.Instance.IsSceneExist)
		{
			return;
		}
		NetworkManager.Send(new MapTransportReq
		{
			transportId = hearthID
		}, ServerType.Data);
	}

	protected void ChangeCityByOneWayHearthRes(short state, MapTransportRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	private void OnFlyShoeInfoNty(short state, FlyShoeInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.UsedFlyShoeFreeTime = down.todayFreeUseTimes;
	}

	public void SendFlyShoeTransport(int map_id, Pos map_pos)
	{
		MainTaskManager.Instance.StopToNPC(false);
		NetworkManager.Send(new FlyShoeTransportReq
		{
			mapId = map_id,
			pos = map_pos
		}, ServerType.Data);
		Debug.Log(string.Concat(new object[]
		{
			"请求小飞鞋传送, map:",
			map_id,
			", pos:",
			map_pos
		}));
	}

	private void OnFlyShoeTransportRes(short state, FlyShoeTransportRes down = null)
	{
		if (state != 0)
		{
			this.NeedDelayEnterNPC = false;
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast<bool>(EventNames.FlyShoeTransportRes, false);
			return;
		}
		EventDispatcher.Broadcast<bool>(EventNames.FlyShoeTransportRes, true);
		if (this.NeedSwitchCity)
		{
			InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
		}
		else if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.Actor.SendPrecisePos();
		}
	}

	protected void EnterGuildFieldReq()
	{
		NetworkManager.Send(new EnterGuildFieldReq(), ServerType.Data);
	}

	protected void EnterGuildFieldRes(short state, EnterGuildFieldRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	protected void ExitGuildFieldReq()
	{
		NetworkManager.Send(new LeaveGuildFieldReq(), ServerType.Data);
	}

	protected void ExitGuildFieldRes(short state, LeaveGuildFieldRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	public bool IsCityScene(int sceneID)
	{
		return this.IsMainCityScene(sceneID) || this.IsGuildFieldScene(sceneID);
	}

	protected bool IsMainCityScene(int sceneID)
	{
		return this.allCityScenes.Contains(sceneID);
	}

	protected int SceneIDToMainCityID(int sceneID)
	{
		return sceneID;
	}

	public bool IsGuildFieldScene(int sceneID)
	{
		return sceneID == 4000;
	}

	public bool IsGuildWarCityScene(int sceneID)
	{
		return DataReader<ZhuChengPeiZhi>.Contains(sceneID) && DataReader<ZhuChengPeiZhi>.Get(sceneID).mapType == 4;
	}

	protected int SceneIDToGuildFieldID(int sceneID)
	{
		return 4000;
	}
}
