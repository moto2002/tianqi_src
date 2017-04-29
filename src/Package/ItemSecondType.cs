using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ItemSecondType")]
	[Serializable]
	public class ItemSecondType : IExtensible
	{
		[ProtoContract(Name = "IST")]
		public enum IST
		{
			[ProtoEnum(Name = "Role_equipment", Value = 1)]
			Role_equipment = 1,
			[ProtoEnum(Name = "Pet_equipment", Value = 2)]
			Pet_equipment,
			[ProtoEnum(Name = "Wing", Value = 3)]
			Wing,
			[ProtoEnum(Name = "Fashion", Value = 4)]
			Fashion,
			[ProtoEnum(Name = "Pet_fragment", Value = 5)]
			Pet_fragment,
			[ProtoEnum(Name = "Wing_fragment", Value = 6)]
			Wing_fragment,
			[ProtoEnum(Name = "Fashion_fragment", Value = 7)]
			Fashion_fragment,
			[ProtoEnum(Name = "Gold", Value = 8)]
			Gold,
			[ProtoEnum(Name = "Energy", Value = 9)]
			Energy,
			[ProtoEnum(Name = "Gift", Value = 10)]
			Gift,
			[ProtoEnum(Name = "Key", Value = 11)]
			Key,
			[ProtoEnum(Name = "Element", Value = 12)]
			Element,
			[ProtoEnum(Name = "Material", Value = 13)]
			Material,
			[ProtoEnum(Name = "MF", Value = 14)]
			MF,
			[ProtoEnum(Name = "GoldValue", Value = 15)]
			GoldValue,
			[ProtoEnum(Name = "Exp", Value = 16)]
			Exp,
			[ProtoEnum(Name = "DiamondValue", Value = 17)]
			DiamondValue,
			[ProtoEnum(Name = "EnergyValue", Value = 18)]
			EnergyValue,
			[ProtoEnum(Name = "CompetitiveCurrency", Value = 19)]
			CompetitiveCurrency,
			[ProtoEnum(Name = "ObtainPet", Value = 40)]
			ObtainPet = 40,
			[ProtoEnum(Name = "Talent", Value = 201)]
			Talent = 201,
			[ProtoEnum(Name = "Honor", Value = 202)]
			Honor,
			[ProtoEnum(Name = "SkillPoint", Value = 203)]
			SkillPoint,
			[ProtoEnum(Name = "CostBox", Value = 44)]
			CostBox = 44,
			[ProtoEnum(Name = "Reputation", Value = 61)]
			Reputation = 61
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
