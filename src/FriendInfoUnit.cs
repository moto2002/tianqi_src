using Foundation.Core.Databinding;
using System;
using UnityEngine;

public class FriendInfoUnit : BaseUIBehaviour
{
	public const uint delDuration = 350u;

	public Transform BtnTops;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.BtnTops = base.FindTransform("BtnTops");
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ImageBinder imageBinder = base.FindTransform("FriendIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SetNativeSize = true;
		imageBinder.SpriteBinding.MemberName = "FriendIcon";
		imageBinder.HSVBinding.MemberName = "FriendIconHSV";
		imageBinder = base.FindTransform("VIPLevel1").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel1";
		imageBinder = base.FindTransform("VIPLevel2").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel2";
		TextBinder textBinder = base.FindTransform("LevelValue").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Level";
		textBinder = base.FindTransform("FriendName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FriendName";
		textBinder = base.FindTransform("BattlePowerValue").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BattlePower";
		VisibilityBinder visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnCheckVisibility";
		visibilityBinder.Target = base.FindTransform("CheckBtn").get_gameObject();
		visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnChatVisibility";
		visibilityBinder.Target = base.FindTransform("ChatBtn").get_gameObject();
		visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnAgreeVisibility";
		visibilityBinder.Target = base.FindTransform("AgreeBtn").get_gameObject();
		visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnRefuseVisibility";
		visibilityBinder.Target = base.FindTransform("RefuseBtn").get_gameObject();
		visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnRemoveVisibility";
		visibilityBinder.Target = base.FindTransform("RemoveBtn").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "RecommendImgVisibility";
		visibilityBinder.Target = base.FindTransform("RecommendImage").get_gameObject();
		UtilsBinder utilsBinder = base.FindTransform("BtnTops").get_gameObject().AddComponent<UtilsBinder>();
		utilsBinder.BindingProxy = base.get_gameObject();
		utilsBinder.Set2ParentPositionBinding.MemberName = "BtnTopsTransform";
		ActionBinder actionBinder = base.get_gameObject().AddComponent<ActionBinder>();
		actionBinder.BindingProxy = base.get_gameObject();
		actionBinder.CallActionOfBoolBinding.MemberName = "CallAction";
		actionBinder.actoncall_bool = new Action<bool>(this.DelAnim);
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUp";
		buttonBinder = base.FindTransform("CheckBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnCheck";
		buttonBinder = base.FindTransform("ChatBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnChat";
		buttonBinder.EnabledBinding.MemberName = "BtnChatEnable";
		buttonBinder = base.FindTransform("AgreeBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnAgree";
		buttonBinder = base.FindTransform("RefuseBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnRefuse";
		buttonBinder = base.FindTransform("RemoveBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnRemove";
	}

	private void DelAnim(bool arg)
	{
		if (!arg)
		{
			return;
		}
		ListViewBinder src = base.get_transform().get_parent().get_parent().GetComponent<ListViewBinder>();
		if (src != null)
		{
			src.set_enabled(false);
			RectTransform rectTransform = base.get_transform() as RectTransform;
			base.StartCoroutine(rectTransform.MoveToAnchoredPosition(new Vector3(rectTransform.get_anchoredPosition().x - 1000f, rectTransform.get_anchoredPosition().y), 0.35f, EaseType.Linear, null));
			TimerHeap.AddTimer(350u, 0, delegate
			{
				src.set_enabled(true);
			});
		}
	}
}
