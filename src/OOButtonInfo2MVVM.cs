using Foundation.Core;
using System;
using UnityEngine;

public class OOButtonInfo2MVVM : ObservableObject
{
	public class Names
	{
		public const string Attr_ButtonBg = "ButtonBg";

		public const string Attr_ButtonName = "ButtonName";

		public const string Attr_IsShowRedPoint = "IsShowRedPoint";

		public const string Event_OnClick = "OnClick";
	}

	public Action OnCallback;

	private SpriteRenderer _ButtonBg;

	private string _ButtonName;

	private bool _IsShowRedPoint;

	public SpriteRenderer ButtonBg
	{
		get
		{
			return this._ButtonBg;
		}
		set
		{
			this._ButtonBg = value;
			base.NotifyProperty("ButtonBg", value);
		}
	}

	public string ButtonName
	{
		get
		{
			return this._ButtonName;
		}
		set
		{
			this._ButtonName = value;
			base.NotifyProperty("ButtonName", value);
		}
	}

	public bool IsShowRedPoint
	{
		get
		{
			return this._IsShowRedPoint;
		}
		set
		{
			this._IsShowRedPoint = value;
			base.NotifyProperty("IsShowRedPoint", value);
		}
	}

	public void OnClick()
	{
		if (this.OnCallback != null)
		{
			this.OnCallback.Invoke();
		}
	}
}
