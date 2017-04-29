using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;
using XUPorterJSON;

public class MainTaskManager : BaseSubSystemManager
{
	private Dictionary<int, BaseTask> mTaskMap = new Dictionary<int, BaseTask>();

	private int mNextDestroyTaskId;

	private int[] mZeroTaskId = new int[5];

	private List<int> mNpcIdList = new List<int>();

	public XDict<int, int> GoodsModels = new XDict<int, int>();

	public Dictionary<Package.Task.TaskType, int> LastTaskIdDict = new Dictionary<Package.Task.TaskType, int>();

	public bool IsRelease;

	public int DelayExecuteTaskId;

	private uint mExecuteDelayId;

	public int ShowTalkUINpc;

	private ArrayList mTaskCounts = new ArrayList();

	private long mPlayerID;

	public bool IsSwitchCityByTask;

	public bool IsOpenAgainOnEnable;

	public NPC OpenNpcSystemAgainOnEnable;

	public NPC OpenNpcMenuAgainOnEnable;

	public bool IsFirstGuildTask;

	public bool IsShowGuildTaskGuide;

	public bool IsAutoGoingToNPC;

	private int mEnterNpcAgainOnEnable;

	public int LastGetRewardZeroTaskGroupId;

	public int LastGetExtraRewardTaskType;

	private bool mClickLock;

	private uint mClickLockId;

	private bool mUseFlyShoeLock;

	private uint mUseFlyShoeLockId;

	private bool mSwitchCityLock;

	private uint mSwitchCityLockId;

	private bool mZeroTaskRefreshLock;

	private uint mZeroTaskRefreshLockId;

	private int mAutoTaskId;

	public bool IsAllAuto;

	public bool IsAutoFast;

	private Package.Task.TaskType mLastAutoType;

	public int CurTaskId;

	private int mMainTaskId;

	public int RingTaskId;

	public int GuildTaskId;

	public int RingTask2Id;

	public int CareerTaskId;

	public int GodWeaponTaskId;

	private int mLastAcceptTaskId;

	private int mLastFinishTaskId;

	private int mLastCommitTaskId;

	public List<BaseTask> QuickAcceptQueue = new List<BaseTask>();

	public List<BaseTask> QuickCommitQueue = new List<BaseTask>();

	private List<ZhuChengPeiZhi> mAllCityDatas;

	private XDict<int, ExtraRewardInfo> mExtraRewardInfos = new XDict<int, ExtraRewardInfo>();

	public Queue<EWaiRenWuJiangLi> ExtraRewardQueue = new Queue<EWaiRenWuJiangLi>();

	private Dictionary<int, int> TaskTimes = new Dictionary<int, int>();

	public int[] AutoTaskType;

	public int CareerMaxLevel;

	public int RingMaxTimes;

	private int ConstRingMaxTimes;

	public int Ring2MaxTimes;

	public int UseFlyShoeCount = 1;

	public int[] TaskSortRult;

	public int FlyShoeCD;

	public float UseFlyShoeMinDist = 1f;

	public int UseDiamondMult;

	public int UseDiamondCount;

	public int UseExtraDiamondMult;

	public int UseExtraDiamondCount;

	public int AutoFreeLevel;

	public int AutoTaskTime;

	public int[] TipsSystems;

	public int ZeroTaskNpcId;

	public int UpgradeTaskNpcId;

	public int ZeroMaxTimes;

	public int ZeroTaskFreeMaxTimes;

	public int ZeroTaskMaxCountDown;

	public int[] ZeroTaskRefreshItem;

	public string[] STATUS_TXT;

	public string[] STATUS_COLOR;

	public string[] TYPE_COLOR;

	public string[] TYPE_FLAG;

	public string[] ZERO_COLOR;

	private static MainTaskManager instance;

	private bool mIsNeedRefreshList;

	public Dictionary<int, BaseTask> TaskMap
	{
		get
		{
			return this.mTaskMap;
		}
	}

	public int[] ZeroTaskId
	{
		get
		{
			return this.mZeroTaskId;
		}
	}

	public int NpcId
	{
		get;
		set;
	}

	public string TaskCountsKey
	{
		get
		{
			return this.mPlayerID + "TaskCounts";
		}
	}

	public bool IsUseDiamond
	{
		get;
		private set;
	}

	public int ZeroTaskFreeTimes
	{
		get;
		set;
	}

	public int ZeroTaskRefreshTime
	{
		get;
		private set;
	}

	public bool ClickLock
	{
		get
		{
			return this.mClickLock;
		}
		set
		{
			this.mClickLock = value;
			if (this.mClickLockId != 0u)
			{
				TimerHeap.DelTimer(this.mClickLockId);
				this.mClickLockId = 0u;
			}
			if (this.mClickLock)
			{
				this.mClickLockId = TimerHeap.AddTimer(5000u, 0, delegate
				{
					this.mClickLock = false;
				});
			}
		}
	}

	public bool UseFlyShoeLock
	{
		get
		{
			return this.mUseFlyShoeLock;
		}
		private set
		{
			this.mUseFlyShoeLock = value;
			if (this.mUseFlyShoeLockId != 0u)
			{
				TimerHeap.DelTimer(this.mUseFlyShoeLockId);
				this.mUseFlyShoeLockId = 0u;
			}
			if (this.mUseFlyShoeLock)
			{
				this.mUseFlyShoeLockId = TimerHeap.AddTimer((uint)this.FlyShoeCD, 0, delegate
				{
					this.mUseFlyShoeLock = false;
				});
			}
		}
	}

	public bool SwitchCityLock
	{
		get
		{
			return this.mSwitchCityLock;
		}
		set
		{
			this.mSwitchCityLock = value;
			if (this.mSwitchCityLockId != 0u)
			{
				TimerHeap.DelTimer(this.mSwitchCityLockId);
				this.mSwitchCityLockId = 0u;
			}
			if (this.mSwitchCityLock)
			{
				this.mSwitchCityLockId = TimerHeap.AddTimer(3000u, 0, delegate
				{
					this.mSwitchCityLock = false;
				});
			}
		}
	}

	public bool ZeroTaskRefreshLock
	{
		get
		{
			return this.mZeroTaskRefreshLock;
		}
		private set
		{
			this.mZeroTaskRefreshLock = value;
			if (this.mZeroTaskRefreshLockId != 0u)
			{
				TimerHeap.DelTimer(this.mZeroTaskRefreshLockId);
				this.mZeroTaskRefreshLockId = 0u;
			}
			if (this.mZeroTaskRefreshLock)
			{
				this.mZeroTaskRefreshLockId = TimerHeap.AddTimer(1000u, 0, delegate
				{
					this.mZeroTaskRefreshLock = false;
				});
			}
		}
	}

	public int AutoTaskId
	{
		get
		{
			return this.mAutoTaskId;
		}
		set
		{
			if (this.mAutoTaskId != value)
			{
				this.mAutoTaskId = value;
				EventDispatcher.Broadcast<int>(EventNames.ChangeAutoTask, this.mAutoTaskId);
				if (this.mAutoTaskId < 0)
				{
					Debug.Log("停止延迟执行任务!!!");
				}
				else if (this.mAutoTaskId == 0)
				{
					Debug.Log("结束委托任务!!!");
				}
				else
				{
					Debug.Log("委托任务->" + this.mAutoTaskId);
				}
			}
		}
	}

	public int MainTaskId
	{
		get
		{
			return this.mMainTaskId;
		}
		set
		{
			this.mMainTaskId = value;
			EventDispatcher.Broadcast(EventNames.CurTaskChange);
		}
	}

	public List<EWaiRenWuJiangLi> ExtraRewardDatas
	{
		get;
		private set;
	}

	public int GuildTaskTimes
	{
		get
		{
			int result;
			this.TaskTimes.TryGetValue(4, ref result);
			return result;
		}
	}

	public int RingTaskTimes
	{
		get
		{
			int result;
			this.TaskTimes.TryGetValue(3, ref result);
			return result;
		}
	}

	public int RingTask2Times
	{
		get
		{
			int result;
			this.TaskTimes.TryGetValue(5, ref result);
			return result;
		}
	}

	public int ZeroTaskTimes
	{
		get
		{
			int result;
			this.TaskTimes.TryGetValue(8, ref result);
			return result;
		}
	}

	public static MainTaskManager Instance
	{
		get
		{
			if (MainTaskManager.instance == null)
			{
				MainTaskManager.instance = new MainTaskManager();
			}
			return MainTaskManager.instance;
		}
	}

	private MainTaskManager()
	{
	}

	public override void Release()
	{
		this.IsRelease = true;
		this.NpcId = 0;
		this.mNpcIdList.Clear();
		this.TaskTimes.Clear();
		this.GoodsModels.Clear();
		this.ExtraRewardQueue.Clear();
		this.ClearTaskOtherData();
		this.mAutoTaskId = 0;
		this.IsAllAuto = false;
		this.IsAutoFast = false;
		this.mLastAutoType = (Package.Task.TaskType)0;
		this.mNextDestroyTaskId = 0;
		this.CurTaskId = 0;
		this.MainTaskId = 0;
		this.RingTaskId = 0;
		this.GuildTaskId = 0;
		this.RingTask2Id = 0;
		this.CareerTaskId = 0;
		this.GodWeaponTaskId = 0;
		this.mLastAcceptTaskId = 0;
		this.mLastFinishTaskId = 0;
		this.mLastCommitTaskId = 0;
		this.DelayExecuteTaskId = 0;
		this.ShowTalkUINpc = 0;
		this.ZeroTaskFreeTimes = 0;
		this.ZeroTaskRefreshTime = 0;
		this.UpgradeTaskNpcId = 0;
		this.IsSwitchCityByTask = false;
		this.IsOpenAgainOnEnable = false;
		this.IsShowGuildTaskGuide = false;
		this.OpenNpcSystemAgainOnEnable = null;
		this.OpenNpcMenuAgainOnEnable = null;
		this.IsAutoGoingToNPC = false;
		this.mEnterNpcAgainOnEnable = 0;
		this.LastGetRewardZeroTaskGroupId = 0;
		this.LastGetExtraRewardTaskType = 0;
		this.SaveTaskCounts();
		List<BaseTask> list = new List<BaseTask>(this.mTaskMap.get_Values());
		for (int i = 0; i < list.get_Count(); i++)
		{
			list.get_Item(i).Dispose();
		}
		this.mTaskMap.Clear();
		for (int j = 0; j < this.mZeroTaskId.Length; j++)
		{
			this.mZeroTaskId[j] = 0;
		}
		this.LastTaskIdDict.Clear();
	}

