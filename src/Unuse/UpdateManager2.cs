using BsDiff;
using FSM;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Unuse
{
	public class UpdateManager2 : FSMSystem<UpdateEvent2, UpdateState2>
	{
		private const float BToMB = 1048576f;

		public PatchInfo CurrentPatchInfo;

		private bool ApkMerging;

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

		public void DownloadServerListAndUpdate()
		{
			LoginManager.Instance.GetServerListFile(LoginManager.AddressType.DOMAIN);
			this.Resume();
		}

		public void Resume()
		{
			this.Updating = false;
			Debug.Log("UpdateManager Resume");
			if (base.CurrentState != null)
			{
				UpdateState2 stateID = (UpdateState2)base.CurrentState.StateID;
				UpdateState2 updateState = stateID;
				if (updateState != UpdateState2.Init && updateState != UpdateState2.End)
				{
					Debug.LogError("当前状态不为停止状态");
				}
			}
			base.Resume(UpdateState2.Init);
		}

		private void InitAllTransition()
		{
			base.InitTransition(UpdateState2.Init, UpdateEvent2.Next, UpdateState2.CheckVersionCode);
			base.InitTransition(UpdateState2.CheckVersionCode, UpdateEvent2.True, UpdateState2.CleanApkAndPatch);
			base.InitTransition(UpdateState2.CheckVersionCode, UpdateEvent2.False, UpdateState2.GetPatchInfo);
			base.InitTransition(UpdateState2.GetPatchInfo, UpdateEvent2.Next, UpdateState2.ValidateApk);
			base.InitTransition(UpdateState2.CleanApkAndPatch, UpdateEvent2.Next, UpdateState2.End);
			base.InitTransition(UpdateState2.ValidateApk, UpdateEvent2.True, UpdateState2.InstallApk);
			base.InitTransition(UpdateState2.ValidateApk, UpdateEvent2.False, UpdateState2.CheckUpdateWay);
			base.InitTransition(UpdateState2.InstallApk, UpdateEvent2.Next, UpdateState2.CheckVersionCode);
			base.InitTransition(UpdateState2.CheckUpdateWay, UpdateEvent2.True, UpdateState2.DownloadApk);
			base.InitTransition(UpdateState2.CheckUpdateWay, UpdateEvent2.False, UpdateState2.ValidatePatch);
			base.InitTransition(UpdateState2.DownloadApk, UpdateEvent2.Next, UpdateState2.ValidateApk);
			base.InitTransition(UpdateState2.ValidatePatch, UpdateEvent2.True, UpdateState2.MergePatch);
			base.InitTransition(UpdateState2.ValidatePatch, UpdateEvent2.False, UpdateState2.DownloadPatch);
			base.InitTransition(UpdateState2.MergePatch, UpdateEvent2.Next, UpdateState2.ValidateApk);
			base.InitTransition(UpdateState2.DownloadPatch, UpdateEvent2.Next, UpdateState2.ValidatePatch);
		}

		private void InitAllState()
		{
			base.InitState(UpdateState2.Init, new FSMState.EnterCallback(this.OnInitEnter), new FSMState.UpdateCallback(this.OnInitUpdate), null);
			base.InitState(UpdateState2.CheckVersionCode, new FSMState.EnterCallback(this.OnCheckVersionCodeEnter), null, null);
			base.InitState(UpdateState2.CleanApkAndPatch, new FSMState.EnterCallback(this.OnCleanApkAndPatchEnter), null, null);
			base.InitState(UpdateState2.GetPatchInfo, new FSMState.EnterCallback(this.OnGetPatchInfo), null, null);
			base.InitState(UpdateState2.ValidateApk, new FSMState.EnterCallback(this.OnValidateApkEnter), null, null);
			base.InitState(UpdateState2.CheckUpdateWay, new FSMState.EnterCallback(this.OnCheckUpdateWayEnter), null, null);
			base.InitState(UpdateState2.InstallApk, new FSMState.EnterCallback(this.OnInstallApkEnter), null, null);
			base.InitState(UpdateState2.DownloadApk, new FSMState.EnterCallback(this.OnDownloadApkEnter), null, null);
			base.InitState(UpdateState2.ValidatePatch, new FSMState.EnterCallback(this.OnValidatePatchEnter), null, null);
			base.InitState(UpdateState2.MergePatch, new FSMState.EnterCallback(this.OnMergePatchEnter), new FSMState.UpdateCallback(this.OnMergePatchUpdate), null);
			base.InitState(UpdateState2.DownloadPatch, new FSMState.EnterCallback(this.OnDownloadPatchEnter), null, null);
			base.InitState(UpdateState2.End, new FSMState.EnterCallback(this.OnEndEnter), null, null);
		}

		private void ClearApk()
		{
			Debug.Log("清理所有apk");
			DirectoryUtil.DeleteIfExist(PathSystem.ApkDir);
		}

		private void ClearPatch()
		{
			Debug.Log("清理所有补丁");
			DirectoryUtil.DeleteIfExist(PathSystem.ApkPatchDir);
		}

		private string GetNewApkName()
		{
			return string.Format("new_{0}.apk", GameManager.Instance.ServerVersionCode);
		}

		private string GetNewApkPath()
		{
			return PathSystem.GetPath(PathType.ApkPath, this.GetNewApkName());
		}

		private string GetDownloadPatchRelPath()
		{
			int versionCode = NativeCallManager.GetVersionCode();
			int serverVersionCode = GameManager.Instance.ServerVersionCode;
			return string.Format("{0}_{1}.patch", versionCode, serverVersionCode);
		}

		private string GetSizeString(long downloadSize)
		{
			return string.Format("{0:f2}", (float)downloadSize / 1048576f);
		}

		private string GetUpdateSizeMsg(long downloadSize)
		{
			return string.Format("检测到更新包({0:f2}MB)，是否下载?", this.GetSizeString(downloadSize));
		}

		private string GetNewPatchName()
		{
			int versionCode = NativeCallManager.GetVersionCode();
			int serverVersionCode = GameManager.Instance.ServerVersionCode;
			return string.Format("{0}_{1}.patch", versionCode, serverVersionCode);
		}

		private string GetNewPatchPath()
		{
			return PathSystem.GetPath(PathType.ApkPatchPath, this.GetNewPatchName());
		}

		private FileBaseInfo GetNewPatchFileInfo()
		{
			string apkMd = NativeCallManager.GetApkMd5();
			return this.CurrentPatchInfo.PatchFileInfo.get_Item(apkMd);
		}

		private void BeginDownloadPackage(string url, string localPath, FileBaseInfo fileInfo)
		{
			GameManager.Instance.OpenPreloadingUI();
			PreloadingUIView.SetProgressName("正在下载更新文件...");
			PreloadingUIView.ResetProgress();
			List<string> list = new List<string>();
			list.Add(url);
			List<string> list2 = new List<string>();
			list2.Add(localPath);
			List<string> list3 = new List<string>();
			list3.Add(fileInfo.Md5);
			List<long> list4 = new List<long>();
			list4.Add((long)fileInfo.FileSize);
			Downloader.Instance.Download(list, list2, list3, list4, new Action<int, int, string>(this.DownloadPackageUpdate), new Action<bool>(this.EndDownloadPackage));
		}

		private void DownloadPackageUpdate(int current, int max, string filePath)
		{
			if (current <= max)
			{
				float progressInSmooth = (float)current / (float)max;
				string progressName = string.Format("正在下载更新文件 :{0}/{1}MB", this.GetSizeString((long)current), this.GetSizeString((long)max));
				PreloadingUIView.SetProgressName(progressName);
				PreloadingUIView.SetProgressInSmooth(progressInSmooth);
			}
		}

		private void EndDownloadPackage(bool isSuccessed)
		{
			if (isSuccessed)
			{
				base.ReactSync(UpdateEvent2.Next);
			}
			else
			{
				Action actionC = delegate
				{
					switch (base.CurrentState.StateID)
					{
					case 7:
						this.DoDownloadApk();
						break;
					case 9:
						this.DoDownloadPatch();
						break;
					}
				};
				DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "下载补丁包出错，请检查网络后重试!", actionC, "确 定", "button_orange_1", UINodesManager.T4RootOfSpecial);
				DialogBoxUIView.Instance.isClick = false;
			}
		}

		private void DoDownloadPackage(string url, string localPath, FileBaseInfo fileInfo)
		{
			PreloadingUIView.SetProgressName("正在计算下载量...");
			long fileDownloadAmmount = Downloader.GetFileDownloadAmmount(localPath, (long)fileInfo.FileSize, fileInfo.Md5);
			if (fileDownloadAmmount == 0L)
			{
				base.React(UpdateEvent2.Next);
			}
			else
			{
				switch (Application.get_internetReachability())
				{
				case 0:
					DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "更新失败，请检查网络后重试", delegate
					{
						this.DoDownloadPackage(url, localPath, fileInfo);
					}, "确 定", "button_orange_1", UINodesManager.T4RootOfSpecial);
					DialogBoxUIView.Instance.isClick = false;
					break;
				case 1:
					DialogBoxUIViewModel.Instance.ShowAsOKCancel("更新", this.GetUpdateSizeMsg(fileDownloadAmmount), delegate
					{
						ClientApp.QuitApp();
					}, delegate
					{
						this.BeginDownloadPackage(url, localPath, fileInfo);
					}, "取 消", "确 定", "button_orange_1", "button_yellow_1", UINodesManager.T4RootOfSpecial, true, true);
					DialogBoxUIView.Instance.isClick = false;
					break;
				case 2:
					this.BeginDownloadPackage(url, localPath, fileInfo);
					break;
				}
			}
		}

		private void OnInitEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("UpdateManager init");
			this.OnInitUpdate(null);
		}

		private void OnInitUpdate(FSMState currentState)
		{
			if (LoginManager.Instance.IsAnalysisSuccess)
			{
				base.React(UpdateEvent2.Next);
			}
		}

		private void OnCheckVersionCodeEnter(int eventID, FSMState preState, FSMState currentState)
		{
			if (!Application.get_isMobilePlatform())
			{
				Debug.Log("非移动平台，跳过更新");
				base.React(UpdateEvent2.True);
			}
			else
			{
				int serverVersionCode = GameManager.Instance.ServerVersionCode;
				int versionCode = NativeCallManager.GetVersionCode();
				Debug.LogFormat("服务器vc:{0}    客户端vc:{1}", new object[]
				{
					serverVersionCode,
					versionCode
				});
				if (versionCode < serverVersionCode)
				{
					base.React(UpdateEvent2.False);
				}
				else if (versionCode >= serverVersionCode)
				{
					base.React(UpdateEvent2.True);
				}
			}
		}

		private void OnCleanApkAndPatchEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("清理已下载的文件");
			string apkDir = PathSystem.ApkDir;
			string apkPatchDir = PathSystem.ApkPatchDir;
			Loom.Current.RunAsync(delegate
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					PreloadingUIView.SetProgressName("正在优化游戏环境...");
				});
				this.ClearApk();
				this.ClearPatch();
				Loom.Current.QueueOnMainThread(delegate
				{
					base.ReactSync(UpdateEvent2.Next);
				});
			});
		}

		private void OnGetPatchInfo(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("开始获取补丁信息...");
			this.Updating = true;
			this.CurrentPatchInfo = null;
			this.BeginDownloadPatchInfo();
		}

		private string GetPatchInfoPath()
		{
			return PathSystem.GetPath(PathType.ApkPatchPath, PathSystem.ApkPatchInfoFileName);
		}

		private void BeginDownloadPatchInfo()
		{
			Debug.Log("下载补丁包信息");
			PreloadingUIView.SetProgressName("正在获取更新信息...");
			string patchInfoPath = this.GetPatchInfoPath();
			FileHelper.DeleteIfExist(patchInfoPath);
			string apkPatchUrl = GameManager.Instance.GetApkPatchUrl(PathSystem.ApkPatchInfoFileName);
			List<string> list = new List<string>();
			list.Add(apkPatchUrl);
			List<string> list2 = new List<string>();
			list2.Add(patchInfoPath);
			Downloader.Instance.Download(list, list2, null, null, null, new Action<bool>(this.OnPatchInfoDownloaded));
		}

		private void OnPatchInfoDownloaded(bool successed)
		{
			if (successed)
			{
				string patchInfoPath = this.GetPatchInfoPath();
				this.CurrentPatchInfo = JsonMapper.ToObject<PatchInfo>(File.ReadAllText(patchInfoPath));
				Debug.Log("补丁信息下载成功");
				base.React(UpdateEvent2.Next);
			}
			else
			{
				Debug.LogError("补丁下载失败");
				DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", "更新出错，请检查网络后重试", delegate
				{
					this.BeginDownloadPatchInfo();
				}, "确定", "button_orange_1", UINodesManager.T4RootOfSpecial);
				DialogBoxUIView.Instance.isClick = false;
			}
		}

		private void OnValidateApkEnter(int eventID, FSMState preState, FSMState currentState)
		{
			PreloadingUIView.SetProgressName("正在校验更新文件...");
			Debug.Log("正在校验apk");
			string newApkPath = this.GetNewApkPath();
			Loom.Current.RunAsync(delegate
			{
				bool flag = false;
				FileInfo fileInfo = new FileInfo(newApkPath);
				if (fileInfo.get_Exists())
				{
					string text = MD5Util.EncryptFile(newApkPath);
					flag = text.Equals(this.CurrentPatchInfo.ApkFileInfo.Md5);
					if (!flag && fileInfo.get_Length() == (long)this.CurrentPatchInfo.ApkFileInfo.FileSize)
					{
						File.Delete(newApkPath);
					}
				}
				if (flag)
				{
					this.ReactSync(UpdateEvent2.True);
				}
				else
				{
					this.ReactSync(UpdateEvent2.False);
				}
			});
		}

		private void OnCheckUpdateWayEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("查询更新方式（整包or补丁包）");
			string dataPath = Application.get_dataPath();
			NativeCallManager.GetApkMd5Async(delegate(string x)
			{
				if (!this.CurrentPatchInfo.PatchFileInfo.ContainsKey(x))
				{
					base.ReactSync(UpdateEvent2.True);
				}
				else
				{
					base.ReactSync(UpdateEvent2.False);
				}
			});
		}

		private void OnInstallApkEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("开始安装apk");
			NativeCallManager.InstallPackage(this.GetNewApkPath());
		}

		private void OnDownloadApkEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("整包更新下载");
			this.DoDownloadApk();
		}

		private void DoDownloadApk()
		{
			string apkPatchUrl = GameManager.Instance.GetApkPatchUrl(this.GetNewApkName());
			this.DoDownloadPackage(apkPatchUrl, this.GetNewApkPath(), this.CurrentPatchInfo.ApkFileInfo);
		}

		private void OnValidatePatchEnter(int eventID, FSMState preState, FSMState currentState)
		{
			PreloadingUIView.SetProgressName("正在校验更新文件...");
			Debug.Log("校验补丁包");
			string newPatch = this.GetNewPatchPath();
			Loom.Current.RunAsync(delegate
			{
				bool flag = false;
				FileInfo fileInfo = new FileInfo(newPatch);
				if (fileInfo.get_Exists())
				{
					string text = MD5Util.EncryptFile(newPatch);
					FileBaseInfo newPatchFileInfo = this.GetNewPatchFileInfo();
					flag = text.Equals(newPatchFileInfo.Md5);
					if (!flag && fileInfo.get_Length() == (long)newPatchFileInfo.FileSize)
					{
						File.Delete(newPatch);
					}
				}
				if (flag)
				{
					this.ReactSync(UpdateEvent2.True);
				}
				else
				{
					this.ReactSync(UpdateEvent2.False);
				}
			});
		}

		private void OnMergePatchEnter(int eventID, FSMState preState, FSMState currentState)
		{
			PreloadingUIView.SetProgressName("正在解压安装文件...");
			PreloadingUIView.ResetProgress();
			Debug.Log("正在合并apk包");
			string current = Application.get_dataPath();
			string newApk = this.GetNewApkPath();
			string patch = this.GetNewPatchPath();
			string directoryName = Path.GetDirectoryName(newApk);
			DirectoryUtil.CreateIfNotExist(directoryName);
			this.ApkMerging = false;
			Loom.Current.RunAsync(delegate
			{
				FileHelper.DeleteIfExist(newApk);
				this.ApkMerging = true;
				using (FileStream fileStream = File.Open(current, 3, 1, 1))
				{
					using (FileStream fileStream2 = File.Open(newApk, 2, 2))
					{
						BinaryPatchUtility.Apply(fileStream, () => File.Open(patch, 3, 1, 1), fileStream2);
					}
				}
				this.ReactSync(UpdateEvent2.Next);
			});
		}

		private void OnMergePatchUpdate(FSMState currentState)
		{
			if (this.ApkMerging)
			{
				string newApkPath = this.GetNewApkPath();
				FileInfo fileInfo = new FileInfo(newApkPath);
				if (fileInfo.get_Exists())
				{
					PreloadingUIView.SetProgress((float)fileInfo.get_Length() / (float)this.CurrentPatchInfo.ApkFileInfo.FileSize);
				}
			}
		}

		private void OnDownloadPatchEnter(int eventID, FSMState preState, FSMState currentState)
		{
			Debug.Log("补丁包更新下载");
			this.DoDownloadPatch();
		}

		private void DoDownloadPatch()
		{
			string apkPatchUrl = GameManager.Instance.GetApkPatchUrl(this.GetNewPatchName());
			Debug.LogFormat("正在下载补丁包 :{0}", new object[]
			{
				apkPatchUrl
			});
			FileBaseInfo newPatchFileInfo = this.GetNewPatchFileInfo();
			this.DoDownloadPackage(apkPatchUrl, this.GetNewPatchPath(), newPatchFileInfo);
		}

		private void OnEndEnter(int eventID, FSMState preState, FSMState currentState)
		{
			this.Updating = false;
			Debug.Log("更新结束");
			PreloadingUIView.Close();
		}
	}
}
