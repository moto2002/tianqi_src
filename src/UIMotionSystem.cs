using GameData;
using System;
using System.Collections.Generic;

public class UIMotionSystem
{
	public static bool IsUIMotionSystemOn = true;

	private static bool m_IsLock;

	public static bool IsLock
	{
		get
		{
			return UIMotionSystem.m_IsLock;
		}
		set
		{
			UIMotionSystem.m_IsLock = value;
		}
	}

	public static UISwitchAnim GetAnim(string uiName)
	{
		return null;
	}

	public static int GetUIID(string uiName)
	{
		List<UINameTable> dataList = DataReader<UINameTable>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).name.Equals(uiName))
			{
				return dataList.get_Item(i).id;
			}
		}
		return -1;
	}
}
