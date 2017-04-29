using AIRuntime;
using System;

namespace AIMind
{
	public class PackTryToPressIconNode : Action
	{
		protected int icon;

		protected int count;

		protected int interval;

		protected int random;

		protected bool isUseRange;

		protected ComparisonOperator comparisonOperator1;

		protected float range1;

		protected ComparisonOperator comparisonOperator2;

		protected float range2;

		protected LogicalOperator logicalOperator = LogicalOperator.And;

		public PackTryToPressIconNode(int Icon, int Count, int Interval, int Random, ComparisonOperator ComparisonOperator1, ComparisonOperator ComparisonOperator2, LogicalOperator LogicalOperator, float Range1, float Range2, bool IsUseRange)
		{
			this.icon = Icon;
			this.count = Count;
			this.interval = Interval;
			this.random = Random;
			this.comparisonOperator1 = ComparisonOperator1;
			this.range1 = Range1;
			this.comparisonOperator2 = ComparisonOperator2;
			this.range2 = Range2;
			this.logicalOperator = LogicalOperator;
			this.isUseRange = IsUseRange;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PackTryToPressIcon(this.icon, this.count, this.interval, this.random, this.comparisonOperator1, this.range1, this.comparisonOperator2, this.range2, this.logicalOperator, (!this.isUseRange) ? TargetRangeType.World : TargetRangeType.SkillRange);
		}
	}
}
