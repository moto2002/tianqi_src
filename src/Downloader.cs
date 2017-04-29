using LuaFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;

public class Downloader
{
	public enum CoverType
	{
		COVER = 1,
		TEMP
	}

	public class RequestState
	{
		private const int BUFFER_SIZE = 524288;

		public byte[] BufferRead;

		public HttpWebRequest request;

		public HttpWebResponse response;

		public Stream streamResponse;

		public int read;

		public RequestState()
		{
			this.BufferRead = new byte[524288];
			this.request = null;
			this.streamResponse = null;
			this.read = 0;
		}

		public void Reset()
		{
			this.read = 0;
			if (this.streamResponse != null)
			{
				this.streamResponse.Close();
			}
			if (this.response != null)
			{
				this.response.Close();
			}
			if (this.request != null)
			{
				Debug.Log("request.Abort();____--------释放链接:" + this.request);
				this.request.Abort();
			}
		}
	}

	public const int ReconnectTimes = 1;

	private const int BUFFER_SIZE = 524288;

	private const int RetryTimes = 3;

	private const int DefaultTimeout = 15000;

	private bool pause;

	private bool allIsFinish;

	private bool ManualHandle;

	private List<string> urls;

	private List<string> localPaths;

	private List<string> md5s;

	private List<long> filesSizes;

	private bool CanCheck;

	private string URL;

	private string LocalPath;

	private int downloadFileIndex;

	private uint timeOutTimer;

	private uint getSizeTimer;

	private long sizeTag;

	private int indexTag;

	private string fileName;

	private string fileFullName;

	private FileStream fs;

	private int from;

	public int localFileSize;

	private long downloadFileSize;

	private int alreadyread;

	private float percent;

	private DateTime beginTime;

	private TimeSpan ts = default(TimeSpan);

	private float downloadSpeed;

	private Action<int, int, string> PercentCallback;

	private Action<bool> FinishCallback;

	private int retryTime;

	private int maxDownloadSpeed;

	private static Downloader instance;

	private AutoResetEvent endRequest = new AutoResetEvent(false);

	private AutoResetEvent allDone = new AutoResetEvent(false);

	private Downloader.RequestState myRequestState = new Downloader.RequestState();

	private IAsyncResult Result;

	private RegisteredWaitHandle Handle;

	public int Alreadyread
	{
		get
		{
			return this.alreadyread;
		}
		set
		{
			this.alreadyread = value;
		}
	}

	public int MaxDownloadSpeed
	{
		get
		{
			if (this.maxDownloadSpeed == 0)
			{
				this.maxDownloadSpeed = 10240;
			}
			return this.maxDownloadSpeed;
		}
		set
		{
			this.maxDownloadSpeed = value;
		}
	}

	public static Downloader Instance
	{
		get
		{
			if (Downloader.instance == null)
			{
				Downloader.instance = new Downloader();
			}
			return Downloader.instance;
		}
	}

	public void Stop()
	{
		if (this.fs != null)
		{
			this.fs.Dispose();
		}
		if (this.myRequestState != null && this.myRequestState.response != null)
		{
			this.myRequestState.response.Close();
		}
	}

	private void TimeoutCallback(object state, bool timedOut)
	{
		if (this.Handle != null)
		{
			this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
			this.Handle = null;
		}
		if (timedOut)
		{
			this.Stop();
			Loom.Current.QueueOnMainThread(delegate
			{
				this.ExceptionHandle();
			});
		}
	}

	public void OnPauseClick()
	{
		this.pause = true;
	}

	public void Download(List<string> _url, List<string> _localPath, List<string> _md5, List<long> _filesSize, Action<int, int, string> _stateCallback = null, Action<bool> _finishCallback = null)
	{
		if (_url == null)
		{
			return;
		}
		this.urls = _url;
		this.localPaths = _localPath;
		this.md5s = _md5;
		this.filesSizes = _filesSize;
		this.pause = false;
		this.allIsFinish = false;
		this.alreadyread = 0;
		this.downloadFileIndex = 0;
		this.PercentCallback = _stateCallback;
		this.FinishCallback = _finishCallback;
		this.retryTime = 0;
		this.Download(this.urls.get_Item(this.downloadFileIndex), this.localPaths.get_Item(this.downloadFileIndex));
	}

	private void Download(string _url, string _localPath)
	{
		TimerHeap.DelTimer(this.timeOutTimer);
		this.URL = _url;
		Debug.Log("Download  URL: " + this.URL);
		this.LocalPath = _localPath;
		this.InitDownloader();
		if (this.filesSizes == null || this.downloadFileIndex >= this.filesSizes.get_Count())
		{
			this.GetDownloadFileSize();
		}
		else
		{
			this.downloadFileSize = this.filesSizes.get_Item(this.downloadFileIndex);
			this.StartDownload();
		}
	}

