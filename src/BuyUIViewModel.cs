using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuyUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_TxtVisisble = "AttrTxtVisible";

		public const string Attr_DefenceVisible = "DefenceVisible";

		public const string Attr_DefenceTxtNum = "DefenceTxtNum";

		public const string Attr_DodgeVisible = "DodgeVisible";

		public const string Attr_DodgeTxtNum = "DodgeTxtNum";

		public const string Attr_AttackVisible = "AttackVisible";

		public const string Attr_AttackTxtNum = "AttackTxtNum";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemDesc = "ItemDesc";

		public const string Attr_ItemOwn = "ItemOwn";

		public const string Attr_ItemNumName = "ItemNumName";

		public const string Attr_BuyCount = "BuyCount";

		public const string Attr_CostIcon = "CostIcon";

		public const string Attr_CostNum = "CostNum";

		public const string Attr_ItemProfessionName = "ItemProfessionName";

		public const string Attr_ItemProfession = "ItemProfession";

		public const string Attr_ItemFighting = "ItemFighting";

		public const string Attr_ItemFightingName = "ItemFightingName";

		public const string Attr_Input = "Input";

		public const string Attr_BtnAddEnable = "BtnAddEnable";

		public const string Attr_BtnMinuseEnable = "BtnMinuseEnable";

		public const string Attr_BtnOKEnable = "BtnOKEnable";

		public const string Attr_BtnOKName = "BtnOKName";

		public const string Event_OnBtnAddClick = "OnBtnAddClick";

		public const string Event_OnBtnMinuseClick = "OnBtnMinuseClick";

		public const string Event_OnBtnOKClick = "OnBtnOKClick";
	}

	public const int DEFAULT_COUNT = 1;

	private static BuyUIViewModel m_instance;

	public bool BuyNumberAdjustOn;

	public Action<int> BuyCallback;

	public string MAX_MORE_TIP = "超过最大购买数量";

	private int _BuyMaxCount = 1;

	private string _Input;

	private bool _BtnAddEnable = true;

	private bool _BtnMinuseEnable = true;

	private string _BuyCount;

	private bool _BtnOKEnable;

	private string _BtnOKName;

	private string _ItemName;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemDesc;

	private string _ItemNumName;

	private string _ItemOwn;

	private string _ItemProfession;

	private string _ItemProfessionName;

	private string _ItemFighting;

	private string _ItemFightingName;

	private SpriteRenderer _CostIcon;

	private string _CostNum;

	private string _AttackTxtNum;

	private string _DodgeTxtNum;

	private string _DefenceTxtNum;

	private bool _AttackVisible;

	private bool _DodgeVisible;

	private bool _DefenceVisible;

	private bool _AttrTxtVisible;

	private bool is_vip_privilege_on = true;

	private bool is_vip_level_on = true;

	private int m_iId;

	private int m_group_price;

	private int m_money_type;

	public static BuyUIViewModel Instance
	{
		get
		{
			return BuyUIViewModel.m_instance;
		}
	}

	public int BuyMaxCount
	{
		get
		{
			return this._BuyMaxCount;
		}
		set
		{
			this._BuyMaxCount = Mathf.Max(1, value);
		}
	}

	public string Input
	{
		get
		{
			return this._Input;
		}
		set
		{
			this._Input = value;
			if (this._Input.Contains("-"))
			{
				this._Input = 1.ToString();
			}
			int num = this.GetInputCount(this.Input);
			if (num > this.BuyMaxCount)
			{
				UIManagerControl.Instance.ShowToastText(this.MAX_MORE_TIP);
				num = this.BuyMaxCount;
				this._Input = num.ToString();
			}
			this.BtnAddEnable = (num < this.BuyMaxCount);
			if (num <= 0)
			{
				num = 1;
				this._Input = num.ToString();
			}
			this.BtnMinuseEnable = (num > 1);
			base.NotifyProperty("Input", this._Input);
			this.SetMoney(this.GetInputCount(this._Input));
		}
	}

	public bool BtnAddEnable
	{
		get
		{
			return this._BtnAddEnable;
		}
		set
		{
			this._BtnAddEnable = value;
			base.NotifyProperty("BtnAddEnable", value);
		}
	}

	public bool BtnMinuseEnable
	{
		get
		{
			return this._BtnMinuseEnable;
		}
		set
		{
			this._BtnMinuseEnable = value;
			base.NotifyProperty("BtnMinuseEnable", value);
		}
	}

	public string BuyCount
	{
		get
		{
			return this._BuyCount;
		}
		set
		{
			this._BuyCount = value;
			base.NotifyProperty("BuyCount", value);
		}
	}

	public bool BtnOKEnable
	{
		get
		{
			return this._BtnOKEnable;
		}
		set
		{
			this._BtnOKEnable = value;
			base.NotifyProperty("BtnOKEnable", value);
		}
	}

	public string BtnOKName
	{
		get
		{
			return this._BtnOKName;
		}
		set
		{
			this._BtnOKName = value;
			base.NotifyProperty("BtnOKName", value);
		}
	}

	public string ItemName
	{
		get
		{
			return this._ItemName;
		}
		set
		{
			this._ItemName = value;
			base.NotifyProperty("ItemName", value);
		}
	}

	public SpriteRenderer ItemFrame
	{
		get
		{
			return this._ItemFrame;
		}
		set
		{
			this._ItemFrame = value;
			base.NotifyProperty("ItemFrame", value);
		}
	}

	public SpriteRenderer ItemIcon
	{
		get
		{
			return this._ItemIcon;
		}
		set
		{
			this._ItemIcon = value;
			base.NotifyProperty("ItemIcon", value);
		}
	}

	public string ItemDesc
	{
		get
		{
			return this._ItemDesc;
		}
		set
		{
			this._ItemDesc = value;
			base.NotifyProperty("ItemDesc", value);
		}
	}

	public string ItemNumName
	{
		get
		{
			return this._ItemNumName;
		}
		set
		{
			this._ItemNumName = value;
			base.NotifyProperty("ItemNumName", value);
		}
	}

	public string ItemOwn
	{
		get
		{
			return this._ItemOwn;
		}
		set
		{
			this._ItemOwn = value;
			base.NotifyProperty("ItemOwn", value);
		}
	}

	public string ItemProfession
	{
		get
		{
			return this._ItemProfession;
		}
		set
		{
			this._ItemProfession = value;
			base.NotifyProperty("ItemProfession", value);
		}
	}

	public string ItemProfessionName
	{
		get
		{
			return this._ItemProfessionName;
		}
		set
		{
			this._ItemProfessionName = value;
			base.NotifyProperty("ItemProfessionName", value);
		}
	}

	public string ItemFighting
	{
		get
		{
			return this._ItemFighting;
		}
		set
		{
			this._ItemFighting = value;
			base.NotifyProperty("ItemFighting", value);
		}
	}

	public string ItemFightingName
	{
		get
		{
			return this._ItemFightingName;
		}
		set
		{
			this._ItemFightingName = value;
			base.NotifyProperty("ItemFightingName", value);
		}
	}

	public SpriteRenderer CostIcon
	{
		get
		{
			return this._CostIcon;
		}
		set
		{
			this._CostIcon = value;
			base.NotifyProperty("CostIcon", value);
		}
	}

	public string CostNum
	{
		get
		{
			return this._CostNum;
		}
		set
		{
			this._CostNum = value;
			base.NotifyProperty("CostNum", value);
		}
	}

	public string AttackTxtNum
	{
		get
		{
			return this._AttackTxtNum;
		}
		set
		{
			this._AttackTxtNum = value;
			base.NotifyProperty("AttackTxtNum", value);
		}
	}

	public string DodgeTxtNum
	{
		get
		{
			return this._DodgeTxtNum;
		}
		set
		{
			this._DodgeTxtNum = value;
			base.NotifyProperty("DodgeTxtNum", value);
		}
	}

	public string DefenceTxtNum
	{
		get
		{
			return this._DefenceTxtNum;
		}
		set
		{
			this._DefenceTxtNum = value;
			base.NotifyProperty("DefenceTxtNum", value);
		}
	}

	public bool AttackVisible
	{
		get
		{
			return this._AttackVisible;
		}
		set
		{
			this._AttackVisible = value;
			base.NotifyProperty("AttackVisible", value);
		}
	}

	public bool DodgeVisible
	{
		get
		{
			return this._DodgeVisible;
		}
		set
		{
			this._DodgeVisible = value;
			base.NotifyProperty("DodgeVisible", value);
		}
	}

	public bool DefenceVisible
	{
		get
		{
			return this._DefenceVisible;
		}
		set
		{
			this._DefenceVisible = value;
			base.NotifyProperty("DefenceVisible", value);
		}
	}

	public bool AttrTxtVisible
	{
		get
		{
			return this._AttrTxtVisible;
		}
		set
		{
			this._AttrTxtVisible = value;
			base.NotifyProperty("AttrTxtVisible", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		BuyUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		this.m_iId = 0;
		this.BuyNumberAdjustOn = false;
	}

	public void OnBtnMinuseClick()
	{
		int inputCount = this.GetInputCount(this.Input);
		if (inputCount > 0)
		{
			this.Input = (inputCount - 1).ToString();
		}
	}

	public void OnBtnAddClick()
	{
		int inputCount = this.GetInputCount(this.Input);
		this.Input = (inputCount + 1).ToString();
	}

	public void OnBtnOKClick()
	{
		BuyUIView.Instance.Show(false);
		if (!this.is_vip_privilege_on)
		{
			LinkNavigationManager.OpenVIPUI2VipLimit();
			return;
		}
		if (!this.is_vip_level_on)
		{
			LinkNavigationManager.OpenVIPUI2Recharge();
			return;
		}
		int inputCount = this.GetInputCount(this.Input);
		if (inputCount > this.BuyMaxCount)
		{
			UIManagerControl.Instance.ShowToastText(this.MAX_MORE_TIP);
			return;
		}
		if (this.BuyCallback != null)
		{
			this.BuyCallback.Invoke(inputCount);
		}
	}

	public void RefreshInfo(int iId, ShangPinBiao dataSPB, int group_price, int money_type)
	{
		if (dataSPB == null)
		{
			return;
		}
		this.ResetBuyRequire();
		this.BtnOKName = GameDataUtils.GetChineseContent(508013, false);
		this.m_iId = iId;
		int resId = dataSPB.resId;
		int resNum = dataSPB.resNum;
		Items dataItem = DataReader<Items>.Get(resId);
		this.SetItem(dataItem, resNum, money_type, group_price);
	}

	public void RefreshInfo(int iId, SShangPinKu dataSSPK, int group_price, int money_type, int buyMaxCount)
	{
		if (dataSSPK == null)
		{
			return;
		}
		StoreGoodsInfo commodityInfo = XMarketManager.Instance.GetCommodityInfo(dataSSPK.Id);
		if (commodityInfo == null)
		{
			return;
		}
		this.ResetBuyRequire();
		if (commodityInfo.extraInfo.vipLvLmt > 0)
		{
			this.is_vip_privilege_on = VIPManager.Instance.IsVIPPrivilegeOn();
			this.is_vip_level_on = (EntityWorld.Instance.EntSelf.VipLv >= commodityInfo.extraInfo.vipLvLmt);
		}
		if (!this.is_vip_privilege_on)
		{
			this.BtnOKName = string.Format("激活VIP{0}", commodityInfo.extraInfo.vipLvLmt);
		}
		else if (!this.is_vip_level_on)
		{
			this.BtnOKName = string.Format("VIP{0}可购买", commodityInfo.extraInfo.vipLvLmt);
		}
		else
		{
			this.BtnOKName = GameDataUtils.GetChineseContent(508013, false);
		}
		this.m_iId = iId;
		this.BuyMaxCount = buyMaxCount;
		int itemId = dataSSPK.itemId;
		int itemNum = 1;
		Items dataItem = DataReader<Items>.Get(itemId);
		this.SetItem(dataItem, itemNum, money_type, group_price);
	}

	public void RefreshInfo(int itemId, int num, int price, int money_type)
	{
		this.ResetBuyRequire();
		this.BtnOKName = GameDataUtils.GetChineseContent(508013, false);
		Items dataItem = DataReader<Items>.Get(itemId);
		this.SetItem(dataItem, num, money_type, price);
	}

	public void RefreshInfo(int itemID, int maxNum, int price, int money_type, string btnName, string titleName)
	{
		this.ResetBuyRequire();
		this.BtnOKName = btnName;
		this.BuyMaxCount = maxNum;
		int itemNum = 1;
		Items dataItem = DataReader<Items>.Get(itemID);
		this.SetItem(dataItem, itemNum, money_type, price);
		this.CostIcon = GameDataUtils.GetItemIcon(money_type);
		this.Input = 1.ToString();
		this.SetMoney(1);
	}

	private void ResetBuyRequire()
	{
		this.BtnOKEnable = true;
		this.is_vip_privilege_on = true;
		this.is_vip_level_on = true;
	}

	private void SetItem(Items dataItem, int itemNum, int money_type, int group_price)
	{
		if (dataItem == null)
		{
			return;
		}
		this.m_group_price = group_price;
		this.m_money_type = money_type;
		long num = BackpackManager.Instance.OnGetGoodCount(dataItem.id);
		this.ItemFrame = GameDataUtils.GetItemFrame(dataItem.id);
		this.ItemIcon = GameDataUtils.GetIcon(dataItem.icon);
		this.ItemName = GameDataUtils.GetItemName(dataItem, true);
		this.ItemDesc = GameDataUtils.GetItemDescWithTab(dataItem, "6c4734");
		this.BuyCount = string.Empty + itemNum;
		if (dataItem.firstType == 1)
		{
			this.ItemProfessionName = "职业：";
			this.ItemProfession = GameDataUtils.GetItemProfession(dataItem);
			this.ItemFightingName = "战力评分：";
			this.ItemFighting = EquipmentManager.Instance.GetEquipFightingByItemID(dataItem.id).ToString();
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(dataItem.id);
			if (zZhuangBeiPeiZhiBiao != null)
			{
				if (EntityWorld.Instance.EntSelf.Lv >= zZhuangBeiPeiZhiBiao.level)
				{
					this.ItemNumName = "穿戴等级：";
					this.ItemOwn = "    " + zZhuangBeiPeiZhiBiao.level.ToString();
				}
				else
				{
					this.ItemNumName = "<color=#ff0000>穿戴等级不足</color>";
					this.ItemOwn = string.Empty;
				}
			}
		}
		else
		{
			this.ItemNumName = "数量：";
			this.ItemProfessionName = string.Empty;
			this.ItemProfession = string.Empty;
			this.ItemOwn = num.ToString();
			this.ItemFightingName = GameDataUtils.GetItemProfession(dataItem);
			this.ItemFighting = string.Empty;
		}
		this.CostIcon = MoneyType.GetIcon(money_type);
		this.Input = 1.ToString();
		this.SetMoney(1);
		bool flag = false;
		this.DefenceVisible = flag;
		flag = flag;
		this.AttackVisible = flag;
		flag = flag;
		this.DodgeVisible = flag;
		this.AttrTxtVisible = flag;
		this.AttackTxtNum = string.Empty;
		this.DefenceTxtNum = string.Empty;
		this.DodgeTxtNum = string.Empty;
		int attId = dataItem.atti;
		if (dataItem.firstType == 5)
		{
			attId = GemGlobal.GetAttrId(dataItem.id);
		}
		List<string> itemAttrText = this.GetItemAttrText(attId);
		int attrs_count = 0;
		if (itemAttrText != null)
		{
			this.AttrTxtVisible = true;
			attrs_count = itemAttrText.get_Count();
			for (int i = 0; i < itemAttrText.get_Count(); i++)
			{
				switch (i)
				{
				case 0:
					this.AttackVisible = true;
					this.AttackTxtNum = itemAttrText.get_Item(0);
					break;
				case 1:
					this.DodgeVisible = true;
					this.DodgeTxtNum = itemAttrText.get_Item(1);
					break;
				case 2:
					this.DefenceVisible = true;
					this.DefenceTxtNum = itemAttrText.get_Item(2);
					break;
				}
			}
		}
		BuyUIView.Instance.SetAutoLayOut(attrs_count, this.BuyNumberAdjustOn);
	}

	private List<string> GetItemAttrText(int attId)
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
		if (attrs.attrs != null)
		{
			list = new List<string>();
			for (int i = 0; i < attrs.attrs.get_Count(); i++)
			{
				list.Add(AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i), "A55A41"));
			}
		}
		return list;
	}

	private int GetInputCount(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return 1;
		}
		if (this.Input.Contains("-"))
		{
			return 1;
		}
		return int.Parse(input);
	}

	private void SetMoney(int num)
	{
		int num2;
		if (this.m_iId > 0)
		{
			num2 = BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(this.m_iId, num);
		}
		else
		{
			num2 = this.m_group_price * num;
		}
		bool flag = MarketTools.IsEnoughMoney(num2, this.m_money_type);
		string text = "x" + num2;
		if (flag)
		{
			this.CostNum = " " + text;
		}
		else
		{
			this.CostNum = " " + TextColorMgr.GetColorByID(text, 1000007);
		}
	}
}
