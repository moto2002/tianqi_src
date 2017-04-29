using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorPlayer : ActorParentContainer<EntityPlayer>
	{
		protected uint stopTimer;

		protected float attackBufferTime;

		protected DateTime lastKeyPressedTime;

		protected int preKeyPressedIndex;

		protected int curKeyPressedIndex;

		protected GameObject aimMark;

		protected int aimMarkFxID;

		protected override void Awake()
		{
			base.Awake();
			this.attackBufferTime = float.Parse(DataReader<GlobalParams>.Get("attack_cache_i").value);
		}

		protected override void Start()
		{
			base.Start();
			this.CheckAimMark();
		}

		protected override void OnDestroy()
		{
			TimerHeap.DelTimer(this.stopTimer);
			FXManager.Instance.DeleteFX(this.aimMarkFxID);
			ActorVisibleManager.Instance.Remove(base.FixTransform);
			base.OnDestroy();
		}

		protected override void Update()
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.MoveProcess();
		}

		public override void ClearData()
		{
			TimerHeap.DelTimer(this.stopTimer);
			base.ClearData();
		}

		public override void DestroyScript()
		{
			if (this.GetEntity() != null && this.GetEntity().IsEntityPlayerType)
			{
				(this.GetEntity() as EntityPlayer).EquipCustomizationer.Release();
			}
			base.DestroyScript();
		}

		public override void FadeDestroyScript(List<AdjustTransparency> alphaControls)
		{
			if (this.GetEntity() != null && this.GetEntity().IsEntityPlayerType)
			{
				(this.GetEntity() as EntityPlayer).EquipCustomizationer.Release();
			}
			base.FadeDestroyScript(alphaControls);
		}

		public override void ResetController()
		{
			AssetManager.AssetOfControllerManager.SetController(base.FixAnimator, this.GetEntity().FixModelID, true);
		}

		public override void DeadAnimationEnd()
		{
			if (this.GetEntity().IsFuse)
			{
				(this.GetEntity() as EntityPlayer).DieEndDefuse();
			}
			else
			{
				this.GetEntity().DieEnd();
			}
		}

		protected override void MoveProcess()
		{
			if (base.FixNavMeshAgent == null)
			{
				return;
			}
			this.deltaTime = Time.get_deltaTime();
			this.commonMoveOffset = base.TryUpdateCommonMove(this.deltaTime);
			base.ColliderMeshMove(this.commonMoveOffset, this.deltaTime, true);
			this.hitMoveOffset = base.TryUpdateHitMove(this.deltaTime);
			base.ColliderAndNavMeshMove(this.hitMoveOffset, this.deltaTime);
			this.assaultMoveOffset = base.TryUpdateAssaultMove(this.deltaTime);
			base.ColliderAndNavMeshMove(this.assaultMoveOffset, this.deltaTime);
			base.ColliderMeshMove(this.actionMoveOffset, 1f, false);
			base.UpdateAfterMoveState();
		}

		protected override void UpdateMoveByPointState(float deltaTime)
		{
			if (base.IsMovePausing)
			{
				if (!this.IsClearTargetPosition && !this.GetEntity().IsStatic && !this.GetEntity().IsDizzy && !this.GetEntity().IsWeak && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && base.ToCorner < base.NavMeshPath.get_corners().Length)
				{
					base.IsMovePausing = false;
					base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
					base.LeftLength = XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]);
					if (this.CanChangeActionTo("run", true, 0, false))
					{
						this.ChangeAction("run", false, true, 1f, 0, 0, string.Empty);
					}
				}
			}
			else
			{
				base.CalculateNextPace(deltaTime);
				if ((this.CurActionStatus != "run" && this.CurActionStatus != "idle" && !this.GetEntity().IsMoveCast) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak)
				{
					base.IsMovePausing = true;
					base.MovingDirection = Vector3.get_zero();
				}
				else if (XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPosition) <= 0.05f || base.ToCorner == base.NavMeshPath.get_corners().Length || (base.NextPace >= base.LeftLength && base.ToCorner == base.NavMeshPath.get_corners().Length - 1))
				{
					if (!this.IsClearTargetPosition)
					{
						this.IsClearTargetPosition = true;
						base.MovingDirection = Vector3.get_zero();
						base.FixTransform.set_position(new Vector3(base.NavMeshPosition.x, base.FixTransform.get_position().y, base.NavMeshPosition.z));
						TimerHeap.DelTimer(this.stopTimer);
						this.stopTimer = TimerHeap.AddTimer(350u, 0, delegate
						{
							if (this.IsClearTargetPosition && XUtility.StartsWith(this.CurActionStatus, "run"))
							{
								this.ChangeAction("idle", false, true, 1f, 0, 0, string.Empty);
							}
						});
					}
				}
				else if (base.NavMeshPath.get_corners().Length > 0 && (XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]) <= 0.05f || base.NextPace > base.LeftLength))
				{
					if (!this.IsClearTargetPosition)
					{
						base.FixTransform.set_position(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
						base.ToCorner++;
						base.MovingDirection = Vector3.get_zero();
						if (base.ToCorner < base.NavMeshPath.get_corners().Length)
						{
							base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
							base.LeftLength = XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]);
						}
					}
				}
				else if (!this.IsClearTargetPosition && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && base.ToCorner < base.NavMeshPath.get_corners().Length)
				{
					Vector3 vector = new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x - base.FixTransform.get_position().x, 0f, base.NavMeshPath.get_corners()[base.ToCorner].z - base.FixTransform.get_position().z);
					base.MovingDirection = vector.get_normalized();
					base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
				}
			}
		}

		public bool PressSimulateKey(int index)
		{
			this.lastKeyPressedTime = DateTime.get_Now();
			this.preKeyPressedIndex = this.curKeyPressedIndex;
			this.curKeyPressedIndex = index;
			EventDispatcher.Broadcast<long, int, bool>(XInputManagerEvent.FakePressSkillKey, this.GetEntity().ID, this.curKeyPressedIndex, this.curKeyPressedIndex == this.preKeyPressedIndex);
			return true;
		}

		public void ClientAssault(long targetID, int skillID, Vector3 endPosition)
		{
			this.assaultTargetID = targetID;
			this.assaultEndSkillID = skillID;
			this.assaultEndSkillDistance = ((DataReader<Skill>.Get(this.assaultEndSkillID).reach == null || DataReader<Skill>.Get(this.assaultEndSkillID).reach.get_Count() <= 1) ? 0.05f : ((float)DataReader<Skill>.Get(this.assaultEndSkillID).reach.get_Item(0) * 0.01f));
			this.assaultEndPos = endPosition;
			this.assaultSpeed = base.OriginAssaultSpeed * base.LogicMoveSpeed / base.ModelOriginSpeed;
			this.assaultTime = XUtility.DistanceNoY(base.FixTransform.get_position(), endPosition) / this.assaultSpeed;
			this.ChangeAction("rush", false, true, 1f, 0, 0, string.Empty);
		}

		public void ServerAssault(Vector3 endPosition, int actionPriority)
		{
			this.assaultEndPos = endPosition;
			this.assaultSpeed = base.OriginAssaultSpeed * base.LogicMoveSpeed / base.ModelOriginSpeed;
			this.assaultTime = XUtility.DistanceNoY(base.FixTransform.get_position(), endPosition) / this.assaultSpeed;
			base.ServerCastAction("rush", actionPriority, ActorParent.EffectFrameSetMode.Server, 1f, 0, 0, string.Empty);
		}

		protected override void AssaultSuccess()
		{
			if (this.GetEntity().IsClientDominate)
			{
				this.GetEntity().GetSkillManager().ClientEndAssault();
			}
		}

		public override void OnCheckCombo(CheckComboCmd cmd)
		{
			base.IsUnderCombo = true;
			if (this.GetEntity().IsClientDrive)
			{
				EventDispatcher.Broadcast<long, bool, bool>(XInputManagerEvent.FakePressSkillKeyWithCushionCheck, this.GetEntity().ID, this.curKeyPressedIndex == this.preKeyPressedIndex, (DateTime.get_Now() - this.lastKeyPressedTime).get_TotalMilliseconds() <= (double)this.attackBufferTime);
			}
		}

		protected void CheckAimMark()
		{
			if (!InstanceManager.IsShowPlayerAimMark(this.GetEntity().IsPlayerMate))
			{
				return;
			}
			this.aimMark = new GameObject();
			this.aimMark.set_name("AimMark");
			this.aimMark.get_transform().set_parent(base.FixTransform);
			this.aimMark.get_transform().set_localPosition(Vector3.get_zero());
			if (this.aimMarkFxID == 0)
			{
				this.aimMarkFxID = FXManager.Instance.PlayFX(3027, this.aimMark.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
		}
	}
}
