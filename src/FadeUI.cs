using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : UIBase
{
	private bool IsInit;

	private bool IsIncrement = true;

	private Action fadeOutCallback;

	private Action fadeInCallback;

	private float time;

	protected override void OnEnable()
	{
		this.IsInit = false;
		this.IsIncrement = true;
	}

	[DebuggerHidden]
	private IEnumerator routine(float waitTime)
	{
		FadeUI.<routine>c__Iterator46 <routine>c__Iterator = new FadeUI.<routine>c__Iterator46();
		<routine>c__Iterator.waitTime = waitTime;
		<routine>c__Iterator.<$>waitTime = waitTime;
		<routine>c__Iterator.<>f__this = this;
		return <routine>c__Iterator;
	}

	private void Update()
	{
		if (!this.IsInit)
		{
			return;
		}
		base.StartCoroutine(this.routine(this.time / 2f));
	}

	private void setAlpha(float a)
	{
		Color color = base.get_gameObject().GetComponent<Image>().get_color();
		float r = color.r;
		float g = color.g;
		float b = color.b;
		base.get_gameObject().GetComponent<Image>().set_color(new Color(r, g, b, a));
	}

	public void Init(Action _fadeOut, Action _fadeIn, float _time)
	{
		this.IsInit = true;
		this.fadeOutCallback = _fadeOut;
		this.fadeInCallback = _fadeIn;
		this.time = _time;
		this.setAlpha(0f);
	}
}
