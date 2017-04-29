using Foundation.Core;
using System;

public class OOPetBasicAttr : ObservableObject
{
	public class Names
	{
		public const string Attr_Name = "Name";

		public const string Attr_Attr01 = "Attr01";

		public const string Attr_Attr02 = "Attr02";
	}

	private string _Name;

	private string _Attr01;

	private string _Attr02;

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

	public string Attr01
	{
		get
		{
			return this._Attr01;
		}
		set
		{
			this._Attr01 = value;
			base.NotifyProperty("Attr01", value);
		}
	}

	public string Attr02
	{
		get
		{
			return this._Attr02;
		}
		set
		{
			this._Attr02 = value;
			base.NotifyProperty("Attr02", value);
		}
	}
}
