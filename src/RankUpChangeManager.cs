using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class RankUpChangeManager : BaseSubSystemManager
{
	protected static RankUpChangeManager instance;

	protected bool hasInit;

	protected int curCareer;

	protected int curState;

	protected List<int> curStageFinishedTask = new List<int>();

	protected bool isWaitForShowCareerFinish;

	protected int waitForShowPreCareer;

	protected int waitForShowCurCareer;

	protected bool isWaitForShowStageFinish;

	protected int waitForShowPreStage;

	protected int waitForShowCurStage;

	public static RankUpChangeManager Instance
	{
		get
		{
			if (RankUpChangeManager.instance == null)
			{
				RankUpChangeManager.instance = new RankUpChangeManager();
			}
			return RankUpChangeManager.instance;
		}
	}

	public bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		protected set
		{
			this.hasInit = value;
		}
	}

	public int CurType
	{
		get
		{
			if (EntityWorld.Instance.EntSelf == null)
			{
				return 0;
			}
			return EntityWorld.Instance.EntSelf.TypeID;
		}
	}

	public int CurCareer
	{
		get
		{
			return this.curCareer;
		}
		set
		{
			this.curCareer = value;
		}
	}

	public int CurStage
	{
		get
		{
			return this.curState;
		}
		set
		{
			this.curState = value;
		}
	}

	public List<int> CurStageFinishedTask
	{
		get
		{
			return this.curStageFinishedTask;
		}
	}

	public bool IsWaitForShowCareerFinish
	{
		get
		{
			return this.isWaitForShowCareerFinish;
		}
		set
		{
			this.isWaitForShowCareerFinish = value;
		}
	}

	public int WaitForShowPreCareer
	{
		get
		{
			return this.waitForShowPreCareer;
		}
		set
		{
			this.waitForShowPreCareer = value;
		}
	}

	public int WaitForShowCurCareer
	{
		get
		{
			return this.waitForShowCurCareer;
		}
		set
		{
			this.waitForShowCurCareer = value;
		}
	}

	public bool IsWaitForShowStageFinish
	{
		get
		{
			return this.isWaitForShowStageFinish;
		}
		set
		{
			this.isWaitForShowStageFinish = value;
		}
	}

	public int WaitForShowPreStage
	{
		get
		{
			return this.waitForShowPreStage;
		}
		set
		{
			this.waitForShowPreStage = value;
		}
	}

	public int WaitForShowCurStage
	{
		get
		{
			return this.waitForShowCurStage;
		}
		set
		{
			this.waitForShowCurStage = value;
		}
	}

	protected RankUpChangeManager()
	{
	}

	public override void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		base.Init();
	}

	public override void Release()
	{
		this.hasInit = false;
		this.curCareer = 0;
		this.curState = 0;
		this.curStageFinishedTask.Clear();
		this.isWaitForShowCareerFinish = false;
		this.waitForShowPreCareer = 0;
		this.waitForShowCurCareer = 0;
		this.isWaitForShowStageFinish = false;
		this.waitForShowPreStage = 0;
		this.waitForShowCurStage = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<CareerAdvancedPush>(new NetCallBackMethod<CareerAdvancedPush>(this.OnLoginPush));
		NetworkManager.AddListenEvent<StageChangeNty>(new NetCallBackMethod<StageChangeNty>(this.OnStageChangeNty));
		NetworkManager.AddListenEvent<StageFinishedNty>(new NetCallBackMethod<StageFinishedNty>(this.OnStageFinishedNty));
		NetworkManager.AddListenEvent<AdvanceFinishedNty>(new NetCallBackMethod<AdvanceFinishedNty>(this.OnRankUpNty));
	}

	protected void OnLoginPush(short state, CareerAdvancedPush down = null)
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
		this.CurCareer = down.careerAdvancedInfo.advancedCfgId;
		this.CurStage = down.careerAdvancedInfo.stageCfgId;
		this.CurStageFinishedTask.Clear();
		this.CurStageFinishedTask.AddRange(down.careerAdvancedInfo.finishedTaskId);
	}

	protected void OnStageChangeNty(short state, StageChangeNty down = null)
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
		if (this.CurStage != down.stageCfgId)
		{
			this.CurStage = down.stageCfgId;
			this.CurStageFinishedTask.Clear();
		}
		this.CurStageFinishedTask.Add(down.finishedTaskId);
		this.TryUpdateStageChange();
	}

	protected void OnStageFinishedNty(short state, StageFinishedNty down = null)
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
		if (DataReader<StageInfo>.Contains(down.nextStageCfgId))
		{
			this.CurStage = down.nextStageCfgId;
			this.CurStageFinishedTask.Clear();
		}
		else
		{
			if (DataReader<StageInfo>.Contains(down.curStageCfgId))
			{
				this.CurStage = down.curStageCfgId;
			}
			if (DataReader<StageInfo>.Contains(this.CurStage))
			{
				this.CurStageFinishedTask.Clear();
				this.CurStageFinishedTask.AddRange(DataReader<StageInfo>.Get(this.CurStage).taskid);
			}
			else
			{
				Debug.LogError("Cfg Error: !DataReader<StageInfo>.Contains(CurStage)");
			}
		}
		this.TryUpdateStageFinish(down.curStageCfgId, down.nextStageCfgId);
	}

	protected void OnRankUpNty(short state, AdvanceFinishedNty down = null)
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
		if (DataReader<AdvancedJob>.Contains(down.nextAdvancedCfgId))
		{
			AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(down.nextAdvancedCfgId);
			if (advancedJob.stageId.get_Count() > 0)
			{
				this.CurCareer = down.nextAdvancedCfgId;
				this.CurStage = DataReader<AdvancedJob>.Get(this.CurCareer).stageId.get_Item(0);
				this.CurStageFinishedTask.Clear();
			}
			else
			{
				Debug.LogError("Cfg Error: careerData.stageId == 0");
			}
		}
		else
		{
			if (DataReader<AdvancedJob>.Contains(down.curAdvancedCfgId))
			{
				this.CurCareer = down.curAdvancedCfgId;
			}
			if (DataReader<AdvancedJob>.Contains(this.CurCareer))
			{
				AdvancedJob advancedJob2 = DataReader<AdvancedJob>.Get(this.CurCareer);
				if (advancedJob2.stageId.get_Count() > 0)
				{
					if (DataReader<StageInfo>.Contains(advancedJob2.stageId.get_Item(advancedJob2.stageId.get_Count() - 1)))
					{
						this.CurStage = advancedJob2.stageId.get_Item(advancedJob2.stageId.get_Count() - 1);
					}
					if (DataReader<StageInfo>.Contains(this.CurStage))
					{
						this.CurStageFinishedTask.Clear();
						this.CurStageFinishedTask.AddRange(DataReader<StageInfo>.Get(this.CurStage).taskid);
					}
					else
					{
						Debug.LogError("Cfg Error: !DataReader<StageInfo>.Contains(CurStage)");
					}
				}
				else
				{
					Debug.LogError("Cfg Error: careerData.stageId == 0");
				}
			}
			else
			{
				Debug.LogError("Cfg Error: !DataReader<AdvancedJob>.Get(CurCareer)");
			}
		}
		this.TryUpdateCareerFinish(down.curAdvancedCfgId, down.nextAdvancedCfgId);
	}

	protected void TryUpdateStageChange()
	{
		RankUpChangeUI rankUpChangeUI = UIManagerControl.Instance.GetUIIfExist("RankUpChangeUI") as RankUpChangeUI;
		if (rankUpChangeUI)
		{
			rankUpChangeUI.SetData(this.CurType, this.CurCareer, this.CurStage, this.curStageFinishedTask);
		}
	}

	protected void TryUpdateStageFinish(int preID, int curID)
	{
		if (UIManagerControl.Instance.IsOpen("RankUpChangeUI"))
		{
			RankUpChangeUI rankUpChangeUI = UIManagerControl.Instance.GetUIIfExist("RankUpChangeUI") as RankUpChangeUI;
			if (rankUpChangeUI)
			{
				rankUpChangeUI.SetData(this.CurType, this.CurCareer, this.CurStage, this.curStageFinishedTask);
			}
		}
		else
		{
			this.IsWaitForShowStageFinish = true;
			this.WaitForShowPreStage = preID;
			this.WaitForShowCurStage = curID;
			if (UIManagerControl.Instance.IsOpen("TownUI"))
			{
				this.TryShowStageFinishUI();
			}
		}
	}

	protected void TryUpdateCareerFinish(int preID, int curID)
	{
		if (UIManagerControl.Instance.IsOpen("RankUpChangeUI"))
		{
			RankUpChangeUI rankUpChangeUI = UIManagerControl.Instance.GetUIIfExist("RankUpChangeUI") as RankUpChangeUI;
			if (rankUpChangeUI)
			{
				rankUpChangeUI.SetData(this.CurType, this.CurCareer, this.CurStage, this.curStageFinishedTask);
			}
		}
		else
		{
			this.IsWaitForShowCareerFinish = true;
			this.WaitForShowPreCareer = preID;
			this.WaitForShowCurCareer = curID;
			if (UIManagerControl.Instance.IsOpen("TownUI"))
			{
				this.TryShowCareerFinishUI();
			}
		}
	}

	public void OpenRankUpChangeUI()
	{
		RankUpChangeUI rankUpChangeUI = UIManagerControl.Instance.OpenUI("RankUpChangeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RankUpChangeUI;
		if (rankUpChangeUI)
		{
			rankUpChangeUI.SetData(this.CurType, this.CurCareer, this.CurStage, this.curStageFinishedTask);
		}
	}

	public void TryShowStageFinishUI()
	{
		if (!this.IsWaitForShowStageFinish)
		{
			return;
		}
		this.IsWaitForShowStageFinish = false;
		if (DataReader<GlobalParams>.Contains("advancedJobText"))
		{
			string value = DataReader<GlobalParams>.Get("advancedJobText").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			if (array.Length > 5 && DataReader<StageInfo>.Contains(this.WaitForShowPreStage) && DataReader<StageInfo>.Contains(this.WaitForShowCurStage))
			{
				DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(int.Parse(array[3]), false), string.Format(GameDataUtils.GetChineseContent(int.Parse(array[4]), false), GameDataUtils.GetChineseContent(DataReader<StageInfo>.Get(this.WaitForShowPreStage).title, false), GameDataUtils.GetChineseContent(DataReader<StageInfo>.Get(this.WaitForShowCurStage).title, false)), delegate
				{
					this.OpenRankUpChangeUI();
				}, GameDataUtils.GetChineseContent(int.Parse(array[5]), false), "button_orange_1", null);
				DialogBoxUIView.Instance.isClick = false;
			}
		}
	}

	public void TryShowCareerFinishUI()
	{
		if (!this.IsWaitForShowCareerFinish)
		{
			return;
		}
		this.IsWaitForShowCareerFinish = false;
		if (!DataReader<AdvancedJob>.Contains(this.WaitForShowPreCareer))
		{
			return;
		}
		AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(this.WaitForShowPreCareer);
		RankUpChangePreviewUI rankUpChangePreviewUI = UIManagerControl.Instance.OpenUI("RankUpChangePreviewUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RankUpChangePreviewUI;
		if (rankUpChangePreviewUI)
		{
			rankUpChangePreviewUI.SetData(advancedJob.advanced1Model);
		}
	}

	public bool IsTaskFinished(int taskID)
	{
		return this.CurStageFinishedTask.Contains(taskID);
	}

	public bool IsAllRankUpFinish()
	{
		if (!DataReader<JobIndex>.Contains(this.CurType))
		{
			return false;
		}
		JobIndex jobIndex = DataReader<JobIndex>.Get(this.CurType);
		if (jobIndex.AdvancedJobId.get_Count() == 0)
		{
			return false;
		}
		if (this.CurCareer != jobIndex.AdvancedJobId.get_Item(jobIndex.AdvancedJobId.get_Count() - 1))
		{
			return false;
		}
		if (!DataReader<AdvancedJob>.Contains(this.CurCareer))
		{
			return false;
		}
		AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(this.CurCareer);
		if (advancedJob.stageId.get_Count() == 0)
		{
			return false;
		}
		if (this.CurStage != advancedJob.stageId.get_Item(advancedJob.stageId.get_Count() - 1))
		{
			return false;
		}
		if (!DataReader<StageInfo>.Contains(this.CurStage))
		{
			return false;
		}
		StageInfo stageInfo = DataReader<StageInfo>.Get(this.CurStage);
		if (stageInfo.taskid.get_Count() == 0)
		{
			return false;
		}
		for (int i = 0; i < stageInfo.taskid.get_Count(); i++)
		{
			if (!this.IsTaskFinished(stageInfo.taskid.get_Item(i)))
			{
				return false;
			}
		}
		return true;
	}

	public List<KeyValuePair<int, int>> GetSkillExtendIDs()
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		if (!DataReader<JobIndex>.Contains(this.CurType))
		{
			return list;
		}
		JobIndex jobIndex = DataReader<JobIndex>.Get(this.CurType);
		List<int> list2 = new List<int>();
		bool flag = this.IsAllRankUpFinish();
		for (int i = 0; i < jobIndex.AdvancedJobId.get_Count(); i++)
		{
			if (jobIndex.AdvancedJobId.get_Item(i) <= this.CurCareer && (jobIndex.AdvancedJobId.get_Item(i) != this.CurCareer || flag))
			{
				if (DataReader<AdvancedJob>.Contains(jobIndex.AdvancedJobId.get_Item(i)))
				{
					list2.Add(DataReader<AdvancedJob>.Get(jobIndex.AdvancedJobId.get_Item(i)).skillExtendId);
				}
			}
		}
		for (int j = 0; j < list2.get_Count(); j++)
		{
			list.Add(new KeyValuePair<int, int>(list2.get_Item(j), 1));
		}
		return list;
	}
}
