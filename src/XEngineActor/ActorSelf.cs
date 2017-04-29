using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorSelf : ActorParentContainer<EntitySelf>
	{
		protected Vector3 lastSendPos;

		protected bool isCheckingCityAutoSendPos;

		public float CityUploadPosInterval = 2f;

		protected bool isCheckingBattleAutoSendPos;

		public float BattleUploadPosInterval = 0.1f;

		protected Vector3 lastSendDir;

		public float UploadDirTime = 200f;

		protected GameObject pointer;

		protected int pointerFxID;

		protected uint hideArrowTimer = 4294967295u;

		protected Vector2 guidepost = Vector2.get_zero();

		protected bool preShowPointer;

		protected bool isShowPointer;

		protected DateTime lastGetDropTime;

		public override bool IsClearTargetPosition
		{
			get
			{
				return base.IsClearTargetPosition;
			}
			set
			{
				if (!this.GetEntity().IsInBattle)
				{
					if (!base.IsClearTargetPosition && value)
					{
						EventDispatcher.Broadcast<bool>(CameraEvent.Pathfinding, false);
					}
					else if (base.IsClearTargetPosition && !value)
					{
						EventDispatcher.Broadcast<bool>(CameraEvent.Pathfinding, true);
					}
				}
				base.IsClearTargetPosition = value;
			}
		}

		public Vector3 LastSendPos
		{
			get
			{
				return this.lastSendPos;
			}
			set
			{
				this.lastSendPos = value;
			}
		}

		public bool IsCheckingCityAutoSendPos
		{
			get
			{
				return this.isCheckingCityAutoSendPos;
			}
			set
			{
				this.isCheckingCityAutoSendPos = value;
			}
		}

		public bool IsCheckingBattleAutoSendPos
		{
			get
			{
				return this.isCheckingBattleAutoSendPos;
			}
			set
			{
				this.isCheckingBattleAutoSendPos = value;
			}
		}

		public Vector3 LastSendDir
		{
			get
			{
				return this.lastSendDir;
			}
			set
			{
				this.lastSendDir = value;
			}
		}

		public override int Floor
		{
			get
			{
				return base.Floor;
			}
			set
			{
				if (base.Floor != value)
				{
					this.SendFloor(value);
				}
				base.Floor = value;
			}
		}

		public bool IsShowPointer
		{
			get
			{
				return this.isShowPointer;
			}
			set
			{
				this.preShowPointer = this.isShowPointer;
				this.isShowPointer = value;
				if (this.preShowPointer && !this.isShowPointer)
				{
					this.HidePointer(500);
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			this.SetPointer();
			XInputManager.Instance.Actor = this;
		}

		protected override void OnDestroy()
		{
			FXManager.Instance.DeleteFX(this.pointerFxID);
			Object.Destroy(this.pointer);
			base.OnDestroy();
		}

		protected override void Update()
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.MoveProcess();
			if (this.GetEntity().IsInBattle)
			{
				this.UpdatePointer();
			}
		}

		public override void ClearData()
		{
			TimerHeap.DelTimer(this.hideArrowTimer);
			base.ClearData();
		}

		public override void DestroyScript()
		{
			if (this.GetEntity() != null)
			{
				(this.GetEntity() as EntitySelf).EquipCustomizationer.Release();
			}
			base.DestroyScript();
		}

		public override void FadeDestroyScript(List<AdjustTransparency> alphaControls)
		{
			if (this.GetEntity() != null)
			{
				(this.GetEntity() as EntitySelf).EquipCustomizationer.Release();
			}
			base.FadeDestroyScript(alphaControls);
		}

		public override void ResetController()
		{
			AssetManager.AssetOfControllerManager.SetController(base.FixAnimator, this.GetEntity().FixModelID, this.GetEntity().IsInBattle);
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

		public override bool ChangeAction(string newAction, bool isForceChange = false, bool isBreakChange = true, float extraSpeed = 1f, int candidateSkillID = 0, int candidateSkillComboID = 0, string candidateSkillTag = "")
		{
			string curActionStatus = this.CurActionStatus;
			if (!base.ChangeAction(newAction, isForceChange, isBreakChange, extraSpeed, candidateSkillID, candidateSkillComboID, candidateSkillTag))
			{
				return false;
			}
			if (isBreakChange)
			{
				base.CheckIsCancelSkill(curActionStatus);
			}
			return true;
		}

		public void CheckIsCancelCurSkill()
		{
			base.CheckIsCancelSkill(this.CurActionStatus);
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

		protected override void AssaultSuccess()
		{
			this.GetEntity().GetSkillManager().ClientEndAssault();
		}

		public override void OnCheckCombo(CheckComboCmd cmd)
		{
			base.IsUnderCombo = true;
			XInputManager.Instance.ComboFramePressSkillKey();
		}

		protected override void MoveProcess()
		{
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

		public override void EnableCityAutoSendPos()
		{
			this.LastSendPos = base.FixTransform.get_position();
			if (this.IsCheckingCityAutoSendPos)
			{
				return;
			}
			this.IsCheckingCityAutoSendPos = true;
			base.StartCoroutine(this.CityAutoSendPos());
		}

		[DebuggerHidden]
		protected IEnumerator CityAutoSendPos()
		{
			ActorSelf.<CityAutoSendPos>c__Iterator13 <CityAutoSendPos>c__Iterator = new ActorSelf.<CityAutoSendPos>c__Iterator13();
			<CityAutoSendPos>c__Iterator.<>f__this = this;
			return <CityAutoSendPos>c__Iterator;
		}

		public override void EnableBattleAutoSendPos()
		{
			this.LastSendPos = base.FixTransform.get_position();
			if (this.IsCheckingBattleAutoSendPos)
			{
				return;
			}
			this.IsCheckingBattleAutoSendPos = true;
			base.StartCoroutine(this.BattleAutoSendPos());
		}

		[DebuggerHidden]
		protected IEnumerator BattleAutoSendPos()
		{
			ActorSelf.<BattleAutoSendPos>c__Iterator14 <BattleAutoSendPos>c__Iterator = new ActorSelf.<BattleAutoSendPos>c__Iterator14();
			<BattleAutoSendPos>c__Iterator.<>f__this = this;
			return <BattleAutoSendPos>c__Iterator;
		}

		public override void SendPos()
		{
			if (!base.EnableSendPos)
			{
				return;
			}
			PosDirUtility.AddPositionFlag(PosDirUtility.PositionFlagType.Self, base.FixTransform.get_position());
			this.LastSendPos = base.FixTransform.get_position();
			GlobalBattleNetwork.Instance.SendMove(base.FixTransform.get_position().x * 100f, base.FixTransform.get_position().z * 100f);
		}

		public override void CheckSendPrecisePosOnReleaseDrag()
		{
			if (this.GetEntity().IsInBattle)
			{
				if (InstanceManager.IsServerBattle)
				{
					this.SendPrecisePos();
				}
			}
			else
			{
				this.SendPrecisePos();
			}
		}

		public override void SendPrecisePos()
		{
			if (!base.EnableSendPos)
			{
				return;
			}
			PosDirUtility.AddPositionFlag(PosDirUtility.PositionFlagType.Self, base.FixTransform.get_position());
			this.LastSendPos = base.FixTransform.get_position();
			GlobalBattleNetwork.Instance.SendPreciseMove(base.FixTransform.get_position().x * 100f, base.FixTransform.get_position().z * 100f);
		}

		public override void LoadEndResetPoistion()
		{
			Vector3 vector;
			if (MySceneManager.GetTerrainPoint(base.FixTransform.get_position().x, base.FixTransform.get_position().z, this.GetEntity().CurFloorStandardHeight, out vector))
			{
				base.FixTransform.set_position(vector + new Vector3(0f, 0.05f, 0f));
				EntityWorld.Instance.AddPosRecord(this.GetEntity().ID, base.FixTransform.get_position(), 0);
			}
			else if (MySceneManager.GetTerrainBornPoint((float)this.GetEntity().Floor * 30f, out vector))
			{
				UIManagerControl.Instance.ShowToastText("重置人物位置");
				base.FixTransform.set_position(vector + new Vector3(0f, 0.05f, 0f));
				EntityWorld.Instance.AddPosRecord(this.GetEntity().ID, base.FixTransform.get_position(), 1);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText("重置人物位置失败，请检查配置");
			}
		}

		[DebuggerHidden]
		protected IEnumerator AutoSendDir()
		{
			ActorSelf.<AutoSendDir>c__Iterator15 <AutoSendDir>c__Iterator = new ActorSelf.<AutoSendDir>c__Iterator15();
			<AutoSendDir>c__Iterator.<>f__this = this;
			return <AutoSendDir>c__Iterator;
		}

		public override void SendDir()
		{
			if (base.EnableSendDir)
			{
				NetworkManager.Send(new RoleRotateReq
				{
					vector = new Vector2
					{
						x = base.FixTransform.get_forward().get_normalized().x,
						y = base.FixTransform.get_forward().get_normalized().z
					}
				}, ServerType.Data);
			}
		}

		public override void SendFloor(int sendfloor)
		{
			NetworkManager.Send(new RoleLayerChangedReport
			{
				layer = sendfloor
			}, ServerType.Data);
		}

		public override void EnterPlatformArea()
		{
			base.EnableSendPos = false;
			base.EnableSendDir = false;
		}

		public override void ExitPlatformArea()
		{
			base.UpdateFloor();
			base.EnableSendPos = true;
			base.EnableSendDir = true;
			this.SendPrecisePos();
		}

		public override void StartPlatformTrip()
		{
		}

		public override void FinishPlatformTrip()
		{
		}

		public void Switch2Disintegrate(bool bIn)
		{
			BeamController beamController = base.FixTransform.get_gameObject().AddMissingComponent<BeamController>();
			beamController.Beam(base.FixTransform, bIn);
		}

		public void Switch2Hologram()
		{
			Hologram hologram = base.FixTransform.get_gameObject().AddMissingComponent<Hologram>();
			hologram.Initialization(base.FixTransform);
		}

		public override void OnTweenCamera(TweenCameraCmd cmd)
		{
			if (CameraRevolve.instance != null && cmd == null)
			{
				return;
			}
		}

		protected void SetPointer()
		{
			this.pointer = new GameObject();
			this.pointer.set_name("Pointer");
			this.pointer.get_transform().set_parent(base.FixTransform);
			this.pointer.get_transform().set_localPosition(Vector3.get_zero());
		}

		protected void UpdatePointer()
		{
			if (this.GetEntity() == null)
			{
				this.IsShowPointer = false;
				return;
			}
			if (!this.GetEntity().IsInBattle)
			{
				this.IsShowPointer = false;
				return;
			}
			this.IsShowPointer = this.CheckIsShowPointer();
			if (this.IsShowPointer)
			{
				this.IsShowPointer = InstanceManager.GetCurrentMarkPoint(out this.guidepost);
			}
			if (this.IsShowPointer)
			{
				this.ShowPointer();
			}
		}

		protected bool CheckIsShowPointer()
		{
			bool result = true;
			if (XUtility.StartsWith(this.CurActionStatus, "victory"))
			{
				result = false;
			}
			return result;
		}

		public void ShowPointer()
		{
			if (!this.GetEntity().IsInBattle)
			{
				return;
			}
			TimerHeap.DelTimer(this.hideArrowTimer);
			this.pointer.SetActive(true);
			if (this.pointerFxID == 0)
			{
				this.pointerFxID = FXManager.Instance.PlayFX(1102, this.pointer.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
			this.pointer.get_transform().LookAt(new Vector3(this.guidepost.x, this.pointer.get_transform().get_position().y, this.guidepost.y));
		}

		public void HidePointer(int time)
		{
			if (this.IsShowPointer)
			{
				this.IsShowPointer = false;
			}
			if (time == 0)
			{
				FXManager.Instance.DeleteFX(this.pointerFxID);
				this.pointerFxID = 0;
				this.pointer.SetActive(false);
			}
			else
			{
				this.hideArrowTimer = TimerHeap.AddTimer((uint)time, 0, delegate
				{
					FXManager.Instance.DeleteFX(this.pointerFxID);
					this.pointerFxID = 0;
					if (this.pointer)
					{
						this.pointer.SetActive(false);
					}
				});
			}
		}

		public override void OnChangeWeaponSlot(ChangeWeaponSlotCmd cmd)
		{
			string[] array = cmd.slot_name.Split(new char[]
			{
				','
			});
			(this.GetEntity() as EntityPlayer).ChangeWeaponSlot(array[0], true, true);
		}

		public override void OnChangeWeaponSlotWithoutChangePosition(ChangeWeaponSlotWithoutChangePositionCmd cmd)
		{
			(this.GetEntity() as EntityPlayer).ChangeWeaponSlot(string.Empty, true, false);
		}

		public override void OnChangeWeaponSlotWithoutChangePositionOver(ChangeWeaponSlotWithoutChangePositionOverCmd cmd)
		{
			(this.GetEntity() as EntityPlayer).ChangeWeaponSlot(string.Empty, false, true);
		}

		public override void GotDrop()
		{
			DateTime now = DateTime.get_Now();
			if ((now - this.lastGetDropTime).get_TotalSeconds() < 1.0)
			{
				return;
			}
			this.lastGetDropTime = now;
			FXManager.Instance.PlayFX(3108, base.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		}
	}
}
