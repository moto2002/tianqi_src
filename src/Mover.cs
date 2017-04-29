using System;
using UnityEngine;

public class Mover : PlatformRider
{
	public enum Mode
	{
		Auto,
		Basic,
		Physics,
		Navigation
	}

	public enum RotationMode
	{
		Mover,
		Agent,
		Ignore
	}

	public Mover.Mode mode;

	public Vector3 targetVelocity;

	public Vector3 targetPosition;

	public Quaternion targetRotation;

	public float rotationInterpolationSpeed = 10f;

	public float positionInterpolationSpeed = 30f;

	public float speed = 5f;

	public float rotationSpeed = 5f;

	public float arrivalDistance = 1f;

	[NonSerialized]
	public float drag;

	public Vector3 gravity = new Vector3(0f, -9.81f, 0f);

	public float minimumGravityMovement = 0.001f;

	public LayerMask groundLayers = -1;

	public float groundedCheckOffset = 0.7f;

	public float groundedDistance = 1f;

	public Mover.RotationMode rotationMode;

	public bool targetDesiredVelocity;

	public bool constrainVelocity;

	public bool constrainRotation;

	public bool debug;

	public float speedScale = 1f;

	private bool applyPosition = true;

	private bool firstSync = true;

	private bool validDestination;

	private Vector3 destination;

	private bool grounded;

	private Vector3 lastPosition;

	public NavMeshAgent navMeshAgent
	{
		get
		{
			return base.GetComponent<NavMeshAgent>();
		}
	}

	public Rigidbody thisRigidbody
	{
		get
		{
			return base.GetComponent<Rigidbody>();
		}
	}

	public bool ValidDestination
	{
		get
		{
			return this.validDestination;
		}
	}

	public Vector3 Destination
	{
		get
		{
			if (!this.validDestination)
			{
				return Vector3.get_zero();
			}
			return this.destination;
		}
		set
		{
			this.validDestination = true;
			if (this.mode == Mover.Mode.Navigation)
			{
				if (!this.navMeshAgent.get_pathPending())
				{
					this.navMeshAgent.set_destination(value);
					this.destination = this.navMeshAgent.get_destination();
				}
			}
			else
			{
				this.destination = value;
			}
		}
	}

	public bool Grounded
	{
		get
		{
			return this.grounded;
		}
	}

	public bool ApplyPosition
	{
		get
		{
			return this.applyPosition;
		}
		set
		{
			this.applyPosition = value;
		}
	}

	public virtual Vector3 ConstrainedVelocity
	{
		get
		{
			return this.targetVelocity * this.speedScale;
		}
	}

	public virtual Quaternion ConstrainedRotation
	{
		get
		{
			return this.targetRotation;
		}
	}

	public virtual Vector3 ConstrainedGravity
	{
		get
		{
			return this.gravity;
		}
	}

	public float Speed
	{
		get
		{
			Mover.Mode mode = this.mode;
			if (mode != Mover.Mode.Navigation)
			{
				return this.speed;
			}
			return this.navMeshAgent.get_speed();
		}
	}

	public Vector3 CurrentVelocity
	{
		get
		{
			switch (this.mode)
			{
			case Mover.Mode.Physics:
				return this.thisRigidbody.get_velocity();
			case Mover.Mode.Navigation:
				return this.navMeshAgent.get_velocity();
			}
			return this.targetVelocity;
		}
	}

	public Vector3 CurrentGravityVelocity
	{
		get
		{
			Vector3 normalized = this.ConstrainedGravity.get_normalized();
			return Vector3.Dot(this.CurrentVelocity, normalized) * normalized;
		}
	}

	public Vector3 CurrentPlanarVelocity
	{
		get
		{
			return this.CurrentVelocity - this.CurrentGravityVelocity;
		}
	}

	protected virtual void Awake()
	{
		switch (this.mode)
		{
		case Mover.Mode.Auto:
			if (this.navMeshAgent != null)
			{
				this.mode = Mover.Mode.Navigation;
			}
			else if (this.thisRigidbody != null)
			{
				this.mode = Mover.Mode.Physics;
			}
			else
			{
				this.mode = Mover.Mode.Basic;
			}
			break;
		case Mover.Mode.Physics:
			if (this.thisRigidbody == null)
			{
				Debuger.Error("Physics mode set with no rigidbody available", new object[0]);
				Object.Destroy(this);
			}
			break;
		case Mover.Mode.Navigation:
			if (this.navMeshAgent == null)
			{
				Debuger.Error("Navigation mode set with no NavMeshAgent available", new object[0]);
				Object.Destroy(this);
			}
			break;
		}
		this.TargetCurrent();
	}

