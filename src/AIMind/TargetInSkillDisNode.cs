using AIRuntime;
using System;

namespace AIMind
{
	public class TargetInSkillDisNode : ConditionConnectors
	{
		protected int nSkill;

		public TargetInSkillDisNode(int Skill)
		{
			this.nSkill = Skill;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckTargetDistanceBySkillIndex(this.nSkill);
		}
	}
}
