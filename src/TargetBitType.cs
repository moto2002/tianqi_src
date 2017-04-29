using System;

public enum TargetBitType : uint
{
	eBitSelfHuman = 1u,
	eBitSelfPet,
	eBitSelfMaster = 4u,
	eBitSelf = 7u,
	eBitTeamHuman,
	eBitTeamPet = 16u,
	eBitTeamMaster = 32u,
	eBitTeam = 56u,
	eBitEnemyHuman = 64u,
	eBitEnemyPet = 128u,
	eBitEnemyMaster = 256u,
	eBitEnemy = 448u,
	eBitSelfAndTeam = 63u,
	eBitAll = 511u
}
