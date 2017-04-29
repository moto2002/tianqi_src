using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNetwork;

public class FashionManager : BaseSubSystemManager
{
	protected static FashionManager instance;

	protected XDict<string, FashionData> fashionImformation = new XDict<string, FashionData>();

	protected XDict<FashionDataSelete, string> dressingFashionData = new XDict<FashionDataSelete, string>();

	protected List<string> newFashionDataIDs = new List<string>();

	protected List<string> timeoutFashionDataIDs = new List<string>();

	public static FashionManager Instance
	{
		get
		{
			if (FashionManager.instance == null)
			{
				FashionManager.instance = new FashionManager();
			}
			return FashionManager.instance;
		}
	}

	protected FashionManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.fashionImformation.Clear();
		this.dressingFashionData.Clear();
		this.newFashionDataIDs.Clear();
		this.timeoutFashionDataIDs.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<SendFashionInfo>(new NetCallBackMethod<SendFashionInfo>(this.OnGetFashionInfo));
		NetworkManager.AddListenEvent<UpFashionRes>(new NetCallBackMethod<UpFashionRes>(this.OnDressRes));
		NetworkManager.AddListenEvent<SendGivingFashion>(new NetCallBackMethod<SendGivingFashion>(this.OnNewFashionRecommend));
		NetworkManager.AddListenEvent<SendFashionOverdue>(new NetCallBackMethod<SendFashionOverdue>(this.OnFashionTimeoutRecommend));
	}

	protected void OnGetFashionInfo(short state, SendFashionInfo down = null)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnGetFashionInfo: ",
			state,
			" ",
			down.Info.get_Count(),
			" ",
			down.wearFashion.get_Count()
		}));
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.fashionImformation.Clear();
		this.dressingFashionData.Clear();
		for (int i = 0; i < down.Info.get_Count(); i++)
		{
			FashionData fashionData = new FashionData();
			fashionData.dataID = down.Info.get_Item(i).fashionId;
			fashionData.time = down.Info.get_Item(i).times;
			fashionData.state = ((!down.Info.get_Item(i).isWear) ? ((down.Info.get_Item(i).times != 0) ? FashionData.FashionDataState.Own : FashionData.FashionDataState.Expired) : FashionData.FashionDataState.Dressing);
			fashionData.isAddAttr = down.Info.get_Item(i).hasEffect;
			this.fashionImformation.Add(down.Info.get_Item(i).fashionId, fashionData);
		}
		for (int j = 0; j < 3; j++)
		{
			if (j < down.wearFashion.get_Count())
			{
				this.dressingFashionData.Add(j + FashionDataSelete.Clothes, down.wearFashion.get_Item(j));
			}
			else
			{
				this.dressingFashionData.Add(j + FashionDataSelete.Clothes, string.Empty);
			}
		}
		this.TryFlushFashionUI();
		this.TryFlushShopUI();
	}

	protected void OnNewFashionRecommend(short state, SendGivingFashion down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (!DataReader<ShiZhuangXiTong>.Contains(down.fashionId))
		{
			return;
		}
		if (!this.newFashionDataIDs.Contains(down.fashionId))
		{
			this.newFashionDataIDs.Add(down.fashionId);
		}
		this.TryShowNewFashionRecommend();
	}

	protected void OnFashionTimeoutRecommend(short state, SendFashionOverdue down = null)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnGetFashionInfo: ",
			state,
			" ",
			down.fashionId
		}));
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (!DataReader<ShiZhuangXiTong>.Contains(down.fashionId))
		{
			return;
		}
		if (DataReader<ShiZhuangXiTong>.Get(down.fashionId).mallID == 0)
		{
			return;
		}
		if (!this.timeoutFashionDataIDs.Contains(down.fashionId))
		{
			this.timeoutFashionDataIDs.Add(down.fashionId);
		}
		this.TryShowTimeoutRecommend();
	}

	public void OpenFashionUI(FashionDataSelete defaultSeleteType = FashionDataSelete.Clothes)
	{
		FashionUI fashionUI = UIManagerControl.Instance.OpenUI("FashionUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FashionUI;
		fashionUI.SetData(this.dressingFashionData.Values, FashionPreviewCell.FashionPreviewCellType.Wardrobe, this.GetAllFashionAttr(), true, defaultSeleteType);
	}

	public void FlushFashionUI(FashionDataSelete seleteType)
	{
		FashionUI fashionUI = UIManagerControl.Instance.GetUIIfExist("FashionUI") as FashionUI;
		if (fashionUI)
		{
			List<string> list = this.GetNewFashionDataIDs(seleteType);
			fashionUI.SetFashion(seleteType, this.GetDressingFashionID(seleteType), this.GetFixAllFashionDataList(seleteType, list), list);
		}
	}

	protected void TryFlushFashionUI()
	{
		if (!UIManagerControl.Instance.IsOpen("FashionUI"))
		{
			return;
		}
		FashionUI fashionUI = UIManagerControl.Instance.GetUIIfExist("FashionUI") as FashionUI;
		if (fashionUI)
		{
			fashionUI.FlushData(this.dressingFashionData.Values, FashionPreviewCell.FashionPreviewCellType.Wardrobe, this.GetAllFashionAttr(), false);
		}
	}

	public void OpenFashionPreviewUI(string fashionDataID)
	{
		if (!DataReader<ShiZhuangXiTong>.Contains(fashionDataID))
		{
			return;
		}
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(fashionDataID);
		FashionDataSelete kind = (FashionDataSelete)shiZhuangXiTong.kind;
		XDict<FashionDataSelete, string> previewFashionDataID = this.GetPreviewFashionDataID(kind, fashionDataID);
		FashionPreviewUI fashionPreviewUI = UIManagerControl.Instance.OpenUI("FashionPreviewUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FashionPreviewUI;
		if (this.dressingFashionData.ContainsKey(kind) && this.dressingFashionData[kind] == fashionDataID)
		{
			fashionPreviewUI.SetUndress(previewFashionDataID.Values, fashionDataID);
		}
		else if (this.fashionImformation.ContainsKey(fashionDataID))
		{
			if (this.fashionImformation[fashionDataID].state == FashionData.FashionDataState.Own)
			{
				fashionPreviewUI.SetDress(previewFashionDataID.Values, fashionDataID);
			}
			else if (shiZhuangXiTong.mallID == 0)
			{
				fashionPreviewUI.SetDisplay(previewFashionDataID.Values, fashionDataID);
			}
			else
			{
				fashionPreviewUI.SetRenewal(previewFashionDataID.Values, fashionDataID);
			}
		}
		else if (shiZhuangXiTong.mallID == 0)
		{
			fashionPreviewUI.SetDisplay(previewFashionDataID.Values, fashionDataID);
		}
		else
		{
			fashionPreviewUI.SetBuy(previewFashionDataID.Values, fashionDataID);
		}
	}

	public void OpenBuyFashionUI(string theFashionDataID)
	{
		if (!DataReader<ShiZhuangXiTong>.Contains(theFashionDataID))
		{
			return;
		}
		WaitUI.OpenUI(10000u);
		ShiZhuangXiTong fashionData = DataReader<ShiZhuangXiTong>.Get(theFashionDataID);
		FashionDataSelete kind = (FashionDataSelete)fashionData.kind;
		XDict<FashionDataSelete, string> tempList = this.GetPreviewFashionDataID(kind, theFashionDataID);
		XMarketManager.Instance.CheckFashionServerData(delegate
		{
			WaitUI.CloseUI(0u);
			BuyFashionUI buyFashionUI = UIManagerControl.Instance.OpenUI("BuyFashionUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BuyFashionUI;
			buyFashionUI.SetData(tempList.Values, theFashionDataID, fashionData.mallID, !this.IsHasEternalFashion(theFashionDataID));
		});
	}

	public void OpenBuyFashionUI(int theCommodityDataID)
	{
		string fashionIDByCommodityID = GameDataUtils.GetFashionIDByCommodityID(theCommodityDataID);
		if (!DataReader<ShiZhuangXiTong>.Contains(fashionIDByCommodityID))
		{
			return;
		}
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(fashionIDByCommodityID);
		FashionDataSelete kind = (FashionDataSelete)shiZhuangXiTong.kind;
		XDict<FashionDataSelete, string> previewFashionDataID = this.GetPreviewFashionDataID(kind, fashionIDByCommodityID);
		BuyFashionUI buyFashionUI = UIManagerControl.Instance.OpenUI("BuyFashionUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BuyFashionUI;
		buyFashionUI.SetData(previewFashionDataID.Values, fashionIDByCommodityID, theCommodityDataID, !this.IsHasEternalFashion(theCommodityDataID));
	}

	protected void TryFlushShopUI()
	{
		XMarketManager.Instance.RefreshShop();
	}

	public void TryShowNewFashionRecommend()
	{
		if (this.newFashionDataIDs.get_Count() == 0)
		{
			return;
		}
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return;
		}
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (!townUI)
		{
			return;
		}
		string text = this.newFashionDataIDs.get_Item(0);
		while (!DataReader<ShiZhuangXiTong>.Contains(text) && this.newFashionDataIDs.get_Count() > 1)
		{
			this.newFashionDataIDs.RemoveAt(0);
			text = this.newFashionDataIDs.get_Item(0);
		}
		if (!DataReader<ShiZhuangXiTong>.Contains(text))
		{
			return;
		}
		ShiZhuangXiTong newFashionData = DataReader<ShiZhuangXiTong>.Get(text);
		if (!DataReader<Items>.Contains(newFashionData.itemsID))
		{
			return;
		}
		Items items = DataReader<Items>.Get(newFashionData.itemsID);
		townUI.SetNewFashionRecommend(text, items.icon, GameDataUtils.GetChineseContent(1005026, false), GameDataUtils.GetChineseContent(items.name, false), GameDataUtils.GetChineseContent(1005025, false), delegate
		{
			LinkNavigationManager.OpenActorUI(delegate
			{
				this.OpenFashionUI((FashionDataSelete)newFashionData.kind);
			});
		}, delegate
		{
			this.newFashionDataIDs.RemoveAt(0);
			this.TryShowNewFashionRecommend();
		});
	}

	public void TryShowTimeoutRecommend()
	{
		if (this.timeoutFashionDataIDs.get_Count() == 0)
		{
			return;
		}
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return;
		}
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (!townUI)
		{
			return;
		}
		string timeoutFashionDataID = this.timeoutFashionDataIDs.get_Item(0);
		this.timeoutFashionDataIDs.RemoveAt(0);
		while (!DataReader<ShiZhuangXiTong>.Contains(timeoutFashionDataID) && this.timeoutFashionDataIDs.get_Count() > 0)
		{
			timeoutFashionDataID = this.timeoutFashionDataIDs.get_Item(0);
			this.timeoutFashionDataIDs.RemoveAt(0);
		}
		if (!DataReader<ShiZhuangXiTong>.Contains(timeoutFashionDataID))
		{
			return;
		}
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(timeoutFashionDataID);
		if (!DataReader<Items>.Contains(shiZhuangXiTong.itemsID))
		{
			return;
		}
		Items items = DataReader<Items>.Get(shiZhuangXiTong.itemsID);
		townUI.SetFashionTimeoutRecommend(items.icon, GameDataUtils.GetChineseContent(1005022, false), GameDataUtils.GetChineseContent(1005023, false), GameDataUtils.GetChineseContent(1005024, false), delegate
		{
			FashionManager.Instance.OpenBuyFashionUI(timeoutFashionDataID);
		}, new Action(this.TryShowTimeoutRecommend));
	}

	protected string GetDressingFashionID(FashionDataSelete seleteType)
	{
		return (!this.dressingFashionData.ContainsKey(seleteType)) ? string.Empty : this.dressingFashionData[seleteType];
	}

	protected XDict<FashionDataSelete, string> GetPreviewFashionDataID(FashionDataSelete seleteType, string fashionDataID)
	{
		XDict<FashionDataSelete, string> xDict = new XDict<FashionDataSelete, string>();
		for (int i = 0; i < this.dressingFashionData.Count; i++)
		{
			if (this.dressingFashionData.ElementKeyAt(i) == seleteType)
			{
				xDict.Add(seleteType, fashionDataID);
			}
			else
			{
				xDict.Add(this.dressingFashionData.ElementKeyAt(i), this.dressingFashionData.ElementValueAt(i));
			}
		}
		return xDict;
	}

	protected List<FashionData> GetFixAllFashionDataList(FashionDataSelete seleteType, List<string> fixNewFashionDataID)
	{
		List<FashionData> list = new List<FashionData>();
		List<ShiZhuangXiTong> dataList = DataReader<ShiZhuangXiTong>.DataList;
		for (int i = 0; i < this.fashionImformation.Count; i++)
		{
			if (DataReader<ShiZhuangXiTong>.Contains(this.fashionImformation.Values.get_Item(i).dataID))
			{
				ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(this.fashionImformation.Values.get_Item(i).dataID);
				if (shiZhuangXiTong.kind == (int)seleteType)
				{
					if (this.fashionImformation.Values.get_Item(i).state != FashionData.FashionDataState.Expired)
					{
						if (fixNewFashionDataID.Contains(this.fashionImformation.Values.get_Item(i).dataID))
						{
							list.Add(this.fashionImformation.Values.get_Item(i));
						}
					}
				}
			}
		}
		for (int j = 0; j < this.fashionImformation.Count; j++)
		{
			if (DataReader<ShiZhuangXiTong>.Contains(this.fashionImformation.Values.get_Item(j).dataID))
			{
				ShiZhuangXiTong shiZhuangXiTong2 = DataReader<ShiZhuangXiTong>.Get(this.fashionImformation.Values.get_Item(j).dataID);
				if (shiZhuangXiTong2.kind == (int)seleteType)
				{
					if (this.fashionImformation.Values.get_Item(j).state != FashionData.FashionDataState.Expired)
					{
						if (!fixNewFashionDataID.Contains(this.fashionImformation.Values.get_Item(j).dataID))
						{
							list.Add(this.fashionImformation.Values.get_Item(j));
						}
					}
				}
			}
		}
		for (int k = 0; k < this.fashionImformation.Count; k++)
		{
			if (DataReader<ShiZhuangXiTong>.Contains(this.fashionImformation.Values.get_Item(k).dataID))
			{
				ShiZhuangXiTong shiZhuangXiTong3 = DataReader<ShiZhuangXiTong>.Get(this.fashionImformation.Values.get_Item(k).dataID);
				if (shiZhuangXiTong3.kind == (int)seleteType)
				{
					if (this.fashionImformation.Values.get_Item(k).state == FashionData.FashionDataState.Expired)
					{
						list.Add(this.fashionImformation.Values.get_Item(k));
					}
				}
			}
		}
		for (int l = 0; l < dataList.get_Count(); l++)
		{
			if (dataList.get_Item(l).kind == (int)seleteType)
			{
				if (this.CompareCareer(dataList.get_Item(l)))
				{
					if (!this.fashionImformation.ContainsKey(dataList.get_Item(l).ID))
					{
						list.Add(new FashionData
						{
							dataID = dataList.get_Item(l).ID,
							time = -1,
							state = FashionData.FashionDataState.None,
							isAddAttr = true
						});
					}
				}
			}
		}
		return list;
	}

	protected List<string> GetNewFashionDataIDs(FashionDataSelete seleteType)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.newFashionDataIDs.get_Count(); i++)
		{
			if (DataReader<ShiZhuangXiTong>.Contains(this.newFashionDataIDs.get_Item(i)))
			{
				ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(this.newFashionDataIDs.get_Item(i));
				if (shiZhuangXiTong.kind == (int)seleteType)
				{
					list.Add(this.newFashionDataIDs.get_Item(i));
				}
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			this.newFashionDataIDs.Remove(list.get_Item(j));
		}
		return list;
	}

	protected bool CompareCareer(ShiZhuangXiTong fashionData)
	{
		if (!DataReader<Items>.Contains(fashionData.itemsID))
		{
			return false;
		}
		Items items = DataReader<Items>.Get(fashionData.itemsID);
		return items.career == EntityWorld.Instance.EntSelf.TypeID || items.career == 0 || items.career == 999;
	}

	public bool IsHasEternalFashion(int theCommodityID)
	{
		return this.IsHasEternalFashion(GameDataUtils.GetFashionIDByCommodityID(theCommodityID));
	}

	public bool IsHasEternalFashion(string theFashionDataID)
	{
		return this.fashionImformation.ContainsKey(theFashionDataID) && (this.fashionImformation[theFashionDataID].state == FashionData.FashionDataState.Dressing || this.fashionImformation[theFashionDataID].state == FashionData.FashionDataState.Own) && this.fashionImformation[theFashionDataID].time == -1;
	}

	public int GetFashionModelTypeID(string id, out FashionModelType type)
	{
		if (!DataReader<ShiZhuangXiTong>.Contains(id))
		{
			type = FashionModelType.None;
			return 0;
		}
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(id);
		switch (shiZhuangXiTong.kind)
		{
		case 1:
		case 2:
			if (shiZhuangXiTong.IsModelChange == 0)
			{
				type = FashionModelType.Equip;
				return shiZhuangXiTong.itemsID;
			}
			type = FashionModelType.Model;
			return shiZhuangXiTong.model;
		case 3:
			type = FashionModelType.Wing;
			return shiZhuangXiTong.model;
		default:
			type = FashionModelType.None;
			return 0;
		}
	}

	public int GetFashionModelID(List<string> fashionID)
	{
		for (int i = 0; i < fashionID.get_Count(); i++)
		{
			if (DataReader<ShiZhuangXiTong>.Contains(fashionID.get_Item(i)))
			{
				ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(fashionID.get_Item(i));
				if (shiZhuangXiTong.kind == 1 || shiZhuangXiTong.kind == 2)
				{
					if (shiZhuangXiTong.IsModelChange != 0)
					{
						return shiZhuangXiTong.model;
					}
				}
			}
		}
		return 0;
	}

	protected string GetAllFashionAttr()
	{
		if (this.fashionImformation.Count == 0)
		{
			return string.Empty;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < this.fashionImformation.Values.get_Count(); i++)
		{
			if (this.fashionImformation.Values.get_Item(i).isAddAttr)
			{
				if (this.fashionImformation.Values.get_Item(i).state == FashionData.FashionDataState.Dressing || this.fashionImformation.Values.get_Item(i).state == FashionData.FashionDataState.Own)
				{
					if (DataReader<ShiZhuangXiTong>.Contains(this.fashionImformation.Values.get_Item(i).dataID))
					{
						ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(this.fashionImformation.Values.get_Item(i).dataID);
						list.Add(shiZhuangXiTong.gainProperty);
					}
				}
			}
		}
		if (list.get_Count() == 0)
		{
			return string.Empty;
		}
		XDict<int, long> xDict = new XDict<int, long>();
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (DataReader<Attrs>.Contains(list.get_Item(j)))
			{
				Attrs attrs = DataReader<Attrs>.Get(list.get_Item(j));
				int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
				if (num != 0)
				{
					for (int k = 0; k < num; k++)
					{
						if (xDict.ContainsKey(attrs.attrs.get_Item(k)))
						{
							XDict<int, long> xDict2;
							XDict<int, long> expr_196 = xDict2 = xDict;
							int key;
							int expr_1A7 = key = attrs.attrs.get_Item(k);
							long num2 = xDict2[key];
							expr_196[expr_1A7] = num2 + (long)attrs.values.get_Item(k);
						}
						else
						{
							xDict.Add(attrs.attrs.get_Item(k), (long)attrs.values.get_Item(k));
						}
					}
				}
			}
		}
		if (xDict.Count == 0)
		{
			return string.Empty;
		}
		if (xDict.Count == 1)
		{
			return AttrUtility.GetDesc(xDict.Keys.get_Item(0), xDict.Values.get_Item(0), " +", "fffae6", "f95f4f", false);
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(AttrUtility.GetDesc(xDict.Keys.get_Item(0), xDict.Values.get_Item(0), " +", "fffae6", "f95f4f", false));
		for (int l = 1; l < xDict.Count; l++)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append(AttrUtility.GetDesc(xDict.Keys.get_Item(l), xDict.Values.get_Item(l), " +", "fffae6", "f95f4f", false));
		}
		return stringBuilder.ToString();
	}

	public void Dress(string fashionDataID)
	{
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(fashionDataID);
		if (shiZhuangXiTong.kind == 3)
		{
			if (WingManager.Instance.IsWingSysOnAndActivation())
			{
				NetworkManager.Send(new UpFashionReq
				{
					fashionId = fashionDataID
				}, ServerType.Data);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(1005021, false), 1f, 1f);
			}
		}
		else
		{
			NetworkManager.Send(new UpFashionReq
			{
				fashionId = fashionDataID
			}, ServerType.Data);
		}
	}

	public void Undress(string fashionDataID)
	{
		NetworkManager.Send(new UpFashionReq
		{
			fashionId = fashionDataID
		}, ServerType.Data);
	}

	protected void OnDressRes(short state, UpFashionRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void Buy(int commodityDataID, int curBuyRank)
	{
		XMarketManager.Instance.BuyFashion(commodityDataID, curBuyRank);
	}
}
