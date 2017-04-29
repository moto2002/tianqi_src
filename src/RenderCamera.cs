using System;
using System.Collections.Generic;
using UnityEngine;

public class RenderCamera : MonoBehaviour
{
	internal RenderManager Instance;

	private bool m_d3d;

	internal HashSet<RenderCompoent> m_affectedObjectsTable = new HashSet<RenderCompoent>();

	internal RenderCompoent[] m_affectedObjects;

	internal bool m_affectedObjectsChanged = true;

	private void OnEnable()
	{
		if (this.Instance == null)
		{
			this.Instance = RenderManager.Instance;
		}
		Camera expr_22 = base.GetComponent<Camera>();
		expr_22.set_depthTextureMode(expr_22.get_depthTextureMode() | 1);
		this.m_d3d = SystemInfo.get_graphicsDeviceVersion().StartsWith("Direct3D");
	}

	internal void RegisterObject(RenderCompoent obj)
	{
		this.m_affectedObjectsTable.Add(obj);
		this.m_affectedObjectsChanged = true;
	}

	internal void UnregisterObject(RenderCompoent obj)
	{
		this.m_affectedObjectsTable.Remove(obj);
		this.m_affectedObjectsChanged = true;
	}

	private void UpdateAffectedObjects()
	{
		if (this.m_affectedObjects == null || this.m_affectedObjectsTable.get_Count() != this.m_affectedObjects.Length)
		{
			this.m_affectedObjects = new RenderCompoent[this.m_affectedObjectsTable.get_Count()];
		}
		this.m_affectedObjectsTable.CopyTo(this.m_affectedObjects);
		this.m_affectedObjectsChanged = false;
	}

	internal void RenderVectors()
	{
		if (this.Instance != null)
		{
			Camera component = base.GetComponent<Camera>();
			float nearClipPlane = component.get_nearClipPlane();
			float farClipPlane = component.get_farClipPlane();
			Vector4 vector;
			if (this.m_d3d)
			{
				vector.x = 1f - farClipPlane / nearClipPlane;
				vector.y = farClipPlane / nearClipPlane;
			}
			else
			{
				vector.x = (1f - farClipPlane / nearClipPlane) / 2f;
				vector.y = (1f + farClipPlane / nearClipPlane) / 2f;
			}
			vector.z = vector.x / farClipPlane;
			vector.w = vector.y / farClipPlane;
			Shader.SetGlobalVector("_EFLOW_ZBUFFER_PARAMS", vector);
			if (this.m_affectedObjectsChanged)
			{
				this.UpdateAffectedObjects();
			}
			for (int i = 0; i < this.m_affectedObjects.Length; i++)
			{
				this.m_affectedObjects[i].OnRenderVectors(component);
			}
		}
	}

	private void OnPostRender()
	{
		RenderTexture active = RenderTexture.get_active();
		Graphics.SetRenderTarget(this.Instance.MotionRenderTexture);
		this.RenderVectors();
		RenderTexture.set_active(active);
	}
}
