using System;
using UnityEngine;

public class RainsplashBox : RainTimer
{
	private MeshFilter mf;

	private RainsplashManager manager;

	private void Start()
	{
		base.get_transform().set_localRotation(Quaternion.get_identity());
		this.manager = base.get_transform().get_parent().GetComponent<RainsplashManager>();
		this.SetBounds();
		this.mf = base.GetComponent<MeshFilter>();
		this.mf.set_sharedMesh(this.manager.GetPreGennedMesh());
		if (RainEffectManager.Instance != null)
		{
			RainEffectManager.Instance.AddRainTimer(this);
		}
		base.set_enabled(false);
	}

	private void SetBounds()
	{
	}

	private void OnBecameVisible()
	{
		base.set_enabled(true);
	}

	private void OnBecameInvisible()
	{
		base.set_enabled(false);
	}

	private void OnDrawGizmos()
	{
	}
}
