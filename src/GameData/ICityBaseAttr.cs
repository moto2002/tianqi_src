using System;

namespace GameData
{
	internal interface ICityBaseAttr : IClientBaseAttr, ISimpleBaseAttr, IDevelopBaseAttr
	{
		long Exp
		{
			get;
			set;
		}

		long ExpLmt
		{
			get;
			set;
		}

		int Energy
		{
			get;
			set;
		}

		int EnergyLmt
		{
			get;
			set;
		}

		int Diamond
		{
			get;
			set;
		}

		long Gold
		{
			get;
			set;
		}

		int RechargeDiamond
		{
			get;
			set;
		}

		int Honor
		{
			get;
			set;
		}

		int CompetitiveCurrency
		{
			get;
			set;
		}

		long SkillPoint
		{
			get;
			set;
		}

		long Reputation
		{
			get;
			set;
		}
	}
}
