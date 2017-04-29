using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class TerrestrialGlobeUI : UIBase
{
	public static UIBase Instance;

	private void Awake()
	{
		TerrestrialGlobeUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		ModelDisplayManager.Instance.ShowTerrestrialGlobe(true);
		RTManager.Instance.SetRotate(true, true);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManagerBase.GetNullSprite(), "BACK", delegate
		{
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
		RTManager.Instance.SetModelRawImage1(base.FindTransform("RawImageModel").GetComponent<RawImage>(), false);
		EventTriggerListener expr_30 = EventTriggerListener.Get(base.FindTransform("ImageTouchPlace").get_gameObject());
		expr_30.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_30.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
	}
}
