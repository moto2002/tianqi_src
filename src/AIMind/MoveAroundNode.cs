using AIRuntime;
using System;

namespace AIMind
{
	public class MoveAroundNode : Action
	{
		protected int random;

		protected float time;

		public MoveAroundNode(int Random, float Time)
		{
			this.random = Random;
			this.time = Time;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveAround(this.random, this.time);
		}
	}
}
