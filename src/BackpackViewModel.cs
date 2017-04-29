using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackViewModel : ViewModelBase
{
	public class Names
	{
		public const string Items = "BackpackItems";

		public const string BackpackFrames = "BackpackFrames";

		public const string TextItems = "TextItems";

		public const string SuitTextItems = "SuitTextItems";

		public const string Count = "Count";

		public const string ItemProfession = "ItemProfession";

		public const string Decs = "Decs";

		public const string Decs2 = "Decs2";

		public const string Name = "Name";

		public const string Tips = "TipsName";

		public const string ItemStep = "ItemStep";

		public const string BtnSellName = "BtnSellName";

		public const string BtnSellBatchName = "BtnSellBatchName";

		public const string ComposeGemTip = "ComposeGemTip";

		public const string IsBtnComposeOneOn = "IsBtnComposeOneOn";

		public const string IsBtnComposeAllOn = "IsBtnComposeAllOn";

		public const string Attr_Visibility = "AttrVisibility";

		public const string Attr_SuitVisibility = "SuitVisibility";

		public const string Attr_SuitNameVisibility = "SuitNameVisibility";

		public const string Attr_TipsVisibility = "TipsVisibility";

		public const string Attr_PetFlagVisibility = "PetFlagVisibility";

		public const string Attr_ItemStepVisibility = "ItemStepVisibility";

		public const string Attr_ItemExcellentAttrVisibility = "ItemExcellentAttrVisibility";

		public const string ItemExcellentImage1 = "ItemExcellentImage1";

		public const string ItemExcellentImage2 = "ItemExcellentImage2";

		public const string ItemExcellentImage3 = "ItemExcellentImage3";

		public const string Attr_EquipStepVisibility = "EquipStepVisibility";

		public const string Attr_BtnUseRedPointVisibility = "BtnUseRedPointVisibility";

		public const string OnAutoDecomposeEquipment = "OnAutoDecomposeEquipment";

		public const string OnDecomposeEquipment = "OnDecomposeEquipment";

		public const string OnUse = "OnUse";

		public const string OnUseBatch = "OnUseBatch";

		public const string OnEquipment = "OnEquipment";

		public const string OnRune = "OnRune";

		public const string OnFragment = "OnFragment";

		public const string OnProp = "OnProp";

		public const string OnEnchantment = "OnEnchantment";

		public const string OnBtnComposeOne = "OnBtnComposeOne";

		public const string OnBtnComposeAll = "OnBtnComposeAll";

		public const string OnBtnSell = "OnBtnSell";

		public const string OnBtnSellBatch = "OnBtnSellBatch";

		public const string OnClickSelect = "OnClickSelect";
	}

	private static BackpackViewModel instance;

	public ObservableCollection<BackpackObservableItem> BackpackItems = new ObservableCollection<BackpackObservableItem>();

	public ObservableCollection<TextObservableItem> TextItems = new ObservableCollection<TextObservableItem>();

	public ObservableCollection<TextObservableItem> SuitTextItems = new ObservableCollection<TextObservableItem>();

	private string btn1 = "fenleianniu_1";

	private string btn2 = "fenleianniu_2";

	[HideInInspector]
	private int _selectIndex;

	[HideInInspector]
	private long _selectId;

	private List<Goods> goodsTpl;

	private Transform DetailPanel;

	private Transform EquipDetailPanel;

	private List<Transform> starTransformList;

	private Transform BaseAttr;

	private Transform ExcellentAttr;

	private Transform StarUpAttr;

	private Transform EnchantmentAttr;

	private bool tipsVisibility;

	private bool attrVisibility;

	private bool suitNameVisibility;

	private bool suitVisibility;

	private bool petFlagVisibility;

	private bool itemStepVisibility;

	private bool btnUseRedPointVisibility;

	private string count;

	private string _ItemProfession;

	private string decs;

	private string decs2;

	private string _Name;

	private string tipsName;

	private string itemStep;

	private string btnSellName;

	private string btnSellBatchName;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer bgIcon;

	private bool _ItemExcellentImage1;

	private bool _ItemExcellentImage2;

	private bool _ItemExcellentImage3;

	private bool _ItemExcellentAttrVisibility;

	private int _ExcellentCount;

	private SpriteRenderer propIcon;

	private SpriteRenderer equipmentIcon;

	private SpriteRenderer fragmentIcon;

	private SpriteRenderer runeIcon;

	private SpriteRenderer enchantmentIcon;

	private bool _IsBtnComposeOneOn;

	private bool _IsBtnComposeAllOn;

	private string _ComposeGemTip;

	private readonly Color HIGH_LIGHT = new Color(1f, 0.98f, 0.902f);

	private readonly Color LOW_LIGHT = new Color(1f, 0.843f, 0.549f);

	public GoodsTab current;

	private GoodsTab lastCurrent;

	private bool isUpdate;

	private int equipIconFxID;

	public static BackpackViewModel Instance
	{
		get
		{
			return BackpackViewModel.instance;
		}
	}

	private int selectIndex
	{
		get
		{
			return this._selectIndex;
		}
		set
		{
			this._selectIndex = value;
		}
	}

	public long selectId
	{
		get
		{
			return this._selectId;
		}
		set
		{
			this._selectId = value;
			this.selectIndex = this.OnGetClickItemIndex(this.current, value);
		}
	}

	public bool TipsVisibility
	{
		get
		{
			return this.tipsVisibility;
		}
		set
		{
			this.tipsVisibility = value;
			base.NotifyProperty("TipsVisibility", value);
		}
	}

	public bool AttrVisibility
	{
		get
		{
			return this.attrVisibility;
		}
		set
		{
			this.attrVisibility = value;
			base.NotifyProperty("AttrVisibility", value);
		}
	}

	public bool SuitNameVisibility
	{
		get
		{
			return this.suitNameVisibility;
		}
		set
		{
			this.suitNameVisibility = value;
			base.NotifyProperty("SuitNameVisibility", value);
		}
	}

	public bool SuitVisibility
	{
		get
		{
			return this.suitVisibility;
		}
		set
		{
			this.suitVisibility = value;
			base.NotifyProperty("SuitVisibility", value);
		}
	}

	public bool PetFlagVisibility
	{
		get
		{
			return this.petFlagVisibility;
		}
		set
		{
			this.petFlagVisibility = value;
			base.NotifyProperty("PetFlagVisibility", value);
		}
	}

	public bool ItemStepVisibility
	{
		get
		{
			return this.itemStepVisibility;
		}
		set
		{
			this.itemStepVisibility = value;
			base.NotifyProperty("ItemStepVisibility", value);
		}
	}

	public bool BtnUseRedPointVisibility
	{
		get
		{
			return this.btnUseRedPointVisibility;
		}
		set
		{
			this.btnUseRedPointVisibility = value;
			base.NotifyProperty("BtnUseRedPointVisibility", value);
		}
	}

	public string Count
	{
		get
		{
			return this.count;
		}
		set
		{
			if (this.count == value)
			{
				return;
			}
			this.count = value;
			base.NotifyProperty("Count", value);
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

	public string Decs
	{
		get
		{
			return this.decs;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			if (this.decs == value)
			{
				return;
			}
			this.decs = value;
			base.NotifyProperty("Decs", value);
		}
	}

	public string Decs2
	{
		get
		{
			return this.decs2;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			if (this.decs2 == value)
			{
				return;
			}
			this.decs2 = value;
			base.NotifyProperty("Decs2", value);
		}
	}

	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			if (this._Name == value)
			{
				return;
			}
			this._Name = value;
			base.NotifyProperty("Name", value);
		}
	}

	public string TipsName
	{
		get
		{
			return this.tipsName;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			if (this.tipsName == value)
			{
				return;
			}
			this.tipsName = value;
			base.NotifyProperty("TipsName", value);
		}
	}

	public string ItemStep
	{
		get
		{
			return this.itemStep;
		}
		set
		{
			this.itemStep = value;
			base.NotifyProperty("ItemStep", value);
		}
	}

	public string BtnSellName
	{
		get
		{
			return this.btnSellName;
		}
		set
		{
			this.btnSellName = value;
			base.NotifyProperty("BtnSellName", value);
		}
	}

	public string BtnSellBatchName
	{
		get
		{
			return this.btnSellBatchName;
		}
		set
		{
			this.btnSellBatchName = value;
			base.NotifyProperty("BtnSellBatchName", value);
		}
	}

	public int ItemColor
	{
		set
		{
			this.ItemFrame = GameDataUtils.GetItemFrame(value);
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
			if (this._ItemFrame == value)
			{
				return;
			}
			this._ItemFrame = value;
			base.NotifyProperty("ItemFrame", value);
		}
	}

	public SpriteRenderer BgIcon
	{
		get
		{
			return this.bgIcon;
		}
		set
		{
			if (this.bgIcon == value)
			{
				return;
			}
			this.bgIcon = value;
			base.NotifyProperty("BgIcon", value);
		}
	}

	public bool ItemExcellentImage1
	{
		get
		{
			return this._ItemExcellentImage1;
		}
		set
		{
			if (this._ItemExcellentImage1 != value)
			{
				this._ItemExcellentImage1 = value;
				base.NotifyProperty("ItemExcellentImage1", value);
			}
		}
	}

	public bool ItemExcellentImage2
	{
		get
		{
			return this._ItemExcellentImage2;
		}
		set
		{
			if (this._ItemExcellentImage2 != value)
			{
				this._ItemExcellentImage2 = value;
				base.NotifyProperty("ItemExcellentImage2", value);
			}
		}
	}

	public bool ItemExcellentImage3
	{
		get
		{
			return this._ItemExcellentImage3;
		}
		set
		{
			if (this._ItemExcellentImage3 != value)
			{
				this._ItemExcellentImage3 = value;
				base.NotifyProperty("ItemExcellentImage3", value);
			}
		}
	}

	public bool ItemExcellentAttrVisibility
	{
		get
		{
			return this._ItemExcellentAttrVisibility;
		}
		set
		{
			this._ItemExcellentAttrVisibility = value;
			base.NotifyProperty("ItemExcellentAttrVisibility", value);
		}
	}

	public int ExcellentCount
	{
		get
		{
			return this._ExcellentCount;
		}
		set
		{
			this._ExcellentCount = value;
			if (this._ExcellentCount > 0)
			{
				this.ItemExcellentAttrVisibility = true;
				this.ItemExcellentImage1 = (this._ExcellentCount >= 1);
				this.ItemExcellentImage2 = (this._ExcellentCount >= 2);
				this.ItemExcellentImage3 = (this._ExcellentCount >= 3);
			}
			else
			{
				this.ItemExcellentAttrVisibility = false;
			}
		}
	}

	public SpriteRenderer PropIcon
	{
		get
		{
			return this.propIcon;
		}
		set
		{
			if (this.propIcon == value)
			{
				return;
			}
			this.propIcon = value;
			base.NotifyProperty("PropIcon", value);
		}
	}

	public SpriteRenderer EquipmentIcon
	{
		get
		{
			return this.equipmentIcon;
		}
		set
		{
			if (this.equipmentIcon == value)
			{
				return;
			}
			this.equipmentIcon = value;
			base.NotifyProperty("EquipmentIcon", value);
		}
	}

	public SpriteRenderer FragmentIcon
	{
		get
		{
			return this.fragmentIcon;
		}
		set
		{
			if (this.fragmentIcon == value)
			{
				return;
			}
			this.fragmentIcon = value;
			base.NotifyProperty("FragmentIcon", value);
		}
	}

	public SpriteRenderer RuneIcon
	{
		get
		{
			return this.runeIcon;
		}
		set
		{
			if (this.runeIcon == value)
			{
				return;
			}
			this.runeIcon = value;
			base.NotifyProperty("RuneIcon", value);
		}
	}

	public SpriteRenderer EnchantmentIcon
	{
		get
		{
			return this.enchantmentIcon;
		}
		set
		{
			if (this.enchantmentIcon == value)
			{
				return;
			}
			this.enchantmentIcon = value;
			base.NotifyProperty("EnchantmentIcon", value);
		}
	}

	public bool IsBtnComposeOneOn
	{
		get
		{
			return this._IsBtnComposeOneOn;
		}
		set
		{
			this._IsBtnComposeOneOn = value;
			base.NotifyProperty("IsBtnComposeOneOn", value);
		}
	}

	public bool IsBtnComposeAllOn
	{
		get
		{
			return this._IsBtnComposeAllOn;
		}
		set
		{
			this._IsBtnComposeAllOn = value;
			base.NotifyProperty("IsBtnComposeAllOn", value);
		}
	}

	public string ComposeGemTip
	{
		get
		{
			return this._ComposeGemTip;
		}
		set
		{
			this._ComposeGemTip = value;
			base.NotifyProperty("ComposeGemTip", value);
		}
	}

	protected override void Awake()
	{
		BackpackViewModel.instance = this;
		base.Awake();
		this.DetailPanel = base.get_transform().FindChild("Panel2").FindChild("DetailPanel");
		this.EquipDetailPanel = base.get_transform().FindChild("Panel2").FindChild("EquipDetailPanel");
		this.starTransformList = new List<Transform>();
		for (int i = 1; i < 16; i++)
		{
			Transform transform = this.EquipDetailPanel.FindChild("starList").FindChild("star" + i);
			if (transform != null)
			{
				this.starTransformList.Add(transform);
			}
		}
		this.BaseAttr = this.EquipDetailPanel.FindChild("attrScrollRect").FindChild("Autoarrange").FindChild("BaseAttr");
		this.ExcellentAttr = this.EquipDetailPanel.FindChild("attrScrollRect").FindChild("Autoarrange").FindChild("ExcellentAttr");
		this.StarUpAttr = this.EquipDetailPanel.FindChild("attrScrollRect").FindChild("Autoarrange").FindChild("StarUpAttr");
		this.EnchantmentAttr = this.EquipDetailPanel.FindChild("attrScrollRect").FindChild("Autoarrange").FindChild("EnchantmentAttr");
	}

	private void OnEnable()
	{
		this.OpenBackackData(GoodsTab.Prop);
	}

	private void OnDisable()
	{
		this.BtnLowLight(this.lastCurrent);
		this.current = GoodsTab.None;
		this.lastCurrent = GoodsTab.None;
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.OnUpdateGoods, new Callback(this.UpdateBackackData));
		EventDispatcher.AddListener<List<DecomposeItem>>(EventNames.OnDecomposeEquipRes, new Callback<List<DecomposeItem>>(this.OnDecomposeEquipRes));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OnUpdateGoods, new Callback(this.UpdateBackackData));
		EventDispatcher.RemoveListener<List<DecomposeItem>>(EventNames.OnDecomposeEquipRes, new Callback<List<DecomposeItem>>(this.OnDecomposeEquipRes));
	}

	private void OnClickBtn(GoodsTab tab)
	{
		this.current = tab;
		if (this.current == this.lastCurrent)
		{
			return;
		}
		this.BtnLowLight(this.lastCurrent);
		this.BtnHighLight(this.current);
		this.UpdateGoods();
		this.lastCurrent = this.current;
	}

	private void OnDecomposeEquipRes(List<DecomposeItem> items)
	{
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		for (int i = 0; i < items.get_Count(); i++)
		{
			list.Add(items.get_Item(i).itemId);
			list2.Add((long)items.get_Item(i).num);
		}
		if (list != null && list.get_Count() > 0)
		{
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
			rewardUI.get_transform().SetAsLastSibling();
			rewardUI.SetRewardItem("分解返还物品", list, list2, true, false, null, null);
		}
	}

	private int OnGetClickItemIndex(GoodsTab tab, long Id)
	{
		switch (tab)
		{
		case GoodsTab.Prop:
			return BackpackManager.Instance.PropGoods.FindIndex((Goods e) => e.GetLongId() == Id);
		case GoodsTab.Equiment:
			return BackpackManager.Instance.EquimentGoods.FindIndex((Goods e) => e.GetLongId() == Id);
		case GoodsTab.Gem:
			return BackpackManager.Instance.RuneGoods.FindIndex((Goods e) => e.GetLongId() == Id);
		case GoodsTab.Fragment:
			return BackpackManager.Instance.FragmentGoods.FindIndex((Goods e) => e.GetLongId() == Id);
		case GoodsTab.Enchantment:
			return BackpackManager.Instance.EnchantmentGoods.FindIndex((Goods e) => e.GetLongId() == Id);
		default:
			Debuger.Error("什么鬼 没有对应的Tab：" + tab, new object[0]);
			return -1;
		}
	}

	public void OnAutoDecomposeEquipment()
	{
		UIManagerControl.Instance.OpenUI("EquipDecomposeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public void OnDecomposeEquipment()
	{
		Items localItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(localItem.id);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			List<DecomposeEquipInfo> decomposeList = new List<DecomposeEquipInfo>();
			DecomposeEquipInfo decomposeEquipInfo = new DecomposeEquipInfo();
			decomposeEquipInfo.position = zZhuangBeiPeiZhiBiao.position;
			decomposeEquipInfo.equipIds.Add(this.selectId);
			decomposeList.Add(decomposeEquipInfo);
			if (zZhuangBeiPeiZhiBiao.quality >= 5)
			{
				DialogBoxUIViewModel.Instance.ShowAsOKCancel("分解装备", "该装备属于贵重装备，是否确认分解", null, delegate
				{
					EquipmentManager.Instance.SendDecomposeEquipReq(decomposeList);
				}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			}
			else
			{
				EquipmentManager.Instance.SendDecomposeEquipReq(decomposeList);
			}
		}
	}

	public void OnUse()
	{
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		Items good = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		if (VipTasteCardManager.Instance.CheckId == good.id)
		{
			VipTasteCardManager.Instance.SendUseCard(good.id);
			VipTasteCardManager.Instance.isHaveShow = true;
			return;
		}
		if (good.function == 2)
		{
			if (OffHookManager.Instance.IsOnLegalCheckHookTime())
			{
				OffHookManager.Instance.SendBuyPlanReq(good.id);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(330034, false), 1f, 1f);
			}
			return;
		}
		if (good.function == 3)
		{
			RenameUI renameUI = UIManagerControl.Instance.OpenUI("RenameUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RenameUI;
			renameUI.SetItem(good.id);
			return;
		}
		if (good.secondType == 44)
		{
			PayTreasureUI payTreasureUI = UIManagerControl.Instance.OpenUI("PayTreasureUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PayTreasureUI;
			payTreasureUI.SetShowItem(good.id, delegate
			{
				BackpackManager.Instance.SendUseGood(this.selectId, 1, good.promptWay);
			});
			return;
		}
		BackpackManager.Instance.SendUseGood(this.selectId, 1, good.promptWay);
	}

	public void OnUseBatch()
	{
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		Items dataItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		int num = BackpackManager.Instance.OnGetGoodCount(this.selectId);
		FindTaskUI uibase = UIManagerControl.Instance.OpenUI("FindTaskUI", null, false, UIType.NonPush) as FindTaskUI;
		uibase.OnOpen("批量使用", (float)num, 0f, string.Empty, false, delegate(float value)
		{
			uibase.SetDetailUseBatch();
		}, delegate(int value)
		{
			BackpackManager.Instance.SendUseGood(this.selectId, value, dataItem.promptWay);
		});
	}

	public void OnBtnComposeOne()
	{
		Items localItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		if (localItem == null)
		{
			return;
		}
		if (this.current == GoodsTab.Gem)
		{
			GemGlobal.ComposeGemOne(localItem.id);
		}
		else if (this.current == GoodsTab.Enchantment)
		{
			EquipGlobal.SendComposeOne(localItem.id, EquipComposeType.EnchantmentCompose);
		}
		else if (localItem.id >= 8 && localItem.id <= 10)
		{
			EquipGlobal.SendComposeOne(localItem.id, EquipComposeType.StarCompose);
		}
	}

	public void OnBtnComposeAll()
	{
		Items localItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		if (localItem == null)
		{
			return;
		}
		if (this.current == GoodsTab.Gem)
		{
			GemGlobal.ComposeGemAll(localItem.id);
		}
		else if (this.current == GoodsTab.Enchantment)
		{
			EquipGlobal.SendComposeAll(localItem.id, EquipComposeType.EnchantmentCompose);
		}
		else if (localItem.id >= 8 && localItem.id <= 10)
		{
			EquipGlobal.SendComposeAll(localItem.id, EquipComposeType.StarCompose);
		}
	}

	public void OnBtnSell()
	{
		Items localItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		if (localItem.sellPrice.get_Count() <= 0)
		{
			return;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), "是否确认出售此物品", null, delegate
		{
			BackpackManager.Instance.SendSellGood(this.selectId, 1);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	public void OnBtnSellBatch()
	{
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		Items localItem = BackpackManager.Instance.OnGetGood(this.selectId).LocalItem;
		int num = BackpackManager.Instance.OnGetGoodCount(this.selectId);
		FindTaskUI uibase = UIManagerControl.Instance.OpenUI("FindTaskUI", null, false, UIType.NonPush) as FindTaskUI;
		string title = "批量" + this.BtnSellName;
		uibase.OnOpen(title, (float)num, 0f, string.Empty, false, delegate(float value)
		{
			uibase.SetDetailUseBatch();
		}, delegate(int value)
		{
			BackpackManager.Instance.SendSellGood(this.selectId, value);
		});
	}

	private void BtnHighLight(GoodsTab type)
	{
		switch (type)
		{
		case GoodsTab.Prop:
			this.PropIcon = ResourceManager.GetIconSprite(this.btn1);
			BackpackUI.Instance.btnTexts.get_Item(0).set_color(this.HIGH_LIGHT);
			break;
		case GoodsTab.Equiment:
			this.EquipmentIcon = ResourceManager.GetIconSprite(this.btn1);
			BackpackUI.Instance.btnTexts.get_Item(1).set_color(this.HIGH_LIGHT);
			break;
		case GoodsTab.Gem:
			this.RuneIcon = ResourceManager.GetIconSprite(this.btn1);
			BackpackUI.Instance.btnTexts.get_Item(2).set_color(this.HIGH_LIGHT);
			break;
		case GoodsTab.Fragment:
			this.FragmentIcon = ResourceManager.GetIconSprite(this.btn1);
			BackpackUI.Instance.btnTexts.get_Item(3).set_color(this.HIGH_LIGHT);
			break;
		case GoodsTab.Enchantment:
			this.EnchantmentIcon = ResourceManager.GetIconSprite(this.btn1);
			BackpackUI.Instance.btnTexts.get_Item(4).set_color(this.HIGH_LIGHT);
			break;
		}
	}

	private void BtnLowLight(GoodsTab type)
	{
		switch (type)
		{
		case GoodsTab.Prop:
			this.PropIcon = ResourceManager.GetIconSprite(this.btn2);
			BackpackUI.Instance.btnTexts.get_Item(0).set_color(this.LOW_LIGHT);
			break;
		case GoodsTab.Equiment:
			this.EquipmentIcon = ResourceManager.GetIconSprite(this.btn2);
			BackpackUI.Instance.btnTexts.get_Item(1).set_color(this.LOW_LIGHT);
			break;
		case GoodsTab.Gem:
			this.RuneIcon = ResourceManager.GetIconSprite(this.btn2);
			BackpackUI.Instance.btnTexts.get_Item(2).set_color(this.LOW_LIGHT);
			break;
		case GoodsTab.Fragment:
			this.FragmentIcon = ResourceManager.GetIconSprite(this.btn2);
			BackpackUI.Instance.btnTexts.get_Item(3).set_color(this.LOW_LIGHT);
			break;
		case GoodsTab.Enchantment:
			this.EnchantmentIcon = ResourceManager.GetIconSprite(this.btn2);
			BackpackUI.Instance.btnTexts.get_Item(4).set_color(this.LOW_LIGHT);
			break;
		}
	}

	private void SetTab()
	{
		switch (this.current)
		{
		case GoodsTab.Prop:
			this.OnProp();
			break;
		case GoodsTab.Equiment:
			this.OnEquipment();
			break;
		case GoodsTab.Gem:
			this.OnRune();
			break;
		case GoodsTab.Fragment:
			this.OnFragment();
			break;
		case GoodsTab.Enchantment:
			this.OnEnchantment();
			break;
		}
	}

	public void OnProp()
	{
		this.OnClickBtn(GoodsTab.Prop);
	}

	public void OnEquipment()
	{
		this.OnClickBtn(GoodsTab.Equiment);
	}

	public void OnRune()
	{
		this.OnClickBtn(GoodsTab.Gem);
	}

	public void OnFragment()
	{
		this.OnClickBtn(GoodsTab.Fragment);
	}

	public void OnEnchantment()
	{
		this.OnClickBtn(GoodsTab.Enchantment);
	}

	private void UpdateBackackData()
	{
		if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeSelf())
		{
			this.isUpdate = true;
			this.UpdateGoods();
		}
	}

	private void UpdateGoods()
	{
		this.SetCurrentGoods();
		this.BackpackItems.Clear();
		if (this.goodsTpl != null && this.goodsTpl.get_Count() > 0)
		{
			this.SetCurrentGoodTabDetails();
			this.SetItems();
			this.SetCurrentItemSelected();
		}
		else
		{
			this.EquipDetailPanel.get_gameObject().SetActive(false);
			this.DetailPanel.get_gameObject().SetActive(false);
			this.TipsVisibility = true;
		}
		this.SetBlankItems();
		this.isUpdate = false;
	}

	private void SetCurrentGoods()
	{
		switch (this.current)
		{
		case GoodsTab.Prop:
			this.goodsTpl = BackpackManager.Instance.PropGoods;
			break;
		case GoodsTab.Equiment:
			this.goodsTpl = BackpackManager.Instance.EquimentGoods;
			break;
		case GoodsTab.Gem:
			this.goodsTpl = BackpackManager.Instance.RuneGoods;
			break;
		case GoodsTab.Fragment:
			this.goodsTpl = BackpackManager.Instance.FragmentGoods;
			break;
		case GoodsTab.Enchantment:
			this.goodsTpl = BackpackManager.Instance.EnchantmentGoods;
			break;
		}
	}

	private void SetCurrentGoodTabDetails()
	{
		if (this.current == GoodsTab.Equiment)
		{
			this.TipsVisibility = false;
			this.DetailPanel.get_gameObject().SetActive(false);
			this.EquipDetailPanel.get_gameObject().SetActive(true);
		}
		else
		{
			this.TipsVisibility = false;
			this.DetailPanel.get_gameObject().SetActive(true);
			this.EquipDetailPanel.get_gameObject().SetActive(false);
		}
	}

	private void SetItems()
	{
		for (int i = 0; i < this.goodsTpl.get_Count(); i++)
		{
			this.BackpackItems.Add(BackpackTools.GetBackpackObservableItem(this.goodsTpl.get_Item(i), new Action<BackpackObservableItem>(this.BackpackObservableItemClick), 1));
		}
	}

	private void SetBlankItems()
	{
		int num = (this.goodsTpl == null) ? 25 : (25 - this.goodsTpl.get_Count());
		if (num < 0)
		{
			num = 5 - this.goodsTpl.get_Count() % 5;
		}
		if (num >= 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.BackpackItems.Add(BackpackTools.GetBackpackObservableItem(null, new Action<BackpackObservableItem>(this.BackpackObservableItemClick), 1));
			}
		}
	}

	private void SetCurrentItemSelected()
	{
		if (this.isUpdate)
		{
			int num = this.goodsTpl.FindIndex((Goods e) => e.GetLongId() == this.selectId);
			if (num < 0)
			{
				this.selectIndex = ((this.selectIndex - 1 >= 0) ? (this.selectIndex - 1) : this.selectIndex);
			}
			else
			{
				this.selectIndex = num;
			}
		}
		else
		{
			this.selectIndex = 0;
		}
		if (this.BackpackItems.Count > this.selectIndex)
		{
			this.BackpackItems[this.selectIndex].SetIsSelected(true);
			this.UpdatePanel2(this.goodsTpl.get_Item(this.selectIndex).GetItemId(), this.goodsTpl.get_Item(this.selectIndex).GetLongId());
		}
	}

	private void BackpackObservableItemClick(BackpackObservableItem item)
	{
		if (item.ItemRootNullOn)
		{
			return;
		}
		for (int i = 0; i < this.BackpackItems.Count; i++)
		{
			BackpackObservableItem item2 = this.BackpackItems.GetItem(i);
			item2.SetIsSelected(item == item2);
		}
		this.UpdatePanel2(BackpackManager.Instance.OnGetGood(item.id).GetItemId(), item.id);
	}

	public void UpdatePanel2(int itemId, long longID)
	{
		Items item = BackpackManager.Instance.GetItem(itemId);
		if (item != null)
		{
			this.selectId = longID;
			if (this.current == GoodsTab.Equiment)
			{
				this.SetEquipItem(this.selectId);
			}
			else
			{
				if (this.current == GoodsTab.Gem)
				{
					this.SetGemButtons(item);
				}
				else if (this.current == GoodsTab.Enchantment)
				{
					this.SetEnchantmentButtons(item);
				}
				else if (item.id >= 8 && item.id <= 10)
				{
					BackpackUI.Instance.ShowButtonSell(false, false, false);
					BackpackUI.Instance.ShowButtonUse(false, false);
					BackpackUI.Instance.ShowButtonCompose(true);
					bool flag = EquipGlobal.IsCanCompose(item.id, EquipComposeType.StarCompose);
					this.IsBtnComposeOneOn = flag;
					this.IsBtnComposeAllOn = flag;
					this.ComposeGemTip = EquipGlobal.GetEnchantmentComposeTip(item.id, EquipComposeType.StarCompose);
				}
				else if (item.secondType == 6)
				{
					BackpackUI.Instance.ShowButtonUse(false, false);
					BackpackUI.Instance.ShowButtonCompose(false);
					bool isShowSell = item.sellPrice.get_Count() > 0;
					bool isBatchOn = BackpackManager.Instance.OnGetGoodCount(longID) > 1;
					BackpackUI.Instance.ShowButtonSell(isShowSell, false, isBatchOn);
					this.BtnSellName = GameDataUtils.GetChineseContent(505072, false);
					this.BtnSellBatchName = "一键兑换";
				}
				else if (item.secondType == 44)
				{
					BackpackUI.Instance.ShowButtonUse(false, false);
					BackpackUI.Instance.ShowButtonCompose(false);
					bool isShowSell2 = item.sellPrice.get_Count() > 0;
					BackpackUI.Instance.ShowButtonSell(isShowSell2, true, false);
					this.BtnSellName = "出 售";
				}
				else
				{
					BackpackUI.Instance.ShowButtonCompose(false);
					BackpackUI.Instance.ShowButtonSell(false, false, false);
					this.BtnUseRedPointVisibility = false;
					if (item.function == 1 || item.function == 2)
					{
						int num = BackpackManager.Instance.OnGetGoodCount(longID);
						bool isBatchOn2 = num > 1 && item.secondType == 11;
						BackpackUI.Instance.ShowButtonUse(true, isBatchOn2);
						if (item.secondType == 11 && item.minLv <= EntityWorld.Instance.EntSelf.Lv)
						{
							this.BtnUseRedPointVisibility = true;
						}
					}
					else if (item.function == 3)
					{
						BackpackUI.Instance.ShowButtonUse(true, false);
						if (item.secondType == 11 && item.minLv <= EntityWorld.Instance.EntSelf.Lv)
						{
							this.BtnUseRedPointVisibility = true;
						}
					}
					else
					{
						BackpackUI.Instance.ShowButtonUse(false, false);
					}
				}
				this.SetItemInfo(item);
				this.SetItemAttrsAndDesc(item);
			}
		}
	}

	private void SetItemInfo(Items dataItem)
	{
		this.ItemColor = dataItem.id;
		this.BgIcon = GameDataUtils.GetIcon(dataItem.icon);
		this.PetFlagVisibility = false;
		this.TipsName = ((EntityWorld.Instance.EntSelf.Lv >= dataItem.minLv) ? string.Empty : string.Format(GameDataUtils.GetChineseContent(509011, false), dataItem.minLv));
		this.Name = string.Format("<color={0}>{1}</color>", BackpackManager.GetNameColor(dataItem.color), GameDataUtils.GetChineseContent(dataItem.name, false));
		this.Count = string.Format("数量：<color=#a55a41>{0}</color>", BackpackManager.Instance.OnGetGoodCount(this.selectId));
		this.ItemProfession = GameDataUtils.GetItemProfession(dataItem);
		this.ItemStepVisibility = (dataItem.step > 0);
		this.ItemStep = string.Format(GameDataUtils.GetChineseContent(505023, false), dataItem.step);
		this.ExcellentCount = dataItem.gogok;
	}

	private void SetItemAttrsAndDesc(Items dataItem)
	{
		List<string> itemAttr = BackpackManager.Instance.GetItemAttr(dataItem);
		if (itemAttr != null)
		{
			this.AttrVisibility = true;
			this.TextItems.Clear();
			for (int i = 0; i < itemAttr.get_Count(); i++)
			{
				this.TextItems.Add(new TextObservableItem
				{
					Content = itemAttr.get_Item(i)
				});
			}
		}
		else
		{
			this.AttrVisibility = false;
		}
		List<string> itemSuit = BackpackManager.Instance.GetItemSuit(dataItem);
		if (itemSuit != null)
		{
			this.SuitNameVisibility = true;
			this.SuitVisibility = true;
			this.SuitTextItems.Clear();
			for (int j = 0; j < itemSuit.get_Count(); j++)
			{
				this.SuitTextItems.Add(new TextObservableItem
				{
					Content = itemSuit.get_Item(j)
				});
			}
		}
		else
		{
			this.SuitNameVisibility = false;
			this.SuitVisibility = false;
		}
		this.Decs = GameDataUtils.GetItemDescWithTab(dataItem, "a55a41");
		this.Decs2 = ((dataItem.describeId2 <= 0) ? string.Empty : GameDataUtils.GetChineseContent(dataItem.describeId2, false));
	}

	private void SetGemButtons(Items dataItem)
	{
		BackpackUI.Instance.ShowButtonSell(false, false, false);
		BackpackUI.Instance.ShowButtonUse(false, false);
		BackpackUI.Instance.ShowButtonCompose(true);
		bool flag = GemGlobal.IsCanCompose(dataItem.id);
		this.IsBtnComposeOneOn = flag;
		this.IsBtnComposeAllOn = flag;
		this.ComposeGemTip = GemGlobal.GetComposeGemTip(dataItem.id);
	}

	private void SetEnchantmentButtons(Items dataItem)
	{
		BackpackUI.Instance.ShowButtonSell(false, false, false);
		BackpackUI.Instance.ShowButtonUse(false, false);
		BackpackUI.Instance.ShowButtonCompose(true);
		bool flag = EquipGlobal.IsCanCompose(dataItem.id, EquipComposeType.EnchantmentCompose);
		this.IsBtnComposeOneOn = flag;
		this.IsBtnComposeAllOn = flag;
		this.ComposeGemTip = EquipGlobal.GetEnchantmentComposeTip(dataItem.id, EquipComposeType.EnchantmentCompose);
	}

	private void OpenBackackData(GoodsTab tab)
	{
		this.current = tab;
		this.SetTab();
		BackpackManager.Instance.SendCleanUpReq();
	}

	private void SetEquipItem(long EquipID)
	{
		EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(EquipID);
		if (equipSimpleInfoByEquipID == null)
		{
			return;
		}
		Dictionary<string, string> equipIconNamesByEquipDataID = EquipGlobal.GetEquipIconNamesByEquipDataID(equipSimpleInfoByEquipID.cfgId, true);
		if (equipIconNamesByEquipDataID == null)
		{
			return;
		}
		FXSpineManager.Instance.DeleteSpine(this.equipIconFxID, true);
		ResourceManager.SetSprite(this.EquipDetailPanel.FindChild("EquipIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconName")));
		int qualityLevel = EquipGlobal.GetQualityLevel(BackpackManager.Instance.GetItem(equipSimpleInfoByEquipID.cfgId).color);
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(EquipID);
		if (equipForgeCfgData != null && equipSimpleInfoByEquipID.suitId > 0)
		{
			ResourceManager.SetSprite(this.EquipDetailPanel.FindChild("EquipItemFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			this.EquipDetailPanel.FindChild("EquipItemName").GetComponent<Text>().set_text(TextColorMgr.GetColor(EquipGlobal.GetEquipSuitMarkName(equipSimpleInfoByEquipID.suitId) + GameDataUtils.GetItemName(equipSimpleInfoByEquipID.cfgId, false, 0L), "FF1919", string.Empty));
			this.equipIconFxID = FXSpineManager.Instance.PlaySpine(equipForgeCfgData.fxId, this.EquipDetailPanel.FindChild("EquipIcon"), "BackpackUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			ResourceManager.SetSprite(this.EquipDetailPanel.FindChild("EquipItemFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconFrameName")));
			this.EquipDetailPanel.FindChild("EquipItemName").GetComponent<Text>().set_text(equipIconNamesByEquipDataID.get_Item("ItemName"));
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipSimpleInfoByEquipID.equipId, 1f);
			this.equipIconFxID = EquipGlobal.GetEquipIconFX(equipSimpleInfoByEquipID.cfgId, excellentAttrsCountByColor, this.EquipDetailPanel.FindChild("EquipIcon"), "BackpackUI", 2000, false);
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfoByEquipID.cfgId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return;
		}
		this.EquipDetailPanel.FindChild("ItemLv").GetComponent<Text>().set_text(zZhuangBeiPeiZhiBiao.level.ToString());
		string equipOccupationName = EquipGlobal.GetEquipOccupationName(zZhuangBeiPeiZhiBiao.id);
		this.EquipDetailPanel.FindChild("ItemCareerLimit").GetComponent<Text>().set_text(equipOccupationName);
		int equipFightingByEquipID = EquipmentManager.Instance.GetEquipFightingByEquipID(equipSimpleInfoByEquipID.equipId);
		this.EquipDetailPanel.FindChild("ItemFighting").GetComponent<Text>().set_text(equipFightingByEquipID.ToString());
		this.EquipDetailPanel.FindChild("EquipStepText").GetComponent<Text>().set_text(equipIconNamesByEquipDataID.get_Item("EquipStep"));
		this.EquipDetailPanel.FindChild("AdvancedTipText").GetComponent<Text>().set_text(string.Empty);
		DepthOfUI depthOfUI = this.EquipDetailPanel.FindChild("EquipStepImg").GetComponent<DepthOfUI>();
		if (depthOfUI == null)
		{
			depthOfUI = this.EquipDetailPanel.FindChild("EquipStepImg").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI.SortingOrder = 2001;
		DepthOfUI depthOfUI2 = this.EquipDetailPanel.FindChild("EquipStepText").GetComponent<DepthOfUI>();
		if (depthOfUI2 == null)
		{
			depthOfUI2 = this.EquipDetailPanel.FindChild("EquipStepText").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI2.SortingOrder = 2001;
		DepthOfUI depthOfUI3 = this.EquipDetailPanel.FindChild("ImageBinding").GetComponent<DepthOfUI>();
		if (depthOfUI3 != null)
		{
			depthOfUI3 = this.EquipDetailPanel.FindChild("ImageBinding").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI3.SortingOrder = 2001;
		int i;
		for (i = 0; i < zZhuangBeiPeiZhiBiao.starNum; i++)
		{
			this.starTransformList.get_Item(i).get_gameObject().SetActive(true);
			if (equipSimpleInfoByEquipID.star > i)
			{
				this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(true);
				string starLevelSpriteName = this.GetStarLevelSpriteName(equipSimpleInfoByEquipID.starToMaterial.get_Item(i));
				ResourceManager.SetSprite(this.starTransformList.get_Item(i).FindChild("OpenStar").GetComponent<Image>(), ResourceManager.GetIconSprite(starLevelSpriteName));
			}
			else
			{
				this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(false);
			}
		}
		for (int j = i; j < this.starTransformList.get_Count(); j++)
		{
			this.starTransformList.get_Item(j).get_gameObject().SetActive(false);
		}
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		if (attrs != null)
		{
			for (int k = 0; k < attrs.attrs.get_Count(); k++)
			{
				if (k > 2)
				{
					break;
				}
				long value = (long)attrs.values.get_Item(k);
				this.BaseAttr.FindChild("EquipItem2Text" + k).get_gameObject().SetActive(true);
				this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Item2Text").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc((GameData.AttrType)attrs.attrs.get_Item(k), value, "ff7d4b"));
			}
			for (int l = attrs.attrs.get_Count(); l < 3; l++)
			{
				this.BaseAttr.FindChild("EquipItem2Text" + l).get_gameObject().SetActive(false);
			}
		}
		Transform transform = this.EquipDetailPanel.FindChild("ImageBinding");
		if (transform != null)
		{
			transform.get_gameObject().SetActive(equipSimpleInfoByEquipID.binding);
		}
		Transform transform2 = this.EquipDetailPanel.FindChild("ExcellentAttrIconList");
		transform2.get_gameObject().SetActive(false);
		if (equipSimpleInfoByEquipID.excellentAttrs.get_Count() > 0)
		{
			this.ExcellentAttr.get_gameObject().SetActive(true);
			int m = 0;
			int num = 0;
			while (m < equipSimpleInfoByEquipID.excellentAttrs.get_Count())
			{
				if (m >= 5)
				{
					break;
				}
				if (equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).attrId < 0)
				{
					this.ExcellentAttr.FindChild("EquipItem2Text" + m).get_gameObject().SetActive(false);
				}
				else
				{
					string excellentTypeColor = EquipGlobal.GetExcellentTypeColor(equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).color);
					string text = string.Empty;
					text = AttrUtility.GetStandardAddDesc(equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).attrId, equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).value, excellentTypeColor, excellentTypeColor);
					this.ExcellentAttr.FindChild("EquipItem2Text" + m).get_gameObject().SetActive(true);
					this.ExcellentAttr.FindChild("EquipItem2Text" + m).FindChild("Item2Text").GetComponent<Text>().set_text(text);
					string excellentRangeText = EquipGlobal.GetExcellentRangeText(equipSimpleInfoByEquipID.cfgId, equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).attrId);
					this.ExcellentAttr.FindChild("EquipItem2Text" + m).FindChild("Item2TextRange").GetComponent<Text>().set_text(excellentRangeText);
					if (equipSimpleInfoByEquipID.excellentAttrs.get_Item(m).color >= 1f)
					{
						this.ExcellentAttr.FindChild("EquipItem2Text" + m).FindChild("ItemImg").GetComponent<Image>().set_enabled(true);
						num++;
					}
					else
					{
						this.ExcellentAttr.FindChild("EquipItem2Text" + m).FindChild("ItemImg").GetComponent<Image>().set_enabled(false);
					}
				}
				m++;
			}
			for (int n = m; n < 5; n++)
			{
				this.ExcellentAttr.FindChild("EquipItem2Text" + n).get_gameObject().SetActive(false);
			}
			if (num > 0)
			{
				if (!transform2.get_gameObject().get_activeSelf())
				{
					transform2.get_gameObject().SetActive(true);
				}
				for (int num2 = 0; num2 < num; num2++)
				{
					if (num2 >= 3)
					{
						break;
					}
					transform2.FindChild("Image" + (num2 + 1)).get_gameObject().SetActive(true);
				}
				for (int num3 = num; num3 < 3; num3++)
				{
					transform2.FindChild("Image" + (num3 + 1)).get_gameObject().SetActive(false);
				}
			}
		}
		else
		{
			this.ExcellentAttr.get_gameObject().SetActive(false);
		}
		if (equipSimpleInfoByEquipID.starAttrs.get_Count() > 0)
		{
			this.StarUpAttr.get_gameObject().SetActive(true);
			this.StarUpAttr.FindChild("EquipItem2Text0").FindChild("Item2Text").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc((GameData.AttrType)equipSimpleInfoByEquipID.starAttrs.get_Item(0).attrId, equipSimpleInfoByEquipID.starAttrs.get_Item(0).value, "ff7d4b"));
		}
		else
		{
			this.StarUpAttr.get_gameObject().SetActive(false);
		}
		if (equipSimpleInfoByEquipID.enchantAttrs.get_Count() > 0)
		{
			this.EnchantmentAttr.get_gameObject().SetActive(true);
			int num4 = 0;
			bool flag = false;
			int num5 = 0;
			while (num4 < equipSimpleInfoByEquipID.enchantAttrs.get_Count())
			{
				if (num4 >= 3)
				{
					break;
				}
				if (equipSimpleInfoByEquipID.enchantAttrs.get_Item(num4).attrId > 0)
				{
					flag = true;
					int attrId = equipSimpleInfoByEquipID.enchantAttrs.get_Item(num4).attrId;
					Items items = DataReader<Items>.Get(attrId);
					string text2 = string.Empty;
					if (items != null)
					{
						text2 = GameDataUtils.GetChineseContent(items.name, false);
					}
					FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
					this.EnchantmentAttr.FindChild("EquipItem2Text" + num4).get_gameObject().SetActive(true);
					string text3 = string.Empty;
					if (fuMoDaoJuShuXing != null)
					{
						if (fuMoDaoJuShuXing.valueType == 0)
						{
							text3 = string.Concat(new object[]
							{
								AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
								" +",
								(float)(equipSimpleInfoByEquipID.enchantAttrs.get_Item(num4).value * 100L) / 1000f,
								"%"
							});
						}
						else
						{
							text3 = AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr) + " +" + equipSimpleInfoByEquipID.enchantAttrs.get_Item(num4).value;
						}
						this.EnchantmentAttr.FindChild("EquipItem2Text" + num4).FindChild("Item2Text").GetComponent<Text>().set_text(string.Format(text2 + "： <color=#ff7d4b>{0}</color>", text3));
						num5++;
					}
				}
				num4++;
			}
			if (!flag)
			{
				this.EnchantmentAttr.get_gameObject().SetActive(false);
			}
			else
			{
				for (int num6 = num5; num6 < 3; num6++)
				{
					this.EnchantmentAttr.FindChild("EquipItem2Text" + num6).get_gameObject().SetActive(false);
				}
			}
		}
		else
		{
			this.EnchantmentAttr.get_gameObject().SetActive(false);
		}
	}

	private string GetStarLevelSpriteName(int itemID)
	{
		switch (itemID)
		{
		case 8:
			return "pinji_tongxing1";
		case 9:
			return "pinji_yinxing1";
		case 10:
			return "pinji_jinxing1";
		default:
			return string.Empty;
		}
	}
}
