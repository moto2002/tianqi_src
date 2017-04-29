using System;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
	public enum Quality
	{
		Mobile,
		Standard,
		SoftEdge_SM3
	}

	public LayerMask CullingMask = -1;

	private static RenderManager m_instance;

	private static RenderCamera m_camPostProcessRender;

	private RenderManager.Quality QualityLevel;

	public static Dictionary<GameObject, RenderCompoent> m_activeObjects = new Dictionary<GameObject, RenderCompoent>();

	private RenderTexture m_motionRT;

	public RenderTexture m_combinedRT;

	private Material m_debugMaterial;

	private Material m_debugBlackMaterial;

	private Material m_combineMaterial;

	private Material m_solidVectorsMaterial;

	private PP_Holywood m_PP_Holywood;

	private PP_Vignette m_PP_Vignette;

	private int m_width;

	private int m_height;

	public static RenderManager Instance
	{
		get
		{
			return RenderManager.m_instance;
		}
	}

	private static RenderCamera PostProcessRenderCamera
	{
		get
		{
			return RenderManager.m_camPostProcessRender;
		}
		set
		{
			RenderManager.m_camPostProcessRender = value;
		}
	}

	internal RenderTexture MotionRenderTexture
	{
		get
		{
			return this.m_motionRT;
		}
	}

	internal RenderTexture CombineRenderTexture
	{
		get
		{
			return this.m_combinedRT;
		}
	}

	public Material DebugMaterial
	{
		get
		{
			return this.m_debugMaterial;
		}
	}

	public Material DebugBlackMaterial
	{
		get
		{
			return this.m_debugBlackMaterial;
		}
	}

	public Material SolidVectorsMaterial
	{
		get
		{
			return this.m_solidVectorsMaterial;
		}
	}

	public void Init()
	{
		RenderManager.m_instance = this;
		RenderManager.PostProcessRenderCamera = base.get_gameObject().AddMissingComponent<RenderCamera>();
		this.InitRes();
		this.UpdateActiveObjects();
	}

	private void OnDisable()
	{
	}

	public void UpdateActiveObjects()
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		for (int i = 0; i < array.Length; i++)
		{
			if (!RenderManager.m_activeObjects.ContainsKey(array[i]))
			{
				RenderManager.TryRegister(array[i]);
			}
		}
	}

	internal static void TryRegister(GameObject gameObj)
	{
		if (gameObj.get_isStatic())
		{
			return;
		}
		Renderer component = gameObj.GetComponent<Renderer>();
		if (component == null || component.get_sharedMaterials() == null || component.get_isPartOfStaticBatch())
		{
			return;
		}
		if (!component.get_enabled() || !RenderManager.FindValidTag(component.get_sharedMaterials()))
		{
			return;
		}
		if (component.GetType() == typeof(MeshRenderer) || component.GetType() == typeof(SkinnedMeshRenderer))
		{
			gameObj.AddMissingComponent<RenderCompoent>();
		}
	}

	internal static void RegisterObject(RenderCompoent obj)
	{
		if (obj != null)
		{
			RenderManager.m_activeObjects.Add(obj.get_gameObject(), obj);
		}
		obj.RegisterCamera(RenderManager.PostProcessRenderCamera);
	}

	internal static void UnregisterObject(RenderCompoent obj)
	{
		obj.UnregisterCamera(RenderManager.PostProcessRenderCamera);
		if (obj != null)
		{
			RenderManager.m_activeObjects.Remove(obj.get_gameObject());
		}
	}

	internal static bool FindValidTag(Material[] materials)
	{
		for (int i = 0; i < materials.Length; i++)
		{
			if (materials[i] != null)
			{
				string tag = materials[i].GetTag("RenderType", false);
				if (tag == "Opaque" || tag == "TransparentCutout")
				{
					return true;
				}
			}
		}
		return false;
	}

	private void InitRes()
	{
		if (!ShaderEffectUtils.CheckSupport())
		{
			base.set_enabled(false);
			return;
		}
		if (!this.CreateMaterials())
		{
			this.DestroyMaterials();
			base.set_enabled(false);
			return;
		}
		this.m_PP_Holywood = base.get_gameObject().AddMissingComponent<PP_Holywood>();
		this.m_PP_Holywood.Initialization();
		this.m_PP_Vignette = base.get_gameObject().AddMissingComponent<PP_Vignette>();
		this.m_PP_Vignette.Initialization();
		this.UpdateRenderTextures();
	}

	private bool CreateMaterials()
	{
		string text = "Hidden/Amplify Motion/Debug";
		string text2 = "Hidden/Amplify Motion/DebugBlack";
		string text3 = "Hidden/LensOnOffCombine";
		string text4 = "Hidden/Amplify Motion/SolidVectorsMobile";
		ShaderEffectUtils.SafeCreateMaterial(ref this.m_debugMaterial, text);
		ShaderEffectUtils.SafeCreateMaterial(ref this.m_debugBlackMaterial, text2);
		ShaderEffectUtils.SafeCreateMaterial(ref this.m_combineMaterial, text3);
		ShaderEffectUtils.SafeCreateMaterial(ref this.m_solidVectorsMaterial, text4);
		return ShaderEffectUtils.CheckMaterialAndShader(this.m_debugMaterial, text) && ShaderEffectUtils.CheckMaterialAndShader(this.m_debugBlackMaterial, text2) && ShaderEffectUtils.CheckMaterialAndShader(this.m_combineMaterial, text3) && ShaderEffectUtils.CheckMaterialAndShader(this.m_solidVectorsMaterial, text4);
	}

	private void DestroyMaterials()
	{
		ShaderEffectUtils.SafeDestroyMaterial(ref this.m_debugMaterial);
		ShaderEffectUtils.SafeDestroyMaterial(ref this.m_debugBlackMaterial);
	}

	private void UpdateRenderTextures()
	{
		Camera component = base.GetComponent<Camera>();
		int width = Mathf.FloorToInt((float)component.get_pixelWidth() + 0.5f);
		int height = Mathf.FloorToInt((float)component.get_pixelHeight() + 0.5f);
		if (this.QualityLevel == RenderManager.Quality.Mobile)
		{
			this.m_width = width;
			this.m_height = height;
		}
		else
		{
			this.m_width = width;
			this.m_height = height;
		}
		RenderTextureFormat fmt = 0;
		RenderTextureReadWrite rw = 1;
		RenderTextureFormat fmt2 = 0;
		RenderTextureReadWrite rw2 = 0;
		ShaderEffectUtils.SafeCreateRenderTexture(ref this.m_motionRT, "Motion", 24, fmt, rw, this.m_width, this.m_height);
		ShaderEffectUtils.SafeCreateRenderTexture(ref this.m_combinedRT, "Combined", 0, fmt2, rw2, this.m_width, this.m_height);
	}

	private void OnPreRender()
	{
		this.UpdateRenderTextures();
		this.m_motionRT.DiscardContents();
		Graphics.SetRenderTarget(this.m_motionRT);
	}

	private void OnPostRender()
	{
		this.UpdateRenderTextures();
		RenderTexture active = RenderTexture.get_active();
		this.m_motionRT.DiscardContents();
		Graphics.SetRenderTarget(this.m_motionRT);
		GL.Clear(true, false, Color.get_black());
		RenderManager.PostProcessRenderCamera.RenderVectors();
		Graphics.SetRenderTarget(active);
		RenderTexture.set_active(active);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.PostProcess(source, destination);
	}

	public void PostProcess(RenderTexture source, RenderTexture destination)
	{
		this.RenderMobile(source, destination, this.m_motionRT);
	}

	private void RenderMobile(RenderTexture source, RenderTexture destination, RenderTexture motion)
	{
		Graphics.Blit(motion, destination, this.DebugMaterial);
	}
}
