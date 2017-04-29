using System;
using UnityEngine;

public class GlobalRatioMgr
{
	private DS_TimerCMDStack timerStack;

	public static readonly GlobalRatioMgr Instance = new GlobalRatioMgr();

	private GlobalRatioMgr()
	{
		this.timerStack = new DS_TimerCMDStack(delegate
		{
			Time.set_timeScale(AppConst.GlobalTimeScale);
		});
	}

	public void SetGlobalTimeRatio(float ratio, int millisecondSeconds)
	{
		this.timerStack.Push(new FuncWithEndTime
		{
			doFunc = delegate
			{
				Time.set_timeScale(ratio);
			},
			endTime = DateTime.get_Now() + TimeSpan.FromMilliseconds((double)millisecondSeconds)
		});
	}
}
