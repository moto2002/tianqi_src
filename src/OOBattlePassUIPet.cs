using Foundation.Core;
using System;
using UnityEngine;

public class OOBattlePassUIPet : ObservableObject
{
	public class Names
	{
		public const string Attr_Icon = "Icon";

		public const string Attr_ExpAmount = "ExpAmount";
	}

	private Sprite _Icon;

	private float _ExpAmount;

	public Sprite Icon
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

	public float ExpAmount
	{
		get
		{
			return this._ExpAmount;
		}
		set
		{
			this._ExpAmount = value;
			base.NotifyProperty("ExpAmount", value);
		}
	}
}
