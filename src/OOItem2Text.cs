using Foundation.Core;
using System;

public class OOItem2Text : ObservableObject
{
	public class Names
	{
		public const string Attr_Content = "Content";
	}

	private string _Content;

	public string Content
	{
		get
		{
			return this._Content;
		}
		set
		{
			this._Content = value;
			base.NotifyProperty("Content", value);
		}
	}
}
