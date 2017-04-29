using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "WingBody")]
	[Serializable]
	public class WingBody : IExtensible
	{
		private int _id;

		private string _slot = string.Empty;

		private string _prefabPath = string.Empty;

		private int _listlv;

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

		[ProtoMember(4, IsRequired = false, Name = "slot", DataFormat = DataFormat.Default), DefaultValue("")]
		public string slot
		{
			get
			{
				return this._slot;
			}
			set
			{
				this._slot = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "prefabPath", DataFormat = DataFormat.Default), DefaultValue("")]
		public string prefabPath
		{
			get
			{
				return this._prefabPath;
			}
			set
			{
				this._prefabPath = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
