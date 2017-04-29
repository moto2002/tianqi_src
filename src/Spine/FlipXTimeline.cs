using System;

namespace Spine
{
	public class FlipXTimeline : Timeline
	{
		internal int boneIndex;

		internal float[] frames;

		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
			set
			{
				this.boneIndex = value;
			}
		}

		public float[] Frames
		{
			get
			{
				return this.frames;
			}
			set
			{
				this.frames = value;
			}
		}

		public int FrameCount
		{
			get
			{
				return this.frames.Length >> 1;
			}
		}

		public FlipXTimeline(int frameCount)
		{
			this.frames = new float[frameCount << 1];
		}

		public void SetFrame(int frameIndex, float time, bool flip)
		{
			frameIndex *= 2;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = (float)((!flip) ? 0 : 1);
		}

		public void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] array = this.frames;
			if (time < array[0])
			{
				if (lastTime > time)
				{
					this.Apply(skeleton, lastTime, 2.14748365E+09f, null, 0f);
				}
				return;
			}
			if (lastTime > time)
			{
				lastTime = -1f;
			}
			int num = ((time < array[array.Length - 2]) ? Animation.binarySearch(array, time, 2) : array.Length) - 2;
			if (array[num] < lastTime)
			{
				return;
			}
			this.SetFlip(skeleton.bones.Items[this.boneIndex], array[num + 1] != 0f);
		}

		protected virtual void SetFlip(Bone bone, bool flip)
		{
			bone.flipX = flip;
		}
	}
}
