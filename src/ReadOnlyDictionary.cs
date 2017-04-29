using System;
using System.Collections.Generic;

public class ReadOnlyDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
	public TValue this[TKey key]
	{
		get
		{
			return base.get_Item(key);
		}
	}

	public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
	{
	}

	public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
	{
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public ReadOnlyDictionary()
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public ReadOnlyDictionary(IEqualityComparer<TKey> comparer)
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public ReadOnlyDictionary(int capacity)
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public ReadOnlyDictionary(int capacity, IEqualityComparer<TKey> comparer)
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public void Add(TKey key, TValue value)
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public void Clear()
	{
		throw new ReadOnlyException();
	}

	[Obsolete("Not Supported for ReadOnlyDictionaries", true)]
	public bool Remove(TKey key)
	{
		throw new ReadOnlyException();
	}
}
