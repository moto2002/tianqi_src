using System;
using System.Collections.Generic;

namespace Spine
{
	public class Skin
	{
		private class AttachmentKeyTupleComparer : IEqualityComparer<Skin.AttachmentKeyTuple>
		{
			internal static readonly Skin.AttachmentKeyTupleComparer Instance = new Skin.AttachmentKeyTupleComparer();

			bool IEqualityComparer<Skin.AttachmentKeyTuple>.Equals(Skin.AttachmentKeyTuple o1, Skin.AttachmentKeyTuple o2)
			{
				return o1.SlotIndex == o2.SlotIndex && o1.NameHashCode == o2.NameHashCode && o1.Name == o2.Name;
			}

			int IEqualityComparer<Skin.AttachmentKeyTuple>.GetHashCode(Skin.AttachmentKeyTuple o)
			{
				return o.SlotIndex;
			}
		}

		private class AttachmentKeyTuple
		{
			public readonly int SlotIndex;

			public readonly string Name;

			public readonly int NameHashCode;

			public AttachmentKeyTuple(int slotIndex, string name)
			{
				this.SlotIndex = slotIndex;
				this.Name = name;
				this.NameHashCode = this.Name.GetHashCode();
			}
		}

		internal string name;

		private Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = new Dictionary<Skin.AttachmentKeyTuple, Attachment>(Skin.AttachmentKeyTupleComparer.Instance);

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public Skin(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name cannot be null.");
			}
			this.name = name;
		}

		public void AddAttachment(int slotIndex, string name, Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment cannot be null.");
			}
			this.attachments.set_Item(new Skin.AttachmentKeyTuple(slotIndex, name), attachment);
		}

		public Attachment GetAttachment(int slotIndex, string name)
		{
			Attachment result;
			this.attachments.TryGetValue(new Skin.AttachmentKeyTuple(slotIndex, name), ref result);
			return result;
		}

		public void FindNamesForSlot(int slotIndex, List<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names cannot be null.");
			}
			using (Dictionary<Skin.AttachmentKeyTuple, Attachment>.KeyCollection.Enumerator enumerator = this.attachments.get_Keys().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Skin.AttachmentKeyTuple current = enumerator.get_Current();
					if (current.SlotIndex == slotIndex)
					{
						names.Add(current.Name);
					}
				}
			}
		}

		public void FindAttachmentsForSlot(int slotIndex, List<Attachment> attachments)
		{
			if (attachments == null)
			{
				throw new ArgumentNullException("attachments cannot be null.");
			}
			using (Dictionary<Skin.AttachmentKeyTuple, Attachment>.Enumerator enumerator = this.attachments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Skin.AttachmentKeyTuple, Attachment> current = enumerator.get_Current();
					if (current.get_Key().SlotIndex == slotIndex)
					{
						attachments.Add(current.get_Value());
					}
				}
			}
		}

		public override string ToString()
		{
			return this.name;
		}

		internal void AttachAll(Skeleton skeleton, Skin oldSkin)
		{
			using (Dictionary<Skin.AttachmentKeyTuple, Attachment>.Enumerator enumerator = oldSkin.attachments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Skin.AttachmentKeyTuple, Attachment> current = enumerator.get_Current();
					int slotIndex = current.get_Key().SlotIndex;
					Slot slot = skeleton.slots.Items[slotIndex];
					if (slot.attachment == current.get_Value())
					{
						Attachment attachment = this.GetAttachment(slotIndex, current.get_Key().Name);
						if (attachment != null)
						{
							slot.Attachment = attachment;
						}
					}
				}
			}
		}
	}
}
