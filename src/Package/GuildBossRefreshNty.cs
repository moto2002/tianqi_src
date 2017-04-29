using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(75), ForSend(75), ProtoContract(Name = "GuildBossRefreshNty")]
	[Serializable]
	public class GuildBossRefreshNty : IExtensible
	{
		public static readonly short OP = 75;

		private bool _guildBossStatus;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "guildBossStatus", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool guildBossStatus
		{
			get
			{
				return this._guildBossStatus;
			}
			set
			{
				this._guildBossStatus = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
