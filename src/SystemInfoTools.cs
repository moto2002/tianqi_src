using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfoTools
{
	public static string GetDeviceName()
	{
		return SystemInfo.get_deviceName();
	}

	public static string GetDeviceModel()
	{
		return SystemInfo.get_deviceModel();
	}

	public static bool IsDeviceModelInBlacklist()
	{
		string deviceModel = SystemInfoTools.GetDeviceModel();
		if (!string.IsNullOrEmpty(deviceModel))
		{
			List<DeviceModel> dataList = DataReader<DeviceModel>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				if (dataList.get_Item(i).name == deviceModel)
				{
					return true;
				}
			}
		}
		return false;
	}
}
