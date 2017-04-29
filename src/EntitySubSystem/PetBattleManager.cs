using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineCommand;

namespace EntitySubSystem
{
	public class PetBattleManager : BattleManager
	{
		public override void PetEnterBattleField(BattleAction_PetEnterField data, bool isServerData)
		{
			if (data.petId != this.owner.ID)
			{
				return;
			}
			if (this.owner.IsDead)
			{
				return;
			}
			this.owner.GetSkillManager().SetDebutCD();
			(this.owner as EntityPet).ExistTime = (float)data.exitTime;
			if (this.owner.Owner != null && this.owner.Owner.IsEntitySelfType)
			{
				BattleBlackboard.Instance.SetPetCountDown(this.owner.OwnerListIdx, new KeyValuePair<float, DateTime>((float)(data.exitTime * 1000), DateTime.get_Now()));
			}
			this.owner.IsFighting = true;
			this.owner.IsFuse = false;
			if (InstanceManager.CurrentCommunicationType == CommunicationType.Mixed && this.owner.Owner != null && this.owner.Owner.Actor)
			{
				this.owner.Pos = LocalInstanceHandler.Instance.GetPetRandomPos(this.owner.TypeID, this.owner.Owner.Actor.FixTransform.get_position(), this.owner.Owner.Actor.FixTransform.get_rotation());
				this.owner.Dir = LocalInstanceHandler.Instance.GetPetDir(this.owner.Owner.Actor.FixTransform.get_rotation());
			}
			else
			{
				this.owner.Pos = PosDirUtility.ToTerrainPoint(data.pos, (!this.owner.Actor) ? this.owner.CurFloorStandardHeight : this.owner.Actor.FixTransform.get_position().y);
				this.owner.Dir = new Vector3(data.vector.x, 0f, data.vector.y);
			}
			if (this.owner.Actor)
			{
				this.owner.Actor.EndAnimationResetToIdle();
				this.owner.Actor.ChangeAction("hide", true, false, 1f, 0, 0, string.Empty);
			}
			if (this.owner.IsClientDominate)
			{
				EnterBattleFieldAnnouncer.Announce(this.owner);
			}
			if (InstanceManager.IsLocalBattle && this.owner.Actor)
			{
				int skillIDByIndex = this.owner.GetSkillIDByIndex(PetSkillManager.DebutSkillIndex);
				this.owner.GetSkillManager().SetTargetBySkillID(skillIDByIndex, TargetRangeType.SkillRange, 0f);
				this.owner.GetSkillManager().ClientCastSkillByID(skillIDByIndex);
			}
		}

		public override void PetLeaveBattleField(BattleAction_PetLeaveField data, bool isServerData)
		{
			if (data.petId != this.owner.ID)
			{
				return;
			}
			if (this.owner.IsDead)
			{
				return;
			}
			if (this.owner.IsClientDominate)
			{
				ExitBattleFieldAnnouncer.Announce(this.owner);
			}
			BattleBlackboard.Instance.SetPetCountDown(this.owner.OwnerListIdx, new KeyValuePair<float, DateTime>(0f, DateTime.get_Now()));
			if (this.owner.Actor)
			{
				this.owner.Actor.EndAnimationResetToIdle();
				if (this.owner.IsFighting)
				{
					FXManager.Instance.PlayFX(96, null, this.owner.Actor.FixTransform.get_position(), this.owner.Actor.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
				}
			}
			if (!this.owner.IsClientCreate && this.owner.IsClientDrive)
			{
				LocalBattleHandler.Instance.AppClearBuff(this.owner.ID);
			}
			CommandCenter.ExecuteCommand(this.owner.Actor.FixTransform, new RemoveAllFXCmd());
			this.owner.IsFighting = false;
		}

		public override void Fit(BattleAction_Fit data, bool isServerData)
		{
			if (data.fitPetId != this.owner.ID)
			{
				return;
			}
			this.Fuse();
		}

		public override void ExitFit(BattleAction_ExitFit data, bool isServerData)
		{
			if (data.petId != this.owner.ID)
			{
				return;
			}
		}

		public void Fuse()
		{
			this.owner.IsFighting = false;
			this.owner.IsFuse = true;
			BattleBlackboard.Instance.PetFuseElement = (long)this.owner.Element;
		}

		public void Defuse()
		{
			this.owner.IsFuse = false;
			if (this.owner.Owner.Actor)
			{
				this.owner.Actor.FixTransform.set_position(this.owner.Owner.Actor.FixTransform.get_position());
			}
			this.owner.Actor.CastAction("born", true, 1f, 0, 0, string.Empty);
		}
	}
}