	public override void Init()
	{
		base.Init();
		this.SetTaskOtherData();
		this.ExtraRewardDatas = DataReader<EWaiRenWuJiangLi>.DataList;
		this.mAllCityDatas = DataReader<ZhuChengPeiZhi>.DataList;
		if (this.STATUS_TXT == null)
		{
			this.STATUS_TXT = new string[]
			{
				GameDataUtils.GetChineseContent(310013, false),
				GameDataUtils.GetChineseContent(310014, false),
				GameDataUtils.GetChineseContent(310015, false),
				GameDataUtils.GetChineseContent(310016, false),
				GameDataUtils.GetChineseContent(310017, false),
				GameDataUtils.GetChineseContent(310018, false),
				GameDataUtils.GetChineseContent(310019, false)
			};
		}
		if (this.STATUS_COLOR == null)
		{
			this.STATUS_COLOR = new string[]
			{
				"<color=#ff3342>{0}</color>",
				"<color=#5ee972>{0}</color>",
				"<color=#5ee972>{0}</color>",
				"<color=#5ee972>{0}</color>",
				"<color=#5ee972>{0}</color>",
				"<color=#5ee972>{0}</color>",
				"<color=#5ee972>{0}</color>"
			};
		}
		if (this.TYPE_COLOR == null)
		{
			this.TYPE_COLOR = new string[]
			{
				"{0}",
				"<color=#ff9b39>{0}</color>",
				"<color=#57f5c7>{0}</color>",
				"<color=#ff6161>{0}</color>",
				"<color=#63dcff>{0}</color>",
				"<color=#ff6161>{0}</color>",
				"<color=#50e549>{0}</color>",
				"<color=#ff9b39>{0}</color>",
				"<color=#ff66eb>{0}</color>",
				"<color=#50e549>{0}</color>"
			};
		}
		if (this.TYPE_FLAG == null)
		{
			this.TYPE_FLAG = new string[]
			{
				string.Empty,
				GameDataUtils.GetChineseContent(310020, false),
				GameDataUtils.GetChineseContent(310021, false),
				GameDataUtils.GetChineseContent(310022, false),
				GameDataUtils.GetChineseContent(310023, false),
				GameDataUtils.GetChineseContent(310025, false),
				GameDataUtils.GetChineseContent(310024, false),
				GameDataUtils.GetChineseContent(310020, false),
				GameDataUtils.GetChineseContent(310029, false),
				GameDataUtils.GetChineseContent(310024, false)
			};
		}
		if (this.ZERO_COLOR == null)
		{
			this.ZERO_COLOR = new string[]
			{
				"{0}",
				"<color=#55ABFA>{0}</color>",
				"<color=#F054FE>{0}</color>",
				"<color=#FF7D4B>{0}</color>",
				"<color=#FFE349>{0}</color>"
			};
		}
	}

	private void SetTaskOtherData()
	{
		this.ConstRingMaxTimes = this.GetIntOtherData("challengeNum");
		this.UseFlyShoeCount = this.GetIntOtherData("flyExpend");
		this.FlyShoeCD = this.GetIntOtherData("respondTime");
		this.UseFlyShoeMinDist = (float)this.GetIntOtherData("transmissionDistance");
		this.UseDiamondMult = this.GetIntOtherData("routineCoefficient");
		this.UseDiamondCount = this.GetIntOtherData("routinePrice");
		this.UseExtraDiamondMult = this.GetIntOtherData("additionalCoefficient");
		this.UseExtraDiamondCount = this.GetIntOtherData("additionalPrice");
		this.CareerMaxLevel = this.GetIntOtherData("forceLv");
		this.AutoFreeLevel = this.GetIntOtherData("freeLv");
		this.AutoTaskTime = this.GetIntOtherData("waitTime");
		this.ZeroTaskNpcId = this.GetIntOtherData("receiveNpc");
		this.ZeroMaxTimes = this.GetIntOtherData("performNum");
		this.ZeroTaskFreeMaxTimes = this.GetIntOtherData("freeRefresh");
		this.ZeroTaskMaxCountDown = this.GetIntOtherData("countDown");
		this.AutoTaskType = this.GetIntsOtherList("automaticType");
		this.TaskSortRult = this.GetIntsOtherList("array");
		this.TipsSystems = this.GetIntsOtherList("systemId");
		this.ZeroTaskRefreshItem = this.GetIntsOtherList("refreshCard");
	}

	private void ClearTaskOtherData()
	{
		this.ConstRingMaxTimes = 0;
		this.UseFlyShoeCount = -1;
		this.FlyShoeCD = 0;
		this.UseFlyShoeMinDist = 1f;
		this.UseDiamondMult = 0;
		this.UseDiamondCount = 0;
		this.UseExtraDiamondMult = 0;
		this.UseExtraDiamondCount = 0;
		this.CareerMaxLevel = 0;
		this.AutoFreeLevel = 0;
		this.AutoTaskTime = 0;
		this.ZeroTaskNpcId = 0;
		this.ZeroMaxTimes = 0;
		this.ZeroTaskFreeMaxTimes = 0;
		this.ZeroTaskMaxCountDown = 0;
		this.AutoTaskType = null;
		this.TaskSortRult = null;
		this.TipsSystems = null;
		this.ZeroTaskRefreshItem = null;
	}

	private int GetIntOtherData(string key)
	{
		float num = 0f;
		PaoHuanRenWuPeiZhi paoHuanRenWuPeiZhi = DataReader<PaoHuanRenWuPeiZhi>.Get(key);
		if (paoHuanRenWuPeiZhi != null && float.TryParse(paoHuanRenWuPeiZhi.value, ref num))
		{
			return (int)num;
		}
		return 0;
	}

