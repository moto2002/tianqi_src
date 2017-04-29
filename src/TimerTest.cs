using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
	private static float boxWidthName = 150f;

	private static float boxWidthTime = 200f;

	private float timeUpdate;

	private float timeIEnumerator;

	private float deltaTimeIEnumerator = 1f;

	private float timeInvoke;

	private float deltaTimeInvoke = 0.01f;

	private float timeSystemTimer;

	private float deltaSystemTimer = 1f;

	private float timeThreadingTimer;

	private float deltaThreadingTimer = 1f;

	private Timer timerClose;

	private void OnGUI()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Box("Timer update: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.timeUpdate, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Box("accuracy: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + Time.get_deltaTime(), new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthTime)
		});
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Box("Timer IEnumerator: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.timeIEnumerator, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Box("accuracy: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.deltaTimeIEnumerator, new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthTime)
		});
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Box("Timer Invoke: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.timeInvoke, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Box("accuracy: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.deltaTimeInvoke, new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthTime)
		});
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Box("SystemTimer: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.timeSystemTimer, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Box("accuracy: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.deltaSystemTimer + "ms(1s/1000)", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthTime)
		});
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Box("ThreadingTimer: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.timeThreadingTimer, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Box("accuracy: ", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthName)
		});
		GUILayout.Label(string.Empty + this.deltaThreadingTimer + "ms(1s/1000)", new GUILayoutOption[]
		{
			GUILayout.Width(TimerTest.boxWidthTime)
		});
		GUILayout.EndHorizontal();
	}

	[DebuggerHidden]
	private IEnumerator wait(float seconds)
	{
		TimerTest.<wait>c__Iterator35 <wait>c__Iterator = new TimerTest.<wait>c__Iterator35();
		<wait>c__Iterator.seconds = seconds;
		<wait>c__Iterator.<$>seconds = seconds;
		<wait>c__Iterator.<>f__this = this;
		return <wait>c__Iterator;
	}

	private void InvokeTimer()
	{
		this.timeInvoke += this.deltaTimeInvoke;
	}

	private void Start()
	{
		base.StartCoroutine(this.wait(this.deltaTimeIEnumerator));
		base.InvokeRepeating("InvokeTimer", 0f, this.deltaTimeInvoke);
		Timer timer = new Timer((double)this.deltaSystemTimer);
		timer.add_Elapsed(new ElapsedEventHandler(this.theout));
		timer.set_AutoReset(true);
		timer.set_Enabled(true);
		this.timerClose = new Timer(new TimerCallback(this.timerCall), this, 0, (int)this.deltaThreadingTimer);
		this.timerClose.ToString();
	}

	public void theout(object source, ElapsedEventArgs e)
	{
		this.timeSystemTimer += this.deltaSystemTimer / 1000f;
	}

	private void timerCall(object obj)
	{
		this.timeThreadingTimer += this.deltaThreadingTimer / 1000f;
	}

	private void Update()
	{
		this.timeUpdate += Time.get_deltaTime();
	}
}
