using System;

namespace Spine
{
	public class BoundingBoxAttachment : Attachment
	{
		internal float[] vertices;

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

		public BoundingBoxAttachment(string name) : base(name)
		{
		}

		public void ComputeWorldVertices(Bone bone, float[] worldVertices)
		{
			float num = bone.skeleton.x + bone.worldX;
			float num2 = bone.skeleton.y + bone.worldY;
			float m = bone.m00;
			float m2 = bone.m01;
			float m3 = bone.m10;
			float m4 = bone.m11;
			float[] array = this.vertices;
			int i = 0;
			int num3 = array.Length;
			while (i < num3)
			{
				float num4 = array[i];
				float num5 = array[i + 1];
				worldVertices[i] = num4 * m + num5 * m2 + num;
				worldVertices[i + 1] = num4 * m3 + num5 * m4 + num2;
				i += 2;
			}
		}
	}
}
