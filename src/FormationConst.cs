using GameData;
using System;

public class FormationConst
{
	public const int FORMATION_COUNT = 3;

	public const int FORMATION_PET_COUNT = 3;

	public const int attrID1 = 201;

	public const int attrID2 = 601;

	public const int attrID3 = 301;

	public const int attrID4 = 1304;

	public const int attrID5 = 1305;

	public static string GetContent(int attrId, float value)
	{
		return AttrUtility.GetAttrName((AttrType)attrId) + TextColorMgr.GetColor(value.ToString(), "A55539", string.Empty);
	}
}
