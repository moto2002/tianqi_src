using System;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleScaler : MonoBehaviour
{
	public float particleScale = 1f;

	public bool alsoScaleGameobject = true;

	private float prevScale;

	private void Start()
	{
		this.prevScale = this.particleScale;
	}

	private void Update()
	{
	}

	private void ScaleShurikenSystems(float scaleFactor)
	{
	}

	private void ScaleLegacySystems(float scaleFactor)
	{
	}

	private void ScaleTrailRenderers(float scaleFactor)
	{
		TrailRenderer[] componentsInChildren = base.GetComponentsInChildren<TrailRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			TrailRenderer trailRenderer = componentsInChildren[i];
			TrailRenderer expr_15 = trailRenderer;
			expr_15.set_startWidth(expr_15.get_startWidth() * scaleFactor);
			TrailRenderer expr_23 = trailRenderer;
			expr_23.set_endWidth(expr_23.get_endWidth() * scaleFactor);
		}
	}
}
