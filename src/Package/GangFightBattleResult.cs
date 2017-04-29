using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(903), ForSend(903), ProtoContract(Name = "GangFightBattleResult")]
	[Serializable]
	public class GangFightBattleResult : IExtensible
	{
		public static readonly short OP = 903;

		private long _toRoleId;

		private string _toName;

		private int _toLv;

		private int _toLastCombatWinCount;

		private int _toCurrCombatWinCount;

		private int _fromLastCombatWinCount;

		private int _fromCurrCombatWinCount;

		private long _winnerId;

		private int _elapseSec;

		private readonly List<ItemBriefInfo> _reward = new List<ItemBriefInfo>();

		private readonly List<ItemBriefInfo> _rewardExt = new List<ItemBriefInfo>();

		private bool _forceExist;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "toRoleId", DataFormat = DataFormat.TwosComplement)]
		public long toRoleId
		{
			get
			{
				return this._toRoleId;
			}
			set
			{
				this._toRoleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "toName", DataFormat = DataFormat.Default)]
		public string toName
		{
			get
			{
				return this._toName;
			}
			set
			{
				this._toName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "toLv", DataFormat = DataFormat.TwosComplement)]
		public int toLv
		{
			get
			{
				return this._toLv;
			}
			set
			{
				this._toLv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "toLastCombatWinCount", DataFormat = DataFormat.TwosComplement)]
		public int toLastCombatWinCount
		{
			get
			{
				return this._toLastCombatWinCount;
			}
			set
			{
				this._toLastCombatWinCount = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "toCurrCombatWinCount", DataFormat = DataFormat.TwosComplement)]
		public int toCurrCombatWinCount
		{
			get
			{
				return this._toCurrCombatWinCount;
			}
			set
			{
				this._toCurrCombatWinCount = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "fromLastCombatWinCount", DataFormat = DataFormat.TwosComplement)]
		public int fromLastCombatWinCount
		{
			get
			{
				return this._fromLastCombatWinCount;
			}
			set
			{
				this._fromLastCombatWinCount = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "fromCurrCombatWinCount", DataFormat = DataFormat.TwosComplement)]
		public int fromCurrCombatWinCount
		{
			get
			{
				return this._fromCurrCombatWinCount;
			}
			set
			{
				this._fromCurrCombatWinCount = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(9, IsRequired = true, Name = "elapseSec", DataFormat = DataFormat.TwosComplement)]
		public int elapseSec
		{
			get
			{
				return this._elapseSec;
			}
			set
			{
				this._elapseSec = value;
			}
		}

		[ProtoMember(10, Name = "reward", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> reward
		{
			get
			{
				return this._reward;
			}
		}

		[ProtoMember(11, Name = "rewardExt", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewardExt
		{
			get
			{
				return this._rewardExt;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "forceExist", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool forceExist
		{
			get
			{
				return this._forceExist;
			}
			set
			{
				this._forceExist = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
