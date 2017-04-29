using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToFollowTargetNode : Action
	{
		protected float searchRange;

		protected float startDistance;

		protected float stopDistance;

		protected FollowTargetType followTargetType;

		public MoveToFollowTargetNode(FollowTargetType FollowTargetType, float SearchRange, float StartDistance, float StopDistance)
		{
			this.followTargetType = FollowTargetType;
			this.searchRange = SearchRange;
			this.startDistance = StartDistance;
			this.stopDistance = StopDistance;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToFollowTarget(this.followTargetType, this.searchRange, this.startDistance, this.stopDistance);
		}
	}
}
