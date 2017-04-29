using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3801), ForSend(3801), ProtoContract(Name = "RoleEnteredFieldNty")]
	[Serializable]
	public class RoleEnteredFieldNty : IExtensible
	{
		public static readonly short OP = 3801;

		private BattleFieldT.BFT _type;

		private BattleFieldLogicType.BFLT _logicType;

		private readonly List<int> _buffIds = new List<int>();

		private long _cliDrvBattleRandSeed;

		private int _cliDrvBattleRandLen;

		private bool _cliDrvBattleIgnoreDmg;

		private readonly List<int> _preloadMonsterId = new List<int>();

		private readonly List<int> _preloadRoleTypeId = new List<int>();

		private readonly List<int> _preloadPetId = new List<int>();

		private int _dungeonId;

		private readonly List<int> _preloadRoleSkillId = new List<int>();

		private readonly List<int> _preloadPetSkillId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public BattleFieldT.BFT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "logicType", DataFormat = DataFormat.TwosComplement)]
		public BattleFieldLogicType.BFLT logicType
		{
			get
			{
				return this._logicType;
			}
			set
			{
				this._logicType = value;
			}
		}

		[ProtoMember(3, Name = "buffIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> buffIds
		{
			get
			{
				return this._buffIds;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "cliDrvBattleRandSeed", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cliDrvBattleRandSeed
		{
			get
			{
				return this._cliDrvBattleRandSeed;
			}
			set
			{
				this._cliDrvBattleRandSeed = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "cliDrvBattleRandLen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cliDrvBattleRandLen
		{
			get
			{
				return this._cliDrvBattleRandLen;
			}
			set
			{
				this._cliDrvBattleRandLen = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "cliDrvBattleIgnoreDmg", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool cliDrvBattleIgnoreDmg
		{
			get
			{
				return this._cliDrvBattleIgnoreDmg;
			}
			set
			{
				this._cliDrvBattleIgnoreDmg = value;
			}
		}

		[ProtoMember(6, Name = "preloadMonsterId", DataFormat = DataFormat.TwosComplement)]
		public List<int> preloadMonsterId
		{
			get
			{
				return this._preloadMonsterId;
			}
		}

		[ProtoMember(7, Name = "preloadRoleTypeId", DataFormat = DataFormat.TwosComplement)]
		public List<int> preloadRoleTypeId
		{
			get
			{
				return this._preloadRoleTypeId;
			}
		}

		[ProtoMember(8, Name = "preloadPetId", DataFormat = DataFormat.TwosComplement)]
		public List<int> preloadPetId
		{
			get
			{
				return this._preloadPetId;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(10, Name = "preloadRoleSkillId", DataFormat = DataFormat.TwosComplement)]
		public List<int> preloadRoleSkillId
		{
			get
			{
				return this._preloadRoleSkillId;
			}
		}

		[ProtoMember(11, Name = "preloadPetSkillId", DataFormat = DataFormat.TwosComplement)]
		public List<int> preloadPetSkillId
		{
			get
			{
				return this._preloadPetSkillId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
