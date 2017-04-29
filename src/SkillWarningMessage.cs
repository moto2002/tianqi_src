using System;
using System.Collections.Generic;

public class SkillWarningMessage
{
	public EntityParent caster;

	public EntityParent target;

	public int skillID;

	public Dictionary<int, XPoint> effectMessage = new Dictionary<int, XPoint>();
}
