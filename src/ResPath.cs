using System;
using XEngine;

public class ResPath
{
	public const string UiAtlas_KEY = "UiAtlas_KEY";

	public const char CutResourcesOfSymbol = ';';

	public const string DEFAULT_SPRITE_NAME = "99999";

	public static string GetEffectFolderName(string fxPrefix)
	{
		string name = fxPrefix + "_00000";
		return FileSystem.GetPath(name, string.Empty);
	}
}
