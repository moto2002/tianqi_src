using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class PopButtonsAdjustUIView : UIBase
{
	public static PopButtonsAdjustUIView Instance;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.5f;
		this.isClick = true;
	}

	private void Awake()
	{
		PopButtonsAdjustUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("UIManagerControl.HidePopButtonsAdjustUI", new Callback(this.HidePopButtonsAdjustUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("UIManagerControl.HidePopButtonsAdjustUI", new Callback(this.HidePopButtonsAdjustUI));
	}

	private void HidePopButtonsAdjustUI()
	{
		this.Show(false);
	}

	protected override void InitUI()
	{
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Privates").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "ButtonInfo2Adjust";
		listBinder.SourceBinding.MemberName = "ButtonInfos";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public void SetBackgroundHeight(float height)
	{
		RectTransform rectTransform = base.FindTransform("PrivatesScrollBg") as RectTransform;
		rectTransform.set_sizeDelta(new Vector2(rectTransform.get_sizeDelta().x, height));
	}
}
