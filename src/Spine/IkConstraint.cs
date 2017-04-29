using System;
using System.Collections.Generic;

namespace Spine
{
	public class IkConstraint
	{
		private const float radDeg = 57.2957764f;

		internal IkConstraintData data;

		internal ExposedList<Bone> bones = new ExposedList<Bone>();

		internal Bone target;

		internal int bendDirection;

		internal float mix;

		public IkConstraintData Data
		{
			get
			{
				return this.data;
			}
		}

		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		public Bone Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		public int BendDirection
		{
			get
			{
				return this.bendDirection;
			}
			set
			{
				this.bendDirection = value;
			}
		}

		public float Mix
		{
			get
			{
				return this.mix;
			}
			set
			{
				this.mix = value;
			}
		}

		public IkConstraint(IkConstraintData data, Skeleton skeleton)
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
			this.mix = data.mix;
			this.bendDirection = data.bendDirection;
			this.bones = new ExposedList<Bone>(data.bones.get_Count());
			using (List<BoneData>.Enumerator enumerator = data.bones.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BoneData current = enumerator.get_Current();
					this.bones.Add(skeleton.FindBone(current.name));
				}
			}
			this.target = skeleton.FindBone(data.target.name);
		}

		public void apply()
		{
			Bone bone = this.target;
			ExposedList<Bone> exposedList = this.bones;
			int count = exposedList.Count;
			if (count != 1)
			{
				if (count == 2)
				{
					IkConstraint.apply(exposedList.Items[0], exposedList.Items[1], bone.worldX, bone.worldY, this.bendDirection, this.mix);
				}
			}
			else
			{
				IkConstraint.apply(exposedList.Items[0], bone.worldX, bone.worldY, this.mix);
			}
		}

		public override string ToString()
		{
			return this.data.name;
		}

		public static void apply(Bone bone, float targetX, float targetY, float alpha)
		{
			float num = (bone.data.inheritRotation && bone.parent != null) ? bone.parent.worldRotation : 0f;
			float rotation = bone.rotation;
			float num2 = (float)Math.Atan2((double)(targetY - bone.worldY), (double)(targetX - bone.worldX)) * 57.2957764f;
			if (bone.worldFlipX != (bone.worldFlipY != Bone.yDown))
			{
				num2 = -num2;
			}
			num2 -= num;
			bone.rotationIK = rotation + (num2 - rotation) * alpha;
		}

		public static void apply(Bone parent, Bone child, float targetX, float targetY, int bendDirection, float alpha)
		{
			float rotation = child.rotation;
			float rotation2 = parent.rotation;
			if (alpha == 0f)
			{
				child.rotationIK = rotation;
				parent.rotationIK = rotation2;
				return;
			}
			Bone parent2 = parent.parent;
			float x;
			float y;
			if (parent2 != null)
			{
				parent2.worldToLocal(targetX, targetY, out x, out y);
				targetX = (x - parent.x) * parent2.worldScaleX;
				targetY = (y - parent.y) * parent2.worldScaleY;
			}
			else
			{
				targetX -= parent.x;
				targetY -= parent.y;
			}
			if (child.parent == parent)
			{
				x = child.x;
				y = child.y;
			}
			else
			{
				child.parent.localToWorld(child.x, child.y, out x, out y);
				parent.worldToLocal(x, y, out x, out y);
			}
			float num = x * parent.worldScaleX;
			float num2 = y * parent.worldScaleY;
			float num3 = (float)Math.Atan2((double)num2, (double)num);
			float num4 = (float)Math.Sqrt((double)(num * num + num2 * num2));
			float num5 = child.data.length * child.worldScaleX;
			float num6 = 2f * num4 * num5;
			if (num6 < 0.0001f)
			{
				child.rotationIK = rotation + ((float)Math.Atan2((double)targetY, (double)targetX) * 57.2957764f - rotation2 - rotation) * alpha;
				return;
			}
			float num7 = (targetX * targetX + targetY * targetY - num4 * num4 - num5 * num5) / num6;
			if (num7 < -1f)
			{
				num7 = -1f;
			}
			else if (num7 > 1f)
			{
				num7 = 1f;
			}
			float num8 = (float)Math.Acos((double)num7) * (float)bendDirection;
			float num9 = num4 + num5 * num7;
			float num10 = num5 * (float)Math.Sin((double)num8);
			float num11 = (float)Math.Atan2((double)(targetY * num9 - targetX * num10), (double)(targetX * num9 + targetY * num10));
			float num12 = (num11 - num3) * 57.2957764f - rotation2;
			if (num12 > 180f)
			{
				num12 -= 360f;
			}
			else if (num12 < -180f)
			{
				num12 += 360f;
			}
			parent.rotationIK = rotation2 + num12 * alpha;
			num12 = (num8 + num3) * 57.2957764f - rotation;
			if (num12 > 180f)
			{
				num12 -= 360f;
			}
			else if (num12 < -180f)
			{
				num12 += 360f;
			}
			child.rotationIK = rotation + (num12 + parent.worldRotation - child.parent.worldRotation) * alpha;
		}
	}
}
