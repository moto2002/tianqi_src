using LuaFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class DownloaderManager
{
	private int baseId;

	private int patchId;

	private bool isTemp;

	private string tempNum;

	private bool AnalysisFile = true;

	private bool hasFileWaitDownload;

	private Action<float, float, string> PercentCallback;

	private Action<bool> FinishCallback;

	private Action<int, string, float, float> ShowState;

	private Action<long> AnalysisCallback;

	private string localFilePath;

	private string dataPath;

	private string packageIndexUrl = string.Empty;

	private string message = string.Empty;

	private List<string> downloadFilePaths = new List<string>();

	private List<long> downloadFileSizes = new List<long>();

	private List<string> downloadFileUrls = new List<string>();

	private List<string> downloadFileMd5s = new List<string>();

	private List<string> downloadFilePathsTemp = new List<string>();

	private List<long> downloadFileSizesTemp = new List<long>();

	private List<string> downloadFileUrlsTemp = new List<string>();

	private List<string> downloadFileMd5sTemp = new List<string>();

	private long downloadedFilesTotalSize;

	private long tempSize;

	private long thisTimeDownloadFilesTotalSize;

	private long packageSize;

	private int fileCount;

	private Action<float, float> ProgressCallback;

	private int GetSizePackageId;

	private Action<float> GetPackageSizeCallback;

	private float analysisProgress;

	private static DownloaderManager instance;

	public bool HasFileWaitDownload
	{
		get
		{
			return this.hasFileWaitDownload;
		}
	}

	public float AnalysisProgress
	{
		get
		{
			return this.analysisProgress;
		}
	}

	public static DownloaderManager Instance
	{
		get
		{
			if (DownloaderManager.instance == null)
			{
				DownloaderManager.instance = new DownloaderManager();
			}
			return DownloaderManager.instance;
		}
	}

	public void ResetAll()
	{
		this.downloadFilePaths.Clear();
		this.downloadFileSizes.Clear();
		this.downloadFileUrls.Clear();
		this.downloadFileMd5s.Clear();
		this.downloadFilePathsTemp.Clear();
		this.downloadFileSizesTemp.Clear();
		this.downloadFileMd5sTemp.Clear();
		this.downloadFileUrlsTemp.Clear();
		this.downloadedFilesTotalSize = 0L;
		this.thisTimeDownloadFilesTotalSize = 0L;
		this.fileCount = 0;
	}

	public void ResetCallback()
	{
		this.GetPackageSizeCallback = null;
		this.ProgressCallback = null;
		this.PercentCallback = null;
		this.FinishCallback = null;
		this.GetSizePackageId = -1;
	}

	public void DowanloadUpdatePackage(int _baseId = 0, int _patchId = 0, Action<float, float, string> _percentCallback = null, Action<bool> _finishCallback = null, bool _isTemp = true, bool _analysis = true)
	{
		this.ResetCallback();
		this.baseId = _baseId;
		this.patchId = _patchId;
		this.PercentCallback = _percentCallback;
		this.FinishCallback = _finishCallback;
		this.isTemp = _isTemp;
		this.AnalysisFile = _analysis;
		this.GetPackageIndexUrl(this.baseId, this.patchId);
		this.DownloadIndexFile();
	}

	public void DownloadPackage(Action<float, float> progressCallback = null, Action<bool> finishCallback = null)
	{
		if (progressCallback != null)
		{
			this.ProgressCallback = progressCallback;
		}
		if (finishCallback != null)
		{
			this.FinishCallback = finishCallback;
		}
		this.downloadFilePaths.Add(this.downloadFilePathsTemp.get_Item(0));
		this.downloadFileSizes.Add(this.downloadFileSizesTemp.get_Item(0));
		this.downloadFileMd5s.Add(this.downloadFileMd5sTemp.get_Item(0));
		this.downloadFileUrls.Add(this.downloadFileUrlsTemp.get_Item(0));
		this.downloadFilePathsTemp.RemoveAt(0);
		this.downloadFileSizesTemp.RemoveAt(0);
		this.downloadFileMd5sTemp.RemoveAt(0);
		this.downloadFileUrlsTemp.RemoveAt(0);
		if (this.downloadFilePathsTemp.get_Count() > 0)
		{
			this.hasFileWaitDownload = true;
		}
		else
		{
			this.hasFileWaitDownload = false;
		}
		Debug.LogError("start download package");
		Downloader.Instance.Download(this.downloadFileUrls, this.downloadFilePaths, this.downloadFileMd5s, this.downloadFileSizes, new Action<int, int, string>(this.DownloadFilePercentCallback), new Action<bool>(this.DownloadFileFinishCallback));
	}

	private void GetPackageIndexUrl(int _baseId, int _patchId)
	{
		string text = string.Empty;
		if (_baseId != 0)
		{
			text = "base";
		}
		else if (_patchId != 0)
		{
			text = "patch";
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Unmatched download pack, _baseId :",
				_baseId,
				", _patchId:",
				_baseId
			}));
		}
		this.packageIndexUrl = string.Format(AppConst.ZipUrl, SerializeUtility.GetRuntimePlatformFolderName(Application.get_platform()), text);
	}

	public void DownloadIndexFile()
	{
		this.ResetAll();
		this.dataPath = Util.DataPath;
		string indexName = Util.GetIndexName();
		string text = Path.Combine(this.packageIndexUrl, indexName);
		string text2 = Path.Combine(this.dataPath, indexName);
		Debug.LogWarning("LoadUpdate---->>>" + text);
		if (!Directory.Exists(this.dataPath))
		{
			Directory.CreateDirectory(this.dataPath);
		}
		this.downloadFilePaths.Add(text2);
		this.downloadFileUrls.Add(text);
		Downloader.Instance.Download(this.downloadFileUrls, this.downloadFilePaths, null, null, new Action<int, int, string>(this.DownloadFilePercentCallback), new Action<bool>(this.DownloadIndexFileFinishCallback));
	}

	private void DownloadIndexFileFinishCallback(bool isFinish)
	{
		if (isFinish)
		{
			if (this.AnalysisFile)
			{
				this.ToAnalysis(null, true);
			}
			else
			{
				if (this.FinishCallback != null)
				{
					this.FinishCallback.Invoke(isFinish);
				}
				if (this.GetSizePackageId >= 1)
				{
					this.GetSizePackageId = 0;
					if (this.GetPackageSizeCallback != null)
					{
						this.GetPackageSizeCallback.Invoke(1f);
					}
				}
			}
			return;
		}
		Debug.LogError("下载索引文件时发生错误，请检查");
		if (this.FinishCallback != null)
		{
			this.FinishCallback.Invoke(isFinish);
		}
		if (this.GetSizePackageId >= 1)
		{
			this.GetSizePackageId = 0;
			if (this.GetPackageSizeCallback != null)
			{
				this.GetPackageSizeCallback.Invoke(-1f);
			}
			return;
		}
	}

	public void GetZipFiles(Action<long> _AnalysisCallback = null, bool _isTemp = false)
	{
		this.isTemp = _isTemp;
		this.AnalysisCallback = _AnalysisCallback;
		StreamReader streamReader = File.OpenText(this.dataPath + Util.GetIndexName());
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.ResetAll();
		string[] array = text.Trim().Split(new char[]
		{
			'\n'
		});
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				string text2 = array2[0];
				if (this.baseId > 0)
				{
					string text3 = "base" + this.baseId.ToString() + ".zip";
					if (text2 == text3)
					{
						string[] array3 = array2[1].Split(new char[]
						{
							';'
						});
						this.thisTimeDownloadFilesTotalSize += long.Parse(array3[1].Trim());
						this.downloadFilePathsTemp.Add(this.dataPath + text2);
						this.downloadFileSizesTemp.Add(long.Parse(array3[1].Trim()));
						this.downloadFileMd5sTemp.Add(array3[0].Trim());
						this.downloadFileUrlsTemp.Add(this.packageIndexUrl + text2);
						break;
					}
				}
				else if (this.patchId > 0)
				{
					string text3 = "patch" + this.patchId.ToString() + ".zip";
					if (text2.Substring(0, 5) == "patch" && Path.GetExtension(text2) == ".zip" && int.Parse(text2.Substring(5, 1)) <= this.patchId && int.Parse(text2.Substring(5, 1)) > int.Parse(GameManager.Instance.clientVersions[3]) && this.patchId != 0)
					{
						string[] array4 = array2[1].Split(new char[]
						{
							';'
						});
						this.thisTimeDownloadFilesTotalSize += long.Parse(array4[1].Trim());
						this.downloadFilePathsTemp.Add(this.dataPath + text2);
						this.downloadFileSizesTemp.Add(long.Parse(array4[1].Trim()));
						this.downloadFileMd5sTemp.Add(array4[0].Trim());
						this.downloadFileUrlsTemp.Add(this.packageIndexUrl + text2);
					}
				}
				else
				{
					Debug.LogError(string.Concat(new object[]
					{
						"UnMatched Download Mode, baseId && patchId: ",
						this.baseId,
						"&&",
						this.patchId
					}));
				}
			}
		}
		Debug.LogError(string.Concat(new object[]
		{
			"Zip Analysis Finish:",
			this.downloadFilePathsTemp.get_Count(),
			"TotalSize: ",
			this.thisTimeDownloadFilesTotalSize
		}));
		if (!this.isTemp)
		{
			this.AnalysisCallback.Invoke(this.thisTimeDownloadFilesTotalSize);
		}
		else
		{
			this.AnalysisCallback.Invoke(this.thisTimeDownloadFilesTotalSize);
		}
	}

	public void ToAnalysis(Action<long> _AnalysisCallback = null, bool _isTemp = true)
	{
		this.isTemp = _isTemp;
		this.AnalysisCallback = _AnalysisCallback;
		Debug.LogError("start analysis indexfile...");
		if (UIManagerControl.Instance.GetUIIfExist("PreloadingUI"))
		{
			PreloadingUIView.SetProgressName(GameDataUtils.GetChineseContent(621302, false));
		}
		Thread thread = new Thread(new ThreadStart(this.AnalysisIndexFile));
		thread.set_IsBackground(false);
		thread.Start();
	}

	private void AnalysisIndexFile()
	{
		DownloaderManager.<AnalysisIndexFile>c__AnonStoreyE9 <AnalysisIndexFile>c__AnonStoreyE = new DownloaderManager.<AnalysisIndexFile>c__AnonStoreyE9();
		<AnalysisIndexFile>c__AnonStoreyE.<>f__this = this;
		StreamReader streamReader = File.OpenText(this.dataPath + Util.GetIndexName());
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		string text2 = this.dataPath + "AnalysisFinish.txt";
		FileStream fileStream = new FileStream(text2, 2, 2);
		StreamWriter streamWriter = new StreamWriter(fileStream);
		this.ResetAll();
		string[] array = text.Trim().Split(new char[]
		{
			'\n'
		});
		<AnalysisIndexFile>c__AnonStoreyE.filesNum = array.Length;
		int i;
		for (i = 0; i < <AnalysisIndexFile>c__AnonStoreyE.filesNum; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				string text3 = array2[0];
				this.localFilePath = (this.dataPath + text3).Trim();
				string directoryName = Path.GetDirectoryName(this.localFilePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				string[] array3 = array2[1].Split(new char[]
				{
					';'
				});
				bool flag = !File.Exists(this.localFilePath);
				if (!flag)
				{
					FileInfo fileInfo = new FileInfo(this.localFilePath);
					if (fileInfo.get_Length() == long.Parse(array3[1]))
					{
						string text4 = array3[0].Trim();
						string text5 = Util.md5file(this.localFilePath);
						flag = !text4.Equals(text5);
						if (flag)
						{
							File.Delete(this.localFilePath);
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.thisTimeDownloadFilesTotalSize += long.Parse(array3[1].Trim());
					this.downloadFilePaths.Add(this.localFilePath);
					this.downloadFileSizes.Add(long.Parse(array3[1].Trim()));
					this.downloadFileMd5s.Add(array3[0].Trim());
					this.downloadFileUrls.Add(this.packageIndexUrl + text3);
					streamWriter.WriteLine(string.Concat(new string[]
					{
						text3,
						"|",
						array3[0].Trim(),
						";",
						array3[1].Trim()
					}));
				}
				if (UIManagerControl.Instance.GetUIIfExist("PreloadingUI"))
				{
					if (!this.isTemp)
					{
						Loom.Current.QueueOnMainThread(delegate
						{
							PreloadingUIView.SetProgress((float)(i + 1) / (float)<AnalysisIndexFile>c__AnonStoreyE.filesNum);
						});
					}
					else
					{
						Loom.Current.QueueOnMainThread(delegate
						{
							PreloadingUIView.SetProgress((float)(i + 1) / (float)<AnalysisIndexFile>c__AnonStoreyE.filesNum * 0.7f);
						});
					}
				}
			}
		}
		streamWriter.Close();
		fileStream.Close();
		Debug.LogError(string.Concat(new object[]
		{
			"本地解析完毕,待下载文件数：",
			this.downloadFilePaths.get_Count(),
			"TotalSize: ",
			this.thisTimeDownloadFilesTotalSize
		}));
		if (!this.isTemp)
		{
			Loom.Current.QueueOnMainThread(delegate
			{
				<AnalysisIndexFile>c__AnonStoreyE.<>f__this.AnalysisFileFinish();
			});
		}
		else
		{
			this.AnalysisTempFile();
		}
	}

	private void AnalysisTempFile()
	{
		DownloaderManager.<AnalysisTempFile>c__AnonStoreyEA <AnalysisTempFile>c__AnonStoreyEA = new DownloaderManager.<AnalysisTempFile>c__AnonStoreyEA();
		<AnalysisTempFile>c__AnonStoreyEA.<>f__this = this;
		Debug.Log(" IN  AnalysisTempFile");
		if (this.patchId == 0)
		{
			this.tempNum = ".TEMP" + this.baseId.ToString();
		}
		else
		{
			this.tempNum = ".temp" + this.patchId.ToString();
		}
		Debug.LogError("Analysis create tempNum:" + this.tempNum);
		<AnalysisTempFile>c__AnonStoreyEA.filesNum = this.downloadFilePaths.get_Count();
		int i;
		for (i = <AnalysisTempFile>c__AnonStoreyEA.filesNum - 1; i >= 0; i--)
		{
			string text = this.downloadFilePaths.get_Item(i) + this.tempNum;
			bool flag = !File.Exists(text);
			if (!flag)
			{
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.get_Length() == this.downloadFileSizes.get_Item(i))
				{
					string text2 = this.downloadFileMd5s.get_Item(i);
					string text3 = Util.md5file(text);
					flag = !text2.Equals(text3);
					if (flag)
					{
						File.Delete(text);
					}
				}
				else
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.tempSize += this.downloadFileSizes.get_Item(i);
				this.DownloadListDelete(i);
			}
			else
			{
				this.downloadFilePaths.set_Item(i, text);
			}
			if (UIManagerControl.Instance.GetUIIfExist("PreloadingUI"))
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					PreloadingUIView.SetProgress((float)(<AnalysisTempFile>c__AnonStoreyEA.filesNum - i) / (float)<AnalysisTempFile>c__AnonStoreyEA.filesNum * 0.3f + 0.7f);
				});
			}
		}
		Loom.Current.QueueOnMainThread(delegate
		{
			<AnalysisTempFile>c__AnonStoreyEA.<>f__this.AnalysisFileFinish();
		});
	}

	private void DownloadListDelete(int i)
	{
		this.downloadFilePaths.RemoveAt(i);
		this.downloadFileSizes.RemoveAt(i);
		this.downloadFileMd5s.RemoveAt(i);
		this.downloadFileUrls.RemoveAt(i);
	}

	private void AnalysisFileFinish()
	{
		Debug.LogError(string.Concat(new object[]
		{
			"解析完毕,待下载文件数：",
			this.downloadFilePaths.get_Count(),
			"TotalSize: ",
			this.thisTimeDownloadFilesTotalSize
		}));
		if (this.AnalysisCallback != null)
		{
			this.AnalysisCallback.Invoke(this.thisTimeDownloadFilesTotalSize - this.tempSize);
			this.AnalysisCallback = null;
			return;
		}
		if (this.downloadFilePaths.get_Count() > 0)
		{
			this.DownloadPackage(null, null);
		}
		else
		{
			if (this.PercentCallback != null)
			{
				this.PercentCallback.Invoke(0f, -1f, null);
			}
			this.DownloadFileFinishCallback(true);
		}
	}

	private void DownloadFilePercentCallback(int downloadedSize, int downloadingFileSize, string fileName)
	{
		if (fileName != Util.GetIndexName())
		{
			float num = (float)(this.downloadedFilesTotalSize + (long)downloadedSize) / (float)this.thisTimeDownloadFilesTotalSize;
		}
		if (downloadedSize == downloadingFileSize)
		{
			this.fileCount++;
			this.downloadedFilesTotalSize += (long)downloadingFileSize;
			if (this.ProgressCallback != null)
			{
				this.ProgressCallback.Invoke((float)(this.downloadedFilesTotalSize / 1024L) / 1024f, (float)(this.thisTimeDownloadFilesTotalSize / 1024L) / 1024f);
			}
			if (this.PercentCallback != null)
			{
				if (fileName != Util.GetIndexName())
				{
					this.PercentCallback.Invoke((float)this.downloadedFilesTotalSize / 1024f, (float)this.thisTimeDownloadFilesTotalSize / 1024f, fileName);
				}
				else
				{
					this.PercentCallback.Invoke(0f, 1000f, fileName);
				}
			}
		}
		else
		{
			if (this.ProgressCallback != null)
			{
				this.ProgressCallback.Invoke((float)(this.downloadedFilesTotalSize / 1024L) / 1024f, (float)(this.thisTimeDownloadFilesTotalSize / 1024L) / 1024f);
			}
			if (this.PercentCallback != null)
			{
				if (fileName != Util.GetIndexName())
				{
					this.PercentCallback.Invoke((float)this.downloadedFilesTotalSize / 1024f, (float)this.thisTimeDownloadFilesTotalSize / 1024f, fileName);
				}
				else
				{
					this.PercentCallback.Invoke(0f, 1000f, fileName);
				}
			}
		}
	}

	private void DownloadFileFinishCallback(bool isFinish)
	{
		if (!isFinish)
		{
			if (this.FinishCallback != null)
			{
				this.FinishCallback.Invoke(isFinish);
			}
			Debug.LogError("下载时发生错误，请检查");
		}
		else
		{
			if (UIManagerControl.Instance.GetUIIfExist("PreloadingUI"))
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					PreloadingUIView.SetProgressName(GameDataUtils.GetChineseContent(621312, false));
				});
				Loom.Current.QueueOnMainThread(delegate
				{
					PreloadingUIView.SetSpeed(string.Empty);
				});
			}
			Thread thread = new Thread(new ThreadStart(this.CheckMd5s));
			thread.set_IsBackground(true);
			thread.Start();
		}
	}

	private void CheckMd5s()
	{
		int i;
		for (i = this.downloadFilePaths.get_Count() - 1; i >= 0; i--)
		{
			if (this.downloadFileMd5s.get_Item(i) == null)
			{
				this.DownloadListDelete(i);
			}
			else
			{
				string text = Util.md5file(this.downloadFilePaths.get_Item(i));
				if (text.Equals(this.downloadFileMd5s.get_Item(i)))
				{
					this.DownloadListDelete(i);
				}
				else
				{
					File.Delete(this.downloadFilePaths.get_Item(i));
				}
				if (UIManagerControl.Instance.GetUIIfExist("PreloadingUI"))
				{
					Loom.Current.QueueOnMainThread(delegate
					{
						PreloadingUIView.SetProgress((float)(this.downloadFilePaths.get_Count() - i) / (float)this.downloadFilePaths.get_Count());
					});
				}
			}
		}
		Loom.Current.QueueOnMainThread(delegate
		{
			this.CheckFinish();
		});
	}

	private void CheckFinish()
	{
		if (this.downloadFilePaths.get_Count() > 0)
		{
			Debug.LogError("start download package,downloadFilePaths.Count = " + this.downloadFilePaths.get_Count());
			Downloader.Instance.Download(this.downloadFileUrls, this.downloadFilePaths, this.downloadFileMd5s, this.downloadFileSizes, new Action<int, int, string>(this.DownloadFilePercentCallback), new Action<bool>(this.DownloadFileFinishCallback));
		}
		else if (this.FinishCallback != null)
		{
			this.FinishCallback.Invoke(true);
		}
	}
}
