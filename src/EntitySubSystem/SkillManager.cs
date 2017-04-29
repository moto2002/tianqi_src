using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineActor;
using XEngineCommand;

namespace EntitySubSystem
{
	public class SkillManager : ISkillManager, ISubSystem
	{
		protected static int effectPoolID;

		protected EntityParent owner;

		protected ActorParent ownerActor;

		protected int curSkillID;

		protected int curActionSkillID;

		protected DateTime debutTime = DateTime.get_Now().AddYears(1);

		protected Dictionary<int, KeyValuePair<int, DateTime>> skillGroupPublicCDAndTime = new Dictionary<int, KeyValuePair<int, DateTime>>();

		protected Dictionary<int, DateTime> skillCastTime = new Dictionary<int, DateTime>();

		protected Dictionary<int, EffectMaterial> effectMessageCache = new Dictionary<int, EffectMaterial>();

		protected List<KeyValuePair<string, int>> serverTrusteeSkillActionStatusName = new List<KeyValuePair<string, int>>();

		protected bool IsOpenLog;

		public int CurSkillID
		{
			get
			{
				return this.curSkillID;
			}
			set
			{
				this.curSkillID = value;
			}
		}

		public int CurActionSkillID
		{
			get
			{
				return this.curActionSkillID;
			}
			set
			{
				this.curActionSkillID = value;
			}
		}

		public DateTime DebutTime
		{
			get
			{
				return this.debutTime;
			}
			protected set
			{
				this.debutTime = value;
			}
		}

		public virtual void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.ownerActor = this.owner.Actor;
			this.AddListeners();
		}

		public void OnDestroy()
		{
			this.RemoveListeners();
			this.ResetData();
			this.owner = null;
		}

		protected virtual void AddListeners()
		{
			EventDispatcher.AddListener<long>(SkillEvent.CancelUseSkill, new Callback<long>(this.BreakSkill));
			EventDispatcher.AddListener<long, int>(ActorActionEvent.SetActionSkillID, new Callback<long, int>(this.SetSkillActionID));
			EventDispatcher.AddListener<string, long>(ActorActionEvent.CheckSkillTrustee, new Callback<string, long>(this.ServerTrusteeSkillActionEnd));
		}

		protected virtual void RemoveListeners()
		{
			EventDispatcher.RemoveListener<long>(SkillEvent.CancelUseSkill, new Callback<long>(this.BreakSkill));
			EventDispatcher.RemoveListener<long, int>(ActorActionEvent.SetActionSkillID, new Callback<long, int>(this.SetSkillActionID));
			EventDispatcher.RemoveListener<string, long>(ActorActionEvent.CheckSkillTrustee, new Callback<string, long>(this.ServerTrusteeSkillActionEnd));
		}

		public void UpdateActor(ActorParent actor)
		{
			this.ownerActor = actor;
		}

		public void ResetData()
		{
			this.CurSkillID = 0;
			this.DebutTime = DateTime.get_Now().AddYears(1);
			this.skillGroupPublicCDAndTime.Clear();
			this.skillCastTime.Clear();
			this.ResetActionSkillID();
			this.effectMessageCache.Clear();
			this.owner.IsSkillInTrustee = false;
		}

		public bool CheckHasSkillByID(int skillID)
		{
			return this.owner.ContainSkillID(skillID);
		}

		public bool GetSkillIDByIndex(int skillIndex, out int value)
		{
			if (this.owner.ContainSkillIndex(skillIndex))
			{
				value = this.owner.GetSkillIDByIndex(skillIndex);
				return true;
			}
			value = 0;
			return false;
		}

		public void ResetActionSkillID()
		{
			this.CurActionSkillID = 0;
		}

		protected void SetSkillActionID(long id, int skillID)
		{
			if (this.owner == null)
			{
				return;
			}
			if (!this.owner.IsInBattle)
			{
				return;
			}
			if (this.owner.ID != id)
			{
				return;
			}
			this.CurActionSkillID = skillID;
		}

		public virtual void SetDebutCD()
		{
			this.DebutTime = DateTime.get_Now();
		}

