using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuRenWuPinZhi")]
	[Serializable]
	public class ChongWuRenWuPinZhi : IExtensible
	{
		private int _id;

		private readonly List<int> _drop = new List<int>();

		private readonly List<int> _maindrop = new List<int>();

		private int _petquality;

		private string _art = string.Empty;

		private int _time;

		private int _lv;

		private readonly List<int> _name = new List<int>();

		private int _weight;

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

		[ProtoMember(3, Name = "drop", DataFormat = DataFormat.TwosComplement)]
		public List<int> drop
		{
			get
			{
				return this._drop;
			}
		}

		[ProtoMember(4, Name = "maindrop", DataFormat = DataFormat.TwosComplement)]
		public List<int> maindrop
		{
			get
			{
				return this._maindrop;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "petquality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petquality
		{
			get
			{
				return this._petquality;
			}
			set
			{
				this._petquality = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "art", DataFormat = DataFormat.Default), DefaultValue("")]
		public string art
		{
			get
			{
				return this._art;
			}
			set
			{
				this._art = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "name", DataFormat = DataFormat.TwosComplement)]
		public List<int> name
		{
			get
			{
				return this._name;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
