using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "RankingType")]
	[Serializable]
	public class RankingType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Lv", Value = 1)]
			Lv = 1,
			[ProtoEnum(Name = "Fighting", Value = 2)]
			Fighting,
			[ProtoEnum(Name = "PetFighting", Value = 3)]
			PetFighting
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
