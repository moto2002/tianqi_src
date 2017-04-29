using System;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffectUtils
{
	public const float FLASH_LIGHT_BEGINTIME = 0f;

	private static string[] HitShaders = new string[]
	{
		"Toon/Lighted Outline",
		"Toon/Lighted Outline(HSV)",
		"Hsh(Mobile)/Character0/FlowLightWithTwirl",
		"Hsh(Mobile)/Character0/FlowLightWithTwir(Shadow)",
		"Toon/SelfIlluminationReflective(HSV)2",
		"Toon/SelfIlluminationReflective(HSV)2(Shadow)",
		"Hsh(Mobile)/Standard/Standard",
		"Toony Gooch/Toony Gooch RimLight",
		"Toony Gooch/Toony Gooch RimLight Outline",
		"MatCap/Vertex/Textured Multiply",
		"MatCap/Vertex/Textured Multiply Outline",
		"MatCap/Vertex/Textured Multiply(NoTransparent)",
		"Toony Gooch/Toony Gooch RimLight Spec",
		"Toony Gooch/Toony Gooch RimLight(alpha)"
	};

	public static bool OutlineIncludeNormal = false;

	private static string[] OutlineShaders = new string[]
	{
		"Toon/Basic Outline",
		"Toon/Lighted Outline",
		"Toon/Lighted Outline(HSV)",
		"Hsh(Mobile)/Character0/FlowLightWithTwirl",
		"Hsh(Mobile)/Character0/FlowLightWithTwir(Shadow)"
	};

	public static void SafeCreateRenderTexture(ref RenderTexture rt, string name, int depth, RenderTextureFormat fmt, RenderTextureReadWrite rw, int width, int height)
	{
		if (rt == null)
		{
			rt = new RenderTexture(width, height, depth, fmt, rw);
			rt.set_name(name);
			rt.set_wrapMode(1);
			rt.set_filterMode(1);
			rt.set_hideFlags(52);
			rt.Create();
		}
	}

	public static void SafeCreateMaterial(ref Material mat, ref Shader sd)
	{
		if (mat == null && sd != null)
		{
			mat = new Material(sd);
			mat.set_hideFlags(52);
		}
	}

	public static void SafeCreateMaterial(ref Material mat, string shaderName)
	{
		Shader shader = ShaderEffectUtils.FindShader(shaderName);
		ShaderEffectUtils.SafeCreateMaterial(ref mat, ref shader);
	}

	public static void SafeCreateTexture(ref Texture2D tex, string textureName)
	{
		if (tex == null)
		{
			tex = (ShaderEffectUtils.GetCodeTexture(textureName) as Texture2D);
		}
	}

	private static Texture GetCodeTexture(string name)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullTexture();
		}
		return AssetManager.GetTexture(name);
	}

	public static void SafeDestroyRenderTexture(ref RenderTexture rt)
	{
		if (rt != null)
		{
			Object.DestroyImmediate(rt);
			rt = null;
		}
	}

	public static void SafeDestroyTexture(ref Texture tex)
	{
		if (tex != null)
		{
			Object.DestroyImmediate(tex);
			tex = null;
		}
	}

	public static void SafeDestroyMaterial(ref Material mat)
	{
		if (mat != null)
		{
			Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	public static void SafeDestroyMaterial(Material mat)
	{
		if (mat != null)
		{
			Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	public static Shader FindShader(string name)
	{
		Shader shader = ShaderManager.Find(name);
		if (shader == null)
		{
			Debuger.Error("Error find shader : " + name, new object[0]);
			return null;
		}
		if (!shader.get_isSupported())
		{
			Debuger.Error("Shader not supported on this platform : " + name, new object[0]);
			return null;
		}
		return shader;
	}

	public static bool CheckSupport()
	{
		if (!SystemInfo.get_supportsImageEffects() || !SystemInfo.get_supportsRenderTextures())
		{
			Debuger.Error("Initialization failed. Requires support for Image Effects and Render Textures.", new object[0]);
			return false;
		}
		return true;
	}

	public static bool CheckMaterialAndShader(Material material, string name)
	{
		if (material == null || material.get_shader() == null)
		{
			Debuger.Error("Error creating material : " + name, new object[0]);
			return false;
		}
		if (!material.get_shader().get_isSupported())
		{
			Debuger.Error("Shader not supported on this platform : " + name, new object[0]);
			return false;
		}
		material.set_hideFlags(52);
		return true;
	}

	public static void InitShaderRenderers(Transform root, List<Renderer> shaderRenderers, ref Renderer shadowRenderer, ref ShadowSlicePlane shadowSlicePlane)
	{
		shaderRenderers.Clear();
		Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (renderer == null)
			{
				Debug.LogError("item is null");
			}
			else if (ShadowSlicePlaneMgr.IsShadow(renderer.get_gameObject()))
			{
				shadowRenderer = renderer;
				shadowSlicePlane = renderer.GetComponent<ShadowSlicePlane>();
				if (shadowSlicePlane == null)
				{
					Debug.LogError("shadowSlicePlane is null");
				}
			}
			else
			{
				shaderRenderers.Add(renderer);
			}
		}
	}

	public static void InitHitEffects(List<Renderer> shaderRenderers, List<ActorHitEffectBase> hitControls)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (shaderRenderers == null || shaderRenderers.get_Count() == 0)
		{
			return;
		}
		hitControls.Clear();
		for (int i = 0; i < shaderRenderers.get_Count(); i++)
		{
			if (!(shaderRenderers.get_Item(i) == null))
			{
				if (!LayerSystem.IsSpecialEffectLayers(shaderRenderers.get_Item(i).get_gameObject().get_layer()))
				{
					for (int j = 0; j < shaderRenderers.get_Item(i).get_sharedMaterials().Length; j++)
					{
						Material material = shaderRenderers.get_Item(i).get_sharedMaterials()[j];
						if (material != null && material.get_shader() != null)
						{
							ActorHitEffectBase actorHitEffectBase = null;
							Shader shader = material.get_shader();
							bool flag = false;
							if (!flag)
							{
								int k = 0;
								while (k < ShaderEffectUtils.HitShaders.Length)
								{
									if (shader.get_name().Equals(ShaderEffectUtils.HitShaders[k]))
									{
										if (actorHitEffectBase != null && actorHitEffectBase is ActorHitEffect)
										{
											actorHitEffectBase.set_enabled(true);
											flag = true;
											break;
										}
										if (actorHitEffectBase != null)
										{
											actorHitEffectBase.set_enabled(false);
										}
										actorHitEffectBase = shaderRenderers.get_Item(i).get_gameObject().AddMissingComponent<ActorHitEffect>();
										flag = true;
										break;
									}
									else
									{
										k++;
									}
								}
							}
							if (flag && actorHitEffectBase != null)
							{
								actorHitEffectBase.Init(shaderRenderers.get_Item(i));
								hitControls.Add(actorHitEffectBase);
								break;
							}
						}
					}
				}
			}
		}
	}

	public static void SetHitEffects(List<ActorHitEffectBase> listBase)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (listBase == null || listBase.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < listBase.get_Count(); i++)
		{
			if (listBase.get_Item(i) != null)
			{
				listBase.get_Item(i).SetActorHitEffect();
			}
		}
	}

	public static void InitTransparencys(List<Renderer> renderers, List<AdjustTransparency> alphaControls)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (renderers == null || renderers.get_Count() == 0)
		{
			return;
		}
		alphaControls.Clear();
		for (int i = 0; i < renderers.get_Count(); i++)
		{
			Renderer renderer = renderers.get_Item(i);
			if (renderer == null)
			{
				Debug.LogError("item is null");
			}
			else if (renderer.get_gameObject().get_activeSelf())
			{
				if (!LayerSystem.IsSpecialEffectLayers(renderer.get_gameObject().get_layer()))
				{
					if (renderer.get_materials() != null && renderer.get_materials().Length > 0)
					{
						AdjustTransparency adjustTransparency = renderer.get_gameObject().AddMissingComponent<AdjustTransparency>();
						adjustTransparency.Init(renderer);
						alphaControls.Add(adjustTransparency);
					}
				}
			}
		}
	}

	public static void SetIsNearCamera(Transform root, bool isHide = false)
	{
		if (!SystemConfig.IsFindOcclusionByCameraOn)
		{
			return;
		}
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (root == null)
		{
			return;
		}
		Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (!LayerSystem.IsSpecialEffectLayers(renderer.get_gameObject().get_layer()) && !ShadowSlicePlaneMgr.IsShadow(renderer.get_gameObject()))
			{
				Material[] materials = renderer.get_materials();
				if (materials != null && materials.Length > 0)
				{
					AdjustTransparency adjustTransparency = renderer.get_gameObject().AddMissingComponent<AdjustTransparency>();
					adjustTransparency.SetIsNearCamera(isHide);
				}
			}
		}
	}

	public static void SetFadeRightNow(List<AdjustTransparency> alphaControls, bool isHide)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (alphaControls == null)
		{
			return;
		}
		for (int i = 0; i < alphaControls.get_Count(); i++)
		{
			if (alphaControls.get_Item(i) != null)
			{
				alphaControls.get_Item(i).SetFadeRightNow(isHide);
			}
		}
	}

	public static void SetFade(List<AdjustTransparency> alphaControls, bool isHide, Action action = null)
	{
		if (alphaControls.get_Count() == 0)
		{
			if (action != null)
			{
				action.Invoke();
			}
			return;
		}
		bool flag = false;
		for (int i = 0; i < alphaControls.get_Count(); i++)
		{
			AdjustTransparency adjustTransparency = alphaControls.get_Item(i);
			if (!(adjustTransparency == null))
			{
				if (adjustTransparency.get_enabled())
				{
					if (!flag && adjustTransparency.get_gameObject().get_activeSelf() && adjustTransparency.IsSettingAction())
					{
						flag = true;
						adjustTransparency.actionFadeFinish = action;
					}
					adjustTransparency.InFade = true;
					adjustTransparency.SetFade(isHide);
				}
			}
		}
		if (!flag)
		{
			if (action != null)
			{
				action.Invoke();
			}
			return;
		}
	}

	public static void SetOutlineStatus(Transform actorTarget, LuminousOutline.Status status)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (!SystemConfig.IsOutlineStatusOn)
		{
			return;
		}
		if (actorTarget == null)
		{
			return;
		}
		Renderer[] componentsInChildren = actorTarget.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			Material shareMaterial = Utils.GetShareMaterial(renderer);
			if (shareMaterial != null && shareMaterial.get_shader() != null)
			{
				Shader shader = shareMaterial.get_shader();
				for (int j = 0; j < ShaderEffectUtils.OutlineShaders.Length; j++)
				{
					if (shader.get_name().Equals(ShaderEffectUtils.OutlineShaders[j]))
					{
						LuminousOutline luminousOutline = renderer.get_gameObject().AddMissingComponent<LuminousOutline>();
						luminousOutline.SetStatus(ref shareMaterial, status);
						break;
					}
				}
			}
		}
	}

	public static void SetHSV(Transform tran, List<float> colour)
	{
		if (colour != null && colour.get_Count() >= 3)
		{
			Renderer[] componentsInChildren = tran.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Renderer renderer = componentsInChildren[i];
				Material material = renderer.get_material();
				if (material != null && material.get_shader() != null)
				{
					material.SetFloat(ShaderPIDManager._HueShift, colour.get_Item(0));
					material.SetFloat(ShaderPIDManager._Sat, colour.get_Item(1));
					material.SetFloat(ShaderPIDManager._Val, colour.get_Item(2));
				}
			}
		}
	}

	public static void SetMeshRenderToLayerFXDistortion(Transform tran)
	{
		if (!SystemConfig.IsShaderEffectOn)
		{
			return;
		}
		if (tran == null)
		{
			return;
		}
		PostProcessManager.Instance.EnableLocalHeatDistortion(true);
		Renderer[] componentsInChildren = tran.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			renderer.get_gameObject().set_layer(LayerSystem.NameToLayer("FX_Distortion"));
		}
	}

	public static void SwitchShader(Material[] m_materials, bool transparent, ShaderManager.ShaderType shaderType)
	{
		if (shaderType == ShaderManager.ShaderType.None)
		{
			return;
		}
		if (!transparent)
		{
			if (shaderType == ShaderManager.ShaderType.TextureMult)
			{
				ShaderEffectUtils.SetShader(m_materials, "MatCap/Vertex/Textured Multiply(NoTransparent)");
			}
		}
		else if (shaderType == ShaderManager.ShaderType.TextureMult)
		{
			ShaderEffectUtils.SetShader(m_materials, "MatCap/Vertex/Textured Multiply");
		}
	}

	public static void SetShader(Material[] m_materials, string name)
	{
		for (int i = 0; i < m_materials.Length; i++)
		{
			if (m_materials[i] != null && m_materials[i].get_shader() != null)
			{
				m_materials[i].set_shader(ShaderManager.Find(name));
			}
		}
	}

	public static void CheckInternalErrorShader(Material[] m_materials)
	{
		for (int i = 0; i < m_materials.Length; i++)
		{
			if (m_materials[i] != null && m_materials[i].get_shader() != null && m_materials[i].get_shader().get_name() == "Hidden/InternalErrorShader")
			{
				m_materials[i].set_shader(ShaderManager.Find("MatCap/Vertex/Textured Multiply(NoTransparent)"));
			}
		}
	}

	public static Material CreateAutoFlashLightMat(float timeOnce = 0.4f, float interval = 5f)
	{
		Material material = new Material(ShaderManager.Find("Hsh(Mobile)/UI/AutoFlashLight"));
		material.SetFloat(ShaderPIDManager._BeginTime, 0f);
		material.SetFloat(ShaderPIDManager._Interval, interval);
		material.SetFloat(ShaderPIDManager._TimeOnce, timeOnce);
		return material;
	}
}
