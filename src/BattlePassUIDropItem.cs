using Foundation.Core.Databinding;
using System;

public class BattlePassUIDropItem : BaseUIBehaviour
{
	private int fx_id;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	private void OnEnable()
	{
		this.fx_id = FXSpineManager.Instance.ReplaySpine(this.fx_id, 409, base.get_transform(), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void OnDisable()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "IconBg";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "Icon";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("Num").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "GoodNum";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("Button").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnButtonClick";
	}
}
