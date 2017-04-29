using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "MailType")]
	[Serializable]
	public class MailType : IExtensible
	{
		[ProtoContract(Name = "MT")]
		public enum MT
		{
			[ProtoEnum(Name = "Faction", Value = 0)]
			Faction,
			[ProtoEnum(Name = "Private", Value = 1)]
			Private,
			[ProtoEnum(Name = "System", Value = 2)]
			System
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
