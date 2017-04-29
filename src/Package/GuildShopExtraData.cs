using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildShopExtraData")]
	[Serializable]
	public class GuildShopExtraData : IExtensible
	{
		private bool _refreshFlag = true;

		private int _refreshLimit;

		private int _useRefreshTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "refreshFlag", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool refreshFlag
		{
			get
			{
				return this._refreshFlag;
			}
			set
			{
				this._refreshFlag = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "refreshLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshLimit
		{
			get
			{
				return this._refreshLimit;
			}
			set
			{
				this._refreshLimit = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "useRefreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int useRefreshTime
		{
			get
			{
				return this._useRefreshTime;
			}
			set
			{
				this._useRefreshTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
