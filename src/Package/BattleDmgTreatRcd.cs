using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleDmgTreatRcd")]
	[Serializable]
	public class BattleDmgTreatRcd : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Dmg", Value = 0)]
			Dmg,
			[ProtoEnum(Name = "Treat", Value = 1)]
			Treat
		}

		private BattleDmgTreatRcd.ENUM _type;

		private bool _isActive;

		private long _ownerId;

		private long _targetId;

		private int _skillId;

		private int _fitPetTypeId;

		private long _val;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public BattleDmgTreatRcd.ENUM type
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

		[ProtoMember(2, IsRequired = true, Name = "isActive", DataFormat = DataFormat.Default)]
		public bool isActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				this._isActive = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "ownerId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "targetId", DataFormat = DataFormat.TwosComplement)]
		public long targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "fitPetTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fitPetTypeId
		{
			get
			{
				return this._fitPetTypeId;
			}
			set
			{
				this._fitPetTypeId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "val", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long val
		{
			get
			{
				return this._val;
			}
			set
			{
				this._val = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
