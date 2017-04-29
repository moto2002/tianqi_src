using System;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(SkeletonUtilityBone))]
public abstract class SkeletonUtilityConstraint : MonoBehaviour
{
	protected SkeletonUtilityBone utilBone;

	protected SkeletonUtility skeletonUtility;

	protected virtual void OnEnable()
	{
		this.utilBone = base.GetComponent<SkeletonUtilityBone>();
		this.skeletonUtility = SkeletonUtility.GetInParent<SkeletonUtility>(base.get_transform());
		this.skeletonUtility.RegisterConstraint(this);
	}

	protected virtual void OnDisable()
	{
		this.skeletonUtility.UnregisterConstraint(this);
	}

	public abstract void DoUpdate();
}