	private void TimeOutListener()
	{
		this.sizeTag = (long)this.localFileSize;
		this.indexTag = this.downloadFileIndex;
		Debug.Log("TimeOutListener ,DateTime.Now :" + DateTime.get_Now());
		this.timeOutTimer = TimerHeap.AddTimer(10000u, 10000, delegate
		{
			if (this.sizeTag == (long)this.localFileSize && this.indexTag == this.downloadFileIndex)
			{
				Debug.Log("timeTimeOutListener_ timeOut:" + DateTime.get_Now());
				this.ExceptionHandle();
			}
			else
			{
				this.sizeTag = (long)this.localFileSize;
				this.indexTag = this.downloadFileIndex;
			}
		});
	}

	private void GetSizeTimer()
	{
		this.sizeTag = this.downloadFileSize;
		this.indexTag = this.downloadFileIndex;
		Debug.Log("GetSizeTimer ,DateTime.Now :" + DateTime.get_Now());
		this.getSizeTimer = TimerHeap.AddTimer(8000u, 8000, delegate
		{
			if (this.sizeTag == this.downloadFileSize && this.indexTag == this.downloadFileIndex)
			{
				Debug.Log("timeTimeOutListener_ timeOut:" + DateTime.get_Now());
				this.ExceptionHandle();
			}
			else
			{
				this.sizeTag = this.downloadFileSize;
				this.indexTag = this.downloadFileIndex;
			}
		});
	}

	private void StartDownload()
	{
		this.SetDetails();
		if ((long)this.from >= this.downloadFileSize)
		{
			if (++this.downloadFileIndex < this.urls.get_Count())
			{
				this.SetDetails();
				this.retryTime = 0;
				this.Download(this.urls.get_Item(this.downloadFileIndex), this.localPaths.get_Item(this.downloadFileIndex));
			}
			else
			{
				this.allIsFinish = true;
				this.fs.Close();
				this.DoFinished();
			}
			return;
		}
		this.GoDownload();
	}

	private bool CheckMd5()
	{
		this.fs.Dispose();
		if (this.md5s == null || string.IsNullOrEmpty(this.md5s.get_Item(this.downloadFileIndex)))
		{
			return true;
		}
		string text = Util.md5file(this.LocalPath);
		return text.Equals(this.md5s.get_Item(this.downloadFileIndex));
	}

	public static string GetMd5FilePath(string filePath)
	{
		return string.Format("{0}.md5", filePath);
	}

	private void InitDownloader()
	{
		try
		{
			this.fileName = this.URL.Substring(this.URL.LastIndexOf('/') + 1);
			this.fileFullName = this.LocalPath;
			if (this.fs != null)
			{
				this.fs.Dispose();
			}
			if (this.md5s == null || string.IsNullOrEmpty(this.md5s.get_Item(this.downloadFileIndex)))
			{
				this.CanCheck = false;
			}
			else
			{
				this.CanCheck = true;
			}
			bool flag = true;
			if (this.CanCheck)
			{
				string text = this.md5s.get_Item(this.downloadFileIndex);
				string md5FilePath = Downloader.GetMd5FilePath(this.fileFullName);
				if (File.Exists(md5FilePath))
				{
					string text2 = File.ReadAllText(md5FilePath);
					flag = !text2.Equals(text);
				}
				else
				{
					string directoryName = Path.GetDirectoryName(md5FilePath);
					DirectoryUtil.CreateIfNotExist(directoryName);
					File.WriteAllText(md5FilePath, text);
				}
			}
			if (flag)
			{
				string directoryName2 = Path.GetDirectoryName(this.fileFullName);
				DirectoryUtil.CreateIfNotExist(directoryName2);
				this.fs = new FileStream(this.fileFullName, 2, 2, 0, 524288, true);
			}
			else
			{
				this.fs = new FileStream(this.fileFullName, 6, 2, 0, 524288, true);
			}
			this.localFileSize = (int)this.fs.get_Length();
			this.from = (int)this.fs.get_Length();
		}
		catch (Exception ex)
		{
			Debug.LogError("InitDownloader Exception has been thrown!");
			Debug.LogError(ex.ToString());
			if (!this.ManualHandle)
			{
				this.FinishCallback.Invoke(this.allIsFinish);
			}
		}
	}

