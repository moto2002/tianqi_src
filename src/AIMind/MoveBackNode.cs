using AIRuntime;
using System;

namespace AIMind
{
	public class MoveBackNode : Action
	{
		protected int random;

		protected float time;

		public MoveBackNode(int Random, float Time)
		{
			this.random = Random;
			this.time = Time;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveBack(this.random, this.time);
		}
	}
}
