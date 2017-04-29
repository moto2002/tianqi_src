using System;

namespace EntitySubSystem
{
	public interface IFeedbackManager
	{
		StrategyType CurStrategyType
		{
			get;
			set;
		}

		int AllStrategyTypeCount
		{
			get;
		}

		void Active();

		void Deactive();
	}
}
