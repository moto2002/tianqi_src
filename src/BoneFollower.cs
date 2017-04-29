using Spine;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class BoneFollower : MonoBehaviour
{
	[NonSerialized]
	public bool valid;

	public SkeletonRenderer skeletonRenderer;

	public Bone bone;

	public bool followZPosition = true;

	public bool followBoneRotation = true;

	[SpineBone("", "skeletonRenderer")]
	public string boneName;

	public bool resetOnAwake = true;

	protected Transform cachedTransform;

	protected Transform skeletonTransform;

	public SkeletonRenderer SkeletonRenderer
	{
		get
		{
			return this.skeletonRenderer;
		}
		set
		{
			this.skeletonRenderer = value;
			this.Reset();
		}
	}

	public void HandleResetRenderer(SkeletonRenderer skeletonRenderer)
	{
		this.Reset();
	}

	public void Reset()
	{
		this.bone = null;
		this.cachedTransform = base.get_transform();
		this.valid = (this.skeletonRenderer != null && this.skeletonRenderer.valid);
		if (!this.valid)
		{
			return;
		}
		this.skeletonTransform = this.skeletonRenderer.get_transform();
		SkeletonRenderer expr_5B = this.skeletonRenderer;
		expr_5B.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_5B.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleResetRenderer));
		SkeletonRenderer expr_82 = this.skeletonRenderer;
		expr_82.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(expr_82.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleResetRenderer));
		if (Application.get_isEditor())
		{
			this.DoUpdate();
		}
	}

	private void OnDestroy()
	{
		if (this.skeletonRenderer != null)
		{
			SkeletonRenderer expr_17 = this.skeletonRenderer;
			expr_17.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_17.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleResetRenderer));
		}
	}

	public void Awake()
	{
		if (this.resetOnAwake)
		{
			this.Reset();
		}
	}

	private void LateUpdate()
	{
		this.DoUpdate();
	}

	public void DoUpdate()
	{
		if (!this.valid)
		{
			this.Reset();
			return;
		}
		if (this.bone == null)
		{
			if (this.boneName == null || this.boneName.get_Length() == 0)
			{
				return;
			}
			this.bone = this.skeletonRenderer.skeleton.FindBone(this.boneName);
			if (this.bone == null)
			{
				Debug.LogError("Bone not found: " + this.boneName, this);
				return;
			}
		}
		Skeleton skeleton = this.skeletonRenderer.skeleton;
		float num = (!(skeleton.flipX ^ skeleton.flipY)) ? 1f : -1f;
		if (this.cachedTransform.get_parent() == this.skeletonTransform)
		{
			this.cachedTransform.set_localPosition(new Vector3(this.bone.worldX, this.bone.worldY, (!this.followZPosition) ? this.cachedTransform.get_localPosition().z : 0f));
			if (this.followBoneRotation)
			{
				Vector3 eulerAngles = this.cachedTransform.get_localRotation().get_eulerAngles();
				this.cachedTransform.set_localRotation(Quaternion.Euler(eulerAngles.x, eulerAngles.y, this.bone.worldRotation * num));
			}
		}
		else
		{
			Vector3 position = this.skeletonTransform.TransformPoint(new Vector3(this.bone.worldX, this.bone.worldY, 0f));
			if (!this.followZPosition)
			{
				position.z = this.cachedTransform.get_position().z;
			}
			this.cachedTransform.set_position(position);
			if (this.followBoneRotation)
			{
				Vector3 eulerAngles2 = this.skeletonTransform.get_rotation().get_eulerAngles();
				this.cachedTransform.set_rotation(Quaternion.Euler(eulerAngles2.x, eulerAngles2.y, this.skeletonTransform.get_rotation().get_eulerAngles().z + this.bone.worldRotation * num));
			}
		}
	}
}
