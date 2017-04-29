using System;
using System.Collections.Generic;

namespace LuaInterface
{
	public class LuaObjectPool
	{
		private class PoolNode
		{
			public int index;

			public object obj;

			public PoolNode(int index, object obj)
			{
				this.index = index;
				this.obj = obj;
			}
		}

		private List<LuaObjectPool.PoolNode> list;

		private LuaObjectPool.PoolNode head;

		private int count;

		public object this[int i]
		{
			get
			{
				if (i > 0 && i < this.count)
				{
					return this.list.get_Item(i).obj;
				}
				return null;
			}
		}

		public LuaObjectPool()
		{
			this.list = new List<LuaObjectPool.PoolNode>(1024);
			this.head = new LuaObjectPool.PoolNode(0, null);
			this.list.Add(this.head);
			this.list.Add(new LuaObjectPool.PoolNode(1, null));
			this.count = this.list.get_Count();
		}

		public void Clear()
		{
			this.list.Clear();
			this.head = null;
			this.count = 0;
		}

		public int Add(object obj)
		{
			int index;
			if (this.head.index != 0)
			{
				index = this.head.index;
				this.list.get_Item(index).obj = obj;
				this.head.index = this.list.get_Item(index).index;
			}
			else
			{
				index = this.list.get_Count();
				this.list.Add(new LuaObjectPool.PoolNode(index, obj));
				this.count = index + 1;
			}
			return index;
		}

		public object TryGetValue(int index)
		{
			if (index > 0 && index < this.count)
			{
				return this.list.get_Item(index).obj;
			}
			return false;
		}

		public object Remove(int pos)
		{
			if (pos > 0 && pos < this.count)
			{
				object obj = this.list.get_Item(pos).obj;
				this.list.get_Item(pos).obj = null;
				this.list.get_Item(pos).index = this.head.index;
				this.head.index = pos;
				return obj;
			}
			return null;
		}

		public object Destroy(int pos)
		{
			if (pos > 0 && pos < this.count)
			{
				object obj = this.list.get_Item(pos).obj;
				this.list.get_Item(pos).obj = null;
				return obj;
			}
			return null;
		}

		public object Replace(int pos, object o)
		{
			if (pos > 0 && pos < this.count)
			{
				object obj = this.list.get_Item(pos).obj;
				this.list.get_Item(pos).obj = o;
				return obj;
			}
			return null;
		}
	}
}
