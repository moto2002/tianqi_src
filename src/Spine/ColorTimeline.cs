using System;

namespace Spine
{
	public class ColorTimeline : CurveTimeline
	{
		protected const int PREV_FRAME_TIME = -5;

		protected const int FRAME_R = 1;

		protected const int FRAME_G = 2;

		protected const int FRAME_B = 3;

		protected const int FRAME_A = 4;

		internal int slotIndex;

		internal float[] frames;

		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
			set
			{
				this.slotIndex = value;
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

		public ColorTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 5];
		}

		public void SetFrame(int frameIndex, float time, float r, float g, float b, float a)
		{
			frameIndex *= 5;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = r;
			this.frames[frameIndex + 2] = g;
			this.frames[frameIndex + 3] = b;
			this.frames[frameIndex + 4] = a;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] array = this.frames;
			if (time < array[0])
			{
				return;
			}
			float num2;
			float num3;
			float num4;
			float num5;
			if (time >= array[array.Length - 5])
			{
				int num = array.Length - 1;
				num2 = array[num - 3];
				num3 = array[num - 2];
				num4 = array[num - 1];
				num5 = array[num];
			}
			else
			{
				int num6 = Animation.binarySearch(array, time, 5);
				float num7 = array[num6 - 4];
				float num8 = array[num6 - 3];
				float num9 = array[num6 - 2];
				float num10 = array[num6 - 1];
				float num11 = array[num6];
				float num12 = 1f - (time - num11) / (array[num6 + -5] - num11);
				num12 = base.GetCurvePercent(num6 / 5 - 1, (num12 >= 0f) ? ((num12 <= 1f) ? num12 : 1f) : 0f);
				num2 = num7 + (array[num6 + 1] - num7) * num12;
				num3 = num8 + (array[num6 + 2] - num8) * num12;
				num4 = num9 + (array[num6 + 3] - num9) * num12;
				num5 = num10 + (array[num6 + 4] - num10) * num12;
			}
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (alpha < 1f)
			{
				slot.r += (num2 - slot.r) * alpha;
				slot.g += (num3 - slot.g) * alpha;
				slot.b += (num4 - slot.b) * alpha;
				slot.a += (num5 - slot.a) * alpha;
			}
			else
			{
				slot.r = num2;
				slot.g = num3;
				slot.b = num4;
				slot.a = num5;
			}
		}
	}
}
