using System;
using System.Threading;

public class UnzipThread
{
	private static UnzipThread instance;

	public bool isUnzip;

	public string file;

	public string outputPath;

	public int allFileCount;

	public string result;

	private Thread thread;

	public static UnzipThread Instance
	{
		get
		{
			if (UnzipThread.instance == null)
			{
				UnzipThread.instance = new UnzipThread();
			}
			return UnzipThread.instance;
		}
	}

	public void Init()
	{
		this.isUnzip = false;
		this.file = string.Empty;
		this.outputPath = string.Empty;
		this.result = string.Empty;
		if (this.thread != null)
		{
			this.thread.Abort();
			this.thread = null;
		}
	}
}