	private int[] GetIntsOtherList(string key)
	{
		float num = 0f;
		PaoHuanRenWuPeiZhi paoHuanRenWuPeiZhi = DataReader<PaoHuanRenWuPeiZhi>.Get(key);
		if (paoHuanRenWuPeiZhi != null)
		{
			string[] array = paoHuanRenWuPeiZhi.value.Split(new char[]
			{
				';'
			});
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				float.TryParse(array[i], ref num);
				array2[i] = (int)num;
			}
			return array2;
		}
		return null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<LoginPushTaskInfo>(new NetCallBackMethod<LoginPushTaskInfo>(this.OnLoginPushTaskInfo));
		NetworkManager.AddListenEvent<Package.Task>(new NetCallBackMethod<Package.Task>(this.OnUpdateTask));
		NetworkManager.AddListenEvent<PushOtherData>(new NetCallBackMethod<PushOtherData>(this.OnPushOtherData));
		NetworkManager.AddListenEvent<ExtraRewardPush>(new NetCallBackMethod<ExtraRewardPush>(this.OnExtraRewardPush));
		NetworkManager.AddListenEvent<AcceptTaskRes>(new NetCallBackMethod<AcceptTaskRes>(this.OnAcceptTaskRes));
		NetworkManager.AddListenEvent<CommitTaskRes>(new NetCallBackMethod<CommitTaskRes>(this.OnCommitTaskRes));
		NetworkManager.AddListenEvent<GetExtraRewardRes>(new NetCallBackMethod<GetExtraRewardRes>(this.OnGetExtraRewardRes));
		NetworkManager.AddListenEvent<ChallengeMirrorRes>(new NetCallBackMethod<ChallengeMirrorRes>(this.OnChallengeMirrorRes));
		NetworkManager.AddListenEvent<ResultMirrorNty>(new NetCallBackMethod<ResultMirrorNty>(this.OnResultMirrorNty));
		NetworkManager.AddListenEvent<ExitMirrorRes>(new NetCallBackMethod<ExitMirrorRes>(this.OnExitMirrorRes));
		NetworkManager.AddListenEvent<TaskGroupAcceptRes>(new NetCallBackMethod<TaskGroupAcceptRes>(this.OnTaskGroupAcceptRes));
		NetworkManager.AddListenEvent<GetZeroCityPrizeRes>(new NetCallBackMethod<GetZeroCityPrizeRes>(this.OnGetZeroCityPrizeRes));
		NetworkManager.AddListenEvent<ZeroCityRefreshRes>(new NetCallBackMethod<ZeroCityRefreshRes>(this.OnZeroCityRefreshRes));
		NetworkManager.AddListenEvent<AbandonTaskRes>(new NetCallBackMethod<AbandonTaskRes>(this.OnAbandonTaskRes));
		EventDispatcher.AddListener<int>(TaskNPCBehavior.OnEnterNPC, new Callback<int>(this.OnEnterNPC));
		EventDispatcher.AddListener<int>(TaskNPCBehavior.OnExitNPC, new Callback<int>(this.OnExitNPC));
		EventDispatcher.AddListener<int>(TaskNPCBehavior.OnSeleteNPC, new Callback<int>(this.OnSeleteNPC));
		EventDispatcher.AddListener(SceneManagerEvent.ClearSceneDependentLogic, new Callback(this.ClearNpcIds));
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionChange", new Callback(this.OnRoleSelfProfessionChange));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.LoadSceneStart, new Callback<int, int>(this.OnLoadSceneStart));
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.OnRoleSelfLevelUp));
		EventDispatcher.AddListener<bool>(EventNames.BroadcastOfTownUI, new Callback<bool>(this.OnTownUISetActive));
		EventDispatcher.AddListener("TimeManagerEvent.ZeroPointTrigger", new Callback(this.OnZeroPointTrigger));
	}

	private void OnLoginPushTaskInfo(short state, LoginPushTaskInfo down = null)
	{
		this.ClickLock = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			ArrayList taskCounts = this.GetTaskCounts();
			if (taskCounts != null)
			{
				for (int i = 0; i < down.taskInfo.get_Count(); i++)
				{
					Package.Task task = down.taskInfo.get_Item(i);
					bool flag = false;
					for (int j = 0; j < taskCounts.get_Count(); j += 2)
					{
						if (task.taskId == (int)float.Parse(taskCounts.get_Item(j).ToString()))
						{
							task.count = (int)float.Parse(taskCounts.get_Item(j + 1).ToString());
							flag = true;
							break;
						}
					}
					BaseTask baseTask = this.SetTaskSerData(task, true);
					if (flag && baseTask != null && baseTask.Data != null && baseTask.Data.target != null)
					{
						int num = 0;
						if (baseTask.Data.type == 5 || baseTask.Data.type == 4)
						{
							if (baseTask.Data.target.get_Count() == 2)
							{
								num = baseTask.Data.target.get_Item(1);
							}
							else if (baseTask.Data.target.get_Count() == 3)
							{
								num = baseTask.Data.target.get_Item(2);
							}
						}
						else if (baseTask.Data.type == 2)
						{
							num = baseTask.Data.target.get_Item(baseTask.Data.target.get_Count() - 1);
						}
						if (baseTask.Task.count >= num)
						{
							baseTask.Task.count = Mathf.Max(0, num - 1);
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < down.taskInfo.get_Count(); k++)
				{
					this.SetTaskSerData(down.taskInfo.get_Item(k), true);
				}
			}
			this.CurTaskId = this.MainTaskId;
			for (int l = 0; l < down.lastTaskId.get_Count(); l++)
			{
				LastTaskId lastTaskId = down.lastTaskId.get_Item(l);
				this.LastTaskIdDict.Add((Package.Task.TaskType)lastTaskId.taskType, lastTaskId.taskId);
			}
			this.IsFirstGuildTask = down.guildTaskFlag;
			if (this.GodWeaponTaskId > 0)
			{
				BaseTask task2 = this.GetTask(this.MainTaskId, true);
				if (task2 != null)
				{
					task2.IsActive = false;
				}
			}
			List<BaseTask> list = new List<BaseTask>(this.mTaskMap.get_Values());
			for (int m = 0; m < list.get_Count(); m++)
			{
				this.CheckQuickTask(list.get_Item(m));
				this.CheckZeroTaskGroupReward(list.get_Item(m));
			}
		}
	}

	public void OnUpdateTask(short state, Package.Task down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.SetTaskSerData(down, false) != null)
		{
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, down);
			if (down.status == Package.Task.TaskStatus.WaitingToClaimPrize || down.status == Package.Task.TaskStatus.TaskFinished || this.mIsNeedRefreshList)
			{
				this.mIsNeedRefreshList = false;
				EventDispatcher.Broadcast(EventNames.SortTaskList);
			}
			if (this.mLastFinishTaskId == down.taskId)
			{
				BaseTask baseTask = null;
				if (this.GetTask(this.mLastFinishTaskId, out baseTask, true))
				{
					baseTask.FinishAfter();
					this.mLastFinishTaskId = 0;
				}
			}
			if (this.AutoTaskId > 0)
			{
				if (EntityWorld.Instance.EntSelf.Lv < this.AutoFreeLevel && down.taskType == Package.Task.TaskType.MainTask && down.status == Package.Task.TaskStatus.TaskCanAccept)
				{
					this.StopToNPC(true);
					this.AutoTaskId = -1;
				}
				else if (this.mLastAutoType == down.taskType)
				{
					this.AutoTaskId = down.taskId;
				}
			}
		}
	}

	public void SendAcceptTaskReq(int id, int zeroId = 0)
	{
		this.ClickLock = true;
		this.mLastAcceptTaskId = id;
		NetworkManager.VitalSend(new AcceptTaskReq
		{
			taskId = id,
			taskNumber = zeroId
		}, ServerType.Data);
		Debug.LogFormat("<请求接受[{0}]任务, 任务组[{1}]>", new object[]
		{
			id,
			zeroId
		});
	}

	private void OnAcceptTaskRes(short state, AcceptTaskRes down = null)
	{
		this.ClickLock = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		BaseTask baseTask;
		if (this.GetTask(this.mLastAcceptTaskId, out baseTask, true))
		{
			Debug.Log("<返回接受[" + this.mLastAcceptTaskId + "]任务>");
			if (UIManagerControl.Instance.IsOpen("TownUI"))
			{
				baseTask.AcceptAfter();
			}
			this.mLastAcceptTaskId = 0;
		}
	}

	public void SendCommitTaskReq(int id, bool isUseDiamond = false, int zeroId = 0)
	{
		this.ClickLock = true;
		if (this.AutoTaskId == id)
		{
			BaseTask baseTask = null;
			if (this.GetTask(id, out baseTask, true))
			{
				this.mLastAutoType = baseTask.Task.taskType;
			}
		}
		this.mLastCommitTaskId = id;
		this.IsUseDiamond = isUseDiamond;
		NetworkManager.VitalSend(new CommitTaskReq
		{
			taskId = id,
			useDiamond = isUseDiamond,
			taskNumber = zeroId
		}, ServerType.Data);
		Debug.LogFormat("<请求提交[{0}]任务, 任务组[{1}], 是否花费钻石:{2}>", new object[]
		{
			id,
			zeroId,
			isUseDiamond
		});
	}

	private void OnCommitTaskRes(short state, CommitTaskRes down = null)
	{
		this.ClickLock = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast<BaseTask>(EventNames.FinishTaskNty, null);
			return;
		}
		Debug.Log("<返回提交[" + this.mLastCommitTaskId + "]任务>");
		BaseTask baseTask = null;
		if (this.GetTask(this.mLastCommitTaskId, out baseTask, true))
		{
			this.mLastCommitTaskId = 0;
			if (baseTask.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize || baseTask.Task.status == Package.Task.TaskStatus.TaskFinished)
			{
				baseTask.CommitAfter();
				if (baseTask.Task.taskType != Package.Task.TaskType.MainTask || baseTask.hasNextTask)
				{
					this.NextDeleteTask(baseTask.Task.taskId);
				}
			}
			if (baseTask.Task.taskType == Package.Task.TaskType.GodWeaponTask && !baseTask.hasNextTask)
			{
				this.ActiveMainTask();
			}
			if (this.LastTaskIdDict.ContainsKey(baseTask.Task.taskType))
			{
				this.LastTaskIdDict.set_Item(baseTask.Task.taskType, baseTask.Task.taskId);
			}
			else
			{
				this.LastTaskIdDict.Add(baseTask.Task.taskType, baseTask.Task.taskId);
			}
			if (this.IsShowGuildTaskGuide)
			{
				this.IsShowGuildTaskGuide = false;
				this.IsFirstGuildTask = false;
			}
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, null);
			EventDispatcher.Broadcast(EventNames.SortTaskList);
			baseTask.GetTaskRewards(ref DisplayItemManager.Instance.ItemDataDict, true);
			if (this.IsUseDiamond)
			{
				for (int i = 0; i < DisplayItemManager.Instance.ItemDataDict.Count; i++)
				{
					List<long> values;
					List<long> expr_19A = values = DisplayItemManager.Instance.ItemDataDict.Values;
					int num;
					int expr_19D = num = i;
					long num2 = values.get_Item(num);
					expr_19A.set_Item(expr_19D, num2 * 2L);
				}
			}
			EventDispatcher.Broadcast<BaseTask>(EventNames.FinishTaskNty, baseTask);
			if (baseTask.Task.status == Package.Task.TaskStatus.TaskFinished && baseTask.Task.taskType == Package.Task.TaskType.GodWeaponTask)
			{
				if (GodWeaponManager.Instance.TownPlayQueue.get_Count() > 0 || GodWeaponManager.Instance.WeaponLock)
				{
					GodWeaponManager.Instance.HasWeaponTaskFinish = baseTask.Task.taskId;
				}
				else
				{
					EventDispatcher.Broadcast<int>("GuideManager.TaskFinish", baseTask.Task.taskId);
				}
			}
			else if (baseTask.Task.status == Package.Task.TaskStatus.TaskFinished && (baseTask.Task.taskType == Package.Task.TaskType.MainTask || baseTask.Task.taskType == Package.Task.TaskType.BranchTask))
			{
				EventDispatcher.Broadcast<int>("GuideManager.TaskFinish", baseTask.Task.taskId);
			}
			else if (baseTask.Task.taskType == Package.Task.TaskType.ChangeCareer && !baseTask.hasNextTask && !ChangeCareerManager.Instance.IsCareerChanged())
			{
				LinkNavigationManager.OpenChangeCareerUI();
				this.AutoTaskId = 0;
			}
		}
	}

	public void SendFinishTaskReq(int id, int npcId = 0)
	{
		this.mLastFinishTaskId = id;
		if (npcId > 0)
		{
			NetworkManager.Send(new TaskDataNty
			{
				taskId = id,
				extraData = npcId
			}, ServerType.Data);
			Debug.Log(string.Concat(new object[]
			{
				"<请求完成变身任务[",
				id,
				"], NPCid:",
				npcId,
				">"
			}));
		}
		else
		{
			NetworkManager.Send(new TaskDataNty
			{
				taskId = id
			}, ServerType.Data);
			Debug.Log("<请求完成[" + id + "]任务>");
		}
	}

	private void OnPushOtherData(short state, PushOtherData down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.SetTaskTimes(down.otherData);
			if (this.GuildTaskTimes <= 0 && this.GuildTaskId > 0)
			{
				this.GuildTaskId = 0;
				this.CurTaskId = 0;
			}
			if (this.RingTaskTimes <= 0 && this.RingTaskId > 0)
			{
				this.RingTaskId = 0;
				this.CurTaskId = 0;
			}
			if (this.RingTask2Times <= 0 && this.RingTask2Id > 0)
			{
				this.RingTask2Id = 0;
				this.CurTaskId = 0;
			}
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, null);
			EventDispatcher.Broadcast(EventNames.PushOtherData);
		}
	}

	public void SendTaskGroupAcceptReq(int number)
	{
		Debug.Log("<请求接取零城任务组[" + number + "]>");
		NetworkManager.Send(new TaskGroupAcceptReq
		{
			num = number
		}, ServerType.Data);
	}

	private void OnTaskGroupAcceptRes(short state, TaskGroupAcceptRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310043, false));
		EventDispatcher.Broadcast(EventNames.SortTaskList);
	}

	public void SendGetZeroCityPrizeReq(int number)
	{
		this.LastGetRewardZeroTaskGroupId = number;
		NetworkManager.Send(new GetZeroCityPrizeReq
		{
			num = number
		}, ServerType.Data);
		Debug.Log("<请求领取零城任务组[" + number + "]奖励>");
	}

	private void OnGetZeroCityPrizeRes(short state, GetZeroCityPrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("<返回领取零城任务组奖励>");
		EventDispatcher.Broadcast(EventNames.GetZeroTaskRewardRes);
		EventDispatcher.Broadcast(EventNames.SortTaskList);
	}

	public void SendZeroCityRefreshReq()
	{
		this.ZeroTaskRefreshLock = true;
		Debug.Log("<请求刷新零城任务>");
		NetworkManager.Send(new ZeroCityRefreshReq
		{
			free = this.ZeroTaskFreeTimes > 0
		}, ServerType.Data);
	}

	private void OnZeroCityRefreshRes(short state, ZeroCityRefreshRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.RefreshZeroTaskRes);
		EventDispatcher.Broadcast(EventNames.SortTaskList);
	}

	public void SendAbandonTaskReq(int num)
	{
		Debug.Log("<请求放弃零城任务>");
		NetworkManager.VitalSend(new AbandonTaskReq
		{
			number = num
		}, ServerType.Data);
	}

	private void OnAbandonTaskRes(short state, AbandonTaskRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310044, false));
		EventDispatcher.Broadcast(EventNames.SortTaskList);
	}

	private void OnExtraRewardPush(short state, ExtraRewardPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			ExtraRewardInfo info;
			EWaiRenWuJiangLi edata;
			for (int i = 0; i < down.record.get_Count(); i++)
			{
				info = down.record.get_Item(i);
				if (this.mExtraRewardInfos.Keys.Contains(info.taskType))
				{
					this.mExtraRewardInfos[info.taskType] = info;
				}
				else
				{
					this.mExtraRewardInfos.Add(info.taskType, info);
				}
				edata = this.ExtraRewardDatas.Find((EWaiRenWuJiangLi e) => e.taskType == info.taskType);
				int num;
				if (edata != null && this.TaskTimes.TryGetValue(edata.taskType, ref num) && !info.gotTimesPoint.Exists((int e) => e == edata.time))
				{
					this.ExtraRewardQueue.Enqueue(edata);
				}
			}
			EventDispatcher.Broadcast(EventNames.PushOtherData);
		}
	}

	public void SendGetExtraReward(int type, int time, bool isUseDiamond = false)
	{
		this.IsUseDiamond = isUseDiamond;
		this.LastGetExtraRewardTaskType = type;
		NetworkManager.Send(new GetExtraRewardReq
		{
			taskType = type,
			timePoint = time,
			useDiamond = isUseDiamond
		}, ServerType.Data);
		Debug.LogFormat("请求领取[{0}]任务额外奖励", new object[]
		{
			(Package.Task.TaskType)type
		});
	}

	private void OnGetExtraRewardRes(short state, GetExtraRewardRes down = null)
	{
		if ((int)state == Status.TASK_HAS_GOT_PRIZE || (int)state == Status.TASK_TIMES_NOT_ENOUGH)
		{
			EventDispatcher.Broadcast<int, List<ItemBriefInfo>>(EventNames.GetExtraRewardRes, 0, null);
			return;
		}
		if (state != 0)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"状态错误：错误码为: ",
				state,
				" ",
				Status.GetStatusDesc((int)state)
			}));
			EventDispatcher.Broadcast<int, List<ItemBriefInfo>>(EventNames.GetExtraRewardRes, -1, null);
			return;
		}
		if (down != null)
		{
			EventDispatcher.Broadcast<int, List<ItemBriefInfo>>(EventNames.GetExtraRewardRes, 1, down.item);
			Debug.LogFormat("返回领取[{0}]任务额外奖励", new object[]
			{
				(Package.Task.TaskType)this.LastGetExtraRewardTaskType
			});
		}
	}

	public void SendChallengeMirrorReq(int id)
	{
		NetworkManager.Send(new ChallengeMirrorReq
		{
			taskId = id
		}, ServerType.Data);
	}

	private void OnChallengeMirrorRes(short state, ChallengeMirrorRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		WaitUI.CloseUI(0u);
	}

	public void SendExitMirrorReq()
	{
		Debug.Log("<请求退出野外副本>");
		NetworkManager.Send(new ExitMirrorReq
		{
			pos = Pos.FromScenePos(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position())
		}, ServerType.Data);
	}

	private void OnExitMirrorRes(short state, ExitMirrorRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.ExitTaskFightResult);
	}

	private void OnResultMirrorNty(short state, ResultMirrorNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			FieldInstance.Instance.GetInstanceResult(down);
			EventDispatcher.Broadcast<bool>(EventNames.MirrorResultNty, down.isWin);
			Debug.Log("野外副本结算推送:" + down.isWin);
		}
	}

	private void SetTaskTimes(List<OtherData> datas)
	{
		OtherData odata;
		for (int i = 0; i < datas.get_Count(); i++)
		{
			odata = datas.get_Item(i);
			Debug.LogFormat("=====每日任务[{0}]剩余:{1}次, VIP额外:{2}次=====", new object[]
			{
				(Package.Task.TaskType)odata.taskType,
				odata.taskTimes,
				odata.lastVipAddTimes
			});
			if (odata.taskType == 8)
			{
				this.ZeroTaskFreeTimes = odata.freeRefreshTimes;
				this.ZeroTaskRefreshTime = odata.nextFreshTime;
			}
			else if (odata.taskType == 3)
			{
				this.RingMaxTimes = this.ConstRingMaxTimes + odata.lastVipAddTimes;
			}
			if (this.TaskTimes.ContainsKey(odata.taskType))
			{
				this.TaskTimes.set_Item(odata.taskType, odata.taskTimes);
			}
			else
			{
				this.TaskTimes.Add(odata.taskType, odata.taskTimes);
			}
			if (this.mExtraRewardInfos.Keys.Contains(odata.taskType))
			{
				this.mExtraRewardInfos[odata.taskType].commitTimes = odata.taskTimes;
			}
			else
			{
				ExtraRewardInfo extraRewardInfo = new ExtraRewardInfo();
				extraRewardInfo.taskType = odata.taskType;
				extraRewardInfo.commitTimes = odata.taskTimes;
				this.mExtraRewardInfos[odata.taskType] = extraRewardInfo;
			}
			EWaiRenWuJiangLi eWaiRenWuJiangLi = this.ExtraRewardDatas.Find((EWaiRenWuJiangLi e) => e.taskType == odata.taskType);
			if (this.mExtraRewardInfos != null && eWaiRenWuJiangLi != null && this.mExtraRewardInfos.Keys.Contains(eWaiRenWuJiangLi.taskType))
			{
				ExtraRewardInfo extraRewardInfo2 = this.mExtraRewardInfos[eWaiRenWuJiangLi.taskType];
				if (eWaiRenWuJiangLi.taskType == 3)
				{
					if (this.RingMaxTimes - odata.taskTimes == eWaiRenWuJiangLi.time && (extraRewardInfo2 == null || extraRewardInfo2.gotTimesPoint == null || extraRewardInfo2.gotTimesPoint.get_Count() <= 0 || extraRewardInfo2.commitTimes > extraRewardInfo2.gotTimesPoint.get_Item(0)))
					{
						this.ExtraRewardQueue.Enqueue(eWaiRenWuJiangLi);
					}
				}
				else if (eWaiRenWuJiangLi.taskType == 8 && this.ZeroMaxTimes - odata.taskTimes == eWaiRenWuJiangLi.time && (extraRewardInfo2 == null || extraRewardInfo2.gotTimesPoint == null || extraRewardInfo2.gotTimesPoint.get_Count() <= 0 || extraRewardInfo2.commitTimes > extraRewardInfo2.gotTimesPoint.get_Item(0)))
				{
					this.ExtraRewardQueue.Enqueue(eWaiRenWuJiangLi);
				}
			}
		}
	}

	private void OnRoleSelfProfessionChange()
	{
		this.NextDeleteTask(this.CareerTaskId);
		this.CareerTaskId = 0;
		EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, null);
		EventDispatcher.Broadcast(EventNames.UpdateMainTask);
	}

	private void ActiveMainTask()
	{
		BaseTask baseTask = null;
		if (this.GetTask(this.MainTaskId, out baseTask, true))
		{
			baseTask.IsActive = true;
			if (baseTask.Data.quickReceive == 1 && baseTask.Task.status == Package.Task.TaskStatus.TaskCanAccept)
			{
				baseTask.Accept();
			}
		}
		this.GodWeaponTaskId = 0;
	}

	private BaseTask SetTaskSerData(Package.Task task, bool isLoginPush = false)
	{
		if (task == null || task.taskId <= 0)
		{
			Debug.Log("<color=red>Error:</color>后端任务数据异常!!!");
			return null;
		}
		BaseTask baseTask = null;
		if (this.GetTask(task.taskId, out baseTask, false))
		{
			if (this.mNextDestroyTaskId == task.taskId)
			{
				Debug.Log("回收任务[<color=white>" + this.mNextDestroyTaskId + "</color>]");
				this.mNextDestroyTaskId = 0;
			}
			if (task.extParams.get_Count() == 6)
			{
				if (task.status == baseTask.Task.status && task.count == baseTask.Task.count && task.extParams.get_Item(0) == baseTask.Task.extParams.get_Item(0) && task.extParams.get_Item(4) == baseTask.Task.extParams.get_Item(4) && task.extParams.get_Item(5) == baseTask.Task.extParams.get_Item(5))
				{
					Debug.LogFormat("<任务[{0}]状态:{1} 组[{2}]状态:{3} 进度[{4}/{5}]<color=red>重复推送，已忽略</color>>", new object[]
					{
						task.taskId,
						task.status,
						task.extParams.get_Item(0),
						(Package.Task.TaskStatus)task.extParams.get_Item(4),
						task.extParams.get_Item(5),
						task.extParams.get_Item(3)
					});
					return null;
				}
			}
			else if (task.status == baseTask.Task.status && task.count == baseTask.Task.count)
			{
				Debug.LogFormat("<任务[{0}]状态[{1}]<color=red>重复推送，已忽略</color>>", new object[]
				{
					task.taskId,
					task.status
				});
				return null;
			}
			baseTask.SetTask(task, true);
			this.SetZeroTaskId(task);
		}
		else
		{
			baseTask = this.CreateTask(task);
		}
		if (task.extParams.get_Count() == 6)
		{
			Debug.LogFormat("<更新任务[<color=white>{0}</color>]状态:[<color=white>{1}</color>] 组[<color=white>{2}</color>]状态[<color=white>{3}</color>] 进度[<color=white>{4}/{5}</color>]>", new object[]
			{
				task.taskId,
				task.status,
				task.extParams.get_Item(0),
				(Package.Task.TaskStatus)task.extParams.get_Item(4),
				task.extParams.get_Item(5),
				task.extParams.get_Item(3)
			});
		}
		else
		{
			Debug.LogFormat("<更新任务[<color=white>{0}</color>]状态[<color=white>{1}</color>]>", new object[]
			{
				task.taskId,
				task.status
			});
		}
		this.CheckAllTaskId(task);
		if (baseTask != null)
		{
			if (task.status == Package.Task.TaskStatus.TaskNotOpen || task.status == Package.Task.TaskStatus.TaskFinished)
			{
				baseTask.IsActive = false;
			}
			if (task.taskType == Package.Task.TaskType.MainTask && this.GodWeaponTaskId > 0)
			{
				BaseTask baseTask2 = null;
				if (this.GetTask(this.GodWeaponTaskId, out baseTask2, true) && baseTask2.Task.status != Package.Task.TaskStatus.TaskNotOpen && baseTask2.Task.status != Package.Task.TaskStatus.TaskFinished)
				{
					baseTask.IsActive = false;
				}
			}
		}
		if (task.taskType == Package.Task.TaskType.MainTask || (task.taskType == Package.Task.TaskType.ChangeCareer && EntityWorld.Instance.EntSelf.Lv >= this.CareerMaxLevel))
		{
			EventDispatcher.Broadcast(EventNames.UpdateMainTask);
		}
		if (!isLoginPush)
		{
			this.CheckQuickTask(baseTask);
			this.CheckZeroTaskGroupReward(baseTask);
		}
		return baseTask;
	}

	private void CheckAllTaskId(Package.Task task)
	{
		switch (task.taskType)
		{
		case Package.Task.TaskType.MainTask:
			this.MainTaskId = this.CheckOnlyTask(this.MainTaskId, task.taskId);
			EventDispatcher.Broadcast<int>(EventNames.UpdateTaskId, task.taskId);
			break;
		case Package.Task.TaskType.RingTask:
			this.RingTaskId = this.CheckOnlyTask(this.RingTaskId, task.taskId);
			break;
		case Package.Task.TaskType.GuildTask:
			this.GuildTaskId = this.CheckOnlyTask(this.GuildTaskId, task.taskId);
			break;
		case Package.Task.TaskType.RingTask2:
			this.RingTask2Id = this.CheckOnlyTask(this.RingTask2Id, task.taskId);
			break;
		case Package.Task.TaskType.ChangeCareer:
			this.CareerTaskId = this.CheckOnlyTask(this.CareerTaskId, task.taskId);
			break;
		case Package.Task.TaskType.GodWeaponTask:
			this.GodWeaponTaskId = this.CheckOnlyTask(this.GodWeaponTaskId, task.taskId);
			break;
		}
		this.CurTaskId = task.taskId;
	}

	private int CheckOnlyTask(int oldId, int newId)
	{
		if (oldId > 0 && oldId != newId)
		{
			this.mIsNeedRefreshList = true;
			this.NextDeleteTask(oldId);
		}
		return newId;
	}

	private BaseTask CreateTask(Package.Task task)
	{
		BaseTask baseTask = null;
		RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(task.taskId);
		if (renWuPeiZhi != null)
		{
			switch (renWuPeiZhi.type)
			{
			case 1:
				baseTask = new TalkTask(task);
				break;
			case 2:
				baseTask = new CollectTask(task);
				break;
			case 3:
				baseTask = new ProtectTask(task);
				break;
			case 4:
			case 5:
			case 6:
			case 11:
			case 12:
			case 16:
				baseTask = new FightTask(task);
				break;
			case 7:
				baseTask = new LinkTask(task, 21, -1);
				break;
			case 8:
				baseTask = new LinkTask(task, 22, -1);
				break;
			case 9:
			case 18:
			case 19:
			case 20:
			case 27:
			case 29:
			case 30:
				baseTask = new LinkTask(task, 0, 0);
				break;
			case 10:
				baseTask = new LinkTask(task, delegate
				{
					PetManager.Instance.Jump2Formation(true);
				});
				break;
			case 13:
				baseTask = new ChangeTask(task);
				break;
			case 14:
				baseTask = new LinkTask(task, 61, -1);
				break;
			case 15:
				baseTask = new WeaponTask(task);
				break;
			case 17:
				baseTask = new TameTask(task);
				break;
			case 21:
			case 22:
				baseTask = new HuntTask(task);
				break;
			case 23:
				baseTask = new UpgradeTask(task);
				break;
			case 24:
				baseTask = new LinkTask(task, 37, -1);
				break;
			case 25:
				baseTask = new LinkTask(task, 4, -1);
				break;
			case 26:
				baseTask = new LinkTask(task, 112, -1);
				break;
			case 28:
				baseTask = new MushroomTask(task);
				break;
			case 31:
				baseTask = new LinkTask(task, 5, -1);
				break;
			case 32:
				baseTask = new LinkTask(task, delegate
				{
					PetManager.Instance.Jump2PetLevelUI();
				});
				break;
			case 33:
				baseTask = new LinkTask(task, 45, -1);
				break;
			default:
				Debug.Log("<color=red>Error:</color>没有指定[" + renWuPeiZhi.type + "]任务类型!!!");
				break;
			}
			if (baseTask != null)
			{
				this.mTaskMap.Add(task.taskId, baseTask);
				this.SetZeroTaskId(task);
			}
		}
		if (baseTask == null)
		{
			Debug.Log("<color=red>Error:</color>创建任务[" + task.taskId + "]失败!!!");
		}
		return baseTask;
	}

	private void SetZeroTaskId(Package.Task task)
	{
		if (task.taskType == Package.Task.TaskType.ZeroCity)
		{
			int num = this.mZeroTaskId[task.extParams.get_Item(0)];
			if (num > 0 && task.taskId != num)
			{
				int num2 = this.ExistsZeroId(task.taskId);
				if (num2 > -1)
				{
					this.mZeroTaskId[num2] = 0;
				}
				else
				{
					this.NextDeleteTask(num);
				}
			}
			this.mZeroTaskId[task.extParams.get_Item(0)] = task.taskId;
		}
	}

	private int ExistsZeroId(int id)
	{
		for (int i = 0; i < this.mZeroTaskId.Length; i++)
		{
			if (this.mZeroTaskId[i] == id)
			{
				return i;
			}
		}
		return -1;
	}

	private void CheckQuickTask(BaseTask cell)
	{
		bool arg_35_0;
		if (MySceneManager.Instance != null)
		{
			arg_35_0 = this.mAllCityDatas.Exists((ZhuChengPeiZhi e) => e.scene == MySceneManager.Instance.CurSceneID);
		}
		else
		{
			arg_35_0 = false;
		}
		bool flag = arg_35_0;
		if (cell != null && cell.Data != null && cell.IsActive)
		{
			if (cell.Task.status == Package.Task.TaskStatus.TaskCanAccept && cell.Data.quickReceive == 1)
			{
				if (cell.Task.taskType == Package.Task.TaskType.ZeroCity && cell.Task.extParams.get_Item(4) == 2 && cell.Task.extParams.get_Item(5) == 0)
				{
					return;
				}
				if (this.CanAcceptTask(cell, flag))
				{
					this.QuickAcceptTask(cell);
				}
				else
				{
					this.QuickAcceptQueue.Add(cell);
				}
			}
			else if (cell.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize && (cell.Data.quickAchieve == 1 || cell.Data.quickComplete == 1))
			{
				if (flag)
				{
					if (cell.Data.quickAchieve == 1)
					{
						this.QuickCommitTask(cell);
					}
					else if (cell.Data.quickComplete == 1)
					{
						LinkNavigationManager.OpenTaskDescUI(cell.Task.taskId, false, true);
					}
				}
				else if (cell.Data.quickAchieve == 1)
				{
					if (this.CanCommitTask(cell, flag))
					{
						this.QuickCommitTask(cell);
					}
					else
					{
						this.QuickCommitQueue.Add(cell);
					}
				}
			}
		}
	}

	private bool CanAcceptTask(BaseTask cell, bool isInCity)
	{
		return cell is LinkTask || (cell is TriggerTask && isInCity);
	}

	private bool CanCommitTask(BaseTask cell, bool isInCity)
	{
		if (cell is LinkTask)
		{
			return cell.Data.type != 21 && cell.Data.type != 19;
		}
		return cell is TriggerTask && isInCity;
	}

	private void QuickAcceptTask(BaseTask cell)
	{
		Debug.Log("↓↓↓↓↓快速接取任务↓↓↓↓↓");
		cell.Accept();
	}

	private void QuickCommitTask(BaseTask cell)
	{
		Debug.Log("↓↓↓↓↓快速提交任务↓↓↓↓↓");
		cell.Commit(false);
	}

	private void CheckZeroTaskGroupReward(BaseTask cell)
	{
		if (cell != null && cell.Task.taskType == Package.Task.TaskType.ZeroCity && cell.Task.extParams.get_Item(4) == 4)
		{
			this.SendGetZeroCityPrizeReq(cell.Task.extParams.get_Item(0));
		}
	}

	private void NextDeleteTask(int id)
	{
		if (this.mNextDestroyTaskId == id)
		{
			return;
		}
		this.DeleteTask(this.mNextDestroyTaskId, true);
		this.mNextDestroyTaskId = id;
		BaseTask baseTask = null;
		if (this.GetTask(this.mNextDestroyTaskId, out baseTask, true))
		{
			baseTask.KillTask();
			Debug.Log("-----失活任务[" + baseTask.Task.taskId + "]-----");
		}
	}

	private void DeleteTask(BaseTask task)
	{
		if (task != null)
		{
			Debug.Log("~~~~~移除任务[" + task.Task.taskId + "]~~~~~");
			this.mTaskMap.Remove(task.Task.taskId);
			task.Dispose();
		}
	}

	private void DeleteTask(int taskId, bool showLog = true)
	{
		this.DeleteTask(this.GetTask(taskId, showLog));
	}

	private void OnLoadSceneStart(int curSceneId, int loadSceneId)
	{
		this.SelectShowGoods(loadSceneId);
	}

	private XDict<int, int> SelectShowGoods(int sceneId)
	{
		this.GoodsModels.Clear();
		BaseTask task = this.GetTask(this.MainTaskId, true);
		if (task == null)
		{
			return this.GoodsModels;
		}
		Package.Task.TaskStatus status = task.Task.status;
		Package.Task.TaskStatus godState = Package.Task.TaskStatus.TaskNotOpen;
		task = this.GetTask(this.GodWeaponTaskId, true);
		if (task != null)
		{
			godState = task.Task.status;
		}
		List<CaiJiPeiZhi> list = DataReader<CaiJiPeiZhi>.DataList;
		for (int i = 0; i < list.get_Count(); i++)
		{
			if (this.SelectCollectNPC(sceneId, list.get_Item(i), status, godState) && !this.GoodsModels.ContainsKey(list.get_Item(i).id))
			{
				this.GoodsModels.Add(list.get_Item(i).id, 0);
			}
		}
		List<BaseTask> list2 = new List<BaseTask>(this.mTaskMap.get_Values());
		for (int j = 0; j < list2.get_Count(); j++)
		{
			if (list2.get_Item(j).IsActive)
			{
				if (list2.get_Item(j) is TameTask)
				{
					TameTask tameTask = list2.get_Item(j) as TameTask;
					if (tameTask.Task.status == Package.Task.TaskStatus.TaskReceived)
					{
						CaiJiPeiZhi caiJiPeiZhi = tameTask.GetGoodsData();
						if (caiJiPeiZhi != null && caiJiPeiZhi.scene == sceneId && caiJiPeiZhi.model > 0 && !this.GoodsModels.ContainsKey(caiJiPeiZhi.id))
						{
							this.GoodsModels.Add(caiJiPeiZhi.id, 0);
						}
					}
				}
				else if (list2.get_Item(j) is CollectTask)
				{
					CollectTask collectTask = list2.get_Item(j) as CollectTask;
					list = collectTask.GetGoodsDataList();
					if (list == null)
					{
						break;
					}
					for (int k = 0; k < list.get_Count(); k++)
					{
						CaiJiPeiZhi caiJiPeiZhi = list.get_Item(k);
						if (caiJiPeiZhi != null && caiJiPeiZhi.scene == sceneId && caiJiPeiZhi.model > 0 && (collectTask.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize || collectTask.Task.status == Package.Task.TaskStatus.TaskFinished || k < collectTask.Task.count) && this.GoodsModels.ContainsKey(caiJiPeiZhi.id))
						{
							this.GoodsModels.Remove(caiJiPeiZhi.id);
						}
					}
				}
			}
		}
		return this.GoodsModels;
	}

	private bool SelectCollectNPC(int sceneId, CaiJiPeiZhi goods, Package.Task.TaskStatus mainState, Package.Task.TaskStatus godState)
	{
		if (goods.scene == sceneId && goods.model > 0 && goods.type == 1)
		{
			if (this.MainTaskId > 0 && goods.behindMainTask.get_Count() == 2 && goods.behindMainTask.get_Item(1) < 200000 && goods.behindMainTask.get_Item(1) >= 100000 && goods.behindMainTask.get_Item(1) >= this.MainTaskId && (goods.frontMainTask.get_Count() < 2 || (goods.frontMainTask.get_Count() == 2 && (goods.frontMainTask.get_Item(1) < this.MainTaskId || (goods.frontMainTask.get_Item(1) == this.MainTaskId && goods.frontMainTask.get_Item(0) == 1 && mainState == Package.Task.TaskStatus.TaskReceived)))))
			{
				return true;
			}
			if (this.GodWeaponTaskId > 0 && goods.behindMainTask.get_Count() == 2 && goods.behindMainTask.get_Item(1) < 800000 && goods.behindMainTask.get_Item(1) >= 700000 && goods.behindMainTask.get_Item(1) >= this.GodWeaponTaskId && (goods.frontMainTask.get_Count() < 2 || (goods.frontMainTask.get_Count() == 2 && (goods.frontMainTask.get_Item(1) < this.GodWeaponTaskId || (goods.frontMainTask.get_Item(1) == this.GodWeaponTaskId && goods.frontMainTask.get_Item(0) == 1 && godState == Package.Task.TaskStatus.TaskReceived)))))
			{
				return true;
			}
		}
		return false;
	}

	private void OnRoleSelfLevelUp()
	{
		if (this.CareerTaskId > 0 && EntityWorld.Instance.EntSelf.Lv == this.CareerMaxLevel)
		{
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, null);
		}
	}

	private NPCInformation CreateNpcInfo(int npcId, int beginNpcId, int endNpcId)
	{
		NPC nPC = DataReader<NPC>.Get(npcId);
		NPCInformation result = null;
		BaseTask baseTask = null;
		if (nPC != null && this.GetTask(this.MainTaskId, out baseTask, true))
		{
			if (nPC.id == beginNpcId)
			{
				if (baseTask.Task.status <= Package.Task.TaskStatus.TaskCanAccept)
				{
					result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
				}
				else if (baseTask.Task.status == Package.Task.TaskStatus.TaskReceived)
				{
					if (this.CareerTaskId > 0 && EntityWorld.Instance.EntSelf.Lv >= this.CareerMaxLevel)
					{
						result = this.NewNpcInfo(nPC, NPCStatus.NAV_TO_POINT);
					}
					else
					{
						result = this.NewNpcInfo(nPC, NPCStatus.FOLLOW);
					}
				}
			}
			else if (nPC.id == endNpcId)
			{
				if (baseTask.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize)
				{
					result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
				}
			}
			else if (nPC.frontMainTask.get_Count() == 0 && nPC.behindMainTask.get_Count() == 0)
			{
				result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
			}
			else if (nPC.frontMainTask.get_Count() == 2 && nPC.frontMainTask.get_Item(1) == baseTask.Data.id && nPC.behindMainTask.get_Count() == 2 && nPC.behindMainTask.get_Item(1) == baseTask.Data.id)
			{
				if (baseTask.Task.status == Package.Task.TaskStatus.TaskCanAccept || baseTask.Task.status == Package.Task.TaskStatus.TaskReceived)
				{
					result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
				}
			}
			else if (nPC.frontMainTask.get_Count() == 2 && nPC.frontMainTask.get_Item(1) == baseTask.Data.id)
			{
				if ((nPC.frontMainTask.get_Item(0) == 1 && baseTask.Task.status >= Package.Task.TaskStatus.TaskCanAccept) || (nPC.frontMainTask.get_Item(0) == 2 && baseTask.Task.status >= Package.Task.TaskStatus.WaitingToClaimPrize))
				{
					result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
				}
			}
			else if (nPC.behindMainTask.get_Count() == 2 && nPC.behindMainTask.get_Item(1) == baseTask.Data.id)
			{
				if ((nPC.behindMainTask.get_Item(0) == 1 && baseTask.Task.status < Package.Task.TaskStatus.TaskCanAccept) || (nPC.behindMainTask.get_Item(0) == 2 && baseTask.Task.status < Package.Task.TaskStatus.WaitingToClaimPrize))
				{
					result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
				}
			}
			else if ((nPC.frontMainTask.get_Count() == 0 && nPC.behindMainTask.get_Count() == 2 && nPC.behindMainTask.get_Item(1) > baseTask.Data.id) || (nPC.behindMainTask.get_Count() == 0 && nPC.frontMainTask.get_Count() == 2 && nPC.frontMainTask.get_Item(1) < baseTask.Data.id) || (nPC.frontMainTask.get_Count() == 2 && nPC.frontMainTask.get_Item(1) < baseTask.Data.id && nPC.behindMainTask.get_Count() == 2 && nPC.behindMainTask.get_Item(1) > baseTask.Data.id))
			{
				result = this.NewNpcInfo(nPC, NPCStatus.STATIC);
			}
		}
		return result;
	}

	private NPCInformation NewNpcInfo(NPC npc, NPCStatus status)
	{
		NPCInformation nPCInformation = new NPCInformation();
		nPCInformation.id = npc.id;
		if (npc.position.get_Count() == 3)
		{
			nPCInformation.position = new Vector3((float)npc.position.get_Item(0), (float)npc.position.get_Item(1), (float)npc.position.get_Item(2));
		}
		else
		{
			nPCInformation.position = Vector3.get_zero();
		}
		nPCInformation.status = status;
		return nPCInformation;
	}

	private ArrayList GetTaskCounts()
	{
		try
		{
			this.mPlayerID = EntityWorld.Instance.EntSelf.ID;
		}
		catch
		{
			this.mPlayerID = 0L;
		}
		string stringPrefs = PlayerPrefsExt.GetStringPrefs(this.TaskCountsKey);
		return MiniJSON.jsonDecode(stringPrefs) as ArrayList;
	}

	private void OnTownUISetActive(bool isVisable)
	{
		if (!isVisable)
		{
			return;
		}
		bool flag = false;
		if (CityManager.Instance.NeedSwitchCity && CityManager.Instance.NeedDelayEnterNPC)
		{
			BaseTask task = null;
			if (this.IsTaskNpc(this.NpcId) && this.GetTask(this.CurTaskId, out task, true))
			{
				this.mExecuteDelayId = TimerHeap.AddTimer(600u, 0, delegate
				{
					if (GuideManager.Instance.guide_lock)
					{
						Debug.Log("任务[" + task.Task.taskId + "]延迟后被新手开关拦截!!!");
						return;
					}
					task.OnEnterNPC(this.NpcId);
				});
				flag = true;
			}
		}
		else if (this.mEnterNpcAgainOnEnable > 0)
		{
			BaseTask baseTask = null;
			if (this.IsTaskNpc(this.mEnterNpcAgainOnEnable) && this.GetTask(this.CurTaskId, out baseTask, true))
			{
				baseTask.OnEnterNPC(this.mEnterNpcAgainOnEnable);
			}
			this.mEnterNpcAgainOnEnable = 0;
		}
		int num = 0;
		int num2 = 0;
		if (this.QuickAcceptQueue.get_Count() > 0)
		{
			for (int i = 0; i < this.QuickAcceptQueue.get_Count(); i++)
			{
				if (this.QuickAcceptQueue.get_Item(i) is LinkTask)
				{
					this.QuickAcceptTask(this.QuickAcceptQueue.get_Item(i));
				}
				else if (this.QuickAcceptQueue.get_Item(i) is TriggerTask)
				{
					if (num <= 0)
					{
						if (flag)
						{
							Debug.Log("已经有进入NPC事件触发!!! by OnTownUISetActive");
						}
						else
						{
							num = this.QuickAcceptQueue.get_Item(i).Task.taskId;
							this.QuickAcceptTask(this.QuickAcceptQueue.get_Item(i));
						}
					}
					else if (num > 0)
					{
						Debug.Log("<color=red>Error:</color>配置冲突，多个触发类型任务同时接取[" + num + "]");
					}
				}
			}
			this.QuickAcceptQueue.Clear();
		}
		if (this.QuickCommitQueue.get_Count() > 0)
		{
			for (int j = 0; j < this.QuickCommitQueue.get_Count(); j++)
			{
				if (this.QuickCommitQueue.get_Item(j) is LinkTask)
				{
					this.QuickCommitTask(this.QuickCommitQueue.get_Item(j));
				}
				else if (this.QuickCommitQueue.get_Item(j) is TriggerTask)
				{
					if (num <= 0 && num2 <= 0)
					{
						if (flag)
						{
							Debug.Log("已经有进入NPC事件触发!!! by OnTownUISetActive");
						}
						else
						{
							num2 = this.QuickCommitQueue.get_Item(j).Task.taskId;
							this.QuickCommitTask(this.QuickCommitQueue.get_Item(j));
						}
					}
					else if (num > 0)
					{
						Debug.Log("<color=red>Error:</color>配置冲突，多个触发类型任务同时接取[" + num + "]");
					}
					else if (num2 > 0)
					{
						Debug.Log("<color=red>Error:</color>配置冲突，多个触发类型任务同时提交[" + num2 + "]");
					}
				}
			}
			this.QuickCommitQueue.Clear();
		}
		if (this.OpenNpcMenuAgainOnEnable != null)
		{
			EventDispatcher.Broadcast<NPC>(EventNames.OpenNPCMenu, this.OpenNpcMenuAgainOnEnable);
			this.OpenNpcMenuAgainOnEnable = null;
		}
		if (!flag && num <= 0 && num2 <= 0)
		{
			if (this.IsNeedExecuteTask())
			{
				this.DelayExecute();
			}
			else if (this.OpenNpcSystemAgainOnEnable != null)
			{
				EventDispatcher.Broadcast<NPC>(EventNames.OpenNPCSystem, this.OpenNpcSystemAgainOnEnable);
				CityManager.Instance.NeedDelayEnterNPC = false;
				this.OpenNpcSystemAgainOnEnable = null;
			}
		}
	}

	private bool IsNeedExecuteTask()
	{
		if (GuideManager.Instance.guide_lock)
		{
			Debug.Log("任务延迟执行被新手打断!!!");
			return false;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("任务延迟执行被神器打断!!!");
			return false;
		}
		BaseTask task = this.GetTask(this.CurTaskId, true);
		if (task != null)
		{
			if (task is FightTask && (task as FightTask).IsVictory)
			{
				(task as FightTask).IsVictory = false;
				return true;
			}
			if (this.IsSwitchCityByTask)
			{
				this.IsSwitchCityByTask = false;
				return true;
			}
			if (this.IsOpenAgainOnEnable)
			{
				this.IsOpenAgainOnEnable = false;
				return true;
			}
		}
		return false;
	}

	private void DelayExecute()
	{
		this.mExecuteDelayId = TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (GuideManager.Instance.guide_lock)
			{
				Debug.Log("任务延时后被新手开关拦截!!!");
				return;
			}
			this.ExecuteTask(0, false);
		});
	}

	private ButtonInfoData GetButton2Promote(int systemId, int chineseId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(chineseId, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				LinkNavigationManager.SystemLink(systemId, true, null);
			}
		};
	}

	private void OnZeroPointTrigger()
	{
		this.ExtraRewardQueue.Clear();
	}

	public bool GetTask(int id, out BaseTask task, bool showLog = true)
	{
		if (this.mTaskMap.TryGetValue(id, ref task))
		{
			return true;
		}
		if (id > 0 && showLog)
		{
			Debug.Log("<color=red>Error:</color>任务列表中找不到[<color=white>" + id + "</color>]任务!!!");
		}
		return false;
	}

	public BaseTask GetTask(int id, bool showLog = true)
	{
		BaseTask result = null;
		this.GetTask(id, out result, showLog);
		return result;
	}

	public bool ExecuteTask(int taskId = 0, bool isFast = false)
	{
		if (taskId <= 0)
		{
			taskId = this.CurTaskId;
		}
		return this.ExecuteTask(this.GetTask(taskId, true), isFast, false);
	}

	public bool ExecuteTask(BaseTask cell, bool isFast = false, bool isFromClick = false)
	{
		if (cell == null)
		{
			this.CurTaskId = 0;
			Debug.Log("<color=red>Error:</color>没指定当前任务！");
			return false;
		}
		if (!isFromClick && cell is LinkTask)
		{
			return false;
		}
		this.CurTaskId = cell.Task.taskId;
		if (this.IsAutoTaskType((int)cell.Task.taskType))
		{
			this.AutoTaskId = this.CurTaskId;
		}
		return cell.Execute(isFast || this.IsAutoFast, isFromClick);
	}

	public bool IsTaskNpc(int npcId)
	{
		BaseTask baseTask = null;
		if (npcId <= 0 || !this.GetTask(this.CurTaskId, out baseTask, true))
		{
			return false;
		}
		if (baseTask.Task.status == Package.Task.TaskStatus.TaskCanAccept)
		{
			return this.IsSameNpc(baseTask.Data.linkNpc1, npcId);
		}
		if (baseTask.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize)
		{
			return this.IsSameNpc(baseTask.Data.linkNpc2, npcId);
		}
		if (baseTask.Task.status == Package.Task.TaskStatus.TaskReceived)
		{
			if (baseTask.Data.type == 1)
			{
				if (baseTask.Data.target.get_Count() > 1)
				{
					return this.IsSameNpc(baseTask.Data.target.get_Item(0), npcId);
				}
			}
			else if (baseTask.Data.type == 3)
			{
				if (baseTask.Data.target.get_Count() == 2)
				{
					return this.IsSameNpc(baseTask.Data.target.get_Item(0), npcId) || this.IsSameNpc(baseTask.Data.target.get_Item(1), npcId);
				}
			}
			else
			{
				if (baseTask.Data.type == 6 || baseTask.Data.type == 5 || baseTask.Data.type == 4 || baseTask.Data.type == 16)
				{
					return npcId == baseTask.Data.instanceNpc;
				}
				if (baseTask.Data.type == 13)
				{
					if (baseTask.Data.target.get_Count() > 1)
					{
						return this.IsSameNpc(baseTask.Data.target.get_Item(1), npcId);
					}
				}
				else if (baseTask.Data.type == 23 && baseTask.Data.target.get_Count() > 1)
				{
					return this.IsSameNpc(baseTask.Data.target.get_Item(1), npcId);
				}
			}
		}
		return false;
	}

	public bool IsSameNpc(int idA, int idB)
	{
		if (idA == idB)
		{
			return true;
		}
		NPC nPC = DataReader<NPC>.Get(idA);
		if (nPC == null)
		{
			return false;
		}
		NPC nPC2 = DataReader<NPC>.Get(idB);
		return nPC2 != null && (nPC.sameNpc > 0 && nPC2.sameNpc > 0) && nPC.sameNpc == nPC2.sameNpc;
	}

	public bool HasNpcId(int id)
	{
		return id > 0 && this.mNpcIdList.Exists((int e) => e == id);
	}

	public void ClearNpcIds()
	{
		this.mNpcIdList.Clear();
	}

	public XDict<int, NPCInformation> SelectShowNPC(List<int> list)
	{
		XDict<int, NPCInformation> xDict = new XDict<int, NPCInformation>();
		BaseTask baseTask = null;
		int beginNpcId = 0;
		int endNpcId = 0;
		if (this.GetTask(this.MainTaskId, out baseTask, true) && baseTask.Data.type == 3 && baseTask.Data.target.get_Count() == 2)
		{
			beginNpcId = baseTask.Data.target.get_Item(0);
			endNpcId = baseTask.Data.target.get_Item(1);
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			NPCInformation nPCInformation = this.CreateNpcInfo(list.get_Item(j), beginNpcId, endNpcId);
			if (nPCInformation != null)
			{
				xDict.Add(list.get_Item(j), nPCInformation);
			}
		}
		int i;
		for (i = 0; i < this.mNpcIdList.get_Count(); i++)
		{
			this.mNpcIdList.set_Item(i, xDict.Keys.Find((int e) => e == this.mNpcIdList.get_Item(i)));
		}
		return xDict;
	}

	public void RemoveGoodsById(int goodsId)
	{
		if (this.GoodsModels.ContainsKey(goodsId))
		{
			this.GoodsModels.Remove(goodsId);
		}
	}

	public bool IsGoingTask(int turnId)
	{
		BaseTask baseTask = null;
		return this.GetTask(this.CurTaskId, out baseTask, true) && (baseTask.Task != null && baseTask.Task.status == Package.Task.TaskStatus.TaskReceived && baseTask.Data != null && baseTask.Data.type >= 4 && baseTask.Data.target.get_Count() > 0) && baseTask.Data.target.get_Item(0) == turnId;
	}

	public bool GetHearthPosition(out Vector3 hearthPosition)
	{
		hearthPosition = Vector3.get_zero();
		BaseTask baseTask = null;
		if (!this.GetTask(this.CurTaskId, out baseTask, true))
		{
			return false;
		}
		this.IsSwitchCityByTask = true;
		if (baseTask.Task.status == Package.Task.TaskStatus.TaskReceived && baseTask.Data.transferPoint.get_Count() == 3)
		{
			hearthPosition = new Vector3((float)baseTask.Data.transferPoint.get_Item(0), (float)baseTask.Data.transferPoint.get_Item(1), (float)baseTask.Data.transferPoint.get_Item(2)) * 0.01f;
			return true;
		}
		return false;
	}

	public void NormalTalkUI()
	{
		this.OpenTalkUI(DataReader<NPC>.Get(this.NpcId).word, true, null, this.NpcId);
		SoundManager.Instance.PlayNPC(this.NpcId);
	}

	public void OpenTalkUI(List<int> talkIds, bool isMask = false, Action CallBack = null, int npcId = 0)
	{
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("对话被神器开关拦截!!!");
			return;
		}
		if (UIManagerControl.Instance.IsOpen("TownUI") && !UIManagerControl.Instance.IsOpen("TalkUI"))
		{
			GuideManager.Instance.out_system_lock = true;
			UIManagerControl.Instance.OpenUI("TalkUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
			TalkUI.Instance.AddCallBack(talkIds, isMask, CallBack, npcId);
			if (talkIds.get_Count() > 0)
			{
				UIManagerControl.Instance.HideUI("TownUI");
			}
			else
			{
				this.ShowTalkUINpc = 0;
				Debug.Log("对话列表为空!!!");
			}
		}
	}

	public bool IsFinishedTask(int taskId)
	{
		int num;
		if (taskId > 700000)
		{
			return this.LastTaskIdDict.TryGetValue(Package.Task.TaskType.GodWeaponTask, ref num) && num >= taskId;
		}
		if (taskId > 600000)
		{
			return this.LastTaskIdDict.TryGetValue(Package.Task.TaskType.ChangeCareer, ref num) && num >= taskId;
		}
		if (taskId > 200000)
		{
			RenWuPeiZhi data = DataReader<RenWuPeiZhi>.Get(taskId);
			if (data != null)
			{
				RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.DataList.Find((RenWuPeiZhi e) => e.taskBranch == data.taskBranch && e.frontTask < 200000);
				if (renWuPeiZhi.id > this.MainTaskId)
				{
					return false;
				}
				List<BaseTask> list = new List<BaseTask>(this.TaskMap.get_Values());
				for (int i = 0; i < list.get_Count(); i++)
				{
					if (list.get_Item(i).Task.taskType == Package.Task.TaskType.BranchTask)
					{
						RenWuPeiZhi renWuPeiZhi2 = DataReader<RenWuPeiZhi>.Get(list.get_Item(i).Task.taskId);
						if (renWuPeiZhi2 != null && renWuPeiZhi2.taskBranch == data.taskBranch)
						{
							return renWuPeiZhi2.id > data.id;
						}
					}
				}
			}
			return true;
		}
		return this.MainTaskId == 0 || this.MainTaskId > taskId;
	}

	public static int GetFrontTaskId()
	{
		if (MainTaskManager.instance != null && MainTaskManager.instance.CurTaskId > 0)
		{
			RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(MainTaskManager.instance.CurTaskId);
			if (renWuPeiZhi != null && renWuPeiZhi.frontTask > 0)
			{
				return renWuPeiZhi.frontTask;
			}
		}
		return 0;
	}

	public bool IsAutoTaskType(int type)
	{
		if (this.IsAllAuto)
		{
			return true;
		}
		if (this.AutoTaskType != null)
		{
			for (int i = 0; i < this.AutoTaskType.Length; i++)
			{
				if (this.AutoTaskType[i] == type)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void SaveTaskCounts()
	{
		this.mTaskCounts.Clear();
		List<BaseTask> list = new List<BaseTask>(this.mTaskMap.get_Values());
		for (int i = 0; i < list.get_Count(); i++)
		{
			BaseTask baseTask = list.get_Item(i);
			if (baseTask.Data.type == 5 || baseTask.Data.type == 4 || baseTask.Data.type == 2)
			{
				this.mTaskCounts.Add(baseTask.Task.taskId);
				this.mTaskCounts.Add(baseTask.Task.count);
			}
		}
		PlayerPrefsExt.SetStringPrefs(this.TaskCountsKey, MiniJSON.jsonEncode(this.mTaskCounts));
	}

	public bool HasUpgradeTips(Package.Task task)
	{
		if (task.taskType == Package.Task.TaskType.MainTask && task.status == Package.Task.TaskStatus.TaskNotOpen)
		{
			for (int i = 0; i < this.TipsSystems.Length; i++)
			{
				if (SystemOpenManager.IsSystemOn(this.TipsSystems[i]))
				{
					return true;
				}
			}
		}
		return false;
	}

	public void ShowUpgradeTips()
	{
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		for (int i = 0; i < this.TipsSystems.Length; i++)
		{
			if (SystemOpenManager.IsSystemOn(this.TipsSystems[i]))
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(this.TipsSystems[i]);
				if (systemOpen != null)
				{
					list.Add(this.GetButton2Promote(systemOpen.id, systemOpen.name));
				}
			}
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIView popButtonsAdjustUIView = UIManagerControl.Instance.OpenUI("PopButtonsAdjustUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PopButtonsAdjustUIView;
			popButtonsAdjustUIView.get_transform().set_localPosition(new Vector3(-370f, 100f, 0f));
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	public void GetTaskExtraReward(int taskType, ref XDict<int, long> rewards)
	{
		EWaiRenWuJiangLi eWaiRenWuJiangLi = this.ExtraRewardDatas.Find((EWaiRenWuJiangLi e) => e.taskType == taskType);
		if (eWaiRenWuJiangLi != null)
		{
			rewards.Clear();
			List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
			int lv = EntityWorld.Instance.EntSelf.Lv;
			for (int i = 0; i < eWaiRenWuJiangLi.rewardId.get_Count(); i++)
			{
				int num = eWaiRenWuJiangLi.rewardId.get_Item(i);
				for (int j = 0; j < dataList.get_Count(); j++)
				{
					DiaoLuo diaoLuo = dataList.get_Item(j);
					if (diaoLuo.ruleId == num && ((diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0) || (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv) || (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)))
					{
						if (rewards.Keys.Contains(diaoLuo.goodsId))
						{
							List<long> values;
							List<long> expr_100 = values = rewards.Values;
							int num2;
							int expr_115 = num2 = rewards.Keys.IndexOf(diaoLuo.goodsId);
							long num3 = values.get_Item(num2);
							expr_100.set_Item(expr_115, num3 + diaoLuo.minNum);
						}
						else
						{
							rewards.Add(diaoLuo.goodsId, diaoLuo.minNum);
						}
					}
				}
			}
		}
	}

	public bool GoToNPC(NPC npc, int taskId = 0, bool isFastNav = false, float offset = 1f)
	{
		EntitySelf entSelf = EntityWorld.Instance.EntSelf;
		if (npc == null || entSelf == null || !entSelf.Actor)
		{
			if (npc == null)
			{
				Debug.Log("<color=red>Error:</color>寻路失败，目标 Npc数据 为 null");
			}
			else
			{
				Debug.Log("<color=red>Error:</color>寻路失败，角色 EntSelf 为 null");
			}
			return false;
		}
		if (this.HasNpcId(npc.id))
		{
			return true;
		}
		Vector3 npcPosition = this.GetNpcPosition(npc, offset);
		if (npc.scene != MySceneManager.Instance.CurSceneID)
		{
			if (isFastNav)
			{
				this.DelaySendFlyShoe(true, npc.scene, npcPosition);
			}
			else
			{
				TownCountdownUI townCountdownUI = UIManagerControl.Instance.OpenUI("TownCountdownUI", null, false, UIType.NonPush) as TownCountdownUI;
				townCountdownUI.StartTransmit(taskId, npc.scene);
				EntityWorld.Instance.EntSelf.IsNavigating = true;
			}
			return false;
		}
		float distanceNoY = XUtility.GetDistanceNoY(entSelf.Actor.FixTransform.get_position(), npcPosition);
		if (isFastNav)
		{
			return this.FastGotoNpc(npc, distanceNoY, npcPosition);
		}
		return this.NormalGotoNpc(npc, distanceNoY, npcPosition);
	}

	public bool GoToNPC(int npcId, int taskId = 0, bool isFastNav = false, float offset = 1f)
	{
		NPC npc = DataReader<NPC>.Get(npcId);
		return this.GoToNPC(npc, taskId, isFastNav, offset);
	}

	protected bool FastGotoNpc(NPC npc, float dist, Vector3 npcPoint)
	{
		if (dist > this.UseFlyShoeMinDist)
		{
			this.DelaySendFlyShoe(false, npc.scene, npcPoint);
			Debug.Log(string.Concat(new object[]
			{
				"<快速导航[",
				npc.id,
				"]NPC>",
				npcPoint
			}));
			return false;
		}
		if (this.IsAutoFast)
		{
			return this.NormalGotoNpc(npc, dist, npcPoint);
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505300, false), 1f, 1f);
		return false;
	}

	protected bool NormalGotoNpc(NPC npc, float dist, Vector3 npcPoint)
	{
		if (dist > 1f)
		{
			EventDispatcher.Broadcast(EventNames.BeginNav);
			EntityWorld.Instance.EntSelf.NavToNPC(npc.scene, npcPoint, 0.5f, delegate
			{
				this.StopToNPC(false);
			});
			Debug.Log(string.Concat(new object[]
			{
				"<自动导航[",
				npc.id,
				"]NPC>",
				npcPoint
			}));
			return false;
		}
		if (GuideManager.Instance.IsNPCEnterLock())
		{
			Debug.Log("进入NPC触发被指引打断!!!");
			return false;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("进入NPC触发被神器打断!!!");
			return false;
		}
		return true;
	}

	public void DelaySendFlyShoe(bool needSwitchCity, int sceneId, Vector3 pos)
	{
		if (this.UseFlyShoeLock || CityManager.Instance.NeedDelayEnterNPC)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505109, false));
		}
		else
		{
			this.UseFlyShoeLock = true;
			CityManager.Instance.NeedDelayEnterNPC = true;
			CityManager.Instance.NeedSwitchCity = needSwitchCity;
			if (needSwitchCity)
			{
				this.StopToNPC(false);
				CityManager.Instance.SendFlyShoeTransport(sceneId, new Pos
				{
					x = pos.x * 100f,
					y = pos.z * 100f
				});
			}
			else
			{
				if (TownUI.Instance != null)
				{
					TownUI.Instance.PlayFastTransMask();
				}
				TimerHeap.AddTimer(200u, 0, delegate
				{
					if (!UIManagerControl.Instance.IsOpen("LoadingUI"))
					{
						CityManager.Instance.SendFlyShoeTransport(sceneId, new Pos
						{
							x = pos.x * 100f,
							y = pos.z * 100f
						});
					}
				});
			}
		}
	}

	public Vector3 GetNpcPosition(NPC npc, float offset)
	{
		Vector3 zero = Vector3.get_zero();
		if (npc.position.get_Count() == 3)
		{
			zero.Set((float)npc.position.get_Item(0) * 0.01f, (float)npc.position.get_Item(1) * 0.01f, (float)npc.position.get_Item(2) * 0.01f);
		}
		Vector3 zero2 = Vector3.get_zero();
		if (npc.face.get_Count() == 3)
		{
			zero2.Set((float)npc.face.get_Item(0) * 0.01f, (float)npc.face.get_Item(1) * 0.01f, (float)npc.face.get_Item(2) * 0.01f);
		}
		Vector3 vector = Quaternion.Euler(zero2) * Vector3.get_forward() * offset;
		return zero + vector;
	}

	public void StopToNPC(bool isStopAuto = false)
	{
		EntityWorld.Instance.EntSelf.StopNavToNPC();
		EventDispatcher.Broadcast(EventNames.EndNav);
		EventDispatcher.Broadcast("GuideManager.OnEndNav");
		if (this.mExecuteDelayId != 0u)
		{
			TimerHeap.DelTimer(this.mExecuteDelayId);
		}
		if (isStopAuto)
		{
			this.AutoTaskId = 0;
		}
	}

	protected void OnSeleteNPC(int npcId)
	{
		if (npcId > 0)
		{
			this.IsAutoGoingToNPC = true;
			if (this.GoToNPC(npcId, 0, false, 1f))
			{
				this.OnEnterNPC(npcId);
			}
		}
	}

	protected void OnEnterNPC(int npcId)
	{
		if (GuideManager.Instance.IsNPCEnterLock())
		{
			Debug.Log("进入NPC触发被指引打断!!!");
			return;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("进入NPC触发被神器打断!!!");
			return;
		}
		NPC nPC = DataReader<NPC>.Get(npcId);
		if (nPC == null)
		{
			return;
		}
		Debug.Log("====>>>进入[" + npcId + "]NPC范围");
		this.NpcId = npcId;
		if (!this.mNpcIdList.Contains(npcId))
		{
			this.mNpcIdList.Add(npcId);
		}
		TaskNPCManager.Instance.LookAtActorSelf(npcId);
		BaseTask task = null;
		if (this.IsTaskNpc(npcId) && this.GetTask(this.CurTaskId, out task, true))
		{
			if (!CityManager.Instance.NeedSwitchCity && CityManager.Instance.NeedDelayEnterNPC)
			{
				this.mExecuteDelayId = TimerHeap.AddTimer(600u, 0, delegate
				{
					task.OnEnterNPC(npcId);
				});
			}
			else if (UIManagerControl.Instance.IsOpen("TownUI"))
			{
				task.OnEnterNPC(npcId);
			}
			else
			{
				this.mEnterNpcAgainOnEnable = npcId;
				Debug.Log("进入NPC触发任务被未显示主城打断!!!");
			}
		}
		else if (this.IsAutoGoingToNPC)
		{
			this.IsAutoGoingToNPC = false;
			if (nPC.function != null && nPC.function.get_Count() > 0)
			{
				if (UIManagerControl.Instance.IsOpen("TownUI"))
				{
					EventDispatcher.Broadcast<NPC>(EventNames.OpenNPCSystem, nPC);
					CityManager.Instance.NeedDelayEnterNPC = false;
				}
				else
				{
					this.OpenNpcSystemAgainOnEnable = nPC;
					Debug.Log("进入NPC打开系统被未显示主城打断!!!");
				}
			}
			else
			{
				this.OpenTalkUI(nPC.word, true, null, nPC.id);
			}
		}
		if (nPC.function != null && nPC.function.get_Count() > 0)
		{
			if (UIManagerControl.Instance.IsOpen("TownUI"))
			{
				EventDispatcher.Broadcast<NPC>(EventNames.OpenNPCMenu, nPC);
			}
			else
			{
				this.OpenNpcMenuAgainOnEnable = nPC;
				Debug.Log("进入NPC打开菜单被未显示主城打断!!!");
			}
		}
		else if (nPC.word != null && nPC.word.get_Count() > 0)
		{
			EventDispatcher.Broadcast<bool, int>(EventNames.UpdateTalkBubble, true, npcId);
		}
	}

	protected void OnExitNPC(int npcId)
	{
		if (GuideManager.Instance.IsNPCEnterLock())
		{
			Debug.Log("离开NPC触发被指引打断!!!");
			return;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("离开NPC触发被神器打断!!!");
			return;
		}
		Debug.Log("<<<====离开[" + npcId + "]NPC范围");
		this.NpcId = 0;
		this.mNpcIdList.Remove(npcId);
		TaskNPCManager.Instance.ResetFaceDirection(npcId);
		EventDispatcher.Broadcast<bool, int>(EventNames.UpdateTalkBubble, false, npcId);
		EventDispatcher.Broadcast(EventNames.CloseNPCMenu);
		if (TaskDescUI.OpenByNpc == npcId)
		{
			UIManagerControl.Instance.HideUI("TaskDescUI");
		}
		BaseTask baseTask = null;
		if (this.GetTask(this.CurTaskId, out baseTask, true))
		{
			baseTask.OnExitNPC(npcId);
		}
	}
}
