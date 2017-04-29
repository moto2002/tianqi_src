using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionIntroductionUI : UIBase
{
	private Action endCallBack;

	public Text name1;

	public Text name2;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	public void SetInit(string name)
	{
		this.name1.set_text(name);
		this.name2.set_text(name);
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		componentInChildren.set_enabled(true);
		componentInChildren.Play("Introduction", 0, 0f);
	}

	public void OnIntroductionEnd()
	{
	}
}
