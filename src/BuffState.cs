using GameData;
using Package;
using System;

public class BuffState
{
	public bool isGlobalBuff;

	public bool isCommunicateMix;

	public long casterID;

	public int fromSkillID;

	public int fromSkillLevel;

	public XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange;

	public float intervalDefaultTime;

	public float intervalLeftTime;

	public double removeLeftTime;

	public bool isBlock;
}
