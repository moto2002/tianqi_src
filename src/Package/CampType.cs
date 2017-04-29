using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CampType")]
	[Serializable]
	public class CampType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Natural", Value = 0)]
			Natural,
			[ProtoEnum(Name = "PlayerLeft", Value = 1)]
			PlayerLeft,
			[ProtoEnum(Name = "PlayerRight", Value = 2)]
			PlayerRight,
			[ProtoEnum(Name = "Monster", Value = 3)]
			Monster
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
