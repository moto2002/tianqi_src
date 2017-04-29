using System;
using System.IO;
using UnityEngine;

public class ABUtility
{
	public static string GetRelativePath()
	{
		if (Application.get_isEditor())
		{
			return Environment.get_CurrentDirectory().Replace("\\", "/");
		}
		if (Application.get_isWebPlayer())
		{
			return Path.GetDirectoryName(Application.get_absoluteURL()).Replace("\\", "/") + "/StreamingAssets";
		}
		if (Application.get_isMobilePlatform() || Application.get_isConsolePlatform())
		{
			return Application.get_streamingAssetsPath();
		}
		return Application.get_streamingAssetsPath();
	}

	public static string GetPlatformFolderForAssetBundles()
	{
		switch (Application.get_platform())
		{
		case 8:
			return "iOS";
		case 11:
			return "Android";
		}
		return "Windows";
	}
}
