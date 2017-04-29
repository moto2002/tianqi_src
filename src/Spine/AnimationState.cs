using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spine
{
	public class AnimationState
	{
		public delegate void StartEndDelegate(AnimationState state, int trackIndex);

		public delegate void EventDelegate(AnimationState state, int trackIndex, Event e);

		public delegate void CompleteDelegate(AnimationState state, int trackIndex, int loopCount);

		private AnimationStateData data;

		private ExposedList<TrackEntry> tracks = new ExposedList<TrackEntry>();

		private ExposedList<Event> events = new ExposedList<Event>();

		private float timeScale = 1f;

		public event AnimationState.StartEndDelegate Start
		{
			[MethodImpl(32)]
			add
			{
				this.Start = (AnimationState.StartEndDelegate)Delegate.Combine(this.Start, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.Start = (AnimationState.StartEndDelegate)Delegate.Remove(this.Start, value);
			}
		}

		public event AnimationState.StartEndDelegate End
		{
			[MethodImpl(32)]
			add
			{
				this.End = (AnimationState.StartEndDelegate)Delegate.Combine(this.End, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.End = (AnimationState.StartEndDelegate)Delegate.Remove(this.End, value);
			}
		}

		public event AnimationState.EventDelegate Event
		{
			[MethodImpl(32)]
			add
			{
				this.Event = (AnimationState.EventDelegate)Delegate.Combine(this.Event, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.Event = (AnimationState.EventDelegate)Delegate.Remove(this.Event, value);
			}
		}

		public event AnimationState.CompleteDelegate Complete
		{
			[MethodImpl(32)]
			add
			{
				this.Complete = (AnimationState.CompleteDelegate)Delegate.Combine(this.Complete, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.Complete = (AnimationState.CompleteDelegate)Delegate.Remove(this.Complete, value);
			}
		}

		public AnimationStateData Data
		{
			get
			{
				return this.data;
			}
		}

		public float TimeScale
		{
			get
			{
				return this.timeScale;
			}
			set
			{
				this.timeScale = value;
			}
		}

		public AnimationState(AnimationStateData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data cannot be null.");
			}
			this.data = data;
		}

		public void Update(float delta)
		{
			delta *= this.timeScale;
			for (int i = 0; i < this.tracks.Count; i++)
			{
				TrackEntry trackEntry = this.tracks.Items[i];
				if (trackEntry != null)
				{
					float num = delta * trackEntry.timeScale;
					float num2 = trackEntry.time + num;
					float endTime = trackEntry.endTime;
					trackEntry.time = num2;
					if (trackEntry.previous != null)
					{
						trackEntry.previous.time += num;
						trackEntry.mixTime += num;
					}
					if ((!trackEntry.loop) ? (trackEntry.lastTime < endTime && num2 >= endTime) : (trackEntry.lastTime % endTime > num2 % endTime))
					{
						int loopCount = (int)(num2 / endTime);
						trackEntry.OnComplete(this, i, loopCount);
						if (this.Complete != null)
						{
							this.Complete(this, i, loopCount);
						}
					}
					TrackEntry next = trackEntry.next;
					if (next != null)
					{
						next.time = trackEntry.lastTime - next.delay;
						if (next.time >= 0f)
						{
							this.SetCurrent(i, next);
						}
					}
					else if (!trackEntry.loop && trackEntry.lastTime >= trackEntry.endTime)
					{
						this.ClearTrack(i);
					}
				}
			}
		}

		public void Apply(Skeleton skeleton)
		{
			ExposedList<Event> exposedList = this.events;
			for (int i = 0; i < this.tracks.Count; i++)
			{
				TrackEntry trackEntry = this.tracks.Items[i];
				if (trackEntry != null)
				{
					exposedList.Clear(true);
					float num = trackEntry.time;
					bool loop = trackEntry.loop;
					if (!loop && num > trackEntry.endTime)
					{
						num = trackEntry.endTime;
					}
					TrackEntry previous = trackEntry.previous;
					if (previous == null)
					{
						if (trackEntry.mix == 1f)
						{
							trackEntry.animation.Apply(skeleton, trackEntry.lastTime, num, loop, exposedList);
						}
						else
						{
							trackEntry.animation.Mix(skeleton, trackEntry.lastTime, num, loop, exposedList, trackEntry.mix);
						}
					}
					else
					{
						float num2 = previous.time;
						if (!previous.loop && num2 > previous.endTime)
						{
							num2 = previous.endTime;
						}
						previous.animation.Apply(skeleton, previous.lastTime, num2, previous.loop, null);
						previous.lastTime = num2;
						float num3 = trackEntry.mixTime / trackEntry.mixDuration * trackEntry.mix;
						if (num3 >= 1f)
						{
							num3 = 1f;
							trackEntry.previous = null;
						}
						trackEntry.animation.Mix(skeleton, trackEntry.lastTime, num, loop, exposedList, num3);
					}
					int j = 0;
					int count = exposedList.Count;
					while (j < count)
					{
						Event e = exposedList.Items[j];
						trackEntry.OnEvent(this, i, e);
						if (this.Event != null)
						{
							this.Event(this, i, e);
						}
						j++;
					}
					trackEntry.lastTime = trackEntry.time;
				}
			}
		}

		public void ClearTracks()
		{
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				this.ClearTrack(i);
				i++;
			}
			this.tracks.Clear(true);
		}

		public void ClearTrack(int trackIndex)
		{
			if (trackIndex >= this.tracks.Count)
			{
				return;
			}
			TrackEntry trackEntry = this.tracks.Items[trackIndex];
			if (trackEntry == null)
			{
				return;
			}
			trackEntry.OnEnd(this, trackIndex);
			if (this.End != null)
			{
				this.End(this, trackIndex);
			}
			this.tracks.Items[trackIndex] = null;
		}

		private TrackEntry ExpandToIndex(int index)
		{
			if (index < this.tracks.Count)
			{
				return this.tracks.Items[index];
			}
			while (index >= this.tracks.Count)
			{
				this.tracks.Add(null);
			}
			return null;
		}

		private void SetCurrent(int index, TrackEntry entry)
		{
			TrackEntry trackEntry = this.ExpandToIndex(index);
			if (trackEntry != null)
			{
				TrackEntry previous = trackEntry.previous;
				trackEntry.previous = null;
				trackEntry.OnEnd(this, index);
				if (this.End != null)
				{
					this.End(this, index);
				}
				entry.mixDuration = this.data.GetMix(trackEntry.animation, entry.animation);
				if (entry.mixDuration > 0f)
				{
					entry.mixTime = 0f;
					if (previous != null && trackEntry.mixTime / trackEntry.mixDuration < 0.5f)
					{
						entry.previous = previous;
					}
					else
					{
						entry.previous = trackEntry;
					}
				}
			}
			this.tracks.Items[index] = entry;
			entry.OnStart(this, index);
			if (this.Start != null)
			{
				this.Start(this, index);
			}
		}

		public TrackEntry SetAnimation(int trackIndex, string animationName, bool loop, Action callbackComplete, int templateId)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Animation not found:  name = ",
					animationName,
					", templateId = ",
					templateId
				}));
			}
			return this.SetAnimation(trackIndex, animation, loop, callbackComplete);
		}

		private TrackEntry SetAnimation(int trackIndex, Animation animation, bool loop, Action callbackComplete)
		{
			if (animation == null)
			{
			}
			TrackEntry trackEntry = new TrackEntry();
			trackEntry.animation = animation;
			trackEntry.loop = loop;
			trackEntry.CallbackComplete = callbackComplete;
			trackEntry.time = 0f;
			trackEntry.endTime = animation.Duration;
			this.SetCurrent(trackIndex, trackEntry);
			return trackEntry;
		}

		public TrackEntry AddAnimation(int trackIndex, string animationName, bool loop, float delay)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + animationName);
			}
			return this.AddAnimation(trackIndex, animation, loop, delay);
		}

		public TrackEntry AddAnimation(int trackIndex, Animation animation, bool loop, float delay)
		{
			if (animation == null)
			{
				throw new ArgumentException("animation cannot be null.");
			}
			TrackEntry trackEntry = new TrackEntry();
			trackEntry.animation = animation;
			trackEntry.loop = loop;
			trackEntry.time = 0f;
			trackEntry.endTime = animation.Duration;
			TrackEntry trackEntry2 = this.ExpandToIndex(trackIndex);
			if (trackEntry2 != null)
			{
				while (trackEntry2.next != null)
				{
					trackEntry2 = trackEntry2.next;
				}
				trackEntry2.next = trackEntry;
			}
			else
			{
				this.tracks.Items[trackIndex] = trackEntry;
			}
			if (delay <= 0f)
			{
				if (trackEntry2 != null)
				{
					delay += trackEntry2.endTime - this.data.GetMix(trackEntry2.animation, animation);
				}
				else
				{
					delay = 0f;
				}
			}
			trackEntry.delay = delay;
			return trackEntry;
		}

		public TrackEntry GetCurrent(int trackIndex)
		{
			if (trackIndex >= this.tracks.Count)
			{
				return null;
			}
			return this.tracks.Items[trackIndex];
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = this.tracks.Items[i];
				if (trackEntry != null)
				{
					if (stringBuilder.get_Length() > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(trackEntry.ToString());
				}
				i++;
			}
			if (stringBuilder.get_Length() == 0)
			{
				return "<none>";
			}
			return stringBuilder.ToString();
		}
	}
}
