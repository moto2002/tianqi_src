using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OOPrivilegePageDetail : ObservableObject
{
	public class Names
	{
		public const string EffectContent = "EffectContent";

		public const string Node2HideVisibility = "Node2HideVisibility";

		public const string VIPName = "VIPName";

		public const string ItemsName = "ItemsName";

		public const string ObatinItems = "ObatinItems";

		public const string ObatinSpecialItems = "ObatinSpecialItems";

		public const string ObatinDiamondItems = "ObatinDiamondItems";

		public const string ShowFlag = "ShowFlag";

		public const string ShowBtnOpen = "ShowBtnOpen";

		public const string EnableOfBtnOpen = "EnableOfBtnOpen";

		public const string BtnOpenText = "BtnOpenText";

		public const string CanNotGetText = "CanNotGetText";

		public const string ShowCanNotGetTxt = "ShowCanNotGetTxt";

		public const string Attr_VIPLevel10 = "VIPLevel10";

		public const string Attr_VIPLevel1 = "VIPLevel1";

		public const string Attr_ImageDetialTitleBg = "ImageDetialTitleBg";

		public const string ImageDetialTitleBgVisibility = "ImageDetialTitleBgVisibility";

		public const string Event_OnBtnOpenClick = "OnBtnOpenClick";
	}

	public Action callback;

	private string _EffectContent;

	private bool _Node2HideVisibility;

	private string _VIPName;

	private SpriteRenderer _VIPLevel10;

	private SpriteRenderer _VIPLevel1;

	private SpriteRenderer _ImageDetialTitleBg;

	private string _ItemsName;

	private bool _ShowFlag;

	private bool _ShowBtnOpen = true;

	private bool _ShowCanNotGetTxt;

	private string _CanNotGetText;

	private bool _EnableOfBtnOpen;

	private string _BtnOpenText;

	private bool _ImageDetialTitleBgVisibility;

	public ObservableCollection<OOItem2Draw> ObatinItems = new ObservableCollection<OOItem2Draw>();

	public ObservableCollection<OOItem2SpecialItem> ObatinSpecialItems = new ObservableCollection<OOItem2SpecialItem>();

	public ObservableCollection<OOItem2SpecialItem> ObatinDiamondItems = new ObservableCollection<OOItem2SpecialItem>();

	public string EffectContent
	{
		get
		{
			return this._EffectContent;
		}
		set
		{
			this._EffectContent = value;
			base.NotifyProperty("EffectContent", value);
		}
	}

	public bool Node2HideVisibility
	{
		get
		{
			return this._Node2HideVisibility;
		}
		set
		{
			this._Node2HideVisibility = value;
			base.NotifyProperty("Node2HideVisibility", value);
		}
	}

	public string VIPName
	{
		get
		{
			return this._VIPName;
		}
		set
		{
			this._VIPName = value;
			base.NotifyProperty("VIPName", value);
		}
	}

	public SpriteRenderer VIPLevel10
	{
		get
		{
			return this._VIPLevel10;
		}
		set
		{
			this._VIPLevel10 = value;
			base.NotifyProperty("VIPLevel10", value);
		}
	}

	public SpriteRenderer VIPLevel1
	{
		get
		{
			return this._VIPLevel1;
		}
		set
		{
			this._VIPLevel1 = value;
			base.NotifyProperty("VIPLevel1", value);
		}
	}

	public SpriteRenderer ImageDetialTitleBg
	{
		get
		{
			return this._ImageDetialTitleBg;
		}
		set
		{
			this._ImageDetialTitleBg = value;
			base.NotifyProperty("ImageDetialTitleBg", value);
		}
	}

	public string ItemsName
	{
		get
		{
			return this._ItemsName;
		}
		set
		{
			this._ItemsName = value;
			base.NotifyProperty("ItemsName", value);
		}
	}

	public bool ShowFlag
	{
		get
		{
			return this._ShowFlag;
		}
		set
		{
			this._ShowFlag = value;
			base.NotifyProperty("ShowFlag", value);
		}
	}

	public bool ShowBtnOpen
	{
		get
		{
			return this._ShowBtnOpen;
		}
		set
		{
			this._ShowBtnOpen = value;
			base.NotifyProperty("ShowBtnOpen", this._ShowBtnOpen);
		}
	}

	public bool ShowCanNotGetTxt
	{
		get
		{
			return this._ShowCanNotGetTxt;
		}
		set
		{
			this._ShowCanNotGetTxt = value;
			base.NotifyProperty("ShowCanNotGetTxt", this._ShowCanNotGetTxt);
		}
	}

	public string CanNotGetText
	{
		get
		{
			return this._CanNotGetText;
		}
		set
		{
			this._CanNotGetText = value;
			base.NotifyProperty("CanNotGetText", value);
		}
	}

	public bool EnableOfBtnOpen
	{
		get
		{
			return this._EnableOfBtnOpen;
		}
		set
		{
			this._EnableOfBtnOpen = value;
			base.NotifyProperty("EnableOfBtnOpen", value);
			if (value)
			{
				this.BtnOpenText = "领 取";
			}
			else
			{
				this.BtnOpenText = "已领取";
			}
		}
	}

	public string BtnOpenText
	{
		get
		{
			return this._BtnOpenText;
		}
		set
		{
			this._BtnOpenText = value;
			base.NotifyProperty("BtnOpenText", value);
		}
	}

	public bool ImageDetialTitleBgVisibility
	{
		get
		{
			return this._ImageDetialTitleBgVisibility;
		}
		set
		{
			this._ImageDetialTitleBgVisibility = value;
			base.NotifyProperty("ImageDetialTitleBgVisibility", value);
		}
	}

	public void OnBtnOpenClick()
	{
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
	}

	public void OnBtnBackClick()
	{
		PrivilegeUIViewModel.Instance.SwitchMode(PrivilegeUIViewModel.Mode.Privilege, PrivilegeUIView.Instance.PrivilegeDetailPageIndex);
	}

	public void SetTreasure(int vipLevel, VipXiaoGuo dataVIPEffect)
	{
		this.ObatinItems.Clear();
		this.ObatinSpecialItems.Clear();
		List<VipBoxItemInfo> vIPBox = VIPManager.Instance.GetVIPBox(dataVIPEffect.effect);
		if (vIPBox != null)
		{
			for (int i = 0; i < vIPBox.get_Count(); i++)
			{
				VipBoxItemInfo vipBoxItemInfo = vIPBox.get_Item(i);
				OOItem2Draw oOItem2Draw = new OOItem2Draw();
				oOItem2Draw.BgShow = true;
				oOItem2Draw.ID = vipBoxItemInfo.itemId;
				oOItem2Draw.FrameIcon = GameDataUtils.GetItemFrame(vipBoxItemInfo.itemId);
				oOItem2Draw.ItemIcon = GameDataUtils.GetItemIcon(vipBoxItemInfo.itemId);
				oOItem2Draw.ItemName = Utils.GetItemNum(vipBoxItemInfo.itemId, (long)vipBoxItemInfo.itemCount);
				this.ObatinItems.Add(oOItem2Draw);
			}
		}
	}

	public void UpdateDiamondCount(int num, List<string> info)
	{
		this.ObatinDiamondItems.Clear();
		for (int i = 0; i < num; i++)
		{
			OOItem2SpecialItem oOItem2SpecialItem = new OOItem2SpecialItem();
			oOItem2SpecialItem.Icon = ResourceManager.GetIconSprite("j_diamond001");
			oOItem2SpecialItem.Content = info.get_Item(i);
			this.ObatinDiamondItems.Add(oOItem2SpecialItem);
		}
	}

	public void SetIconPosition(int viplv)
	{
		if (viplv < 10)
		{
		}
	}
}
