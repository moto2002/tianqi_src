using AIMind;
using System;

namespace AIRuntime
{
	public class ConditionConnectors : Command
	{
		public override bool Proc(IAIProc theOwner)
		{
			return false;
		}

		public override void AddChild(Command _node)
		{
			Debuger.Error("this node can not add child --------------------------", new object[0]);
		}
	}
}
