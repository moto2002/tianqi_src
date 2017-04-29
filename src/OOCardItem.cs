using Foundation.Core;
using System;
using UnityEngine;

public class OOCardItem : ObservableObject
{
	public class Names
	{
		public const string Attr_CardName = "CardName";

		public const string Attr_Price = "Price";

		public const string ImageSelectVisibility = "ImageSelectVisibility";

		public const string ImageBgSelectVisibility = "ImageBgSelectVisibility";

		public const string ImageName1Visibility = "ImageName1Visibility";

		public const string ImageName2Visibility = "ImageName2Visibility";

		public const string ImageName3Visibility = "ImageName3Visibility";

		public const string Attr_GoodsIcon = "GoodsIcon";

		public const string Event_OnButtonClick = "OnButtonClick";
	}

	public Action callback;

	private int _ID;

	private string _CardName;

	private string _Price;

	private bool _ImageSelectVisibility;

	private bool _ImageBgSelectVisibility;

	private bool _ImageName1Visibility;

	private bool _ImageName2Visibility;

	private bool _ImageName3Visibility;

	private SpriteRenderer _GoodsIcon;

	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			this._ID = value;
			switch (this._ID)
			{
			case 1:
				this.ImageName1Visibility = true;
				this.ImageName2Visibility = false;
				this.ImageName3Visibility = false;
				break;
			case 2:
				this.ImageName1Visibility = false;
				this.ImageName2Visibility = true;
				this.ImageName3Visibility = false;
				break;
			case 3:
				this.ImageName1Visibility = false;
				this.ImageName2Visibility = false;
				this.ImageName3Visibility = true;
				break;
			}
		}
	}

	public string CardName
	{
		get
		{
			return this._CardName;
		}
		set
		{
			this._CardName = value;
			base.NotifyProperty("CardName", this._CardName);
		}
	}

	public string Price
	{
		get
		{
			return this._Price;
		}
		set
		{
			this._Price = value;
			base.NotifyProperty("Price", this._Price);
		}
	}

	public bool ImageSelectVisibility
	{
		get
		{
			return this._ImageSelectVisibility;
		}
		set
		{
			this._ImageSelectVisibility = value;
			base.NotifyProperty("ImageSelectVisibility", value);
		}
	}

	public bool ImageBgSelectVisibility
	{
		get
		{
			return this._ImageBgSelectVisibility;
		}
		set
		{
			this._ImageBgSelectVisibility = value;
			base.NotifyProperty("ImageBgSelectVisibility", value);
		}
	}

	public bool ImageName1Visibility
	{
		get
		{
			return this._ImageName1Visibility;
		}
		set
		{
			this._ImageName1Visibility = value;
			base.NotifyProperty("ImageName1Visibility", value);
		}
	}

	public bool ImageName2Visibility
	{
		get
		{
			return this._ImageName2Visibility;
		}
		set
		{
			this._ImageName2Visibility = value;
			base.NotifyProperty("ImageName2Visibility", value);
		}
	}

	public bool ImageName3Visibility
	{
		get
		{
			return this._ImageName3Visibility;
		}
		set
		{
			this._ImageName3Visibility = value;
			base.NotifyProperty("ImageName3Visibility", value);
		}
	}

	public SpriteRenderer GoodsIcon
	{
		get
		{
			return this._GoodsIcon;
		}
		set
		{
			this._GoodsIcon = value;
			base.NotifyProperty("GoodsIcon", value);
		}
	}

	public void OnButtonClick()
	{
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
	}
}
