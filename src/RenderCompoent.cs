using System;
using UnityEngine;

public class RenderCompoent : MonoBehaviour
{
	private RenderUnit m_RenderUnit;

	private void OnEnable()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			RenderManager.RegisterObject(this);
		}
		if (component == null)
		{
			base.set_enabled(false);
		}
	}

	private void OnDisable()
	{
		RenderManager.UnregisterObject(this);
	}

	internal void RegisterCamera(RenderCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		if ((component.get_cullingMask() & 1 << base.get_gameObject().get_layer()) != 0)
		{
			this.m_RenderUnit = new RenderUnit(camera, this);
			camera.RegisterObject(this);
		}
	}

	internal void UnregisterCamera(RenderCamera camera)
	{
		camera.UnregisterObject(this);
	}

	internal void OnRenderVectors(Camera camera)
	{
		this.m_RenderUnit.RenderVectors(camera);
	}
}
