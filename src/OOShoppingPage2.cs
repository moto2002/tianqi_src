using Foundation.Core;
using System;

public class OOShoppingPage2 : ObservableObject
{
	public class Names
	{
		public const string Attr_Items = "Items";
	}

	public ObservableCollection<OOShoppingUnit2> Items = new ObservableCollection<OOShoppingUnit2>();
}
