using System;
using UnityEngine;
using UnityEngine.UI;

public static class UIHelper
{
	private static Transform GetNode(Transform trans, string subnode)
	{
		Transform transform = trans.Find(subnode);
		if (transform == null)
		{
			Debug.Log(string.Concat(new string[]
			{
				"<color=red>Error:</color>",
				subnode,
				" node not found in '",
				trans.get_name(),
				"'!!!"
			}));
		}
		return transform;
	}

	public static T Get<T>(Transform trans, string subnode) where T : Component
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<T>();
		}
		return (T)((object)null);
	}

	public static T Get<T>(Component com, string subnode) where T : Component
	{
		return UIHelper.Get<T>(com.get_transform(), subnode);
	}

	public static T Get<T>(GameObject go, string subnode) where T : Component
	{
		return UIHelper.Get<T>(go.get_transform(), subnode);
	}

	public static Component GetComponent(Transform trans, string subnode, string type)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent(type);
		}
		return null;
	}

	public static Component GetComponent(Component com, string subnode, string type)
	{
		return UIHelper.GetComponent(com.get_transform(), subnode, type);
	}

	public static Component GetComponent(GameObject go, string subnode, string type)
	{
		return UIHelper.GetComponent(go.get_transform(), subnode, type);
	}

	public static GameObject GetObject(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.get_gameObject();
		}
		return null;
	}

	public static GameObject GetObject(Component com, string subnode)
	{
		return UIHelper.GetObject(com.get_transform(), subnode);
	}

	public static GameObject GetObject(GameObject go, string subnode)
	{
		return UIHelper.GetObject(go.get_transform(), subnode);
	}

	public static RectTransform GetRect(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<RectTransform>();
		}
		return null;
	}

	public static RectTransform GetRect(Component com, string subnode)
	{
		return UIHelper.GetRect(com.get_transform(), subnode);
	}

	public static RectTransform GetRect(GameObject go, string subnode)
	{
		return UIHelper.GetRect(go.get_transform(), subnode);
	}

	public static Text GetText(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<Text>();
		}
		return null;
	}

	public static Text GetText(Component com, string subnode)
	{
		return UIHelper.GetText(com.get_transform(), subnode);
	}

	public static Text GetText(GameObject go, string subnode)
	{
		return UIHelper.GetText(go.get_transform(), subnode);
	}

	public static Image GetImage(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<Image>();
		}
		return null;
	}

	public static Image GetImage(Component com, string subnode)
	{
		return UIHelper.GetImage(com.get_transform(), subnode);
	}

	public static Image GetImage(GameObject go, string subnode)
	{
		return UIHelper.GetImage(go.get_transform(), subnode);
	}

	public static Button GetButton(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<Button>();
		}
		return null;
	}

	public static Button GetButton(Component com, string subnode)
	{
		return UIHelper.GetButton(com.get_transform(), subnode);
	}

	public static Button GetButton(GameObject go, string subnode)
	{
		return UIHelper.GetButton(go.get_transform(), subnode);
	}

	public static ButtonCustom GetCustomButton(Transform trans, string subnode)
	{
		Transform node = UIHelper.GetNode(trans, subnode);
		if (node != null)
		{
			return node.GetComponent<ButtonCustom>();
		}
		return null;
	}

	public static ButtonCustom GetCustomButton(Component com, string subnode)
	{
		return UIHelper.GetCustomButton(com.get_transform(), subnode);
	}

	public static ButtonCustom GetCustomButton(GameObject go, string subnode)
	{
		return UIHelper.GetCustomButton(go.get_transform(), subnode);
	}
}
