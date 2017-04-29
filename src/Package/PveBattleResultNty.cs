using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(815), ForSend(815), ProtoContract(Name = "PveBattleResultNty")]
	[Serializable]
	public class PveBattleResultNty : IExtensible
	{
		[ProtoContract(Name = "ItemInfo")]
		[Serializable]
		public class ItemInfo : IExtensible
		{
			private int _itemId;

			private long _itemCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
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

			[ProtoMember(2, IsRequired = true, Name = "itemCount", DataFormat = DataFormat.TwosComplement)]
			public long itemCount
			{
				get
				{
					return this._itemCount;
				}
				set
				{
					this._itemCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 815;

		private ChallengeResult _result;

		private readonly List<ItemBriefInfo> _normalPrize = new List<ItemBriefInfo>();

		private readonly List<PveBattleResultNty.ItemInfo> _tacitPrize = new List<PveBattleResultNty.ItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChallengeResult result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(2, Name = "normalPrize", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> normalPrize
		{
			get
			{
				return this._normalPrize;
			}
		}

		[ProtoMember(3, Name = "tacitPrize", DataFormat = DataFormat.Default)]
		public List<PveBattleResultNty.ItemInfo> tacitPrize
		{
			get
			{
				return this._tacitPrize;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
