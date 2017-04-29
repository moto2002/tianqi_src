using Foundation.Core;
using System;
using UnityEngine;

public class OOItem2Show : ObservableObject
{
	public class Names
	{
		public const string Attr_Frame = "Frame";

		public const string Attr_Icon = "Icon";

		public const string Event_OnBtnUp = "OnBtnUp";
	}

	public int id;

	private int _Type;

	private SpriteRenderer _Frame;

	private SpriteRenderer _Icon;

	public int Type
	{
		get
		{
			return this._Type;
		}
		set
		{
			this._Type = value;
		}
	}

	public SpriteRenderer Frame
	{
		get
		{
			return this._Frame;
		}
		set
		{
			this._Frame = value;
			base.NotifyProperty("Frame", value);
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

	public void OnBtnUp()
	{
		Debuger.Error("OnBtnUp", new object[0]);
		ChatUIViewModel.Instance.Remove4ItemShows(this.id);
	}
}
