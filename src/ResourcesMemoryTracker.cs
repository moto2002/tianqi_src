using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ResourcesMemoryTracker : Singleton<ResourcesMemoryTracker>
{
	private class TrackTypeInfo
	{
		public Type TrackType;

		public Func<ResourcesMemoryTracker.ObjectInfo, object> GroupFunction;
	}

	private class ObjectInfo
	{
		private Object TargetInstance;

		public Object Target
		{
			get;
			private set;
		}

		public uint Size
		{
			get;
			private set;
		}

		public string Name
		{
			get
			{
				return this.Target.get_name();
			}
		}

		public ObjectInfo(Object obj)
		{
			this.Target = obj;
			this.Size = (uint)((!(this.Target != null)) ? 0 : Profiler.GetRuntimeMemorySize(this.Target));
		}
	}

	private const float ByteToMB = 1048576f;

	private List<ResourcesMemoryTracker.TrackTypeInfo> TypeInfoList = new List<ResourcesMemoryTracker.TrackTypeInfo>();

	private static int LastSampleFrame;

	private static float TotalAllocatedMemory;

	public ResourcesMemoryTracker()
	{
		this.Init();
	}

	private void Init()
	{
		this.TypeInfoList.Clear();
		this.RegisterType<Shader>(null);
		this.RegisterType<AnimationClip>(null);
		this.RegisterType<Texture2D>((ResourcesMemoryTracker.ObjectInfo x) => ((x.Target as Texture2D).get_mipmapCount() != 1) ? "Scene" : "UI");
	}

	private void RegisterType<T>(Func<ResourcesMemoryTracker.ObjectInfo, object> groupFunc = null)
	{
		Type t = typeof(T);
		if (this.TypeInfoList.Exists((ResourcesMemoryTracker.TrackTypeInfo x) => x.TrackType.Equals(t)))
		{
			Debug.LogErrorFormat("重复注册的类型: {0}", new object[]
			{
				t
			});
		}
		else
		{
			this.TypeInfoList.Add(new ResourcesMemoryTracker.TrackTypeInfo
			{
				TrackType = t,
				GroupFunction = groupFunc
			});
		}
	}

	public void Sample()
	{
		ResourcesMemoryTracker.SampleGlobal();
		using (List<ResourcesMemoryTracker.TrackTypeInfo>.Enumerator enumerator = this.TypeInfoList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ResourcesMemoryTracker.TrackTypeInfo current = enumerator.get_Current();
				this.DoTrack(current);
			}
		}
	}

	private void DoTrack(ResourcesMemoryTracker.TrackTypeInfo info)
	{
		uint num = 0u;
		List<ResourcesMemoryTracker.ObjectInfo> list = new List<ResourcesMemoryTracker.ObjectInfo>();
		Object[] array = Object.FindObjectsOfTypeIncludingAssets(info.TrackType);
		Object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Object obj = array2[i];
			ResourcesMemoryTracker.ObjectInfo objectInfo = new ResourcesMemoryTracker.ObjectInfo(obj);
			list.Add(objectInfo);
			num += objectInfo.Size;
		}
		this.Log("-----------{0} size: {1}  percent: {2}--------------", new object[]
		{
			info.TrackType.get_Name(),
			this.ToMB(num),
			this.ToPercent(num / ResourcesMemoryTracker.TotalAllocatedMemory)
		});
		if (info.GroupFunction != null)
		{
			using (IEnumerator<IGrouping<object, ResourcesMemoryTracker.ObjectInfo>> enumerator = Enumerable.GroupBy<ResourcesMemoryTracker.ObjectInfo, object>(list, info.GroupFunction).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<object, ResourcesMemoryTracker.ObjectInfo> current = enumerator.get_Current();
					this.Log("---group {0} begin---", new object[]
					{
						current.get_Key()
					});
					uint num2 = 0u;
					List<ResourcesMemoryTracker.ObjectInfo> list2 = Enumerable.ToList<ResourcesMemoryTracker.ObjectInfo>(current);
					list2.Sort(delegate(ResourcesMemoryTracker.ObjectInfo x, ResourcesMemoryTracker.ObjectInfo y)
					{
						if (x.Size != y.Size)
						{
							return y.Size.CompareTo(x.Size);
						}
						return x.Name.CompareTo(y.Name);
					});
					using (List<ResourcesMemoryTracker.ObjectInfo>.Enumerator enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ResourcesMemoryTracker.ObjectInfo current2 = enumerator2.get_Current();
							num2 += current2.Size;
							this.LogObjectInfo(current2);
						}
					}
					this.Log("---group {0} end : size: {1}   percent: {2}", new object[]
					{
						current.get_Key(),
						this.ToMB(num2),
						this.ToPercent(num2 / num)
					});
				}
			}
		}
		else
		{
			using (IEnumerator<ResourcesMemoryTracker.ObjectInfo> enumerator3 = Enumerable.OrderByDescending<ResourcesMemoryTracker.ObjectInfo, uint>(list, (ResourcesMemoryTracker.ObjectInfo x) => x.Size).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ResourcesMemoryTracker.ObjectInfo current3 = enumerator3.get_Current();
					this.LogObjectInfo(current3);
				}
			}
		}
	}

	private static void SampleGlobal()
	{
		if (ResourcesMemoryTracker.LastSampleFrame != Time.get_frameCount())
		{
			ResourcesMemoryTracker.TotalAllocatedMemory = Profiler.GetTotalAllocatedMemory();
			ResourcesMemoryTracker.LastSampleFrame = Time.get_frameCount();
		}
	}

	private void LogObjectInfo(ResourcesMemoryTracker.ObjectInfo info)
	{
		this.Log("name: {0}\tsize: {1}", new object[]
		{
			info.Name,
			this.ToMB(info.Size)
		});
	}

	private void Log(string format, params object[] args)
	{
		Singleton<ResourcesMemoryInfoFile>.S.WriteLine(string.Format(format, args));
	}

	private string ToMB(uint size)
	{
		return string.Format("{0:f2}MB", size / 1048576f);
	}

	private string ToPercent(float ratio)
	{
		return string.Format("{0:f2}%", ratio * 100f);
	}
}
