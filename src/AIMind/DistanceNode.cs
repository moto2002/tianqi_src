using AIRuntime;
using System;

namespace AIMind
{
	public class DistanceNode : ConditionConnectors
	{
		protected float fDis;

		public DistanceNode(float Dis)
		{
			this.fDis = Dis;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckDistanceBetweenOwnerAndTarget(this.fDis);
		}
	}
}
