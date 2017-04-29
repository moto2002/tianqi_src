using System;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class NpcPatrol : MonoBehaviour
{
	public bool patrolBacktrack;

	public float speed = 1.2f;

	public float waitTime = 3f;

	public Transform[] patrolWayPoints;

	private float waitTimer;

	private int currentWaypoint;

	private bool backingUp;

	private NavMeshAgent nav;

	private Transform trans;

	private Animator anim;

	private int RunId;

	private int IdleId;

	private int AnimationId;

	private void Start()
	{
		this.trans = base.get_transform();
		this.nav = base.GetComponent<NavMeshAgent>();
		this.anim = base.GetComponentInChildren<Animator>();
		this.nav.set_updateRotation(true);
		this.RunId = Animator.StringToHash("run");
		this.IdleId = Animator.StringToHash("idle");
	}

	private void Update()
	{
		this.Patrol();
	}

	private void Patrol()
	{
		if (this.patrolWayPoints.Length > 0)
		{
			this.nav.set_speed(this.speed);
			if (this.nav.get_remainingDistance() <= this.nav.get_stoppingDistance())
			{
				this.AnimationId = this.IdleId;
				this.waitTimer += Time.get_deltaTime();
				if (this.waitTimer >= this.waitTime)
				{
					this.waitTimer = 0f;
					if (this.backingUp)
					{
						this.currentWaypoint--;
					}
					else
					{
						this.currentWaypoint++;
					}
					if (this.currentWaypoint < 0)
					{
						this.backingUp = false;
						this.currentWaypoint = 1;
					}
					if (this.currentWaypoint >= this.patrolWayPoints.Length)
					{
						if (this.patrolBacktrack)
						{
							this.backingUp = true;
							this.currentWaypoint -= 2;
						}
						else
						{
							this.currentWaypoint = 0;
						}
					}
				}
			}
			else
			{
				this.AnimationId = this.RunId;
				this.waitTimer = 0f;
			}
			this.NavAnimSetup();
		}
	}

	private void OnAnimatorMove()
	{
		this.nav.set_velocity(this.anim.get_deltaPosition() / Time.get_deltaTime());
		this.trans.set_rotation(this.anim.get_rootRotation());
	}

	private void NavAnimSetup()
	{
		float num = this.FindAngle(this.trans.get_forward(), this.nav.get_desiredVelocity());
		if (Mathf.Abs(num) > 26f)
		{
			this.trans.Rotate(Vector3.get_up(), num, 0);
		}
		this.Setup();
	}

	private float FindAngle(Vector3 fromVector, Vector3 toVector)
	{
		fromVector.y = 0f;
		toVector.y = 0f;
		if (toVector == Vector3.get_zero())
		{
			return 0f;
		}
		float num = Vector3.Angle(fromVector, toVector);
		if (Vector3.Cross(fromVector, toVector).y < 0f)
		{
			num *= -1f;
		}
		return num;
	}

	private void Setup()
	{
		this.anim.CrossFade(this.AnimationId, 0.2f);
		this.anim.Play(this.AnimationId);
		this.nav.set_destination(this.patrolWayPoints[this.currentWaypoint].get_position());
	}
}
