using System;
using UnityEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorPet : ActorParentContainer<EntityPet>
	{
		protected GameObject aimMark;

		protected int aimMarkFxID;

		protected override void Start()
		{
			base.Start();
			this.CheckAimMark();
		}

		protected override void OnDestroy()
		{
			FXManager.Instance.DeleteFX(this.aimMarkFxID);
			base.OnDestroy();
		}

		protected override void Update()
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.MoveProcess();
			if (base.NextRotateCountDown > 0f)
			{
				base.NextRotateCountDown -= Time.get_deltaTime();
			}
			else
			{
				base.UpdateSight();
			}
		}

		public override bool MoveToSkillTarget(float skillReach)
		{
			if (this.GetEntity().AITarget == null)
			{
				return false;
			}
			if (!this.GetEntity().AITarget.Actor)
			{
				return false;
			}
			float num = XUtility.DistanceNoY(base.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position()) - (skillReach + XUtility.GetHitRadius(this.GetEntity().AITarget.Actor.FixTransform));
			if (num <= -0.05f)
			{
				base.StopMoveToPoint();
				return false;
			}
			if (!XUtility.StartsWith(this.CurActionStatus, "run") && !this.CanChangeActionTo("run", true, 0, false) && ActionStatusName.IsSkillAction(this.CurActionStatus) && !this.GetEntity().IsMoveCast && !base.IsUnderTermination)
			{
				return false;
			}
			Vector3 vector = new Vector3(this.GetEntity().AITarget.Actor.FixTransform.get_position().x - base.FixTransform.get_position().x, 0f, this.GetEntity().AITarget.Actor.FixTransform.get_position().z - base.FixTransform.get_position().z);
			Vector3 normalized = vector.get_normalized();
			Vector3 aIMoveFixPoint = base.GetAIMoveFixPoint(base.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position(), normalized, num, XUtility.GetHitRadius(base.FixTransform));
			this.MoveToPoint(aIMoveFixPoint, 0f, null);
			this.GetEntity().AIToPoint = new XPoint
			{
				position = aIMoveFixPoint
			};
			return true;
		}

		public override void OnTweenCamera(TweenCameraCmd cmd)
		{
			Debug.Log("OnTweenCamera");
			if (InstanceManager.CurrentInstanceType != InstanceType.DungeonNormal)
			{
				return;
			}
			Transform fixTransform = base.FixTransform;
		}

		public override void OnUltraSkill(UltraSkillCmd cmd)
		{
			Debug.Log("OnUltraSkill");
			if (InstanceManager.CurrentInstanceType != InstanceType.DungeonNormal)
			{
				return;
			}
			EventDispatcher.Broadcast<EntityParent>(CameraEvent.PetUltraSKillEnd, this.GetEntity());
		}

		public override void OnBornBegin(BornBeginCmd cmd)
		{
			Debug.Log("OnBornBegin");
			if (InstanceManager.CurrentInstanceType != InstanceType.DungeonNormal)
			{
				return;
			}
			FollowCamera.instance.bornFxId = PetManager.Instance.PlayScreenFXOfBattle(201);
			Utils.PauseAI(true);
		}

		public override void OnBornEnd(BornEndCmd cmd)
		{
			Debug.Log("OnBornEnd");
			if (InstanceManager.CurrentInstanceType != InstanceType.DungeonNormal)
			{
				return;
			}
			CameraGlobal.cameraType = CameraType.Follow;
			FollowCamera.instance.isPetSkill = true;
			FollowCamera.instance.smoothingMoveSpeed = (EntityWorld.Instance.TraSelf.get_position() + FollowCamera.instance.fightCameraPosition - CamerasMgr.MainCameraRoot.get_position()).get_magnitude() / 0.5f;
			FollowCamera.instance.smoothingRotateAngle = Quaternion.Angle(CamerasMgr.MainCameraRoot.get_rotation(), FollowCamera.instance.fightCameraRotation) / 0.5f;
		}

		public override void OnSkillBegin(SkillBeginCmd cmd)
		{
			FollowCamera.instance.fightCameraPosition = CamerasMgr.MainCameraRoot.get_position() - EntityWorld.Instance.TraSelf.get_position();
			FollowCamera.instance.fightCameraRotation = CamerasMgr.MainCameraRoot.get_rotation();
			Debug.Log(string.Concat(new object[]
			{
				"OnSkillBegin.position=",
				FollowCamera.instance.fightCameraPosition,
				".rotaion=",
				FollowCamera.instance.fightCameraRotation
			}));
		}

		public override void OnPlaybackSpeed(PlaybackSpeedCmd cmd)
		{
			Debug.Log("OnPlaybackSpeed");
			base.FixAnimator.set_speed(Mathf.Clamp01(cmd.speed));
		}

		public override void OnShowRoles(ShowRolesCmd cmd)
		{
			if (InstanceManager.CurrentInstanceType != InstanceType.DungeonNormal)
			{
				return;
			}
			Debug.Log("OnShowRoles");
			PetManager.Instance.SetMainCameraCullingMaskWithActors();
		}

		protected void CheckAimMark()
		{
			if (!InstanceManager.IsShowPetAimMark(this.GetEntity().IsPlayerMate))
			{
				return;
			}
			this.aimMark = new GameObject();
			this.aimMark.set_name("AimMark");
			this.aimMark.get_transform().set_parent(base.FixTransform);
			this.aimMark.get_transform().set_localPosition(Vector3.get_zero());
			if (this.aimMarkFxID == 0)
			{
				this.aimMarkFxID = FXManager.Instance.PlayFX(3028, this.aimMark.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
		}
	}
}
