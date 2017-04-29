using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToTargetNode : Action
	{
		protected int nSkill;

		public MoveToTargetNode(int Skill)
		{
			this.nSkill = Skill;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToTargetBySkillIndex(this.nSkill);
		}
	}
}
