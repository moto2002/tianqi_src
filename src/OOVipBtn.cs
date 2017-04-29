using Foundation.Core;
using System;

public class OOVipBtn : ObservableObject
{
	public class Names
	{
		public const string Attr_BtnName = "BtnName";

		public const string ImageGreyVisibility = "ImageGreyVisibility";

		public const string Event_OnButtonClick = "OnButtonClick";
	}

	public Action callback;

	private string _BtnName;

	private bool _ImageGreyVisibility;

	public string BtnName
	{
		get
		{
			return this._BtnName;
		}
		set
		{
			this._BtnName = value;
			base.NotifyProperty("BtnName", this._BtnName);
		}
	}

	public bool ImageGreyVisibility
	{
		get
		{
			return this._ImageGreyVisibility;
		}
		set
		{
			this._ImageGreyVisibility = value;
			base.NotifyProperty("ImageGreyVisibility", value);
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
