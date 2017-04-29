using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILocalDimension
{
	void Update(float deltaTime);

	Vector3 GetSpawnPosition(int groupID);

	long GetSpiritCurHp(EntityParent entity);

	void SetSpiritCurHp(EntityParent entity, long curHp);

	bool GetSpiritIsDead(EntityParent entity);

	XDict<int, LocalDimensionPetSpirit> GetPetSpiritByOwnerID(long ownerID);

	void SummonPet(long ownerID, LocalDimensionPetSpirit spirit);

	void ReleasePet(LocalDimensionPetSpirit spirit, bool isDead = false);

	void RemovePetSummonRitualSkill(long ownerID, LocalDimensionPetSpirit spirit);

	void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, int pointGroupID, Quaternion casterRotation, List<int> offset);

	void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, Vector3 pos);
}
