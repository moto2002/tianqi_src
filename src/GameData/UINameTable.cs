using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "UINameTable")]
	[Serializable]
	public class UINameTable : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private int _parent;

		private int _hideTheVisible;

		private int _type;

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(6, IsRequired = false, Name = "parent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hideTheVisible", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hideTheVisible
		{
			get
			{
				return this._hideTheVisible;
			}
			set
			{
				this._hideTheVisible = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
