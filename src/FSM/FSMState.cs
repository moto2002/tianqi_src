using System;
using System.Collections.Generic;

namespace FSM
{
	public class FSMState
	{
		public delegate void EnterCallback(int eventID, FSMState preState, FSMState currentState);

		public delegate void UpdateCallback(FSMState currentState);

		public delegate void ExitCallBack(FSMState currentState);

		protected Dictionary<int, FSMState> TransitionMap = new Dictionary<int, FSMState>();

		public FSMState.EnterCallback OnEnter;

		public FSMState.UpdateCallback OnUpdate;

		public FSMState.ExitCallBack OnExit;

		public int StateID
		{
			get;
			private set;
		}

		public FSMState(int id)
		{
			this.StateID = id;
		}

		public void AddTransition(int eventID, FSMState state)
		{
			this.TransitionMap.Add(eventID, state);
		}

		public FSMState GetTransition(int eventID)
		{
			FSMState fSMState = null;
			return (!this.TransitionMap.TryGetValue(eventID, ref fSMState)) ? null : fSMState;
		}

		public void Enter(int eventID, FSMState preState)
		{
			if (this.OnEnter != null)
			{
				this.OnEnter(eventID, preState, this);
			}
		}

		public void Update()
		{
			if (this.OnUpdate != null)
			{
				this.OnUpdate(this);
			}
		}

		public void Exit()
		{
			if (this.OnExit != null)
			{
				this.OnExit(this);
			}
		}
	}
}
