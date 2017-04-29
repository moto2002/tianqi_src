using System;
using UnityEngine;

public class RainBox : RainTimer
{
	private MeshFilter mf;

	private Vector3 defaultPosition;

	private RainManager manager;

	private Transform cachedTransform;

	private float cachedMinY;

	private float cachedAreaHeight;

	private float cachedFallingSpeed;

	private void Start()
	{
		this.cachedTransform = base.get_transform();
		this.manager = this.cachedTransform.GetComponentInParent<RainManager>();
		if (this.manager == null)
		{
			Debug.LogError("RainManager is null");
		}
		this.SetBounds();
		this.mf = base.GetComponent<MeshFilter>();
		this.mf.set_sharedMesh(this.manager.GetPreGennedMesh());
		this.cachedMinY = this.manager.minYPosition;
		this.cachedAreaHeight = this.manager.areaHeight;
		this.SetFallingSpeed();
		this.SetDirection(this.manager.fallingDirection);
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

	private void Update()
	{
		if (this.cachedTransform != null)
		{
			Transform expr_17 = this.cachedTransform;
			expr_17.set_position(expr_17.get_position() - Vector3.get_up() * Time.get_deltaTime() * this.cachedFallingSpeed);
			if (this.cachedTransform.get_position().y + this.cachedAreaHeight < this.cachedMinY)
			{
				this.cachedTransform.set_position(this.cachedTransform.get_position() + new Vector3((float)((double)(Vector3.get_up().x * this.cachedAreaHeight) * 2.0), (float)((double)(Vector3.get_up().y * this.cachedAreaHeight) * 2.0), (float)((double)(Vector3.get_up().z * this.cachedAreaHeight) * 2.0)));
			}
		}
	}

	public void SetDirection(Vector3 dir)
	{
		if (this.cachedTransform != null)
		{
			this.cachedTransform.set_localEulerAngles(dir);
		}
	}

	private void SetFallingSpeed()
	{
		this.cachedFallingSpeed = this.manager.fallingSpeed;
	}
}
