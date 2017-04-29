using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BaoShiShengJi")]
	[Serializable]
	public class BaoShiShengJi : IExtensible
	{
		private int _id;

		private int _type;

		private string _color;

		private int _afterId;

		private int _lv;

		private int _needId;

		private int _composeAmount;

		private int _value;

		private int _propertyType;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "color", DataFormat = DataFormat.Default)]
		public string color
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

		[ProtoMember(6, IsRequired = false, Name = "afterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int afterId
		{
			get
			{
				return this._afterId;
			}
			set
			{
				this._afterId = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "needId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needId
		{
			get
			{
				return this._needId;
			}
			set
			{
				this._needId = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "composeAmount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int composeAmount
		{
			get
			{
				return this._composeAmount;
			}
			set
			{
				this._composeAmount = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "value", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(11, IsRequired = false, Name = "propertyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int propertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				this._propertyType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
