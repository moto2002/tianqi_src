using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3863), ForSend(3863), ProtoContract(Name = "GuildStorageExchangeReq")]
	[Serializable]
	public class GuildStorageExchangeReq : IExtensible
	{
		public static readonly short OP = 3863;

		private ItemBriefInfo _item;

		private EquipBriefInfo _equip;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "item", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ItemBriefInfo item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "equip", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EquipBriefInfo equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this._equip = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
