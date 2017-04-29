using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(992), ForSend(992), ProtoContract(Name = "DefendFightBtlResultNty")]
	[Serializable]
	public class DefendFightBtlResultNty : IExtensible
	{
		[ProtoContract(Name = "Reason")]
		[Serializable]
		public class Reason : IExtensible
		{
			[ProtoContract(Name = "RS")]
			public enum RS
			{
				[ProtoEnum(Name = "Win", Value = 0)]
				Win,
				[ProtoEnum(Name = "AllRoleDead", Value = 1)]
				AllRoleDead,
				[ProtoEnum(Name = "ProtectedNpcDead", Value = 2)]
				ProtectedNpcDead,
				[ProtoEnum(Name = "BattleTimeout", Value = 3)]
				BattleTimeout,
				[ProtoEnum(Name = "NotReachArea", Value = 4)]
				NotReachArea,
				[ProtoEnum(Name = "Logout", Value = 5)]
				Logout,
				[ProtoEnum(Name = "UnKnown", Value = 100)]
				UnKnown = 100
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 992;

		private ChallengeResult _result;

		private readonly List<ItemBriefInfo> _normalDropItems = new List<ItemBriefInfo>();

		private readonly List<ItemBriefInfo> _extendDropItems = new List<ItemBriefInfo>();

		private DefendFightBtlResultNty.Reason.RS _loseReason;

		private int _bossRandomRate;

		private int _maxWave;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChallengeResult result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(2, Name = "normalDropItems", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> normalDropItems
		{
			get
			{
				return this._normalDropItems;
			}
		}

		[ProtoMember(3, Name = "extendDropItems", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> extendDropItems
		{
			get
			{
				return this._extendDropItems;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "loseReason", DataFormat = DataFormat.TwosComplement)]
		public DefendFightBtlResultNty.Reason.RS loseReason
		{
			get
			{
				return this._loseReason;
			}
			set
			{
				this._loseReason = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "bossRandomRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossRandomRate
		{
			get
			{
				return this._bossRandomRate;
			}
			set
			{
				this._bossRandomRate = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "maxWave", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxWave
		{
			get
			{
				return this._maxWave;
			}
			set
			{
				this._maxWave = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
