using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRuneUI : UIBase
{
	private ListPool pool;

	private List<Transform> runeStoneGroupTransList;

	private List<Transform> runeStoneEmbedBtnTransList;

	private int currentProtectID;

	private int currentSelectRuneStoneID;

	private int currentSelectSkillID;

	private SkillRuneStoneItem lastSelectRuneStoneItem;

	private SkillRuneBtnItem currentSelectSkillBtnItem;

	private SkillRuneBtnItem lastSelectSkillBtnItem;

	private Text rightItemTitleText;

	private Text rightDownDescText;

	private ButtonCustom runeStoneUpgradeBtn;

	private Text runeStoneLockLVText;

	private Text runeStoneLockNeedPreText;

	private GameObject rightCanUpgradeRoot;

	private Image runeStoneUpgradeFullImg;

	private Image runeStoneUnLockImg;

	private Image upgradeNeedMaterialImg;

	private Text upgradeNeedMaterialNumText;

	private Image protectStoneIcon;

	private ButtonCustom addProtectStoneBtn;

	private Text runeStoneUpgradeRatioText;

	private bool IsSelectAddProtectBtn;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		this.runeStoneGroupTransList = new List<Transform>();
		this.runeStoneEmbedBtnTransList = new List<Transform>();
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.pool = base.FindTransform("Grid").GetComponent<ListPool>();
		this.rightItemTitleText = base.FindTransform("txtName").GetComponent<Text>();
		this.rightDownDescText = base.FindTransform("DownDesc").GetComponent<Text>();
		this.runeStoneUpgradeBtn = base.FindTransform("RuneStoneUpgradeBtn").GetComponent<ButtonCustom>();
		this.runeStoneLockLVText = base.FindTransform("RuneStoneLockLVText").GetComponent<Text>();
		this.runeStoneLockNeedPreText = base.FindTransform("RuneStoneLockNeedPreText").GetComponent<Text>();
		this.rightCanUpgradeRoot = base.FindTransform("CanUpgradeRoot").get_gameObject();
		this.runeStoneUpgradeFullImg = base.FindTransform("RuneStoneUpgradeFull").GetComponent<Image>();
		this.runeStoneUnLockImg = base.FindTransform("RuneStoneNotUnLock").GetComponent<Image>();
		this.runeStoneUpgradeRatioText = base.FindTransform("RuneStoneUpgradeRatioText").GetComponent<Text>();
		this.protectStoneIcon = base.FindTransform("ProtectStoneIcon").GetComponent<Image>();
		this.upgradeNeedMaterialImg = base.FindTransform("GoldIcon").GetComponent<Image>();
		this.upgradeNeedMaterialNumText = base.FindTransform("GoldNum").GetComponent<Text>();
		this.addProtectStoneBtn = base.FindTransform("AddProtectBtn").GetComponent<ButtonCustom>();
		this.addProtectStoneBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAddProtectBtn);
		this.runeStoneUpgradeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUpgradeRuneStoneBtn);
		for (int i = 1; i <= 4; i++)
		{
			Transform transform = base.FindTransform("RuneStoneGroup" + i);
			Transform transform2 = base.FindTransform("RuneStoneEmbedBtn" + i);
			if (transform != null)
			{
				this.runeStoneGroupTransList.Add(transform);
			}
			if (transform2 != null)
			{
				this.runeStoneEmbedBtnTransList.Add(transform2);
				transform2.FindChild("NoEmbedBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEmbedBtn);
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110028), string.Empty, new Action(this.OnClickExit), false);
		this.IsSelectAddProtectBtn = false;
		this.SetAddProtectBtnState();
		this.RefreshSkillItemData();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.OnRunedStoneUpgradeRes, new Callback<int>(this.OnRunedStoneUpgradeRes));
		EventDispatcher.AddListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
		EventDispatcher.AddListener(EventNames.OnRuneStoneChangedNty, new Callback(this.OnRuneStoneChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.OnRunedStoneUpgradeRes, new Callback<int>(this.OnRunedStoneUpgradeRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnRuneStoneEmbedRes, new Callback<int>(this.OnRuneStoneEmbedRes));
		EventDispatcher.RemoveListener(EventNames.OnRuneStoneChangedNty, new Callback(this.OnRuneStoneChangedNty));
	}

	private void OnClickExit()
	{
		this.SureExit();
	}

	private void SureExit()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickAddProtectBtn(GameObject go)
	{
		Runes runesCfgData = SkillRuneManager.Instance.GetRunesCfgData(this.currentSelectRuneStoneID);
		if (runesCfgData == null)
		{
			return;
		}
		int templateId = runesCfgData.protect.get_Item(0);
		long num = BackpackManager.Instance.OnGetGoodCount(templateId);
		if (num <= 0L)
		{
			return;
		}
		this.IsSelectAddProtectBtn = !this.IsSelectAddProtectBtn;
		this.SetAddProtectBtnState();
		int addRatio = (!this.IsSelectAddProtectBtn) ? 0 : runesCfgData.protect.get_Item(1);
		this.UpdateRuneStoneUpgradeSuccessRatio(runesCfgData.successRate, addRatio);
	}

	private void OnClickUpgradeRuneStoneBtn(GameObject go)
	{
	}

	private void OnClickEmbedBtn(GameObject go)
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			if (go == this.runeStoneEmbedBtnTransList.get_Item(i).FindChild("NoEmbedBtn").get_gameObject())
			{
				num = i + 1;
			}
		}
		if (num <= 0)
		{
			return;
		}
		SkillRuneManager.Instance.SendEmbedRunedStoneReq(this.currentSelectSkillID, num);
	}

	private void OnClickSelectRuneStone(GameObject go)
	{
		if (go != null && go.GetComponent<SkillRuneStoneItem>().runeStoneCfgData != null)
		{
			if (this.lastSelectRuneStoneItem != null)
			{
				this.lastSelectRuneStoneItem.Selected = false;
			}
			this.currentSelectRuneStoneID = go.GetComponent<SkillRuneStoneItem>().runeStoneCfgData.id;
			go.GetComponent<SkillRuneStoneItem>().Selected = true;
			this.UpdateRightDescPanel(true);
			this.lastSelectRuneStoneItem = go.GetComponent<SkillRuneStoneItem>();
		}
	}

	private void OnClickSelectSkillItem(GameObject go)
	{
		if (go != null && go.GetComponent<SkillRuneBtnItem>() != null)
		{
			if (go.GetComponent<SkillRuneBtnItem>() == this.currentSelectSkillBtnItem && this.currentSelectRuneStoneID == 0)
			{
				return;
			}
			this.currentSelectRuneStoneID = 0;
			this.RefreshCurrentSkillRuneData(go.GetComponent<SkillRuneBtnItem>());
		}
	}

	private void RefreshCurrentSkillRuneData(SkillRuneBtnItem skillRuneBtnItem)
	{
		if (skillRuneBtnItem == null)
		{
			return;
		}
		this.lastSelectSkillBtnItem = this.currentSelectSkillBtnItem;
		if (this.lastSelectSkillBtnItem != null)
		{
			this.lastSelectSkillBtnItem.Selected = false;
		}
		this.currentSelectSkillBtnItem = skillRuneBtnItem;
		this.currentSelectSkillID = this.currentSelectSkillBtnItem.skillID;
		this.currentSelectSkillBtnItem.Selected = true;
		this.UpdateRightDescPanel(false);
		this.RefreshRuneStoneData(this.currentSelectSkillID);
		this.UpdateRuneStoneEmbedBtnsState();
	}

	private void RefreshSkillItemData()
	{
		if (SkillRuneManager.Instance.SkillRuneInfoDic == null)
		{
			return;
		}
		List<int> skillIds = SkillRuneManager.Instance.GetSkillList();
		this.pool.Create(skillIds.get_Count(), delegate(int index)
		{
			if (index < skillIds.get_Count())
			{
				SkillRuneBtnItem component = this.pool.Items.get_Item(index).GetComponent<SkillRuneBtnItem>();
				if (component != null)
				{
					component.UpdateSkillItem(skillIds.get_Item(index), false);
					this.pool.Items.get_Item(index).GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectSkillItem);
					if (skillIds.get_Item(index) == this.currentSelectSkillID && this.currentSelectRuneStoneID > 0)
					{
						this.RefreshCurrentSkillRuneData(component);
					}
					else if (index == 0 && this.currentSelectRuneStoneID <= 0)
					{
						this.RefreshCurrentSkillRuneData(component);
					}
				}
			}
		});
	}

	private void RefreshRuneStoneData(int skillID = 1401011)
	{
		Dictionary<int, List<Runes_basic>> runeInfoDataBySkillID = SkillRuneManager.Instance.GetRuneInfoDataBySkillID(skillID);
		for (int i = 0; i < 4; i++)
		{
			Transform transform = this.runeStoneGroupTransList.get_Item(i);
			if (runeInfoDataBySkillID != null && runeInfoDataBySkillID.ContainsKey(i + 1) && transform != null)
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
						instantiate2Prefab.set_name("RuneStoneItem" + (j + 1));
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
					if (this.currentSelectRuneStoneID == list.get_Item(j).id)
					{
						skillRuneStoneItem.Selected = true;
					}
				}
			}
		}
	}

	private void UpdateRightDescPanel(bool isShowRuneDesc = false)
	{
		if (isShowRuneDesc)
		{
			this.UpdateSelectRuneStoneDetail(this.currentSelectRuneStoneID);
		}
		else
		{
			this.UpdateSelectSkillDetail(this.currentSelectSkillID);
		}
	}

	private void UpdateSelectRuneStoneDetail(int runeID)
	{
		if (this.currentSelectRuneStoneID <= 0)
		{
			return;
		}
		base.FindTransform("SkillBaseAttr").get_gameObject().SetActive(false);
		base.FindTransform("SkillRunesAttrs").get_gameObject().SetActive(true);
		int runeStoneLv = SkillRuneManager.Instance.GetRuneStoneLV(runeID);
		Runes_basic runesBasicCfgData = SkillRuneManager.Instance.GetRunesBasicCfgData(runeID);
		Runes runesCfgData = SkillRuneManager.Instance.GetRunesCfgData(runeID);
		bool flag = SkillRuneManager.Instance.CheckRuneStoneCanUpgrade(runeID);
		bool flag2 = SkillRuneManager.Instance.CheckRuneStoneIsUnLock(runeID);
		if (runesBasicCfgData != null)
		{
			this.rightItemTitleText.set_text(GameDataUtils.GetChineseContent(runesBasicCfgData.name, false));
			this.runeStoneLockLVText.set_text(string.Empty);
			this.runeStoneLockNeedPreText.set_text(string.Empty);
			if (!flag2)
			{
				this.runeStoneLockLVText.set_text(string.Format("人物达到{0}级别解锁", runesBasicCfgData.unlockLv));
				if (runesBasicCfgData.condition == null || runesBasicCfgData.condition.get_Count() <= 0)
				{
					this.runeStoneLockNeedPreText.set_text(string.Empty);
				}
				else
				{
					int key = runesBasicCfgData.condition.get_Item(0).key;
					int value = runesBasicCfgData.condition.get_Item(0).value;
					Runes_basic runesBasicCfgData2 = SkillRuneManager.Instance.GetRunesBasicCfgData(key);
					string text = string.Empty;
					if (runesBasicCfgData2 != null)
					{
						text = GameDataUtils.GetChineseContent(runesBasicCfgData2.name, false);
					}
					this.runeStoneLockNeedPreText.set_text(string.Format(GameDataUtils.GetChineseContent(518003, false), text, value));
				}
			}
		}
		this.runeStoneUnLockImg.get_gameObject().SetActive(!flag2);
		this.runeStoneUpgradeFullImg.get_gameObject().SetActive(!flag && flag2);
		if (runesCfgData != null)
		{
			this.rightDownDescText.set_text(GameDataUtils.GetChineseContent(runesCfgData.desc, false));
			this.rightCanUpgradeRoot.SetActive(flag);
			if (flag)
			{
				Items item = BackpackManager.Instance.GetItem(runesCfgData.materials.get_Item(0));
				int icon = item.icon;
				ResourceManager.SetSprite(this.upgradeNeedMaterialImg, GameDataUtils.GetIcon(icon));
				this.upgradeNeedMaterialNumText.set_text(this.GetCostContent((long)runesCfgData.materials.get_Item(1), BackpackManager.Instance.OnGetGoodCount(item.id)));
				int templateId = runesCfgData.protect.get_Item(0);
				int addRatio = 0;
				this.currentProtectID = templateId;
				long num = BackpackManager.Instance.OnGetGoodCount(templateId);
				Items item2 = BackpackManager.Instance.GetItem(templateId);
				ResourceManager.SetSprite(this.protectStoneIcon, GameDataUtils.GetIcon(item2.icon));
				base.FindTransform("ProtectStoneName").GetComponent<Text>().set_text(num + "/" + 1);
				if (num > 0L && this.IsSelectAddProtectBtn)
				{
					addRatio = runesCfgData.protect.get_Item(1);
				}
				else
				{
					base.FindTransform("ProtectStoneName").GetComponent<Text>().set_text(string.Concat(new object[]
					{
						"<color=red>",
						num,
						"/",
						1,
						"</color>"
					}));
					this.IsSelectAddProtectBtn = false;
					this.SetAddProtectBtnState();
				}
				this.UpdateRuneStoneUpgradeSuccessRatio(runesCfgData.successRate, addRatio);
			}
			int num2 = 0;
			string text2 = string.Empty;
			for (int i = 0; i < runesCfgData.templateId.get_Count(); i++)
			{
				int key2 = runesCfgData.templateId.get_Item(i);
				Runes_template runes_template = DataReader<Runes_template>.Get(key2);
				if (runes_template != null)
				{
					for (int j = 0; j < runes_template.damageIncreaseId.get_Count(); j++)
					{
						int key3 = runes_template.damageIncreaseId.get_Item(j);
						damageIncrease damageIncrease = DataReader<damageIncrease>.Get(key3);
						if (damageIncrease != null)
						{
							num2 += damageIncrease.Value1;
						}
					}
				}
			}
			Runes runes2 = DataReader<Runes>.DataList.Find((Runes runes) => runes.id == runeID && runes.lv == runeStoneLv + 1);
			if (runes2 == null)
			{
				text2 = "MAX";
			}
			else
			{
				int num3 = 0;
				for (int k = 0; k < runes2.templateId.get_Count(); k++)
				{
					int key4 = runes2.templateId.get_Item(k);
					Runes_template runes_template2 = DataReader<Runes_template>.Get(key4);
					if (runes_template2 != null)
					{
						for (int l = 0; l < runes_template2.damageIncreaseId.get_Count(); l++)
						{
							int key5 = runes_template2.damageIncreaseId.get_Item(l);
							damageIncrease damageIncrease2 = DataReader<damageIncrease>.Get(key5);
							if (damageIncrease2 != null)
							{
								num3 += damageIncrease2.Value1;
							}
						}
					}
				}
				text2 = (float)num3 * 0.1f + "%";
			}
			base.FindTransform("Attr1Strengthen" + 1).get_gameObject().SetActive(true);
			base.FindTransform("Attr1Strengthen" + 1).FindChild("TextAttr1ValueNow").GetComponent<Text>().set_text((float)num2 * 0.1f + "%");
			base.FindTransform("Attr1Strengthen" + 1).FindChild("TextAttr1ValueAfter").GetComponent<Text>().set_text(text2);
			for (int m = 1; m < 3; m++)
			{
				base.FindTransform("Attr1Strengthen" + (m + 1)).get_gameObject().SetActive(false);
			}
		}
	}

	private void UpdateSelectSkillDetail(int skillID)
	{
		Skill skill = DataReader<Skill>.Get(skillID);
		if (skill == null)
		{
			return;
		}
		base.FindTransform("SkillBaseAttr").get_gameObject().SetActive(true);
		base.FindTransform("SkillRunesAttrs").get_gameObject().SetActive(false);
		this.rightItemTitleText.set_text(GameDataUtils.GetChineseContent(skill.name, false));
		this.rightDownDescText.set_text(GameDataUtils.GetChineseContent(skill.describeId, false));
		base.FindTransform("txCD").GetComponent<Text>().set_text(string.Format("（冷却时间：{0}秒）", (float)skill.cd * 0.001f));
		JiNengShengJiBiao skillUpgradeCfgDataByID = SkillUIManager.Instance.GetSkillUpgradeCfgDataByID(skillID);
		if (skillUpgradeCfgDataByID != null)
		{
			base.FindTransform("txBaseDamage").GetComponent<Text>().set_text(string.Format("基础伤害：<color=#ff7d4b>{0:P1}</color>", skillUpgradeCfgDataByID.skillDate1));
		}
		base.FindTransform("txFuwen").GetComponent<Text>().set_text("符文伤害加成：<color=#ff7d4b>" + (float)SkillRuneManager.Instance.GetEmbedRuneStoneAddAttrToSkill(skillID) * 0.1f + "%</color>");
		this.runeStoneUpgradeFullImg.get_gameObject().SetActive(false);
		this.runeStoneUnLockImg.get_gameObject().SetActive(false);
		this.rightCanUpgradeRoot.SetActive(false);
		this.runeStoneLockLVText.set_text(string.Empty);
		this.runeStoneLockNeedPreText.set_text(string.Empty);
	}

	private void UpdateRuneStoneEmbedBtnsState()
	{
		if (SkillRuneManager.Instance.SkillRuneInfoDic.ContainsKey(this.currentSelectSkillID))
		{
			RunedStoneInfo runedStoneInfo = SkillRuneManager.Instance.SkillRuneInfoDic.get_Item(this.currentSelectSkillID);
			for (int i = 0; i < 4; i++)
			{
				if (runedStoneInfo.embedGroupId == i + 1)
				{
					this.runeStoneEmbedBtnTransList.get_Item(i).FindChild("NoEmbedBtn").get_gameObject().SetActive(false);
					this.runeStoneEmbedBtnTransList.get_Item(i).FindChild("haveEmbed").get_gameObject().SetActive(true);
				}
				else
				{
					this.runeStoneEmbedBtnTransList.get_Item(i).FindChild("NoEmbedBtn").get_gameObject().SetActive(true);
					this.runeStoneEmbedBtnTransList.get_Item(i).FindChild("haveEmbed").get_gameObject().SetActive(false);
				}
			}
		}
	}

	private void UpdateRuneStoneUpgradeSuccessRatio(int ratio, int addRatio = 0)
	{
		int num = (ratio + addRatio < 100) ? (ratio + addRatio) : 100;
		this.runeStoneUpgradeRatioText.set_text(num + "%");
		base.FindTransform("jindutiaoSuccess").GetComponent<RectTransform>().set_sizeDelta(new Vector2((float)(330 * num) / 100f, 20f));
	}

	private void SetAddProtectBtnState()
	{
		this.addProtectStoneBtn.get_transform().FindChild("AddProtectBtnOn").get_gameObject().SetActive(this.IsSelectAddProtectBtn);
	}

	private string GetCostContent(long need, long have)
	{
		string result = string.Empty;
		if (need > have)
		{
			result = "<color=red>x" + need + "</color>";
		}
		else
		{
			result = "x" + need.ToString();
		}
		return result;
	}

	private void OnRunedStoneUpgradeRes(int runeStoneID)
	{
		if (this.currentSelectRuneStoneID == runeStoneID && this.currentSelectRuneStoneID > 0)
		{
			this.UpdateRightDescPanel(true);
		}
		else if (runeStoneID == 0 && this.currentSelectRuneStoneID > 0)
		{
			this.UpdateSelectRuneStoneDetail(this.currentSelectRuneStoneID);
		}
	}

	private void OnRuneStoneEmbedRes(int skillID)
	{
		if (this.currentSelectSkillID == skillID)
		{
			this.UpdateRuneStoneEmbedBtnsState();
			if (this.currentSelectRuneStoneID <= 0)
			{
				this.UpdateRightDescPanel(false);
			}
		}
	}

	private void OnRuneStoneChangedNty()
	{
		this.RefreshSkillItemData();
	}
}
