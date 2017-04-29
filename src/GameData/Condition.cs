using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"conditionId"
	}), ProtoContract(Name = "Condition")]
	[Serializable]
	public class Condition : IExtensible
	{
		private int _conditionId;

		private int _target;

		private int _occasion;

		private int _dataId;

		private readonly List<int> _buffId = new List<int>();

		private readonly List<int> _buffType = new List<int>();

		private int _cgId;

		private readonly List<int> _rangeId = new List<int>();

		private int _delay;

		private readonly List<int> _damageType = new List<int>();

		private int _attr;

		private string _percentage = string.Empty;

		private string _base = string.Empty;

		private int _count;

		private readonly List<int> _extraInspection = new List<int>();

		private readonly List<int> _petType = new List<int>();

		private readonly List<int> _effectIdList = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "conditionId", DataFormat = DataFormat.TwosComplement)]
		public int conditionId
		{
			get
			{
				return this._conditionId;
			}
			set
			{
				this._conditionId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "target", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "occasion", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int occasion
		{
			get
			{
				return this._occasion;
			}
			set
			{
				this._occasion = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "dataId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dataId
		{
			get
			{
				return this._dataId;
			}
			set
			{
				this._dataId = value;
			}
		}

		[ProtoMember(7, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public List<int> buffId
		{
			get
			{
				return this._buffId;
			}
		}

		[ProtoMember(8, Name = "buffType", DataFormat = DataFormat.TwosComplement)]
		public List<int> buffType
		{
			get
			{
				return this._buffType;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "cgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cgId
		{
			get
			{
				return this._cgId;
			}
			set
			{
				this._cgId = value;
			}
		}

		[ProtoMember(10, Name = "rangeId", DataFormat = DataFormat.TwosComplement)]
		public List<int> rangeId
		{
			get
			{
				return this._rangeId;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "delay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int delay
		{
			get
			{
				return this._delay;
			}
			set
			{
				this._delay = value;
			}
		}

		[ProtoMember(12, Name = "damageType", DataFormat = DataFormat.TwosComplement)]
		public List<int> damageType
		{
			get
			{
				return this._damageType;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "attr", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attr
		{
			get
			{
				return this._attr;
			}
			set
			{
				this._attr = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "percentage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string percentage
		{
			get
			{
				return this._percentage;
			}
			set
			{
				this._percentage = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "base", DataFormat = DataFormat.Default), DefaultValue("")]
		public string @base
		{
			get
			{
				return this._base;
			}
			set
			{
				this._base = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(17, Name = "extraInspection", DataFormat = DataFormat.TwosComplement)]
		public List<int> extraInspection
		{
			get
			{
				return this._extraInspection;
			}
		}

		[ProtoMember(18, Name = "petType", DataFormat = DataFormat.TwosComplement)]
		public List<int> petType
		{
			get
			{
				return this._petType;
			}
		}

		[ProtoMember(19, Name = "effectIdList", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectIdList
		{
			get
			{
				return this._effectIdList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
