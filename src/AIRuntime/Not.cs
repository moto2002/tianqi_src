using AIMind;
using System;

namespace AIRuntime
{
	public class Not : DecoratorNode
	{
		public override bool Proc(IAIProc theOwner)
		{
			return !base.Proc(theOwner);
		}
	}
}
