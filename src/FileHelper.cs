using System;
using System.IO;

public static class FileHelper
{
	public static void DeleteIfExist(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	public static void CreateIfNotExist(string path)
	{
		if (!File.Exists(path))
		{
			File.Create(path).Close();
		}
	}
}
