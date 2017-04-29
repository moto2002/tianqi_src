using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class PrivilegePage : BaseUIBehaviour
{
	private int fx_id;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("SmallList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "PrivilegeSmallItem";
		listBinder.SourceBinding.MemberName = "SmallItems";
		ImageBinder imageBinder = base.FindTransform("PrivilegeItemBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PrivilegeItemBg";
		TextBinder textBinder = base.FindTransform("VIPName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPName";
		textBinder = base.FindTransform("TimesNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TimesNum";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Node2Hide").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "Node2HideVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("TimesTip").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "TimesTipOn";
		ActionBinder actionBinder = base.get_gameObject().AddComponent<ActionBinder>();
		actionBinder.BindingProxy = base.get_gameObject();
		actionBinder.CallActionOfBoolBinding.MemberName = "RefreshFXOfBox";
		actionBinder.actoncall_bool = new Action<bool>(this.RefreshFXOfBox);
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("PrivilegeItem").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnPrivilegeItemClick";
	}

	private void RefreshFXOfBox(bool arg)
	{
		if (arg)
		{
			if (this.fx_id == 0)
			{
				this.fx_id = FXSpineManager.Instance.PlaySpine(704, base.get_transform(), "PrivilegeUI", 2001, null, "UI", 142f, -112f, 1f, 1f, true, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
			this.fx_id = 0;
		}
	}
}
