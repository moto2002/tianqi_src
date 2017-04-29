using System;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class ObjectPool
	{
		private const int MAX_SINGLE_LIMIT = 3;

		private Dictionary<string, Stack<GameObject>> m_pooled = new Dictionary<string, Stack<GameObject>>();

		public Dictionary<int, GameObject> m_allGObjects = new Dictionary<int, GameObject>();

		private int guid;

		private Transform root;

		public virtual GameObject Get(string resName)
		{
			if (!this.m_pooled.ContainsKey(resName))
			{
				this.m_pooled.Add(resName, new Stack<GameObject>());
			}
			if (this.IsExistInPool(resName))
			{
				this.m_allGObjects.Add(++this.guid, this.GetFromPool(resName));
			}
			else
			{
				this.m_allGObjects.Add(++this.guid, ObjectPool.Instantiate(this.GetAssetWithPool(resName), resName));
			}
			return this.m_allGObjects.get_Item(this.guid);
		}

		public int Get(string resName, Action<GameObject> callback)
		{
			ObjectPool.<Get>c__AnonStorey92 <Get>c__AnonStorey = new ObjectPool.<Get>c__AnonStorey92();
			<Get>c__AnonStorey.resName = resName;
			<Get>c__AnonStorey.callback = callback;
			<Get>c__AnonStorey.<>f__this = this;
			if (!this.m_pooled.ContainsKey(<Get>c__AnonStorey.resName))
			{
				this.m_pooled.Add(<Get>c__AnonStorey.resName, new Stack<GameObject>());
			}
			if (this.IsExistInPool(<Get>c__AnonStorey.resName))
			{
				this.m_allGObjects.Add(++this.guid, this.GetFromPool(<Get>c__AnonStorey.resName));
				<Get>c__AnonStorey.callback.Invoke(this.m_allGObjects.get_Item(this.guid));
				return this.guid;
			}
			this.m_allGObjects.Add(++this.guid, null);
			int id = this.guid;
			this.LoadAssetWithPool(<Get>c__AnonStorey.resName, delegate(bool isSuccess)
			{
				if (this.m_allGObjects.ContainsKey(id))
				{
					if (isSuccess)
					{
						this.m_allGObjects.set_Item(id, ObjectPool.Instantiate(this.GetAssetWithPool(<Get>c__AnonStorey.resName), <Get>c__AnonStorey.resName));
						<Get>c__AnonStorey.callback.Invoke(this.m_allGObjects.get_Item(id));
					}
					else
					{
						<Get>c__AnonStorey.callback.Invoke(null);
					}
				}
				else if (isSuccess)
				{
					Debug.Log("recycle " + <Get>c__AnonStorey.resName);
					this.DoPoolRecycle(ObjectPool.Instantiate(this.GetAssetWithPool(<Get>c__AnonStorey.resName), <Get>c__AnonStorey.resName));
				}
			});
			return id;
		}

		private static GameObject Instantiate(Object obj, string resName)
		{
			if (obj != null)
			{
				GameObject gameObject = Object.Instantiate(obj) as GameObject;
				gameObject.AddUniqueComponent<BaseBehaviour>().ResName = resName;
				return gameObject;
			}
			return null;
		}

		private bool IsExistInPool(string resName)
		{
			while (this.m_pooled.get_Item(resName).get_Count() > 0 && this.m_pooled.get_Item(resName).Peek() == null)
			{
				this.m_pooled.get_Item(resName).Pop();
			}
			return this.m_pooled.get_Item(resName).get_Count() > 0;
		}

		private GameObject GetFromPool(string resName)
		{
			return this.m_pooled.get_Item(resName).Pop();
		}

		public void PoolRecycle(int id)
		{
			if (this.m_allGObjects.ContainsKey(id))
			{
				if (this.m_allGObjects.get_Item(id) != null)
				{
					this.DoPoolRecycle(this.m_allGObjects.get_Item(id));
				}
				this.m_allGObjects.Remove(id);
			}
		}

		public void PoolRecycle(GameObject obj)
		{
			this.DoPoolRecycle(obj);
		}

		private void PoolRecycleAll()
		{
			List<int> list = new List<int>(this.m_allGObjects.get_Keys());
			for (int i = 0; i < list.get_Count(); i++)
			{
				this.PoolRecycle(list.get_Item(i));
			}
		}

		private void DoPoolRecycle(GameObject obj)
		{
			if (obj.GetComponentsInChildren<BaseBehaviour>(true).Length == 0)
			{
				Debug.LogError("不含回收标记，不可回收!");
				return;
			}
			string resName = obj.GetComponentsInChildren<BaseBehaviour>(true)[0].ResName;
			if (string.IsNullOrEmpty(resName))
			{
				Debug.LogError("回收标记错误!");
				return;
			}
			if (!this.m_pooled.ContainsKey(resName))
			{
				this.m_pooled.Add(resName, new Stack<GameObject>());
			}
			if (this.m_pooled.get_Item(resName).Contains(obj))
			{
				return;
			}
			if (this.m_pooled.get_Item(resName).get_Count() < 3)
			{
				obj.set_name("pooled_" + resName);
				obj.SetActive(false);
				if (this.root != null && obj.get_transform().get_parent() != this.root)
				{
					obj.get_transform().SetParent(this.root);
				}
				this.m_pooled.get_Item(resName).Push(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}

		public void DestroyAll()
		{
			using (Dictionary<string, Stack<GameObject>>.Enumerator enumerator = this.m_pooled.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, Stack<GameObject>> current = enumerator.get_Current();
					while (current.get_Value().get_Count() > 0)
					{
						Object.Destroy(current.get_Value().Pop());
					}
				}
			}
			this.m_pooled.Clear();
		}

		public void Clear()
		{
			this.PoolRecycleAll();
			this.DestroyAll();
		}

		public void SetRootNode(Transform trans)
		{
			this.root = trans;
		}

		protected virtual void LoadAssetWithPool(string path, Action<bool> finish_callback)
		{
			AssetManager.LoadAssetWithPool(path, finish_callback);
		}

		protected virtual Object GetAssetWithPool(string path)
		{
			return AssetManager.LoadAssetNowWithPool(path);
		}
	}
}
