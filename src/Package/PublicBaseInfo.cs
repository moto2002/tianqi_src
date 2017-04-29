using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PublicBaseInfo")]
	[Serializable]
	public class PublicBaseInfo : IExtensible
	{
		private SimpleBaseInfo _simpleInfo;

		private int _Atk;

		private int _Defence;

		private long _HpLmt;

		private int _PveAtk;

		private int _PvpAtk;

		private int _HitRatio;

		private int _DodgeRatio;

		private int _CritRatio;

		private int _DecritRatio;

		private int _CritHurtAddRatio;

		private int _ParryRatio;

		private int _DeparryRatio;

		private int _ParryHurtDeRatio;

		private int _SuckBloodScale;

		private int _HurtAddRatio;

		private int _HurtDeRatio;

		private int _PveHurtAddRatio;

		private int _PveHurtDeRatio;

		private int _PvpHurtAddRatio;

		private int _PvpHurtDeRatio;

		private int _AtkMulAmend;

		private int _DefMulAmend;

		private int _HpLmtMulAmend;

		private int _PveAtkMulAmend;

		private int _PvpAtkMulAmend;

		private int _ActPointRecoverSpeedAmend;

		private int _ThunderBuffAddProbAddAmend;

		private int _ThunderBuffDurTimeAddAmend;

		private int _WaterBuffAddProbAddAmend;

		private int _WaterBuffDurTimeAddAmend;

		private int _VpLmt;

		private int _VpLmtMulAmend;

		private int _VpResume;

		private int _VpAtk;

		private int _VpAtkMulAmend;

		private int _IdleVpResume;

		private int _HealIncreasePercent;

		private int _CritAddValue;

		private int _HpRestore;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "simpleInfo", DataFormat = DataFormat.Default)]
		public SimpleBaseInfo simpleInfo
		{
			get
			{
				return this._simpleInfo;
			}
			set
			{
				this._simpleInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "Atk", DataFormat = DataFormat.TwosComplement)]
		public int Atk
		{
			get
			{
				return this._Atk;
			}
			set
			{
				this._Atk = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "Defence", DataFormat = DataFormat.TwosComplement)]
		public int Defence
		{
			get
			{
				return this._Defence;
			}
			set
			{
				this._Defence = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "HpLmt", DataFormat = DataFormat.TwosComplement)]
		public long HpLmt
		{
			get
			{
				return this._HpLmt;
			}
			set
			{
				this._HpLmt = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "PveAtk", DataFormat = DataFormat.TwosComplement)]
		public int PveAtk
		{
			get
			{
				return this._PveAtk;
			}
			set
			{
				this._PveAtk = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "PvpAtk", DataFormat = DataFormat.TwosComplement)]
		public int PvpAtk
		{
			get
			{
				return this._PvpAtk;
			}
			set
			{
				this._PvpAtk = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "HitRatio", DataFormat = DataFormat.TwosComplement)]
		public int HitRatio
		{
			get
			{
				return this._HitRatio;
			}
			set
			{
				this._HitRatio = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "DodgeRatio", DataFormat = DataFormat.TwosComplement)]
		public int DodgeRatio
		{
			get
			{
				return this._DodgeRatio;
			}
			set
			{
				this._DodgeRatio = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "CritRatio", DataFormat = DataFormat.TwosComplement)]
		public int CritRatio
		{
			get
			{
				return this._CritRatio;
			}
			set
			{
				this._CritRatio = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "DecritRatio", DataFormat = DataFormat.TwosComplement)]
		public int DecritRatio
		{
			get
			{
				return this._DecritRatio;
			}
			set
			{
				this._DecritRatio = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "CritHurtAddRatio", DataFormat = DataFormat.TwosComplement)]
		public int CritHurtAddRatio
		{
			get
			{
				return this._CritHurtAddRatio;
			}
			set
			{
				this._CritHurtAddRatio = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "ParryRatio", DataFormat = DataFormat.TwosComplement)]
		public int ParryRatio
		{
			get
			{
				return this._ParryRatio;
			}
			set
			{
				this._ParryRatio = value;
			}
		}

		[ProtoMember(13, IsRequired = true, Name = "DeparryRatio", DataFormat = DataFormat.TwosComplement)]
		public int DeparryRatio
		{
			get
			{
				return this._DeparryRatio;
			}
			set
			{
				this._DeparryRatio = value;
			}
		}

		[ProtoMember(14, IsRequired = true, Name = "ParryHurtDeRatio", DataFormat = DataFormat.TwosComplement)]
		public int ParryHurtDeRatio
		{
			get
			{
				return this._ParryHurtDeRatio;
			}
			set
			{
				this._ParryHurtDeRatio = value;
			}
		}

		[ProtoMember(15, IsRequired = true, Name = "SuckBloodScale", DataFormat = DataFormat.TwosComplement)]
		public int SuckBloodScale
		{
			get
			{
				return this._SuckBloodScale;
			}
			set
			{
				this._SuckBloodScale = value;
			}
		}

		[ProtoMember(16, IsRequired = true, Name = "HurtAddRatio", DataFormat = DataFormat.TwosComplement)]
		public int HurtAddRatio
		{
			get
			{
				return this._HurtAddRatio;
			}
			set
			{
				this._HurtAddRatio = value;
			}
		}

		[ProtoMember(17, IsRequired = true, Name = "HurtDeRatio", DataFormat = DataFormat.TwosComplement)]
		public int HurtDeRatio
		{
			get
			{
				return this._HurtDeRatio;
			}
			set
			{
				this._HurtDeRatio = value;
			}
		}

		[ProtoMember(18, IsRequired = true, Name = "PveHurtAddRatio", DataFormat = DataFormat.TwosComplement)]
		public int PveHurtAddRatio
		{
			get
			{
				return this._PveHurtAddRatio;
			}
			set
			{
				this._PveHurtAddRatio = value;
			}
		}

		[ProtoMember(19, IsRequired = true, Name = "PveHurtDeRatio", DataFormat = DataFormat.TwosComplement)]
		public int PveHurtDeRatio
		{
			get
			{
				return this._PveHurtDeRatio;
			}
			set
			{
				this._PveHurtDeRatio = value;
			}
		}

		[ProtoMember(20, IsRequired = true, Name = "PvpHurtAddRatio", DataFormat = DataFormat.TwosComplement)]
		public int PvpHurtAddRatio
		{
			get
			{
				return this._PvpHurtAddRatio;
			}
			set
			{
				this._PvpHurtAddRatio = value;
			}
		}

		[ProtoMember(21, IsRequired = true, Name = "PvpHurtDeRatio", DataFormat = DataFormat.TwosComplement)]
		public int PvpHurtDeRatio
		{
			get
			{
				return this._PvpHurtDeRatio;
			}
			set
			{
				this._PvpHurtDeRatio = value;
			}
		}

		[ProtoMember(22, IsRequired = true, Name = "AtkMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int AtkMulAmend
		{
			get
			{
				return this._AtkMulAmend;
			}
			set
			{
				this._AtkMulAmend = value;
			}
		}

		[ProtoMember(23, IsRequired = true, Name = "DefMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int DefMulAmend
		{
			get
			{
				return this._DefMulAmend;
			}
			set
			{
				this._DefMulAmend = value;
			}
		}

		[ProtoMember(24, IsRequired = true, Name = "HpLmtMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int HpLmtMulAmend
		{
			get
			{
				return this._HpLmtMulAmend;
			}
			set
			{
				this._HpLmtMulAmend = value;
			}
		}

		[ProtoMember(25, IsRequired = true, Name = "PveAtkMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int PveAtkMulAmend
		{
			get
			{
				return this._PveAtkMulAmend;
			}
			set
			{
				this._PveAtkMulAmend = value;
			}
		}

		[ProtoMember(26, IsRequired = true, Name = "PvpAtkMulAmend", DataFormat = DataFormat.TwosComplement)]
		public int PvpAtkMulAmend
		{
			get
			{
				return this._PvpAtkMulAmend;
			}
			set
			{
				this._PvpAtkMulAmend = value;
			}
		}

		[ProtoMember(27, IsRequired = true, Name = "ActPointRecoverSpeedAmend", DataFormat = DataFormat.TwosComplement)]
		public int ActPointRecoverSpeedAmend
		{
			get
			{
				return this._ActPointRecoverSpeedAmend;
			}
			set
			{
				this._ActPointRecoverSpeedAmend = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "ThunderBuffAddProbAddAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ThunderBuffAddProbAddAmend
		{
			get
			{
				return this._ThunderBuffAddProbAddAmend;
			}
			set
			{
				this._ThunderBuffAddProbAddAmend = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "ThunderBuffDurTimeAddAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ThunderBuffDurTimeAddAmend
		{
			get
			{
				return this._ThunderBuffDurTimeAddAmend;
			}
			set
			{
				this._ThunderBuffDurTimeAddAmend = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "WaterBuffAddProbAddAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int WaterBuffAddProbAddAmend
		{
			get
			{
				return this._WaterBuffAddProbAddAmend;
			}
			set
			{
				this._WaterBuffAddProbAddAmend = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "WaterBuffDurTimeAddAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int WaterBuffDurTimeAddAmend
		{
			get
			{
				return this._WaterBuffDurTimeAddAmend;
			}
			set
			{
				this._WaterBuffDurTimeAddAmend = value;
			}
		}

		[ProtoMember(34, IsRequired = false, Name = "VpLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpLmt
		{
			get
			{
				return this._VpLmt;
			}
			set
			{
				this._VpLmt = value;
			}
		}

		[ProtoMember(35, IsRequired = false, Name = "VpLmtMulAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpLmtMulAmend
		{
			get
			{
				return this._VpLmtMulAmend;
			}
			set
			{
				this._VpLmtMulAmend = value;
			}
		}

		[ProtoMember(36, IsRequired = false, Name = "VpResume", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpResume
		{
			get
			{
				return this._VpResume;
			}
			set
			{
				this._VpResume = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "VpAtk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpAtk
		{
			get
			{
				return this._VpAtk;
			}
			set
			{
				this._VpAtk = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "VpAtkMulAmend", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpAtkMulAmend
		{
			get
			{
				return this._VpAtkMulAmend;
			}
			set
			{
				this._VpAtkMulAmend = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "IdleVpResume", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int IdleVpResume
		{
			get
			{
				return this._IdleVpResume;
			}
			set
			{
				this._IdleVpResume = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "HealIncreasePercent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HealIncreasePercent
		{
			get
			{
				return this._HealIncreasePercent;
			}
			set
			{
				this._HealIncreasePercent = value;
			}
		}

		[ProtoMember(41, IsRequired = false, Name = "CritAddValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CritAddValue
		{
			get
			{
				return this._CritAddValue;
			}
			set
			{
				this._CritAddValue = value;
			}
		}

		[ProtoMember(42, IsRequired = false, Name = "HpRestore", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HpRestore
		{
			get
			{
				return this._HpRestore;
			}
			set
			{
				this._HpRestore = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
