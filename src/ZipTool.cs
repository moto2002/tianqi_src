using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using UnityEngine;

public class ZipTool
{
	public static int m_UnZipFileCount;

	public static int m_AllFileCount;

	public static bool isUnzip;

	public static byte[] LoadBytes(string filename)
	{
		byte[] result;
		using (Stream stream = File.OpenRead(filename))
		{
			byte[] array = new byte[stream.get_Length()];
			int num = 0;
			while ((long)num < array.get_LongLength())
			{
				num += stream.Read(array, num, array.Length - num);
			}
			stream.Close();
			stream.Dispose();
			result = array;
		}
		return result;
	}

	public static void StartUnZipFile(string zipedFile, string unZipDir, string password, bool overWrite)
	{
		if (!unZipDir.EndsWith("\\"))
		{
			unZipDir += Path.DirectorySeparatorChar;
		}
		if (!Directory.Exists(unZipDir))
		{
			Directory.CreateDirectory(unZipDir);
		}
		ZipTool.UnZipFile(ZipTool.WriteFile(zipedFile), unZipDir, string.Empty, overWrite);
	}

	public static void StartUnZipFile(byte[] bts, string unZipDir, string password, bool overWrite = true)
	{
		if (!unZipDir.EndsWith("\\"))
		{
			unZipDir += Path.DirectorySeparatorChar;
		}
		if (!Directory.Exists(unZipDir))
		{
			Directory.CreateDirectory(unZipDir);
		}
		ZipTool.UnZipFile(ZipTool.WriteFile(bts), unZipDir, password, overWrite);
	}

	public static string UnThreadZipFile(string zipedFile, string unZipDir, string password, bool overWrite)
	{
		Debug.LogError(File.Exists(zipedFile) + "start-->" + zipedFile);
		string result;
		try
		{
			using (FileStream fileStream = File.OpenRead(zipedFile))
			{
				using (ZipInputStream zipInputStream = new ZipInputStream(fileStream))
				{
					zipInputStream.set_Password(password);
					ZipEntry nextEntry;
					while ((nextEntry = zipInputStream.GetNextEntry()) != null)
					{
						string text = nextEntry.get_Name().Replace("\\", "/");
						string directoryName = Path.GetDirectoryName(text);
						Debug.LogError(text);
						if (directoryName != null)
						{
							string text2 = Path.Combine(unZipDir, directoryName);
							if (!Directory.Exists(text2))
							{
								Directory.CreateDirectory(text2);
							}
						}
						if (text != null)
						{
							string text3 = Path.Combine(unZipDir, text).Replace("\\", "/");
							if ((File.Exists(text3) && overWrite) || !File.Exists(text3))
							{
								using (FileStream fileStream2 = File.Create(text3))
								{
									int num = 1048576;
									byte[] array = new byte[num];
									while (true)
									{
										num = zipInputStream.Read(array, 0, array.Length);
										if (num <= 0)
										{
											break;
										}
										fileStream2.Write(array, 0, num);
									}
									fileStream2.Close();
									fileStream2.Dispose();
									ZipTool.m_UnZipFileCount++;
								}
							}
						}
					}
					zipInputStream.Close();
					zipInputStream.Dispose();
					fileStream.Close();
					fileStream.Dispose();
				}
				Debug.LogError("UnZipFile  end");
				result = null;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("error:" + ex.get_Message());
			result = ex.get_Message();
		}
		return result;
	}

	public static void UnZipFile(Stream ms, string unZipDir, string password, bool overWrite)
	{
		if (ms == null)
		{
			Debug.LogError("解压流为null");
			return;
		}
		using (ZipInputStream zipInputStream = new ZipInputStream(ms))
		{
			zipInputStream.set_Password(password);
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				string text = nextEntry.get_Name().Replace("\\", "/");
				string directoryName = Path.GetDirectoryName(text);
				if (directoryName != null)
				{
					string text2 = Path.Combine(unZipDir, directoryName);
					if (!Directory.Exists(text2))
					{
						Directory.CreateDirectory(text2);
					}
				}
				if (text != null)
				{
					string text3 = Path.Combine(unZipDir, text).Replace("\\", "/");
					if (!File.Exists(text3) || overWrite)
					{
						using (FileStream fileStream = File.Create(text3))
						{
							int num = 1048576;
							byte[] array = new byte[num];
							while (true)
							{
								num = zipInputStream.Read(array, 0, array.Length);
								if (num <= 0)
								{
									break;
								}
								fileStream.Write(array, 0, num);
							}
							fileStream.Close();
							fileStream.Dispose();
						}
					}
				}
			}
			zipInputStream.Close();
			zipInputStream.Dispose();
		}
		ms.Close();
		ms.Dispose();
		ms = null;
	}

