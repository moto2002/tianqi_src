using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "NpcExchangeInfo")]
	[Serializable]
	public class NpcExchangeInfo : IExtensible
	{
		private int _cfgId;

		private int _count;

		private int _itemType;

		private EquipParamInfo _equipParams;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "cfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "itemType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				this._itemType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "equipParams", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EquipParamInfo equipParams
		{
			get
			{
				return this._equipParams;
			}
			set
			{
				this._equipParams = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
