using System;
using UnityEngine;

public class FXSyncPosition : MonoBehaviour
{
	private Transform TargetTransform;

	public void SetTargetTransform(Transform target)
	{
		this.TargetTransform = target;
		this.Update();
	}

	private void OnDisable()
	{
		base.set_enabled(false);
	}

	private void Update()
	{
		if (this.TargetTransform != null)
		{
			base.get_transform().set_position(this.TargetTransform.get_position());
		}
	}
}
