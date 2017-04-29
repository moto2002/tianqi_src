using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
using UnityEngine;

public class TimerHeap
{
	private static uint m_nNextTimerId;

	private static uint m_unTick;

	private static KeyedPriorityQueue<uint, AbsTimerData, ulong> m_queue;

	private static Stopwatch m_stopWatch;

	private static readonly object m_queueLock;

	private static float m_deltaSystemTimer;

	private static float m_timeSystemTimer;

	private static float m_timeSystemTimerTmp;

	private static float m_checkPerTime;

	private static float m_checkTimeTmp;

	private static float m_invokeReaptingTime;

	private static Action m_cheatHandler;

	private static bool m_cheat;

	private static bool isPause;

	private TimerHeap()
	{
	}

	static TimerHeap()
	{
		TimerHeap.m_queueLock = new object();
		TimerHeap.m_deltaSystemTimer = 1000f;
		TimerHeap.m_checkPerTime = 10f;
		TimerHeap.m_invokeReaptingTime = 0.02f;
		TimerHeap.isPause = false;
		TimerHeap.m_cheat = false;
		TimerHeap.m_queue = new KeyedPriorityQueue<uint, AbsTimerData, ulong>();
		TimerHeap.m_stopWatch = new Stopwatch();
		Timer timer = new Timer((double)TimerHeap.m_deltaSystemTimer);
		timer.add_Elapsed(new ElapsedEventHandler(TimerHeap.theout));
		timer.set_AutoReset(true);
		timer.set_Enabled(true);
	}

	public static void theout(object source, ElapsedEventArgs e)
	{
		TimerHeap.m_timeSystemTimer += TimerHeap.m_deltaSystemTimer / 1000f;
		TimerHeap.m_timeSystemTimerTmp = TimerHeap.m_timeSystemTimer;
	}

	public static uint AddTimer(uint start, int interval, Action handler)
	{
		TimerData timerData = TimerHeap.GetTimerData<TimerData>(new TimerData(), start, interval);
		timerData.Action = handler;
		return TimerHeap.AddTimer(timerData);
	}

	public static uint AddTimer<T>(uint start, int interval, Action<T> handler, T arg1)
	{
		TimerData<T> timerData = TimerHeap.GetTimerData<TimerData<T>>(new TimerData<T>(), start, interval);
		timerData.Action = handler;
		timerData.Arg1 = arg1;
		return TimerHeap.AddTimer(timerData);
	}

	public static uint AddTimer<T, U>(uint start, int interval, Action<T, U> handler, T arg1, U arg2)
	{
		TimerData<T, U> timerData = TimerHeap.GetTimerData<TimerData<T, U>>(new TimerData<T, U>(), start, interval);
		timerData.Action = handler;
		timerData.Arg1 = arg1;
		timerData.Arg2 = arg2;
		return TimerHeap.AddTimer(timerData);
	}

	public static uint AddTimer<T, U, V>(uint start, int interval, Action<T, U, V> handler, T arg1, U arg2, V arg3)
	{
		TimerData<T, U, V> timerData = TimerHeap.GetTimerData<TimerData<T, U, V>>(new TimerData<T, U, V>(), start, interval);
		timerData.Action = handler;
		timerData.Arg1 = arg1;
		timerData.Arg2 = arg2;
		timerData.Arg3 = arg3;
		return TimerHeap.AddTimer(timerData);
	}

	public static void DelTimer(uint timerId)
	{
		object queueLock = TimerHeap.m_queueLock;
		lock (queueLock)
		{
			TimerHeap.m_queue.Remove(timerId);
		}
	}

	public static void ExecuteImmediately(uint timerId)
	{
		TimerHeap.m_queue.Get(timerId).DoAction();
		TimerHeap.DelTimer(timerId);
	}

	public static void AddCheatCheckHandler(Action handler)
	{
		TimerHeap.m_cheatHandler = handler;
	}

