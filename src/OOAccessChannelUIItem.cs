using Foundation.Core;
using System;
using UnityEngine;

public class OOAccessChannelUIItem : ObservableObject
{
	public class Names
	{
		public const string Attr_Icon = "Icon";

		public const string Attr_Title = "Title";

		public const string Attr_TitleDesc = "TitleDesc";

		public const string Event_OnChannelBtnUp = "OnChannelBtnUp";
	}

	public int InstanceId;

	private SpriteRenderer _Icon;

	private string _Title;

	private string _TitleDesc;

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

	public string Title
	{
		get
		{
			return this._Title;
		}
		set
		{
			this._Title = value;
			base.NotifyProperty("Title", value);
		}
	}

	public string TitleDesc
	{
		get
		{
			return this._TitleDesc;
		}
		set
		{
			this._TitleDesc = value;
			base.NotifyProperty("TitleDesc", value);
		}
	}

	public void OnChannelBtnUp()
	{
		AccessChannelUIViewModel.Instance.OnCloseBtnUp();
		InstanceManagerUI.OpenInstanceUI(this.InstanceId, false, UIType.FullScreen);
	}
}
