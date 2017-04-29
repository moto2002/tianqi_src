using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuaJiDiTuPeiZhi")]
	[Serializable]
	public class GuaJiDiTuPeiZhi : IExtensible
	{
		private int _id;

		private int _minLv;

		private int _maxLv;

		private int _name;

		private readonly List<int> _area = new List<int>();

		private readonly List<int> _drop = new List<int>();

		private string _miniMap = string.Empty;

		private int _title;

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

		[ProtoMember(3, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, Name = "area", DataFormat = DataFormat.TwosComplement)]
		public List<int> area
		{
			get
			{
				return this._area;
			}
		}

		[ProtoMember(7, Name = "drop", DataFormat = DataFormat.TwosComplement)]
		public List<int> drop
		{
			get
			{
				return this._drop;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "miniMap", DataFormat = DataFormat.Default), DefaultValue("")]
		public string miniMap
		{
			get
			{
				return this._miniMap;
			}
			set
			{
				this._miniMap = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
