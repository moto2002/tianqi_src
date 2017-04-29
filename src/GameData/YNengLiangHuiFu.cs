using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YNengLiangHuiFu")]
	[Serializable]
	public class YNengLiangHuiFu : IExtensible
	{
		private int _id;

		private string _eventName = string.Empty;

		private string _powerName = string.Empty;

		private string _Model = string.Empty;

		private int _powerPoint;

		private int _probability;

		private int _depictId;

		private int _lessLevel;

		private int _maxLevel;

		private int _condition;

		private readonly List<int> _conditionValue = new List<int>();

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

		[ProtoMember(4, IsRequired = false, Name = "powerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string powerName
		{
			get
			{
				return this._powerName;
			}
			set
			{
				this._powerName = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Model", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(6, IsRequired = false, Name = "powerPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int powerPoint
		{
			get
			{
				return this._powerPoint;
			}
			set
			{
				this._powerPoint = value;
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

		[ProtoMember(8, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "lessLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "maxLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, Name = "conditionValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> conditionValue
		{
			get
			{
				return this._conditionValue;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
