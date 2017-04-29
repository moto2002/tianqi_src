using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"part"
	}), ProtoContract(Name = "EquipPartPeiZhi")]
	[Serializable]
	public class EquipPartPeiZhi : IExtensible
	{
		private int _part;

		private string _name = string.Empty;

		private string _nameImage = string.Empty;

		private string _partIcon1 = string.Empty;

		private string _partIcon2 = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "part", DataFormat = DataFormat.TwosComplement)]
		public int part
		{
			get
			{
				return this._part;
			}
			set
			{
				this._part = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "nameImage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string nameImage
		{
			get
			{
				return this._nameImage;
			}
			set
			{
				this._nameImage = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "partIcon1", DataFormat = DataFormat.Default), DefaultValue("")]
		public string partIcon1
		{
			get
			{
				return this._partIcon1;
			}
			set
			{
				this._partIcon1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "partIcon2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string partIcon2
		{
			get
			{
				return this._partIcon2;
			}
			set
			{
				this._partIcon2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