	public static string UnZipFile(Stream iStream, string unZipDir, bool isSafeSave)
	{
		ZipTool.m_UnZipFileCount = 0;
		ZipInputStream zipInputStream = null;
		FileStream fileStream = null;
		try
		{
			zipInputStream = new ZipInputStream(iStream);
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				string directoryName = Path.GetDirectoryName(nextEntry.get_Name());
				string fileName = Path.GetFileName(nextEntry.get_Name());
				if (directoryName != null)
				{
					string text = Path.Combine(unZipDir, directoryName);
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					if (!string.IsNullOrEmpty(fileName))
					{
						string text2 = Path.Combine(text, fileName);
						string text3;
						if (isSafeSave)
						{
							text3 = text2 + ".temp";
						}
						else
						{
							text3 = text2;
						}
						fileStream = File.Create(text3);
						int num = 2048;
						byte[] array = new byte[num];
						while (true)
						{
							int num2 = zipInputStream.Read(array, 0, array.Length);
							if (num2 <= 0)
							{
								break;
							}
							fileStream.Write(array, 0, num2);
						}
						fileStream.Close();
						fileStream.Dispose();
						if (isSafeSave)
						{
							if (File.Exists(text2))
							{
								File.Delete(text2);
							}
							File.Move(text3, text2);
							ZipTool.m_UnZipFileCount++;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_StackTrace());
			string stackTrace = ex.get_StackTrace();
			string result;
			if (!string.IsNullOrEmpty(stackTrace) && stackTrace.get_Length() > 100)
			{
				string[] array2 = new string[]
				{
					"(",
					ex.get_Message(),
					"\n",
					stackTrace.Substring(0, 50),
					"\n",
					stackTrace.Substring(stackTrace.get_Length() - 16, 16),
					")"
				};
				result = string.Concat(array2);
				return result;
			}
			string[] array3 = new string[]
			{
				"(",
				ex.get_Message(),
				"\n",
				stackTrace,
				")"
			};
			result = string.Concat(array3);
			return result;
		}
		finally
		{
			if (fileStream != null)
			{
				try
				{
					fileStream.Close();
					fileStream.Dispose();
				}
				catch (Exception ex2)
				{
					Debug.LogWarning(ex2.get_StackTrace());
				}
				fileStream = null;
			}
			if (zipInputStream != null)
			{
				try
				{
					zipInputStream.Close();
					zipInputStream.Dispose();
				}
				catch (Exception ex3)
				{
					Debug.LogWarning(ex3.get_StackTrace());
				}
				zipInputStream = null;
			}
		}
		return null;
	}

	private static MemoryStream WriteFile(byte[] pReadByte)
	{
		MemoryStream memoryStream = null;
		try
		{
			memoryStream = new MemoryStream();
			memoryStream.Write(pReadByte, 0, pReadByte.Length);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_Message());
		}
		return memoryStream;
	}

	private static FileStream WriteFile(string path)
	{
		FileStream result = null;
		try
		{
			result = File.OpenRead(path);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_Message());
		}
		return result;
	}
}
