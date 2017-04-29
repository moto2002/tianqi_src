using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class XShopUIView : UIBase
{
	public static XShopUIView Instance;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		XShopUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(120803), "BACK", delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.FindTransform("RoleSayName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508044, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("ButtonShops").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.SourceBinding.MemberName = "ButtonShops";
		listBinder.PrefabName = "ButtonToggle2XShopSub";
		listBinder = base.FindTransform("Show1ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.SourceBinding.MemberName = "ItemList";
		listBinder.PrefabName = "XShoppingUnit";
		TextBinder textBinder = base.FindTransform("BuyTimeName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BuyTimeName";
		textBinder = base.FindTransform("RefreshTime").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "RefreshTime";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
