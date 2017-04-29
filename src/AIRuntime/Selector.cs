using AIMind;
using System;

namespace AIRuntime
{
	public class Selector : CompositeNode
	{
		public override bool Proc(IAIProc theOwner)
		{
			for (int i = 0; i < this.children.get_Count(); i++)
			{
				if (this.children.get_Item(i).Proc(theOwner))
				{
					return true;
				}
			}
			return false;
		}
	}
}