	private static void CheckCheat()
	{
		if (TimerHeap.m_timeSystemTimerTmp - TimerHeap.m_timeSystemTimer > 5f)
		{
			TimerHeap.m_cheat = true;
			TimerHeap.m_cheatHandler.Invoke();
		}
	}

	public static void Pause(bool _isPause)
	{
		TimerHeap.isPause = _isPause;
	}

	public static void Tick()
	{
		if (TimerHeap.isPause)
		{
			return;
		}
		if (!TimerHeap.m_stopWatch.get_IsRunning())
		{
			TimerHeap.m_stopWatch.Start();
		}
		TimerHeap.m_unTick = (uint)(Time.get_time() * 1000f);
		TimerHeap.m_checkTimeTmp += TimerHeap.m_invokeReaptingTime;
		TimerHeap.m_timeSystemTimerTmp += TimerHeap.m_invokeReaptingTime;
		if (!TimerHeap.m_cheat && TimerHeap.m_checkTimeTmp > TimerHeap.m_checkPerTime)
		{
			TimerHeap.m_checkTimeTmp = 0f;
			TimerHeap.CheckCheat();
		}
		bool flag = Debug.get_isDebugBuild() || Application.get_isEditor();
		while (TimerHeap.m_queue.Count != 0)
		{
			object queueLock = TimerHeap.m_queueLock;
			AbsTimerData absTimerData;
			lock (queueLock)
			{
				absTimerData = TimerHeap.m_queue.Peek();
			}
			if ((ulong)TimerHeap.m_unTick < absTimerData.UnNextTick)
			{
				break;
			}
			object queueLock2 = TimerHeap.m_queueLock;
			lock (queueLock2)
			{
				TimerHeap.m_queue.Dequeue();
			}
			if (absTimerData.NInterval > 0)
			{
				absTimerData.UnNextTick += (ulong)((long)absTimerData.NInterval);
				object queueLock3 = TimerHeap.m_queueLock;
				lock (queueLock3)
				{
					TimerHeap.m_queue.Enqueue(absTimerData.NTimerId, absTimerData, absTimerData.UnNextTick);
				}
				if (flag)
				{
					string arg_174_0 = (!string.IsNullOrEmpty(absTimerData.StackTrack)) ? absTimerData.StackTrack : absTimerData.Action.get_Method().get_Name();
				}
				absTimerData.DoAction();
				if (flag)
				{
				}
			}
			else
			{
				if (flag)
				{
					string arg_1B8_0 = (!string.IsNullOrEmpty(absTimerData.StackTrack)) ? absTimerData.StackTrack : absTimerData.Action.get_Method().get_Name();
				}
				absTimerData.DoAction();
				if (flag)
				{
				}
			}
		}
	}

	public static void Reset()
	{
		TimerHeap.m_unTick = 0u;
		TimerHeap.m_nNextTimerId = 0u;
		object queueLock = TimerHeap.m_queueLock;
		lock (queueLock)
		{
			while (TimerHeap.m_queue.Count != 0)
			{
				TimerHeap.m_queue.Dequeue();
			}
		}
	}

	private static uint AddTimer(AbsTimerData p)
	{
		if (Debug.get_isDebugBuild())
		{
			StackFrame stackFrame = new StackFrame(2, true);
			string text = (!Application.get_isMobilePlatform()) ? stackFrame.GetFileName() : stackFrame.GetFileName().Replace('\\', '/');
			p.StackTrack = string.Format("[{0}, {1}]", Path.GetFileName(text), stackFrame.GetFileLineNumber());
		}
		object queueLock = TimerHeap.m_queueLock;
		lock (queueLock)
		{
			TimerHeap.m_queue.Enqueue(p.NTimerId, p, p.UnNextTick);
		}
		return p.NTimerId;
	}

	private static T GetTimerData<T>(T p, uint start, int interval) where T : AbsTimerData
	{
		p.NInterval = interval;
		p.NTimerId = (TimerHeap.m_nNextTimerId += 1u);
		p.UnNextTick = (ulong)(TimerHeap.m_unTick + 1u + start);
		return p;
	}
}
