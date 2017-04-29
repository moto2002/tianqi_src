using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddMaterialOnHit : MonoBehaviour
{
	public float RemoveAfterTime = 5f;

	public Material Material;

	public bool UsePointMatrixTransform;

	public Vector3 TransformScale = Vector3.get_one();

	private FadeInOutShaderColor[] fadeInOutShaderColor;

	private FadeInOutShaderFloat[] fadeInOutShaderFloat;

	private UVTextureAnimator uvTextureAnimator;

	private Renderer renderParent;

	private Material instanceMat;

	private int materialQueue = -1;

	public void UpdateMaterial(RaycastHit hit)
	{
		Transform transform = hit.get_transform();
		if (transform != null)
		{
			Object.Destroy(base.get_gameObject(), this.RemoveAfterTime);
			this.fadeInOutShaderColor = base.GetComponents<FadeInOutShaderColor>();
			this.fadeInOutShaderFloat = base.GetComponents<FadeInOutShaderFloat>();
			this.uvTextureAnimator = base.GetComponent<UVTextureAnimator>();
			this.renderParent = base.get_transform().get_parent().GetComponent<Renderer>();
			Material[] sharedMaterials = this.renderParent.get_sharedMaterials();
			int num = sharedMaterials.Length + 1;
			Material[] array = new Material[num];
			sharedMaterials.CopyTo(array, 0);
			this.renderParent.set_material(this.Material);
			this.instanceMat = this.renderParent.get_material();
			array[num - 1] = this.instanceMat;
			this.renderParent.set_sharedMaterials(array);
			if (this.UsePointMatrixTransform)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(hit.get_transform().InverseTransformPoint(hit.get_point()), Quaternion.Euler(180f, 180f, 0f), this.TransformScale);
				this.instanceMat.SetMatrix("_DecalMatr", matrix4x);
			}
			if (this.materialQueue != -1)
			{
				this.instanceMat.set_renderQueue(this.materialQueue);
			}
			if (this.fadeInOutShaderColor != null)
			{
				for (int i = 0; i < this.fadeInOutShaderColor.Length; i++)
				{
					this.fadeInOutShaderColor[i].UpdateMaterial(this.instanceMat);
				}
			}
			if (this.fadeInOutShaderFloat != null)
			{
				for (int j = 0; j < this.fadeInOutShaderFloat.Length; j++)
				{
					this.fadeInOutShaderFloat[j].UpdateMaterial(this.instanceMat);
				}
			}
			if (this.uvTextureAnimator != null)
			{
				this.uvTextureAnimator.SetInstanceMaterial(this.instanceMat, hit.get_textureCoord());
			}
		}
	}

	public void SetMaterialQueue(int matlQueue)
	{
		this.materialQueue = matlQueue;
	}

	private void OnDestroy()
	{
		if (this.renderParent == null)
		{
			return;
		}
		List<Material> list = Enumerable.ToList<Material>(this.renderParent.get_sharedMaterials());
		list.Remove(this.instanceMat);
		this.renderParent.set_sharedMaterials(list.ToArray());
	}
}
