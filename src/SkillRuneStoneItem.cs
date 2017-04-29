using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillRuneStoneItem : BaseUIBehaviour
{
	private Image iconImg;

	private GameObject lockImgObj;

	private GameObject selectImgObj;

	private GameObject unLockImgObj;

	private Text runeStoneNameText;

	private Transform fxTrans;

	private bool isInit;

	public Runes_basic runeStoneCfgData;

	public bool IsUnLock;

	protected bool selected;

	private int fxID;

	private int runeEmbedFxID;

	private int runeStoneUnlockFxID;

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
		this.iconImg = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.lockImgObj = base.FindTransform("FrameMask").get_gameObject();
		this.selectImgObj = base.FindTransform("ImageFrameSelect").get_gameObject();
		this.unLockImgObj = base.FindTransform("ImageFrameUnlock").get_gameObject();
		this.runeStoneNameText = base.FindTransform("RuneStoneNameText").GetComponent<Text>();
		this.fxTrans = base.FindTransform("FX");
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		FXSpineManager.Instance.DeleteSpine(this.runeEmbedFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.runeStoneUnlockFxID, true);
	}

	public void UpdateUI(Runes_basic runeStoneData, int skillID)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.runeStoneCfgData = runeStoneData;
		if (this.runeStoneCfgData == null)
		{
			return;
		}
		this.IsUnLock = SkillRuneManager.Instance.CheckRuneStoneIsUnLock(this.runeStoneCfgData.id);
		this.lockImgObj.SetActive(!this.IsUnLock);
		this.unLockImgObj.SetActive(this.IsUnLock);
		if (this.iconImg != null)
		{
			ResourceManager.SetSprite(this.iconImg, GameDataUtils.GetIcon(runeStoneData.icon));
			int skillEmbedGroupIndex = SkillRuneManager.Instance.GetSkillEmbedGroupIndex(skillID);
			this.iconImg.set_color(new Color(this.iconImg.get_color().r, this.iconImg.get_color().b, this.iconImg.get_color().g, 1f));
			if (this.IsUnLock && skillEmbedGroupIndex != this.runeStoneCfgData.runesGroup)
			{
				this.iconImg.set_color(new Color(this.iconImg.get_color().r, this.iconImg.get_color().b, this.iconImg.get_color().g, 1f));
				ImageColorMgr.SetImageColor(this.iconImg, true);
			}
			else if (this.IsUnLock && skillEmbedGroupIndex == this.runeStoneCfgData.runesGroup)
			{
				this.iconImg.set_color(new Color(this.iconImg.get_color().r, this.iconImg.get_color().b, this.iconImg.get_color().g, 1f));
				ImageColorMgr.SetImageColor(this.iconImg, false);
			}
			else if (!this.IsUnLock)
			{
				ImageColorMgr.SetImageColor(this.iconImg, false);
				this.iconImg.set_color(new Color(this.iconImg.get_color().r, this.iconImg.get_color().b, this.iconImg.get_color().g, 0.5f));
			}
		}
		this.runeStoneNameText.set_text(string.Empty);
	}

	private void SetSelectBtnState(bool isSelect)
	{
		this.selectImgObj.SetActive(isSelect);
	}

	private void OnRuneStoneEmbedRes(int skillID)
	{
		FXSpineManager.Instance.DeleteSpine(this.runeEmbedFxID, true);
		int skillEmbedGroupIndex = SkillRuneManager.Instance.GetSkillEmbedGroupIndex(skillID);
		if (this.runeStoneCfgData != null && this.runeStoneCfgData.skillId == skillID && skillEmbedGroupIndex == this.runeStoneCfgData.runesGroup)
		{
			this.runeEmbedFxID = FXSpineManager.Instance.PlaySpine(3026, this.fxTrans, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public void PlayUnlockRuneStoneFX(int runeStoneID)
	{
		FXSpineManager.Instance.DeleteSpine(this.runeStoneUnlockFxID, true);
		if (this.runeStoneCfgData != null && this.runeStoneCfgData.id == runeStoneID)
		{
			this.runeStoneUnlockFxID = FXSpineManager.Instance.PlaySpine(3028, this.fxTrans, "SkillRuneStoneItem", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}
}
