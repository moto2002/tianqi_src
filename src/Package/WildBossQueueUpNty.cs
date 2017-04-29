using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(58), ForSend(58), ProtoContract(Name = "WildBossQueueUpNty")]
	[Serializable]
	public class WildBossQueueUpNty : IExtensible
	{
		[ProtoContract(Name = "QueueDetailInfo")]
		[Serializable]
		public class QueueDetailInfo : IExtensible
		{
			private long _bossHp;

			private long _bossHpLmt;

			private readonly List<WildBossRoleShowInfo> _challengingRoles = new List<WildBossRoleShowInfo>();

			private readonly List<WildBossRoleShowInfo> _queueRoles = new List<WildBossRoleShowInfo>();

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "bossHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long bossHp
			{
				get
				{
					return this._bossHp;
				}
				set
				{
					this._bossHp = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "bossHpLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long bossHpLmt
			{
				get
				{
					return this._bossHpLmt;
				}
				set
				{
					this._bossHpLmt = value;
				}
			}

			[ProtoMember(3, Name = "challengingRoles", DataFormat = DataFormat.Default)]
			public List<WildBossRoleShowInfo> challengingRoles
			{
				get
				{
					return this._challengingRoles;
				}
			}

			[ProtoMember(4, Name = "queueRoles", DataFormat = DataFormat.Default)]
			public List<WildBossRoleShowInfo> queueRoles
			{
				get
				{
					return this._queueRoles;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 58;

		private int _bossCode;

		private int _queueCount;

		private int _bossLv;

		private bool _teamBoss;

		private int _idx;

		private WildBossQueueUpNty.QueueDetailInfo _detail;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "bossCode", DataFormat = DataFormat.TwosComplement)]
		public int bossCode
		{
			get
			{
				return this._bossCode;
			}
			set
			{
				this._bossCode = value;
			}
		}

		[ProtoMember(1, IsRequired = true, Name = "queueCount", DataFormat = DataFormat.TwosComplement)]
		public int queueCount
		{
			get
			{
				return this._queueCount;
			}
			set
			{
				this._queueCount = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "bossLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossLv
		{
			get
			{
				return this._bossLv;
			}
			set
			{
				this._bossLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "teamBoss", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool teamBoss
		{
			get
			{
				return this._teamBoss;
			}
			set
			{
				this._teamBoss = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "idx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "detail", DataFormat = DataFormat.Default), DefaultValue(null)]
		public WildBossQueueUpNty.QueueDetailInfo detail
		{
			get
			{
				return this._detail;
			}
			set
			{
				this._detail = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
