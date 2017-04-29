using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class LevelUpUIView : UIBase
{
	public static LevelUpUIView Instance;

	private VerticalLayoutGroup m_vlg;

	private int fxid;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.92f;
		this.isClick = true;
	}

	private void Awake()
	{
		LevelUpUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
		this.PlayFX();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIQueueManager.Instance.Islocked = false;
		this.DeleteFX();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			LevelUpUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		LevelUpUIViewModel.Instance.OnBtnComfirmClick();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnConfirmName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(500024, false));
		this.m_vlg = base.FindTransform("attrs").GetComponent<VerticalLayoutGroup>();
		Image component = base.FindTransform("Pic").GetComponent<Image>();
		ResourceManager.SetSprite(component, UIUtils.GetRoleSelfBodyImage());
		component.SetNativeSize();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("attrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "LevelUpUnit";
		listBinder.SourceBinding.MemberName = "UpgradeUnits";
		ImageBinder imageBinder = base.FindTransform("Name").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "FunctionName";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnConfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComfirmClick";
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
		LevelUpUIViewModel.Instance.RollingNext();
	}

	public void SetSpacing(float spacing)
	{
		this.m_vlg.set_spacing(spacing);
	}

	private void PlayFX()
	{
		this.DeleteFX();
		FXSpineManager.Instance.PlaySpine(1001, base.FindTransform("Name"), "LevelUpUI", 14002, delegate
		{
			this.fxid = FXSpineManager.Instance.ReplaySpine(this.fxid, 1002, base.FindTransform("Name"), "LevelUpUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fxid, true);
	}
}
