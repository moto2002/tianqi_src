using System;
using System.Collections;

namespace Spine
{
	public class WaitForSpineEvent : IEnumerator
	{
		private EventData m_TargetEvent;

		private string m_EventName;

		private AnimationState m_AnimationState;

		private bool m_WasFired;

		private bool m_unsubscribeAfterFiring;

		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		public bool WillUnsubscribeAfterFiring
		{
			get
			{
				return this.m_unsubscribeAfterFiring;
			}
			set
			{
				this.m_unsubscribeAfterFiring = value;
			}
		}

		public WaitForSpineEvent(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			this.Subscribe(state, eventDataReference, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			this.Subscribe(skeletonAnimation.state, eventDataReference, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			this.SubscribeByName(state, eventName, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, string eventName, bool unsubscribeAfterFiring = true)
		{
			this.SubscribeByName(skeletonAnimation.state, eventName, unsubscribeAfterFiring);
		}

		bool IEnumerator.MoveNext()
		{
			if (this.m_WasFired)
			{
				this.Reset();
				return false;
			}
			return true;
		}

		void IEnumerator.Reset()
		{
			this.m_WasFired = false;
		}

		private void Subscribe(AnimationState state, EventData eventDataReference, bool unsubscribe)
		{
			if (state == null || eventDataReference == null)
			{
				this.m_WasFired = true;
			}
			else
			{
				this.m_AnimationState = state;
				this.m_TargetEvent = eventDataReference;
				state.Event += new AnimationState.EventDelegate(this.HandleAnimationStateEvent);
				this.m_unsubscribeAfterFiring = unsubscribe;
			}
		}

		private void SubscribeByName(AnimationState state, string eventName, bool unsubscribe)
		{
			if (state == null || string.IsNullOrEmpty(eventName))
			{
				this.m_WasFired = true;
			}
			else
			{
				this.m_AnimationState = state;
				this.m_EventName = eventName;
				state.Event += new AnimationState.EventDelegate(this.HandleAnimationStateEventByName);
				this.m_unsubscribeAfterFiring = unsubscribe;
			}
		}

		private void HandleAnimationStateEventByName(AnimationState state, int trackIndex, Event e)
		{
			if (state != this.m_AnimationState)
			{
				return;
			}
			this.m_WasFired |= (e.Data.Name == this.m_EventName);
			if (this.m_WasFired && this.m_unsubscribeAfterFiring)
			{
				state.Event -= new AnimationState.EventDelegate(this.HandleAnimationStateEventByName);
			}
		}

		private void HandleAnimationStateEvent(AnimationState state, int trackIndex, Event e)
		{
			if (state != this.m_AnimationState)
			{
				return;
			}
			this.m_WasFired |= (e.Data == this.m_TargetEvent);
			if (this.m_WasFired && this.m_unsubscribeAfterFiring)
			{
				state.Event -= new AnimationState.EventDelegate(this.HandleAnimationStateEvent);
			}
		}

		public WaitForSpineEvent NowWaitFor(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			this.Reset();
			this.Clear(state);
			this.Subscribe(state, eventDataReference, unsubscribeAfterFiring);
			return this;
		}

		public WaitForSpineEvent NowWaitFor(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			this.Reset();
			this.Clear(state);
			this.SubscribeByName(state, eventName, unsubscribeAfterFiring);
			return this;
		}

		private void Clear(AnimationState state)
		{
			state.Event -= new AnimationState.EventDelegate(this.HandleAnimationStateEvent);
			state.Event -= new AnimationState.EventDelegate(this.HandleAnimationStateEventByName);
		}
	}
}
