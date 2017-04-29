using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;

public class EquipCompareTipUI : UIBase
{
	private Transform leftTipTrans;

	private Transform rightTipTrans;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isEndNav = false;
		this.isInterruptStick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.leftTipTrans = base.FindTransform("leftTip");
		this.rightTipTrans = base.FindTransform("rightTip");
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void RefreshUI(WearEquipInfo equipData, int depthValue = 3000)
	{
		if (equipData == null)
		{
			return;
		}
		EquipItemTipUI component = this.rightTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
		if (component != null)
		{
			component.RefreshUIByWearingInfo(equipData, depthValue);
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipData.id);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			EquipLibType.ELT pos = (EquipLibType.ELT)zZhuangBeiPeiZhiBiao.position;
			EquipItemTipUI component2 = this.leftTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
			EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
			component2.RefreshUIByEquipID(equipLib.wearingId, depthValue);
		}
	}

	public void RefreshUI(int itemID, long equipID, int depthValue = 3000)
	{
		EquipItemTipUI component = this.rightTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			component.RefreshUIByEquipID(equipID, depthValue);
		}
		else
		{
			component.RefreshUIByItemID(itemID, depthValue);
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(itemID);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			EquipLibType.ELT pos = (EquipLibType.ELT)zZhuangBeiPeiZhiBiao.position;
			EquipItemTipUI component2 = this.leftTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
			EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
			component2.RefreshUIByEquipID(equipLib.wearingId, depthValue);
		}
	}

	public void RefreshUI(int itemID, int depthValue = 3000)
	{
		EquipItemTipUI component = this.rightTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
		long num = BackpackManager.Instance.OnGetGoodLongId(itemID);
		if (EquipmentManager.Instance.dicEquips.ContainsKey(num))
		{
			component.RefreshUIByEquipID(num, depthValue);
		}
		else
		{
			component.RefreshUIByItemID(itemID, depthValue);
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(itemID);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			EquipLibType.ELT pos = (EquipLibType.ELT)zZhuangBeiPeiZhiBiao.position;
			EquipItemTipUI component2 = this.leftTipTrans.FindChild("EquipItemTipUI").GetComponent<EquipItemTipUI>();
			EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
			component2.RefreshUIByEquipID(equipLib.wearingId, depthValue);
		}
	}

	public void BuyItemShow(int itemID, int costType, int costNum)
	{
	}
}
