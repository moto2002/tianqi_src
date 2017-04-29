using System;
using UnityEngine;

public class RainTimer : TimerInterval
{
	private bool m_bRainEffectOpen = true;

	public void TimeUpdate()
	{
		if (base.IsTime1Over())
		{
			this.m_bRainEffectOpen = !this.m_bRainEffectOpen;
			if (this.m_bRainEffectOpen)
			{
				base.Circle1 = Random.Range(RainEffectManager.Instance.MinRainOpenTime, RainEffectManager.Instance.MaxRainOpenTime);
				this.SetRain(true);
			}
			else
			{
				base.Circle1 = Random.Range(RainEffectManager.Instance.MinRainCloseTime, RainEffectManager.Instance.MaxRainCloseTime);
				this.SetRain(false);
			}
		}
	}

	public void ResetTime()
	{
		base.Circle1 = Random.Range(RainEffectManager.Instance.MinRainOpenTime, RainEffectManager.Instance.MaxRainOpenTime);
	}

	private void SetRain(bool bEnable)
	{
		base.get_transform().GetComponent<MeshRenderer>().set_enabled(bEnable);
	}
}
