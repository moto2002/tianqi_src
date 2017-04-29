using Spine;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class SkeletonUtilityBone : MonoBehaviour
{
	public enum Mode
	{
		Follow,
		Override
	}

	[NonSerialized]
	public bool valid;

	[NonSerialized]
	public SkeletonUtility skeletonUtility;

	[NonSerialized]
	public Bone bone;

	public SkeletonUtilityBone.Mode mode;

	public bool zPosition = true;

	public bool position;

	public bool rotation;

	public bool scale;

	public bool flip;

	public bool flipX;

	[Range(0f, 1f)]
	public float overrideAlpha = 1f;

	public string boneName;

	public Transform parentReference;

	[HideInInspector]
	public bool transformLerpComplete;

	protected Transform cachedTransform;

	protected Transform skeletonTransform;

	private bool nonUniformScaleWarning;

	public bool NonUniformScaleWarning
	{
		get
		{
			return this.nonUniformScaleWarning;
		}
	}

	public void Reset()
	{
		this.bone = null;
		this.cachedTransform = base.get_transform();
		this.valid = (this.skeletonUtility != null && this.skeletonUtility.skeletonRenderer != null && this.skeletonUtility.skeletonRenderer.valid);
		if (!this.valid)
		{
			return;
		}
		this.skeletonTransform = this.skeletonUtility.get_transform();
		this.skeletonUtility.OnReset -= new SkeletonUtility.SkeletonUtilityDelegate(this.HandleOnReset);
		this.skeletonUtility.OnReset += new SkeletonUtility.SkeletonUtilityDelegate(this.HandleOnReset);
		this.DoUpdate();
	}

	private void OnEnable()
	{
		this.skeletonUtility = SkeletonUtility.GetInParent<SkeletonUtility>(base.get_transform());
		if (this.skeletonUtility == null)
		{
			return;
		}
		this.skeletonUtility.RegisterBone(this);
		this.skeletonUtility.OnReset += new SkeletonUtility.SkeletonUtilityDelegate(this.HandleOnReset);
	}

	private void HandleOnReset()
	{
		this.Reset();
	}

	private void OnDisable()
	{
		if (this.skeletonUtility != null)
		{
			this.skeletonUtility.OnReset -= new SkeletonUtility.SkeletonUtilityDelegate(this.HandleOnReset);
			this.skeletonUtility.UnregisterBone(this);
		}
	}

	public void DoUpdate()
	{
		if (!this.valid)
		{
			this.Reset();
			return;
		}
		Skeleton skeleton = this.skeletonUtility.skeletonRenderer.skeleton;
		if (this.bone == null)
		{
			if (this.boneName == null || this.boneName.get_Length() == 0)
			{
				return;
			}
			this.bone = skeleton.FindBone(this.boneName);
			if (this.bone == null)
			{
				Debug.LogError("Bone not found: " + this.boneName, this);
				return;
			}
		}
		float num = (!(skeleton.flipX ^ skeleton.flipY)) ? 1f : -1f;
		float num2 = 0f;
		if (this.flip && (this.flipX || this.flipX != this.bone.flipX) && this.bone.parent != null)
		{
			num2 = this.bone.parent.WorldRotation * -2f;
		}
		if (this.mode == SkeletonUtilityBone.Mode.Follow)
		{
			if (this.flip)
			{
				this.flipX = this.bone.flipX;
			}
			if (this.position)
			{
				this.cachedTransform.set_localPosition(new Vector3(this.bone.x, this.bone.y, 0f));
			}
			if (this.rotation)
			{
				if (this.bone.Data.InheritRotation)
				{
					if (this.bone.FlipX)
					{
						this.cachedTransform.set_localRotation(Quaternion.Euler(0f, 180f, this.bone.rotationIK - num2));
					}
					else
					{
						this.cachedTransform.set_localRotation(Quaternion.Euler(0f, 0f, this.bone.rotationIK));
					}
				}
				else
				{
					Vector3 eulerAngles = this.skeletonTransform.get_rotation().get_eulerAngles();
					this.cachedTransform.set_rotation(Quaternion.Euler(eulerAngles.x, eulerAngles.y, this.skeletonTransform.get_rotation().get_eulerAngles().z + this.bone.worldRotation * num));
				}
			}
			if (this.scale)
			{
				this.cachedTransform.set_localScale(new Vector3(this.bone.scaleX, this.bone.scaleY, (float)((!this.bone.worldFlipX) ? 1 : -1)));
				this.nonUniformScaleWarning = (this.bone.scaleX != this.bone.scaleY);
			}
		}
		else if (this.mode == SkeletonUtilityBone.Mode.Override)
		{
			if (this.transformLerpComplete)
			{
				return;
			}
			if (this.parentReference == null)
			{
				if (this.position)
				{
					this.bone.x = Mathf.Lerp(this.bone.x, this.cachedTransform.get_localPosition().x, this.overrideAlpha);
					this.bone.y = Mathf.Lerp(this.bone.y, this.cachedTransform.get_localPosition().y, this.overrideAlpha);
				}
				if (this.rotation)
				{
					float num3 = Mathf.LerpAngle(this.bone.Rotation, this.cachedTransform.get_localRotation().get_eulerAngles().z, this.overrideAlpha) + num2;
					if (this.flip)
					{
						if (!this.flipX && this.bone.flipX)
						{
							num3 -= num2;
						}
						if (num3 >= 360f)
						{
							num3 -= 360f;
						}
						else if (num3 <= -360f)
						{
							num3 += 360f;
						}
					}
					this.bone.Rotation = num3;
					this.bone.RotationIK = num3;
				}
				if (this.scale)
				{
					this.bone.scaleX = Mathf.Lerp(this.bone.scaleX, this.cachedTransform.get_localScale().x, this.overrideAlpha);
					this.bone.scaleY = Mathf.Lerp(this.bone.scaleY, this.cachedTransform.get_localScale().y, this.overrideAlpha);
					this.nonUniformScaleWarning = (this.bone.scaleX != this.bone.scaleY);
				}
				if (this.flip)
				{
					this.bone.flipX = this.flipX;
				}
			}
			else
			{
				if (this.transformLerpComplete)
				{
					return;
				}
				if (this.position)
				{
					Vector3 vector = this.parentReference.InverseTransformPoint(this.cachedTransform.get_position());
					this.bone.x = Mathf.Lerp(this.bone.x, vector.x, this.overrideAlpha);
					this.bone.y = Mathf.Lerp(this.bone.y, vector.y, this.overrideAlpha);
				}
				if (this.rotation)
				{
					float num4 = Mathf.LerpAngle(this.bone.Rotation, Quaternion.LookRotation((!this.flipX) ? Vector3.get_forward() : (Vector3.get_forward() * -1f), this.parentReference.InverseTransformDirection(this.cachedTransform.get_up())).get_eulerAngles().z, this.overrideAlpha) + num2;
					if (this.flip)
					{
						if (!this.flipX && this.bone.flipX)
						{
							num4 -= num2;
						}
						if (num4 >= 360f)
						{
							num4 -= 360f;
						}
						else if (num4 <= -360f)
						{
							num4 += 360f;
						}
					}
					this.bone.Rotation = num4;
					this.bone.RotationIK = num4;
				}
				if (this.scale)
				{
					this.bone.scaleX = Mathf.Lerp(this.bone.scaleX, this.cachedTransform.get_localScale().x, this.overrideAlpha);
					this.bone.scaleY = Mathf.Lerp(this.bone.scaleY, this.cachedTransform.get_localScale().y, this.overrideAlpha);
					this.nonUniformScaleWarning = (this.bone.scaleX != this.bone.scaleY);
				}
				if (this.flip)
				{
					this.bone.flipX = this.flipX;
				}
			}
			this.transformLerpComplete = true;
		}
	}

	public void FlipX(bool state)
	{
		if (state != this.flipX)
		{
			this.flipX = state;
			if (this.flipX && Mathf.Abs(base.get_transform().get_localRotation().get_eulerAngles().y) > 90f)
			{
				this.skeletonUtility.skeletonAnimation.LateUpdate();
				return;
			}
			if (!this.flipX && Mathf.Abs(base.get_transform().get_localRotation().get_eulerAngles().y) < 90f)
			{
				this.skeletonUtility.skeletonAnimation.LateUpdate();
				return;
			}
		}
		this.bone.FlipX = state;
		base.get_transform().RotateAround(base.get_transform().get_position(), this.skeletonUtility.get_transform().get_up(), 180f);
		Vector3 eulerAngles = base.get_transform().get_localRotation().get_eulerAngles();
		eulerAngles.x = 0f;
		eulerAngles.y = (float)((!this.bone.FlipX) ? 0 : 180);
		base.get_transform().set_localRotation(Quaternion.Euler(eulerAngles));
	}

	public void AddBoundingBox(string skinName, string slotName, string attachmentName)
	{
		SkeletonUtility.AddBoundingBox(this.bone.skeleton, skinName, slotName, attachmentName, base.get_transform(), true);
	}

	private void OnDrawGizmos()
	{
		if (this.NonUniformScaleWarning)
		{
			Gizmos.DrawIcon(base.get_transform().get_position() + new Vector3(0f, 0.128f, 0f), "icon-warning");
		}
	}
}
