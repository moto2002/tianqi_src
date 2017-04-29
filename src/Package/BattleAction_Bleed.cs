using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_Bleed")]
	[Serializable]
	public class BattleAction_Bleed : IExtensible
	{
		[ProtoContract(Name = "DmgSrcType")]
		public enum DmgSrcType
		{
			[ProtoEnum(Name = "Attack", Value = 0)]
			Attack,
			[ProtoEnum(Name = "Buff", Value = 1)]
			Buff,
			[ProtoEnum(Name = "Elem", Value = 2)]
			Elem
		}

		private long _dmgSrcSoldierId;

		private GameObjectType.ENUM _dmgSrcWrapType;

		private long _bleedSoldierId;

		private GameObjectType.ENUM _bleedSoldierWrapType;

		private BattleAction_Bleed.DmgSrcType _dmgSrcType;

		private ElemType.ENUM _elemType;

		private bool _isCrt;

		private bool _isParry;

		private bool _isMiss;

		private long _bleedHp;

		private long _hp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dmgSrcSoldierId", DataFormat = DataFormat.TwosComplement)]
		public long dmgSrcSoldierId
		{
			get
			{
				return this._dmgSrcSoldierId;
			}
			set
			{
				this._dmgSrcSoldierId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "dmgSrcWrapType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM dmgSrcWrapType
		{
			get
			{
				return this._dmgSrcWrapType;
			}
			set
			{
				this._dmgSrcWrapType = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "bleedSoldierId", DataFormat = DataFormat.TwosComplement)]
		public long bleedSoldierId
		{
			get
			{
				return this._bleedSoldierId;
			}
			set
			{
				this._bleedSoldierId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "bleedSoldierWrapType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM bleedSoldierWrapType
		{
			get
			{
				return this._bleedSoldierWrapType;
			}
			set
			{
				this._bleedSoldierWrapType = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "dmgSrcType", DataFormat = DataFormat.TwosComplement)]
		public BattleAction_Bleed.DmgSrcType dmgSrcType
		{
			get
			{
				return this._dmgSrcType;
			}
			set
			{
				this._dmgSrcType = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "elemType", DataFormat = DataFormat.TwosComplement)]
		public ElemType.ENUM elemType
		{
			get
			{
				return this._elemType;
			}
			set
			{
				this._elemType = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "isCrt", DataFormat = DataFormat.Default)]
		public bool isCrt
		{
			get
			{
				return this._isCrt;
			}
			set
			{
				this._isCrt = value;
			}
		}

		[ProtoMember(13, IsRequired = true, Name = "isParry", DataFormat = DataFormat.Default)]
		public bool isParry
		{
			get
			{
				return this._isParry;
			}
			set
			{
				this._isParry = value;
			}
		}

		[ProtoMember(14, IsRequired = true, Name = "isMiss", DataFormat = DataFormat.Default)]
		public bool isMiss
		{
			get
			{
				return this._isMiss;
			}
			set
			{
				this._isMiss = value;
			}
		}

		[ProtoMember(15, IsRequired = true, Name = "bleedHp", DataFormat = DataFormat.TwosComplement)]
		public long bleedHp
		{
			get
			{
				return this._bleedHp;
			}
			set
			{
				this._bleedHp = value;
			}
		}

		[ProtoMember(16, IsRequired = true, Name = "hp", DataFormat = DataFormat.TwosComplement)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
