using System;

namespace ProtoBuf
{
	[AttributeUsage]
	public sealed class ProtoEnumAttribute : Attribute
	{
		private bool hasValue;

		private int enumValue;

		private string name;

		public int Value
		{
			get
			{
				return this.enumValue;
			}
			set
			{
				this.enumValue = value;
				this.hasValue = true;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public bool HasValue()
		{
			return this.hasValue;
		}
	}
}
