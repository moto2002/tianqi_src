using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6639), ForSend(6639), ProtoContract(Name = "BuyGoodsRes")]
	[Serializable]
	public class BuyGoodsRes : IExtensible
	{
		public static readonly short OP = 6639;

		private StoreGoodsInfo _goodsInfo;

		private StoreExtra _extra;

		private int _storeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "goodsInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public StoreGoodsInfo goodsInfo
		{
			get
			{
				return this._goodsInfo;
			}
			set
			{
				this._goodsInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "extra", DataFormat = DataFormat.Default), DefaultValue(null)]
		public StoreExtra extra
		{
			get
			{
				return this._extra;
			}
			set
			{
				this._extra = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "storeId", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
