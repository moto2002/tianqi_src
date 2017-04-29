using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(115), ForSend(115), ProtoContract(Name = "PushFundInfo")]
	[Serializable]
	public class PushFundInfo : IExtensible
	{
		public static readonly short OP = 115;

		private readonly List<RewardInfo> _items = new List<RewardInfo>();

		private bool _hasBuy;

		private bool _hasGet;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RewardInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "hasBuy", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasBuy
		{
			get
			{
				return this._hasBuy;
			}
			set
			{
				this._hasBuy = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hasGet", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasGet
		{
			get
			{
				return this._hasGet;
			}
			set
			{
				this._hasGet = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
