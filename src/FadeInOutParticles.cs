using System;
using UnityEngine;

public class FadeInOutParticles : MonoBehaviour
{
	private EffectSettings effectSettings;

	private ParticleSystem[] particles;

	private bool oldVisibleStat;

	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.get_parent();
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.get_transform());
			}
		}
	}

	private void Start()
	{
		this.GetEffectSettingsComponent(base.get_transform());
		this.particles = this.effectSettings.GetComponentsInChildren<ParticleSystem>();
		this.oldVisibleStat = this.effectSettings.IsVisible;
	}

	private void Update()
	{
		if (this.effectSettings.IsVisible != this.oldVisibleStat)
		{
			if (this.effectSettings.IsVisible)
			{
				for (int i = 0; i < this.particles.Length; i++)
				{
					if (this.effectSettings.IsVisible)
					{
						this.particles[i].Play();
						this.particles[i].set_enableEmission(true);
					}
				}
			}
			else
			{
				for (int j = 0; j < this.particles.Length; j++)
				{
					if (!this.effectSettings.IsVisible)
					{
						this.particles[j].Stop();
						this.particles[j].set_enableEmission(false);
					}
				}
			}
		}
		this.oldVisibleStat = this.effectSettings.IsVisible;
	}
}
