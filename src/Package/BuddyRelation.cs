using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BuddyRelation")]
	[Serializable]
	public class BuddyRelation : IExtensible
	{
		[ProtoContract(Name = "BR")]
		public enum BR
		{
			[ProtoEnum(Name = "Buddy", Value = 1)]
			Buddy = 1,
			[ProtoEnum(Name = "StrangerBlack", Value = 2)]
			StrangerBlack,
			[ProtoEnum(Name = "BuddyBlack", Value = 3)]
			BuddyBlack,
			[ProtoEnum(Name = "Stranger", Value = 4)]
			Stranger
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
