using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuoMoXingBiao")]
	[Serializable]
	public class DiaoLuoMoXingBiao : IExtensible
	{
		private int _id;

		private string _path = string.Empty;

		private int _listlv;

		private int _height_HP;

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

		[ProtoMember(3, IsRequired = false, Name = "path", DataFormat = DataFormat.Default), DefaultValue("")]
		public string path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listlv
		{
			get
			{
				return this._listlv;
			}
			set
			{
				this._listlv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "height_HP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int height_HP
		{
			get
			{
				return this._height_HP;
			}
			set
			{
				this._height_HP = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
