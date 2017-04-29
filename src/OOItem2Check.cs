using Foundation.Core;
using System;
using UnityEngine;

public class OOItem2Check : ObservableObject
{
	public class Names
	{
		public const string Attr_Frame = "Frame";

		public const string Attr_Icon = "Icon";

		public const string Attr_CheckVisibility = "CheckVisibility";

		public const string Event_OnCheckUp = "OnCheckUp";
	}

	public int id;

	private int _Type;

	private SpriteRenderer _Frame;

	private SpriteRenderer _Icon;

	private bool _CheckVisibility;

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

	public bool CheckVisibility
	{
		get
		{
			return this._CheckVisibility;
		}
		set
		{
			this._CheckVisibility = value;
			base.NotifyProperty("CheckVisibility", value);
			if (value)
			{
				ChatUIViewModel.Instance.Add2ItemShows(this.id);
			}
			else
			{
				ChatUIViewModel.Instance.Remove4ItemShows(this.id);
			}
		}
	}

	public void OnCheckUp()
	{
		if (!this.CheckVisibility && RevealPackUIViewModel.Instance.IsShowItemHasFull())
		{
			FloatTextAddManager.Instance.AddFloatText(GameDataUtils.GetChineseContent(502062, false), Color.get_green());
			return;
		}
		this.CheckVisibility = !this.CheckVisibility;
	}
}
