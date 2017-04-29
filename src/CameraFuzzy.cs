using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class CameraFuzzy : MonoBehaviour
{
	public Shader BlurBoxShader;

	[Range(0f, 1f)]
	public float BlurSize = 0.5f;

	private Material BlurBoxMaterial;

	private Material material
	{
		get
		{
			if (this.BlurBoxMaterial == null)
			{
				this.BlurBoxMaterial = new Material(this.BlurBoxShader);
				this.BlurBoxMaterial.set_hideFlags(61);
			}
			return this.BlurBoxMaterial;
		}
	}

	private void Start()
	{
		this.BlurBoxShader = ShaderManager.Find("Unlit/GaussianBlur");
		if (!SystemInfo.get_supportsImageEffects())
		{
			base.set_enabled(false);
			return;
		}
		if (!this.BlurBoxShader || !this.BlurBoxShader.get_isSupported())
		{
			base.set_enabled(false);
		}
	}

	public void FourTapCone(RenderTexture source, RenderTexture dest)
	{
		float num = this.BlurSize + 0.5f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
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
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.BlurBoxShader != null)
		{
			int num = sourceTexture.get_width() / 8;
			int num2 = sourceTexture.get_height() / 8;
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
			this.DownSample4x(sourceTexture, sourceTexture);
			this.FourTapCone(sourceTexture, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	public void OnDisable()
	{
		if (this.BlurBoxMaterial)
		{
			Object.DestroyImmediate(this.BlurBoxMaterial);
		}
	}

	[DebuggerHidden]
	private IEnumerator Fuzzy()
	{
		CameraFuzzy.<Fuzzy>c__Iterator65 <Fuzzy>c__Iterator = new CameraFuzzy.<Fuzzy>c__Iterator65();
		<Fuzzy>c__Iterator.<>f__this = this;
		return <Fuzzy>c__Iterator;
	}
}