	private void GetDownloadFileSize()
	{
		try
		{
			this.myRequestState.request = (HttpWebRequest)WebRequest.Create(this.URL);
			this.myRequestState.request.set_KeepAlive(false);
			this.myRequestState.request.set_Timeout(8000);
			this.myRequestState.request.set_ReadWriteTimeout(8000);
			this.GetSizeTimer();
			this.Result = this.myRequestState.request.BeginGetResponse(new AsyncCallback(this.GetSizeRes), this.myRequestState);
		}
		catch (Exception ex)
		{
			TimerHeap.DelTimer(this.getSizeTimer);
			if (this.Handle != null)
			{
				this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
				this.Handle = null;
			}
			if (!this.ManualHandle)
			{
				Debug.LogError("GetDownloadFileSize Exception raised!");
				Debug.LogError(ex.ToString());
				Loom.Current.QueueOnMainThread(delegate
				{
					this.ExceptionHandle();
				});
			}
		}
	}

	private void ExceptionHandle()
	{
		this.ManualHandle = true;
		this.myRequestState.Reset();
		this.ManualHandle = false;
		if (this.retryTime++ < 1)
		{
			Debug.Log("正在重试: " + this.retryTime);
			this.Download(this.urls.get_Item(this.downloadFileIndex), this.localPaths.get_Item(this.downloadFileIndex));
		}
		else
		{
			TimerHeap.DelTimer(this.timeOutTimer);
			this.FinishCallback.Invoke(this.allIsFinish);
		}
	}

