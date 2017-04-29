using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ItemInfo")]
	[Serializable]
	public class ItemInfo : IExtensible
	{
		private ItemBaseInfo _baseInfo;

		private ItemExtendInfo _exInfo;

		private int _fuckVal;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "baseInfo", DataFormat = DataFormat.Default)]
		public ItemBaseInfo baseInfo
		{
			get
			{
				return this._baseInfo;
			}
			set
			{
				this._baseInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "exInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ItemExtendInfo exInfo
		{
			get
			{
				return this._exInfo;
			}
			set
			{
				this._exInfo = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "fuckVal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuckVal
		{
			get
			{
				return this._fuckVal;
			}
			set
			{
				this._fuckVal = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
