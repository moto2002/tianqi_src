using System;
using System.Collections.Generic;

public class LimitedQueue<T> : Queue<T>
{
	private int limit = -1;

	public int Limit
	{
		get
		{
			return this.limit;
		}
		set
		{
			this.limit = value;
		}
	}

	public LimitedQueue(int limit) : base(limit)
	{
		this.Limit = limit;
	}

	public void Enqueue(T item)
	{
		if (this.get_Count() >= this.Limit)
		{
			base.Dequeue();
		}
		base.Enqueue(item);
	}
}
