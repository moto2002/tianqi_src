using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using UnityEngine;

public class PingManager
{
	private static PingManager instance;

	protected bool enablePing = true;

	protected bool hasInit;

	protected int pingValue;

	protected Ping pingSender = new Ping();

	protected PingOptions options = new PingOptions(64, true);

	protected IPAddress hostAddress;

	protected byte[] sendBuffer;

	protected int timeout;

	protected AutoResetEvent waiter = new AutoResetEvent(false);

	protected Thread pingThread;

	public static PingManager Instance
	{
		get
		{
			if (PingManager.instance == null)
			{
				PingManager.instance = new PingManager();
			}
			return PingManager.instance;
		}
	}

	public bool EnablePing
	{
		get
		{
			return this.enablePing;
		}
		set
		{
			this.enablePing = value;
		}
	}

	protected bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		set
		{
			this.hasInit = value;
		}
	}

	public void InitData(IPEndPoint host)
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.hostAddress = host.get_Address();
		this.pingSender.add_PingCompleted(new PingCompletedEventHandler(this.PingCompletedCallback));
		this.sendBuffer = Encoding.get_ASCII().GetBytes("tqzm");
		this.timeout = 3500;
		this.pingThread = new Thread(new ThreadStart(this.Start));
		this.pingThread.Start();
	}

	protected void Start()
	{
		while (true)
		{
			Thread.Sleep(4000);
			if (!this.EnablePing && this.pingThread.get_ThreadState() == null)
			{
				break;
			}
			if (this.pingThread.get_ThreadState() == 256)
			{
				this.pingThread.Resume();
			}
			this.PingHost();
		}
		this.pingThread.Suspend();
	}

	protected void PingHost()
	{
		if (!this.HasInit)
		{
			return;
		}
		if (this.hostAddress != null && this.sendBuffer != null)
		{
			this.pingSender.SendAsync(this.hostAddress, this.timeout, this.sendBuffer, this.options, this.waiter);
			this.waiter.WaitOne();
		}
		else
		{
			Debug.Log("有些数据为空");
		}
	}

	protected void PingCompletedCallback(object sender, PingCompletedEventArgs eventArgs)
	{
		if (eventArgs.get_Cancelled() || eventArgs.get_Error() != null)
		{
			Debug.Log("ping PingCompletedCallback error");
			((AutoResetEvent)eventArgs.get_UserState()).Set();
			this.ClearData();
			return;
		}
		PingReply reply = eventArgs.get_Reply();
		this.DisplayReply(reply);
		((AutoResetEvent)eventArgs.get_UserState()).Set();
	}

	protected void DisplayReply(PingReply reply)
	{
		if (reply == null)
		{
			return;
		}
		if (reply.get_Status() == null)
		{
			this.pingValue = (int)reply.get_RoundtripTime();
			Debug.Log("pingValue: " + this.pingValue);
		}
		else
		{
			Debug.Log("Ping 返回失败");
		}
	}

	public void ClearData()
	{
		if (this.pingThread != null)
		{
			this.pingThread.Abort();
			this.pingThread = null;
		}
		this.HasInit = false;
	}
}
