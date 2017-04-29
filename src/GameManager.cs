using LuaFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using XEngine;
using XEngine.AssetLoader;
using XNetwork;

public class GameManager : MonoBehaviour
{
	public const string FINISHED_UPDATE_BASE_ID = "FinishedUpdateBaseId";

	public const string FINISHED_UPDATE_PATCH_ID = "FinishedUpdatePatchId";

	private const int COLUMN_COUNT = 5;

	public static GameManager Instance;

	public string[] clientVersions;

	private string[] serverVersions;

	public string serverVersion;

	public string auditVersion = string.Empty;

	public string update_url = string.Empty;

	public string PatchUrlRoot = "http://172.19.1.36";

	public int ServerVersionCode;

	public string CurrentApkMd5 = string.Empty;

	public string ResVersion = string.Empty;

	private int finishNum;

	private string ExtTemp;

	private uint t;

	private uint tm;

	private float fileSize;

	private string dataPath = Util.DataPath;

	private Action ReplaceFinish;

	private Action UnZipFinish;

	private HashSet<string> ExtendFiles;

	private HashSet<string> MissingFiles = new HashSet<string>();

	private static string AssetBundleBuildRoot = Util.DataPath;

	private bool IsInDebugInfoCountStatus;

	private float time;

	public static bool IsDebug = false;

	private bool IsTestSetting;

	private string resName = "UGUI/Res/TPAtlas/InstanceUI_07_pb";

	private int count;

	private string ApkPatchRoot = string.Empty;

	public UpdateManager CurrentUpdateManager
	{
		get;
		private set;
	}

