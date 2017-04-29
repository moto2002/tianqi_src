using LuaFramework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DownloadAnnouncementFile
{
	private bool Downloading;

	private static DownloadAnnouncementFile instance;

	public bool IsFinish
	{
		get;
		private set;
	}

	public static DownloadAnnouncementFile Instance
	{
		get
		{
			if (DownloadAnnouncementFile.instance == null)
			{
				DownloadAnnouncementFile.instance = new DownloadAnnouncementFile();
			}
			return DownloadAnnouncementFile.instance;
		}
	}

	public void Down()
	{
		if (this.Downloading)
		{
			return;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		list.Add(AppConst.GetRemoteFilePath(SDKManager.Instance.GetSDKName(), "notice.json", 0));
		list2.Add(Path.Combine(Util.DataPath, "notice.json"));
		this.IsFinish = false;
		this.Downloading = true;
		Downloader.Instance.Download(list, list2, null, null, null, new Action<bool>(this.DownFinish));
		WaitUI.OpenUI(10000u);
	}

	private void DownFinish(bool isFinish)
	{
		WaitUI.CloseUI(0u);
		this.Downloading = false;
		this.IsFinish = isFinish;
		EventDispatcher.Broadcast<bool>(EventNames.Download_AnnouncementFile_Finish, this.IsFinish);
		Debug.Log("DownAnnouncementFileFinish:" + this.IsFinish);
	}
}
