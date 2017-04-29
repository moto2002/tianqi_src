using System;
using System.Collections;
using System.Collections.Generic;

namespace LuaInterface
{
	public class LuaArrayTable : IDisposable, IEnumerable<object>, IEnumerable
	{
		private class Enumerator : IDisposable, IEnumerator, IEnumerator<object>
		{
			private LuaState state;

			private int index = 1;

			private object current;

			public object Current
			{
				get
				{
					return this.current;
				}
			}

			public Enumerator(LuaArrayTable list)
			{
				this.state = list.state;
				this.state.Push(list.table);
			}

			public bool MoveNext()
			{
				this.state.LuaRawGetI(-1, this.index);
				this.current = this.state.ToVariant(-1);
				this.state.LuaPop(1);
				this.index++;
				return this.current != null;
			}

			public void Reset()
			{
				this.index = 1;
				this.current = null;
			}

			public void Dispose()
			{
				this.state.LuaPop(1);
			}
		}

		private LuaTable table;

		private LuaState state;

		public int Length
		{
			get
			{
				return this.table.Length;
			}
		}

		public object this[int key]
		{
			get
			{
				return this.table[key];
			}
			set
			{
				this.table[key] = value;
			}
		}

		public LuaArrayTable(LuaTable table)
		{
			table.AddRef();
			this.table = table;
			this.state = table.GetLuaState();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		~LuaArrayTable()
		{
			this.table.Dispose(false);
		}

		public void Dispose()
		{
			this.table.Dispose(true);
		}

		public IEnumerator<object> GetEnumerator()
		{
			return new LuaArrayTable.Enumerator(this);
		}
	}
}
