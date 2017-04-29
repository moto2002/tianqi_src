using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "FriendUpdateType")]
	[Serializable]
	public class FriendUpdateType : IExtensible
	{
		[ProtoContract(Name = "FUT")]
		public enum FUT
		{
			[ProtoEnum(Name = "Add", Value = 0)]
			Add,
			[ProtoEnum(Name = "Del", Value = 1)]
			Del,
			[ProtoEnum(Name = "Update", Value = 2)]
			Update
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
