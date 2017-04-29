using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class PetBasicUIView : UIBase
{
	private const float X_OFFSET = 300f;

	private const float TIME = 0.65f;

	public static PetBasicUIView Instance;

	private Transform NodeBackground;

	private RawImage actorRawImage;

	private RawImage m_spBackgroundImage;

	private RawImage m_spBackgroundImageMask;

	public Transform Node2PetChoose;

	public PolygenSytem m_PolygenSytem;

	private Text m_lblBattlePower;

	private int fx_fighting_id;

	private BaseTweenPostion m_btpBackgroundImage;

	private BaseTweenPostion m_btpRawImageModel;

	private int fx_id_upgrademax;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isInterruptStick = true;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		PetBasicUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("PetChooseUI");
		if (uIIfExist != null)
		{
			uIIfExist.get_gameObject().SetActive(true);
		}
		this.SetRawImageModelLayer(false, true, false);
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		RenderSettings.set_fog(false);
		ModelDisplayManager.IsAlwaysCombo = true;
		ModelDisplayManager.Instance.ShowPetScene(true);
		this.SetBack(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		RenderSettings.set_fog(true);
		ModelDisplayManager.IsAlwaysCombo = false;
		ModelDisplayManager.Instance.ShowPetScene(false);
		CurrenciesUIViewModel.Show(false);
		EventDispatcher.Broadcast("UIManagerControl.HideTipsUI");
		this.JustPlayActivation(false, false);
		this.RefreshFXOfUpgradeMax(false);
		if (this.m_spBackgroundImage != null)
		{
			RTSyncBackground component = this.m_spBackgroundImage.GetComponent<RTSyncBackground>();
			if (component != null)
			{
				component.Release();
			}
		}
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PetBasicUIView.Instance = null;
			PetBasicUIViewModel.Instance = null;
			ModelPool.Instance.Clear();
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		this.NodeBackground = base.FindTransform("NodeBackground");
		this.actorRawImage = base.FindTransform("RawImageModel").GetComponent<RawImage>();
		RTManager.Instance.SetModelRawImage1(this.actorRawImage, false);
		EventTriggerListener expr_4D = EventTriggerListener.Get(base.FindTransform("ImageTouchPlace").get_gameObject());
		expr_4D.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_4D.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.Node2PetChoose = base.FindTransform("Node2PetChoose");
		this.m_PolygenSytem = base.FindTransform("RootPetPolygen").GetComponent<PolygenSytem>();
		this.m_PolygenSytem.Init();
		this.m_spBackgroundImage = base.FindTransform("BackgroundImage").GetComponent<RawImage>();
		this.m_spBackgroundImageMask = base.FindTransform("BackgroundImageMask").GetComponent<RawImage>();
		this.InitPosition();
		UIAnimatorEventReceiver component = base.FindTransform("SubPanelPetInfo").GetComponent<UIAnimatorEventReceiver>();
		component.CallBackOfEnd = new Action(this.animEndOfActivation);
		UIAnimatorDisableWhenNoactive component2 = base.FindTransform("SubPanelPetInfo").GetComponent<UIAnimatorDisableWhenNoactive>();
		component2.DisableCallBack = new Action(this.animEndOfActivation);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelPets";
		visibilityBinder.Target = base.FindTransform("SubPanelPets").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelFormation";
		visibilityBinder.Target = base.FindTransform("SubPanelFormation").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubFormationBadge";
		visibilityBinder.Target = base.FindTransform("SubFormationBadge").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelCombination";
		visibilityBinder.Target = base.FindTransform("SubPanelCombination").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelPetInfo";
		visibilityBinder.Target = base.FindTransform("SubPanelPetInfo").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelPetInfoRoot";
		visibilityBinder.Target = base.FindTransform("SubPPIPanelRoot").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnLevel";
		visibilityBinder.Target = base.FindTransform("PanelLevel").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnLevelBadge";
		visibilityBinder.Target = base.FindTransform("FnBtnLevelBadge").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnUpgrade";
		visibilityBinder.Target = base.FindTransform("PanelUpgrade").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnUpgradeBadge";
		visibilityBinder.Target = base.FindTransform("FnBtnUpgradeBadge").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnSkillEvo";
		visibilityBinder.Target = base.FindTransform("PanelSkillEvo").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FnBtnSkillEvoBadge";
		visibilityBinder.Target = base.FindTransform("FnBtnSkillEvoBadge").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelSkillEvo";
		visibilityBinder.Target = base.FindTransform("SubPanelSkillEvo").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SubPanelNaturalEvo";
		visibilityBinder.Target = base.FindTransform("SubPanelNaturalEvo").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.Target = base.FindTransform("SubPPIArrow").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowArrow";
		ListBinder listBinder = base.FindTransform("BasicAttrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = WidgetName.PetBasicAttr;
		listBinder.SourceBinding.MemberName = "BasicAttrs";
		TextBinder textBinder = base.FindTransform("PetName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetName";
		textBinder = base.FindTransform("PetLevel").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetLevel";
		this.m_lblBattlePower = base.FindTransform("BattlePower").GetComponent<Text>();
		textBinder = base.FindTransform("MorphBtnText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MorphBtnText";
		ImageBinder imageBinder = base.FindTransform("PetTypeIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetTypeIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("PetTypeZi").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetTypeZi";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("PetQualityIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetQuality";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("SupportSkillIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "SupportSkillIcon";
		imageBinder.HSVBinding.MemberName = "SupportSkillHSV";
		listBinder = base.FindTransform("ExpList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = WidgetName.PetEXPUnit;
		listBinder.ITEM_NAME = "ItemExp_";
		listBinder.SourceBinding.MemberName = "PetEXPs";
		FillAmmountBinder fillAmmountBinder = base.FindTransform("EXPBarFg").get_gameObject().AddComponent<FillAmmountBinder>();
		fillAmmountBinder.BindingProxy = base.get_gameObject();
		fillAmmountBinder.FillValueBinding.MemberName = "EXPNumAmount";
		textBinder = base.FindTransform("EXPNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "EXPNum";
		listBinder = base.FindTransform("LevelBasicAttrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = WidgetName.PetBasicAttr;
		listBinder.SourceBinding.MemberName = "LevelBasicAttrs";
		fillAmmountBinder = base.FindTransform("UpMatBarFg").get_gameObject().AddComponent<FillAmmountBinder>();
		fillAmmountBinder.BindingProxy = base.get_gameObject();
		fillAmmountBinder.FillValueBinding.MemberName = "FragNumAmount";
		textBinder = base.FindTransform("UpMatNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FragNum";
		imageBinder = base.FindTransform("UpgradePetNowFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowFrame";
		imageBinder = base.FindTransform("UpgradePetNowFramePet").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowFramePet";
		imageBinder = base.FindTransform("UpgradePetNowIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("UpgradePetNowQuality").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowQuality";
		imageBinder.SetNativeSize = true;
		textBinder = base.FindTransform("UpgradePetNowFighting").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "UpgradePetNowFighting";
		imageBinder = base.FindTransform("UpgradePetUpFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetUpFrame";
		imageBinder = base.FindTransform("UpgradePetUpFramePet").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetUpFramePet";
		imageBinder = base.FindTransform("UpgradePetUpIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetUpIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("UpgradePetUpQuality").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetUpQuality";
		imageBinder.SetNativeSize = true;
		textBinder = base.FindTransform("UpgradePetUpFighting").get_gameObject().AddComponent<TextBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "UpgradePetUpFighting";
		imageBinder = base.FindTransform("UpgradePetMaxFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowFrame";
		imageBinder = base.FindTransform("UpgradePetMaxIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("UpgradePetMaxQuality").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpgradePetNowQuality";
		imageBinder.SetNativeSize = true;
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "UpgradeMax";
		visibilityBinder.Target = base.FindTransform("UpgradeMax").get_gameObject();
		visibilityBinder.InverseTarget = base.FindTransform("UpgradeNormal").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowUpNaturalEvo";
		visibilityBinder.Target = base.FindTransform("UpNaturalEvo").get_gameObject();
		imageBinder = base.FindTransform("UpNaturalEvoIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "UpNaturalEvoIcon";
		textBinder = base.FindTransform("UpNaturalEvoName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "UpNaturalEvoName";
		textBinder = base.FindTransform("UpNaturalEvoDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "UpNaturalEvoDesc";
		textBinder = base.FindTransform("SkillPointInfo").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "SkillPointInfo";
		textBinder = base.FindTransform("PetAttackNow").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetAttackNow";
		imageBinder = base.FindTransform("BtnFollowBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "BtnFollowBg";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnFollow").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnFollowShow";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ToggleBinder toggleBinder = base.FindTransform("SubPets").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubPanelPets";
		toggleBinder = base.FindTransform("SubFormation").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubPanelFormation";
		toggleBinder = base.FindTransform("SubCombination").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubPanelCombination";
		toggleBinder = base.FindTransform("FnBtnLevel").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "FnBtnLevel";
		toggleBinder = base.FindTransform("FnBtnUpgrade").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "FnBtnUpgrade";
		toggleBinder = base.FindTransform("FnBtnSkillEvo").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "FnBtnSkillEvo";
		toggleBinder = base.FindTransform("ToggleAttribute").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubPanelSkillEvo";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ToggleNatural").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SubPanelNaturalEvo";
		toggleBinder.OffWhenDisable = false;
		ButtonBinder buttonBinder = base.FindTransform("MorphBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnMorphBtnUp";
		buttonBinder = base.FindTransform("SPBtnBuy").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnSPBtnBuyUp";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SPBtnBuyEnable";
		visibilityBinder.Target = base.FindTransform("SPBtnBuy").get_gameObject();
		ButtonCustomBinder buttonCustomBinder = base.FindTransform("SupportSkill").get_gameObject().AddComponent<ButtonCustomBinder>();
		buttonCustomBinder.BindingProxy = base.get_gameObject();
		buttonCustomBinder.OnDownBinding.MemberName = "OnSupportSkillBtnDown";
		buttonCustomBinder.OnUpBinding.MemberName = "OnSupportSkillBtnUp";
		buttonBinder = base.FindTransform("BtnLevel1").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnLevel1";
		buttonBinder.EnabledBinding.MemberName = "BtnLevelOn";
		buttonBinder = base.FindTransform("BtnLevel10").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnLevel10";
		buttonBinder.EnabledBinding.MemberName = "BtnLevelOn";
		buttonBinder = base.FindTransform("BtnUpgradeObtain").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUpgradeObtainUp";
		buttonBinder = base.FindTransform("BtnUpgrade").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUpgradeUp";
		buttonBinder.EnabledBinding.MemberName = "BtnUpgradeEnable";
		buttonBinder = base.FindTransform("BtnPreview").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnPreview";
		buttonBinder = base.FindTransform("SubPPIArrowL").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnPPIArrowL";
		buttonBinder = base.FindTransform("SubPPIArrowR").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnPPIArrowR";
		buttonBinder = base.FindTransform("BtnFollow").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnFollowUp";
		buttonBinder = base.FindTransform("BtnResetLevel").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnResetLevel";
		buttonBinder.EnabledBinding.MemberName = "BtnResetLevelOn";
	}

	public void SetRawImageModelLayer(bool playSkill, bool petShow, bool isActivation = false)
	{
		this.JustPlayActivation(false, isActivation);
		if (playSkill)
		{
			this.NodeBackground.SetAsLastSibling();
			if (this.actorRawImage != null)
			{
				this.actorRawImage.set_material(RTManager.Instance.RTNoAlphaMat);
			}
			if (isActivation)
			{
				this.SetPositionMiddle();
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
				this.JustPlayActivation(true, isActivation);
			}
		}
	}

	public void SetBack(bool isOut)
	{
		if (PetManager.Instance.IsFromLink)
		{
			isOut = true;
		}
		CurrenciesUIViewModel.Show(true);
		if (isOut)
		{
			CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110026), "BACK", delegate
			{
				if (PetBasicUIView.Instance != null)
				{
					PetBasicUIView.Instance.Show(false);
				}
				UIStackManager.Instance.PopUIPrevious(base.uiType);
			}, false);
		}
		else
		{
			CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110026), "BACK", delegate
			{
				ModelDisplayManager.Instance.DeleteModel();
				PetBasicUIViewModel.Instance.SubPanelPetInfo = false;
				PetBasicUIViewModel.Instance.SubPanelPets = true;
			}, false);
		}
	}

	public void ShowFighting(long fightingNow, bool anim, long fightingOld)
	{
		if (anim)
		{
			this.fx_fighting_id = FXSpineManager.Instance.PlaySpine(303, base.FindTransform("RootFighting"), "PetBasicUI", 2001, null, "UI", 100f, -20f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			ChangeNumAnim changeNumAnim = this.m_lblBattlePower.get_gameObject().AddMissingComponent<ChangeNumAnim>();
			changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_lblBattlePower, fightingOld, fightingNow, null, delegate(string resultStr)
			{
				this.m_lblBattlePower.set_text(PetBasicUIViewModel.Instance.GetFighting(resultStr));
			}, delegate
			{
				FXSpineManager.Instance.DeleteSpine(this.fx_fighting_id, true);
			});
		}
		else if (fightingNow > 0L)
		{
			this.m_lblBattlePower.set_text(PetBasicUIViewModel.Instance.GetFighting(fightingNow.ToString()));
		}
		else
		{
			this.m_lblBattlePower.set_text("战力: 激活可见");
		}
	}

	public void SetBackground(int type)
	{
		ResourceManager.SetCodeTexture(this.m_spBackgroundImage, PetManagerBase.GetBackground(type));
		ResourceManager.SetCodeTexture(this.m_spBackgroundImageMask, PetManagerBase.GetBackground(type));
	}

	private void animEndOfActivation()
	{
		this.JustPlayActivation(false, false);
		this.ShowPetBackground(false);
	}

	private void JustPlayActivation(bool play, bool isActivation = false)
	{
		Transform transform = base.FindTransform("SubPanelPetInfo");
		if (transform == null)
		{
			return;
		}
		Animator component = transform.GetComponent<Animator>();
		if (component != null)
		{
			component.set_enabled(play);
		}
		if (!play)
		{
			Transform transform2 = base.FindTransform("PanelDetailInfo");
			if (transform2 == null)
			{
				return;
			}
			(transform2 as RectTransform).set_anchoredPosition(Vector2.get_zero());
			base.FindTransform("SubPPIPanelL").GetComponent<CanvasGroup>().set_alpha(1f);
			this.SetPositionL(false, isActivation);
		}
		else
		{
			this.SetPositionL(true, isActivation);
		}
	}

	public void ShowPetBackground(bool isShow)
	{
		if (this.m_spBackgroundImage != null)
		{
			this.m_spBackgroundImage.set_enabled(isShow);
			RTSyncBackground component = this.m_spBackgroundImage.GetComponent<RTSyncBackground>();
			if (component != null)
			{
				component.Create();
			}
		}
	}

	private void InitPosition()
	{
		this.m_btpBackgroundImage = this.m_spBackgroundImage.get_gameObject().AddUniqueComponent<BaseTweenPostion>();
		this.m_btpRawImageModel = this.actorRawImage.get_gameObject().AddUniqueComponent<BaseTweenPostion>();
	}

	private void SetPositionL(bool move, bool isActivation = false)
	{
		if (move)
		{
			this.SetPositionMiddle();
			if (this.m_btpBackgroundImage != null)
			{
				this.m_btpBackgroundImage.MoveTo(new Vector3(this.m_spBackgroundImage.get_rectTransform().get_sizeDelta().x - 300f, 0f, 0f), 0.65f);
			}
			if (this.m_btpRawImageModel != null)
			{
				this.m_btpRawImageModel.MoveTo(new Vector3(-300f, 0f, 0f), 0.65f);
			}
		}
		else
		{
			if (isActivation)
			{
				return;
			}
			if (this.m_btpBackgroundImage != null)
			{
				this.m_btpBackgroundImage.Reset(false, false);
			}
			if (this.m_spBackgroundImage != null && this.actorRawImage != null)
			{
				this.m_spBackgroundImage.get_transform().set_localPosition(new Vector3(this.actorRawImage.get_rectTransform().get_sizeDelta().x - 300f, 0f, 0f));
			}
			if (this.m_btpRawImageModel != null)
			{
				this.m_btpRawImageModel.Reset(false, false);
			}
			if (this.m_btpRawImageModel != null)
			{
				this.m_btpRawImageModel.get_transform().set_localPosition(new Vector3(-300f, 0f, 0f));
			}
		}
	}

	public void SetPositionMiddle()
	{
		if (this.m_btpBackgroundImage != null)
		{
			this.m_btpBackgroundImage.Reset(false, false);
		}
		if (this.m_spBackgroundImage != null)
		{
			this.m_spBackgroundImage.get_transform().set_localPosition(new Vector3(this.m_spBackgroundImage.get_rectTransform().get_sizeDelta().x, 0f, 0f));
		}
		if (this.m_btpRawImageModel != null)
		{
			this.m_btpRawImageModel.Reset(false, false);
		}
		if (this.actorRawImage != null)
		{
			this.actorRawImage.get_transform().set_localPosition(Vector3.get_zero());
		}
	}

	public void RefreshFXOfUpgradeMax(bool arg)
	{
		if (arg)
		{
			this.fx_id_upgrademax = FXSpineManager.Instance.ReplaySpine(this.fx_id_upgrademax, 304, base.FindTransform("UpgradeMax"), "PetBasicUI", 2001, null, "UI", -2f, 95f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_id_upgrademax, true);
			this.fx_id_upgrademax = 0;
		}
	}
}
