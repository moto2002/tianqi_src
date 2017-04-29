using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceDescUI : UIBase
{
	private RectTransform m_Contair;

	private TextCustom m_lblDesc;

	private Text m_lblTitle;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_Contair = (base.FindTransform("Contair") as RectTransform);
		this.m_lblDesc = base.FindTransform("Desc").GetComponent<TextCustom>();
		this.m_lblTitle = base.FindTransform("TitleName").GetComponent<Text>();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public static void Open(Transform uiRoot, int titleID, int descID)
	{
		SpecialInstanceDescUI specialInstanceDescUI = UIManagerControl.Instance.OpenUI("SpecialInstanceDescUI", uiRoot, false, UIType.NonPush) as SpecialInstanceDescUI;
		specialInstanceDescUI.SetTitleAndDesc(titleID, descID);
	}

	private void SetTitleAndDesc(int titleID, int descID)
	{
		if (this.m_lblDesc != null)
		{
			this.m_lblDesc.TextId = descID;
			this.m_Contair.set_sizeDelta(new Vector2(0f, this.m_lblDesc.get_preferredHeight()));
		}
		if (this.m_lblTitle != null)
		{
			this.m_lblTitle.set_text(GameDataUtils.GetChineseContent(titleID, false));
		}
	}
}
