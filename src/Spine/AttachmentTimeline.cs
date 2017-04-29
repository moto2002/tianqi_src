using System;

namespace Spine
{
	public class AttachmentTimeline : Timeline
	{
		internal int slotIndex;

		internal float[] frames;

		private string[] attachmentNames;

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

		public string[] AttachmentNames
		{
			get
			{
				return this.attachmentNames;
			}
			set
			{
				this.attachmentNames = value;
			}
		}

		public int FrameCount
		{
			get
			{
				return this.frames.Length;
			}
		}

		public AttachmentTimeline(int frameCount)
		{
			this.frames = new float[frameCount];
			this.attachmentNames = new string[frameCount];
		}

		public void SetFrame(int frameIndex, float time, string attachmentName)
		{
			this.frames[frameIndex] = time;
			this.attachmentNames[frameIndex] = attachmentName;
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
			int num = ((time < array[array.Length - 1]) ? Animation.binarySearch(array, time) : array.Length) - 1;
			if (array[num] < lastTime)
			{
				return;
			}
			string text = this.attachmentNames[num];
			skeleton.slots.Items[this.slotIndex].Attachment = ((text != null) ? skeleton.GetAttachment(this.slotIndex, text) : null);
		}
	}
}
