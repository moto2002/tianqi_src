using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using XEngine;

public class ShaderManager : MonoBehaviour
{
	public enum ShaderType
	{
		None,
		TextureMult
	}

	public const string PrefabName = "Shader";

	public const string TextureMult = "MatCap/Vertex/Textured Multiply";

	public const string TextureMultNoTransparent = "MatCap/Vertex/Textured Multiply(NoTransparent)";

	public const string SequenceFramesAdd = "Hsh(Mobile)/FX/SequenceFramesAdd";

	public const string SequenceFramesAdd_3 = "Hsh(Mobile)/FX/SequenceFramesAdd_3";

	public const string SequenceFramesAlphaBlended = "Hsh(Mobile)/FX/SequenceFramesAlphaBlended";

	public const string SequenceFramesAlphaBlended_3 = "Hsh(Mobile)/FX/SequenceFramesAlphaBlended_3";

	public const string ParticlesAdditive = "Particles/Additive";

	public const string ParticleAlphaBlended = "Hsh(Mobile)/FX/ParticleAlphaBlended";

	public const string ParticleAdditive = "Hsh(Mobile)/FX/ParticleAdditive";

	public const string DiffuseUnlit = "Hsh(Mobile)/Environment/Diffuse-Unlit";

	public const string UIRT = "Hsh(Mobile)/UI/UIRT";

	public const string UIRT_NoAlpha = "Hsh(Mobile)/UI/UIRT_NoAlpha";

	public const string UI = "Hsh(Mobile)/UI/UI";

	public const string UIETC = "Hsh(Mobile)/UI/UIETC";

	public const string UIStencilETC = "Hsh(Mobile)/UI/UIStencilETC";

	public const string UIStencilFont = "Hsh(Mobile)/UI/UIStencilFont";

	public const string UIStencilMask = "Hsh(Mobile)/UI/UIStencilMask";

	public const string UIStencilAlphaBlended = "Hsh(Mobile)/UI/UIStencilAlphaBlended";

	public const string UIStencilAdd = "Hsh(Mobile)/UI/UIStencilAdd";

	public const string UISpineAlphaBlended = "Hsh(Mobile)/UI/UISpineAlphaBlended";

	public const string UISpineAdd = "Hsh(Mobile)/UI/UISpineAdd";

	public const string BlurPassesForDOF = "Hidden/BlurPassesForDOF";

	public const string HeightDepthOfField = "Hidden/HeightDepthOfField";

	public const string LocalHeatDistortion = "Hidden/LocalHeatDistortion";

	public const string MobileGray = "Hidden/MobileGray";

	public const string MobileBloom = "Hidden/MobileBloom";

	public const string FastBloom = "Hidden/FastBloom";

	public const string MobileBlurBlurry = "Hidden/MobileBlurBlurry";

	public const string MobileBlurBlurry_Culling = "Hidden/MobileBlurBlurry_Culling";

	public const string RadialBlur = "Hidden/RadialBlur";

	public const string DepthOfFieldHdr = "Hidden/Dof/DepthOfFieldHdr";

	public const string VignettingShader = "Hidden/Vignetting";

	public const string SeparableBlur = "Hidden/SeparableBlur";

	public const string ChromaticAberrationShader = "Hidden/ChromaticAberration";

	public const string Disintegrate = "Hsh(Mobile)/Character/Disintegrate";

	public const string Hologram = "Hsh(Mobile)/Character/Hologram";

	public const string AutoFlashLight = "Hsh(Mobile)/UI/AutoFlashLight";

	public const string SSAA = "Hidden/SSAA";

	public const string FXAA = "Hidden/FXAA III (Console)";

	public const string ImageBlend = "Hidden/ImageBlend";

	public const string FrostImageBlend = "Hidden/FrostImageBlend";

	public const string Vortex = "Hidden/Vortex";

	public const string HeatDistortion = "Hidden/HeatDistortion";

	public const string Holywood = "Hidden/Holywood";

	public const string HolywoodOfRT = "Hidden/HolywoodOfRT";

	public const string Vignette = "Hidden/Vignette";

	public const string VignetteOfRT = "Hidden/VignetteOfRT";

	public const string FastBlur = "Hidden/FastBlur";

	public const string MobileBlur_Noise = "Hidden/MobileBlur_Noise";

