using Foundation.Core.Databinding;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;

public class CloseServerUI : UIBase
{
	protected Text text;

	protected int countDownSecond;

	protected Action countDownCallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.text = base.FindTransform("TimeDown").GetComponent<Text>();
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		CloseServerUI.<Start>c__Iterator43 <Start>c__Iterator = new CloseServerUI.<Start>c__Iterator43();
		<Start>c__Iterator.<>f__this = this;
		return <Start>c__Iterator;
	}

	public void SetTime(int time, Action endAction)
	{
		this.countDownSecond = time;
		this.countDownCallBack = endAction;
	}
}
