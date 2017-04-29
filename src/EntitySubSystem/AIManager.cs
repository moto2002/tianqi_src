using AIMind;
using AIRuntime;
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public class AIManager : IAIProc, IAIManager, ISubSystem
	{
		protected EntityParent owner;

		protected ActorParent ownerActor;

		protected BTBlackBoard blackBoard;

		protected BTNode aiRoot;

		protected string aiType = string.Empty;

		protected int thinkInterval;

		protected uint timerID;

		protected object thisLock = new object();

		protected bool isActive;

		protected bool isThinking;

		protected bool isFirstActive = true;

		protected int moveSkipThinkCount;

		protected float moveSkipTime;

		protected XDict<int, List<Vector3>> pointGroupCache = new XDict<int, List<Vector3>>();

		protected XDict<int, int> pointGroupIndex = new XDict<int, int>();

		protected List<Command> allAINode = new List<Command>();

		protected BTNode AIRoot
		{
			get
			{
				return this.aiRoot;
			}
			set
			{
				if (value != this.aiRoot)
				{
					this.SetAllAINode(value);
				}
				this.aiRoot = value;
			}
		}

		public string AIType
		{
			get
			{
				return this.aiType;
			}
			set
			{
				this.aiType = value;
			}
		}

		public int ThinkInterval
		{
			get
			{
				return this.thinkInterval;
			}
			set
			{
				this.thinkInterval = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		public bool IsThinking
		{
			get
			{
				return this.isThinking;
			}
			set
			{
				this.isThinking = value;
			}
		}

		protected bool IsFirstActive
		{
			get
			{
				return this.isFirstActive;
			}
			set
			{
				this.isFirstActive = value;
			}
		}

		public int MoveSkipThinkCount
		{
			get
			{
				return this.moveSkipThinkCount;
			}
			set
			{
				this.moveSkipThinkCount = value;
			}
		}

		public float MoveSkipTime
		{
			get
			{
				return this.moveSkipTime;
			}
			set
			{
				this.moveSkipTime = value;
			}
		}

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.ownerActor = this.owner.Actor;
			this.AddListeners();
		}

		public void OnDestroy()
		{
			this.RemoveListners();
			TimerHeap.DelTimer(this.timerID);
			this.owner = null;
			this.ownerActor = null;
		}

		protected virtual void AddListeners()
		{
			EventDispatcher.AddListener(AIManagerEvent.PauseAI, new Callback(this.PauseThink));
			EventDispatcher.AddListener(AIManagerEvent.ResumeAI, new Callback(this.ResumeThink));
		}

		protected virtual void RemoveListners()
		{
			EventDispatcher.RemoveListener(AIManagerEvent.PauseAI, new Callback(this.PauseThink));
			EventDispatcher.RemoveListener(AIManagerEvent.ResumeAI, new Callback(this.ResumeThink));
		}

		public virtual void Active()
		{
			if (this.IsActive)
			{
				return;
			}
			TimerHeap.DelTimer(this.timerID);
			this.GetAIDataByType();
			this.AIRoot = BTLoader.GetBehaviorTree(this.AIType);
			if (this.AIRoot == null)
			{
				return;
			}
			this.IsActive = true;
			this.IsThinking = InstanceManager.IsAIThinking;
			this.owner.GetConditionManager().RegistThinkCondition(this.GetAIConditionMessage(this.AIRoot));
			this.timerID = TimerHeap.AddTimer(0u, this.ThinkInterval, new System.Action(this.Think));
		}

		public virtual void Deactive()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.IsActive = false;
			this.IsThinking = false;
			TimerHeap.DelTimer(this.timerID);
			if (this.ownerActor)
			{
				this.ownerActor.StopAIMove();
			}
		}

		public void PauseThink()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.IsThinking = false;
			if (this.ownerActor)
			{
				this.ownerActor.StopAIMove();
			}
		}

		public void ResumeThink()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.IsThinking = true;
		}

		public void TryThink()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.Think();
		}

		protected virtual void Think()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.IsActive)
				{
					if (this.IsThinking)
					{
						this.AIRoot.Proc(this);
					}
				}
			}
		}

		protected virtual void GetAIDataByType()
		{
		}

		protected List<int> GetAIConditionMessage(BTNode aiRoot)
		{
			List<int> list = new List<int>();
			if (!(aiRoot.root is CompositeNode))
			{
				return list;
			}
			for (int i = 0; i < this.allAINode.get_Count(); i++)
			{
				if (this.allAINode.get_Item(i) is CheckConditionNode)
				{
					list.Add((this.allAINode.get_Item(i) as CheckConditionNode).conditionID);
				}
			}
			return list;
		}

		public bool IsAIActive()
		{
			return this.IsActive;
		}

		public void ResetAIManager()
		{
			this.IsFirstActive = true;
		}

		public void UpdateActor(ActorParent actor)
		{
			this.ownerActor = actor;
		}

		public virtual bool CheckSelf()
		{
			if (this.MoveSkipThinkCount > 0)
			{
				this.MoveSkipThinkCount--;
				return false;
			}
			if (this.MoveSkipTime > 0f)
			{
				this.MoveSkipTime -= (float)this.ThinkInterval * 0.001f;
				return false;
			}
			return !this.owner.IsStatic && !this.owner.IsDizzy && !this.owner.IsDead && !this.owner.IsAssault && (!this.ownerActor || !ActionStatusName.IsSkillAction(this.ownerActor.CurActionStatus) || this.owner.IsMoveCast || this.ownerActor.IsUnderTermination);
		}

		public bool CheckCondition(int conditionID)
		{
			if (this.owner.LastTriggerConditionID != conditionID)
			{
				return false;
			}
			if (this.owner.LastTriggerConditionMessage == null)
			{
				return false;
			}
			this.owner.LastTriggerConditionID = 0;
			return true;
		}

		public bool CheckRandom(int random)
		{
			return Random.Range(1, 101) < random;
		}

		public bool CheckOwnerContainBuffID(int buffID)
		{
			return this.owner.GetBuffManager() != null && this.owner.GetBuffManager().HasBuff(buffID);
		}

		public bool CheckTargetContainBuffID(int buffID)
		{
			return this.owner.AITarget != null && this.owner.AITarget.GetBuffManager() != null && this.owner.AITarget.GetBuffManager().HasBuff(buffID);
		}

		public bool CheckSkillInCDByIndex(int skillIndex)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.CheckSkillInCDByID(skillID);
		}

		protected bool CheckSkillInCDByID(int skillID)
		{
			return this.owner.GetSkillManager().CheckSkillInCDByID(skillID);
		}

		public bool CheckTargetDistance(ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			if (!this.ownerActor)
			{
				return false;
			}
			if (this.owner.AITarget == null || this.owner.AITarget.Actor == null || this.owner.AITarget.Actor.FixTransform == null)
			{
				return false;
			}
			if (comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None)
			{
				return false;
			}
			bool flag = false;
			switch (comparisonOperator1)
			{
			case ComparisonOperator.GreaterThan:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) > range1);
				break;
			case ComparisonOperator.GreatThanOrEqual:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) >= range1);
				break;
			case ComparisonOperator.Equal:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) == range1);
				break;
			case ComparisonOperator.LessThanOrEqual:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) <= range1);
				break;
			case ComparisonOperator.LessThan:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) < range1);
				break;
			}
			if (comparisonOperator2 == ComparisonOperator.None)
			{
				return flag;
			}
			bool flag2 = false;
			switch (comparisonOperator2)
			{
			case ComparisonOperator.GreaterThan:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) > range2);
				break;
			case ComparisonOperator.GreatThanOrEqual:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) >= range2);
				break;
			case ComparisonOperator.Equal:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) == range2);
				break;
			case ComparisonOperator.LessThanOrEqual:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) <= range2);
				break;
			case ComparisonOperator.LessThan:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) < range2);
				break;
			}
			if (comparisonOperator1 == ComparisonOperator.None)
			{
				return flag2;
			}
			if (logicalOperator != LogicalOperator.And)
			{
				return logicalOperator == LogicalOperator.Or && (flag || flag2);
			}
			return flag && flag2;
		}

		public bool CheckDistanceBetweenOwnerAndTarget(float distance)
		{
			return this.owner.AITarget != null && XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) <= distance;
		}

		public bool CheckTargetDistanceBySkillIndex(int skillIndex)
		{
			int key;
			if (!this.GetSkillIDBySkillIndex(skillIndex, out key))
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(key);
			if (skill == null)
			{
				return false;
			}
			if (this.owner.AITarget == null)
			{
				return false;
			}
			if (this.owner.AITarget.Actor == null || this.owner.AITarget.Actor.FixTransform == null)
			{
				return false;
			}
			if (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) <= (float)skill.reach.get_Item(0) * 0.01f + XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform) && XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.AITarget.Actor.FixTransform.get_position()) >= skill.reachLimit * 0.01f - XUtility.GetHitRadius(this.owner.AITarget.Actor.FixTransform))
			{
				float num = Vector3.Angle(this.ownerActor.FixTransform.get_forward(), new Vector3(this.owner.AITarget.Actor.FixTransform.get_position().x, this.ownerActor.FixTransform.get_position().y, this.owner.AITarget.Actor.FixTransform.get_position().z) - this.ownerActor.FixTransform.get_position());
				if (num <= (float)skill.reach.get_Item(1) || this.ownerActor.FixTransform.get_position() == this.owner.AITarget.Actor.FixTransform.get_position())
				{
					return true;
				}
			}
			return false;
		}

		public bool CheckPointDistance(int groupID, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			if (comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None)
			{
				return false;
			}
			Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, groupID);
			Vector2 vector = new Vector3(point.x, this.ownerActor.FixTransform.get_position().y, point.y);
			bool flag = false;
			switch (comparisonOperator1)
			{
			case ComparisonOperator.GreaterThan:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) > range1);
				break;
			case ComparisonOperator.GreatThanOrEqual:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) >= range1);
				break;
			case ComparisonOperator.Equal:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) == range1);
				break;
			case ComparisonOperator.LessThanOrEqual:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) <= range1);
				break;
			case ComparisonOperator.LessThan:
				flag = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) < range1);
				break;
			}
			if (comparisonOperator2 == ComparisonOperator.None)
			{
				return flag;
			}
			bool flag2 = false;
			switch (comparisonOperator2)
			{
			case ComparisonOperator.GreaterThan:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) > range2);
				break;
			case ComparisonOperator.GreatThanOrEqual:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) >= range2);
				break;
			case ComparisonOperator.Equal:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) == range2);
				break;
			case ComparisonOperator.LessThanOrEqual:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) <= range2);
				break;
			case ComparisonOperator.LessThan:
				flag2 = (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector) < range2);
				break;
			}
			if (comparisonOperator1 == ComparisonOperator.None)
			{
				return flag2;
			}
			if (logicalOperator != LogicalOperator.And)
			{
				return logicalOperator == LogicalOperator.Or && (flag || flag2);
			}
			return flag && flag2;
		}

		public bool CheckExitWeak()
		{
			return this.owner.IsWeak && (float)this.owner.Vp + 0.01f >= (float)this.owner.RealVpLmt && this.owner.RealVpLmt > 0;
		}

		public bool CheckIsInStrategy(int strategyID, bool isTrue)
		{
			return this.owner.GetFeedbackManager() != null && strategyID <= this.owner.GetFeedbackManager().AllStrategyTypeCount && !(strategyID == (int)this.owner.GetFeedbackManager().CurStrategyType ^ isTrue);
		}

		public bool CheckCanChangeActionTo(string newAction)
		{
			return this.ownerActor.CanChangeActionTo(newAction, true, 0, false);
		}

		public bool SetEnemyTargetType(int mode)
		{
			this.owner.CurEnemyTargetFixType = (EnemyTargetFixType)mode;
			return true;
		}

		public bool ResetEnemyTargetType()
		{
			this.owner.CurEnemyTargetFixType = EnemyTargetFixType.None;
			return true;
		}

		public bool SetTargetBySkillID(int skillID, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = -1f)
		{
			if (this.owner.GetSkillManager() == null)
			{
				return false;
			}
			if (!DataReader<Skill>.Contains(skillID))
			{
				return false;
			}
			bool flag = (float)Random.Range(1, 101) < reuseRate;
			EntityParent aITarget = this.owner.AITarget;
			float rushDistance = (float)((!isUseRushDistance) ? 0 : DataReader<Skill>.Get(skillID).rush);
			bool flag2 = this.owner.GetSkillManager().SetTargetBySkillID(skillID, rangeType, rushDistance);
			if (flag2 && flag && this.owner.GetSkillManager().CheckTargetBySkillID(aITarget, skillID, rangeType, rushDistance))
			{
				this.owner.AITarget = aITarget;
			}
			return flag2;
		}

		public bool SetTargetBySkillIndex(int skillIndex, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = -1f)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.SetTargetBySkillID(skillID, rangeType, isUseRushDistance, reuseRate);
		}

		public bool SetTargetBySkillType(int skillType, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = -1f)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.SetTargetBySkillID(skillID, rangeType, isUseRushDistance, reuseRate);
		}

		public bool SetTargetFromLockOnTargetBySkillID(int skillID, TargetRangeType rangeType, bool isUseRushDistance)
		{
			if (EntityWorld.Instance.LockOnTarget == null)
			{
				return false;
			}
			if (!EntityWorld.Instance.LockOnTarget.Actor)
			{
				return false;
			}
			if (this.owner.GetSkillManager() == null)
			{
				return false;
			}
			if (!DataReader<Skill>.Contains(skillID))
			{
				return false;
			}
			float rushDistance = (float)((!isUseRushDistance) ? 0 : DataReader<Skill>.Get(skillID).rush);
			if (!this.owner.GetSkillManager().CheckTargetBySkillID(EntityWorld.Instance.LockOnTarget, skillID, rangeType, rushDistance))
			{
				return false;
			}
			this.owner.AITarget = EntityWorld.Instance.LockOnTarget;
			return true;
		}

		public bool SetTargetFromLockOnTargetBySkillIndex(int skillIndex, TargetRangeType rangeType, bool isUseRushDistance)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.SetTargetFromLockOnTargetBySkillID(skillID, rangeType, isUseRushDistance);
		}

		public bool SetTargetFromLockOnTargetBySkillType(int skillType, TargetRangeType rangeType, bool isUseRushDistance)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.SetTargetFromLockOnTargetBySkillID(skillID, rangeType, isUseRushDistance);
		}

		public bool MoveToTargetBySkillID(int skillID)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(skillID);
			return skill != null && skill.reach != null && skill.reach.get_Count() != 0 && this.ownerActor.MoveToSkillTarget((float)skill.reach.get_Item(0) * 0.01f);
		}

		public bool MoveToTargetBySkillIndex(int skillIndex)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.MoveToTargetBySkillID(skillID);
		}

		public bool MoveToTargetBySkillType(int skillType)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.MoveToTargetBySkillID(skillID);
		}

		public bool MoveToSkillEdgeBySkillID(int skillID)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(skillID);
			return skill != null && skill.reach != null && skill.reach.get_Count() != 0 && this.ownerActor.MoveToSkillEdge((float)skill.reach.get_Item(0) * 0.01f);
		}

		public bool MoveToSkillEdgeBySkillIndex(int skillIndex)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.MoveToSkillEdgeBySkillID(skillID);
		}

		public bool MoveToSkillEdgeBySkillType(int skillType)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.MoveToSkillEdgeBySkillID(skillID);
		}

		public virtual bool MoveToEffectOutside(int thinkCount)
		{
			return !this.owner.IsWeak && this.ownerActor && this.owner.GetWarningManager().ExecuteWarningMessage(delegate
			{
				this.MoveSkipThinkCount = thinkCount;
			}, delegate
			{
				this.MoveSkipThinkCount = 0;
			});
		}

		public bool MoveToCurrentBatchPoint()
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			if (this.owner.AITarget != null)
			{
				return false;
			}
			List<EntityParent> values = EntityWorld.Instance.AllEntities.Values;
			for (int i = 0; i < values.get_Count(); i++)
			{
				if (values.get_Item(i).Camp != this.owner.Camp)
				{
					return false;
				}
			}
			Vector2 zero = Vector2.get_zero();
			if (InstanceManager.GetCurrentBattlePathPoint(out zero))
			{
				if (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), new Vector3(zero.x, this.ownerActor.FixTransform.get_position().y, zero.y)) > 0.5f)
				{
					this.ownerActor.MoveToPoint(new Vector3(zero.x, this.ownerActor.FixTransform.get_position().y, zero.y), 0f, null);
				}
				return true;
			}
			return false;
		}

		public bool MoveToCurrentBatchPointIgnoreCamp()
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			Vector2 zero = Vector2.get_zero();
			if (InstanceManager.GetCurrentBattlePathPoint(out zero))
			{
				if (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), new Vector3(zero.x, this.ownerActor.FixTransform.get_position().y, zero.y)) > 0.5f)
				{
					this.ownerActor.MoveToPoint(new Vector3(zero.x, this.ownerActor.FixTransform.get_position().y, zero.y), 0f, null);
				}
				return true;
			}
			return false;
		}

		public bool MoveAround(int random, float time)
		{
			if (!this.CheckRandom(random))
			{
				return false;
			}
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			if (this.owner.AITarget == null)
			{
				return false;
			}
			if (this.owner.AITarget.ID == this.owner.ID)
			{
				return false;
			}
			this.owner.MoveAroundCenter = this.owner.AITarget;
			this.ownerActor.IsRight = (new Random().Next(0, 2) == 0);
			if (this.ownerActor.MoveAroundCenter(time))
			{
				this.MoveSkipTime = time;
				return true;
			}
			return false;
		}

		public bool MoveBack(int random, float time)
		{
			if (!this.CheckRandom(random))
			{
				return false;
			}
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			if (this.owner.AITarget == null)
			{
				return false;
			}
			if (this.owner.AITarget.ID == this.owner.ID)
			{
				return false;
			}
			this.owner.MoveAroundCenter = this.owner.AITarget;
			if (this.ownerActor.MoveBackCenter(time))
			{
				this.MoveSkipTime = time;
				return true;
			}
			return false;
		}

		public bool MoveToFollowTarget(FollowTargetType followTargetType, float searchRange, float startDistance, float stopDistance)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			float hitRadius = XUtility.GetHitRadius(this.ownerActor.FixTransform);
			if (this.owner.FollowTarget == null)
			{
				List<EntityParent> list = new List<EntityParent>();
				switch (followTargetType)
				{
				case FollowTargetType.Self:
					list.Add(EntityWorld.Instance.EntSelf);
					break;
				case FollowTargetType.OtherPlayer:
					list.AddRange(EntityWorld.Instance.GetEntities<EntityPlayer>().Values);
					break;
				case FollowTargetType.Boss:
				{
					List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
					for (int i = 0; i < values.get_Count(); i++)
					{
						if (values.get_Item(i).IsLogicBoss)
						{
							list.Add(values.get_Item(i));
						}
					}
					break;
				}
				}
				EntityParent followTarget = null;
				float num = 3.40282347E+38f;
				for (int j = 0; j < list.get_Count(); j++)
				{
					if (list.get_Item(j).Actor)
					{
						if (list.get_Item(j).Actor.FixTransform)
						{
							float num2 = XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), list.get_Item(j).Actor.FixTransform.get_position()) - hitRadius - XUtility.GetHitRadius(list.get_Item(j).Actor.FixTransform);
							if (num2 < searchRange && num2 < num)
							{
								followTarget = list.get_Item(j);
								num = num2;
							}
						}
					}
				}
				this.owner.FollowTarget = followTarget;
			}
			if (this.owner.FollowTarget == null)
			{
				return false;
			}
			if (!this.owner.FollowTarget.Actor)
			{
				this.owner.FollowTarget = null;
				return false;
			}
			float num3 = XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), this.owner.FollowTarget.Actor.FixTransform.get_position()) - hitRadius - XUtility.GetHitRadius(this.owner.FollowTarget.Actor.FixTransform);
			if (num3 < stopDistance)
			{
				this.ownerActor.StopMoveToPoint();
			}
			else if (num3 > startDistance)
			{
				this.ownerActor.MoveToPoint(this.owner.FollowTarget.Actor.FixTransform.get_position(), 0f, null);
			}
			return true;
		}

		public bool MoveToPoint(float x, float z, float range)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			Vector3 terrainPoint = MySceneManager.GetTerrainPoint(x, z, this.ownerActor.FixTransform.get_position().y);
			if (XUtility.DistanceNoY(terrainPoint, this.ownerActor.FixTransform.get_position()) > range)
			{
				this.ownerActor.MoveToPoint(terrainPoint, 0f, null);
			}
			return true;
		}

		public bool MoveByPointGroup(int groupID)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			if (!this.pointGroupCache.ContainsKey(groupID))
			{
				ArrayList pointDataByGroupKey = MapDataManager.Instance.GetPointDataByGroupKey(MySceneManager.Instance.CurSceneID, groupID);
				if (pointDataByGroupKey == null)
				{
					return false;
				}
				this.pointGroupCache.Add(groupID, new List<Vector3>());
				for (int i = 0; i < pointDataByGroupKey.get_Count(); i++)
				{
					Hashtable hashtable = (Hashtable)pointDataByGroupKey.get_Item(i);
					float x = (float)((double)hashtable.get_Item("x") * 0.0099999997764825821);
					float z = (float)((double)hashtable.get_Item("y") * 0.0099999997764825821);
					this.pointGroupCache[groupID].Add(MySceneManager.GetTerrainPoint(x, z, this.owner.CurFloorStandardHeight));
				}
			}
			if (this.pointGroupCache[groupID].get_Count() == 0)
			{
				return false;
			}
			if (!this.pointGroupIndex.ContainsKey(groupID))
			{
				this.pointGroupIndex.Add(groupID, 0);
			}
			this.ownerActor.MoveToPoint(this.pointGroupCache[groupID].get_Item(this.pointGroupIndex[groupID]), 0f, delegate
			{
				this.pointGroupIndex[groupID] = ((this.pointGroupIndex[groupID] + 1 >= this.pointGroupCache[groupID].get_Count()) ? 0 : (this.pointGroupIndex[groupID] + 1));
				this.Think();
			});
			return true;
		}

		public bool MoveByForward()
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			this.ownerActor.MoveToPoint(this.ownerActor.FixTransform.get_position() + this.ownerActor.FixTransform.get_forward().get_normalized() * 10000f, 0f, null);
			return true;
		}

		public bool MoveByForwardInThinkCount(int thinkCount)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			Vector3 position = this.ownerActor.FixTransform.get_position();
			for (int i = 0; i < thinkCount; i++)
			{
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(position + this.ownerActor.FixTransform.get_forward().get_normalized() * this.ownerActor.PureMoveSpeed * (float)this.ThinkInterval * 0.001f * (float)i, ref navMeshHit, 10f, -1))
				{
					position = navMeshHit.get_position();
				}
			}
			this.ownerActor.MoveToPoint(position, 0f, null);
			this.MoveSkipThinkCount = thinkCount;
			return true;
		}

		public bool StareBlanklyInThinkCount(int thinkCount)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			this.ownerActor.StopAllMove();
			this.MoveSkipThinkCount = thinkCount;
			return true;
		}

		public bool TurnToRandomDir(float angle1, float angle2)
		{
			if (this.owner.IsWeak)
			{
				return true;
			}
			if (!this.ownerActor)
			{
				return true;
			}
			if (this.ownerActor.IsLockModelDir)
			{
				return true;
			}
			if (!XUtility.StartsWith(this.ownerActor.CurActionStatus, "idle"))
			{
				return true;
			}
			this.ownerActor.ForceSetDirection(Quaternion.Euler(this.ownerActor.FixTransform.get_eulerAngles().x, this.ownerActor.FixTransform.get_eulerAngles().y + Random.Range(angle1, angle2), this.ownerActor.FixTransform.get_eulerAngles().z) * Vector3.get_forward());
			this.ownerActor.ApplyMovingDirAsForward();
			return true;
		}

		public bool CastSkillBySkillID(int skillID)
		{
			return (!ActionStatusName.IsSkillAction(this.ownerActor.CurActionStatus) || this.ownerActor.IsUnderTermination) && this.owner.GetSkillManager().ClientCastSkillByID(skillID);
		}

		public bool CastSkillBySkillIndex(int skillIndex)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.CastSkillBySkillID(skillID);
		}

		public bool CastSkillBySkillType(int skillType)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.CastSkillBySkillID(skillID);
		}

		public virtual bool PressIcon(int skillIndex, int count, int interval)
		{
			return false;
		}

		public bool PackTryToCastSkillBySkillID(int skillID, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			if (ActionStatusName.IsSkillAction(this.ownerActor.CurActionStatus) && !this.ownerActor.IsUnderTermination)
			{
				return false;
			}
			List<int> skillAllValue = this.owner.GetSkillAllValue();
			return skillAllValue.get_Count() != 0 && skillAllValue.Contains(skillID) && !this.CheckSkillInCDByID(skillID) && this.SetTargetBySkillID(skillID, TargetRangeType.SkillRange, false, -1f) && ((comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None) || this.CheckTargetDistance(comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator)) && this.CheckRandom(random) && this.owner.GetSkillManager().ClientCastSkillByID(skillID);
		}

		public bool PackTryToCastSkillBySkillIndex(int skillIndex, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.PackTryToCastSkillBySkillID(skillID, random, comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator);
		}

		public bool PackTryToCastSkillBySkillType(int skillType, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.PackTryToCastSkillBySkillID(skillID, random, comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator);
		}

		public bool PackTryToPressIcon(int skillIndex, int count, int interval, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator, TargetRangeType rangeType)
		{
			int num;
			if (!this.GetSkillIDBySkillIndex(skillIndex, out num))
			{
				return false;
			}
			if (!this.SetTargetBySkillID(num, rangeType, true, -1f))
			{
				return false;
			}
			if (comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None)
			{
				if (!this.CheckTargetDistanceBySkillIndex(skillIndex))
				{
					return false;
				}
			}
			else if (!this.CheckTargetDistance(comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator))
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(num);
			return this.owner.ActPoint + skill.actionPoint + this.owner.GetSkillActionPointVariationByType(skill.skilltype) >= 0 && this.CheckRandom(random) && this.PressIcon(skillIndex, count, interval);
		}

		public bool PackTryToMoveToTargetBySkillID(int skillID, int random, TargetRangeType rangeType)
		{
			return this.CheckRandom(random) && this.SetTargetBySkillID(skillID, rangeType, false, -1f) && this.MoveToTargetBySkillID(skillID);
		}

		public bool PackTryToMoveToTargetBySkillIndex(int skillIndex, int random, TargetRangeType rangeType)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.PackTryToMoveToTargetBySkillID(skillID, random, rangeType);
		}

		public bool PackTryToMoveToTargetBySkillType(int skillType, int random, TargetRangeType rangeType)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.PackTryToMoveToTargetBySkillID(skillID, random, rangeType);
		}

		public bool PackTryToCastSkillBySkillIDAndLockOnTarget(int skillID, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			if (ActionStatusName.IsSkillAction(this.ownerActor.CurActionStatus) && !this.ownerActor.IsUnderTermination)
			{
				return false;
			}
			List<int> skillAllValue = this.owner.GetSkillAllValue();
			return skillAllValue.get_Count() != 0 && skillAllValue.Contains(skillID) && !this.CheckSkillInCDByID(skillID) && this.SetTargetFromLockOnTargetBySkillID(skillID, TargetRangeType.SkillRange, false) && ((comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None) || this.CheckTargetDistance(comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator)) && this.CheckRandom(random) && this.owner.GetSkillManager().ClientCastSkillByID(skillID);
		}

		public bool PackTryToCastSkillBySkillIndexAndLockOnTarget(int skillIndex, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.PackTryToCastSkillBySkillIDAndLockOnTarget(skillID, random, comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator);
		}

		public bool PackTryToCastSkillBySkillTypeAndLockOnTarget(int skillType, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.PackTryToCastSkillBySkillIDAndLockOnTarget(skillID, random, comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator);
		}

		public bool PackTryToPressIconByLockOnTarget(int skillIndex, int count, int interval, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator, TargetRangeType rangeType)
		{
			int num;
			if (!this.GetSkillIDBySkillIndex(skillIndex, out num))
			{
				return false;
			}
			if (!this.SetTargetFromLockOnTargetBySkillID(num, rangeType, true))
			{
				return false;
			}
			if (comparisonOperator1 == ComparisonOperator.None && comparisonOperator2 == ComparisonOperator.None)
			{
				if (!this.CheckTargetDistanceBySkillIndex(skillIndex))
				{
					return false;
				}
			}
			else if (!this.CheckTargetDistance(comparisonOperator1, range1, comparisonOperator2, range2, logicalOperator))
			{
				return false;
			}
			Skill skill = DataReader<Skill>.Get(num);
			return this.owner.ActPoint + skill.actionPoint + this.owner.GetSkillActionPointVariationByType(skill.skilltype) >= 0 && this.CheckRandom(random) && this.PressIcon(skillIndex, count, interval);
		}

		public bool PackTryToMoveToTargetBySkillIDAndLockOnTarget(int skillID, int random, TargetRangeType rangeType)
		{
			return this.CheckRandom(random) && this.SetTargetFromLockOnTargetBySkillID(skillID, rangeType, false) && this.MoveToTargetBySkillID(skillID);
		}

		public bool PackTryToMoveToTargetBySkillIndexAndLockOnTarget(int skillIndex, int random, TargetRangeType rangeType)
		{
			int skillID;
			return this.GetSkillIDBySkillIndex(skillIndex, out skillID) && this.PackTryToMoveToTargetBySkillIDAndLockOnTarget(skillID, random, rangeType);
		}

		public bool PackTryToMoveToTargetBySkillTypeAndLockOnTarget(int skillType, int random, TargetRangeType rangeType)
		{
			int skillID;
			return this.GetSkillIDBySkillType(skillType, out skillID) && this.PackTryToMoveToTargetBySkillIDAndLockOnTarget(skillID, random, rangeType);
		}

		public bool ChangeAction(string newAction)
		{
			return this.ownerActor.ChangeAction(newAction, false, true, 1f, 0, 0, string.Empty);
		}

		public BTBlackBoard GetBlackBoard()
		{
			return this.blackBoard;
		}

		protected void SetAllAINode(BTNode root)
		{
			this.allAINode.Clear();
			if (root == null)
			{
				return;
			}
			this.allAINode.Add(root);
			for (int i = 0; i < this.allAINode.get_Count(); i++)
			{
				if (this.allAINode.get_Item(i) is CompositeNode)
				{
					this.allAINode.AddRange((this.allAINode.get_Item(i) as CompositeNode).GetChild());
				}
			}
		}

		protected bool ContainAINode(Type type)
		{
			for (int i = 0; i < this.allAINode.get_Count(); i++)
			{
				if (this.allAINode.get_Item(i).GetType() == type)
				{
					return true;
				}
			}
			return false;
		}

		protected bool GetSkillIDBySkillIndex(int skillIndex, out int skillID)
		{
			return this.owner.GetSkillManager().GetSkillIDByIndex(skillIndex, out skillID);
		}

		protected bool GetSkillIDBySkillType(int skillType, out int skillID)
		{
			List<int> skillAllValue = this.owner.GetSkillAllValue();
			if (skillAllValue.get_Count() == 0)
			{
				skillID = 0;
				return false;
			}
			List<int> list = new List<int>();
			for (int i = 0; i < skillAllValue.get_Count(); i++)
			{
				Skill skill = DataReader<Skill>.Get(skillAllValue.get_Item(i));
				if (skill != null)
				{
					if (skill.skilltype == skillType)
					{
						list.Add(skillAllValue.get_Item(i));
					}
				}
			}
			if (list.get_Count() == 0)
			{
				skillID = 0;
				return false;
			}
			skillID = list.get_Item(Random.Range(0, list.get_Count()));
			return true;
		}
	}
}
