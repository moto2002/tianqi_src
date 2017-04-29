using System;

namespace Spine
{
	public class Skeleton
	{
		internal SkeletonData data;

		internal ExposedList<Bone> bones;

		internal ExposedList<Slot> slots;

		internal ExposedList<Slot> drawOrder;

		internal ExposedList<IkConstraint> ikConstraints;

		private ExposedList<ExposedList<Bone>> boneCache = new ExposedList<ExposedList<Bone>>();

		internal Skin skin;

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		internal float time;

		internal bool flipX;

		internal bool flipY;

		internal float x;

		internal float y;

		public SkeletonData Data
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

		public ExposedList<Slot> Slots
		{
			get
			{
				return this.slots;
			}
		}

		public ExposedList<Slot> DrawOrder
		{
			get
			{
				return this.drawOrder;
			}
		}

		public ExposedList<IkConstraint> IkConstraints
		{
			get
			{
				return this.ikConstraints;
			}
			set
			{
				this.ikConstraints = value;
			}
		}

		public Skin Skin
		{
			get
			{
				return this.skin;
			}
			set
			{
				this.skin = value;
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

		public float Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
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

		public Bone RootBone
		{
			get
			{
				return (this.bones.Count != 0) ? this.bones.Items[0] : null;
			}
		}

		public Skeleton(SkeletonData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data cannot be null.");
			}
			this.data = data;
			this.bones = new ExposedList<Bone>(data.bones.Count);
			foreach (BoneData current in data.bones)
			{
				Bone bone = (current.parent != null) ? this.bones.Items[data.bones.IndexOf(current.parent)] : null;
				Bone item = new Bone(current, this, bone);
				if (bone != null)
				{
					bone.children.Add(item);
				}
				this.bones.Add(item);
			}
			this.slots = new ExposedList<Slot>(data.slots.Count);
			this.drawOrder = new ExposedList<Slot>(data.slots.Count);
			foreach (SlotData current2 in data.slots)
			{
				Bone bone2 = this.bones.Items[data.bones.IndexOf(current2.boneData)];
				Slot item2 = new Slot(current2, bone2);
				this.slots.Add(item2);
				this.drawOrder.Add(item2);
			}
			this.ikConstraints = new ExposedList<IkConstraint>(data.ikConstraints.Count);
			foreach (IkConstraintData current3 in data.ikConstraints)
			{
				this.ikConstraints.Add(new IkConstraint(current3, this));
			}
			this.UpdateCache();
			this.UpdateWorldTransform();
		}

		public void UpdateCache()
		{
			ExposedList<ExposedList<Bone>> exposedList = this.boneCache;
			ExposedList<IkConstraint> exposedList2 = this.ikConstraints;
			int count = exposedList2.Count;
			int num = count + 1;
			if (exposedList.Count > num)
			{
				exposedList.RemoveRange(num, exposedList.Count - num);
			}
			int i = 0;
			int count2 = exposedList.Count;
			while (i < count2)
			{
				exposedList.Items[i].Clear(true);
				i++;
			}
			while (exposedList.Count < num)
			{
				exposedList.Add(new ExposedList<Bone>());
			}
			ExposedList<Bone> exposedList3 = exposedList.Items[0];
			int j = 0;
			int count3 = this.bones.Count;
			while (j < count3)
			{
				Bone bone = this.bones.Items[j];
				Bone bone2 = bone;
				int k;
				while (true)
				{
					k = 0;
					IL_143:
					while (k < count)
					{
						IkConstraint ikConstraint = exposedList2.Items[k];
						Bone bone3 = ikConstraint.bones.Items[0];
						Bone bone4 = ikConstraint.bones.Items[ikConstraint.bones.Count - 1];
						while (bone2 != bone4)
						{
							if (bone4 == bone3)
							{
								k++;
								goto IL_143;
							}
							bone4 = bone4.parent;
						}
						goto Block_4;
					}
					bone2 = bone2.parent;
					if (bone2 == null)
					{
						goto Block_7;
					}
				}
				IL_164:
				j++;
				continue;
				Block_4:
				exposedList.Items[k].Add(bone);
				exposedList.Items[k + 1].Add(bone);
				goto IL_164;
				Block_7:
				exposedList3.Add(bone);
				goto IL_164;
			}
		}

		public void UpdateWorldTransform()
		{
			ExposedList<Bone> exposedList = this.bones;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Bone bone = exposedList.Items[i];
				bone.rotationIK = bone.rotation;
				i++;
			}
			ExposedList<ExposedList<Bone>> exposedList2 = this.boneCache;
			ExposedList<IkConstraint> exposedList3 = this.ikConstraints;
			int num = 0;
			int num2 = exposedList2.Count - 1;
			while (true)
			{
				ExposedList<Bone> exposedList4 = exposedList2.Items[num];
				int j = 0;
				int count2 = exposedList4.Count;
				while (j < count2)
				{
					exposedList4.Items[j].UpdateWorldTransform();
					j++;
				}
				if (num == num2)
				{
					break;
				}
				exposedList3.Items[num].apply();
				num++;
			}
		}

