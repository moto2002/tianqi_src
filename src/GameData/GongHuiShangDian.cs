using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangDian")]
	[Serializable]
	public class GongHuiShangDian : IExtensible
	{
		private int _lv;

		private int _title;

		private int _heading;

		private int _moneyType;

		private readonly List<string> _RefreshTime = new List<string>();

		private int _initiativeRefresh;

		private readonly List<int> _commodityPool = new List<int>();

		private readonly List<int> _commodityQuantity = new List<int>();

		private int _refreshCostType;

		private int _refreshTime;

		private int _openLevel;

		private string _RefreshPrice = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
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

		[ProtoMember(4, IsRequired = false, Name = "heading", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int heading
		{
			get
			{
				return this._heading;
			}
			set
			{
				this._heading = value;
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

		[ProtoMember(6, Name = "RefreshTime", DataFormat = DataFormat.Default)]
		public List<string> RefreshTime
		{
			get
			{
				return this._RefreshTime;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "initiativeRefresh", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int initiativeRefresh
		{
			get
			{
				return this._initiativeRefresh;
			}
			set
			{
				this._initiativeRefresh = value;
			}
		}

		[ProtoMember(8, Name = "commodityPool", DataFormat = DataFormat.TwosComplement)]
		public List<int> commodityPool
		{
			get
			{
				return this._commodityPool;
			}
		}

		[ProtoMember(9, Name = "commodityQuantity", DataFormat = DataFormat.TwosComplement)]
		public List<int> commodityQuantity
		{
			get
			{
				return this._commodityQuantity;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "refreshCostType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshCostType
		{
			get
			{
				return this._refreshCostType;
			}
			set
			{
				this._refreshCostType = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this._refreshTime = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "openLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openLevel
		{
			get
			{
				return this._openLevel;
			}
			set
			{
				this._openLevel = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "RefreshPrice", DataFormat = DataFormat.Default), DefaultValue("")]
		public string RefreshPrice
		{
			get
			{
				return this._RefreshPrice;
			}
			set
			{
				this._RefreshPrice = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
