using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class XDict<TKey, TValue> : IJsonSerializable
{
	public List<TKey> Keys = new List<TKey>();

	public List<TValue> Values = new List<TValue>();

	public int Count
	{
		get
		{
			return this.Keys.get_Count();
		}
	}

	public TValue this[TKey key]
	{
		get
		{
			int num = this.Keys.IndexOf(key);
			if (num < 0)
			{
				throw new KeyNotFoundException();
			}
			return this.Values.get_Item(num);
		}
		set
		{
			this.AddOrReplace(key, value);
		}
	}

	public TKey ElementKeyAt(int index)
	{
		if (index > this.Keys.get_Count() || index < 0)
		{
			return default(TKey);
		}
		return this.Keys.get_Item(index);
	}

	public TValue ElementValueAt(int index)
	{
		if (index > this.Values.get_Count() || index < 0)
		{
			return default(TValue);
		}
		return this.Values.get_Item(index);
	}

	private void AddOrReplace(TKey key, TValue value)
	{
		if (this.Keys.Contains(key))
		{
			this.Values.set_Item(this.Keys.IndexOf(key), value);
			return;
		}
		this.Keys.Add(key);
		this.Values.Add(value);
	}

	public void Add(TKey key, TValue value)
	{
		if (this.Keys.Contains(key))
		{
			Debug.LogError(string.Format("already exist key[{0}] in dictionary", key.ToString()));
			return;
		}
		this.Keys.Add(key);
		this.Values.Add(value);
	}

	public void Remove(TKey key)
	{
		if (this.Keys.Contains(key))
		{
			this.Values.RemoveAt(this.Keys.IndexOf(key));
			this.Keys.Remove(key);
		}
	}

	public void Clear()
	{
		this.Keys.Clear();
		this.Values.Clear();
	}

	public bool ContainsKey(TKey key)
	{
		return this.Keys.Contains(key);
	}

	public bool ContainsValue(TValue value)
	{
		return this.Values.Contains(value);
	}

	public string ToJson()
	{
		return JsonUtility.ToJson(this, true);
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		int num = this.Keys.IndexOf(key);
		if (num >= 0)
		{
			value = this.Values.get_Item(num);
			return true;
		}
		value = default(TValue);
		return false;
	}
}
