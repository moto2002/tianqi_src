using System;

namespace Spine
{
	public class Slot
	{
		internal SlotData data;

		internal Bone bone;

		internal float r;

		internal float g;

		internal float b;

		internal float a;

		internal Attachment attachment;

		internal float attachmentTime;

		internal float[] attachmentVertices = new float[0];

		internal int attachmentVerticesCount;

		public SlotData Data
		{
			get
			{
				return this.data;
			}
		}

		public Bone Bone
		{
			get
			{
				return this.bone;
			}
		}

		public Skeleton Skeleton
		{
			get
			{
				return this.bone.skeleton;
			}
		}

		public float R
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		public float G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		public float B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		public float A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
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
				this.attachmentTime = this.bone.skeleton.time;
				this.attachmentVerticesCount = 0;
			}
		}

		public float AttachmentTime
		{
			get
			{
				return this.bone.skeleton.time - this.attachmentTime;
			}
			set
			{
				this.attachmentTime = this.bone.skeleton.time - value;
			}
		}

		public float[] AttachmentVertices
		{
			get
			{
				return this.attachmentVertices;
			}
			set
			{
				this.attachmentVertices = value;
			}
		}

		public int AttachmentVerticesCount
		{
			get
			{
				return this.attachmentVerticesCount;
			}
			set
			{
				this.attachmentVerticesCount = value;
			}
		}

		public Slot(SlotData data, Bone bone)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data cannot be null.");
			}
			if (bone == null)
			{
				throw new ArgumentNullException("bone cannot be null.");
			}
			this.data = data;
			this.bone = bone;
			this.SetToSetupPose();
		}

		internal void SetToSetupPose(int slotIndex)
		{
			this.r = this.data.r;
			this.g = this.data.g;
			this.b = this.data.b;
			this.a = this.data.a;
			this.Attachment = ((this.data.attachmentName != null) ? this.bone.skeleton.GetAttachment(slotIndex, this.data.attachmentName) : null);
		}

		public void SetToSetupPose()
		{
			this.SetToSetupPose(this.bone.skeleton.data.slots.IndexOf(this.data));
		}

		public override string ToString()
		{
			return this.data.name;
		}
	}
}
