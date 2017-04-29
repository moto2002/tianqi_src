using System;

namespace GameData
{
	internal interface IBattleBaseAttr : IClientBaseAttr, IClientBattleBaseAttr, ISimpleBaseAttr, IDevelopBaseAttr
	{
		int ActPointLmt
		{
			get;
			set;
		}

		int BuffMoveSpeedMulPosAmend
		{
			get;
			set;
		}

		int BuffActSpeedMulPosAmend
		{
			get;
			set;
		}

		int SkillTreatScaleBOAtk
		{
			get;
			set;
		}

		int SkillTreatScaleBOHpLmt
		{
			get;
			set;
		}

		int SkillIgnoreDefenceHurt
		{
			get;
			set;
		}

		int SkillNmlDmgScale
		{
			get;
			set;
		}

		int SkillNmlDmgAddAmend
		{
			get;
			set;
		}

		int SkillHolyDmgScaleBOMaxHp
		{
			get;
			set;
		}

		int SkillHolyDmgScaleBOCurHp
		{
			get;
			set;
		}

		int Affinity
		{
			get;
			set;
		}

		int OnlineTime
		{
			get;
			set;
		}

		int ActPoint
		{
			get;
			set;
		}

		long Hp
		{
			get;
			set;
		}

		int Vp
		{
			get;
			set;
		}
	}
}
