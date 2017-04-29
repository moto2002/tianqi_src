using AIRuntime;
using System;

namespace AIMind
{
	public class PackTryToCastSkillNode : Action
	{
		protected int skillIndex;

		protected int random;

		protected ComparisonOperator comparisonOperator1;

		protected float range1;

		protected ComparisonOperator comparisonOperator2;

		protected float range2;

		protected LogicalOperator logicalOperator = LogicalOperator.And;

		public PackTryToCastSkillNode(ComparisonOperator ComparisonOperator1, ComparisonOperator ComparisonOperator2, LogicalOperator LogicalOperator, int Random, float Range1, float Range2, int SkillIndex)
		{
			this.skillIndex = SkillIndex;
			this.random = Random;
			this.comparisonOperator1 = ComparisonOperator1;
			this.range1 = Range1;
			this.comparisonOperator2 = ComparisonOperator2;
			this.range2 = Range2;
			this.logicalOperator = LogicalOperator;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PackTryToCastSkillBySkillIndex(this.skillIndex, this.random, this.comparisonOperator1, this.range1, this.comparisonOperator2, this.range2, this.logicalOperator);
		}
	}
}
