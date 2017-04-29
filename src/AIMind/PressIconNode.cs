using AIRuntime;
using System;

namespace AIMind
{
	public class PressIconNode : Action
	{
		protected int icon;

		protected int count;

		protected int interval;

		public PressIconNode(int Icon, int Count, int Interval)
		{
			this.icon = Icon;
			this.count = Count;
			this.interval = Interval;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PressIcon(this.icon, this.count, this.interval);
		}
	}
}
