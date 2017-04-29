using AIRuntime;
using System;

namespace AIMind
{
	public class UseSkillNode : Action
	{
		protected int nSkill;

		public UseSkillNode(int Skill)
		{
			this.nSkill = Skill;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CastSkillBySkillIndex(this.nSkill);
		}
	}
}
