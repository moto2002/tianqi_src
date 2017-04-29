using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public abstract class FSMSystem<TEvent, TState> where TEvent : struct, IConvertible where TState : struct, IConvertible
	{
		private Dictionary<int, FSMState> StateMap = new Dictionary<int, FSMState>();

		public FSMState CurrentState
		{
			get;
			private set;
		}

		public void Init()
		{
			Array values = Enum.GetValues(typeof(TState));
			this.StateMap.Clear();
			for (int i = 0; i < values.get_Length(); i++)
			{
				int hashCode = values.GetValue(i).GetHashCode();
				this.StateMap.Add(hashCode, new FSMState(hashCode));
			}
			this.DoInit();
		}

		public void Resume(TState firstState)
		{
			if (this.CurrentState != null)
			{
				this.CurrentState.Exit();
			}
			this.CurrentState = this.GetState(firstState);
			this.CurrentState.Enter(-1, null);
		}

		public FSMState React(TEvent eventID)
		{
			int hashCode = eventID.GetHashCode();
			FSMState transition = this.CurrentState.GetTransition(hashCode);
			if (transition != null)
			{
				FSMState currentState = this.CurrentState;
				this.CurrentState.Exit();
				this.CurrentState = transition;
				this.CurrentState.Enter(hashCode, currentState);
			}
			return transition;
		}

		public void ReactSync(TEvent eventID)
		{
			Loom.Current.QueueOnMainThread(delegate
			{
				this.React(eventID);
			});
		}

		public void Update()
		{
			if (this.CurrentState != null)
			{
				this.CurrentState.Update();
			}
		}

		protected abstract void DoInit();

		protected FSMState GetState(TState stateID)
		{
			return this.StateMap.get_Item(stateID.GetHashCode());
		}

		protected FSMState GetState(int stateID)
		{
			return this.StateMap.get_Item(stateID);
		}

		protected void InitState(TState stateID, FSMState.EnterCallback onEnter, FSMState.UpdateCallback onUpdate = null, FSMState.ExitCallBack onExit = null)
		{
			FSMState state = this.GetState(stateID);
			if (state != null)
			{
				if (state.OnEnter == null)
				{
					state.OnEnter = onEnter;
				}
				else
				{
					Debug.LogError("重复设置的Enter函数");
				}
				if (state.OnUpdate == null)
				{
					state.OnUpdate = onUpdate;
				}
				else
				{
					Debug.LogError("重复设置的Update函数");
				}
				if (state.OnExit == null)
				{
					state.OnExit = onExit;
				}
				else
				{
					Debug.LogError("重复设置的Exit函数");
				}
			}
		}

		protected void InitTransition(TState src, TEvent eventID, TState dst)
		{
			FSMState state = this.GetState(src);
			state.AddTransition(eventID.GetHashCode(), this.GetState(dst));
		}
	}
}
