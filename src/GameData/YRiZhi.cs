using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YRiZhi")]
	[Serializable]
	public class YRiZhi : IExtensible
	{
		private int _id;

		private string _eventType = string.Empty;

		private string _eventId = string.Empty;

		private string _depict = string.Empty;

		private readonly List<int> _dataType = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "eventType", DataFormat = DataFormat.Default), DefaultValue("")]
		public string eventType
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

		[ProtoMember(4, IsRequired = false, Name = "eventId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string eventId
		{
			get
			{
				return this._eventId;
			}
			set
			{
				this._eventId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "depict", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(6, Name = "dataType", DataFormat = DataFormat.TwosComplement)]
		public List<int> dataType
		{
			get
			{
				return this._dataType;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
