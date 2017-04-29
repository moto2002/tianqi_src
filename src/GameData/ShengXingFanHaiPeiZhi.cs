using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShengXingFanHaiPeiZhi")]
	[Serializable]
	public class ShengXingFanHaiPeiZhi : IExtensible
	{
		private int _star;

		private string _copper = string.Empty;

		private string _silver = string.Empty;

		private string _gold = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "star", DataFormat = DataFormat.TwosComplement)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "copper", DataFormat = DataFormat.Default), DefaultValue("")]
		public string copper
		{
			get
			{
				return this._copper;
			}
			set
			{
				this._copper = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "silver", DataFormat = DataFormat.Default), DefaultValue("")]
		public string silver
		{
			get
			{
				return this._silver;
			}
			set
			{
				this._silver = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "gold", DataFormat = DataFormat.Default), DefaultValue("")]
		public string gold
		{
			get
			{
				return this._gold;
			}
			set
			{
				this._gold = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
