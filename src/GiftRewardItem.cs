using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class GiftRewardItem : BaseUIBehaviour
{
	public long goodsNumber;

	private Image frame;

	private Image icon;

	private Text num;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.frame = base.FindTransform("Frame").GetComponent<Image>();
		this.icon = base.FindTransform("Icon").GetComponent<Image>();
		this.num = base.FindTransform("Num").GetComponent<Text>();
	}

	public void SetItem(int itemId, int itemNum)
	{
		GameDataUtils.SetItem(itemId, this.frame, this.icon, null, true);
		this.num.set_text(itemNum.ToString());
	}
}
