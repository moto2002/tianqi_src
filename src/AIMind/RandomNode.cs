using AIRuntime;
using System;

namespace AIMind
{
	public class RandomNode : ConditionConnectors
	{
		protected int nRandom;

		public RandomNode(int Random)
		{
			this.nRandom = Random;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckRandom(this.nRandom);
		}
	}
}
