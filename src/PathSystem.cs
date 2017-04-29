using System;
using System.IO;
using UnityEngine;

public class PathSystem
{
	public class RootOfResources
	{
		public static readonly string RESOURCE_FOLDER_PREFIX = "Assets/Resources/";

		public static readonly string UIPrefabPath = "UGUI/Prefabs";

		public static readonly string RootOfTPUiSprite = "UGUI/Res/TPAtlas";

		public static readonly string GameEffectResourcePath = "GameEffect";

		public static readonly string Reserved = PathSystem.ReservedName + "/";
	}

	public class RootOfU3DNoAssets
	{
		public static readonly string RootOfReserved = "/Resources/" + PathSystem.ReservedName;

		public static readonly string RootOfSrcUiSprite = "/Src[UGUI]/Res";

		public static readonly string RootOfSrcUiSpriteRGB = "/Src[UGUI]/ResRGB";

		public static readonly string RootOfSrcUiEffect = "/Src[UGUI]/UIEffects";

		public static readonly string RootOfSrcSpriteIcon = "/Src[UGUI]/Res/Icon";

		public static readonly string RootOfUiPrefab = "/Resources/UGUI/Prefabs";

		public static readonly string RootOfUi3DPrefab = "/Resources/UGUI/Prefab3D";

		public static readonly string RootOfUiFonts = "/Resources/UGUI/FontsFile";

		public static readonly string RootOfTPUiSprite = "/Resources/UGUI/Res/TPAtlas";

		public static readonly string RootOfTPUiEffect = "/Resources/UGUI/Res/TPAtlasEffect";

		public static readonly string RootOfUIAnim = "/Resources/UGUI/UIAnimation";

		public static readonly string RootOfShaderEffect = "/Resources/ShaderEffect";

		public static readonly string RootOfShader = "/Resources/Shader";

		public static readonly string RootOfShaderPrefab = "/Resources/Shader/ShaderPrefab";

		public static readonly string RootOfTextureRGBA = "/Resources/UGUI/Res/Textures";

		public static readonly string RootOfTextureRGB = "/Resources/UGUI/Res/TexturesRGB";

		public static readonly string RootOfTextureShader = "/Resources/UGUI/Res/TexturesShader";

		public static readonly string RootOfSpinePrefab = "/Resources/UGUI/PrefabSpine2d";

		public static readonly string RootOfSrcSpine = "/Src[spine2d]/SpineSrc";

		public static readonly string RootOfOutputSpine = "/Src[spine2d]/SpineOutput";

		public static readonly string MapPointData = "/Resources/MapPointData";

		public static readonly string ModelData = "/Resources/ModelData";

		public static readonly string Camera = "/Resources/Camera";

		public static readonly string RootOfActorAnim2OldModel = "/Resources/Actor/Model";

		public static readonly string RootOfActorAnim2SrcModel = "/Src[model]/";

		public static readonly string RootOfActorPrefab = "/Resources/Actor/Prefabs";

		public static readonly string RootOfActorAnimation = "/Resources/Actor/Controllers";

		public static readonly string RootOfSceneModelTexture = "/Resources/Envi/Model";

		public static readonly string RootOfScene = "/Resources/Scenes";

		public static readonly string GameEffectPrefabRoot = "/Resources/GameEffect/Prefabs";

		public static readonly string AudioRoot = "/Resources/Audio";

		public static readonly string FontPrefabRoot = "/Resources/UGUI/FontsFile";

		public static readonly string GameDataRoot = "/StreamingAssets/Data/";

		public static readonly string GameDataCsRoot = "/Libs/GameData/";
	}

	public class RootOfU3DHasAssets
	{
		public const string SpineOutputs = "Assets/Src[spine2d]/SpineOutput";
	}

	public class SubPackageInfoFile
	{
		public static readonly string CoreList = "CoreList.txt";

		public static readonly string ExtendList = "ExtendList.txt";

		public static readonly string ReservedList = "CoreReservedList.txt";
	}

	public static readonly string ReservedName = "Reserved";

	private static readonly string FullArtUIRootDir = "/../ArtsResources";

	private static readonly string[] designPath = new string[]
	{
		"E:/Project3/design",
		"/../../../../design",
		"/../../../design",
		"D:/workspace/project3/design",
		"E:/pr3/design"
	};

	private static readonly string SrcGameDataRootDir = "/run/data";

	private static readonly string SrcGameDataCsRootDir = "/run/client";

	public static readonly string ApkPatchInfoFileName = "patch_info.json";

	public static readonly string ClientExtendVersion = "client_extend_version.txt";

	public static string PersistentDataPath
	{
		get;
		private set;
	}

	public static string DataPath
	{
		get;
		private set;
	}

	public static string ApkDir
	{
		get
		{
			return PathUtil.Combine(new string[]
			{
				PathSystem.PersistentDataPath,
				"Apk"
			});
		}
	}

	public static string ApkPatchDir
	{
		get
		{
			return PathUtil.Combine(new string[]
			{
				PathSystem.PersistentDataPath,
				"apkPatch"
			});
		}
	}

