using GameData;
using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode, RequireComponent(typeof(Camera))]
	public class BloomOptimized : PostProcessBase
	{
		public enum Resolution
		{
			Low,
			High
		}

		public enum BlurType
		{
			Standard,
			Sgx
		}

		[Range(0f, 1.5f)]
		public float threshold = 0.5f;

		[Range(0f, 2.5f)]
		public float intensity = 0.5f;

		[Range(0.25f, 5.5f)]
		public float blurSize = 0.25f;

		[Range(1f, 4f)]
		public int blurIterations = 4;

		private BloomOptimized.Resolution resolution;

		public BloomOptimized.BlurType blurType;

		private Material fastBloomMaterial;

		public override void Initialization()
		{
			base.Initialization();
			if (this.IsInitSuccessed)
			{
				this.IsDestoryOnDisable = false;
				Scene scene = DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID);
				if (scene != null && scene.bloomParams.get_Count() >= 3)
				{
					this.threshold = scene.bloomParams.get_Item(0);
					this.intensity = scene.bloomParams.get_Item(1);
					this.blurSize = scene.bloomParams.get_Item(2);
				}
			}
		}

		protected override void SetShaders()
		{
			if (!this.m_shaderNames.Contains("Hidden/FastBloom"))
			{
				this.m_shaderNames.Add("Hidden/FastBloom");
			}
		}

		protected override void GetMaterials()
		{
			this.fastBloomMaterial = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.fastBloomMaterial == null)
			{
				Graphics.Blit(source, destination);
				base.set_enabled(false);
				return;
			}
			int num = (this.resolution != BloomOptimized.Resolution.Low) ? 2 : 4;
			float num2 = (this.resolution != BloomOptimized.Resolution.Low) ? 1f : 0.5f;
			this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2, 0f, this.threshold, this.intensity));
			source.set_filterMode(1);
			int num3 = source.get_width() / num;
			int num4 = source.get_height() / num;
			RenderTexture renderTexture = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
			renderTexture.set_filterMode(1);
			Graphics.Blit(source, renderTexture, this.fastBloomMaterial, 1);
			int num5 = (this.blurType != BloomOptimized.BlurType.Standard) ? 2 : 0;
			for (int i = 0; i < this.blurIterations; i++)
			{
				this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2 + (float)i * 1f, 0f, this.threshold, this.intensity));
				RenderTexture temporary = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
				temporary.set_filterMode(1);
				Graphics.Blit(renderTexture, temporary, this.fastBloomMaterial, 2 + num5);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
				temporary.set_filterMode(1);
				Graphics.Blit(renderTexture, temporary, this.fastBloomMaterial, 3 + num5);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			this.fastBloomMaterial.SetTexture("_Bloom", renderTexture);
			Graphics.Blit(source, destination, this.fastBloomMaterial, 0);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}
}
