using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceItem : MonoBehaviour
{
	protected Text InfoText;

	protected SystemOpenFailedType failedType;

	protected int failedValue;

	protected int systemId;

	protected bool isModeOpen;

	protected DateTime currentTime;

	protected DateTime openTime;

	protected DateTime closeTime;

	protected int today;

	protected string taskOpenText;

	protected string countDownText;

	protected string[] weekText;

	protected SpecialFightMode mode;

	protected Action<SpecialFightMode> btnCallBack;

	protected StringBuilder sb = new StringBuilder();

	public void Init(SpecialFightMode theMode, Action<SpecialFightMode> theBtnCallBack, string theTaskOpenText, string theCountDownText, string[] theWeekText)
	{
		this.mode = theMode;
		this.btnCallBack = theBtnCallBack;
		this.taskOpenText = theTaskOpenText;
		this.countDownText = theCountDownText;
		this.weekText = theWeekText;
		this.InfoText = base.get_transform().FindChild(base.get_gameObject().get_name() + "Text").GetComponent<Text>();
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnClick);
		this.systemId = SpecialFightManager.GetSystemIDByMode(this.mode);
	}

	public void UpdateItem()
	{
		this.isModeOpen = (this.systemId == 0 || SystemOpenManager.IsSystemOn(this.systemId, out this.failedType, out this.failedValue));
		if (this.isModeOpen)
		{
			this.UpdateTime();
		}
		else
		{
			SystemOpenFailedType systemOpenFailedType = this.failedType;
			if (systemOpenFailedType != SystemOpenFailedType.LevelOpen)
			{
				if (systemOpenFailedType != SystemOpenFailedType.Task)
				{
					this.InfoText.set_text(string.Empty);
				}
				else
				{
					this.InfoText.set_text(GameDataUtils.GetChineseContent(513532, false));
				}
			}
			else
			{
				this.InfoText.set_text(string.Format(GameDataUtils.GetChineseContent(513512, false), this.failedValue));
			}
		}
	}

	protected void UpdateTime()
	{
		SpecialFightModeGroup modeCroup = SpecialFightManager.GetModeCroup(this.mode);
		if (modeCroup != SpecialFightModeGroup.Defend)
		{
			if (modeCroup == SpecialFightModeGroup.Expericence)
			{
				this.InfoText.set_text(this.taskOpenText);
			}
		}
		else
		{
			this.UpdateOpenTime();
		}
	}

	protected void UpdateOpenTime()
	{
	}

	protected void OnBtnClick(GameObject go)
	{
		if (this.btnCallBack != null)
		{
			this.btnCallBack.Invoke(this.mode);
		}
	}
}
