using System;

public class RoleAttrTool
{
	public static int GetAtk(EntitySelf self)
	{
		return (int)((double)self.Atk * (1.0 + (double)self.AtkMulAmend * 0.001));
	}

	public static int GetDefence(EntitySelf self)
	{
		return (int)((double)self.Defence * (1.0 + (double)self.DefMulAmend * 0.001));
	}

	public static long GetHpLmt(EntitySelf self)
	{
		return (long)((double)self.HpLmt * (1.0 + (double)self.HpLmtMulAmend * 0.001));
	}

	public static int GetPveAtk(EntitySelf self)
	{
		return (int)((double)self.PveAtk * (1.0 + (double)self.PveAtkMulAmend * 0.001));
	}

	public static int GetPvpAtk(EntitySelf self)
	{
		return (int)((double)self.PvpAtk * (1.0 + (double)self.PvpAtkMulAmend * 0.001));
	}
}
