using System;

namespace Spine
{
	public class SlotData
	{
		internal string name;

		internal BoneData boneData;

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		internal string attachmentName;

		internal BlendMode blendMode;

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public BoneData BoneData
		{
			get
			{
				return this.boneData;
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

		public string AttachmentName
		{
			get
			{
				return this.attachmentName;
			}
			set
			{
				this.attachmentName = value;
			}
		}

		public BlendMode BlendMode
		{
			get
			{
				return this.blendMode;
			}
			set
			{
				this.blendMode = value;
			}
		}

		public SlotData(string name, BoneData boneData)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name cannot be null.");
			}
			if (boneData == null)
			{
				throw new ArgumentNullException("boneData cannot be null.");
			}
			this.name = name;
			this.boneData = boneData;
		}

		public override string ToString()
		{
			return this.name;
		}
	}
}
