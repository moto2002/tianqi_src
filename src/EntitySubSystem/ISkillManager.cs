using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public interface ISkillManager
	{
		int CurActionSkillID
		{
			get;
			set;
		}

		DateTime DebutTime
		{
			get;
		}

		void UpdateActor(ActorParent actor);

		void ResetData();

		bool CheckHasSkillByID(int skillID);

		bool GetSkillIDByIndex(int skillIndex, out int value);

		void SetDebutCD();

		bool CheckSkillInCDByID(int skillID);

		KeyValuePair<float, DateTime> GetSkillCDByID(int skillID);

		bool SetTargetBySkillID(int skillID, TargetRangeType rangeType, float rushDistance = 0f);

		EntityParent GetTargetBySkillID(int skillID, TargetRangeType rangeType, float rushDistance = 0f);

		bool CheckTargetBySkillID(EntityParent entity, int skillID, TargetRangeType rangeType, float rushDistance = 0f);

		void ClientBeginAssault(int skillID, EntityParent assaultTarget);

		void ClientEndAssault();

		void ServerBeginAssault(Vector3 toPos, int actionPriority);

		void ServerEndAssault(Vector3 finalPos, Vector3 finalDir);

		bool ClientCastSkillByID(int skillID);

		bool CheckClientHandleSkillByID(int skillID);

		void ClientHandleSkillByID(int skillID);

		void ServerCastSkillByID(int skillID, int actionPriority, Vector3 dir, bool isClientHandle, int uniqueID);

		void ServerEndRepeatSkill(int skillID);

		void ClientActionTriggerEffect(int effectID);

		void ServerActionTriggerEffect(int effectID);

		void ClientHandleHit(EntityParent caster, Effect effectData, XPoint basePoint);

		void ServerHandleHit(long casterID, int effectID, string hitAction, int actionPriority, bool isKnock, Vector3 toPos, bool isManage, int oldManageState, int uniqueID);

		void ClearSkillTrusteeMessage();
	}
}
