using Foundation.Core.Databinding;
using System;

public class PetChooseUnit : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("PetIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetIcon";
		imageBinder.HSVBinding.MemberName = "PetIconHSV";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("PetIconBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetIconBg";
		imageBinder.HSVBinding.MemberName = "PetIconHSV";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BadgeTip").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BadgeTip";
		TextBinder textBinder = base.FindTransform("PetName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetName";
		textBinder = base.FindTransform("Level").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Level";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MatVisibility";
		visibilityBinder.Target = base.FindTransform("Node2Chip").get_gameObject();
		textBinder = base.FindTransform("ChipMatNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MatNum";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("InFormation").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "InFormation";
		RenameBinder renameBinder = base.get_gameObject().AddComponent<RenameBinder>();
		renameBinder.BindingProxy = base.get_gameObject();
		renameBinder.prefix = "Item_";
		renameBinder.RenameBinding.MemberName = "PetId";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnChoosed";
	}

	private void RefreshFXOfActivation(bool arg)
	{
		if (arg)
		{
			if (this.fx_id == 0)
			{
				this.fx_id = FXSpineManager.Instance.PlaySpine(301, base.get_transform(), "PetChooseUI", 2001, null, "UI", 31f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
			this.fx_id = 0;
		}
	}
}
