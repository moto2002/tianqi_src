using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class UpgradeUIView : UIBase
{
	public static UpgradeUIView Instance;

	private RawImage m_spBackgroundImage;

	private VerticalLayoutGroup m_vlg;

	private Transform NodeBackground;

	private RawImage actorRawImage;

	private int fx_id;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		UpgradeUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIQueueManager.Instance.Islocked = false;
		this.RefreshFXOfUpgrade(false);
		this.SetState(true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spBackgroundImage = base.FindTransform("BackgroundImage").GetComponent<RawImage>();
		this.m_vlg = base.FindTransform("attrs").GetComponent<VerticalLayoutGroup>();
		this.NodeBackground = base.FindTransform("NodeBackground");
		this.actorRawImage = base.FindTransform("RawImageModel").GetComponent<RawImage>();
		RTManager.Instance.SetModelRawImage1(this.actorRawImage, false);
		EventTriggerListener expr_7F = EventTriggerListener.Get(base.FindTransform("ImageTouchPlace").get_gameObject());
		expr_7F.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_7F.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		UIAnimatorEventReceiver component = base.FindTransform("PanelRoot").GetComponent<UIAnimatorEventReceiver>();
		component.CallBackOfEnd = new Action(this.animEndOfUpgrade);
		UIAnimatorDisableWhenNoactive component2 = base.FindTransform("PanelRoot").GetComponent<UIAnimatorDisableWhenNoactive>();
		component2.DisableCallBack = new Action(this.animEndOfUpgrade);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ImageBinder imageBinder = base.FindTransform("StarIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "StarIcon";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("PetName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetName";
		textBinder = base.FindTransform("FightingNum1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FightingNum1";
		textBinder = base.FindTransform("FightingNum2").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FightingNum2";
		ListBinder listBinder = base.FindTransform("attrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "UpgradeUnit";
		listBinder.SourceBinding.MemberName = "UpgradeUnits";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("NaturalRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowNaturalRegion";
		imageBinder = base.FindTransform("NaturalIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "NaturalIcon";
		textBinder = base.FindTransform("NaturalName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "NaturalName";
		textBinder = base.FindTransform("NaturalDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "NaturalDesc";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnConfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComfirmClick";
	}

	public void SetBackground(int type)
	{
		ResourceManager.SetCodeTexture(this.m_spBackgroundImage, PetManagerBase.GetBackground(type));
	}

	public void SetSpacing(float spacing)
	{
		this.m_vlg.set_spacing(spacing);
	}

	public void SetRawImageModelLayer(bool skillPlaying, bool petShow, bool isActivation)
	{
		this.actorRawImage.set_enabled(petShow);
		this.JustPlayUpgrade(false);
		if (skillPlaying)
		{
			this.NodeBackground.SetAsLastSibling();
			if (this.actorRawImage != null)
			{
				this.actorRawImage.set_material(RTManager.Instance.RTNoAlphaMat);
			}
			if (isActivation)
			{
				this.actorRawImage.get_rectTransform().set_anchoredPosition(new Vector2(0f, 0f));
				this.m_spBackgroundImage.set_uvRect(new Rect(0f, 0f, 1f, 1f));
			}
		}
		else
		{
			this.NodeBackground.SetAsFirstSibling();
			if (this.actorRawImage != null)
			{
				this.actorRawImage.set_material(RTManager.Instance.RTNoAlphaMat);
			}
			if (isActivation)
			{
				this.JustPlayUpgrade(true);
				this.RefreshFXOfUpgrade(true);
			}
		}
	}

	private void animEndOfUpgrade()
	{
		this.JustPlayUpgrade(false);
	}

	private void JustPlayUpgrade(bool play)
	{
		base.FindTransform("PanelRoot").GetComponent<Animator>().set_enabled(play);
		if (!play)
		{
			this.SetState(false);
		}
	}

	private void SetState(bool isReadyState)
	{
		if (isReadyState)
		{
			(base.FindTransform("PanelRight") as RectTransform).set_anchoredPosition(new Vector2(1000f, 0f));
			base.FindTransform("PanelL").GetComponent<CanvasGroup>().set_alpha(0f);
			this.actorRawImage.get_rectTransform().set_anchoredPosition(new Vector2(0f, 0f));
			this.m_spBackgroundImage.set_uvRect(new Rect(0f, 0f, 1f, 1f));
		}
		else
		{
			(base.FindTransform("PanelRight") as RectTransform).set_anchoredPosition(Vector2.get_zero());
			base.FindTransform("PanelL").GetComponent<CanvasGroup>().set_alpha(1f);
			this.actorRawImage.get_rectTransform().set_anchoredPosition(new Vector2(-300f, 0f));
			this.m_spBackgroundImage.set_uvRect(new Rect(0.23f, 0f, 1f, 1f));
		}
	}

	private void RefreshFXOfUpgrade(bool arg)
	{
		if (arg)
		{
			if (this.fx_id == 0)
			{
				this.fx_id = FXSpineManager.Instance.PlaySpine(305, base.FindTransform("Star"), "UpgradeUI", 3001, null, "UI", -57f, 1f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
			this.fx_id = 0;
		}
	}
}
