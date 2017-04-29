using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillPassiveBtnItem : BaseUIBehaviour
{
	private Image m_skillIcon;

	private GameObject m_goFrameMaskObj;

	private Transform m_scaleRootTrans;

	private Transform m_fxTrans;

	private Text m_skillLvText;

	private bool selected;

	private int skillID = -1;

	private ArtifactSkill artifactSkillCfgData;

	private bool isInit;

	private int m_fxID;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			this.SetSelectPassiveBtnState(this.selected);
		}
	}

	public ArtifactSkill ArtifactSkillData
	{
		get
		{
			return this.artifactSkillCfgData;
		}
		set
		{
			this.artifactSkillCfgData = value;
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
		this.m_goFrameMaskObj = base.FindTransform("Maskbg").get_gameObject();
		this.m_scaleRootTrans = base.FindTransform("ScaleRoot");
		this.m_fxTrans = base.FindTransform("FXTrans");
		this.m_skillLvText = base.FindTransform("SkillLVText").GetComponent<Text>();
		this.m_goFrameMaskObj.SetActive(false);
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdatePassiveSkillItem(ArtifactSkill artifactSkillCfg)
	{
		this.artifactSkillCfgData = artifactSkillCfg;
		if (artifactSkillCfg == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.skillID = artifactSkillCfg.id;
		ResourceManager.SetSprite(this.m_skillIcon, GameDataUtils.GetIcon(artifactSkillCfg.icon));
		bool flag = SkillUIManager.Instance.CheckArtifactSkillIsUnlock(this.skillID);
		this.m_goFrameMaskObj.SetActive(!flag);
		this.m_skillLvText.set_text(GameDataUtils.GetChineseContent(this.artifactSkillCfgData.name, false));
		this.Selected = false;
	}

	private void SetSelectPassiveBtnState(bool isSelect)
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		if (isSelect)
		{
			this.m_skillLvText.set_color(new Color(1f, 0.635294139f, 0f));
			this.m_fxID = FXSpineManager.Instance.PlaySpine(115, this.m_fxTrans, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		}
		else
		{
			this.m_skillLvText.set_color(new Color(0.721568644f, 0.521568656f, 0.345098048f));
		}
	}
}
