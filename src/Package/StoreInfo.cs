using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "StoreInfo")]
	[Serializable]
	public class StoreInfo : IExtensible
	{
		private int _storeId;

		private readonly List<StoreGoodsInfo> _goodsInfo = new List<StoreGoodsInfo>();

		private StoreExtra _storeExtra;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "storeId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "goodsInfo", DataFormat = DataFormat.Default)]
		public List<StoreGoodsInfo> goodsInfo
		{
			get
			{
				return this._goodsInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "storeExtra", DataFormat = DataFormat.Default), DefaultValue(null)]
		public StoreExtra storeExtra
		{
			get
			{
				return this._storeExtra;
			}
			set
			{
				this._storeExtra = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
