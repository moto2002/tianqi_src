using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1515), ForSend(1515), ProtoContract(Name = "GuildMatchResultNty")]
	[Serializable]
	public class GuildMatchResultNty : IExtensible
	{
		[ProtoContract(Name = "GuildInfo")]
		[Serializable]
		public class GuildInfo : IExtensible
		{
			private long _guildId;

			private string _guildName = string.Empty;

			private int _teamNum;

			private long _totalFighting;

			private long _totalResource;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this._guildId = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "guildName", DataFormat = DataFormat.Default), DefaultValue("")]
			public string guildName
			{
				get
				{
					return this._guildName;
				}
				set
				{
					this._guildName = value;
				}
			}

			[ProtoMember(3, IsRequired = false, Name = "teamNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int teamNum
			{
				get
				{
					return this._teamNum;
				}
				set
				{
					this._teamNum = value;
				}
			}

			[ProtoMember(4, IsRequired = false, Name = "totalFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long totalFighting
			{
				get
				{
					return this._totalFighting;
				}
				set
				{
					this._totalFighting = value;
				}
			}

			[ProtoMember(5, IsRequired = false, Name = "totalResource", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long totalResource
			{
				get
				{
					return this._totalResource;
				}
				set
				{
					this._totalResource = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 1515;

		private readonly List<GuildMatchResultNty.GuildInfo> _guildInfos = new List<GuildMatchResultNty.GuildInfo>();

		private long _winnerId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildInfos", DataFormat = DataFormat.Default)]
		public List<GuildMatchResultNty.GuildInfo> guildInfos
		{
			get
			{
				return this._guildInfos;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
		public long winnerId
		{
			get
			{
				return this._winnerId;
			}
			set
			{
				this._winnerId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
