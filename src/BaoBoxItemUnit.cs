using Foundation.Core.Databinding;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaoBoxItemUnit : BaseUIBehaviour
{
	public Image BaoBoxImg;

	public Text Value;

	private int boxIndex;

	private int value;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BaoBoxImg = base.FindTransform("BaoBoxImg").GetComponent<Image>();
		this.Value = base.FindTransform("Value").GetComponent<Text>();
		base.FindTransform("BtnBaoBoxItem").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBaoBox));
	}

	public void SetValue(int _value)
	{
		this.value = _value;
		this.Value.set_text(this.value.ToString());
	}

	private void OnClickBaoBox()
	{
		if (RechargeGiftUI.Instance != null && RechargeGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			RechargeGiftUI.Instance.ShowRewardList(this.value);
		}
		if (ConsumeGiftUI.Instance != null && ConsumeGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			ConsumeGiftUI.Instance.ShowRewardList(this.value);
		}
	}
}
