using Foundation.Core;
using System;

public class OOUpgradeUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_AttBegin = "AttBegin";

		public const string Attr_AttBegin1 = "AttBegin1";

		public const string Attr_AttEnd = "AttEnd";
	}

	private string _AttBegin;

	private string _AttBegin1;

	private string _AttEnd;

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

	public string AttEnd
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
}
