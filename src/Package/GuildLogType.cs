using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildLogType")]
	[Serializable]
	public class GuildLogType : IExtensible
	{
		[ProtoContract(Name = "GDLT")]
		public enum GDLT
		{
			[ProtoEnum(Name = "GuildCreate", Value = 1)]
			GuildCreate = 1,
			[ProtoEnum(Name = "GuildJoin", Value = 2)]
			GuildJoin,
			[ProtoEnum(Name = "GuildQuit", Value = 3)]
			GuildQuit,
			[ProtoEnum(Name = "GuildAppoint", Value = 4)]
			GuildAppoint,
			[ProtoEnum(Name = "GuildShop", Value = 5)]
			GuildShop,
			[ProtoEnum(Name = "GuildShoping", Value = 6)]
			GuildShoping,
			[ProtoEnum(Name = "GuildLevelUp", Value = 7)]
			GuildLevelUp,
			[ProtoEnum(Name = "GuildBuild", Value = 8)]
			GuildBuild,
			[ProtoEnum(Name = "GuildTrained", Value = 9)]
			GuildTrained,
			[ProtoEnum(Name = "GuildWar", Value = 10)]
			GuildWar,
			[ProtoEnum(Name = "GuildEquipBuild", Value = 11)]
			GuildEquipBuild,
			[ProtoEnum(Name = "GuildStorageEquipDonate", Value = 12)]
			GuildStorageEquipDonate
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
