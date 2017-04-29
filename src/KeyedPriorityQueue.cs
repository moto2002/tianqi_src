using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

[Serializable]
public class KeyedPriorityQueue<K, V, P> where V : class
{
	[Serializable]
	private struct HeapNode<KK, VV, PP>
	{
		public KK Key;

		public VV Value;

		public PP Priority;

		public HeapNode(KK key, VV value, PP priority)
		{
			this.Key = key;
			this.Value = value;
			this.Priority = priority;
		}
	}

	private List<KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P>> heap;

	private KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P> placeHolder;

	private Comparer<P> priorityComparer;

	private int size;

	public event EventHandler<KeyedPriorityQueueHeadChangedEventArgs<V>> FirstElementChanged
	{
		[MethodImpl(32)]
		add
		{
			this.FirstElementChanged = (EventHandler<KeyedPriorityQueueHeadChangedEventArgs<V>>)Delegate.Combine(this.FirstElementChanged, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.FirstElementChanged = (EventHandler<KeyedPriorityQueueHeadChangedEventArgs<V>>)Delegate.Remove(this.FirstElementChanged, value);
		}
	}

	public int Count
	{
		get
		{
			return this.size;
		}
	}

	public ReadOnlyCollection<K> Keys
	{
		get
		{
			List<K> list = new List<K>();
			for (int i = 1; i <= this.size; i++)
			{
				list.Add(this.heap.get_Item(i).Key);
			}
			return new ReadOnlyCollection<K>(list);
		}
	}

	public ReadOnlyCollection<V> Values
	{
		get
		{
			List<V> list = new List<V>();
			for (int i = 1; i <= this.size; i++)
			{
				list.Add(this.heap.get_Item(i).Value);
			}
			return new ReadOnlyCollection<V>(list);
		}
	}

	public KeyedPriorityQueue()
	{
		this.heap = new List<KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P>>();
		this.priorityComparer = Comparer<P>.get_Default();
		this.placeHolder = default(KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P>);
		this.heap.Add(default(KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P>));
	}

	public void Clear()
	{
		this.heap.Clear();
		this.size = 0;
	}

	public V Dequeue()
	{
		V result = (this.size >= 1) ? this.DequeueImpl() : ((V)((object)null));
		V newHead = (this.size >= 1) ? this.heap.get_Item(1).Value : ((V)((object)null));
		this.RaiseHeadChangedEvent((V)((object)null), newHead);
		return result;
	}

	private V DequeueImpl()
	{
		V value = this.heap.get_Item(1).Value;
		this.heap.set_Item(1, this.heap.get_Item(this.size));
		this.heap.set_Item(this.size--, this.placeHolder);
		this.Heapify(1);
		return value;
	}

	public void Enqueue(K key, V value, P priority)
	{
		V v = (this.size <= 0) ? ((V)((object)null)) : this.heap.get_Item(1).Value;
		int num = ++this.size;
		int num2 = num / 2;
		if (num == this.heap.get_Count())
		{
			this.heap.Add(this.placeHolder);
		}
		while (num > 1 && this.IsHigher(priority, this.heap.get_Item(num2).Priority))
		{
			this.heap.set_Item(num, this.heap.get_Item(num2));
			num = num2;
			num2 = num / 2;
		}
		this.heap.set_Item(num, new KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P>(key, value, priority));
		V value2 = this.heap.get_Item(1).Value;
		if (!value2.Equals(v))
		{
			this.RaiseHeadChangedEvent(v, value2);
		}
	}

	public V FindByPriority(P priority, Predicate<V> match)
	{
		if (this.size >= 1)
		{
			return this.Search(priority, 1, match);
		}
		return (V)((object)null);
	}

	private void Heapify(int i)
	{
		int num = 2 * i;
		int num2 = num + 1;
		int num3 = i;
		if (num <= this.size && this.IsHigher(this.heap.get_Item(num).Priority, this.heap.get_Item(i).Priority))
		{
			num3 = num;
		}
		if (num2 <= this.size && this.IsHigher(this.heap.get_Item(num2).Priority, this.heap.get_Item(num3).Priority))
		{
			num3 = num2;
		}
		if (num3 != i)
		{
			this.Swap(i, num3);
			this.Heapify(num3);
		}
	}

	protected virtual bool IsHigher(P p1, P p2)
	{
		return this.priorityComparer.Compare(p1, p2) < 1;
	}

	public V Peek()
	{
		if (this.size >= 1)
		{
			return this.heap.get_Item(1).Value;
		}
		return (V)((object)null);
	}

	private void RaiseHeadChangedEvent(V oldHead, V newHead)
	{
		if (oldHead != newHead)
		{
			EventHandler<KeyedPriorityQueueHeadChangedEventArgs<V>> firstElementChanged = this.FirstElementChanged;
			if (firstElementChanged != null)
			{
				firstElementChanged.Invoke(this, new KeyedPriorityQueueHeadChangedEventArgs<V>(oldHead, newHead));
			}
		}
	}

	public V Remove(K key)
	{
		if (this.size >= 1)
		{
			V value = this.heap.get_Item(1).Value;
			for (int i = 1; i <= this.size; i++)
			{
				if (this.heap.get_Item(i).Key.Equals(key))
				{
					V value2 = this.heap.get_Item(i).Value;
					this.Swap(i, this.size);
					this.heap.set_Item(this.size--, this.placeHolder);
					this.Heapify(i);
					V value3 = this.heap.get_Item(1).Value;
					if (!value.Equals(value3))
					{
						this.RaiseHeadChangedEvent(value, value3);
					}
					return value2;
				}
			}
		}
		return (V)((object)null);
	}

	public V Get(K key)
	{
		if (this.size >= 1)
		{
			for (int i = 1; i <= this.size; i++)
			{
				if (this.heap.get_Item(i).Key.Equals(key))
				{
					return this.heap.get_Item(i).Value;
				}
			}
		}
		return (V)((object)null);
	}

	private V Search(P priority, int i, Predicate<V> match)
	{
		V v = (V)((object)null);
		if (this.IsHigher(this.heap.get_Item(i).Priority, priority))
		{
			if (match.Invoke(this.heap.get_Item(i).Value))
			{
				v = this.heap.get_Item(i).Value;
			}
			int num = 2 * i;
			int num2 = num + 1;
			if (v == null && num <= this.size)
			{
				v = this.Search(priority, num, match);
			}
			if (v == null && num2 <= this.size)
			{
				v = this.Search(priority, num2, match);
			}
		}
		return v;
	}

	private void Swap(int i, int j)
	{
		KeyedPriorityQueue<K, V, P>.HeapNode<K, V, P> heapNode = this.heap.get_Item(i);
		this.heap.set_Item(i, this.heap.get_Item(j));
		this.heap.set_Item(j, heapNode);
	}
}
