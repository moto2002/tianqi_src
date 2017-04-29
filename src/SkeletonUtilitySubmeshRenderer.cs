using System;
using UnityEngine;

[ExecuteInEditMode]
public class SkeletonUtilitySubmeshRenderer : MonoBehaviour
{
	[NonSerialized]
	public Mesh mesh;

	public int submeshIndex;

	public Material hiddenPassMaterial;

	private Renderer cachedRenderer;

	private MeshFilter filter;

	private Material[] sharedMaterials;

	private void Awake()
	{
		this.cachedRenderer = base.GetComponent<Renderer>();
		this.filter = base.GetComponent<MeshFilter>();
		this.sharedMaterials = new Material[0];
	}

	public void SetMesh(Renderer parentRenderer, Mesh mesh, Material mat)
	{
		if (this.cachedRenderer == null)
		{
			return;
		}
		this.cachedRenderer.set_enabled(true);
		this.filter.set_sharedMesh(mesh);
		if (this.cachedRenderer.get_sharedMaterials().Length != parentRenderer.get_sharedMaterials().Length)
		{
			this.sharedMaterials = parentRenderer.get_sharedMaterials();
		}
		for (int i = 0; i < this.sharedMaterials.Length; i++)
		{
			if (i == this.submeshIndex)
			{
				this.sharedMaterials[i] = mat;
			}
			else
			{
				this.sharedMaterials[i] = this.hiddenPassMaterial;
			}
		}
		this.cachedRenderer.set_sharedMaterials(this.sharedMaterials);
	}
}
