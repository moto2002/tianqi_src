using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public class SelfSkillManager : SkillManager
	{
		protected int assaultSkillID;

		protected new bool IsOpenLog;

		protected override void AddListeners()
		{
			base.AddListeners();
			EventDispatcher.AddListener<int, bool>(XInputManagerEvent.PressSkillKey, new Callback<int, bool>(this.PressSkillKey));
			EventDispatcher.AddListener<bool, bool>(XInputManagerEvent.PressSkillKeyWithCushionCheck, new Callback<bool, bool>(this.ComboFramePressSkillKey));
			EventDispatcher.AddListener<long>(SkillEvent.EndRepeatUseSkill, new Callback<long>(this.ClientEndRepeatUseSkill));
		}

		protected override void RemoveListeners()
		{
			base.RemoveListeners();
			EventDispatcher.RemoveListener<int, bool>(XInputManagerEvent.PressSkillKey, new Callback<int, bool>(this.PressSkillKey));
			EventDispatcher.RemoveListener<bool, bool>(XInputManagerEvent.PressSkillKeyWithCushionCheck, new Callback<bool, bool>(this.ComboFramePressSkillKey));
			EventDispatcher.RemoveListener<long>(SkillEvent.EndRepeatUseSkill, new Callback<long>(this.ClientEndRepeatUseSkill));
		}

		protected void PressSkillKey(int skillIndex, bool isSkillIndexSameAsLast)
		{
			if (this.owner == null)
			{
				if (this.IsOpenLog)
				{
					Debug.LogError("return 0");
				}
				return;
			}
			if (!this.owner.Actor)
			{
				if (this.IsOpenLog)
				{
					Debug.LogError("return 1");
				}
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
			if (this.GetSkillIDByIndex(skillIndex, out num2))
			{
				if (num2 != 0 && DataReader<Skill>.Contains(num2) && this.owner.Actor.CurActionStatus != DataReader<Skill>.Get(num2).attAction)
				{
					this.ClientCastSkillByID(num2);
				}
				else if (this.IsOpenLog)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"return 3: ",
						num2,
						" ",
						num2 != 0,
						" ",
						DataReader<Skill>.Contains(num2),
						" ",
						this.owner.Actor.CurActionStatus != DataReader<Skill>.Get(num2).attAction,
						" ",
						this.owner.Actor.CurActionStatus
					}));
				}
			}
			else if (this.IsOpenLog)
			{
				Debug.LogError("return 2");
			}
		}

		protected void ComboFramePressSkillKey(bool isSkillIndexSameAsLast, bool isInCushionTime)
		{
			if (this.owner == null)
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

		public override void SetDebutCD()
		{
			base.SetDebutCD();
			XDict<int, KeyValuePair<float, DateTime>> xDict = new XDict<int, KeyValuePair<float, DateTime>>();
			using (Dictionary<int, int>.Enumerator enumerator = this.owner.GetSkillPairPart().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					Skill skill = DataReader<Skill>.Get(current.get_Value());
					if (skill != null)
					{
						xDict.Add(current.get_Key(), new KeyValuePair<float, DateTime>((float)skill.initCd, this.DebutTime));
					}
				}
			}
			BattleBlackboard.Instance.SetSelfSkillCD(xDict, false);
		}

		protected override void MarkCD(Skill skillData)
		{
			if (SystemConfig.IsClearCD)
			{
				return;
			}
			DateTime now = DateTime.get_Now();
			List<int> list = new List<int>();
			this.skillCastTime.set_Item(skillData.id, now);
			list.Add(skillData.id);
			if (skillData.groupCd.get_Count() > 0)
			{
				List<int> list2 = new List<int>();
				for (int i = 0; i < skillData.groupCd.get_Count(); i++)
				{
					if (!this.skillGroupPublicCDAndTime.ContainsKey(skillData.groupCd.get_Item(i).key) || (double)this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Key() - (now - this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Value()).get_TotalMilliseconds() <= (double)skillData.groupCd.get_Item(i).value)
					{
						list2.Add(skillData.groupCd.get_Item(i).key);
						this.skillGroupPublicCDAndTime.set_Item(skillData.groupCd.get_Item(i).key, new KeyValuePair<int, DateTime>(skillData.groupCd.get_Item(i).value, now));
					}
				}
				if (list2.get_Count() > 0)
				{
					List<int> skillAllValue = this.owner.GetSkillAllValue();
					for (int j = 0; j < skillAllValue.get_Count(); j++)
					{
						Skill skill = DataReader<Skill>.Get(skillAllValue.get_Item(j));
						if (skill != null)
						{
							if (list2.Contains(skill.group))
							{
								if (!list.Contains(skillAllValue.get_Item(j)))
								{
									list.Add(skillAllValue.get_Item(j));
								}
							}
						}
					}
				}
			}
			XDict<int, KeyValuePair<float, DateTime>> xDict = new XDict<int, KeyValuePair<float, DateTime>>();
			int k = 0;
			while (k < list.get_Count())
			{
				int num;
				if (skillData.type2 == 1)
				{
					num = 1;
					goto IL_221;
				}
				num = this.owner.GetSkillIndexByID(list.get_Item(k));
				if (num != 0)
				{
					goto IL_221;
				}
				IL_238:
				k++;
				continue;
				IL_221:
				xDict.Add(num, this.GetSkillCDByID(list.get_Item(k)));
				goto IL_238;
			}
			BattleBlackboard.Instance.SetSelfSkillCD(xDict, true);
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
			Vector3 endPosition = this.ownerActor.FixTransform.get_position() + (assaultTarget.Actor.FixTransform.get_position() - this.ownerActor.FixTransform.get_position()).get_normalized() * (XUtility.DistanceNoY(assaultTarget.Actor.FixTransform.get_position(), this.ownerActor.FixTransform.get_position()) - XUtility.GetHitRadius(assaultTarget.Actor.FixTransform));
			this.ownerActor.TurnToPos(assaultTarget.Actor.FixTransform.get_position());
			(this.ownerActor as ActorSelf).ClientAssault(assaultTarget.ID, this.assaultSkillID, endPosition);
			GlobalBattleNetwork.Instance.SendAssault(this.owner.ID, this.ownerActor.FixTransform.get_position(), this.ownerActor.FixTransform.get_forward(), endPosition);
		}

		public override void ClientEndAssault()
		{
			base.ClientCastSkillByID(this.assaultSkillID);
		}

		public override bool ClientCastSkillByID(int skillID)
		{
			if (!GlobalBattleNetwork.Instance.IsServerEnable)
			{
				if (this.IsOpenLog)
				{
					Debug.LogError("return 4");
				}
				return false;
			}
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
				if (!this.owner.IsStatic && !this.owner.IsDizzy && !this.owner.IsWeak && !this.owner.IsFixed && !this.owner.IsAssault && !this.owner.IsHitMoving && num <= (float)skill.rush * 0.01f && num > 0f && (!ActionStatusName.IsSkillAction(this.ownerActor.CurActionStatus) || this.ownerActor.IsUnderTermination) && this.ownerActor.CanChangeActionTo("rush", true, 0, false) && this.CheckClientHandleSkillByID(skillID))
				{
					this.ClientBeginAssault(skillID, this.owner.AITarget);
					return false;
				}
			}
			return base.ClientCastSkillByID(skillID);
		}

		public override void ClientHandleSkillByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return;
			}
			if (XInputManager.IsDragging && (skill.type3 == 1 || !string.IsNullOrEmpty(skill.attAction)))
			{
				this.ownerActor.ForceSetDirection(XInputManager.Instance.CurrentDragDirection);
				this.ownerActor.ApplyMovingDirAsForward();
			}
			base.ClientHandleSkillByID(skillID);
			if (skill.cameraCorrection_x.get_Count() > 0)
			{
				EventDispatcher.Broadcast<int>(CameraEvent.RoleNormalAttack, skillID);
			}
		}

		protected override void ServerHandleSkillByID(int skillID, int actionPriority, Vector3 dir, bool isClientHandle, int uniqueID)
		{
			base.ServerHandleSkillByID(skillID, actionPriority, dir, isClientHandle, uniqueID);
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return;
			}
			if (skill.cameraCorrection_x.get_Count() > 0)
			{
				EventDispatcher.Broadcast<int>(CameraEvent.RoleNormalAttack, skillID);
			}
		}

		protected override void BreakSkill(long id)
		{
			if (this.owner.ID != id)
			{
				return;
			}
			base.BreakSkill(id);
			GlobalBattleNetwork.Instance.SendCancelUseSkill(id, this.CurActionSkillID);
		}

		protected override void ClientEndRepeatUseSkill(long id)
		{
			if (this.owner.ID != id)
			{
				return;
			}
			GlobalBattleNetwork.Instance.SendEndRepeatSkill(this.owner.ID, base.CurSkillID);
		}
	}
}
