using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : UIBase
{
	protected GameObject PowerUpUIThing;

	protected Image PowerUpUIBG;

	protected Transform PowerUpUINum;

	protected List<Image> PowerUpUINumList = new List<Image>();

	protected Transform PowerUpUIEffect;

	protected int fx_uid_open = 2147483647;

	protected int fx_uid_close = 2147483647;

	protected int fx_templateId_open;

	protected int fx_templateId_close;

	protected uint numTimer = 4294967295u;

	protected bool isRollingNumber;

	protected bool isShowingOpenFx;

	protected int slowDownTimes = 17;

	protected int finishTimes = 23;

	protected uint interval = 40u;

	protected uint slowDownInterval = 100u;

	protected uint beginInterval = 300u;

	protected uint finishInterval = 1000u;

	protected bool IsRollingNumber
	{
		get
		{
			return this.isRollingNumber;
		}
		set
		{
			this.isRollingNumber = value;
		}
	}

	protected bool IsShowingOpenFx
	{
		get
		{
			return this.isShowingOpenFx;
		}
		set
		{
			this.isShowingOpenFx = value;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isInterruptStick = false;
		this.isEndNav = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.PowerUpUIThing = base.FindTransform("PowerUpUIThing").get_gameObject();
		this.PowerUpUIThing.SetActive(false);
		this.PowerUpUIBG = base.FindTransform("PowerUpUIBG").GetComponent<Image>();
		this.PowerUpUINum = base.FindTransform("PowerUpUINum");
		for (int i = 0; i < 10; i++)
		{
			this.PowerUpUINumList.Add(base.FindTransform("PowerUpUINum" + i).GetComponent<Image>());
		}
		this.PowerUpUIEffect = base.FindTransform("PowerUpUIEffect");
		for (int j = 0; j < this.PowerUpUINumList.get_Count(); j++)
		{
			this.PowerUpUINumList.get_Item(j).get_gameObject().SetActive(false);
			this.PowerUpUINumList.get_Item(j).get_transform().set_localPosition(new Vector3((float)(j * 40 + 65), -6f, 0f));
		}
	}

	protected override void OnDisable()
	{
		TimerHeap.DelTimer(this.numTimer);
		base.OnDisable();
	}

	public void SetPowerUp(int oldValue, int newValue)
	{
		if (oldValue == newValue)
		{
			this.Show(false);
			return;
		}
		FXSpineManager.Instance.DeleteSpine(this.fx_uid_open, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_uid_close, true);
		TimerHeap.DelTimer(this.numTimer);
		string prefix = "font_zhanliup_";
		if (oldValue > newValue)
		{
			prefix = "font_zhanlidown_";
		}
		this.IsRollingNumber = true;
		this.IsShowingOpenFx = true;
		this.PowerUpUIThing.SetActive(true);
		this.DoVerticalOpen();
		this.SetBG(oldValue > newValue);
		this.SetNumPos((oldValue <= newValue) ? newValue : oldValue);
		this.ShowPowerChange(oldValue, newValue, prefix, 0, this.finishTimes);
		if (oldValue > newValue)
		{
			this.fx_templateId_open = 1502;
			this.fx_templateId_close = 1502;
		}
		else
		{
			this.fx_templateId_open = 1501;
			this.fx_templateId_close = 1501;
		}
		this.fx_uid_open = FXSpineManager.Instance.PlaySpine(this.fx_templateId_open, this.PowerUpUIEffect, "PowerUpUI", 15001, delegate
		{
			this.fx_uid_open = 0;
			this.IsShowingOpenFx = false;
			this.CheckOpenEnd();
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void SetNumPos(int thisValue)
	{
		this.PowerUpUINum.set_localPosition(new Vector3((float)(-(float)(thisValue.ToString().get_Length() - 1)) * 40f / 2f, 0f, 0f));
	}

	protected void SetBG(bool isDown)
	{
		if (isDown)
		{
			ResourceManager.SetSprite(this.PowerUpUIBG, ResourceManager.GetIconSprite("zhandouli_down"));
		}
		else
		{
			ResourceManager.SetSprite(this.PowerUpUIBG, ResourceManager.GetIconSprite("zhandouli_up"));
		}
	}

	protected void ShowPowerChange(int oldValue, int newValue, string prefix, int animateCounter, int animateTimes)
	{
		int thisValue = (int)((float)oldValue + (float)(newValue - oldValue) * (float)animateCounter / (float)animateTimes);
		if (animateCounter == 0)
		{
			this.SetNum(thisValue, prefix);
			this.numTimer = TimerHeap.AddTimer(this.beginInterval + this.interval, 0, delegate
			{
				this.ShowPowerChange(oldValue, newValue, prefix, animateCounter + 1, animateTimes);
			});
		}
		else if (animateCounter > animateTimes)
		{
			this.IsRollingNumber = false;
			this.CheckOpenEnd();
		}
		else if (animateCounter > this.slowDownTimes)
		{
			this.SetNum(thisValue, prefix);
			this.numTimer = TimerHeap.AddTimer(this.slowDownInterval, 0, delegate
			{
				this.ShowPowerChange(oldValue, newValue, prefix, animateCounter + 1, animateTimes);
			});
		}
		else
		{
			this.SetNum(thisValue, prefix);
			this.numTimer = TimerHeap.AddTimer(this.interval, 0, delegate
			{
				this.ShowPowerChange(oldValue, newValue, prefix, animateCounter + 1, animateTimes);
			});
		}
	}

	protected void SetNum(int thisValue, string prefix)
	{
		for (int i = 0; i < this.PowerUpUINumList.get_Count(); i++)
		{
			this.PowerUpUINumList.get_Item(i).get_gameObject().SetActive(false);
		}
		int num = 0;
		string text = thisValue.ToString();
		string text2 = text;
		for (int j = 0; j < text2.get_Length(); j++)
		{
			char c = text2.get_Chars(j);
			this.PowerUpUINumList.get_Item(num).get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.PowerUpUINumList.get_Item(num), ResourceManager.GetIconSprite(prefix + c));
			num++;
		}
	}

	protected void CheckOpenEnd()
	{
		if (this.IsRollingNumber || this.IsShowingOpenFx)
		{
			return;
		}
		this.numTimer = TimerHeap.AddTimer(this.finishInterval, 0, delegate
		{
			this.DoVerticalClose();
			this.fx_uid_close = FXSpineManager.Instance.PlaySpine(this.fx_templateId_close, this.PowerUpUIEffect, "PowerUpUI", 15001, delegate
			{
				this.IsShowingOpenFx = false;
				this.PowerUpUIThing.SetActive(false);
				UIManagerControl.Instance.HideUI("PowerUpUI");
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		});
	}

	protected void DoVerticalOpen()
	{
		UIUtils.animPlay(this.PowerUpUIThing, "UI2VerticalOpen");
	}

	protected void DoVerticalClose()
	{
		UIUtils.animPlay(this.PowerUpUIThing, "UI2VerticalClose");
	}
}
