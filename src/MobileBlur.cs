using System;
using UnityEngine;

public class MobileBlur : PostProcessBase
{
	public int iterations;

	public float blurSpread = 0.6f;

	private Material m_matFastBlur;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/FastBlur"))
		{
			this.m_shaderNames.Add("Hidden/FastBlur");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matFastBlur = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
	}

	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.m_matFastBlur, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.m_matFastBlur, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matFastBlur == null)
		{
			base.set_enabled(false);
			return;
		}
		int num = source.get_width() / 4;
		int num2 = source.get_height() / 4;
		RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0);
		this.DownSample4x(source, renderTexture);
		for (int i = 0; i < this.iterations; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
			this.FourTapCone(renderTexture, temporary, i);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		Graphics.Blit(renderTexture, destination);
		RenderTexture.ReleaseTemporary(renderTexture);
	}
}
