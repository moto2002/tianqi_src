using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SingleGoods")]
	[Serializable]
	public class SingleGoods : IExtensible
	{
		[ProtoContract(Name = "GoodsInfo")]
		[Serializable]
		public class GoodsInfo : IExtensible
		{
			private int _cfgId;

			private int _count = 1;

			private int _firstType;

			private int _secondType;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
			public int cfgId
			{
				get
				{
					return this._cfgId;
				}
				set
				{
					this._cfgId = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
			public int count
			{
				get
				{
					return this._count;
				}
				set
				{
					this._count = value;
				}
			}

			[ProtoMember(3, IsRequired = false, Name = "firstType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int firstType
			{
				get
				{
					return this._firstType;
				}
				set
				{
					this._firstType = value;
				}
			}

			[ProtoMember(4, IsRequired = false, Name = "secondType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int secondType
			{
				get
				{
					return this._secondType;
				}
				set
				{
					this._secondType = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "PriceInfo")]
		[Serializable]
		public class PriceInfo : IExtensible
		{
			private int _type;

			private int _price;

			private int _originalCost = -1;

			private string _desc = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

			[ProtoMember(2, IsRequired = true, Name = "price", DataFormat = DataFormat.TwosComplement)]
			public int price
			{
				get
				{
					return this._price;
				}
				set
				{
					this._price = value;
				}
			}

			[ProtoMember(3, IsRequired = false, Name = "originalCost", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
			public int originalCost
			{
				get
				{
					return this._originalCost;
				}
				set
				{
					this._originalCost = value;
				}
			}

			[ProtoMember(4, IsRequired = false, Name = "desc", DataFormat = DataFormat.Default), DefaultValue("")]
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

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private long _goodsNumber;

		private SingleGoods.GoodsInfo _goodsInfo;

		private SingleGoods.PriceInfo _priceInfo;

		private int _limitBuyCount = 1;

		private int _limitSecond = 86400;

		private int _beginUtc;

		private int _endUtc;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "goodsNumber", DataFormat = DataFormat.TwosComplement)]
		public long goodsNumber
		{
			get
			{
				return this._goodsNumber;
			}
			set
			{
				this._goodsNumber = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "goodsInfo", DataFormat = DataFormat.Default)]
		public SingleGoods.GoodsInfo goodsInfo
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

		[ProtoMember(3, IsRequired = true, Name = "priceInfo", DataFormat = DataFormat.Default)]
		public SingleGoods.PriceInfo priceInfo
		{
			get
			{
				return this._priceInfo;
			}
			set
			{
				this._priceInfo = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "limitBuyCount", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int limitBuyCount
		{
			get
			{
				return this._limitBuyCount;
			}
			set
			{
				this._limitBuyCount = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "limitSecond", DataFormat = DataFormat.TwosComplement), DefaultValue(86400)]
		public int limitSecond
		{
			get
			{
				return this._limitSecond;
			}
			set
			{
				this._limitSecond = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "beginUtc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beginUtc
		{
			get
			{
				return this._beginUtc;
			}
			set
			{
				this._beginUtc = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "endUtc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endUtc
		{
			get
			{
				return this._endUtc;
			}
			set
			{
				this._endUtc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
