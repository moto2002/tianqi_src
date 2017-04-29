using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "WildBossStatus")]
	[Serializable]
	public class WildBossStatus : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Born", Value = 1)]
			Born = 1,
			[ProtoEnum(Name = "Challenge", Value = 2)]
			Challenge,
			[ProtoEnum(Name = "Death", Value = 3)]
			Death
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
