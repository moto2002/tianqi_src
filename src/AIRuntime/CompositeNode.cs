using AIMind;
using System;
using System.Collections.Generic;

namespace AIRuntime
{
	public abstract class CompositeNode : Command
	{
		protected List<Command> children = new List<Command>();

		public override bool Proc(IAIProc theOwner)
		{
			return true;
		}

		public override void AddChild(Command _node)
		{
			this.children.Add(_node);
		}

		public void DelChild(Command _node)
		{
			this.children.Remove(_node);
		}

		public void ClearChildren()
		{
			this.children.Clear();
		}

		public List<Command> GetChild()
		{
			return this.children;
		}
	}
}
