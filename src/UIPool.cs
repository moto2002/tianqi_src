using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UIPool
{
	private const string UNUSE_NAME = "[Unuse]";

	private const string DEFAULT_USE_NAME = "_Item";

	private string m_prefabname;

	private Transform m_rootNode;

	private bool m_forceShow;

	private List<GameObject> m_pools = new List<GameObject>();

	public List<GameObject> m_useds = new List<GameObject>();

	private int preload_num;

	protected WaitForSeconds wfs;

	private UIPool()
	{
	}

	public UIPool(string prefabName, Transform rootNode, bool forceShow)
	{
		this.m_prefabname = prefabName;
		this.m_rootNode = rootNode;
		this.m_forceShow = forceShow;
	}

	public GameObject Get(string name = "")
	{
		GameObject gameObject;
		if (this.m_pools.get_Count() > 0)
		{
			gameObject = this.m_pools.get_Item(0);
			this.m_useds.Add(gameObject);
			this.m_pools.Remove(gameObject);
		}
		else
		{
			gameObject = this.CreateInstantiate();
			this.m_useds.Add(gameObject);
		}
		if (gameObject != null)
		{
			if (!string.IsNullOrEmpty(name))
			{
				gameObject.set_name(name);
			}
			else
			{
				gameObject.set_name("_Item");
			}
			this.Show(gameObject, true);
		}
		this.Reset(gameObject);
		return gameObject;
	}

	public void ReUse(GameObject go)
	{
		this.ReUse(go, false);
	}

	public void ReUse(GameObject go, bool safe)
	{
		if (go != null)
		{
			this.Show(go, false);
			go.set_name("[Unuse]");
			this.Reset(go);
			if (safe)
			{
				if (!this.m_pools.Contains(go))
				{
					this.m_pools.Add(go);
				}
			}
			else
			{
				this.m_pools.Add(go);
			}
			this.m_useds.Remove(go);
		}
	}

	public void LoadInstantiateOfNum(int num, float delay_second, float duration_second)
	{
		this.preload_num = num;
		if (this.preload_num > this.GetTotalNum())
		{
			XTaskManager.instance.StartCoroutine(this.DoLoadInstantiateOfNum(delay_second, duration_second));
		}
	}

	[DebuggerHidden]
	public IEnumerator DoLoadInstantiateOfNum(float delay_second, float duration_second)
	{
		UIPool.<DoLoadInstantiateOfNum>c__Iterator3B <DoLoadInstantiateOfNum>c__Iterator3B = new UIPool.<DoLoadInstantiateOfNum>c__Iterator3B();
		<DoLoadInstantiateOfNum>c__Iterator3B.delay_second = delay_second;
		<DoLoadInstantiateOfNum>c__Iterator3B.duration_second = duration_second;
		<DoLoadInstantiateOfNum>c__Iterator3B.<$>delay_second = delay_second;
		<DoLoadInstantiateOfNum>c__Iterator3B.<$>duration_second = duration_second;
		<DoLoadInstantiateOfNum>c__Iterator3B.<>f__this = this;
		return <DoLoadInstantiateOfNum>c__Iterator3B;
	}

	private GameObject CreateInstantiate()
	{
		if (this.m_rootNode == null || string.IsNullOrEmpty(this.m_prefabname))
		{
			return null;
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(this.m_prefabname);
		UGUITools.SetParent(this.m_rootNode.get_gameObject(), instantiate2Prefab, this.m_forceShow);
		return instantiate2Prefab;
	}

	private void Reset(GameObject go)
	{
		if (go != null)
		{
			UGUITools.ResetTransform(go.get_transform(), this.m_rootNode);
		}
	}

	private int GetTotalNum()
	{
		return this.m_pools.get_Count() + this.m_useds.get_Count();
	}

	private void Show(GameObject go, bool isShow)
	{
		if (isShow)
		{
			go.SetActive(true);
		}
		else if (!this.m_forceShow)
		{
			go.SetActive(false);
		}
	}
}
