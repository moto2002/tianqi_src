using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SubTab")]
	[Serializable]
	public class SubTab : IExtensible
	{
		[ProtoContract(Name = "ST")]
		public enum ST
		{
			[ProtoEnum(Name = "Lv", Value = 1)]
			Lv = 1,
			[ProtoEnum(Name = "Fighting", Value = 2)]
			Fighting,
			[ProtoEnum(Name = "EliteDungeon", Value = 3)]
			EliteDungeon,
			[ProtoEnum(Name = "StrengthenEquip", Value = 4)]
			StrengthenEquip,
			[ProtoEnum(Name = "DressGem", Value = 5)]
			DressGem,
			[ProtoEnum(Name = "UpStartEquip", Value = 6)]
			UpStartEquip,
			[ProtoEnum(Name = "PetLv", Value = 7)]
			PetLv,
			[ProtoEnum(Name = "PetUpStage", Value = 8)]
			PetUpStage
		}

		public static readonly short OP = 233;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
