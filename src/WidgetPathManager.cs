using System;
using System.Collections.Generic;
using UnityEngine;

public class WidgetPathManager
{
	private static Dictionary<string, Dictionary<string, string>> mapWidgetPath = new Dictionary<string, Dictionary<string, string>>();

	private static Transform m_myTransform;

	private static Dictionary<string, string> mWidgetToFullName;

	private static bool mNameUnique = false;

	public static Dictionary<string, string> FillFullNameData(string name, Transform rootTransform, bool name_unique)
	{
		if (WidgetPathManager.mapWidgetPath.ContainsKey(name))
		{
			return WidgetPathManager.mapWidgetPath.get_Item(name);
		}
		WidgetPathManager.m_myTransform = rootTransform;
		WidgetPathManager.mNameUnique = name_unique;
		WidgetPathManager.mWidgetToFullName = new Dictionary<string, string>();
		WidgetPathManager.AddWigetToFullNameData(rootTransform.get_name(), rootTransform.get_name());
		WidgetPathManager.ToFillFullNameData(rootTransform);
		if (!string.IsNullOrEmpty(name))
		{
			WidgetPathManager.mapWidgetPath.set_Item(name, WidgetPathManager.mWidgetToFullName);
		}
		return WidgetPathManager.mWidgetToFullName;
	}

	private static void ToFillFullNameData(Transform rootTransform)
	{
		for (int i = 0; i < rootTransform.get_childCount(); i++)
		{
			WidgetPathManager.AddWigetToFullNameData(rootTransform.GetChild(i).get_name(), WidgetPathManager.GetFullName(rootTransform.GetChild(i)));
			WidgetPathManager.ToFillFullNameData(rootTransform.GetChild(i));
		}
	}

	private static void AddWigetToFullNameData(string widgetName, string fullName)
	{
		if (WidgetPathManager.mNameUnique)
		{
			if (WidgetPathManager.mWidgetToFullName.ContainsKey(widgetName))
			{
				Debuger.Error(widgetName, new object[0]);
			}
			else
			{
				WidgetPathManager.mWidgetToFullName.Add(widgetName, fullName);
			}
		}
		else
		{
			WidgetPathManager.mWidgetToFullName.set_Item(widgetName, fullName);
		}
	}

	private static string GetFullName(Transform currentTran)
	{
		string text = string.Empty;
		while (currentTran != WidgetPathManager.m_myTransform)
		{
			text = currentTran.get_name() + text;
			if (currentTran.get_parent() != WidgetPathManager.m_myTransform)
			{
				text = "/" + text;
			}
			currentTran = currentTran.get_parent();
		}
		return text;
	}
}
