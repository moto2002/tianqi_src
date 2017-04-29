using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ZeroTaskProgressUI : BaseUIBehaviour
{
	private Text mTxTitle;

	private Text mTxLine;

	private Text mTxDesc;

	private BaseTask mTempTask;

	public void AwakeSelf()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.mTxTitle = base.FindTransform("txTitle").GetComponent<Text>();
		this.mTxLine = base.FindTransform("txLine").GetComponent<Text>();
		this.mTxDesc = base.FindTransform("txDesc").GetComponent<Text>();
		base.FindTransform("Background").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButton);
	}

	protected void OnEnable()
	{
		this.RefreshUI();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.SortTaskList, new Callback(this.RefreshUI));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.SortTaskList, new Callback(this.RefreshUI));
	}

	private void RefreshUI()
	{
		this.mTxTitle.set_text(string.Format("零城任务({0}/{1})", MainTaskManager.Instance.ZeroMaxTimes - MainTaskManager.Instance.ZeroTaskTimes, MainTaskManager.Instance.ZeroMaxTimes));
		this.mTxLine.set_text(MainTaskItem.GetUnderline(this.mTxTitle, 0));
		bool flag = false;
		for (int i = 0; i < MainTaskManager.Instance.ZeroTaskId.Length; i++)
		{
			if (!flag && MainTaskManager.Instance.GetTask(MainTaskManager.Instance.ZeroTaskId[i], out this.mTempTask, true))
			{
				flag = (this.mTempTask.Task.extParams.get_Item(4) == 4);
			}
		}
		if (flag)
		{
			this.mTxDesc.set_text("点击领取零城任务奖励");
		}
		else if (MainTaskManager.Instance.ZeroTaskTimes > 0)
		{
			this.mTxDesc.set_text("点击接取零城任务");
		}
		else
		{
			this.mTxDesc.set_text("零城任务已完成");
		}
	}

	private void OnClickButton(GameObject go)
	{
		LinkNavigationManager.OpenZeroTaskUI();
	}
}
