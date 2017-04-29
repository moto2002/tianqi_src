using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUIView : UIBase
{
	public static WaitingUIView Instance;

	public string m_uiNameOfWaiting = string.Empty;

	private bool IsLockOfWaitOpenUI;

	private bool IsLockOfWaitNextGuide;

	private Image maskImage;

	private uint maxtime_lock_timer_id;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isInterruptStick = false;
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		WaitingUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.maskImage = base.FindTransform("Mask").GetComponent<Image>();
	}

	protected override void OnDisable()
	{
		TimerHeap.DelTimer(this.maxtime_lock_timer_id);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<string>("UIManagerControl.UIOpenOfSuccess", new Callback<string>(this.UIOpenOfSuccess));
		EventDispatcher.AddListener("GuideManager.LockOffWaitNextGuide", new Callback(this.LockOffWaitNextGuide));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<string>("UIManagerControl.UIOpenOfSuccess", new Callback<string>(this.UIOpenOfSuccess));
		EventDispatcher.RemoveListener("GuideManager.LockOffWaitNextGuide", new Callback(this.LockOffWaitNextGuide));
	}

	private void UIOpenOfSuccess(string uiName)
	{
		if (this.m_uiNameOfWaiting == uiName)
		{
			this.IsLockOfWaitOpenUI = false;
			this.LockOff();
		}
	}

	private void LockOffWaitNextGuide()
	{
		this.IsLockOfWaitNextGuide = false;
		this.LockOff();
	}

	private void LockOff()
	{
		if (!this.IsLockOfWaitOpenUI && !this.IsLockOfWaitNextGuide)
		{
			this.Show(false);
		}
	}

	public void LockOnOfWaitOpenUI(string uiNameOfWaiting)
	{
		this.IsLockOfWaitOpenUI = true;
		this.m_uiNameOfWaiting = uiNameOfWaiting;
		this.SetLockOfMintime();
	}

	public void LockOnOfWaitNextGuide()
	{
		this.IsLockOfWaitNextGuide = true;
		this.SetLockOfMintime();
	}

	public void SetAlpha(float alpha)
	{
		this.maskImage.set_color(new Color(this.maskImage.get_color().r, this.maskImage.get_color().g, this.maskImage.get_color().b, alpha));
	}

	private void SetLockOfMintime()
	{
		TimerHeap.DelTimer(this.maxtime_lock_timer_id);
		this.maxtime_lock_timer_id = TimerHeap.AddTimer(8000u, 0, delegate
		{
			this.LockOff();
		});
	}
}
