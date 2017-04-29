using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;
using XEngineCommand;

public class ActorCityPet : Actor, IActorVisible
{
	public static readonly float StartDistance = 2.5f;

	public static readonly float StopDistance = 1.5f;

	protected EntityCityPet entity;

	public Transform fixTransform;

	public GameObject fixGameObject;

	public Animator fixAnimator;

	public NavMeshAgent fixNavAgent;

	protected float logicMoveSpeed;

	protected float realMoveSpeed;

	protected float logicDefaultActionSpeed = 1f;

	protected float logicRunActionSpeed = 1f;

	protected float realActionSpeed;

	protected string curActionStatus = string.Empty;

	protected string curOutPutAction = string.Empty;

	protected bool isFollowing;

	public EntityCityPet Entity
	{
		get
		{
			return this.entity;
		}
		set
		{
			this.entity = value;
		}
	}

	protected bool IsOwnerDataExist
	{
		get
		{
			return this.Entity != null && this.Entity.Owner != null && this.Entity.Owner.Actor;
		}
	}

	public Transform FixTransform
	{
		get
		{
			return this.fixTransform;
		}
	}

	public GameObject FixGameObject
	{
		get
		{
			return this.fixGameObject;
		}
	}

	public Animator FixAnimator
	{
		get
		{
			return this.fixAnimator;
		}
	}

