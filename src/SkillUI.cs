using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : UIBase
{
	private Transform skillActivityPanel;

	private Transform skillbeActivityPanel;

	private ListPool pool;

	private List<Transform> runeStoneGroupTransList;

	private List<ButtonCustom> skillNotchBtnList;

	private ButtonCustom activeSkillBtn;

	private ButtonCustom beActiveSkillBtn;

	private Text runeStoneNameText;

	private Text skillNameText;

	private Text skillDescText;

	private Text skillBasicDamageText;

	private Text skillCdText;

	private ButtonCustom skillUpgradeBtn;

	private ButtonCustom skillChangeBtn;

	private Text runeStoneDescText;

	private int currentSelectSkillID;

	private int currentSelectRuneStoneID;

	private SkillRuneStoneItem lastSelectRuneStoneItem;

	private SkillBtnItem currentSelectSkillBtnItem;

	private SkillBtnItem lastSelectSkillBtnItem;

	private bool IsOpenSkillChangePanel;

	private Transform runeUpgradePanelTrans;

	private Transform skillUpgradePanelTrans;

	private Transform skillChangePanelTrans;

	private Vector3 skillChangePanelEndPos;

	private Vector3 skillChangePanelStartPos;

	private Dictionary<int, bool> runeStonesUnlockFxPlayed;

	private bool isFirstOperate = true;

	private Transform beActivitySkillGridsTrans;

	private Text beActivitySkillNameText;

	private Text beActivityDownDescText;

	private int currentSelectPassiveSkillID;

	private SkillPassiveBtnItem lastSelectPassiveSkillBtnItem;

	private SkillPassiveBtnItem currentSelectPsssiveSkillBtnItem;

	private int skillNotchChangeTipFxID1;

	private int skillNotchChangeTipFxID2;

	private int skillNotchChangeTipFxID3;

	private int skillUpBtnFXID1;

	private int skillUpBtnFXID2;

	private int skillNotchFxID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.skillActivityPanel = base.FindTransform("ActivitySkillPanel");
		this.skillbeActivityPanel = base.FindTransform("BeActivitySkillPanel");
		this.pool = base.FindTransform("SkillGrids").GetComponent<ListPool>();
		this.pool.SetItem("SkillBtnItem");
		this.skillChangePanelTrans = base.FindTransform("SkillChangePanel");
		this.skillUpgradePanelTrans = base.FindTransform("SkillUpgradePanel");
		this.runeUpgradePanelTrans = base.FindTransform("RuneUpgradePanel");
		this.skillNameText = base.FindTransform("SkillNameText").GetComponent<Text>();
		this.skillDescText = base.FindTransform("SkillDownDesc").GetComponent<Text>();
		this.skillBasicDamageText = base.FindTransform("txBaseDamage").GetComponent<Text>();
		this.skillCdText = base.FindTransform("txCD").GetComponent<Text>();
		this.runeStoneNameText = base.FindTransform("RuneStoneNameText").GetComponent<Text>();
		this.runeStoneDescText = base.FindTransform("RuneStoneDownDesc").GetComponent<Text>();
		this.skillChangeBtn = base.FindTransform("SkillChangeBtn").GetComponent<ButtonCustom>();
		this.skillChangeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeSkill);
		this.skillUpgradeBtn = base.FindTransform("SkillUpgradeBtn").GetComponent<ButtonCustom>();
		this.skillUpgradeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkillUpgrade);
		this.skillNotchBtnList = new List<ButtonCustom>();
		for (int i = 1; i <= 3; i++)
		{
			ButtonCustom component = base.FindTransform("SkillNotch" + i).GetComponent<ButtonCustom>();
			component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkillNotchBtn);
			this.skillNotchBtnList.Add(component);
		}
		this.runeStoneGroupTransList = new List<Transform>();
		for (int j = 1; j <= 4; j++)
		{
			Transform transform = base.FindTransform("RuneStoneGroup" + j);
			if (transform != null)
			{
				this.runeStoneGroupTransList.Add(transform);
			}
		}
		this.activeSkillBtn = base.FindTransform("BtnActiveSkill").GetComponent<ButtonCustom>();
		this.activeSkillBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickActivityOrBeActivityBtn);
		this.beActiveSkillBtn = base.FindTransform("BtnBeActiveSkill").GetComponent<ButtonCustom>();
		this.beActiveSkillBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickActivityOrBeActivityBtn);
		this.beActivitySkillGridsTrans = base.FindTransform("BeActivitySkillGrids");
		this.beActivitySkillNameText = base.FindTransform("BeActivitySkillNameText").GetComponent<Text>();
		this.beActivityDownDescText = base.FindTransform("BeActivityDownDescText").GetComponent<Text>();
		this.skillChangePanelEndPos = new Vector3(330f, -173.5f, 0f);
		this.skillChangePanelStartPos = new Vector3(908f, -173.5f, 0f);
		this.runeStonesUnlockFxPlayed = new Dictionary<int, bool>();
		base.FindTransform("NoRuneStoneTipText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(511505, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.isFirstOperate = true;
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110028), string.Empty, new Action(this.OnClickExit), false);
		if (SkillRuneManager.Instance.NewOpenRuneStones != null && SkillRuneManager.Instance.NewOpenRuneStones.get_Count() > 0)
		{
			using (Dictionary<int, List<int>>.Enumerator enumerator = SkillRuneManager.Instance.NewOpenRuneStones.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, List<int>> current = enumerator.get_Current();
					this.currentSelectSkillID = current.get_Key();
				}
			}
		}
		else
		{
			this.currentSelectSkillID = 0;
			if (SkillUIManager.Instance.OpenSkillIDs != null && SkillUIManager.Instance.OpenSkillIDs.get_Count() > 0)
			{
				int num = SkillUIManager.Instance.OpenSkillIDs.get_Count() - 1;
				this.currentSelectSkillID = SkillUIManager.Instance.OpenSkillIDs.get_Item(num);
			}
		}
		this.SetSkillPanelState(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.isFirstOperate = false;
		CurrenciesUIViewModel.Show(false);
		FXSpineManager.Instance.DeleteSpine(this.skillUpBtnFXID1, true);
		FXSpineManager.Instance.DeleteSpine(this.skillUpBtnFXID2, true);
		this.skillUpBtnFXID1 = 0;
		this.skillUpBtnFXID2 = 0;
		FXSpineManager.Instance.DeleteSpine(this.skillNotchFxID, true);
		this.skillNotchFxID = 0;
		this.RemoveChangeSkillPanelAnim();
		if (SkillUIManager.Instance.NewOpenSkillIDs != null)
		{
			SkillUIManager.Instance.NewOpenSkillIDs.Clear();
		}
		if (SkillRuneManager.Instance.NewOpenRuneStones != null)
		{
			SkillRuneManager.Instance.NewOpenRuneStones.Clear();
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnSkillConfigInfoChangeNty, new Callback(this.OnSkillConfigInfoChangeNty));
		EventDispatcher.AddListener(EventNames.OnSkillTrainChangeNty, new Callback(this.OnSkillTrainChangeNty));
		EventDispatcher.AddListener(EventNames.OnRuneStoneChangedNty, new Callback(this.OnRuneStoneChangedNty));
		EventDispatcher.AddListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
		EventDispatcher.AddListener<int>(EventNames.OnSkillUpgradeRes, new Callback<int>(this.OnSkillUpgradeRes));
		EventDispatcher.AddListener<int>(EventNames.OnSkillConfigRes, new Callback<int>(this.OnSkillConfigRes));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.SkillPointChanged, new Callback(this.ChangeSkillPoint));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnSkillConfigInfoChangeNty, new Callback(this.OnSkillConfigInfoChangeNty));
		EventDispatcher.RemoveListener(EventNames.OnSkillTrainChangeNty, new Callback(this.OnSkillTrainChangeNty));
		EventDispatcher.RemoveListener(EventNames.OnRuneStoneChangedNty, new Callback(this.OnRuneStoneChangedNty));
		EventDispatcher.RemoveListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnSkillUpgradeRes, new Callback<int>(this.OnSkillUpgradeRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnSkillConfigRes, new Callback<int>(this.OnSkillConfigRes));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.SkillPointChanged, new Callback(this.ChangeSkillPoint));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickExit()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickChangeSkill(GameObject go)
	{
		this.IsOpenSkillChangePanel = !this.IsOpenSkillChangePanel;
		this.RefreshSkillChange();
	}

	private void OnClickSkillUpgrade(GameObject go)
	{
		int skillSlotNumByID = SkillUIManager.Instance.GetSkillSlotNumByID(this.currentSelectSkillID);
		if (skillSlotNumByID > 0)
		{
			SkillUIManager.Instance.SendSkillUpReq(skillSlotNumByID, this.currentSelectSkillID);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("该技能不能升级");
		}
	}

	private void OnClickSelectRuneStone(GameObject go)
	{
		if (go != null && go.GetComponent<SkillRuneStoneItem>().runeStoneCfgData != null)
		{
			SkillRuneStoneItem component = go.GetComponent<SkillRuneStoneItem>();
			if (component.runeStoneCfgData == null)
			{
				Debug.Log("符石数据为空");
			}
			int id = component.runeStoneCfgData.id;
			if (!SkillRuneManager.Instance.CheckRuneStoneIsUnLock(id))
			{
				return;
			}
			this.RefreshSelectRuneStoneData(go.GetComponent<SkillRuneStoneItem>());
		}
	}

	private void OnClickSelectSkillItem(GameObject go)
	{
		if (go != null && go.get_transform().get_parent().GetComponent<SkillBtnItem>() != null)
		{
			SkillBtnItem component = go.get_transform().get_parent().GetComponent<SkillBtnItem>();
			if (!SkillUIManager.Instance.CheckSkillIsUnLock(component.SkillUnLockCfgData.skillId))
			{
				string text = string.Empty;
				string text2 = string.Empty;
				int skillUnlockArtifactID = SkillUIManager.Instance.GetSkillUnlockArtifactID(component.SkillUnLockCfgData.skillId);
				if (skillUnlockArtifactID > 0)
				{
					Artifact artifact = DataReader<Artifact>.Get(skillUnlockArtifactID);
					if (artifact != null)
					{
						text = GameDataUtils.GetChineseContent(artifact.name, false);
					}
					Skill skill = DataReader<Skill>.Get(component.SkillUnLockCfgData.skillId);
					if (skill != null)
					{
						text2 = GameDataUtils.GetChineseContent(skill.name, false);
					}
					string text3 = string.Format(GameDataUtils.GetChineseContent(518004, false), text, text2);
					UIManagerControl.Instance.ShowToastText(text3);
				}
			}
			this.currentSelectRuneStoneID = 0;
			this.RefreshSelectSkillItemData(component);
		}
	}

	private void OnClickSkillNotchBtn(GameObject go)
	{
		for (int i = 0; i < this.skillNotchBtnList.get_Count(); i++)
		{
			ButtonCustom buttonCustom = this.skillNotchBtnList.get_Item(i);
			if (go == buttonCustom.get_gameObject() && this.IsOpenSkillChangePanel)
			{
				int skillSlotNumByID = SkillUIManager.Instance.GetSkillSlotNumByID(this.currentSelectSkillID);
				if (skillSlotNumByID <= 0)
				{
					return;
				}
				SkillUIManager.Instance.SendSkillConfigReq(skillSlotNumByID, i + 1, 1);
			}
		}
	}

	private void OnClickActivityOrBeActivityBtn(GameObject go)
	{
		if (go == this.activeSkillBtn.get_gameObject())
		{
			this.SetSkillPanelState(true);
		}
		else if (go == this.beActiveSkillBtn.get_gameObject())
		{
			this.SetSkillPanelState(false);
		}
	}

	private void OnClickSelectPssiveSkillItem(GameObject go)
	{
		if (go == null || go.GetComponent<SkillPassiveBtnItem>() == null)
		{
			return;
		}
		SkillPassiveBtnItem component = go.GetComponent<SkillPassiveBtnItem>();
		bool flag = SkillUIManager.Instance.CheckArtifactSkillIsUnlock(component.ArtifactSkillData.id);
		this.RefreshSelectPassiveSkillItemData(component);
	}

	public void SetSkillPanelState(bool isShowActivity = true)
	{
		this.activeSkillBtn.get_transform().FindChild("Select").get_gameObject().SetActive(isShowActivity);
		this.beActiveSkillBtn.get_transform().FindChild("Select").get_gameObject().SetActive(!isShowActivity);
		this.skillActivityPanel.get_gameObject().SetActive(isShowActivity);
		this.skillbeActivityPanel.get_gameObject().SetActive(!isShowActivity);
		if (isShowActivity)
		{
			this.RefreshActivitySkillPanel();
		}
		else
		{
			this.RefreshBeActivitySkillPanel();
		}
	}

	private void RefreshBeActivitySkillPanel()
	{
		this.RefreshPassiveSkillData();
	}

	private void RefreshActivitySkillPanel()
	{
		this.ResetActivitySkillUI();
		this.RefreshAllSkillData();
		this.CheckCanShowSkillUpRedPoint();
	}

	private void ResetAllPanelPosition(bool isOpen = false)
	{
		this.skillChangePanelTrans.set_localPosition((!isOpen) ? this.skillChangePanelStartPos : this.skillChangePanelEndPos);
		this.runeUpgradePanelTrans.get_gameObject().SetActive(!isOpen);
	}

	private void ResetActivitySkillUI()
	{
		this.IsOpenSkillChangePanel = false;
		this.ResetAllPanelPosition(false);
		this.SetSkillNotchData();
	}

	private void CheckCanShowSkillUpRedPoint()
	{
		bool active = SkillUIManager.Instance.CheckRoleSkillsCanUpgrade();
		this.activeSkillBtn.get_transform().FindChild("RedPointTip").get_gameObject().SetActive(active);
	}

	private void RefreshAllSkillData()
	{
		List<JiNengJieSuoBiao> skillUnlockCfgList = SkillUIManager.Instance.GetSkillUnlockCfgData();
		if (skillUnlockCfgList == null)
		{
			return;
		}
		this.pool.Create(skillUnlockCfgList.get_Count(), delegate(int index)
		{
			if (index < skillUnlockCfgList.get_Count())
			{
				SkillBtnItem component = this.pool.Items.get_Item(index).GetComponent<SkillBtnItem>();
				if (component != null)
				{
					component.UpdateSkillItem(skillUnlockCfgList.get_Item(index));
					this.pool.Items.get_Item(index).get_transform().FindChild("ScaleRoot").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectSkillItem);
					if ((this.currentSelectSkillID > 0 && skillUnlockCfgList.get_Item(index).skillId == this.currentSelectSkillID) || (this.currentSelectSkillID <= 0 && index == 0))
					{
						this.RefreshSelectSkillItemData(component);
					}
				}
				if (index == skillUnlockCfgList.get_Count() - 1)
				{
					this.PlayUnlockSkillFX();
				}
			}
		});
	}

	private void RefreshRuneStoneDataBySkillID(int skillID)
	{
		Dictionary<int, List<Runes_basic>> runeInfoDataBySkillID = SkillRuneManager.Instance.GetRuneInfoDataBySkillID(skillID);
		if (runeInfoDataBySkillID == null)
		{
			return;
		}
		if (!SkillRuneManager.Instance.CheckHaveUnLockRuneStonBySkillID(skillID))
		{
			this.runeUpgradePanelTrans.FindChild("NoRuneStoneRoot").get_gameObject().SetActive(true);
			this.runeUpgradePanelTrans.FindChild("HaveRuneStoneRoot").get_gameObject().SetActive(false);
			return;
		}
		this.runeUpgradePanelTrans.FindChild("NoRuneStoneRoot").get_gameObject().SetActive(false);
		this.runeUpgradePanelTrans.FindChild("HaveRuneStoneRoot").get_gameObject().SetActive(true);
		for (int i = 0; i < 4; i++)
		{
			Transform transform = this.runeStoneGroupTransList.get_Item(i);
			if (runeInfoDataBySkillID != null && runeInfoDataBySkillID.ContainsKey(i + 1))
			{
				List<Runes_basic> list = runeInfoDataBySkillID.get_Item(i + 1);
				for (int j = 0; j < list.get_Count(); j++)
				{
					Transform transform2 = null;
					if (j < transform.get_childCount())
					{
						transform2 = transform.GetChild(j);
					}
					if (transform2 == null)
					{
						GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("SKillRuneStoneItem");
						instantiate2Prefab.set_name("RuneStoneItemGroup" + i);
						instantiate2Prefab.get_transform().SetParent(transform);
						instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
						instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectRuneStone);
						transform2 = instantiate2Prefab.get_transform();
					}
					if (transform2 == null)
					{
						return;
					}
					SkillRuneStoneItem skillRuneStoneItem = transform2.GetComponent<SkillRuneStoneItem>();
					if (skillRuneStoneItem == null)
					{
						skillRuneStoneItem = transform2.get_gameObject().AddComponent<SkillRuneStoneItem>();
					}
					skillRuneStoneItem.UpdateUI(list.get_Item(j), skillID);
					skillRuneStoneItem.Selected = false;
					int skillEmbedGroupIndex = SkillRuneManager.Instance.GetSkillEmbedGroupIndex(skillID);
					if ((skillEmbedGroupIndex > 0 && skillEmbedGroupIndex == i + 1) || (skillEmbedGroupIndex <= 0 && i == 0))
					{
						this.RefreshSelectRuneStoneData(skillRuneStoneItem);
					}
				}
			}
		}
		this.PlayUnlockRuneStoneFX(skillID);
	}

	private void RefreshSelectRuneStoneDesc(int runeID)
	{
		this.runeStoneNameText.set_text(string.Empty);
		this.runeStoneDescText.set_text(string.Empty);
		if (runeID <= 0)
		{
			return;
		}
		Runes runesCfgData = SkillRuneManager.Instance.GetRunesCfgData(runeID);
		if (runesCfgData != null)
		{
			this.runeStoneDescText.set_text(GameDataUtils.GetChineseContent(runesCfgData.desc, false));
		}
		Runes_basic runesBasicCfgData = SkillRuneManager.Instance.GetRunesBasicCfgData(runeID);
		if (runesBasicCfgData != null)
		{
			this.runeStoneNameText.set_text(GameDataUtils.GetChineseContent(runesBasicCfgData.name, false));
		}
	}

	private void RefreshSelectSkillDesc(int skillID)
	{
		Skill skill = DataReader<Skill>.Get(skillID);
		if (skill == null)
		{
			return;
		}
		this.skillNameText.set_text(GameDataUtils.GetChineseContent(skill.name, false));
		this.skillDescText.set_text(GameDataUtils.GetChineseContent(skill.describeId, false));
		this.skillCdText.set_text(string.Format("冷却时间：<color=red>{0}秒</color>", (float)skill.cd * 0.001f));
		JiNengShengJiBiao skillUpgradeCfgDataByID = SkillUIManager.Instance.GetSkillUpgradeCfgDataByID(skillID);
		if (skillUpgradeCfgDataByID != null)
		{
			this.skillBasicDamageText.set_text(string.Format("基础伤害：<color=#ff7d4b>{0:P1}</color>", skillUpgradeCfgDataByID.skillDate1));
		}
		bool flag = SkillUIManager.Instance.CheckSkillIsUnLock(skillID);
		base.FindTransform("SkillUnLockImg").get_gameObject().SetActive(!flag);
		bool flag2 = SkillUIManager.Instance.CheckSkillIsCanUpgrade(skillID);
		base.FindTransform("SkillLvFullImg").get_gameObject().SetActive(!flag2 && flag);
		bool flag3 = EntityWorld.Instance.EntSelf.Lv >= SkillUIManager.Instance.SkillCanUpLevel;
		base.FindTransform("SkillCanNotUp").get_gameObject().SetActive(flag && !flag3);
		if (flag && flag2 && flag3)
		{
			this.SetSkillUpgradeBtnState(true);
			base.FindTransform("SkillUpItemInfo").get_gameObject().SetActive(true);
			JiNengShengJiBiao skillNextLvCfgDataByID = SkillUIManager.Instance.GetSkillNextLvCfgDataByID(skillID);
			if (skillNextLvCfgDataByID != null)
			{
				this.SetSkillUpgradeItem(11, skillNextLvCfgDataByID.itemNum.get_Item(0));
			}
		}
		else
		{
			this.SetSkillUpgradeBtnState(false);
			base.FindTransform("SkillUpItemInfo").get_gameObject().SetActive(false);
		}
	}

	private void SetSkillUpgradeBtnState(bool isClick = true)
	{
		this.skillUpgradeBtn.set_enabled(isClick);
		Image component = this.skillUpgradeBtn.GetComponent<Image>();
		string spriteName = (!isClick) ? "button_gray_1" : "button_yellow_1";
		ResourceManager.SetIconSprite(component, spriteName);
	}

	private void SetSkillUpgradeItem(int itemId, int num)
	{
		Image component = base.FindTransform("GoldIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemIcon(itemId));
		Text component2 = base.FindTransform("GoldNum").GetComponent<Text>();
		if (EntityWorld.Instance.EntSelf.SkillPoint >= (long)num)
		{
			component2.set_text(EntityWorld.Instance.EntSelf.SkillPoint + "/" + num);
		}
		else
		{
			component2.set_text(string.Concat(new object[]
			{
				"<color=red>",
				EntityWorld.Instance.EntSelf.SkillPoint,
				"/",
				num,
				"</color>"
			}));
		}
	}

	private void RefreshSelectSkillItemData(SkillBtnItem skillBtnItem)
	{
		if (skillBtnItem == null)
		{
			return;
		}
		this.lastSelectSkillBtnItem = this.currentSelectSkillBtnItem;
		if (this.lastSelectSkillBtnItem != null)
		{
			this.lastSelectSkillBtnItem.Selected = false;
		}
		this.currentSelectSkillBtnItem = skillBtnItem;
		this.currentSelectSkillBtnItem.Selected = true;
		this.currentSelectSkillID = this.currentSelectSkillBtnItem.SkillUnLockCfgData.skillId;
		this.RefreshSelectSkillDesc(this.currentSelectSkillID);
		this.RefreshRuneStoneDataBySkillID(this.currentSelectSkillID);
		bool flag = SkillUIManager.Instance.CheckSkillIsUnLock(this.currentSelectSkillID);
		if (this.isFirstOperate)
		{
			this.isFirstOperate = false;
			bool flag2 = SkillRuneManager.Instance.CheckHaveUnLockRuneStonBySkillID(this.currentSelectSkillID);
			if (!flag2)
			{
				this.IsOpenSkillChangePanel = true;
				this.SkillPanelMoveOnPath(this.IsOpenSkillChangePanel);
				this.runeUpgradePanelTrans.get_gameObject().SetActive(!this.IsOpenSkillChangePanel);
			}
			else if (flag2)
			{
				this.IsOpenSkillChangePanel = false;
				this.SkillPanelMoveOnPath(this.IsOpenSkillChangePanel);
				this.runeUpgradePanelTrans.get_gameObject().SetActive(!this.IsOpenSkillChangePanel);
			}
		}
		this.RemoveChangeSkillPanelAnim();
		if (flag && this.IsOpenSkillChangePanel && !SkillUIManager.Instance.CheckSkillIsEmbedNotch(this.currentSelectSkillID))
		{
			this.PlayChangeSkillPanelAnim();
		}
	}

	private void RefreshSelectRuneStoneData(SkillRuneStoneItem skillRuneStoneItem)
	{
		if (skillRuneStoneItem == null)
		{
			return;
		}
		if (this.lastSelectRuneStoneItem != null)
		{
			this.lastSelectRuneStoneItem.Selected = false;
		}
		this.currentSelectRuneStoneID = skillRuneStoneItem.runeStoneCfgData.id;
		skillRuneStoneItem.Selected = true;
		this.RefreshSelectRuneStoneDesc(this.currentSelectRuneStoneID);
		this.lastSelectRuneStoneItem = skillRuneStoneItem;
		if (skillRuneStoneItem.IsUnLock && SkillUIManager.Instance.CheckSkillIsUnLock(this.currentSelectSkillID))
		{
			SkillRuneManager.Instance.SendEmbedRunedStoneReq(this.currentSelectSkillID, this.currentSelectRuneStoneID);
		}
	}

	private void SetSkillNotchData()
	{
		for (int i = 0; i < this.skillNotchBtnList.get_Count(); i++)
		{
			if (SkillUIManager.Instance.SkillNotchDic != null && SkillUIManager.Instance.SkillNotchDic.ContainsKey(i + 1) && SkillUIManager.Instance.SkillNotchDic.get_Item(i + 1).skillId > 0)
			{
				Image component = this.skillNotchBtnList.get_Item(i).get_transform().FindChild("NotchIcon").GetComponent<Image>();
				component.get_gameObject().SetActive(true);
				Skill skill = DataReader<Skill>.Get(SkillUIManager.Instance.SkillNotchDic.get_Item(i + 1).skillId);
				if (skill != null)
				{
					ResourceManager.SetSprite(component, GameDataUtils.GetIcon(skill.icon));
				}
			}
			else
			{
				this.skillNotchBtnList.get_Item(i).get_transform().FindChild("NotchIcon").get_gameObject().SetActive(false);
			}
		}
	}

	private void SkillPanelMoveOnPath(bool isOpenChange)
	{
		Vector3 target = Vector3.get_zero();
		target = ((!isOpenChange) ? this.skillChangePanelStartPos : this.skillChangePanelEndPos);
		if (base.get_gameObject().get_activeInHierarchy())
		{
			base.StartCoroutine(this.skillChangePanelTrans.GetComponent<RectTransform>().MoveTo(target, 0.5f, EaseType.Linear, delegate
			{
			}));
		}
		else
		{
			this.ResetAllPanelPosition(isOpenChange);
		}
	}

	private void RefreshSkillChange()
	{
		this.SkillPanelMoveOnPath(this.IsOpenSkillChangePanel);
		this.runeUpgradePanelTrans.get_gameObject().SetActive(!this.IsOpenSkillChangePanel);
		if (this.IsOpenSkillChangePanel)
		{
			bool flag = SkillUIManager.Instance.CheckSkillIsUnLock(this.currentSelectSkillID);
			if (flag && !SkillUIManager.Instance.CheckSkillIsEmbedNotch(this.currentSelectSkillID))
			{
				this.PlayChangeSkillPanelAnim();
			}
		}
		else
		{
			this.RemoveChangeSkillPanelAnim();
		}
	}

	private void PlayUnlockSkillFX()
	{
		if (SkillUIManager.Instance.NewOpenSkillIDs != null)
		{
			for (int i = 0; i < SkillUIManager.Instance.NewOpenSkillIDs.get_Count(); i++)
			{
				int unlockSkillId = SkillUIManager.Instance.NewOpenSkillIDs.get_Item(i);
				for (int j = 0; j < this.pool.Items.get_Count(); j++)
				{
					SkillBtnItem component = this.pool.Items.get_Item(j).GetComponent<SkillBtnItem>();
					component.PlayUnlockFxID(unlockSkillId);
				}
			}
			SkillUIManager.Instance.NewOpenSkillIDs.Clear();
		}
	}

	private void PlayUnlockRuneStoneFX(int skillID)
	{
		if (this.runeStonesUnlockFxPlayed.ContainsKey(skillID))
		{
			return;
		}
		if (SkillRuneManager.Instance.NewOpenRuneStones != null && SkillRuneManager.Instance.NewOpenRuneStones.ContainsKey(skillID))
		{
			for (int i = 0; i < SkillRuneManager.Instance.NewOpenRuneStones.get_Item(skillID).get_Count(); i++)
			{
				int num = SkillRuneManager.Instance.NewOpenRuneStones.get_Item(skillID).get_Item(i);
				for (int j = 0; j < 4; j++)
				{
					Transform transform = this.runeStoneGroupTransList.get_Item(j);
					if (transform.get_childCount() >= 1)
					{
						SkillRuneStoneItem component = transform.GetChild(0).GetComponent<SkillRuneStoneItem>();
						if (component != null && component.runeStoneCfgData != null && component.runeStoneCfgData.id == num)
						{
							component.PlayUnlockRuneStoneFX(num);
						}
					}
				}
			}
			this.runeStonesUnlockFxPlayed.Add(skillID, true);
			SkillRuneManager.Instance.NewOpenRuneStones.Remove(skillID);
		}
	}

	private void PlayChangeSkillPanelAnim()
	{
		for (int i = 0; i < this.skillNotchBtnList.get_Count(); i++)
		{
			Transform transform = this.skillNotchBtnList.get_Item(i).get_transform().FindChild("SkillNotchFX");
			if (transform != null)
			{
				switch (i)
				{
				case 0:
					this.skillNotchChangeTipFxID1 = FXSpineManager.Instance.ReplaySpine(this.skillNotchChangeTipFxID1, 115, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					break;
				case 1:
					this.skillNotchChangeTipFxID2 = FXSpineManager.Instance.ReplaySpine(this.skillNotchChangeTipFxID2, 115, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					break;
				case 2:
					this.skillNotchChangeTipFxID3 = FXSpineManager.Instance.ReplaySpine(this.skillNotchChangeTipFxID3, 115, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					break;
				}
			}
		}
	}

	private void RemoveChangeSkillPanelAnim()
	{
		if (this.skillNotchChangeTipFxID1 > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.skillNotchChangeTipFxID1, true);
		}
		if (this.skillNotchChangeTipFxID2 > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.skillNotchChangeTipFxID2, true);
		}
		if (this.skillNotchChangeTipFxID3 > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.skillNotchChangeTipFxID3, true);
		}
	}

	private void RefreshPassiveSkillData()
	{
		List<ArtifactSkill> allArtifactSkillCfgData = SkillUIManager.Instance.GetAllArtifactSkillCfgData();
		if (allArtifactSkillCfgData == null)
		{
			return;
		}
		for (int i = 0; i < allArtifactSkillCfgData.get_Count(); i++)
		{
			ArtifactSkill artifactSkill = allArtifactSkillCfgData.get_Item(i);
			Transform transform = null;
			if (i < this.beActivitySkillGridsTrans.get_childCount())
			{
				transform = this.beActivitySkillGridsTrans.GetChild(i);
			}
			if (transform == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("SkillPassiveBtnItem");
				instantiate2Prefab.set_name("SkillPassiveBtnItem" + i);
				instantiate2Prefab.get_transform().SetParent(this.beActivitySkillGridsTrans);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectPssiveSkillItem);
				instantiate2Prefab.SetActive(true);
				transform = instantiate2Prefab.get_transform();
			}
			if (transform == null)
			{
				return;
			}
			SkillPassiveBtnItem skillPassiveBtnItem = transform.GetComponent<SkillPassiveBtnItem>();
			if (skillPassiveBtnItem == null)
			{
				skillPassiveBtnItem = transform.get_gameObject().AddComponent<SkillPassiveBtnItem>();
			}
			skillPassiveBtnItem.UpdatePassiveSkillItem(artifactSkill);
			skillPassiveBtnItem.Selected = false;
			if ((this.currentSelectPassiveSkillID > 0 && this.currentSelectPassiveSkillID == artifactSkill.id) || (this.currentSelectPassiveSkillID <= 0 && i == 0))
			{
				this.RefreshSelectPassiveSkillItemData(skillPassiveBtnItem);
			}
		}
	}

	private void RefreshSelectPassiveSkillItemData(SkillPassiveBtnItem passiveBtnItem)
	{
		if (passiveBtnItem == null)
		{
			return;
		}
		this.lastSelectPassiveSkillBtnItem = this.currentSelectPsssiveSkillBtnItem;
		if (this.lastSelectPassiveSkillBtnItem != null)
		{
			this.lastSelectPassiveSkillBtnItem.Selected = false;
		}
		this.currentSelectPsssiveSkillBtnItem = passiveBtnItem;
		this.currentSelectPsssiveSkillBtnItem.Selected = true;
		this.currentSelectPassiveSkillID = this.currentSelectPsssiveSkillBtnItem.ArtifactSkillData.id;
		this.RefreshBeActivityDownDetail(this.currentSelectPassiveSkillID);
	}

	private void RefreshBeActivityDownDetail(int beActivitySkillID)
	{
		ArtifactSkill artifactSkill = DataReader<ArtifactSkill>.Get(beActivitySkillID);
		if (artifactSkill != null)
		{
			this.beActivitySkillNameText.set_text(GameDataUtils.GetChineseContent(artifactSkill.name, false));
			this.beActivityDownDescText.set_text(GameDataUtils.GetChineseContent(artifactSkill.desc, false));
			if (!SkillUIManager.Instance.CheckArtifactSkillIsUnlock(beActivitySkillID))
			{
				Text expr_51 = this.beActivityDownDescText;
				expr_51.set_text(expr_51.get_text() + "\n" + SkillUIManager.Instance.GetArtifactNameLockTipByID(beActivitySkillID));
			}
		}
	}

	private void OnSkillConfigInfoChangeNty()
	{
		this.RefreshAllSkillData();
		this.SetSkillNotchData();
	}

	private void OnSkillTrainChangeNty()
	{
		this.RefreshAllSkillData();
		this.CheckCanShowSkillUpRedPoint();
	}

	private void OnRuneStoneChangedNty()
	{
		this.RefreshRuneStoneDataBySkillID(this.currentSelectSkillID);
	}

	private void OnRuneStoneEmbedRes(int skillID)
	{
		if (this.currentSelectSkillID == skillID)
		{
			this.RefreshRuneStoneDataBySkillID(skillID);
		}
	}

	private void OnSkillUpgradeRes(int skillID)
	{
		FXSpineManager.Instance.DeleteSpine(this.skillUpBtnFXID1, true);
		FXSpineManager.Instance.DeleteSpine(this.skillUpBtnFXID2, true);
		Transform transform = base.FindTransform("SkillUpgradeFX");
		if (transform != null)
		{
			FXSpineManager.Instance.PlaySpine(3306, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.PlaySpine(3308, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void OnSkillConfigRes(int targetNotch)
	{
		FXSpineManager.Instance.DeleteSpine(this.skillNotchFxID, true);
		int num = targetNotch - 1;
		if (this.skillNotchBtnList != null && num >= 0 && num < this.skillNotchBtnList.get_Count())
		{
			Transform transform = this.skillNotchBtnList.get_Item(num).get_transform().FindChild("SkillNotchFX");
			if (transform != null)
			{
				this.skillNotchFxID = FXSpineManager.Instance.PlaySpine(3022, transform, "SkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
	}

	private void ChangeSkillPoint()
	{
		this.RefreshAllSkillData();
		this.CheckCanShowSkillUpRedPoint();
	}
}
