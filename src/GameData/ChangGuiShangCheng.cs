using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChangGuiShangCheng")]
	[Serializable]
	public class ChangGuiShangCheng : IExtensible
	{
		private int _shopid;

		private byte[] _name;

		private int _itemid;

		private int _type1;

		private int _sort;

		private int _sex;

		private int _type;

		private int _probability;

		private int _status;

		private float _Discount;

		private int _grounding;

		private byte[] _online_time;

		private byte[] _offline_time;

		private int _rmbprice;

		private int _yxbprice;

		private int _conditiontpye;

		private int _condition;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "shopid", DataFormat = DataFormat.TwosComplement)]
		public int shopid
		{
			get
			{
				return this._shopid;
			}
			set
			{
				this._shopid = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] name
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

		[ProtoMember(3, IsRequired = false, Name = "itemid", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemid
		{
			get
			{
				return this._itemid;
			}
			set
			{
				this._itemid = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "type1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type1
		{
			get
			{
				return this._type1;
			}
			set
			{
				this._type1 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "sort", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this._sort = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "sex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sex
		{
			get
			{
				return this._sex;
			}
			set
			{
				this._sex = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "Discount", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float Discount
		{
			get
			{
				return this._Discount;
			}
			set
			{
				this._Discount = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "grounding", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int grounding
		{
			get
			{
				return this._grounding;
			}
			set
			{
				this._grounding = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "online_time", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] online_time
		{
			get
			{
				return this._online_time;
			}
			set
			{
				this._online_time = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "offline_time", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] offline_time
		{
			get
			{
				return this._offline_time;
			}
			set
			{
				this._offline_time = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "rmbprice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rmbprice
		{
			get
			{
				return this._rmbprice;
			}
			set
			{
				this._rmbprice = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "yxbprice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int yxbprice
		{
			get
			{
				return this._yxbprice;
			}
			set
			{
				this._yxbprice = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "conditiontpye", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int conditiontpye
		{
			get
			{
				return this._conditiontpye;
			}
			set
			{
				this._conditiontpye = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int condition
		{
			get
			{
				return this._condition;
			}
			set
			{
				this._condition = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
