using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesUI : UIBase
{
	private Action callback;

	private List<string> content;

	private bool IsFadedIn;

	protected override void OnEnable()
	{
		ButtonCustom component = base.get_transform().FindChild("btnClose").GetComponent<ButtonCustom>();
		component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnClose);
	}

	protected override void OnClickMaskAction()
	{
		if (this.content.get_Count() == 1)
		{
			base.OnClickMaskAction();
			this.callback.Invoke();
		}
		else
		{
			this.content.RemoveAt(0);
			this.SetContent(this.content.get_Item(0));
			this.IsFadedIn = true;
		}
	}

	[DebuggerHidden]
	private IEnumerator routine(float waitTime)
	{
		SubtitlesUI.<routine>c__Iterator49 <routine>c__Iterator = new SubtitlesUI.<routine>c__Iterator49();
		<routine>c__Iterator.waitTime = waitTime;
		<routine>c__Iterator.<$>waitTime = waitTime;
		<routine>c__Iterator.<>f__this = this;
		return <routine>c__Iterator;
	}

	private void Update()
	{
		if (!this.IsFadedIn)
		{
			return;
		}
		base.StartCoroutine(this.routine(5f));
	}

	private void OnBtnClose(GameObject go)
	{
		if (this.content.get_Count() == 1)
		{
			this.callback.Invoke();
			this.Show(false);
		}
		else
		{
			this.content.RemoveAt(0);
			this.SetContent(this.content.get_Item(0));
			this.IsFadedIn = true;
		}
	}

	public void Init(List<string> content, Action _callback)
	{
		this.content = content;
		this.SetContent(content.get_Item(0));
		this.SetCallback(_callback);
	}

	private void SetContent(string content)
	{
		Text component = base.get_transform().FindChild("texContent").GetComponent<Text>();
		component.set_text(content);
		this.IsFadedIn = true;
		float r = component.get_color().r;
		float g = component.get_color().g;
		float b = component.get_color().b;
		component.set_color(new Color(r, g, b, 0f));
	}

	private void SetCallback(Action _callback)
	{
		this.callback = _callback;
	}
}
