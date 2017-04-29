using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "PetConversion")]
	[Serializable]
	public class PetConversion : IExtensible
	{
		private int _id;

		private int _nameQY;

		private int _frameQY;

		private string _nameSuffix = string.Empty;

		private string _tianfuSuffix = string.Empty;

		private int _frameID;

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

		[ProtoMember(3, IsRequired = false, Name = "nameQY", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameQY
		{
			get
			{
				return this._nameQY;
			}
			set
			{
				this._nameQY = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "frameQY", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frameQY
		{
			get
			{
				return this._frameQY;
			}
			set
			{
				this._frameQY = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "nameSuffix", DataFormat = DataFormat.Default), DefaultValue("")]
		public string nameSuffix
		{
			get
			{
				return this._nameSuffix;
			}
			set
			{
				this._nameSuffix = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "tianfuSuffix", DataFormat = DataFormat.Default), DefaultValue("")]
		public string tianfuSuffix
		{
			get
			{
				return this._tianfuSuffix;
			}
			set
			{
				this._tianfuSuffix = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "frameID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frameID
		{
			get
			{
				return this._frameID;
			}
			set
			{
				this._frameID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
