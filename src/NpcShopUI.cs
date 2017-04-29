using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShopUI : UIBase
{
	public static NpcShopUI Instance;

	private GridLayoutGroup m_itemlist;

	private int OnlyId;

	private GameObject m_textTimeStr;

	private Text m_textUpdateTime;

	private Text m_reputation;

	private Image ImageGril;

	public List<KeyValuePair<string, int>> itemIdInfoss = new List<KeyValuePair<string, int>>();

	private TimeCountDown timeCoundDown;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		NpcShopUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_itemlist = base.FindTransform("LeftContent").FindChild("LeftGrid").GetComponent<GridLayoutGroup>();
		this.m_textTimeStr = base.FindTransform("TextTimeStr").get_gameObject();
		this.m_textUpdateTime = base.FindTransform("TextUpdateTime").GetComponent<Text>();
		this.m_reputation = base.FindTransform("TextSwValue").GetComponent<Text>();
		base.FindTransform("BtnEnsure").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
		base.FindTransform("CloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClosePanel);
		this.ImageGril = base.FindTransform("RawImageGril").GetComponent<Image>();
		base.FindTransform("LeftContent").GetComponent<ScrollRect>().set_horizontalNormalizedPosition(0f);
		this.m_reputation.set_text(string.Empty);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (EntityWorld.Instance.EntSelf != null && this.m_reputation != null)
		{
			this.m_reputation.set_text(EntityWorld.Instance.EntSelf.Reputation.ToString());
		}
		this.ResetBodyPos();
		this.SendMsg();
		this.InitTime();
		this.ChangeBodyImage();
	}

	protected override void OnDisable()
	{
		this.ClearTimeCoundDown();
		this.itemIdInfoss.Clear();
		base.FindTransform("LeftContent").GetComponent<ScrollRect>().set_horizontalNormalizedPosition(0f);
	}

	protected override void ActionClose()
	{
		base.ActionClose();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		this.OnCliseDo();
	}

	private void OnClosePanel(GameObject go)
	{
		this.OnCliseDo();
	}

	private void OnCliseDo()
	{
		this.ClearTimeCoundDown();
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<List<NpcExchangeInfo>, int>(EventNames.RefreshNpcShopExchangeItem, new Callback<List<NpcExchangeInfo>, int>(this.OnRefreshNpcShopExchangeItem));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<List<NpcExchangeInfo>, int>(EventNames.RefreshNpcShopExchangeItem, new Callback<List<NpcExchangeInfo>, int>(this.OnRefreshNpcShopExchangeItem));
	}

	private void OnClickGet(GameObject go)
	{
		if (this.OnlyId < 0)
		{
			return;
		}
		int currentShopId = TransactionNPCManager.Instance.CurrentShopId;
		TransactionNPCManager.Instance.SendShoppingReq(ShopType.ST.NpcShop, currentShopId, this.OnlyId, 1);
	}

	public void SendMsg()
	{
		int currentShopId = TransactionNPCManager.Instance.CurrentShopId;
		if (currentShopId < 0)
		{
			return;
		}
		TransactionNPCManager.Instance.SendNpcShopReq(currentShopId);
	}

	protected void OnRefreshNpcShopExchangeItem(List<NpcExchangeInfo> exchangeData, int onlyId)
	{
		this.ClearExchangeScroll();
		if (exchangeData == null || exchangeData.get_Count() < 1)
		{
			return;
		}
		for (int i = 0; i < exchangeData.get_Count(); i++)
		{
			this.UpdateItemInfo(i, exchangeData.get_Item(i));
		}
		this.OnlyId = onlyId;
	}

	private void ClearExchangeScroll()
	{
		for (int i = 1; i <= 3; i++)
		{
			string text = "ExchangeItem" + i.ToString();
			if (base.FindTransform("ExchangePanel").FindChild(text) != null)
			{
				base.FindTransform("ExchangePanel").FindChild(text).get_gameObject().SetActive(false);
			}
		}
		this.itemIdInfoss.Clear();
	}

	private void UpdateItemIdData(string index, int id)
	{
		if (id < 1)
		{
			return;
		}
		this.itemIdInfoss.Add(new KeyValuePair<string, int>(index, id));
	}

	private void UpdateItemInfo(int i, NpcExchangeInfo itemData)
	{
		i++;
		string text = "ExchangeItem" + i.ToString();
		if (base.FindTransform("ExchangePanel").FindChild(text) != null)
		{
			Transform transform = base.FindTransform("ExchangePanel").FindChild(text);
			transform.get_gameObject().SetActive(true);
			transform.FindChild("SItemIcon").FindChild("ImageState1").get_gameObject().SetActive(false);
			transform.FindChild("SItemIcon").FindChild("ImageState2").get_gameObject().SetActive(false);
			transform.FindChild("SItemIcon").FindChild("ImageState3").get_gameObject().SetActive(false);
			int num = itemData.cfgId;
			long num2;
			if (itemData.cfgId == -1)
			{
				EquipParamInfo equipParams = itemData.equipParams;
				List<EquipSimpleInfo> nPCShopEquipsData = EquipGlobal.GetNPCShopEquipsData(equipParams.step, equipParams.quality, equipParams.position, equipParams.betterQuality);
				num2 = long.Parse(nPCShopEquipsData.get_Count().ToString());
				if (num2 > 0L)
				{
					num = int.Parse(nPCShopEquipsData.get_Item(0).equipId.ToString());
				}
				else
				{
					List<zZhuangBeiPeiZhiBiao> dataList = DataReader<zZhuangBeiPeiZhiBiao>.DataList;
					for (int j = 0; j < dataList.get_Count(); j++)
					{
						if (dataList.get_Item(j).occupation == EntityWorld.Instance.EntSelf.TypeID && dataList.get_Item(j).step == equipParams.step && dataList.get_Item(j).position == equipParams.position)
						{
							Items items = DataReader<Items>.Get(dataList.get_Item(j).id);
							if (items != null && items.color == equipParams.quality)
							{
								num = dataList.get_Item(j).id;
								break;
							}
						}
					}
				}
			}
			else
			{
				num2 = BackpackManager.Instance.OnGetGoodCount(num);
			}
			string text2 = string.Format("({0}/{1})", itemData.count, num2);
			transform.FindChild("Count").GetComponent<Text>().set_text(text2);
			transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image1").get_gameObject().SetActive(false);
			transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image2").get_gameObject().SetActive(false);
			transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image3").get_gameObject().SetActive(false);
			if (num > 0)
			{
				Transform transform2 = transform.FindChild("SItemIcon").FindChild("SImageIcon").get_transform();
				if (transform2.get_childCount() > 0)
				{
					for (int k = 0; k < transform2.get_childCount(); k++)
					{
						Object.Destroy(transform2.GetChild(k).get_gameObject());
					}
				}
				ItemShow.ShowItem(transform2, num, -1L, false, null, 2001);
				if (itemData.equipParams != null)
				{
					transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image1").get_gameObject().SetActive(itemData.equipParams.betterQuality >= 1);
					transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image2").get_gameObject().SetActive(itemData.equipParams.betterQuality >= 2);
					transform.FindChild("SItemIcon").FindChild("ExcellentAttrIconList").FindChild("Image3").get_gameObject().SetActive(itemData.equipParams.betterQuality >= 3);
				}
				transform.FindChild("SItemIcon").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDetal);
			}
			this.UpdateItemIdData(text, num);
			if (num2 < (long)itemData.count)
			{
				transform.FindChild("SItemIcon").FindChild("ImageState1").get_gameObject().SetActive(true);
			}
			else
			{
				transform.FindChild("SItemIcon").FindChild("ImageState1").get_gameObject().SetActive(false);
			}
			transform.FindChild("SItemIcon").FindChild("ImageState2").get_gameObject().SetActive(false);
			transform.FindChild("SItemIcon").FindChild("ImageState3").get_gameObject().SetActive(false);
		}
	}

	private void OnClickBtnDetal(GameObject go)
	{
		string name = go.get_transform().get_parent().get_name();
		Debug.LogError("OnClickBtnDetal===" + name);
		if (this.itemIdInfoss == null || this.itemIdInfoss.get_Count() < 1)
		{
			return;
		}
		for (int i = 0; i < this.itemIdInfoss.get_Count(); i++)
		{
			if (this.itemIdInfoss.get_Item(i).get_Key() == name)
			{
				ItemTipUIViewModel.ShowItem(this.itemIdInfoss.get_Item(i).get_Value(), null);
			}
		}
	}

	public void RefreshUI(OpenNpcShopRes down = null)
	{
		WaitUI.CloseUI(0u);
		this.ClearScroll();
		if (down.shopInfo != null && down.shopInfo.goodsInfo != null)
		{
			int num = 0;
			for (int i = 0; i < down.shopInfo.goodsInfo.get_Count(); i++)
			{
				int itemId = down.shopInfo.goodsInfo.get_Item(i).itemId;
				Items items = DataReader<Items>.Get(itemId);
				if (items != null && (items.career == EntityWorld.Instance.EntSelf.TypeID || items.career == 0 || items.career == 999))
				{
					num++;
					this.AddScrollCell(num, down.shopInfo.goodsInfo.get_Item(i));
				}
			}
		}
		int childCount = this.m_itemlist.get_transform().get_childCount();
		if (childCount > 0)
		{
			this.m_itemlist.get_transform().GetChild(0).GetComponent<NpcShopItem>().SelectItem();
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_itemlist.get_transform().get_childCount(); i++)
		{
			this.m_itemlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void AddScrollCell(int index, NpcGoodsInfo itemData)
	{
		Transform transform = this.m_itemlist.get_transform().FindChild("LeftItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<NpcShopItem>().UpdateItem(itemData.libIndex, itemData.itemId, itemData.itemCount, itemData.stock, itemData.reputation, itemData.exchangeInfo);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("NpcShopItem");
			instantiate2Prefab.get_transform().SetParent(this.m_itemlist.get_transform(), false);
			instantiate2Prefab.set_name("LeftItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<NpcShopItem>().UpdateItem(itemData.libIndex, itemData.itemId, itemData.itemCount, itemData.stock, itemData.reputation, itemData.exchangeInfo);
		}
	}

	public void ReFreshItemSelect()
	{
		int childCount = this.m_itemlist.get_transform().get_childCount();
		if (childCount > 0)
		{
			for (int i = 0; i < this.m_itemlist.get_transform().get_childCount(); i++)
			{
				this.m_itemlist.get_transform().GetChild(i).GetComponent<NpcShopItem>().ResetState();
			}
		}
	}

	public void UpdateShoppingInfo(int onlyId, int stock)
	{
		int childCount = this.m_itemlist.get_transform().get_childCount();
		if (childCount > 0)
		{
			for (int i = 0; i < this.m_itemlist.get_transform().get_childCount(); i++)
			{
				this.m_itemlist.get_transform().GetChild(i).GetComponent<NpcShopItem>().ReFreshItemCount(stock, onlyId);
			}
		}
	}

	public void InitTime()
	{
		List<NpcShopSt> transactionNpcData = TransactionNPCManager.Instance.TransactionNpcData;
		if (transactionNpcData != null)
		{
			int currentShopId = TransactionNPCManager.Instance.CurrentShopId;
			for (int i = 0; i < transactionNpcData.get_Count(); i++)
			{
				if (transactionNpcData.get_Item(i).shopId == currentShopId)
				{
					this.LeftTimeCountDown(transactionNpcData.get_Item(i).nextUpdateTime);
					return;
				}
			}
		}
		this.m_textUpdateTime.set_text(string.Empty);
		this.m_textTimeStr.SetActive(false);
	}

	public void LeftTimeCountDown(int time)
	{
		if (time == -1)
		{
			this.m_textUpdateTime.set_text(string.Empty);
			this.m_textTimeStr.SetActive(false);
			return;
		}
		this.m_textTimeStr.SetActive(true);
		int seconds = time - TimeManager.Instance.PreciseServerSecond;
		this.ClearTimeCoundDown();
		this.timeCoundDown = new TimeCountDown(seconds, TimeFormat.SECOND, delegate
		{
			this.m_textUpdateTime.set_text(TimeConverter.GetTime(this.timeCoundDown.GetSeconds(), TimeFormat.HHMMSS));
		}, delegate
		{
			this.m_textUpdateTime.set_text(TimeConverter.GetTime(0, TimeFormat.HHMMSS));
		}, true);
	}

	private void ClearTimeCoundDown()
	{
		if (this.timeCoundDown != null)
		{
			this.timeCoundDown.Dispose();
			this.timeCoundDown = null;
		}
	}

	private void ChangeBodyImage()
	{
		Vector3 localPosition = new Vector3(-363f, -73f, 0f);
		int currentShopId = TransactionNPCManager.Instance.CurrentShopId;
		NPCShangChengBiao nPCShangChengBiao = DataReader<NPCShangChengBiao>.Get(currentShopId);
		if (nPCShangChengBiao != null)
		{
			int key = nPCShangChengBiao.shopNPC.get_Item(0);
			NPC nPC = DataReader<NPC>.Get(key);
			if (nPC != null && nPC.pic > 0)
			{
				int pic = nPC.pic;
				ResourceManager.SetSprite(this.ImageGril, GameDataUtils.GetIcon(pic));
				base.FindTransform("RawImageGril").get_gameObject().SetActive(true);
				float num = (float)nPCShangChengBiao.deviation;
				localPosition = new Vector3(localPosition.x + num, localPosition.y, 0f);
				base.FindTransform("RawImageGril").get_transform().set_localPosition(localPosition);
				return;
			}
		}
		base.FindTransform("RawImageGril").get_transform().set_localPosition(localPosition);
		base.FindTransform("RawImageGril").get_gameObject().SetActive(false);
	}

	private void ResetBodyPos()
	{
		Vector3 localPosition = new Vector3(-363f, -73f, 0f);
		base.FindTransform("RawImageGril").get_transform().set_localPosition(localPosition);
	}
}
