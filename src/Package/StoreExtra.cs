using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "StoreExtra")]
	[Serializable]
	public class StoreExtra : IExtensible
	{
		private readonly List<int> _refreshData = new List<int>();

		private string _stockRefreshTime = string.Empty;

		private readonly List<string> _LmtRefreshTime = new List<string>();

		private int _buyTimes = -1;

		private int _vipLmtTimes = -1;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "refreshData", DataFormat = DataFormat.TwosComplement)]
		public List<int> refreshData
		{
			get
			{
				return this._refreshData;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "stockRefreshTime", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, Name = "LmtRefreshTime", DataFormat = DataFormat.Default)]
		public List<string> LmtRefreshTime
		{
			get
			{
				return this._LmtRefreshTime;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "buyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		[ProtoMember(5, IsRequired = false, Name = "vipLmtTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int vipLmtTimes
		{
			get
			{
				return this._vipLmtTimes;
			}
			set
			{
				this._vipLmtTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
