using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EveryDayInfo")]
	[Serializable]
	public class EveryDayInfo : IExtensible
	{
		public static readonly short OP = 512;

		private int _loginDays;

		private ItemInfo1 _rewardItem;

		private int _status;

		private int _iconId;

		private int _chineseId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "loginDays", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loginDays
		{
			get
			{
				return this._loginDays;
			}
			set
			{
				this._loginDays = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "rewardItem", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ItemInfo1 rewardItem
		{
			get
			{
				return this._rewardItem;
			}
			set
			{
				this._rewardItem = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "iconId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iconId
		{
			get
			{
				return this._iconId;
			}
			set
			{
				this._iconId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "chineseId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chineseId
		{
			get
			{
				return this._chineseId;
			}
			set
			{
				this._chineseId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
