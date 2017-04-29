using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodWeaponUI : UIBase
{
	private const int ITEM_COUNT = 5;

	private ButtonCustom mBtnLeftArrow;

	private ButtonCustom mBtnRightArrow;

	private Image mImgTitle;

	private int mPage;

	private float mPageWitch;

	private List<GodWeaponItem> mItemList;

	private GameObject mGoListPanel;

	private RectTransform mListGrid;

	private ScrollRectCustom mScrollRect;

	private int mLastIndex = 2;

	private GodWeaponItem mTempItem;

	private uint mDelayScrollId;

	private uint mDelayEffectId;

	private readonly string[] TITLE_SPR = new string[]
	{
		string.Empty,
		"zi_juexing",
		"zi_jinjie",
		"zi_shengwu",
		"zi_fushi"
	};

	private GameObject mGoDescPanel;

	private Image mImgGodWeapon;

	private RawImage mRawGodWeapon;

	private GameObject mGoModel;

	private GameObject mGoCamera;

	private Text mTxModelName;

	private Image mSkillIcon;

	private Text mTxSkillName;

	private Text mTxWeaponName;

	private Text mTxWeaponDesc;

	private Text mTxSkillTitle;

	private Text mTxSkillDesc;

	private Text mTxGetDesc;

	private ButtonCustom mBtnChallenge;

	private HolyWeaponInfo mInfo;

	private Artifact mData;

	private int mIndex;

	private List<HolyWeaponInfo> mCurList;

	private int mModelId;

	private int mFxDescId;

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110045), string.Empty, delegate
		{
			if (this.mGoDescPanel.get_activeSelf())
			{
				this.CloseDescPanel();
			}
			else
			{
				this.Show(false);
				SoundManager.SetBGMFade(true);
			}
		}, false);
		if (GodWeaponManager.Instance.OpenDescId > 0)
		{
			HolyWeaponInfo info = GodWeaponManager.Instance.WeaponList.Find((HolyWeaponInfo e) => e.Id == GodWeaponManager.Instance.OpenDescId);
			if (info != null)
			{
				this.SwitchGodType(info.Type);
				GodWeaponItem godWeaponItem = this.mItemList.Find((GodWeaponItem e) => e.Info.Id == info.Id);
				if (godWeaponItem != null)
				{
					this.OpenDescPanel(godWeaponItem.Index, godWeaponItem.Info);
				}
			}
			GodWeaponManager.Instance.OpenDescId = 0;
		}
		else if (GodWeaponManager.Instance.UIPlayQueue != null && GodWeaponManager.Instance.UIPlayQueue.get_Count() > 0)
		{
			this.PlayEffect();
		}
		else
		{
			this.SwitchGodType(1);
			this.mGoDescPanel.SetActive(false);
			this.mGoListPanel.SetActive(true);
		}
		WaitUI.CloseUI(0u);
	}

	protected override void OnDisable()
	{
		TimerHeap.DelTimer(this.mDelayScrollId);
		TimerHeap.DelTimer(this.mDelayEffectId);
		base.OnDisable();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mBtnLeftArrow = base.FindTransform("btnLeftArrow").GetComponent<ButtonCustom>();
		this.mBtnRightArrow = base.FindTransform("btnRightArrow").GetComponent<ButtonCustom>();
		this.mBtnLeftArrow.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLeftArrow);
		this.mBtnRightArrow.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRightArrow);
		this.mImgTitle = base.FindTransform("imgType").GetComponent<Image>();
		this.mItemList = new List<GodWeaponItem>();
		this.mGoListPanel = base.FindTransform("ListPanel").get_gameObject();
		this.mListGrid = base.FindTransform("Grid").GetComponent<RectTransform>();
		this.mPageWitch = this.mListGrid.GetComponent<GridLayoutGroup>().get_cellSize().x * 5f;
		this.mScrollRect = base.FindTransform("Content").GetComponent<ScrollRectCustom>();
		base.FindTransform("Toggle1").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				this.SwitchGodType(1);
			}
		});
		base.FindTransform("Toggle2").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				this.SwitchGodType(2);
			}
		});
		base.FindTransform("Toggle3").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				this.SwitchGodType(3);
			}
		});
		base.FindTransform("Toggle4").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				this.SwitchGodType(4);
			}
		});
		this.mGoDescPanel = base.FindTransform("DescPanel").get_gameObject();
		this.mImgGodWeapon = base.FindTransform("WeaponImage").GetComponent<Image>();
		this.mRawGodWeapon = base.FindTransform("WeaponModel").GetComponent<RawImage>();
		this.mRawGodWeapon.set_material(RTManager.Instance.RTNoAlphaMat);
		this.mTxModelName = base.FindTransform("txModelName").GetComponent<Text>();
		this.mSkillIcon = base.FindTransform("Icon").GetComponent<Image>();
		this.mTxSkillName = base.FindTransform("txSkillName").GetComponent<Text>();
		this.mTxWeaponName = base.FindTransform("txWeaponName").GetComponent<Text>();
		this.mTxWeaponDesc = base.FindTransform("txWeaponDesc").GetComponent<Text>();
		this.mTxSkillTitle = base.FindTransform("txSkillTitle").GetComponent<Text>();
		this.mTxSkillDesc = base.FindTransform("txSkillDesc").GetComponent<Text>();
		this.mTxGetDesc = base.FindTransform("txGetDesc").GetComponent<Text>();
		this.mBtnChallenge = base.FindTransform("btnChallenge").GetComponent<ButtonCustom>();
		this.mBtnChallenge.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChallenge);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener<HolyWeaponInfo>(EventNames.GotGodWeaponNty, new Callback<HolyWeaponInfo>(this.FirstRefreshUI));
		EventDispatcher.AddListener(EventNames.PlayGodWeaponUIEffect, new Callback(this.PlayEffect));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener<HolyWeaponInfo>(EventNames.GotGodWeaponNty, new Callback<HolyWeaponInfo>(this.FirstRefreshUI));
		EventDispatcher.RemoveListener(EventNames.PlayGodWeaponUIEffect, new Callback(this.PlayEffect));
	}

	private void SwitchGodType(int type)
	{
		List<HolyWeaponInfo> list = null;
		if (GodWeaponManager.Instance.WeaponDict == null)
		{
			GodWeaponManager.Instance.SendGetGodWeaponInfos();
		}
		else if (GodWeaponManager.Instance.WeaponDict.TryGetValue(type, ref list))
		{
			this.ClearWeaponItem();
			if (type != 4)
			{
				list = this.SortWeaponList(list);
			}
			for (int i = 0; i < list.get_Count(); i++)
			{
				this.CreateWeaponItem(i, list.get_Item(i));
			}
			ResourceManager.SetSprite(this.mImgTitle, ResourceManager.GetIconSprite(this.TITLE_SPR[type]));
			base.FindTransform("Toggle" + type).GetComponent<Toggle>().set_isOn(true);
			this.mPage = (int)(this.mListGrid.get_sizeDelta().x / this.mPageWitch);
		}
	}

	private List<HolyWeaponInfo> SortWeaponList(List<HolyWeaponInfo> list)
	{
		List<HolyWeaponInfo> list2 = new List<HolyWeaponInfo>();
		for (int i = 0; i < list.get_Count(); i++)
		{
			if (list.get_Item(i).State == 2)
			{
				list2.Add(list.get_Item(i));
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (list.get_Item(j).State == 1 || list.get_Item(j).State == 3)
			{
				list2.Add(list.get_Item(j));
			}
		}
		for (int k = 0; k < list.get_Count(); k++)
		{
			if (list.get_Item(k).State == 0)
			{
				list2.Add(list.get_Item(k));
			}
		}
		return list2;
	}

	private void CreateWeaponItem(int index, HolyWeaponInfo data)
	{
		GodWeaponItem godWeaponItem = this.mItemList.Find((GodWeaponItem e) => e.get_gameObject().get_name() == "Unused");
		if (godWeaponItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GodWeaponItem");
			UGUITools.SetParent(this.mListGrid.get_gameObject(), instantiate2Prefab, false);
			godWeaponItem = instantiate2Prefab.GetComponent<GodWeaponItem>();
			godWeaponItem.EventHandler = new Action<int, HolyWeaponInfo>(this.OpenDescPanel);
			this.mItemList.Add(godWeaponItem);
		}
		godWeaponItem.SetData(index, data);
		godWeaponItem.get_gameObject().set_name(data.Id.ToString());
		godWeaponItem.get_gameObject().SetActive(true);
	}

	private void ClearWeaponItem()
	{
		for (int i = 0; i < this.mItemList.get_Count(); i++)
		{
			this.mItemList.get_Item(i).Unused();
		}
	}

	private void RefreshDescPanel(HolyWeaponInfo info)
	{
		if (info != null)
		{
			this.mInfo = info;
			this.mData = DataReader<Artifact>.Get(info.Id);
			GodWeaponManager.Instance.WeaponDict.TryGetValue(this.mInfo.Type, ref this.mCurList);
			if (this.mData != null)
			{
				ResourceManager.SetSprite(this.mImgTitle, ResourceManager.GetIconSprite(this.TITLE_SPR[this.mInfo.Type]));
				if (this.mData.model > 0)
				{
					this.mImgGodWeapon.get_gameObject().SetActive(true);
					this.mRawGodWeapon.get_gameObject().SetActive(false);
					ResourceManager.SetSprite(this.mImgGodWeapon, GameDataUtils.GetIcon(this.mData.model));
					this.PlaySpine(this.mData.system);
				}
				this.mTxModelName.set_text(GameDataUtils.GetChineseContent(this.mData.name, false));
				this.mTxWeaponName.set_text(GameDataUtils.GetChineseContent(this.mData.name, false));
				this.mTxWeaponDesc.set_text(GameDataUtils.GetChineseContent(this.mData.explain, false));
				this.mBtnChallenge.get_gameObject().SetActive(this.mData.acquisitionMode == 1 && this.mInfo.State == 3);
				this.RefreshBottomPanel(this.mData);
			}
		}
		this.mBtnLeftArrow.get_gameObject().SetActive(this.mIndex > 0);
		this.mBtnRightArrow.get_gameObject().SetActive(this.mIndex < this.mCurList.get_Count() - 1);
	}

	private void RefreshBottomPanel(Artifact data)
	{
		Artifact.SystemparameterPair systemparameterPair = data.systemParameter.Find((Artifact.SystemparameterPair e) => e.key == EntityWorld.Instance.EntSelf.TypeID);
		if (systemparameterPair != null || data.system == 5)
		{
			base.FindTransform("SkillPanel").get_gameObject().SetActive(true);
			switch (data.system)
			{
			case 1:
				this.RefreshSkillDesc(systemparameterPair.value, data);
				break;
			case 2:
				this.RefreshRuneDesc(systemparameterPair.value, data);
				break;
			case 3:
				this.RefreshSystemDesc(systemparameterPair.value, data);
				break;
			case 4:
				this.RefreshWeaponSkillDesc(systemparameterPair.value, data);
				break;
			case 5:
				this.RefreshBuffDesc(data);
				break;
			default:
				base.FindTransform("SkillPanel").get_gameObject().SetActive(false);
				break;
			}
		}
		else
		{
			base.FindTransform("SkillPanel").get_gameObject().SetActive(false);
		}
	}

	private void RefreshSkillDesc(int id, Artifact godData)
	{
		Skill skill = DataReader<Skill>.Get(id);
		if (skill != null)
		{
			ResourceManager.SetSprite(this.mSkillIcon, GameDataUtils.GetIcon(skill.icon));
			this.mTxSkillName.set_text(GameDataUtils.GetChineseContent(skill.name, false));
			this.mTxSkillTitle.set_text("技能描述:");
			this.mTxSkillDesc.set_text(GameDataUtils.GetChineseContent(skill.describeId, false));
			string text = GameDataUtils.GetChineseContent(godData.access, false);
			if (godData.activation == 1)
			{
				RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(godData.activationParameter);
				if (renWuPeiZhi != null)
				{
					text = string.Format(text, GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false), renWuPeiZhi.lv + "级");
				}
			}
			this.mTxGetDesc.set_text(text);
		}
	}

	private void RefreshRuneDesc(int id, Artifact godData)
	{
		Runes runes = DataReader<Runes>.Get(id);
		Runes_basic runes_basic = DataReader<Runes_basic>.Get(id);
		if (runes_basic != null)
		{
			ResourceManager.SetSprite(this.mSkillIcon, GameDataUtils.GetIcon(runes_basic.icon));
			this.mTxSkillName.set_text(GameDataUtils.GetChineseContent(runes_basic.name, false));
			this.mTxSkillTitle.set_text("符文描述:");
			this.mTxSkillDesc.set_text(GameDataUtils.GetChineseContent(runes.desc, false));
			string text = GameDataUtils.GetChineseContent(godData.access, false);
			if (godData.activation == 1)
			{
				RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(godData.activationParameter);
				if (renWuPeiZhi != null)
				{
					text = string.Format(text, GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false), renWuPeiZhi.lv + "级");
				}
			}
			else if (godData.activation == 2)
			{
				text = string.Format(text, godData.activationParameter);
			}
			else if (godData.activation == 4)
			{
				Artifact artifact = DataReader<Artifact>.Get(godData.activationParameter);
				if (artifact != null)
				{
					text = string.Format(text, GameDataUtils.GetChineseContent(artifact.name, false), godData.lv);
				}
			}
			this.mTxGetDesc.set_text(text);
		}
	}

	private void RefreshSystemDesc(int id, Artifact godData)
	{
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(id);
		if (systemOpen != null)
		{
			ResourceManager.SetSprite(this.mSkillIcon, GameDataUtils.GetIcon(systemOpen.icon));
			this.mTxSkillName.set_text(GameDataUtils.GetChineseContent(systemOpen.name, false));
			this.mTxSkillTitle.set_text("系统描述:");
			this.mTxSkillDesc.set_text(GameDataUtils.GetChineseContent(systemOpen.bewrite, false));
			string text = string.Empty;
			if (systemOpen.taskId > 0)
			{
				RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(systemOpen.taskId);
				if (renWuPeiZhi != null)
				{
					text = string.Format(GameDataUtils.GetChineseContent(godData.access, false), GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false));
				}
			}
			else if (EntityWorld.Instance.EntSelf.Lv < systemOpen.level)
			{
				text = string.Format("达到等级{0}", systemOpen.level);
			}
			this.mTxGetDesc.set_text(text);
		}
	}

	private void RefreshWeaponSkillDesc(int skillId, Artifact godData)
	{
		ArtifactSkill artifactSkill = DataReader<ArtifactSkill>.Get(skillId);
		if (artifactSkill != null)
		{
			ResourceManager.SetSprite(this.mSkillIcon, GameDataUtils.GetIcon(artifactSkill.icon));
			this.mTxSkillName.set_text(GameDataUtils.GetChineseContent(artifactSkill.name, false));
			this.mTxSkillTitle.set_text("技能描述:");
			this.mTxSkillDesc.set_text(GameDataUtils.GetChineseContent(artifactSkill.desc, false));
			this.mTxGetDesc.set_text(string.Format(GameDataUtils.GetChineseContent(godData.access, false), godData.activationParameter));
		}
	}

	private void RefreshBuffDesc(Artifact godData)
	{
		if (godData != null)
		{
			ResourceManager.SetSprite(this.mSkillIcon, GameDataUtils.GetIcon(godData.icon));
			this.mTxSkillName.set_text(GameDataUtils.GetChineseContent(godData.skillName, false));
			this.mTxSkillTitle.set_text("BUFF描述:");
			this.mTxSkillDesc.set_text(GameDataUtils.GetChineseContent(godData.skillExplain, false));
			this.mTxGetDesc.set_text(GameDataUtils.GetChineseContent(godData.access, false));
		}
	}

	private void SetModel(int modelId)
	{
		this.mModelId = modelId;
		ModelDisplayManager.SetRawImage(this.mRawGodWeapon, modelId, new Vector2(1000f, ModelDisplayManager.OFFSET_TO_PETUI.y), ref this.mGoModel, ref this.mGoCamera);
	}

	private void Update()
	{
		if (this.mGoModel != null)
		{
			Transform expr_1C = this.mGoModel.get_transform();
			expr_1C.set_rotation(expr_1C.get_rotation() * Quaternion.AngleAxis(2f, Vector3.get_up()));
		}
	}

	private void ClearModel()
	{
		if (this.mGoModel != null)
		{
			Object.Destroy(this.mGoModel);
		}
		if (this.mGoCamera != null)
		{
			Object.Destroy(this.mGoCamera);
		}
		this.mGoModel = null;
		this.mGoCamera = null;
	}

	private void OpenDescPanel(int index, HolyWeaponInfo info)
	{
		this.mIndex = index;
		this.mGoListPanel.SetActive(false);
		this.mGoDescPanel.SetActive(true);
		this.RefreshDescPanel(info);
	}

	private void CloseDescPanel()
	{
		this.ClearModel();
		this.mGoDescPanel.SetActive(false);
		this.mGoListPanel.SetActive(true);
		this.mBtnLeftArrow.get_gameObject().SetActive(true);
		this.mBtnRightArrow.get_gameObject().SetActive(true);
		if (GodWeaponManager.Instance.UIPlayQueue != null && GodWeaponManager.Instance.UIPlayQueue.get_Count() > 0)
		{
			this.PlayEffect();
		}
	}

	private void PlaySpine(int type)
	{
		int templateId = 0;
		switch (type)
		{
		case 1:
		case 4:
			templateId = 3903;
			break;
		case 2:
			templateId = 3905;
			break;
		case 3:
			templateId = 3904;
			break;
		case 5:
			templateId = 3906;
			break;
		}
		if (this.mFxDescId > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.mFxDescId, true);
		}
		this.mFxDescId = FXSpineManager.Instance.PlaySpine(templateId, this.mImgGodWeapon.get_transform(), "GodWeaponUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void OnClickLeftArrow(GameObject go)
	{
		if (this.mGoDescPanel.get_activeSelf())
		{
			if (this.mCurList != null && this.mIndex - 1 >= 0)
			{
				this.mIndex--;
				this.RefreshDescPanel(this.mCurList.get_Item(this.mIndex));
			}
		}
		else
		{
			int num = Mathf.RoundToInt(-this.mListGrid.get_anchoredPosition().x / this.mPageWitch);
			if (num > 0)
			{
				this.mScrollRect.Move2Index((num - 1) * 5, false);
			}
			else
			{
				this.mScrollRect.Move2First(false);
			}
		}
	}

	private void OnClickRightArrow(GameObject go)
	{
		if (this.mGoDescPanel.get_activeSelf())
		{
			if (this.mIndex + 1 < this.mCurList.get_Count())
			{
				this.mIndex++;
				this.RefreshDescPanel(this.mCurList.get_Item(this.mIndex));
			}
		}
		else
		{
			this.mPage = (int)(this.mListGrid.get_sizeDelta().x / this.mPageWitch);
			int num = Mathf.RoundToInt(-this.mListGrid.get_anchoredPosition().x / this.mPageWitch);
			if (num + 1 < this.mPage)
			{
				this.mScrollRect.Move2Index((num + 1) * 5, false);
			}
			else
			{
				this.mScrollRect.Move2Last(false);
			}
		}
	}

	private void FirstRefreshUI(HolyWeaponInfo info)
	{
		if (info != null)
		{
			this.SwitchGodType(info.Type);
			this.mGoDescPanel.SetActive(false);
			this.mGoListPanel.SetActive(true);
		}
	}

	private void OnClickChallenge(GameObject go)
	{
		if (this.mData.acquisitionMode == 1)
		{
			if (!GodWeaponManager.Instance.CheckEquipLevelAndQuality(this.mData.id))
			{
				GodWeaponChallengeUI godWeaponChallengeUI = UIManagerControl.Instance.OpenUI("GodWeaponChallengeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GodWeaponChallengeUI;
				godWeaponChallengeUI.Open(this.mData);
			}
			else if (this.mData.battle > 0)
			{
				GodWeaponManager.Instance.ChallengeDungeon(this.mData.battle, this.mData.model);
			}
		}
	}

	private void PlayEffect()
	{
		if (!this.mGoDescPanel.get_activeSelf())
		{
			int id = GodWeaponManager.Instance.UIPlayQueue.Dequeue();
			HolyWeaponInfo info = GodWeaponManager.Instance.WeaponList.Find((HolyWeaponInfo e) => e.Id == id);
			if (info != null)
			{
				this.SwitchGodType(info.Type);
				this.mGoListPanel.SetActive(true);
				this.mGoDescPanel.SetActive(false);
				this.mTempItem = this.mItemList.Find((GodWeaponItem e) => e.Info.Id == info.Id);
				if (this.mTempItem != null)
				{
					int num = this.mTempItem.Index - 2;
					if (num < 0)
					{
						num = 0;
					}
					this.mScrollRect.Move2Index(num, false);
					if (Mathf.Abs(this.mLastIndex - num) > 2)
					{
						this.mDelayScrollId = TimerHeap.AddTimer(750u, 0, new Action(this.DelayPlayEffect));
					}
					else
					{
						this.DelayPlayEffect();
					}
					this.mLastIndex = num;
				}
			}
		}
	}

	private void DelayPlayEffect()
	{
		FXSpineManager.Instance.PlaySpine(3710, this.mTempItem.get_transform(), "GodWeaponUI", 2000, null, "UI", 0f, 35f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		if (GodWeaponManager.Instance.UIPlayQueue.get_Count() > 0)
		{
			this.mDelayEffectId = TimerHeap.AddTimer(1500u, 0, new Action(this.PlayEffect));
		}
	}

	private void FixedRT()
	{
		if (this.mGoCamera == null)
		{
			return;
		}
		Camera component = this.mGoCamera.GetComponent<Camera>();
		if (component == null || component.get_targetTexture() != null)
		{
			return;
		}
		Object.Destroy(this.mGoCamera);
		this.mGoCamera = null;
		this.SetModel(this.mModelId);
	}
}
