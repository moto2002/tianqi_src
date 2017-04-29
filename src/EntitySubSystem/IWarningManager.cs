using System;
using XEngineActor;

namespace EntitySubSystem
{
	public interface IWarningManager
	{
		void UpdateActor(ActorParent actor);

		bool ExecuteWarningMessage(Action successCallBack = null, Action moveEndCallBack = null);

		bool HasWarningMessage();

		void ClearWarningMessage();
	}
}
