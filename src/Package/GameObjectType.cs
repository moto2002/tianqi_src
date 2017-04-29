using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GameObjectType")]
	[Serializable]
	public class GameObjectType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Role", Value = 0)]
			Role,
			[ProtoEnum(Name = "Monster", Value = 1)]
			Monster,
			[ProtoEnum(Name = "Pet", Value = 2)]
			Pet,
			[ProtoEnum(Name = "Soldier", Value = 3)]
			Soldier,
			[ProtoEnum(Name = "Map", Value = 4)]
			Map,
			[ProtoEnum(Name = "OtherType", Value = 5)]
			OtherType
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
