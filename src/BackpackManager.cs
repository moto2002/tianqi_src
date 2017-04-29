using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class BackpackManager : BaseSubSystemManager
{
	private List<Goods> bag = new List<Goods>();

	private List<ItemInfo> roleBag = new List<ItemInfo>();

	private List<ItemInfo> fashionBag = new List<ItemInfo>();

	private List<Sort> sort = new List<Sort>();

	public List<Goods> PropGoods = new List<Goods>();

	public List<Goods> FragmentGoods = new List<Goods>();

	public List<Goods> EquimentGoods = new List<Goods>();

	public List<Goods> RuneGoods = new List<Goods>();

	public List<Goods> EnchantmentGoods = new List<Goods>();

	public List<Goods> RiseGoods = new List<Goods>();

	private int lastCapacity;

	private static BackpackManager instance;

	private int m_promptWay;

	private int BagSize;

	public List<long> ItemCanUseRecommendTipList;

	private bool isCanShowRedPoint;

	public List<Goods> Bag
	{
		get
		{
			return this.bag;
		}
	}

	public List<ItemInfo> RoleBag
	{
		get
		{
			return this.roleBag;
		}
	}

	public List<ItemInfo> FashionBag
	{
		get
		{
			return this.fashionBag;
		}
	}

	public static BackpackManager Instance
	{
		get
		{
			if (BackpackManager.instance == null)
			{
				BackpackManager.instance = new BackpackManager();
				BackpackManager.instance.sort = DataReader<Sort>.DataList;
				BackpackManager.instance.BagSize = (int)float.Parse(DataReader<GlobalParams>.Get("package_limit_i").value);
			}
			return BackpackManager.instance;
		}
	}

	public bool IsCanShowRedPoint
	{
		get
		{
			return this.isCanShowRedPoint;
		}
		set
		{
			this.isCanShowRedPoint = value;
		}
	}

	private BackpackManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.bag.Clear();
		this.roleBag.Clear();
		this.fashionBag.Clear();
		this.PropGoods.Clear();
		this.FragmentGoods.Clear();
		this.EquimentGoods.Clear();
		this.RuneGoods.Clear();
		this.EnchantmentGoods.Clear();
		this.RiseGoods.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<Bags>(new NetCallBackMethod<Bags>(this.OnGetBagsData));
		NetworkManager.AddListenEvent<UseItemRes>(new NetCallBackMethod<UseItemRes>(this.OnGetUseGoodRes));
		NetworkManager.AddListenEvent<ItemUpdates>(new NetCallBackMethod<ItemUpdates>(this.OnUpdateGoods));
		NetworkManager.AddListenEvent<SystemRecoveryRes>(new NetCallBackMethod<SystemRecoveryRes>(this.OnSellGoods));
	}

	public void SendUseGood(long goodId, int count, int promptWay)
	{
		this.m_promptWay = promptWay;
		GlobalManager.Instance.PromptWay = promptWay;
		NetworkManager.Send(new UseItemReq
		{
			id = goodId,
			count = count
		}, ServerType.Data);
	}

	public void SendCleanUpReq()
	{
		NetworkManager.Send(new CleanUpReq(), ServerType.Data);
	}

	public void SendSellGood(long goodId, int countNum = 1)
	{
		NetworkManager.Send(new SystemRecoveryReq
		{
			id = goodId,
			count = countNum
		}, ServerType.Data);
	}

	private void OnGetUseGoodRes(short state, UseItemRes down = null)
	{
		if (state != 0)
		{
			if (state == 401)
			{
				Items items = DataReader<Items>.Get(down.itemId);
				if (items != null)
				{
					UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(509004, false), items.minLv));
				}
			}
			else
			{
				StateManager.Instance.StateShow(state, 0);
			}
			return;
		}
		if (down != null && this.m_promptWay == 2)
		{
			Items item = this.GetItem(down.itemId);
			if (item != null)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(item.promptId, false));
			}
		}
	}

	private void OnUpdateGoods(short state, ItemUpdates down = null)
	{
		if (state != 0)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm("提示", GameDataUtils.GetChineseContent(505148, false), null, "确定", "button_orange_1", null);
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.updates.get_Count(); i++)
			{
				this.AddAndDelGoods(down.updates.get_Item(i));
			}
			this.GoodsSort();
			if (this.IsBackpackFull())
			{
				DialogBoxUIViewModel.Instance.ShowAsConfirm("提示", GameDataUtils.GetChineseContent(505150, false), null, "确定", "button_orange_1", null);
			}
			EventDispatcher.Broadcast(EventNames.OnUpdateGoods);
		}
	}

	private void OnGetBagsData(short state, Bags down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.bags.get_Count(); i++)
			{
				if (down.bags.get_Item(i).items != null)
				{
					switch (down.bags.get_Item(i).type)
					{
					case BagType.BT.Bag:
						this.OnGetGroup(down.bags.get_Item(i).items.items);
						break;
					case BagType.BT.Role_bag:
						this.roleBag.AddRange(down.bags.get_Item(i).items.items);
						break;
					case BagType.BT.Fashion_bag:
						this.fashionBag.AddRange(down.bags.get_Item(i).items.items);
						break;
					}
				}
			}
			this.CheckShowTip();
			this.CheckBagCanShowRedPoint();
			this.CheckItemCanUseRecommendTip();
		}
	}

	private void CheckShowTip()
	{
		GemManager.Instance.CheckCanShowWearingGemPromoteWay();
	}

	private void OnSellGoods(short state, SystemRecoveryRes down = null)
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
		List<ItemBriefInfo> itemInfo = down.itemInfo;
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		List<long> list3 = new List<long>();
		for (int i = 0; i < itemInfo.get_Count(); i++)
		{
			ItemBriefInfo itemBriefInfo = itemInfo.get_Item(i);
			list.Add(itemBriefInfo.cfgId);
			list2.Add(itemBriefInfo.count);
			list3.Add(itemBriefInfo.uId);
		}
		RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.NormalUIRoot);
		rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
	}

	private void AddAndDelGoods(ItemUpdate goods)
	{
		for (int i = 0; i < goods.items.get_Count(); i++)
		{
			BagType.BT bagType = goods.bagType;
			if (bagType == BagType.BT.Bag)
			{
				Goods goods2 = this.OnGetGood(goods.items.get_Item(i).baseInfo.id);
				if (goods2 != null)
				{
					if (goods.items.get_Item(i).baseInfo.count <= 0)
					{
						this.OnBagGroupAddAndDel(goods.items.get_Item(i).baseInfo.id, false);
						this.bag.Remove(goods2);
					}
					else
					{
						goods2.ServerItem.baseInfo.count = goods.items.get_Item(i).baseInfo.count;
					}
				}
				else
				{
					this.bag.Add(new Goods(goods.items.get_Item(i)));
					this.OnBagGroupAddAndDel(goods.items.get_Item(i).baseInfo.id, true);
				}
			}
		}
	}

	private void DeleteGoods(ItemUpdate goods)
	{
		BackpackManager.<DeleteGoods>c__AnonStoreyAC <DeleteGoods>c__AnonStoreyAC = new BackpackManager.<DeleteGoods>c__AnonStoreyAC();
		<DeleteGoods>c__AnonStoreyAC.goods = goods;
		int i;
		for (i = 0; i < <DeleteGoods>c__AnonStoreyAC.goods.items.get_Count(); i++)
		{
			switch (<DeleteGoods>c__AnonStoreyAC.goods.bagType)
			{
			case BagType.BT.Bag:
			{
				Goods goods2 = this.OnGetGood(<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.id);
				if (goods2 != null)
				{
					if (<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count <= 0)
					{
						this.bag.Remove(goods2);
					}
					goods2.ServerItem.baseInfo.count = <DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count;
				}
				else
				{
					this.bag.Add(new Goods(<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i)));
				}
				break;
			}
			case BagType.BT.Role_bag:
			{
				ItemInfo itemInfo = this.roleBag.Find((ItemInfo e) => e.baseInfo.id == <DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.id);
				if (itemInfo != null)
				{
					if (<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count <= 0)
					{
						this.roleBag.Remove(itemInfo);
					}
					itemInfo.baseInfo.count = <DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count;
				}
				else
				{
					this.roleBag.Add(<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i));
				}
				break;
			}
			case BagType.BT.Fashion_bag:
			{
				ItemInfo itemInfo2 = this.fashionBag.Find((ItemInfo e) => e.baseInfo.id == <DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.id);
				if (itemInfo2 != null)
				{
					if (<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count <= 0)
					{
						this.fashionBag.Remove(itemInfo2);
					}
					itemInfo2.baseInfo.count = <DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i).baseInfo.count;
				}
				else
				{
					this.fashionBag.Add(<DeleteGoods>c__AnonStoreyAC.goods.items.get_Item(i));
				}
				break;
			}
			}
		}
	}

	private void OnGetGroup(List<ItemInfo> ItemInfos)
	{
		for (int i = 0; i < ItemInfos.get_Count(); i++)
		{
			Goods goods = new Goods(ItemInfos.get_Item(i));
			if (goods.LocalItem != null)
			{
				switch (goods.LocalItem.tab)
				{
				case 1:
					this.PropGoods.Add(goods);
					break;
				case 2:
					this.EquimentGoods.Add(goods);
					break;
				case 3:
					this.RuneGoods.Add(goods);
					break;
				case 4:
					this.FragmentGoods.Add(goods);
					break;
				case 5:
					this.EnchantmentGoods.Add(goods);
					break;
				}
				if (goods.LocalItem.point > 0)
				{
					this.RiseGoods.Add(goods);
				}
			}
			else
			{
				Debuger.Error("item is null, id = " + ItemInfos.get_Item(i).baseInfo.itemId, new object[0]);
			}
		}
		this.bag.AddRange(this.PropGoods);
		this.bag.AddRange(this.FragmentGoods);
		this.bag.AddRange(this.EquimentGoods);
		this.bag.AddRange(this.RuneGoods);
		this.bag.AddRange(this.EnchantmentGoods);
		this.GoodsSort();
	}

	private void OnBagGroupAddAndDel(long id, bool isAdd)
	{
		Goods goods = this.OnGetGood(id);
		if (goods == null || goods.LocalItem == null)
		{
			Debuger.Error("配置没有所对应的ID：" + id, new object[0]);
			return;
		}
		switch (goods.LocalItem.tab)
		{
		case 1:
			if (isAdd)
			{
				this.PropGoods.Add(goods);
			}
			else
			{
				this.PropGoods.Remove(goods);
			}
			break;
		case 2:
			if (isAdd)
			{
				this.EquimentGoods.Add(goods);
			}
			else
			{
				this.EquimentGoods.Remove(goods);
			}
			break;
		case 3:
			if (isAdd)
			{
				this.RuneGoods.Add(goods);
			}
			else
			{
				this.RuneGoods.Remove(goods);
			}
			break;
		case 4:
			if (isAdd)
			{
				this.FragmentGoods.Add(goods);
			}
			else
			{
				this.FragmentGoods.Remove(goods);
			}
			break;
		case 5:
			if (isAdd)
			{
				this.EnchantmentGoods.Add(goods);
			}
			else
			{
				this.EnchantmentGoods.Remove(goods);
			}
			break;
		}
		if (goods.LocalItem.point > 0)
		{
			if (isAdd)
			{
				this.RiseGoods.Add(goods);
			}
			else
			{
				this.RiseGoods.Remove(goods);
			}
		}
		if (goods.LocalItem.tab == 1)
		{
			this.UpdateItemCanUseRecommendTip(goods, isAdd);
		}
	}

	public int OnGetGoodCount(long id)
	{
		int result = 0;
		Goods goods = this.OnGetGood(id);
		if (goods != null)
		{
			result = goods.GetCount();
		}
		return result;
	}

	public long OnGetGoodCount(int templateId)
	{
		long num = 0L;
		Items item = BackpackManager.Instance.GetItem(templateId);
		if (item != null)
		{
			if (item.id == 2)
			{
				num = EntityWorld.Instance.EntSelf.Gold;
			}
			else if (item.id == 3)
			{
				num = (long)EntityWorld.Instance.EntSelf.Diamond;
			}
			else if (item.id == 1)
			{
				num = EntityWorld.Instance.EntSelf.Exp;
			}
			else if (item.id == 5)
			{
				num = (long)EntityWorld.Instance.EntSelf.Honor;
			}
			else if (item.secondType == 40)
			{
				num = 1L;
			}
			else if (item.id == 13)
			{
				num = (long)((!GuildManager.Instance.IsJoinInGuild()) ? 0 : GuildManager.Instance.MyGuildnfo.guildFund);
			}
			else
			{
				List<Goods> list = this.OnGetGood(templateId);
				if (list != null)
				{
					for (int i = 0; i < list.get_Count(); i++)
					{
						num += (long)list.get_Item(i).GetCount();
					}
				}
			}
		}
		return num;
	}

	public List<Goods> OnGetGood(int templateId)
	{
		if (templateId <= 0)
		{
			return null;
		}
		return this.bag.FindAll((Goods e) => e.ServerItem.baseInfo.itemId == templateId);
	}

	public Goods OnGetGood(long id)
	{
		if (id <= 0L)
		{
			return null;
		}
		return this.bag.Find((Goods e) => e.ServerItem.baseInfo.id == id);
	}

	public Items GetItem(int templateId)
	{
		return DataReader<Items>.Get(templateId);
	}

	public List<Items> GetItems(GoodsTab type)
	{
		return DataReader<Items>.DataList.FindAll((Items e) => e.tab == (int)type);
	}

	public long OnGetGoodLongId(int templateId)
	{
		List<Goods> list = this.OnGetGood(templateId);
		if (list != null && list.get_Count() > 0 && list.get_Item(0).GetCount() > 0)
		{
			return list.get_Item(0).GetLongId();
		}
		return 0L;
	}

	public int OnGetGoodItemId(long uuid)
	{
		Goods goods = this.OnGetGood(uuid);
		if (goods == null)
		{
			return 0;
		}
		return goods.GetItemId();
	}

	public void GoodsSort()
	{
		this.PropGoods = new GoodsSort(this.PropGoods, GoodsTab.Prop).Sort();
		this.FragmentGoods = new GoodsSort(this.FragmentGoods, GoodsTab.Fragment).Sort();
		this.EquimentGoods = new GoodsSort(this.EquimentGoods, GoodsTab.Equiment).Sort();
		this.RuneGoods = new GoodsSort(BackpackManager.Instance.RuneGoods, GoodsTab.Gem).Sort();
		this.EnchantmentGoods = new GoodsSort(this.EnchantmentGoods, GoodsTab.Enchantment).Sort();
	}

	public int OnGetTypeToSort(GoodsTab tp, int sortId)
	{
		List<int> type = this.sort.Find((Sort e) => e.id == (int)tp).type;
		return type.FindIndex((int e) => e == sortId);
	}

	public List<string> OnGetSort(GoodsTab tp)
	{
		return this.sort.Find((Sort e) => e.id == (int)tp).order;
	}

	public List<string> GetItemAttr(Items dataItem)
	{
		if (dataItem == null)
		{
			return null;
		}
		int attId = dataItem.atti;
		if (dataItem.firstType == 5)
		{
			attId = GemGlobal.GetAttrId(dataItem.id);
		}
		if (dataItem.tab == 5 && DataReader<FuMoDaoJuShuXing>.Contains(dataItem.id))
		{
			FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(dataItem.id);
			string[] array = fuMoDaoJuShuXing.describe.Split(new char[]
			{
				';'
			});
			List<string> list = new List<string>();
			string text = AttrUtility.GetAttrName(fuMoDaoJuShuXing.runeAttr) + "   " + string.Format(string.Concat(new string[]
			{
				"<color=#ff7d4b>",
				array[0],
				" - ",
				array[1],
				"</color>"
			}), new object[0]) + string.Empty;
			list.Add(text);
			return list;
		}
		return this.GetItemAttrText(attId);
	}

	public List<string> GetItemAttrText(int attId)
	{
		List<string> list = null;
		if (attId == 0)
		{
			return list;
		}
		Attrs attrs = DataReader<Attrs>.Get(attId);
		if (attrs == null)
		{
			return list;
		}
		if (attrs.attrs == null)
		{
			return list;
		}
		list = new List<string>();
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			list.Add(AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i), "ff7d4b", 3));
		}
		return list;
	}

	public List<string> GetItemSuit(Items dataItem)
	{
		if (dataItem == null)
		{
			return null;
		}
		List<string> list = null;
		if (dataItem.secondType == 1)
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(dataItem.id);
			if (zZhuangBeiPeiZhiBiao != null && zZhuangBeiPeiZhiBiao.firstGroupId > 0)
			{
				list = new List<string>();
				List<int> effectId = DataReader<zTaoZhuangPeiZhi>.Get(zZhuangBeiPeiZhiBiao.firstGroupId).effectId;
				List<int> effectValue = DataReader<zTaoZhuangPeiZhi>.Get(zZhuangBeiPeiZhiBiao.firstGroupId).effectValue;
				for (int i = 0; i < effectId.get_Count(); i++)
				{
					list.Add(AttrUtility.GetStandardAddDesc(effectId.get_Item(i), effectValue.get_Item(i), "ff7d4b", 3));
				}
			}
		}
		return list;
	}

	public bool IsBackpackFull()
	{
		bool result = false;
		if (this.Bag.get_Count() >= this.BagSize && this.lastCapacity >= this.BagSize && this.lastCapacity < BackpackManager.Instance.Bag.get_Count())
		{
			result = true;
		}
		this.lastCapacity = this.Bag.get_Count();
		return result;
	}

	public bool ShowBackpackFull()
	{
		return false;
	}

	private void CheckItemCanUseRecommendTip()
	{
		if (this.PropGoods != null)
		{
			for (int i = 0; i < this.PropGoods.get_Count(); i++)
			{
				Goods goods = this.PropGoods.get_Item(i);
				if (goods != null && goods.LocalItem != null && this.PropGoods.get_Item(i).LocalItem.useTips > 0)
				{
					if (this.ItemCanUseRecommendTipList == null)
					{
						this.ItemCanUseRecommendTipList = new List<long>();
					}
					int num = this.ItemCanUseRecommendTipList.FindIndex((long a) => a == goods.GetLongId());
					if (num < 0)
					{
						this.ItemCanUseRecommendTipList.Add(goods.GetLongId());
					}
				}
			}
		}
	}

	private void UpdateItemCanUseRecommendTip(Goods goods, bool isAdd)
	{
		if (goods == null || goods.LocalItem == null || goods.LocalItem.useTips <= 0 || goods.LocalItem.function <= 0)
		{
			return;
		}
		if (isAdd)
		{
			if (this.ItemCanUseRecommendTipList == null)
			{
				this.ItemCanUseRecommendTipList = new List<long>();
			}
			int num = this.ItemCanUseRecommendTipList.FindIndex((long a) => a == goods.GetLongId());
			if (num < 0)
			{
				this.ItemCanUseRecommendTipList.Add(goods.GetLongId());
			}
		}
		else if (this.ItemCanUseRecommendTipList != null)
		{
			int num2 = this.ItemCanUseRecommendTipList.FindIndex((long a) => a == goods.GetLongId());
			if (num2 >= 0)
			{
				this.ItemCanUseRecommendTipList.RemoveAt(num2);
			}
		}
		EventDispatcher.Broadcast(EventNames.UpdateItemUseRecommendTip);
	}

	public void ClearAllItemCanUseTip()
	{
		if (this.ItemCanUseRecommendTipList != null)
		{
			this.ItemCanUseRecommendTipList.Clear();
		}
	}

	public void ClearItemCanUseTip(long itemServerID)
	{
		if (this.ItemCanUseRecommendTipList != null)
		{
			int num = this.ItemCanUseRecommendTipList.FindIndex((long a) => a == itemServerID);
			if (num >= 0)
			{
				this.ItemCanUseRecommendTipList.RemoveAt(num);
			}
		}
	}

	public void GetEquimentGoods(ref List<Goods> list, int min_qualitylv, bool includeHighQuality)
	{
		if (list == null)
		{
			list = new List<Goods>();
		}
		list.Clear();
		for (int i = 0; i < this.EquimentGoods.get_Count(); i++)
		{
			int qualityLevelByEquipId = EquipGlobal.GetQualityLevelByEquipId(this.EquimentGoods.get_Item(i).GetItemId());
			if (includeHighQuality)
			{
				if (qualityLevelByEquipId >= min_qualitylv)
				{
					list.Add(this.EquimentGoods.get_Item(i));
				}
			}
			else if (qualityLevelByEquipId == min_qualitylv)
			{
				list.Add(this.EquimentGoods.get_Item(i));
			}
		}
	}

	public static string GetNameColor(int colorId)
	{
		return "#" + TextColorMgr.RGB.GetRGB(colorId);
	}

	public void CheckBagCanShowRedPoint()
	{
		this.IsCanShowRedPoint = false;
		for (int i = 0; i < this.PropGoods.get_Count(); i++)
		{
			Items item = this.PropGoods.get_Item(i).GetItem();
			if (item != null && item.secondType == 11 && item.minLv <= EntityWorld.Instance.EntSelf.Lv)
			{
				this.IsCanShowRedPoint = true;
				return;
			}
		}
	}
}
