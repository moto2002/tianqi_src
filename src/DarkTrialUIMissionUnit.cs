using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DarkTrialUIMissionUnit : BaseUIBehaviour
{
	protected RawImage DarkTrialUIMissionUnitIcon;

	protected GameObject DarkTrialUIMissionUnitIconFGFG;

	protected Text DarkTrialUIMissionUnitNameText;

	protected Text DarkTrialUIMissionUnitLevelText;

	protected int i = -1;

	protected Action<int> clickAction;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.DarkTrialUIMissionUnitIcon = base.FindTransform("DarkTrialUIMissionUnitIcon").GetComponent<RawImage>();
		this.DarkTrialUIMissionUnitIconFGFG = base.FindTransform("DarkTrialUIMissionUnitIconFGFG").get_gameObject();
		this.DarkTrialUIMissionUnitNameText = base.FindTransform("DarkTrialUIMissionUnitNameText").GetComponent<Text>();
		this.DarkTrialUIMissionUnitLevelText = base.FindTransform("DarkTrialUIMissionUnitLevelText").GetComponent<Text>();
		this.DarkTrialUIMissionUnitIcon.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMissionBtn);
	}

	public void SetData(int index, int iconID, int name, int level, Action<int> clickCallback)
	{
		this.i = index;
		this.clickAction = clickCallback;
		this.SetIcon(iconID);
		this.SetName(GameDataUtils.GetChineseContent(name, false));
		this.SetLevel(level);
	}

	protected void SetIcon(int iconID)
	{
		ResourceManager.SetTexture(this.DarkTrialUIMissionUnitIcon, GameDataUtils.GetIconName(iconID));
	}

	protected void SetName(string name)
	{
		this.DarkTrialUIMissionUnitNameText.set_text(name);
	}

	protected void SetLevel(int level)
	{
		this.DarkTrialUIMissionUnitLevelText.set_text(string.Format(GameDataUtils.GetChineseContent(50711, false), level));
	}

	public void SetClickState(bool isClick)
	{
		if (this.DarkTrialUIMissionUnitIconFGFG.get_activeSelf() != isClick)
		{
			this.DarkTrialUIMissionUnitIconFGFG.SetActive(isClick);
		}
	}

	protected void OnClickMissionBtn(GameObject go)
	{
		if (this.clickAction != null)
		{
			this.clickAction.Invoke(this.i);
		}
	}
}