		public void SetToSetupPose()
		{
			this.SetBonesToSetupPose();
			this.SetSlotsToSetupPose();
		}

		public void SetBonesToSetupPose()
		{
			ExposedList<Bone> exposedList = this.bones;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				exposedList.Items[i].SetToSetupPose();
				i++;
			}
			ExposedList<IkConstraint> exposedList2 = this.ikConstraints;
			int j = 0;
			int count2 = exposedList2.Count;
			while (j < count2)
			{
				IkConstraint ikConstraint = exposedList2.Items[j];
				ikConstraint.bendDirection = ikConstraint.data.bendDirection;
				ikConstraint.mix = ikConstraint.data.mix;
				j++;
			}
		}

		public void SetSlotsToSetupPose()
		{
			ExposedList<Slot> exposedList = this.slots;
			this.drawOrder.Clear(true);
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				this.drawOrder.Add(exposedList.Items[i]);
				i++;
			}
			int j = 0;
			int count2 = exposedList.Count;
			while (j < count2)
			{
				exposedList.Items[j].SetToSetupPose(j);
				j++;
			}
		}

		public Bone FindBone(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName cannot be null.");
			}
			ExposedList<Bone> exposedList = this.bones;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Bone bone = exposedList.Items[i];
				if (bone.data.name == boneName)
				{
					return bone;
				}
				i++;
			}
			return null;
		}

		public int FindBoneIndex(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName cannot be null.");
			}
			ExposedList<Bone> exposedList = this.bones;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				if (exposedList.Items[i].data.name == boneName)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public Slot FindSlot(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Slot slot = exposedList.Items[i];
				if (slot.data.name == slotName)
				{
					return slot;
				}
				i++;
			}
			return null;
		}

		public int FindSlotIndex(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				if (exposedList.Items[i].data.name.Equals(slotName))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public void SetSkin(string skinName)
		{
			Skin skin = this.data.FindSkin(skinName);
			if (skin == null)
			{
				throw new ArgumentException("Skin not found: " + skinName);
			}
			this.SetSkin(skin);
		}

		public void SetSkin(Skin newSkin)
		{
			if (newSkin != null)
			{
				if (this.skin != null)
				{
					newSkin.AttachAll(this, this.skin);
				}
				else
				{
					ExposedList<Slot> exposedList = this.slots;
					int i = 0;
					int count = exposedList.Count;
					while (i < count)
					{
						Slot slot = exposedList.Items[i];
						string attachmentName = slot.data.attachmentName;
						if (attachmentName != null)
						{
							Attachment attachment = newSkin.GetAttachment(i, attachmentName);
							if (attachment != null)
							{
								slot.Attachment = attachment;
							}
						}
						i++;
					}
				}
			}
			this.skin = newSkin;
		}

		public Attachment GetAttachment(string slotName, string attachmentName)
		{
			return this.GetAttachment(this.data.FindSlotIndex(slotName), attachmentName);
		}

		public Attachment GetAttachment(int slotIndex, string attachmentName)
		{
			if (attachmentName == null)
			{
				throw new ArgumentNullException("attachmentName cannot be null.");
			}
			if (this.skin != null)
			{
				Attachment attachment = this.skin.GetAttachment(slotIndex, attachmentName);
				if (attachment != null)
				{
					return attachment;
				}
			}
			if (this.data.defaultSkin != null)
			{
				return this.data.defaultSkin.GetAttachment(slotIndex, attachmentName);
			}
			return null;
		}

		public void SetAttachment(string slotName, string attachmentName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Slot slot = exposedList.Items[i];
				if (slot.data.name == slotName)
				{
					Attachment attachment = null;
					if (attachmentName != null)
					{
						attachment = this.GetAttachment(i, attachmentName);
						if (attachment == null)
						{
							throw new Exception("Attachment not found: " + attachmentName + ", for slot: " + slotName);
						}
					}
					slot.Attachment = attachment;
					return;
				}
				i++;
			}
			throw new Exception("Slot not found: " + slotName);
		}

		public IkConstraint FindIkConstraint(string ikConstraintName)
		{
			if (ikConstraintName == null)
			{
				throw new ArgumentNullException("ikConstraintName cannot be null.");
			}
			ExposedList<IkConstraint> exposedList = this.ikConstraints;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				IkConstraint ikConstraint = exposedList.Items[i];
				if (ikConstraint.data.name == ikConstraintName)
				{
					return ikConstraint;
				}
				i++;
			}
			return null;
		}

		public void Update(float delta)
		{
			this.time += delta;
		}
	}
}
