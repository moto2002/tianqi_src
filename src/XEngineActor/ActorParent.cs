using Foundation.EF;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorParent : Actor, IAnimator, IPlatformRider, IActorVisible
	{
		public enum EffectFrameSetMode
		{
			Client,
			Server,
			Ignore,
			None
		}

		protected enum EffectFrameMode
		{
			Client,
			Server,
			Ignore
		}

		public class ActionSystem : AbstractColleague
		{
			public override void OnCreate()
			{
			}

			public override void OnDestroy()
			{
			}
		}

		public class AnimatorSpeedSystem : AbstractColleague
		{
			private DS_TimerCMDStack timerStack;

			public void DoSingleFroze(SingleTimerCmd cmd)
			{
				NotifyPropChangedCmd command = new NotifyPropChangedCmd
				{
					propName = "FrozenActionSpeed",
					propValue = cmd.rate
				};
				this.timerStack.Push(new FuncWithEndTime
				{
					doFunc = delegate
					{
						CommandCenter.ExecuteCommand((this.mediator as MonoBehaviour).get_transform(), command);
					},
					endTime = DateTime.get_Now() + TimeSpan.FromMilliseconds(cmd.seconds)
				});
			}

			public override void OnCreate()
			{
				Debuger.Info(base.GetType().get_Name() + " OnCreate", new object[0]);
				NotifyPropChangedCmd command = new NotifyPropChangedCmd
				{
					propName = "FrozenActionSpeed",
					propValue = 1f
				};
				this.timerStack = new DS_TimerCMDStack(delegate
				{
					CommandCenter.ExecuteCommand((this.mediator as MonoBehaviour).get_transform(), command);
				});
			}

			public override void OnDestroy()
			{
				Debuger.Info(base.GetType().get_Name() + " OnDestroy", new object[0]);
			}
		}

		public class FXSystem : AbstractColleague
		{
			private List<int> m_actorAllFXs = new List<int>();

			private Dictionary<int, List<int>> m_buffFXs = new Dictionary<int, List<int>>();

			private Dictionary<int, List<int>> m_actionFXs = new Dictionary<int, List<int>>();

			private Transform transform;

			private void AddBuffFX(int buffID, int fxID)
			{
				if (!this.m_buffFXs.ContainsKey(buffID))
				{
					this.m_buffFXs.Add(buffID, new List<int>());
				}
				this.m_buffFXs.get_Item(buffID).Add(fxID);
				this.m_actorAllFXs.Add(fxID);
			}

			public void OnPlayBuffFX(PlayBuffFXCmd cmd)
			{
				for (int i = 0; i < cmd.fxID.get_Count(); i++)
				{
					int templateId = cmd.fxID.get_Item(i);
					this.AddBuffFX(cmd.buffID, FXManager.Instance.PlayFX(templateId, this.transform, Vector3.get_zero(), Quaternion.get_identity(), 1f, cmd.scale, cmd.time, true, 0, null, null, 1f, FXClassification.Normal));
				}
			}

			public void OnRemoveBuffFX(RemoveBuffFXCmd cmd)
			{
				if (this.m_buffFXs.ContainsKey(cmd.buffID) && this.m_buffFXs.get_Item(cmd.buffID).get_Count() > 0)
				{
					List<int> list = this.m_buffFXs.get_Item(cmd.buffID);
					for (int i = 0; i < list.get_Count(); i++)
					{
						int num = list.get_Item(i);
						FXManager.Instance.DeleteFX(num);
						this.m_actorAllFXs.Remove(num);
					}
					this.m_buffFXs.get_Item(cmd.buffID).Clear();
				}
			}

			public void AddActionFX(int fxID)
			{
				int currentActionHash = (this.mediator as IAnimator).GetCurrentActionHash();
				if (!this.m_actionFXs.ContainsKey(currentActionHash))
				{
					this.m_actionFXs.Add(currentActionHash, new List<int>());
				}
				this.m_actionFXs.get_Item(currentActionHash).Add(fxID);
				this.m_actorAllFXs.Add(fxID);
			}

			public void OnChangeSpeedActionFX(ChangeSpeedActionFXCmd cmd)
			{
				if (this.m_actionFXs.ContainsKey(cmd.actionID) && this.m_actionFXs.get_Item(cmd.actionID).get_Count() > 0)
				{
					List<int> list = this.m_actionFXs.get_Item(cmd.actionID);
					for (int i = 0; i < list.get_Count(); i++)
					{
						FXManager.Instance.SetSpeed(list.get_Item(i), cmd.rate);
					}
				}
			}

			public void OnChangeAllFXLayer(ChangeAllFXLayerCmd cmd)
			{
				for (int i = 0; i < this.m_actorAllFXs.get_Count(); i++)
				{
					FXManager.Instance.GetActorByID(this.m_actorAllFXs.get_Item(i)).ChangeLayer(cmd.layer);
				}
			}

			public void OnPlayActionFX(PlayActionFXCmd cmd)
			{
				if (DataReader<Fx>.Get(cmd.fxID) == null)
				{
					Debuger.Error("cannot find " + cmd.fxID, new object[0]);
					return;
				}
				switch (DataReader<Fx>.Get(cmd.fxID).type1)
				{
				case 0:
				{
					FXManager arg_B8_0 = FXManager.Instance;
					FXClassification fxClassification = (!(this.mediator is ActorParent)) ? FXClassification.Normal : (this.mediator as ActorParent).GetFXClassification();
					this.AddActionFX(arg_B8_0.PlayFX(cmd.fxID, null, this.transform.get_position(), this.transform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, fxClassification));
					break;
				}
				case 1:
				{
					FXManager arg_123_0 = FXManager.Instance;
					float rate = cmd.rate;
					FXClassification fxClassification = (!(this.mediator is ActorParent)) ? FXClassification.Normal : (this.mediator as ActorParent).GetFXClassification();
					this.AddActionFX(arg_123_0.PlayFX(cmd.fxID, this.transform, Vector3.get_zero(), Quaternion.get_identity(), cmd.speed, 1f, 0, false, 0, null, null, rate, fxClassification));
					break;
				}
				case 2:
				{
					FXManager arg_194_0 = FXManager.Instance;
					float rate = cmd.rate;
					FXClassification fxClassification = (!(this.mediator is ActorParent)) ? FXClassification.Normal : (this.mediator as ActorParent).GetFXClassification();
					arg_194_0.PlayFX(cmd.fxID, null, this.transform.get_position(), this.transform.get_rotation(), cmd.speed, 1f, 0, false, 0, null, null, rate, fxClassification);
					break;
				}
				}
			}

			public void OnRemoveActionFX(RemoveActionFXCmd cmd)
			{
				int currentActionHash = (this.mediator as IAnimator).GetCurrentActionHash();
				if (this.m_actionFXs.ContainsKey(currentActionHash) && this.m_actionFXs.get_Item(currentActionHash).get_Count() > 0)
				{
					List<int> list = this.m_actionFXs.get_Item(currentActionHash);
					for (int i = 0; i < list.get_Count(); i++)
					{
						int num = list.get_Item(i);
						ActorFX actorByID = FXManager.Instance.GetActorByID(num);
						if (actorByID != null && actorByID.OnlyKilledBySuicide)
						{
							actorByID.SetFrameRate(1f, false);
						}
						else
						{
							FXManager.Instance.DeleteFX(num);
							this.m_actorAllFXs.Remove(num);
						}
					}
					this.m_actionFXs.get_Item(currentActionHash).Clear();
				}
			}

			public void OnHitFX(HitFXCmd cmd)
			{
				if (!GameLevelManager.IsHitFXOn())
				{
					return;
				}
				Fx fx = DataReader<Fx>.Get(cmd.fxID);
				if (fx == null)
				{
					Debuger.Error("{0} FxData equals null", new object[]
					{
						cmd.fxID
					});
					return;
				}
				string hook = fx.hook;
				if (XUtility.RecursiveFindTransform(this.transform, hook) != null)
				{
					Vector3 vector = this.transform.get_position() + (cmd.caster.get_position() - this.transform.get_position()).get_normalized() * XUtility.GetHitRadius(this.transform);
					XPoint xPoint = new XPoint
					{
						position = new Vector3(vector.x, XUtility.RecursiveFindTransform(this.transform, hook).get_position().y, vector.z),
						rotation = this.transform.get_rotation()
					}.ApplyOffset(cmd.offsets);
					if (fx.isScaleHit > 0)
					{
						FXManager arg_134_0 = FXManager.Instance;
						float scale = cmd.scale;
						arg_134_0.PlayFX(cmd.fxID, null, xPoint.position, xPoint.rotation, 1f, scale, 0, false, 0, null, null, 1f, FXClassification.Normal);
					}
					else
					{
						FXManager.Instance.PlayFX(cmd.fxID, null, xPoint.position, xPoint.rotation, 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
					}
				}
				else
				{
					Debuger.Error(string.Concat(new object[]
					{
						"Fx: ",
						cmd.fxID,
						" Cannot find mountPoint :",
						hook,
						" in ",
						this.transform
					}), new object[0]);
				}
			}

			public void OnBulletFX(BulletFXCmd cmd)
			{
				if (DataReader<Fx>.Get(cmd.fxID) == null)
				{
					return;
				}
				if (cmd.fxID != 0)
				{
					int num;
					if (cmd.collisionCallback == null)
					{
						FXManager arg_E1_0 = FXManager.Instance;
						float scale = cmd.scale;
						num = arg_E1_0.PlayFX(cmd.fxID, null, new Vector3(cmd.point.position.x, (!cmd.useY) ? EntityWorld.Instance.ActSelf.FixTransform.get_position().y : cmd.point.position.y, cmd.point.position.z), cmd.point.rotation, 1f, scale, 0, false, 0, null, null, 1f, (!(this.mediator is ActorParent)) ? FXClassification.Normal : (this.mediator as ActorParent).GetFXClassification());
					}
					else
					{
						if (this.transform == null)
						{
							return;
						}
						XPoint xPoint = new XPoint();
						if (cmd.point != null)
						{
							xPoint.position = cmd.point.position;
							xPoint.rotation = cmd.point.rotation;
						}
						else
						{
							xPoint.position = this.transform.get_position();
							xPoint.rotation = this.transform.get_rotation();
						}
						xPoint = xPoint.ApplyOffset(cmd.offset);
						FXManager arg_1A4_0 = FXManager.Instance;
						int bulletLife = cmd.bulletLife;
						Action<Actor, XPoint, ActorParent> collisionCallback = cmd.collisionCallback;
						num = arg_1A4_0.PlayFX(cmd.fxID, null, xPoint.position, xPoint.rotation, 1f, 1f, 0, false, bulletLife, collisionCallback, null, 1f, FXClassification.Normal);
					}
					this.m_actorAllFXs.Add(num);
				}
			}

			public override void OnCreate()
			{
				this.transform = (this.mediator as MonoBehaviour).get_transform();
			}

			public void OnRemoveAllFX(RemoveAllFXCmd cmd)
			{
				this.RemoveAllFX();
			}

			public void RemoveAllFX()
			{
				for (int i = 0; i < this.m_actorAllFXs.get_Count(); i++)
				{
					FXManager.Instance.DeleteFX(this.m_actorAllFXs.get_Item(i));
				}
				this.m_actorAllFXs.Clear();
			}

			public override void OnDestroy()
			{
				this.RemoveAllFX();
			}
		}

		protected const float YChangeSuddenlyValue = 1f;

		protected Animator fixAnimator;

		protected string curActionStatus = string.Empty;

		protected string curOutPutAction = string.Empty;

		protected Animator wingAnimator;

		protected XDict<string, int> actionPriorityTable = new XDict<string, int>();

		protected static readonly string CitySuffix = "_city";

		protected static readonly string SuffixPrefix = "_";

		protected string stageSuffix = string.Empty;

		protected bool isStageSuffixDirty;

		protected bool canAnimatorApplyMotion;

		protected bool freezeXZ;

		protected bool freezeY;

		protected bool isUnderTermination;

		protected bool isUnderCombo;

		protected bool canRepeat;

		protected float repeatPoint;

		protected bool isJumpFollow;

		protected uint straightTime;

		private uint straightTimerID;

		protected string appointedLoopActionName;

		protected int appointedLoopActionCountdown;

		protected bool allowToChangeSpeed;

		protected bool allowToChangeDirection;

		private Vector3 m_direction;

		protected bool isLockFaceForward;

		private float m_speed;

		public static readonly string foot_y_node_name = "foot_y_node";

		protected GameObject fixGameObject;

		protected Transform fixTransform;

		protected NavMeshAgent fixNavMeshAgent;

		protected ActorCollider actorCollider;

		protected ActorCushion actorCushion;

		protected CharacterController fixCharacterController;

		protected AudioPlayer audioPlayer;

		protected ActorParent.FXSystem fixFXSystem;

		protected bool hasSetCameraPointHeight;

		protected float cameraPointHeight;

		protected Transform foot_y_node;

		protected float deltaTime;

		protected Vector3 commonMoveOffset = Vector3.get_zero();

		protected Vector3 hitMoveOffset = Vector3.get_zero();

		protected Vector3 assaultMoveOffset = Vector3.get_zero();

		protected Vector3 actionMoveOffset = Vector3.get_zero();

		protected MoveMode curMoveMode;

		protected Action MoveEndCallBack;

		protected Vector3 navMeshPosition;

		protected NavMeshPath navMeshPath = new NavMeshPath();

		protected bool isClearTragetPosition = true;

		protected int toCorner;

		protected float leftLength;

		protected float nextPace;

		protected bool isMovePausing;

		protected CollisionFlags collisionFlag;

		protected bool enableSendPos;

		protected bool enableSendDir;

		protected float moveToYAngle;

		protected float angularSpeed;

		protected float angularPace;

		protected bool isRight;

		protected float moveByCenterTime;

		protected NavMeshHit fixHit = default(NavMeshHit);

		protected bool isLockModelDir;

		protected bool isRotating;

		protected float nextRotateCountDown;

		protected float rotateSpeed = 90f;

		protected float rotateInterval = 1f;

		protected float startRotateAngle = 5f;

		protected float finishRotateAngle = 5f;

		protected Vector3 turnToForward;

		protected int turnToSign = -1;

		protected float leftAngle;

		protected int floor;

		protected KeyValuePair<int, Action> FloorSetEndEvent = new KeyValuePair<int, Action>(-1, null);

		protected ActorParent.EffectFrameSetMode setEffectFrameMode = ActorParent.EffectFrameSetMode.None;

		protected ActorParent.EffectFrameMode curEffectFrameState;

		protected float defaultModelHeight;

		protected float modelHeight;

		protected float floatRate;

		protected int actionSkillComboID;

		protected string actionSkillTag = string.Empty;

		protected float originAssaultSpeed;

		protected float modelOriginSpeed;

		protected float assaultSpeed;

		protected float assaultTime;

		protected Vector3 assaultEndPos;

		protected long assaultTargetID;

		protected EntityParent assaultTarget;

		protected int assaultEndSkillID;

		protected float assaultEndSkillDistance;

		protected DateTime lastPlayParryFxTime;

		protected Vector3 hitMoveDirection = Vector3.get_zero();

		protected float hitMoveSpeed;

		protected float hitMoveCountDown;

		protected Action<Vector3, Vector3> hitMoveCallBack;

		protected int dataLayerState;

		protected int frameLayerState;

		protected int redererLayerState;

		protected bool isDisplayingByLayer;

		protected float realMoveSpeed;

		protected float pureMoveSpeed;

		protected float logicMoveSpeed;

		protected float realActionSpeed;

		protected float pureActionSpeed = 1f;

		protected float logicDefaultActionSpeed = 1f;

		protected float logicRunActionSpeed = 1f;

		protected float frameActionSpeed = 1f;

		protected bool isStraight;

		protected float straightActionSpeed = 1f;

		protected float frozenActionSpeed = 1f;

		protected float tempActionSpeed = 1f;

		protected List<uint> freezeTimer = new List<uint>();

		protected List<uint> thawTimer = new List<uint>();

		private int chosenFx;

		public bool IsOpenLayerTest;

		public Animator FixAnimator
		{
			get
			{
				return this.fixAnimator;
			}
		}

		public virtual string CurActionStatus
		{
			get
			{
				return this.curActionStatus;
			}
			set
			{
				this.curActionStatus = value;
			}
		}

		public virtual string CurOutPutAction
		{
			get
			{
				return this.curOutPutAction;
			}
			set
			{
				this.curOutPutAction = value;
			}
		}

		public Animator WingAnimator
		{
			protected get
			{
				return this.wingAnimator;
			}
			set
			{
				this.wingAnimator = value;
			}
		}

		public XDict<string, int> ActionPriorityTable
		{
			get
			{
				return this.actionPriorityTable;
			}
			set
			{
				this.actionPriorityTable = value;
			}
		}

		public string StageSuffix
		{
			get
			{
				return this.stageSuffix;
			}
			set
			{
				if (this.stageSuffix != value)
				{
					this.IsStageSuffixDirty = true;
				}
				this.stageSuffix = value;
			}
		}

		public bool IsStageSuffixDirty
		{
			get
			{
				return this.isStageSuffixDirty;
			}
			set
			{
				this.isStageSuffixDirty = value;
			}
		}

		public bool CanAnimatorApplyMotion
		{
			get
			{
				return this.canAnimatorApplyMotion;
			}
			set
			{
				this.canAnimatorApplyMotion = value;
				if (value)
				{
					this.IsClearTargetPosition = true;
				}
			}
		}

		public bool FreezeXZ
		{
			get
			{
				return this.freezeXZ || this.GetEntity().IsFixed;
			}
			set
			{
				this.freezeXZ = value;
			}
		}

		public bool FreezeY
		{
			get
			{
				return this.freezeY;
			}
			set
			{
				this.freezeY = value;
			}
		}

		public bool IsUnderTermination
		{
			get
			{
				return this.isUnderTermination;
			}
			set
			{
				this.isUnderTermination = value;
			}
		}

		public bool IsUnderCombo
		{
			get
			{
				return this.isUnderCombo;
			}
			set
			{
				this.isUnderCombo = value;
			}
		}

		public bool CanRepeat
		{
			get
			{
				return this.canRepeat;
			}
			set
			{
				if (this.canRepeat && !value)
				{
					EventDispatcher.Broadcast<long>(SkillEvent.EndRepeatUseSkill, this.GetEntity().ID);
				}
				this.canRepeat = value;
			}
		}

		public float RepeatPoint
		{
			get
			{
				return this.repeatPoint;
			}
			set
			{
				this.repeatPoint = value;
			}
		}

		public bool IsJumpFollow
		{
			get
			{
				return this.isJumpFollow;
			}
			set
			{
				this.isJumpFollow = value;
			}
		}

		public string AppointedLoopActionName
		{
			get
			{
				return this.appointedLoopActionName;
			}
			protected set
			{
				this.appointedLoopActionName = value;
			}
		}

		public int AppointedLoopActionCountdown
		{
			get
			{
				return this.appointedLoopActionCountdown;
			}
			protected set
			{
				this.appointedLoopActionCountdown = value;
			}
		}

		public bool AllowToChangeSpeed
		{
			get
			{
				return this.allowToChangeSpeed;
			}
			set
			{
				this.allowToChangeSpeed = value;
			}
		}

		public bool AllowToChangeDirection
		{
			get
			{
				return this.allowToChangeDirection;
			}
			set
			{
				this.allowToChangeDirection = value;
				this.GetEntity().IsMoveCast = value;
			}
		}

		public Vector3 Velocity
		{
			get
			{
				return this.m_speed * this.m_direction;
			}
		}

		public Vector3 MovingDirection
		{
			get
			{
				return this.m_direction;
			}
			set
			{
				if (this.AllowToChangeDirection || XUtility.StartsWith(this.CurActionStatus, "run"))
				{
					if (this.IsLockModelDir)
					{
						return;
					}
					this.m_direction = value.get_normalized();
				}
			}
		}

		public bool IsLockFaceForward
		{
			get
			{
				return this.isLockFaceForward;
			}
			set
			{
				this.isLockFaceForward = value;
			}
		}

		public float MovingSpeed
		{
			get
			{
				return this.m_speed;
			}
			set
			{
				if (this.AllowToChangeSpeed || XUtility.StartsWith(this.CurActionStatus, "run"))
				{
					this.m_speed = value;
				}
			}
		}

		public GameObject FixGameObject
		{
			get
			{
				return this.fixGameObject;
			}
		}

		public Transform FixTransform
		{
			get
			{
				return this.fixTransform;
			}
		}

		public NavMeshAgent FixNavMeshAgent
		{
			get
			{
				return this.fixNavMeshAgent;
			}
		}

		public ActorCollider ActorCollider
		{
			get
			{
				return this.actorCollider;
			}
		}

		public ActorCushion ActorCushion
		{
			get
			{
				return this.actorCushion;
			}
		}

		public CharacterController FixCharacterController
		{
			get
			{
				return this.fixCharacterController;
			}
		}

		public AudioPlayer AudioPlayer
		{
			get
			{
				return this.audioPlayer;
			}
		}

		public ActorParent.FXSystem FixFXSystem
		{
			get
			{
				return this.fixFXSystem;
			}
		}

		public float CameraPointHeight
		{
			get
			{
				if (!this.hasSetCameraPointHeight)
				{
					this.hasSetCameraPointHeight = true;
					this.cameraPointHeight = XUtility.GetTop(this.FixTransform, "camerapoint");
				}
				return this.cameraPointHeight;
			}
		}

		public MoveMode CurMoveMode
		{
			get
			{
				return this.curMoveMode;
			}
			set
			{
				this.curMoveMode = value;
			}
		}

		public Vector3 NavMeshPosition
		{
			get
			{
				return this.navMeshPosition;
			}
			protected set
			{
				this.navMeshPosition = value;
			}
		}

		public NavMeshPath NavMeshPath
		{
			get
			{
				return this.navMeshPath;
			}
			set
			{
				this.navMeshPath = value;
			}
		}

		public virtual bool IsClearTargetPosition
		{
			get
			{
				return this.isClearTragetPosition;
			}
			set
			{
				this.isClearTragetPosition = value;
			}
		}

		protected int ToCorner
		{
			get
			{
				return this.toCorner;
			}
			set
			{
				this.toCorner = value;
			}
		}

		protected float LeftLength
		{
			get
			{
				return this.leftLength;
			}
			set
			{
				this.leftLength = value;
			}
		}

		public float NextPace
		{
			get
			{
				return this.nextPace;
			}
			set
			{
				this.nextPace = value;
			}
		}

		protected bool IsMovePausing
		{
			get
			{
				return this.isMovePausing;
			}
			set
			{
				this.isMovePausing = value;
			}
		}

		protected CollisionFlags CollisionFlag
		{
			get
			{
				return this.collisionFlag;
			}
			set
			{
				this.collisionFlag = value;
			}
		}

		public bool EnableSendPos
		{
			get
			{
				return this.enableSendPos;
			}
			set
			{
				this.enableSendPos = value;
			}
		}

		public bool EnableSendDir
		{
			get
			{
				return this.enableSendDir;
			}
			set
			{
				this.enableSendDir = value;
			}
		}

		public float MoveToYAngle
		{
			get
			{
				return this.moveToYAngle;
			}
			set
			{
				this.moveToYAngle = value;
			}
		}

		public float AngularSpeed
		{
			get
			{
				return this.angularSpeed;
			}
			set
			{
				this.angularSpeed = value;
			}
		}

		protected float AngularPace
		{
			get
			{
				return this.angularPace;
			}
			set
			{
				this.angularPace = value;
			}
		}

		public bool IsRight
		{
			get
			{
				return this.isRight;
			}
			set
			{
				this.isRight = value;
			}
		}

		public float MoveByCenterTime
		{
			get
			{
				return this.moveByCenterTime;
			}
			set
			{
				this.moveByCenterTime = value;
			}
		}

		public bool IsLockModelDir
		{
			get
			{
				return this.isLockModelDir;
			}
			set
			{
				this.isLockModelDir = value;
			}
		}

		public bool IsRotating
		{
			get
			{
				return this.isRotating;
			}
			set
			{
				if (this.isRotating && !value)
				{
					this.NextRotateCountDown = this.RotateInterval;
				}
				this.isRotating = value;
			}
		}

		public float NextRotateCountDown
		{
			get
			{
				return this.nextRotateCountDown;
			}
			set
			{
				this.nextRotateCountDown = value;
			}
		}

		public float RotateSpeed
		{
			get
			{
				return this.rotateSpeed;
			}
			set
			{
				this.rotateSpeed = value;
			}
		}

		public float RotateInterval
		{
			get
			{
				return this.rotateInterval;
			}
			set
			{
				this.rotateInterval = value * 0.001f;
			}
		}

		public float StartRotateAngle
		{
			get
			{
				return this.startRotateAngle;
			}
			set
			{
				this.startRotateAngle = value;
			}
		}

		public float FinishRotateAngle
		{
			get
			{
				return this.finishRotateAngle;
			}
			set
			{
				this.finishRotateAngle = value;
			}
		}

		protected Vector3 TurnToForward
		{
			get
			{
				return this.turnToForward;
			}
			set
			{
				this.turnToForward = value;
			}
		}

		protected int TurnToSign
		{
			get
			{
				return this.turnToSign;
			}
			set
			{
				this.turnToSign = value;
			}
		}

		protected float LeftAngle
		{
			get
			{
				return this.leftAngle;
			}
			set
			{
				this.leftAngle = value;
			}
		}

		public virtual int Floor
		{
			get
			{
				return this.floor;
			}
			set
			{
				this.floor = value;
				if (value == this.FloorSetEndEvent.get_Key() && this.FloorSetEndEvent.get_Value() != null)
				{
					Action value2 = this.FloorSetEndEvent.get_Value();
					this.FloorSetEndEvent = new KeyValuePair<int, Action>(-1, null);
					value2.Invoke();
				}
			}
		}

		public ActorParent.EffectFrameSetMode SetEffectFrameMode
		{
			get
			{
				return this.setEffectFrameMode;
			}
			set
			{
				this.setEffectFrameMode = value;
			}
		}

		protected ActorParent.EffectFrameMode CurEffectFrameState
		{
			get
			{
				return this.curEffectFrameState;
			}
			set
			{
				this.curEffectFrameState = value;
			}
		}

		public float DefaultModelHeight
		{
			get
			{
				return this.defaultModelHeight;
			}
			set
			{
				this.defaultModelHeight = value;
			}
		}

		public float ModelHeight
		{
			get
			{
				return this.modelHeight;
			}
			set
			{
				this.modelHeight = value;
			}
		}

		public float FloatRate
		{
			get
			{
				return this.floatRate;
			}
			set
			{
				this.floatRate = value;
			}
		}

		protected int ActionSkillComboID
		{
			get
			{
				return this.actionSkillComboID;
			}
			set
			{
				this.actionSkillComboID = value;
			}
		}

		protected string ActionSkillTag
		{
			get
			{
				return this.actionSkillTag;
			}
			set
			{
				this.actionSkillTag = value;
			}
		}

		protected float OriginAssaultSpeed
		{
			get
			{
				if (this.originAssaultSpeed == 0f)
				{
					this.originAssaultSpeed = float.Parse(DataReader<GlobalParams>.Get("rush_speed_i").value);
				}
				return this.originAssaultSpeed;
			}
		}

		protected float ModelOriginSpeed
		{
			get
			{
				if (this.modelOriginSpeed == 0f)
				{
					this.modelOriginSpeed = (float)DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).speed * 0.01f;
				}
				return this.modelOriginSpeed;
			}
		}

		public int DataLayerState
		{
			get
			{
				return this.dataLayerState;
			}
			set
			{
				this.dataLayerState = value;
				this.UpdateLayer();
			}
		}

		public int FrameLayerState
		{
			get
			{
				return this.frameLayerState;
			}
			set
			{
				this.frameLayerState = value;
				this.UpdateLayer();
			}
		}

		public int RedererLayerState
		{
			get
			{
				return this.redererLayerState;
			}
			set
			{
				this.redererLayerState = value;
				this.UpdateLayer();
			}
		}

		public int LayerState
		{
			get
			{
				return this.DataLayerState | this.FrameLayerState | this.RedererLayerState;
			}
		}

		public bool IsDisplayingByLayer
		{
			get
			{
				return this.isDisplayingByLayer;
			}
			set
			{
				this.isDisplayingByLayer = value;
				if (value)
				{
					LayerSystem.SetGameObjectLayer(this.FixTransform.get_gameObject(), "CameraRange", 2);
				}
				else
				{
					this.UpdateLayer();
				}
			}
		}

		public float RealMoveSpeed
		{
			get
			{
				return this.realMoveSpeed;
			}
			set
			{
				this.realMoveSpeed = value;
				this.ForceSetSpeed(this.RealMoveSpeed);
			}
		}

		public float PureMoveSpeed
		{
			get
			{
				return this.pureMoveSpeed;
			}
			set
			{
				this.pureMoveSpeed = value;
			}
		}

		public float LogicMoveSpeed
		{
			get
			{
				return this.logicMoveSpeed;
			}
			set
			{
				this.logicMoveSpeed = value * 0.01f;
				this.UpdateMoveSpeed();
			}
		}

		protected float StateMoveFactor
		{
			get
			{
				if (this.GetEntity() == null || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak || this.GetEntity().IsFixed || this.GetEntity().IsAssault || this.GetEntity().IsHitMoving)
				{
					return 0f;
				}
				return 1f;
			}
		}

		protected float ActionMoveFactor
		{
			get
			{
				if (XUtility.StartsWith(this.CurActionStatus, "run"))
				{
					return 1f;
				}
				return 0f;
			}
		}

		public float RealActionSpeed
		{
			get
			{
				return this.realActionSpeed;
			}
			set
			{
				this.realActionSpeed = value;
				this.ChangeAnimationSpeed();
			}
		}

		protected float PureActionSpeed
		{
			get
			{
				return this.pureActionSpeed;
			}
			set
			{
				this.pureActionSpeed = value;
			}
		}

		public float LogicDefaultActionSpeed
		{
			get
			{
				return this.logicDefaultActionSpeed;
			}
			set
			{
				this.logicDefaultActionSpeed = value * 0.001f;
				this.UpdateActionSpeed();
			}
		}

		public float LogicRunActionSpeed
		{
			get
			{
				return this.logicRunActionSpeed;
			}
			set
			{
				this.logicRunActionSpeed = value * 0.001f;
				this.UpdateActionSpeed();
			}
		}

		protected float LogicActionSpeed
		{
			get
			{
				return (!XUtility.StartsWith(this.CurActionStatus, "run")) ? this.LogicDefaultActionSpeed : this.LogicRunActionSpeed;
			}
		}

		protected float StateActionFactor
		{
			get
			{
				if (this.GetEntity() == null || this.GetEntity().IsStatic)
				{
					return 0f;
				}
				return 1f;
			}
		}

		public float FrameActionSpeed
		{
			get
			{
				return this.frameActionSpeed;
			}
			set
			{
				if (this.frameActionSpeed != value)
				{
					this.frameActionSpeed = value;
					this.UpdateActionSpeed();
				}
			}
		}

		public bool IsStraight
		{
			get
			{
				return this.isStraight;
			}
			set
			{
				this.isStraight = value;
			}
		}

		public float StraightActionSpeed
		{
			get
			{
				return this.straightActionSpeed;
			}
			set
			{
				this.straightActionSpeed = value;
				this.UpdateActionSpeed();
			}
		}

		public float FrozenActionSpeed
		{
			get
			{
				return this.frozenActionSpeed;
			}
			set
			{
				this.frozenActionSpeed = value;
				this.UpdateActionSpeed();
			}
		}

		public float TempActionSpeed
		{
			get
			{
				return this.tempActionSpeed;
			}
			set
			{
				this.tempActionSpeed = value;
				this.UpdateActionSpeed();
			}
		}

		public override void OnPlayAction(PlayActionCmd cmd)
		{
			if (cmd.isBreak)
			{
				this.OnAnimationBreakEnd(this.CurOutPutAction);
			}
			this.OnActionStatusExit(new ActionStatusExitCmd
			{
				actName = this.CurActionStatus,
				isBreak = cmd.isBreak
			});
			string fromActionStatus = this.CurActionStatus;
			this.CurActionStatus = cmd.actName;
			this.TempActionSpeed = cmd.tempSpeed;
			EventDispatcher.Broadcast<long, int>(ActorActionEvent.SetActionSkillID, this.GetEntity().ID, cmd.skillID);
			this.ActionSkillComboID = cmd.skillComboID;
			this.ActionSkillTag = cmd.skillTag;
			this.OnActionStatusEnter(new ActionStatusEnterCmd
			{
				actName = this.CurActionStatus
			});
			if (cmd.jumpToPlay)
			{
				this.TryPlayAnimator(fromActionStatus, this.CurActionStatus, cmd.percent);
			}
			else
			{
				this.TryPlayAnimator(fromActionStatus, this.CurActionStatus, 0f);
			}
		}

		protected bool TryPlayAnimator(string fromActionStatus, string toActionStatus, float normalizedPercent)
		{
			string realActionResourceName = this.GetRealActionResourceName(toActionStatus);
			if (this.HasAction(realActionResourceName))
			{
				this.PlayAnimator(fromActionStatus, realActionResourceName, normalizedPercent);
				return true;
			}
			Debuger.Warning("{0} does not have actionName '{1}', try to use origin actionName {2}", new object[]
			{
				this.FixAnimator,
				realActionResourceName,
				toActionStatus
			});
			if (this.HasAction(toActionStatus))
			{
				this.PlayAnimator(fromActionStatus, toActionStatus, normalizedPercent);
				return true;
			}
			Debuger.Warning("{0} does not have origin actionName '{1}'", new object[]
			{
				this.FixAnimator,
				realActionResourceName
			});
			return false;
		}

		protected void PlayAnimator(string fromActionStatus, string toActionStatus, float normalizedPercent)
		{
			this.IsStageSuffixDirty = false;
			this.CurOutPutAction = toActionStatus;
			ActionFuse actionFuse = (!DataReader<ActionFuse>.Contains(fromActionStatus)) ? null : DataReader<ActionFuse>.Get(fromActionStatus);
			float num = 0f;
			if (actionFuse != null && actionFuse.GetType().GetProperty(toActionStatus) != null)
			{
				num = (float)actionFuse.GetType().GetProperty(toActionStatus).GetValue(actionFuse, null);
			}
			if (num > 0f)
			{
				this.FixAnimator.CrossFade(toActionStatus, num, 0, normalizedPercent);
			}
			else
			{
				this.FixAnimator.Play(toActionStatus, 0, normalizedPercent);
			}
		}

		public override void OnAnimationStart(AnimationStartCmd cmd)
		{
		}

		public override void OnAnimationEnd(AnimationEndCmd cmd)
		{
			if (this.CurOutPutAction != string.Empty && this.CurOutPutAction != cmd.actName)
			{
				return;
			}
			if (ActionStatusName.IsDieAction(this.CurActionStatus))
			{
				if (this.GetEntity().IsEntityMonsterType)
				{
					if (!(this.GetEntity() as EntityMonster).IsComponont)
					{
						this.DeadAnimationEnd();
					}
				}
				else
				{
					this.DeadAnimationEnd();
				}
			}
			else if (ActionStatusName.IsSpinAction(this.CurActionStatus) && !this.IsRotating)
			{
				this.EndAnimationResetToIdle();
			}
			else
			{
				if (ActionStatusName.IsBornAction(this.CurActionStatus))
				{
					this.BornAnimationEnd();
				}
				if (ActionStatusName.IsVictoryAction(this.CurActionStatus))
				{
					this.VictoryAnimationEnd();
				}
				if (!DataReader<Action>.Contains(this.CurActionStatus))
				{
					if (!string.IsNullOrEmpty(this.CurActionStatus))
					{
						Debug.LogError("Action表不存在 " + this.CurActionStatus);
					}
					this.EndAnimationResetToIdle();
				}
				else if (DataReader<Action>.Get(this.CurActionStatus).loop == 0)
				{
					if (!ActionStatusName.IsVictoryAction(this.CurActionStatus))
					{
						this.EndAnimationResetToIdle();
					}
				}
				else if (this.IsStageSuffixDirty)
				{
					this.EndAnimationResetToIdle();
				}
			}
		}

		public override void OnActionStatusExit(ActionStatusExitCmd cmd)
		{
			if (this.ActionPriorityTable.ContainsKey(cmd.actName))
			{
				this.ActionPriorityTable[cmd.actName] = this.GetOriginalPriority(cmd.actName);
			}
			this.CanAnimatorApplyMotion = false;
			this.FreezeXZ = false;
			this.FreezeY = false;
			this.FixAnimator.get_transform().set_localPosition(new Vector3(this.FixAnimator.get_transform().get_localPosition().x, 0f, this.FixAnimator.get_transform().get_localPosition().z));
			this.FixCharacterController.set_detectCollisions(true);
			this.FrameActionSpeed = 1f;
			this.ClearStraight();
			this.IsLockFaceForward = false;
			this.AllowToChangeSpeed = false;
			this.AllowToChangeDirection = false;
			this.ForceSetSpeed(0f);
			this.TempActionSpeed = 1f;
			if (!this.IsUnderCombo && !this.IsUnderTermination && cmd.isBreak && DataReader<Action>.Contains(cmd.actName) && DataReader<Action>.Get(cmd.actName).loop == 0 && this.GetEntity() != null)
			{
				SoundManager.Instance.StopPlayer(this.GetEntity().ID);
			}
			this.IsUnderCombo = false;
			this.IsUnderTermination = false;
			this.ActionSkillComboID = 0;
			this.ModelHeight = this.DefaultModelHeight;
			this.FrameLayerState = 0;
			this.IsJumpFollow = false;
			if (this.GetEntity() != null && this.GetEntity().IsEntityMonsterType && ActionStatusName.IsBornAction(cmd.actName))
			{
				ShadowController.ShowShadow(this.GetEntity().ID, this.FixTransform, false, this.GetEntity().FixModelID);
			}
			Component[] componentsInChildren = base.get_transform().GetComponentsInChildren(typeof(BaseUnit), true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				(componentsInChildren[i] as BaseUnit).OnExit();
			}
			if (DataReader<Action>.Contains(cmd.actName) && DataReader<Action>.Get(cmd.actName).loop == 0)
			{
				CommandCenter.ExecuteCommand(this.FixTransform, new RemoveActionFXCmd());
			}
			this.CurEffectFrameState = ActorParent.EffectFrameMode.Client;
			if (this.GetEntity() != null && this.GetEntity().IsSkillInTrustee)
			{
				EventDispatcher.Broadcast<string, long>(ActorActionEvent.CheckSkillTrustee, cmd.actName, this.GetEntity().ID);
			}
		}

		public void OnAnimationBreakEnd(string breakActionName)
		{
			if (breakActionName == string.Empty)
			{
				return;
			}
			if (ActionStatusName.IsDieAction(breakActionName))
			{
				this.DeadAnimationEnd();
			}
			else if (ActionStatusName.IsBornAction(breakActionName))
			{
				this.BornAnimationEnd();
			}
			else if (ActionStatusName.IsVictoryAction(breakActionName))
			{
				this.VictoryAnimationEnd();
			}
		}

		public override void OnActionStatusEnter(ActionStatusEnterCmd cmd)
		{
			ActorParent.EffectFrameSetMode effectFrameSetMode = this.SetEffectFrameMode;
			if (effectFrameSetMode != ActorParent.EffectFrameSetMode.Server)
			{
				if (effectFrameSetMode != ActorParent.EffectFrameSetMode.Ignore)
				{
					this.CurEffectFrameState = ActorParent.EffectFrameMode.Client;
				}
				else
				{
					this.CurEffectFrameState = ActorParent.EffectFrameMode.Ignore;
				}
			}
			else
			{
				this.CurEffectFrameState = ActorParent.EffectFrameMode.Server;
			}
			this.SetEffectFrameMode = ActorParent.EffectFrameSetMode.None;
			this.UpdateMoveSpeed();
			this.UpdateActionSpeed();
			this.PlayWing();
		}

		public virtual void EndAnimationResetToIdle()
		{
			this.ChangeAction("idle", true, false, 1f, 0, 0, string.Empty);
		}

		public virtual void EndAnimationResetToLoopAction(string loopAction)
		{
			this.ChangeAction(loopAction, true, false, this.TempActionSpeed, 0, 0, string.Empty);
		}

		public void OnAnimatorBecameVisiable()
		{
			if (DataReader<Action>.Contains(this.CurActionStatus))
			{
				if (DataReader<Action>.Get(this.CurActionStatus).loop != 0)
				{
					this.CastAction(this.CurActionStatus, true, 1f, 0, 0, string.Empty);
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(this.CurActionStatus))
				{
					Debug.LogError("Action表不存在 " + this.CurActionStatus);
				}
				this.EndAnimationResetToIdle();
			}
		}

		public virtual void InitActionPriorityTable()
		{
			this.ActionPriorityTable.Clear();
			for (int i = 0; i < DataReader<Action>.DataList.get_Count(); i++)
			{
				this.ActionPriorityTable.Add(DataReader<Action>.DataList.get_Item(i).action, DataReader<Action>.DataList.get_Item(i).priority);
			}
		}

		protected virtual int GetOriginalPriority(string name)
		{
			return DataReader<Action>.Get(name).priority;
		}

		public void AddStageSuffix(string name)
		{
			if (!XUtility.StartsWith(name, ActorParent.SuffixPrefix))
			{
				return;
			}
			this.StageSuffix = name;
		}

		public void RemoveStageSuffix(string name)
		{
			if (this.StageSuffix != name)
			{
				return;
			}
			this.StageSuffix = string.Empty;
		}

		protected string GetRealActionResourceName(string toActionName)
		{
			if (!(toActionName == "idle") && !(toActionName == "run") && !(toActionName == "hit"))
			{
				return toActionName;
			}
			if (this.GetEntity().IsInBattle)
			{
				return toActionName + this.StageSuffix;
			}
			string text = toActionName + ActorParent.CitySuffix;
			if (this.HasAction(text))
			{
				return text;
			}
			return toActionName;
		}

		public override void OnRootMotion(RootMotionCmd cmd)
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.CanAnimatorApplyMotion = cmd.rootMotion;
		}

		public override void OnIgnoreCollision(IgnoreCollisionCmd cmd)
		{
			if (cmd.closeCollision)
			{
				this.FrameLayerState = 1;
			}
		}

		protected void ChangeAnimationSpeed()
		{
			if (this.RealActionSpeed < 10f)
			{
				this.FixAnimator.set_speed(this.RealActionSpeed);
				CommandCenter.ExecuteCommand(this.FixTransform, new ChangeSpeedActionFXCmd
				{
					actionID = this.GetCurrentActionHash(),
					rate = this.RealActionSpeed
				});
			}
			else
			{
				Debug.LogError(string.Concat(new object[]
				{
					this.GetEntity().ID,
					" 变速值异常: ",
					this.RealActionSpeed,
					": ",
					this.LogicActionSpeed,
					" ",
					this.StateActionFactor,
					" ",
					this.FrameActionSpeed,
					" ",
					this.FrozenActionSpeed,
					" ",
					this.StraightActionSpeed
				}));
			}
		}

		public override void OnActionStraight(ActionStraightCmd cmd)
		{
			if (this.straightTime > 0u && !this.IsStraight && this.GetEntity() != null)
			{
				this.IsStraight = true;
				this.StraightActionSpeed = (float)DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).hitstraight * cmd.rate;
				TimerHeap.DelTimer(this.straightTimerID);
				this.straightTimerID = TimerHeap.AddTimer(this.straightTime, 0, new Action(this.EndStraight));
			}
		}

		protected void EndStraight()
		{
			this.IsStraight = false;
			this.StraightActionSpeed = 1f;
		}

		public void ClearStraight()
		{
			TimerHeap.DelTimer(this.straightTimerID);
			this.straightTime = 0u;
			this.IsStraight = false;
			this.StraightActionSpeed = 1f;
		}

		protected bool HasAction(string actionName)
		{
			return this.FixAnimator && this.FixAnimator.HasAction(actionName);
		}

		protected bool HasActionOrFixAction(string actionName)
		{
			return this.FixAnimator && (this.FixAnimator.HasAction(actionName) || this.FixAnimator.HasAction(this.GetRealActionResourceName(actionName)));
		}

		public int GetCurrentActionHash()
		{
			return Animator.StringToHash(this.CurActionStatus);
		}

		public void PlayWing()
		{
			if (this.WingAnimator == null)
			{
				return;
			}
			if (this.WingAnimator.get_runtimeAnimatorController() == null)
			{
				return;
			}
			for (int i = 0; i < this.WingAnimator.get_runtimeAnimatorController().get_animationClips().Length; i++)
			{
				if (this.WingAnimator.get_runtimeAnimatorController().get_animationClips()[i].get_name() == this.CurOutPutAction)
				{
					this.WingAnimator.Play(this.CurOutPutAction);
					return;
				}
			}
			this.WingAnimator.Play("idle");
		}

		public void SetLoopActionCountdown(string actionName, int countdown)
		{
			this.AppointedLoopActionName = actionName;
			this.AppointedLoopActionCountdown = countdown;
		}

		public void ResetLoopActionCountdown()
		{
			this.AppointedLoopActionName = null;
			this.AppointedLoopActionCountdown = 0;
		}

		protected void CheckChangeResetLoopActionCountdown(string actionName)
		{
			if (actionName != this.AppointedLoopActionName)
			{
				this.ResetLoopActionCountdown();
			}
		}

		protected bool CheckLoopActionCountdownEnd()
		{
			if (string.IsNullOrEmpty(this.AppointedLoopActionName))
			{
				return false;
			}
			this.AppointedLoopActionCountdown--;
			if (this.AppointedLoopActionCountdown > 0)
			{
				return false;
			}
			this.ResetLoopActionCountdown();
			return true;
		}

		public override void OnAllowChangeSpeed(AllowChangeSpeedCmd cmd)
		{
			this.AllowToChangeSpeed = true;
		}

		public override void OnAllowChangeDirection(AllowChangeDirectionCmd cmd)
		{
			this.AllowToChangeDirection = true;
		}

		public void ForceSetDirection(Vector3 vec)
		{
			this.m_direction = vec.get_normalized();
		}

		public override void OnLockFaceForward(LockFaceForwardCmd cmd)
		{
			this.IsLockFaceForward = true;
		}

		public void ApplyMovingDirAsForward()
		{
			if (this.GetEntity().IsDead || this.IsLockModelDir || this.IsLockFaceForward)
			{
				return;
			}
			if (this.MovingDirection != Vector3.get_zero())
			{
				this.FixTransform.set_forward(this.MovingDirection);
			}
		}

		public void ForceSetSpeed(float _speed)
		{
			this.m_speed = _speed;
		}

		public virtual EntityParent GetEntity()
		{
			return null;
		}

		protected override void Awake()
		{
			base.Awake();
			this.fixTransform = base.get_transform();
			this.fixGameObject = base.get_gameObject();
			this.fixAnimator = base.GetComponentInChildren<Animator>();
			this.fixAnimator.set_enabled(true);
			this.fixCharacterController = base.GetComponent<CharacterController>();
			this.fixCharacterController.set_contactOffset(this.fixCharacterController.get_radius());
			this.SetNavMeshAgent();
			this.SetCollider();
			this.audioPlayer = base.get_gameObject().AddUniqueComponent<AudioPlayer>();
			this.foot_y_node = XUtility.RecursiveFindTransform(this.FixTransform, ActorParent.foot_y_node_name);
		}

		protected override void Start()
		{
			base.Start();
			if (MySceneManager.Instance.IsSceneExist)
			{
				this.LoadEndResetPoistion();
			}
			this.audioPlayer.RoleId = this.GetEntity().ID;
		}

		protected virtual void Update()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		public virtual void ClearData()
		{
			TimerHeap.DelTimer(this.straightTimerID);
			for (int i = 0; i < this.freezeTimer.get_Count(); i++)
			{
				TimerHeap.DelTimer(this.freezeTimer.get_Item(i));
			}
			for (int j = 0; j < this.thawTimer.get_Count(); j++)
			{
				TimerHeap.DelTimer(this.thawTimer.get_Item(j));
			}
		}

		public virtual void DestroyScript()
		{
			this.Destroy();
		}

		public virtual void FadeDestroyScript(List<AdjustTransparency> alphaControls)
		{
			ShaderEffectUtils.SetFade(alphaControls, true, delegate
			{
				this.Destroy();
			});
		}

		public virtual void ResetController()
		{
		}

		public virtual void SetBorn()
		{
			this.CastAction("born", true, 1f, 0, 0, string.Empty);
		}

		public virtual void BornAnimationEnd()
		{
			this.GetEntity().BornEnd();
		}

		public void SetDie()
		{
			this.CastAction("die", true, 1f, 0, 0, string.Empty);
			List<int> diefx = DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).diefx;
			for (int i = 0; i < diefx.get_Count(); i++)
			{
				int templateId = diefx.get_Item(i);
				FXManager.Instance.PlayFX(templateId, null, new Vector3(this.FixTransform.get_position().x, 0f, this.FixTransform.get_position().z), this.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
			if (this.chosenFx != 0)
			{
				FXManager.Instance.DeleteFX(this.chosenFx);
				this.chosenFx = 0;
			}
		}

		public virtual void DeadAnimationEnd()
		{
			this.GetEntity().DieEnd();
			this.DeadToDestroy();
		}

		public virtual void DeadToDestroy()
		{
			this.GetEntity().OnLeaveField();
			this.FrameLayerState = 1;
			this.ClearStraight();
			this.ClearFreezeFrame();
			if (this.actorCollider)
			{
				this.actorCollider.Actor = null;
			}
			if (this.actorCushion)
			{
				this.actorCushion.Actor = null;
			}
			this.DestroyScript();
		}

		public void VictoryAnimationEnd()
		{
			this.GetEntity().VictoryEnd();
		}

		public void CastAction(string actionStatusName, bool isCheckHitMoving = true, float extraSpeed = 1f, int candidateSkillID = 0, int candidateSkillComboID = 0, string candidateSkillTag = "")
		{
			if (this.CanChangeActionTo(actionStatusName, isCheckHitMoving, candidateSkillID, false))
			{
				this.ChangeAction(actionStatusName, false, true, extraSpeed, candidateSkillID, candidateSkillComboID, candidateSkillTag);
			}
		}

		public void ServerCastAction(string actionStatusName, int actionPriority, ActorParent.EffectFrameSetMode effectSetMode = ActorParent.EffectFrameSetMode.Server, float extraSpeed = 1f, int candidateSkillID = 0, int candidateSkillComboID = 0, string candidateSkillTag = "")
		{
			this.ActionPriorityTable[actionStatusName] = actionPriority;
			this.SetEffectFrameMode = effectSetMode;
			this.ChangeAction(actionStatusName, true, true, extraSpeed, candidateSkillID, candidateSkillComboID, candidateSkillTag);
		}

		public virtual bool CanChangeActionTo(string newAction, bool isCheckHitMoving = true, int candidateSkillID = 0, bool isLogOpen = false)
		{
			if (string.IsNullOrEmpty(newAction) || string.IsNullOrEmpty(this.CurActionStatus))
			{
				return true;
			}
			if (this.GetEntity().IsStatic)
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.2");
				}
				return false;
			}
			if (isCheckHitMoving && this.GetEntity().IsHitMoving && !ActionStatusName.IsDieAction(newAction))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.3");
				}
				return false;
			}
			if (isCheckHitMoving && this.IsStraight && !ActionStatusName.IsDieAction(newAction) && !ActionStatusName.IsHitAction(newAction))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.4");
				}
				return false;
			}
			if (this.GetEntity().IsEndure && ActionStatusName.IsHitAction(newAction))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.5");
				}
				return false;
			}
			if (this.GetEntity().IsWeak && (ActionStatusName.IsActionCauseNormalMove(newAction) || ActionStatusName.IsSpinAction(newAction)))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.6");
				}
				return false;
			}
			if (!this.HasActionOrFixAction(newAction))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.7");
				}
				return false;
			}
			if (this.IsUnderCombo && this.ActionSkillComboID != 0 && this.ActionSkillComboID == candidateSkillID)
			{
				return true;
			}
			if (newAction == this.CurActionStatus)
			{
				return true;
			}
			if (this.ActionPriorityTable[newAction] <= this.ActionPriorityTable[this.CurActionStatus] && !this.IsUnderTermination && !ActionStatusName.IsIdleAction(newAction) && (!ActionStatusName.IsActionCauseNormalMove(this.CurActionStatus) || !ActionStatusName.IsActionCauseNormalMove(newAction)))
			{
				if (this.GetEntity().IsEntitySelfType && isLogOpen)
				{
					Debug.LogError("return 5.4.8");
				}
				return false;
			}
			return true;
		}

		public virtual bool ChangeAction(string newAction, bool isForceChange = false, bool isBreakChange = true, float extraSpeed = 1f, int candidateSkillID = 0, int candidateSkillComboID = 0, string candidateSkillTag = "")
		{
			if (string.IsNullOrEmpty(newAction))
			{
				return true;
			}
			if (this.GetEntity() == null)
			{
				return false;
			}
			if (this.GetEntity().IsStatic)
			{
				return false;
			}
			if (!this.FixAnimator)
			{
				return false;
			}
			if (newAction == "idle" && DataReader<AvatarModel>.Contains(this.GetEntity().ModelID) && DataReader<AvatarModel>.Get(this.GetEntity().ModelID).mineCar == 1)
			{
				newAction = "run";
			}
			if (!isForceChange && this.GetEntity().IsDead && !ActionStatusName.IsDieAction(newAction))
			{
				newAction = "die";
			}
			if (this.HasActionOrFixAction(newAction))
			{
				this.CheckIsEndAssault(this.CurActionStatus, newAction);
				if (this.CurActionStatus != newAction || DataReader<Action>.Get(newAction).loop == 0 || isForceChange)
				{
					CommandCenter.ExecuteCommand(this.FixTransform, new PlayActionCmd
					{
						actName = newAction,
						jumpToPlay = true,
						isBreak = isBreakChange,
						tempSpeed = extraSpeed,
						skillID = candidateSkillID,
						skillComboID = candidateSkillComboID,
						skillTag = candidateSkillTag
					});
				}
				return true;
			}
			return false;
		}

		protected void CheckIsEndAssault(string oldAction, string newAction)
		{
			if (this.GetEntity().IsAssault && XUtility.StartsWith(oldAction, "rush") && !XUtility.StartsWith(newAction, "rush"))
			{
				this.GetEntity().IsAssault = false;
			}
		}

		protected void CheckIsCancelSkill(string actionName)
		{
			if (ActionStatusName.IsSkillAction(actionName))
			{
				EventDispatcher.Broadcast<long>(SkillEvent.CancelUseSkill, this.GetEntity().ID);
			}
		}

		public override void OnSetTermination(SetTerminationCmd cmd)
		{
			if (this.GetEntity() != null && InstanceManager.IsLocalBattle && (string.IsNullOrEmpty(cmd.actionName) || cmd.actionName == this.CurActionStatus))
			{
				this.IsUnderTermination = true;
			}
		}

		protected virtual void MoveProcess()
		{
			if (this.FixNavMeshAgent == null)
			{
				return;
			}
			this.deltaTime = Time.get_deltaTime();
			this.commonMoveOffset = this.TryUpdateCommonMove(this.deltaTime);
			this.ColliderMeshMove(this.commonMoveOffset, this.deltaTime, true);
			this.hitMoveOffset = this.TryUpdateHitMove(this.deltaTime);
			this.ColliderAndNavMeshMove(this.hitMoveOffset, this.deltaTime);
			this.ColliderMeshMove(this.actionMoveOffset, 1f, false);
			this.UpdateAfterMoveState();
		}

		public void SetNavMeshAgent()
		{
			NavMeshAgent component = this.FixTransform.GetComponent<NavMeshAgent>();
			if (component)
			{
				Object.Destroy(component);
			}
			if (this.fixNavMeshAgent)
			{
				return;
			}
			if (base.get_transform().FindChild("NavMeshHelper") == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.SetActive(MySceneManager.Instance.IsSceneExist);
				gameObject.set_name("NavMeshHelper");
				gameObject.get_transform().SetParent(this.FixTransform);
				gameObject.get_transform().set_localPosition(Vector3.get_zero());
				this.fixNavMeshAgent = gameObject.AddMissingComponent<NavMeshAgent>();
			}
			else
			{
				GameObject gameObject2 = base.get_transform().FindChild("NavMeshHelper").get_gameObject();
				gameObject2.SetActive(MySceneManager.Instance.IsSceneExist);
				this.fixNavMeshAgent = gameObject2.AddMissingComponent<NavMeshAgent>();
			}
			this.fixNavMeshAgent.set_updatePosition(false);
			this.fixNavMeshAgent.set_updateRotation(false);
		}

		public virtual void MoveToPoint(Vector3 pos, float radius = 0f, Action callback = null)
		{
			if (this.FixNavMeshAgent == null)
			{
				return;
			}
			this.IsClearTargetPosition = false;
			this.NavMeshPosition = this.FixNavMeshPosition(pos);
			this.FixNavMeshAgent.get_transform().set_localPosition(Vector3.get_zero());
			this.FixNavMeshAgent.get_transform().set_position(this.FixNavMeshPosition(this.FixNavMeshAgent.get_transform().get_position()));
			this.FixNavMeshAgent.set_radius(radius);
			NavMesh.CalculatePath(this.FixNavMeshAgent.get_transform().get_position(), this.NavMeshPosition, -1, this.NavMeshPath);
			if (this.NavMeshPath.get_corners().Length < 1)
			{
				this.IsClearTargetPosition = true;
				return;
			}
			this.CurMoveMode = MoveMode.MoveByPoint;
			this.FixCharacterController.Move(Vector3.get_zero());
			this.MoveEndCallBack = callback;
			this.GetEntity().AIToPoint = null;
			if (XUtility.DistanceNoY(this.NavMeshPath.get_corners()[0], this.FixTransform.get_position()) > this.NextPace || this.NavMeshPath.get_corners().Length == 1)
			{
				this.ToCorner = 0;
				if (XUtility.DistanceNoY(this.NavMeshPath.get_corners()[0], this.FixTransform.get_position()) <= this.NextPace)
				{
					this.IsClearTargetPosition = true;
					this.FixTransform.set_position(new Vector3(this.NavMeshPath.get_corners()[0].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[0].z));
					if (this.MoveEndCallBack != null)
					{
						this.MoveEndCallBack.Invoke();
						this.MoveEndCallBack = null;
					}
					return;
				}
			}
			else
			{
				this.ToCorner = 1;
				if (XUtility.DistanceNoY(this.NavMeshPath.get_corners()[0], this.FixTransform.get_position()) <= this.NextPace)
				{
					this.FixTransform.set_position(new Vector3(this.NavMeshPath.get_corners()[0].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[0].z));
				}
			}
			this.LeftLength = XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPath.get_corners()[this.ToCorner]);
			if (!this.CanChangeActionTo("run", true, 0, false))
			{
				return;
			}
			this.TurnToPos(new Vector3(this.NavMeshPath.get_corners()[this.ToCorner].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[this.ToCorner].z));
			this.ChangeAction("run", false, true, 1f, 0, 0, string.Empty);
		}

		protected Vector3 FixNavMeshPosition(Vector3 pos)
		{
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(pos, ref navMeshHit, 500f, -1))
			{
				return navMeshHit.get_position();
			}
			return pos;
		}

		public bool MoveToAngle(float toYAngle)
		{
			if (!this.CanChangeActionTo("swingright", true, 0, false))
			{
				return false;
			}
			if (!this.CanChangeActionTo("swingleft", true, 0, false))
			{
				return false;
			}
			this.CurMoveMode = MoveMode.MoveByAngle;
			this.MoveToYAngle = toYAngle;
			return true;
		}

		public bool MoveAroundCenter(float time)
		{
			if (!this.CanChangeActionTo("swingright", true, 0, false))
			{
				return false;
			}
			if (!this.CanChangeActionTo("swingleft", true, 0, false))
			{
				return false;
			}
			if (this.IsRight)
			{
				if (!this.HasAction("swingright"))
				{
					return false;
				}
				this.CurMoveMode = MoveMode.MoveAroundByCenter;
				this.MoveByCenterTime = time;
				this.CastAction("swingright", true, 1f, 0, 0, string.Empty);
			}
			else
			{
				if (!this.HasAction("swingleft"))
				{
					return false;
				}
				this.CurMoveMode = MoveMode.MoveAroundByCenter;
				this.MoveByCenterTime = time;
				this.CastAction("swingleft", true, 1f, 0, 0, string.Empty);
			}
			return true;
		}

		public bool MoveBackCenter(float time)
		{
			if (!this.CanChangeActionTo("swingback", true, 0, false))
			{
				return false;
			}
			if (!this.HasAction("swingback"))
			{
				return false;
			}
			this.CurMoveMode = MoveMode.MoveBackByCenter;
			this.MoveByCenterTime = time;
			this.CastAction("swingback", true, 1f, 0, 0, string.Empty);
			return true;
		}

		public void StopAllMove()
		{
			switch (this.CurMoveMode)
			{
			case MoveMode.MoveByPoint:
				this.StopMoveToPoint();
				break;
			case MoveMode.MoveByAngle:
				this.StopMoveToAngle();
				break;
			case MoveMode.MoveAroundByCenter:
				this.StopMoveAroundCenter();
				break;
			case MoveMode.MoveBackByCenter:
				this.StopMoveBackCenter();
				break;
			}
		}

		public void StopMoveToPoint()
		{
			this.IsClearTargetPosition = true;
			this.MovingDirection = Vector3.get_zero();
			if (XUtility.StartsWith(this.CurActionStatus, "run"))
			{
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
			}
		}

		public void StopMoveToAngle()
		{
			this.MoveToYAngle = this.FixTransform.get_eulerAngles().y;
			if (XUtility.StartsWith(this.CurActionStatus, "swing"))
			{
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
			}
		}

		public void StopMoveAroundCenter()
		{
			this.MoveByCenterTime = 0f;
			if (XUtility.StartsWith(this.CurActionStatus, "swing"))
			{
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
			}
		}

		public void StopMoveBackCenter()
		{
			this.MoveByCenterTime = 0f;
			if (XUtility.StartsWith(this.CurActionStatus, "swing"))
			{
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
			}
		}

		public void Teleport(Vector3 pos)
		{
			Vector3 position = this.FixTransform.get_position();
			this.SetAndFixPosition(pos, null, 301);
			FXManager.Instance.PlayFX(96, null, position, this.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			FXManager.Instance.PlayFX(96, null, this.FixTransform.get_position(), this.FixTransform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		}

		protected Vector3 TryUpdateCommonMove(float deltaTime)
		{
			switch (this.CurMoveMode)
			{
			case MoveMode.MoveByPoint:
				this.UpdateMoveByPointState(deltaTime);
				return this.MoveByPoint();
			case MoveMode.MoveByAngle:
				this.UpdateMoveByAngleState();
				this.MoveByAngle(deltaTime);
				return Vector3.get_zero();
			case MoveMode.MoveAroundByCenter:
				this.UpdateAroundByCenterState(deltaTime);
				this.MoveAroundByCenter();
				return Vector3.get_zero();
			case MoveMode.MoveBackByCenter:
				this.UpdateMoveBackByCenterState(deltaTime);
				this.MoveBackByCenter();
				return Vector3.get_zero();
			default:
				return Vector3.get_zero();
			}
		}

		protected virtual void UpdateMoveByPointState(float deltaTime)
		{
			if (this.IsMovePausing)
			{
				if (!this.IsClearTargetPosition && !this.GetEntity().IsStatic && !this.GetEntity().IsDizzy && !this.GetEntity().IsWeak && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && this.ToCorner < this.NavMeshPath.get_corners().Length)
				{
					this.IsMovePausing = false;
					this.FixTransform.LookAt(new Vector3(this.NavMeshPath.get_corners()[this.ToCorner].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[this.ToCorner].z));
					this.LeftLength = XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPath.get_corners()[this.ToCorner]);
					if (this.CanChangeActionTo("run", true, 0, false))
					{
						this.ChangeAction("run", false, true, 1f, 0, 0, string.Empty);
					}
				}
			}
			else
			{
				this.CalculateNextPace(deltaTime);
				if ((!XUtility.StartsWith(this.CurActionStatus, "run") && !XUtility.StartsWith(this.CurActionStatus, "idle") && !this.GetEntity().IsMoveCast) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak)
				{
					this.IsMovePausing = true;
					this.MovingDirection = Vector3.get_zero();
				}
				else if (XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPosition) <= 0.05f || this.ToCorner == this.NavMeshPath.get_corners().Length || (this.NextPace >= this.LeftLength && this.ToCorner == this.NavMeshPath.get_corners().Length - 1))
				{
					if (!this.IsClearTargetPosition)
					{
						this.StopMoveToPoint();
						this.FixTransform.set_position(new Vector3(this.NavMeshPosition.x, this.FixTransform.get_position().y, this.NavMeshPosition.z));
						this.LeftLength = 0f;
						if (this.MoveEndCallBack != null)
						{
							Action moveEndCallBack = this.MoveEndCallBack;
							this.MoveEndCallBack = null;
							moveEndCallBack.Invoke();
						}
					}
				}
				else if (this.NavMeshPath.get_corners().Length > 0 && (XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPath.get_corners()[this.ToCorner]) <= 0.05f || this.NextPace > this.LeftLength))
				{
					if (!this.IsClearTargetPosition)
					{
						this.FixTransform.set_position(new Vector3(this.NavMeshPath.get_corners()[this.ToCorner].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[this.ToCorner].z));
						this.ToCorner++;
						this.MovingDirection = Vector3.get_zero();
						if (this.ToCorner < this.NavMeshPath.get_corners().Length)
						{
							this.FixTransform.LookAt(new Vector3(this.NavMeshPath.get_corners()[this.ToCorner].x, this.FixTransform.get_position().y, this.NavMeshPath.get_corners()[this.ToCorner].z));
							this.LeftLength = XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPath.get_corners()[this.ToCorner]);
						}
					}
				}
				else if (!this.IsClearTargetPosition && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && this.ToCorner < this.NavMeshPath.get_corners().Length)
				{
					Vector3 vector = new Vector3(this.NavMeshPath.get_corners()[this.ToCorner].x - this.FixTransform.get_position().x, 0f, this.NavMeshPath.get_corners()[this.ToCorner].z - this.FixTransform.get_position().z);
					this.MovingDirection = vector.get_normalized();
				}
			}
		}

		protected void CalculateNextPace(float deltaTime)
		{
			this.NextPace = this.RealMoveSpeed * deltaTime;
		}

		protected Vector3 MoveByPoint()
		{
			if (!MySceneManager.Instance.IsSceneExist)
			{
				return Vector3.get_zero();
			}
			if (this.CurMoveMode != MoveMode.MoveByPoint)
			{
				return Vector3.get_zero();
			}
			return this.Velocity;
		}

		protected void UpdateMoveByAngleState()
		{
			if (!XUtility.StartsWith(this.CurActionStatus, "swing") || (!this.CanChangeActionTo("swingleft", true, 0, false) && !this.CanChangeActionTo("swingright", true, 0, false)) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak || this.GetEntity().IsFixed || this.GetEntity().IsHitMoving || this.GetEntity().IsAssault)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
			}
		}

		protected void MoveByAngle(float deltaTime)
		{
			if (this.CurMoveMode != MoveMode.MoveByAngle)
			{
				return;
			}
			float num = this.MoveToYAngle % 360f;
			float num2 = this.FixTransform.get_eulerAngles().y % 360f;
			if (num < 0f)
			{
				num += 360f;
			}
			if (num2 < 0f)
			{
				num2 += 360f;
			}
			if (num - num2 < -180f)
			{
				num += 360f;
			}
			this.AngularSpeed = Mathf.Atan(0f) * 180f / 3.14159274f;
			float num3 = num - num2;
			this.AngularPace = deltaTime * this.AngularSpeed;
			if (Mathf.Abs(num3) < this.AngularPace)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.FixTransform.set_eulerAngles(new Vector3(0f, this.MoveToYAngle, 0f));
			}
			else
			{
				this.IsRight = (num3 > 0f && num3 < 180f);
				if (this.IsRight)
				{
					this.FixTransform.Rotate(new Vector3(0f, this.AngularPace, 0f), 1);
					this.CastAction("swingright", true, 1f, 0, 0, string.Empty);
				}
				else
				{
					this.FixTransform.Rotate(new Vector3(0f, -this.AngularPace, 0f), 1);
					this.CastAction("swingleft", true, 1f, 0, 0, string.Empty);
				}
			}
		}

		protected void UpdateAroundByCenterState(float deltaTime)
		{
			if (!XUtility.StartsWith(this.CurActionStatus, "swing") || (!this.CanChangeActionTo("swingleft", true, 0, false) && !this.CanChangeActionTo("swingright", true, 0, false)) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak || this.GetEntity().IsFixed || this.GetEntity().IsHitMoving || this.GetEntity().IsAssault)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				return;
			}
			this.MoveByCenterTime -= deltaTime;
			if (this.MoveByCenterTime < 0f)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
			if (this.GetEntity().MoveAroundCenter == null)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
			if (!this.GetEntity().MoveAroundCenter.Actor)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
		}

		protected void MoveAroundByCenter()
		{
			if (this.CurMoveMode != MoveMode.MoveAroundByCenter)
			{
				return;
			}
			this.FixTransform.LookAt(new Vector3(this.GetEntity().MoveAroundCenter.Actor.FixTransform.get_position().x, this.FixTransform.get_position().y, this.GetEntity().MoveAroundCenter.Actor.FixTransform.get_position().z));
			if (this.IsRight)
			{
				this.CastAction("swingright", true, 1f, 0, 0, string.Empty);
			}
			else
			{
				this.CastAction("swingleft", true, 1f, 0, 0, string.Empty);
			}
		}

		protected void UpdateMoveBackByCenterState(float deltaTime)
		{
			if (!XUtility.StartsWith(this.CurActionStatus, "swing") || !this.CanChangeActionTo("swingback", true, 0, false) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak || this.GetEntity().IsFixed || this.GetEntity().IsHitMoving || this.GetEntity().IsAssault)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				return;
			}
			this.MoveByCenterTime -= deltaTime;
			if (this.MoveByCenterTime < 0f)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
			if (this.GetEntity().MoveAroundCenter == null)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
			if (!this.GetEntity().MoveAroundCenter.Actor)
			{
				this.CurMoveMode = MoveMode.MoveByPoint;
				this.CastAction("idle", true, 1f, 0, 0, string.Empty);
				return;
			}
		}

		protected void MoveBackByCenter()
		{
			if (this.CurMoveMode != MoveMode.MoveBackByCenter)
			{
				return;
			}
			this.FixTransform.LookAt(new Vector3(this.GetEntity().MoveAroundCenter.Actor.FixTransform.get_position().x, this.FixTransform.get_position().y, this.GetEntity().MoveAroundCenter.Actor.FixTransform.get_position().z));
			this.CastAction("swingback", true, 1f, 0, 0, string.Empty);
		}

		protected Vector3 TryUpdateHitMove(float deltaTime)
		{
			if (!this.GetEntity().IsHitMoving)
			{
				return Vector3.get_zero();
			}
			this.UpdateHitMoveState(deltaTime);
			return this.HitMove();
		}

		protected void UpdateHitMoveState(float deltaTime)
		{
			if (this.hitMoveCountDown >= 0f && this.GetEntity().IsHitMoving && !this.GetEntity().IsStatic && !this.GetEntity().IsFixed)
			{
				this.hitMoveCountDown -= deltaTime;
			}
			else if (this.GetEntity().IsHitMoving)
			{
				this.EndHitMove();
			}
		}

		protected Vector3 HitMove()
		{
			if (!this.GetEntity().IsHitMoving)
			{
				return Vector3.get_zero();
			}
			return -this.FixTransform.get_forward() * this.hitMoveSpeed;
		}

		protected Vector3 TryUpdateAssaultMove(float deltaTime)
		{
			if (!this.GetEntity().IsAssault)
			{
				return Vector3.get_zero();
			}
			this.UpdateAssaultState(deltaTime);
			return this.Assault();
		}

		protected void UpdateAssaultState(float deltaTime)
		{
			if (!this.GetEntity().IsStatic && !this.GetEntity().IsDizzy && !this.GetEntity().IsWeak && !this.GetEntity().IsFixed && !this.GetEntity().IsHitMoving && XUtility.StartsWith(this.CurActionStatus, "rush") && this.GetEntity().IsAssault)
			{
				this.assaultTime -= deltaTime;
				this.assaultTarget = EntityWorld.Instance.GetEntityByID(this.assaultTargetID);
				if (this.assaultTime <= 0f || (this.assaultTarget != null && this.assaultTarget.Actor != null && XUtility.DistanceNoY(this.FixTransform.get_position(), this.assaultTarget.Actor.FixTransform.get_position()) - XUtility.GetHitRadius(this.assaultTarget.Actor.FixTransform) <= this.assaultEndSkillDistance))
				{
					this.GetEntity().IsAssault = false;
					this.AssaultSuccess();
				}
			}
			else if (this.GetEntity().IsAssault)
			{
				this.EndAssault();
			}
		}

		protected Vector3 Assault()
		{
			if (!this.GetEntity().IsAssault)
			{
				return Vector3.get_zero();
			}
			this.ForceSetDirection(this.assaultEndPos - this.FixTransform.get_position());
			this.ApplyMovingDirAsForward();
			return this.FixTransform.get_forward() * this.assaultSpeed;
		}

		public void MarkActionMove(Vector3 deltaMove)
		{
			this.actionMoveOffset += deltaMove;
		}

		protected void ResetActionMove()
		{
			this.actionMoveOffset = Vector3.get_zero();
		}

		protected void ColliderMeshMove(Vector3 offset, float deltaTime, bool isUseGravity)
		{
			if (!this.FixCharacterController.get_enabled())
			{
				this.FixCharacterController.set_enabled(true);
			}
			if (offset == Vector3.get_zero())
			{
				return;
			}
			Vector3 position = this.FixTransform.get_position();
			this.CollisionFlag = this.FixCharacterController.Move(offset * deltaTime);
			Vector3 position2;
			if (MySceneManager.GetTerrainPoint(this.FixTransform.get_position().x, this.FixTransform.get_position().z, this.FixTransform.get_position().y, out position2))
			{
				if (this.IsYChangeSuddenly(position.y, this.FixTransform.get_position().y))
				{
					if (this.IsYChangeSuddenly(position.y, position2.y))
					{
						this.FixCurrentPosition();
					}
					else
					{
						this.FixTransform.set_position(position2);
					}
				}
				else if (isUseGravity && this.FixTransform.get_position().y > position2.y)
				{
					if ((XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle")) && (this.CollisionFlag & 4) == null)
					{
						this.FixCharacterController.Move(Physics.get_gravity() * deltaTime);
					}
				}
				else if (this.IsYChangeSuddenly(position.y, position2.y))
				{
					this.FixCurrentPosition();
				}
				else
				{
					this.FixTransform.set_position(position2);
				}
			}
			else
			{
				this.FixTransform.set_position(position);
			}
		}

		protected void ColliderAndNavMeshMove(Vector3 offset, float deltaTime)
		{
			if (offset == Vector3.get_zero())
			{
				return;
			}
			offset.y = 0f;
			Vector3 position;
			if (NavMesh.SamplePosition(this.FixTransform.get_position() + offset * deltaTime, ref this.fixHit, 500f, -1) && MySceneManager.GetTerrainPoint(this.fixHit.get_position().x, this.fixHit.get_position().z, this.FixTransform.get_position().y, out position))
			{
				this.FixTransform.set_position(position);
			}
		}

		protected void UpdateAfterMoveState()
		{
			if (this.NavMeshPath != null && this.NavMeshPath.get_corners() != null && this.ToCorner < this.NavMeshPath.get_corners().Length)
			{
				this.LeftLength = XUtility.DistanceNoY(this.FixTransform.get_position(), this.NavMeshPath.get_corners()[this.ToCorner]);
			}
			this.ResetActionMove();
		}

		public virtual void EnableCityAutoSendPos()
		{
		}

		public virtual void EnableBattleAutoSendPos()
		{
		}

		public virtual void SendPos()
		{
		}

		public virtual void CheckSendPrecisePosOnReleaseDrag()
		{
		}

		public virtual void SendPrecisePos()
		{
		}

		public virtual void LoadEndResetPoistion()
		{
			this.FixTransform.set_position(new Vector3(this.FixTransform.get_position().x, this.GetEntity().CurFloorStandardHeight, this.FixTransform.get_position().z));
			this.FixCurrentPosition();
		}

		public virtual void SendDir()
		{
		}

		public virtual void SendFloor(int sendfloor)
		{
		}

		public bool MoveDeviationEstimated(Vector3 toPosition)
		{
			return false;
		}

		public bool EndAssaultDeviationEstimated(Vector3 toPosition)
		{
			float num = XUtility.DistanceNoY(toPosition, this.FixTransform.get_position());
			return num >= this.PureMoveSpeed * 0.01f || num <= this.NextPace;
		}

		public bool EndHitMoveDeviationEstimated(Vector3 toPosition)
		{
			float num = XUtility.DistanceNoY(toPosition, this.FixTransform.get_position());
			return num >= this.PureMoveSpeed * 0.01f || num <= this.NextPace;
		}

		public bool EndSkillManageDeviationEstimated(Vector3 toPosition)
		{
			float num = XUtility.DistanceNoY(toPosition, this.FixTransform.get_position());
			return num >= this.PureMoveSpeed * 0.01f || num <= this.NextPace;
		}

		public void FixCurrentPosition()
		{
			if (NavMesh.SamplePosition(this.FixTransform.get_position(), ref this.fixHit, 500f, -1))
			{
				this.FixTransform.set_position(MySceneManager.GetTerrainPoint(this.fixHit.get_position().x, this.fixHit.get_position().z, this.FixTransform.get_position().y));
			}
		}

		public void SetAndFixPosition(Vector3 position, string logStr = null, int color = 301)
		{
			this.FixTransform.set_position(position);
			if (NavMesh.SamplePosition(this.FixTransform.get_position(), ref this.fixHit, 500f, -1))
			{
				this.FixTransform.set_position(MySceneManager.GetTerrainPoint(this.fixHit.get_position().x, this.fixHit.get_position().z, this.FixTransform.get_position().y));
			}
		}

		protected bool IsYChangeSuddenly(float beforeY, float afterY)
		{
			return Math.Abs(beforeY - afterY) > 1f;
		}

		public void StopAIMove()
		{
			this.StopAllMove();
		}

		public virtual bool MoveToSkillTarget(float skillReach)
		{
			if (this.GetEntity().AITarget == null)
			{
				return false;
			}
			if (!this.GetEntity().AITarget.Actor)
			{
				return false;
			}
			float num = XUtility.DistanceNoY(this.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position()) - (skillReach + XUtility.GetHitRadius(this.GetEntity().AITarget.Actor.FixTransform));
			if (num <= -0.05f)
			{
				this.StopMoveToPoint();
				Vector3 vector = new Vector3(this.GetEntity().AITarget.Actor.FixTransform.get_position().x - this.FixTransform.get_position().x, 0f, this.GetEntity().AITarget.Actor.FixTransform.get_position().z - this.FixTransform.get_position().z);
				this.ForceSetDirection(vector.get_normalized());
				this.ApplyMovingDirAsForward();
				return false;
			}
			if (!XUtility.StartsWith(this.CurActionStatus, "run") && !this.CanChangeActionTo("run", true, 0, false) && ActionStatusName.IsSkillAction(this.CurActionStatus) && !this.GetEntity().IsMoveCast && !this.IsUnderTermination)
			{
				return false;
			}
			Vector3 vector2 = new Vector3(this.GetEntity().AITarget.Actor.FixTransform.get_position().x - this.FixTransform.get_position().x, 0f, this.GetEntity().AITarget.Actor.FixTransform.get_position().z - this.FixTransform.get_position().z);
			Vector3 normalized = vector2.get_normalized();
			Vector3 aIMoveFixPoint = this.GetAIMoveFixPoint(this.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position(), normalized, num, XUtility.GetHitRadius(this.FixTransform));
			this.MoveToPoint(aIMoveFixPoint, 0f, null);
			this.GetEntity().AIToPoint = new XPoint
			{
				position = aIMoveFixPoint
			};
			return true;
		}

		public virtual bool MoveToSkillEdge(float skillReach)
		{
			if (this.GetEntity().AITarget == null)
			{
				return false;
			}
			if (!this.GetEntity().AITarget.Actor)
			{
				return false;
			}
			float num = XUtility.DistanceNoY(this.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position()) - (skillReach + XUtility.GetHitRadius(this.GetEntity().AITarget.Actor.FixTransform));
			if (num > skillReach)
			{
				this.StopMoveToPoint();
				return false;
			}
			if (this.CurActionStatus != "run" && !this.CanChangeActionTo("run", true, 0, false))
			{
				return false;
			}
			Vector3 vector = new Vector3(this.FixTransform.get_position().x - this.GetEntity().AITarget.Actor.FixTransform.get_position().x, 0f, this.FixTransform.get_position().z - this.GetEntity().AITarget.Actor.FixTransform.get_position().z);
			Vector3 normalized = vector.get_normalized();
			if (normalized == Vector3.get_zero())
			{
				normalized = (Vector3.get_zero() - this.FixTransform.get_position()).get_normalized();
			}
			if (normalized == Vector3.get_zero())
			{
				normalized = this.FixTransform.get_forward().get_normalized();
			}
			this.MoveToPoint(this.GetEntity().AITarget.Actor.FixTransform.get_position() + normalized * (skillReach + XUtility.GetHitRadius(this.GetEntity().AITarget.Actor.FixTransform)), 0f, null);
			return true;
		}

		protected Vector3 GetAIMoveFixPoint(Vector3 source, Vector3 target, Vector3 dir, float distance, float sourceRange)
		{
			Vector3 vector = source + dir * (distance + 0.05f);
			float num = XUtility.DistanceNoY(vector, target);
			Quaternion quaternion = default(Quaternion);
			quaternion.SetLookRotation(new Vector3(vector.x, 0f, vector.z), new Vector3(target.x, 0f, target.z));
			bool flag = true;
			int num2 = 0;
			Vector3 vector2 = vector;
			while (!this.CheckAIMoveFixPoint(vector2, sourceRange, flag) && num2 < 7)
			{
				if (flag)
				{
					num2++;
				}
				Vector3 vector3 = Quaternion.Euler(quaternion.get_eulerAngles().x, (!flag) ? (quaternion.get_eulerAngles().y - (float)(10 * num2)) : (quaternion.get_eulerAngles().y + (float)(10 * num2)), quaternion.get_eulerAngles().z) * Vector3.get_forward();
				vector2 = target + vector3 * num;
				flag = !flag;
			}
			if (num2 >= 7)
			{
				int num3 = new Random().Next(-60, 60);
				Vector3 vector4 = Quaternion.Euler(quaternion.get_eulerAngles().x, quaternion.get_eulerAngles().y + (float)num3, quaternion.get_eulerAngles().z) * Vector3.get_forward();
				vector2 = target + vector4 * num;
			}
			return vector2;
		}

		protected bool CheckAIMoveFixPoint(Vector3 postion, float range, bool isPlus)
		{
			List<EntityParent> values = EntityWorld.Instance.AllEntities.Values;
			for (int i = 0; i < values.get_Count(); i++)
			{
				if (values.get_Item(i).ID != this.GetEntity().ID && !values.get_Item(i).IsDead && values.get_Item(i).IsFighting && values.get_Item(i).Actor && values.get_Item(i).AIToPoint != null && XUtility.DistanceNoY(postion, values.get_Item(i).AIToPoint.position) <= range + XUtility.GetHitRadius(values.get_Item(i).Actor.FixTransform))
				{
					return false;
				}
			}
			return true;
		}

		public void MoveToNPC(Vector3 pos, float radius = 0f, Action callback = null)
		{
			int num = this.GetFloor(pos.y);
			if (num > this.Floor)
			{
				this.MoveToPoint(this.GetLiftPos(this.Floor, true), radius, delegate
				{
					this.FloorSetEndEvent = new KeyValuePair<int, Action>(this.Floor + 1, delegate
					{
						this.MoveToNPC(pos, radius, callback);
					});
				});
			}
			else if (num < this.Floor)
			{
				this.MoveToPoint(this.GetLiftPos(this.Floor, false), radius, delegate
				{
					this.FloorSetEndEvent = new KeyValuePair<int, Action>(this.Floor - 1, delegate
					{
						this.MoveToNPC(pos, radius, callback);
					});
				});
			}
			else
			{
				this.MoveToPoint(pos, radius, callback);
			}
		}

		public void StopMoveToNPC()
		{
			this.StopMoveToPoint();
		}

		protected Vector3 GetLiftPos(int calFloor, bool isUp)
		{
			if (isUp)
			{
				if (calFloor == 1)
				{
					return new Vector3(0f, 12.14f, 10f);
				}
			}
			else if (calFloor == 2)
			{
				return new Vector3(0f, 20.87f, 10f);
			}
			return Vector3.get_zero();
		}

		public void TurnToPos(Vector3 position)
		{
			if (this.GetEntity().IsDead || this.IsLockModelDir || ActionStatusName.IsBornAction(this.CurActionStatus))
			{
				return;
			}
			position.y = this.FixTransform.get_position().y;
			if (position == this.FixTransform.get_position())
			{
				return;
			}
			this.FixTransform.set_forward((position - this.FixTransform.get_position()).get_normalized());
		}

		protected void UpdateSight()
		{
			if (this.IsClearTargetPosition && !this.GetEntity().IsDead && !this.GetEntity().IsStatic && !this.GetEntity().IsDizzy && !this.GetEntity().IsWeak && !this.GetEntity().IsFixed && !this.GetEntity().IsHitMoving && !this.IsLockModelDir && !this.IsLockFaceForward && this.RotateSpeed != 0f && (XUtility.StartsWith(this.CurActionStatus, "idle") || XUtility.StartsWith(this.CurActionStatus, "turn")) && this.GetEntity().AITarget != null && this.GetEntity().AITarget.ID != this.GetEntity().ID && this.GetEntity().AITarget.Actor && (this.GetEntity().AITarget.Actor.FixTransform.get_position().x != this.FixTransform.get_position().x || this.GetEntity().AITarget.Actor.FixTransform.get_position().z != this.FixTransform.get_position().z))
			{
				this.TurnToForward = new Vector3(this.GetEntity().AITarget.Actor.FixTransform.get_position().x - this.FixTransform.get_position().x, 0f, this.GetEntity().AITarget.Actor.FixTransform.get_position().z - this.FixTransform.get_position().z);
				this.LeftAngle = Vector3.Angle(new Vector3(this.FixTransform.get_forward().x, 0f, this.FixTransform.get_forward().z), this.TurnToForward);
				if (this.LeftAngle <= this.FinishRotateAngle)
				{
					this.IsRotating = false;
				}
				else if (this.LeftAngle > this.StartRotateAngle)
				{
					this.IsRotating = true;
				}
				if (this.IsRotating)
				{
					if (this.RotateSpeed * Time.get_deltaTime() < this.LeftAngle)
					{
						this.TurnToSign = 1;
						if (Vector3.Dot(this.TurnToForward, this.FixTransform.get_right()) < 0f)
						{
							this.TurnToSign = -1;
						}
						if (this.TurnToSign == 1)
						{
							if (XUtility.StartsWith(this.CurActionStatus, "idle") && this.FixAnimator.HasState(0, Animator.StringToHash("turnleft")))
							{
								this.CastAction("turnleft", true, 1f, 0, 0, string.Empty);
							}
						}
						else if (XUtility.StartsWith(this.CurActionStatus, "idle") && this.FixAnimator.HasState(0, Animator.StringToHash("turnright")))
						{
							this.CastAction("turnright", true, 1f, 0, 0, string.Empty);
						}
						this.FixTransform.Rotate(0f, this.RotateSpeed * Time.get_deltaTime() * (float)this.TurnToSign, 0f);
					}
					else
					{
						this.FixTransform.set_forward(this.TurnToForward);
					}
				}
			}
			else
			{
				this.IsRotating = false;
			}
		}

		public void UpdateFloor()
		{
			this.Floor = this.GetFloor(this.FixTransform.get_position().y + 15f);
		}

		public void SetFloor(float height)
		{
			if (this.FixTransform != null)
			{
				this.FixTransform.set_position(MySceneManager.GetTerrainPoint(this.FixTransform.get_position().x, this.FixTransform.get_position().z, height));
				this.UpdateFloor();
			}
		}

		protected int GetFloor(float height)
		{
			return (int)(height / 30f);
		}

		public override void OnAudioEvent(AudioEventCmd cmd)
		{
			if (this.SkillTagFilter(cmd.args.get_stringParameter()))
			{
				this.Playe3DSound(cmd.args.get_intParameter());
			}
		}

		public override void OnAudio2DEvent(Audio2DEventCmd cmd)
		{
			if (this.SkillTagFilter(cmd.args.get_stringParameter()))
			{
				this.Playe2DSound(cmd.args.get_intParameter());
			}
		}

		protected void Playe3DSound(int soundID)
		{
			SoundManager.Instance.PlayPlayer(this.audioPlayer, soundID);
		}

		protected void Playe2DSound(int soundID)
		{
			SoundManager.PlayUI(soundID, false);
		}

		public override void OnFXEvent(FXEventCmd cmd)
		{
			AnimationEvent args = cmd.args;
			string stringParameter = args.get_stringParameter();
			int intParameter = args.get_intParameter();
			if (this.SkillTagFilter(stringParameter))
			{
				if (intParameter != 0)
				{
					CommandCenter.ExecuteCommand(this.FixTransform, new PlayActionFXCmd
					{
						fxID = intParameter,
						speed = this.PureActionSpeed,
						rate = this.RealActionSpeed,
						scale = DataReader<AvatarModel>.Get(this.resGUID).scale
					});
				}
				else
				{
					Debuger.Error("no fxID ", new object[0]);
				}
			}
		}

		public override void OnFrozeFrame(FrozeFrameCmd cmd)
		{
			this.FreezeFrame(cmd.count, cmd.rate, cmd.time, cmd.timeRateList, cmd.interval, cmd.callback);
		}

		public override void OnSingleTimer(SingleTimerCmd cmd)
		{
			using (List<AbstractColleague>.Enumerator enumerator = this.colleagues.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AbstractColleague current = enumerator.get_Current();
					if (current is ActorParent.AnimatorSpeedSystem)
					{
						(current as ActorParent.AnimatorSpeedSystem).DoSingleFroze(cmd);
						break;
					}
				}
			}
		}

		public override void OnPlayActionFX(PlayActionFXCmd cmd)
		{
			this.FixFXSystem.OnPlayActionFX(cmd);
		}

		public override void OnRemoveAllFX(RemoveAllFXCmd cmd)
		{
			this.FixFXSystem.OnRemoveAllFX(cmd);
		}

		public override void OnHitFX(HitFXCmd cmd)
		{
			this.FixFXSystem.OnHitFX(cmd);
		}

		public override void OnBulletFX(BulletFXCmd cmd)
		{
			this.FixFXSystem.OnBulletFX(cmd);
		}

		public override void OnChangeSpeedActionFX(ChangeSpeedActionFXCmd cmd)
		{
			this.FixFXSystem.OnChangeSpeedActionFX(cmd);
		}

		public override void OnPlayBuffFX(PlayBuffFXCmd cmd)
		{
			this.FixFXSystem.OnPlayBuffFX(cmd);
		}

		public override void OnRemoveBuffFX(RemoveBuffFXCmd cmd)
		{
			this.FixFXSystem.OnRemoveBuffFX(cmd);
		}

		public override void OnChangeAllFXLayer(ChangeAllFXLayerCmd cmd)
		{
			this.FixFXSystem.OnChangeAllFXLayer(cmd);
		}

		public override void OnRemoveActionFX(RemoveActionFXCmd cmd)
		{
			this.FixFXSystem.OnRemoveActionFX(cmd);
		}

		public override void OnNotifyPropChanged(NotifyPropChangedCmd cmd)
		{
			string propName = cmd.propName;
			if (propName != null)
			{
				if (ActorParent.<>f__switch$map10 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("AnimFactor", 0);
					dictionary.Add("ModelHeight", 1);
					dictionary.Add("FrozenActionSpeed", 2);
					ActorParent.<>f__switch$map10 = dictionary;
				}
				int num;
				if (ActorParent.<>f__switch$map10.TryGetValue(propName, ref num))
				{
					switch (num)
					{
					case 0:
						if (this.SkillTagFilter(cmd.propTag))
						{
							this.FrameActionSpeed = cmd.propValue;
						}
						break;
					case 1:
						this.ModelHeight = cmd.propValue;
						break;
					case 2:
						this.FrozenActionSpeed = cmd.propValue;
						break;
					}
				}
			}
		}

		public override void OnSkillEffect(SkillEffectCmd cmd)
		{
			if (!ActionStatusName.IsSkillAction(this.CurOutPutAction))
			{
				return;
			}
			switch (this.CurEffectFrameState)
			{
			case ActorParent.EffectFrameMode.Client:
				if (this.SkillTagFilter(cmd.args.get_stringParameter()))
				{
					this.GetEntity().GetSkillManager().ClientActionTriggerEffect(cmd.args.get_intParameter());
				}
				break;
			case ActorParent.EffectFrameMode.Server:
				if (this.SkillTagFilter(cmd.args.get_stringParameter()))
				{
					this.GetEntity().GetSkillManager().ServerActionTriggerEffect(cmd.args.get_intParameter());
				}
				break;
			case ActorParent.EffectFrameMode.Ignore:
				if (this.SkillTagFilter(cmd.args.get_stringParameter()))
				{
					this.GetEntity().GetSkillManager().ServerActionTriggerEffect(cmd.args.get_intParameter());
				}
				break;
			}
		}

		public override void OnTraverse(TraverseCmd cmd)
		{
			Component[] componentsInChildren = base.get_transform().GetComponentsInChildren(typeof(BaseUnit), true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].GetType().get_Name() == cmd.className)
				{
					(componentsInChildren[i] as BaseUnit).OnEnter();
				}
			}
		}

		protected void SetCollider()
		{
			if (base.GetComponent<CapsuleCollider>() != null)
			{
				Object.Destroy(base.GetComponent<CapsuleCollider>());
			}
			if (base.get_transform().FindChild("ColliderHelper") == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("ColliderHelper");
				gameObject.get_transform().set_parent(base.get_transform());
				gameObject.get_transform().set_localPosition(Vector3.get_zero());
				this.actorCollider = gameObject.AddMissingComponent<ActorCollider>();
			}
			else
			{
				this.actorCollider = base.get_transform().FindChild("ColliderHelper").get_gameObject().AddMissingComponent<ActorCollider>();
			}
			this.actorCollider.SetCollider(this, this.FixCharacterController, (float)DataReader<AvatarModel>.Get(this.resGUID).height_HP * 0.1f);
		}

		public void SetAllCollider(bool isEnable)
		{
			this.FixCharacterController.set_enabled(isEnable);
			this.ActorCollider.set_enabled(isEnable);
		}

		protected void SetCushion()
		{
			if (base.get_transform().FindChild("CushionHelper") == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("CushionHelper");
				gameObject.get_transform().set_parent(base.get_transform());
				gameObject.get_transform().set_localPosition(Vector3.get_zero());
				this.actorCushion = gameObject.AddMissingComponent<ActorCushion>();
			}
			else
			{
				this.actorCushion = base.get_transform().FindChild("CushionHelper").get_gameObject().AddMissingComponent<ActorCushion>();
			}
			this.actorCushion.SetCushion(this, this.FixCharacterController);
		}

		public void OnCushionEnter(ActorParent other)
		{
			if (this.CurActionStatus != "born")
			{
				this.FreezeXZ = true;
			}
		}

		public void OnCushionStay(ActorParent other)
		{
			if (this.CurActionStatus != "born")
			{
				this.FreezeXZ = true;
			}
		}

		public void OnCushionExit(ActorParent other)
		{
			if (this.CurActionStatus != "born")
			{
				this.FreezeXZ = false;
			}
		}

		public override void AddSkill(AddSkillCmd cmd)
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			string[] array = cmd.skillMessage.Split(new char[]
			{
				','
			});
			int num = 0;
			while (num + 2 < array.Length)
			{
				this.GetEntity().AddSkill(int.Parse(array[num]), int.Parse(array[num + 1]), int.Parse(array[num + 2]), null);
				num += 3;
			}
		}

		public override void RemoveSkill(RemoveSkillCmd cmd)
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			string[] array = cmd.skillMessage.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.GetEntity().RemoveSkill(int.Parse(array[i]));
			}
		}

		public void CancelSkillTrustee()
		{
			this.ChangeAction("idle", true, true, 1f, 0, 0, string.Empty);
		}

		protected virtual void AssaultSuccess()
		{
		}

		public void EndAssault()
		{
			this.GetEntity().IsAssault = false;
		}

		public void CancelAssault()
		{
			this.assaultTargetID = 0L;
			this.assaultEndSkillID = 0;
			this.assaultEndSkillDistance = 0f;
			this.assaultEndPos = Vector3.get_zero();
			this.assaultSpeed = 0f;
			this.assaultTime = 0f;
			this.ChangeAction("idle", true, true, 1f, 0, 0, string.Empty);
		}

		public void PlayHitFx(Transform casterTransform, int fxID, float scale, List<int> offsets)
		{
			CommandCenter.ExecuteCommand(this.FixTransform, new HitFXCmd
			{
				fxID = fxID,
				caster = casterTransform,
				scale = scale,
				offsets = offsets
			});
		}

		public void PlayParryFx()
		{
			DateTime now = DateTime.get_Now();
			if ((now - this.lastPlayParryFxTime).get_TotalSeconds() < 1.0)
			{
				return;
			}
			this.lastPlayParryFxTime = now;
			FXManager.Instance.PlayFX(94, this.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		}

		public string GetHitAction(string hitAction)
		{
			if (this.CurActionStatus != "float2" && hitAction == "float2" && (Random.get_value() > this.FloatRate || !this.HasAction("float2")))
			{
				hitAction = "float";
			}
			if (this.CurActionStatus != "float" && hitAction == "float" && (Random.get_value() > this.FloatRate || !this.HasAction("float")))
			{
				hitAction = "hit";
			}
			return hitAction;
		}

		public bool ClientPlayHit(string hitAction, int hitStraightTime, int hitPriority, bool isCheckHitMoving = true)
		{
			if (isCheckHitMoving && this.GetEntity().IsHitMoving)
			{
				return false;
			}
			if (!this.HasAction(hitAction))
			{
				return false;
			}
			if (hitPriority != 0)
			{
				this.ActionPriorityTable[hitAction] = hitPriority;
			}
			else
			{
				this.ActionPriorityTable[hitAction] = this.GetOriginalPriority(hitAction);
			}
			this.CastAction(hitAction, isCheckHitMoving, 1f, 0, 0, string.Empty);
			this.straightTime = (uint)hitStraightTime;
			return true;
		}

		public bool ServerPlayHit(string hitAction, int hitStraightTime, int hitPriority, bool isForcePlay = true)
		{
			if (!this.HasAction(hitAction))
			{
				return false;
			}
			if (hitPriority != 0)
			{
				this.ActionPriorityTable[hitAction] = hitPriority;
			}
			else
			{
				this.ActionPriorityTable[hitAction] = this.GetOriginalPriority(hitAction);
			}
			if (isForcePlay)
			{
				this.ChangeAction(hitAction, true, true, 1f, 0, 0, string.Empty);
			}
			else
			{
				this.CastAction(hitAction, true, 1f, 0, 0, string.Empty);
			}
			this.straightTime = (uint)hitStraightTime;
			return true;
		}

		public void ClientPlayHitMove(string hitAction, Vector3 hitMoveDir, float hitMoveDistance, float hitMoveTime, int hitStraightTime, int hitPriority, Action<Vector3, Vector3> callback = null)
		{
			if (this.ClientPlayHit(hitAction, hitStraightTime, hitPriority, false) && this.CurActionStatus == hitAction)
			{
				if (!this.IsLockModelDir && hitMoveDir != Vector3.get_zero())
				{
					this.ForceSetDirection(hitMoveDir);
					this.ApplyMovingDirAsForward();
				}
				if (this.IsLockModelDir || hitMoveTime == 0f || hitMoveDistance == 0f || this.GetEntity().IsFixed || DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).hitMove == 0f)
				{
					if (callback != null)
					{
						callback.Invoke(this.FixTransform.get_position(), this.FixTransform.get_forward());
					}
				}
				else
				{
					if (!this.GetEntity().IsHitMoving)
					{
						this.GetEntity().IsHitMoving = true;
					}
					bool flag = XUtility.StartsWith(hitAction, "float");
					if (this.GetEntity().IsSuspended != flag)
					{
						this.GetEntity().IsSuspended = flag;
					}
					this.hitMoveCountDown = hitMoveTime;
					this.hitMoveSpeed = hitMoveDistance / hitMoveTime;
					this.hitMoveCallBack = callback;
				}
			}
			else if (callback != null)
			{
				callback.Invoke(this.FixTransform.get_position(), this.FixTransform.get_forward());
			}
		}

		public void ServerPlayHitMove(string hitAction, Vector3 hitMoveDir, float hitMoveDistance, float hitMoveTime, int hitStraightTime, int hitPriority, Action<Vector3, Vector3> callback = null)
		{
			if (this.ServerPlayHit(hitAction, hitStraightTime, hitPriority, true))
			{
				if (!this.IsLockModelDir && hitMoveDir != Vector3.get_zero())
				{
					this.ForceSetDirection(hitMoveDir);
					this.ApplyMovingDirAsForward();
				}
				if (this.IsLockModelDir || hitMoveTime == 0f || hitMoveDistance == 0f || DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).hitMove == 0f)
				{
					if (callback != null)
					{
						callback.Invoke(this.FixTransform.get_position(), this.FixTransform.get_forward());
					}
				}
				else
				{
					this.hitMoveCountDown = hitMoveTime;
					this.hitMoveSpeed = hitMoveDistance / hitMoveTime;
					this.hitMoveCallBack = callback;
				}
			}
			else
			{
				this.GetEntity().IsHitMoving = false;
				this.GetEntity().IsSuspended = false;
				if (callback != null)
				{
					callback.Invoke(this.FixTransform.get_position(), this.FixTransform.get_forward());
				}
			}
		}

		public void EndHitMove()
		{
			this.GetEntity().IsHitMoving = false;
			this.GetEntity().IsSuspended = false;
			if (this.hitMoveCallBack != null)
			{
				this.hitMoveCallBack.Invoke(this.FixTransform.get_position(), this.FixTransform.get_forward());
			}
		}

		public void CancelHitMove()
		{
			this.hitMoveSpeed = 0f;
			this.hitMoveCountDown = 0f;
			this.hitMoveCallBack = null;
			this.ChangeAction("idle", true, true, 1f, 0, 0, string.Empty);
		}

		public void PlayHitSound(int soundID)
		{
			this.Playe3DSound(soundID);
		}

		protected bool SkillTagFilter(string tag)
		{
			return string.IsNullOrEmpty(tag) || this.ActionSkillTag.Equals(tag);
		}

		public virtual bool BoardPlatform(Platform platform)
		{
			return true;
		}

		public virtual bool LeavePlatform(Platform platform)
		{
			return true;
		}

		public virtual void UpdatePlatform(Vector3 platformDelta, bool isEqual = false)
		{
			if (isEqual)
			{
				base.get_transform().set_position(platformDelta);
			}
			else
			{
				base.get_transform().Translate(platformDelta, 0);
			}
		}

		public virtual void StartPlatformTrip()
		{
		}

		public virtual void FinishPlatformTrip()
		{
		}

		public virtual void EnterPlatformArea()
		{
		}

		public virtual void ExitPlatformArea()
		{
		}

		public Vector3 GetAnimationFootPos()
		{
			if (this.foot_y_node != null)
			{
				return this.foot_y_node.get_position();
			}
			if (this.FixTransform != null)
			{
				return this.FixTransform.get_position();
			}
			return Vector3.get_zero();
		}

		public override void OnJumpFollow(JumpFollowCmd cmd)
		{
			this.isJumpFollow = (cmd.jumpFollow == 0);
		}

		public override void OnPointBPriority(PointBPriorityCmd cmd)
		{
			FollowCamera.instance.SetPointBPriority(cmd.number - 1);
		}

		public override void OnCameraPosition(CameraPositionCmd cmd)
		{
			FollowCamera.instance.SetCameraPosition(cmd.distance, cmd.height);
		}

		public virtual void UpdateLayer()
		{
			if (this.IsDisplayingByLayer)
			{
				return;
			}
			if (this.GetEntity() == null)
			{
				return;
			}
			Vector3 position = this.FixTransform.get_position();
			if (this.IsOpenLayerTest && InstanceManager.IsServerBattle)
			{
				LayerSystem.SetGameObjectLayer(this.FixGameObject, "LayerZero", 2);
			}
			else
			{
				LayerSystem.SetGameObjectLayer(this.FixGameObject, LayerSystem.GetGameObjectLayerName(this.GetEntity().Camp, this.GetEntity().LayerEntityNumber, this.LayerState), 2);
			}
			if (this.IsYChangeSuddenly(position.y, this.FixTransform.get_position().y))
			{
				this.SetAndFixPosition(position, null, 301);
			}
		}

		public void UpdateMoveSpeed()
		{
			this.RealMoveSpeed = this.LogicMoveSpeed * this.StateMoveFactor * this.ActionMoveFactor;
			this.PureMoveSpeed = this.LogicMoveSpeed;
		}

		public virtual void UpdateActionSpeed()
		{
			this.RealActionSpeed = this.LogicActionSpeed * this.StateActionFactor * this.FrameActionSpeed * this.FrozenActionSpeed * this.StraightActionSpeed * this.TempActionSpeed;
			this.PureActionSpeed = this.LogicDefaultActionSpeed * this.FrameActionSpeed * this.TempActionSpeed;
		}

		protected void FreezeFrame(int count, float freezeRate, int defaultFreezeTime, List<float> freezeTimeRateList, int freezeInterval, Action callback)
		{
			if (count == 0)
			{
				return;
			}
			if (defaultFreezeTime == 0)
			{
				return;
			}
			if (freezeTimeRateList == null)
			{
				return;
			}
			if (freezeTimeRateList.get_Count() == 0)
			{
				return;
			}
			if (this.freezeTimer.get_Count() > 0)
			{
				return;
			}
			if (this.thawTimer.get_Count() > 0)
			{
				return;
			}
			int num = (int)Math.Ceiling((double)float.Parse(DataReader<GlobalParams>.Get("frame_times_i").value));
			int num2 = Mathf.Min(new int[]
			{
				count,
				num,
				freezeTimeRateList.get_Count()
			});
			List<float> finalFrozeTimeRateList = freezeTimeRateList.GetRange(0, num2);
			NotifyPropChangedCmd freeazeCommand = new NotifyPropChangedCmd
			{
				propName = "FrozenActionSpeed",
				propValue = freezeRate
			};
			NotifyPropChangedCmd thawCommand = new NotifyPropChangedCmd
			{
				propName = "FrozenActionSpeed",
				propValue = 1f
			};
			if (freezeInterval == 0)
			{
				this.freezeTimer.Add(TimerHeap.AddTimer(0u, 0, delegate
				{
					CommandCenter.ExecuteCommand(this.FixTransform, freeazeCommand);
					this.thawTimer.Add(TimerHeap.AddTimer((uint)((float)defaultFreezeTime * finalFrozeTimeRateList.get_Item(0)), 0, delegate
					{
						CommandCenter.ExecuteCommand(this.FixTransform, thawCommand);
						this.freezeTimer.Clear();
						this.thawTimer.Clear();
						if (callback != null)
						{
							callback.Invoke();
						}
					}));
				}));
			}
			else
			{
				uint num3 = 0u;
				for (int i = finalFrozeTimeRateList.get_Count() - 1; i >= 0; i--)
				{
					int index = i;
					uint beginTime = num3;
					uint freezeTime = (uint)((float)defaultFreezeTime * finalFrozeTimeRateList.get_Item(index));
					this.freezeTimer.Add(TimerHeap.AddTimer(beginTime, 0, delegate
					{
						CommandCenter.ExecuteCommand(this.FixTransform, freeazeCommand);
						this.thawTimer.Add(TimerHeap.AddTimer(beginTime + freezeTime, 0, delegate
						{
							CommandCenter.ExecuteCommand(this.FixTransform, thawCommand);
							if (index == 0)
							{
								this.freezeTimer.Clear();
								this.thawTimer.Clear();
								if (callback != null)
								{
									callback.Invoke();
								}
							}
						}));
					}));
					num3 += (uint)((ulong)freezeTime + (ulong)((long)freezeInterval));
				}
			}
		}

		public void ClearFreezeFrame()
		{
			this.FrozenActionSpeed = 1f;
			for (int i = 0; i < this.freezeTimer.get_Count(); i++)
			{
				TimerHeap.DelTimer(this.freezeTimer.get_Item(i));
			}
			for (int j = 0; j < this.thawTimer.get_Count(); j++)
			{
				TimerHeap.DelTimer(this.thawTimer.get_Item(j));
			}
		}

		public virtual void GotDrop()
		{
		}

		public virtual void OnSelected(bool isSelected)
		{
			if (isSelected)
			{
				this.chosenFx = FXManager.Instance.PlayFX(6999, base.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, DataReader<AvatarModel>.Get(this.GetEntity().FixModelID).fxScale, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
			else if (this.chosenFx != 0)
			{
				FXManager.Instance.DeleteFX(this.chosenFx);
				this.chosenFx = 0;
			}
		}

		public FXClassification GetFXClassification()
		{
			if (!EntityWorld.Instance.EntSelf.IsInBattle)
			{
				return FXClassification.Normal;
			}
			if (this is ActorSelf)
			{
				return FXClassification.Normal;
			}
			if (this is ActorPlayer)
			{
				return (!this.GetEntity().IsPlayerMate) ? FXClassification.LowLod : FXClassification.NoPlay;
			}
			if (!(this is ActorPet))
			{
				return FXClassification.Normal;
			}
			if (!this.GetEntity().IsPlayerMate)
			{
				return FXClassification.LowLod;
			}
			if (this.GetEntity().OwnerID == EntityWorld.Instance.EntSelf.ID)
			{
				return FXClassification.Normal;
			}
			return FXClassification.NoPlay;
		}

		protected override void OnCreate()
		{
			base.addColleague(new ActorParent.AnimatorSpeedSystem());
			base.addColleague(new ActorParent.ActionSystem());
			this.fixFXSystem = new ActorParent.FXSystem();
			base.addColleague(this.fixFXSystem);
			base.OnCreate();
		}

		protected override void OnDestroy()
		{
			base.deleteColleague(typeof(ActorParent.AnimatorSpeedSystem));
			base.deleteColleague(typeof(ActorParent.ActionSystem));
			base.deleteColleague(typeof(ActorParent.FXSystem));
			this.fixFXSystem = null;
			base.OnDestroy();
		}
	}
}
