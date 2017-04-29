using System;

namespace Spine
{
	public class MeshAttachment : Attachment
	{
		internal float[] vertices;

		internal float[] uvs;

		internal float[] regionUVs;

		internal int[] triangles;

		internal float regionOffsetX;

		internal float regionOffsetY;

		internal float regionWidth;

		internal float regionHeight;

		internal float regionOriginalWidth;

		internal float regionOriginalHeight;

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		public int HullLength
		{
			get;
			set;
		}

		public float[] Vertices
		{
			get
			{
				return this.vertices;
			}
			set
			{
				this.vertices = value;
			}
		}

		public float[] RegionUVs
		{
			get
			{
				return this.regionUVs;
			}
			set
			{
				this.regionUVs = value;
			}
		}

		public float[] UVs
		{
			get
			{
				return this.uvs;
			}
			set
			{
				this.uvs = value;
			}
		}

		public int[] Triangles
		{
			get
			{
				return this.triangles;
			}
			set
			{
				this.triangles = value;
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

		public string Path
		{
			get;
			set;
		}

		public object RendererObject
		{
			get;
			set;
		}

		public float RegionU
		{
			get;
			set;
		}

		public float RegionV
		{
			get;
			set;
		}

		public float RegionU2
		{
			get;
			set;
		}

		public float RegionV2
		{
			get;
			set;
		}

		public bool RegionRotate
		{
			get;
			set;
		}

		public float RegionOffsetX
		{
			get
			{
				return this.regionOffsetX;
			}
			set
			{
				this.regionOffsetX = value;
			}
		}

		public float RegionOffsetY
		{
			get
			{
				return this.regionOffsetY;
			}
			set
			{
				this.regionOffsetY = value;
			}
		}

		public float RegionWidth
		{
			get
			{
				return this.regionWidth;
			}
			set
			{
				this.regionWidth = value;
			}
		}

		public float RegionHeight
		{
			get
			{
				return this.regionHeight;
			}
			set
			{
				this.regionHeight = value;
			}
		}

		public float RegionOriginalWidth
		{
			get
			{
				return this.regionOriginalWidth;
			}
			set
			{
				this.regionOriginalWidth = value;
			}
		}

		public float RegionOriginalHeight
		{
			get
			{
				return this.regionOriginalHeight;
			}
			set
			{
				this.regionOriginalHeight = value;
			}
		}

		public int[] Edges
		{
			get;
			set;
		}

		public float Width
		{
			get;
			set;
		}

		public float Height
		{
			get;
			set;
		}

		public MeshAttachment(string name) : base(name)
		{
		}

		public void UpdateUVs()
		{
			float regionU = this.RegionU;
			float regionV = this.RegionV;
			float num = this.RegionU2 - this.RegionU;
			float num2 = this.RegionV2 - this.RegionV;
			float[] array = this.regionUVs;
			if (this.uvs == null || this.uvs.Length != array.Length)
			{
				this.uvs = new float[array.Length];
			}
			float[] array2 = this.uvs;
			if (this.RegionRotate)
			{
				int i = 0;
				int num3 = array2.Length;
				while (i < num3)
				{
					array2[i] = regionU + array[i + 1] * num;
					array2[i + 1] = regionV + num2 - array[i] * num2;
					i += 2;
				}
			}
			else
			{
				int j = 0;
				int num4 = array2.Length;
				while (j < num4)
				{
					array2[j] = regionU + array[j] * num;
					array2[j + 1] = regionV + array[j + 1] * num2;
					j += 2;
				}
			}
		}

		public void ComputeWorldVertices(Slot slot, float[] worldVertices)
		{
			Bone bone = slot.bone;
			float num = bone.skeleton.x + bone.worldX;
			float num2 = bone.skeleton.y + bone.worldY;
			float m = bone.m00;
			float m2 = bone.m01;
			float m3 = bone.m10;
			float m4 = bone.m11;
			float[] attachmentVertices = this.vertices;
			int num3 = attachmentVertices.Length;
			if (slot.attachmentVerticesCount == num3)
			{
				attachmentVertices = slot.AttachmentVertices;
			}
			for (int i = 0; i < num3; i += 2)
			{
				float num4 = attachmentVertices[i];
				float num5 = attachmentVertices[i + 1];
				worldVertices[i] = num4 * m + num5 * m2 + num;
				worldVertices[i + 1] = num4 * m3 + num5 * m4 + num2;
			}
		}
	}
}
