using Foundation.Core;
using System;
using UnityEngine;

public class OOItem2SpecialItem : ObservableObject
{
	public class Names
	{
		public const string Attr_Content = "Content";

		public const string Attr_Icon = "Icon";
	}

	private string _Content;

	private SpriteRenderer _Icon;

	public string Content
	{
		get
		{
			return this._Content;
		}
		set
		{
			this._Content = value;
			base.NotifyProperty("Content", value);
		}
	}

	public SpriteRenderer Icon
	{
		get
		{
			return this._Icon;
		}
		set
		{
			this._Icon = value;
			base.NotifyProperty("Icon", value);
		}
	}
}
