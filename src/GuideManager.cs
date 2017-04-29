using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNetwork;

public class GuideManager : BaseSubSystemManager
{
	public class EventNames
	{
		public const string NextStep = "GuideManager.NextStep";

		public const string StepBegin = "GuideManager.StepBegin";

		public const string StepSuccess = "GuideManager.StepSuccess";

		public const string BroadOfEndGuide = "GuideManager.BroadOfEndGuide";

		public const string TriggerFocusWidget = "GuideManager.TriggerFocusWidget";

		public const string TalkUIClose = "GuideManager.TalkUIClose";

		public const string ResetInvalidClick = "GuideManager.ResetInvalidClick";

		public const string ResetGuideUI = "GuideManager.ResetGuideUI";

		public const string PauseGuideSystem = "GuideManager.PauseGuideSystem";

		public const string LockOffWaitNextGuide = "GuideManager.LockOffWaitNextGuide";

		public const string CheckGodWeaponGuide = "GuideManager.CheckGodWeaponGuide";

		public const string EnterInstance = "GuideManager.EnterInstance";

		public const string InstanceOfTime = "GuideManager.InstanceOfTime";

		public const string MonsterCastSkill = "GuideManager.MonsterCastSkill";

		public const string MateCastSkill = "GuideManager.MateCastSkill";

		public const string MonsterDie = "GuideManager.MonsterDie";

		public const string MonsterBorn = "GuideManager.MonsterBorn";

		public const string OnInstanceOfPetCDOK = "GuideManager.InstanceOfActPoint";

		public const string BossEnterTired = "GuideManager.BossEnterTired";

		public const string LevelUp = "GuideManager.LevelUp";

		public const string LevelNow = "GuideManager.LevelNow";

		public const string TaskFinish = "GuideManager.TaskFinish";

		public const string ReTriggerTaskOfLogin = "ReTriggerTaskOfLogin";

		public const string RuneInlayFull = "GuideManager.RuneInlayFull";

		public const string PetUpStarOn = "GuideManager.PetUpStarOn";

		public const string PetActivateOn = "GuideManager.PetActivateOn";

		public const string EquipCombineOn = "GuideManager.EquipCombineOn";

		public const string InstanceWin = "GuideManager.InstanceWin";

		public const string InstanceExit = "GuideManager.InstanceExit";

		public const string BountyExistProduction = "GuideManager.BountyExistProduction";

		public const string EndNav = "GuideManager.OnEndNav";
	}

	public class StepType
	{
		public const int None = 0;

		public const int Finger_None = 1;

		public const int Finger_InstructionRole = 2;

		public const int InstructionRole = 3;

		public const int SystemOpen = 4;

		public const int WidgetsOff = 5;

		public const int WidgetsOn = 6;

		public const int Finger_InstructionBubble = 7;

		public const int InstructionBubble = 8;

		public const int Spine = 9;

		public const int SkillOpenInBattle = 10;

		public const int ImageShow = 11;

		public static bool IsStepOfNoLimit(GuideStep dataStep)
		{
			return dataStep.type == 5;
		}
	}

	public class TriggerType
	{
		public const int Logic = 0;

		public const int Instance_Time = 3;

		public const int Instance_Begin = 4;

		public const int Instance_Skill = 5;

		public const int Instance_Die = 6;

		public const int Instance_Born = 7;

		public const int Instance_PetCDOK = 9;

		public const int Instance_BossTired = 13;

		public const int Instance_GuideFinished = 14;

		public const int Instance_Win = 16;

		public const int Instance_Exit = 18;

		public const int Level = 1;

		public const int Task = 2;

		public const int RuneInlayFull = 8;

		public const int PetUpgradeOn = 10;

		public const int PetActivateOn = 11;

		public const int EquipCombineOn = 12;

		public const int NoInstance_GuideFinished = 15;

		public const int BountyExistProduction = 17;

		public const int EndNav = 19;

		public static bool IsNeedInstanceLock(Guide dataguide, GuideStep dataStep)
		{
			return dataguide != null && dataStep != null && EntityWorld.Instance.EntSelf != null && (EntityWorld.Instance.EntSelf.IsInBattle && GuideManager.LockMode.IsLockScreen(dataStep));
		}

		public static bool IsNeedInstanceSlow(Guide dataguide, GuideStep dataStep)
		{
			return dataguide != null && dataStep != null && EntityWorld.Instance.EntSelf != null && (EntityWorld.Instance.EntSelf.IsInBattle && GuideManager.LockMode.IsSlow(dataStep));
		}

		public static bool IsInstanceTriggerType(Guide dataguide)
		{
			return dataguide == null || (dataguide.triggerType == 4 || dataguide.triggerType == 3 || dataguide.triggerType == 5 || dataguide.triggerType == 6 || dataguide.triggerType == 7 || dataguide.triggerType == 9 || dataguide.triggerType == 13 || dataguide.triggerType == 14);
		}

		public static bool IsInstanceTriggerType(int group_id)
		{
			Guide dataguide = DataReader<Guide>.Get(group_id);
			return GuideManager.TriggerType.IsInstanceTriggerType(dataguide);
		}
	}

	public class StepSuccessMode
	{
		public const int TriggerButton = 1;

		public const int ClickAll = 2;

		public const int TalkUIOpen = 3;

		public const int TaskFinish = 4;

		public const int AutomaticTime = 5;
	}

	public class GuideIdPriority
	{
		public int group;

		public int recordStep;

		public int priority;
	}

	public class LockMode
	{
		public const int InInstanceLockScreen = 1;

		public const int NoLockScreen = 2;

		public const int InInstanceSlow = 3;

		public static bool IsLockScreen(GuideStep dataStep)
		{
			return dataStep.lockMode == 1;
		}

		public static bool IsSlow(GuideStep dataStep)
		{
			return dataStep.lockMode == 3;
		}
	}

	public class DealStepIfCanStateTracker
	{
		public enum State
		{
			NONE,
			FindingNoWidget,
			LoadingUIIsOpen,
			StepWidgetsOff,
			StepWidgetsOn,
			WaitingInput
		}

		public GuideManager.DealStepIfCanStateTracker.State current_state;

		public int widgetId;

		public void Reset()
		{
			this.current_state = GuideManager.DealStepIfCanStateTracker.State.NONE;
			this.widgetId = 0;
		}

		public void DebugIfNeed()
		{
			if (this.current_state == GuideManager.DealStepIfCanStateTracker.State.NONE)
			{
				return;
			}
			switch (this.current_state)
			{
			case GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget:
				Debug.LogError("==>[指引错误状态]:找不到控件, widgetId = " + this.widgetId);
				break;
			case GuideManager.DealStepIfCanStateTracker.State.LoadingUIIsOpen:
				Debug.LogError("==>[指引错误状态]:LoadingUI打开中");
				break;
			case GuideManager.DealStepIfCanStateTracker.State.StepWidgetsOff:
				Debug.LogError("==>[指引错误状态]:StepWidgetsOff中");
				break;
			case GuideManager.DealStepIfCanStateTracker.State.StepWidgetsOn:
				Debug.LogError("==>[指引错误状态]:StepWidgetsOn中");
				break;
			}
		}
	}

	public const int FIND_WIDGET_MAX_SECOND = 15;

	private const int fault_tolerant_time = 60000;

	private GuideManager.DealStepIfCanStateTracker mDealStepIfCanStateTracker = new GuideManager.DealStepIfCanStateTracker();

	private int m_guide_group;

	private int m_guide_step;

	private bool m_guide_lock;

	private bool m_out_system_lock;

	private bool m_mintime_lock;

	private bool m_finger_move_lock;

	private bool m_step_attempt_lock;

	private bool m_wait_next_guide_lock;

	private bool m_uidrag_lock;

	private bool m_instance_time_lock;

	private bool m_instanceSlow;

	private bool m_talk_desc_ui_lock;

	public TimerHeapCustom m_TimerHeapCustom;

	private List<GuideManager.GuideIdPriority> m_listOfQueue = new List<GuideManager.GuideIdPriority>();

