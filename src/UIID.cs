using GameData;
using System;
using System.Collections.Generic;

public class UIID
{
	private static Dictionary<string, int> mNameToID;

	public static int GetID(string name)
	{
		if (UIID.mNameToID == null)
		{
			UIID.mNameToID = new Dictionary<string, int>();
			List<UINameTable> dataList = DataReader<UINameTable>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				UIID.mNameToID.set_Item(dataList.get_Item(i).name, dataList.get_Item(i).id);
			}
		}
		int result = 0;
		UIID.mNameToID.TryGetValue(name, ref result);
		return result;
	}
}