	private void GetSizeRes(IAsyncResult getSizeRes)
	{
		TimerHeap.DelTimer(this.getSizeTimer);
		try
		{
			this.myRequestState = (Downloader.RequestState)getSizeRes.get_AsyncState();
			this.myRequestState.response = (HttpWebResponse)this.myRequestState.request.EndGetResponse(getSizeRes);
			this.downloadFileSize = this.myRequestState.response.get_ContentLength();
			Debug.Log(string.Concat(new object[]
			{
				"GetDownloadFileSize : ",
				this.downloadFileSize,
				"  _URL  : ",
				this.URL
			}));
			this.ManualHandle = true;
			this.myRequestState.Reset();
			this.ManualHandle = false;
			Loom.Current.QueueOnMainThread(delegate
			{
				this.StartDownload();
			});
		}
		catch (Exception ex)
		{
			if (this.Handle != null)
			{
				this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
				this.Handle = null;
			}
			Debug.LogErrorFormat("GetSizeRes WebException raised: {0}", new object[]
			{
				ex.ToString()
			});
			if (!this.ManualHandle)
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					this.ExceptionHandle();
				});
			}
		}
	}

	public void GoDownload()
	{
		try
		{
			Debug.LogFormat("GoDownload:{0} from:{1}", new object[]
			{
				this.URL,
				this.from
			});
			this.myRequestState.request = (HttpWebRequest)WebRequest.Create(this.URL);
			this.myRequestState.request.set_KeepAlive(false);
			this.myRequestState.request.set_Timeout(8000);
			this.myRequestState.request.set_ReadWriteTimeout(8000);
			this.myRequestState.request.AddRange(this.from);
			this.TimeOutListener();
			this.Result = this.myRequestState.request.BeginGetResponse(new AsyncCallback(this.RespCallback), this.myRequestState);
		}
		catch (Exception ex)
		{
			TimerHeap.DelTimer(this.timeOutTimer);
			if (this.Handle != null)
			{
				this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
				this.Handle = null;
			}
			Debug.LogError("GoDownload Exception raised!");
			Debug.LogError(ex.ToString());
			if (!this.ManualHandle)
			{
				this.ExceptionHandle();
			}
		}
	}

	private void RespCallback(IAsyncResult asynchronousResult)
	{
		try
		{
			this.myRequestState = (Downloader.RequestState)asynchronousResult.get_AsyncState();
			this.myRequestState.response = (HttpWebResponse)this.myRequestState.request.EndGetResponse(asynchronousResult);
			this.myRequestState.streamResponse = this.myRequestState.response.GetResponseStream();
			Debug.Log("BeginRead:");
			this.Result = this.myRequestState.streamResponse.BeginRead(this.myRequestState.BufferRead, 0, 524288, new AsyncCallback(this.ReadCallBack), this.myRequestState);
		}
		catch (Exception ex)
		{
			TimerHeap.DelTimer(this.timeOutTimer);
			if (this.Handle != null)
			{
				this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
				this.Handle = null;
			}
			if (!this.ManualHandle)
			{
				Debug.LogError("RespCallback WebException raised!");
				Debug.LogError(ex.ToString());
				Loom.Current.QueueOnMainThread(delegate
				{
					this.ExceptionHandle();
				});
			}
		}
	}

	private void ReadCallBack(IAsyncResult asyncResult)
	{
		this.myRequestState = (Downloader.RequestState)asyncResult.get_AsyncState();
		Stream streamResponse = this.myRequestState.streamResponse;
		int num = streamResponse.EndRead(asyncResult);
		this.myRequestState.read = num;
		try
		{
			if (num > 0)
			{
				this.Result = this.fs.BeginWrite(this.myRequestState.BufferRead, 0, num, new AsyncCallback(this.WriteCallBack), this.myRequestState);
				return;
			}
		}
		catch (Exception ex)
		{
			TimerHeap.DelTimer(this.timeOutTimer);
			if (this.Handle != null)
			{
				this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
				this.Handle = null;
			}
			if (this.ManualHandle)
			{
				return;
			}
			Debug.LogError("ReadCallBack Exception raised!");
			Debug.LogError(ex.ToString());
			if (this.ManualHandle)
			{
				return;
			}
			Loom.Current.QueueOnMainThread(delegate
			{
				this.ExceptionHandle();
			});
			return;
		}
		string md5FilePath = Downloader.GetMd5FilePath(this.fileFullName);
		FileHelper.DeleteIfExist(md5FilePath);
		TimerHeap.DelTimer(this.timeOutTimer);
		long length = this.fs.get_Length();
		this.fs.Dispose();
		if (this.Handle != null)
		{
			this.Handle.Unregister(this.Result.get_AsyncWaitHandle());
		}
		this.myRequestState.response.Close();
		Debug.LogFormat("localFileLen: {0} localFileSize: {1} downloadFileSize: {2}", new object[]
		{
			length,
			this.localFileSize,
			this.downloadFileSize
		});
		if ((long)this.localFileSize == this.downloadFileSize && length == this.downloadFileSize)
		{
			if (++this.downloadFileIndex < this.urls.get_Count())
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					this.SetDetails();
				});
				this.fs.Close();
				this.retryTime = 0;
				Loom.Current.QueueOnMainThread(delegate
				{
					this.Download(this.urls.get_Item(this.downloadFileIndex), this.localPaths.get_Item(this.downloadFileIndex));
				});
			}
			else
			{
				this.fs.Close();
				this.allIsFinish = true;
				Loom.Current.QueueOnMainThread(delegate
				{
					this.DoFinished();
				});
			}
		}
		else
		{
			this.fs.Dispose();
			File.Delete(this.LocalPath);
			this.retryTime = 0;
			Loom.Current.QueueOnMainThread(delegate
			{
				this.Download(this.urls.get_Item(this.downloadFileIndex), this.localPaths.get_Item(this.downloadFileIndex));
			});
		}
	}

	private void WriteCallBack(IAsyncResult asyncResult)
	{
		this.myRequestState = (Downloader.RequestState)asyncResult.get_AsyncState();
		Stream streamResponse = this.myRequestState.streamResponse;
		this.fs.EndWrite(asyncResult);
		int read = this.myRequestState.read;
		this.localFileSize += read;
		this.alreadyread += read;
		Loom.Current.QueueOnMainThread(delegate
		{
			this.SetDetails();
		});
		if (!this.pause)
		{
			this.Result = streamResponse.BeginRead(this.myRequestState.BufferRead, 0, 524288, new AsyncCallback(this.ReadCallBack), this.myRequestState);
		}
		else
		{
			this.ManualHandle = true;
			this.myRequestState.Reset();
			this.ManualHandle = false;
			this.fs.Dispose();
		}
	}

	private void SetDetails()
	{
		if (this.PercentCallback != null)
		{
			this.PercentCallback.Invoke(this.localFileSize, (int)this.downloadFileSize, this.fileName);
		}
	}

	private void DoFinished()
	{
		if (this.allIsFinish && this.FinishCallback != null)
		{
			TimerHeap.DelTimer(this.timeOutTimer);
			this.FinishCallback.Invoke(this.allIsFinish);
		}
	}

	public static long GetFileDownloadAmmount(string filePath, long size, string md5)
	{
		if (string.IsNullOrEmpty(md5))
		{
			return size;
		}
		if (!File.Exists(filePath))
		{
			return size;
		}
		string text = string.Format("{0}.md5", filePath);
		if (!File.Exists(text))
		{
			return size;
		}
		string text2 = File.ReadAllText(text);
		if (text2.Equals(md5))
		{
			return size - new FileInfo(filePath).get_Length();
		}
		return size;
	}
}
