using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RefreshShopInfo")]
	[Serializable]
	public class RefreshShopInfo : IExtensible
	{
		private int _shopId;

		private bool _sysRefreshFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "shopId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "sysRefreshFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool sysRefreshFlag
		{
			get
			{
				return this._sysRefreshFlag;
			}
			set
			{
				this._sysRefreshFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
