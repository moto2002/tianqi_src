using System;
using System.Collections;
using System.Diagnostics;

public class XTask
{
	private bool _running;

	private bool _paused;

	private bool _stopped;

	public Action<bool> Finished;

	private IEnumerator coroutine;

	public bool isRunning
	{
		get
		{
			return this._running;
		}
	}

	public bool isPaused
	{
		get
		{
			return this._paused;
		}
	}

	public XTask(IEnumerator c)
	{
		this.coroutine = c;
	}

	public void Pause()
	{
		this._paused = true;
	}

	public void Continue()
	{
		this._paused = false;
	}

	public void Start()
	{
		this._running = true;
		XTaskManager.instance.StartCoroutine(this.CallWrapper());
	}

	public void Stop()
	{
		this._running = false;
		this._stopped = true;
	}

	[DebuggerHidden]
	private IEnumerator CallWrapper()
	{
		XTask.<CallWrapper>c__IteratorD <CallWrapper>c__IteratorD = new XTask.<CallWrapper>c__IteratorD();
		<CallWrapper>c__IteratorD.<>f__this = this;
		return <CallWrapper>c__IteratorD;
	}
}
