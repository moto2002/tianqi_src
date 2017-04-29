using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "AwardInfo")]
	[Serializable]
	public class AwardInfo : IExtensible
	{
		private int _itemId;

		private int _itemCount;

		private ItemFirstType.IFT _itemFirstType;

		private bool _hadFlag;

		private int _petCfgId;

		private int _petOldStar;

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

		[ProtoMember(2, IsRequired = false, Name = "itemCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemCount
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

		[ProtoMember(3, IsRequired = true, Name = "itemFirstType", DataFormat = DataFormat.TwosComplement)]
		public ItemFirstType.IFT itemFirstType
		{
			get
			{
				return this._itemFirstType;
			}
			set
			{
				this._itemFirstType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "hadFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hadFlag
		{
			get
			{
				return this._hadFlag;
			}
			set
			{
				this._hadFlag = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "petCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petCfgId
		{
			get
			{
				return this._petCfgId;
			}
			set
			{
				this._petCfgId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "petOldStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petOldStar
		{
			get
			{
				return this._petOldStar;
			}
			set
			{
				this._petOldStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
