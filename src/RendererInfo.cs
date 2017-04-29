using System;
using UnityEngine;

[Serializable]
public struct RendererInfo
{
	public Renderer renderer;

	public int lightmapIndex;

	public Vector4 lightmapScaleOffset;

	public void UpdateLightmap()
	{
		if (this.renderer == null)
		{
			return;
		}
		this.renderer.set_lightmapIndex(this.lightmapIndex);
		this.renderer.set_lightmapScaleOffset(this.lightmapScaleOffset);
	}
}
