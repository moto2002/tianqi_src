using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuo")]
	[Serializable]
	public class DiaoLuo : IExtensible
	{
		private int _id;

		private int _ruleId;

		private int _ruleType;

		private int _dropType;

		private int _goodsId;

		private long _minNum;

		private long _maxNum;

		private int _weigh;

		private int _circle;

		private int _minLv;

		private int _maxLv;

		private int _first;

		private string _beginTime = string.Empty;

		private string _endTime = string.Empty;

		private int _itemType;

		private int _excellentNum;

		private int _maxGlobalCount;

		private int _GlobleTimePeriod;

		private int _maxPersonalCount;

		private int _PersonalTimePeriod;

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

		[ProtoMember(3, IsRequired = true, Name = "ruleId", DataFormat = DataFormat.TwosComplement)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ruleType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruleType
		{
			get
			{
				return this._ruleType;
			}
			set
			{
				this._ruleType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "dropType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropType
		{
			get
			{
				return this._dropType;
			}
			set
			{
				this._dropType = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "goodsId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsId
		{
			get
			{
				return this._goodsId;
			}
			set
			{
				this._goodsId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "minNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long minNum
		{
			get
			{
				return this._minNum;
			}
			set
			{
				this._minNum = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "maxNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long maxNum
		{
			get
			{
				return this._maxNum;
			}
			set
			{
				this._maxNum = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "weigh", DataFormat = DataFormat.TwosComplement)]
		public int weigh
		{
			get
			{
				return this._weigh;
			}
			set
			{
				this._weigh = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "circle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int circle
		{
			get
			{
				return this._circle;
			}
			set
			{
				this._circle = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "first", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int first
		{
			get
			{
				return this._first;
			}
			set
			{
				this._first = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "beginTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string beginTime
		{
			get
			{
				return this._beginTime;
			}
			set
			{
				this._beginTime = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "endTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "itemType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				this._itemType = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "excellentNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int excellentNum
		{
			get
			{
				return this._excellentNum;
			}
			set
			{
				this._excellentNum = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "maxGlobalCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxGlobalCount
		{
			get
			{
				return this._maxGlobalCount;
			}
			set
			{
				this._maxGlobalCount = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "GlobleTimePeriod", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int GlobleTimePeriod
		{
			get
			{
				return this._GlobleTimePeriod;
			}
			set
			{
				this._GlobleTimePeriod = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "maxPersonalCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxPersonalCount
		{
			get
			{
				return this._maxPersonalCount;
			}
			set
			{
				this._maxPersonalCount = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "PersonalTimePeriod", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int PersonalTimePeriod
		{
			get
			{
				return this._PersonalTimePeriod;
			}
			set
			{
				this._PersonalTimePeriod = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
