using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BuddyUpdateType")]
	[Serializable]
	public class BuddyUpdateType : IExtensible
	{
		[ProtoContract(Name = "BUT")]
		public enum BUT
		{
			[ProtoEnum(Name = "Add", Value = 0)]
			Add,
			[ProtoEnum(Name = "Del", Value = 1)]
			Del,
			[ProtoEnum(Name = "Update", Value = 3)]
			Update = 3
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
