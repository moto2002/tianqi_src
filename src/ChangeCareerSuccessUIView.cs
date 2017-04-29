using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class ChangeCareerSuccessUIView : UIBase
{
	public static ChangeCareerSuccessUIView Instance;

	private int fxid;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.85f;
	}

	private void Awake()
	{
		ChangeCareerSuccessUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.PlayFX();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.RollingNext, new Callback(this.OnRollingNext));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.RollingNext, new Callback(this.OnRollingNext));
	}

	private void OnRollingNext()
	{
		ChangeCareerSuccessUIViewModel.Instance.RollingNext();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnConfirmName").GetComponent<Text>().set_text("点击返回");
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("attrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "ChangeCareerSuccessUnit";
		listBinder.SourceBinding.MemberName = "ChangeCareerUnits";
		ImageBinder imageBinder = base.FindTransform("CareerNameBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "CareerNameBg";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("CareerName").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "CareerName";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("Pic").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "CareerPic";
		imageBinder.SetNativeSize = true;
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnConfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnBtnConfirmClick";
	}

	private void PlayFX()
	{
		this.DeleteFX();
		FXSpineManager.Instance.PlaySpine(2101, base.FindTransform("Name"), "ChangeCareerSuccessUI", 14002, delegate
		{
			this.fxid = FXSpineManager.Instance.ReplaySpine(this.fxid, 2102, base.FindTransform("Name"), "ChangeCareerSuccessUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fxid, true);
	}
}
