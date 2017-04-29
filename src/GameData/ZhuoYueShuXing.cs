using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuoYueShuXing")]
	[Serializable]
	public class ZhuoYueShuXing : IExtensible
	{
		private int _id;

		private int _libraryId;

		private int _attrId;

		private float _num;

		private int _value;

		private string _maxAndLessValue = string.Empty;

		private int _floatingValue;

		private float _floatingStand;

		private int _color;

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

		[ProtoMember(3, IsRequired = true, Name = "libraryId", DataFormat = DataFormat.TwosComplement)]
		public int libraryId
		{
			get
			{
				return this._libraryId;
			}
			set
			{
				this._libraryId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "attrId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "num", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float num
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

		[ProtoMember(6, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "maxAndLessValue", DataFormat = DataFormat.Default), DefaultValue("")]
		public string maxAndLessValue
		{
			get
			{
				return this._maxAndLessValue;
			}
			set
			{
				this._maxAndLessValue = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "floatingValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int floatingValue
		{
			get
			{
				return this._floatingValue;
			}
			set
			{
				this._floatingValue = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "floatingStand", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float floatingStand
		{
			get
			{
				return this._floatingStand;
			}
			set
			{
				this._floatingStand = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
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
