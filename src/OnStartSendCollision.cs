using System;
using UnityEngine;

public class OnStartSendCollision : MonoBehaviour
{
	private EffectSettings effectSettings;

	private bool isInitialized;

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
		this.effectSettings.OnCollisionHandler(new CollisionInfo());
		this.isInitialized = true;
	}

	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.effectSettings.OnCollisionHandler(new CollisionInfo());
		}
	}
}
