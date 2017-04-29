using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class FindTipsUI : UIBase
{
	public Text txTitle;

	public Text txContent;

	private Text rolesContent;

	private Text valuesContent;

	private Text rankingContent;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.rolesContent = base.FindTransform("rolesContent").GetComponent<Text>();
		this.valuesContent = base.FindTransform("valuesCotnent").GetComponent<Text>();
		this.rankingContent = base.FindTransform("rankingContent").GetComponent<Text>();
		if (this.rolesContent != null)
		{
			this.rolesContent.set_text(string.Empty);
		}
		if (this.valuesContent != null)
		{
			this.valuesContent.set_text(string.Empty);
		}
		if (this.rankingContent != null)
		{
			this.rankingContent.set_text(string.Empty);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	public void OnOpen(string title, string content)
	{
		this.txTitle.set_text(title);
		this.txContent.set_text(content);
		if (this.rolesContent != null)
		{
			this.rolesContent.set_text(string.Empty);
		}
		if (this.valuesContent != null)
		{
			this.valuesContent.set_text(string.Empty);
		}
		if (this.rankingContent != null)
		{
			this.rankingContent.set_text(string.Empty);
		}
	}

	public void OnSetRolesAndValue(string title, string rankingText, string rolesText, string valuesText)
	{
		this.txTitle.set_text(title);
		if (this.rolesContent != null)
		{
			this.rolesContent.set_text(rolesText);
		}
		if (this.valuesContent != null)
		{
			this.valuesContent.set_text(valuesText);
		}
		if (this.rankingContent != null)
		{
			this.rankingContent.set_text(rankingText);
		}
		if (this.txContent != null)
		{
			this.txContent.set_text(string.Empty);
		}
	}
}
