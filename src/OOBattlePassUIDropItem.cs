using Foundation.Core;
using System;
using UnityEngine;

public class OOBattlePassUIDropItem : ObservableObject
{
	public class Names
	{
		public const string Attr_Icon = "Icon";

		public const string Attr_IconBg = "IconBg";

		public const string Attr_GoodNum = "GoodNum";

		public const string Event_OnButtonClick = "OnButtonClick";
	}

	public int ItemId;

	private string goodNum;

	private SpriteRenderer _Icon;

	private SpriteRenderer _IconBg;

	public string GoodNum
	{
		get
		{
			return this.goodNum;
		}
		set
		{
			this.goodNum = value;
			base.NotifyProperty("GoodNum", value);
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

	public SpriteRenderer IconBg
	{
		get
		{
			return this._IconBg;
		}
		set
		{
			this._IconBg = value;
			base.NotifyProperty("IconBg", value);
		}
	}

	public void OnButtonClick()
	{
		ItemTipUIViewModel.ShowItem(this.ItemId, null);
	}
}
