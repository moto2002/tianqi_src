using System;
using System.IO;

public static class DirectoryUtil
{
	public static void CreateIfNotExist(string dir)
	{
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}
	}

	public static void DeleteIfExist(string dir)
	{
		if (Directory.Exists(dir))
		{
			Directory.Delete(dir, true);
		}
	}

	public static void Copy(string src, string dst)
	{
		DirectoryUtil.CreateIfNotExist(dst);
		string[] files = Directory.GetFiles(src);
		string[] array = files;
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string fileName = Path.GetFileName(text);
			string text2 = PathUtil.Combine(new string[]
			{
				dst,
				fileName
			});
			File.Copy(text, text2);
		}
		string[] directories = Directory.GetDirectories(src);
		for (int j = 0; j < directories.Length; j++)
		{
			string text3 = directories[j];
			string fileName2 = Path.GetFileName(text3);
			string dst2 = PathUtil.Combine(new string[]
			{
				dst,
				fileName2
			});
			DirectoryUtil.Copy(text3, dst2);
		}
	}
}
