using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadarTransferItem : BaseUIBehaviour
{
	public int mTransferID;

	private Image m_spBackground;

	private Text m_lblName;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_spBackground = base.FindTransform("Background").GetComponent<Image>();
		this.m_lblName = base.FindTransform("Name").GetComponent<Text>();
		base.get_gameObject().GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	private void OnButtonClick()
	{
		RadarMapUIView.Instance.OnTeleportClick(this.mTransferID, this);
		this.SetIsSelected(true);
	}

	public void SetName(string name)
	{
		this.m_lblName.set_text(name);
	}

	public void SetIsSelected(bool isSelected)
	{
		if (isSelected)
		{
			ResourceManager.SetSprite(this.m_spBackground, ResourceManager.GetIconSprite("dt_button_1"));
			this.m_lblName.set_fontSize(26);
			this.m_lblName.set_color(Color.get_white());
		}
		else
		{
			ResourceManager.SetSprite(this.m_spBackground, ResourceManager.GetIconSprite("dt_button"));
			this.m_lblName.set_fontSize(24);
			this.m_lblName.set_color(new Color32(244, 231, 205, 255));
		}
	}
}
