using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "KeyAndValue")]
	[Serializable]
	public class KeyAndValue : IExtensible
	{
		private int _key;

		private int _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
		public int key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public int value
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
