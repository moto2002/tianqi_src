using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public static class UGUITools
{
	public static WWW OpenURL(string url)
	{
		WWW result = null;
		try
		{
			result = new WWW(url);
		}
		catch (Exception ex)
		{
			Debuger.Error(ex.get_Message(), new object[0]);
		}
		return result;
	}

	public static WWW OpenURL(string url, WWWForm form)
	{
		if (form == null)
		{
			return UGUITools.OpenURL(url);
		}
		WWW result = null;
		try
		{
			result = new WWW(url, form);
		}
		catch (Exception ex)
		{
			Debuger.Error((ex == null) ? "<null>" : ex.get_Message(), new object[0]);
		}
		return result;
	}

	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	public static GameObject AddChild(GameObject parent, GameObject asset, bool forceShow, string name)
	{
		GameObject gameObject = UGUITools.AddChild(parent, asset, forceShow);
		if (gameObject != null && !string.IsNullOrEmpty(name))
		{
			gameObject.set_name(name);
		}
		return gameObject;
	}

	public static GameObject AddChild(GameObject parent, GameObject asset, bool forceShow)
	{
		if (asset == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(asset);
		ResourceManager.SetInstantiateUIRef(gameObject, null);
		UGUITools.SetParent(parent, gameObject, forceShow);
		return gameObject;
	}

	public static void SetParent(GameObject parent, GameObject goInstantiate, bool forceShow, string name)
	{
		if (goInstantiate != null && !string.IsNullOrEmpty(name))
		{
			goInstantiate.set_name(name);
		}
		UGUITools.SetParent(parent, goInstantiate, forceShow);
	}

	public static void SetParent(GameObject parent, GameObject goInstantiate, bool forceShow)
	{
		if (goInstantiate != null && forceShow)
		{
			goInstantiate.SetActive(true);
		}
		if (goInstantiate != null && parent != null)
		{
			Transform transform = goInstantiate.get_transform();
			transform.SetParent(parent.get_transform());
			goInstantiate.set_layer(parent.get_layer());
			transform.set_localPosition(Vector3.get_zero());
			transform.set_localRotation(Quaternion.get_identity());
			transform.set_localScale(Vector3.get_one());
		}
		else if (parent == null)
		{
			Transform transform2 = goInstantiate.get_transform();
			transform2.set_localPosition(Vector3.get_zero());
			transform2.set_localRotation(Quaternion.get_identity());
			transform2.set_localScale(Vector3.get_one());
		}
	}

	public static void ResetTransform(Transform go, Transform parent)
	{
		go.SetParent(parent);
		if (go is RectTransform)
		{
			UGUITools.ResetTransform(go as RectTransform);
		}
		else
		{
			UGUITools.ResetTransform(go);
		}
	}

	public static void ResetTransform(Transform go)
	{
		go.set_localPosition(Vector3.get_zero());
		go.set_localRotation(Quaternion.get_identity());
		go.set_localScale(Vector3.get_one());
	}

	public static void ResetTransform(RectTransform go, Transform parent)
	{
		if (go != null)
		{
			go.SetParent(parent);
			UGUITools.ResetTransform(go);
		}
		else
		{
			Debug.LogError("go is null");
		}
	}

	public static void ResetTransform(RectTransform go)
	{
		if (go != null)
		{
			go.set_localPosition(Vector3.get_zero());
			go.set_anchoredPosition(Vector2.get_zero());
			go.set_localRotation(Quaternion.get_identity());
			go.set_localScale(Vector3.get_one());
		}
		else
		{
			Debug.LogError("go is null");
		}
	}

	public static GameObject GetRoot(GameObject go)
	{
		Transform transform = go.get_transform();
		while (true)
		{
			Transform parent = transform.get_parent();
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.get_gameObject();
	}

	public static T GetTargetParent<T>(GameObject childNode) where T : Component
	{
		Transform transform = childNode.get_transform();
		T component = transform.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		return transform.GetComponentInParent<T>();
	}

	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.get_parent();
		}
		return false;
	}

	public static string GetHierarchy(GameObject obj)
	{
		if (obj == null)
		{
			return string.Empty;
		}
		string text = obj.get_name();
		while (obj.get_transform().get_parent() != null)
		{
			obj = obj.get_transform().get_parent().get_gameObject();
			text = obj.get_name() + "\\" + text;
		}
		return text;
	}

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		T component = go.GetComponent<T>();
		if (component == null)
		{
			Transform parent = go.get_transform().get_parent();
			while (parent != null && component == null)
			{
				component = parent.get_gameObject().GetComponent<T>();
				parent = parent.get_parent();
			}
		}
		return component;
	}

	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if (trans == null)
		{
			return (T)((object)null);
		}
		return trans.GetComponentInParent<T>();
	}

	private static void Activate(Transform t, bool compatibilityMode)
	{
		UGUITools.SetActiveSelf(t.get_gameObject(), true);
		if (compatibilityMode)
		{
			int i = 0;
			int childCount = t.get_childCount();
			while (i < childCount)
			{
				Transform child = t.GetChild(i);
				if (child.get_gameObject().get_activeSelf())
				{
					return;
				}
				i++;
			}
			int j = 0;
			int childCount2 = t.get_childCount();
			while (j < childCount2)
			{
				Transform child2 = t.GetChild(j);
				UGUITools.Activate(child2, true);
				j++;
			}
		}
	}

	private static void Deactivate(Transform t)
	{
		UGUITools.SetActiveSelf(t.get_gameObject(), false);
	}

	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.get_transform();
		if (state)
		{
			int i = 0;
			int childCount = transform.get_childCount();
			while (i < childCount)
			{
				Transform child = transform.GetChild(i);
				UGUITools.Activate(child, true);
				i++;
			}
		}
		else
		{
			int j = 0;
			int childCount2 = transform.get_childCount();
			while (j < childCount2)
			{
				Transform child2 = transform.GetChild(j);
				UGUITools.Deactivate(child2);
				j++;
			}
		}
	}

	[DebuggerHidden, DebuggerStepThrough]
	public static bool GetActive(Behaviour mb)
	{
		return mb && mb.get_enabled() && mb.get_gameObject().get_activeInHierarchy();
	}

	[DebuggerHidden, DebuggerStepThrough]
	public static bool GetActive(GameObject go)
	{
		return go && go.get_activeInHierarchy();
	}

	[DebuggerHidden, DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	public static string GetFuncName(object obj, string method)
	{
		if (obj == null)
		{
			return "<null>";
		}
		string text = obj.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		return (!string.IsNullOrEmpty(method)) ? (text + "/" + method) : text;
	}

	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		T[] components = go.GetComponents<T>();
		for (int i = 0; i < components.Length; i++)
		{
			T t = components[i];
			MethodInfo method = t.GetType().GetMethod(funcName, 52);
			if (method != null)
			{
				method.Invoke(t, null);
			}
		}
	}
}
