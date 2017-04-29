using Package;
using System;

public class LinkType
{
	public const int Role = 1;

	public const int Item = 2;

	public const int UI = 3;

	public const int Interface = 4;

	public static DetailType.DT GetDetailType(int linkType)
	{
		if (linkType == 1)
		{
			return DetailType.DT.Role;
		}
		if (linkType == 2)
		{
			return DetailType.DT.Equipment;
		}
		if (linkType == 3)
		{
			return DetailType.DT.UI;
		}
		if (linkType == 4)
		{
			return DetailType.DT.Interface;
		}
		return DetailType.DT.Default;
	}
}
