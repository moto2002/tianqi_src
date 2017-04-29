using System;
using UnityEngine;

public class SkeletonUtilityEyeConstraint : SkeletonUtilityConstraint
{
	public Transform[] eyes;

	public float radius = 0.5f;

	public Transform target;

	public Vector3 targetPosition;

	public float speed = 10f;

	private Vector3[] origins;

	private Vector3 centerPoint;

	protected override void OnEnable()
	{
		if (!Application.get_isPlaying())
		{
			return;
		}
		base.OnEnable();
		Bounds bounds = new Bounds(this.eyes[0].get_localPosition(), Vector3.get_zero());
		this.origins = new Vector3[this.eyes.Length];
		for (int i = 0; i < this.eyes.Length; i++)
		{
			this.origins[i] = this.eyes[i].get_localPosition();
			bounds.Encapsulate(this.origins[i]);
		}
		this.centerPoint = bounds.get_center();
	}

	protected override void OnDisable()
	{
		if (!Application.get_isPlaying())
		{
			return;
		}
		base.OnDisable();
	}

	public override void DoUpdate()
	{
		if (this.target != null)
		{
			this.targetPosition = this.target.get_position();
		}
		Vector3 vector = this.targetPosition;
		Vector3 vector2 = base.get_transform().TransformPoint(this.centerPoint);
		Vector3 vector3 = vector - vector2;
		if (vector3.get_magnitude() > 1f)
		{
			vector3.Normalize();
		}
		for (int i = 0; i < this.eyes.Length; i++)
		{
			vector2 = base.get_transform().TransformPoint(this.origins[i]);
			this.eyes[i].set_position(Vector3.MoveTowards(this.eyes[i].get_position(), vector2 + vector3 * this.radius, this.speed * Time.get_deltaTime()));
		}
	}
}
