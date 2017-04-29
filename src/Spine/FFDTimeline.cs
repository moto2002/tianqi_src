using System;

namespace Spine
{
	public class FFDTimeline : CurveTimeline
	{
		internal int slotIndex;

		internal float[] frames;

		private float[][] frameVertices;

		internal Attachment attachment;

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

		public float[][] Vertices
		{
			get
			{
				return this.frameVertices;
			}
			set
			{
				this.frameVertices = value;
			}
		}

		public Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
			set
			{
				this.attachment = value;
			}
		}

		public FFDTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount];
			this.frameVertices = new float[frameCount][];
		}

		public void SetFrame(int frameIndex, float time, float[] vertices)
		{
			this.frames[frameIndex] = time;
			this.frameVertices[frameIndex] = vertices;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (slot.attachment != this.attachment)
			{
				return;
			}
			float[] array = this.frames;
			if (time < array[0])
			{
				return;
			}
			float[][] array2 = this.frameVertices;
			int num = array2[0].Length;
			float[] array3 = slot.attachmentVertices;
			if (array3.Length < num)
			{
				array3 = new float[num];
				slot.attachmentVertices = array3;
			}
			if (array3.Length != num)
			{
				alpha = 1f;
			}
			slot.attachmentVerticesCount = num;
			if (time >= array[array.Length - 1])
			{
				float[] array4 = array2[array.Length - 1];
				if (alpha < 1f)
				{
					for (int i = 0; i < num; i++)
					{
						float num2 = array3[i];
						array3[i] = num2 + (array4[i] - num2) * alpha;
					}
				}
				else
				{
					Array.Copy(array4, 0, array3, 0, num);
				}
				return;
			}
			int num3 = Animation.binarySearch(array, time);
			float num4 = array[num3];
			float num5 = 1f - (time - num4) / (array[num3 - 1] - num4);
			num5 = base.GetCurvePercent(num3 - 1, (num5 >= 0f) ? ((num5 <= 1f) ? num5 : 1f) : 0f);
			float[] array5 = array2[num3 - 1];
			float[] array6 = array2[num3];
			if (alpha < 1f)
			{
				for (int j = 0; j < num; j++)
				{
					float num6 = array5[j];
					float num7 = array3[j];
					array3[j] = num7 + (num6 + (array6[j] - num6) * num5 - num7) * alpha;
				}
			}
			else
			{
				for (int k = 0; k < num; k++)
				{
					float num8 = array5[k];
					array3[k] = num8 + (array6[k] - num8) * num5;
				}
			}
		}
	}
}
