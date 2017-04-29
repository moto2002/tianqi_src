using System;

namespace GameData
{
	internal interface IDevelopBaseAttr : ISimpleBaseAttr
	{
		int Atk
		{
			get;
			set;
		}

		int Defence
		{
			get;
			set;
		}

		long HpLmt
		{
			get;
			set;
		}

		int PveAtk
		{
			get;
			set;
		}

		int PvpAtk
		{
			get;
			set;
		}

		int HitRatio
		{
			get;
			set;
		}

		int DodgeRatio
		{
			get;
			set;
		}

		int CritRatio
		{
			get;
			set;
		}

		int DecritRatio
		{
			get;
			set;
		}

		int CritHurtAddRatio
		{
			get;
			set;
		}

		int ParryRatio
		{
			get;
			set;
		}

		int DeparryRatio
		{
			get;
			set;
		}

		int ParryHurtDeRatio
		{
			get;
			set;
		}

		int SuckBloodScale
		{
			get;
			set;
		}

		int HurtAddRatio
		{
			get;
			set;
		}

		int HurtDeRatio
		{
			get;
			set;
		}

		int PveHurtAddRatio
		{
			get;
			set;
		}

		int PveHurtDeRatio
		{
			get;
			set;
		}

		int PvpHurtAddRatio
		{
			get;
			set;
		}

		int PvpHurtDeRatio
		{
			get;
			set;
		}

		int AtkMulAmend
		{
			get;
			set;
		}

		int DefMulAmend
		{
			get;
			set;
		}

		int HpLmtMulAmend
		{
			get;
			set;
		}

		int PveAtkMulAmend
		{
			get;
			set;
		}

		int PvpAtkMulAmend
		{
			get;
			set;
		}

		int ActPointRecoverSpeedAmend
		{
			get;
			set;
		}

		int VpLmt
		{
			get;
			set;
		}

		int VpLmtMulAmend
		{
			get;
			set;
		}

		int VpAtk
		{
			get;
			set;
		}

		int VpAtkMulAmend
		{
			get;
			set;
		}

		int VpResume
		{
			get;
			set;
		}

		int IdleVpResume
		{
			get;
			set;
		}

		int ThunderBuffAddProbAddAmend
		{
			get;
			set;
		}

		int ThunderBuffDurTimeAddAmend
		{
			get;
			set;
		}

		int WaterBuffAddProbAddAmend
		{
			get;
			set;
		}

		int WaterBuffDurTimeAddAmend
		{
			get;
			set;
		}

		int HealIncreasePercent
		{
			get;
			set;
		}

		int CritAddValue
		{
			get;
			set;
		}

		int HpRestore
		{
			get;
			set;
		}
	}
}
