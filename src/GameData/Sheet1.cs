using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Sheet1")]
	[Serializable]
	public class Sheet1 : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private readonly List<int> _position = new List<int>();

		private int _runeType;

		private int _runeAttr;

		private readonly List<int> _runeValue = new List<int>();

		private readonly List<int> _runeValueWeight = new List<int>();

		private float _floatingValue;

		private float _floatingStand;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public List<int> position
		{
			get
			{
				return this._position;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "runeType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "runeAttr", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, Name = "runeValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> runeValue
		{
			get
			{
				return this._runeValue;
			}
		}

		[ProtoMember(7, Name = "runeValueWeight", DataFormat = DataFormat.TwosComplement)]
		public List<int> runeValueWeight
		{
			get
			{
				return this._runeValueWeight;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "floatingValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float floatingValue
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
