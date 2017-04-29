using System;

public class ActionStatusName
{
	public const string NONE = "";

	public const string STAND = "stand";

	public const string WALK = "walk";

	public const string IDLE = "idle";

	public const string IDLE_CITY = "idle_city";

	public const string RUN = "run";

	public const string SIDESWAY = "sidesway";

	public const string ASSAULT = "rush";

	public const string ATTACK = "attack";

	public const string ATTACK1 = "attack1";

	public const string ATTACK2 = "attack2";

	public const string ATTACK3 = "attack3";

	public const string ATTACK4 = "attack4";

	public const string HIT = "hit";

	public const string HIT2 = "hit2";

	public const string SKILL = "skill";

	public const string SKILL1 = "skill1";

	public const string SKILL2 = "skill2";

	public const string SKILL3 = "skill3";

	public const string SKILL4 = "skill4";

	public const string SKILL5 = "skill5";

	public const string SKILL6 = "skill6";

	public const string SKILL7 = "skill7";

	public const string SKILL8 = "skill8";

	public const string SKILL9 = "skill9";

	public const string SKILL10 = "skill10";

	public const string SKILL11 = "skill11";

	public const string SKILL12 = "skill12";

	public const string SKILL13 = "skill13";

	public const string SKILL14 = "skill14";

	public const string SKILL15 = "skill15";

	public const string SKILL16 = "skill16";

	public const string SKILL17 = "skill17";

	public const string SKILL18 = "skill18";

	public const string SKILL19 = "skill19";

	public const string SKILL20 = "skill20";

	public const string SKILLCHARGE = "skillCharge";

	public const string SKILLCHARGE1 = "skillCharge1";

	public const string SKILLCHARGE2 = "skillCharge2";

	public const string SKILLCHARGE3 = "skillCharge3";

	public const string SKILLCHARGE4 = "skillCharge4";

	public const string SKILLCHARGE5 = "skillCharge5";

	public const string SKILLCHARGE6 = "skillCharge6";

	public const string SKILLCHARGE7 = "skillCharge7";

	public const string SKILLCHARGE8 = "skillCharge8";

	public const string SKILLCHARGE9 = "skillCharge9";

	public const string SKILLCHARGE10 = "skillCharge10";

	public const string FUSE = "fuse";

	public const string EXCHANGE = "exchange";

	public const string HITBACK = "hitback";

	public const string DOWN = "down";

	public const string FLOAT = "float";

	public const string FLOAT2 = "float2";

	public const string FLOATHIT = "floathit";

	public const string BORN = "born";

	public const string VICTORY = "victory";

	public const string DIE = "die";

	public const string SWING = "swing";

	public const string SWINGLEFT = "swingleft";

	public const string SWINGRIGHT = "swingright";

	public const string SWINGBACK = "swingback";

	public const string TURN = "turn";

	public const string TURNLEFT = "turnleft";

	public const string TURNRIGHT = "turnright";

	public const string ROLL = "roll";

	public const string ROLL2 = "roll2";

	public const string RUN_CITY = "run_city";

	public const string COLLECT_CITY = "collect_city";

	public const string TAME_CITY = "tame_city";

	public const string HIDE = "hide";

	public static bool IsIdleAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "idle") || XUtility.StartsWith(actionName, "stand");
	}

	public static bool IsSkillAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "attack") || XUtility.StartsWith(actionName, "skill") || XUtility.StartsWith(actionName, "exchange") || XUtility.StartsWith(actionName, "fuse") || XUtility.StartsWith(actionName, "roll");
	}

	public static bool IsSkillChargeAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "skillCharge");
	}

	public static bool IsHitAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "hit") || XUtility.StartsWith(actionName, "float");
	}

	public static bool IsBornAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "born");
	}

	public static bool IsDieAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "die");
	}

	public static bool IsSpinAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "turn");
	}

	public static bool IsVictoryAction(string actionName)
	{
		return XUtility.StartsWith(actionName, "victory");
	}

	public static bool IsActionCauseNormalMove(string actionName)
	{
		return XUtility.StartsWith(actionName, "run") || XUtility.StartsWith(actionName, "swing");
	}
}
