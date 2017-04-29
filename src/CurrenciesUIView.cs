using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class CurrenciesUIView : UIBase
{
	public static CurrenciesUIView Instance;

	private int fxgoldId;

	private int fxdiamId;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
		this.isEndNav = false;
		this.isInterruptStick = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		CurrenciesUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.PlayFX();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ShowPopAnimation(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			CurrenciesUIView.Instance = null;
			CurrenciesUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("DiamondLab").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Diamond";
		textBinder = base.FindTransform("GoldLab").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Gold";
		textBinder = base.FindTransform("StrengthLab").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Strength";
		textBinder = base.FindTransform("BtnBackName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnBackName";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("SubUI").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowSubUI";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("CurrenciesClass").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowCurrenciesClass";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("GoldAdd").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowGoldAdd";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("DiamondAdd").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowDiamondAdd";
		ImageBinder imageBinder = base.FindTransform("SubUIName").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SetNativeSize = true;
		imageBinder.SpriteBinding.MemberName = "SubUIName";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("Diamond").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickDiamond";
		buttonBinder = base.FindTransform("Gold").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickGold";
		buttonBinder = base.FindTransform("Strength").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickStrength";
		buttonBinder = base.FindTransform("BtnBack").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickBack";
	}

	private void PlayFX()
	{
		this.fxdiamId = FXSpineManager.Instance.ReplaySpine(this.fxdiamId, 1201, base.FindTransform("DiamondImg"), "CurrenciesUI", 2004, null, "UI", -4f, -3f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fxgoldId = FXSpineManager.Instance.ReplaySpine(this.fxgoldId, 1202, base.FindTransform("GoldImg"), "CurrenciesUI", 2004, null, "UI", 1f, -2f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void ShowPopAnimation(bool isShow)
	{
		Animator component = base.GetComponent<Animator>();
		component.set_enabled(isShow);
		if (isShow)
		{
			component.Play("CurrenciesUI_open", 0, 0f);
		}
	}
}
