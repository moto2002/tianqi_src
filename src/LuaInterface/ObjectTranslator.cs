using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaInterface
{
	public class ObjectTranslator
	{
		private class DelayGC
		{
			public int id;

			public Object obj;

			public float time;

			public DelayGC(int id, Object obj, float time)
			{
				this.id = id;
				this.time = time;
				this.obj = obj;
			}
		}

		private class CompareObject : IEqualityComparer<object>
		{
			public bool Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			public int GetHashCode(object obj)
			{
				if (obj != null)
				{
					return obj.GetHashCode();
				}
				return 0;
			}
		}

		public readonly Dictionary<object, int> objectsBackMap = new Dictionary<object, int>(new ObjectTranslator.CompareObject());

		public readonly LuaObjectPool objects = new LuaObjectPool();

		private List<ObjectTranslator.DelayGC> gcList = new List<ObjectTranslator.DelayGC>();

		private static ObjectTranslator _translator;

		public bool LogGC
		{
			get;
			set;
		}

		public ObjectTranslator()
		{
			this.LogGC = false;
			ObjectTranslator._translator = this;
		}

		public int AddObject(object obj)
		{
			int num = this.objects.Add(obj);
			if (!TypeChecker.IsValueType(obj.GetType()))
			{
				this.objectsBackMap.set_Item(obj, num);
			}
			return num;
		}

		public static ObjectTranslator Get(IntPtr L)
		{
			return ObjectTranslator._translator;
		}

		public void RemoveObject(int udata)
		{
			object obj = this.objects.Remove(udata);
			if (obj != null)
			{
				if (!TypeChecker.IsValueType(obj.GetType()))
				{
					this.objectsBackMap.Remove(obj);
				}
				if (this.LogGC)
				{
					Debugger.Log("gc object {0}, id {1}", obj, udata);
				}
			}
		}

		public object GetObject(int udata)
		{
			return this.objects.TryGetValue(udata);
		}

		public void Destroy(int udata)
		{
			object obj = this.objects.Destroy(udata);
			if (obj != null)
			{
				if (!TypeChecker.IsValueType(obj.GetType()))
				{
					this.objectsBackMap.Remove(obj);
				}
				if (this.LogGC)
				{
					Debugger.Log("destroy object {0}, id {1}", obj, udata);
				}
			}
		}

		public void DelayDestroy(int id, float time)
		{
			Object @object = (Object)this.GetObject(id);
			if (@object != null)
			{
				this.gcList.Add(new ObjectTranslator.DelayGC(id, @object, time));
			}
		}

		public bool Getudata(object o, out int index)
		{
			index = -1;
			return this.objectsBackMap.TryGetValue(o, ref index);
		}

		public void SetBack(int index, object o)
		{
			object obj = this.objects.Replace(index, o);
			this.objectsBackMap.Remove(obj);
			this.objectsBackMap.set_Item(o, index);
		}

		private bool RemoveFromGCList(int id)
		{
			int num = this.gcList.FindIndex((ObjectTranslator.DelayGC p) => p.id == id);
			if (num >= 0)
			{
				this.gcList.RemoveAt(num);
				return true;
			}
			return false;
		}

		private void DestroyUnityObject(int udata, Object obj)
		{
			object obj2 = this.objects.TryGetValue(udata);
			if (object.ReferenceEquals(obj2, obj))
			{
				this.objectsBackMap.Remove(obj2);
				this.objects.Destroy(udata);
				if (this.LogGC)
				{
					Debugger.Log("destroy object {0}, id {1}", obj2, udata);
				}
			}
			Object.Destroy(obj);
		}

		public void Collect()
		{
			if (this.gcList.get_Count() == 0)
			{
				return;
			}
			float deltaTime = Time.get_deltaTime();
			for (int i = this.gcList.get_Count() - 1; i >= 0; i--)
			{
				float num = this.gcList.get_Item(i).time - deltaTime;
				if (num <= 0f)
				{
					this.DestroyUnityObject(this.gcList.get_Item(i).id, this.gcList.get_Item(i).obj);
					this.gcList.RemoveAt(i);
				}
				else
				{
					this.gcList.get_Item(i).time = num;
				}
			}
		}

		public void Dispose()
		{
			this.objectsBackMap.Clear();
			this.objects.Clear();
			ObjectTranslator._translator = null;
		}
	}
}
