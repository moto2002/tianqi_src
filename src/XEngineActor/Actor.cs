using Foundation.EF;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using XEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class Actor : AbstractActorMediator, IEventSystemHandler, ICommandReceiver
	{
		public int InstanceID;

		[HideInInspector]
		public int resGUID;

		[HideInInspector]
		public int GameObjectID;

		public IReusePool pool;

		protected virtual void Awake()
		{
			this.OnCreate();
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnCreate()
		{
		}

		protected virtual void OnDestroy()
		{
			this.Reuse();
		}

		protected void Reuse()
		{
			if (this.pool != null)
			{
				this.pool.DestroyByObj(this.resGUID, this);
			}
			else
			{
				Object.Destroy(base.get_gameObject());
			}
		}

		public virtual void OnSingleTimer(SingleTimerCmd cmd)
		{
		}

		public virtual void OnFrozeFrame(FrozeFrameCmd cmd)
		{
		}

		public virtual void OnFXEvent(FXEventCmd cmd)
		{
		}

		public virtual void OnAudioEvent(AudioEventCmd cmd)
		{
		}

		public virtual void OnAudio2DEvent(Audio2DEventCmd cmd)
		{
		}

		public virtual void OnSkillEffect(SkillEffectCmd cmd)
		{
		}

		public virtual void OnAnimationEnd(AnimationEndCmd cmd)
		{
		}

		public virtual void OnActionStatusEnter(ActionStatusEnterCmd cmd)
		{
		}

		public virtual void OnActionStatusExit(ActionStatusExitCmd cmd)
		{
		}

		public virtual void OnPlayAction(PlayActionCmd cmd)
		{
		}

		public virtual void OnPlayActionFX(PlayActionFXCmd cmd)
		{
		}

		public virtual void OnChangeAllFXLayer(ChangeAllFXLayerCmd cmd)
		{
		}

		public virtual void OnRemoveActionFX(RemoveActionFXCmd cmd)
		{
		}

		public virtual void OnNotifyPropChanged(NotifyPropChangedCmd cmd)
		{
		}

		public virtual void OnTweenCamera(TweenCameraCmd cmd)
		{
		}

		public virtual void OnUltraSkill(UltraSkillCmd cmd)
		{
		}

		public virtual void OnJumpFollow(JumpFollowCmd cmd)
		{
		}

		public virtual void OnBornBegin(BornBeginCmd cmd)
		{
		}

		public virtual void OnBornEnd(BornEndCmd cmd)
		{
		}

		public virtual void OnSkillBegin(SkillBeginCmd cmd)
		{
		}

		public virtual void OnPlaybackSpeed(PlaybackSpeedCmd cmd)
		{
		}

		public virtual void OnShowRoles(ShowRolesCmd cmd)
		{
		}

		public virtual void OnPointBPriority(PointBPriorityCmd cmd)
		{
		}

		public virtual void OnCameraPosition(CameraPositionCmd cmd)
		{
		}

		public virtual void OnActivePart(ActivePartCmd cmd)
		{
		}

		public virtual void OnCheckCombo(CheckComboCmd cmd)
		{
		}

		public virtual void OnSetTermination(SetTerminationCmd cmd)
		{
		}

		public virtual void OnDeactivePart(DeactivePartCmd cmd)
		{
		}

		public virtual void OnAllowChangeDirection(AllowChangeDirectionCmd cmd)
		{
		}

		public virtual void OnAllowChangeSpeed(AllowChangeSpeedCmd cmd)
		{
		}

		public virtual void OnLockFaceForward(LockFaceForwardCmd cmd)
		{
		}

		public virtual void OnAnimationStart(AnimationStartCmd cmd)
		{
		}

		public virtual void OnRootMotion(RootMotionCmd cmd)
		{
		}

		public virtual void OnIgnoreCollision(IgnoreCollisionCmd cmd)
		{
		}

		public virtual void OnPlayBuffFX(PlayBuffFXCmd cmd)
		{
		}

		public virtual void OnRemoveBuffFX(RemoveBuffFXCmd cmd)
		{
		}

		public virtual void AddSkill(AddSkillCmd cmd)
		{
		}

		public virtual void RemoveSkill(RemoveSkillCmd cmd)
		{
		}

		public virtual void OnShowTexture(ShowTextureCmd cmd)
		{
		}

		public virtual void OnHideTexture(HideTextureCmd cmd)
		{
		}

		public virtual void OnChangeAI(ChangeAICmd cmd)
		{
		}

		public virtual void OnRemoveAllFX(RemoveAllFXCmd cmd)
		{
		}

		public virtual void OnChangeSpeedActionFX(ChangeSpeedActionFXCmd cmd)
		{
		}

		public virtual void OnHitFX(HitFXCmd cmd)
		{
		}

		public virtual void OnBulletFX(BulletFXCmd cmd)
		{
		}

		public virtual void OnActionStraight(ActionStraightCmd cmd)
		{
		}

		public virtual void OnChangeWeaponSlot(ChangeWeaponSlotCmd cmd)
		{
		}

		public virtual void OnChangeWeaponSlotWithoutChangePosition(ChangeWeaponSlotWithoutChangePositionCmd cmd)
		{
		}

		public virtual void OnChangeWeaponSlotWithoutChangePositionOver(ChangeWeaponSlotWithoutChangePositionOverCmd cmd)
		{
		}

		public virtual void OnSlowMotionIntroduction(SlowMotionIntroductionCmd cmd)
		{
		}

		public virtual void OnTraverse(TraverseCmd cmd)
		{
		}
	}
}
