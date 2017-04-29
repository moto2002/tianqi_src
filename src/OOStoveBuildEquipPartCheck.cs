using Foundation.Core;
using System;
using UnityEngine;

public class OOStoveBuildEquipPartCheck : ObservableObject
{
	public class Names
	{
		public const string Attr_Name = "Name";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemIconCm = "ItemIconCm";

		public const string Attr_IsOn = "IsOn";
	}

	public int id;

	private SpriteRenderer _Name;

	private SpriteRenderer _ItemIcon;

	private SpriteRenderer _ItemIconCm;

	private bool _IsOn;

	public SpriteRenderer Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
			base.NotifyProperty("Name", value);
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

	public SpriteRenderer ItemIconCm
	{
		get
		{
			return this._ItemIconCm;
		}
		set
		{
			this._ItemIconCm = value;
			base.NotifyProperty("ItemIconCm", value);
		}
	}

	public bool IsOn
	{
		get
		{
			return this._IsOn;
		}
		set
		{
			this._IsOn = value;
			base.NotifyProperty("IsOn", value);
		}
	}
}
