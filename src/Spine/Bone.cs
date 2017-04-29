using System;

namespace Spine
{
	public class Bone
	{
		public static bool yDown;

		internal BoneData data;

		internal Skeleton skeleton;

		internal Bone parent;

		internal ExposedList<Bone> children = new ExposedList<Bone>();

		internal float x;

		internal float y;

		internal float rotation;

		internal float rotationIK;

		internal float scaleX;

		internal float scaleY;

		internal bool flipX;

		internal bool flipY;

		internal float m00;

		internal float m01;

		internal float m10;

		internal float m11;

		internal float worldX;

		internal float worldY;

		internal float worldRotation;

		internal float worldScaleX;

		internal float worldScaleY;

		internal bool worldFlipX;

		internal bool worldFlipY;

		public BoneData Data
		{
			get
			{
				return this.data;
			}
		}

		public Skeleton Skeleton
		{
			get
			{
				return this.skeleton;
			}
		}

		public Bone Parent
		{
			get
			{
				return this.parent;
			}
		}

		public ExposedList<Bone> Children
		{
			get
			{
				return this.children;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		public float Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		public float RotationIK
		{
			get
			{
				return this.rotationIK;
			}
			set
			{
				this.rotationIK = value;
			}
		}

		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		public bool FlipX
		{
			get
			{
				return this.flipX;
			}
			set
			{
				this.flipX = value;
			}
		}

		public bool FlipY
		{
			get
			{
				return this.flipY;
			}
			set
			{
				this.flipY = value;
			}
		}

		public float M00
		{
			get
			{
				return this.m00;
			}
		}

		public float M01
		{
			get
			{
				return this.m01;
			}
		}

		public float M10
		{
			get
			{
				return this.m10;
			}
		}

		public float M11
		{
			get
			{
				return this.m11;
			}
		}

		public float WorldX
		{
			get
			{
				return this.worldX;
			}
		}

		public float WorldY
		{
			get
			{
				return this.worldY;
			}
		}

		public float WorldRotation
		{
			get
			{
				return this.worldRotation;
			}
		}

		public float WorldScaleX
		{
			get
			{
				return this.worldScaleX;
			}
		}

		public float WorldScaleY
		{
			get
			{
				return this.worldScaleY;
			}
		}

		public bool WorldFlipX
		{
			get
			{
				return this.worldFlipX;
			}
			set
			{
				this.worldFlipX = value;
			}
		}

		public bool WorldFlipY
		{
			get
			{
				return this.worldFlipY;
			}
			set
			{
				this.worldFlipY = value;
			}
		}

		public Bone(BoneData data, Skeleton skeleton, Bone parent)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton cannot be null.");
			}
			this.data = data;
			this.skeleton = skeleton;
			this.parent = parent;
			this.SetToSetupPose();
		}

		public void UpdateWorldTransform()
		{
			Bone bone = this.parent;
			float num = this.x;
			float num2 = this.y;
			if (bone != null)
			{
				this.worldX = num * bone.m00 + num2 * bone.m01 + bone.worldX;
				this.worldY = num * bone.m10 + num2 * bone.m11 + bone.worldY;
				if (this.data.inheritScale)
				{
					this.worldScaleX = bone.worldScaleX * this.scaleX;
					this.worldScaleY = bone.worldScaleY * this.scaleY;
				}
				else
				{
					this.worldScaleX = this.scaleX;
					this.worldScaleY = this.scaleY;
				}
				this.worldRotation = ((!this.data.inheritRotation) ? this.rotationIK : (bone.worldRotation + this.rotationIK));
				this.worldFlipX = (bone.worldFlipX != this.flipX);
				this.worldFlipY = (bone.worldFlipY != this.flipY);
			}
			else
			{
				Skeleton skeleton = this.skeleton;
				bool flag = skeleton.flipX;
				bool flag2 = skeleton.flipY;
				this.worldX = ((!flag) ? num : (-num));
				this.worldY = ((flag2 == Bone.yDown) ? num2 : (-num2));
				this.worldScaleX = this.scaleX;
				this.worldScaleY = this.scaleY;
				this.worldRotation = this.rotationIK;
				this.worldFlipX = (flag != this.flipX);
				this.worldFlipY = (flag2 != this.flipY);
			}
			float num3 = this.worldRotation * 3.14159274f / 180f;
			float num4 = (float)Math.Cos((double)num3);
			float num5 = (float)Math.Sin((double)num3);
			if (this.worldFlipX)
			{
				this.m00 = -num4 * this.worldScaleX;
				this.m01 = num5 * this.worldScaleY;
			}
			else
			{
				this.m00 = num4 * this.worldScaleX;
				this.m01 = -num5 * this.worldScaleY;
			}
			if (this.worldFlipY != Bone.yDown)
			{
				this.m10 = -num5 * this.worldScaleX;
				this.m11 = -num4 * this.worldScaleY;
			}
			else
			{
				this.m10 = num5 * this.worldScaleX;
				this.m11 = num4 * this.worldScaleY;
			}
		}

		public void SetToSetupPose()
		{
			BoneData boneData = this.data;
			this.x = boneData.x;
			this.y = boneData.y;
			this.rotation = boneData.rotation;
			this.rotationIK = this.rotation;
			this.scaleX = boneData.scaleX;
			this.scaleY = boneData.scaleY;
			this.flipX = boneData.flipX;
			this.flipY = boneData.flipY;
		}

		public void worldToLocal(float worldX, float worldY, out float localX, out float localY)
		{
			float num = worldX - this.worldX;
			float num2 = worldY - this.worldY;
			float num3 = this.m00;
			float num4 = this.m10;
			float num5 = this.m01;
			float num6 = this.m11;
			if (this.worldFlipX != (this.worldFlipY != Bone.yDown))
			{
				num3 = -num3;
				num6 = -num6;
			}
			float num7 = 1f / (num3 * num6 - num5 * num4);
			localX = num * num3 * num7 - num2 * num5 * num7;
			localY = num2 * num6 * num7 - num * num4 * num7;
		}

		public void localToWorld(float localX, float localY, out float worldX, out float worldY)
		{
			worldX = localX * this.m00 + localY * this.m01 + this.worldX;
			worldY = localX * this.m10 + localY * this.m11 + this.worldY;
		}

		public override string ToString()
		{
			return this.data.name;
		}
	}
}
