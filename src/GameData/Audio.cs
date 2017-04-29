using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Audio")]
	[Serializable]
	public class Audio : IExtensible
	{
		private int _id;

		private int _type;

		private int _listlv;

		private string _path = string.Empty;

		private int _mode;

		private float _volumeSize;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
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

		[ProtoMember(5, IsRequired = false, Name = "path", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(6, IsRequired = false, Name = "mode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "volumeSize", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float volumeSize
		{
			get
			{
				return this._volumeSize;
			}
			set
			{
				this._volumeSize = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
