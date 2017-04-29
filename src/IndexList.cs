using System;
using System.Collections.Generic;

public class IndexList<TKey, TValue>
{
	private Dictionary<TKey, TValue> m_dict = new Dictionary<TKey, TValue>();

	private List<TValue> m_list = new List<TValue>();

	public TValue this[TKey key]
	{
		get
		{
			return this.m_dict.get_Item(key);
		}
		set
		{
			this.m_dict.set_Item(key, value);
		}
	}

	public int Count
	{
		get
		{
			return this.m_dict.get_Count();
		}
	}

	public void Add(TKey key, TValue value)
	{
		this.m_dict.set_Item(key, value);
	}

	public void AddValue(TValue value)
	{
		this.m_list.Add(value);
	}

	public bool Remove(TValue removeValue)
	{
		List<TKey> list = new List<TKey>();
		using (Dictionary<TKey, TValue>.Enumerator enumerator = this.m_dict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<TKey, TValue> current = enumerator.get_Current();
				TValue value = current.get_Value();
				if (value.Equals(removeValue))
				{
					list.Add(current.get_Key());
				}
			}
		}
		using (List<TKey>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				TKey current2 = enumerator2.get_Current();
				this.m_dict.Remove(current2);
			}
		}
		this.m_list.Remove(removeValue);
		return true;
	}

	public void Clear()
	{
		this.m_dict.Clear();
		this.m_list.Clear();
	}

	public bool ContainsKey(TKey key)
	{
		return this.m_dict.ContainsKey(key);
	}

	public bool ContainsValue(TValue value)
	{
		return this.m_list.Contains(value) || this.m_dict.ContainsValue(value);
	}

	public Dictionary<TKey, TValue> GetPairPart()
	{
		Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
		using (Dictionary<TKey, TValue>.Enumerator enumerator = this.m_dict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<TKey, TValue> current = enumerator.get_Current();
				dictionary.Add(current.get_Key(), current.get_Value());
			}
		}
		return dictionary;
	}

	public List<TValue> GetSinglePart()
	{
		List<TValue> list = new List<TValue>();
		using (List<TValue>.Enumerator enumerator = this.m_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TValue current = enumerator.get_Current();
				if (!this.m_dict.ContainsValue(current))
				{
					list.Add(current);
				}
			}
		}
		return list;
	}

	public List<TValue> GetAllValue()
	{
		List<TValue> list = new List<TValue>();
		using (Dictionary<TKey, TValue>.Enumerator enumerator = this.m_dict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<TKey, TValue> current = enumerator.get_Current();
				if (!list.Contains(current.get_Value()))
				{
					list.Add(current.get_Value());
				}
			}
		}
		for (int i = 0; i < this.m_list.get_Count(); i++)
		{
			TValue tValue = this.m_list.get_Item(i);
			if (!list.Contains(tValue))
			{
				list.Add(tValue);
			}
		}
		return list;
	}
}
