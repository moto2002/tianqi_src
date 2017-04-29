using System;

namespace Foundation.EF
{
	public interface ILifeTimeObject
	{
		void OnCreate();

		void OnDestroy();
	}
}
