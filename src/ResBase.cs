using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ResBase
{
	public static List<string> Suffixs = new List<string>();

	public static string RootPath = string.Empty;

	private static Hashtable Names = new Hashtable();

	public static Hashtable GetNamesOfPath(bool no_suffix_key, string searchPattern = "*", bool key_tolower = false, string custom_suffix = "")
	{
		ResBase.Names.Clear();
		DirectoryInfo dirInfo = new DirectoryInfo(ResBase.RootPath);
		for (int i = 0; i < ResBase.Suffixs.get_Count(); i++)
		{
			ResBase.AddOnesOfPath(dirInfo, ResBase.Suffixs.get_Item(i), no_suffix_key, searchPattern, key_tolower, custom_suffix);
		}
		return ResBase.Names;
	}

	public static Hashtable GetNamesOfFolder(bool key_no_suffix = false, bool folder_name_tolower = false)
	{
		ResBase.Names.Clear();
		DirectoryInfo dirInfo = new DirectoryInfo(ResBase.RootPath);
		for (int i = 0; i < ResBase.Suffixs.get_Count(); i++)
		{
			ResBase.AddOnesOfFolder(dirInfo, ResBase.Suffixs.get_Item(i), key_no_suffix, folder_name_tolower);
		}
		return ResBase.Names;
	}

	private static void AddOnesOfPath(DirectoryInfo dirInfo, string suffix, bool key_no_suffix, string searchPattern = "*", bool key_tolower = false, string custom_suffix = "")
	{
		if (dirInfo == null)
		{
			return;
		}
		FileInfo[] files = dirInfo.GetFiles(searchPattern + suffix, 1);
		for (int i = 0; i < files.Length; i++)
		{
			FileInfo fileInfo = files[i];
			if (!fileInfo.get_Name().EndsWith("meta"))
			{
				string text = fileInfo.get_Name();
				if (key_no_suffix)
				{
					text = text.Remove(text.get_Length() - suffix.get_Length());
				}
				if (!ResBase.Names.Contains(text))
				{
					if (key_tolower)
					{
						text = text.ToLower();
					}
					text += custom_suffix;
					ResBase.AddNameOfPath(text, fileInfo.get_FullName().Remove(fileInfo.get_FullName().get_Length() - suffix.get_Length()).Replace("\\", "/"));
				}
			}
		}
	}

	private static void AddNameOfPath(string key, string fullName)
	{
		string text = PathUtil.absToU3d(fullName);
		if (text.get_Length() > PathSystem.RootOfResources.RESOURCE_FOLDER_PREFIX.get_Length())
		{
			text = text.Substring(PathSystem.RootOfResources.RESOURCE_FOLDER_PREFIX.get_Length());
			ResBase.Names.set_Item(key, text);
		}
	}

	private static void AddOnesOfFolder(DirectoryInfo dirInfo, string suffix, bool key_no_suffix = false, bool folder_name_tolower = false)
	{
		FileInfo[] files = dirInfo.GetFiles("*" + suffix, 1);
		for (int i = 0; i < files.Length; i++)
		{
			FileInfo fileInfo = files[i];
			if (!fileInfo.get_Name().EndsWith("meta"))
			{
				string text = fileInfo.get_Name();
				if (key_no_suffix)
				{
					text = text.Remove(text.get_Length() - suffix.get_Length());
				}
				if (!ResBase.Names.Contains(text))
				{
					if (folder_name_tolower)
					{
						ResBase.AddNameOfFolder(text, fileInfo.get_Directory().get_Name().ToLower());
					}
					else
					{
						ResBase.AddNameOfFolder(text, fileInfo.get_Directory().get_Name());
					}
				}
			}
		}
	}

	private static void AddNameOfFolder(string key, string folderName)
	{
		ResBase.Names.set_Item(key, folderName);
	}
}
