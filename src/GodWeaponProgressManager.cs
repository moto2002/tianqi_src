using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GodWeaponProgressManager : BaseSubSystemManager
{
	private const int STATUS_TASK = 1;

	private const int STATUS_LEVEL = 2;

	private XiTongKaiQiYuGao CurSystemData;

	private Artifact CurGodWeaponData;

	private Dictionary<int, List<int>> mTaskRangeDict;

	private bool mIsUseDiamond;

	private bool mIsEffectTimeout;

	private bool mIsCommitResult;

	private static GodWeaponProgressManager instance;

	public static GodWeaponProgressManager Instance
	{
		get
		{
			if (GodWeaponProgressManager.instance == null)
			{
				GodWeaponProgressManager.instance = new GodWeaponProgressManager();
			}
			return GodWeaponProgressManager.instance;
		}
	}

	private GodWeaponProgressManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.InitAcitveDict();
	}

	public override void Release()
	{
		this.mIsUseDiamond = false;
		this.CurSystemData = null;
		this.mTaskRangeDict.Clear();
		this.mTaskRangeDict = null;
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(EventNames.CurTaskChange, new Callback(this.OnCurrentTaskChange));
		EventDispatcher.AddListener<HolyWeaponInfo>(EventNames.GotGodWeaponNty, new Callback<HolyWeaponInfo>(this.OnCurrentWeaponChange));
		EventDispatcher.AddListener<BaseTask>(EventNames.FinishTaskNty, new Callback<BaseTask>(this.OnFinishTaskNty));
	}

	private void OnCurrentTaskChange()
	{
		if (GodWeaponManager.Instance.WeaponDict != null)
		{
			this.Refresh();
		}
	}

	private void OnCurrentWeaponChange(HolyWeaponInfo info)
	{
		this.OnCurrentTaskChange();
	}

	private void OnLevelUp()
	{
		this.LevelUp();
		this.Refresh();
	}

	public void Refresh()
	{
		this.SetCurrentData();
		this.RefreshUI();
		EventDispatcher.Broadcast(EventNames.SystemOpenProgressChange);
	}

	private void SetCurrentData()
	{
		GodWeaponProgressManager.<SetCurrentData>c__AnonStoreyB6 <SetCurrentData>c__AnonStoreyB = new GodWeaponProgressManager.<SetCurrentData>c__AnonStoreyB6();
		this.CurSystemData = null;
		this.CurGodWeaponData = null;
		<SetCurrentData>c__AnonStoreyB.godInfos = GodWeaponManager.Instance.WeaponDict.get_Item(1);
		List<XiTongKaiQiYuGao> dataList = DataReader<XiTongKaiQiYuGao>.DataList;
		int index;
		for (index = 0; index < <SetCurrentData>c__AnonStoreyB.godInfos.get_Count(); index++)
		{
			XiTongKaiQiYuGao xiTongKaiQiYuGao = dataList.Find((XiTongKaiQiYuGao e) => e.artifactId == <SetCurrentData>c__AnonStoreyB.godInfos.get_Item(index).Id);
			Artifact artifact = DataReader<Artifact>.Get(<SetCurrentData>c__AnonStoreyB.godInfos.get_Item(index).Id);
			if (!this.IsSystemOpened(xiTongKaiQiYuGao, artifact, <SetCurrentData>c__AnonStoreyB.godInfos.get_Item(index)))
			{
				this.CurSystemData = xiTongKaiQiYuGao;
				this.CurGodWeaponData = artifact;
				return;
			}
		}
	}

	private bool IsSystemOpened(XiTongKaiQiYuGao sysData, Artifact godData, HolyWeaponInfo godInfo)
	{
		return sysData != null && godData != null && godInfo != null && EntityWorld.Instance.EntSelf != null && (sysData.openType != 1 || (MainTaskManager.Instance.IsFinishedTask(godData.activationParameter) && godInfo.State == 2));
	}

	private float GetGodWeaponProgress()
	{
		if (this.CurSystemData == null || this.CurGodWeaponData == null || this.mTaskRangeDict == null)
		{
			return 1f;
		}
		if (this.CurSystemData.openType == 1)
		{
			List<int> list = null;
			if (this.mTaskRangeDict.TryGetValue(this.CurGodWeaponData.id, ref list))
			{
				return this.GetProgress(list, MainTaskManager.Instance.MainTaskId);
			}
			Debug.Log("找不到神器[" + this.CurGodWeaponData.id + "]任务进度!!!");
		}
		return 0f;
	}

	private float GetProgress(List<int> list, int taskId)
	{
		int i;
		for (i = 0; i < list.get_Count(); i++)
		{
			if (list.get_Item(i) >= taskId)
			{
				break;
			}
		}
		return (float)i / (float)list.get_Count();
	}

	private bool IsTaskInCurrent(int taskId)
	{
		if (this.CurSystemData == null)
		{
			return false;
		}
		if (this.CurSystemData.openType != 1)
		{
			return false;
		}
		if (this.CurGodWeaponData == null)
		{
			return false;
		}
		List<int> list = null;
		return this.mTaskRangeDict.TryGetValue(this.CurGodWeaponData.id, ref list) && list.Exists((int e) => e == taskId);
	}

	private void InitAcitveDict()
	{
		this.mTaskRangeDict = new Dictionary<int, List<int>>();
		List<XiTongKaiQiYuGao> dataList = DataReader<XiTongKaiQiYuGao>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			XiTongKaiQiYuGao xiTongKaiQiYuGao = dataList.get_Item(i);
			if (xiTongKaiQiYuGao.artifactId > 0 && xiTongKaiQiYuGao.start > 0 && xiTongKaiQiYuGao.ending > 0)
			{
				this.mTaskRangeDict.Add(xiTongKaiQiYuGao.artifactId, this.GetTaskRange(xiTongKaiQiYuGao.start, xiTongKaiQiYuGao.ending));
			}
		}
	}

	private List<int> GetTaskRange(int startId, int endId)
	{
		List<int> list = new List<int>();
		this.FindNextTask(startId, endId, list);
		return list;
	}

	private void FindNextTask(int id, int endId, List<int> list)
	{
		RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(id);
		if (renWuPeiZhi == null)
		{
			return;
		}
		list.Add(renWuPeiZhi.id);
		if (renWuPeiZhi.nextTask > endId)
		{
			return;
		}
		this.FindNextTask(renWuPeiZhi.nextTask, endId, list);
	}

	public void RefreshUI()
	{
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return;
		}
		if (GodWeaponProgressUIView.Instance == null)
		{
			return;
		}
		if (!this.IsSystemProgressOn())
		{
			GodWeaponProgressUIView.Instance.get_gameObject().SetActive(false);
		}
		else
		{
			GodWeaponProgressUIView.Instance.get_gameObject().SetActive(true);
			float godWeaponProgress = this.GetGodWeaponProgress();
			GodWeaponProgressUIView.Instance.SetProgress(godWeaponProgress);
			GodWeaponProgressUIView.Instance.SetProgress((int)(godWeaponProgress * 100f) + "%");
			if (this.CurGodWeaponData != null && this.CurSystemData != null)
			{
				GodWeaponProgressUIView.Instance.SetSystemIcon(this.CurSystemData.icon);
				GodWeaponProgressUIView.Instance.SetSystemTip(GameDataUtils.GetChineseContent(this.CurGodWeaponData.name, false));
			}
		}
	}

	public bool IsSystemProgressOn()
	{
		return this.CurSystemData != null && this.CurGodWeaponData != null;
	}

	public void OpenGodWeaponUI()
	{
		if (this.CurGodWeaponData == null)
		{
			return;
		}
		GodWeaponManager.Instance.OpenDescId = this.CurGodWeaponData.id;
		LinkNavigationManager.OpenGodWeaponUI();
	}

	public bool NeedPlayCommitEffect(BaseTask cell, Action<BaseTask, bool> onFinish, bool isUseDiamond)
	{
		if (cell.Task.taskType != Package.Task.TaskType.MainTask)
		{
			return false;
		}
		if (!this.IsTaskInCurrent(cell.Task.taskId))
		{
			return false;
		}
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return false;
		}
		this.mIsEffectTimeout = false;
		this.mIsCommitResult = false;
		this.mIsUseDiamond = isUseDiamond;
		this.PlaySpineInTaskDescUI(delegate
		{
			if (onFinish != null)
			{
				onFinish.Invoke(cell, isUseDiamond);
			}
		});
		TimerHeap.AddTimer(1500u, 0, delegate
		{
			this.mIsEffectTimeout = true;
			if (this.mIsCommitResult)
			{
				this.PlaySpineInSystemOpenProgressUI();
			}
		});
		return true;
	}

	public void LevelUp()
	{
		if (this.CurSystemData == null)
		{
			return;
		}
		if (this.CurSystemData.openType != 2)
		{
			return;
		}
		this.PlaySpineInSystemOpenProgressUI();
	}

	private void OnFinishTaskNty(BaseTask cell)
	{
		if (cell != null)
		{
			this.mIsCommitResult = true;
			if (this.mIsEffectTimeout)
			{
				this.PlaySpineInSystemOpenProgressUI();
			}
		}
	}

	private void PlaySpineInTaskDescUI(Action onFinish)
	{
		FXSpineManager.Instance.PlaySpine(3308, (!this.mIsUseDiamond) ? TaskDescUI.Instance.BtnGet.get_transform() : TaskDescUI.Instance.BtnGetMultiple.get_transform(), "TaskDescUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(3306, (!this.mIsUseDiamond) ? TaskDescUI.Instance.BtnGet.get_transform() : TaskDescUI.Instance.BtnGetMultiple.get_transform(), "TaskDescUI", 3001, onFinish, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void PlaySpineInSystemOpenProgressUI()
	{
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return;
		}
		FXSpineManager.Instance.PlaySpine(3310, TownUI.Instance.FXGodWeaponProgressUI, string.Empty, 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(3309, TownUI.Instance.FXGodWeaponProgressUI, string.Empty, 2001, null, "UI", 15f, -15f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(3307, TownUI.Instance.FXGodWeaponProgressUI, string.Empty, 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
