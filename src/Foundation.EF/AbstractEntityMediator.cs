using System;
using System.Collections;

namespace Foundation.EF
{
	public abstract class AbstractEntityMediator : IInvoker
	{
		protected Hashtable colleagues = new Hashtable();

		protected void addColleague(string name, AbstractColleague c)
		{
			c.setMediator(this);
			this.colleagues.Add(name, c);
		}

		protected void deleteColleague(string name)
		{
			this.colleagues.Remove(name);
		}
	}
}
