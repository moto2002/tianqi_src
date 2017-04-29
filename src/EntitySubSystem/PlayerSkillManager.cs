using GameData;
using System;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public class PlayerSkillManager : SkillManager
	{
		protected int assaultSkillID;

		protected override void AddListeners()
		{
			base.AddListeners();
			EventDispatcher.AddListener<long, int, bool>(XInputManagerEvent.FakePressSkillKey, new Callback<long, int, bool>(this.PressSkillKey));
			EventDispatcher.AddListener<long, bool, bool>(XInputManagerEvent.FakePressSkillKeyWithCushionCheck, new Callback<long, bool, bool>(this.ComboFramePressSkillKey));
		}

		protected override void RemoveListeners()
		{
			base.RemoveListeners();
			EventDispatcher.RemoveListener<long, int, bool>(XInputManagerEvent.FakePressSkillKey, new Callback<long, int, bool>(this.PressSkillKey));
			EventDispatcher.RemoveListener<long, bool, bool>(XInputManagerEvent.FakePressSkillKeyWithCushionCheck, new Callback<long, bool, bool>(this.ComboFramePressSkillKey));
		}

		protected void PressSkillKey(long id, int skillIndex, bool isSkillIndexSameAsLast)
		{
			if (this.owner == null)
			{
				return;
			}
			if (this.owner.ID != id)
			{
				return;
			}
			if (!this.owner.Actor)
			{
				return;
			}
			if (isSkillIndexSameAsLast && this.owner.Actor.IsUnderCombo)
			{
				int num = (!DataReader<Skill>.Contains(this.CurActionSkillID)) ? 0 : DataReader<Skill>.Get(this.CurActionSkillID).combo;
				if (num != 0)
				{
					if (!this.CheckHasSkillByID(num))
					{
						this.owner.AddSkill(num, 1, null);
					}
					this.ClientCastSkillByID(num);
					return;
				}
			}
			int num2 = 0;
			if (this.GetSkillIDByIndex(skillIndex, out num2) && num2 != 0 && DataReader<Skill>.Contains(num2) && this.owner.Actor.CurActionStatus != DataReader<Skill>.Get(num2).attAction)
			{
				this.ClientCastSkillByID(num2);
			}
		}

		protected void ComboFramePressSkillKey(long id, bool isSkillIndexSameAsLast, bool isInCushionTime)
		{
			if (this.owner == null)
			{
				return;
			}
			if (this.owner.ID != id)
			{
				return;
			}
			if (!isSkillIndexSameAsLast)
			{
				return;
			}
			if (!isInCushionTime)
			{
				return;
			}
			if (!DataReader<Skill>.Contains(this.CurActionSkillID))
			{
				return;
			}
			int combo = DataReader<Skill>.Get(this.CurActionSkillID).combo;
			if (combo == 0)
			{
				return;
			}
			if (!this.CheckHasSkillByID(combo))
			{
				this.owner.AddSkill(combo, 1, null);
			}
			this.ClientCastSkillByID(combo);
		}

		public override void ClientBeginAssault(int skillID, EntityParent assaultTarget)
		{
			if (!this.ownerActor)
			{
				return;
			}
			if (assaultTarget == null)
			{
				return;
			}
			if (!assaultTarget.Actor)
			{
				return;
			}
			this.owner.IsAssault = true;
			this.assaultSkillID = skillID;
			Vector3 endPosition = this.ownerActor.FixTransform.get_position() + (assaultTarget.Actor.FixTransform.get_position() - this.ownerActor.FixTransform.get_position()).get_normalized() * (XUtility.DistanceNoY(assaultTarget.Actor.FixTransform.get_position(), this.ownerActor.FixTransform.get_position()) - XUtility.GetHitRadius(assaultTarget.Actor.FixTransform.get_transform()));
			this.ownerActor.TurnToPos(assaultTarget.Actor.FixTransform.get_position());
			(this.ownerActor as ActorPlayer).ClientAssault(assaultTarget.ID, this.assaultSkillID, endPosition);
		}

		public override void ClientEndAssault()
		{
			base.ClientCastSkillByID(this.assaultSkillID);
		}

		public override void ServerBeginAssault(Vector3 toPos, int actionPriority)
		{
			if (!this.ownerActor)
			{
				return;
			}
			this.ownerActor.TurnToPos(toPos);
			(this.ownerActor as ActorPlayer).ServerAssault(toPos, actionPriority);
		}

		public override void ServerEndAssault(Vector3 toPos, Vector3 dir)
		{
			if (!this.ownerActor)
			{
				return;
			}
			this.ownerActor.StopMoveToPoint();
			this.ownerActor.FixTransform.set_forward(dir);
			if (this.ownerActor.EndAssaultDeviationEstimated(toPos))
			{
				this.ownerActor.SetAndFixPosition(toPos, "EndAssault瞬移", 1000006);
			}
			else
			{
				this.ownerActor.MoveToPoint(toPos, 0f, null);
			}
		}

		public override bool ClientCastSkillByID(int skillID)
		{
			if (this.owner.IsClientDominate)
			{
				Skill skill = DataReader<Skill>.Get(skillID);
				if (skill.rush > 0)
				{
					this.owner.GetSkillManager().SetTargetBySkillID(skillID, TargetRangeType.SkillRange, (float)skill.rush);
				}
				else
				{
					this.owner.GetSkillManager().SetTargetBySkillID(skillID, TargetRangeType.SkillRange, 0f);
				}
				if (skill.rush > 0 && this.owner.AITarget != null && this.owner.AITarget.Actor)
				{
					float num = XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - (float)skill.reach.get_Item(0) * 0.01f - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform);
					if (!this.owner.IsStatic && !this.owner.IsDizzy && !this.owner.IsWeak && !this.owner.IsFixed && !this.owner.IsAssault && !this.owner.IsHitMoving && num <= (float)skill.rush * 0.01f && num > 0f && this.CheckClientHandleSkillByID(skillID))
					{
						this.ClientBeginAssault(skillID, this.owner.AITarget);
						return false;
					}
				}
				return base.ClientCastSkillByID(skillID);
			}
			return base.ClientCastSkillByID(skillID);
		}

		public override void ClientHandleHit(EntityParent caster, Effect effectData, XPoint basePoint)
		{
			if (!this.ownerActor)
			{
				return;
			}
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.owner.FixModelID);
			XPoint xPoint = basePoint.ApplyOffset(effectData.offset);
			string hitAction = this.ownerActor.GetHitAction(effectData.hitAction);
			float num = 0f;
			float hitMoveTime = 0f;
			if (effectData.hitMove != null && effectData.hitMove.get_Count() > 1)
			{
				num = effectData.hitMove.get_Item(0) * avatarModel.hitMove;
				hitMoveTime = effectData.hitMove.get_Item(1);
			}
			Vector3 arg_157_0;
			if (effectData.hitBase == 1)
			{
				Vector3 vector = new Vector3(xPoint.position.x - this.ownerActor.FixTransform.get_position().x, 0f, xPoint.position.z - this.ownerActor.FixTransform.get_position().z);
				arg_157_0 = vector.get_normalized();
			}
			else
			{
				Vector3 vector2 = new Vector3(basePoint.position.x - this.ownerActor.FixTransform.get_position().x, 0f, basePoint.position.z - this.ownerActor.FixTransform.get_position().z);
				arg_157_0 = vector2.get_normalized();
			}
			Vector3 hitMoveDir = arg_157_0;
			string curActionStatus = this.ownerActor.CurActionStatus;
			if (num == 0f)
			{
				this.ownerActor.ClientPlayHit(hitAction, effectData.hitstraight, effectData.hitActionPriority, true);
			}
			else
			{
				this.ownerActor.ClientPlayHitMove(hitAction, hitMoveDir, num, hitMoveTime, effectData.hitstraight, effectData.hitActionPriority, delegate(Vector3 toPos, Vector3 dir)
				{
				});
			}
			if (caster != null && caster.IsEntitySelfType && caster.Actor && this.ownerActor.CurActionStatus == hitAction && ActionStatusName.IsSkillAction(curActionStatus))
			{
				WaveBloodManager.Instance.ThrowBlood(HPChangeMessage.GetBreakMessage(this.owner, caster));
			}
		}
	}
}
