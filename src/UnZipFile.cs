using ICSharpCode.SharpZipLib.Zip;
using LuaFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class UnZipFile
{
	private const int BufferSize = 2048;

	private string dataPath = Util.DataPath;

	private string Source;

	private volatile int unzipFileCount;

	private int nowUnZipFile;

	private int finishThreadNum;

	private object locker = new object();

	private Action UnZipFinish;

	private List<string> FilterDir;

	public List<ManualResetEvent> singals;

	public AutoResetEvent waitOneFile = new AutoResetEvent(false);

	public AutoResetEvent waitOneThread = new AutoResetEvent(false);

	public int oneFileSize;

	public int finishSize;

	private string zipFileName;

	private int processors = Environment.get_ProcessorCount();

	private int threadNum;

	private long fileSize;

	private static UnZipFile instance;

	private float unZipProgress;

	public int WaitUnZipFiles
	{
		get;
		private set;
	}

	public int UnzipFileCount
	{
		get
		{
			return this.unzipFileCount;
		}
	}

	public int ThreadNum
	{
		get
		{
			return this.threadNum;
		}
		set
		{
			this.threadNum = value;
		}
	}

	public static UnZipFile Instance
	{
		get
		{
			if (UnZipFile.instance == null)
			{
				UnZipFile.instance = new UnZipFile();
			}
			return UnZipFile.instance;
		}
	}

	public float UnZipProgress
	{
		get
		{
			return this.unZipProgress;
		}
	}

	public void UnZip(object _zipFileArgs)
	{
		ZipFileArgs zipFileArgs = (ZipFileArgs)_zipFileArgs;
		string zipFile = zipFileArgs.zipFile;
		this.dataPath = zipFileArgs.TarDir;
		bool overWrite = zipFileArgs.OverWrite;
		this.UnZipFinish = zipFileArgs.unZipFinish;
		this.FilterDir = zipFileArgs.FilterDir;
		if (string.IsNullOrEmpty(zipFile))
		{
			Debug.LogError("ZipFileName is null: " + zipFile);
			return;
		}
		if (!File.Exists(zipFile))
		{
			Debug.LogError("Cannot find file:" + zipFile);
			return;
		}
		this.zipFileName = zipFile;
		this.fileSize = new FileInfo(zipFile).get_Length();
		Debug.Log(string.Concat(new object[]
		{
			"UnZip File: ",
			zipFile,
			" fileSize:",
			this.fileSize
		}));
		this.WaitUnZipFiles = 0;
		this.nowUnZipFile = 0;
		this.finishThreadNum = 0;
		this.unzipFileCount = 0;
		using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(this.zipFileName)))
		{
			ZipEntry nextEntry;
			while ((nextEntry = zipInputStream.GetNextEntry()) != null)
			{
				this.WaitUnZipFiles++;
				string text = Path.GetDirectoryName(nextEntry.get_Name());
				string relPath = this.GetRelPath(text);
				if (!string.IsNullOrEmpty(relPath))
				{
					text = Path.Combine(this.dataPath, relPath);
					DirectoryUtil.CreateIfNotExist(text);
				}
			}
		}
		Debug.Log("ThreadNum:" + this.ThreadNum);
		for (int i = 1; i <= this.ThreadNum; i++)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.UnZipNextFile));
		}
	}

	private string GetRelPath(string path)
	{
		path = PathUtil.NormalizingPath(path);
		string result = string.Empty;
		string text = string.Empty;
		if (this.FilterDir != null)
		{
			for (int i = 0; i < this.FilterDir.get_Count(); i++)
			{
				string text2 = this.FilterDir.get_Item(i);
				if (path.StartsWith(text2))
				{
					text = text2;
					break;
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			result = PathUtil.GetRelativePath(path, text);
		}
		return result;
	}

	private int GetNextPosition(int threadId)
	{
		this.nowUnZipFile++;
		if (this.nowUnZipFile <= this.WaitUnZipFiles)
		{
			return this.nowUnZipFile;
		}
		return -1;
	}

	private void UnZipBlockFiles(object _bf)
	{
		DateTime now = DateTime.get_Now();
		BlockFile blockFile = (BlockFile)_bf;
		Debug.LogError(string.Concat(new object[]
		{
			"UnZipBlockFiles Start :",
			blockFile.threadId,
			", dt:",
			now
		}));
		using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(this.zipFileName)))
		{
			for (int i = 1; i <= blockFile.endPosition; i++)
			{
				if (i < blockFile.startPosition)
				{
					zipInputStream.GetNextEntry();
				}
				else
				{
					ZipEntry nextEntry = zipInputStream.GetNextEntry();
					if (nextEntry == null)
					{
						break;
					}
					string text = Path.Combine(blockFile.tarDir, Path.GetDirectoryName(nextEntry.get_Name()));
					string fileName = Path.GetFileName(nextEntry.get_Name());
					if (string.IsNullOrEmpty(fileName))
					{
						if (File.Exists(Path.Combine(text, fileName)) && !blockFile.overWrite)
						{
							Debug.LogError(string.Concat(new object[]
							{
								"overWrite:",
								blockFile.overWrite,
								"  ,File.Exists:",
								fileName
							}));
						}
						else
						{
							using (FileStream fileStream = File.Create(Path.Combine(text, fileName)))
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
							}
						}
					}
				}
			}
		}
		TimeSpan timeSpan = DateTime.get_Now() - now;
		Debug.LogError(string.Concat(new object[]
		{
			"UnZipBlockFiles end :",
			blockFile.threadId,
			", ts:",
			timeSpan
		}));
		this.waitOneThread.Set();
	}

	private void UpdateUnZipProgress(object _threadId, bool timeout)
	{
		BlockFile blockFile = (BlockFile)_threadId;
		if (timeout)
		{
			Debug.LogError("One File UnZip Timeout:" + timeout);
		}
		else
		{
			this.finishSize++;
			this.unZipProgress = (float)this.finishSize / (float)this.threadNum;
		}
	}

	private void UnZipOneFile(object _file)
	{
		OneFile oneFile = (OneFile)_file;
		using (ZipInputStream zipInputStream = new ZipInputStream(File.Open(this.zipFileName, 3, 1, 3)))
		{
			for (int i = 0; i < oneFile.entryCounter; i++)
			{
				zipInputStream.GetNextEntry();
			}
			using (FileStream fileStream = File.Create(oneFile.filePath))
			{
				int num = 2048;
				byte[] array = new byte[2048];
				while (true)
				{
					try
					{
						num = zipInputStream.Read(array, 0, array.Length);
					}
					catch (Exception ex)
					{
						Debug.LogError("zipFile.Read " + ex.ToString());
						break;
					}
					if (num <= 0)
					{
						break;
					}
					fileStream.Write(array, 0, num);
				}
			}
			this.unZipProgress = (float)zipInputStream.get_Position() / (float)this.fileSize;
		}
	}

	private void UnZipNextFile(object _file)
	{
		int num = 0;
		int num2 = 0;
		object obj = this.locker;
		lock (obj)
		{
			num2 = this.GetNextPosition(Thread.get_CurrentThread().get_ManagedThreadId());
		}
		using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(this.zipFileName)))
		{
			while (num2 > num && num2 > 0)
			{
				int num3 = num2 - num;
				ZipEntry zipEntry = null;
				for (int i = 0; i < num3; i++)
				{
					zipEntry = zipInputStream.GetNextEntry();
				}
				string fileName = Path.GetFileName(zipEntry.get_Name());
				string relPath = this.GetRelPath(zipEntry.get_Name());
				if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(relPath))
				{
					string text = Path.Combine(this.dataPath, relPath);
					using (FileStream fileStream = File.Create(text))
					{
						byte[] array = new byte[2048];
						while (true)
						{
							int num4 = zipInputStream.Read(array, 0, array.Length);
							if (num4 <= 0)
							{
								break;
							}
							fileStream.Write(array, 0, num4);
						}
					}
					this.unzipFileCount++;
				}
				num = num2;
				object obj2 = this.locker;
				lock (obj2)
				{
					num2 = this.GetNextPosition(Thread.get_CurrentThread().get_ManagedThreadId());
				}
			}
		}
		this.OneThreadFinish();
	}

	private void OneThreadFinish()
	{
		if (++this.finishThreadNum == this.threadNum && this.UnZipFinish != null)
		{
			Loom.Current.QueueOnMainThread(delegate
			{
				this.UnZipFinish.Invoke();
			});
		}
	}

	public void ToZip(string _source, string _zipName)
	{
		this.Source = _source;
		Debug.LogError("_source: " + _source);
		string fullPath = Path.GetFullPath(_source + "/../" + _zipName);
		Debug.LogError("TartgetFile: " + fullPath);
		using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(fullPath)))
		{
			zipOutputStream.SetLevel(9);
			this.Compress(this.Source, zipOutputStream);
			zipOutputStream.Finish();
			zipOutputStream.Close();
		}
	}

	private void Compress(string source, ZipOutputStream s)
	{
		string[] fileSystemEntries = Directory.GetFileSystemEntries(source);
		string[] array = fileSystemEntries;
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (Directory.Exists(text))
			{
				this.Compress(text, s);
			}
			else
			{
				using (FileStream fileStream = File.OpenRead(text))
				{
					byte[] array2 = new byte[4096];
					string text2 = text.Replace(this.Source, string.Empty);
					string arg_63_0 = text2;
					char directorySeparatorChar = Path.DirectorySeparatorChar;
					if (arg_63_0.StartsWith(directorySeparatorChar.ToString()))
					{
						text2 = text2.Substring(1);
					}
					ZipEntry zipEntry = new ZipEntry(text2);
					zipEntry.set_DateTime(DateTime.get_Now());
					s.PutNextEntry(zipEntry);
					int num;
					do
					{
						num = fileStream.Read(array2, 0, array2.Length);
						s.Write(array2, 0, num);
					}
					while (num > 0);
				}
			}
		}
	}
}
