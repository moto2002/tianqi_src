using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class HeightDepthofField : MonoBehaviour
{
	public DofResolution resolution = DofResolution.High;

	public float maxBlurSpread = 3f;

	public float foregroundBlurExtrude = 3f;

	public float smoothness = 1f;

	public bool visualize;

	private Camera m_main;

	private GameObject eye;

	private GameObject fx;

	private Shader dofBlurShader;

	private Material dofBlurMaterial;

	private Shader dofShader;

	private Material dofMaterial;

	private float widthOverHeight = 1.25f;

	private float oneOverBaseSize = 0.001953125f;

	private float cameraNear = 0.5f;

	private float cameraFar = 50f;

	private float cameraFov = 60f;

	private float cameraAspect = 1.333333f;

	public Camera main
	{
		get
		{
			if (this.m_main == null)
			{
				this.m_main = Camera.get_main();
			}
			return this.m_main;
		}
	}

	private void Start()
	{
		this.CreatEye();
		this.CreateMaterials();
		base.StartCoroutine(this.Fuzzy());
	}

	private void OnDisable()
	{
		if (this.dofBlurMaterial)
		{
			Object.DestroyImmediate(this.dofBlurMaterial);
			this.dofBlurMaterial = null;
		}
		if (this.dofMaterial)
		{
			Object.DestroyImmediate(this.dofMaterial);
			this.dofMaterial = null;
		}
	}

	[DebuggerHidden]
	private IEnumerator Fuzzy()
	{
		HeightDepthofField.<Fuzzy>c__Iterator66 <Fuzzy>c__Iterator = new HeightDepthofField.<Fuzzy>c__Iterator66();
		<Fuzzy>c__Iterator.<>f__this = this;
		return <Fuzzy>c__Iterator;
	}

	private void CreateMaterials()
	{
		if (!this.dofBlurMaterial)
		{
			this.dofBlurMaterial = new Material(ShaderManager.Find("Hidden/BlurPassesForDOF"));
		}
		if (!this.dofMaterial)
		{
			this.dofMaterial = new Material(ShaderManager.Find("Hidden/HeightDepthOfField"));
		}
	}

	private void CreatEye()
	{
		this.fx = GameObject.Find("FXSystem");
		if (this.fx != null)
		{
			this.fx.get_gameObject().SetActive(false);
		}
		this.eye = InstantiateToolsOfPrefab.Get("EyeAnimation", this.main.get_transform());
		this.eye.get_transform().set_localPosition(new Vector3(0f, 0f, 0.5f));
		this.eye.get_transform().set_localScale(new Vector3(10f, 10f, 10f));
	}

	private void DestroyObject()
	{
		if (this.fx != null)
		{
			this.fx.get_gameObject().SetActive(true);
		}
		if (this.eye != null)
		{
			Object.Destroy(this.eye);
		}
		Object.Destroy(this);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.widthOverHeight = 1f * (float)source.get_width() / (1f * (float)source.get_height());
		this.oneOverBaseSize = 0.001953125f;
		this.cameraNear = this.main.get_nearClipPlane();
		this.cameraFar = this.main.get_farClipPlane();
		this.cameraFov = this.main.get_fieldOfView();
		this.cameraAspect = this.main.get_aspect();
		Matrix4x4 identity = Matrix4x4.get_identity();
		float num = this.cameraFov * 0.5f;
		Vector3 vector = this.main.get_transform().get_right() * this.cameraNear * Mathf.Tan(num * 0.0174532924f) * this.cameraAspect;
		Vector3 vector2 = this.main.get_transform().get_up() * this.cameraNear * Mathf.Tan(num * 0.0174532924f);
		Vector3 vector3 = this.main.get_transform().get_forward() * this.cameraNear - vector + vector2;
		float num2 = vector3.get_magnitude() * this.cameraFar / this.cameraNear;
		vector3.Normalize();
		vector3 *= num2;
		Vector3 vector4 = this.main.get_transform().get_forward() * this.cameraNear + vector + vector2;
		vector4.Normalize();
		vector4 *= num2;
		Vector3 vector5 = this.main.get_transform().get_forward() * this.cameraNear + vector - vector2;
		vector5.Normalize();
		vector5 *= num2;
		Vector3 vector6 = this.main.get_transform().get_forward() * this.cameraNear - vector - vector2;
		vector6.Normalize();
		vector6 *= num2;
		identity.SetRow(0, vector3);
		identity.SetRow(1, vector4);
		identity.SetRow(2, vector5);
		identity.SetRow(3, vector6);
		this.dofMaterial.SetMatrix(ShaderPIDManager._FrustumCornersWS, identity);
		this.dofMaterial.SetVector(ShaderPIDManager._CameraWS, this.main.get_transform().get_position());
		this.dofMaterial.SetVector(ShaderPIDManager._ObjectFocusParameter, new Vector4(0f - this.main.get_transform().get_position().y, 1f / this.smoothness, 1f, 0.55f));
		this.dofMaterial.SetFloat(ShaderPIDManager._ForegroundBlurExtrude, this.foregroundBlurExtrude);
		this.dofMaterial.SetVector(ShaderPIDManager._InvRenderTargetSize, new Vector4(1f / (1f * (float)source.get_width()), 1f / (1f * (float)source.get_height()), 0f, 0f));
		int num3 = 1;
		if (this.resolution == DofResolution.Medium)
		{
			num3 = 2;
		}
		else if (this.resolution >= DofResolution.Medium)
		{
			num3 = 3;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.get_width() / num3, source.get_height() / num3, 0);
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.get_width() / num3, source.get_height() / num3, 0);
		RenderTexture temporary4 = RenderTexture.GetTemporary(source.get_width() / (num3 * 2), source.get_height() / (num3 * 2), 0);
		source.set_filterMode(1);
		temporary.set_filterMode(1);
		temporary4.set_filterMode(1);
		temporary2.set_filterMode(1);
		temporary3.set_filterMode(1);
		this.CustomGraphicsBlit(null, source, this.dofMaterial, 3);
		temporary3.DiscardContents();
		Graphics.Blit(source, temporary3, this.dofMaterial, 6);
		this.Blur(temporary3, temporary2, 1, 0, this.maxBlurSpread * 0.75f);
		this.Blur(temporary2, temporary4, 2, 0, this.maxBlurSpread);
		this.dofBlurMaterial.SetTexture("_TapLow", temporary4);
		this.dofBlurMaterial.SetTexture("_TapMedium", temporary2);
		Graphics.Blit(null, temporary3, this.dofBlurMaterial, 2);
		this.dofMaterial.SetTexture("_TapLowBackground", temporary3);
		this.dofMaterial.SetTexture("_TapMedium", temporary2);
		temporary.DiscardContents();
		Graphics.Blit(source, temporary, this.dofMaterial, (!this.visualize) ? 0 : 2);
		this.CustomGraphicsBlit(temporary, source, this.dofMaterial, 5);
		Graphics.Blit(source, temporary3, this.dofMaterial, 6);
		this.Blur(temporary3, temporary2, 1, 1, this.maxBlurSpread * 0.75f);
		this.Blur(temporary2, temporary4, 2, 1, this.maxBlurSpread);
		this.dofBlurMaterial.SetTexture("_TapLow", temporary4);
		this.dofBlurMaterial.SetTexture("_TapMedium", temporary2);
		Graphics.Blit(null, temporary3, this.dofBlurMaterial, 2);
		if (destination != null)
		{
			destination.DiscardContents();
		}
		this.dofMaterial.SetTexture("_TapLowForeground", temporary3);
		this.dofMaterial.SetTexture("_TapMedium", temporary2);
		Graphics.Blit(source, destination, this.dofMaterial, (!this.visualize) ? 4 : 1);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
	}

	private void Blur(RenderTexture from, RenderTexture to, int iterations, int blurPass, float spread)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(to.get_width(), to.get_height(), 0);
		if (iterations < 2)
		{
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
			temporary.DiscardContents();
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
			to.DiscardContents();
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		else
		{
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
			temporary.DiscardContents();
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
			to.DiscardContents();
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, 0f, 0f));
			temporary.DiscardContents();
			Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector(ShaderPIDManager.offsets, new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, -spread * this.oneOverBaseSize, 0f, 0f));
			to.DiscardContents();
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	private void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
	{
		RenderTexture.set_active(dest);
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		GL.MultiTexCoord2(0, 0f, 0f);
		GL.Vertex3(0f, 0f, 3f);
		GL.MultiTexCoord2(0, 1f, 0f);
		GL.Vertex3(1f, 0f, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, 0f, 1f);
		GL.Vertex3(0f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}
}
