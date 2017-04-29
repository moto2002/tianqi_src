using System;
using UnityEngine;

public class ProjectileCollisionBehaviour : MonoBehaviour
{
	public float RandomMoveRadius;

	public float RandomMoveSpeed;

	public float RandomRange;

	public RandomMoveCoordinates RandomMoveCoordinates;

	public GameObject EffectOnHitObject;

	public GameObject GoLight;

	public AnimationCurve Acceleration;

	public float AcceleraionTime = 1f;

	public bool IsCenterLightPosition;

	public bool IsLookAt;

	public bool AttachAfterCollision;

	public bool IsRootMove = true;

	public bool IsLocalSpaceRandomMove;

	public bool IsDeviation;

	public bool SendCollisionMessage = true;

	public bool ResetParentPositionOnDisable;

	private EffectSettings effectSettings;

	private Transform tRoot;

	private Transform tTarget;

	private Transform t;

	private Transform tLight;

	private Vector3 forwardDirection;

	private Vector3 startPosition;

	private Vector3 startParentPosition;

	private RaycastHit hit;

	private Vector3 smootRandomPos;

	private Vector3 oldSmootRandomPos;

	private float deltaSpeed;

	private float startTime;

	private float randomSpeed;

	private float randomRadiusX;

	private float randomRadiusY;

	private int randomDirection1;

	private int randomDirection2;

	private int randomDirection3;

	private bool onCollision;

	private bool isInitializedOnStart;

	private Vector3 randomTargetOffsetXZVector;

