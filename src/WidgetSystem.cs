using GameData;
using System;
using UnityEngine;

public class WidgetSystem
{
	public static Transform FindWidgetOnUI(int widgetId, bool activeSelf = true)
	{
		Transform result = null;
		string text = WidgetSystem.FindNameOfUIByWidget(widgetId);
		string[] array = WidgetSystem.FindNameOfWidgetById(widgetId);
		if (!string.IsNullOrEmpty(text) && array != null && array.Length >= 1)
		{
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist(text);
			if (uIIfExist != null)
			{
				if (activeSelf && !uIIfExist.get_gameObject().get_activeSelf())
				{
					return null;
				}
				if (array.Length == 1)
				{
					result = WidgetSystem.FindTransformOfuibase(uIIfExist, GameDataUtils.SplitString4Dot0(array[0]));
				}
				else if (array.Length == 2)
				{
					Transform transform = WidgetSystem.FindTransformOfuibase(uIIfExist, GameDataUtils.SplitString4Dot0(array[0]));
					if (transform != null)
					{
						result = XUtility.RecursiveFindTransform(transform, GameDataUtils.SplitString4Dot0(array[1]));
					}
				}
			}
		}
		return result;
	}

	public static bool IsWidgetActiveSelf(int widgetId)
	{
		Transform transform = WidgetSystem.FindWidgetOnUI(widgetId, true);
		return !(transform == null) && transform.get_gameObject().get_activeSelf();
	}

	public static string FindNameOfUIByWidget(int widgetId)
	{
		UIWidgetTable uIWidgetTable = DataReader<UIWidgetTable>.Get(widgetId);
		if (uIWidgetTable != null)
		{
			if (uIWidgetTable.uiId > 0)
			{
				return WidgetSystem.FindNameOfUIById(uIWidgetTable.uiId);
			}
			Debug.LogError("GameData.UIWidgetTable no uiId, widgetId = " + widgetId);
		}
		return string.Empty;
	}

	public static string FindNameOfUIById(int uiId)
	{
		UINameTable uINameTable = DataReader<UINameTable>.Get(uiId);
		if (uINameTable != null)
		{
			return uINameTable.name;
		}
		return string.Empty;
	}

	public static string[] FindNameOfWidgetById(int widgetId)
	{
		UIWidgetTable uIWidgetTable = DataReader<UIWidgetTable>.Get(widgetId);
		if (uIWidgetTable != null && !string.IsNullOrEmpty(uIWidgetTable.widgetName))
		{
			return uIWidgetTable.widgetName.Split(";".ToCharArray());
		}
		return null;
	}

	public static bool IsUIOpen(int uiId)
	{
		string prefabName = WidgetSystem.FindNameOfUIById(uiId);
		return UIManagerControl.Instance.IsOpen(prefabName);
	}

	public static void OpenUI(int uiId)
	{
		UINameTable uINameTable = DataReader<UINameTable>.Get(uiId);
		if (uINameTable == null)
		{
			return;
		}
		Transform root = WidgetSystem.GetRoot(uINameTable.parent);
		bool hideTheVisible = false;
		if (uINameTable.hideTheVisible == 1)
		{
			hideTheVisible = true;
		}
		UIType type = (UIType)uINameTable.type;
		UIManagerControl.Instance.OpenUI(uINameTable.name, root, hideTheVisible, type);
	}

	public static Transform GetRoot(int parent_value)
	{
		Transform result = UINodesManager.NormalUIRoot;
		switch (parent_value)
		{
		case 0:
			result = UINodesManager.NormalUIRoot;
			break;
		case 1:
			result = UINodesManager.MiddleUIRoot;
			break;
		case 2:
			result = UINodesManager.TopUIRoot;
			break;
		case 3:
			result = UINodesManager.T2RootOfSpecial;
			break;
		case 4:
			result = UINodesManager.T3RootOfSpecial;
			break;
		case 5:
			result = UINodesManager.T4RootOfSpecial;
			break;
		}
		return result;
	}

	public static bool IsStaticWidget(int widgetId)
	{
		string text = WidgetSystem.FindNameOfUIByWidget(widgetId);
		string[] array = WidgetSystem.FindNameOfWidgetById(widgetId);
		if (!string.IsNullOrEmpty(text) && array != null && array.Length >= 1)
		{
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist(text);
			if (uIIfExist != null && array.Length == 1)
			{
				return WidgetSystem.IsStatic(uIIfExist, GameDataUtils.SplitString4Dot0(array[0]));
			}
		}
		return false;
	}

	public static Transform FindTransformOfuibase(UIBase uiBase, string widgetName)
	{
		Transform transform = uiBase.FindTransform(widgetName);
		if (transform != null)
		{
			return transform;
		}
		return XUtility.RecursiveFindTransform(uiBase.get_transform(), widgetName);
	}

	public static bool IsStatic(UIBase uiBase, string widgetName)
	{
		Transform transform = uiBase.FindTransform(widgetName);
		return transform != null;
	}
}
