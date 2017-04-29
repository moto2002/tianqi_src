using System;
using System.IO;

public static class PathUtil
{
	public static string absToU3d(string absPath)
	{
		if (absPath.Contains("Assets"))
		{
			return absPath.Substring(absPath.IndexOf("Assets"));
		}
		return null;
	}

	public static string absToAssetPath(string absPath)
	{
		if (absPath.Contains("Assets"))
		{
			return absPath.Substring(absPath.IndexOf("Assets") + "Assets".get_Length() + 1);
		}
		return null;
	}

	public static string GetPathWithoutExtension(string path)
	{
		return (Path.GetDirectoryName(path) + "/" + Path.GetFileNameWithoutExtension(path)).Replace("\\", "/");
	}

	public static string AssetPathToResourcesName(string assetPath)
	{
		return PathUtil.GetPathWithoutExtension(PathUtil.absToAssetPath(assetPath).Substring("Resources/".get_Length()));
	}

	public static string NormalizingPath(string path)
	{
		return path.Replace('\\', '/');
	}

	public static string NormalizingDir(string dir)
	{
		int num = dir.get_Length() - 1;
		char c = dir.get_Chars(num);
		if (c == '\\' || c == '/')
		{
			return PathUtil.NormalizingPath(dir.Remove(num));
		}
		return PathUtil.NormalizingPath(dir);
	}

	public static string GetRelativePath(string absPath, string root)
	{
		string text = PathUtil.NormalizingDir(root);
		string text2 = PathUtil.NormalizingPath(absPath);
		return text2.Substring(text.get_Length() + 1);
	}

	public static string Combine(params string[] args)
	{
		string text = args[0];
		for (int i = 1; i < args.Length; i++)
		{
			text = Path.Combine(text, args[i]);
		}
		return PathUtil.NormalizingPath(text);
	}
}
