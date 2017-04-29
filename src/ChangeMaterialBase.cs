using System;
using UnityEngine;

public abstract class ChangeMaterialBase : MaterialControllerBase
{
	protected override void ChangeRendererToCommonMaterial()
	{
		Renderer[] componentsInChildren = this.prefabRoot.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (base.IsRendererValid(renderer))
			{
				if (!this.UseOriginalPrevious)
				{
					int instanceID = renderer.GetInstanceID();
					this.m_src_matmap.set_Item(instanceID, Utils.GetShareMaterials(renderer));
				}
				Utils.SetShareMaterials(renderer, this.mat);
				this.m_dst_matmap.set_Item(renderer.GetInstanceID(), Utils.GetShareMaterials(renderer));
			}
		}
	}

	protected override void RecoverRendererToOriginalMaterial()
	{
		if (this.prefabRoot != null)
		{
			Renderer[] componentsInChildren = this.prefabRoot.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Renderer renderer = componentsInChildren[i];
				if (this.m_src_matmap.ContainsKey(renderer.GetInstanceID()))
				{
					this.ResetSrcMaterials(this.m_src_matmap.get_Item(renderer.GetInstanceID()));
					Utils.SetShareMaterials(renderer, this.m_src_matmap.get_Item(renderer.GetInstanceID()));
				}
			}
		}
		else
		{
			Debuger.Error("prefabRoot is null", new object[0]);
		}
		this.m_src_matmap.Clear();
		this.m_dst_matmap.Clear();
	}

	private void ResetSrcMaterials(Material[] materials)
	{
		for (int i = 0; i < materials.Length; i++)
		{
			ActorHitEffectBase.SetHitValue(materials[i], 1f);
		}
	}
}
