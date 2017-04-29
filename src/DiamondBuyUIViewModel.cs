using Foundation.Core;
using System;
using UnityEngine;

public class DiamondBuyUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Info1 = "Info1";

		public const string Attr_Info2_1 = "Info2_1";

		public const string Attr_Icon = "Icon";

		public const string Event_OnBtnCancelClick = "OnBtnCancelClick";

		public const string Event_OnBtnOKClick = "OnBtnOKClick";
	}

	public static DiamondBuyUIViewModel Instance;

	public Action CallBack;

	private string _Info1;

	private string _Info2_1;

	private SpriteRenderer _Icon;

	public string Info1
	{
		get
		{
			return this._Info1;
		}
		set
		{
			this._Info1 = value;
			base.NotifyProperty("Info1", value);
		}
	}

	public string Info2_1
	{
		get
		{
			return this._Info2_1;
		}
		set
		{
			this._Info2_1 = value;
			base.NotifyProperty("Info2_1", value);
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

	protected override void Awake()
	{
		base.Awake();
		DiamondBuyUIViewModel.Instance = this;
	}

	public void OnBtnCancelClick()
	{
		DiamondBuyUIView.Instance.Show(false);
	}

	public void OnBtnOKClick()
	{
		DiamondBuyUIView.Instance.Show(false);
		if (this.CallBack != null)
		{
			this.CallBack.Invoke();
		}
	}
}
