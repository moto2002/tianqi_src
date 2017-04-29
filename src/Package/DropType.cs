using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DropType")]
	[Serializable]
	public class DropType : IExtensible
	{
		[ProtoContract(Name = "DT")]
		public enum DT
		{
			[ProtoEnum(Name = "Dungeon", Value = 1)]
			Dungeon = 1,
			[ProtoEnum(Name = "Gift", Value = 2)]
			Gift,
			[ProtoEnum(Name = "TaskPrize", Value = 3)]
			TaskPrize,
			[ProtoEnum(Name = "DailyTaskPrize", Value = 4)]
			DailyTaskPrize,
			[ProtoEnum(Name = "Vip", Value = 5)]
			Vip,
			[ProtoEnum(Name = "LevelUp", Value = 6)]
			LevelUp,
			[ProtoEnum(Name = "LuckDraw", Value = 7)]
			LuckDraw,
			[ProtoEnum(Name = "Activity", Value = 9)]
			Activity = 9,
			[ProtoEnum(Name = "Achievement", Value = 10)]
			Achievement,
			[ProtoEnum(Name = "Production", Value = 11)]
			Production,
			[ProtoEnum(Name = "HitMouse", Value = 12)]
			HitMouse,
			[ProtoEnum(Name = "GuildQuestion", Value = 13)]
			GuildQuestion,
			[ProtoEnum(Name = "GuildField", Value = 14)]
			GuildField,
			[ProtoEnum(Name = "PlatformGift", Value = 15)]
			PlatformGift,
			[ProtoEnum(Name = "GuildEquipBuild", Value = 16)]
			GuildEquipBuild,
			[ProtoEnum(Name = "GangFight", Value = 17)]
			GangFight,
			[ProtoEnum(Name = "RedPacket", Value = 18)]
			RedPacket
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
