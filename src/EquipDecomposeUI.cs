using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDecomposeUI : UIBase
{
	private ListPool equipStepListPool;

	private List<Transform> qualityTransList;

	private Dictionary<int, Transform> equipStepTransDic;

	private Dictionary<int, bool> selectIndexDic;

	private Dictionary<int, bool> selectEquipStepDic;

	private List<int> equipStepList;

	private bool isAllowDecSuit;

	private GameObject allowToDecomposeSuitObj;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.selectIndexDic = new Dictionary<int, bool>();
		this.qualityTransList = new List<Transform>();
		for (int i = 1; i <= 6; i++)
		{
			Transform transform = base.FindTransform("equipQuality" + i);
			transform.FindChild("SelectBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectBtn);
			this.qualityTransList.Add(transform);
		}
		base.FindTransform("BtnRight").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSure);
		base.FindTransform("AllowToDecomposeSuit").FindChild("SelectBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAllowDecSuitBtn);
		this.allowToDecomposeSuitObj = base.FindTransform("AllowToDecomposeSuit").FindChild("SelectBtn").get_gameObject();
		this.equipStepListPool = base.FindTransform("EquipStepListPool").GetComponent<ListPool>();
		this.equipStepTransDic = new Dictionary<int, Transform>();
		this.selectEquipStepDic = new Dictionary<int, bool>();
		this.equipStepList = new List<int>();
		this.equipStepListPool.Clear();
	}

	protected override void OnEnable()
	{
		this.isAllowDecSuit = true;
		this.OnClickAllowDecSuitBtn(this.allowToDecomposeSuitObj);
		this.RefreshUI();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		List<Goods> equimentGoods = BackpackManager.Instance.EquimentGoods;
		this.equipStepList.Clear();
		for (int i = 0; i < equimentGoods.get_Count(); i++)
		{
			int equipCfgIDByEquipID = EquipGlobal.GetEquipCfgIDByEquipID(equimentGoods.get_Item(i).GetLongId());
			if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgIDByEquipID))
			{
				zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByEquipID);
				if (!this.equipStepList.Contains(zZhuangBeiPeiZhiBiao.step))
				{
					this.equipStepList.Add(zZhuangBeiPeiZhiBiao.step);
				}
			}
		}
		this.UpdateEquipStepList();
	}

	private void UpdateEquipStepList()
	{
		this.equipStepListPool.Clear();
		this.equipStepListPool.Create(this.equipStepList.get_Count(), delegate(int index)
		{
			if (index < this.equipStepList.get_Count() && index <= this.equipStepListPool.Items.get_Count())
			{
				Transform transform = this.equipStepListPool.Items.get_Item(index).get_transform();
				if (transform != null)
				{
					transform.FindChild("equipStepText").GetComponent<Text>().set_text(this.equipStepList.get_Item(index) + "阶装备");
					transform.FindChild("SelectBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectStepBtn);
					transform.FindChild("SelectBtn").FindChild("selectImg").get_gameObject().SetActive(true);
					transform.set_name("EquipStep" + this.equipStepList.get_Item(index));
					if (!this.equipStepTransDic.ContainsKey(this.equipStepList.get_Item(index)))
					{
						this.equipStepTransDic.Add(this.equipStepList.get_Item(index), transform.FindChild("SelectBtn"));
					}
					if (!this.selectEquipStepDic.ContainsKey(this.equipStepList.get_Item(index)))
					{
						this.selectEquipStepDic.Add(this.equipStepList.get_Item(index), true);
					}
				}
				if (index == this.equipStepList.get_Count() - 1)
				{
					this.UpdateEquipQualityList();
				}
			}
		});
	}

	private void UpdateEquipQualityList()
	{
		List<int> list = new List<int>();
		List<Goods> equimentGoods = BackpackManager.Instance.EquimentGoods;
		List<int> list2 = new List<int>();
		using (Dictionary<int, bool>.Enumerator enumerator = this.selectEquipStepDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, bool> current = enumerator.get_Current();
				if (current.get_Value())
				{
					list2.Add(current.get_Key());
				}
			}
		}
		for (int i = 0; i < equimentGoods.get_Count(); i++)
		{
			int color = equimentGoods.get_Item(i).GetItem().color;
			int equipCfgIDByEquipID = EquipGlobal.GetEquipCfgIDByEquipID(equimentGoods.get_Item(i).GetLongId());
			EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equimentGoods.get_Item(i).GetLongId());
			if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgIDByEquipID))
			{
				zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByEquipID);
				for (int k = 0; k < list2.get_Count(); k++)
				{
					if (zZhuangBeiPeiZhiBiao.step == list2.get_Item(k) && !list.Contains(color) && (this.isAllowDecSuit || (!this.isAllowDecSuit && equipSimpleInfoByEquipID != null && equipSimpleInfoByEquipID.suitId <= 0)))
					{
						list.Add(color);
					}
				}
			}
		}
		int j;
		for (j = 0; j < this.qualityTransList.get_Count(); j++)
		{
			int num = list.FindIndex((int a) => a == j + 1);
			if (num >= 0)
			{
				this.qualityTransList.get_Item(j).get_gameObject().SetActive(true);
				if (!this.selectIndexDic.ContainsKey(j + 1))
				{
					this.selectIndexDic.Add(j + 1, false);
				}
			}
			else
			{
				this.qualityTransList.get_Item(j).get_gameObject().SetActive(false);
			}
		}
	}

	protected override void OnClickMaskAction()
	{
		if (!this.isClick)
		{
			return;
		}
		this.Show(false);
		SoundManager.PlayUI(10037, false);
	}

	private void OnClickAllowDecSuitBtn(GameObject go)
	{
		this.isAllowDecSuit = !this.isAllowDecSuit;
		go.get_transform().FindChild("selectImg").get_gameObject().SetActive(this.isAllowDecSuit);
		this.UpdateEquipQualityList();
	}

	private void OnClickSelectStepBtn(GameObject go)
	{
		int num = 0;
		using (Dictionary<int, Transform>.Enumerator enumerator = this.equipStepTransDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, Transform> current = enumerator.get_Current();
				if (go == current.get_Value().get_gameObject())
				{
					num = current.get_Key();
				}
			}
		}
		if (this.selectEquipStepDic.ContainsKey(num))
		{
			this.selectEquipStepDic.set_Item(num, !this.selectEquipStepDic.get_Item(num));
			bool active = this.selectEquipStepDic.get_Item(num);
			go.get_transform().FindChild("selectImg").get_gameObject().SetActive(active);
			this.UpdateEquipQualityList();
		}
	}

	private void OnClickSelectBtn(GameObject go)
	{
		int num = this.qualityTransList.FindIndex((Transform a) => a == go.get_transform().get_parent());
		if (num >= 0)
		{
			if (!this.selectIndexDic.ContainsKey(num + 1))
			{
				return;
			}
			if (!this.selectIndexDic.get_Item(num + 1))
			{
				this.qualityTransList.get_Item(num).FindChild("SelectBtn").FindChild("selectImg").get_gameObject().SetActive(true);
				this.selectIndexDic.set_Item(num + 1, true);
			}
			else
			{
				this.qualityTransList.get_Item(num).FindChild("SelectBtn").FindChild("selectImg").get_gameObject().SetActive(false);
				this.selectIndexDic.set_Item(num + 1, false);
			}
		}
	}

	private void OnClickSure(GameObject go)
	{
		bool flag = false;
		List<int> list = new List<int>();
		using (Dictionary<int, bool>.Enumerator enumerator = this.selectEquipStepDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, bool> current = enumerator.get_Current();
				if (current.get_Value())
				{
					list.Add(current.get_Key());
				}
			}
		}
		List<int> list2 = new List<int>();
		using (Dictionary<int, bool>.Enumerator enumerator2 = this.selectIndexDic.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<int, bool> current2 = enumerator2.get_Current();
				if (current2.get_Value())
				{
					list2.Add(current2.get_Key());
				}
			}
		}
		List<Goods> equimentGoods = BackpackManager.Instance.EquimentGoods;
		List<Goods> list3 = new List<Goods>();
		for (int i = 0; i < equimentGoods.get_Count(); i++)
		{
			for (int j = 0; j < list.get_Count(); j++)
			{
				if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equimentGoods.get_Item(i).GetItemId()))
				{
					zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equimentGoods.get_Item(i).GetItemId());
					if (zZhuangBeiPeiZhiBiao.step == list.get_Item(j))
					{
						list3.Add(equimentGoods.get_Item(i));
					}
				}
			}
		}
		Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
		for (int k = 0; k < list3.get_Count(); k++)
		{
			EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(list3.get_Item(k).GetLongId());
			zZhuangBeiPeiZhiBiao equipCfgDataByEquipID = EquipGlobal.GetEquipCfgDataByEquipID(list3.get_Item(k).GetLongId());
			for (int l = 0; l < list2.get_Count(); l++)
			{
				if (list3.get_Item(k).GetItem().color == list2.get_Item(l) && (this.isAllowDecSuit || (!this.isAllowDecSuit && equipSimpleInfoByEquipID != null && equipSimpleInfoByEquipID.suitId <= 0)))
				{
					if ((equipCfgDataByEquipID.step > 3 || list2.get_Item(l) > 3) && !flag)
					{
						flag = true;
					}
					if (equipCfgDataByEquipID != null && dictionary.ContainsKey(equipCfgDataByEquipID.position))
					{
						List<long> list4 = dictionary.get_Item(equipCfgDataByEquipID.position);
						list4.Add(list3.get_Item(k).GetLongId());
					}
					else if (equipCfgDataByEquipID != null && !dictionary.ContainsKey(equipCfgDataByEquipID.position))
					{
						List<long> list5 = new List<long>();
						list5.Add(list3.get_Item(k).GetLongId());
						dictionary.Add(equipCfgDataByEquipID.position, list5);
					}
				}
			}
		}
		List<DecomposeEquipInfo> decomposeList = new List<DecomposeEquipInfo>();
		using (Dictionary<int, List<long>>.Enumerator enumerator3 = dictionary.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<int, List<long>> current3 = enumerator3.get_Current();
				DecomposeEquipInfo decomposeEquipInfo = new DecomposeEquipInfo();
				decomposeEquipInfo.position = current3.get_Key();
				decomposeEquipInfo.equipIds.Clear();
				decomposeEquipInfo.equipIds.AddRange(current3.get_Value());
				decomposeList.Add(decomposeEquipInfo);
			}
		}
		if (flag)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("分解装备", "该装备属于贵重装备，是否确认分解", null, delegate
			{
				EquipmentManager.Instance.SendDecomposeEquipReq(decomposeList);
				this.Show(false);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			EquipmentManager.Instance.SendDecomposeEquipReq(decomposeList);
			this.Show(false);
		}
	}

	private void OnClickCancel(GameObject go)
	{
		this.Show(false);
	}
}
