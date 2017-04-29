using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FuMoDaoJuShuXing")]
	[Serializable]
	public class FuMoDaoJuShuXing : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private readonly List<int> _position = new List<int>();

		private int _runeType;

		private int _Attrtype;

		private int _runeAttr;

		private int _valueType;

		private readonly List<int> _runeValue = new List<int>();

		private readonly List<int> _runeValueWeight = new List<int>();

		private int _floatingValue;

		private int _floatingStand;

		private string _describe = string.Empty;

		private string _resolve = string.Empty;

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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
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

		[ProtoMember(5, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public List<int> position
		{
			get
			{
				return this._position;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "runeType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int runeType
		{
			get
			{
				return this._runeType;
			}
			set
			{
				this._runeType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "Attrtype", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Attrtype
		{
			get
			{
				return this._Attrtype;
			}
			set
			{
				this._Attrtype = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "runeAttr", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int runeAttr
		{
			get
			{
				return this._runeAttr;
			}
			set
			{
				this._runeAttr = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "valueType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int valueType
		{
			get
			{
				return this._valueType;
			}
			set
			{
				this._valueType = value;
			}
		}

		[ProtoMember(10, Name = "runeValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> runeValue
		{
			get
			{
				return this._runeValue;
			}
		}

		[ProtoMember(11, Name = "runeValueWeight", DataFormat = DataFormat.TwosComplement)]
		public List<int> runeValueWeight
		{
			get
			{
				return this._runeValueWeight;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "floatingValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "floatingStand", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int floatingStand
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

		[ProtoMember(14, IsRequired = false, Name = "describe", DataFormat = DataFormat.Default), DefaultValue("")]
		public string describe
		{
			get
			{
				return this._describe;
			}
			set
			{
				this._describe = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "resolve", DataFormat = DataFormat.Default), DefaultValue("")]
		public string resolve
		{
			get
			{
				return this._resolve;
			}
			set
			{
				this._resolve = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
