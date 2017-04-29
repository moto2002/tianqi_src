using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DarkTrialUIChapterUnit : BaseUIBehaviour
{
	protected Image DarkTrialUIChapterUnitImage;

	protected Text DarkTrialUIChapterUnitText;

	protected int i = -1;

	protected Action<int> clickAction;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.DarkTrialUIChapterUnitImage = base.FindTransform("DarkTrialUIChapterUnitImage").GetComponent<Image>();
		this.DarkTrialUIChapterUnitText = base.FindTransform("DarkTrialUIChapterUnitText").GetComponent<Text>();
		this.DarkTrialUIChapterUnitImage.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChapterBtn);
	}

	public void SetData(int index, int name, Action<int> clickCallback)
	{
		this.i = index;
		this.clickAction = clickCallback;
		this.SetName(GameDataUtils.GetChineseContent(name, false));
	}

	protected void SetName(string text)
	{
		this.DarkTrialUIChapterUnitText.set_text(text);
	}

	public void SetClickState(bool isClick)
	{
		ResourceManager.SetSprite(this.DarkTrialUIChapterUnitImage, ResourceManager.GetIconSprite((!isClick) ? "y_fenye2" : "y_fenye1"));
	}

	protected void OnClickChapterBtn(GameObject go)
	{
		if (this.clickAction != null)
		{
			this.clickAction.Invoke(this.i);
		}
	}
}
