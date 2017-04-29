using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ChallengeResult")]
	[Serializable]
	public class ChallengeResult : IExtensible
	{
		[ProtoContract(Name = "SoldierSettleInfo")]
		[Serializable]
		public class SoldierSettleInfo : IExtensible
		{
			private GameObjectType.ENUM _wrapType = GameObjectType.ENUM.OtherType;

			private CampType.ENUM _camp;

			private bool _isBoss;

			private long _soldierId;

			private long _ownerId;

			private int _soldierTypeId;

			private string _soldierName = string.Empty;

			private int _totalDamage;

			private readonly List<SoldierFitDamage> _fitTotalDamages = new List<SoldierFitDamage>();

			private int _totalBleedHp;

			private readonly List<SoldierFitBleedHp> _fitTotalBleedHps = new List<SoldierFitBleedHp>();

			private BattleDmgTreatRcds _dmgTreatRcds;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "wrapType", DataFormat = DataFormat.TwosComplement), DefaultValue(GameObjectType.ENUM.OtherType)]
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

			[ProtoMember(3, IsRequired = false, Name = "camp", DataFormat = DataFormat.TwosComplement), DefaultValue(CampType.ENUM.Natural)]
			public CampType.ENUM camp
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

			[ProtoMember(2, IsRequired = false, Name = "isBoss", DataFormat = DataFormat.Default), DefaultValue(false)]
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

			[ProtoMember(4, IsRequired = false, Name = "soldierId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long soldierId
			{
				get
				{
					return this._soldierId;
				}
				set
				{
					this._soldierId = value;
				}
			}

			[ProtoMember(5, IsRequired = false, Name = "ownerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long ownerId
			{
				get
				{
					return this._ownerId;
				}
				set
				{
					this._ownerId = value;
				}
			}

			[ProtoMember(6, IsRequired = false, Name = "soldierTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int soldierTypeId
			{
				get
				{
					return this._soldierTypeId;
				}
				set
				{
					this._soldierTypeId = value;
				}
			}

			[ProtoMember(7, IsRequired = false, Name = "soldierName", DataFormat = DataFormat.Default), DefaultValue("")]
			public string soldierName
			{
				get
				{
					return this._soldierName;
				}
				set
				{
					this._soldierName = value;
				}
			}

			[ProtoMember(20, IsRequired = false, Name = "totalDamage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int totalDamage
			{
				get
				{
					return this._totalDamage;
				}
				set
				{
					this._totalDamage = value;
				}
			}

			[ProtoMember(21, Name = "fitTotalDamages", DataFormat = DataFormat.Default)]
			public List<SoldierFitDamage> fitTotalDamages
			{
				get
				{
					return this._fitTotalDamages;
				}
			}

			[ProtoMember(22, IsRequired = false, Name = "totalBleedHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int totalBleedHp
			{
				get
				{
					return this._totalBleedHp;
				}
				set
				{
					this._totalBleedHp = value;
				}
			}

			[ProtoMember(23, Name = "fitTotalBleedHps", DataFormat = DataFormat.Default)]
			public List<SoldierFitBleedHp> fitTotalBleedHps
			{
				get
				{
					return this._fitTotalBleedHps;
				}
			}

			[ProtoMember(30, IsRequired = false, Name = "dmgTreatRcds", DataFormat = DataFormat.Default), DefaultValue(null)]
			public BattleDmgTreatRcds dmgTreatRcds
			{
				get
				{
					return this._dmgTreatRcds;
				}
				set
				{
					this._dmgTreatRcds = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private long _winnerId;

		private uint _star;

		private readonly List<ChallengeResult.SoldierSettleInfo> _settleInfos = new List<ChallengeResult.SoldierSettleInfo>();

		private readonly List<uint> _condIdsPassed = new List<uint>();

		private int _killTargetUsedTime = 1000000;

		private int _treat;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public uint star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(3, Name = "settleInfos", DataFormat = DataFormat.Default)]
		public List<ChallengeResult.SoldierSettleInfo> settleInfos
		{
			get
			{
				return this._settleInfos;
			}
		}

		[ProtoMember(4, Name = "condIdsPassed", DataFormat = DataFormat.TwosComplement)]
		public List<uint> condIdsPassed
		{
			get
			{
				return this._condIdsPassed;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "killTargetUsedTime", DataFormat = DataFormat.TwosComplement), DefaultValue(1000000)]
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

		[ProtoMember(6, IsRequired = false, Name = "treat", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
