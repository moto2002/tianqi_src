using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleProgressBarUI : UIBase
{
	protected Text BattleProgressBarUITitle;

	protected Slider BattleProgressBarUISlider;

	protected bool isCountingdown;

	protected Action callback;

	protected float defaultTime;

	protected float curTime;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isEndNav = false;
		this.isInterruptStick = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BattleProgressBarUITitle = base.FindTransform("BattleProgressBarUITitle").GetComponent<Text>();
		this.BattleProgressBarUISlider = base.FindTransform("BattleProgressBarUISlider").GetComponent<Slider>();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.isCountingdown = false;
		this.callback = null;
	}

	private void Update()
	{
		if (!this.isCountingdown)
		{
			return;
		}
		this.curTime -= Time.get_deltaTime();
		if (this.curTime <= 0f)
		{
			this.isCountingdown = false;
			this.BattleProgressBarUISlider.set_value(1f);
			if (this.callback != null)
			{
				this.callback.Invoke();
			}
		}
		else
		{
			this.BattleProgressBarUISlider.set_value((this.defaultTime == 0f) ? 0f : ((this.defaultTime - this.curTime) / this.defaultTime));
		}
	}

	public void SetData(string text, float theDefaultTime, float theCurTime, Action theCallback)
	{
		this.isCountingdown = true;
		this.defaultTime = theDefaultTime;
		this.curTime = theCurTime;
		this.callback = theCallback;
		this.BattleProgressBarUITitle.set_text(text);
		this.BattleProgressBarUISlider.set_value((this.defaultTime == 0f) ? 0f : ((this.defaultTime - this.curTime) / this.defaultTime));
	}
}
