using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Loom : MonoBehaviour
{
	public int maxThreads = 8;

	private static int numThreads;

	private static Loom current;

	private static bool initialized;

	private List<Action> actions = new List<Action>();

	private List<DelayedQueueItem> delayed = new List<DelayedQueueItem>();

	private List<DelayedQueueItem> currentDelayed = new List<DelayedQueueItem>();

	private List<Action> currentActions = new List<Action>();

	protected WaitForSeconds wfs = new WaitForSeconds(0.5f);

	public static Loom Current
	{
		get
		{
			Loom.Initialize();
			return Loom.current;
		}
	}

	public void Init()
	{
		this.actions.Clear();
		this.delayed.Clear();
		this.currentDelayed.Clear();
	}

	private void Awake()
	{
		Loom.current = this;
		Loom.initialized = true;
		Object.DontDestroyOnLoad(base.get_gameObject());
	}

	private static void Initialize()
	{
		if (!Loom.initialized)
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			Loom.initialized = true;
			GameObject gameObject = new GameObject("Loom");
			Loom.current = gameObject.AddComponent<Loom>();
		}
	}

	private void OnDisable()
	{
		if (Loom.current == this)
		{
			Loom.current = null;
		}
	}

	private void OnDestroy()
	{
		if (Loom.current == this)
		{
			Loom.current = null;
			Loom.initialized = false;
		}
	}

	public void QueueOnMainThread(Action action)
	{
		this.QueueOnMainThread(action, 0f);
	}

	public void QueueOnMainThread(Action action, float time)
	{
		if (time != 0f)
		{
			List<DelayedQueueItem> list = Loom.Current.delayed;
			lock (list)
			{
				Loom.Current.delayed.Add(new DelayedQueueItem
				{
					time = Time.get_time() + time,
					action = action
				});
			}
		}
		else
		{
			List<Action> list2 = Loom.Current.actions;
			lock (list2)
			{
				Loom.Current.actions.Add(action);
			}
		}
	}

	private void Update()
	{
		List<Action> list = this.actions;
		lock (list)
		{
			this.currentActions.Clear();
			this.currentActions.AddRange(this.actions);
			this.actions.Clear();
		}
		for (int i = 0; i < this.currentActions.get_Count(); i++)
		{
			this.currentActions.get_Item(i).Invoke();
		}
		List<DelayedQueueItem> list2 = this.delayed;
		lock (list2)
		{
			this.currentDelayed.Clear();
			for (int j = 0; j < this.delayed.get_Count(); j++)
			{
				if (this.delayed.get_Item(j).time <= Time.get_time())
				{
					this.currentDelayed.Add(this.delayed.get_Item(j));
				}
			}
			for (int k = 0; k < this.currentDelayed.get_Count(); k++)
			{
				this.delayed.Remove(this.currentDelayed.get_Item(k));
			}
		}
		for (int l = 0; l < this.currentDelayed.get_Count(); l++)
		{
			this.currentDelayed.get_Item(l).action.Invoke();
		}
	}

	public Thread RunAsync(Action a)
	{
		Loom.Initialize();
		while (Loom.numThreads >= this.maxThreads)
		{
			Thread.Sleep(1);
		}
		Interlocked.Increment(ref Loom.numThreads);
		ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunAction), a);
		return null;
	}

	private void RunAction(object action)
	{
		try
		{
			((Action)action).Invoke();
		}
		catch
		{
		}
		finally
		{
			Interlocked.Decrement(ref Loom.numThreads);
		}
	}

	[DebuggerHidden]
	private IEnumerator CheckPingComplete(object sender)
	{
		Loom.<CheckPingComplete>c__Iterator52 <CheckPingComplete>c__Iterator = new Loom.<CheckPingComplete>c__Iterator52();
		<CheckPingComplete>c__Iterator.sender = sender;
		<CheckPingComplete>c__Iterator.<$>sender = sender;
		return <CheckPingComplete>c__Iterator;
	}
}