		public KeyValuePair<float, DateTime> GetSkillCDByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return new KeyValuePair<float, DateTime>(3.40282347E+38f, DateTime.MaxValue);
			}
			DateTime now = DateTime.get_Now();
			if ((now - this.DebutTime).get_TotalMilliseconds() < 0.0)
			{
				return new KeyValuePair<float, DateTime>(3.40282347E+38f, DateTime.MaxValue);
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			if ((now - this.DebutTime).get_TotalMilliseconds() < (double)skill.initCd)
			{
				num = (float)skill.initCd;
				num2 = (float)((double)num - (now - this.DebutTime).get_TotalMilliseconds());
			}
			if (this.skillCastTime.ContainsKey(skillID))
			{
				num3 = (float)(skill.cd + this.owner.GetSkillCDVariationByType(skill.skilltype));
				num4 = (float)((double)num3 - (now - this.skillCastTime.get_Item(skillID)).get_TotalMilliseconds());
			}
			if (this.skillGroupPublicCDAndTime.ContainsKey(skill.group))
			{
				num5 = (float)this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Key();
				num6 = (float)((double)num5 - (now - this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Value()).get_TotalMilliseconds());
			}
			if (num2 >= num4 && num2 >= num6)
			{
				return new KeyValuePair<float, DateTime>(num, this.DebutTime);
			}
			if (num4 >= num2 && num4 >= num6)
			{
				return new KeyValuePair<float, DateTime>(num3, this.skillCastTime.get_Item(skillID));
			}
			if (num6 >= num2 && num6 >= num4)
			{
				return new KeyValuePair<float, DateTime>(num5, this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Value());
			}
			return new KeyValuePair<float, DateTime>(0f, DateTime.MinValue);
		}

		protected virtual void MarkCD(Skill skillData)
		{
			if (SystemConfig.IsClearCD)
			{
				return;
			}
			DateTime now = DateTime.get_Now();
			this.skillCastTime.set_Item(skillData.id, now);
			for (int i = 0; i < skillData.groupCd.get_Count(); i++)
			{
				if (!this.skillGroupPublicCDAndTime.ContainsKey(skillData.groupCd.get_Item(i).key) || (double)this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Key() - (now - this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Value()).get_TotalMilliseconds() <= (double)skillData.groupCd.get_Item(i).value)
				{
					this.skillGroupPublicCDAndTime.set_Item(skillData.groupCd.get_Item(i).key, new KeyValuePair<int, DateTime>(skillData.groupCd.get_Item(i).value, now));
				}
			}
		}

		public virtual bool CheckSkillInCDByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return true;
			}
			DateTime now = DateTime.get_Now();
			return (now - this.DebutTime).get_TotalMilliseconds() < (double)skill.initCd || (this.skillCastTime.ContainsKey(skill.id) && (now - this.skillCastTime.get_Item(skill.id)).get_TotalMilliseconds() < (double)(skill.cd + this.owner.GetSkillCDVariationByType(skill.skilltype))) || (this.skillGroupPublicCDAndTime.ContainsKey(skill.group) && (now - this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Value()).get_TotalMilliseconds() < (double)this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Key());
		}

		public bool SetTargetBySkillID(int skillID, TargetRangeType rangeType, float rushDistance = 0f)
		{
			this.owner.AITarget = this.GetTargetBySkillID(skillID, rangeType, rushDistance);
			return this.owner.AITarget != null;
		}

		public virtual EntityParent GetTargetBySkillID(int skillID, TargetRangeType rangeType, float rushDistance = 0f)
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
				outerDistance = ((float)skill.reach.get_Item(0) + rushDistance) * 0.01f;
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
				return null;
			}
			EntityParent target = this.owner.GetTarget(rangeType, skill.targetType, outerDistance, innerDistance, angle, forwardFixAngle, skill.antiaircraft, skill.getTarget);
			int num = 0;
			while (rangeType != TargetRangeType.World && num < skill.angle.get_Count() && target == null)
			{
				target = this.owner.GetTarget(rangeType, skill.targetType, outerDistance, innerDistance, skill.angle.get_Item(num), 0, skill.antiaircraft, skill.getTarget);
				num++;
			}
			return target;
		}

		public bool CheckTargetBySkillID(EntityParent entity, int skillID, TargetRangeType rangeType, float rushDistance = 0f)
		{
			if (entity == null)
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return false;
			}
			float outerDistance = -1f;
			int angle = -1;
			float innerDistance = 0f;
			if (rangeType != TargetRangeType.SkillRange)
			{
				if (rangeType != TargetRangeType.Configure)
				{
				}
			}
			else
			{
				outerDistance = ((float)skill.reach.get_Item(0) + rushDistance) * 0.01f;
				if (skill.reach.get_Count() >= 2)
				{
					angle = skill.reach.get_Item(1);
				}
				innerDistance = skill.reachLimit * 0.01f;
			}
			return this.owner.CheckTarget(entity, rangeType, skill.targetType, outerDistance, innerDistance, angle, skill.antiaircraft);
		}

		public virtual void ClientBeginAssault(int skillID, EntityParent assaultTarget)
		{
		}

		public virtual void ClientEndAssault()
		{
		}

		public virtual void ServerBeginAssault(Vector3 toPos, int actionPriority)
		{
		}

		public virtual void ServerEndAssault(Vector3 toPos, Vector3 dir)
		{
		}

		public virtual bool ClientCastSkillByID(int skillID)
		{
			if (!this.CheckClientHandleSkillByID(skillID))
			{
				if (this.owner.IsEntitySelfType && this.IsOpenLog)
				{
					Debug.LogError("return 5");
				}
				return false;
			}
			this.ClientHandleSkillByID(skillID);
			if (this.owner != null && this.owner.IsPlayerMate)
			{
				EventDispatcher.BroadcastAsync<int, int>("GuideManager.MateCastSkill", this.owner.TypeID, skillID);
			}
			return true;
		}

		public virtual bool CheckClientHandleSkillByID(int skillID)
		{
			if (this.ownerActor == null)
			{
				if (this.owner.IsEntitySelfType && this.IsOpenLog)
				{
					Debug.LogError("return 5.1");
				}
				return false;
			}
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				if (this.owner.IsEntitySelfType && this.IsOpenLog)
				{
					Debug.LogError("return 5.2");
				}
				return false;
			}
			if (skill.type3 == 1)
			{
				if (this.owner.IsDead || !this.owner.IsFighting || this.owner.IsStatic || this.owner.IsDizzy || this.owner.IsWeak || this.owner.IsAssault || this.owner.IsHitMoving)
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.3");
					}
					return false;
				}
				if (!this.CheckHasSkillByID(skillID) || !this.ownerActor.CanChangeActionTo(skill.attAction, true, skillID, this.IsOpenLog))
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.4");
					}
					return false;
				}
				if (this.owner.ActPoint + skill.actionPoint + this.owner.GetSkillActionPointVariationByType(skill.skilltype) < 0)
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.5");
					}
					return false;
				}
				if (this.CheckSkillInCDByID(skillID))
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.6");
					}
					return false;
				}
			}
			else
			{
				if (this.owner.IsDead || !this.owner.IsFighting)
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.7");
					}
					return false;
				}
				if (!this.CheckHasSkillByID(skillID))
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.8");
					}
					return false;
				}
				if (this.owner.ActPoint + skill.actionPoint + this.owner.GetSkillActionPointVariationByType(skill.skilltype) < 0)
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.9");
					}
					return false;
				}
				if (this.CheckSkillInCDByID(skillID))
				{
					if (this.owner.IsEntitySelfType && this.IsOpenLog)
					{
						Debug.LogError("return 5.10");
					}
					return false;
				}
			}
			return true;
		}

		public virtual void ClientHandleSkillByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return;
			}
			this.CurSkillID = skillID;
			this.MarkCD(skill);
			this.ownerActor.IsRotating = false;
			if (skill.autoAim == 1 && this.owner.AITarget != null && this.owner.AITarget.Actor)
			{
				this.ownerActor.ForceSetDirection(new Vector3(this.owner.AITarget.Actor.FixTransform.get_position().x - this.ownerActor.FixTransform.get_position().x, 0f, this.owner.AITarget.Actor.FixTransform.get_position().z - this.ownerActor.FixTransform.get_position().z));
				this.ownerActor.ApplyMovingDirAsForward();
			}
			for (int i = 0; i < skill.effect.get_Count(); i++)
			{
				this.MarkStaticEffectMessage(skill.effect.get_Item(i), true);
			}
			if (!string.IsNullOrEmpty(skill.attAction))
			{
				List<int> actionEffects = XUtility.GetActionEffects(this.ownerActor.FixAnimator.get_runtimeAnimatorController(), skill.attAction);
				for (int j = 0; j < actionEffects.get_Count(); j++)
				{
					this.MarkStaticEffectMessage(actionEffects.get_Item(j), true);
				}
			}
			if (skill.aiSkillMove == 1 && this.owner.AITarget != null)
			{
				SkillWarningMessage skillWarningMessage = new SkillWarningMessage();
				skillWarningMessage.caster = this.owner;
				skillWarningMessage.target = this.owner.AITarget;
				skillWarningMessage.skillID = skillID;
				skillWarningMessage.effectMessage = new Dictionary<int, XPoint>();
				if (skill.effect != null)
				{
					for (int k = 0; k < skill.effect.get_Count(); k++)
					{
						int num = skill.effect.get_Item(k);
						if (this.effectMessageCache.ContainsKey(num) && !skillWarningMessage.effectMessage.ContainsKey(num))
						{
							skillWarningMessage.effectMessage.Add(num, this.effectMessageCache.get_Item(num).basePoint);
						}
					}
				}
				List<int> actionEffects2 = XUtility.GetActionEffects(this.ownerActor.FixAnimator.get_runtimeAnimatorController(), skill.attAction);
				for (int l = 0; l < actionEffects2.get_Count(); l++)
				{
					int num2 = actionEffects2.get_Item(l);
					if (this.effectMessageCache.ContainsKey(num2) && !skillWarningMessage.effectMessage.ContainsKey(num2))
					{
						skillWarningMessage.effectMessage.Add(num2, this.effectMessageCache.get_Item(num2).basePoint);
					}
				}
				EventDispatcher.BroadcastAsync<SkillWarningMessage>(WarningManagerEvent.AddSkillWarningMessage, skillWarningMessage);
			}
			bool flag = this.CheckSkillTrustee(skill);
			if (flag)
			{
				this.owner.IsSkillInTrustee = true;
				this.serverTrusteeSkillActionStatusName.Add(new KeyValuePair<string, int>(skill.attAction, 0));
				if (string.IsNullOrEmpty(skill.attAction))
				{
					this.ServerTrusteeSkillActionEnd(string.Empty, this.owner.ID);
				}
			}
			if (skill.actionPriority != 0)
			{
				this.ownerActor.ActionPriorityTable[skill.attAction] = skill.actionPriority;
			}
			this.ownerActor.CastAction(skill.attAction, true, skill.changeSpeed, (!string.IsNullOrEmpty(skill.attAction)) ? skillID : 0, skill.combo, skill.eventTag);
			if (skill.superArmor == 1)
			{
				int fxID = FXManager.Instance.PlayFX(93, this.ownerActor.FixTransform, this.ownerActor.FixTransform.get_position(), this.ownerActor.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
				this.ownerActor.FixFXSystem.AddActionFX(fxID);
			}
			if (skill.skillWarningTime != 0 && this.owner.AITarget != null)
			{
				WarningGraghHandler.AddWarningGragh(new SkillWarningGraghMessage
				{
					casterActor = this.ownerActor,
					targetPoint = (!this.owner.AITarget.Actor) ? null : new XPoint
					{
						position = this.owner.AITarget.Actor.FixTransform.get_position(),
						rotation = this.owner.AITarget.Actor.FixTransform.get_rotation()
					},
					skillID = skillID
				});
			}
			GlobalBattleNetwork.Instance.SendUseSkill(this.owner.ID, skillID, (this.owner.AITarget != null) ? this.owner.AITarget.ID : 0L, flag, this.ownerActor.FixTransform.get_position(), this.ownerActor.FixTransform.get_forward());
			this.ClientLogicTriggerEffect(skill);
		}

		protected bool CheckSkillTrustee(Skill skillData)
		{
			if (skillData.effect != null)
			{
				for (int i = 0; i < skillData.effect.get_Count(); i++)
				{
					Effect effect = DataReader<Effect>.Get(skillData.effect.get_Item(i));
					if (effect != null)
					{
						if (effect.type2 == 3 || effect.type2 == 4)
						{
							return true;
						}
						if (effect.bullet != 0)
						{
							return true;
						}
						if (effect.tremble != 0 && effect.fx != 0)
						{
							return true;
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(skillData.attAction))
			{
				List<int> actionEffects = XUtility.GetActionEffects(this.ownerActor.FixAnimator.get_runtimeAnimatorController(), skillData.attAction);
				for (int j = 0; j < actionEffects.get_Count(); j++)
				{
					Effect effect2 = DataReader<Effect>.Get(actionEffects.get_Item(j));
					if (effect2 != null)
					{
						if (effect2.type2 == 3 || effect2.type2 == 4)
						{
							return true;
						}
						if (effect2.bullet != 0)
						{
							return true;
						}
						if (effect2.tremble != 0 && effect2.fx != 0)
						{
							return true;
						}
					}
				}
				if (this.ownerActor)
				{
					if (!this.ownerActor.FixAnimator.get_runtimeAnimatorController())
					{
						Debug.LogError(string.Format("runtimeAnimatorController为空：{0}", this.owner.FixModelID));
						return false;
					}
					AnimationClip[] animationClips = this.ownerActor.FixAnimator.get_runtimeAnimatorController().get_animationClips();
					for (int k = 0; k < animationClips.Length; k++)
					{
						if (!(animationClips[k].get_name() != skillData.attAction))
						{
							for (int l = 0; l < animationClips[k].get_events().Length; l++)
							{
								if (animationClips[k].get_events()[l].get_functionName() == "MoveOn")
								{
									return true;
								}
							}
							break;
						}
					}
				}
			}
			return false;
		}

		public void ServerCastSkillByID(int skillID, int actionPriority, Vector3 dir, bool isClientHandle, int uniqueID)
		{
			this.ServerHandleSkillByID(skillID, actionPriority, dir, isClientHandle, uniqueID);
		}

		protected virtual void ServerHandleSkillByID(int skillID, int actionPriority, Vector3 dir, bool isClientHandle, int uniqueID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				return;
			}
			this.CurSkillID = skillID;
			this.ownerActor.IsRotating = false;
			this.ownerActor.ForceSetDirection(dir);
			this.ownerActor.ApplyMovingDirAsForward();
			if (skill.effect != null)
			{
				for (int i = 0; i < skill.effect.get_Count(); i++)
				{
					this.MarkStaticEffectMessage(skill.effect.get_Item(i), isClientHandle);
				}
			}
			if (!string.IsNullOrEmpty(skill.attAction))
			{
				List<int> actionEffects = XUtility.GetActionEffects(this.ownerActor.FixAnimator.get_runtimeAnimatorController(), skill.attAction);
				for (int j = 0; j < actionEffects.get_Count(); j++)
				{
					this.MarkStaticEffectMessage(actionEffects.get_Item(j), isClientHandle);
				}
			}
			if (isClientHandle)
			{
				this.serverTrusteeSkillActionStatusName.Add(new KeyValuePair<string, int>(skill.attAction, uniqueID));
				if (string.IsNullOrEmpty(skill.attAction))
				{
					this.ServerTrusteeSkillActionEnd(string.Empty, this.owner.ID);
				}
			}
			this.ownerActor.ServerCastAction(skill.attAction, actionPriority, (!isClientHandle) ? ActorParent.EffectFrameSetMode.Ignore : ActorParent.EffectFrameSetMode.Server, skill.changeSpeed, (!string.IsNullOrEmpty(skill.attAction)) ? skillID : 0, skill.combo, skill.eventTag);
			if (skill.superArmor == 1)
			{
				int fxID = FXManager.Instance.PlayFX(93, this.ownerActor.FixTransform, this.ownerActor.FixTransform.get_position(), this.ownerActor.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
				this.ownerActor.FixFXSystem.AddActionFX(fxID);
			}
			if (skill.skillWarningTime != 0)
			{
				WarningGraghHandler.AddWarningGragh(new SkillWarningGraghMessage
				{
					casterActor = this.ownerActor,
					targetPoint = null,
					skillID = skillID
				});
			}
			this.ServerLogicTriggerEffect(skill);
		}

		protected void MarkStaticEffectMessage(int effectID, bool isClientHandle = true)
		{
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			EffectMaterial effectMaterial = new EffectMaterial();
			effectMaterial.isClientHandle = isClientHandle;
			effectMaterial.skillTargetID = ((this.owner.AITarget != null) ? this.owner.AITarget.ID : 0L);
			effectMaterial.basePoint = ((effect.type2 != 3 && effect.type2 != 4) ? this.GetEffectBasePoint((EffectBasePointType)effect.@base, (float)effect.tremble, (this.owner.AITarget != null) ? this.owner.AITarget.ID : 0L, effect.summonId, effect.coord, effect.orientation) : null);
			if (!this.effectMessageCache.ContainsKey(effectID))
			{
				this.effectMessageCache.Add(effectID, effectMaterial);
			}
			else
			{
				this.effectMessageCache.set_Item(effectID, effectMaterial);
			}
		}

		protected XPoint GetEffectBasePoint(EffectBasePointType basePointType, float shakeRange, long targetID, int spawnPointID, List<int> prescribedPositionList, List<int> prescribedToPositionList)
		{
			Vector3 zero = Vector3.get_zero();
			if (shakeRange != 0f)
			{
				Vector2 vector = Random.get_insideUnitCircle() * shakeRange * 0.01f;
				zero = new Vector3(vector.x, 0f, vector.y);
			}
			switch (basePointType)
			{
			case EffectBasePointType.Self:
				return new XPoint
				{
					position = this.ownerActor.FixTransform.get_position() + zero,
					rotation = this.ownerActor.FixTransform.get_rotation()
				};
			case EffectBasePointType.Target:
				return (!EntityWorld.Instance.AllEntities.ContainsKey(targetID) || !EntityWorld.Instance.AllEntities[targetID].Actor) ? null : new XPoint
				{
					position = EntityWorld.Instance.AllEntities[targetID].Actor.FixTransform.get_position() + zero,
					rotation = this.ownerActor.FixTransform.get_rotation()
				};
			case EffectBasePointType.SpawnPoint:
			{
				if (spawnPointID == 0)
				{
					return null;
				}
				Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, spawnPointID);
				Vector3 terrainPoint = MySceneManager.GetTerrainPoint(point.x, point.y, this.ownerActor.FixTransform.get_position().y);
				Vector3 vector2 = Vector3.get_forward();
				if (prescribedToPositionList != null && prescribedToPositionList.get_Count() >= 3)
				{
					vector2 = new Vector3((float)prescribedToPositionList.get_Item(0) * 0.01f, (float)prescribedToPositionList.get_Item(1) * 0.01f, (float)prescribedToPositionList.get_Item(2) * 0.01f) - terrainPoint;
				}
				return new XPoint
				{
					position = terrainPoint + zero,
					rotation = Quaternion.LookRotation(vector2)
				};
			}
			case EffectBasePointType.TriggerPoint:
				return null;
			case EffectBasePointType.PrescribedPoint:
			{
				if (prescribedPositionList == null)
				{
					return null;
				}
				if (prescribedPositionList.get_Count() < 3)
				{
					return null;
				}
				Vector3 vector3 = new Vector3((float)prescribedPositionList.get_Item(0) * 0.01f, (float)prescribedPositionList.get_Item(1) * 0.01f, (float)prescribedPositionList.get_Item(2) * 0.01f);
				Vector3 vector4 = Vector3.get_forward();
				if (prescribedToPositionList != null && prescribedToPositionList.get_Count() >= 3)
				{
					vector4 = new Vector3((float)prescribedToPositionList.get_Item(0) * 0.01f, (float)prescribedToPositionList.get_Item(1) * 0.01f, (float)prescribedToPositionList.get_Item(2) * 0.01f) - vector3;
				}
				return new XPoint
				{
					position = vector3 + zero,
					rotation = Quaternion.LookRotation(vector4)
				};
			}
			default:
				return null;
			}
		}

		public void ClearSkillTrusteeMessage()
		{
			this.serverTrusteeSkillActionStatusName.Clear();
		}

		protected virtual void BreakSkill(long id)
		{
		}

		protected void ServerTrusteeSkillActionEnd(string actName, long id)
		{
			if (this.owner == null || this.owner.ID != id)
			{
				return;
			}
			if (this.serverTrusteeSkillActionStatusName.get_Count() == 0)
			{
				return;
			}
			bool flag = false;
			int uniqueID = 0;
			int i;
			for (i = 0; i < this.serverTrusteeSkillActionStatusName.get_Count(); i++)
			{
				if (!(this.serverTrusteeSkillActionStatusName.get_Item(i).get_Key() != actName))
				{
					flag = true;
					uniqueID = this.serverTrusteeSkillActionStatusName.get_Item(i).get_Value();
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			if (this.serverTrusteeSkillActionStatusName.get_Count() > i)
			{
				this.serverTrusteeSkillActionStatusName.RemoveAt(i);
			}
			this.owner.IsSkillInTrustee = (this.serverTrusteeSkillActionStatusName.get_Count() != 0);
			this.ownerActor.StopMoveToPoint();
			GlobalBattleNetwork.Instance.SendEndSkillManage(this.owner.ID, this.ownerActor.FixTransform.get_position(), this.ownerActor.FixTransform.get_forward(), uniqueID);
		}

		protected virtual void ClientEndRepeatUseSkill(long id)
		{
		}

		public void ServerEndRepeatSkill(int skillID)
		{
			if (skillID != this.CurSkillID)
			{
				return;
			}
			this.ownerActor.CanRepeat = false;
		}

		public void ClientActionTriggerEffect(int effectID)
		{
			this.ClientTriggerEffect(effectID, this.CurActionSkillID);
		}

		protected void ClientLogicTriggerEffect(Skill skillData)
		{
			for (int i = 0; i < skillData.effect.get_Count(); i++)
			{
				this.ClientTriggerEffect(skillData.effect.get_Item(i), skillData.id);
			}
		}

		protected void ClientTriggerEffect(int effectID, int skillID)
		{
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			SkillManager.effectPoolID++;
			EffectMaterial effectMaterial = (!this.effectMessageCache.ContainsKey(effectID)) ? null : this.effectMessageCache.get_Item(effectID);
			EffectMessage effectMessage = new EffectMessage();
			effectMessage.caster = this.owner;
			effectMessage.casterActor = this.ownerActor;
			effectMessage.skillData = DataReader<Skill>.Get(skillID);
			effectMessage.effectData = effect;
			if (effect.type2 == 4 || effect.type2 == 3)
			{
				effectMessage.basePoint = this.GetEffectBasePoint((EffectBasePointType)effect.@base, (float)effect.tremble, (effectMaterial != null) ? effectMaterial.skillTargetID : 0L, effect.summonId, effect.coord, effect.orientation);
			}
			else
			{
				effectMessage.basePoint = ((effectMaterial != null) ? effectMaterial.basePoint : null);
			}
			effectMessage.UID = SkillManager.effectPoolID;
			effectMessage.isClientHandle = (effectMaterial == null || effectMaterial.isClientHandle);
			if (effect.aiEffectMove == 1 && effectMessage.basePoint != null)
			{
				EffectWarningMessage effectWarningMessage = new EffectWarningMessage();
				effectWarningMessage.caster = this.owner;
				effectWarningMessage.effectID = effectID;
				effectWarningMessage.basePoint = effectMessage.basePoint.ApplyOffset(effect.offset);
				EventDispatcher.Broadcast<EffectWarningMessage>(WarningManagerEvent.AddEffectWarningMessage, effectWarningMessage);
			}
			ClientEffectHandler.Instance.TriggerEffect(effect.delay, effectMessage);
			if (effectMessage.basePoint != null && effect.fx != 0)
			{
				CommandCenter.ExecuteCommand(this.ownerActor.FixTransform, new BulletFXCmd
				{
					fxID = effect.fx,
					point = effectMessage.basePoint,
					scale = DataReader<AvatarModel>.Get(this.ownerActor.GetEntity().FixModelID).scale
				});
			}
		}

		public void ServerActionTriggerEffect(int effectID)
		{
			this.ServerTriggerEffect(effectID, this.CurActionSkillID);
		}

		protected void ServerLogicTriggerEffect(Skill skillData)
		{
			for (int i = 0; i < skillData.effect.get_Count(); i++)
			{
				this.ServerTriggerEffect(skillData.effect.get_Item(i), skillData.id);
			}
		}

		protected void ServerTriggerEffect(int effectID, int skillID)
		{
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			SkillManager.effectPoolID++;
			EffectMaterial effectMaterial = (!this.effectMessageCache.ContainsKey(effectID)) ? null : this.effectMessageCache.get_Item(effectID);
			EffectMessage effectMessage = new EffectMessage();
			effectMessage.caster = this.owner;
			effectMessage.casterActor = this.ownerActor;
			effectMessage.skillData = DataReader<Skill>.Get(skillID);
			effectMessage.effectData = effect;
			if (effect.type2 == 4 || effect.type2 == 3)
			{
				effectMessage.basePoint = this.GetEffectBasePoint((EffectBasePointType)effect.@base, (float)effect.tremble, (effectMaterial != null) ? effectMaterial.skillTargetID : 0L, effect.summonId, effect.coord, effect.orientation);
			}
			else
			{
				effectMessage.basePoint = ((effectMaterial != null) ? effectMaterial.basePoint : null);
			}
			effectMessage.UID = SkillManager.effectPoolID;
			effectMessage.isClientHandle = (effectMaterial == null || effectMaterial.isClientHandle);
			if (effect.aiEffectMove == 1 && effectMessage.basePoint != null)
			{
				EffectWarningMessage effectWarningMessage = new EffectWarningMessage();
				effectWarningMessage.caster = this.owner;
				effectWarningMessage.effectID = effectID;
				effectWarningMessage.basePoint = effectMessage.basePoint.ApplyOffset(effect.offset);
				EventDispatcher.Broadcast<EffectWarningMessage>(WarningManagerEvent.AddEffectWarningMessage, effectWarningMessage);
			}
			ClientEffectHandler.Instance.TriggerEffect(effect.delay, effectMessage);
			if (effectMessage.basePoint != null && effect.fx != 0)
			{
				CommandCenter.ExecuteCommand(this.ownerActor.FixTransform, new BulletFXCmd
				{
					fxID = effect.fx,
					point = effectMessage.basePoint,
					scale = DataReader<AvatarModel>.Get(this.ownerActor.GetEntity().FixModelID).scale
				});
			}
		}

		public virtual void ClientHandleHit(EntityParent caster, Effect effectData, XPoint basePoint)
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
		}

		public void ServerHandleHit(long casterID, int effectID, string hitAction, int actionPriority, bool isKnock, Vector3 toPos, bool isManage, int oldManageState, int uniqueID)
		{
			if (isManage)
			{
				this.owner.CheckCancelManage(casterID, oldManageState, true);
			}
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			bool flag = XUtility.StartsWith(hitAction, "float");
			if (this.owner.IsSuspended != flag)
			{
				this.owner.IsSuspended = flag;
			}
			if (isKnock)
			{
				if (this.IsOpenLog)
				{
					Debug.LogError("Fuck1");
				}
				if (!this.owner.IsHitMoving)
				{
					this.owner.IsHitMoving = true;
				}
				Action<Vector3, Vector3> callback = null;
				if (isManage)
				{
					callback = delegate(Vector3 pos, Vector3 dir)
					{
						GlobalBattleNetwork.Instance.SendEndKnock(this.owner.ID, pos, dir, uniqueID);
					};
				}
				if (effect.hitMove != null && effect.hitMove.get_Count() > 1)
				{
					Debuger.Error(string.Concat(new object[]
					{
						"hitMove 1: ",
						this.ownerActor.FixTransform.get_position().x - toPos.x,
						"  ",
						this.ownerActor.FixTransform.get_position().z - toPos.z
					}), new object[0]);
					ActorParent arg_1CC_0 = this.ownerActor;
					Vector3 vector = new Vector3(this.ownerActor.FixTransform.get_position().x - toPos.x, 0f, this.ownerActor.FixTransform.get_position().z - toPos.z);
					arg_1CC_0.ServerPlayHitMove(hitAction, vector.get_normalized(), XUtility.DistanceNoY(toPos, this.ownerActor.FixTransform.get_position()), effect.hitMove.get_Item(1), effect.hitstraight, actionPriority, callback);
				}
				else
				{
					Debuger.Error(string.Concat(new object[]
					{
						"hitMove 2: ",
						this.ownerActor.FixTransform.get_position().x - toPos.x,
						"  ",
						this.ownerActor.FixTransform.get_position().z - toPos.z
					}), new object[0]);
					ActorParent arg_2CF_0 = this.ownerActor;
					Vector3 vector2 = new Vector3(this.ownerActor.FixTransform.get_position().x - toPos.x, 0f, this.ownerActor.FixTransform.get_position().z - toPos.z);
					arg_2CF_0.ServerPlayHitMove(hitAction, vector2.get_normalized(), XUtility.DistanceNoY(toPos, this.ownerActor.FixTransform.get_position()), 0f, effect.hitstraight, actionPriority, callback);
				}
			}
			else
			{
				object arg_338_0 = "hitMove 3: ";
				Vector3 vector3 = new Vector3(this.ownerActor.FixTransform.get_position().x - toPos.x, 0f, this.ownerActor.FixTransform.get_position().z - toPos.z);
				Debuger.Error(arg_338_0 + vector3.get_normalized(), new object[0]);
				this.ownerActor.ServerPlayHit(hitAction, effect.hitstraight, actionPriority, false);
			}
		}
	}
}
