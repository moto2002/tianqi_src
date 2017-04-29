using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "NoteData")]
	[Serializable]
	public class NoteData : IExtensible
	{
		private int _attrId;

		private long _value;

		private float _color;

		private int _position;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "attrId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(4, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
