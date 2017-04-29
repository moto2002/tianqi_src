using Foundation.Core;
using System;

public class OOShoppingPage : ObservableObject
{
	public class Names
	{
		public const string Attr_Items = "Items";
	}

	public ObservableCollection<OOShoppingUnit> Items = new ObservableCollection<OOShoppingUnit>();
}
