using System;
using System.Collections.Generic;
using UnityEngine;

public class ListHelper : MonoBehaviour
{
	public string PrefabName;

	private List<Component> ChildrenCache = new List<Component>();

	public Transform ParentNode;

	public List<Transform> ParentNodes = new List<Transform>();

	public List<Component> Show<T>(int count) where T : Component
	{
		this.HideAll();
		List<Component> list = new List<Component>();
		for (int i = 0; i < count; i++)
		{
			if (i < this.ChildrenCache.get_Count())
			{
				this.ChildrenCache.get_Item(i).get_gameObject().SetActive(true);
			}
			else
			{
				Component component = this.Create().AddMissingComponent<T>();
				this.ChildrenCache.Add(component);
			}
			list.Add(this.ChildrenCache.get_Item(i));
		}
		return list;
	}

	public Component Get(int index)
	{
		if (index < this.ChildrenCache.get_Count())
		{
			return this.ChildrenCache.get_Item(index);
		}
		return null;
	}

	private void HideAll()
	{
		for (int i = 0; i < this.ChildrenCache.get_Count(); i++)
		{
			this.ChildrenCache.get_Item(i).get_gameObject().SetActive(false);
		}
	}

	private GameObject Create()
	{
		Transform transform = this.ParentNode;
		if (this.ChildrenCache.get_Count() < this.ParentNodes.get_Count())
		{
			transform = this.ParentNodes.get_Item(this.ChildrenCache.get_Count());
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(this.PrefabName);
		UGUITools.SetParent(transform.get_gameObject(), instantiate2Prefab, false);
		instantiate2Prefab.set_name("_Item " + this.ChildrenCache.get_Count());
		instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
		instantiate2Prefab.SetActive(true);
		return instantiate2Prefab;
	}
}