	public static void Init()
	{
		PathSystem.PersistentDataPath = Application.get_persistentDataPath();
		PathSystem.DataPath = Application.get_dataPath();
	}

	public static string GetSrcUiEffectsRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSrcUiEffect);
	}

	public static string GetSrcSpriteRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSrcUiSprite);
	}

	public static string GetSrcSpriteRGBRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSrcUiSpriteRGB);
	}

	public static string GetTextureRGBARoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfTextureRGBA);
	}

	public static string GetTextureRGBRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfTextureRGB);
	}

	public static string GetTextureShaderRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfTextureShader);
	}

	public static string GetSrcSpriteIconRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSrcSpriteIcon);
	}

	public static string GetTPAtlasRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfTPUiSprite);
	}

	public static string GetTPAtlasEffectRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfTPUiEffect);
	}

	public static string GetUIPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfUiPrefab);
	}

	public static string GetReservedPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfReserved);
	}

	public static string GetUI3DPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfUi3DPrefab);
	}

	public static string GetUIAnimRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfUIAnim);
	}

	public static string GetSpineRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSpinePrefab);
	}

	public static string GetSrcSpineRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSrcSpine);
	}

	public static string GetOutputSpineRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfOutputSpine);
	}

	public static string GetShaderRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfShader);
	}

	public static string GetShaderPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfShaderPrefab);
	}

	public static string GetShaderEffectRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfShaderEffect);
	}

	public static string GetAudioRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.AudioRoot);
	}

	public static string GetFontRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.FontPrefabRoot);
	}

	public static string GetSceneModelTextureRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfSceneModelTexture);
	}

	public static string GetActorOldAnimModelRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfActorAnim2OldModel);
	}

	public static string GetActorSrcAnimModelRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfActorAnim2SrcModel);
	}

	public static string GetActorPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfActorPrefab);
	}

	public static string GetActorAnimationRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfActorAnimation);
	}

	public static string GetArtUIRootDir2Abs()
	{
		return Application.get_dataPath() + PathSystem.FullArtUIRootDir;
	}

	public static string GetGameEffectPrefabRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.GameEffectPrefabRoot);
	}

	public static string GetSceneRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.RootOfScene);
	}

	public static string GetGameDataRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.GameDataRoot);
	}

	public static string GetGameDataCsRoot(PathType type)
	{
		return PathSystem.GetPath(type, PathSystem.RootOfU3DNoAssets.GameDataCsRoot);
	}

	public static string GetSrcGameDataRootDir2Abs()
	{
		for (int i = 0; i < PathSystem.designPath.Length; i++)
		{
			string text = PathSystem.designPath[i] + PathSystem.SrcGameDataRootDir;
			if (Directory.Exists(text))
			{
				return text;
			}
		}
		return string.Empty;
	}

	public static string GetSrcGameDataCsRootDir2Abs()
	{
		for (int i = 0; i < PathSystem.designPath.Length; i++)
		{
			string text = PathSystem.designPath[i] + PathSystem.SrcGameDataCsRootDir;
			if (Directory.Exists(text))
			{
				return text;
			}
		}
		return string.Empty;
	}

	public static string GetPath(PathType type, string u3dPathOfnoasset)
	{
		switch (type)
		{
		case PathType.absPath:
			return Application.get_dataPath() + u3dPathOfnoasset;
		case PathType.u3dPath_no_assets:
			return u3dPathOfnoasset;
		case PathType.u3dPath_has_asset:
		{
			string path = PathSystem.GetPath(PathType.absPath, u3dPathOfnoasset);
			return path.Substring(path.IndexOf("Assets"));
		}
		case PathType.resourcesPath:
			if (u3dPathOfnoasset.Contains("/Resources/"))
			{
				return u3dPathOfnoasset.Substring("/Resources/".get_Length());
			}
			PathSystem.ShowError();
			return string.Empty;
		case PathType.SubPackageInfo:
			return Path.Combine(Environment.get_CurrentDirectory(), string.Format("SubPackageInfo/{0}", u3dPathOfnoasset));
		case PathType.ApkPath:
			return PathUtil.Combine(new string[]
			{
				PathSystem.ApkDir,
				u3dPathOfnoasset
			});
		case PathType.ApkPatchPath:
			return PathUtil.Combine(new string[]
			{
				PathSystem.ApkPatchDir,
				u3dPathOfnoasset
			});
		case PathType.ResPatch:
			return PathUtil.Combine(new string[]
			{
				PathSystem.PersistentDataPath,
				"resPatch",
				u3dPathOfnoasset
			});
		default:
			return string.Empty;
		}
	}

	private static void ShowError()
	{
		Debug.LogError("no path on this type");
	}

	public static string GetSubPackageInfoPath(string fileName)
	{
		return PathSystem.GetPath(PathType.SubPackageInfo, fileName);
	}

	public static string GetEditorDataDir()
	{
		return Path.Combine(Environment.get_CurrentDirectory(), Singleton<EditorConfig>.S.Data.AssetBundlePath);
	}
}
