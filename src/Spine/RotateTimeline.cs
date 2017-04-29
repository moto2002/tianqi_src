using System;

namespace Spine
{
	public class RotateTimeline : CurveTimeline
	{
		protected const int PREV_FRAME_TIME = -2;

		protected const int FRAME_VALUE = 1;

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

		public RotateTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount << 1];
		}

		public void SetFrame(int frameIndex, float time, float angle)
		{
			frameIndex *= 2;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = angle;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] array = this.frames;
			if (time < array[0])
			{
				return;
			}
			Bone bone = skeleton.bones.Items[this.boneIndex];
			float num;
			if (time >= array[array.Length - 2])
			{
				for (num = bone.data.rotation + array[array.Length - 1] - bone.rotation; num > 180f; num -= 360f)
				{
				}
				while (num < -180f)
				{
					num += 360f;
				}
				bone.rotation += num * alpha;
				return;
			}
			int num2 = Animation.binarySearch(array, time, 2);
			float num3 = array[num2 - 1];
			float num4 = array[num2];
			float num5 = 1f - (time - num4) / (array[num2 + -2] - num4);
			num5 = base.GetCurvePercent((num2 >> 1) - 1, (num5 >= 0f) ? ((num5 <= 1f) ? num5 : 1f) : 0f);
			for (num = array[num2 + 1] - num3; num > 180f; num -= 360f)
			{
			}
			while (num < -180f)
			{
				num += 360f;
			}
			for (num = bone.data.rotation + (num3 + num * num5) - bone.rotation; num > 180f; num -= 360f)
			{
			}
			while (num < -180f)
			{
				num += 360f;
			}
			bone.rotation += num * alpha;
		}
	}
}
