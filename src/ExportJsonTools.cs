using System;
using System.Collections;
using System.IO;
using XUPorterJSON;

public class ExportJsonTools
{
	public static void ExportJSON(string absPath, Hashtable hashTable)
	{
		if (File.Exists(absPath))
		{
			File.Delete(absPath);
		}
		FileInfo fileInfo = new FileInfo(absPath);
		StreamWriter streamWriter = fileInfo.CreateText();
		string text = MiniJSON.jsonEncode(hashTable);
		streamWriter.WriteLine(text);
		streamWriter.Close();
		streamWriter.Dispose();
	}
}
