using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3765), ForSend(3765), ProtoContract(Name = "GuildShopPush")]
	[Serializable]
	public class GuildShopPush : IExtensible
	{
		public static readonly short OP = 3765;

		private GuildShopExtraData _data;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "data", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildShopExtraData data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
