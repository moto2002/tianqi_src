using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YChongWuJiaChengKu")]
	[Serializable]
	public class YChongWuJiaChengKu : IExtensible
	{
		private int _id;

		private string _eventName = string.Empty;

		private string _Model = string.Empty;

		private int _propertyId;

		private int _addNum;

		private int _probability;

		private string _depict = string.Empty;

		private int _depictId;

		private readonly List<int> _buffId = new List<int>();

		private string _depictCount = string.Empty;

		private int _lessLevel;

		private int _maxLevel;

		private int _condition;

		private readonly List<int> _conditionValue = new List<int>();

		private int _buffType;

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

		[ProtoMember(3, IsRequired = false, Name = "eventName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string eventName
		{
			get
			{
				return this._eventName;
			}
			set
			{
				this._eventName = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Model", DataFormat = DataFormat.Default), DefaultValue("")]
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				this._Model = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "propertyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int propertyId
		{
			get
			{
				return this._propertyId;
			}
			set
			{
				this._propertyId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "addNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addNum
		{
			get
			{
				return this._addNum;
			}
			set
			{
				this._addNum = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "depict", DataFormat = DataFormat.Default), DefaultValue("")]
		public string depict
		{
			get
			{
				return this._depict;
			}
			set
			{
				this._depict = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int depictId
		{
			get
			{
				return this._depictId;
			}
			set
			{
				this._depictId = value;
			}
		}

		[ProtoMember(10, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public List<int> buffId
		{
			get
			{
				return this._buffId;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "depictCount", DataFormat = DataFormat.Default), DefaultValue("")]
		public string depictCount
		{
			get
			{
				return this._depictCount;
			}
			set
			{
				this._depictCount = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "lessLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lessLevel
		{
			get
			{
				return this._lessLevel;
			}
			set
			{
				this._lessLevel = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "maxLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				this._maxLevel = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(15, Name = "conditionValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> conditionValue
		{
			get
			{
				return this._conditionValue;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "buffType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buffType
		{
			get
			{
				return this._buffType;
			}
			set
			{
				this._buffType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
