using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class ActivityTossDiscountManager : BaseSubSystemManager
{
	public static int selectProductSelectType = 1;

	public static int payProductSelectType = 2;

	public static int gameNeedItemId = 71033;

	private TimeCountDown timeCoundDown;

	private FlipCoinRes flipCoinRes;

	private bool isSelectHead;

	private int tempSelectProductId = -1;

	public List<int> endTimeouts = new List<int>();

	private bool isAlreadyConfirm;

	public int currentShangPinId = -1;

	private float currentShangPinDiscount = 100f;

	private List<DiscountItemsInfo> selectDataList;

	public DiscountItemsLoginPush dataList;

	public static ActivityTossDiscountManager instance;

	public FlipCoinRes FlipCoinResult
	{
		get
		{
			return this.flipCoinRes;
		}
		set
		{
			this.flipCoinRes = value;
		}
	}

	public bool IsSelectHead
	{
		get
		{
			return this.isSelectHead;
		}
		set
		{
			this.isSelectHead = value;
		}
	}

	public int TempSelectProductId
	{
		get
		{
			return this.tempSelectProductId;
		}
		set
		{
			this.tempSelectProductId = value;
		}
	}

	public bool IsAlreadyConfirm
	{
		get
		{
			return this.isAlreadyConfirm;
		}
		set
		{
			this.isAlreadyConfirm = value;
		}
	}

	public int CurrentShangPinId
	{
		get
		{
			return this.currentShangPinId;
		}
		set
		{
			this.currentShangPinId = value;
		}
	}

	public float CurrentShangPinDiscount
	{
		get
		{
			return this.currentShangPinDiscount;
		}
		set
		{
			this.currentShangPinDiscount = value;
		}
	}

	public static ActivityTossDiscountManager Instance
	{
		get
		{
			if (ActivityTossDiscountManager.instance == null)
			{
				ActivityTossDiscountManager.instance = new ActivityTossDiscountManager();
				TimerHeap.AddTimer(0u, 1000, new Action(ActivityTossDiscountManager.instance.TimerCallback));
			}
			return ActivityTossDiscountManager.instance;
		}
	}

	private ActivityTossDiscountManager()
	{
	}

	public void cleanCurrentShangPinId()
	{
		this.currentShangPinId = -1;
	}

	public bool isHaveCurrentShangPinId()
	{
		return ActivityTossDiscountManager.Instance.CurrentShangPinId != -1;
	}

	private void UpdateData(DiscountItemsLoginPush data)
	{
		this.dataList = data;
	}

	private void startTimeCoundDown(int time)
	{
		this.ClearTimeCoundDown();
		if (time <= 0)
		{
			return;
		}
		this.timeCoundDown = new TimeCountDown(time, TimeFormat.SECOND, delegate
		{
			if (ActivityTossDiscountUI.Instance != null)
			{
				ActivityTossDiscountUI.Instance.updateTimeCoundDown(TimeConverter.GetTime(this.timeCoundDown.GetSeconds(), TimeFormat.HHMMSS) + " 后刷新商品折扣");
			}
		}, delegate
		{
			if (ActivityTossDiscountUI.Instance != null)
			{
				this.initDiscountData();
				ActivityTossDiscountManager.Instance.cleanCurrentShangPinId();
				ActivityTossDiscountManager.Instance.cleanDiscountData();
				if (ActivityTossDiscountUI.Instance != null)
				{
					ActivityTossDiscountUI.Instance.updateTimeCoundDown(string.Empty);
					ActivityTossDiscountUI.Instance.initYingBiSpine();
					ActivityTossDiscountUI.Instance.initView();
					ActivityTossDiscountUI.Instance.RefreshUI();
				}
			}
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

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.endTimeouts.Clear();
		this.ClearTimeCoundDown();
		this.clearData();
	}

	private void clearData()
	{
		this.tempSelectProductId = -1;
		this.currentShangPinId = -1;
		this.currentShangPinDiscount = 100f;
		if (this.selectDataList != null)
		{
			this.selectDataList.Clear();
		}
		this.dataList = null;
	}

	private void initDiscountData()
	{
		this.tempSelectProductId = -1;
		this.currentShangPinId = -1;
		this.currentShangPinDiscount = 100f;
		this.cleanDiscountData();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DiscountItemsLoginPush>(new NetCallBackMethod<DiscountItemsLoginPush>(this.OnUpdatePanel));
		NetworkManager.AddListenEvent<FlipCoinRes>(new NetCallBackMethod<FlipCoinRes>(this.OnFlipCoinRes));
		NetworkManager.AddListenEvent<ReplaceItemRes>(new NetCallBackMethod<ReplaceItemRes>(this.OnReplaceItemRes));
		NetworkManager.AddListenEvent<SelectItemRes>(new NetCallBackMethod<SelectItemRes>(this.OnSelectItemRes));
		NetworkManager.AddListenEvent<BuyDiscountItemRes>(new NetCallBackMethod<BuyDiscountItemRes>(this.OnBuyDiscountItemRes));
	}

	private void TimerCallback()
	{
		for (int i = 0; i < this.endTimeouts.get_Count(); i++)
		{
			if (this.endTimeouts.get_Item(i) > 0)
			{
				List<int> list;
				List<int> expr_1F = list = this.endTimeouts;
				int num;
				int expr_22 = num = i;
				num = list.get_Item(num);
				expr_1F.set_Item(expr_22, num - 1);
			}
		}
	}

	public void SendFlipCoinReq(bool isHead)
	{
		this.isSelectHead = isHead;
		NetworkManager.Send(new FlipCoinReq
		{
			id = this.currentShangPinId,
			side = isHead
		}, ServerType.Data);
	}

	public void SendSelectItemReq(int productId)
	{
	}

	public void SendSelectItemReq()
	{
		NetworkManager.Send(new SelectItemReq
		{
			id = this.currentShangPinId
		}, ServerType.Data);
	}

	public void SendReplaceItemReq(int productId)
	{
		NetworkManager.Send(new ReplaceItemReq
		{
			id = productId
		}, ServerType.Data);
	}

	public void sendBuyDiscountItemReq(int productId)
	{
		NetworkManager.Send(new BuyDiscountItemReq
		{
			id = productId
		}, ServerType.Data);
	}

	public void OnFlipCoinRes(short state, FlipCoinRes flipCoinRes = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.startTimeCoundDown((int)(flipCoinRes.countdown / 1000f));
		bool result = flipCoinRes.result;
		if (result)
		{
			this.setDiscountDataById(flipCoinRes.id, (float)flipCoinRes.discount);
			this.currentShangPinDiscount = (float)flipCoinRes.discount;
		}
		this.FlipCoinResult = flipCoinRes;
		if (ActivityTossDiscountUI.Instance != null)
		{
		}
	}

	public void OnReplaceItemRes(short state, ReplaceItemRes selectItemRes = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.ClearTimeCoundDown();
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.updateTimeCoundDown(string.Empty);
		}
		ActivityTossDiscountManager.Instance.cleanCurrentShangPinId();
		ActivityTossDiscountManager.Instance.cleanDiscountData();
		ActivityTossDiscountManager.Instance.IsAlreadyConfirm = false;
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.OnReplaceItemRes(selectItemRes);
		}
	}

	public int getListDataIndexByID(int id)
	{
		int result = -1;
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			if (discountItemsInfo.id == id)
			{
				result = i;
			}
		}
		return result;
	}

	public static bool changeListData(List<DiscountItemsInfo> list, int rawID, int newID)
	{
		if (rawID >= list.get_Count() || newID >= list.get_Count() || rawID < 0 || newID < 0 || rawID == newID)
		{
			return false;
		}
		bool result;
		try
		{
			DiscountItemsInfo discountItemsInfo = list.get_Item(rawID);
			list.set_Item(rawID, list.get_Item(newID));
			list.set_Item(newID, discountItemsInfo);
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	public float getDiscountDataById(int itemId)
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			if (itemId == discountItemsInfo.id)
			{
				return discountItemsInfo.discount;
			}
		}
		return 100f;
	}

	public DiscountItemsInfo getDiscountInfoById(int itemId)
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			if (itemId == discountItemsInfo.id)
			{
				return discountItemsInfo;
			}
		}
		return null;
	}

	public void setIsSelectItemById(int itemId)
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			discountItemsInfo.isOpt = (itemId == discountItemsInfo.id);
		}
	}

	public void setProductNumById(int itemId, int num)
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			if (itemId == discountItemsInfo.id)
			{
				discountItemsInfo.num = num;
			}
		}
	}

	public void setDiscountDataById(int itemId, float discount)
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			if (itemId == discountItemsInfo.id)
			{
				discountItemsInfo.discount = discount;
			}
		}
	}

	public void cleanDiscountData()
	{
		for (int i = 0; i < this.dataList.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = this.dataList.itemsInfo.get_Item(i);
			discountItemsInfo.isOpt = false;
			ShangPin shangPin = DataReader<ShangPin>.Get(discountItemsInfo.id);
			if (shangPin != null)
			{
				List<int> discount = shangPin.discount;
				if (discount.get_Count() > 0)
				{
					discountItemsInfo.discount = (float)discount.get_Item(0);
				}
			}
		}
	}

	public void OnSelectItemRes(short state, SelectItemRes selectItemRes = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.updateSelectItemRes(selectItemRes);
		}
	}

	public void OnBuyDiscountItemRes(short state, BuyDiscountItemRes buyDiscountItemRes = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.setProductNumById(buyDiscountItemRes.id, buyDiscountItemRes.nums);
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513183, false), 1f, 1f);
		ActivityTossDiscountManager.Instance.cleanCurrentShangPinId();
		ActivityTossDiscountManager.Instance.cleanDiscountData();
		ActivityTossDiscountManager.Instance.IsAlreadyConfirm = false;
		this.productDataSort();
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.replaceOrBuyReturnLogic(false);
		}
	}

	public void OnUpdatePanel(short state, DiscountItemsLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.itemsInfo.get_Count(); i++)
		{
			DiscountItemsInfo discountItemsInfo = down.itemsInfo.get_Item(i);
			if (discountItemsInfo.isOpt && discountItemsInfo.num > 0 && !this.isHaveCurrentShangPinId())
			{
				this.CurrentShangPinId = discountItemsInfo.id;
				this.CurrentShangPinDiscount = discountItemsInfo.discount;
				this.IsAlreadyConfirm = true;
				this.startTimeCoundDown((int)(down.countdown / 1000f));
			}
		}
		if (down != null)
		{
			this.UpdateData(down);
			this.productDataSort();
			this.RefreshUI();
		}
		else
		{
			Debug.LogError("DiscountItemsLoginPush  is  empty！________________________________________________________________________");
		}
	}

	public void productDataSort()
	{
		if (this.dataList != null && this.dataList.itemsInfo != null)
		{
			this.dataList.itemsInfo.Sort(delegate(DiscountItemsInfo item1, DiscountItemsInfo item2)
			{
				int result;
				if (item1.id > item2.id)
				{
					if (item1.num == 0 && item2.num == 0)
					{
						result = -1;
					}
					else if (item1.num != 0 && item2.num == 0)
					{
						result = -1;
					}
					else if (item1.num != 0 && item2.num != 0)
					{
						result = -1;
					}
					else if (item1.num == 0 && item2.num != 0)
					{
						result = 1;
					}
					else
					{
						result = -1;
					}
				}
				else if (item1.num == 0 && item2.num == 0)
				{
					result = 1;
				}
				else if (item1.num != 0 && item2.num == 0)
				{
					result = -1;
				}
				else if (item1.num != 0 && item2.num != 0)
				{
					result = 1;
				}
				else if (item1.num == 0 && item2.num != 0)
				{
					result = 1;
				}
				else
				{
					result = 1;
				}
				return result;
			});
		}
	}

	public IList<DiscountItemsInfo> productDataSort(List<DiscountItemsInfo> itemsInfo)
	{
		IList<DiscountItemsInfo> list = new List<DiscountItemsInfo>();
		if (itemsInfo != null)
		{
			for (int i = 0; i < itemsInfo.get_Count(); i++)
			{
				list.Add(itemsInfo.get_Item(i));
			}
			List<string> list2 = new List<string>();
			list2.Add("id");
			list2.Add("num");
			bool[] expr_52 = new bool[2];
			expr_52[0] = true;
			bool[] sortBy = expr_52;
			list = new IListSort<DiscountItemsInfo>(list, list2, sortBy).Sort(null);
		}
		return list;
	}

	public void RefreshUI()
	{
	}

	public DiscountItemsLoginPush GetDiscountItemsData()
	{
		return this.dataList;
	}

	public List<DiscountItemsInfo> GetSelectDataList()
	{
		return this.selectDataList;
	}
}
