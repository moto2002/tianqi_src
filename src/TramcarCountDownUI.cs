using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class TramcarCountDownUI : UIBase
{
	private Image mNumber0;

	private Image mNumber1;

	private TimeCountDown mTimeCountDown;

	private Action mOnFinish;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.mNumber0 = base.FindTransform("Number0").GetComponent<Image>();
		this.mNumber1 = base.FindTransform("Number1").GetComponent<Image>();
	}

	private void AddTramcarCountDown(int remianTime)
	{
		this.RemoveTramcarCountDown();
		if (remianTime <= 0)
		{
			this.OnCountdownEnd();
			return;
		}
		this.mTimeCountDown = new TimeCountDown(remianTime, TimeFormat.SECOND, new Action(this.OnCountdown), new Action(this.OnCountdownEnd), true);
	}

	private void SetUISecond(int second)
	{
		if (second > 99)
		{
			second = 99;
		}
		if (second < 0)
		{
			second = 0;
		}
		this.mNumber0.get_gameObject().SetActive(second >= 10);
		ResourceManager.SetSprite(this.mNumber0, ResourceManager.GetIconSprite("fight_combofont_" + second / 10));
		ResourceManager.SetSprite(this.mNumber1, ResourceManager.GetIconSprite("fight_combofont_" + second % 10));
	}

	private void OnCountdown()
	{
		this.SetUISecond(this.mTimeCountDown.GetSeconds());
	}

	private void OnCountdownEnd()
	{
		if (this.mOnFinish != null)
		{
			this.mOnFinish.Invoke();
		}
		this.Show(false);
	}

	public void Open(int second, Action onFinish)
	{
		this.mOnFinish = onFinish;
		this.SetUISecond(second);
		this.AddTramcarCountDown(second);
	}

	public void RemoveTramcarCountDown()
	{
		if (this.mTimeCountDown != null)
		{
			this.mTimeCountDown.Dispose();
			this.mTimeCountDown = null;
		}
	}
}
