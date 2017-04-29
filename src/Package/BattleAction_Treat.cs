using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_Treat")]
	[Serializable]
	public class BattleAction_Treat : IExtensible
	{
		[ProtoContract(Name = "TreatSrcType")]
		public enum TreatSrcType
		{
			[ProtoEnum(Name = "Treat", Value = 0)]
			Treat,
			[ProtoEnum(Name = "SuckBlood", Value = 1)]
			SuckBlood,
			[ProtoEnum(Name = "KillRecover", Value = 2)]
			KillRecover,
			[ProtoEnum(Name = "HpRestore", Value = 3)]
			HpRestore
		}

		private BattleAction_Treat.TreatSrcType _treatSrcType;

		private long _treatSrcSoldierId;

		private GameObjectType.ENUM _treatSrcWrapType;

		private long _beTreatedSoldierId;

		private GameObjectType.ENUM _beTreatedWrapType;

		private long _treatHp;

		private long _hp;

		private Pos _pos;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "treatSrcType", DataFormat = DataFormat.TwosComplement)]
		public BattleAction_Treat.TreatSrcType treatSrcType
		{
			get
			{
				return this._treatSrcType;
			}
			set
			{
				this._treatSrcType = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "treatSrcSoldierId", DataFormat = DataFormat.TwosComplement)]
		public long treatSrcSoldierId
		{
			get
			{
				return this._treatSrcSoldierId;
			}
			set
			{
				this._treatSrcSoldierId = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "treatSrcWrapType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM treatSrcWrapType
		{
			get
			{
				return this._treatSrcWrapType;
			}
			set
			{
				this._treatSrcWrapType = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "beTreatedSoldierId", DataFormat = DataFormat.TwosComplement)]
		public long beTreatedSoldierId
		{
			get
			{
				return this._beTreatedSoldierId;
			}
			set
			{
				this._beTreatedSoldierId = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "beTreatedWrapType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM beTreatedWrapType
		{
			get
			{
				return this._beTreatedWrapType;
			}
			set
			{
				this._beTreatedWrapType = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "treatHp", DataFormat = DataFormat.TwosComplement)]
		public long treatHp
		{
			get
			{
				return this._treatHp;
			}
			set
			{
				this._treatHp = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "hp", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(13, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
