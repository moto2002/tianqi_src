using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToSkillEdgeNode : Action
	{
		protected int skill;

		public MoveToSkillEdgeNode(int Skill)
		{
			this.skill = Skill;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToSkillEdgeBySkillIndex(this.skill);
		}
	}
}