	private bool frameDroped;

	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.get_parent();
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.get_transform());
			}
		}
	}

	private void Start()
	{
		this.t = base.get_transform();
		this.GetEffectSettingsComponent(this.t);
		if (this.effectSettings == null)
		{
			Debuger.Info("Prefab root or children have not script \"PrefabSettings\"", new object[0]);
		}
		if (!this.IsRootMove)
		{
			this.startParentPosition = base.get_transform().get_parent().get_position();
		}
		if (this.GoLight != null)
		{
			this.tLight = this.GoLight.get_transform();
		}
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	private void OnDisable()
	{
		if (this.ResetParentPositionOnDisable && this.isInitializedOnStart && !this.IsRootMove)
		{
			base.get_transform().get_parent().set_position(this.startParentPosition);
		}
	}

	private void InitializeDefault()
	{
		this.hit = default(RaycastHit);
		this.onCollision = false;
		this.smootRandomPos = default(Vector3);
		this.oldSmootRandomPos = default(Vector3);
		this.deltaSpeed = 0f;
		this.startTime = 0f;
		this.randomSpeed = 0f;
		this.randomRadiusX = 0f;
		this.randomRadiusY = 0f;
		this.randomDirection1 = 0;
		this.randomDirection2 = 0;
		this.randomDirection3 = 0;
		this.frameDroped = false;
		this.tRoot = ((!this.IsRootMove) ? base.get_transform().get_parent() : this.effectSettings.get_transform());
		this.startPosition = this.tRoot.get_position();
		this.tTarget = this.effectSettings.Target.get_transform();
		if ((double)this.effectSettings.EffectRadius > 0.001)
		{
			Vector2 vector = Random.get_insideUnitCircle() * this.effectSettings.EffectRadius;
			this.randomTargetOffsetXZVector = new Vector3(vector.x, 0f, vector.y);
		}
		else
		{
			this.randomTargetOffsetXZVector = Vector3.get_zero();
		}
		if (this.IsLookAt)
		{
			this.tRoot.LookAt(this.tTarget);
		}
		this.forwardDirection = this.tRoot.get_position() + (this.tTarget.get_position() + this.randomTargetOffsetXZVector - this.tRoot.get_position()).get_normalized() * this.effectSettings.MoveDistance;
		this.GetTargetHit();
		this.InitRandomVariables();
	}

	private void Update()
	{
		if (!this.frameDroped)
		{
			this.frameDroped = true;
			return;
		}
		if ((this.tTarget == null || this.onCollision) && this.frameDroped)
		{
			return;
		}
		Vector3 vector = (!this.effectSettings.IsHomingMove) ? this.forwardDirection : this.tTarget.get_position();
		float num = Vector3.Distance(this.tRoot.get_position(), vector);
		float num2 = this.effectSettings.MoveSpeed * Time.get_deltaTime();
		if (num2 > num)
		{
			num2 = num;
		}
		if (num <= this.effectSettings.ColliderRadius)
		{
			this.hit = default(RaycastHit);
			this.CollisionEnter();
		}
		Vector3 normalized = (vector - this.tRoot.get_position()).get_normalized();
		RaycastHit raycastHit;
		if (Physics.Raycast(this.tRoot.get_position(), normalized, ref raycastHit, num2 + this.effectSettings.ColliderRadius, this.effectSettings.LayerMask))
		{
			this.hit = raycastHit;
			vector = raycastHit.get_point() - normalized * this.effectSettings.ColliderRadius;
			this.CollisionEnter();
		}
		if (this.IsCenterLightPosition && this.GoLight != null)
		{
			this.tLight.set_position((this.startPosition + this.tRoot.get_position()) / 2f);
		}
		Vector3 vector2 = default(Vector3);
		if (this.RandomMoveCoordinates != RandomMoveCoordinates.None)
		{
			this.UpdateSmootRandomhPos();
			vector2 = this.smootRandomPos - this.oldSmootRandomPos;
		}
		float num3 = 1f;
		if (this.Acceleration.get_length() > 0)
		{
			float num4 = (Time.get_time() - this.startTime) / this.AcceleraionTime;
			num3 = this.Acceleration.Evaluate(num4);
		}
		Vector3 vector3 = Vector3.MoveTowards(this.tRoot.get_position(), vector, this.effectSettings.MoveSpeed * Time.get_deltaTime() * num3);
		Vector3 vector4 = vector3 + vector2;
		if (this.IsLookAt && this.effectSettings.IsHomingMove)
		{
			this.tRoot.LookAt(vector4);
		}
		if (this.IsLocalSpaceRandomMove)
		{
			if (this.IsRootMove)
			{
				this.tRoot.set_position(vector3);
				Transform expr_264 = this.t;
				expr_264.set_localPosition(expr_264.get_localPosition() + vector2);
			}
			else
			{
				this.tRoot.set_position(vector4);
			}
		}
		else if (this.IsRootMove)
		{
			this.tRoot.set_position(vector4);
		}
		this.oldSmootRandomPos = this.smootRandomPos;
	}

	private void CollisionEnter()
	{
		if (this.EffectOnHitObject != null && this.hit.get_transform() != null)
		{
			Transform transform = this.hit.get_transform();
			Renderer componentInChildren = transform.GetComponentInChildren<Renderer>();
			GameObject gameObject = Object.Instantiate<GameObject>(this.EffectOnHitObject);
			gameObject.get_transform().set_parent(componentInChildren.get_transform());
			gameObject.get_transform().set_localPosition(Vector3.get_zero());
			gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(this.hit);
		}
		if (this.AttachAfterCollision)
		{
			this.tRoot.set_parent(this.hit.get_transform());
		}
		if (this.SendCollisionMessage)
		{
			CollisionInfo e = new CollisionInfo
			{
				Hit = this.hit
			};
			this.effectSettings.OnCollisionHandler(e);
			if (this.hit.get_transform() != null)
			{
				ShieldCollisionBehaviour component = this.hit.get_transform().GetComponent<ShieldCollisionBehaviour>();
				if (component != null)
				{
					component.ShieldCollisionEnter(e);
				}
			}
		}
		this.onCollision = true;
	}

	private void InitRandomVariables()
	{
		this.deltaSpeed = this.RandomMoveSpeed * Random.Range(1f, 1000f * this.RandomRange + 1f) / 1000f - 1f;
		this.startTime = Time.get_time();
		this.randomRadiusX = Random.Range(this.RandomMoveRadius / 20f, this.RandomMoveRadius * 100f) / 100f;
		this.randomRadiusY = Random.Range(this.RandomMoveRadius / 20f, this.RandomMoveRadius * 100f) / 100f;
		this.randomSpeed = Random.Range(this.RandomMoveSpeed / 20f, this.RandomMoveSpeed * 100f) / 100f;
		this.randomDirection1 = ((Random.Range(0, 2) <= 0) ? -1 : 1);
		this.randomDirection2 = ((Random.Range(0, 2) <= 0) ? -1 : 1);
		this.randomDirection3 = ((Random.Range(0, 2) <= 0) ? -1 : 1);
	}

	private void GetTargetHit()
	{
		Ray ray = new Ray(this.tRoot.get_position(), Vector3.Normalize(this.tTarget.get_position() + this.randomTargetOffsetXZVector - this.tRoot.get_position()));
		Collider component = this.tTarget.GetComponent<Collider>();
		RaycastHit raycastHit;
		if (component != null && this.tTarget.GetComponent<Collider>().Raycast(ray, ref raycastHit, this.effectSettings.MoveDistance))
		{
			this.hit = raycastHit;
		}
	}

	private void UpdateSmootRandomhPos()
	{
		float num = Time.get_time() - this.startTime;
		float num2 = num * this.randomSpeed;
		float num3 = num * this.deltaSpeed;
		float num5;
		float num6;
		if (this.IsDeviation)
		{
			float num4 = Vector3.Distance(this.tRoot.get_position(), this.hit.get_point()) / this.effectSettings.MoveDistance;
			num5 = (float)this.randomDirection2 * Mathf.Sin(num2) * this.randomRadiusX * num4;
			num6 = (float)this.randomDirection3 * Mathf.Sin(num2 + (float)this.randomDirection1 * 3.14159274f / 2f * num + Mathf.Sin(num3)) * this.randomRadiusY * num4;
		}
		else
		{
			num5 = (float)this.randomDirection2 * Mathf.Sin(num2) * this.randomRadiusX;
			num6 = (float)this.randomDirection3 * Mathf.Sin(num2 + (float)this.randomDirection1 * 3.14159274f / 2f * num + Mathf.Sin(num3)) * this.randomRadiusY;
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XY)
		{
			this.smootRandomPos = new Vector3(num5, num6, 0f);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XZ)
		{
			this.smootRandomPos = new Vector3(num5, 0f, num6);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.YZ)
		{
			this.smootRandomPos = new Vector3(0f, num5, num6);
		}
		if (this.RandomMoveCoordinates == RandomMoveCoordinates.XYZ)
		{
			this.smootRandomPos = new Vector3(num5, num6, (num5 + num6) / 2f * (float)this.randomDirection1);
		}
	}
}