	public NavMeshAgent FixNavAgent
	{
		get
		{
			return this.fixNavAgent;
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
			return (!XUtility.StartsWith(this.CurActionStatus, "run")) ? this.LogicDefaultActionSpeed : this.LogicRunActionSpeed;
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

	protected string CurActionStatus
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

	protected bool IsFollowing
	{
		get
		{
			return this.isFollowing;
		}
		set
		{
			this.isFollowing = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		this.SetTransform();
		this.SetGameObject();
		this.SetAnimator();
		this.SetCharacterController();
		this.SetNavMeshAgent();
	}

	protected override void Start()
	{
		base.Start();
	}

	protected void Update()
	{
		if (this.IsOwnerDataExist)
		{
			if (MySceneManager.Instance.IsSceneExist)
			{
				this.CheckFollow();
			}
		}
		else
		{
			this.Entity.OnLeaveField();
		}
	}

	public void OnCallToDestroy()
	{
		this.Destroy();
	}

	protected override void OnDestroy()
	{
		ActorVisibleManager.Instance.Remove(this.FixTransform);
		base.OnDestroy();
	}

	protected void SetTransform()
	{
		this.fixTransform = base.get_transform();
	}

	protected void SetGameObject()
	{
		this.fixGameObject = base.get_gameObject();
	}

	protected void SetAnimator()
	{
		this.fixAnimator = base.GetComponentInChildren<Animator>();
	}

	protected void SetCharacterController()
	{
		CharacterController[] componentsInChildren = base.GetComponentsInChildren<CharacterController>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_enabled(false);
			}
		}
	}

	protected void SetNavMeshAgent()
	{
		if (this.fixNavAgent)
		{
			return;
		}
		if (!MySceneManager.Instance.IsSceneExist)
		{
			return;
		}
		this.fixNavAgent = this.FixGameObject.AddUniqueComponent<NavMeshAgent>();
		this.FixNavAgent.set_autoBraking(false);
		this.FixNavAgent.set_autoTraverseOffMeshLink(false);
		this.FixNavAgent.set_radius(0f);
		this.FixNavAgent.Warp(this.FixTransform.get_position());
	}

	public void SetScale(float scale)
	{
		this.FixTransform.set_localScale(Vector3.get_one() * scale);
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>(true);
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_startSpeed(componentsInChildren[i].get_startSpeed() * scale);
				componentsInChildren[i].set_startSize(componentsInChildren[i].get_startSize() * scale);
			}
		}
	}

	public void Init()
	{
		if (!this.IsOwnerDataExist)
		{
			return;
		}
		this.SetNavMeshAgent();
		this.SetPosition();
		this.LogicMoveSpeed = this.Entity.MoveSpeed;
		this.LogicRunActionSpeed = this.Entity.ActionSpeed;
	}

	protected void UpdateMoveSpeed()
	{
		this.RealMoveSpeed = this.LogicMoveSpeed;
	}

	protected void UpdateActionSpeed()
	{
		this.RealActionSpeed = this.LogicActionSpeed;
	}

	protected void ChangeAnimationSpeed()
	{
		this.FixAnimator.set_speed(this.RealActionSpeed);
	}

	public override void OnActionStatusExit(ActionStatusExitCmd cmd)
	{
		if (XUtility.StartsWith(cmd.actName, string.Empty))
		{
			return;
		}
		this.UpdateActionSpeed();
	}

	public override void OnActionStatusEnter(ActionStatusEnterCmd cmd)
	{
		this.UpdateActionSpeed();
	}

	public void CastAction(string actionName)
	{
		if (!this.FixAnimator)
		{
			return;
		}
		this.OnActionStatusExit(new ActionStatusExitCmd
		{
			actName = this.CurOutPutAction,
			isBreak = true
		});
		this.CurActionStatus = actionName;
		this.CurOutPutAction = this.GetOutPutAction(actionName);
		this.FixAnimator.Play(this.CurOutPutAction);
		this.OnActionStatusEnter(new ActionStatusEnterCmd
		{
			actName = this.CurOutPutAction
		});
	}

	protected string GetOutPutAction(string actionName)
	{
		string text = actionName + "_city";
		if (this.FixAnimator.HasAction(text))
		{
			return text;
		}
		return actionName;
	}

	public void OnAnimatorBecameVisiable()
	{
		if (DataReader<Action>.Contains(this.CurActionStatus))
		{
			if (DataReader<Action>.Get(this.CurActionStatus).loop != 0)
			{
				this.CastAction(this.CurActionStatus);
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

	protected void EndAnimationResetToIdle()
	{
		this.CastAction("idle");
	}

	protected void SetPosition()
	{
		XPoint arg_68_0 = new XPoint
		{
			position = this.Entity.Owner.Actor.FixTransform.get_position(),
			rotation = this.Entity.Owner.Actor.FixTransform.get_rotation()
		};
		List<int> list = new List<int>();
		list.Add(0);
		list.Add((int)(-ActorCityPet.StopDistance * 100f));
		XPoint xPoint = arg_68_0.ApplyOffset(list);
		NavMeshHit navMeshHit;
		if (this.FixNavAgent && NavMesh.SamplePosition(xPoint.position, ref navMeshHit, 50f, -1))
		{
			this.FixTransform.set_position(navMeshHit.get_position());
			this.FixNavAgent.Warp(this.FixTransform.get_position());
		}
		this.FixTransform.set_rotation(this.Entity.Owner.Actor.FixTransform.get_rotation());
	}

	protected void CheckFollow()
	{
		float num = XUtility.DistanceNoY(this.FixTransform.get_position(), this.Entity.Owner.Actor.FixTransform.get_position());
		if (num <= ActorCityPet.StopDistance)
		{
			this.StopFollow();
		}
		else if (num > ActorCityPet.StartDistance)
		{
			this.Follow();
		}
	}

	protected void StopFollow()
	{
		if (!this.IsFollowing)
		{
			return;
		}
		if (!this.FixNavAgent)
		{
			return;
		}
		this.IsFollowing = false;
		bool flag = this.FixNavAgent.Warp(this.FixTransform.get_position());
		if (flag)
		{
			this.FixNavAgent.Stop();
			this.FixNavAgent.ResetPath();
		}
		this.FixNavAgent.set_speed(0f);
		this.FixNavAgent.set_updatePosition(false);
		this.FixNavAgent.set_updateRotation(false);
		this.FixNavAgent.set_enabled(false);
		this.CastAction("idle");
		this.LookAt(this.Entity.Owner.Actor.FixTransform.get_position());
	}

	protected void Follow()
	{
		if (!this.FixNavAgent)
		{
			return;
		}
		if (!this.IsFollowing)
		{
			this.IsFollowing = this.FixNavAgent.Warp(this.FixTransform.get_position());
			this.FixNavAgent.set_enabled(true);
			this.FixNavAgent.set_speed(this.RealMoveSpeed);
			this.FixNavAgent.set_updatePosition(true);
			this.FixNavAgent.set_updateRotation(true);
			this.FixNavAgent.set_angularSpeed(1080f);
			if (this.IsFollowing && !XUtility.StartsWith(this.CurActionStatus, "run"))
			{
				this.CastAction("run");
			}
		}
		if (this.IsFollowing)
		{
			this.FixNavAgent.SetDestination(this.Entity.Owner.Actor.FixTransform.get_position());
			this.FixNavAgent.Resume();
		}
	}

	protected void LookAt(Vector3 targetPos)
	{
		this.FixTransform.set_forward(new Vector3(targetPos.x - this.FixTransform.get_position().x, 0f, targetPos.z - this.FixTransform.get_position().z));
	}
}
