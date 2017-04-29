using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillRuneBtnItem : BaseUIBehaviour
{
	public int skillID;

	protected Image m_skillIcon;

	protected GameObject m_equipedIcon;

	protected GameObject m_goFrameMask;

	protected Transform m_fxTrans;

	protected GameObject m_selectedImg;

	protected Text m_nameTxt;

	protected bool selected;

	private bool isInit;

	private int fxID;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			this.SetSelectBtnState(this.selected);
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_skillIcon = base.FindTransform("Icon").GetComponent<Image>();
		this.m_nameTxt = base.FindTransform("NameDes").GetComponent<Text>();
		this.m_goFrameMask = base.FindTransform("maskbg").get_gameObject();
		this.m_equipedIcon = base.FindTransform("isEquip").get_gameObject();
		this.m_fxTrans = base.FindTransform("FXTrans");
		this.m_selectedImg = base.FindTransform("selectImg").get_gameObject();
		this.m_goFrameMask.SetActive(false);
		this.isInit = true;
	}

	public void UpdateSkillItem(int skillId, bool showName = false)
	{
		this.skillID = skillId;
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (!showName)
		{
			this.m_nameTxt.set_text(string.Empty);
		}
		Skill skill = DataReader<Skill>.Get(this.skillID);
		if (skill != null)
		{
			ResourceManager.SetSprite(this.m_skillIcon, GameDataUtils.GetIcon(skill.icon));
		}
		this.Selected = false;
	}

	private void SetSelectBtnState(bool isSelect)
	{
		FXSpineManager.Instance.DeleteSpine(this.fxID, true);
		if (isSelect)
		{
			this.fxID = FXSpineManager.Instance.PlaySpine(115, this.m_fxTrans, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		base.get_transform().set_localScale(Vector3.get_one() * ((!isSelect) ? 0.75f : 1f));
		this.m_selectedImg.SetActive(isSelect);
	}
}
