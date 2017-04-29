using System;

namespace ProtoBuf.Meta
{
	internal sealed class MutableList : BasicList
	{
		public object this[int index]
		{
			get
			{
				return this.head[index];
			}
			set
			{
				this.head[index] = value;
			}
		}

		public void RemoveLast()
		{
			this.head.RemoveLastWithMutate();
		}

		public void Clear()
		{
			this.head.Clear();
		}
	}
}
