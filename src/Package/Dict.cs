using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "Dict")]
	[Serializable]
	public class Dict : IExtensible
	{
		private int _key;

		private string _value;

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

		[ProtoMember(2, IsRequired = true, Name = "value", DataFormat = DataFormat.Default)]
		public string value
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
