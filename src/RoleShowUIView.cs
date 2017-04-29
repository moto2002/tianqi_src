using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class RoleShowUIView : UIBase
{
	public static RoleShowUIView Instance;

	public Transform PanelProperty;

	public Transform PanelFormation;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		RoleShowUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110014), "BACK", delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected override void OnDisable()
	{
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		CurrenciesUIViewModel.Show(false);
	}

	protected override void InitUI()
	{
		RTManager.Instance.SetModelRawImage1(base.FindTransform("RawImageActor").GetComponent<RawImage>(), false);
		EventTriggerListener expr_30 = EventTriggerListener.Get(base.FindTransform("ImageTouchPlaces").get_gameObject());
		expr_30.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_30.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.PanelProperty = base.FindTransform("PanelProperty");
		this.PanelFormation = base.FindTransform("PanelFormation");
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubProperty";
		visibilityBinder.Target = base.FindTransform("PanelProperty").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubFormation";
		visibilityBinder.Target = base.FindTransform("PanelFormation").get_gameObject();
		TextBinder textBinder = base.FindTransform("FightNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextPower";
		textBinder = base.FindTransform("TextLV").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextLv";
		textBinder = base.FindTransform("TextName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextName";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ToggleBinder toggleBinder = base.FindTransform("SubProperty").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubProperty";
		toggleBinder = base.FindTransform("SubFormation").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubFormation";
	}
}
