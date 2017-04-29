using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(65), ForSend(65), ProtoContract(Name = "GuildBossInfoRes")]
	[Serializable]
	public class GuildBossInfoRes : IExtensible
	{
		public static readonly short OP = 65;

		private int _guildFund;

		private bool _challenging;

		private int _canKillBossCD;

		private int _willChallengeBossTimes;

		private int _canCallTimes;

		private GuildBossInfo _bossInfo;

		private readonly List<GuildBossHurtInfo> _hurtInfos = new List<GuildBossHurtInfo>();

		private long _fatal2BossRoleId;

		private int _rmCleanTimes;

		private int _openCD;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildFund", DataFormat = DataFormat.TwosComplement)]
		public int guildFund
		{
			get
			{
				return this._guildFund;
			}
			set
			{
				this._guildFund = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "challenging", DataFormat = DataFormat.Default)]
		public bool challenging
		{
			get
			{
				return this._challenging;
			}
			set
			{
				this._challenging = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "canKillBossCD", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canKillBossCD
		{
			get
			{
				return this._canKillBossCD;
			}
			set
			{
				this._canKillBossCD = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "willChallengeBossTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int willChallengeBossTimes
		{
			get
			{
				return this._willChallengeBossTimes;
			}
			set
			{
				this._willChallengeBossTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "canCallTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canCallTimes
		{
			get
			{
				return this._canCallTimes;
			}
			set
			{
				this._canCallTimes = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "bossInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildBossInfo bossInfo
		{
			get
			{
				return this._bossInfo;
			}
			set
			{
				this._bossInfo = value;
			}
		}

		[ProtoMember(7, Name = "hurtInfos", DataFormat = DataFormat.Default)]
		public List<GuildBossHurtInfo> hurtInfos
		{
			get
			{
				return this._hurtInfos;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "fatal2BossRoleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fatal2BossRoleId
		{
			get
			{
				return this._fatal2BossRoleId;
			}
			set
			{
				this._fatal2BossRoleId = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rmCleanTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rmCleanTimes
		{
			get
			{
				return this._rmCleanTimes;
			}
			set
			{
				this._rmCleanTimes = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "openCD", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openCD
		{
			get
			{
				return this._openCD;
			}
			set
			{
				this._openCD = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