	protected virtual void Start()
	{
		Mover.Mode mode = this.mode;
		if (mode != Mover.Mode.Physics)
		{
			if (mode == Mover.Mode.Navigation)
			{
				this.speed = this.navMeshAgent.get_speed();
				this.navMeshAgent.set_enabled(true);
				if (this.applyPosition)
				{
					this.navMeshAgent.set_updatePosition(false);
					this.navMeshAgent.set_updateRotation(false);
				}
				else
				{
					this.navMeshAgent.set_updatePosition(true);
					switch (this.rotationMode)
					{
					case Mover.RotationMode.Mover:
					case Mover.RotationMode.Ignore:
						this.navMeshAgent.set_updateRotation(false);
						break;
					case Mover.RotationMode.Agent:
						this.navMeshAgent.set_updateRotation(true);
						break;
					}
				}
			}
		}
		else
		{
			this.thisRigidbody.set_isKinematic(false);
			this.thisRigidbody.set_freezeRotation(true);
			this.thisRigidbody.set_useGravity(false);
		}
	}

	public virtual void Stop()
	{
		this.targetVelocity = Vector3.get_zero();
		this.validDestination = false;
		Mover.Mode mode = this.mode;
		if (mode != Mover.Mode.Physics)
		{
			if (mode == Mover.Mode.Navigation)
			{
				this.navMeshAgent.ResetPath();
			}
		}
		else
		{
			Rigidbody arg_55_0 = this.thisRigidbody;
			Vector3 zero = Vector3.get_zero();
			this.thisRigidbody.set_angularVelocity(zero);
			arg_55_0.set_velocity(zero);
			this.thisRigidbody.Sleep();
		}
	}

	public void ClearDestination()
	{
		this.validDestination = false;
		if (this.mode == Mover.Mode.Navigation)
		{
			this.navMeshAgent.ResetPath();
		}
	}

	public void TargetCurrent()
	{
		this.targetPosition = base.get_transform().get_position();
		this.targetRotation = base.get_transform().get_rotation();
		switch (this.mode)
		{
		case Mover.Mode.Basic:
			this.targetVelocity = Vector3.get_zero();
			this.drag = 0f;
			break;
		case Mover.Mode.Physics:
			this.targetVelocity = this.thisRigidbody.get_velocity();
			this.drag = this.thisRigidbody.get_drag();
			break;
		case Mover.Mode.Navigation:
			this.targetVelocity = this.navMeshAgent.get_velocity();
			this.drag = 0f;
			break;
		}
	}

	private void ConvertDestinationToVelocity()
	{
		if (this.validDestination)
		{
			if ((this.destination - base.get_transform().get_position()).get_magnitude() < this.arrivalDistance)
			{
				this.Stop();
			}
			else
			{
				this.targetVelocity = (this.destination - base.get_transform().get_position()).get_normalized() * this.speed;
			}
		}
	}

	protected virtual void Update()
	{
		this.MoverUpdate();
	}

	protected virtual void MoverUpdate()
	{
		if (this.applyPosition)
		{
			Vector3 vector = Vector3.Lerp(base.get_transform().get_position(), this.targetPosition, Time.get_deltaTime() * this.positionInterpolationSpeed);
			switch (this.mode)
			{
			case Mover.Mode.Basic:
				base.get_transform().set_position(vector);
				break;
			case Mover.Mode.Physics:
			{
				Rigidbody arg_78_0 = this.thisRigidbody;
				Vector3 zero = Vector3.get_zero();
				this.thisRigidbody.set_angularVelocity(zero);
				arg_78_0.set_velocity(zero);
				this.thisRigidbody.MovePosition(vector);
				break;
			}
			case Mover.Mode.Navigation:
				this.navMeshAgent.set_velocity(this.targetVelocity);
				this.navMeshAgent.Move(vector - base.get_transform().get_position());
				this.navMeshAgent.set_nextPosition(vector);
				break;
			}
			this.PredictPosition(Time.get_deltaTime());
		}
		else
		{
			switch (this.mode)
			{
			case Mover.Mode.Basic:
			{
				this.ConvertDestinationToVelocity();
				Transform expr_108 = base.get_transform();
				expr_108.set_position(expr_108.get_position() + ((!this.constrainVelocity) ? this.targetVelocity : this.ConstrainedVelocity) * Time.get_deltaTime());
				break;
			}
			case Mover.Mode.Navigation:
				if (!this.navMeshAgent.get_enabled() || !this.navMeshAgent.get_updatePosition())
				{
					Transform expr_169 = base.get_transform();
					expr_169.set_position(expr_169.get_position() + ((!this.constrainVelocity) ? this.targetVelocity : this.ConstrainedVelocity) * Time.get_deltaTime());
				}
				else if (!this.validDestination)
				{
					this.navMeshAgent.set_velocity((!this.constrainVelocity) ? this.targetVelocity : this.ConstrainedVelocity);
				}
				else if (!this.navMeshAgent.get_pathPending() && this.navMeshAgent.get_remainingDistance() <= this.navMeshAgent.get_stoppingDistance())
				{
					this.Stop();
				}
				else
				{
					this.navMeshAgent.set_destination(this.destination);
					this.destination = this.navMeshAgent.get_destination();
					this.targetVelocity = ((!this.targetDesiredVelocity) ? this.navMeshAgent.get_velocity() : this.navMeshAgent.get_desiredVelocity());
					if (this.targetDesiredVelocity || this.constrainVelocity)
					{
						this.navMeshAgent.set_velocity((!this.constrainVelocity) ? this.targetVelocity : this.ConstrainedVelocity);
					}
				}
				break;
			}
			this.targetPosition = base.get_transform().get_position();
		}
		if (this.mode == Mover.Mode.Physics)
		{
			this.thisRigidbody.set_drag(this.drag);
		}
		if (this.applyPosition)
		{
			base.get_transform().set_rotation(Quaternion.Slerp(base.get_transform().get_rotation(), (!this.constrainRotation) ? this.targetRotation : this.ConstrainedRotation, Time.get_deltaTime() * this.rotationInterpolationSpeed));
		}
		else
		{
			if (this.rotationMode == Mover.RotationMode.Mover)
			{
				base.get_transform().set_rotation(Quaternion.Slerp(base.get_transform().get_rotation(), (!this.constrainRotation) ? this.targetRotation : this.ConstrainedRotation, Time.get_deltaTime() * this.rotationSpeed));
			}
			this.targetRotation = base.get_transform().get_rotation();
		}
	}

