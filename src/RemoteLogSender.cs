using AltProg;
using System;
using System.Net.Sockets;
using UnityEngine;

public class RemoteLogSender : MonoBehaviour
{
	public static RemoteLogSender Instance;

	protected ClientLogFileWriter logWriter;

	protected UdpClient udpClient;

	protected string ip = "192.168.1.101";

	protected int port = 8888;

	public static bool IsLogCacheUnitsChanged;

	private LimitedQueue<LogUnit> m_logCacheUnits = new LimitedQueue<LogUnit>(50);

	public string IP
	{
		get
		{
			return this.ip;
		}
		set
		{
			this.ip = value;
		}
	}

	public int Port
	{
		get
		{
			return this.port;
		}
		set
		{
			this.port = value;
		}
	}

	public LimitedQueue<LogUnit> LogCacheUnits
	{
		get
		{
			return this.m_logCacheUnits;
		}
		set
		{
			this.m_logCacheUnits = value;
		}
	}

	private void Awake()
	{
		RemoteLogSender.Instance = this;
		Application.add_logMessageReceived(new Application.LogCallback(this.SendToRemote));
		SDKManager.Instance.RegisterCallback("log", delegate(SDKStatusCode a, string b)
		{
			Debug.Log(b);
		});
		this.InitUdp();
	}

	private void OnDestroy()
	{
		Application.remove_logMessageReceived(new Application.LogCallback(this.SendToRemote));
		if (this.logWriter != null)
		{
			this.logWriter.Release();
		}
		this.ReleaseUdp();
	}

	private void OnApplicationPause(bool bPause)
	{
		if (bPause)
		{
			this.ReleaseUdp();
		}
		else
		{
			this.InitUdp();
		}
	}

	private void InitUdp()
	{
		this.udpClient = new UdpClient();
	}

	private void ReleaseUdp()
	{
		if (this.udpClient != null)
		{
			this.udpClient.Close();
			this.udpClient = null;
		}
	}

	public void SendToRemote(string logString, string stackTrace, LogType type)
	{
		try
		{
			this.m_logCacheUnits.Enqueue(new LogUnit
			{
				logString = string.Concat(new object[]
				{
					"[",
					DateTime.get_Now(),
					"]",
					logString
				}),
				stackTrace = stackTrace,
				logType = type
			});
			RemoteLogSender.IsLogCacheUnitsChanged = true;
			this.SendToServer(logString, stackTrace, type);
			if (SystemConfig.IsLogToFile)
			{
				if (this.logWriter != null)
				{
					this.logWriter.WriteLog(logString + "\n" + stackTrace);
				}
			}
			else
			{
				this.SendToConsole(logString, stackTrace, type);
			}
		}
		catch (Exception ex)
		{
			this.m_logCacheUnits.Enqueue(new LogUnit
			{
				logString = string.Concat(new object[]
				{
					"[",
					DateTime.get_Now(),
					"]",
					ex.ToString()
				}),
				stackTrace = stackTrace,
				logType = type
			});
			RemoteLogSender.IsLogCacheUnitsChanged = true;
		}
	}

	public void SendLogCachesToConsole()
	{
		LogUnit[] array = RemoteLogSender.Instance.LogCacheUnits.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			this.SendToConsole(array[i].logString, array[i].stackTrace, array[i].logType);
		}
	}

	private void SendToServer(string logString, string stackTrace, LogType type)
	{
		switch (type)
		{
		case 4:
			ChatManager.Instance.SendClientLog(string.Format("[{0}][{1}] {2}\n{3}", new object[]
			{
				SerializeUtility.GetRuntimePlatformFolderName(Application.get_platform()),
				DateTime.get_Now(),
				logString,
				stackTrace
			}), type);
			if (this.logWriter != null)
			{
				this.logWriter.WriteLog(logString + "\n" + stackTrace);
			}
			break;
		}
	}

	private void SendToConsole(string logString, string stackTrace, LogType type)
	{
		if (Application.get_isMobilePlatform())
		{
			if (this.udpClient == null)
			{
				Debug.LogError("udpClient is null.");
				return;
			}
			LogPacket logPacket = default(LogPacket);
			logPacket.logString = string.Concat(new object[]
			{
				"[",
				DateTime.get_Now(),
				"]",
				logString
			});
			logPacket.stackTrace = stackTrace;
			logPacket.type = type;
			byte[] array = logPacket.ToByteArray();
			this.udpClient.Send(array, array.Length, this.IP, this.Port);
		}
	}
}
