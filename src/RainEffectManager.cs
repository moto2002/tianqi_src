using System;
using System.Collections.Generic;
using UnityEngine;

public class RainEffectManager : MonoBehaviour
{
	private static RainEffectManager m_instance;

	public float MaxRainOpenTime = 30f;

	public float MinRainOpenTime = 5f;

	public float MaxRainCloseTime = 30f;

	public float MinRainCloseTime = 5f;

	public bool m_bRainEnable;

	private List<RainTimer> m_listRainTimer = new List<RainTimer>();

	public static RainEffectManager Instance
	{
		get
		{
			return RainEffectManager.m_instance;
		}
	}

	private void Awake()
	{
		RainEffectManager.m_instance = this;
		this.EnableRain(this.m_bRainEnable);
	}

	public void EnableRain(bool bEnable)
	{
		this.m_bRainEnable = bEnable;
		this.SetRain(bEnable);
	}

	private void SetRain(bool isRain)
	{
		base.get_transform().FindChild("Root").get_gameObject().SetActive(isRain);
	}

	public void AddRainTimer(RainTimer rt)
	{
		rt.ResetTime();
		this.m_listRainTimer.Add(rt);
	}

	private void Update()
	{
		if (!this.m_bRainEnable)
		{
			return;
		}
		for (int i = 0; i < this.m_listRainTimer.get_Count(); i++)
		{
			if (this.m_listRainTimer.get_Item(i) != null)
			{
				this.m_listRainTimer.get_Item(i).TimeUpdate();
			}
		}
	}
}
