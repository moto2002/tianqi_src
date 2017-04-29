using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "StrongerPowerType")]
	[Serializable]
	public class StrongerPowerType : IExtensible
	{
		[ProtoContract(Name = "PowerType")]
		public enum PowerType
		{
			[ProtoEnum(Name = "EQUIPQUALITY", Value = 1)]
			EQUIPQUALITY = 1,
			[ProtoEnum(Name = "INTENSIFYLEVEL", Value = 2)]
			INTENSIFYLEVEL,
			[ProtoEnum(Name = "GEMLEVEL", Value = 3)]
			GEMLEVEL,
			[ProtoEnum(Name = "SKILLLEVEL", Value = 4)]
			SKILLLEVEL,
			[ProtoEnum(Name = "PETLEVEL", Value = 5)]
			PETLEVEL,
			[ProtoEnum(Name = "PETSTAR", Value = 6)]
			PETSTAR,
			[ProtoEnum(Name = "PETSKILL", Value = 7)]
			PETSKILL
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