	private List<GuideInfo> m_listOfComplete = new List<GuideInfo>();

	private bool is_guide_finish_flag;

	private List<Guide> SortedGuideList = new List<Guide>();

	private static GuideManager instance;

	private int m_finish_task_id;

	private uint time_scale_to_1_timer_id;

	private uint dealStep_timer_id;

	private uint dealStepMax_timer_id;

	private uint automaticTime_timer_id;

	private uint fault_tolerant_timer_id;

	private uint mintime_lock_timer_id;

	public bool IsNeedChangeGuideLayer;

	private bool m_isgm;

	protected int guide_group
	{
		get
		{
			return this.m_guide_group;
		}
		set
		{
			this.m_guide_group = value;
		}
	}

	protected int guide_step
	{
		get
		{
			return this.m_guide_step;
		}
		set
		{
			this.m_guide_step = value;
			if (value == 0)
			{
				this.uidrag_lock = false;
			}
			else
			{
				GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step);
				if (guideStep == null)
				{
					return;
				}
				if (guideStep.listDragOn == 0)
				{
					this.uidrag_lock = true;
				}
				else
				{
					this.uidrag_lock = false;
				}
			}
		}
	}

	public bool guide_lock
	{
		get
		{
			return this.m_guide_lock;
		}
		set
		{
			this.m_guide_lock = value;
			if (!this.m_guide_lock)
			{
				this.CheckQueue(true, false);
			}
		}
	}

	public bool out_system_lock
	{
		get
		{
			return this.m_out_system_lock;
		}
		set
		{
			this.m_out_system_lock = value;
		}
	}

	public bool mintime_lock
	{
		get
		{
			return this.m_mintime_lock;
		}
		set
		{
			this.m_mintime_lock = value;
		}
	}

	public bool finger_move_lock
	{
		get
		{
			return this.m_finger_move_lock;
		}
		set
		{
			this.m_finger_move_lock = value;
			this.DelayDealInstanceSlowOfTimeScale();
		}
	}

	public bool step_attempt_lock
	{
		get
		{
			return this.m_step_attempt_lock;
		}
		set
		{
			this.m_step_attempt_lock = value;
		}
	}

	public bool wait_next_guide_lock
	{
		get
		{
			return this.m_wait_next_guide_lock;
		}
		set
		{
			this.m_wait_next_guide_lock = value;
			if (value)
			{
				UIStateSystem.LockOfWaitNextGuide();
			}
			else
			{
				EventDispatcher.Broadcast("GuideManager.LockOffWaitNextGuide");
			}
		}
	}

	public bool uidrag_lock
	{
		get
		{
			return this.m_uidrag_lock;
		}
		set
		{
			this.m_uidrag_lock = value;
		}
	}

	public bool instance_time_lock
	{
		get
		{
			return this.m_instance_time_lock;
		}
		set
		{
			this.m_instance_time_lock = value;
			InstanceManager.IsPauseTimeEscape = this.m_instance_time_lock;
		}
	}

	private bool instanceSlow
	{
		get
		{
			return this.m_instanceSlow;
		}
		set
		{
			this.m_instanceSlow = value;
		}
	}

	public bool talk_desc_ui_lock
	{
		get
		{
			return this.guide_lock && this.m_talk_desc_ui_lock;
		}
		set
		{
			this.m_talk_desc_ui_lock = value;
		}
	}

	public static GuideManager Instance
	{
		get
		{
			if (GuideManager.instance == null)
			{
				GuideManager.instance = new GuideManager();
				GuideManager.instance.m_TimerHeapCustom = new TimerHeapCustom();
			}
			return GuideManager.instance;
		}
	}

	private int finish_task_id
	{
		get
		{
			if (this.m_finish_task_id <= 0)
			{
				this.m_finish_task_id = MainTaskManager.GetFrontTaskId();
			}
			return this.m_finish_task_id;
		}
		set
		{
			this.m_finish_task_id = value;
		}
	}

	private GuideManager()
	{
	}

	public void DebugIfNeed()
	{
		if (this.mDealStepIfCanStateTracker != null)
		{
			this.mDealStepIfCanStateTracker.DebugIfNeed();
		}
	}

	public void ReleaseAllLocks()
	{
		this.m_guide_lock = false;
		this.m_out_system_lock = false;
		this.m_finger_move_lock = false;
		this.m_mintime_lock = false;
		this.m_step_attempt_lock = false;
		this.m_wait_next_guide_lock = false;
		this.m_uidrag_lock = false;
		this.m_instance_time_lock = false;
		this.m_instanceSlow = false;
		this.m_talk_desc_ui_lock = false;
	}

	public void CheckQueue(bool ui_opened, bool isFromGodWeapon = false)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (!isFromGodWeapon)
		{
			EventDispatcher.Broadcast("GuideManager.CheckGodWeaponGuide");
		}
		if (!ui_opened)
		{
			this.DealSpecialGuide();
		}
		if (!this.IsGuideSysLock())
		{
			this.guide_group = 0;
			this.guide_step = 0;
			for (int i = 0; i < this.m_listOfQueue.get_Count(); i++)
			{
				GuideManager.GuideIdPriority guideIdPriority = this.m_listOfQueue.get_Item(i);
				if (this.IsGuideValidToExecute(guideIdPriority.group) && ui_opened && this.IsAllowNow() && this.IsGuideCanBegin(guideIdPriority.group, guideIdPriority.recordStep + 1))
				{
					this.guide_group = guideIdPriority.group;
					this.guide_step = guideIdPriority.recordStep;
					this.m_listOfQueue.RemoveAt(i);
					break;
				}
			}
			if (this.guide_group > 0)
			{
				this.BeginGuide();
			}
			else
			{
				UIQueueManager.Instance.CheckQueue(PopCondition.NoGuide);
			}
		}
	}

	private void DealSpecialGuide()
	{
		for (int i = 0; i < this.m_listOfQueue.get_Count(); i++)
		{
			GuideManager.GuideIdPriority guideIdPriority = this.m_listOfQueue.get_Item(i);
			if (this.IsGuideValidToExecute(guideIdPriority.group))
			{
				this.DealSpecialGuide(guideIdPriority.group, guideIdPriority.recordStep + 1);
			}
		}
	}

	private void SetGuideStatOfStepSuccess(int step, bool JustFinish)
	{
		if (this.guide_group <= 0)
		{
			return;
		}
		Guide guide = DataReader<Guide>.Get(this.guide_group);
		if (guide == null)
		{
			return;
		}
		if (this.is_guide_finish_flag)
		{
			return;
		}
		if (JustFinish)
		{
			this.is_guide_finish_flag = true;
			this.StoreGuideComplete();
		}
		else
		{
			GuideStep guideStep = GuideManager.FindStep(this.guide_group, step);
			if (guideStep != null)
			{
				if (guideStep.step_finish == 1)
				{
					this.is_guide_finish_flag = true;
					this.StoreGuideComplete();
				}
				else if (guideStep.step_record == 1)
				{
					this.SendSaveNotComplete(guide, step);
				}
			}
		}
	}

	private void StoreGuideComplete()
	{
		this.SendSaveGuideInfo(this.guide_group);
		GuideInfo guideInfo = this.GetGuideStat(this.guide_group);
		if (guideInfo == null)
		{
			guideInfo = new GuideInfo
			{
				guideGroupId = this.guide_group,
				completeTimes = 0
			};
			this.m_listOfComplete.Add(guideInfo);
		}
		guideInfo.completeTimes++;
	}

	private GuideInfo GetGuideStat(int guideId)
	{
		for (int i = 0; i < this.m_listOfComplete.get_Count(); i++)
		{
			if (this.m_listOfComplete.get_Item(i).guideGroupId == guideId)
			{
				return this.m_listOfComplete.get_Item(i);
			}
		}
		return null;
	}

	private bool IsGuideComplete(int guideId)
	{
		GuideInfo guideStat = this.GetGuideStat(guideId);
		return guideStat != null;
	}

	private static int SortCompare(Guide guide1, Guide guide2)
	{
		int result = 0;
		if (guide1.priority > guide2.priority)
		{
			result = -1;
		}
		else if (guide1.priority < guide2.priority)
		{
			result = 1;
		}
		else if (guide1.id < guide2.id)
		{
			result = -1;
		}
		else if (guide1.id > guide2.id)
		{
			result = 1;
		}
		return result;
	}

	public override void Init()
	{
		base.Init();
		this.SortedGuideList = DataReader<Guide>.DataList;
		this.SortedGuideList.Sort(new Comparison<Guide>(GuideManager.SortCompare));
	}

	public override void Release()
	{
		this.m_listOfQueue.Clear();
		this.m_listOfComplete.Clear();
		this.StopGuide();
		this.ReleaseAllLocks();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuideLoginPush>(new NetCallBackMethod<GuideLoginPush>(this.OnGuideLoginPush));
		NetworkManager.AddListenEvent<SaveGuideInfoRes>(new NetCallBackMethod<SaveGuideInfoRes>(this.OnSaveGuideInfoRes));
		NetworkManager.AddListenEvent<SaveNotCompleteRes>(new NetCallBackMethod<SaveNotCompleteRes>(this.OnSaveNotCompleteRes));
		EventDispatcher.AddListener("GuideManager.NextStep", new Callback(this.OnNextStep));
		EventDispatcher.AddListener("GuideManager.TriggerFocusWidget", new Callback(this.OnTriggerFocusWidget));
		EventDispatcher.AddListener("GuideManager.TalkUIClose", new Callback(this.OnTalkUIClose));
		EventDispatcher.AddListener<int>("GuideManager.LevelUp", new Callback<int>(this.OnLevelUp));
		EventDispatcher.AddListener("GuideManager.LevelNow", new Callback(this.OnLevelNow));
		EventDispatcher.AddListener<int>("GuideManager.TaskFinish", new Callback<int>(this.OnTaskFinish));
		EventDispatcher.AddListener("ReTriggerTaskOfLogin", new Callback(this.OnReTriggerTaskOfLogin));
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
		EventDispatcher.AddListener<int>("GuideManager.InstanceOfTime", new Callback<int>(this.OnInstanceOfTime));
		EventDispatcher.AddListener<int, int>("GuideManager.MonsterCastSkill", new Callback<int, int>(this.OnInstanceOfSkill));
		EventDispatcher.AddListener<int>("GuideManager.MonsterDie", new Callback<int>(this.OnInstanceOfDie));
		EventDispatcher.AddListener<int>("GuideManager.MonsterBorn", new Callback<int>(this.OnInstanceOfBorn));
		EventDispatcher.AddListener<int>("GuideManager.RuneInlayFull", new Callback<int>(this.OnRuneInlayFull));
		EventDispatcher.AddListener("GuideManager.InstanceOfActPoint", new Callback(this.OnInstanceOfPetCDOK));
		EventDispatcher.AddListener<int>("GuideManager.PetUpStarOn", new Callback<int>(this.OnPetUpStarOn));
		EventDispatcher.AddListener<int>("GuideManager.PetActivateOn", new Callback<int>(this.OnPetActivateOn));
		EventDispatcher.AddListener<int>("GuideManager.EquipCombineOn", new Callback<int>(this.OnEquipCombineOn));
		EventDispatcher.AddListener("GuideManager.BossEnterTired", new Callback(this.OnInstanceOfBossTired));
		EventDispatcher.AddListener("GuideManager.InstanceWin", new Callback(this.OnInstanceWin));
		EventDispatcher.AddListener("GuideManager.InstanceExit", new Callback(this.OnInstanceExit));
		EventDispatcher.AddListener("GuideManager.BountyExistProduction", new Callback(this.OnBountyExistProduction));
		EventDispatcher.AddListener("GuideManager.OnEndNav", new Callback(this.OnEndNav));
	}

	public void SendSaveGuideInfo(int guideId)
	{
		NetworkManager.Send(new SaveGuideInfoReq
		{
			guideGroupId = guideId
		}, ServerType.Data);
	}

	public void SendSaveNotComplete(Guide dataguide, int step)
	{
		if (dataguide.triggerType == 1 || dataguide.triggerType == 2 || dataguide.triggerType == 15)
		{
			NetworkManager.Send(new SaveNotCompleteReq
			{
				guideGroupId = dataguide.id,
				guideStep = step
			}, ServerType.Data);
		}
	}

	private void OnGuideLoginPush(short state, GuideLoginPush down = null)
	{
		if (state != 0 || down == null)
		{
			return;
		}
		this.m_listOfComplete = down.guideInfos;
		for (int i = 0; i < down.notCompleteGuideGroupInfos.get_Count(); i++)
		{
			this.AddToQueue(down.notCompleteGuideGroupInfos.get_Item(i).guideGroupId, down.notCompleteGuideGroupInfos.get_Item(i).completeTimes);
		}
	}

	private void OnSaveGuideInfoRes(short state, SaveGuideInfoRes down = null)
	{
		if (state != 0 || down == null)
		{
			return;
		}
	}

	private void OnSaveNotCompleteRes(short state, SaveNotCompleteRes down = null)
	{
		if (state != 0 || down == null)
		{
			return;
		}
	}

	private void OnTalkUIClose()
	{
		this.OnNextStepIfSuccess(3, 0);
	}

	private void LoadSceneEnd(int sceneId)
	{
		this.ClearInstanceTriggerType(sceneId);
		this.OnEnterInstance(sceneId);
	}

	private void OnInstanceExit()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		this.OnReTriggerTaskByInstanceExit();
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData != null)
		{
			GuideManager.Instance.OnInstanceExit(currentInstanceData.id);
		}
	}

	private void OnNextStep()
	{
		this.SetGuideStatOfStepSuccess(this.guide_step, false);
		GuideUIView.IsTriggerNextStep = false;
		if (this.guide_group != 0)
		{
			EventDispatcher.Broadcast("GuideManager.StepBegin");
			this.talk_desc_ui_lock = false;
			this.JustDoDealStep();
		}
	}

	private void OnNextStepIfSuccess(int mode, int arg = 0)
	{
		bool flag = false;
		GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step);
		if (guideStep != null)
		{
			if (guideStep.successMode.get_Count() == 0)
			{
				return;
			}
			if (guideStep.successMode.get_Item(0) == 4)
			{
				if (guideStep.successMode.get_Count() <= 1)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"指引错误[StepSuccessMode.TaskFinish], successMode size = 1, group = ",
						this.guide_group,
						", step = ",
						this.guide_step
					}));
					return;
				}
				if (guideStep.successMode.get_Item(1) == arg)
				{
					flag = true;
				}
			}
			else if (guideStep.successMode.get_Item(0) == mode)
			{
				flag = true;
			}
			if (flag)
			{
				if (GuideUIView.Instance != null)
				{
					GuideUIView.Instance.Show(false);
				}
				this.OnNextStep();
			}
		}
	}

	private void OnTriggerFocusWidget()
	{
		GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step);
		if (guideStep != null)
		{
			if (guideStep.successMode.get_Item(0) == 1 || guideStep.successMode.get_Item(0) == 2)
			{
				EventDispatcher.Broadcast("GuideManager.NextStep");
			}
			else if (guideStep.successMode.get_Item(0) == 3 && GuideUIView.Instance != null && UIManagerControl.Instance.IsOpen("GuideUI"))
			{
				GuideUIView.Instance.JustLockScreen();
			}
		}
	}

	private void OnLevelUp(int level)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 1 && guide.args.get_Count() > 0 && this.IsOperatorOfArgsPass(guide, guide.args.get_Item(0), level))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnLevelNow()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
		{
			this.OnLevelUp(EntityWorld.Instance.EntSelf.Lv);
		}
	}

	private void OnTaskFinish(int taskId)
	{
		if (taskId <= 0)
		{
			return;
		}
		this.finish_task_id = taskId;
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		this.OnNextStepIfSuccess(4, taskId);
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 2 && guide.args.Contains(taskId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnReTriggerTaskOfLogin()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		this.ReTriggerGuideOfTask(this.finish_task_id);
	}

	private void OnReTriggerTaskByInstanceExit()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		this.ReTriggerGuideOfTask(this.finish_task_id);
	}

	private void ReTriggerGuideOfTask(int taskId = 0)
	{
		for (int i = 0; i < this.m_listOfComplete.get_Count(); i++)
		{
			GuideInfo guideInfo = this.m_listOfComplete.get_Item(i);
			Guide guide = DataReader<Guide>.Get(guideInfo.guideGroupId);
			if (guide == null)
			{
				Debug.LogError("指引: m_listOfComplete指引找不到, groupId = " + guideInfo.guideGroupId);
			}
			else if (this.IsGuideOn(guide) && guide.triggerType == 2)
			{
				if (taskId > 0)
				{
					if (guide.args.get_Count() > 0 && guide.args.get_Item(0) == taskId)
					{
						this.TriggerPass(guide);
					}
				}
				else
				{
					this.TriggerPass(guide);
				}
			}
		}
	}

	private void OnEnterInstance(int sceneId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 4 && guide.args.Contains(currentInstanceData.id))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceOfTime(int time)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 3 && guide.args.get_Count() >= 2 && guide.args.get_Item(0) == currentInstanceData.id && time >= guide.args.get_Item(1))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceOfSkill(int actorId, int skillId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide dataguide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(dataguide) && dataguide.triggerType == 5 && dataguide.args.get_Count() >= 4 && dataguide.args.get_Item(0) == currentInstanceData.id && dataguide.args.get_Item(1) == actorId && dataguide.args.get_Item(2) == skillId)
			{
				if (dataguide.args.get_Item(3) > 0)
				{
					if (this.IsGuideValid123456(dataguide))
					{
						this.m_TimerHeapCustom.AddTimer(dataguide.args.get_Item(3) * 1000, delegate
						{
							this.TriggerPass(dataguide);
						});
					}
				}
				else
				{
					this.TriggerPass(dataguide);
				}
			}
		}
	}

	private void OnInstanceOfDie(int actorId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide dataguide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(dataguide) && dataguide.triggerType == 6 && dataguide.args.get_Count() >= 3 && dataguide.args.get_Item(0) == currentInstanceData.id && dataguide.args.get_Item(1) == actorId)
			{
				if (dataguide.args.get_Item(2) > 0)
				{
					if (this.IsGuideValid123456(dataguide))
					{
						this.m_TimerHeapCustom.AddTimer(dataguide.args.get_Item(2) * 1000, delegate
						{
							this.TriggerPass(dataguide);
						});
					}
				}
				else
				{
					this.TriggerPass(dataguide);
				}
			}
		}
	}

	private void OnInstanceOfBorn(int actorId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide dataguide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(dataguide) && dataguide.triggerType == 7 && dataguide.args.get_Count() >= 3 && dataguide.args.get_Item(0) == currentInstanceData.id && dataguide.args.get_Item(1) == actorId)
			{
				if (dataguide.args.get_Item(2) > 0)
				{
					if (this.IsGuideValid123456(dataguide))
					{
						this.m_TimerHeapCustom.AddTimer(dataguide.args.get_Item(2) * 1000, delegate
						{
							this.TriggerPass(dataguide);
						});
					}
				}
				else
				{
					this.TriggerPass(dataguide);
				}
			}
		}
	}

	private void OnRuneInlayFull(int petId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 8 && guide.args.Contains(petId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceOfPetCDOK()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 9 && guide.args.get_Count() >= 2 && guide.args.get_Item(0) == currentInstanceData.id && PetManager.Instance.InBattleContainsPet(guide.args.get_Item(1)))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnPetUpStarOn(int petId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 10 && guide.args.Contains(petId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnPetActivateOn(int petId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 11 && guide.args.Contains(petId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnEquipCombineOn(int equipId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 12 && guide.args.Contains(equipId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceOfBossTired()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide dataguide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(dataguide) && dataguide.triggerType == 13 && dataguide.args.get_Count() >= 2 && dataguide.args.get_Item(0) == currentInstanceData.id)
			{
				if (dataguide.args.get_Item(1) > 0)
				{
					if (this.IsGuideValid123456(dataguide))
					{
						this.m_TimerHeapCustom.AddTimer(dataguide.args.get_Item(1) * 1000, delegate
						{
							this.TriggerPass(dataguide);
						});
					}
				}
				else
				{
					this.TriggerPass(dataguide);
				}
			}
		}
	}

	private void OnInstanceOfGuideFinished(int guideId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (InstanceManager.CurrentInstanceData == null)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 14 && guide.args != null && guide.args.Contains(guideId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnNoInstanceOfGuideFinished(int guideId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 15 && guide.args != null && guide.args.Contains(guideId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceWin()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		FuBenJiChuPeiZhi currentInstanceData = InstanceManager.CurrentInstanceData;
		if (currentInstanceData != null)
		{
			GuideManager.Instance.OnInstanceWin(currentInstanceData.id);
		}
	}

	private void OnInstanceWin(int finishedId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 16 && guide.args != null && guide.args.Contains(finishedId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnBountyExistProduction()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 17)
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnInstanceExit(int instanceId)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 18 && guide.args != null && guide.args.Contains(instanceId))
			{
				this.TriggerPass(guide);
			}
		}
	}

	private void OnEndNav()
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf == null || EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		List<Guide> sortedGuideList = this.SortedGuideList;
		for (int i = 0; i < sortedGuideList.get_Count(); i++)
		{
			Guide guide = sortedGuideList.get_Item(i);
			if (this.IsGuideOn(guide) && guide.triggerType == 19)
			{
				this.TriggerPass(guide);
			}
		}
	}

	private bool IsOperatorOfArgsPass(Guide dataguide, int dataarg, int myarg)
	{
		switch (dataguide.Operator)
		{
		case 0:
			if (myarg == dataarg)
			{
				return true;
			}
			break;
		case 1:
			if (myarg > dataarg)
			{
				return true;
			}
			break;
		case 2:
			if (myarg < dataarg)
			{
				return true;
			}
			break;
		case 3:
			if (myarg >= dataarg)
			{
				return true;
			}
			break;
		case 4:
			if (myarg <= dataarg)
			{
				return true;
			}
			break;
		}
		return false;
	}

	private bool IsGuideOn(Guide dataguide)
	{
		return dataguide.off == 0;
	}

	private void ClearInstanceTriggerType(int sceneId)
	{
		if (EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
		{
			this.m_listOfQueue.RemoveAll((GuideManager.GuideIdPriority e) => GuideManager.TriggerType.IsInstanceTriggerType(e.group));
		}
	}

	private void TriggerPass(Guide dataguide)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			return;
		}
		if (dataguide.id == this.guide_group)
		{
			return;
		}
		if (this.m_isgm || this.IsGuideValidToQueue(dataguide))
		{
			if (!this.IsInQueue(dataguide))
			{
				this.DealSpecialGuide(dataguide.id, 1);
				this.SendSaveNotComplete(dataguide, 0);
				this.AddToQueue(dataguide, 0);
			}
			this.CheckQueue(true, false);
			return;
		}
		if (this.IsGuideValidOfSpecial12356(dataguide))
		{
			this.DealSpecialGuide(dataguide.id, 1);
			this.SendSaveNotComplete(dataguide, 0);
			this.AddToQueue(dataguide, 0);
			return;
		}
	}

	private bool IsAllowNow()
	{
		return !this.IsGuideSysLock() && !this.IsUIPop();
	}

	private bool IsUIPop()
	{
		return UIManagerControl.Instance.IsOpen("UpgradeUI");
	}

	private void AddToQueue(int guideId, int step = 0)
	{
		Guide guide = DataReader<Guide>.Get(guideId);
		if (guide != null)
		{
			this.AddToQueue(guide, step);
		}
	}

	private void AddToQueue(Guide dataguide, int step = 0)
	{
		if (!this.IsInQueue(dataguide))
		{
			GuideManager.GuideIdPriority guideIdPriority = new GuideManager.GuideIdPriority
			{
				group = dataguide.id,
				recordStep = step,
				priority = dataguide.priority
			};
			this.m_listOfQueue.Add(guideIdPriority);
			this.m_listOfQueue = Enumerable.ToList<GuideManager.GuideIdPriority>(Enumerable.OrderByDescending<GuideManager.GuideIdPriority, int>(this.m_listOfQueue, (GuideManager.GuideIdPriority t) => t.priority));
		}
	}

	private bool IsInQueue(Guide dataguide)
	{
		for (int i = 0; i < this.m_listOfQueue.get_Count(); i++)
		{
			if (this.m_listOfQueue.get_Item(i).group == dataguide.id)
			{
				return true;
			}
		}
		return false;
	}

	private void DealSpecialGuide(int guideId, int step)
	{
		GuideStep guideStep = GuideManager.FindStep(guideId, step);
		if (guideStep == null)
		{
			return;
		}
		if (GuideManager.StepType.IsStepOfNoLimit(guideStep) && guideStep.type == 5)
		{
			this.JustStepWidgetsOff(guideStep);
		}
	}

	private bool IsGuideValidToQueue(int guideId)
	{
		Guide dataguide = DataReader<Guide>.Get(guideId);
		return this.IsGuideValidToQueue(dataguide);
	}

	private bool IsGuideValidToQueue(Guide dataguide)
	{
		if (dataguide == null)
		{
			return false;
		}
		if (this.m_isgm)
		{
			return true;
		}
		if (GuideManager.TriggerType.IsInstanceTriggerType(dataguide))
		{
			return this.IsGuideValid12356(dataguide);
		}
		return this.IsGuideValid123456(dataguide);
	}

	private bool IsGuideValidToExecute(int guideId)
	{
		Guide dataguide = DataReader<Guide>.Get(guideId);
		return this.IsGuideValidToExecute(dataguide);
	}

	private bool IsGuideValidToExecute(Guide dataguide)
	{
		if (dataguide == null)
		{
			return false;
		}
		if (this.m_isgm)
		{
			return true;
		}
		if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return this.IsGuideValid134(dataguide) && this.LimitPassSpecial01(dataguide);
		}
		return this.IsGuideValid123456(dataguide) && this.LimitPassSpecial01(dataguide);
	}

	private bool IsGuideValid123456(Guide dataguide)
	{
		return dataguide != null && (this.LimitPassOfLevel(dataguide) && this.LimitPassOfTask(dataguide) && this.LimitPassOfGuideTimes(dataguide) && this.LimitPassOfPreposeGuide(dataguide) && this.LimitPassOfInstance(dataguide) && this.LimitPassOfGuideFinished(dataguide));
	}

	private bool IsGuideValid12356(Guide dataguide)
	{
		return dataguide != null && (this.LimitPassOfLevel(dataguide) && this.LimitPassOfTask(dataguide) && this.LimitPassOfGuideTimes(dataguide) && this.LimitPassOfInstance(dataguide) && this.LimitPassOfGuideFinished(dataguide));
	}

	private bool IsGuideValid134(Guide dataguide)
	{
		return dataguide != null && (this.LimitPassOfLevel(dataguide) && this.LimitPassOfGuideTimes(dataguide) && this.LimitPassOfPreposeGuide(dataguide));
	}

	private bool IsGuideValidOfSpecial12356(Guide dataguide)
	{
		return dataguide != null && (dataguide.triggerType == 2 || dataguide.triggerType == 15) && (this.LimitPassOfLevel(dataguide) && this.LimitPassOfTask(dataguide) && this.LimitPassOfGuideTimes(dataguide) && this.LimitPassOfInstance(dataguide) && this.LimitPassOfGuideFinished(dataguide));
	}

	private bool LimitPassOfLevel(Guide dataguide)
	{
		return this.m_isgm || dataguide.levelRange.get_Count() < 2 || (EntityWorld.Instance.EntSelf.Lv >= dataguide.levelRange.get_Item(0) && EntityWorld.Instance.EntSelf.Lv <= dataguide.levelRange.get_Item(1));
	}

	private bool LimitPassOfTask(Guide dataguide)
	{
		return this.m_isgm || dataguide.finishTaskId <= 0 || !MainTaskManager.Instance.IsFinishedTask(dataguide.finishTaskId);
	}

	private bool LimitPassOfGuideTimes(Guide dataguide)
	{
		if (this.m_isgm)
		{
			return true;
		}
		int num = 0;
		GuideInfo guideStat = this.GetGuideStat(dataguide.id);
		if (guideStat != null)
		{
			num = guideStat.completeTimes;
		}
		return dataguide.guideTimes == 0 || num < Mathf.Max(dataguide.guideTimes, 1);
	}

	private bool LimitPassOfPreposeGuide(Guide dataguide)
	{
		if (this.m_isgm)
		{
			return true;
		}
		if (dataguide.preguide.get_Count() <= 0)
		{
			return true;
		}
		for (int i = 0; i < this.m_listOfComplete.get_Count(); i++)
		{
			if (dataguide.preguide.Contains(this.m_listOfComplete.get_Item(i).guideGroupId))
			{
				return true;
			}
		}
		return false;
	}

	private bool LimitPassOfInstance(Guide dataguide)
	{
		return this.m_isgm || DungeonManager.Instance == null || !DungeonManager.Instance.IsFinish(dataguide.finishInstanceId);
	}

	private bool LimitPassOfGuideFinished(Guide dataguide)
	{
		return this.m_isgm || dataguide.finishGuideId <= 0 || !this.IsGuideComplete(dataguide.finishGuideId);
	}

	private bool LimitPassSpecial01(Guide dataguide)
	{
		return dataguide.triggerType != 17 || BountyManager.Instance.HasProductionPos();
	}

	private bool IsGuideCanBegin(int ggroup, int beginStep)
	{
		Guide dataguide = DataReader<Guide>.Get(ggroup);
		return this.IsGuideCanBegin(dataguide, beginStep);
	}

	private bool IsGuideCanBegin(Guide dataguide, int beginStep)
	{
		if (dataguide == null)
		{
			return false;
		}
		GuideStep guideStep = GuideManager.FindStep(dataguide.id, beginStep);
		return guideStep != null && (guideStep.triggerUI <= 0 || this.IsTriggerUIPass(guideStep));
	}

	private bool IsTriggerUIPass(GuideStep dataStep)
	{
		if (dataStep.triggerUI <= 0)
		{
		}
		string text = WidgetSystem.FindNameOfUIById(dataStep.triggerUI);
		return WidgetSystem.IsUIOpen(dataStep.triggerUI) && (!(text == "TownUI") || (!this.IsNormalRootOpenForTownUI() && !this.IsMiddelRootOpen() && this.IsWidgetActiveInTownUIPass(dataStep)));
	}

	private bool IsNormalRootOpenForTownUI()
	{
		for (int i = 0; i < UINodesManager.NormalUIRoot.get_childCount(); i++)
		{
			Transform child = UINodesManager.NormalUIRoot.GetChild(i);
			if (child.get_gameObject().get_activeSelf())
			{
				UIBase component = child.GetComponent<UIBase>();
				if (component != null)
				{
					if (!(component.prefabName == "TownUI") && !(component.prefabName == "CurrenciesUI") && !(component.prefabName == "ChatTipUI"))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool IsMiddelRootOpen()
	{
		for (int i = 0; i < UINodesManager.MiddleUIRoot.get_childCount(); i++)
		{
			Transform child = UINodesManager.MiddleUIRoot.GetChild(i);
			if (child.get_gameObject().get_activeSelf())
			{
				UIBase component = child.GetComponent<UIBase>();
				if (component != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool IsWidgetActiveInTownUIPass(GuideStep dataStep)
	{
		if (dataStep.widgetId <= 0)
		{
			return true;
		}
		if (TownUI.Instance == null)
		{
			return false;
		}
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, false);
		return transform == null || !WidgetSystem.IsStaticWidget(dataStep.widgetId) || transform.get_gameObject().get_activeSelf();
	}

	private void BeginGuide()
	{
		MainTaskManager.Instance.AutoTaskId = 0;
		this.guide_lock = true;
		this.is_guide_finish_flag = false;
		this.DealOfBeginGuide(this.guide_group);
		this.FaultTolerant();
		this.JustDoDealStep();
	}

	private void DealOfBeginGuide(int guideId)
	{
		if (guideId <= 0)
		{
			return;
		}
		Guide guide = DataReader<Guide>.Get(guideId);
		if (guide == null)
		{
			return;
		}
		if (guide.stopAutoRun == 1 && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.CheckCancelNavToNPC())
		{
			MainTaskManager.Instance.StopToNPC(true);
		}
		this.m_wait_next_guide_lock = false;
	}

	public void StopGuide()
	{
		this.OnInstanceOfGuideFinished(this.guide_group);
		this.OnNoInstanceOfGuideFinished(this.guide_group);
		if (GuideUIView.Instance != null && GuideUIView.Instance.get_gameObject().get_activeSelf())
		{
			GuideUIView.Instance.Show(false);
		}
		TimerHeap.DelTimer(this.dealStep_timer_id);
		TimerHeap.DelTimer(this.dealStepMax_timer_id);
		TimerHeap.DelTimer(this.fault_tolerant_timer_id);
		TimerHeap.DelTimer(this.mintime_lock_timer_id);
		TimerHeap.DelTimer(this.automaticTime_timer_id);
		this.DealOfStopGuide(this.guide_group);
		this.ResetAll();
		EventDispatcher.Broadcast("GuideManager.BroadOfEndGuide");
		this.guide_lock = false;
		this.mintime_lock = false;
		this.finger_move_lock = false;
		this.instance_time_lock = false;
		this.instanceSlow = false;
		this.talk_desc_ui_lock = false;
		EventDispatcher.Broadcast("GuideManager.CheckGodWeaponGuide");
	}

	private void DealOfStopGuide(int guideId)
	{
		if (guideId <= 0)
		{
			return;
		}
		Guide guide = DataReader<Guide>.Get(guideId);
		if (guide == null)
		{
			return;
		}
		this.DealInstanceLock(false);
		this.DealInstanceSlow(false);
		if (guide.lockWaitNext == 1)
		{
			this.m_wait_next_guide_lock = true;
		}
	}

	public void StopGuideOfFinish()
	{
		this.SetGuideStatOfStepSuccess(0, true);
		this.StopGuide();
	}

	public void StopGuideIfInstance()
	{
		if (this.guide_group > 0 && GuideManager.TriggerType.IsInstanceTriggerType(this.guide_group))
		{
			this.StopGuide();
		}
	}

	private void DealInstanceLock(bool instanceLock)
	{
		if (instanceLock)
		{
			if (!this.instance_time_lock)
			{
				Utils.SetInstanceLock(true);
				this.instance_time_lock = true;
			}
		}
		else if (this.instance_time_lock)
		{
			Utils.SetInstanceLock(false);
			this.instance_time_lock = false;
		}
	}

	private void DealInstanceSlow(bool isSlow)
	{
		if (isSlow)
		{
			if (!this.instanceSlow)
			{
				this.instanceSlow = true;
			}
		}
		else if (this.instanceSlow)
		{
			this.instanceSlow = false;
			this.SetTimeScaleTo1Linear();
		}
	}

	private void DelayDealInstanceSlowOfTimeScale()
	{
		if (this.m_finger_move_lock)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle && this.instanceSlow)
		{
			Time.set_timeScale(0.1f);
		}
	}

	private void SetTimeScaleTo1Linear()
	{
		this.time_scale_to_1_timer_id = TimerHeap.AddTimer(0u, 100, delegate
		{
			if (Time.get_timeScale() + 0.1f > 1f)
			{
				Time.set_timeScale(1f);
				TimerHeap.DelTimer(this.time_scale_to_1_timer_id);
			}
			else
			{
				Time.set_timeScale(Time.get_timeScale() + 0.1f);
			}
		});
	}

	private void DealAudio(int audioId)
	{
		if (audioId == -1)
		{
			if (SoundManager.Instance != null && SoundManager.Instance.NPCIsGuideAudio)
			{
				SoundManager.SetBGMFade(true);
				SoundManager.Instance.NPCIsGuideAudio = false;
				SoundManager.Instance.StopNPCAudio();
			}
		}
		else
		{
			if (audioId == 0)
			{
				return;
			}
			if (SoundManager.Instance != null)
			{
				SoundManager.SetBGMVolume(0.2f);
				SoundManager.Instance.NPCIsGuideAudio = true;
				SoundManager.Instance.PlayNpcAudio(audioId);
			}
		}
	}

	private void JustDoDealStep()
	{
		if (!this.DealStep() && this.guide_lock)
		{
			TimerHeap.DelTimer(this.dealStep_timer_id);
			TimerHeap.DelTimer(this.dealStepMax_timer_id);
			this.dealStep_timer_id = TimerHeap.AddTimer(0u, 500, delegate
			{
				if (this.guide_group == 0)
				{
					TimerHeap.DelTimer(this.dealStep_timer_id);
					TimerHeap.DelTimer(this.dealStepMax_timer_id);
					this.StopGuide();
					return;
				}
				if (this.DealStep())
				{
					TimerHeap.DelTimer(this.dealStep_timer_id);
					TimerHeap.DelTimer(this.dealStepMax_timer_id);
				}
			});
			TimerHeap.DelTimer(this.dealStepMax_timer_id);
			this.dealStepMax_timer_id = TimerHeap.AddTimer(15000u, 0, delegate
			{
				Debug.LogError(string.Concat(new object[]
				{
					"步骤指引超时, 判定为无法正确执行该步骤, guide = ",
					this.guide_group,
					", step = ",
					this.guide_step
				}));
				this.mDealStepIfCanStateTracker.DebugIfNeed();
				TimerHeap.DelTimer(this.dealStep_timer_id);
				TimerHeap.DelTimer(this.dealStepMax_timer_id);
				this.StopGuide();
			});
		}
	}

	private bool DealStep()
	{
		this.step_attempt_lock = true;
		if (this.CheckGuideIsFinish())
		{
			this.step_attempt_lock = false;
			return true;
		}
		bool flag = this.DealStepIfCan();
		if (flag)
		{
			this.step_attempt_lock = false;
			if (this.guide_lock)
			{
				EventDispatcher.Broadcast("GuideManager.StepSuccess");
			}
			EventDispatcher.Broadcast("GuideManager.ResetInvalidClick");
			if (this.guide_group > 0)
			{
				Guide dataguide = DataReader<Guide>.Get(this.guide_group);
				GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step);
				this.DealInstanceLock(GuideManager.TriggerType.IsNeedInstanceLock(dataguide, guideStep));
				this.DealInstanceSlow(GuideManager.TriggerType.IsNeedInstanceSlow(dataguide, guideStep));
				if (guideStep != null)
				{
					this.DealAudio(guideStep.audioId);
				}
			}
			else
			{
				this.DealInstanceLock(false);
				this.DealInstanceSlow(false);
			}
		}
		return flag;
	}

	private bool CheckGuideIsFinish()
	{
		GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step + 1);
		if (guideStep != null)
		{
			return false;
		}
		this.SetGuideStatOfStepSuccess(0, true);
		this.StopGuide();
		return true;
	}

	private bool DealStepIfCan()
	{
		GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step + 1);
		if (guideStep == null)
		{
			return true;
		}
		if (!GuideManager.StepType.IsStepOfNoLimit(guideStep) && UIManagerControl.Instance.IsOpen("LoadingUI"))
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.LoadingUIIsOpen;
			return false;
		}
		switch (guideStep.type)
		{
		case 1:
			if (this.StepFinger(guideStep))
			{
				this.SetLockOfMintime(guideStep);
				return true;
			}
			return false;
		case 2:
			if (this.StepFinger_InstructionRole(guideStep))
			{
				this.SetLockOfMintime(guideStep);
				return true;
			}
			return false;
		case 3:
			if (this.StepInstructionRole(guideStep))
			{
				this.SetLockOfMintime(guideStep);
				return true;
			}
			return false;
		case 4:
			return this.StepSystemOpen(guideStep);
		case 5:
			return this.StepWidgetsOff(guideStep);
		case 6:
			return this.StepWidgetsOn(guideStep);
		case 7:
			if (this.StepFinger_InstructionBubble(guideStep))
			{
				this.SetLockOfMintime(guideStep);
				return true;
			}
			return false;
		case 8:
			if (this.StepInstructionBubble(guideStep))
			{
				this.SetLockOfMintime(guideStep);
				return true;
			}
			return false;
		case 9:
			return this.StepPlaySpine(guideStep);
		case 10:
			return this.StepSkillOpenInBattle(guideStep);
		case 11:
			return this.StepImageShow(guideStep);
		default:
			return true;
		}
	}

	private void BeginStep(GuideStep dataStep, bool isNeedOpenGuideUI, UIType type = UIType.NonPush)
	{
		this.guide_step++;
		if (isNeedOpenGuideUI)
		{
			UIManagerControl.Instance.OpenUI("GuideUI", UINodesManager.MiddleUIRoot, false, type);
			if (dataStep.successMode.get_Count() == 0)
			{
				return;
			}
			GuideUIView.Instance.LockScreen(GuideManager.LockMode.IsLockScreen(dataStep), dataStep.successMode.get_Item(0), dataStep.skipPosition);
			if (dataStep.successMode.get_Item(0) == 5)
			{
				this.automaticTime_timer_id = TimerHeap.AddTimer((uint)(dataStep.liveTime * 1000f), 0, delegate
				{
					if (GuideUIView.Instance != null)
					{
						GuideUIView.Instance.Show(false);
					}
					EventDispatcher.Broadcast("GuideManager.NextStep");
				});
			}
			else
			{
				TimerHeap.DelTimer(this.automaticTime_timer_id);
			}
		}
	}

	private bool StepFinger(GuideStep dataStep)
	{
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, true);
		if (transform == null)
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget;
			this.mDealStepIfCanStateTracker.widgetId = dataStep.widgetId;
			return false;
		}
		this.BeginStep(dataStep, true, UIType.NonPush);
		this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.WaitingInput;
		GuideUIView.Instance.SetFingerUI(transform);
		this.ChangeGuideWidget(transform, dataStep);
		this.FaultTolerant();
		return true;
	}

	private bool StepFinger_InstructionRole(GuideStep dataStep)
	{
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, true);
		if (transform == null)
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget;
			this.mDealStepIfCanStateTracker.widgetId = dataStep.widgetId;
			return false;
		}
		this.BeginStep(dataStep, true, UIType.NonPush);
		this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.WaitingInput;
		GuideUIView.Instance.mStepType = 2;
		GuideUIView.Instance.SetFingerUI(transform);
		Vector2 pos = GuideManager.GetPos(dataStep);
		GuideUIView.Instance.SetInstructionRoleUI(pos, GameDataUtils.GetChineseContent(dataStep.instructionId, false));
		this.ChangeGuideWidget(transform, dataStep);
		this.FaultTolerant();
		return true;
	}

	private bool StepFinger_InstructionBubble(GuideStep dataStep)
	{
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, true);
		if (transform == null)
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget;
			this.mDealStepIfCanStateTracker.widgetId = dataStep.widgetId;
			return false;
		}
		this.BeginStep(dataStep, true, UIType.NonPush);
		this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.WaitingInput;
		GuideUIView.Instance.mStepType = 7;
		GuideUIView.Instance.SetFingerUI(transform);
		Vector2 pos = GuideManager.GetPos(dataStep);
		GuideUIView.Instance.SetInstructionBubbleUI(pos, GameDataUtils.GetChineseContent(dataStep.instructionId, false), dataStep.direction);
		this.ChangeGuideWidget(transform, dataStep);
		this.FaultTolerant();
		return true;
	}

	private bool StepInstructionBubble(GuideStep dataStep)
	{
		this.BeginStep(dataStep, true, UIType.NonPush);
		Vector2 pos = GuideManager.GetPos(dataStep);
		GuideUIView.Instance.SetInstructionBubbleUI(pos, GameDataUtils.GetChineseContent(dataStep.instructionId, false), dataStep.direction);
		this.FaultTolerant();
		return true;
	}

	private bool StepInstructionRole(GuideStep dataStep)
	{
		this.BeginStep(dataStep, true, UIType.NonPush);
		Vector2 pos = GuideManager.GetPos(dataStep);
		GuideUIView.Instance.SetInstructionRoleUI(pos, GameDataUtils.GetChineseContent(dataStep.instructionId, false));
		return true;
	}

	private bool StepSystemOpen(GuideStep dataStep)
	{
		this.BeginStep(dataStep, true, UIType.NonPush);
		if (dataStep.systemId <= 0)
		{
			return false;
		}
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(dataStep.systemId);
		if (systemOpen == null)
		{
			return false;
		}
		GuideUIView.Instance.SetSystemOpen(systemOpen);
		return true;
	}

	private bool StepWidgetsOff(GuideStep dataStep)
	{
		if (this.JustStepWidgetsOff(dataStep))
		{
			this.BeginStep(dataStep, false, UIType.NonPush);
			EventDispatcher.Broadcast("GuideManager.NextStep");
			return true;
		}
		this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.StepWidgetsOff;
		return false;
	}

	private bool JustStepWidgetsOff(GuideStep dataStep)
	{
		if (dataStep.dynamicWidgets.get_Count() == 0)
		{
			return true;
		}
		bool result = false;
		List<int> dynamicWidgets = dataStep.dynamicWidgets;
		for (int i = 0; i < dynamicWidgets.get_Count(); i++)
		{
			Transform transform = GuideManager.GuideFindWidgetOnUI(dynamicWidgets.get_Item(i), false);
			if (transform != null)
			{
				result = true;
				transform.get_gameObject().SetActive(false);
			}
		}
		return result;
	}

	private bool StepWidgetsOn(GuideStep dataStep)
	{
		if (this.JustStepWidgetsOn(dataStep))
		{
			this.BeginStep(dataStep, false, UIType.NonPush);
			EventDispatcher.Broadcast("GuideManager.NextStep");
			return true;
		}
		this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.StepWidgetsOn;
		return false;
	}

	private bool JustStepWidgetsOn(GuideStep dataStep)
	{
		if (dataStep.dynamicWidgets.get_Count() == 0)
		{
			return true;
		}
		bool result = false;
		List<int> dynamicWidgets = dataStep.dynamicWidgets;
		for (int i = 0; i < dynamicWidgets.get_Count(); i++)
		{
			Transform transform = GuideManager.GuideFindWidgetOnUI(dynamicWidgets.get_Item(i), true);
			if (transform != null)
			{
				result = true;
				transform.get_gameObject().SetActive(true);
			}
		}
		return result;
	}

	private bool StepPlaySpine(GuideStep dataStep)
	{
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, true);
		if (transform == null)
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget;
			this.mDealStepIfCanStateTracker.widgetId = dataStep.widgetId;
			return false;
		}
		for (int i = 0; i < dataStep.effectId.get_Count(); i++)
		{
			FXSpineManager.Instance.PlaySpine(dataStep.effectId.get_Item(i), transform, string.Empty, 10003 + i + 1, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		this.BeginStep(dataStep, false, UIType.NonPush);
		EventDispatcher.Broadcast("GuideManager.NextStep");
		return true;
	}

	private bool StepSkillOpenInBattle(GuideStep dataStep)
	{
		Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.widgetId, true);
		if (transform == null)
		{
			this.mDealStepIfCanStateTracker.current_state = GuideManager.DealStepIfCanStateTracker.State.FindingNoWidget;
			this.mDealStepIfCanStateTracker.widgetId = dataStep.widgetId;
			return false;
		}
		this.BeginStep(dataStep, true, UIType.NonPush);
		GuideUIView.Instance.SetSkillOpen(transform);
		return true;
	}

	private bool StepImageShow(GuideStep dataStep)
	{
		GuideUIView.IsPopUIPrevious = false;
		this.BeginStep(dataStep, true, UIType.NonPush);
		GuideUIView.Instance.SetImageShowUI(dataStep);
		return true;
	}

	public void CheckCurrentGuide()
	{
		if (this.guide_group <= 0)
		{
			return;
		}
		GuideStep guideStep = GuideManager.FindStep(this.guide_group, this.guide_step);
		if (guideStep == null)
		{
			return;
		}
		if (guideStep.triggerUI <= 0)
		{
			return;
		}
		if (!GuideManager.LockMode.IsLockScreen(guideStep) && !WidgetSystem.IsUIOpen(guideStep.triggerUI))
		{
			this.StopGuideOfFinish();
		}
	}

	public bool IsNPCEnterLock()
	{
		return this.mDealStepIfCanStateTracker.current_state == GuideManager.DealStepIfCanStateTracker.State.WaitingInput;
	}

	private void ResetAll()
	{
		this.guide_group = 0;
		this.guide_step = 0;
		this.mDealStepIfCanStateTracker.Reset();
	}

	private void FaultTolerant()
	{
		TimerHeap.DelTimer(this.fault_tolerant_timer_id);
		this.fault_tolerant_timer_id = TimerHeap.AddTimer(60000000u, 0, delegate
		{
			this.StopGuide();
		});
	}

	private void SetLockOfMintime(GuideStep dataStep)
	{
		if (dataStep.minTime > 0f)
		{
			this.mintime_lock = true;
			TimerHeap.DelTimer(this.mintime_lock_timer_id);
			this.mintime_lock_timer_id = TimerHeap.AddTimer((uint)(dataStep.minTime * 1000f), 0, delegate
			{
				this.mintime_lock = false;
			});
		}
	}

	private void ChangeGuideWidget(Transform guideWidget, GuideStep dataStep)
	{
		if (guideWidget == null)
		{
			return;
		}
		if (GuideManager.LockMode.IsLockScreen(dataStep))
		{
			this.IsNeedChangeGuideLayer = true;
		}
		else
		{
			this.IsNeedChangeGuideLayer = false;
		}
		if (dataStep.successMode.get_Count() <= 0 || (dataStep.successMode.get_Item(0) != 2 && dataStep.successMode.get_Item(0) != 5))
		{
			Transform transform = GuideManager.FindWidget_FaultTip(dataStep, dataStep.lightWidgetId, false);
			if (transform != null)
			{
				this.ChangeGuideWidget(transform);
			}
			else
			{
				this.ChangeGuideWidget(guideWidget);
			}
		}
	}

	private void ChangeGuideWidget(Transform target)
	{
		GuideFocusWidget guideFocusWidget = target.get_gameObject().GetComponent<GuideFocusWidget>();
		if (guideFocusWidget != null)
		{
			guideFocusWidget.set_enabled(false);
			guideFocusWidget.set_enabled(true);
		}
		else
		{
			guideFocusWidget = target.get_gameObject().AddComponent<GuideFocusWidget>();
		}
	}

	private bool IsGuideSysLock()
	{
		return this.guide_lock || this.out_system_lock;
	}

	private static GuideStep FindStep(int group, int step)
	{
		if (group == 0)
		{
			return null;
		}
		List<GuideStep> dataList = DataReader<GuideStep>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).group == group && dataList.get_Item(i).step == step)
			{
				return dataList.get_Item(i);
			}
		}
		return null;
	}

	private static Transform FindWidget_FaultTip(GuideStep dataStep, int widgetId, bool isErrorTip)
	{
		if (widgetId <= 0)
		{
			return null;
		}
		Transform transform = GuideManager.GuideFindWidgetOnUI(widgetId, true);
		if (transform == null)
		{
			return null;
		}
		string text = WidgetSystem.FindNameOfUIByWidget(widgetId);
		if (text == "TownUI" && TownUI.Instance != null)
		{
			TownUI.Instance.ForceOpenRightBottom();
		}
		return transform;
	}

	private static Vector2 GetPos(GuideStep dataStep)
	{
		Vector2 zero = Vector2.get_zero();
		if (dataStep.pos.get_Count() >= 2)
		{
			zero = new Vector2(dataStep.pos.get_Item(0), dataStep.pos.get_Item(1));
		}
		return zero;
	}

	public static bool IsGuideSystemPause(string uiName)
	{
		return uiName == "ChangeCareerSuccessUI" || uiName == "PetObtainUI" || uiName == "LevelUpUI" || uiName == "UpgradeUI" || uiName == "PVPUpUI";
	}

	public static Transform GuideFindWidgetOnUI(int widgetId, bool activeSelf = true)
	{
		Transform transform = WidgetSystem.FindWidgetOnUI(widgetId, activeSelf);
		if (transform != null)
		{
			string text = WidgetSystem.FindNameOfUIByWidget(widgetId);
			if (text == "TaskDescUI")
			{
				GuideManager.Instance.talk_desc_ui_lock = false;
			}
			else
			{
				GuideManager.Instance.talk_desc_ui_lock = true;
				UIManagerControl.Instance.HideUI("TaskDescUI");
			}
		}
		return transform;
	}

	public void TriggerPassOfLogic(int id)
	{
		Guide guide = DataReader<Guide>.Get(id);
		if (guide != null)
		{
			this.TriggerPass(guide);
		}
	}

	public void TriggerPassOfGM(int id)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			UIManagerControl.Instance.ShowToastText("指引系统未开启, 请到设置界面开启");
			return;
		}
		Guide guide = DataReader<Guide>.Get(id);
		if (guide != null)
		{
			this.m_isgm = true;
			this.TriggerPass(guide);
		}
	}

	public void TriggerPassOfGMNow(int id)
	{
		if (!SystemConfig.IsGuideSystemOn)
		{
			UIManagerControl.Instance.ShowToastText("指引系统未开启, 请到设置界面开启");
			return;
		}
		Guide guide = DataReader<Guide>.Get(id);
		if (guide != null)
		{
			this.m_isgm = true;
			this.StopGuide();
			this.guide_group = guide.id;
			this.guide_step = 0;
			this.BeginGuide();
		}
	}

	public void StopGM()
	{
		this.m_isgm = false;
	}

	public void StopGuideOfGM()
	{
		this.m_listOfQueue.Clear();
		this.StopGuide();
	}

	public string PrintMessage()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text2 = string.Concat(new object[]
		{
			"指引调试日志： group = ",
			this.guide_group,
			", step = ",
			this.guide_step
		});
		Debug.LogWarning(text2);
		text = text + text2 + "\n";
		if (MainTaskManager.Instance.CurTaskId > 0)
		{
			Debug.LogWarning("指引,当前任务id = " + MainTaskManager.Instance.CurTaskId);
		}
		text2 = "指引调试日志： lock = " + this.guide_lock;
		Debug.LogWarning(text2);
		text += text2;
		for (int i = 0; i < this.m_listOfQueue.get_Count(); i++)
		{
			text2 = "指引调试日志：  [队列中]指引id = " + this.m_listOfQueue.get_Item(i).group;
			Debug.LogWarning(text2);
			text = text + text2 + "\n";
		}
		for (int j = 0; j < this.m_listOfComplete.get_Count(); j++)
		{
			text2 = "指引调试日志： [已完成]指引id = " + this.m_listOfComplete.get_Item(j).guideGroupId;
			Debug.LogWarning(text2);
			text = text + text2 + "\n";
		}
		this.mDealStepIfCanStateTracker.DebugIfNeed();
		return text;
	}
}
