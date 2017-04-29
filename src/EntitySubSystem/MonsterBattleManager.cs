using GameData;
using Package;
using System;
using UnityEngine;

namespace EntitySubSystem
{
	public class MonsterBattleManager : BattleManager
	{
		public override void Bleed(BattleAction_Bleed data, bool isServerData)
		{
			if (data.bleedSoldierId != this.owner.ID)
			{
				return;
			}
			if (EntityWorld.Instance.AllEntities.ContainsKey(data.dmgSrcSoldierId))
			{
				this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, EntityWorld.Instance.AllEntities[data.dmgSrcSoldierId], data.isCrt, data.isParry, data.isMiss));
				if (data.hp <= 0L && DataReader<AvatarModel>.Get(this.owner.FixModelID).noTurn == 0 && !this.owner.IsLogicBoss && this.owner.Actor && !this.owner.Actor.IsLockModelDir)
				{
					Vector3 position = EntityWorld.Instance.AllEntities[data.dmgSrcSoldierId].Actor.FixTransform.get_position();
					Vector3 vector = new Vector3(position.x - this.owner.Actor.FixTransform.get_position().x, 0f, position.z - this.owner.Actor.FixTransform.get_position().z);
					if (vector != Vector3.get_zero())
					{
						this.owner.Actor.ForceSetDirection(vector);
						this.owner.Actor.ApplyMovingDirAsForward();
					}
				}
			}
			else
			{
				this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, null, data.isCrt, data.isParry, data.isMiss));
			}
			this.owner.DamageSourceID = data.dmgSrcSoldierId;
			this.owner.Hp = data.hp;
		}

		public override void ChangeCamp(BattleAction_ChangeCamp data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.Camp = data.camp;
			if (this.owner.Actor)
			{
				if (this.owner.IsPlayerMate)
				{
					EventDispatcher.Broadcast<Transform>(CameraEvent.MonsterDie, this.owner.Actor.FixTransform);
				}
				else
				{
					EventDispatcher.Broadcast<int, Transform>(CameraEvent.MonsterBorn, this.owner.TypeID, this.owner.Actor.FixTransform);
				}
			}
		}
	}
}
