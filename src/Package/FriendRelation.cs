using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "FriendRelation")]
	[Serializable]
	public class FriendRelation : IExtensible
	{
		[ProtoContract(Name = "FR")]
		public enum FR
		{
			[ProtoEnum(Name = "Buddy", Value = 0)]
			Buddy,
			[ProtoEnum(Name = "Black", Value = 1)]
			Black
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
