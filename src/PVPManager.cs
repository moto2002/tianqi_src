using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using XNetwork;

public class PVPManager : BaseSubSystemManager
{
	private const string ladderIcon = "huizhang_";

	private int old_intergral_level = -1;

	private ArenaInfo pvpData;

	private List<RankingsItem> rankingData = new List<RankingsItem>();

	private List<ChallengeRecord> challengePVPData = new List<ChallengeRecord>();

	private MatchRoleInfo matchData;

	private static PVPManager instance;

	public bool isEnterPVP;

	public ArenaInfo PVPData
	{
		get
		{
			return this.pvpData;
		}
		set
		{
			this.pvpData = value;
			if (this.pvpData == null)
			{
				this.old_intergral_level = -1;
			}
			else
			{
				int id = this.GetIntegralLevel(this.pvpData.score).id;
				if (this.old_intergral_level != -1 && id > this.old_intergral_level)
				{
					this.ShowPVPUp();
				}
				this.old_intergral_level = id;
			}
		}
	}

	public List<RankingsItem> RankingData
	{
		get
		{
			return this.rankingData;
		}
	}

	public List<ChallengeRecord> ChallengePVPData
	{
		get
		{
			return this.challengePVPData;
		}
	}

	public MatchRoleInfo MatchData
	{
		get
		{
			return this.matchData;
		}
	}

	public static PVPManager Instance
	{
		get
		{
			if (PVPManager.instance == null)
			{
				PVPManager.instance = new PVPManager();
			}
			return PVPManager.instance;
		}
	}

