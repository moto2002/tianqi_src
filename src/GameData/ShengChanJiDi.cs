using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShengChanJiDi")]
	[Serializable]
	public class ShengChanJiDi : IExtensible
	{
		private int _id;

		private string _baseName = string.Empty;

		private int _baseicon;

		private int _baseType;

		private int _baseQuality;

		private int _time;

		private int _dropId;

		private int _probability;

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

		[ProtoMember(3, IsRequired = false, Name = "baseName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string baseName
		{
			get
			{
				return this._baseName;
			}
			set
			{
				this._baseName = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "baseicon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int baseicon
		{
			get
			{
				return this._baseicon;
			}
			set
			{
				this._baseicon = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "baseType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int baseType
		{
			get
			{
				return this._baseType;
			}
			set
			{
				this._baseType = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "baseQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int baseQuality
		{
			get
			{
				return this._baseQuality;
			}
			set
			{
				this._baseQuality = value;
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

		[ProtoMember(8, IsRequired = false, Name = "dropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
