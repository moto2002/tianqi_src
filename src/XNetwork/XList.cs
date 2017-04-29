using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XNetwork
{
	public class XList<T>
	{
		public T[] buffer;

		public int size;

		public T this[int i]
		{
			get
			{
				return this.buffer[i];
			}
			set
			{
				this.buffer[i] = value;
			}
		}

		[DebuggerHidden]
		public IEnumerator<T> GetEnumerator()
		{
			XList<T>.<GetEnumerator>c__IteratorB <GetEnumerator>c__IteratorB = new XList<T>.<GetEnumerator>c__IteratorB();
			<GetEnumerator>c__IteratorB.<>f__this = this;
			return <GetEnumerator>c__IteratorB;
		}

		private void AllocateMore()
		{
			int num = (this.buffer != null) ? (this.buffer.Length << 1) : 0;
			if (num < 32)
			{
				num = 32;
			}
			T[] array = new T[num];
			if (this.buffer != null && this.size > 0)
			{
				this.buffer.CopyTo(array, 0);
			}
			this.buffer = array;
		}

		private void Trim()
		{
			if (this.size > 0)
			{
				if (this.size < this.buffer.Length)
				{
					T[] array = new T[this.size];
					for (int i = 0; i < this.size; i++)
					{
						array[i] = this.buffer[i];
					}
					this.buffer = array;
				}
			}
			else
			{
				this.buffer = new T[0];
			}
		}

		public void Clear()
		{
			this.size = 0;
		}

		public void Release()
		{
			this.size = 0;
			this.buffer = null;
		}

		public void Add(T item)
		{
			if (this.buffer == null || this.size == this.buffer.Length)
			{
				this.AllocateMore();
			}
			this.buffer[this.size++] = item;
		}

		public void Insert(int index, T item)
		{
			if (this.buffer == null || this.size == this.buffer.Length)
			{
				this.AllocateMore();
			}
			if (index < this.size)
			{
				for (int i = this.size; i > index; i--)
				{
					this.buffer[i] = this.buffer[i - 1];
				}
				this.buffer[index] = item;
				this.size++;
			}
			else
			{
				this.Add(item);
			}
		}

		public bool Contains(T item)
		{
			if (this.buffer == null)
			{
				return false;
			}
			for (int i = 0; i < this.size; i++)
			{
				if (this.buffer[i].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		public bool Remove(T item)
		{
			if (this.buffer != null)
			{
				EqualityComparer<T> @default = EqualityComparer<T>.get_Default();
				for (int i = 0; i < this.size; i++)
				{
					if (@default.Equals(this.buffer[i], item))
					{
						this.size--;
						this.buffer[i] = default(T);
						for (int j = i; j < this.size; j++)
						{
							this.buffer[j] = this.buffer[j + 1];
						}
						return true;
					}
				}
			}
			return false;
		}

		public void RemoveAt(int index)
		{
			if (this.buffer != null && index < this.size)
			{
				this.size--;
				this.buffer[index] = default(T);
				for (int i = index; i < this.size; i++)
				{
					this.buffer[i] = this.buffer[i + 1];
				}
			}
		}

		public T Pop()
		{
			if (this.buffer != null && this.size != 0)
			{
				T result = this.buffer[--this.size];
				this.buffer[this.size] = default(T);
				return result;
			}
			return default(T);
		}

		public T[] ToArray()
		{
			this.Trim();
			return this.buffer;
		}

		public void Sort(Comparison<T> comparer)
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int i = 1; i < this.size; i++)
				{
					if (comparer.Invoke(this.buffer[i - 1], this.buffer[i]) > 0)
					{
						T t = this.buffer[i];
						this.buffer[i] = this.buffer[i - 1];
						this.buffer[i - 1] = t;
						flag = true;
					}
				}
			}
		}
	}
}
