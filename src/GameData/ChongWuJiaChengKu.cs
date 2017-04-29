using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuJiaChengKu")]
	[Serializable]
	public class ChongWuJiaChengKu : IExtensible
	{
		private int _id;

		private string _eventName = string.Empty;

		private int _Model;

		private int _propertyId;

		private int _addNum;

		private int _probability;

		private string _depict = string.Empty;

		private int _depictId;

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

		[ProtoMember(2, IsRequired = false, Name = "eventName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, IsRequired = false, Name = "Model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Model
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

		[ProtoMember(4, IsRequired = false, Name = "propertyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "addNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "depict", DataFormat = DataFormat.Default), DefaultValue("")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
