using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodWeaponChallengeUI : UIBase
{
	private Text mTxTitle;

	private Text mTxTips;

	private Text mTxPower;

	private GameObject mItemGrid;

	private Artifact mGWData;

	private List<GodWeaponEquipItem> mItemList;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mTxTitle = base.FindTransform("titleName").GetComponent<Text>();
		this.mTxTips = base.FindTransform("txTips").GetComponent<Text>();
		this.mTxPower = base.FindTransform("txPower").GetComponent<Text>();
		this.mItemGrid = base.FindTransform("Grid").get_gameObject();
		this.mItemList = new List<GodWeaponEquipItem>();
		base.FindTransform("BtnLeft").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
		base.FindTransform("BtnRight").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChallenge);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	public void Open(Artifact data)
	{
		this.mGWData = data;
		this.mTxTitle.set_text("挑战提示");
		this.mTxTips.set_text("当前战斗力不足，挑战比较危险！");
		TuiJianZhuangBei tuiJianZhuangBei = DataReader<TuiJianZhuangBei>.Get(this.mGWData.id);
		if (tuiJianZhuangBei != null)
		{
			this.mTxPower.set_text(TextColorMgr.GetColor("推荐战斗力：" + tuiJianZhuangBei.recommendedPower, (EntityWorld.Instance.EntSelf.Fighting >= (long)tuiJianZhuangBei.recommendedPower) ? "00c800" : "ee0000", string.Empty));
		}
		this.ClearItem();
		List<zZhuangBeiPeiZhiBiao> recommendEquipsData = GodWeaponManager.Instance.GetRecommendEquipsData(this.mGWData.id);
		for (int i = 0; i < recommendEquipsData.get_Count(); i++)
		{
			zZhuangBeiPeiZhiBiao equipCfgDataByPos = EquipGlobal.GetEquipCfgDataByPos((EquipLibType.ELT)GodWeaponManager.Instance.EQUIP_TYPE[i]);
			if (equipCfgDataByPos != null)
			{
				this.CreateItem(recommendEquipsData.get_Item(i).id, equipCfgDataByPos.quality >= recommendEquipsData.get_Item(i).quality && equipCfgDataByPos.step >= recommendEquipsData.get_Item(i).step);
			}
		}
	}

	private void CreateItem(int id, bool isHave)
	{
		GodWeaponEquipItem godWeaponEquipItem = this.mItemList.Find((GodWeaponEquipItem e) => e.get_gameObject().get_name() == "Unused");
		if (godWeaponEquipItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GodWeaponEquipItem");
			UGUITools.SetParent(this.mItemGrid, instantiate2Prefab, true);
			godWeaponEquipItem = instantiate2Prefab.GetComponent<GodWeaponEquipItem>();
			this.mItemList.Add(godWeaponEquipItem);
		}
		godWeaponEquipItem.SetItem(id, isHave);
		godWeaponEquipItem.get_gameObject().set_name(id.ToString());
		godWeaponEquipItem.get_gameObject().SetActive(true);
	}

	private void OnClickChallenge(GameObject go)
	{
		GodWeaponManager.Instance.ChallengeDungeon(this.mGWData.battle, this.mGWData.model);
	}

	private void ClearItem()
	{
		for (int i = 0; i < this.mItemList.get_Count(); i++)
		{
			this.mItemList.get_Item(i).get_gameObject().set_name("Unused");
			this.mItemList.get_Item(i).get_gameObject().SetActive(false);
		}
	}
}
