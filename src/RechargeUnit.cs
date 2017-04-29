using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class RechargeUnit : BaseUIBehaviour
{
	private int fx_id;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("MCDayTip").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513003, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("PriceNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PriceNum";
		textBinder = base.FindTransform("ExtraNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ExtraNum";
		textBinder = base.FindTransform("ExtraEveryDay").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ExtraEveryDayNum";
		textBinder = base.FindTransform("DiamondNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DiamondNum";
		textBinder = base.FindTransform("VIPTipNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPTipNum";
		textBinder = base.FindTransform("MCDayNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MCDayNum";
		textBinder = base.FindTransform("DaysNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DaysNum";
		textBinder = base.FindTransform("TreasureName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TreasureName";
		ImageBinder imageBinder = base.FindTransform("DiamondIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "BigIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("TreasureIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "TreasureIcon";
		imageBinder.SetNativeSize = true;
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("VIPTip").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPTipVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Extra").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPExtraVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ExtraEveryDay").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExtraEveryDayVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("MCDay").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MCDayVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Price").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "PriceVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("DailyLimitIcon").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "DailyLimitIconVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("TodayBought").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "TodayBoughtVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Treasure").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "TreasureVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Diamond").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "DiamondVisibility";
		ActionBinder actionBinder = base.get_gameObject().AddComponent<ActionBinder>();
		actionBinder.BindingProxy = base.get_gameObject();
		actionBinder.CallActionOfBoolBinding.MemberName = "RefreshFXOfMonthCard";
		actionBinder.actoncall_bool = new Action<bool>(this.RefreshFXOfMonthCard);
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUp";
	}

	private void RefreshFXOfMonthCard(bool arg)
	{
		if (arg)
		{
			if (this.fx_id == 0)
			{
				this.fx_id = FXSpineManager.Instance.PlaySpine(702, base.get_transform(), "PrivilegeUI", 2001, null, "UI", -4f, 94f, 1f, 1f, true, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
			this.fx_id = 0;
		}
	}
}
