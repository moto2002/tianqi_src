using Foundation.Core.Databinding;
using System;

public class ElementInstancePropertyUI : UIBase
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}
}
