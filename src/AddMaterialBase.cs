using System;
using UnityEngine;

public abstract class AddMaterialBase : MaterialControllerBase
{
	protected override void ChangeRendererToCommonMaterial()
	{
		Renderer[] componentsInChildren = this.prefabRoot.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (base.IsRendererValid(renderer))
			{
				int instanceID = renderer.GetInstanceID();
				Material[] shareMaterials = Utils.GetShareMaterials(renderer);
				if (!this.UseOriginalPrevious)
				{
					this.m_src_matmap.set_Item(instanceID, shareMaterials);
				}
				if (shareMaterials != null && shareMaterials.Length > 0)
				{
					Material[] mats = new Material[]
					{
						shareMaterials[0],
						this.mat
					};
					Utils.SetShareMaterials(renderer, mats);
				}
				else
				{
					Utils.SetShareMaterials(renderer, this.mat);
				}
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
}
