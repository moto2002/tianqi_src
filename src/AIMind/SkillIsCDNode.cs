using AIRuntime;
using System;

namespace AIMind
{
	public class SkillIsCDNode : ConditionConnectors
	{
		protected int nSkill;

		public SkillIsCDNode(int Skill)
		{
			this.nSkill = Skill;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return !theOwner.CheckSkillInCDByIndex(this.nSkill);
		}
	}
}
