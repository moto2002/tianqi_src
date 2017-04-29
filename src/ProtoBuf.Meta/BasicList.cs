using System;
using System.Collections;

namespace ProtoBuf.Meta
{
	internal class BasicList : IEnumerable
	{
		public struct NodeEnumerator : IEnumerator
		{
			private int position;

			private readonly BasicList.Node node;

			public object Current
			{
				get
				{
					return this.node[this.position];
				}
			}

			internal NodeEnumerator(BasicList.Node node)
			{
				this.position = -1;
				this.node = node;
			}

			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			public bool MoveNext()
			{
				int length = this.node.Length;
				return this.position <= length && ++this.position < length;
			}
		}

		internal sealed class Node
		{
			private readonly object[] data;

			private int length;

			public object this[int index]
			{
				get
				{
					if (index >= 0 && index < this.length)
					{
						return this.data[index];
					}
					throw new ArgumentOutOfRangeException("index");
				}
				set
				{
					if (index >= 0 && index < this.length)
					{
						this.data[index] = value;
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			public int Length
			{
				get
				{
					return this.length;
				}
			}

			internal Node(object[] data, int length)
			{
				this.data = data;
				this.length = length;
			}

			public void RemoveLastWithMutate()
			{
				if (this.length == 0)
				{
					throw new InvalidOperationException();
				}
				this.length--;
			}

			public BasicList.Node Append(object value)
			{
				int num = this.length + 1;
				object[] array;
				if (this.data == null)
				{
					array = new object[10];
				}
				else if (this.length == this.data.Length)
				{
					array = new object[this.data.Length * 2];
					Array.Copy(this.data, array, this.length);
				}
				else
				{
					array = this.data;
				}
				array[this.length] = value;
				return new BasicList.Node(array, num);
			}

			public BasicList.Node Trim()
			{
				if (this.length == 0 || this.length == this.data.Length)
				{
					return this;
				}
				object[] array = new object[this.length];
				Array.Copy(this.data, array, this.length);
				return new BasicList.Node(array, this.length);
			}

			internal int IndexOfString(string value)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (value == (string)this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			internal int IndexOfReference(object instance)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (instance == this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (predicate(this.data[i], ctx))
					{
						return i;
					}
				}
				return -1;
			}

			internal void CopyTo(Array array, int offset)
			{
				if (this.length > 0)
				{
					Array.Copy(this.data, 0, array, offset, this.length);
				}
			}

			internal void Clear()
			{
				if (this.data != null)
				{
					Array.Clear(this.data, 0, this.data.Length);
				}
				this.length = 0;
			}
		}

		internal sealed class Group
		{
			public readonly int First;

			public readonly BasicList Items;

			public Group(int first)
			{
				this.First = first;
				this.Items = new BasicList();
			}
		}

		internal delegate bool MatchPredicate(object value, object ctx);

		private static readonly BasicList.Node nil = new BasicList.Node(null, 0);

		protected BasicList.Node head = BasicList.nil;

		public object this[int index]
		{
			get
			{
				return this.head[index];
			}
		}

		public int Count
		{
			get
			{
				return this.head.Length;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		public void CopyTo(Array array, int offset)
		{
			this.head.CopyTo(array, offset);
		}

		public int Add(object value)
		{
			return (this.head = this.head.Append(value)).Length - 1;
		}

		public void Trim()
		{
			this.head = this.head.Trim();
		}

		public BasicList.NodeEnumerator GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
		{
			return this.head.IndexOf(predicate, ctx);
		}

		internal int IndexOfString(string value)
		{
			return this.head.IndexOfString(value);
		}

		internal int IndexOfReference(object instance)
		{
			return this.head.IndexOfReference(instance);
		}

		internal bool Contains(object value)
		{
			BasicList.NodeEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (object.Equals(current, value))
				{
					return true;
				}
			}
			return false;
		}

		internal static BasicList GetContiguousGroups(int[] keys, object[] values)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length < keys.Length)
			{
				throw new ArgumentException("Not all keys are covered by values", "values");
			}
			BasicList basicList = new BasicList();
			BasicList.Group group = null;
			for (int i = 0; i < keys.Length; i++)
			{
				if (i == 0 || keys[i] != keys[i - 1])
				{
					group = null;
				}
				if (group == null)
				{
					group = new BasicList.Group(keys[i]);
					basicList.Add(group);
				}
				group.Items.Add(values[i]);
			}
			return basicList;
		}
	}
}
