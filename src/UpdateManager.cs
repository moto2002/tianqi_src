using BsDiff;
using FSM;
using LitJson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public class UpdateManager : FSMSystem<UpdateEvent, UpdateState>
{
	private enum PatchFileType
	{
		None,
		Extend,
		ExtendPatch,
		CorePatch,
		ApkPatch,
		Apk,
		Max
	}

	private class FileBaseInfo
	{
		public string Md5 = string.Empty;

		public int FileSize;
	}

	private class DownloadFileInfo
	{
		public UpdateManager.PatchFileType FileType;

		public string Url = string.Empty;

		private string localPath = string.Empty;

		public UpdateManager.FileBaseInfo TargetBaseInfo;

		public string LocalPath
		{
			get
			{
				return this.localPath;
			}
			set
			{
				this.localPath = value;
				this.TargetFileInfo = new FileInfo(this.localPath);
			}
		}

		public FileInfo TargetFileInfo
		{
			get;
			private set;
		}

		public bool IfFull()
		{
			this.TargetFileInfo.Refresh();
			return this.TargetFileInfo.get_Exists() && this.TargetFileInfo.get_Length() == (long)this.TargetBaseInfo.FileSize;
		}
	}

	private class ApkPatchInfo
	{
		public UpdateManager.FileBaseInfo ApkFileInfo;

		public Dictionary<string, UpdateManager.FileBaseInfo> PatchFileInfo;
	}

	private class ResPatchInfo
	{
		public string InstallOrder = string.Empty;

		public string ExtendVersion = string.Empty;

		public UpdateManager.FileBaseInfo ExtendFileInfo;

		public Dictionary<string, UpdateManager.FileBaseInfo> CorePatchMap = new Dictionary<string, UpdateManager.FileBaseInfo>();

		public Dictionary<string, UpdateManager.FileBaseInfo> ExtendPatchMap = new Dictionary<string, UpdateManager.FileBaseInfo>();

		public UpdateManager.FileBaseInfo TryGetCorePatch(string version)
		{
			UpdateManager.FileBaseInfo fileBaseInfo = null;
			return (!this.CorePatchMap.TryGetValue(version, ref fileBaseInfo)) ? null : fileBaseInfo;
		}

		public UpdateManager.FileBaseInfo TryGetExtendPatch(string version)
		{
			UpdateManager.FileBaseInfo fileBaseInfo = null;
			return (!this.ExtendPatchMap.TryGetValue(version, ref fileBaseInfo)) ? null : fileBaseInfo;
		}
	}

	private class UpdatePatchInfo
	{
		public bool IsCoreUpdate;

		public bool IsExtendUpdate;

		public bool IsDownloadExtend;

		public int VersionCode;

		public UpdateManager.ApkPatchInfo CurrentApkInfo;

		public UpdateManager.ResPatchInfo CurrentResInfo;

		public Dictionary<UpdateManager.PatchFileType, UpdateManager.DownloadFileInfo> DownloadFiles = new Dictionary<UpdateManager.PatchFileType, UpdateManager.DownloadFileInfo>();

		public long DownloadAmmount;

		public bool IsResUpdate()
		{
			return this.IsCoreUpdate || this.IsExtendUpdate || this.IsDownloadExtend;
		}

		public bool IsApkUpdate()
		{
			return this.VersionCode != 0;
		}

		public void AddDownloadFile(UpdateManager.DownloadFileInfo info)
		{
			this.DownloadAmmount += (long)info.TargetBaseInfo.FileSize;
			if (info.TargetFileInfo.get_Exists() && info.TargetFileInfo.get_Length() > (long)info.TargetBaseInfo.FileSize)
			{
				info.TargetFileInfo.Delete();
				info.TargetFileInfo.Refresh();
			}
			Debug.LogFormat("添加更新文件:{0}", new object[]
			{
				info.Url
			});
			this.DownloadFiles.Add(info.FileType, info);
		}

		public UpdateManager.DownloadFileInfo GetDownloadInfo(UpdateManager.PatchFileType type)
		{
			UpdateManager.DownloadFileInfo downloadFileInfo;
			return (!this.DownloadFiles.TryGetValue(type, ref downloadFileInfo)) ? null : downloadFileInfo;
		}

		public long GetCurrentFilesSize()
		{
			long num = 0L;
			for (UpdateManager.PatchFileType patchFileType = UpdateManager.PatchFileType.Extend; patchFileType < UpdateManager.PatchFileType.Max; patchFileType++)
			{
				UpdateManager.DownloadFileInfo downloadInfo = this.GetDownloadInfo(patchFileType);
				if (downloadInfo != null)
				{
					downloadInfo.TargetFileInfo.Refresh();
					if (downloadInfo.TargetFileInfo.get_Exists())
					{
						num += downloadInfo.TargetFileInfo.get_Length();
					}
				}
			}
			return num;
		}
	}

	private enum ResPathDir
	{
		corePatch,
		extendPatch,
		extend,
		config
	}

	private const string KeyCoreVersion = "CoreVersion";

	private const string KeyExtendVersion = "ExtendVersion";

	private const float BToMB = 1048576f;

	private UpdateManager.UpdatePatchInfo UpdateInfo;

	private CultureInfo DefaultCulture = new CultureInfo("fr-FR");

	private Downloader CurrentDownloader = new Downloader();

	private static readonly DateTime DefaultExtendVersion = DateTime.MaxValue;

	private bool ShouldExtendDownload;

	private List<UpdateManager.PatchFileType> InstallOrder = new List<UpdateManager.PatchFileType>();

	private Action OnUpdateEnd;

	private int RecordCount;

	private bool Downloading;

	private Action InstallUpdate;

	private int CurrentInstallIndex = -1;

	private int InstallCount;

	private FileInfo MergeFile;

	public bool Updating
	{
		get;
		private set;
	}

	protected override void DoInit()
	{
		this.InitAllState();
		this.InitAllTransition();
	}

	private void InitAllState()
	{
		base.InitState(UpdateState.Init, new FSMState.EnterCallback(this.OnInitEnter), new FSMState.UpdateCallback(this.OnInitUpdate), null);
		base.InitState(UpdateState.CheckVersion, new FSMState.EnterCallback(this.OnCheckVersionEnter), null, null);
		base.InitState(UpdateState.CleanPatch, new FSMState.EnterCallback(this.OnCleanPatchEnter), null, null);
		base.InitState(UpdateState.GetUpdateInfo, new FSMState.EnterCallback(this.OnGetUpdateInfoEnter), null, null);
		base.InitState(UpdateState.ShouldDownload, new FSMState.EnterCallback(this.OnShouldDownloadEnter), new FSMState.UpdateCallback(this.OnShouldDownloadUpdate), null);
		base.InitState(UpdateState.DownloadPatch, new FSMState.EnterCallback(this.OnDownloadPatchEnter), new FSMState.UpdateCallback(this.OnDownloadPatchUpdate), null);
		base.InitState(UpdateState.ValidatePatch, new FSMState.EnterCallback(this.OnValidatePatchEnter), null, null);
		base.InitState(UpdateState.AskForDownloadAgain, new FSMState.EnterCallback(this.OnAskForDownloadAgainEnter), null, null);
		base.InitState(UpdateState.InstallPatch, new FSMState.EnterCallback(this.OnInstallPatchEnter), new FSMState.UpdateCallback(this.OnInstallPatchUpdate), null);
		base.InitState(UpdateState.Restart, new FSMState.EnterCallback(this.OnRestartEnter), null, null);
		base.InitState(UpdateState.End, new FSMState.EnterCallback(this.OnEndEnter), null, null);
	}

	private void InitAllTransition()
	{
		base.InitTransition(UpdateState.Init, UpdateEvent.Next, UpdateState.CheckVersion);
		base.InitTransition(UpdateState.CheckVersion, UpdateEvent.True, UpdateState.GetUpdateInfo);
		base.InitTransition(UpdateState.CheckVersion, UpdateEvent.False, UpdateState.CleanPatch);
		base.InitTransition(UpdateState.CleanPatch, UpdateEvent.Next, UpdateState.End);
		base.InitTransition(UpdateState.GetUpdateInfo, UpdateEvent.Next, UpdateState.ShouldDownload);
		base.InitTransition(UpdateState.ShouldDownload, UpdateEvent.True, UpdateState.DownloadPatch);
		base.InitTransition(UpdateState.ShouldDownload, UpdateEvent.False, UpdateState.ValidatePatch);
		base.InitTransition(UpdateState.DownloadPatch, UpdateEvent.Next, UpdateState.ValidatePatch);
		base.InitTransition(UpdateState.ValidatePatch, UpdateEvent.True, UpdateState.InstallPatch);
		base.InitTransition(UpdateState.ValidatePatch, UpdateEvent.False, UpdateState.AskForDownloadAgain);
		base.InitTransition(UpdateState.AskForDownloadAgain, UpdateEvent.True, UpdateState.ShouldDownload);
		base.InitTransition(UpdateState.InstallPatch, UpdateEvent.True, UpdateState.Restart);
		base.InitTransition(UpdateState.InstallPatch, UpdateEvent.False, UpdateState.AskForDownloadAgain);
	}

	private bool IsResourcesUpdateEnable()
	{
		return !string.IsNullOrEmpty(GameManager.Instance.ResVersion);
	}

	public void DownloadServerListAndUpdate()
	{
		LoginManager.Instance.GetServerListFile(LoginManager.AddressType.DOMAIN);
		this.Resume(false, null);
	}

	public bool IsExtendDownloaded()
	{
		bool result = true;
		if (this.IsResourcesUpdateEnable() && Application.get_platform() == 11)
		{
			result = !UpdateManager.DefaultExtendVersion.Equals(this.GetExtendVersion());
		}
		return result;
	}

	public void Resume(bool shoudExtendDownload, Action onUpdateEnd)
	{
		this.Updating = false;
		this.ShouldExtendDownload = shoudExtendDownload;
		this.OnUpdateEnd = onUpdateEnd;
		if (base.CurrentState != null)
		{
			UpdateState stateID = (UpdateState)base.CurrentState.StateID;
			UpdateState updateState = stateID;
			if (updateState != UpdateState.Init && updateState != UpdateState.End)
			{
				Debug.LogError("当前状态不为停止状态");
			}
		}
		base.Resume(UpdateState.Init);
	}

	private void InitInstallOrder()
	{
		this.InstallOrder.Clear();
		if (this.UpdateInfo.CurrentResInfo == null)
		{
			for (UpdateManager.PatchFileType patchFileType = UpdateManager.PatchFileType.Extend; patchFileType < UpdateManager.PatchFileType.Max; patchFileType++)
			{
				this.InstallOrder.Add(patchFileType);
			}
		}
		else
		{
			string[] array = this.UpdateInfo.CurrentResInfo.InstallOrder.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				try
				{
					UpdateManager.PatchFileType patchFileType2 = (UpdateManager.PatchFileType)((int)Enum.Parse(typeof(UpdateManager.PatchFileType), text));
					this.InstallOrder.Add(patchFileType2);
				}
				catch
				{
					Debug.LogFormat("{0}类型解析错误:{1}", new object[]
					{
						typeof(UpdateManager.PatchFileType),
						text
					});
				}
			}
		}
	}

	private DateTime StringToDateTime(string strTime)
	{
		if (string.IsNullOrEmpty(strTime) || strTime.get_Length() != 10)
		{
			return DateTime.MinValue;
		}
		return DateTime.ParseExact(strTime, "yyMMddHHmm", this.DefaultCulture);
	}

	private string DateToString(DateTime date)
	{
		return date.ToString("yyMMddHHmm");
	}

	private string GetVersionFileText(string key)
	{
		string text = PathUtil.Combine(new string[]
		{
			PathSystem.PersistentDataPath,
			key
		});
		string result = string.Empty;
		if (File.Exists(text))
		{
			result = File.ReadAllText(text);
		}
		return result;
	}

	private void SetVersionFileText(string key, string value)
	{
		string text = PathUtil.Combine(new string[]
		{
			PathSystem.PersistentDataPath,
			key
		});
		string directoryName = Path.GetDirectoryName(text);
		DirectoryUtil.CreateIfNotExist(directoryName);
		File.WriteAllText(text, value);
	}

	private DateTime GetCoreVersion()
	{
		string text = this.GetVersionFileText("CoreVersion");
		if (string.IsNullOrEmpty(text))
		{
			if (Application.get_isMobilePlatform())
			{
				text = GameManager.Instance.GetLocalVersions()[3];
			}
			else
			{
				text = "1704111554";
			}
		}
		DateTime result = DateTime.MinValue;
		if (!string.IsNullOrEmpty(text))
		{
			result = this.StringToDateTime(text);
		}
		return result;
	}

	private DateTime GetExtendVersion()
	{
		string text = this.GetVersionFileText("ExtendVersion");
		DateTime result = UpdateManager.DefaultExtendVersion;
		if (string.IsNullOrEmpty(text))
		{
			if (NativeCallManager.ContainsInAssets(string.Empty, PathSystem.ClientExtendVersion))
			{
				byte[] fromAssets = NativeCallManager.getFromAssets(PathSystem.ClientExtendVersion);
				string @string = Encoding.get_UTF8().GetString(fromAssets);
				Debug.LogFormat("{0}:{1}", new object[]
				{
					PathSystem.ClientExtendVersion,
					@string
				});
				string[] array = @string.Split(new char[]
				{
					'.'
				});
				if (array.Length >= 4)
				{
					text = array[3];
				}
			}
			else
			{
				Debug.Log("未找到:" + PathSystem.ClientExtendVersion);
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			result = this.StringToDateTime(text);
		}
		return result;
	}

	private bool IsNewThan(DateTime newTarget, DateTime oldTarget)
	{
		return newTarget.CompareTo(oldTarget) > 0;
	}

	private string GetApkPatchInfoPath()
	{
		return PathSystem.GetPath(PathType.ApkPatchPath, PathSystem.ApkPatchInfoFileName);
	}

	private string GetDownloadPatchRelPath()
	{
		int versionCode = NativeCallManager.GetVersionCode();
		int serverVersionCode = GameManager.Instance.ServerVersionCode;
		return string.Format("{0}_{1}.patch", versionCode, serverVersionCode);
	}

	private string GetResPatchDir(UpdateManager.ResPathDir type)
	{
		return PathSystem.GetPath(PathType.ResPatch, type.ToString());
	}

	private string GetResPatchRelPath(UpdateManager.ResPathDir type, string version)
	{
		string result = string.Empty;
		switch (type)
		{
		case UpdateManager.ResPathDir.corePatch:
		case UpdateManager.ResPathDir.extendPatch:
			result = string.Format("{0}/{1}/{2}.zip", type, GameManager.Instance.ResVersion, version);
			break;
		case UpdateManager.ResPathDir.extend:
			result = string.Format("{0}/{1}.zip", type, version);
			break;
		case UpdateManager.ResPathDir.config:
			result = string.Format("config/{0}.json", GameManager.Instance.ResVersion);
			break;
		}
		return result;
	}

	private string GetResPatchConfigPath()
	{
		return PathSystem.GetPath(PathType.ResPatch, this.GetResPatchRelPath(UpdateManager.ResPathDir.config, string.Empty));
	}

	private string GetResPatchPath(UpdateManager.ResPathDir type, string version)
	{
		return PathSystem.GetPath(PathType.ResPatch, this.GetResPatchRelPath(type, version));
	}

	private string GetResPatchUrl(UpdateManager.ResPathDir type, string version)
	{
		string resPatchRelPath = this.GetResPatchRelPath(type, version);
		return GameManager.Instance.GetResPatchUrl(resPatchRelPath);
	}

	private string GetSizeString(long downloadSize)
	{
		return string.Format("{0:f2}", (float)downloadSize / 1048576f);
	}

	private void ShowDownloadFailed(Action onclick)
	{
		Debug.LogError("补丁下载失败");
		DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "更新出错，请检查网络后重试", onclick, "确定", "button_orange_1", UINodesManager.T4RootOfSpecial);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void OnInitEnter(int eventID, FSMState preState, FSMState currentState)
	{
		Debug.Log("UpdateManager init");
		this.UpdateInfo = new UpdateManager.UpdatePatchInfo();
		this.OnInitUpdate(null);
	}

	private void OnInitUpdate(FSMState currentState)
	{
		if (LoginManager.Instance.IsAnalysisSuccess)
		{
			base.React(UpdateEvent.Next);
		}
	}

	private void OnCheckVersionEnter(int eventID, FSMState preState, FSMState currentState)
	{
		bool flag = false;
		if (Singleton<SwitchFile>.S.IsFileExist(SwitchFile.FileName.NoUpdate))
		{
			flag = false;
		}
		else if (Application.get_platform() == 11)
		{
			DateTime newTarget = this.StringToDateTime(GameManager.Instance.ResVersion);
			DateTime coreVersion = this.GetCoreVersion();
			if (this.IsNewThan(newTarget, coreVersion))
			{
				Debug.Log("更新corePatch");
				this.UpdateInfo.IsCoreUpdate = true;
				flag = true;
			}
			DateTime extendVersion = this.GetExtendVersion();
			if (this.IsNewThan(newTarget, extendVersion))
			{
				Debug.Log("更新extendVersion");
				this.UpdateInfo.IsExtendUpdate = true;
				flag = true;
			}
			if (this.ShouldExtendDownload && extendVersion.Equals(UpdateManager.DefaultExtendVersion))
			{
				Debug.Log("下载extend");
				this.UpdateInfo.IsDownloadExtend = true;
				flag = true;
			}
			int versionCode = NativeCallManager.GetVersionCode();
			if (GameManager.Instance.ServerVersionCode > versionCode)
			{
				this.UpdateInfo.VersionCode = GameManager.Instance.ServerVersionCode;
				flag = true;
			}
		}
		if (flag)
		{
			base.React(UpdateEvent.True);
		}
		else
		{
			base.React(UpdateEvent.False);
		}
	}

	private void ClearApk()
	{
		Debug.Log("清理所有apk");
		try
		{
			DirectoryUtil.DeleteIfExist(PathSystem.ApkDir);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ClearApkPatch()
	{
		Debug.Log("清理所有apk补丁");
		try
		{
			DirectoryUtil.DeleteIfExist(PathSystem.ApkPatchDir);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ClearCorePatch()
	{
		Debug.Log("清理所有core补丁");
		try
		{
			DirectoryUtil.DeleteIfExist(this.GetResPatchDir(UpdateManager.ResPathDir.corePatch));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ClearExtendPatch()
	{
		Debug.Log("清理所有extend补丁");
		try
		{
			DirectoryUtil.DeleteIfExist(this.GetResPatchDir(UpdateManager.ResPathDir.extendPatch));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ClearExtend()
	{
		Debug.Log("清理所有ExtendZip");
		try
		{
			DirectoryUtil.DeleteIfExist(this.GetResPatchDir(UpdateManager.ResPathDir.extend));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void OnCleanPatchEnter(int eventID, FSMState preState, FSMState currentState)
	{
		Debug.Log("清理已下载的文件");
		PreloadingUIView.SetProgressName("正在优化游戏环境...");
		Loom.Current.RunAsync(delegate
		{
			this.ClearApk();
			this.ClearApkPatch();
			this.ClearCorePatch();
			this.ClearExtendPatch();
			base.ReactSync(UpdateEvent.Next);
		});
	}

	private void OnGetUpdateInfoEnter(int eventID, FSMState preState, FSMState currentState)
	{
		if (PreloadingUIView.Instance == null || !PreloadingUIView.Instance.get_gameObject().get_activeSelf())
		{
			GameManager.Instance.OpenPreloadingUI();
		}
		this.DoGetUpdateInfo();
	}

	private void DoGetUpdateInfo()
	{
		PreloadingUIView.SetProgressName("正在获取更新信息...");
		this.Updating = true;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (this.UpdateInfo.IsResUpdate())
		{
			string resPatchUrl = GameManager.Instance.GetResPatchUrl(this.GetResPatchRelPath(UpdateManager.ResPathDir.config, string.Empty));
			list.Add(resPatchUrl);
			list2.Add(this.GetResPatchConfigPath());
		}
		if (this.UpdateInfo.IsApkUpdate())
		{
			list.Add(GameManager.Instance.GetApkPatchUrl(PathSystem.ApkPatchInfoFileName));
			list2.Add(this.GetApkPatchInfoPath());
		}
		this.CurrentDownloader.Download(list, list2, null, null, null, new Action<bool>(this.OnPatchInfoDownloaded));
	}

	private void OnPatchInfoDownloaded(bool successed)
	{
		if (successed)
		{
			base.ReactSync(UpdateEvent.Next);
			Debug.Log("更新信息下载成功");
		}
		else
		{
			this.ShowDownloadFailed(delegate
			{
				this.DoGetUpdateInfo();
			});
		}
	}

	private bool IsRecordEnd()
	{
		return this.RecordCount == 2;
	}

	private string GetNewApkName()
	{
		return string.Format("new_{0}.apk", GameManager.Instance.ServerVersionCode);
	}

	private string GetNewApkPath()
	{
		return PathSystem.GetPath(PathType.ApkPath, this.GetNewApkName());
	}

	private void OnShouldDownloadEnter(int eventID, FSMState preState, FSMState currentState)
	{
		this.RecordCount = 0;
		if (this.UpdateInfo.IsApkUpdate())
		{
			string apkPatchInfoPath = this.GetApkPatchInfoPath();
			this.UpdateInfo.CurrentApkInfo = JsonMapper.ToObject<UpdateManager.ApkPatchInfo>(File.ReadAllText(apkPatchInfoPath));
			this.RecordApkInfo();
		}
		else
		{
			this.RecordCount++;
		}
		if (this.UpdateInfo.IsResUpdate())
		{
			string resPatchConfigPath = this.GetResPatchConfigPath();
			this.UpdateInfo.CurrentResInfo = JsonMapper.ToObject<UpdateManager.ResPatchInfo>(File.ReadAllText(resPatchConfigPath));
			this.RecordResInfo();
		}
		else
		{
			this.RecordCount++;
		}
	}

	private void OnShouldDownloadUpdate(FSMState currentState)
	{
		if (this.IsRecordEnd())
		{
			if (this.UpdateInfo.GetCurrentFilesSize() == this.UpdateInfo.DownloadAmmount)
			{
				base.React(UpdateEvent.False);
			}
			else
			{
				base.React(UpdateEvent.True);
			}
		}
	}

	private string GetNewPatchName()
	{
		Debug.Log("GetNewPatchName");
		int versionCode = NativeCallManager.GetVersionCode();
		Debug.LogFormat("currentCode :{0}", new object[]
		{
			versionCode
		});
		int serverVersionCode = GameManager.Instance.ServerVersionCode;
		return string.Format("{0}_{1}_{2}.patch", versionCode, serverVersionCode, NativeCallManager.GetApkMd5());
	}

	private string GetNewPatchPath()
	{
		return PathSystem.GetPath(PathType.ApkPatchPath, this.GetNewPatchName());
	}

	private void RecordApkInfo()
	{
		Debug.Log("查询apk更新方式（整包or补丁包）");
		NativeCallManager.GetApkMd5Async(delegate(string x)
		{
			Loom.Current.QueueOnMainThread(delegate
			{
				this.DoRecordApkInfo(x);
			});
		});
	}

	private void DoRecordApkInfo(string apkMd5)
	{
		try
		{
			UpdateManager.FileBaseInfo targetBaseInfo = null;
			Debug.Log("获取apk下载信息");
			UpdateManager.DownloadFileInfo info;
			if (this.UpdateInfo.CurrentApkInfo.PatchFileInfo.TryGetValue(apkMd5, ref targetBaseInfo))
			{
				Debug.Log("下载apkpatch");
				info = new UpdateManager.DownloadFileInfo
				{
					FileType = UpdateManager.PatchFileType.ApkPatch,
					Url = GameManager.Instance.GetApkPatchUrl(this.GetNewPatchName()),
					LocalPath = this.GetNewPatchPath(),
					TargetBaseInfo = targetBaseInfo
				};
			}
			else
			{
				Debug.Log("下载apk整包");
				info = new UpdateManager.DownloadFileInfo
				{
					FileType = UpdateManager.PatchFileType.Apk,
					Url = GameManager.Instance.GetApkPatchUrl(this.GetNewApkName()),
					LocalPath = this.GetNewApkPath(),
					TargetBaseInfo = this.UpdateInfo.CurrentApkInfo.ApkFileInfo
				};
			}
			this.UpdateInfo.AddDownloadFile(info);
			this.RecordCount++;
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			throw ex;
		}
	}

	private void ShowVersionError()
	{
		DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "未找到对应版本资源，请重新下载客户端", delegate
		{
			ClientApp.QuitApp();
		}, "确 定", "button_orange_1", UINodesManager.T4RootOfSpecial);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void AddResPatchDownload(UpdateManager.PatchFileType fileType, UpdateManager.ResPathDir dir, string version, UpdateManager.FileBaseInfo fbi)
	{
		UpdateManager.DownloadFileInfo info = new UpdateManager.DownloadFileInfo
		{
			FileType = fileType,
			Url = this.GetResPatchUrl(dir, version),
			LocalPath = this.GetResPatchPath(dir, version),
			TargetBaseInfo = fbi
		};
		this.UpdateInfo.AddDownloadFile(info);
	}

	private void RecordResInfo()
	{
		Debug.Log("查询资源下载信息");
		if (this.UpdateInfo.IsCoreUpdate)
		{
			string text = this.DateToString(this.GetCoreVersion());
			UpdateManager.FileBaseInfo fileBaseInfo = this.UpdateInfo.CurrentResInfo.TryGetCorePatch(text);
			if (fileBaseInfo == null)
			{
				Debug.LogErrorFormat("错误的coreVersion :{0}", new object[]
				{
					text
				});
				this.ShowVersionError();
				return;
			}
			this.AddResPatchDownload(UpdateManager.PatchFileType.CorePatch, UpdateManager.ResPathDir.corePatch, text, fileBaseInfo);
		}
		string text2 = string.Empty;
		if (this.UpdateInfo.IsDownloadExtend)
		{
			this.AddResPatchDownload(UpdateManager.PatchFileType.Extend, UpdateManager.ResPathDir.extend, this.UpdateInfo.CurrentResInfo.ExtendVersion, this.UpdateInfo.CurrentResInfo.ExtendFileInfo);
			if (!this.UpdateInfo.CurrentResInfo.ExtendVersion.Equals(GameManager.Instance.ResVersion))
			{
				text2 = this.UpdateInfo.CurrentResInfo.ExtendVersion;
			}
		}
		if (this.UpdateInfo.IsExtendUpdate)
		{
			text2 = this.DateToString(this.GetExtendVersion());
		}
		if (!string.IsNullOrEmpty(text2))
		{
			UpdateManager.FileBaseInfo fileBaseInfo2 = this.UpdateInfo.CurrentResInfo.TryGetExtendPatch(text2);
			if (fileBaseInfo2 == null)
			{
				Debug.LogErrorFormat("错误的extendPatchVersion :{0}", new object[]
				{
					text2
				});
				this.ShowVersionError();
				return;
			}
			this.AddResPatchDownload(UpdateManager.PatchFileType.ExtendPatch, UpdateManager.ResPathDir.extendPatch, text2, fileBaseInfo2);
		}
		this.RecordCount++;
	}

	private string GetUpdateSizeMsg(long downloadSize)
	{
		return string.Format("检测到更新包({0:f2}MB)，是否下载?", this.GetSizeString(downloadSize));
	}

	private void OnDownloadPatchEnter(int eventID, FSMState preState, FSMState currentState)
	{
		Debug.Log("询问下载");
		this.DoAskForDownload();
	}

	private void OnDownloadPatchUpdate(FSMState currentState)
	{
		if (this.Downloading)
		{
			long currentFilesSize = this.UpdateInfo.GetCurrentFilesSize();
			double num = (double)currentFilesSize / (double)this.UpdateInfo.DownloadAmmount;
			PreloadingUIView.SetProgressInSmooth((float)num);
		}
	}

	private void DoAskForDownload()
	{
		long downloadSize = this.UpdateInfo.DownloadAmmount - this.UpdateInfo.GetCurrentFilesSize();
		NetworkReachability internetReachability = Application.get_internetReachability();
		if (internetReachability != 1)
		{
			if (internetReachability != 2)
			{
				DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "更新失败，请检查网络后重试", delegate
				{
					this.DoAskForDownload();
				}, "确 定", "button_orange_1", UINodesManager.T4RootOfSpecial);
				DialogBoxUIView.Instance.isClick = false;
			}
			else
			{
				this.DoDownload();
			}
		}
		else
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("更新", this.GetUpdateSizeMsg(downloadSize), delegate
			{
				ClientApp.QuitApp();
			}, delegate
			{
				this.DoDownload();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", UINodesManager.T4RootOfSpecial, true, true);
			DialogBoxUIView.Instance.isClick = false;
		}
	}

	private void DoDownload()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<long> list4 = new List<long>();
		for (UpdateManager.PatchFileType patchFileType = UpdateManager.PatchFileType.Extend; patchFileType < UpdateManager.PatchFileType.Max; patchFileType++)
		{
			UpdateManager.DownloadFileInfo downloadInfo = this.UpdateInfo.GetDownloadInfo(patchFileType);
			if (downloadInfo != null)
			{
				if (!downloadInfo.IfFull())
				{
					list.Add(downloadInfo.Url);
					list2.Add(downloadInfo.LocalPath);
					list3.Add(downloadInfo.TargetBaseInfo.Md5);
					list4.Add((long)downloadInfo.TargetBaseInfo.FileSize);
				}
			}
		}
		PreloadingUIView.SetProgressName("正在下载更新文件...");
		PreloadingUIView.ResetProgress();
		this.Downloading = true;
		this.CurrentDownloader.Download(list, list2, list3, list4, null, new Action<bool>(this.OnDownloadEnd));
	}

	private void OnDownloadEnd(bool isSuccessed)
	{
		this.Downloading = false;
		if (isSuccessed)
		{
			base.ReactSync(UpdateEvent.Next);
		}
		else
		{
			Action onclick = delegate
			{
				this.DoAskForDownload();
			};
			this.ShowDownloadFailed(onclick);
		}
	}

	private void OnValidatePatchEnter(int eventID, FSMState preState, FSMState currentState)
	{
		Debug.Log("正在校验补丁");
		PreloadingUIView.SetProgressName("正在校验更新文件,过程中不消耗流量");
		Loom.Current.RunAsync(delegate
		{
			bool isAllValid = true;
			for (UpdateManager.PatchFileType patchFileType = UpdateManager.PatchFileType.Extend; patchFileType < UpdateManager.PatchFileType.Max; patchFileType++)
			{
				UpdateManager.DownloadFileInfo downloadInfo = this.UpdateInfo.GetDownloadInfo(patchFileType);
				if (downloadInfo != null)
				{
					string text = MD5Util.EncryptFile(downloadInfo.LocalPath);
					if (!downloadInfo.TargetBaseInfo.Md5.Equals(text))
					{
						isAllValid = false;
						downloadInfo.TargetFileInfo.Delete();
						downloadInfo.TargetFileInfo.Refresh();
					}
				}
			}
			Loom.Current.QueueOnMainThread(delegate
			{
				this.OnValidateFinish(isAllValid);
			});
		});
	}

	private void OnValidateFinish(bool isAllValid)
	{
		if (isAllValid)
		{
			base.React(UpdateEvent.True);
		}
		else
		{
			base.React(UpdateEvent.False);
		}
	}

	private void OnAskForDownloadAgainEnter(int eventID, FSMState preState, FSMState currentState)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("更新", "更新出错，是否重新下载", delegate
		{
			ClientApp.QuitApp();
		}, delegate
		{
			base.React(UpdateEvent.Next);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", UINodesManager.T4RootOfSpecial, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void OnInstallPatchEnter(int eventID, FSMState preState, FSMState currentState)
	{
		this.InitInstallOrder();
		this.CurrentInstallIndex = -1;
		this.InstallCount = 0;
		this.InstallNext();
	}

	private void InstallNext()
	{
		Loom.Current.QueueOnMainThread(new Action(this.DoInstallNext));
	}

	private void DoInstallNext()
	{
		this.CurrentInstallIndex++;
		if (this.CurrentInstallIndex < this.InstallOrder.get_Count())
		{
			this.InstallUpdate = null;
			UpdateManager.PatchFileType type = this.InstallOrder.get_Item(this.CurrentInstallIndex);
			if (this.UpdateInfo.GetDownloadInfo(type) == null)
			{
				this.InstallNext();
			}
			else
			{
				this.InstallCount++;
				string progressName = string.Format("正在解压安装文件,过程中不消耗流量({0}/{1})", this.InstallCount, this.UpdateInfo.DownloadFiles.get_Count());
				PreloadingUIView.SetProgressName(progressName);
				PreloadingUIView.ResetProgress();
				this.InstallByType(type);
			}
		}
		else
		{
			base.ReactSync(UpdateEvent.True);
		}
	}

	private void OnInstallPatchUpdate(FSMState currentState)
	{
		if (this.InstallUpdate != null)
		{
			this.InstallUpdate.Invoke();
		}
	}

	private void InstallByType(UpdateManager.PatchFileType type)
	{
		switch (type)
		{
		case UpdateManager.PatchFileType.Extend:
		case UpdateManager.PatchFileType.ExtendPatch:
		case UpdateManager.PatchFileType.CorePatch:
		{
			UpdateManager.DownloadFileInfo downloadInfo = this.UpdateInfo.GetDownloadInfo(type);
			this.UnzipPatch(downloadInfo);
			break;
		}
		case UpdateManager.PatchFileType.ApkPatch:
			this.InstallByApkPatch();
			break;
		case UpdateManager.PatchFileType.Apk:
			this.InstallByApk();
			break;
		}
	}

	private void InstallByApk()
	{
		Debug.LogFormat("InstallByApk", new object[0]);
		UpdateManager.DownloadFileInfo downloadInfo = this.UpdateInfo.GetDownloadInfo(UpdateManager.PatchFileType.Apk);
		NativeCallManager.InstallPackage(downloadInfo.LocalPath);
		this.InstallNext();
	}

	private void InstallByApkPatch()
	{
		this.InstallUpdate = new Action(this.InstallByApkPatchUpdate);
		Debug.LogFormat("InstallByApkPatch", new object[0]);
		string newApk = this.GetNewApkPath();
		string current = NativeCallManager.GetApkPath();
		string patch = this.GetNewPatchPath();
		Loom.Current.RunAsync(delegate
		{
			FileInfo fileInfo = new FileInfo(newApk);
			if (fileInfo.get_Exists() && fileInfo.get_Length() >= (long)this.UpdateInfo.CurrentApkInfo.ApkFileInfo.FileSize)
			{
				string text = MD5Util.EncryptFile(newApk);
				if (this.UpdateInfo.CurrentApkInfo.ApkFileInfo.Md5.Equals(text))
				{
					Loom.Current.QueueOnMainThread(delegate
					{
						NativeCallManager.InstallPackage(newApk);
						this.InstallNext();
					});
				}
				else
				{
					this.ClearApk();
					this.ClearApkPatch();
					Action onClick = delegate
					{
						this.ReactSync(UpdateEvent.False);
					};
					Loom.Current.QueueOnMainThread(delegate
					{
						this.ShowDownloadFailed(onClick);
					});
				}
			}
			else
			{
				string directoryName = Path.GetDirectoryName(newApk);
				DirectoryUtil.CreateIfNotExist(directoryName);
				FileHelper.DeleteIfExist(newApk);
				this.MergeFile = new FileInfo(newApk);
				using (FileStream fileStream = File.Open(current, 3, 1, 1))
				{
					using (FileStream fileStream2 = File.Open(newApk, 2, 2))
					{
						BinaryPatchUtility.Apply(fileStream, () => File.Open(patch, 3, 1, 1), fileStream2);
					}
				}
				Loom.Current.QueueOnMainThread(delegate
				{
					this.InstallByApkPatch();
				});
			}
		});
	}

	private void InstallByApkPatchUpdate()
	{
		if (this.MergeFile != null)
		{
			this.MergeFile.Refresh();
			if (this.MergeFile.get_Exists())
			{
				PreloadingUIView.SetProgressInSmooth((float)this.MergeFile.get_Length() / (float)this.UpdateInfo.CurrentApkInfo.ApkFileInfo.FileSize);
			}
		}
	}

	private void UnzipPatch(UpdateManager.DownloadFileInfo info)
	{
		Debug.LogFormat("解压文件 :{0}", new object[]
		{
			info.LocalPath
		});
		UnZipFile.Instance.ThreadNum = Environment.get_ProcessorCount() * 2;
		Loom.Current.RunAsync(delegate
		{
			this.InstallUpdate = new Action(this.UnZipUpdate);
			ZipFileArgs zipFileArgs = new ZipFileArgs();
			zipFileArgs.OverWrite = true;
			zipFileArgs.TarDir = PathSystem.PersistentDataPath;
			zipFileArgs.unZipFinish = delegate
			{
				this.UnZipFileFinish(info);
			};
			zipFileArgs.zipFile = info.LocalPath;
			zipFileArgs.FilterDir = new List<string>();
			zipFileArgs.FilterDir.Add("assets/");
			UnZipFile.Instance.UnZip(zipFileArgs);
		});
	}

	private void UnZipUpdate()
	{
		float progressInSmooth = (float)UnZipFile.Instance.UnzipFileCount / (float)UnZipFile.Instance.WaitUnZipFiles;
		PreloadingUIView.SetProgressInSmooth(progressInSmooth);
	}

	private void UnZipFileFinish(UpdateManager.DownloadFileInfo info)
	{
		switch (info.FileType)
		{
		case UpdateManager.PatchFileType.Extend:
			this.SetVersionFileText("ExtendVersion", this.UpdateInfo.CurrentResInfo.ExtendVersion);
			this.ClearExtend();
			break;
		case UpdateManager.PatchFileType.ExtendPatch:
			this.SetVersionFileText("ExtendVersion", GameManager.Instance.ResVersion);
			this.ClearExtendPatch();
			break;
		case UpdateManager.PatchFileType.CorePatch:
			this.SetVersionFileText("CoreVersion", GameManager.Instance.ResVersion);
			this.ClearCorePatch();
			break;
		}
		this.InstallNext();
	}

	private void OnRestartEnter(int eventID, FSMState preState, FSMState currentState)
	{
		DialogBoxUIViewModel.Instance.ShowAsConfirm("提示", "更新完毕，是否现在重启？", delegate
		{
			NativeCallManager.Restart();
		}, "确 定", "button_orange_1", UINodesManager.T4RootOfSpecial);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void OnEndEnter(int eventID, FSMState preState, FSMState currentState)
	{
		this.Updating = false;
		this.UpdateInfo = null;
		if (this.OnUpdateEnd != null)
		{
			this.OnUpdateEnd.Invoke();
		}
		else
		{
			PreloadingUIView.Close();
		}
	}
}
