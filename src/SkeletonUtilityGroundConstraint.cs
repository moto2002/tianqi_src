using System;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(SkeletonUtilityBone))]
public class SkeletonUtilityGroundConstraint : SkeletonUtilityConstraint
{
	[Tooltip("LayerMask for what objects to raycast against")]
	public LayerMask groundMask;

	[Tooltip("The 2D")]
	public bool use2D;

	[Tooltip("Uses SphereCast for 3D mode and CircleCast for 2D mode")]
	public bool useRadius;

	[Tooltip("The Radius")]
	public float castRadius = 0.1f;

	[Tooltip("How high above the target bone to begin casting from")]
	public float castDistance = 5f;

	[Tooltip("X-Axis adjustment")]
	public float castOffset;

	[Tooltip("Y-Axis adjustment")]
	public float groundOffset;

	[Tooltip("How fast the target IK position adjusts to the ground.  Use smaller values to prevent snapping")]
	public float adjustSpeed = 5f;

	private Vector3 rayOrigin;

	private Vector3 rayDir = new Vector3(0f, -1f, 0f);

	private float hitY;

	private float lastHitY;

	protected override void OnEnable()
	{
		base.OnEnable();
		this.lastHitY = base.get_transform().get_position().y;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	public override void DoUpdate()
	{
		this.rayOrigin = base.get_transform().get_position() + new Vector3(this.castOffset, this.castDistance, 0f);
		this.hitY = -3.40282347E+38f;
		if (this.use2D)
		{
			RaycastHit2D raycastHit2D;
			if (this.useRadius)
			{
				raycastHit2D = Physics2D.CircleCast(this.rayOrigin, this.castRadius, this.rayDir, this.castDistance + this.groundOffset, this.groundMask);
			}
			else
			{
				raycastHit2D = Physics2D.Raycast(this.rayOrigin, this.rayDir, this.castDistance + this.groundOffset, this.groundMask);
			}
			if (raycastHit2D.get_collider() != null)
			{
				this.hitY = raycastHit2D.get_point().y + this.groundOffset;
				if (Application.get_isPlaying())
				{
					this.hitY = Mathf.MoveTowards(this.lastHitY, this.hitY, this.adjustSpeed * Time.get_deltaTime());
				}
			}
			else if (Application.get_isPlaying())
			{
				this.hitY = Mathf.MoveTowards(this.lastHitY, base.get_transform().get_position().y, this.adjustSpeed * Time.get_deltaTime());
			}
		}
		else
		{
			RaycastHit raycastHit;
			bool flag;
			if (this.useRadius)
			{
				flag = Physics.SphereCast(this.rayOrigin, this.castRadius, this.rayDir, ref raycastHit, this.castDistance + this.groundOffset, this.groundMask);
			}
			else
			{
				flag = Physics.Raycast(this.rayOrigin, this.rayDir, ref raycastHit, this.castDistance + this.groundOffset, this.groundMask);
			}
			if (flag)
			{
				this.hitY = raycastHit.get_point().y + this.groundOffset;
				if (Application.get_isPlaying())
				{
					this.hitY = Mathf.MoveTowards(this.lastHitY, this.hitY, this.adjustSpeed * Time.get_deltaTime());
				}
			}
			else if (Application.get_isPlaying())
			{
				this.hitY = Mathf.MoveTowards(this.lastHitY, base.get_transform().get_position().y, this.adjustSpeed * Time.get_deltaTime());
			}
		}
		Vector3 position = base.get_transform().get_position();
		position.y = Mathf.Clamp(position.y, Mathf.Min(this.lastHitY, this.hitY), 3.40282347E+38f);
		base.get_transform().set_position(position);
		this.utilBone.bone.X = base.get_transform().get_localPosition().x;
		this.utilBone.bone.Y = base.get_transform().get_localPosition().y;
		this.lastHitY = this.hitY;
	}

	private void OnDrawGizmos()
	{
		Vector3 vector = this.rayOrigin + this.rayDir * Mathf.Min(this.castDistance, this.rayOrigin.y - this.hitY);
		Vector3 vector2 = this.rayOrigin + this.rayDir * this.castDistance;
		Gizmos.DrawLine(this.rayOrigin, vector);
		if (this.useRadius)
		{
			Gizmos.DrawLine(new Vector3(vector.x - this.castRadius, vector.y - this.groundOffset, vector.z), new Vector3(vector.x + this.castRadius, vector.y - this.groundOffset, vector.z));
			Gizmos.DrawLine(new Vector3(vector2.x - this.castRadius, vector2.y, vector2.z), new Vector3(vector2.x + this.castRadius, vector2.y, vector2.z));
		}
		Gizmos.set_color(Color.get_red());
		Gizmos.DrawLine(vector, vector2);
	}
}
