using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleHurtInfoType")]
	public enum BattleHurtInfoType
	{
		[ProtoEnum(Name = "RoleMakeBossHurt", Value = 1)]
		RoleMakeBossHurt = 1,
		[ProtoEnum(Name = "RoleBeBossHurt", Value = 2)]
		RoleBeBossHurt,
		[ProtoEnum(Name = "RoleBePkHurt", Value = 3)]
		RoleBePkHurt,
		[ProtoEnum(Name = "RoleMakeTotalHurt", Value = 4)]
		RoleMakeTotalHurt
	}
}
