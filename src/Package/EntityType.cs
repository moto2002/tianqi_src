using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "EntityType")]
	[Serializable]
	public class EntityType : IExtensible
	{
		[ProtoContract(Name = "ET")]
		public enum ET
		{
			[ProtoEnum(Name = "Role", Value = 1)]
			Role = 1,
			[ProtoEnum(Name = "Pet", Value = 2)]
			Pet
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
