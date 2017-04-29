using Spine;
using System;
using System.IO;
using UnityEngine;

public class MaterialsTextureLoader : TextureLoader
{
	private AtlasAsset atlasAsset;

	public MaterialsTextureLoader(AtlasAsset atlasAsset)
	{
		this.atlasAsset = atlasAsset;
	}

	public void Load(AtlasPage page, string path)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		Material material = null;
		Material[] materials = this.atlasAsset.materials;
		int num = 0;
		if (num < materials.Length)
		{
			Material material2 = materials[num];
			if (material2.get_mainTexture() == null)
			{
				Debug.LogError("Material is missing texture: " + material2.get_name(), material2);
				return;
			}
			material = material2;
		}
		if (material == null)
		{
			Debug.LogError("Material with texture name \"" + fileNameWithoutExtension + "\" not found for atlas asset: " + this.atlasAsset.get_name(), this.atlasAsset);
			return;
		}
		page.rendererObject = material;
		if (page.width == 0 || page.height == 0)
		{
			page.width = material.get_mainTexture().get_width();
			page.height = material.get_mainTexture().get_height();
		}
	}

	public void Unload(object texture)
	{
	}
}
