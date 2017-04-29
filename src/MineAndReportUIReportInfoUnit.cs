using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MineAndReportUIReportInfoUnit : BaseUIBehaviour
{
	protected const float BGWidth = 255f;

	protected const float BGHeightPadding = 10.5f;

	protected RectTransform rectTransform;

	protected LayoutElement layoutElement;

	protected RectTransform MineAndReportUIMessageInfoUnitBG;

	protected Text MineAndReportUIMessageInfoUnitText;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.rectTransform = base.GetComponent<RectTransform>();
		this.layoutElement = base.GetComponent<LayoutElement>();
		this.MineAndReportUIMessageInfoUnitBG = base.FindTransform("MineAndReportUIMessageInfoUnitBG").GetComponent<RectTransform>();
		this.MineAndReportUIMessageInfoUnitText = base.FindTransform("MineAndReportUIMessageInfoUnitText").GetComponent<Text>();
	}

	private void OnEnbale()
	{
		this.layoutElement.set_ignoreLayout(false);
	}

	private void OnDisable()
	{
		this.layoutElement.set_ignoreLayout(true);
	}

	public void SetData(string text)
	{
		this.MineAndReportUIMessageInfoUnitText.set_text(text);
		this.MineAndReportUIMessageInfoUnitText.get_rectTransform().set_sizeDelta(new Vector2(this.MineAndReportUIMessageInfoUnitText.get_rectTransform().get_sizeDelta().x, this.MineAndReportUIMessageInfoUnitText.get_preferredHeight()));
		this.MineAndReportUIMessageInfoUnitBG.set_sizeDelta(new Vector2(255f, this.MineAndReportUIMessageInfoUnitText.get_preferredHeight() + 10.5f));
		this.layoutElement.set_preferredHeight(this.MineAndReportUIMessageInfoUnitBG.get_sizeDelta().y);
		this.layoutElement.set_ignoreLayout(false);
	}
}
