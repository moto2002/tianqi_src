using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShopItem : MonoBehaviour
{
	private Image ImgIcon;

	private Transform SItem;

	private Text TextName;

	private Text TextCount;

	private Text TextNeedCount;

	private Text TextReputation;

	private int IconItemId;

	public int OnlyId;

	public int ReputationValue;

	public int StockCount;

	private List<NpcExchangeInfo> ExchangeData;

	public GameObject ImageState1;

	public GameObject ImageState2;

	public GameObject ImageState3;

	public GameObject SelectPicture;

	public Transform ImgIconGameObject;

	public GameObject ExcellentAttrIconList;

	public bool IsCanClick = true;

	private void Awake()
	{
		this.SItem = base.get_transform().FindChild("Abg").FindChild("SItem");
		this.ImgIcon = this.SItem.FindChild("SItemIcon").FindChild("SImageIcon").GetComponent<Image>();
		this.TextName = this.SItem.FindChild("Name").GetComponent<Text>();
		this.TextCount = this.SItem.FindChild("Count").GetComponent<Text>();
		this.TextNeedCount = this.SItem.FindChild("SItemIcon").FindChild("STextNum").GetComponent<Text>();
		this.TextReputation = this.SItem.FindChild("Reputation").GetComponent<Text>();
		this.SItem.FindChild("SItemIcon").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDetal);
		base.get_transform().FindChild("BtnRect").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
		this.ImageState1 = this.SItem.FindChild("ImageState1").get_gameObject();
		this.ImageState2 = this.SItem.FindChild("ImageState2").get_gameObject();
		this.ImageState3 = this.SItem.FindChild("ImageState3").get_gameObject();
		this.SelectPicture = this.SItem.FindChild("SelectPicture").get_gameObject();
		this.ImgIconGameObject = this.SItem.FindChild("SItemIcon").FindChild("SImageIcon").get_transform();
		this.ExcellentAttrIconList = this.SItem.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").get_gameObject();
	}

	private void OnEnable()
	{
	}

	private void OnClickBtnDetal(GameObject go)
	{
		ItemTipUIViewModel.ShowItem(this.IconItemId, null);
	}

	private void OnClickGet(GameObject go)
	{
		this.SelectItem();
	}

	public void SelectItem()
	{
		if (this.IsCanClick)
		{
			TransactionNPCManager.Instance.ReFreshItem();
			this.SelectPicture.SetActive(true);
			EventDispatcher.Broadcast<List<NpcExchangeInfo>, int>(EventNames.RefreshNpcShopExchangeItem, this.ExchangeData, this.OnlyId);
		}
	}

	public void UpdateItem(int onlyId, int id, int itemCount, int count, int reputation, List<NpcExchangeInfo> data)
	{
		this.HideGY();
		this.IconItemId = id;
		this.OnlyId = onlyId;
		this.ReputationValue = reputation;
		this.StockCount = count;
		for (int i = 0; i < this.ImgIconGameObject.get_childCount(); i++)
		{
			Object.Destroy(this.ImgIconGameObject.GetChild(i).get_gameObject());
		}
		GameObject gameObject = ItemShow.ShowItem(this.ImgIconGameObject, id, -1L, false, null, 2001);
		this.TextName.set_text(GameDataUtils.GetItemName(id, false, 0L));
		string text = string.Empty;
		string text2 = string.Empty;
		this.ImageState1.SetActive(false);
		this.ImageState2.SetActive(false);
		this.ImageState3.SetActive(false);
		if (count == -1)
		{
			text = "声望要求：" + reputation.ToString();
			if (EntityWorld.Instance.EntSelf.Reputation >= (long)reputation)
			{
				bool flag = this.CheckItemEnough(data);
				if (flag)
				{
					this.ImageState2.SetActive(true);
				}
			}
		}
		else
		{
			if (count < 1)
			{
				this.ImageState3.SetActive(true);
			}
			else if (EntityWorld.Instance.EntSelf.Reputation >= (long)reputation)
			{
				bool flag2 = this.CheckItemEnough(data);
				if (flag2)
				{
					this.ImageState2.SetActive(true);
				}
			}
			text = "剩余库存：" + count.ToString();
			text2 = "声望要求：" + reputation.ToString();
		}
		this.TextCount.set_text(text);
		this.TextReputation.set_text(text2);
		this.TextNeedCount.set_text(itemCount.ToString());
		this.ExchangeData = data;
		this.IsCanClick = true;
	}

	private bool CheckItemEnough(List<NpcExchangeInfo> data)
	{
		for (int i = 0; i < data.get_Count(); i++)
		{
			int cfgId = data.get_Item(i).cfgId;
			long num = long.Parse(data.get_Item(i).count.ToString());
			long num2;
			if (cfgId == -1)
			{
				EquipParamInfo equipParams = data.get_Item(i).equipParams;
				List<EquipSimpleInfo> nPCShopEquipsData = EquipGlobal.GetNPCShopEquipsData(equipParams.step, equipParams.quality, equipParams.position, equipParams.betterQuality);
				num2 = long.Parse(nPCShopEquipsData.get_Count().ToString());
			}
			else
			{
				num2 = BackpackManager.Instance.OnGetGoodCount(cfgId);
			}
			if (num2 < num)
			{
				return false;
			}
		}
		return true;
	}

	public void UpdateExchangeItem(NpcExchangeInfo itemData)
	{
		this.HideGY();
		this.IconItemId = itemData.cfgId;
		long num;
		if (itemData.cfgId == -1)
		{
			EquipParamInfo equipParams = itemData.equipParams;
			List<EquipSimpleInfo> nPCShopEquipsData = EquipGlobal.GetNPCShopEquipsData(equipParams.step, equipParams.quality, equipParams.position, equipParams.betterQuality);
			num = long.Parse(nPCShopEquipsData.get_Count().ToString());
			if (num > 0L)
			{
				this.IconItemId = int.Parse(nPCShopEquipsData.get_Item(0).equipId.ToString());
			}
			else
			{
				List<zZhuangBeiPeiZhiBiao> dataList = DataReader<zZhuangBeiPeiZhiBiao>.DataList;
				for (int i = 0; i < dataList.get_Count(); i++)
				{
					if (dataList.get_Item(i).occupation == EntityWorld.Instance.EntSelf.TypeID && dataList.get_Item(i).step == equipParams.step && dataList.get_Item(i).position == equipParams.position)
					{
						Items items = DataReader<Items>.Get(dataList.get_Item(i).id);
						if (items != null && items.color == equipParams.quality)
						{
							this.IconItemId = dataList.get_Item(i).id;
							break;
						}
					}
				}
			}
		}
		else
		{
			num = BackpackManager.Instance.OnGetGoodCount(this.IconItemId);
		}
		string text = "背包剩余：" + num.ToString();
		long num2 = long.Parse(itemData.count.ToString());
		this.TextCount.set_text(text);
		this.TextNeedCount.set_text(itemData.count.ToString());
		this.TextReputation.set_text(string.Empty);
		if (this.IconItemId > 0)
		{
			for (int j = 0; j < this.ImgIconGameObject.get_childCount(); j++)
			{
				Object.Destroy(this.ImgIconGameObject.GetChild(j).get_gameObject());
			}
			GameObject gameObject = ItemShow.ShowItem(this.ImgIconGameObject, this.IconItemId, -1L, false, null, 2001);
			if (itemData.equipParams != null)
			{
				this.ShowGY(itemData.equipParams.betterQuality);
			}
			this.TextName.set_text(GameDataUtils.GetItemName(this.IconItemId, false, 0L));
		}
		this.IsCanClick = false;
		if (num < num2)
		{
			this.ImageState1.SetActive(true);
		}
		else
		{
			this.ImageState1.SetActive(false);
		}
		this.ImageState2.SetActive(false);
		this.ImageState3.SetActive(false);
	}

	private void ShowGY(int betterQuality)
	{
		this.ExcellentAttrIconList.SetActive(true);
		this.ExcellentAttrIconList.get_transform().FindChild("Image1").get_gameObject().SetActive(betterQuality >= 1);
		this.ExcellentAttrIconList.get_transform().FindChild("Image2").get_gameObject().SetActive(betterQuality >= 2);
		this.ExcellentAttrIconList.get_transform().FindChild("Image3").get_gameObject().SetActive(betterQuality >= 3);
	}

	private void HideGY()
	{
		this.ExcellentAttrIconList.SetActive(false);
		this.ExcellentAttrIconList.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
		this.ExcellentAttrIconList.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
		this.ExcellentAttrIconList.get_transform().FindChild("Image3").get_gameObject().SetActive(false);
	}

	public void ResetState()
	{
		this.SelectPicture.SetActive(false);
	}

	public void ReFreshItemCount(int count, int onlyId)
	{
		if (onlyId == this.OnlyId)
		{
			this.StockCount = count;
		}
		if (this.StockCount != -1)
		{
			if (this.StockCount < 1)
			{
				this.ImageState3.SetActive(true);
			}
			this.TextCount.set_text("剩余库存：" + this.StockCount.ToString());
		}
		this.ImageState1.SetActive(false);
		this.ImageState2.SetActive(false);
		this.ImageState3.SetActive(false);
		if (this.StockCount == -1)
		{
			if (EntityWorld.Instance.EntSelf.Reputation >= (long)this.ReputationValue)
			{
				bool flag = this.CheckItemEnough(this.ExchangeData);
				if (flag)
				{
					this.ImageState2.SetActive(true);
				}
			}
		}
		else if (this.StockCount < 1)
		{
			this.ImageState3.SetActive(true);
		}
		else if (EntityWorld.Instance.EntSelf.Reputation >= (long)this.ReputationValue)
		{
			bool flag2 = this.CheckItemEnough(this.ExchangeData);
			if (flag2)
			{
				this.ImageState2.SetActive(true);
			}
		}
		if (this.IsCanClick && onlyId == this.OnlyId)
		{
			EventDispatcher.Broadcast<List<NpcExchangeInfo>, int>(EventNames.RefreshNpcShopExchangeItem, this.ExchangeData, this.OnlyId);
		}
	}
}
