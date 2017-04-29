using Foundation.Core.Databinding;
using System;

public class HuntInstructionUI : UIBase
{
	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.alpha = 0.7f;
		this.isMask = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	protected override void InitUI()
	{
		base.InitUI();
	}
}
