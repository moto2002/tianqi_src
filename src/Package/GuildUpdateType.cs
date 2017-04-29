using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildUpdateType")]
	[Serializable]
	public class GuildUpdateType : IExtensible
	{
		[ProtoContract(Name = "GUT")]
		public enum GUT
		{
			[ProtoEnum(Name = "AddMember", Value = 1)]
			AddMember = 1,
			[ProtoEnum(Name = "RemMember", Value = 2)]
			RemMember
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
