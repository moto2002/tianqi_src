using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YDaoJuKu")]
	[Serializable]
	public class YDaoJuKu : IExtensible
	{
		private int _id;

		private string _holdName = string.Empty;

		private string _Model = string.Empty;

		private int _dropId;

		private int _probability;

		private int _depictId;

		private int _eventType;

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

		[ProtoMember(3, IsRequired = false, Name = "holdName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string holdName
		{
			get
			{
				return this._holdName;
			}
			set
			{
				this._holdName = value;
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

		[ProtoMember(5, IsRequired = false, Name = "dropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId
		{
			get
			{
				return this._dropId;
			}
			set
			{
				this._dropId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "eventType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int eventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				this._eventType = value;
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
