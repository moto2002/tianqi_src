using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToPointNode : Action
	{
		protected float x;

		protected float z;

		protected float range;

		public MoveToPointNode(float X, float Z, float Range)
		{
			this.x = X;
			this.z = Z;
			this.range = Range;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToPoint(this.x, this.z, this.range);
		}
	}
}
