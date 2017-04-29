using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PetFormationType")]
	[Serializable]
	public class PetFormationType : IExtensible
	{
		[ProtoContract(Name = "FORMATION_TYPE")]
		public enum FORMATION_TYPE
		{
			[ProtoEnum(Name = "Normal", Value = 1)]
			Normal = 1,
			[ProtoEnum(Name = "Elite", Value = 2)]
			Elite,
			[ProtoEnum(Name = "Arena", Value = 3)]
			Arena,
			[ProtoEnum(Name = "Gang", Value = 4)]
			Gang,
			[ProtoEnum(Name = "PVE", Value = 5)]
			PVE,
			[ProtoEnum(Name = "GuildWar", Value = 6)]
			GuildWar,
			[ProtoEnum(Name = "Survival", Value = 7)]
			Survival,
			[ProtoEnum(Name = "ElementCopy", Value = 8)]
			ElementCopy,
			[ProtoEnum(Name = "PVP", Value = 9)]
			PVP,
			[ProtoEnum(Name = "CreateFight", Value = 10)]
			CreateFight,
			[ProtoEnum(Name = "Defend", Value = 11)]
			Defend,
			[ProtoEnum(Name = "Bounty", Value = 12)]
			Bounty,
			[ProtoEnum(Name = "MultiPve", Value = 122)]
			MultiPve = 122
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
