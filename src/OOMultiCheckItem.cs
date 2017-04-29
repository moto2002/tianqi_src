using Foundation.Core;
using System;

public class OOMultiCheckItem : ObservableObject
{
	public class Names
	{
		public const string Attr_Name = "Name";

		public const string Attr_IsOn = "IsOn";
	}

	public int id;

	private string _Name;

	private bool _IsOn;

	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
			base.NotifyProperty("Name", value);
		}
	}

	public bool IsOn
	{
		get
		{
			return this._IsOn;
		}
		set
		{
			this._IsOn = value;
			base.NotifyProperty("IsOn", value);
		}
	}
}
