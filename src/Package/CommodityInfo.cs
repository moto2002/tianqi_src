using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "CommodityInfo")]
	[Serializable]
	public class CommodityInfo : IExtensible
	{
		private int _commodityId;

		private int _itemId;

		private int _itemNum;

		private int _unitPrice;

		private int _moneyType;

		private bool _sell;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "itemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemNum
		{
			get
			{
				return this._itemNum;
			}
			set
			{
				this._itemNum = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "unitPrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int unitPrice
		{
			get
			{
				return this._unitPrice;
			}
			set
			{
				this._unitPrice = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moneyType
		{
			get
			{
				return this._moneyType;
			}
			set
			{
				this._moneyType = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "sell", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool sell
		{
			get
			{
				return this._sell;
			}
			set
			{
				this._sell = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
