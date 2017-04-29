using System;
using UnityEngine;

public class RenderUnit
{
	protected bool m_error;

	protected bool m_initialized;

	protected RenderCamera m_owner;

	protected RenderCompoent m_obj;

	public MeshRenderer m_meshRenderer;

	private Material[] m_sharedMaterials;

	private Mesh m_mesh;

	public RenderUnit(RenderCamera owner, RenderCompoent obj)
	{
		this.m_error = false;
		this.m_initialized = false;
		this.m_owner = owner;
		this.m_obj = obj;
		this.Initialize();
	}

	private void Initialize()
	{
		this.m_initialized = true;
		this.m_meshRenderer = this.m_obj.GetComponent<MeshRenderer>();
		if (this.m_meshRenderer != null)
		{
			this.m_sharedMaterials = this.m_meshRenderer.get_sharedMaterials();
		}
		MeshFilter component = this.m_obj.GetComponent<MeshFilter>();
		if (component != null)
		{
			this.m_mesh = component.get_mesh();
		}
	}

	internal void RenderVectors(Camera camera)
	{
		if (this.m_meshRenderer == null || !this.m_meshRenderer.get_isVisible())
		{
			return;
		}
		if (this.m_initialized && !this.m_error && this.m_meshRenderer.get_isVisible())
		{
			bool flag = (this.m_owner.Instance.CullingMask & 1 << this.m_obj.get_gameObject().get_layer()) != 0;
			if (flag)
			{
				for (int i = 0; i < this.m_sharedMaterials.Length; i++)
				{
					int pass = 0;
					if (this.m_owner.Instance.SolidVectorsMaterial.SetPass(pass))
					{
						Graphics.DrawMeshNow(this.m_mesh, this.m_obj.get_transform().get_localToWorldMatrix(), i);
					}
				}
			}
		}
	}
}
