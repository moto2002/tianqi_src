using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BuffTypeInfo")]
	[Serializable]
	public class BuffTypeInfo : IExtensible
	{
		private int _buffType;

		private int _num;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "buffType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buffType
		{
			get
			{
				return this._buffType;
			}
			set
			{
				this._buffType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
