using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntitySubSystem
{
	public class MonsterSkillManager : SkillManager
	{
		protected float positiveSkillInterval;

		protected DateTime lastPositiveSkillTime;

		public override void OnCreate(EntityParent theOwner)
		{
			base.OnCreate(theOwner);
			this.positiveSkillInterval = DataReader<Monster>.Get(this.owner.TypeID).skillInterval;
		}

		protected override void MarkCD(Skill skillData)
		{
			if (SystemConfig.IsClearCD)
			{
				return;
			}
			DateTime now = DateTime.get_Now();
			if (skillData.type3 == 1)
			{
				this.lastPositiveSkillTime = now;
			}
			this.skillCastTime.set_Item(skillData.id, now);
			for (int i = 0; i < skillData.groupCd.get_Count(); i++)
			{
				if (!this.skillGroupPublicCDAndTime.ContainsKey(skillData.groupCd.get_Item(i).key) || (double)this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Key() - (now - this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Value()).get_TotalMilliseconds() <= (double)skillData.groupCd.get_Item(i).value)
				{
					this.skillGroupPublicCDAndTime.set_Item(skillData.groupCd.get_Item(i).key, new KeyValuePair<int, DateTime>(skillData.groupCd.get_Item(i).value, now));
				}
			}
		}

		public override bool CheckSkillInCDByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				Debug.LogError("CheckSkillInCDByID: " + skillID);
				return true;
			}
			DateTime now = DateTime.get_Now();
			return (now - this.DebutTime).get_TotalMilliseconds() < (double)skill.initCd || (this.skillCastTime.ContainsKey(skill.id) && (now - this.skillCastTime.get_Item(skill.id)).get_TotalMilliseconds() < (double)(skill.cd + this.owner.GetSkillCDVariationByType(skill.skilltype))) || (this.skillGroupPublicCDAndTime.ContainsKey(skill.group) && (now - this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Value()).get_TotalMilliseconds() < (double)this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Key()) || (skill.type3 == 1 && (now - this.lastPositiveSkillTime).get_TotalMilliseconds() < (double)this.positiveSkillInterval);
		}

		public override EntityParent GetTargetBySkillID(int skillID, TargetRangeType rangeType, float rushDistance = 0f)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return null;
			}
			float outerDistance = -1f;
			float innerDistance = 0f;
			int angle = -1;
			int forwardFixAngle = 0;
			switch (rangeType)
			{
			case TargetRangeType.SkillRange:
				outerDistance = (float)skill.reach.get_Item(0) * 0.01f + rushDistance;
				if (skill.reach.get_Count() >= 2)
				{
					angle = skill.reach.get_Item(1);
				}
				if (skill.reach.get_Count() >= 3)
				{
					forwardFixAngle = skill.reach.get_Item(2);
				}
				innerDistance = skill.reachLimit * 0.01f;
				break;
			case TargetRangeType.Configure:
				outerDistance = (float)DataReader<Monster>.Get(this.owner.TypeID).range;
				break;
			}
			return this.owner.GetTarget(rangeType, skill.targetType, outerDistance, innerDistance, angle, forwardFixAngle, skill.antiaircraft, skill.getTarget);
		}

		public override bool ClientCastSkillByID(int skillID)
		{
			bool flag = base.ClientCastSkillByID(skillID);
			if (flag && this.owner != null)
			{
				EventDispatcher.BroadcastAsync<int, int>("GuideManager.MonsterCastSkill", this.owner.TypeID, skillID);
				EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 2, this.owner as EntityMonster, skillID.ToString());
			}
			return flag;
		}

		public override void ClientHandleSkillByID(int skillID)
		{
			base.ClientHandleSkillByID(skillID);
			if (this.owner != null)
			{
				Skill skill = DataReader<Skill>.Get(skillID);
				if (skill == null)
				{
					return;
				}
				if (this.owner.IsLogicBoss && skill.talk == 1)
				{
					BubbleDialogueManager.Instance.BubbleDialogueTrigger(4, 0);
				}
			}
		}

		protected override void ServerHandleSkillByID(int skillID, int actionPriority, Vector3 dir, bool isClientHandle, int uniqueID)
		{
			base.ServerHandleSkillByID(skillID, actionPriority, dir, isClientHandle, uniqueID);
			EventDispatcher.BroadcastAsync<int, int>("GuideManager.MonsterCastSkill", this.owner.TypeID, skillID);
			EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 2, this.owner as EntityMonster, skillID.ToString());
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return;
			}
			if (this.owner.IsLogicBoss && skill.talk == 1)
			{
				BubbleDialogueManager.Instance.BubbleDialogueTrigger(4, 0);
			}
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
			if (caster != null && caster.IsEntitySelfType && caster.Actor && this.ownerActor.CurActionStatus == hitAction && this.owner.IsLogicBoss && ActionStatusName.IsSkillAction(curActionStatus))
			{
				WaveBloodManager.Instance.ThrowBlood(HPChangeMessage.GetBreakMessage(this.owner, caster));
			}
		}
	}
}
