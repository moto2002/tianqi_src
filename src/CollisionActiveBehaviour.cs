using System;
using UnityEngine;

public class CollisionActiveBehaviour : MonoBehaviour
{
	public bool IsReverse;

	public float TimeDelay;

	public bool IsLookAt;

	private EffectSettings effectSettings;

	private void Start()
	{
		this.GetEffectSettingsComponent(base.get_transform());
		if (this.IsReverse)
		{
			this.effectSettings.RegistreInactiveElement(base.get_gameObject(), this.TimeDelay);
			base.get_gameObject().SetActive(false);
		}
		else
		{
			this.effectSettings.RegistreActiveElement(base.get_gameObject(), this.TimeDelay);
		}
		if (this.IsLookAt)
		{
			this.effectSettings.CollisionEnter += new EventHandler<CollisionInfo>(this.effectSettings_CollisionEnter);
		}
	}

	private void effectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		base.get_transform().LookAt(this.effectSettings.get_transform().get_position() + e.Hit.get_normal());
	}

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
}
