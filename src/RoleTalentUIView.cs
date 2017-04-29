using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoleTalentUIView : UIBase
{
	public static RoleTalentUIView Instance;

	private int m_selected_cfgId;

	private ListPool m_levelZonePool;

	private Text m_lblInfoName;

	private Text m_lblPointNum;

	private Text m_lblInfoDescContent;

	private GameObject m_goPointBadge;

	private GameObject m_goBtnReset;

	private Image m_spBtnResetBg;

	private GameObject m_goBtnActivation;

	private Image m_spBtnActivationBg;

	private GameObject m_goBtnAdd;

	private Image m_spBtnAddBg;

	private GameObject m_goConditionActivation;

	private GameObject m_goConditionLevel;

	private Text m_lblConditionLevel1T;

	private Text m_lblConditionLevel1;

	private Text m_lblConditionLevel2T;

	private Text m_lblConditionLevel2;

	private List<TalentActivation> listActivation = new List<TalentActivation>();

	private List<TalentLevelZone> listLevelZone = new List<TalentLevelZone>();

	private int m_fx_reset;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		RoleTalentUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_levelZonePool = base.FindTransform("LevelZoneList").GetComponent<ListPool>();
		this.m_levelZonePool.SetItem("TalentLevelZone");
		this.m_lblInfoName = base.FindTransform("InfoName").GetComponent<Text>();
		this.m_lblPointNum = base.FindTransform("PointNum").GetComponent<Text>();
		this.m_lblInfoDescContent = base.FindTransform("InfoDescContent").GetComponent<Text>();
		this.m_goPointBadge = base.FindTransform("PointBadge").get_gameObject();
		this.m_goBtnReset = base.FindTransform("BtnReset").get_gameObject();
		this.m_spBtnResetBg = base.FindTransform("BtnResetBg").GetComponent<Image>();
		this.m_goBtnActivation = base.FindTransform("BtnActivation").get_gameObject();
		this.m_spBtnActivationBg = base.FindTransform("BtnActivationBg").GetComponent<Image>();
		this.m_goBtnAdd = base.FindTransform("BtnAdd").get_gameObject();
		this.m_spBtnAddBg = base.FindTransform("BtnAddBg").GetComponent<Image>();
		this.m_goConditionActivation = base.FindTransform("ConditionActivation").get_gameObject();
		this.m_goConditionLevel = base.FindTransform("ConditionLevel").get_gameObject();
		this.m_lblConditionLevel1T = base.FindTransform("ConditionLevel1T").GetComponent<Text>();
		this.m_lblConditionLevel1 = base.FindTransform("ConditionLevel1").GetComponent<Text>();
		this.m_lblConditionLevel2T = base.FindTransform("ConditionLevel2T").GetComponent<Text>();
		this.m_lblConditionLevel2 = base.FindTransform("ConditionLevel2").GetComponent<Text>();
		this.listActivation.Add(base.FindTransform("TalentActivation1").GetComponent<TalentActivation>());
		this.listActivation.Add(base.FindTransform("TalentActivation2").GetComponent<TalentActivation>());
		for (int i = 0; i < this.listActivation.get_Count(); i++)
		{
			this.listActivation.get_Item(i).AwakeSelf();
		}
		base.FindTransform("BtnActivation").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnActivation));
		base.FindTransform("BtnAdd").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnAdd));
		base.FindTransform("BtnReset").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnReset));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110028), string.Empty, delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
		this.m_selected_cfgId = 0;
		this.DeleteSpineOfReset();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			RoleTalentUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnActivation()
	{
		RoleTalentManager.Instance.SendRoleTalentActivate(this.m_selected_cfgId);
	}

	private void OnClickBtnAdd()
	{
		Talent.ActivationPair upgradeItemOfPoint = RoleTalentManager.Instance.GetUpgradeItemOfPoint(this.m_selected_cfgId);
		if (upgradeItemOfPoint != null && upgradeItemOfPoint.value > RoleTalentManager.Instance.TalentPoint)
		{
			UIManagerControl.Instance.ShowToastText("天赋点不足");
			return;
		}
		RoleTalentManager.Instance.SendRoleTalentUpgrade(this.m_selected_cfgId);
	}

	private void OnClickBtnReset()
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("重置天赋", string.Format("是否花费{0}钻石重置所有天赋", RoleTalentManager.Instance.GetResetPrice()), null, delegate
		{
			RoleTalentManager.Instance.SendRoleTalentReset();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	public void RefreshUI()
	{
		this.RefreshLevelZones();
		this.RefreshTalentInfo((this.m_selected_cfgId == 0) ? RoleTalentManager.Instance.GetDefaultCfgId() : this.m_selected_cfgId);
		this.RefreshReset();
	}

	public void TalentActivation(int talentId)
	{
		RoleTalentItem talent = this.GetTalent(talentId);
		if (talent != null)
		{
			talent.AddSpineOfActivation();
		}
	}

	public void TalentLevelUp(int talentId)
	{
		RoleTalentItem talent = this.GetTalent(talentId);
		if (talent != null)
		{
			talent.AddSpineOfLevelUp();
		}
	}

	public void TalentReset()
	{
		this.AddSpineOfReset();
		for (int i = 0; i < this.listLevelZone.get_Count(); i++)
		{
			TalentLevelZone talentLevelZone = this.listLevelZone.get_Item(i);
			for (int j = 0; j < talentLevelZone.listItems.get_Count(); j++)
			{
				talentLevelZone.listItems.get_Item(j).AddSpineOfReset();
			}
		}
	}

	public void RefreshLevelZones()
	{
		int num = RoleTalentManager.Instance.ZoneToTalent.get_Count();
		this.listLevelZone.Clear();
		this.m_levelZonePool.Create(num, delegate(int index)
		{
			if (index < num && index < this.m_levelZonePool.Items.get_Count())
			{
				TalentLevelZone component = this.m_levelZonePool.Items.get_Item(index).GetComponent<TalentLevelZone>();
				component.m_row = index;
				this.listLevelZone.Add(component);
			}
			if (index == num - 1)
			{
				this.RefreshTalents(delegate
				{
					this.RefreshTalentsPrevious();
				});
			}
		});
	}

	private void RefreshTalents(Action finishCallback)
	{
		Dictionary<int, List<int>> zoneToTalent = RoleTalentManager.Instance.ZoneToTalent;
		int num = zoneToTalent.get_Count();
		int num2 = 0;
		using (Dictionary<int, List<int>>.KeyCollection.Enumerator enumerator = zoneToTalent.get_Keys().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				if (num2 < this.listLevelZone.get_Count())
				{
					TalentLevelZone talentLevelZone = this.listLevelZone.get_Item(num2);
					talentLevelZone.RefreshTalents(zoneToTalent.get_Item(current), delegate
					{
						num--;
						if (num <= 0 && finishCallback != null)
						{
							finishCallback.Invoke();
						}
					});
					talentLevelZone.SetLevel(current);
					num2++;
				}
			}
		}
	}

	private RoleTalentItem GetTalent(int talentId)
	{
		for (int i = 0; i < this.listLevelZone.get_Count(); i++)
		{
			TalentLevelZone talentLevelZone = this.listLevelZone.get_Item(i);
			for (int j = 0; j < talentLevelZone.listItems.get_Count(); j++)
			{
				if (talentLevelZone.listItems.get_Item(j).m_id == talentId)
				{
					return talentLevelZone.listItems.get_Item(j);
				}
			}
		}
		return null;
	}

	private void RefreshTalentsPrevious()
	{
		Dictionary<int, List<int>> zoneToTalent = RoleTalentManager.Instance.ZoneToTalent;
		using (Dictionary<int, List<int>>.ValueCollection.Enumerator enumerator = zoneToTalent.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				List<int> current = enumerator.get_Current();
				for (int i = 0; i < current.get_Count(); i++)
				{
					Talent dataCurrent = RoleTalentManager.Instance.GetDataCurrent(current.get_Item(i));
					if (dataCurrent.preTalent.get_Count() != 0)
					{
						for (int j = 0; j < dataCurrent.preTalent.get_Count(); j++)
						{
							this.SetPrevious(dataCurrent.id, dataCurrent.preTalent.get_Item(j).key);
						}
					}
				}
			}
		}
	}

	private void SetPrevious(int current_id, int previous_id)
	{
		RoleTalentItem roleTalentItem = this.FindItem(current_id);
		RoleTalentItem roleTalentItem2 = this.FindItem(previous_id);
		if (roleTalentItem == null || roleTalentItem2 == null)
		{
			return;
		}
		roleTalentItem2.SetArrow(roleTalentItem.m_row, roleTalentItem.m_column);
	}

	private RoleTalentItem FindItem(int id)
	{
		for (int i = 0; i < this.listLevelZone.get_Count(); i++)
		{
			List<RoleTalentItem> listItems = this.listLevelZone.get_Item(i).listItems;
			for (int j = 0; j < listItems.get_Count(); j++)
			{
				RoleTalentItem roleTalentItem = listItems.get_Item(j);
				if (roleTalentItem.m_id == id)
				{
					return roleTalentItem;
				}
			}
		}
		return null;
	}

	private void RefreshReset()
	{
		bool flag = RoleTalentManager.Instance.IsResetCan();
		this.m_goBtnReset.GetComponent<Button>().set_interactable(flag);
		ImageColorMgr.SetImageColor(this.m_spBtnResetBg, !flag);
		this.SetPoint(RoleTalentManager.Instance.TalentPoint);
	}

	private void SetPoint(int num)
	{
		this.m_goPointBadge.SetActive(num > 0);
		this.m_lblPointNum.set_text("剩余天赋点：" + TextColorMgr.GetColor(num.ToString(), "28c800", string.Empty));
	}

	public void RefreshTalentInfo(int cfgId)
	{
		this.m_selected_cfgId = cfgId;
		this.SetSelected();
		Talent dataCurrent = RoleTalentManager.Instance.GetDataCurrent(cfgId);
		this.m_lblInfoName.set_text(GameDataUtils.GetChineseContent(dataCurrent.name, false));
		this.SetInfoDescContent(dataCurrent);
		if (RoleTalentManager.Instance.IsActivation(cfgId))
		{
			this.SetConditionActivation(false, false, dataCurrent);
			this.SetConditionUpgrade(true, RoleTalentManager.Instance.IsUpgradeCan(dataCurrent), dataCurrent);
		}
		else
		{
			this.SetConditionActivation(true, RoleTalentManager.Instance.IsActivationCan(dataCurrent), dataCurrent);
			this.SetConditionUpgrade(false, false, dataCurrent);
		}
	}

	private void SetInfoDescContent(Talent data)
	{
		this.m_lblInfoDescContent.set_text(GameDataUtils.GetChineseContent(data.desc, false));
	}

	private void SetConditionActivation(bool isShow, bool isActivationOn, Talent data)
	{
		this.m_goConditionActivation.SetActive(isShow);
		this.m_goBtnActivation.SetActive(isShow);
		if (isShow)
		{
			for (int i = 0; i < this.listActivation.get_Count(); i++)
			{
				Talent.ActivationPair activationItem = RoleTalentManager.Instance.GetActivationItem(data, i);
				if (activationItem != null)
				{
					this.listActivation.get_Item(i).get_gameObject().SetActive(true);
					this.listActivation.get_Item(i).SetItem(activationItem.key, activationItem.value);
				}
				else
				{
					this.listActivation.get_Item(i).get_gameObject().SetActive(false);
				}
			}
			if (isActivationOn)
			{
				this.m_goBtnActivation.GetComponent<Button>().set_interactable(true);
				ImageColorMgr.SetImageColor(this.m_spBtnActivationBg, false);
			}
			else
			{
				this.m_goBtnActivation.GetComponent<Button>().set_interactable(false);
				ImageColorMgr.SetImageColor(this.m_spBtnActivationBg, true);
			}
		}
	}

	private void SetConditionUpgrade(bool isShow, bool isUpgradeOn, Talent data)
	{
		this.m_goConditionLevel.SetActive(isShow);
		this.m_goBtnAdd.SetActive(isShow);
		if (isShow)
		{
			this.m_lblConditionLevel1.set_text(string.Format("人物等级达到{0}级", data.minRoleLv));
			if (data.preTalent.get_Count() == 0)
			{
				this.m_lblConditionLevel2T.get_gameObject().SetActive(false);
				this.m_lblConditionLevel2.set_text(string.Empty);
			}
			else
			{
				this.m_lblConditionLevel2T.get_gameObject().SetActive(true);
				string text = string.Empty;
				for (int i = 0; i < data.preTalent.get_Count(); i++)
				{
					Talent data2 = RoleTalentManager.Instance.GetData(data.preTalent.get_Item(i).key, data.preTalent.get_Item(i).value);
					string text2 = string.Format("天赋{0}达到{1}级", GameDataUtils.GetChineseContent(data2.name, false), data2.lv);
					if (i == 0)
					{
						text = text2;
					}
					else
					{
						text = text + "\n" + text2;
					}
				}
				this.m_lblConditionLevel2.set_text(text);
			}
			if (isUpgradeOn)
			{
				this.m_goBtnAdd.GetComponent<Button>().set_interactable(true);
				ImageColorMgr.SetImageColor(this.m_spBtnAddBg, false);
			}
			else
			{
				this.m_goBtnAdd.GetComponent<Button>().set_interactable(false);
				ImageColorMgr.SetImageColor(this.m_spBtnAddBg, true);
			}
		}
	}

	private void SetSelected()
	{
		for (int i = 0; i < this.listLevelZone.get_Count(); i++)
		{
			List<RoleTalentItem> listItems = this.listLevelZone.get_Item(i).listItems;
			for (int j = 0; j < listItems.get_Count(); j++)
			{
				RoleTalentItem roleTalentItem = listItems.get_Item(j);
				roleTalentItem.SetSelected(roleTalentItem.m_id == this.m_selected_cfgId);
			}
		}
	}

	private void AddSpineOfReset()
	{
		this.m_fx_reset = FXSpineManager.Instance.ReplaySpine(this.m_fx_reset, 3025, base.get_transform(), "RoleTalentUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteSpineOfReset()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fx_reset, true);
	}
}
