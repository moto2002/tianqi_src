using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailedPopUI : UIBase
{
	public static EquipDetailedPopUI Instance;

	private List<Transform> equipCellTransList;

	private Transform equipCellParentTrans;

	private int CurrentSelectID;

	private Transform HaveEquips;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		EquipDetailedPopUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.equipCellTransList = new List<Transform>();
		this.equipCellParentTrans = base.FindTransform("Contair");
		this.HaveEquips = base.FindTransform("HaveEquips");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.FindTransform("ListViewEquipLib").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.AddListener<bool>(EventNames.OnGetEquipAdvancedRes, new Callback<bool>(this.OnGetEquipAdvancedRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.RemoveListener<bool>(EventNames.OnGetEquipAdvancedRes, new Callback<bool>(this.OnGetEquipAdvancedRes));
	}

	private void EquipEquipmentSucess()
	{
		this.Show(false);
	}

	private void OnGetEquipAdvancedRes(bool haveChange)
	{
		this.Show(false);
	}

	private void OnClickSelectEquipItem(GameObject go)
	{
		if (this.CurrentSelectID >= 0)
		{
			this.equipCellTransList.get_Item(this.CurrentSelectID).GetComponent<PetID>().Selected = false;
		}
		int currentSelectID = this.equipCellTransList.FindIndex((Transform a) => a.get_gameObject() == go);
		PetID component = go.GetComponent<PetID>();
		component.Selected = true;
		this.CurrentSelectID = currentSelectID;
		this.SelectCenterEquipTip(component.EquipID, false);
	}

	private void UpdateGoods(EquipLibType.ELT pos)
	{
		this.HaveEquips.get_gameObject().SetActive(true);
		for (int i = 0; i < this.equipCellParentTrans.get_childCount(); i++)
		{
			Transform child = this.equipCellParentTrans.GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
		this.equipCellTransList.Clear();
		List<EquipSimpleInfo> list = new List<EquipSimpleInfo>();
		List<KeyValuePair<long, int>> list2 = new List<KeyValuePair<long, int>>();
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return;
		}
		for (int j = 0; j < equipLib.equips.get_Count(); j++)
		{
			EquipSimpleInfo equipSimpleInfo = equipLib.equips.get_Item(j);
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
			if (zZhuangBeiPeiZhiBiao != null)
			{
				if (equipSimpleInfo.equipId != equipLib.wearingId)
				{
					int equipFightingByEquipID = EquipmentManager.Instance.GetEquipFightingByEquipID(equipSimpleInfo.equipId);
					if (zZhuangBeiPeiZhiBiao.position == (int)pos && zZhuangBeiPeiZhiBiao.level <= EntityWorld.Instance.EntSelf.Lv && (zZhuangBeiPeiZhiBiao.occupation == 0 || zZhuangBeiPeiZhiBiao.occupation == EntityWorld.Instance.EntSelf.TypeID))
					{
						KeyValuePair<long, int> keyValuePair = new KeyValuePair<long, int>(equipSimpleInfo.equipId, equipFightingByEquipID);
						list2.Add(keyValuePair);
					}
				}
			}
		}
		list2.Sort(new Comparison<KeyValuePair<long, int>>(EquipDetailedPopUI.FightingSortCompare));
		for (int k = 0; k < list2.get_Count(); k++)
		{
			long equipID = list2.get_Item(k).get_Key();
			int num = equipLib.equips.FindIndex((EquipSimpleInfo a) => a.equipId == equipID);
			if (num >= 0)
			{
				list.Add(equipLib.equips.get_Item(num));
			}
		}
		int num2 = 0;
		for (int l = 0; l < list.get_Count(); l++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
			num2++;
			instantiate2Prefab.set_name("EquipItem_" + num2);
			PetID component = instantiate2Prefab.GetComponent<PetID>();
			instantiate2Prefab.get_transform().SetParent(this.equipCellParentTrans);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectEquipItem);
			component.SetEquipItemData(list.get_Item(l).cfgId, list.get_Item(l).equipId, SelectImgType.HighLight);
			this.equipCellTransList.Add(instantiate2Prefab.get_transform());
		}
		if (list.get_Count() > 0)
		{
			this.CurrentSelectID = 0;
			this.equipCellTransList.get_Item(this.CurrentSelectID).GetComponent<PetID>().Selected = true;
			this.SelectCenterEquipTip(this.equipCellTransList.get_Item(this.CurrentSelectID).GetComponent<PetID>().EquipID, false);
		}
		else
		{
			this.SelectCenterEquipTip(0L, true);
		}
		int num3 = (list == null) ? 6 : (6 - list.get_Count());
		if (num3 > 0)
		{
			for (int m = 0; m < num3; m++)
			{
				GameObject instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
				num2++;
				instantiate2Prefab2.set_name("EquipItem_" + num2);
				PetID component2 = instantiate2Prefab2.GetComponent<PetID>();
				instantiate2Prefab2.get_transform().SetParent(this.equipCellParentTrans);
				instantiate2Prefab2.SetActive(true);
				instantiate2Prefab2.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				component2.SetItemFrameMask();
			}
		}
	}

	private void SelectCenterEquipTip(long equip_uuid, bool noEquip = false)
	{
		Transform transform = base.FindTransform("RightTip");
		if (transform.get_childCount() > 0)
		{
			EquipItemTipUI component = transform.GetChild(0).GetComponent<EquipItemTipUI>();
			component.GetComponent<EquipItemTipUI>().RefreshUI(equip_uuid, false, noEquip, false, 3000);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipItemTipUI");
			instantiate2Prefab.get_transform().SetParent(transform);
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
			instantiate2Prefab.GetComponent<EquipItemTipUI>().RefreshUI(equip_uuid, false, noEquip, false, 3000);
		}
	}

	private void SetHasEquipedTip(EquipLibType.ELT pos, bool isShowStrength = false)
	{
		Transform transform = base.FindTransform("LeftTip");
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib != null)
		{
			if (transform.get_childCount() > 0)
			{
				EquipItemTipUI component = transform.GetChild(0).GetComponent<EquipItemTipUI>();
				component.GetComponent<EquipItemTipUI>().RefreshUI(equipLib.wearingId, true, false, isShowStrength, 3000);
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipItemTipUI");
				instantiate2Prefab.get_transform().SetParent(transform);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				instantiate2Prefab.SetActive(true);
				instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
				instantiate2Prefab.GetComponent<EquipItemTipUI>().RefreshUI(equipLib.wearingId, true, false, isShowStrength, 3000);
			}
		}
	}

	public void SetSelectEquipTip(EquipLibType.ELT pos, bool isShowStrength = false)
	{
		this.SetHasEquipedTip(pos, isShowStrength);
		this.UpdateGoods(pos);
	}

	public void SetBuildSuccess(long equip_uuid)
	{
		int key = BackpackManager.Instance.OnGetGoodItemId(equip_uuid);
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(key);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			this.SetHasEquipedTip((EquipLibType.ELT)zZhuangBeiPeiZhiBiao.position, false);
			this.SelectCenterEquipTip(equip_uuid, false);
			this.HaveEquips.get_gameObject().SetActive(false);
		}
	}

	private static int FightingSortCompare(KeyValuePair<long, int> AF1, KeyValuePair<long, int> AF2)
	{
		int result = 0;
		if (AF1.get_Value() > AF2.get_Value())
		{
			result = -1;
		}
		return result;
	}
}
