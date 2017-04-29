using AIMind;
using System;

namespace AIRuntime
{
	public class BTNode : Command
	{
		public Command root;

		public override bool Proc(IAIProc theOwner)
		{
			return this.root.Proc(theOwner);
		}

		public override void AddChild(Command root)
		{
			this.root = root;
		}
	}
}
