using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SShangChengLeiXing")]
	[Serializable]
	public class SShangChengLeiXing : IExtensible
	{
		private int _shopId;

		private int _title;

		private int _goodsPool;

		private int _stock;

		private readonly List<int> _stockRefreshDate = new List<int>();

		private string _stockRefreshTime = string.Empty;

		private int _limit;

		private readonly List<string> _limitRefreshTime = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "shopId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "goodsPool", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsPool
		{
			get
			{
				return this._goodsPool;
			}
			set
			{
				this._goodsPool = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "stock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stock
		{
			get
			{
				return this._stock;
			}
			set
			{
				this._stock = value;
			}
		}

		[ProtoMember(6, Name = "stockRefreshDate", DataFormat = DataFormat.TwosComplement)]
		public List<int> stockRefreshDate
		{
			get
			{
				return this._stockRefreshDate;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "stockRefreshTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string stockRefreshTime
		{
			get
			{
				return this._stockRefreshTime;
			}
			set
			{
				this._stockRefreshTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "limit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int limit
		{
			get
			{
				return this._limit;
			}
			set
			{
				this._limit = value;
			}
		}

		[ProtoMember(9, Name = "limitRefreshTime", DataFormat = DataFormat.Default)]
		public List<string> limitRefreshTime
		{
			get
			{
				return this._limitRefreshTime;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
