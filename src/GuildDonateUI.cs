using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildDonateUI : UIBase
{
	private GameObject noEquipGoodsObj;

	private ButtonCustom btnOk;

	private ListPool bagListPool;

	private List<Goods> selectGoods;

	private Text titleNameText;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, false);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.titleNameText = base.FindTransform("TitleName").GetComponent<Text>();
		this.noEquipGoodsObj = base.FindTransform("NoEquipGoodsRoot").get_gameObject();
		this.selectGoods = new List<Goods>();
		this.btnOk = base.FindTransform("BtnOk").GetComponent<ButtonCustom>();
		this.btnOk.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOk);
		this.bagListPool = base.FindTransform("GridList").GetComponent<ListPool>();
		this.bagListPool.Clear();
		base.FindTransform("NoEquipGoodsTipText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(621605, false));
		if (this.noEquipGoodsObj != null && this.noEquipGoodsObj.get_activeSelf())
		{
			this.noEquipGoodsObj.SetActive(false);
		}
		this.titleNameText.set_text("选择捐献装备");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
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
		List<Goods> list = new List<Goods>();
		if (BackpackManager.Instance.EquimentGoods == null)
		{
			return;
		}
		for (int i = 0; i < BackpackManager.Instance.EquimentGoods.get_Count(); i++)
		{
			Goods goods = BackpackManager.Instance.EquimentGoods.get_Item(i);
			long longId = goods.GetLongId();
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(longId, 1f);
			int color = goods.LocalItem.color;
			EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(longId);
			if (equipSimpleInfoByEquipID != null && !equipSimpleInfoByEquipID.binding && excellentAttrsCountByColor >= 2 && color >= 5)
			{
				list.Add(goods);
			}
		}
		this.UpdateStorageBagList(list);
	}

	private void UpdateStorageBagList(List<Goods> donateGoodList)
	{
		this.bagListPool.Clear();
		if (donateGoodList == null || donateGoodList.get_Count() <= 0)
		{
			if (this.noEquipGoodsObj != null && !this.noEquipGoodsObj.get_activeSelf())
			{
				this.noEquipGoodsObj.SetActive(true);
			}
			return;
		}
		if (this.noEquipGoodsObj != null && this.noEquipGoodsObj.get_activeSelf())
		{
			this.noEquipGoodsObj.SetActive(false);
		}
		this.bagListPool.Create(donateGoodList.get_Count(), delegate(int index)
		{
			if (index < donateGoodList.get_Count() && index < this.bagListPool.Items.get_Count())
			{
				PetID component = this.bagListPool.Items.get_Item(index).GetComponent<PetID>();
				if (component != null)
				{
					int itemId = donateGoodList.get_Item(index).GetItemId();
					long longId = donateGoodList.get_Item(index).GetLongId();
					component.get_gameObject().SetActive(true);
					component.SetEquipItemData(itemId, longId, SelectImgType.Check);
					component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectEquipItem);
				}
			}
		});
	}

	private void OnClickOk(GameObject go)
	{
		if (this.selectGoods == null || this.selectGoods.get_Count() <= 0)
		{
			this.Show(false);
			return;
		}
		int num = 0;
		if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStorageInfo.baseInfo != null)
		{
			num = GuildStorageManager.Instance.GuildStorageInfo.baseInfo.capacity - GuildStorageManager.Instance.GuildStorageInfo.baseInfo.size;
		}
		if (this.selectGoods.get_Count() > num)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621608, false));
			return;
		}
		List<int> list = new List<int>();
		List<EquipBriefInfo> m_equips = new List<EquipBriefInfo>();
		for (int i = 0; i < this.selectGoods.get_Count(); i++)
		{
			EquipBriefInfo equipBriefInfo = new EquipBriefInfo();
			equipBriefInfo.equipId = this.selectGoods.get_Item(i).GetLongId();
			if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(this.selectGoods.get_Item(i).GetItemId()))
			{
				equipBriefInfo.position = DataReader<zZhuangBeiPeiZhiBiao>.Get(this.selectGoods.get_Item(i).GetItemId()).position;
			}
			list.Add(this.selectGoods.get_Item(i).GetItemId());
			m_equips.Add(equipBriefInfo);
		}
		int equipsTotalPoint = GuildStorageManager.Instance.GetEquipsTotalPoint(list, true);
		string content = string.Format(GameDataUtils.GetChineseContent(621602, false), equipsTotalPoint);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
		{
			GuildStorageManager.Instance.SendGuildStorageDonateEquipReq(m_equips);
			this.Show(false);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickSelectEquipItem(GameObject go)
	{
		PetID equipItem = go.GetComponent<PetID>();
		if (equipItem != null)
		{
			equipItem.Selected = !equipItem.Selected;
			bool selected = equipItem.Selected;
			int num = this.selectGoods.FindIndex((Goods a) => a.GetLongId() == equipItem.EquipID);
			if (num >= 0 && !selected)
			{
				this.selectGoods.RemoveAt(num);
			}
			else if (num < 0 && selected)
			{
				Goods goods = BackpackManager.Instance.OnGetGood(equipItem.EquipID);
				if (goods != null)
				{
					this.selectGoods.Add(goods);
				}
			}
		}
	}
}
