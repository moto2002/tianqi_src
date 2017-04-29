using System;
using System.Collections.Generic;

namespace XEngine
{
	public abstract class XPool<T> where T : IReusable
	{
		private Stack<T> m_units = new Stack<T>();

		public T Get()
		{
			Stack<T> units = this.m_units;
			T result;
			lock (units)
			{
				if (this.m_units.get_Count() > 0)
				{
					result = this.m_units.Pop();
				}
				else
				{
					result = this.CreateUnit();
				}
			}
			return result;
		}

		protected abstract T CreateUnit();

		public void Recycle(T unit)
		{
			Stack<T> units = this.m_units;
			lock (units)
			{
				unit.Reset();
				this.m_units.Push(unit);
			}
		}
	}
}
