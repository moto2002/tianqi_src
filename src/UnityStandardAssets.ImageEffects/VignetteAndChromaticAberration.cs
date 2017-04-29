using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode, RequireComponent(typeof(Camera))]
	public class VignetteAndChromaticAberration : PostProcessBase
	{
		public enum AberrationMode
		{
			Simple,
			Advanced
		}

		public VignetteAndChromaticAberration.AberrationMode mode;

		public float intensity = 0.4f;

		public float blur = 0.76f;

		public float blurSpread = 0.5f;

		public float chromaticAberration = 0.2f;

		public float axialAberration = 0.5f;

		public float luminanceDependency = 0.25f;

		public float blurDistance = 2.5f;

		private Material m_VignetteMaterial;

		private Material m_SeparableBlurMaterial;

		private Material m_ChromAberrationMaterial;

		public override void Initialization()
		{
			base.Initialization();
			if (this.IsInitSuccessed)
			{
				this.IsDestoryOnDisable = false;
			}
		}

		protected override void SetShaders()
		{
			if (!this.m_shaderNames.Contains("Hidden/Vignetting"))
			{
				this.m_shaderNames.Add("Hidden/Vignetting");
			}
			if (!this.m_shaderNames.Contains("Hidden/SeparableBlur"))
			{
				this.m_shaderNames.Add("Hidden/SeparableBlur");
			}
			if (!this.m_shaderNames.Contains("Hidden/ChromaticAberration"))
			{
				this.m_shaderNames.Add("Hidden/ChromaticAberration");
			}
		}

		protected override void GetMaterials()
		{
			this.m_VignetteMaterial = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
			this.m_SeparableBlurMaterial = this.m_materials.get_Item(this.m_shaderNames.get_Item(1));
			this.m_ChromAberrationMaterial = this.m_materials.get_Item(this.m_shaderNames.get_Item(2));
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.m_VignetteMaterial == null || this.m_SeparableBlurMaterial == null || this.m_ChromAberrationMaterial == null)
			{
				Graphics.Blit(source, destination);
				base.set_enabled(false);
				return;
			}
			int width = source.get_width();
			int height = source.get_height();
			float num = 1f * (float)width / (1f * (float)height);
			RenderTexture renderTexture = null;
			RenderTexture renderTexture2 = null;
			bool flag = Mathf.Abs(this.blur) > 0f || Mathf.Abs(this.intensity) > 0f;
			if (flag)
			{
				renderTexture = RenderTexture.GetTemporary(width, height, 0, source.get_format());
				if (Mathf.Abs(this.blur) > 0f)
				{
					renderTexture2 = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
					Graphics.Blit(source, renderTexture2, this.m_ChromAberrationMaterial, 0);
					for (int i = 0; i < 2; i++)
					{
						this.m_SeparableBlurMaterial.SetVector("offsets", new Vector4(0f, this.blurSpread * 0.001953125f, 0f, 0f));
						RenderTexture temporary = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
						Graphics.Blit(renderTexture2, temporary, this.m_SeparableBlurMaterial);
						RenderTexture.ReleaseTemporary(renderTexture2);
						this.m_SeparableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 0.001953125f / num, 0f, 0f, 0f));
						renderTexture2 = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
						Graphics.Blit(temporary, renderTexture2, this.m_SeparableBlurMaterial);
						RenderTexture.ReleaseTemporary(temporary);
					}
				}
				this.m_VignetteMaterial.SetFloat("_Intensity", 1f / (1f - this.intensity) - 1f);
				this.m_VignetteMaterial.SetFloat("_Blur", 1f / (1f - this.blur) - 1f);
				this.m_VignetteMaterial.SetTexture("_VignetteTex", renderTexture2);
				Graphics.Blit(source, renderTexture, this.m_VignetteMaterial, 0);
			}
			this.m_ChromAberrationMaterial.SetFloat("_ChromaticAberration", this.chromaticAberration);
			this.m_ChromAberrationMaterial.SetFloat("_AxialAberration", this.axialAberration);
			this.m_ChromAberrationMaterial.SetVector("_BlurDistance", new Vector2(-this.blurDistance, this.blurDistance));
			this.m_ChromAberrationMaterial.SetFloat("_Luminance", 1f / Mathf.Max(Mathf.Epsilon, this.luminanceDependency));
			if (flag)
			{
				renderTexture.set_wrapMode(1);
			}
			else
			{
				source.set_wrapMode(1);
			}
			Graphics.Blit((!flag) ? source : renderTexture, destination, this.m_ChromAberrationMaterial, (this.mode != VignetteAndChromaticAberration.AberrationMode.Advanced) ? 1 : 2);
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture.ReleaseTemporary(renderTexture2);
		}
	}
}
