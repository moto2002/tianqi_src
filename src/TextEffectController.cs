using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextEffectController : MonoBehaviour
{
	public bool EnableOutline = true;

	public bool EnableShadow = true;

	private void Awake()
	{
		if (Application.get_isPlaying())
		{
			this.SetEnableOutline();
			this.SetEnableShadow();
		}
	}

	private void Update()
	{
		if (!Application.get_isPlaying())
		{
			this.SetEnableOutline();
			this.SetEnableShadow();
		}
	}

	private void SetEnableOutline()
	{
		if (this.EnableOutline)
		{
			Outline outline = base.get_gameObject().AddMissingComponent<Outline>();
			outline.set_effectColor(TextColorMgr.EffectColor2Outline);
			outline.set_enabled(true);
		}
		else
		{
			Outline component = base.get_gameObject().GetComponent<Outline>();
			if (component != null)
			{
				component.set_enabled(false);
			}
		}
	}

	private void SetEnableShadow()
	{
		if (this.EnableShadow)
		{
			Shadow[] components = base.get_gameObject().GetComponents<Shadow>();
			Shadow shadow = null;
			for (int i = 0; i < components.Length; i++)
			{
				if (!(components[i] is Outline))
				{
					shadow = components[i];
				}
			}
			if (shadow == null)
			{
				shadow = base.get_gameObject().AddComponent<Shadow>();
			}
			shadow.set_effectColor(TextColorMgr.EffectColor2Shadow);
			shadow.set_enabled(true);
		}
		else
		{
			Shadow component = base.get_gameObject().GetComponent<Shadow>();
			if (component != null)
			{
				component.set_enabled(false);
			}
		}
	}
}
