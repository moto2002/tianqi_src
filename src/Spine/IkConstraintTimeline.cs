using System;

namespace Spine
{
	public class IkConstraintTimeline : CurveTimeline
	{
		private const int PREV_FRAME_TIME = -3;

		private const int PREV_FRAME_MIX = -2;

		private const int PREV_FRAME_BEND_DIRECTION = -1;

		private const int FRAME_MIX = 1;

		internal int ikConstraintIndex;

		internal float[] frames;

		public int IkConstraintIndex
		{
			get
			{
				return this.ikConstraintIndex;
			}
			set
			{
				this.ikConstraintIndex = value;
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

		public IkConstraintTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 3];
		}

		public void SetFrame(int frameIndex, float time, float mix, int bendDirection)
		{
			frameIndex *= 3;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = mix;
			this.frames[frameIndex + 2] = (float)bendDirection;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] array = this.frames;
			if (time < array[0])
			{
				return;
			}
			IkConstraint ikConstraint = skeleton.ikConstraints.Items[this.ikConstraintIndex];
			if (time >= array[array.Length - 3])
			{
				ikConstraint.mix += (array[array.Length - 2] - ikConstraint.mix) * alpha;
				ikConstraint.bendDirection = (int)array[array.Length - 1];
				return;
			}
			int num = Animation.binarySearch(array, time, 3);
			float num2 = array[num + -2];
			float num3 = array[num];
			float num4 = 1f - (time - num3) / (array[num + -3] - num3);
			num4 = base.GetCurvePercent(num / 3 - 1, (num4 >= 0f) ? ((num4 <= 1f) ? num4 : 1f) : 0f);
			float num5 = num2 + (array[num + 1] - num2) * num4;
			ikConstraint.mix += (num5 - ikConstraint.mix) * alpha;
			ikConstraint.bendDirection = (int)array[num + -1];
		}
	}
}
