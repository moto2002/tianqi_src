using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeCareerSkill : MonoBehaviour
{
	private Text m_lblSkillDesc;

	private string m_desc;

	private int fxId;

	private bool m_selected;

	public ChangeCareerInfo m_info;

	private void Awake()
	{
		base.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	private void OnDisable()
	{
		this.m_selected = false;
		FXSpineManager.Instance.DeleteSpine(this.fxId, true);
	}

	private void OnButtonClick()
	{
		this.SetDesc();
		this.m_info.SetSkillSelected(this);
	}

	public void SetSelectBtnState(bool isSelected)
	{
		if (this.m_selected == isSelected)
		{
			return;
		}
		if (isSelected)
		{
			this.fxId = FXSpineManager.Instance.ReplaySpine(this.fxId, 114, base.get_transform(), "ChangeCareerUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fxId, true);
		}
		this.m_selected = isSelected;
	}

	public void SetDesc(Text label, string desc)
	{
		this.m_lblSkillDesc = label;
		this.m_desc = desc;
	}

	public void SetDesc()
	{
		this.m_lblSkillDesc.set_text(this.m_desc);
	}

	public void SetIcon(SpriteRenderer spr)
	{
		ResourceManager.SetSprite(base.get_transform().FindChild("SkillIcon").GetComponent<Image>(), spr);
	}
}
