using Foundation.Core;
using System;
using UnityEngine;

public class OOItem2Draw : ObservableObject
{
	public class Names
	{
		public const string Attr_FrameIcon = "FrameIcon";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_BgShow = "BgShow";

		public const string Event_OnClick = "OnClick";
	}

	public int ID;

	private SpriteRenderer _FrameIcon;

	private SpriteRenderer _ItemIcon;

	private string _ItemName;

	private bool _BgShow;

	public SpriteRenderer FrameIcon
	{
		get
		{
			return this._FrameIcon;
		}
		set
		{
			this._FrameIcon = value;
			base.NotifyProperty("FrameIcon", value);
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

	public bool BgShow
	{
		get
		{
			return this._BgShow;
		}
		set
		{
			this._BgShow = value;
			base.NotifyProperty("BgShow", value);
		}
	}

	public void OnClick()
	{
		ItemTipUIViewModel.ShowItem(this.ID, null);
	}
}
