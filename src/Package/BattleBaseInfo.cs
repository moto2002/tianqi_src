using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleBaseInfo")]
	[Serializable]
	public class BattleBaseInfo : IExtensible
	{
		private GameObjectType.ENUM _wrapType;

		private int _camp;

		private bool _clientDrive;

		private PublicBaseInfo _publicBaseInfo;

		private BattleBaseAttr _battleBaseAttr;

		private int _ownedListIdx;

		private readonly List<long> _ownedIds = new List<long>();

		private readonly List<BattleSkillInfo> _skills = new List<BattleSkillInfo>();

		private readonly List<BattleSkillExtend> _skillExs = new List<BattleSkillExtend>();

		private long _finalOwnerId;

		private bool _isLoading;

		private bool _isFit;

		private bool _isInFit;

		private bool _isFighting = true;

		private bool _isFixed;

		private bool _isStatic;

		private bool _isTaunt;

		private bool _isSuperArmor;

		private bool _isIgnoreDmgFormula;

		private bool _isCloseRenderer;

		private bool _isStun;

		private bool _isMoveCast;

		private bool _isAssaulting;

		private bool _isKnocking;

		private bool _isSuspended;

		private bool _isSkillManaging;

		private bool _isSkillPressing;

		private bool _isBorning;

		private bool _isBoss;

		private int _pressingSkillId;

		private int _reliveTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "wrapType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM wrapType
		{
			get
			{
				return this._wrapType;
			}
			set
			{
				this._wrapType = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "camp", DataFormat = DataFormat.TwosComplement)]
		public int camp
		{
			get
			{
				return this._camp;
			}
			set
			{
				this._camp = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "clientDrive", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool clientDrive
		{
			get
			{
				return this._clientDrive;
			}
			set
			{
				this._clientDrive = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "publicBaseInfo", DataFormat = DataFormat.Default)]
		public PublicBaseInfo publicBaseInfo
		{
			get
			{
				return this._publicBaseInfo;
			}
			set
			{
				this._publicBaseInfo = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "battleBaseAttr", DataFormat = DataFormat.Default)]
		public BattleBaseAttr battleBaseAttr
		{
			get
			{
				return this._battleBaseAttr;
			}
			set
			{
				this._battleBaseAttr = value;
			}
		}

		[ProtoMember(102, IsRequired = false, Name = "ownedListIdx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownedListIdx
		{
			get
			{
				return this._ownedListIdx;
			}
			set
			{
				this._ownedListIdx = value;
			}
		}

		[ProtoMember(103, Name = "ownedIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> ownedIds
		{
			get
			{
				return this._ownedIds;
			}
		}

		[ProtoMember(120, Name = "skills", DataFormat = DataFormat.Default)]
		public List<BattleSkillInfo> skills
		{
			get
			{
				return this._skills;
			}
		}

		[ProtoMember(121, Name = "skillExs", DataFormat = DataFormat.Default)]
		public List<BattleSkillExtend> skillExs
		{
			get
			{
				return this._skillExs;
			}
		}

		[ProtoMember(130, IsRequired = false, Name = "finalOwnerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long finalOwnerId
		{
			get
			{
				return this._finalOwnerId;
			}
			set
			{
				this._finalOwnerId = value;
			}
		}

		[ProtoMember(200, IsRequired = false, Name = "isLoading", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isLoading
		{
			get
			{
				return this._isLoading;
			}
			set
			{
				this._isLoading = value;
			}
		}

		[ProtoMember(201, IsRequired = false, Name = "isFit", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isFit
		{
			get
			{
				return this._isFit;
			}
			set
			{
				this._isFit = value;
			}
		}

		[ProtoMember(202, IsRequired = false, Name = "isInFit", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isInFit
		{
			get
			{
				return this._isInFit;
			}
			set
			{
				this._isInFit = value;
			}
		}

		[ProtoMember(203, IsRequired = false, Name = "isFighting", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool isFighting
		{
			get
			{
				return this._isFighting;
			}
			set
			{
				this._isFighting = value;
			}
		}

		[ProtoMember(204, IsRequired = false, Name = "isFixed", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isFixed
		{
			get
			{
				return this._isFixed;
			}
			set
			{
				this._isFixed = value;
			}
		}

		[ProtoMember(205, IsRequired = false, Name = "isStatic", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isStatic
		{
			get
			{
				return this._isStatic;
			}
			set
			{
				this._isStatic = value;
			}
		}

		[ProtoMember(206, IsRequired = false, Name = "isTaunt", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isTaunt
		{
			get
			{
				return this._isTaunt;
			}
			set
			{
				this._isTaunt = value;
			}
		}

		[ProtoMember(207, IsRequired = false, Name = "isSuperArmor", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSuperArmor
		{
			get
			{
				return this._isSuperArmor;
			}
			set
			{
				this._isSuperArmor = value;
			}
		}

		[ProtoMember(208, IsRequired = false, Name = "isIgnoreDmgFormula", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isIgnoreDmgFormula
		{
			get
			{
				return this._isIgnoreDmgFormula;
			}
			set
			{
				this._isIgnoreDmgFormula = value;
			}
		}

		[ProtoMember(209, IsRequired = false, Name = "isCloseRenderer", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isCloseRenderer
		{
			get
			{
				return this._isCloseRenderer;
			}
			set
			{
				this._isCloseRenderer = value;
			}
		}

		[ProtoMember(210, IsRequired = false, Name = "isStun", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isStun
		{
			get
			{
				return this._isStun;
			}
			set
			{
				this._isStun = value;
			}
		}

		[ProtoMember(211, IsRequired = false, Name = "isMoveCast", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isMoveCast
		{
			get
			{
				return this._isMoveCast;
			}
			set
			{
				this._isMoveCast = value;
			}
		}

		[ProtoMember(212, IsRequired = false, Name = "isAssaulting", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isAssaulting
		{
			get
			{
				return this._isAssaulting;
			}
			set
			{
				this._isAssaulting = value;
			}
		}

		[ProtoMember(213, IsRequired = false, Name = "isKnocking", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isKnocking
		{
			get
			{
				return this._isKnocking;
			}
			set
			{
				this._isKnocking = value;
			}
		}

		[ProtoMember(214, IsRequired = false, Name = "isSuspended", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSuspended
		{
			get
			{
				return this._isSuspended;
			}
			set
			{
				this._isSuspended = value;
			}
		}

		[ProtoMember(215, IsRequired = false, Name = "isSkillManaging", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSkillManaging
		{
			get
			{
				return this._isSkillManaging;
			}
			set
			{
				this._isSkillManaging = value;
			}
		}

		[ProtoMember(216, IsRequired = false, Name = "isSkillPressing", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSkillPressing
		{
			get
			{
				return this._isSkillPressing;
			}
			set
			{
				this._isSkillPressing = value;
			}
		}

		[ProtoMember(217, IsRequired = false, Name = "isBorning", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isBorning
		{
			get
			{
				return this._isBorning;
			}
			set
			{
				this._isBorning = value;
			}
		}

		[ProtoMember(250, IsRequired = false, Name = "isBoss", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isBoss
		{
			get
			{
				return this._isBoss;
			}
			set
			{
				this._isBoss = value;
			}
		}

		[ProtoMember(300, IsRequired = false, Name = "pressingSkillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pressingSkillId
		{
			get
			{
				return this._pressingSkillId;
			}
			set
			{
				this._pressingSkillId = value;
			}
		}

		[ProtoMember(301, IsRequired = false, Name = "reliveTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reliveTimes
		{
			get
			{
				return this._reliveTimes;
			}
			set
			{
				this._reliveTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
