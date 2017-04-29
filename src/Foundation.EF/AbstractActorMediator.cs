using System;
using UnityEngine;

namespace Foundation.EF
{
	public abstract class AbstractActorMediator : MonoBehaviour, IInvoker
	{
		protected XDict<Type, AbstractColleague> colleagues = new XDict<Type, AbstractColleague>();

		protected void addColleague(AbstractColleague c)
		{
			c.setMediator(this);
			c.OnCreate();
			this.colleagues.Add(c.GetType(), c);
		}

		protected void deleteColleague(Type type)
		{
			AbstractColleague abstractColleague = this.colleagues[type];
			abstractColleague.setMediator(null);
			abstractColleague.OnDestroy();
			this.colleagues.Remove(type);
		}
	}
}
