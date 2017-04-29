using System;
using UnityEngine;

public class FPSManager
{
	private static FPSManager instance;

	private int m_time_pass;

	private bool m_inFPSSleep;

	public static FPSManager Instance
	{
		get
		{
			if (FPSManager.instance == null)
			{
				FPSManager.instance = new FPSManager();
			}
			return FPSManager.instance;
		}
	}

	private FPSManager()
	{
	}

	public void Init()
	{
		this.ResetFPS();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondPast));
		EventDispatcher.AddListener("UIStateSystem.ResetFPSSleep", new Callback(this.OnResetFPSSleep));
	}

	public void Release()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondPast));
		EventDispatcher.RemoveListener("UIStateSystem.ResetFPSSleep", new Callback(this.OnResetFPSSleep));
	}

	private void OnSecondPast()
	{
		this.m_time_pass++;
		if (this.m_time_pass >= 300 && !this.m_inFPSSleep)
		{
			this.m_inFPSSleep = true;
			Application.set_targetFrameRate(20);
		}
	}

	private void OnResetFPSSleep()
	{
		this.m_time_pass = 0;
		if (this.m_inFPSSleep)
		{
			this.m_inFPSSleep = false;
			this.ResetFPS();
		}
	}

	public void FPSLimitOff()
	{
		Debug.LogError("===>IsTargetFrameRateOn off");
		Application.set_targetFrameRate(-1);
	}

	public void SetFPS(int fps)
	{
		AppConst.GameFrameRate = fps;
		this.ResetFPS();
	}

	public void ResetFPS()
	{
		if (SystemConfig.IsTargetFrameRateOn)
		{
			Application.set_targetFrameRate(AppConst.GameFrameRate);
		}
		else
		{
			this.FPSLimitOff();
		}
	}

	public void vSyncOff()
	{
		QualitySettings.set_vSyncCount(0);
	}
}
