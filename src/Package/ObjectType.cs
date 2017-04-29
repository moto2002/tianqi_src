using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ObjectType")]
	[Serializable]
	public class ObjectType : IExtensible
	{
		[ProtoContract(Name = "GameObject")]
		public enum GameObject
		{
			[ProtoEnum(Name = "PLAYER", Value = 0)]
			PLAYER,
			[ProtoEnum(Name = "PET", Value = 1)]
			PET
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
