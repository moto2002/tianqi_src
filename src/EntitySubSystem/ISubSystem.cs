using System;

namespace EntitySubSystem
{
	public interface ISubSystem
	{
		void OnCreate(EntityParent owner);

		void OnDestroy();
	}
}
