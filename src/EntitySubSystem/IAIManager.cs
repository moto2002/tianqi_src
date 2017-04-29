using AIMind;
using System;
using XEngineActor;

namespace EntitySubSystem
{
	public interface IAIManager : IAIProc, ISubSystem
	{
		string AIType
		{
			get;
			set;
		}

		int ThinkInterval
		{
			get;
			set;
		}

		void Active();

		void Deactive();

		void TryThink();

		void ResetAIManager();

		void UpdateActor(ActorParent actor);

		bool IsAIActive();
	}
}
