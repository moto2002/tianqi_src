using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCBehavior : NPCBehavior
{
	public class TaskNPCState
	{
		public enum TaskNPCStateType
		{
			None,
			Stand,
			Follow,
			NavToPos,
			Die
		}

		public TaskNPCBehavior.TaskNPCState.TaskNPCStateType state;

		public Vector3 Position;

		public Vector3 Direction;
	}

	public static readonly string OnEnterNPC = "TaskNPCBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "TaskNPCBehavior.OnExitNPC";

	public static readonly string OnSeleteNPC = "TaskNPCBehavior.OnSeleteNPC";

	public static readonly string OnNPCDieEnd = "TaskNPCBehavior.OnNPCDieEnd";

	public static readonly float AppointedMoveSpeed = 600f;

	public static readonly float AppointedDefaultActionSpeed = 1f;

	public static readonly float AppointedRunActionSpeed = 1f;

	public static readonly float FollowSelfStartDistance = 2.5f;

	public static readonly float FollowSelfStopDistance = 1.5f;

	public static readonly float NavToPointStopDistance = 0.2f;

	protected int npcDataID;

	protected TaskNPCBehavior.TaskNPCState.TaskNPCStateType npcState;

	protected Vector3 npcPosition;

	protected Vector3 npcDirection;

	protected NavMeshAgent navMeshAgent;

	protected Collider notTriggerCollider;

	protected Collider triggerCollider;

	protected float logicMoveSpeed;

	protected float realMoveSpeed;

	protected float logicDefaultActionSpeed = 1f;

	protected float logicRunActionSpeed = 1f;

	protected bool isFollowingSelf;

	protected bool isNavToPoint;

	public override bool EnableUpdate
	{
		get
		{
			return true;
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

	protected float RealMoveSpeed
	{
		get
		{
			return this.realMoveSpeed;
		}
		set
		{
			this.realMoveSpeed = value;
		}
	}

	protected float LogicDefaultActionSpeed
	{
		get
		{
			return this.logicDefaultActionSpeed;
		}
		set
		{
			this.logicDefaultActionSpeed = value;
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
			this.logicRunActionSpeed = value;
			this.UpdateActionSpeed();
		}
	}

	protected float LogicActionSpeed
	{
		get
		{
			return (!XUtility.StartsWith(base.CurActionStatus, "run")) ? this.LogicDefaultActionSpeed : this.LogicRunActionSpeed;
		}
	}

	protected bool IsSelfExist
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Actor;
		}
	}

	protected bool IsFollowingSelf
	{
		get
		{
			return this.isFollowingSelf;
		}
		set
		{
			this.isFollowingSelf = value;
		}
	}

	protected bool IsNavToPoint
	{
		get
		{
			return this.isNavToPoint;
		}
		set
		{
			this.isNavToPoint = value;
		}
	}

	public TaskNPCBehavior(int theNPCDataID, TaskNPCBehavior.TaskNPCState theTaskNPCState)
	{
		this.npcDataID = theNPCDataID;
		this.npcState = theTaskNPCState.state;
		this.npcPosition = theTaskNPCState.Position;
		this.npcDirection = theTaskNPCState.Direction;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		NPC nPC = DataReader<NPC>.Get(this.npcDataID);
		if (nPC == null)
		{
			return;
		}
		if (DataReader<AvatarModel>.Get(modelID) == null)
		{
			return;
		}
		this.id = theID;
		this.SetTransform(root);
		this.SetModel(root, modelID);
		this.SetCollider(root, nPC.touchRange, nPC.triggeredRange);
		this.SetNavMeshAgent(root);
		if (!string.IsNullOrEmpty(nPC.action))
		{
			base.DefaultIdleActionStatus = nPC.action;
		}
		this.LogicMoveSpeed = ((EntityWorld.Instance.EntSelf != null) ? ((float)EntityWorld.Instance.EntSelf.MoveSpeed) : TaskNPCBehavior.AppointedMoveSpeed);
		this.LogicDefaultActionSpeed = TaskNPCBehavior.AppointedDefaultActionSpeed;
		this.LogicRunActionSpeed = TaskNPCBehavior.AppointedRunActionSpeed;
		this.ApplyDefaultState();
	}

	protected void SetTransform(Transform root)
	{
		this.transform = root;
	}

	protected void SetModel(Transform root, int modelID)
	{
		base.GetAsyncModel(root, modelID, delegate
		{
			NPC nPC = DataReader<NPC>.Get(this.npcDataID);
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
			this.animator = root.GetComponentInChildren<Animator>();
			if (this.animator)
			{
				AssetManager.AssetOfControllerManager.SetController(this.animator, modelID, false);
				this.CastAction(this.DefaultIdleActionStatus);
				this.ChangeAnimationSpeed();
			}
			this.SetCharacterController(root);
			BillboardManager.Instance.AddBillboardsInfo(31, root, (float)avatarModel.height_HP, (long)this.id, false, true, true);
			HeadInfoManager.Instance.SetName(31, (long)this.id, GameDataUtils.GetChineseContent(nPC.name, false));
			ShadowController.ShowShadow((long)this.id, root, false, modelID);
			ActorVisibleManager.Instance.Add((long)this.id, root, 31, 0L);
			this.ShowShopInfo(nPC, root, avatarModel.height_HP);
		});
	}

	protected void ShowShopInfo(NPC npcData, Transform root, int height)
	{
		if (npcData != null && npcData.function != null && npcData.function.get_Count() > 0 && npcData.function.get_Item(0) == TransactionNPCManager.Instance.SystemId && BubbleDialogueManager.Instance.AddBubbleDialogueLimit(root, (float)height, (long)this.id, 0))
		{
			BubbleDialogueManager.Instance.SetContentsByShopNpc((long)this.id, npcData.function.get_Item(1), 9999999);
		}
	}

	protected void SetCharacterController(Transform root)
	{
		CharacterController[] componentsInChildren = root.GetComponentsInChildren<CharacterController>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_enabled(false);
			}
		}
	}

	protected void SetCollider(Transform root, List<int> touchRange, List<int> triggeredRange)
	{
		GameObject gameObject = new GameObject("notTriggerAgent");
		UGUITools.ResetTransform(gameObject.get_transform(), root);
		this.notTriggerCollider = base.CreateCollider(gameObject, touchRange);
		if (this.notTriggerCollider)
		{
			this.notTriggerCollider.set_isTrigger(false);
		}
		GameObject gameObject2 = new GameObject("triggerAgent");
		UGUITools.ResetTransform(gameObject2.get_transform(), root);
		gameObject2.AddComponent<NPCTriggerReceiver>();
		this.triggerCollider = base.CreateCollider(gameObject2, triggeredRange);
		if (this.triggerCollider)
		{
			this.triggerCollider.set_isTrigger(true);
		}
	}

	protected void EnableCollider()
	{
		if (this.notTriggerCollider && !this.notTriggerCollider.get_enabled())
		{
			this.notTriggerCollider.set_enabled(true);
		}
		if (this.triggerCollider && !this.triggerCollider.get_enabled())
		{
			this.triggerCollider.set_enabled(MySceneManager.Instance.IsSceneExist);
		}
	}

	protected void DisableCollider()
	{
		if (this.notTriggerCollider && this.notTriggerCollider.get_enabled())
		{
			this.notTriggerCollider.set_enabled(false);
		}
		if (this.triggerCollider && this.triggerCollider.get_enabled())
		{
			this.triggerCollider.set_enabled(false);
			this.OnExit();
		}
	}

	protected void SetNavMeshAgent(Transform root)
	{
		this.navMeshAgent = root.get_gameObject().AddUniqueComponent<NavMeshAgent>();
		this.navMeshAgent.set_autoBraking(false);
		this.navMeshAgent.set_autoTraverseOffMeshLink(false);
		this.navMeshAgent.set_radius(0f);
		this.navMeshAgent.Warp(root.get_position());
	}

	public override void ApplyDefaultState()
	{
		TaskNPCBehavior.TaskNPCState.TaskNPCStateType taskNPCStateType = this.npcState;
		if (taskNPCStateType != TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Follow)
		{
			this.SetDefaultFixStand();
		}
		else
		{
			this.SetDefaultFollowSelf();
		}
		this.UpdateCollider();
	}

	protected void SetDefaultFixStand()
	{
		this.transform.set_position(MySceneManager.GetTerrainPoint(this.npcPosition.x, this.npcPosition.z, this.npcPosition.y));
		this.navMeshAgent.Warp(this.transform.get_position());
		this.transform.set_forward(this.npcDirection);
		base.CastAction(base.DefaultIdleActionStatus);
	}

	protected void SetDefaultFollowSelf()
	{
		XPoint arg_66_0 = new XPoint
		{
			position = EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position(),
			rotation = EntityWorld.Instance.EntSelf.Actor.FixTransform.get_rotation()
		};
		List<int> list = new List<int>();
		list.Add(0);
		list.Add((int)(-TaskNPCBehavior.FollowSelfStopDistance * 100f));
		XPoint xPoint = arg_66_0.ApplyOffset(list);
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(xPoint.position, ref navMeshHit, 500f, -1))
		{
			this.transform.set_position(navMeshHit.get_position());
			this.navMeshAgent.Warp(this.transform.get_position());
		}
		this.transform.set_forward(new Vector3(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().x - this.transform.get_position().x, 0f, EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().z - this.transform.get_position().z));
	}

	public override void Release()
	{
		this.npcDataID = 0;
		this.npcState = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.None;
		this.animator = null;
		this.navMeshAgent = null;
		this.notTriggerCollider = null;
		this.triggerCollider = null;
		base.Release();
	}

	public override void Update()
	{
		this.ApplyState();
	}

	public override void Born()
	{
	}

	public override void Die()
	{
		if (this.animator.HasAction("die"))
		{
			base.CastAction("die");
		}
		else
		{
			this.DeadAnimationEnd();
		}
	}

	public override void OnEnter()
	{
		if (this.npcDataID != 0)
		{
			EventDispatcher.Broadcast<int>(TaskNPCBehavior.OnEnterNPC, this.npcDataID);
		}
	}

	public override void OnExit()
	{
		if (this.npcDataID != 0)
		{
			EventDispatcher.Broadcast<int>(TaskNPCBehavior.OnExitNPC, this.npcDataID);
		}
	}

	public override void OnSeleted()
	{
		if (this.npcDataID != 0)
		{
			EventDispatcher.Broadcast<int>(TaskNPCBehavior.OnSeleteNPC, this.npcDataID);
		}
	}

	public override int GetState()
	{
		return (int)this.npcState;
	}

	public override void UpdateState(object state)
	{
		if (this.npcState == TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Die)
		{
			return;
		}
		TaskNPCBehavior.TaskNPCState taskNPCState = state as TaskNPCBehavior.TaskNPCState;
		if (taskNPCState == null)
		{
			return;
		}
		if (this.npcState == taskNPCState.state && (this.npcPosition.x == taskNPCState.Position.x & this.npcPosition.z == taskNPCState.Position.z) && this.npcDirection.x == taskNPCState.Direction.x && this.npcDirection.z == taskNPCState.Direction.z)
		{
			return;
		}
		this.npcState = taskNPCState.state;
		this.npcPosition = taskNPCState.Position;
		this.npcDirection = new Vector3(taskNPCState.Direction.x, 0f, taskNPCState.Direction.z);
		this.UpdateCollider();
	}

	protected void ApplyState()
	{
		switch (this.npcState)
		{
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Stand:
			this.CheckFixStand();
			break;
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Follow:
			this.CheckFollowSelf();
			break;
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.NavToPos:
			this.CheckNavToPoint();
			break;
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Die:
			this.CheckDie();
			break;
		}
	}

	protected void UpdateCollider()
	{
		switch (this.npcState)
		{
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Stand:
			this.EnableCollider();
			break;
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Follow:
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.NavToPos:
		case TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Die:
			this.DisableCollider();
			break;
		}
	}

	protected void StopNavAgent()
	{
		if (!this.navMeshAgent.get_isOnNavMesh())
		{
			return;
		}
		this.navMeshAgent.Stop();
		this.navMeshAgent.ResetPath();
		this.navMeshAgent.set_speed(0f);
		this.navMeshAgent.set_updatePosition(false);
		this.navMeshAgent.set_updateRotation(false);
		this.navMeshAgent.set_enabled(false);
		this.LogicDefaultActionSpeed = TaskNPCBehavior.AppointedDefaultActionSpeed;
	}

	protected void UpdateMoveSpeed()
	{
		this.RealMoveSpeed = this.LogicMoveSpeed;
	}

	protected override void UpdateActionSpeed()
	{
		base.RealActionSpeed = this.LogicActionSpeed * base.FrameActionSpeed;
	}

	protected override void DeadAnimationEnd()
	{
		if (this.npcDataID != 0)
		{
			EventDispatcher.Broadcast<int>(TaskNPCBehavior.OnNPCDieEnd, this.npcDataID);
		}
	}

	protected void CheckFixStand()
	{
		float num = XUtility.DistanceNoY(this.transform.get_position(), this.npcPosition);
		if (num > TaskNPCBehavior.NavToPointStopDistance)
		{
			this.transform.set_position(MySceneManager.GetTerrainPoint(this.npcPosition.x, this.npcPosition.z, this.npcPosition.y));
			this.navMeshAgent.Warp(this.transform.get_position());
		}
		if (this.transform.get_forward() != this.npcDirection)
		{
			this.transform.set_forward(this.npcDirection);
		}
		if (this.IsFollowingSelf)
		{
			this.IsFollowingSelf = false;
		}
		if (this.IsNavToPoint)
		{
			this.IsNavToPoint = false;
		}
		this.StopNavAgent();
		base.CastAction(base.DefaultIdleActionStatus);
		this.UpdateState(new TaskNPCBehavior.TaskNPCState
		{
			state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.None,
			Position = this.transform.get_position(),
			Direction = this.transform.get_forward()
		});
	}

	protected void CheckFollowSelf()
	{
		if (!this.IsSelfExist)
		{
			return;
		}
		if (!MySceneManager.Instance.IsSceneExist)
		{
			return;
		}
		float num = XUtility.DistanceNoY(this.transform.get_position(), EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position());
		if (num <= TaskNPCBehavior.FollowSelfStopDistance)
		{
			this.StopFollowSelf();
		}
		else if (num > TaskNPCBehavior.FollowSelfStartDistance)
		{
			this.FollowSelf();
		}
	}

	protected void StopFollowSelf()
	{
		if (!this.IsFollowingSelf)
		{
			return;
		}
		this.IsFollowingSelf = false;
		this.StopNavAgent();
		base.CastAction(base.DefaultIdleActionStatus);
		this.LookAt(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position());
	}

	protected void LookAt(Vector3 targetPos)
	{
		this.transform.set_forward(new Vector3(targetPos.x - this.transform.get_position().x, 0f, targetPos.z - this.transform.get_position().z));
	}

	protected void FollowSelf()
	{
		if (this.IsFollowingSelf)
		{
			this.navMeshAgent.SetDestination(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position());
			this.navMeshAgent.Resume();
			if (!this.navMeshAgent.get_hasPath())
			{
				this.StopFollowSelf();
			}
		}
		else
		{
			this.IsFollowingSelf = true;
			this.LogicMoveSpeed = ((EntityWorld.Instance.EntSelf != null) ? ((float)EntityWorld.Instance.EntSelf.MoveSpeed) : TaskNPCBehavior.AppointedMoveSpeed);
			this.LogicRunActionSpeed = TaskNPCBehavior.AppointedRunActionSpeed;
			this.navMeshAgent.set_enabled(true);
			this.navMeshAgent.Warp(this.transform.get_position());
			this.navMeshAgent.set_speed(this.RealMoveSpeed);
			this.navMeshAgent.set_updatePosition(true);
			this.navMeshAgent.set_updateRotation(true);
			this.navMeshAgent.set_angularSpeed(1080f);
			base.CastAction("run");
			this.navMeshAgent.SetDestination(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position());
			this.navMeshAgent.Resume();
		}
	}

	protected void CheckNavToPoint()
	{
		float num = XUtility.DistanceNoY(this.transform.get_position(), this.npcPosition);
		if (num <= TaskNPCBehavior.NavToPointStopDistance)
		{
			this.StopNavToPoint();
		}
		else
		{
			this.NavToPoint();
		}
	}

	protected void NavToPoint()
	{
		if (this.IsNavToPoint)
		{
			this.navMeshAgent.SetDestination(this.npcPosition);
			this.navMeshAgent.Resume();
			if (!this.navMeshAgent.get_hasPath())
			{
				this.StopNavToPoint();
			}
		}
		else
		{
			this.IsNavToPoint = true;
			this.LogicMoveSpeed = ((EntityWorld.Instance.EntSelf != null) ? ((float)EntityWorld.Instance.EntSelf.MoveSpeed) : TaskNPCBehavior.AppointedMoveSpeed);
			this.LogicRunActionSpeed = TaskNPCBehavior.AppointedRunActionSpeed;
			this.navMeshAgent.set_enabled(true);
			this.navMeshAgent.Warp(this.transform.get_position());
			this.navMeshAgent.set_speed(this.RealMoveSpeed);
			this.navMeshAgent.set_updatePosition(true);
			this.navMeshAgent.set_updateRotation(true);
			this.navMeshAgent.set_angularSpeed(1080f);
			base.CastAction("run");
			this.navMeshAgent.SetDestination(this.npcPosition);
			this.navMeshAgent.Resume();
		}
	}

	protected void StopNavToPoint()
	{
		if (!this.IsNavToPoint)
		{
			return;
		}
		this.IsNavToPoint = false;
		this.StopNavAgent();
		this.UpdateState(new TaskNPCBehavior.TaskNPCState
		{
			state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Stand,
			Position = this.transform.get_position(),
			Direction = this.transform.get_forward()
		});
	}

	protected void CheckDie()
	{
		this.UpdateState(new TaskNPCBehavior.TaskNPCState
		{
			state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.None,
			Position = this.transform.get_position(),
			Direction = this.transform.get_forward()
		});
		this.Die();
	}

	public override void UpdateHeadInfoState()
	{
		NPC nPC = DataReader<NPC>.Get(this.npcDataID);
		if (nPC != null && nPC.function != null && nPC.function.get_Count() > 0 && nPC.function.get_Item(0) == TransactionNPCManager.Instance.SystemId)
		{
			BubbleDialogueManager.Instance.SetContentsByShopNpc((long)this.id, nPC.function.get_Item(1), 9999999);
		}
	}
}
