using AIRuntime;
using System;

namespace AIMind
{
	public class CheckPointDistanceNode : ConditionConnectors
	{
		protected int pointID;

		protected ComparisonOperator comparisonOperator1;

		protected float range1;

		protected ComparisonOperator comparisonOperator2;

		protected float range2;

		protected LogicalOperator logicalOperator = LogicalOperator.And;

		public CheckPointDistanceNode(ComparisonOperator ComparisonOperator1, ComparisonOperator ComparisonOperator2, LogicalOperator LogicalOperator, int PointID, float Range1, float Range2)
		{
			this.pointID = PointID;
			this.comparisonOperator1 = ComparisonOperator1;
			this.range1 = Range1;
			this.comparisonOperator2 = ComparisonOperator2;
			this.range2 = Range2;
			this.logicalOperator = LogicalOperator;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckPointDistance(this.pointID, this.comparisonOperator1, this.range1, this.comparisonOperator2, this.range2, this.logicalOperator);
		}
	}
}
