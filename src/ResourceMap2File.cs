using System;
using System.Collections;
using UnityEngine;
using XEngine;

public class ResourceMap2File
{
	private static Hashtable _AllResources;

	public static Hashtable AllResources
	{
		get
		{
			if (ResourceMap2File._AllResources == null)
			{
				ResourceMap2File._AllResources = new Hashtable();
			}
			return ResourceMap2File._AllResources;
		}
	}

	public static Hashtable GetAllResources()
	{
		ResourceMap2File.AllResources.Clear();
		ResourceMap2File.ResourcesOfUI();
		ResourceMap2File.ResourcesOfTexture();
		ResourceMap2File.ResourcesOfTPAtlas();
		ResourceMap2File.ResourcesOfShaders();
		ResourceMap2File.ResourcesOfShaderEffects();
		ResourceMap2File.ResourcesOfSceneLightmaps();
		ResourceMap2File.ResourcesOfAudios();
		ResourceMap2File.ResourcesOfModels();
		ResourceMap2File.ResourcesOfAnimatorController();
		ResourceMap2File.ResourcesOffxs();
		return ResourceMap2File.AllResources;
	}

	private static void Add2AllResources(Hashtable names)
	{
		IEnumerator enumerator = names.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.get_Current();
				if (!ResourceMap2File.AllResources.Contains(current))
				{
					ResourceMap2File.AllResources.set_Item(current, names.get_Item(current));
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	private static void ResourcesOfUI()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_prefab);
		ResBase.RootPath = PathSystem.GetReservedPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_prefab);
		ResBase.RootPath = PathSystem.GetUIPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_prefab);
		ResBase.RootPath = PathSystem.GetUI3DPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_prefab);
		ResBase.RootPath = PathSystem.GetReservedPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_material);
		ResBase.RootPath = PathSystem.GetUI3DPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_controller);
		ResBase.RootPath = PathSystem.GetUIAnimRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(FileSystem.key_suffix_prefab);
		ResBase.RootPath = PathSystem.GetSpineRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, FileSystem.key_suffix_spine));
	}

	private static void ResourcesOfTexture()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".png");
		ResBase.Suffixs.Add(".jpg");
		ResBase.Suffixs.Add(".tga");
		ResBase.Suffixs.Add(".psd");
		ResBase.RootPath = PathSystem.GetTextureRGBARoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, string.Empty));
		ResBase.RootPath = PathSystem.GetTextureRGBRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, string.Empty));
		ResBase.RootPath = PathSystem.GetTextureShaderRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, string.Empty));
		ResBase.RootPath = PathSystem.GetReservedPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, string.Empty));
	}

	private static void ResourcesOfTPAtlas()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".png");
		ResBase.Suffixs.Add(".jpg");
		ResBase.Suffixs.Add(".PNG");
		ResBase.Suffixs.Add(".JPG");
		ResBase.RootPath = PathSystem.GetSrcSpriteRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfFolder(true, true));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".png");
		ResBase.Suffixs.Add(".jpg");
		ResBase.Suffixs.Add(".PNG");
		ResBase.Suffixs.Add(".JPG");
		ResBase.RootPath = PathSystem.GetSrcSpriteRGBRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfFolder(true, true));
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetTPAtlasRoot(PathType.absPath);
		Hashtable namesOfPath = ResBase.GetNamesOfPath(true, "*", true, string.Empty);
		ResourceMap2File.Add2AllResources(namesOfPath);
		string text = string.Empty;
		IEnumerator enumerator = namesOfPath.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.get_Current();
				string text2 = current.ToString();
				text2 = text2.Substring(0, text2.get_Length() - "_pb".get_Length());
				text = text + text2 + ';';
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		ResourceMap2File.AllResources.set_Item("UiAtlas_KEY", text);
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetReservedPrefabRoot(PathType.absPath);
		namesOfPath = ResBase.GetNamesOfPath(true, "*", true, string.Empty);
		ResourceMap2File.Add2AllResources(namesOfPath);
	}

	private static void ResourcesOfShaders()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetShaderPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(true, "*", false, ".shader"));
	}

	private static void ResourcesOfShaderEffects()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetShaderEffectRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
	}

	private static void ResourcesOfSceneLightmaps()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetSceneRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "s*", false, string.Empty));
	}

	private static void ResourcesOfAudios()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".wav");
		ResBase.Suffixs.Add(".mp3");
		ResBase.RootPath = PathSystem.GetAudioRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
	}

	private static void ResourcesOfModels()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetActorPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
	}

	private static void ResourcesOfAnimatorController()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".controller");
		ResBase.RootPath = PathSystem.GetActorAnimationRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
	}

	private static void ResourcesOffxs()
	{
		ResBase.Suffixs.Clear();
		ResBase.Suffixs.Add(".prefab");
		ResBase.RootPath = PathSystem.GetGameEffectPrefabRoot(PathType.absPath);
		ResourceMap2File.Add2AllResources(ResBase.GetNamesOfPath(false, "*", false, string.Empty));
	}

	public static Hashtable ExportJSON_ResourceMap(string path)
	{
		Debug.Log("ExportJSON_ResourceMap");
		Hashtable allResources = ResourceMap2File.GetAllResources();
		ExportJsonTools.ExportJSON(path, allResources);
		return allResources;
	}
}
