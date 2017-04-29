using Foundation.Core;
using System;
using UnityEngine;

public class OOPrivilegeSmallItem : ObservableObject
{
	public class Names
	{
		public const string Attr_Background = "Background";

		public const string Attr_TimesTipOn = "TimesTipOn";

		public const string Attr_TimesNum = "TimesNum";

		public const string Attr_TimesTipWord = "TimesTipWord";

		public const string Event_OnButtonClick = "OnButtonClick";
	}

	public Action callback;

	private SpriteRenderer _Background;

	private bool _TimesTipOn;

	private string _TimesNum;

	public SpriteRenderer Background
	{
		get
		{
			return this._Background;
		}
		set
		{
			this._Background = value;
			base.NotifyProperty("Background", value);
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
			base.NotifyProperty("TimesNum", this._TimesNum);
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