	protected PVPManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.PVPData = null;
		this.RankingData.Clear();
		this.ChallengePVPData.Clear();
		this.matchData = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ArenaInfo>(new NetCallBackMethod<ArenaInfo>(this.OnGetPVPData));
		NetworkManager.AddListenEvent<RankingsInfo>(new NetCallBackMethod<RankingsInfo>(this.OnGetRankingData));
		NetworkManager.AddListenEvent<ChallengeRecordInfo>(new NetCallBackMethod<ChallengeRecordInfo>(this.OnGetChallengeData));
		NetworkManager.AddListenEvent<StartChallengingRes>(new NetCallBackMethod<StartChallengingRes>(this.OnStartChallenging));
		NetworkManager.AddListenEvent<CancelChallengingRes>(new NetCallBackMethod<CancelChallengingRes>(this.OnCancelChallenging));
		NetworkManager.AddListenEvent<MatchFailNotice>(new NetCallBackMethod<MatchFailNotice>(this.OnGetFailNotice));
		NetworkManager.AddListenEvent<MatchRoleInfo>(new NetCallBackMethod<MatchRoleInfo>(this.OnGetMatchRoleData));
		NetworkManager.AddListenEvent<ArenaChallengeBattleResult>(new NetCallBackMethod<ArenaChallengeBattleResult>(this.OnArenaChallengeBattleResult));
		NetworkManager.AddListenEvent<ExitAreaBattleRes>(new NetCallBackMethod<ExitAreaBattleRes>(this.OnExitAreaBattleRes));
		NetworkManager.AddListenEvent<QueryRankingsInfoRes>(new NetCallBackMethod<QueryRankingsInfoRes>(this.OnQueryRankingsInfoRes));
	}

	public void SendRankingReq()
	{
		NetworkManager.Send(new QueryRankingsInfoReq(), ServerType.Data);
	}

	public void SendChallengeRecordReq()
	{
		NetworkManager.Send(new QueryChallengeRecordInfoReq(), ServerType.Data);
	}

	public void SendArenaRoomDestoryReq()
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		NetworkManager.Send(new ArenaRoomDestoryReq(), ServerType.Data);
	}

	public void SendStartChallengeReq()
	{
		InstanceManager.SecurityCheck(delegate
		{
			(UIManagerControl.Instance.OpenUI("MatchUI", null, false, UIType.NonPush) as MatchUI).SetDataCanle(30, false, delegate
			{
				this.SetEnd();
			}, delegate
			{
				UIManagerControl.Instance.ShowToastText("没匹配到玩家", 1f, 1f);
			});
			NetworkManager.Send(new StartChallengingReq(), ServerType.Data);
		}, null);
	}

	public void SendCancleChallengeReq()
	{
		NetworkManager.Send(new CancelChallengingReq(), ServerType.Data);
	}

	private void OnGetPVPData(short state, ArenaInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.PVPData = down;
			EventDispatcher.Broadcast(EventNames.PVPUpdateUI);
		}
	}

	private void OnGetRankingData(short state, RankingsInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.rankingData = down.items;
			this.rankingData.Sort((RankingsItem a, RankingsItem b) => a.rank.CompareTo(b.rank));
			EventDispatcher.Broadcast(EventNames.PVPUpdateList);
			if (EntityWorld.Instance.EntSelf != null)
			{
				int num = down.items.FindIndex((RankingsItem a) => a.roleId == EntityWorld.Instance.EntSelf.ID);
				if (num >= 0 && this.PVPData != null)
				{
					this.PVPData.rank = down.items.get_Item(num).rank;
					this.PVPData.score = down.items.get_Item(num).score;
					EventDispatcher.Broadcast(EventNames.PVPUpdateUI);
				}
			}
		}
	}

	private void OnGetChallengeData(short state, ChallengeRecordInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.challengePVPData = down.records;
		}
	}

	private void OnStartChallenging(short state, StartChallengingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnCancelChallenging(short state, CancelChallengingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGetFailNotice(short state, MatchFailNotice down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		FigureTime.CanleTimer(false);
	}

	private void OnGetMatchRoleData(short state, MatchRoleInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			InstanceManager.ChangeInstanceManager(PVPInstance.Instance, false);
			this.isEnterPVP = true;
			this.matchData = down;
			PVPVSUI pVPVSUI = UIManagerControl.Instance.OpenUI("PVPVSUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as PVPVSUI;
			PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
			for (int i = 0; i < 3; i++)
			{
				pVPVSUI.MyPets[i].SetActive(false);
			}
			if (petFormation != null && petFormation.petFormationArr != null && petFormation.petFormationArr.Int64Array != null)
			{
				for (int j = 0; j < petFormation.petFormationArr.Int64Array.get_Count(); j++)
				{
					Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(j);
					PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
					Pet pet = DataReader<Pet>.Get(petInfo.petId);
					ResourceManager.SetSprite(pVPVSUI.MyPets[int64IndexValue.index].get_transform().Find("Icon").GetComponent<Image>(), PetManager.Instance.GetSelfPetIcon(pet));
					ResourceManager.SetSprite(pVPVSUI.MyPets[int64IndexValue.index].get_transform().Find("Type").GetComponent<Image>(), ResourceManager.GetIconSprite("fight_pet_biaoshi" + pet.function));
					pVPVSUI.MyPets[int64IndexValue.index].SetActive(true);
				}
			}
			for (int k = 0; k < 3; k++)
			{
				pVPVSUI.OpponentPets[k].SetActive(false);
			}
			for (int l = 0; l < down.petInfos.get_Count(); l++)
			{
				Pet pet2 = DataReader<Pet>.Get(down.petInfos.get_Item(l).typeId);
				ResourceManager.SetSprite(pVPVSUI.OpponentPets[l].get_transform().Find("Icon").GetComponent<Image>(), PetManager.Instance.GetSelfPetIcon(pet2));
				ResourceManager.SetSprite(pVPVSUI.OpponentPets[l].get_transform().Find("Type").GetComponent<Image>(), ResourceManager.GetIconSprite("fight_pet_biaoshi" + pet2.function));
				pVPVSUI.OpponentPets[l].SetActive(true);
			}
		}
	}

	private void OnArenaChallengeBattleResult(short state, ArenaChallengeBattleResult down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			PVPInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnExitAreaBattleRes(short state, ExitAreaBattleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnQueryRankingsInfoRes(short state, QueryRankingsInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void ShowPVPUp()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (!EntityWorld.Instance.EntSelf.IsInBattle)
		{
			UIManagerControl.Instance.OpenUI("PVPUpUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
		}
		UIQueueManager.Instance.Push(delegate
		{
			UIManagerControl.Instance.OpenUI("PVPUpUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
		}, PopPriority.Normal, PopCondition.BackToCity);
	}

	public void OpenPVPSendReq()
	{
		this.SendChallengeRecordReq();
		this.SendRankingReq();
	}

	public void ClosePVPUI()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("PVPUI");
	}

	public void OpenBattleTypeUI()
	{
		UIManagerControl.Instance.OpenUI("BattleTypeUI", null, true, UIType.FullScreen);
	}

	private void SetEnd()
	{
		this.SendCancleChallengeReq();
	}

	public string GetIntegralByScore(int score, bool isBig = true)
	{
		JingJiChangFenDuan integralLevel = this.GetIntegralLevel(score);
		return this.GetGetIntegralByLevel(integralLevel.id, isBig);
	}

	public string GetGetIntegralByLevel(int level, bool big = true)
	{
		string text = "huizhang_" + level;
		if (big)
		{
			return text;
		}
		return string.Concat(new string[]
		{
			text + "_xiao"
		});
	}

	public JingJiChangFenDuan GetIntegralLevel()
	{
		int score = 0;
		if (this.PVPData != null)
		{
			score = this.PVPData.score;
		}
		return this.GetIntegralLevel(score);
	}

	public JingJiChangFenDuan GetIntegralLevel(int score)
	{
		List<JingJiChangFenDuan> dataList = DataReader<JingJiChangFenDuan>.DataList;
		JingJiChangFenDuan result = dataList.get_Item(0);
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (score <= dataList.get_Item(i).max)
			{
				result = dataList.get_Item(i);
				break;
			}
		}
		return result;
	}

	public string GetIntegralLevelName(int score)
	{
		JingJiChangFenDuan integralLevel = this.GetIntegralLevel(score);
		return GameDataUtils.GetChineseContent(integralLevel.name, false);
	}
}
