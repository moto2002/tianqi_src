using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "OpenServerType")]
	[Serializable]
	public class OpenServerType : IExtensible
	{
		[ProtoContract(Name = "acType")]
		public enum acType
		{
			[ProtoEnum(Name = "Level", Value = 1)]
			Level = 1,
			[ProtoEnum(Name = "Wings", Value = 2)]
			Wings,
			[ProtoEnum(Name = "PetFighting", Value = 3)]
			PetFighting,
			[ProtoEnum(Name = "GodWeapon", Value = 4)]
			GodWeapon,
			[ProtoEnum(Name = "GemLv", Value = 5)]
			GemLv,
			[ProtoEnum(Name = "Recharge", Value = 6)]
			Recharge,
			[ProtoEnum(Name = "RoleFighting", Value = 7)]
			RoleFighting
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
