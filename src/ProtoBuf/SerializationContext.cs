using System;

namespace ProtoBuf
{
	public sealed class SerializationContext
	{
		private bool frozen;

		private object context;

		private static readonly SerializationContext @default;

		public object Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (this.context != value)
				{
					this.ThrowIfFrozen();
					this.context = value;
				}
			}
		}

		internal static SerializationContext Default
		{
			get
			{
				return SerializationContext.@default;
			}
		}

		static SerializationContext()
		{
			SerializationContext.@default = new SerializationContext();
			SerializationContext.@default.Freeze();
		}

		internal void Freeze()
		{
			this.frozen = true;
		}

		private void ThrowIfFrozen()
		{
			if (this.frozen)
			{
				throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
			}
		}
	}
}
