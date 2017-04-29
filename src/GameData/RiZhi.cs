using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RiZhi")]
	[Serializable]
	public class RiZhi : IExtensible
	{
		private int _id;

		private string _eventType = string.Empty;

		private string _eventId = string.Empty;

		private string _depict = string.Empty;

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

		[ProtoMember(2, IsRequired = false, Name = "eventType", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, IsRequired = false, Name = "eventId", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(4, IsRequired = false, Name = "depict", DataFormat = DataFormat.Default), DefaultValue("")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
