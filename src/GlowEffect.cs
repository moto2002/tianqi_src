using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Bloom and Glow/Glow (Deprecated)"), ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class GlowEffect : MonoBehaviour
{
	public float glowIntensity = 1.5f;

	public int blurIterations = 3;

	public float blurSpread = 0.7f;

	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	public Shader compositeShader;

	private Material m_CompositeMaterial;

	public Shader blurShader;

	private Material m_BlurMaterial;

	public Shader downsampleShader;

	private Material m_DownsampleMaterial;

	protected Material compositeMaterial
	{
		get
		{
			if (this.m_CompositeMaterial == null)
			{
				this.m_CompositeMaterial = new Material(this.compositeShader);
				this.m_CompositeMaterial.set_hideFlags(61);
			}
			return this.m_CompositeMaterial;
		}
	}

	protected Material blurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.blurShader);
				this.m_BlurMaterial.set_hideFlags(61);
			}
			return this.m_BlurMaterial;
		}
	}

	protected Material downsampleMaterial
	{
		get
		{
			if (this.m_DownsampleMaterial == null)
			{
				this.m_DownsampleMaterial = new Material(this.downsampleShader);
				this.m_DownsampleMaterial.set_hideFlags(61);
			}
			return this.m_DownsampleMaterial;
		}
	}

	protected void OnDisable()
	{
		if (this.m_CompositeMaterial)
		{
			Object.DestroyImmediate(this.m_CompositeMaterial);
		}
		if (this.m_BlurMaterial)
		{
			Object.DestroyImmediate(this.m_BlurMaterial);
		}
		if (this.m_DownsampleMaterial)
		{
			Object.DestroyImmediate(this.m_DownsampleMaterial);
		}
	}

	protected void Start()
	{
		if (!SystemInfo.get_supportsImageEffects())
		{
			base.set_enabled(false);
			return;
		}
		if (this.downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.set_enabled(false);
		}
		else
		{
			if (!this.blurMaterial.get_shader().get_isSupported())
			{
				base.set_enabled(false);
			}
			if (!this.compositeMaterial.get_shader().get_isSupported())
			{
				base.set_enabled(false);
			}
			if (!this.downsampleMaterial.get_shader().get_isSupported())
			{
				base.set_enabled(false);
			}
		}
	}

	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, -num),
			new Vector2(-num, -num)
		});
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		this.downsampleMaterial.set_color(new Color(this.glowTint.r, this.glowTint.g, this.glowTint.b, this.glowTint.a / 4f));
		Graphics.Blit(source, dest, this.downsampleMaterial);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.glowIntensity = Mathf.Clamp(this.glowIntensity, 0f, 10f);
		this.blurIterations = Mathf.Clamp(this.blurIterations, 0, 30);
		this.blurSpread = Mathf.Clamp(this.blurSpread, 0.5f, 1f);
		int num = source.get_width() / 4;
		int num2 = source.get_height() / 4;
		RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0);
		this.DownSample4x(source, renderTexture);
		float num3 = Mathf.Clamp01((this.glowIntensity - 1f) / 4f);
		this.blurMaterial.set_color(new Color(1f, 1f, 1f, 0.25f + num3));
		for (int i = 0; i < this.blurIterations; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
			this.FourTapCone(renderTexture, temporary, i);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		Graphics.Blit(source, destination);
		this.BlitGlow(renderTexture, destination);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	public void BlitGlow(RenderTexture source, RenderTexture dest)
	{
		this.compositeMaterial.set_color(new Color(1f, 1f, 1f, Mathf.Clamp01(this.glowIntensity)));
		Graphics.Blit(source, dest, this.compositeMaterial);
	}
}
