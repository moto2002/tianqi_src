using System;

public class SpineSlot : SpineAttributeBase
{
	public bool containsBoundingBoxes;

	public SpineSlot(string startsWith = "", string dataField = "", bool containsBoundingBoxes = false)
	{
		this.startsWith = startsWith;
		this.dataField = dataField;
		this.containsBoundingBoxes = containsBoundingBoxes;
	}
}
