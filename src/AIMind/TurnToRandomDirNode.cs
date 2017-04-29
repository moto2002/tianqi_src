using AIRuntime;
using System;

namespace AIMind
{
	public class TurnToRandomDirNode : Action
	{
		protected float angle1;

		protected float angle2;

		public TurnToRandomDirNode(float Angle1, float Angle2)
		{
			this.angle1 = Angle1;
			this.angle2 = Angle2;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.TurnToRandomDir(this.angle1, this.angle2);
		}
	}
}
