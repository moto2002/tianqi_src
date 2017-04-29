using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTipUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ItemName = "ItemName";

		public const string Attr_ItemLv = "ItemLv";

		public const string Attr_ItemNum = "ItemNum";

		public const string Attr_ItemProfession = "ItemProfession";

		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_Desc = "Desc";

		public const string Attr_TextItems = "TextItems";

		public const string Attr_AttrDesc = "AttrDesc";

		public const string Attr_BtnUseVisibility = "BtnUseVisibility";

		public const string Attr_ItemStepText = "ItemStepText";

		public const string Attr_ItemStepVisibility = "ItemStepVisibility";

		public const string ExcellentAttrVisibility = "ExcellentAttrVisibility";

		public const string ExcellentImage1 = "ExcellentImage1";

		public const string ExcellentImage2 = "ExcellentImage2";

		public const string ExcellentImage3 = "ExcellentImage3";

		public const string Event_OnBtnUseUp = "OnBtnUseUp";
	}

	private static ItemTipUIViewModel m_instance;

	private string _ItemName;

	private string _ItemLv;

	private string _ItemNum;

	private string _ItemProfession;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _Desc;

	private string _AttrDesc;

	private bool _BtnUseVisibility;

	private string _ItemStepText;

	private bool _ItemStepVisibility;

	private bool _ExcellentImage1;

	private bool _ExcellentImage2;

	private bool _ExcellentImage3;

	private bool _ExcellentAttrVisibility;

	private int _ExcellentCount;

	public ObservableCollection<OOItem2Text> TextItems = new ObservableCollection<OOItem2Text>();

	public static ItemTipUIViewModel Instance
	{
		get
		{
			return ItemTipUIViewModel.m_instance;
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
			ItemTipUIView.Instance.SetLevelPosition();
		}
	}

	public string ItemLv
	{
		get
		{
			return this._ItemLv;
		}
		set
		{
			this._ItemLv = value;
			base.NotifyProperty("ItemLv", value);
		}
	}

	public string ItemNum
	{
		get
		{
			return this._ItemNum;
		}
		set
		{
			this._ItemNum = value;
			base.NotifyProperty("ItemNum", value);
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

	public string Desc
	{
		get
		{
			return this._Desc;
		}
		set
		{
			this._Desc = value;
			base.NotifyProperty("Desc", value);
		}
	}

	public string AttrDesc
	{
		get
		{
			return this._AttrDesc;
		}
		set
		{
			this._AttrDesc = value;
			base.NotifyProperty("AttrDesc", value);
		}
	}

	public bool BtnUseVisibility
	{
		get
		{
			return this._BtnUseVisibility;
		}
		set
		{
			this._BtnUseVisibility = value;
			base.NotifyProperty("BtnUseVisibility", value);
		}
	}

	public string ItemStepText
	{
		get
		{
			return this._ItemStepText;
		}
		set
		{
			if (this._ItemStepText == value)
			{
				return;
			}
			this._ItemStepText = value;
			base.NotifyProperty("ItemStepText", value);
		}
	}

	public bool ItemStepVisibility
	{
		get
		{
			return this._ItemStepVisibility;
		}
		set
		{
			if (this._ItemStepVisibility == value)
			{
				return;
			}
			this._ItemStepVisibility = value;
			base.NotifyProperty("ItemStepVisibility", value);
		}
	}

	public bool ExcellentImage1
	{
		get
		{
			return this._ExcellentImage1;
		}
		set
		{
			if (this._ExcellentImage1 != value)
			{
				this._ExcellentImage1 = value;
				base.NotifyProperty("ExcellentImage1", value);
			}
		}
	}

	public bool ExcellentImage2
	{
		get
		{
			return this._ExcellentImage2;
		}
		set
		{
			if (this._ExcellentImage2 != value)
			{
				this._ExcellentImage2 = value;
				base.NotifyProperty("ExcellentImage2", value);
			}
		}
	}

	public bool ExcellentImage3
	{
		get
		{
			return this._ExcellentImage3;
		}
		set
		{
			if (this._ExcellentImage3 != value)
			{
				this._ExcellentImage3 = value;
				base.NotifyProperty("ExcellentImage3", value);
			}
		}
	}

	public bool ExcellentAttrVisibility
	{
		get
		{
			return this._ExcellentAttrVisibility;
		}
		set
		{
			this._ExcellentAttrVisibility = value;
			base.NotifyProperty("ExcellentAttrVisibility", value);
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
				this.ExcellentAttrVisibility = true;
				this.ExcellentImage1 = (this._ExcellentCount >= 1);
				this.ExcellentImage2 = (this._ExcellentCount >= 2);
				this.ExcellentImage3 = (this._ExcellentCount >= 3);
			}
			else
			{
				this.ExcellentAttrVisibility = false;
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ItemTipUIViewModel.m_instance = this;
	}

	public void OnBtnUseUp()
	{
		Debuger.Error("OnBtnUseUp", new object[0]);
	}

	public static void ShowEquipItem(int itemId, WearEquipInfo equipData, Transform root = null)
	{
		int depthValue = 3000;
		if (root == null)
		{
			root = UINodesManager.MiddleUIRoot;
		}
		else
		{
			Canvas componentInParent = root.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				depthValue = componentInParent.get_sortingOrder();
			}
		}
		if (BackpackManager.Instance.GetItem(itemId) == null)
		{
			return;
		}
		EquipCompareTipUI equipCompareTipUI = UIManagerControl.Instance.OpenUI("EquipCompareTipUI", root, false, UIType.NonPush) as EquipCompareTipUI;
		equipCompareTipUI.RefreshUI(equipData, depthValue);
	}

	public static void ShowEquipItem(int itemID, long equipUId = 0L, Transform root = null)
	{
		int depthValue = 14000;
		if (root == null)
		{
			root = UINodesManager.TopUIRoot;
		}
		else
		{
			Canvas componentInParent = root.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				depthValue = componentInParent.get_sortingOrder();
			}
		}
		if (BackpackManager.Instance.GetItem(itemID) == null)
		{
			return;
		}
		EquipCompareTipUI equipCompareTipUI = UIManagerControl.Instance.OpenUI("EquipCompareTipUI", root, false, UIType.NonPush) as EquipCompareTipUI;
		equipCompareTipUI.RefreshUI(itemID, equipUId, depthValue);
	}

	public static void ShowItem(int itemId, Transform root = null)
	{
		if (root == null)
		{
			root = UINodesManager.TopUIRoot;
		}
		Items item = BackpackManager.Instance.GetItem(itemId);
		if (item == null)
		{
			return;
		}
		if (item.tab == 2)
		{
			int depthValue = 14000;
			if (root != null)
			{
				Canvas componentInParent = root.GetComponentInParent<Canvas>();
				if (componentInParent != null)
				{
					depthValue = componentInParent.get_sortingOrder();
				}
			}
			EquipCompareTipUI equipCompareTipUI = UIManagerControl.Instance.OpenUI("EquipCompareTipUI", root, false, UIType.NonPush) as EquipCompareTipUI;
			equipCompareTipUI.RefreshUI(itemId, depthValue);
		}
		else
		{
			UIManagerControl.Instance.OpenUI("ItemTipUI", root, false, UIType.NonPush);
			ItemTipUIViewModel.Instance.Refresh(itemId);
		}
	}

	public void Refresh(int itemId)
	{
		(base.get_transform() as RectTransform).set_anchoredPosition(new Vector2(5000f, 5000f));
		this.BtnUseVisibility = false;
		this.ItemLv = string.Empty;
		Items item = BackpackManager.Instance.GetItem(itemId);
		if (item != null)
		{
			this.ItemFrame = GameDataUtils.GetItemFrame(item.id);
			this.ItemIcon = GameDataUtils.GetIcon(item.icon);
			this.ItemName = GameDataUtils.GetItemName(item, true);
			if (item.minLv > EntityWorld.Instance.EntSelf.Lv)
			{
				this.ItemLv = string.Format(GameDataUtils.GetChineseContent(509011, false), item.minLv);
			}
			string color = TextColorMgr.GetColor(BackpackManager.Instance.OnGetGoodCount(itemId).ToString(), "ff7d4b", string.Empty);
			string chineseContent = GameDataUtils.GetChineseContent(509010, false);
			this.ItemNum = TextColorMgr.GetColor(string.Format(chineseContent, " " + color), "eccd9b", string.Empty);
			this.ItemProfession = GameDataUtils.GetItemProfession(item);
			if (item.step <= 0)
			{
				this.ItemStepVisibility = false;
			}
			else
			{
				this.ItemStepVisibility = true;
				this.ItemStepText = string.Format(GameDataUtils.GetChineseContent(505023, false), item.step);
			}
			this.ExcellentCount = item.gogok;
			this.TextItems.Clear();
			List<string> itemAttr = BackpackManager.Instance.GetItemAttr(item);
			if (itemAttr != null)
			{
				GameObject gameObject = GameObject.Find("Attrs");
				if (gameObject != null)
				{
					gameObject.GetComponent<Image>().set_enabled(true);
				}
				for (int i = 0; i < itemAttr.get_Count(); i++)
				{
					this.TextItems.Add(new OOItem2Text
					{
						Content = TextColorMgr.GetColor(itemAttr.get_Item(i), "ECCD9B", string.Empty)
					});
				}
			}
			else
			{
				GameObject gameObject2 = GameObject.Find("Attrs");
				if (gameObject2 != null)
				{
					gameObject2.GetComponent<Image>().set_enabled(false);
				}
			}
			if (item.describeId1 > 0)
			{
				this.Desc = TextColorMgr.GetColor(GameDataUtils.GetChineseContent(item.describeId1, false), "FF7D4B", string.Empty);
			}
			else
			{
				this.Desc = string.Empty;
			}
			if (item.describeId2 > 0)
			{
				this.AttrDesc = TextColorMgr.GetColor(GameDataUtils.GetChineseContent(item.describeId2, false), "5AB9FF", string.Empty);
			}
			else
			{
				this.AttrDesc = string.Empty;
			}
		}
	}

	private void Close()
	{
		ItemTipUIView.Instance.Show(false);
	}
}
