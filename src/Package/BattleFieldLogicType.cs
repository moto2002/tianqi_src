using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleFieldLogicType")]
	[Serializable]
	public class BattleFieldLogicType : IExtensible
	{
		[ProtoContract(Name = "BFLT")]
		public enum BFLT
		{
			[ProtoEnum(Name = "Normal", Value = 1)]
			Normal = 1,
			[ProtoEnum(Name = "Elite", Value = 2)]
			Elite,
			[ProtoEnum(Name = "Arena", Value = 3)]
			Arena,
			[ProtoEnum(Name = "Gang", Value = 4)]
			Gang,
			[ProtoEnum(Name = "Pve", Value = 5)]
			Pve,
			[ProtoEnum(Name = "GuildWar", Value = 6)]
			GuildWar,
			[ProtoEnum(Name = "Survival", Value = 7)]
			Survival,
			[ProtoEnum(Name = "ElementCopy", Value = 8)]
			ElementCopy,
			[ProtoEnum(Name = "PVP", Value = 9)]
			PVP,
			[ProtoEnum(Name = "FirstBattle", Value = 10)]
			FirstBattle,
			[ProtoEnum(Name = "Defend", Value = 11)]
			Defend,
			[ProtoEnum(Name = "Bounty", Value = 12)]
			Bounty,
			[ProtoEnum(Name = "MainCityMirror", Value = 13)]
			MainCityMirror,
			[ProtoEnum(Name = "WildBoss", Value = 14)]
			WildBoss,
			[ProtoEnum(Name = "ExperienceCopy", Value = 15)]
			ExperienceCopy,
			[ProtoEnum(Name = "WildBossSvr", Value = 16)]
			WildBossSvr,
			[ProtoEnum(Name = "GuildBoss", Value = 17)]
			GuildBoss,
			[ProtoEnum(Name = "Hook", Value = 18)]
			Hook,
			[ProtoEnum(Name = "MultiPvp", Value = 19)]
			MultiPvp
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
