using AIMind;
using System;

namespace AIRuntime
{
	public class IsEventBeHit : ConditionConnectors
	{
		public override bool Proc(IAIProc theOwner)
		{
			if (theOwner.GetBlackBoard().aiEvent == AIEvent.BeHit)
			{
			}
			return theOwner.GetBlackBoard().aiEvent == AIEvent.BeHit;
		}
	}
}
