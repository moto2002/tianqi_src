using System;
using UnityEngine;
using XEngineActor;
using XEngineCommand;

namespace XEngine
{
	public class ActorEventReceiver : MonoBehaviour
	{
		protected Actor m_actor;

		protected Transform m_root;

		protected ActorParent fixActor;

		protected Animator m_animator;

		protected Vector3 tempForward;

		protected Vector3 deltaMove;

		protected Actor actor
		{
			get
			{
				if (!this.m_actor)
				{
					Transform transform = base.get_transform();
					while (transform)
					{
						this.m_actor = transform.GetComponent<Actor>();
						if (this.m_actor)
						{
							return this.m_actor;
						}
						transform = transform.get_parent();
					}
				}
				return this.m_actor;
			}
		}

		protected Transform root
		{
			get
			{
				if (!this.m_root)
				{
					this.m_root = ((!this.actor) ? base.get_transform().get_parent() : this.actor.get_transform());
				}
				return this.m_root;
			}
		}

		protected ActorParent FixActor
		{
			get
			{
				if (!this.fixActor)
				{
					this.fixActor = (this.actor as ActorParent);
				}
				return this.fixActor;
			}
		}

		private void Awake()
		{
			this.m_animator = base.GetComponent<Animator>();
		}

		public void OnActionStart(AnimationEvent e)
		{
		}

		public void OnActionEnd(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new AnimationEndCmd
			{
				actName = e.get_stringParameter()
			});
		}

		public void RepeatStart(AnimationEvent e)
		{
		}

		public void RepeatEnd(AnimationEvent e)
		{
		}

		public void Effect(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new SkillEffectCmd
			{
				args = e
			});
		}

		public void AddSkill(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new AddSkillCmd
			{
				skillMessage = e.get_stringParameter()
			});
		}

		public void RemoveSkill(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new RemoveSkillCmd
			{
				skillMessage = e.get_stringParameter()
			});
		}

		public void ActivePart(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ActivePartCmd
			{
				args = e
			});
		}

		public void DeactivePart(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new DeactivePartCmd
			{
				args = e
			});
		}

		public void ChangeAI(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ChangeAICmd
			{
				args = e
			});
		}

		public void Combo(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new CheckComboCmd());
		}

		public void Termination(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new SetTerminationCmd
			{
				actionName = e.get_stringParameter()
			});
		}

		public void AllowChangeSpeed(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new AllowChangeSpeedCmd());
		}

		public void ChangeSpeed(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new NotifyPropChangedCmd
			{
				propName = "AnimFactor",
				propValue = e.get_floatParameter(),
				propTag = e.get_stringParameter()
			});
		}

		public void ChangeSpeedOver(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new NotifyPropChangedCmd
			{
				propName = "AnimFactor",
				propValue = 1f
			});
		}

		public void AllowChangeDirection(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new AllowChangeDirectionCmd());
		}

		public void LockFaceForward(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new LockFaceForwardCmd());
		}

		public void MoveOn(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new RootMotionCmd
			{
				rootMotion = true
			});
		}

		public void ColliderOff(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new IgnoreCollisionCmd
			{
				closeCollision = true
			});
		}

		public void ChangeHeight(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new NotifyPropChangedCmd
			{
				propName = "ModelHeight",
				propValue = e.get_floatParameter()
			});
		}

		public void ChangeHeightOver(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new NotifyPropChangedCmd
			{
				propName = "ModelHeight",
				propValue = 0f
			});
		}

		public void ActionStraight(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ActionStraightCmd
			{
				rate = e.get_floatParameter()
			});
		}

		public void audio(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new AudioEventCmd
			{
				args = e
			});
		}

		public void audio2D(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new Audio2DEventCmd
			{
				args = e
			});
		}

		public void fx(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new FXEventCmd
			{
				args = e
			});
		}

		public void ShowTexture(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ShowTextureCmd
			{
				textureNames = e.get_stringParameter()
			});
		}

		public void HideTexture(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new HideTextureCmd
			{
				textureNames = e.get_stringParameter()
			});
		}

		public void ChangeWeaponSlot(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ChangeWeaponSlotCmd
			{
				slot_name = e.get_stringParameter()
			});
		}

		public void ChangeWeaponSlotWithoutChangePosition(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ChangeWeaponSlotWithoutChangePositionCmd
			{
				slot_name = e.get_stringParameter()
			});
		}

		public void ChangeWeaponSlotWithoutChangePositionOver(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ChangeWeaponSlotWithoutChangePositionOverCmd());
		}

		public void Traverse(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new TraverseCmd
			{
				className = e.get_stringParameter()
			});
		}

		public void TweenCamera(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
		}

		public void CameraPosition(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			string[] array = e.get_stringParameter().Split(new char[]
			{
				';'
			});
			if (array.Length < 2)
			{
				Debug.LogError("CameraPosition=参数少于2个");
			}
			CommandCenter.ExecuteCommand(this.root, new CameraPositionCmd
			{
				distance = float.Parse(array[0]),
				height = float.Parse(array[1])
			});
		}

		public void PointBPriority(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new PointBPriorityCmd
			{
				number = e.get_intParameter()
			});
		}

		public void JumpFollow(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new JumpFollowCmd
			{
				jumpFollow = e.get_intParameter()
			});
		}

		public void ShowRoles(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new ShowRolesCmd());
		}

		public void BornBegin(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new BornBeginCmd());
		}

		public void BornEnd(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new BornEndCmd());
		}

		public void UltraSkill(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new UltraSkillCmd
			{
				args = e.get_stringParameter()
			});
		}

		public void SkillBegin(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new SkillBeginCmd());
		}

		public void PlaybackSpeed(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new PlaybackSpeedCmd
			{
				speed = e.get_floatParameter()
			});
		}

		public void OnSlowMotionIntroduction(AnimationEvent e)
		{
			if (!this.CheckEventIsValid(e))
			{
				return;
			}
			CommandCenter.ExecuteCommand(this.root, new SlowMotionIntroductionCmd
			{
				time = e.get_intParameter()
			});
		}

		protected bool CheckEventIsValid(AnimationEvent e)
		{
			return this.m_animator.GetCurrentAnimatorStateInfo(0).Equals(e.get_animatorStateInfo()) && !this.m_animator.IsInTransition(0);
		}

		private void OnAnimatorMove()
		{
			try
			{
				if (this.FixActor)
				{
					if (this.FixActor.FixAnimator.Equals(this.m_animator))
					{
						if (this.FixActor.CanAnimatorApplyMotion)
						{
							this.tempForward = this.m_animator.get_rootRotation() * Vector3.get_forward();
							this.tempForward.y = 0f;
							this.FixActor.FixTransform.set_forward(this.tempForward);
							this.deltaMove = this.m_animator.get_deltaPosition().AssignYZero();
							if (this.FixActor.FreezeXZ)
							{
								this.deltaMove.x = 0f;
								this.deltaMove.z = 0f;
							}
							if (!this.FixActor.FreezeY)
							{
								base.get_transform().set_localPosition(new Vector3(base.get_transform().get_localPosition().x, base.get_transform().get_localPosition().y + this.m_animator.get_deltaPosition().y, base.get_transform().get_localPosition().z));
							}
							this.FixActor.MarkActionMove(this.deltaMove);
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
