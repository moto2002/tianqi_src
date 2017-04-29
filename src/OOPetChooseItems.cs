using Foundation.Core;
using System;

public class OOPetChooseItems : ObservableObject
{
	public class Names
	{
		public const string Attr_Items = "Items";

		public const string Attr_LineRegion = "LineRegion";
	}

	private bool _LineRegion;

	public ObservableCollection<OOPetChooseUnit> Items = new ObservableCollection<OOPetChooseUnit>();

	public bool LineRegion
	{
		get
		{
			return this._LineRegion;
		}
		set
		{
			this._LineRegion = value;
			base.NotifyProperty("LineRegion", value);
		}
	}
}
