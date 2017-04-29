using Foundation.Core;
using System;
using UnityEngine;

public class OOLevelUpUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_AttBegin = "AttBegin";

		public const string Attr_AttBegin1 = "AttBegin1";

		public const string Attr_AttEnd = "AttEnd";

		public const string Attr_BGVisibility = "BGVisibility";
	}

	private string _AttBegin;

	private string _AttBegin1;

	private Vector3 _AttEnd;

	private bool _BGVisibility;

	public string AttBegin
	{
		get
		{
			return this._AttBegin;
		}
		set
		{
			this._AttBegin = value;
			base.NotifyProperty("AttBegin", value);
		}
	}

	public string AttBegin1
	{
		get
		{
			return this._AttBegin1;
		}
		set
		{
			this._AttBegin1 = value;
			base.NotifyProperty("AttBegin1", value);
		}
	}

	public Vector3 AttEnd
	{
		get
		{
			return this._AttEnd;
		}
		set
		{
			this._AttEnd = value;
			base.NotifyProperty("AttEnd", value);
		}
	}

	public bool BGVisibility
	{
		get
		{
			return this._BGVisibility;
		}
		set
		{
			this._BGVisibility = value;
			base.NotifyProperty("BGVisibility", value);
		}
	}
}
