using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DLuJingShuXing")]
	[Serializable]
	public class DLuJingShuXing : IExtensible
	{
		private int _id;

		private int _type;

		private string _name = string.Empty;

		private string _desc = string.Empty;

		private string _icon = string.Empty;

		private string _invokeParam = string.Empty;

		private int _systemOpenID;

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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(5, IsRequired = false, Name = "desc", DataFormat = DataFormat.Default), DefaultValue("")]
		public string desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "icon", DataFormat = DataFormat.Default), DefaultValue("")]
		public string icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "invokeParam", DataFormat = DataFormat.Default), DefaultValue("")]
		public string invokeParam
		{
			get
			{
				return this._invokeParam;
			}
			set
			{
				this._invokeParam = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "systemOpenID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int systemOpenID
		{
			get
			{
				return this._systemOpenID;
			}
			set
			{
				this._systemOpenID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
