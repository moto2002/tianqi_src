using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ExcellentAttr")]
	[Serializable]
	public class ExcellentAttr : IExtensible
	{
		private int _attrId;

		private long _value;

		private float _color;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "attrId", DataFormat = DataFormat.TwosComplement)]
		public int attrId
		{
			get
			{
				return this._attrId;
			}
			set
			{
				this._attrId = value;
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

		[ProtoMember(3, IsRequired = false, Name = "color", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