	protected virtual void FixedUpdate()
	{
		if (this.mode != Mover.Mode.Physics)
		{
			this.grounded = true;
			return;
		}
		this.grounded = (Physics.OverlapSphere(base.get_transform().get_position() + this.ConstrainedGravity.get_normalized() * this.groundedCheckOffset, this.groundedDistance, this.groundLayers).Length > 0);
		if (!this.applyPosition)
		{
			this.ConvertDestinationToVelocity();
			if (!this.grounded || this.ConstrainedVelocity.get_magnitude() > 0f)
			{
				this.thisRigidbody.AddForce(this.ConstrainedVelocity - this.thisRigidbody.get_velocity() + this.CurrentGravityVelocity, 2);
			}
			if (!this.grounded || (base.get_transform().get_position() - this.lastPosition).get_magnitude() > this.minimumGravityMovement)
			{
				this.thisRigidbody.AddForce(this.ConstrainedGravity);
			}
		}
		this.lastPosition = base.get_transform().get_position();
	}

	public virtual void Teleport(Vector3 position)
	{
		base.get_transform().set_position(position);
	}

	public override void UpdatePlatform(Vector3 platformDelta)
	{
		switch (this.mode)
		{
		case Mover.Mode.Basic:
		{
			Transform expr_26 = base.get_transform();
			expr_26.set_position(expr_26.get_position() + platformDelta);
			break;
		}
		case Mover.Mode.Physics:
			base.get_transform().set_position(this.thisRigidbody.get_position() + platformDelta);
			break;
		case Mover.Mode.Navigation:
			this.navMeshAgent.Move(platformDelta);
			break;
		}
	}

	public virtual void PredictPosition(float time)
	{
		float num = Mathf.Max(0f, this.targetVelocity.get_magnitude() - this.drag);
		if (num > 0f)
		{
			this.targetPosition += (this.targetVelocity.get_normalized() * num + ((!this.grounded) ? this.ConstrainedGravity : Vector3.get_zero())) * time;
		}
	}

	protected virtual void OnDrawGizmos()
	{
		if (!this.debug)
		{
			return;
		}
		if (this.mode == Mover.Mode.Physics)
		{
			Gizmos.set_color((!this.grounded) ? Color.get_red() : Color.get_green());
			Gizmos.DrawWireSphere(base.get_transform().get_position() + base.get_transform().get_up() * -this.groundedCheckOffset, this.groundedDistance);
		}
		Gizmos.set_color(Color.get_red());
		Gizmos.DrawLine(base.get_transform().get_position(), base.get_transform().get_position() + this.targetVelocity.get_normalized() * 2f);
		Gizmos.set_color(new Color(1f, 0.3f, 0.3f));
		Gizmos.DrawLine(base.get_transform().get_position(), base.get_transform().get_position() + this.ConstrainedVelocity.get_normalized() * 2f);
	}

	public virtual void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo messageInfo)
	{
		stream.Serialize(ref this.drag);
		if (!stream.get_isWriting())
		{
			if (this.firstSync)
			{
				this.firstSync = false;
				base.get_transform().set_position(this.targetPosition);
				base.get_transform().set_rotation(this.targetRotation);
			}
			else
			{
				this.PredictPosition((float)(Network.get_time() - messageInfo.get_timestamp()));
			}
		}
	}
}
