using System;
using XEngine;

public class ConstTP
{
	public const string suffix_atlas = "_atlas";

	public const string suffix_atlas_alpha = "_atlas_alpha";

	public const string suffix_data = "_data";

	public const string suffix_prefab = "_pb";

	public const string suffix_material = "_material";

	public static string suffix_atlas_To_suffix_prefab(string str)
	{
		return str.Substring(0, str.get_Length() - "_atlas".get_Length()) + "_pb";
	}

	public static string suffix_atlas_To_src(string src_with_suffix_atlas)
	{
		int num = src_with_suffix_atlas.get_Length() - "_atlas".get_Length();
		if (src_with_suffix_atlas.get_Length() > num)
		{
			return src_with_suffix_atlas.Substring(0, num);
		}
		return string.Empty;
	}

	public static string src_To_suffix_atlas(string src)
	{
		return src + "_atlas";
	}

	public static string src_To_suffix_prefab(string src)
	{
		return FileSystem.GetPath(src + "_pb", string.Empty);
	}
}
