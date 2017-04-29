using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "StoreGoodsInfo")]
	[Serializable]
	public class StoreGoodsInfo : IExtensible
	{
		private int _iId;

		private int _itemId;

		private readonly List<int> _unitPrice = new List<int>();

		private int _moneyType = 1;

		private int _buyTimes;

		private GoodsExtra _extraInfo;

		private int _stockCfg = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "iId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iId
		{
			get
			{
				return this._iId;
			}
			set
			{
				this._iId = value;
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

		[ProtoMember(3, Name = "unitPrice", DataFormat = DataFormat.TwosComplement)]
		public List<int> unitPrice
		{
			get
			{
				return this._unitPrice;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
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

		[ProtoMember(5, IsRequired = false, Name = "buyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyTimes
		{
			get
			{
				return this._buyTimes;
			}
			set
			{
				this._buyTimes = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "extraInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GoodsExtra extraInfo
		{
			get
			{
				return this._extraInfo;
			}
			set
			{
				this._extraInfo = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "stockCfg", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int stockCfg
		{
			get
			{
				return this._stockCfg;
			}
			set
			{
				this._stockCfg = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
