using System;
using UnityEngine;

public class UIMotion : MonoBehaviour
{
	private const string MOTION_OPEN = "uiopen";

	private const string MOTION_CLOSE = "uiclose";

	public bool MOTION_OPEN_ON = true;

	public bool MOTION_CLOSE_ON = true;

	private Animator m_animator;

	public Action actionCallback;

	private string transform_name = string.Empty;

	private uint TimerId;

	private Animator GetAnimator()
	{
		if (this.m_animator == null)
		{
			this.m_animator = base.GetComponent<Animator>();
		}
		return this.m_animator;
	}

	private void Awake()
	{
		this.transform_name = base.get_transform().get_name();
	}

	private void OnDisable()
	{
		TimerHeap.DelTimer(this.TimerId);
		if (this.GetAnimator() != null)
		{
			this.GetAnimator().set_enabled(false);
		}
	}

	public void OnMotionEnd()
	{
		UIMotionsCounter.Instance.listMotions.Remove(this.transform_name);
		if (UIMotionsCounter.Instance.listMotions.get_Count() == 0)
		{
			UIMotionSystem.IsLock = false;
		}
		TimerHeap.DelTimer(this.TimerId);
		if (this.actionCallback != null)
		{
			this.actionCallback.Invoke();
		}
	}

	public bool MotionOpen(Action action)
	{
		this.actionCallback = action;
		if (this.MOTION_OPEN_ON && this.GetAnimator() != null && this.GetAnimator().HasAction("uiopen"))
		{
			UIMotionSystem.IsLock = true;
			if (!UIMotionsCounter.Instance.listMotions.Contains(this.transform_name))
			{
				UIMotionsCounter.Instance.listMotions.Add(this.transform_name);
			}
			this.GetAnimator().set_enabled(true);
			this.GetAnimator().PlayInFixedTime("uiopen");
			this.SafeSure();
			return true;
		}
		if (this.actionCallback != null)
		{
			this.actionCallback.Invoke();
		}
		return false;
	}

	public bool MotionClose(Action action)
	{
		this.actionCallback = action;
		if (this.MOTION_CLOSE_ON && this.GetAnimator() != null && this.GetAnimator().HasAction("uiclose"))
		{
			UIMotionSystem.IsLock = true;
			if (!UIMotionsCounter.Instance.listMotions.Contains(this.transform_name))
			{
				UIMotionsCounter.Instance.listMotions.Add(this.transform_name);
			}
			this.GetAnimator().set_enabled(true);
			this.GetAnimator().PlayInFixedTime("uiclose");
			this.SafeSure();
			return true;
		}
		if (this.actionCallback != null)
		{
			this.actionCallback.Invoke();
		}
		return false;
	}

	private void SafeSure()
	{
		TimerHeap.DelTimer(this.TimerId);
		this.TimerId = TimerHeap.AddTimer(10000u, 0, delegate
		{
			this.OnMotionEnd();
		});
	}
}
