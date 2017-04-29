using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class SettingUIView : UIBase
{
	public const string HE_SE = "4B3C32";

	public const string HEHONG_SE = "78503C";

	public const string HE_SE2 = "FFFFFF";

	public static UIBase Instance;

	private List<SettingToggle> mSettingToggles = new List<SettingToggle>();

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		SettingUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.InitPushToggle();
		base.FindTransform("ButtonSwtich").GetComponent<ButtonCustom>().get_onClick().AddListener(new UnityAction(this.OnClickButtonSwitch));
		base.FindTransform("ButtonGiftExchange").GetComponent<ButtonCustom>().get_onClick().AddListener(new UnityAction(this.OnClickButtonGiftExchange));
		base.FindTransform("ButtonCustomerService").GetComponent<ButtonCustom>().get_onClick().AddListener(new UnityAction(this.OnClickButtonCustomerService));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.SavePushSettings();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			SettingUIView.Instance = null;
			SettingUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickButtonSwitch()
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("切换账号", "是否注销当前角色", null, delegate
		{
			ClientApp.Instance.ReInit();
			SDKManager.Instance.Logout();
		}, "取消", "确认", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickButtonGiftExchange()
	{
		LinkNavigationManager.OpenGiftExchangeUI();
	}

	private void OnClickButtonCustomerService()
	{
		SDKManager.Instance.OpenKF();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("GuideOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("新手指引开关", "4B3C32", string.Empty));
		base.FindTransform("PostProcessOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("画面后期特效:", "78503C", string.Empty));
		base.FindTransform("PNName").GetComponent<Text>().set_text(TextColorMgr.GetColor("主城玩家显示数量", "4B3C32", string.Empty));
		base.FindTransform("ResolutionSettingName").GetComponent<Text>().set_text(TextColorMgr.GetColor("分辨率设置:", "78503C", string.Empty));
		base.FindTransform("ResolutionOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("不开启", "78503C", string.Empty));
		base.FindTransform("ResolutionOn1Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("1920", "FFFFFF", string.Empty));
		base.FindTransform("ResolutionOn2Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("1280", "FFFFFF", string.Empty));
		base.FindTransform("ResolutionOn3Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("960", "FFFFFF", string.Empty));
		base.FindTransform("AASettingName").GetComponent<Text>().set_text(TextColorMgr.GetColor("抗锯齿级别", "4B3C32", string.Empty));
		base.FindTransform("AAOn1Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("0", "78503C", string.Empty));
		base.FindTransform("AAOn2Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("2", "78503C", string.Empty));
		base.FindTransform("AAOn3Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("4", "78503C", string.Empty));
		base.FindTransform("AAOn4Text").GetComponent<Text>().set_text(TextColorMgr.GetColor("8", "78503C", string.Empty));
		base.FindTransform("MusicOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("游戏声音", "78503C", string.Empty));
		base.FindTransform("SoundOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("游戏音效", "78503C", string.Empty));
		base.FindTransform("ManNumOnText").GetComponent<Text>().set_text(TextColorMgr.GetColor("显示主城玩家", "78503C", string.Empty));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ToggleBinder toggleBinder = base.FindTransform("GuideOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsGuideOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("PostProcessOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsPostProcessOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("PP_MotionBlurOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "PP_MotionBlurOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("PP_BloomOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "PP_BloomOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ResolutionOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsResolutionOff";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ResolutionOn1").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ResolutionOn1";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ResolutionOn2").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ResolutionOn2";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ResolutionOn3").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ResolutionOn3";
		toggleBinder.OffWhenDisable = false;
		SliderBinder sliderBinder = base.FindTransform("PNSlider").get_gameObject().AddComponent<SliderBinder>();
		sliderBinder.BindingProxy = base.get_gameObject();
		sliderBinder.ValueBinding.MemberName = "PeopleNumAmount";
		TextBinder textBinder = base.FindTransform("PNNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PeopleNum";
		toggleBinder = base.FindTransform("ManNumOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsManNumOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("LOD1").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsLOD1On";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("LOD2").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsLOD2On";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("LOD3").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsLOD3On";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("AAOn1").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "AAOn0";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("AAOn2").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "AAOn2";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("AAOn3").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "AAOn4";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("AAOn4").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "AAOn8";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("MusicOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsMusicOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("SoundOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsSoundOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("HeadInfoOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsHeadInfoOn";
		toggleBinder.OffWhenDisable = false;
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnClose").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnBtnClose";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SystemSetting01On";
		visibilityBinder.Target = base.FindTransform("SystemSetting01").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "PushSettingOn";
		visibilityBinder.Target = base.FindTransform("PushSetting").get_gameObject();
		ToggleBinder toggleBinder = base.FindTransform("BTSystemSetting01").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "SystemSetting01On";
		toggleBinder = base.FindTransform("BTPushSetting").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "PushSettingOn";
	}

	public void InitPushToggle()
	{
		for (int i = 1; i <= 4; i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("SettingToggle");
			UGUITools.SetParent(base.FindTransform("PushOn0" + i).get_gameObject(), instantiate2Prefab, true);
			this.mSettingToggles.Add(instantiate2Prefab.GetComponent<SettingToggle>());
		}
		this.SetPushToggle();
	}

	private void SetPushToggle()
	{
		int num = -1;
		num++;
		this.mSettingToggles.get_Item(num).SetName("蘑菇庄园");
		this.mSettingToggles.get_Item(num).SetSettingType(7);
		this.mSettingToggles.get_Item(num).SetToggle(PushNotificationManager.Instance.GetItemSetting(7));
		num++;
		this.mSettingToggles.get_Item(num).SetName("离线挂机");
		this.mSettingToggles.get_Item(num).SetSettingType(1);
		this.mSettingToggles.get_Item(num).SetToggle(PushNotificationManager.Instance.GetItemSetting(1));
		num++;
		this.mSettingToggles.get_Item(num).SetName("军团活动");
		this.mSettingToggles.get_Item(num).SetSettingType(4);
		this.mSettingToggles.get_Item(num).SetToggle(PushNotificationManager.Instance.GetItemSetting(4));
		num++;
		this.mSettingToggles.get_Item(num).SetName("军团动态");
		this.mSettingToggles.get_Item(num).SetSettingType(3);
		this.mSettingToggles.get_Item(num).SetToggle(PushNotificationManager.Instance.GetItemSetting(3));
	}

	private void SavePushSettings()
	{
		for (int i = 0; i < this.mSettingToggles.get_Count(); i++)
		{
			PushNotificationManager.Instance.SetItemSetting(this.mSettingToggles.get_Item(i).m_pushId, this.mSettingToggles.get_Item(i).IsOn);
		}
		PushNotificationManager.Instance.SendSetMessagePush();
	}
}
