using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(712), ForSend(712), ProtoContract(Name = "SettleDungeonReq")]
	[Serializable]
	public class SettleDungeonReq : IExtensible
	{
		[ProtoContract(Name = "DeadMonster")]
		[Serializable]
		public class DeadMonster : IExtensible
		{
			private int _monsterId;

			private int _monsterCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "monsterId", DataFormat = DataFormat.TwosComplement)]
			public int monsterId
			{
				get
				{
					return this._monsterId;
				}
				set
				{
					this._monsterId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "monsterCount", DataFormat = DataFormat.TwosComplement)]
			public int monsterCount
			{
				get
				{
					return this._monsterCount;
				}
				set
				{
					this._monsterCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 712;

		private bool _selfDead = true;

		private bool _friendDead = true;

		private bool _targetDead;

		private bool _npcDead = true;

		private bool _npcArrivedDst;

		private bool _allPetNotDead;

		private int _minimumHpPcnt;

		private int _minimumHpPcntPet;

		private int _remainingHpPcnt;

		private int _killTargetUsedTime = 1000000;

		private readonly List<int> _summonedPetTypeIds = new List<int>();

		private readonly List<int> _fittedPetTypeIds = new List<int>();

		private int _treat;

		private bool _isWin;

		private int _elapseSec;

		private bool _anyNpcDead;

		private readonly List<SettleDungeonReq.DeadMonster> _deadMonster = new List<SettleDungeonReq.DeadMonster>();

		private int _tick;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "selfDead", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool selfDead
		{
			get
			{
				return this._selfDead;
			}
			set
			{
				this._selfDead = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "friendDead", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool friendDead
		{
			get
			{
				return this._friendDead;
			}
			set
			{
				this._friendDead = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "targetDead", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool targetDead
		{
			get
			{
				return this._targetDead;
			}
			set
			{
				this._targetDead = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "npcDead", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool npcDead
		{
			get
			{
				return this._npcDead;
			}
			set
			{
				this._npcDead = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "npcArrivedDst", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool npcArrivedDst
		{
			get
			{
				return this._npcArrivedDst;
			}
			set
			{
				this._npcArrivedDst = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "allPetNotDead", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool allPetNotDead
		{
			get
			{
				return this._allPetNotDead;
			}
			set
			{
				this._allPetNotDead = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "minimumHpPcnt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minimumHpPcnt
		{
			get
			{
				return this._minimumHpPcnt;
			}
			set
			{
				this._minimumHpPcnt = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "minimumHpPcntPet", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minimumHpPcntPet
		{
			get
			{
				return this._minimumHpPcntPet;
			}
			set
			{
				this._minimumHpPcntPet = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "remainingHpPcnt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainingHpPcnt
		{
			get
			{
				return this._remainingHpPcnt;
			}
			set
			{
				this._remainingHpPcnt = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "killTargetUsedTime", DataFormat = DataFormat.TwosComplement), DefaultValue(1000000)]
		public int killTargetUsedTime
		{
			get
			{
				return this._killTargetUsedTime;
			}
			set
			{
				this._killTargetUsedTime = value;
			}
		}

		[ProtoMember(11, Name = "summonedPetTypeIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> summonedPetTypeIds
		{
			get
			{
				return this._summonedPetTypeIds;
			}
		}

		[ProtoMember(12, Name = "fittedPetTypeIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> fittedPetTypeIds
		{
			get
			{
				return this._fittedPetTypeIds;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "treat", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int treat
		{
			get
			{
				return this._treat;
			}
			set
			{
				this._treat = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "isWin", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isWin
		{
			get
			{
				return this._isWin;
			}
			set
			{
				this._isWin = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "elapseSec", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "anyNpcDead", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool anyNpcDead
		{
			get
			{
				return this._anyNpcDead;
			}
			set
			{
				this._anyNpcDead = value;
			}
		}

		[ProtoMember(20, Name = "deadMonster", DataFormat = DataFormat.Default)]
		public List<SettleDungeonReq.DeadMonster> deadMonster
		{
			get
			{
				return this._deadMonster;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "tick", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tick
		{
			get
			{
				return this._tick;
			}
			set
			{
				this._tick = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
