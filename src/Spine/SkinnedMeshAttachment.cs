using System;

namespace Spine
{
	public class SkinnedMeshAttachment : Attachment
	{
		internal int[] bones;

		internal float[] weights;

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

		public int[] Bones
		{
			get
			{
				return this.bones;
			}
			set
			{
				this.bones = value;
			}
		}

		public float[] Weights
		{
			get
			{
				return this.weights;
			}
			set
			{
				this.weights = value;
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

		public SkinnedMeshAttachment(string name) : base(name)
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
			Skeleton skeleton = slot.bone.skeleton;
			ExposedList<Bone> exposedList = skeleton.bones;
			float x = skeleton.x;
			float y = skeleton.y;
			float[] array = this.weights;
			int[] array2 = this.bones;
			if (slot.attachmentVerticesCount == 0)
			{
				int num = 0;
				int i = 0;
				int num2 = 0;
				int num3 = array2.Length;
				while (i < num3)
				{
					float num4 = 0f;
					float num5 = 0f;
					int num6 = array2[i++] + i;
					while (i < num6)
					{
						Bone bone = exposedList.Items[array2[i]];
						float num7 = array[num2];
						float num8 = array[num2 + 1];
						float num9 = array[num2 + 2];
						num4 += (num7 * bone.m00 + num8 * bone.m01 + bone.worldX) * num9;
						num5 += (num7 * bone.m10 + num8 * bone.m11 + bone.worldY) * num9;
						i++;
						num2 += 3;
					}
					worldVertices[num] = num4 + x;
					worldVertices[num + 1] = num5 + y;
					num += 2;
				}
			}
			else
			{
				float[] attachmentVertices = slot.AttachmentVertices;
				int num10 = 0;
				int j = 0;
				int num11 = 0;
				int num12 = 0;
				int num13 = array2.Length;
				while (j < num13)
				{
					float num14 = 0f;
					float num15 = 0f;
					int num16 = array2[j++] + j;
					while (j < num16)
					{
						Bone bone2 = exposedList.Items[array2[j]];
						float num17 = array[num11] + attachmentVertices[num12];
						float num18 = array[num11 + 1] + attachmentVertices[num12 + 1];
						float num19 = array[num11 + 2];
						num14 += (num17 * bone2.m00 + num18 * bone2.m01 + bone2.worldX) * num19;
						num15 += (num17 * bone2.m10 + num18 * bone2.m11 + bone2.worldY) * num19;
						j++;
						num11 += 3;
						num12 += 2;
					}
					worldVertices[num10] = num14 + x;
					worldVertices[num10 + 1] = num15 + y;
					num10 += 2;
				}
			}
		}
	}
}