	public const string Desaturate = "Hidden/Desaturate";

	public string[] listShaderName;

	public string[] listPrefabName;

	public Shader[] listShader;

	public Dictionary<string, int> listNameToIndex = new Dictionary<string, int>();

	public static ShaderManager Instance;

	public void Init()
	{
		this.listShader = new Shader[this.listPrefabName.Length];
	}

	public int FindIndex(string shader_name)
	{
		for (int i = 0; i < this.listShaderName.Length; i++)
		{
			if (this.listShaderName[i] == shader_name)
			{
				return i;
			}
		}
		return -1;
	}

	public Shader FindShader(string shader_name)
	{
		if (!this.listNameToIndex.ContainsKey(shader_name))
		{
			int num = this.FindIndex(shader_name);
			if (num >= 0 && num < this.listPrefabName.Length)
			{
				Debug.Log("==>shader load, shader prefab = " + this.listPrefabName[num]);
				Shader shader = ShaderManager.LoadShader(this.listPrefabName[num]);
				if (shader != null)
				{
					this.listShader[num] = shader;
					this.listNameToIndex.set_Item(shader_name, num);
					return shader;
				}
			}
			this.listNameToIndex.set_Item(shader_name, -1);
			return null;
		}
		if (this.listNameToIndex.get_Item(shader_name) < 0)
		{
			return null;
		}
		return this.listShader[this.listNameToIndex.get_Item(shader_name)];
	}

	public static ShaderManager.ShaderType GetShaderType(string shaderName)
	{
		if (shaderName == "MatCap/Vertex/Textured Multiply" || shaderName == "MatCap/Vertex/Textured Multiply(NoTransparent)")
		{
			return ShaderManager.ShaderType.TextureMult;
		}
		return ShaderManager.ShaderType.None;
	}

	public static void BeginInit(Action initedCallback)
	{
		Debug.Log("==>ShaderManager.BeginInit01");
		AssetManager.AssetOfNoPool.LoadAsset(FileSystem.GetPath("Shader.shader", string.Empty), typeof(Object), delegate(Object obj)
		{
			Debug.Log("==>ShaderManager.BeginInit02");
			GameObject gameObject = obj as GameObject;
			if (gameObject == null)
			{
				Debug.LogError("==>shader asset is null, name = Shader");
				return;
			}
			ShaderManager.InitInstanceFromGO(gameObject);
			if (initedCallback != null)
			{
				initedCallback.Invoke();
			}
		});
	}

	private static void InitInstanceFromGO(GameObject go)
	{
		if (go == null)
		{
			Debug.LogError("==>shader asset gameobject is null");
			return;
		}
		ShaderManager.Instance = go.GetComponent<ShaderManager>();
		if (ShaderManager.Instance != null)
		{
			Debug.Log("==>ShaderManager.InitInstanceFromGO");
			ShaderManager.Instance.Init();
		}
		else
		{
			Debug.LogError("ShaderManager script is null");
		}
	}

	public static Shader Find(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		if (ShaderManager.Instance == null)
		{
			GameObject go = AssetManager.AssetOfNoPool.LoadAssetNow(FileSystem.GetPath("Shader", string.Empty), typeof(Object)) as GameObject;
			ShaderManager.InitInstanceFromGO(go);
		}
		return ShaderManager.Instance.FindShader(name);
	}

	public static Shader LoadShader(string prefabName)
	{
		GameObject gameObject = AssetManager.AssetOfNoPool.LoadAssetNow(FileSystem.GetPath(prefabName + ".shader", string.Empty), typeof(Object)) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("shader prefab is null, name = " + prefabName);
			return null;
		}
		return gameObject.GetComponent<ShaderPrefab>().shader;
	}

	[DebuggerHidden]
	public IEnumerator WarmUp(Action<int, int> progress, Action finished)
	{
		ShaderManager.<WarmUp>c__Iterator36 <WarmUp>c__Iterator = new ShaderManager.<WarmUp>c__Iterator36();
		<WarmUp>c__Iterator.progress = progress;
		<WarmUp>c__Iterator.finished = finished;
		<WarmUp>c__Iterator.<$>progress = progress;
		<WarmUp>c__Iterator.<$>finished = finished;
		return <WarmUp>c__Iterator;
	}
}
