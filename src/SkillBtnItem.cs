using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnItem : BaseUIBehaviour
{
	private Image m_skillIcon;

	private GameObject m_slotNumBgObj;

	private GameObject m_goFrameMaskObj;

	private GameObject m_selectedImgObj;

	private GameObject m_notHaveEmbedObj;

	private GameObject m_skillCanUpTipObj;

	private GameObject m_skillLvBgObj;

	private Transform m_fxTrans;

	private Text m_skillLockText;

	private Text m_skillLvText;

	private Text m_skillUnSelectText;

	private Image m_slotNumBgImg;

	private bool selected;

	private bool isEmbed;

	private int skillID = -1;

	private int curretSkillLv = 1;

	private JiNengJieSuoBiao skillUnlockCfgData;

	private bool isInit;

	private int m_fxID;

	private int skillUpFxID;

	private int skillUnlockFxID;

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

	public JiNengJieSuoBiao SkillUnLockCfgData
	{
		get
		{
			return this.skillUnlockCfgData;
		}
		set
		{
			this.skillUnlockCfgData = value;
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
		this.m_slotNumBgObj = base.FindTransform("SlotBg").get_gameObject();
		this.m_notHaveEmbedObj = base.FindTransform("NotHaveNotchImg").get_gameObject();
		this.m_skillCanUpTipObj = base.FindTransform("SkillCanUpRedPointTip").get_gameObject();
		this.m_skillLvBgObj = base.FindTransform("SkillLvBg").get_gameObject();
		this.m_fxTrans = base.FindTransform("FXTrans");
		this.m_skillLvText = base.FindTransform("SkillLVText").GetComponent<Text>();
		this.m_skillLockText = base.FindTransform("SkillLockText").GetComponent<Text>();
		this.m_skillUnSelectText = base.FindTransform("SkillLVUnSelectText").GetComponent<Text>();
		this.m_slotNumBgImg = base.FindTransform("SlotNumImg").GetComponent<Image>();
		this.m_selectedImgObj = base.FindTransform("SelectImg").get_gameObject();
		this.m_goFrameMaskObj.SetActive(false);
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.OnSkillUpgradeRes, new Callback<int>(this.OnSkillUpgradeRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.OnSkillUpgradeRes, new Callback<int>(this.OnSkillUpgradeRes));
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		FXSpineManager.Instance.DeleteSpine(this.skillUpFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		FXSpineManager.Instance.DeleteSpine(this.skillUnlockFxID, true);
	}

	public void UpdateSkillItem(JiNengJieSuoBiao skillUnlockCfg)
	{
		this.skillUnlockCfgData = skillUnlockCfg;
		if (skillUnlockCfg == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.skillID = skillUnlockCfg.skillId;
		Skill skill = DataReader<Skill>.Get(this.skillID);
		if (skill != null)
		{
			ResourceManager.SetSprite(this.m_skillIcon, GameDataUtils.GetIcon(skill.icon));
		}
		bool flag = SkillUIManager.Instance.CheckSkillIsUnLock(this.skillID);
		this.m_goFrameMaskObj.SetActive(!flag);
		this.m_skillLockText.set_text((!flag) ? "未解锁" : string.Empty);
		this.m_skillLvBgObj.SetActive(flag);
		this.isEmbed = SkillUIManager.Instance.CheckSkillIsEmbedNotch(this.skillID);
		this.m_slotNumBgObj.SetActive(this.isEmbed);
		if (this.isEmbed)
		{
			int skillNotchNumber = SkillUIManager.Instance.GetSkillNotchNumber(this.skillID);
			string spriteName = "fight_combofont_" + skillNotchNumber;
			ResourceManager.SetIconSprite(this.m_slotNumBgImg, spriteName);
		}
		this.m_notHaveEmbedObj.SetActive(!this.isEmbed);
		this.curretSkillLv = SkillUIManager.Instance.GetSkillLvByID(this.skillID);
		this.m_skillLvText.set_text("Lv" + this.curretSkillLv);
		this.m_skillUnSelectText.set_text(string.Empty);
		bool active = SkillUIManager.Instance.CheckSkillUpgradeCanShowRedPointTip(this.skillID);
		this.m_skillCanUpTipObj.SetActive(active);
		this.Selected = false;
	}

	private void SetSelectBtnState(bool isSelect)
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		this.m_selectedImgObj.SetActive(isSelect);
		if (isSelect)
		{
			this.m_fxID = FXSpineManager.Instance.PlaySpine(115, this.m_fxTrans, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		}
		this.m_skillLvText.set_text((!isSelect) ? string.Empty : ("Lv" + this.curretSkillLv));
		this.m_skillUnSelectText.set_text(isSelect ? string.Empty : ("Lv" + this.curretSkillLv));
		this.m_notHaveEmbedObj.SetActive(!this.isEmbed && !isSelect);
	}

	private void OnSkillUpgradeRes(int upSkillID)
	{
		FXSpineManager.Instance.DeleteSpine(this.skillUpFxID, true);
		if (this.skillID == upSkillID)
		{
			this.skillUpFxID = FXSpineManager.Instance.PlaySpine(3023, this.m_fxTrans, "SkillUI", 2002, null, "UI", 0f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		}
	}

	public void PlayUnlockFxID(int unlockSkillId)
	{
		FXSpineManager.Instance.DeleteSpine(this.skillUnlockFxID, true);
		if (this.skillID == unlockSkillId)
		{
			this.skillUpFxID = FXSpineManager.Instance.PlaySpine(3027, this.m_fxTrans, "SkillUI", 2002, delegate
			{
				if (SkillUIManager.Instance.NewOpenSkillIDs != null)
				{
					int num = SkillUIManager.Instance.NewOpenSkillIDs.FindIndex((int a) => a == this.skillID);
					if (num >= 0)
					{
						SkillUIManager.Instance.NewOpenSkillIDs.RemoveAt(num);
					}
				}
			}, "UI", 0f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		}
	}
}
