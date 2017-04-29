using GameData;
using System;
using System.Collections.Generic;

public class InstanceParent
{
	protected InstanceType type = InstanceType.None;

	protected int instanceDataID;

	public InstanceType Type
	{
		get
		{
			return this.type;
		}
		set
		{
			this.type = value;
		}
	}

	public virtual int InstanceDataID
	{
		get
		{
			return this.instanceDataID;
		}
		set
		{
			this.instanceDataID = value;
		}
	}

	public FuBenJiChuPeiZhi InstanceData
	{
		get
		{
			if (this.InstanceDataID == 0)
			{
				return null;
			}
			return DataReader<FuBenJiChuPeiZhi>.Get(this.InstanceDataID);
		}
	}

	public virtual float CurrentCollectDropTime
	{
		get
		{
			return 10f;
		}
	}

	public virtual void AddInstanceListeners()
	{
	}

	public virtual void RemoveInstanceListeners()
	{
	}

	public virtual void ReleaseData()
	{
	}

	public virtual void EnterBattleField()
	{
	}

	public virtual void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
	}

	public virtual void PreLoadData(int sceneID)
	{
	}

	public virtual List<int> GetPreloadClientCreatePetIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadClientCreateMonsterIDs()
	{
		return new List<int>();
	}

	public virtual void SetPreloadTypeData(List<int> thePlayerTypeList, List<int> thePetTypeList, List<int> theMonsterTypreList, List<int> thePlayerSkillList, List<int> thePetSkillList)
	{
	}

	public virtual List<int> GetPreloadServerCreatePlayerTypeIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadServerCreatePlayerSkillIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadServerCreatePetTypeIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadServerCreatePetSkillIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadServerCreateMonsterTypeIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadCommonFxIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadCGModelIDs()
	{
		return new List<int>();
	}

	public virtual List<int> GetPreloadCGComicIDs()
	{
		return new List<int>();
	}

	public virtual void LoadSceneEnd(int sceneID)
	{
	}

	public virtual void SendClientLoadDoneReq(int sceneID)
	{
	}

	public virtual void WaitForPlayerCountDown(int millisecond)
	{
	}

	public virtual void WaitForPlayerTimesUp()
	{
	}

	public virtual void AllPlayerLoadDone()
	{
	}

	public virtual void PlayOpeningCG(int timeline, Action onPlayerCGEnd)
	{
	}

	public virtual void OpeningCountDown(int millisecond)
	{
	}

	public virtual void OpeningCountDownTimesUp()
	{
	}

	public virtual bool ShouldSetEndingCamera()
	{
		return true;
	}

	public virtual void SetDebutCD()
	{
	}

	public virtual void ShowBattleUI()
	{
	}

	public virtual void ShowOpeningHint(int textID)
	{
	}

	public virtual void ShowOpeningHintTimesUp()
	{
	}

	public virtual void SetAI(bool isOpen)
	{
	}

	public virtual void SetCommonLogic()
	{
	}

	public virtual void SetTime()
	{
	}

	public virtual void SetBGM()
	{
		MySceneManager.Instance.PlayBGM();
	}

	public virtual void SelfInitEnd(EntitySelf self)
	{
		InstanceManager.SelfPositionGot();
	}

	public virtual void SelfCampChanged(int oldCamp, int newCamp)
	{
	}

	public virtual void SelfHpChange(EntitySelf self)
	{
	}

	public virtual void SelfHpLmtChange(EntitySelf self)
	{
	}

	public virtual void SelfActPointChange(EntitySelf self)
	{
	}

	public virtual void SelfActPointLmtChange(EntitySelf self)
	{
	}

	public virtual void SelfDie()
	{
	}

	public virtual void SelfRelive()
	{
	}

	public virtual void GiveUpRelive()
	{
	}

	public virtual void PlayerInitEnd(EntityPlayer player)
	{
	}

	public virtual void PlayerHpChange(EntityPlayer player)
	{
	}

	public virtual void PlayerHpLmtChange(EntityPlayer player)
	{
	}

	public virtual void PlayerDie(EntityPlayer player)
	{
	}

	public virtual void BossInitEnd(EntityMonster boss)
	{
	}

	public virtual void BossHpChange(EntityMonster boss)
	{
	}

	public virtual void BossHpLmtChange(EntityMonster boss)
	{
	}

	public virtual void BossVpChange(EntityMonster boss)
	{
	}

	public virtual void BossVpLmtChange(EntityMonster boss)
	{
	}

	public virtual void BossDie(EntityMonster player)
	{
	}

	public virtual void SetExtraLogic()
	{
	}

	public virtual void EndingCountdown(Action onCountdownEnd)
	{
	}

	public virtual void EndingCountdownTimesUp()
	{
	}

	public virtual void ShowEndingHint(int textID)
	{
	}

	public virtual void ShowEndingHintTimesUp()
	{
	}

	public virtual void PlayEndingCG(int timeline, Action onPlayerCGEnd)
	{
	}

	public virtual void ShowWinPose()
	{
	}

	public virtual void HideBattleUIs()
	{
	}

	public virtual void ShowWinUI()
	{
	}

	public virtual void ShowLoseUI()
	{
	}

	public virtual void ExitBattleField()
	{
	}

	public virtual void ResetBattleFieldResp()
	{
	}

	public virtual bool IsShowMonsterBorn(int monsterID)
	{
		return true;
	}

	public virtual bool IsShowPlayerAimMark(bool isSameCamp)
	{
		return false;
	}

	public virtual bool IsShowPetAimMark(bool isSameCamp)
	{
		return false;
	}

	public virtual void GetNewRealTimeDrop(XDict<int, long> newRealTimeDrop)
	{
	}

	public virtual void UpdateRealTimeDrop(XDict<int, long> realTimeDrop)
	{
	}

	public virtual int GetRankByPassTime(int passTime)
	{
		return -1;
	}

	public virtual int GetStandardTimeByRank(int rank)
	{
		return 0;
	}

	public virtual string GetRankInfoText(int rank, int remainTime)
	{
		return string.Empty;
	}

	public virtual XDict<int, long> GetRankReward(int rank)
	{
		return null;
	}

	public virtual string GetRankRewardText(int rank)
	{
		return string.Empty;
	}
}
