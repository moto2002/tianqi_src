using System;
using System.Collections.Generic;
using XEngine;

public class PreviousLoadingRes
{
	public static HashSet<string> GetAllAtlas()
	{
		HashSet<string> hashSet = new HashSet<string>();
		string path = FileSystem.GetPath("UiAtlas_KEY", string.Empty);
		string[] array = path.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				hashSet.Add(array[i]);
			}
		}
		return hashSet;
	}
}
