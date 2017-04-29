using System;

namespace FSM
{
	public class FSMEnterCallbackAttribute : Attribute
	{
		public int StateID
		{
			get;
			private set;
		}

		public FSMEnterCallbackAttribute(int stateID)
		{
			this.StateID = stateID;
		}
	}
}
