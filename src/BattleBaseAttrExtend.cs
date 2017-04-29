using GameData;
using System;

public class BattleBaseAttrExtend : StandardBaseAttrExtend, ISimpleBaseAttrExtend, IStandardBaseAttrExtend, IBattleBaseAttrExtend
{
	public virtual BuffCtrlAttrs GetBuffCtrlAttrs(int elementType)
	{
		return null;
	}

	public virtual void SetBuffCtrlAttrs(BuffCtrlAttrs attrs)
	{
	}
}
