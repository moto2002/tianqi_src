using AIMind;
using System;

namespace AIRuntime
{
	public class DecoratorNode : Command
	{
		protected Command child;

		public void Proxy(Command _child)
		{
			this.child = _child;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return this.child.Proc(theOwner);
		}

		public override void AddChild(Command _node)
		{
			Debuger.Error("this node can not add child --------------------------", new object[0]);
		}
	}
}
