using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "VipXiaoGuo")]
	[Serializable]
	public class VipXiaoGuo : IExtensible
	{
		private int _effect;

		private int _type;

		private int _picture;

		private int _picText;

		private int _position;

		private int _value1;

		private int _value2;

		private int _value3;

		private int _name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public int effect
		{
			get
			{
				return this._effect;
			}
			set
			{
				this._effect = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "picText", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picText
		{
			get
			{
				return this._picText;
			}
			set
			{
				this._picText = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "value1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value1
		{
			get
			{
				return this._value1;
			}
			set
			{
				this._value1 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "value2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value2
		{
			get
			{
				return this._value2;
			}
			set
			{
				this._value2 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "value3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value3
		{
			get
			{
				return this._value3;
			}
			set
			{
				this._value3 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
