using System;
using System.Collections;

namespace Spine
{
	public class WaitForSpineAnimationComplete : IEnumerator
	{
		private bool m_WasFired;

		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		public WaitForSpineAnimationComplete(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
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

		private void HandleComplete(AnimationState state, int trackIndex, int loopCount)
		{
			this.m_WasFired = true;
		}

		private void SafeSubscribe(TrackEntry trackEntry)
		{
			if (trackEntry == null)
			{
				this.m_WasFired = true;
			}
			else
			{
				trackEntry.Complete += new AnimationState.CompleteDelegate(this.HandleComplete);
			}
		}

		public WaitForSpineAnimationComplete NowWaitFor(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
			return this;
		}
	}
}
