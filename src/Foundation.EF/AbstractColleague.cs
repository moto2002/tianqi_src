using System;

namespace Foundation.EF
{
	public abstract class AbstractColleague : ILifeTimeObject
	{
		protected IInvoker mediator;

		public void setMediator(IInvoker mediator)
		{
			this.mediator = mediator;
		}

		public abstract void OnCreate();

		public abstract void OnDestroy();
	}
}
