using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "Int64IndexValue")]
	[Serializable]
	public class Int64IndexValue : IExtensible
	{
		private int _index;

		private long _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "index", DataFormat = DataFormat.TwosComplement)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
