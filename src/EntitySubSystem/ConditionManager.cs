using GameData;
using System;
using System.Collections.Generic;

namespace EntitySubSystem
{
	public class ConditionManager : IConditionManager, ISubSystem
	{
		protected EntityParent owner;

		protected Dictionary<ConditionType, List<ConditionItem>> conditionTable = new Dictionary<ConditionType, List<ConditionItem>>();

		protected List<uint> conditionTimer = new List<uint>();

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.AddListeners();
		}

		public void OnDestroy()
		{
			this.RemoveListners();
			for (int i = 0; i < this.conditionTimer.get_Count(); i++)
			{
				TimerHeap.DelTimer(this.conditionTimer.get_Item(i));
			}
			this.conditionTimer.Clear();
			this.owner = null;
		}

		protected virtual void AddListeners()
		{
			EventDispatcher.AddListener<ConditionMessage>(ConditionManagerEvent.CheckCondition, new Callback<ConditionMessage>(this.CheckCondition));
		}

		protected virtual void RemoveListners()
		{
			EventDispatcher.RemoveListener<ConditionMessage>(ConditionManagerEvent.CheckCondition, new Callback<ConditionMessage>(this.CheckCondition));
		}

		public void RegistCounterSkillCondition(List<int> skillIDs)
		{
			for (int i = 0; i < skillIDs.get_Count(); i++)
			{
				if (skillIDs.get_Item(i) != 0)
				{
					Skill skill = DataReader<Skill>.Get(skillIDs.get_Item(i));
					if (skill != null)
					{
						if (skill.type3 == 2)
						{
							if (skill.conditionId != 0)
							{
								Condition condition = DataReader<Condition>.Get(skill.conditionId);
								if (condition != null)
								{
									if (condition.occasion != 0)
									{
										if (!this.conditionTable.ContainsKey((ConditionType)condition.occasion))
										{
											this.conditionTable.Add((ConditionType)condition.occasion, new List<ConditionItem>());
										}
										if (!this.CheckContainsCounterSkillConditionItem(this.conditionTable.get_Item((ConditionType)condition.occasion), skillIDs.get_Item(i)))
										{
											CounterSkillConditionItem counterSkillConditionItem = new CounterSkillConditionItem();
											counterSkillConditionItem.conditionData = condition;
											counterSkillConditionItem.counterSkillID = skillIDs.get_Item(i);
											this.conditionTable.get_Item((ConditionType)condition.occasion).Add(counterSkillConditionItem);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		protected bool CheckContainsCounterSkillConditionItem(List<ConditionItem> conditionItems, int skillID)
		{
			for (int i = 0; i < conditionItems.get_Count(); i++)
			{
				if (conditionItems.get_Item(i) is CounterSkillConditionItem && (conditionItems.get_Item(i) as CounterSkillConditionItem).counterSkillID == skillID)
				{
					return true;
				}
			}
			return false;
		}

		public void RegistThinkCondition(List<int> conditionIDs)
		{
			for (int i = 0; i < conditionIDs.get_Count(); i++)
			{
				Condition condition = DataReader<Condition>.Get(conditionIDs.get_Item(i));
				if (condition != null)
				{
					if (condition.occasion != 0)
					{
						if (!this.conditionTable.ContainsKey((ConditionType)condition.occasion))
						{
							this.conditionTable.Add((ConditionType)condition.occasion, new List<ConditionItem>());
						}
						ThinkConditionItem thinkConditionItem = new ThinkConditionItem();
						thinkConditionItem.conditionData = condition;
						thinkConditionItem.isUseTriggerTimes = (condition.count != 0);
						thinkConditionItem.leftTimes = condition.count;
						this.conditionTable.get_Item((ConditionType)condition.occasion).Add(thinkConditionItem);
					}
				}
			}
		}

		public void RegistTriggerSkillCondition(List<int> conditionIDs, List<int> skillIDs)
		{
			int num = (conditionIDs.get_Count() >= skillIDs.get_Count()) ? skillIDs.get_Count() : conditionIDs.get_Count();
			for (int i = 0; i < num; i++)
			{
				if (DataReader<Skill>.Get(skillIDs.get_Item(i)) != null)
				{
					Condition condition = DataReader<Condition>.Get(conditionIDs.get_Item(i));
					if (condition != null)
					{
						if (condition.occasion != 0)
						{
							if (!this.conditionTable.ContainsKey((ConditionType)condition.occasion))
							{
								this.conditionTable.Add((ConditionType)condition.occasion, new List<ConditionItem>());
							}
							if (!this.CheckContainsCounterSkillConditionItem(this.conditionTable.get_Item((ConditionType)condition.occasion), skillIDs.get_Item(i)))
							{
								SkillConditionItem skillConditionItem = new SkillConditionItem();
								skillConditionItem.conditionData = condition;
								skillConditionItem.skillID = skillIDs.get_Item(i);
								this.conditionTable.get_Item((ConditionType)condition.occasion).Add(skillConditionItem);
							}
						}
					}
				}
			}
		}

		public void UnregistCondition()
		{
			using (Dictionary<ConditionType, List<ConditionItem>>.Enumerator enumerator = this.conditionTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<ConditionType, List<ConditionItem>> current = enumerator.get_Current();
					current.get_Value().Clear();
				}
			}
			this.conditionTable.Clear();
		}

		protected void CheckCondition(ConditionMessage message)
		{
			if (message == null)
			{
				return;
			}
			if (!this.conditionTable.ContainsKey(message.type))
			{
				return;
			}
			List<ConditionItem> list = this.conditionTable.get_Item(message.type);
			for (int i = 0; i < list.get_Count(); i++)
			{
				ConditionItem conditionItem = list.get_Item(i);
				if (conditionItem.conditionData != null)
				{
					if (this.CheckConditionCount(conditionItem))
					{
						if (this.CheckConditionAnnouncer(message))
						{
							if (this.CheckConditionTarget(conditionItem.conditionData, message.announcer))
							{
								if (this.CheckConditionDetail(conditionItem.conditionData, message))
								{
									if (conditionItem.isUseTriggerTimes)
									{
										conditionItem.leftTimes--;
									}
									if (conditionItem.conditionData.delay > 0)
									{
										uint timerID = 0u;
										ConditionItem temp = conditionItem;
										timerID = TimerHeap.AddTimer((uint)conditionItem.conditionData.delay, 0, delegate
										{
											this.conditionTimer.Remove(timerID);
											this.TriggerCondition(temp);
										});
										this.conditionTimer.Add(timerID);
									}
									else
									{
										this.TriggerCondition(conditionItem);
									}
								}
							}
						}
					}
				}
			}
		}

		protected bool CheckConditionCount(ConditionItem conditionItem)
		{
			return !conditionItem.isUseTriggerTimes || conditionItem.leftTimes > 0;
		}

		protected bool CheckConditionAnnouncer(ConditionMessage message)
		{
			ConditionType type = message.type;
			return type == ConditionType.CGComplete || type == ConditionType.RangeTrigger || message.announcer != null;
		}

		protected virtual bool CheckConditionTarget(Condition conditionData, EntityParent announcer)
		{
			switch (conditionData.target)
			{
			case 1:
				return this.owner.Camp != announcer.Camp;
			case 2:
				return this.owner.Camp == announcer.Camp && this.owner.ID != announcer.ID;
			case 3:
				return this.owner.ID == announcer.ID;
			case 4:
				return this.owner.Camp == announcer.Camp;
			case 5:
				return false;
			case 6:
				return true;
			case 7:
				return false;
			case 8:
				return announcer.ID == this.owner.DamageSourceID;
			case 9:
				return announcer.ID == EntityWorld.Instance.EntSelf.ID;
			case 10:
				return announcer.IsLogicBoss;
			case 11:
				return announcer.IsBuffEntity;
			default:
				return true;
			}
		}

		protected bool CheckConditionDetail(Condition conditionData, ConditionMessage message)
		{
			ConditionType type = message.type;
			switch (type)
			{
			case ConditionType.UnderDamageEffect:
				return this.CheckUnderDamageEffectCondition(conditionData, message);
			case ConditionType.AttrChange:
				return this.CheckAttrChangeCondition(conditionData, message);
			case ConditionType.BecameSkillTarget:
				return this.CheckBecameSkillTargetCondition(conditionData, message);
			case ConditionType.EnterBattleField:
				return this.CheckEnterBattleFieldCondition(conditionData, message);
			case ConditionType.ExitBattleField:
				return this.CheckExitBattleFieldCondition(conditionData, message);
			case ConditionType.Alert:
				return this.CheckAlertCondition(conditionData, message);
			case ConditionType.AddBuff:
				return this.CheckAddBuffCondition(conditionData, message);
			case ConditionType.CGComplete:
				return this.CheckCGCompleteCondition(conditionData, message);
			case ConditionType.RangeTrigger:
				return this.CheckRangeTriggerCondition(conditionData, message);
			case ConditionType.CauseDamageEffect:
				return this.CheckCauseDamageEffectCondition(conditionData, message);
			case ConditionType.SummonPet:
				return this.CheckSummonPetCondition(conditionData, message);
			case ConditionType.CauseCrit:
				return this.CheckCauseCritCondition(conditionData, message);
			case ConditionType.CauseMiss:
				return this.CheckCauseMissCondition(conditionData, message);
			case ConditionType.CauseParry:
				return this.CheckCauseParryCondition(conditionData, message);
			case ConditionType.UnderCrit:
				return this.CheckUnderCritCondition(conditionData, message);
			case ConditionType.UnderMiss:
				return this.CheckUnderMissCondition(conditionData, message);
			case ConditionType.UnderParry:
				return this.CheckUnderParryCondition(conditionData, message);
			default:
				return type == ConditionType.UseSkill && this.CheckUseSkillCondition(conditionData, message);
			}
		}

		protected bool CheckUnderDamageEffectCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is UnderDamageEffectConditionMessage))
			{
				return false;
			}
			UnderDamageEffectConditionMessage underDamageEffectConditionMessage = message as UnderDamageEffectConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, underDamageEffectConditionMessage.caster, underDamageEffectConditionMessage.announcer) && (conditionData.effectIdList.get_Count() == 0 || conditionData.effectIdList.Contains(underDamageEffectConditionMessage.effectID));
		}

		protected bool CheckAttrChangeCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is AttrChangeConditionMessage))
			{
				return false;
			}
			if (!this.CheckConditionExtraInspection(conditionData, null, null))
			{
				return false;
			}
			AttrChangeConditionMessage attrChangeConditionMessage = message as AttrChangeConditionMessage;
			AttrType attrType;
			switch (conditionData.attr)
			{
			case 1:
				attrType = AttrType.Hp;
				break;
			case 2:
				attrType = AttrType.ActPoint;
				break;
			case 3:
				attrType = AttrType.Vp;
				break;
			default:
				return false;
			}
			return attrType == attrChangeConditionMessage.attrType && (!this.CheckConditionAttrDetail(conditionData.percentage, attrChangeConditionMessage.oldPercentage * 100.0) || !this.CheckConditionAttrDetail(conditionData.@base, (double)attrChangeConditionMessage.oldValue)) && this.CheckConditionAttrDetail(conditionData.percentage, attrChangeConditionMessage.curPercentage * 100.0) && this.CheckConditionAttrDetail(conditionData.@base, (double)attrChangeConditionMessage.curValue);
		}

		protected bool CheckBecameSkillTargetCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is BecameSkillTargetConditionMessage))
			{
				return false;
			}
			if (conditionData.damageType == null)
			{
				return false;
			}
			BecameSkillTargetConditionMessage becameSkillTargetConditionMessage = message as BecameSkillTargetConditionMessage;
			if (!this.CheckConditionExtraInspection(conditionData, becameSkillTargetConditionMessage.caster, becameSkillTargetConditionMessage.announcer))
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(becameSkillTargetConditionMessage.skillID);
			if (skill == null)
			{
				return false;
			}
			if (conditionData.damageType.get_Count() == 0)
			{
				return true;
			}
			for (int i = 0; i < conditionData.damageType.get_Count(); i++)
			{
				if (conditionData.damageType.get_Item(i) == 0)
				{
					return true;
				}
				if (conditionData.damageType.get_Item(i) == skill.skilltype)
				{
					return true;
				}
			}
			return false;
		}

		protected bool CheckEnterBattleFieldCondition(Condition conditionData, ConditionMessage message)
		{
			return this.CheckConditionExtraInspection(conditionData, null, null) && (conditionData.dataId == 0 || conditionData.dataId == message.announcer.TypeID);
		}

		protected bool CheckExitBattleFieldCondition(Condition conditionData, ConditionMessage message)
		{
			return this.CheckConditionExtraInspection(conditionData, null, null) && (conditionData.dataId == 0 || conditionData.dataId == message.announcer.TypeID);
		}

		protected bool CheckAlertCondition(Condition conditionData, ConditionMessage message)
		{
			return this.CheckConditionExtraInspection(conditionData, null, null);
		}

		protected bool CheckAddBuffCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is AddBuffConditionMessage))
			{
				return false;
			}
			AddBuffConditionMessage addBuffConditionMessage = message as AddBuffConditionMessage;
			if (!this.CheckConditionExtraInspection(conditionData, null, addBuffConditionMessage.announcer))
			{
				return false;
			}
			Buff buff = DataReader<Buff>.Get(addBuffConditionMessage.buffID);
			return buff != null && (conditionData.buffId.Contains(addBuffConditionMessage.buffID) || conditionData.buffType.Contains(buff.type));
		}

		protected bool CheckCGCompleteCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is CGCompleteConditionMessage))
			{
				return false;
			}
			if (!this.CheckConditionExtraInspection(conditionData, null, null))
			{
				return false;
			}
			CGCompleteConditionMessage cGCompleteConditionMessage = message as CGCompleteConditionMessage;
			return conditionData.cgId == cGCompleteConditionMessage.cgID;
		}

		protected bool CheckRangeTriggerCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is RangeTriggerConditionMessage))
			{
				return false;
			}
			if (!this.CheckConditionExtraInspection(conditionData, null, null))
			{
				return false;
			}
			RangeTriggerConditionMessage rangeTriggerConditionMessage = message as RangeTriggerConditionMessage;
			return conditionData.rangeId.Contains(rangeTriggerConditionMessage.rangeID);
		}

		protected bool CheckCauseDamageEffectCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is CauseDamageEffectMessage))
			{
				return false;
			}
			CauseDamageEffectMessage causeDamageEffectMessage = message as CauseDamageEffectMessage;
			return this.CheckConditionExtraInspection(conditionData, causeDamageEffectMessage.announcer, causeDamageEffectMessage.target) && (conditionData.effectIdList.get_Count() == 0 || conditionData.effectIdList.Contains(causeDamageEffectMessage.effectID));
		}

		protected bool CheckSummonPetCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is SummonPetConditionMessage))
			{
				return false;
			}
			if (!this.CheckConditionExtraInspection(conditionData, null, null))
			{
				return false;
			}
			if (conditionData.petType == null)
			{
				return false;
			}
			SummonPetConditionMessage summonPetConditionMessage = message as SummonPetConditionMessage;
			if (conditionData.petType.get_Count() == 0)
			{
				return true;
			}
			for (int i = 0; i < conditionData.petType.get_Count(); i++)
			{
				if (conditionData.petType.get_Item(i) == 0)
				{
					return true;
				}
				if (conditionData.petType.get_Item(i) == summonPetConditionMessage.petType)
				{
					return true;
				}
			}
			return false;
		}

		protected bool CheckCauseCritCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is CauseCritConditionMessage))
			{
				return false;
			}
			CauseCritConditionMessage causeCritConditionMessage = message as CauseCritConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, causeCritConditionMessage.announcer, causeCritConditionMessage.target);
		}

		protected bool CheckCauseMissCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is CauseMissConditionMessage))
			{
				return false;
			}
			CauseMissConditionMessage causeMissConditionMessage = message as CauseMissConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, causeMissConditionMessage.announcer, causeMissConditionMessage.target);
		}

		protected bool CheckCauseParryCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is CauseParryConditionMessage))
			{
				return false;
			}
			CauseParryConditionMessage causeParryConditionMessage = message as CauseParryConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, causeParryConditionMessage.announcer, causeParryConditionMessage.target);
		}

		protected bool CheckUnderCritCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is UnderCritConditionMessage))
			{
				return false;
			}
			UnderCritConditionMessage underCritConditionMessage = message as UnderCritConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, underCritConditionMessage.caster, underCritConditionMessage.announcer);
		}

		protected bool CheckUnderMissCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is UnderMissConditionMessage))
			{
				return false;
			}
			UnderMissConditionMessage underMissConditionMessage = message as UnderMissConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, underMissConditionMessage.caster, underMissConditionMessage.announcer);
		}

		protected bool CheckUnderParryCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is UnderParryConditionMessage))
			{
				return false;
			}
			UnderParryConditionMessage underParryConditionMessage = message as UnderParryConditionMessage;
			return this.CheckConditionExtraInspection(conditionData, underParryConditionMessage.caster, underParryConditionMessage.announcer);
		}

		protected bool CheckUseSkillCondition(Condition conditionData, ConditionMessage message)
		{
			if (!(message is UseSkillConditionMessage))
			{
				return false;
			}
			if (conditionData.damageType == null)
			{
				return false;
			}
			UseSkillConditionMessage useSkillConditionMessage = message as UseSkillConditionMessage;
			if (!this.CheckConditionExtraInspection(conditionData, useSkillConditionMessage.announcer, useSkillConditionMessage.target))
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(useSkillConditionMessage.skillID);
			if (skill == null)
			{
				return false;
			}
			if (conditionData.damageType.get_Count() == 0)
			{
				return true;
			}
			for (int i = 0; i < conditionData.damageType.get_Count(); i++)
			{
				if (conditionData.damageType.get_Item(i) == 0)
				{
					return true;
				}
				if (conditionData.damageType.get_Item(i) == skill.skilltype)
				{
					return true;
				}
			}
			return false;
		}

		protected bool CheckConditionExtraInspection(Condition conditionData, EntityParent offensiveSide, EntityParent defensiveSide)
		{
			if (conditionData.extraInspection == null)
			{
				return true;
			}
			if (conditionData.extraInspection.get_Count() == 0)
			{
				return true;
			}
			for (int i = 0; i < conditionData.extraInspection.get_Count(); i++)
			{
				if (!this.CheckConditionExtraInspection(conditionData.extraInspection.get_Item(i), offensiveSide, defensiveSide))
				{
					return false;
				}
			}
			return true;
		}

		protected bool CheckConditionExtraInspection(int extraID, EntityParent offensiveEntity, EntityParent defensiveEntity)
		{
			ExtraInspection extraInspection = DataReader<ExtraInspection>.Get(extraID);
			if (extraInspection == null)
			{
				return false;
			}
			EntityParent entityParent = null;
			switch (extraInspection.checkType)
			{
			case 1:
				entityParent = this.owner;
				break;
			case 2:
				entityParent = offensiveEntity;
				break;
			case 3:
				entityParent = defensiveEntity;
				break;
			}
			if (entityParent == null)
			{
				return false;
			}
			switch (extraInspection.type)
			{
			case 1:
				return this.CheckConditionExtraInspectionHasBuff(extraInspection, entityParent);
			case 2:
				return this.CheckConditionExtraInspectionAttrState(extraInspection, entityParent);
			case 3:
				return this.CheckConditionExtraInspectionCurrentAction(extraInspection, entityParent);
			default:
				return false;
			}
		}

		protected bool CheckConditionExtraInspectionHasBuff(ExtraInspection extraInspectionData, EntityParent checker)
		{
			if (checker.GetBattleManager() == null)
			{
				return false;
			}
			List<int> buffList = checker.GetBuffManager().GetBuffList();
			for (int i = 0; i < extraInspectionData.buffList.get_Count(); i++)
			{
				if (buffList.Contains(extraInspectionData.buffList.get_Item(i)))
				{
					return true;
				}
			}
			return false;
		}

		protected bool CheckConditionExtraInspectionAttrState(ExtraInspection extraInspectionData, EntityParent checker)
		{
			return this.CheckConditionAttrDetail(extraInspectionData.percentage, (double)((float)checker.Hp / (float)checker.RealHpLmt * 100f));
		}

		protected bool CheckConditionExtraInspectionCurrentAction(ExtraInspection extraInspectionData, EntityParent checker)
		{
			if (!checker.Actor)
			{
				return false;
			}
			for (int i = 0; i < extraInspectionData.actionList.get_Count(); i++)
			{
				if (checker.Actor.CurActionStatus == extraInspectionData.actionList.get_Item(i))
				{
					return true;
				}
			}
			return false;
		}

		protected bool CheckConditionAttrDetail(string comparison, double attr)
		{
			if (comparison == null)
			{
				return true;
			}
			string text = comparison.Replace(" ", string.Empty);
			if (text.Contains("and"))
			{
				string[] array = text.Split("and".ToCharArray());
				if (array.Length > 3)
				{
					return EntityWorld.Instance.CompareFilter(array[0], attr) && EntityWorld.Instance.CompareFilter(array[3], attr);
				}
			}
			else
			{
				if (!text.Contains("or"))
				{
					return EntityWorld.Instance.CompareFilter(text, attr);
				}
				string[] array2 = text.Split("or".ToCharArray());
				if (array2.Length > 3)
				{
					return EntityWorld.Instance.CompareFilter(array2[0], attr) && EntityWorld.Instance.CompareFilter(array2[3], attr);
				}
			}
			return true;
		}

		protected void TriggerCondition(ConditionItem item)
		{
			if (item is CounterSkillConditionItem)
			{
				this.TriggerCounterSkillCondition(item as CounterSkillConditionItem);
			}
			else if (item is ThinkConditionItem)
			{
				this.TriggerThinkCondition(item as ThinkConditionItem);
			}
			else if (item is SkillConditionItem)
			{
				this.TriggerSkillCondition(item as SkillConditionItem);
			}
		}

		protected void TriggerCounterSkillCondition(CounterSkillConditionItem item)
		{
			if (InstanceManager.IsLocalBattle)
			{
				this.owner.GetSkillManager().ClientCastSkillByID(item.counterSkillID);
			}
		}

		protected void TriggerThinkCondition(ThinkConditionItem item)
		{
			this.owner.LastTriggerConditionMessage = item;
			this.owner.LastTriggerConditionID = item.conditionData.conditionId;
			this.owner.GetAIManager().TryThink();
		}

		private void TriggerSkillCondition(SkillConditionItem item)
		{
			if (InstanceManager.IsLocalBattle)
			{
				this.owner.GetSkillManager().ClientCastSkillByID(item.skillID);
			}
		}
	}
}
