using AIRuntime;
using System;

namespace AIMind
{
	public interface IAIProc
	{
		BTBlackBoard GetBlackBoard();

		bool CheckSelf();

		bool CheckCondition(int conditionID);

		bool CheckRandom(int random);

		bool CheckOwnerContainBuffID(int buffID);

		bool CheckTargetContainBuffID(int buffID);

		bool CheckSkillInCDByIndex(int skillIndex);

		bool CheckTargetDistanceBySkillIndex(int skillIndex);

		bool CheckTargetDistance(ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool CheckDistanceBetweenOwnerAndTarget(float distance);

		bool CheckPointDistance(int pointID, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool CheckCanChangeActionTo(string newAct);

		bool CheckExitWeak();

		bool CheckIsInStrategy(int strategyID, bool isTrue);

		bool SetEnemyTargetType(int mode);

		bool ResetEnemyTargetType();

		bool SetTargetBySkillID(int skillID, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = 0f);

		bool SetTargetBySkillIndex(int skillIndex, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = 0f);

		bool SetTargetBySkillType(int skillType, TargetRangeType rangeType, bool isUseRushDistance, float reuseRate = 0f);

		bool SetTargetFromLockOnTargetBySkillID(int skillID, TargetRangeType rangeType, bool isUseRushDistance);

		bool SetTargetFromLockOnTargetBySkillIndex(int skillIndex, TargetRangeType rangeType, bool isUseRushDistance);

		bool SetTargetFromLockOnTargetBySkillType(int skillType, TargetRangeType rangeType, bool isUseRushDistance);

		bool MoveToTargetBySkillID(int skillID);

		bool MoveToTargetBySkillIndex(int skillIndex);

		bool MoveToTargetBySkillType(int skillType);

		bool MoveToSkillEdgeBySkillID(int skillID);

		bool MoveToSkillEdgeBySkillIndex(int skillIndex);

		bool MoveToSkillEdgeBySkillType(int skillID);

		bool MoveToEffectOutside(int thinkCount);

		bool MoveToCurrentBatchPoint();

		bool MoveToCurrentBatchPointIgnoreCamp();

		bool MoveAround(int random, float time);

		bool MoveBack(int random, float time);

		bool MoveToFollowTarget(FollowTargetType followTargetType, float searchRange, float startDistance, float stopDistance);

		bool MoveToPoint(float x, float z, float range);

		bool MoveByPointGroup(int pointID);

		bool MoveByForward();

		bool MoveByForwardInThinkCount(int thinkCount);

		bool StareBlanklyInThinkCount(int thinkCount);

		bool TurnToRandomDir(float angle1, float angle2);

		bool CastSkillBySkillID(int skillID);

		bool CastSkillBySkillIndex(int skillIndex);

		bool CastSkillBySkillType(int skillType);

		bool PressIcon(int icon, int count, int interval);

		bool PackTryToCastSkillBySkillID(int skillID, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToCastSkillBySkillIndex(int skillIndex, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToCastSkillBySkillType(int skillType, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToPressIcon(int skillIndex, int count, int interval, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillID(int skillID, int random, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillIndex(int skillIndex, int random, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillType(int skillType, int random, TargetRangeType rangeType);

		bool PackTryToCastSkillBySkillIDAndLockOnTarget(int skillID, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToCastSkillBySkillIndexAndLockOnTarget(int skillIndex, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToCastSkillBySkillTypeAndLockOnTarget(int skillType, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator);

		bool PackTryToPressIconByLockOnTarget(int skillIndex, int count, int interval, int random, ComparisonOperator comparisonOperator1, float range1, ComparisonOperator comparisonOperator2, float range2, LogicalOperator logicalOperator, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillIDAndLockOnTarget(int skillID, int random, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillIndexAndLockOnTarget(int skillIndex, int random, TargetRangeType rangeType);

		bool PackTryToMoveToTargetBySkillTypeAndLockOnTarget(int skillType, int random, TargetRangeType rangeType);

		bool ChangeAction(string newAct);
	}
}
