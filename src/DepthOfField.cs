using System;
using UnityEngine;

public class DepthOfField : PostProcessBase
{
	public enum BlurType
	{
		DiscBlur,
		DX11
	}

	public enum BlurSampleCount
	{
		Low,
		Medium,
		High
	}

	public bool visualizeFocus;

	public float focalLength = 10f;

	public float focalSize = 0.44f;

	public float aperture = 15.86f;

	public float maxBlurSize = 3.5f;

	public bool highResolution;

	public DepthOfField.BlurSampleCount blurSampleCount;

	public bool nearBlur;

	private float foregroundOverlap = 1f;

	private float focalDistance01 = 10f;

	private float internalBlurWidth = 1f;

	private Transform focalTransform;

	private Material dofHdrMaterial;

	protected override void OnEnable()
	{
		if (GameLevelManager.IsPostProcessReachQuality(1000))
		{
			base.OnEnable();
			this.IsDestoryOnDisable = true;
			Camera expr_22 = base.GetComponent<Camera>();
			expr_22.set_depthTextureMode(expr_22.get_depthTextureMode() | 1);
			this.GetMaterials();
			if (this.focalTransform == null)
			{
				GameObject gameObject = new GameObject("FocusTransform");
				gameObject.get_transform().SetParent(CamerasMgr.MainCameraRoot);
				gameObject.get_transform().set_localPosition(Vector3.get_zero());
				gameObject.get_transform().set_localRotation(Quaternion.get_identity());
				gameObject.get_transform().set_localScale(Vector3.get_one());
				this.focalTransform = gameObject.get_transform();
			}
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (this.dofHdrMaterial)
		{
			Object.DestroyImmediate(this.dofHdrMaterial);
		}
		this.dofHdrMaterial = null;
		if (this.focalTransform != null && this.focalTransform.get_gameObject() != null)
		{
			Object.Destroy(this.focalTransform.get_gameObject());
		}
	}

	protected override void GetMaterials()
	{
		this.dofHdrMaterial = new Material(ShaderManager.Find("Hidden/Dof/DepthOfFieldHdr"));
	}

	private float FocalDistance01(float worldDist)
	{
		return base.GetComponent<Camera>().WorldToViewportPoint((worldDist - base.GetComponent<Camera>().get_nearClipPlane()) * base.GetComponent<Camera>().get_transform().get_forward() + base.GetComponent<Camera>().get_transform().get_position()).z / (base.GetComponent<Camera>().get_farClipPlane() - base.GetComponent<Camera>().get_nearClipPlane());
	}

	private void WriteCoc(RenderTexture fromTo, bool fgDilate)
	{
		this.dofHdrMaterial.SetTexture(ShaderPIDManager._FgOverlap, null);
		if (this.nearBlur && fgDilate)
		{
			int num = fromTo.get_width() / 2;
			int num2 = fromTo.get_height() / 2;
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
			Graphics.Blit(fromTo, temporary, this.dofHdrMaterial, 4);
			float num3 = this.internalBlurWidth * this.foregroundOverlap;
			this.dofHdrMaterial.SetVector(ShaderPIDManager._Offsets, new Vector4(0f, num3, 0f, num3));
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
			Graphics.Blit(temporary, temporary2, this.dofHdrMaterial, 2);
			RenderTexture.ReleaseTemporary(temporary);
			this.dofHdrMaterial.SetVector(ShaderPIDManager._Offsets, new Vector4(num3, 0f, 0f, num3));
			temporary = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
			Graphics.Blit(temporary2, temporary, this.dofHdrMaterial, 2);
			RenderTexture.ReleaseTemporary(temporary2);
			this.dofHdrMaterial.SetTexture(ShaderPIDManager._FgOverlap, temporary);
			fromTo.MarkRestoreExpected();
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 13);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else
		{
			fromTo.MarkRestoreExpected();
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 0);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.aperture < 0f)
		{
			this.aperture = 0f;
		}
		if (this.maxBlurSize < 0.1f)
		{
			this.maxBlurSize = 0.1f;
		}
		this.focalSize = Mathf.Clamp(this.focalSize, 0f, 2f);
		this.internalBlurWidth = Mathf.Max(this.maxBlurSize, 0f);
		this.focalDistance01 = ((!this.focalTransform) ? this.FocalDistance01(this.focalLength) : (base.GetComponent<Camera>().WorldToViewportPoint(this.focalTransform.get_position()).z / base.GetComponent<Camera>().get_farClipPlane()));
		this.dofHdrMaterial.SetVector(ShaderPIDManager._CurveParams, new Vector4(1f, this.focalSize, this.aperture / 10f, this.focalDistance01));
		RenderTexture renderTexture = null;
		RenderTexture renderTexture2 = null;
		if (this.visualizeFocus)
		{
			this.WriteCoc(source, true);
			Graphics.Blit(source, destination, this.dofHdrMaterial, 16);
		}
		else
		{
			source.set_filterMode(1);
			if (this.highResolution)
			{
				this.internalBlurWidth *= 2f;
			}
			this.WriteCoc(source, true);
			renderTexture = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
			renderTexture2 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
			int num = (this.blurSampleCount != DepthOfField.BlurSampleCount.High && this.blurSampleCount != DepthOfField.BlurSampleCount.Medium) ? 11 : 17;
			if (this.highResolution)
			{
				this.dofHdrMaterial.SetVector(ShaderPIDManager._Offsets, new Vector4(0f, this.internalBlurWidth, 0.025f, this.internalBlurWidth));
				Graphics.Blit(source, destination, this.dofHdrMaterial, num);
			}
			else
			{
				this.dofHdrMaterial.SetVector(ShaderPIDManager._Offsets, new Vector4(0f, this.internalBlurWidth, 0.1f, this.internalBlurWidth));
				Graphics.Blit(source, renderTexture, this.dofHdrMaterial, 6);
				Graphics.Blit(renderTexture, renderTexture2, this.dofHdrMaterial, num);
				this.dofHdrMaterial.SetTexture(ShaderPIDManager._LowRez, renderTexture2);
				this.dofHdrMaterial.SetTexture(ShaderPIDManager._FgOverlap, null);
				this.dofHdrMaterial.SetVector(ShaderPIDManager._Offsets, Vector4.get_one() * (1f * (float)source.get_width() / (1f * (float)renderTexture2.get_width())) * this.internalBlurWidth);
				Graphics.Blit(source, destination, this.dofHdrMaterial, (this.blurSampleCount != DepthOfField.BlurSampleCount.High) ? 12 : 18);
			}
		}
		if (renderTexture)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		if (renderTexture2)
		{
			RenderTexture.ReleaseTemporary(renderTexture2);
		}
	}
}
