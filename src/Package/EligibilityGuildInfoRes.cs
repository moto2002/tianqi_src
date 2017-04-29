using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(95), ForSend(95), ProtoContract(Name = "EligibilityGuildInfoRes")]
	[Serializable]
	public class EligibilityGuildInfoRes : IExtensible
	{
		public static readonly short OP = 95;

		private readonly List<GuildParticipantInfo> _guildInfo = new List<GuildParticipantInfo>();

		private GuildParticipantInfo _ownerGuildInfo;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildInfo", DataFormat = DataFormat.Default)]
		public List<GuildParticipantInfo> guildInfo
		{
			get
			{
				return this._guildInfo;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "ownerGuildInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildParticipantInfo ownerGuildInfo
		{
			get
			{
				return this._ownerGuildInfo;
			}
			set
			{
				this._ownerGuildInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
