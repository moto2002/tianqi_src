using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleBaseAttr")]
	[Serializable]
	public class BattleBaseAttr : IExtensible
	{
		private int _BuffMoveSpeedMulPosAmend;

		private int _BuffActSpeedMulPosAmend;

		private int _SkillTreatScaleBOAtk;

		private int _SkillTreatScaleBOHpLmt;

		private int _SkillNmlDmgScale;

		private int _SkillNmlDmgAddAmend;

		private int _SkillHolyDmgScaleBOMaxHp;

		private int _SkillHolyDmgScaleBOCurHp;

		private int _SkillIgnoreDefenceHurt;

		private int _Affinity;

		private int _OnlineTime;

		private int _ActPointLmt;

		private int _ActPoint;

		private long _Hp;

		private int _Vp;

		private IExtension extensionObject;

		[ProtoMember(25, IsRequired = true, Name = "BuffMoveSpeedMulPosAmend", DataFormat = DataFormat.TwosComplement)]
		public int BuffMoveSpeedMulPosAmend
		{
			get
			{
				return this._BuffMoveSpeedMulPosAmend;
			}
			set
			{
				this._BuffMoveSpeedMulPosAmend = value;
			}
		}

		[ProtoMember(26, IsRequired = true, Name = "BuffActSpeedMulPosAmend", DataFormat = DataFormat.TwosComplement)]
		public int BuffActSpeedMulPosAmend
		{
			get
			{
				return this._BuffActSpeedMulPosAmend;
			}
			set
			{
				this._BuffActSpeedMulPosAmend = value;
			}
		}

		[ProtoMember(27, IsRequired = true, Name = "SkillTreatScaleBOAtk", DataFormat = DataFormat.TwosComplement)]
		public int SkillTreatScaleBOAtk
		{
			get
			{
				return this._SkillTreatScaleBOAtk;
			}
			set
			{
				this._SkillTreatScaleBOAtk = value;
			}
		}

		[ProtoMember(28, IsRequired = true, Name = "SkillTreatScaleBOHpLmt", DataFormat = DataFormat.TwosComplement)]
		public int SkillTreatScaleBOHpLmt
		{
			get
			{
				return this._SkillTreatScaleBOHpLmt;
			}
			set
			{
				this._SkillTreatScaleBOHpLmt = value;
			}
		}

		[ProtoMember(29, IsRequired = true, Name = "SkillNmlDmgScale", DataFormat = DataFormat.TwosComplement)]
		public int SkillNmlDmgScale
		{
			get
			{
				return this._SkillNmlDmgScale;
			}
			set
			{
				this._SkillNmlDmgScale = value;
			}
		}

		[ProtoMember(30, IsRequired = true, Name = "SkillNmlDmgAddAmend", DataFormat = DataFormat.TwosComplement)]
		public int SkillNmlDmgAddAmend
		{
			get
			{
				return this._SkillNmlDmgAddAmend;
			}
			set
			{
				this._SkillNmlDmgAddAmend = value;
			}
		}

		[ProtoMember(31, IsRequired = true, Name = "SkillHolyDmgScaleBOMaxHp", DataFormat = DataFormat.TwosComplement)]
		public int SkillHolyDmgScaleBOMaxHp
		{
			get
			{
				return this._SkillHolyDmgScaleBOMaxHp;
			}
			set
			{
				this._SkillHolyDmgScaleBOMaxHp = value;
			}
		}

		[ProtoMember(32, IsRequired = true, Name = "SkillHolyDmgScaleBOCurHp", DataFormat = DataFormat.TwosComplement)]
		public int SkillHolyDmgScaleBOCurHp
		{
			get
			{
				return this._SkillHolyDmgScaleBOCurHp;
			}
			set
			{
				this._SkillHolyDmgScaleBOCurHp = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "SkillIgnoreDefenceHurt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int SkillIgnoreDefenceHurt
		{
			get
			{
				return this._SkillIgnoreDefenceHurt;
			}
			set
			{
				this._SkillIgnoreDefenceHurt = value;
			}
		}

		[ProtoMember(33, IsRequired = true, Name = "Affinity", DataFormat = DataFormat.TwosComplement)]
		public int Affinity
		{
			get
			{
				return this._Affinity;
			}
			set
			{
				this._Affinity = value;
			}
		}

		[ProtoMember(34, IsRequired = true, Name = "OnlineTime", DataFormat = DataFormat.TwosComplement)]
		public int OnlineTime
		{
			get
			{
				return this._OnlineTime;
			}
			set
			{
				this._OnlineTime = value;
			}
		}

		[ProtoMember(35, IsRequired = true, Name = "ActPointLmt", DataFormat = DataFormat.TwosComplement)]
		public int ActPointLmt
		{
			get
			{
				return this._ActPointLmt;
			}
			set
			{
				this._ActPointLmt = value;
			}
		}

		[ProtoMember(36, IsRequired = true, Name = "ActPoint", DataFormat = DataFormat.TwosComplement)]
		public int ActPoint
		{
			get
			{
				return this._ActPoint;
			}
			set
			{
				this._ActPoint = value;
			}
		}

		[ProtoMember(37, IsRequired = true, Name = "Hp", DataFormat = DataFormat.TwosComplement)]
		public long Hp
		{
			get
			{
				return this._Hp;
			}
			set
			{
				this._Hp = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "Vp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Vp
		{
			get
			{
				return this._Vp;
			}
			set
			{
				this._Vp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
