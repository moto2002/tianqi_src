using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "StoreRefreshInfo")]
	[Serializable]
	public class StoreRefreshInfo : IExtensible
	{
		private int _storeId;

		private bool _sysRefreshFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "storeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int storeId
		{
			get
			{
				return this._storeId;
			}
			set
			{
				this._storeId = value;
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
