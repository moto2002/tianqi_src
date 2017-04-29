using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class GuildButtonItem : BaseUIBehaviour
{
	public int LV;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	public void Refresh(int lv)
	{
		this.LV = lv;
		base.FindTransform("lv").GetComponent<Text>().set_text(lv + "级");
	}
}
