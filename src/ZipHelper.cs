using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

public class ZipHelper
{
	public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
	{
		if (!File.Exists(fileToZip))
		{
			throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
		}
		using (FileStream fileStream = File.Create(zipedFile))
		{
			using (ZipOutputStream zipOutputStream = new ZipOutputStream(fileStream))
			{
				using (FileStream fileStream2 = new FileStream(fileToZip, 3, 1))
				{
					string text = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
					ZipEntry zipEntry = new ZipEntry(text);
					zipOutputStream.PutNextEntry(zipEntry);
					zipOutputStream.SetLevel(compressionLevel);
					byte[] array = new byte[blockSize];
					try
					{
						int num;
						do
						{
							num = fileStream2.Read(array, 0, array.Length);
							zipOutputStream.Write(array, 0, num);
						}
						while (num > 0);
					}
					catch (Exception ex)
					{
						throw ex;
					}
					fileStream2.Close();
				}
				zipOutputStream.Finish();
				zipOutputStream.Close();
			}
			fileStream.Close();
		}
	}

	public static void ZipFile(string fileToZip, string zipedFile)
	{
		if (!File.Exists(fileToZip))
		{
			throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
		}
		using (FileStream fileStream = File.OpenRead(fileToZip))
		{
			byte[] array = new byte[fileStream.get_Length()];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			using (FileStream fileStream2 = File.Create(zipedFile))
			{
				using (ZipOutputStream zipOutputStream = new ZipOutputStream(fileStream2))
				{
					string text = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
					ZipEntry zipEntry = new ZipEntry(text);
					zipOutputStream.PutNextEntry(zipEntry);
					zipOutputStream.SetLevel(5);
					zipOutputStream.Write(array, 0, array.Length);
					zipOutputStream.Finish();
					zipOutputStream.Close();
				}
			}
		}
	}

	public static int ZipFileDirectory(string strDirectory, string zipedFile, Action<int> callBack)
	{
		int result;
		using (FileStream fileStream = File.Create(zipedFile))
		{
			using (ZipOutputStream zipOutputStream = new ZipOutputStream(fileStream))
			{
				result = ZipHelper.ZipSetp(strDirectory, zipOutputStream, string.Empty, callBack);
			}
		}
		return result;
	}

	private static int ZipSetp(string strDirectory, ZipOutputStream s, string parentPath, Action<int> callBack)
	{
		int num = 0;
		if (strDirectory.get_Chars(strDirectory.get_Length() - 1) != Path.DirectorySeparatorChar)
		{
			strDirectory += Path.DirectorySeparatorChar;
		}
		Crc32 crc = new Crc32();
		string[] fileSystemEntries = Directory.GetFileSystemEntries(strDirectory);
		string[] array = fileSystemEntries;
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (Directory.Exists(text))
			{
				string text2 = parentPath + text.Substring(text.LastIndexOf("\\") + 1);
				text2 += "\\";
				num += ZipHelper.ZipSetp(text, s, text2, callBack);
			}
			else
			{
				using (FileStream fileStream = File.OpenRead(text))
				{
					byte[] array2 = new byte[fileStream.get_Length()];
					fileStream.Read(array2, 0, array2.Length);
					string text3 = parentPath + text.Substring(text.LastIndexOf("\\") + 1);
					ZipEntry zipEntry = new ZipEntry(text3);
					zipEntry.set_DateTime(DateTime.get_Now());
					zipEntry.set_Size(fileStream.get_Length());
					fileStream.Close();
					crc.Reset();
					crc.Update(array2);
					zipEntry.set_Crc(crc.get_Value());
					s.PutNextEntry(zipEntry);
					s.Write(array2, 0, array2.Length);
				}
				num++;
				if (callBack != null)
				{
					callBack.Invoke(num);
				}
			}
		}
		return num;
	}

	public void UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
	{
		if (strDirectory == string.Empty)
		{
			strDirectory = Directory.GetCurrentDirectory();
		}
		if (!strDirectory.EndsWith("\\"))
		{
			strDirectory += "\\";
		}
		using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipedFile)))
		{
			zipInputStream.set_Password(password);
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				text2 = nextEntry.get_Name();
				if (text2 != string.Empty)
				{
					text = Path.GetDirectoryName(text2) + "\\";
				}
				string fileName = Path.GetFileName(text2);
				Directory.CreateDirectory(strDirectory + text);
				if (fileName != string.Empty && ((File.Exists(strDirectory + text + fileName) && overWrite) || !File.Exists(strDirectory + text + fileName)))
				{
					using (FileStream fileStream = File.Create(strDirectory + text + fileName))
					{
						byte[] array = new byte[2048];
						while (true)
						{
							int num = zipInputStream.Read(array, 0, array.Length);
							if (num <= 0)
							{
								break;
							}
							fileStream.Write(array, 0, num);
						}
						fileStream.Close();
					}
				}
			}
			zipInputStream.Close();
		}
	}
}
