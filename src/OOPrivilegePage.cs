using Foundation.Core;
using System;
using UnityEngine;

public class OOPrivilegePage : ObservableObject
{
	public class Names
	{
		public const string Attr_SmallItems = "SmallItems";

		public const string Attr_PrivilegeItemBg = "PrivilegeItemBg";

		public const string Attr_TimesTipOn = "TimesTipOn";

		public const string Attr_TimesNum = "TimesNum";

		public const string Attr_Node2HideVisibility = "Node2HideVisibility";

		public const string Attr_VIPName = "VIPName";

		public const string Attr_RefreshFXOfBox = "RefreshFXOfBox";

		public const string Event_OnPrivilegeItemClick = "OnPrivilegeItemClick";
	}

	public Action callback;

	private SpriteRenderer _PrivilegeItemBg;

	private bool _TimesTipOn;

	private string _TimesNum;

	private bool _Node2HideVisibility;

	private string _VIPName;

	private bool _RefreshFXOfBox;

	public ObservableCollection<OOPrivilegeSmallItem> SmallItems = new ObservableCollection<OOPrivilegeSmallItem>();

	public SpriteRenderer PrivilegeItemBg
	{
		get
		{
			return this._PrivilegeItemBg;
		}
		set
		{
			this._PrivilegeItemBg = value;
			base.NotifyProperty("PrivilegeItemBg", value);
		}
	}

	public bool TimesTipOn
	{
		get
		{
			return this._TimesTipOn;
		}
		set
		{
			this._TimesTipOn = value;
			base.NotifyProperty("TimesTipOn", value);
		}
	}

	public string TimesNum
	{
		get
		{
			return this._TimesNum;
		}
		set
		{
			this._TimesNum = value;
			base.NotifyProperty("TimesNum", value);
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
			this.RefreshFXOfBox = value;
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

	public bool RefreshFXOfBox
	{
		get
		{
			return this._RefreshFXOfBox;
		}
		set
		{
			this._RefreshFXOfBox = value;
			base.NotifyProperty("RefreshFXOfBox", value);
		}
	}

	public void OnPrivilegeItemClick()
	{
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
	}
}
