using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNetwork;

public class GangFightManager : BaseSubSystemManager
{
	public GangFightMatchRoleSummary gangFightMatchRoleSummary;

	public List<CombatWinRankingsItem> CombatWinRankingsItems = new List<CombatWinRankingsItem>();

	public List<FightRecord> FightRecord = new List<FightRecord>();

	public int combatWin;

	public int topCombatWin;

	public int totalWin;

	public int historyCombatWin;

	public GangFightBattleResult gangFightBattleResult;

	public string openTime = string.Empty;

	public string closeTime = string.Empty;

	public DateTime dateTimeOpen;

	public DateTime dateTimeClose;

	public bool open;

	private bool isMatching;

	public bool IsWinLastFight;

	private static GangFightManager m_instance;

	public DateTime severTime
	{
		get
		{
			return TimeManager.Instance.PreciseServerTime;
		}
	}

	public static GangFightManager Instance
	{
		get
		{
			if (GangFightManager.m_instance == null)
			{
				GangFightManager.m_instance = new GangFightManager();
			}
			return GangFightManager.m_instance;
		}
	}

	protected GangFightManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.gangFightMatchRoleSummary = null;
		this.CombatWinRankingsItems.Clear();
		this.FightRecord.Clear();
		this.open = false;
		this.IsWinLastFight = false;
		this.isMatching = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<StartGangFightingRes>(new NetCallBackMethod<StartGangFightingRes>(this.OnGetStartGangFightingRes));
		NetworkManager.AddListenEvent<QueryCombatWinRankingsInfoRes>(new NetCallBackMethod<QueryCombatWinRankingsInfoRes>(this.OnGetQueryCombatWinRankingsInfoRes));
		NetworkManager.AddListenEvent<QueryFightRecordInfoRes>(new NetCallBackMethod<QueryFightRecordInfoRes>(this.OnGetQueryFightRecordInfoRes));
		NetworkManager.AddListenEvent<CancelGangFightingRes>(new NetCallBackMethod<CancelGangFightingRes>(this.OnGetCancelGangFightingRes));
		NetworkManager.AddListenEvent<CombatWinRankingsInfo>(new NetCallBackMethod<CombatWinRankingsInfo>(this.OnGetCombatWinRankingsInfo));
		NetworkManager.AddListenEvent<FightRecordList>(new NetCallBackMethod<FightRecordList>(this.OnGetFightRecordList));
		NetworkManager.AddListenEvent<GangFightPersonalInfo>(new NetCallBackMethod<GangFightPersonalInfo>(this.OnGetGangFightPersonalInfo));
		NetworkManager.AddListenEvent<GangFightMatchRoleSummary>(new NetCallBackMethod<GangFightMatchRoleSummary>(this.OnGetGangFightMatchRoleSummary));
		NetworkManager.AddListenEvent<GangFightMatchFail>(new NetCallBackMethod<GangFightMatchFail>(this.OnGetGangFightMatchFail));
		NetworkManager.AddListenEvent<GangFightBattleResult>(new NetCallBackMethod<GangFightBattleResult>(this.OnGetGangFightBattleResult));
		NetworkManager.AddListenEvent<GangFightExitBattle>(new NetCallBackMethod<GangFightExitBattle>(this.OnGetGangFightExitBattle));
		NetworkManager.AddListenEvent<ExitFromGangFightFieldRes>(new NetCallBackMethod<ExitFromGangFightFieldRes>(this.OnExitFromGangFightFieldRes));
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
		EventDispatcher.AddListener(EventNames.ActivityAnnounce, new Callback(this.OnActivityAnnounce));
	}

	public void SendStartGangFight()
	{
		NetworkManager.Send(new StartGangFightingReq(), ServerType.Data);
	}

	public void QueryFightRecordInfo()
	{
		NetworkManager.Send(new QueryFightRecordInfoReq(), ServerType.Data);
	}

	public void QueryCombatWinRankingsInfo()
	{
		NetworkManager.Send(new QueryCombatWinRankingsInfoReq(), ServerType.Data);
	}

	public void CancelGangFighting()
	{
		NetworkManager.Send(new CancelGangFightingReq(), ServerType.Data);
	}

	public void SendExitFromGangFightFieldReq()
	{
		if (GangFightManager.Instance.gangFightBattleResult != null)
		{
			GangFightManager.Instance.gangFightBattleResult.winnerId = 0L;
		}
		NetworkManager.Send(new ExitFromGangFightFieldReq(), ServerType.Data);
	}

	private void OnGetStartGangFightingRes(short state, StartGangFightingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OnGetStartGangFightingRes();
	}

	private void OnGetQueryCombatWinRankingsInfoRes(short state, QueryCombatWinRankingsInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGetQueryFightRecordInfoRes(short state, QueryFightRecordInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGetCancelGangFightingRes(short state, CancelGangFightingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.OnGetCancelGangFightingRes);
	}

	private void OnGetCombatWinRankingsInfo(short state, CombatWinRankingsInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debuger.Info("state  " + state, new object[0]);
			this.CombatWinRankingsItems = down.items;
			EventDispatcher.Broadcast(EventNames.OnGetCombatWinRankingsInfo);
		}
	}

	private void OnGetFightRecordList(short state, FightRecordList down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debuger.Info("state  " + state, new object[0]);
			this.FightRecord = down.records;
			EventDispatcher.Broadcast(EventNames.OnGetFightRecordList);
		}
	}

	private void OnGetGangFightPersonalInfo(short state, GangFightPersonalInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.combatWin = down.combatWin;
			this.topCombatWin = down.topCombatWin;
			this.totalWin = down.totalWin;
			this.historyCombatWin = down.historyCombatWin;
			this.openTime = down.openTime;
			this.closeTime = down.closeTime;
			this.SetGangFightOpenTime();
			EventDispatcher.Broadcast(EventNames.OnGetGangFightPersonalInfo);
		}
	}

	private void OnGetGangFightMatchRoleSummary(short state, GangFightMatchRoleSummary down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.gangFightMatchRoleSummary = down;
			this.OpenGangFightMatchingFinishUI();
		}
	}

	private void OnGetGangFightMatchFail(short state, GangFightMatchFail down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OnGetGangFightMatchFail();
	}

	private void OnGetGangFightBattleResult(short state, GangFightBattleResult down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.gangFightBattleResult = down;
		if (!down.forceExist)
		{
			GangFightInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnGetGangFightExitBattle(short state, GangFightExitBattle down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.OnGetGangFightExitBattle);
	}

	private void OnExitFromGangFightFieldRes(short state, ExitFromGangFightFieldRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void LoadSceneEnd(int sceneID)
	{
		if (!MySceneManager.IsMainScene(sceneID))
		{
			return;
		}
		if (!GangFightManager.Instance.IsWinLastFight)
		{
			return;
		}
		this.IsWinLastFight = false;
		GangFightManager.Instance.SendStartGangFight();
	}

	private void OnActivityAnnounce()
	{
		if (this.open && !ActivityCenterManager.Instance.CheckActivityIsOpen(10001))
		{
			EventDispatcher.Broadcast(EventNames.CloseGangfight);
			if (this.isMatching)
			{
				this.OnGetGangFightMatchFail();
			}
			this.open = false;
		}
		if (!this.open && ActivityCenterManager.Instance.CheckActivityIsOpen(10001))
		{
			this.open = true;
		}
	}

	public void OnGetStartGangFightingRes()
	{
		if (this.CheckGangFightIsOpen())
		{
			(UIManagerControl.Instance.OpenUI("MatchUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MatchUI).SetDataCanle(10000, true, delegate
			{
				GangFightManager.Instance.CancelGangFighting();
				this.isMatching = false;
			}, delegate
			{
				GangFightManager.Instance.CancelGangFighting();
				this.isMatching = false;
			});
			this.isMatching = true;
		}
	}

	private void OnGetGangFightMatchFail()
	{
		(UIManagerControl.Instance.GetUIIfExist("MatchUI") as MatchUI).CloseMatchUI();
		this.isMatching = false;
	}

	private void OpenGangFightMatchingFinishUI()
	{
		this.isMatching = false;
		UIManagerControl.Instance.UnLoadUIPrefab("MatchingTimeCalUI");
		(UIManagerControl.Instance.OpenUI("MatchUI", null, false, UIType.NonPush) as MatchUI).CloseMatchUI();
		GangFightMatchingFinishUI gangFightMatchingFinishUI = UIManagerControl.Instance.OpenUI("GangFightMatchingFinishUI", null, false, UIType.NonPush) as GangFightMatchingFinishUI;
		gangFightMatchingFinishUI.RefreshUI();
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		for (int i = 0; i < 3; i++)
		{
			gangFightMatchingFinishUI.MyPets[i].SetActive(false);
		}
		if (petFormation != null && petFormation.petFormationArr != null && petFormation.petFormationArr.Int64Array != null)
		{
			for (int j = 0; j < petFormation.petFormationArr.Int64Array.get_Count(); j++)
			{
				Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(j);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
				Pet pet = DataReader<Pet>.Get(petInfo.petId);
				ResourceManager.SetSprite(gangFightMatchingFinishUI.MyPets[int64IndexValue.index].get_transform().Find("Icon").GetComponent<Image>(), PetManager.Instance.GetSelfPetIcon(pet));
				ResourceManager.SetSprite(gangFightMatchingFinishUI.MyPets[int64IndexValue.index].get_transform().Find("Type").GetComponent<Image>(), ResourceManager.GetIconSprite("fight_pet_biaoshi" + pet.function));
				gangFightMatchingFinishUI.MyPets[int64IndexValue.index].SetActive(true);
			}
		}
		for (int k = 0; k < 3; k++)
		{
			gangFightMatchingFinishUI.OpponentPets[k].SetActive(false);
		}
		for (int l = 0; l < this.gangFightMatchRoleSummary.petInfo.get_Count(); l++)
		{
			Pet pet2 = DataReader<Pet>.Get(this.gangFightMatchRoleSummary.petInfo.get_Item(l).petId);
			ResourceManager.SetSprite(gangFightMatchingFinishUI.OpponentPets[l].get_transform().Find("Icon").GetComponent<Image>(), PetManager.Instance.GetSelfPetIcon(pet2));
			ResourceManager.SetSprite(gangFightMatchingFinishUI.OpponentPets[l].get_transform().Find("Type").GetComponent<Image>(), ResourceManager.GetIconSprite("fight_pet_biaoshi" + pet2.function));
			gangFightMatchingFinishUI.OpponentPets[l].SetActive(true);
		}
		if (GangFightManager.Instance.gangFightBattleResult != null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"GangFightManager.Instance.gangFightBattleResult.winnerId  ",
				GangFightManager.Instance.gangFightBattleResult.winnerId,
				"  EntityWorld.Instance.EntSelf.ID  ",
				EntityWorld.Instance.EntSelf.ID
			}));
		}
		else
		{
			Debug.Log("GangFightManager.Instance.gangFightBattleResult == null");
		}
		if (GangFightManager.Instance.gangFightBattleResult != null && GangFightManager.Instance.gangFightBattleResult.winnerId == EntityWorld.Instance.EntSelf.ID)
		{
			EntityWorld.Instance.EntSelf.ReloadServerBattleState();
			InstanceManager.SetIsWaitingEnter(true);
		}
		else
		{
			InstanceManager.ChangeInstanceManager(GangFightInstance.Instance, false);
		}
	}

	public bool CheckGangFightIsOpen()
	{
		return ActivityCenterManager.Instance.CheckActivityIsOpen(10001) || (this.severTime >= this.dateTimeOpen && this.severTime <= this.dateTimeClose);
	}

	private void SetGangFightOpenTime()
	{
		DateTime severTime = this.severTime;
		string[] array = GangFightManager.Instance.openTime.Split(new char[]
		{
			':'
		});
		int num = int.Parse(array[0]);
		int num2 = int.Parse(array[1]);
		array = GangFightManager.Instance.closeTime.Split(new char[]
		{
			':'
		});
		int num3 = int.Parse(array[0]);
		int num4 = int.Parse(array[1]);
		this.dateTimeOpen = new DateTime(severTime.get_Year(), severTime.get_Month(), severTime.get_Day(), num, num2, 0);
		this.dateTimeClose = new DateTime(severTime.get_Year(), severTime.get_Month(), severTime.get_Day(), num3, num4, 0);
		if (this.severTime >= this.dateTimeOpen && this.severTime <= this.dateTimeClose)
		{
			this.open = true;
		}
		else
		{
			this.open = false;
		}
	}
}
