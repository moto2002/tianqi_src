using System;
using UnityEngine;

public class FullScreenGray : PostProcessBase
{
	private Material m_matMobileGray;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/MobileGray"))
		{
			this.m_shaderNames.Add("Hidden/MobileGray");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matMobileGray = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		if (this.m_matMobileGray == null)
		{
			base.set_enabled(false);
			return;
		}
		Graphics.Blit(source, dest, this.m_matMobileGray);
	}
}