	private void Awake()
	{
		if (!Application.get_isEditor() && File.Exists(AppConst.IsDebugFilePath))
		{
			GameManager.IsDebug = true;
		}
		if (Debug.get_isDebugBuild())
		{
			ProfilerWrap.InitPool();
		}
		Singleton<EditorConfig>.S.Init();
		Debug.Log("GameManager Awake");
		GameManager.Instance = this;
		Application.set_runInBackground(true);
		if (AppConst.UseAssetBundle)
		{
			Action<string> loadFailed = null;
			if (!Singleton<SwitchFile>.S.IsFileExist(SwitchFile.FileName.ABCheck))
			{
				RuntimePlatform platform = Application.get_platform();
				switch (platform)
				{
				case 7:
					loadFailed = new Action<string>(this.OnAssetBundleLoadFailed);
					goto IL_152;
				case 8:
				case 11:
					goto IL_152;
				case 9:
				case 10:
					IL_A0:
					if (platform != 2)
					{
						goto IL_152;
					}
					loadFailed = new Action<string>(this.OnAssetBundleLoadFailed);
					if (this.ExtendFiles == null)
					{
						this.ExtendFiles = new HashSet<string>();
						string text = PathUtil.Combine(new string[]
						{
							Application.get_streamingAssetsPath(),
							PathSystem.SubPackageInfoFile.ExtendList
						});
						Debug.LogFormat("extendList :{0}", new object[]
						{
							text
						});
						string[] array = File.ReadAllLines(text);
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text2 = array2[i];
							this.ExtendFiles.Add(text2);
						}
					}
					goto IL_152;
				}
				goto IL_A0;
			}
			loadFailed = new Action<string>(this.OnAssetBundleLoadFailed);
			IL_152:
			AssetBundleLoader.Instance.Initialize(loadFailed);
		}
		CamerasMgr.InitMainCamera();
		UINodesManager.InitUICanvas();
		this.Init();
	}

	private void OnAssetBundleLoadFailed(string file)
	{
		if (this.ExtendFiles != null && !this.ExtendFiles.Contains(file))
		{
			return;
		}
		if (!this.MissingFiles.Contains(file))
		{
			string text = string.Format("丢失的资源:{0}", file);
			Debug.Log(text);
			UIManagerControl.Instance.ShowToastText(text);
			this.MissingFiles.Add(file);
			if (Directory.Exists(Environment.get_CurrentDirectory()))
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (HashSet<string>.Enumerator enumerator = this.MissingFiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.get_Current();
						stringBuilder.AppendLine(current);
					}
				}
				string text2 = PathUtil.Combine(new string[]
				{
					Environment.get_CurrentDirectory(),
					"MissingFiles.txt"
				});
				File.WriteAllText(text2, stringBuilder.ToString());
			}
		}
	}

	private void Init()
	{
		PathSystem.Init();
		if (Application.get_isPlaying() && Application.get_isEditor())
		{
			string text = Application.get_dataPath() + "/StreamingAssets/resources2json.txt";
		}
		Object.DontDestroyOnLoad(base.get_gameObject());
		FileSystem.Init();
		PreloadUIBaseSystem.Init();
		GamePackets.Register();
		base.InvokeRepeating("Tick", 0f, 0.03f);
		Loom.Current.Init();
		this.InitBugly();
		base.get_gameObject().AddComponent<RemoteLogSender>();
		if (GameManager.IsDebug)
		{
			return;
		}
		CamerasMgr.CameraUI.set_enabled(false);
		this.OpenPreloadingUI();
		PreloadingUIView.SetProgressName("正在初始化环境...");
		Screen.set_sleepTimeout(-1);
		Debug.Log("SystemInfo.deviceModel = " + SystemInfoTools.GetDeviceModel());
		this.CurrentUpdateManager = new UpdateManager();
		this.CurrentUpdateManager.Init();
		this.CheckUpdate();
	}

	public void OpenPreloadingUI()
	{
		UIManagerControl.Instance.OpenUI("PreloadingUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
	}

	private void CheckUpdate()
	{
		Debug.Log(">>>>>>>>>>>>>>>>>OnResourceInited");
		this.OnResourceInited();
	}

	private void Tick()
	{
		TimerHeap.Tick();
	}

	private static string LocalVersionFileName()
	{
		return Path.Combine(GameManager.AssetBundleBuildRoot, "local_version.txt");
	}

	private static string LocalVersionFileCfg()
	{
		return Path.Combine(GameManager.AssetBundleBuildRoot, "local_versioncfg.txt");
	}

	private void CheckUnZipFile(Action _FinishCallBack)
	{
		this.UnZipFinish = _FinishCallBack;
		if (PlayerPrefs.HasKey("FinishedUpdateBaseId"))
		{
			this.finishNum = PlayerPrefs.GetInt("FinishedUpdateBaseId", -1);
			if (this.finishNum >= 0)
			{
				ZipFileArgs zipFileArgs = new ZipFileArgs();
				zipFileArgs.OverWrite = true;
				zipFileArgs.TarDir = Util.DataPath;
				zipFileArgs.unZipFinish = new Action(this.UnZipFileFinish);
				zipFileArgs.zipFile = this.dataPath + "base" + this.finishNum.ToString() + ".zip";
				UnZipFile.Instance.ThreadNum = Environment.get_ProcessorCount() * 2;
				UnZipFile.Instance.UnZip(zipFileArgs);
			}
			else if (PlayerPrefs.HasKey("FinishedUpdatePatchId"))
			{
				this.finishNum = PlayerPrefs.GetInt("FinishedUpdatePatchId", -1);
				if (this.finishNum >= 0)
				{
					ZipFileArgs zipFileArgs2 = new ZipFileArgs();
					zipFileArgs2.OverWrite = true;
					zipFileArgs2.TarDir = Util.DataPath;
					zipFileArgs2.unZipFinish = new Action(this.UnZipFileFinish);
					zipFileArgs2.zipFile = this.dataPath + "patch" + this.finishNum.ToString() + ".zip";
					UnZipFile.Instance.ThreadNum = Environment.get_ProcessorCount() * 2;
					UnZipFile.Instance.UnZip(zipFileArgs2);
				}
				else
				{
					this.UnZipFinish.Invoke();
				}
			}
			else
			{
				this.UnZipFinish.Invoke();
			}
		}
		else
		{
			this.UnZipFinish.Invoke();
		}
	}

	private void CheckReplacePackage(Action _FinishCallBack)
	{
		this.ReplaceFinish = _FinishCallBack;
		if (PlayerPrefs.HasKey("FinishedUpdateBaseId"))
		{
			this.finishNum = PlayerPrefs.GetInt("FinishedUpdateBaseId", -1);
			if (this.finishNum >= 0)
			{
				this.GoReplace("TEMP");
			}
			else if (PlayerPrefs.HasKey("FinishedUpdatePatchId"))
			{
				this.finishNum = PlayerPrefs.GetInt("FinishedUpdatePatchId", -1);
				if (this.finishNum >= 0)
				{
					this.GoReplace("temp");
				}
				else
				{
					this.ReplaceFinish.Invoke();
				}
			}
			else
			{
				this.ReplaceFinish.Invoke();
			}
		}
		else
		{
			this.ReplaceFinish.Invoke();
		}
	}

	private void GoReplace(string _temp)
	{
		this.ExtTemp = "." + _temp;
		Debug.LogError(string.Concat(new object[]
		{
			"finishNum:",
			this.finishNum,
			", _temp:",
			this.ExtTemp
		}));
		string chineseContent = GameDataUtils.GetChineseContent(621299, false);
		PreloadingUIView.SetProgressName(chineseContent);
		Thread thread = new Thread(new ThreadStart(this.ReplaceUpdateFiles));
		thread.set_IsBackground(true);
		thread.Start();
	}

	private void DownloadBigFile()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		string text = "http://www.swejackies.com.cn/Resources.tar.gz";
		list.Add(text);
		list2.Add(Path.Combine(GameManager.AssetBundleBuildRoot, "Bigfile.apk"));
		Downloader.Instance.Download(list, list2, null, null, new Action<int, int, string>(this.ShowState), new Action<bool>(this.DownloadBigFileFinish));
	}

	private void DownloadBigFileFinish(bool isFinish)
	{
		TimerHeap.DelTimer(this.t);
		TimerHeap.DelTimer(this.tm);
		NewContinueUI.OpenAsNormal("test", "是否进行大文件下载测试？", new Action(ClientApp.QuitApp), new Action(this.DownloadBigFile), "确 定", "取 消");
	}

	private void ReplaceUpdateFiles()
	{
		int j;
		for (j = 0; j <= this.finishNum; j++)
		{
			string text = this.ExtTemp + j.ToString();
			Debug.LogError("GetFiles ext:*" + text);
			string[] files = Directory.GetFiles(this.dataPath, "*" + text, 1);
			if (files != null)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Replace ",
					text,
					"files.Length",
					files.Length
				}));
			}
			try
			{
				int i;
				for (i = 0; i < files.Length; i++)
				{
					File.Copy(files[i], files[i].Replace(text, string.Empty), true);
					File.Delete(files[i]);
					Loom.Current.QueueOnMainThread(delegate
					{
						PreloadingUIView.SetProgress((float)(i + 1) / (float)(files.Length + 1) / (float)this.finishNum * (float)j);
					});
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("ReplaceUpdateFiles Exception has been thrown!");
				Debug.LogError(ex.ToString());
				Loom.Current.QueueOnMainThread(delegate
				{
					NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621293, false), GameDataUtils.GetChineseContent(621294, false), new Action(ClientApp.QuitApp), new Action(this.DownloadInstallPackage), GameDataUtils.GetChineseContent(621286, false), GameDataUtils.GetChineseContent(621295, false));
				});
				return;
			}
		}
		Loom.Current.QueueOnMainThread(delegate
		{
			this.ReplaceUpdateFilesFinish();
		});
	}

	private void ReplaceUpdateFilesFinish()
	{
		Debug.LogError("ReplaceUpdateFiles-------Finish>>>>>>>>>>>>>>>>>UpdateClientVersionFile");
		this.UpdateClientVersionFile();
		PlayerPrefs.SetInt("FinishedUpdateBaseId", -1);
		PlayerPrefs.SetInt("FinishedUpdatePatchId", -1);
		this.ReplaceFinish.Invoke();
	}

	private void UnZipFileFinish()
	{
		Debug.LogError("UnZipFileFinish>>>>>>>>>>>>>>>>>UpdateClientVersionFile");
		this.UpdateClientVersionFile();
		PlayerPrefs.SetInt("FinishedUpdateBaseId", -1);
		PlayerPrefs.SetInt("FinishedUpdatePatchId", -1);
		this.UnZipFinish.Invoke();
	}

	private void OnContinue()
	{
		this.CheckReplacePackage(new Action(this.DownloadSeverVersion));
	}

	public void UpdateClientVersionFile()
	{
		string text = GameManager.LocalVersionFileName();
		string[] localVersions = this.GetLocalVersions();
		string[] cfgVersions = this.GetCfgVersions();
		for (int i = 0; i < localVersions.Length; i++)
		{
			if (cfgVersions[i] != "-1")
			{
				localVersions[i] = cfgVersions[i];
			}
		}
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		using (FileStream fileStream = new FileStream(text, 4))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream))
			{
				streamWriter.WriteLine(string.Join(".", localVersions));
				streamWriter.Close();
			}
		}
	}

	public void UpdateClientVersionFileCfg(string[] versions)
	{
		string text = GameManager.LocalVersionFileCfg();
		string[] localVersions = this.GetLocalVersions();
		for (int i = 0; i < localVersions.Length; i++)
		{
			if (versions[i] != "-1")
			{
				localVersions[i] = versions[i];
			}
		}
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		using (FileStream fileStream = new FileStream(text, 4))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream))
			{
				streamWriter.WriteLine(string.Join(".", localVersions));
				streamWriter.Close();
			}
		}
	}

	public string[] GetLocalVersions()
	{
		string[] array = new string[]
		{
			"-1",
			"-1",
			"-1",
			"-1",
			"-1"
		};
		if (File.Exists(GameManager.LocalVersionFileName()))
		{
			string text = GameManager.LocalVersionFileName();
			using (FileStream fileStream = new FileStream(text, 3))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					array = streamReader.ReadToEnd().Trim().Split(new char[]
					{
						".".get_Chars(0)
					});
					streamReader.Close();
				}
			}
			string[] clientVersion = this.GetClientVersion();
			array[0] = clientVersion[0];
			Debug.Log("out config");
		}
		else
		{
			array = this.GetClientVersion();
		}
		Debug.Log(array.Pack(","));
		return array;
	}

	public string GetLocalVersionsString()
	{
		string[] localVersions = this.GetLocalVersions();
		return string.Join(".", localVersions);
	}

	private string[] GetClientVersion()
	{
		return XUtility.GetConfigTxt("client_version", ".txt").Trim().Split(new char[]
		{
			'.'
		});
	}

	public string[] GetServerVersions()
	{
		string text = Path.Combine(GameManager.AssetBundleBuildRoot, "server_version.txt");
		string[] result = new string[]
		{
			"-1",
			"0",
			"0",
			"-1"
		};
		if (File.Exists(text))
		{
			using (FileStream fileStream = new FileStream(text, 3))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					result = streamReader.ReadToEnd().Trim().Split(new char[]
					{
						".".get_Chars(0)
					});
					streamReader.Close();
				}
			}
		}
		return result;
	}

	public string[] GetCfgVersions()
	{
		string text = GameManager.LocalVersionFileCfg();
		string[] result = new string[]
		{
			"-1",
			"-1",
			"-1",
			"-1",
			"-1"
		};
		if (File.Exists(text))
		{
			using (FileStream fileStream = new FileStream(text, 3))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					result = streamReader.ReadToEnd().Trim().Split(new char[]
					{
						".".get_Chars(0)
					});
					streamReader.Close();
				}
			}
		}
		return result;
	}

	private void DownloadInstallPackage()
	{
		if (!this.IsWifi())
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621270, false), GameDataUtils.GetChineseContent(621267, false), new Action(this.GoDownloadInstallPackage), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
		else
		{
			this.GoDownloadInstallPackage();
		}
	}

	private void GoDownloadInstallPackage()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		string text = "http://swejackies.com.cn/Android/ceshixiazai.apk";
		list.Add(text);
		list2.Add(Path.Combine(GameManager.AssetBundleBuildRoot, "Apocalypse.apk"));
		Downloader.Instance.Download(list, list2, null, null, new Action<int, int, string>(this.ShowState), new Action<bool>(this.InstallNew));
	}

	private void InstallNew(bool isFinish)
	{
		TimerHeap.DelTimer(this.t);
		TimerHeap.DelTimer(this.tm);
		if (isFinish)
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621297, false), GameDataUtils.GetChineseContent(621305, false), delegate
			{
				NativeCallManager.InstallPackage(Path.Combine(GameManager.AssetBundleBuildRoot, "Apocalypse.apk"));
			}, new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
		else
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621297, false), GameDataUtils.GetChineseContent(621304, false), new Action(this.DownloadInstallPackage), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621285, false), GameDataUtils.GetChineseContent(621286, false));
		}
	}

	private void ShowState(int downloadedSize, int fileSize, string tips)
	{
		this.ShowSpeed();
		this.tm = TimerHeap.AddTimer(0u, 1000, delegate
		{
			string progressName = string.Concat(new string[]
			{
				GameDataUtils.GetChineseContent(621303, false),
				((float)Downloader.Instance.localFileSize / 1024f / 1024f).ToString("0.00"),
				"MB /",
				((float)fileSize / 1024f / 1024f).ToString("0.00"),
				" MB"
			});
			PreloadingUIView.SetProgressName(progressName);
			PreloadingUIView.SetProgress((float)Downloader.Instance.localFileSize / (float)fileSize);
		});
	}

	private void ShowSpeed()
	{
		this.t = TimerHeap.AddTimer(0u, 1000, delegate
		{
			string speed = string.Format("({0})", (Downloader.Instance.Alreadyread / 1024).ToString() + "kb/s");
			PreloadingUIView.SetSpeed(speed);
			Downloader.Instance.Alreadyread = 0;
		});
	}

	private void DownloadSeverVersion()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		string text = string.Format(AppConst.ServerUrl, SerializeUtility.GetRuntimePlatformFolderName(Application.get_platform()));
		list.Add(text);
		list2.Add(Path.Combine(GameManager.AssetBundleBuildRoot, "server_version.txt"));
		string progressName = string.Format(GameDataUtils.GetChineseContent(621301, false), new object[0]);
		PreloadingUIView.SetProgressName(progressName);
		PreloadingUIView.SetProgress(0.1f);
		Downloader.Instance.Download(list, list2, null, null, null, new Action<bool>(this.CheckVersion));
	}

	public void CheckVersion(bool isSuccess = true)
	{
		if (!isSuccess)
		{
			Debug.LogError("DownloadServerVersions Failure！");
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621283, false), GameDataUtils.GetChineseContent(621284, false), new Action(this.DownloadSeverVersion), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621285, false), GameDataUtils.GetChineseContent(621286, false));
			return;
		}
		this.DoCheckVersion();
	}

	private void DoCheckVersion()
	{
		this.serverVersions = this.GetServerVersions();
		if (int.Parse(this.serverVersions[0]) < 0)
		{
			Debug.LogError("GetServerVersions Failure！serverVersions[0]: " + this.serverVersions[0]);
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621283, false), GameDataUtils.GetChineseContent(621284, false), new Action(this.DownloadSeverVersion), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621285, false), GameDataUtils.GetChineseContent(621286, false));
			return;
		}
		this.clientVersions = this.GetLocalVersions();
		if (int.Parse(this.clientVersions[2]) < 0)
		{
			Debug.LogError("GetLocalVersions Failure！clientVersions[0]: " + this.clientVersions[0]);
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621283, false), GameDataUtils.GetChineseContent(621296, false), new Action(this.Awake), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621285, false), GameDataUtils.GetChineseContent(621286, false));
			return;
		}
		if (int.Parse(this.clientVersions[0]) < int.Parse(this.serverVersions[0]))
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621297, false), GameDataUtils.GetChineseContent(621298, false), new Action(this.DownloadInstallPackage), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621289, false), GameDataUtils.GetChineseContent(621290, false));
			return;
		}
		if (this.clientVersions[2] == "0")
		{
			DownloaderManager.Instance.DowanloadUpdatePackage(1, 0, null, new Action<bool>(this.GetIndexFileSize), false, false);
			return;
		}
		if (int.Parse(this.clientVersions[2]) <= int.Parse(this.serverVersions[2]) && int.Parse(this.clientVersions[3]) < int.Parse(this.serverVersions[3]))
		{
			DownloaderManager.Instance.DowanloadUpdatePackage(0, int.Parse(this.serverVersions[3]), null, new Action<bool>(this.GetIndexFileSize), false, false);
		}
		else
		{
			TimerHeap.DelTimer(this.t);
			this.OnResourceInited();
		}
	}

	private void GetIndexFileSize(bool isFinish)
	{
		Debug.Log(">>>>>>>>>>>>>>GetIndexFileSize");
		TimerHeap.DelTimer(this.t);
		PreloadingUIView.SetSpeed(string.Empty);
		if (!isFinish)
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621270, false), GameDataUtils.GetChineseContent(621269, false), new Action(this.DownloadSeverVersion), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
		else
		{
			DownloaderManager.Instance.GetZipFiles(new Action<long>(this.AnalysisFinish), false);
		}
	}

	private void AnalysisFinish(long _fileSize)
	{
		if (_fileSize > 0L)
		{
			this.fileSize = (float)_fileSize / 1024f / 1024f;
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621270, false), string.Format(GameDataUtils.GetChineseContent(621266, false), this.fileSize.ToString("0.00")), new Action(this.CheckNetworkReachability), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
		else
		{
			this.FinishCallback(true);
		}
	}

	private void CheckNetworkReachability()
	{
		if (this.IsWifi())
		{
			this.DoDown();
		}
		else
		{
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621270, false), GameDataUtils.GetChineseContent(621267, false), new Action(this.DoDown), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
	}

	private void DoDown()
	{
		this.ShowSpeed();
		DownloaderManager.Instance.DownloadPackage(new Action<float, float>(this.ProgressCallback), new Action<bool>(this.FinishCallback));
	}

	private bool IsWifi()
	{
		return Application.get_internetReachability() == 2;
	}

	private void ProgressCallback(float downloadedSize, float TotalSize)
	{
		string progressName = string.Concat(new string[]
		{
			GameDataUtils.GetChineseContent(621268, false),
			downloadedSize.ToString("0.00"),
			"MB /",
			this.fileSize.ToString("0.00"),
			" MB"
		});
		PreloadingUIView.SetProgressName(progressName);
		PreloadingUIView.SetProgress(downloadedSize / this.fileSize);
	}

	private void FinishCallback(bool isFinish)
	{
		TimerHeap.DelTimer(this.t);
		PreloadingUIView.SetSpeed(string.Empty);
		if (isFinish)
		{
			string chineseContent = GameDataUtils.GetChineseContent(621311, false);
			PreloadingUIView.SetProgressName(chineseContent);
			string[] array = new string[]
			{
				"-1",
				"-1",
				"-1",
				"-1",
				"-1"
			};
			if (this.clientVersions[2] == "0")
			{
				PlayerPrefs.SetInt("FinishedUpdateBaseId", int.Parse(this.clientVersions[2]) + 1);
				array[2] = (int.Parse(this.clientVersions[2]) + 1).ToString();
			}
			else
			{
				PlayerPrefs.SetInt("FinishedUpdatePatchId", int.Parse(this.clientVersions[3]) + 1);
				array[3] = (int.Parse(this.clientVersions[3]) + 1).ToString();
			}
			this.UpdateClientVersionFileCfg(array);
			if (DownloaderManager.Instance.HasFileWaitDownload)
			{
				this.CheckUnZipFile(new Action(this.DoDown));
			}
			else
			{
				this.CheckUnZipFile(new Action(this.DoCheckVersion));
			}
		}
		else
		{
			string progressName = "更新失败";
			PreloadingUIView.SetProgressName(progressName);
			NewContinueUI.OpenAsNormal(GameDataUtils.GetChineseContent(621270, false), GameDataUtils.GetChineseContent(621269, false), new Action(this.OnContinue), new Action(ClientApp.QuitApp), GameDataUtils.GetChineseContent(621271, false), GameDataUtils.GetChineseContent(621272, false));
		}
	}

	private void OnResourceInited()
	{
		Debug.Log("Initialize OK!!!");
		this.OnInitialize();
	}

	private void OnInitialize()
	{
		base.get_gameObject().AddComponent<ClientApp>();
	}

	private void OnDestroy()
	{
	}

	private void InitBugly()
	{
		BuglyAgent.ConfigDebugMode(SystemConfig.IsBuglySDKLogOn);
		BuglyAgent.ConfigDefault(SDKManager.Instance.GetSDKName(), this.GetLocalVersionsString(), SystemInfoTools.GetDeviceName(), 0L);
		BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogError);
		BuglyAgent.ConfigAutoQuitApplication(false);
		BuglyAgent.InitWithAppId("900057844");
		BuglyAgent.EnableExceptionHandler();
	}

	private void Update()
	{
		this.CheckShowDebugInfo();
		if (Input.get_touchCount() == 6 && !SDKManager.Instance.HasSDK())
		{
			AppConst.UseLAN = true;
		}
		if (this.CurrentUpdateManager != null)
		{
			this.CurrentUpdateManager.Update();
		}
	}

	private void CheckShowDebugInfo()
	{
		this.IsInDebugInfoCountStatus = (Input.get_touchCount() >= 5);
		if (this.IsInDebugInfoCountStatus)
		{
			this.time += Time.get_deltaTime();
			if (this.time > 2.5f && !SDKManager.Instance.HasSDK())
			{
				ClientGMManager.ShowDebugInfo(true);
			}
		}
		else
		{
			this.time = 0f;
		}
	}

	private void OnGUI()
	{
		if (!GameManager.IsDebug)
		{
			return;
		}
		this.TestSetting();
		this.LoadTestGUI();
		this.SDKTestGUI();
		this.NotificationGUI();
	}

	private void TestSetting()
	{
		if (this.IsTestSetting)
		{
			return;
		}
		this.IsTestSetting = true;
		SystemConfig.LogSetting(true);
		UIUtils.SetHardwareResolution(960);
	}

	private Rect GetRect(int index)
	{
		return new Rect((float)(20 + 160 * (index % 5)), (float)(80 + 50 * (index / 5 - 1)), 150f, 45f);
	}

	private void LoadTestGUI()
	{
		int num = -1;
		num++;
		this.resName = GUI.TextField(new Rect(100f, 20f, 300f, 30f), this.resName);
		num += 5;
		if (GUI.Button(this.GetRect(num), "unload asset"))
		{
			AssetBundleLoader.Instance.Initialize(null);
			Debug.LogError("begin " + Time.get_time());
			AssetBundleLoader.Instance.UnloadAB(this.resName, delegate(bool isOK)
			{
				Debug.LogError("done " + Time.get_time());
			}, null);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "load asset"))
		{
			AssetBundleLoader.Instance.Initialize(null);
			Debug.LogError("begin " + Time.get_time());
			AssetBundleLoader.Instance.AsyncLoadAB(this.resName, delegate(bool isOK)
			{
				Debug.LogError("done " + Time.get_time());
			}, null);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "print all"))
		{
			List<string> list = new List<string>();
			using (Dictionary<string, Object>.Enumerator enumerator = AssetBundleLoader.Instance.m_alreadyLoaded.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, Object> current = enumerator.get_Current();
					list.Add(current.get_Key());
				}
			}
			Debug.LogError("==>[print all]");
			list.Sort();
			using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string current2 = enumerator2.get_Current();
					Debug.LogError(current2);
				}
			}
		}
		num++;
		if (GUI.Button(this.GetRect(num), "ref stack"))
		{
			Debug.LogError("==>[ref stack]");
			AssetBundleLoader.Instance.OutputStackTrace(this.resName);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "load jni"))
		{
			Debug.LogError("==>[load jni]");
			Debug.LogError(NativeCallManager.ContainsInAssets(string.Empty, "abmap.txt"));
			Debug.LogError(Encoding.get_UTF8().GetString(NativeCallManager.getFromAssets("abmap.txt")));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "test shader"))
		{
			Debug.LogError("==>[test shader]");
			ShaderManager.BeginInit(delegate
			{
				for (int i = 0; i < ShaderManager.Instance.listShader.Length; i++)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"index = ",
						i,
						", shader = ",
						ShaderManager.Instance.listShader[i].get_name()
					}));
				}
				for (int j = 0; j < ShaderManager.Instance.listShader.Length; j++)
				{
					if (!ShaderManager.Instance.listShader[j].get_isSupported())
					{
						Debug.LogError("index = " + j + ", shader no supported");
					}
				}
			});
		}
		num++;
		if (GUI.Button(this.GetRect(num), "test shader now"))
		{
			Debug.LogError("==>[test shader now]");
			Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPath("Shader", string.Empty), typeof(Object));
			if (@object != null)
			{
				Debug.LogError("shader asset not null");
			}
			else
			{
				Debug.LogError("shader asset is null");
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator test1()
	{
		return new GameManager.<test1>c__Iterator2E();
	}

	private void SDKTestGUI()
	{
		int num = 14;
		num++;
		if (GUI.Button(this.GetRect(num), "172.19.1.47:8000"))
		{
			NativeCallManager.m_isTest = true;
			LoginManager.m_host = "172.19.1.47";
			LoginManager.m_port = 8000;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "172.19.1.15:7740"))
		{
			LoginManager.m_host = "172.19.1.15";
			LoginManager.m_port = 7740;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "172.19.8.101:8200"))
		{
			LoginManager.m_host = "172.19.8.101";
			LoginManager.m_port = 8200;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "172.19.8.194:7710"))
		{
			LoginManager.m_host = "172.19.8.194";
			LoginManager.m_port = 7710;
		}
		num = 19;
		num++;
		if (GUI.Button(this.GetRect(num), "1.连接网络"))
		{
			NetworkService.Instance.Init();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Login方式1\n(没有多方式的任意选择)"))
		{
			SDKManager.Instance.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 1);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Login方式2\n(没有多方式的任意选择)"))
		{
			SDKManager.Instance.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 2);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Login方式3\n(没有多方式的任意选择)"))
		{
			SDKManager.Instance.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 3);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Pay(0.1rmb)"))
		{
			SDKManager.Instance.Pay("1", "商品名称", 0.10000000149011612);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Pay(1rmb)"))
		{
			SDKManager.Instance.Pay("2", "商品名称", 1.0);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "submitExtendData"))
		{
			SDKManager.Instance.SubmitExtendData(null, string.Empty, string.Empty);
		}
	}

	private void NotificationGUI()
	{
		int num = 29;
		num++;
		num++;
		if (GUI.Button(this.GetRect(num), "一次推送"))
		{
			for (int i = 1; i < 100; i++)
			{
				NativeCallManager.NotificationMessage(i, "一次推送" + i, DateTime.get_Now().AddSeconds((double)(i * 60)), NotificationRepeatInterval.None);
			}
		}
		num++;
		if (GUI.Button(this.GetRect(num), "循环推送1"))
		{
			NativeCallManager.NotificationMessage(1000, "循环推送1", DateTime.get_Now().AddSeconds(5.0), NotificationRepeatInterval.ForTest);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "循环推送2"))
		{
			NativeCallManager.NotificationMessage(1000, "循环推送2", DateTime.get_Now().AddSeconds(5.0), NotificationRepeatInterval.ForTest2);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "清空"))
		{
			Debug.LogError("[推送]清理");
			LocalForAndroidManager.CancelNotification(1);
			LocalForAndroidManager.CancelNotification(2);
			LocalForAndroidManager.CancelNotification(3);
		}
	}

	public string GetApkPatchUrl(string relToPkgName)
	{
		if (string.IsNullOrEmpty(this.ApkPatchRoot))
		{
			Debug.Log("GetApkPatchUrl");
			string androidPackageName = NativeCallManager.GetAndroidPackageName();
			Debug.LogFormat("package name :{0}", new object[]
			{
				androidPackageName
			});
			this.ApkPatchRoot = string.Format("{0}/apkPatch/{1}/{2}", this.PatchUrlRoot, androidPackageName, this.ServerVersionCode);
		}
		return string.Format("{0}/{1}", this.ApkPatchRoot, relToPkgName);
	}

	public string GetResPatchUrl(string rel)
	{
		return string.Format("{0}/resPatch/{1}", this.PatchUrlRoot, rel);
	}
}
