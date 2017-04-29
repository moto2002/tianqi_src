using GameData;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using XEngine.AssetLoader;

public sealed class DataReader<T> where T : class, new()
{
	private static List<T> m_dataList;

	private static Dictionary<int, T> m_intDataMap = new Dictionary<int, T>();

	private static Dictionary<string, T> m_strDataMap = new Dictionary<string, T>();

	public static List<T> DataList
	{
		get
		{
			DataReader<T>.Init();
			return DataReader<T>.m_dataList;
		}
	}

	public static void Init()
	{
		if (DataReader<T>.m_dataList != null)
		{
			return;
		}
		TextAsset textAsset = DataReader<T>.LoadData();
		if (textAsset != null)
		{
			DataReader<T>.InitData(textAsset.get_bytes());
			DataReader<T>.UnloadAsset();
		}
	}

	private static string GetFileName()
	{
		Type typeFromHandle = typeof(T);
		string[] array = typeFromHandle.ToString().Split(new char[]
		{
			'.'
		});
		return array[array.Length - 1];
	}

	public static TextAsset LoadData()
	{
		if (DataReader<T>.m_dataList != null)
		{
			return null;
		}
		string fileName = DataReader<T>.GetFileName();
		TextAsset textAsset = AssetLoader.LoadAssetNow("Data/cc_" + fileName, typeof(Object)) as TextAsset;
		if (textAsset == null)
		{
			Debug.LogError("加载 " + fileName + " 失败");
		}
		return textAsset;
	}

	public static void UnloadAsset()
	{
		AssetLoader.UnloadAsset("Data/cc_" + DataReader<T>.GetFileName(), null);
	}

	public static void PrepareSerializer()
	{
		Serializer.PrepareSerializer<List<T>>();
	}

	public static void InitData(byte[] data)
	{
		Type typeFromHandle = typeof(T);
		string fileName = DataReader<T>.GetFileName();
		if (data == null)
		{
			Debug.LogError("加载 " + fileName + " 失败");
			return;
		}
		try
		{
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				DataReader<T>.m_dataList = Serializer.Deserialize<List<T>>(memoryStream);
			}
		}
		catch (ProtoException ex)
		{
			Debug.LogError("表格数据解析错误 " + fileName + ": " + ex.ToString());
			return;
		}
		PropertyInfo propertyInfo = typeFromHandle.GetProperties(20)[0];
		if (propertyInfo.get_PropertyType() == typeof(int) || propertyInfo.get_PropertyType() == typeof(uint) || propertyInfo.get_PropertyType() == typeof(string))
		{
			for (int i = 0; i < DataReader<T>.m_dataList.get_Count(); i++)
			{
				if (propertyInfo.get_PropertyType() == typeof(int) || propertyInfo.get_PropertyType() == typeof(uint))
				{
					int num = Convert.ToInt32(propertyInfo.GetValue(DataReader<T>.m_dataList.get_Item(i), null));
					if (!DataReader<T>.m_intDataMap.ContainsKey(num))
					{
						DataReader<T>.m_intDataMap.Add(num, DataReader<T>.m_dataList.get_Item(i));
					}
				}
				else if (propertyInfo.get_PropertyType() == typeof(string))
				{
					string text = Convert.ToString(propertyInfo.GetValue(DataReader<T>.m_dataList.get_Item(i), null));
					if (!DataReader<T>.m_strDataMap.ContainsKey(text))
					{
						DataReader<T>.m_strDataMap.Add(text, DataReader<T>.m_dataList.get_Item(i));
					}
				}
			}
		}
	}

	public static T Get(int key)
	{
		if (typeof(T).Equals(typeof(ZhuXianPeiZhi)))
		{
			Debug.LogError("配置错误,该系统部分逻辑使用了表格ZhuXianPeiZhi,请迅速联系策划修改需求");
		}
		DataReader<T>.Init();
		if (DataReader<T>.m_intDataMap.ContainsKey(key))
		{
			return DataReader<T>.m_intDataMap.get_Item(key);
		}
		Debug.LogError(string.Concat(new object[]
		{
			"此表  ",
			typeof(T),
			" 没有包含为  key  ",
			key,
			"  对应的数据"
		}));
		return (T)((object)null);
	}

	public static T Get(string key)
	{
		if (typeof(T).Equals(typeof(ZhuXianPeiZhi)))
		{
			Debug.LogError("配置错误,该系统部分逻辑使用了表格ZhuXianPeiZhi,请迅速联系策划修改需求");
		}
		DataReader<T>.Init();
		if (DataReader<T>.m_strDataMap.ContainsKey(key))
		{
			return DataReader<T>.m_strDataMap.get_Item(key);
		}
		Debuger.Error(string.Concat(new object[]
		{
			"此表  ",
			typeof(T),
			" 没有包含为  key  ",
			key,
			"  对应的数据"
		}), new object[0]);
		return (T)((object)null);
	}

	public static bool Contains(int key)
	{
		DataReader<T>.Init();
		return DataReader<T>.m_intDataMap.ContainsKey(key);
	}

	public static bool Contains(string key)
	{
		DataReader<T>.Init();
		return DataReader<T>.m_strDataMap.ContainsKey(key);
	}
}
