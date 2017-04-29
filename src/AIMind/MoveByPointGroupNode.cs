using AIRuntime;
using System;

namespace AIMind
{
	public class MoveByPointGroupNode : Action
	{
		protected int pointID;

		public MoveByPointGroupNode(int PointID)
		{
			this.pointID = PointID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveByPointGroup(this.pointID);
		}
	}
}
