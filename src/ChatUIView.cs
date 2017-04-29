using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class ChatUIView : UIBase
{
	private const string ChatChannelViewPrefabName = "ChatChannelView";

	public static ChatUIView Instance;

	[HideInInspector]
	public Transform Node2FaceUI;

	[HideInInspector]
	public Transform Node2RevealPackUI;

	[HideInInspector]
	public Transform Node2PrivatesUI;

	private Dictionary<int, ChatChannelView> m_ChatChannelViews = new Dictionary<int, ChatChannelView>();

	[HideInInspector]
	public ChatInfo2Input m_ChatInputUnit;

	private void SetPos(RectTransform rTransform)
	{
		rTransform.set_sizeDelta(new Vector2(424f, 0f));
		rTransform.set_anchoredPosition(Vector2.get_zero());
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0f;
		this.isClick = true;
		this.isInterruptStick = true;
		this.isEndNav = false;
	}

	private void Awake()
	{
		ChatUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("BtnSay").get_gameObject().SetActive(SystemConfig.IsVoiceTalkOn);
	}

	private void Start()
	{
		using (Dictionary<int, ChatChannelView>.ValueCollection.Enumerator enumerator = this.m_ChatChannelViews.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ChatChannelView current = enumerator.get_Current();
				if (current.get_gameObject() != null)
				{
					this.SetPos(current.get_gameObject().GetComponent<RectTransform>());
				}
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		UIManagerControl.Instance.HideUI("TaskDescUI");
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("ChannelAllText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502010, false));
		base.FindTransform("ChannelAllCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502010, false));
		base.FindTransform("ChannelGuildText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502011, false));
		base.FindTransform("ChannelGuildCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502011, false));
		base.FindTransform("ChannelPrivateText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502012, false));
		base.FindTransform("ChannelPrivateCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502012, false));
		base.FindTransform("ChannelSystemText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502013, false));
		base.FindTransform("ChannelSystemCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502013, false));
		base.FindTransform("ChannelTeamText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502085, false));
		base.FindTransform("ChannelTeamCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502085, false));
		base.FindTransform("ChannelTeamOrgText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505418, false));
		base.FindTransform("ChannelTeamOrgCmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505418, false));
		base.FindTransform("BulletCurtainOnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502067, false));
		base.FindTransform("BtnSendText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502015, false));
		base.FindTransform("ChatSendPlaceholder").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502068, false));
		this.Node2FaceUI = base.FindTransform("Node2FaceUI");
		this.Node2RevealPackUI = base.FindTransform("Node2RevealPackUI");
		this.Node2PrivatesUI = base.FindTransform("Node2PrivatesUI");
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ChatInfo2Input");
		UGUITools.SetParent(base.FindTransform("Node2ChatInfo2Input").get_gameObject(), instantiate2Prefab, false, "ChatInputUnit");
		this.m_ChatInputUnit = instantiate2Prefab.AddUniqueComponent<ChatInfo2Input>();
		GameObject gameObject = base.FindTransform("Region2ChannelView").get_gameObject();
		GameObject instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2World");
		this.m_ChatChannelViews.set_Item(1, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2Guild");
		this.m_ChatChannelViews.set_Item(2, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2Private");
		this.m_ChatChannelViews.set_Item(4, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2System");
		this.m_ChatChannelViews.set_Item(8, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2Team");
		this.m_ChatChannelViews.set_Item(32, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab("ChatChannelView");
		UGUITools.SetParent(gameObject, instantiate2Prefab2, false, "CCV2TeamOrg");
		this.m_ChatChannelViews.set_Item(128, instantiate2Prefab2.AddUniqueComponent<ChatChannelView>());
		base.FindTransform("Items2ShowScroll").SetAsLastSibling();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelWorldOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(1).get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelGuildOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(2).get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelPrivateOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(4).get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelSystemOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(8).get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelTeamOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(32).get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelTeamOrgOn";
		visibilityBinder.Target = this.m_ChatChannelViews.get_Item(128).get_gameObject();
		ImageBinder imageBinder = base.FindTransform("BtnSayBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "BtnSayBg";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnPrivateTalkVisibility";
		visibilityBinder.Target = base.FindTransform("BtnPrivateTalk").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChatSendOn";
		visibilityBinder.Target = base.FindTransform("ChatSend").get_gameObject();
		visibilityBinder.InverseTarget = base.FindTransform("ChatNoSend").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsSayOn";
		visibilityBinder.Target = base.FindTransform("RegionSendSay").get_gameObject();
		visibilityBinder.InverseTarget = base.FindTransform("RegionSendText").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelPrivateBadge";
		visibilityBinder.Target = base.FindTransform("ChannelPrivateBadge").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BulletCurtainOnShow";
		visibilityBinder.Target = base.FindTransform("BulletCurtainOn").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelTipOnShow";
		visibilityBinder.Target = base.FindTransform("ChannelTipOn").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ChannelMaskOnShow";
		visibilityBinder.Target = base.FindTransform("ChannelMaskOn").get_gameObject();
		InputFieldCustomBinder inputFieldCustomBinder = base.FindTransform("ChatSendInput").get_gameObject().AddComponent<InputFieldCustomBinder>();
		inputFieldCustomBinder.BindingProxy = base.get_gameObject();
		inputFieldCustomBinder.TextBinding.MemberName = "SendContent";
		ListBinder listBinder = base.FindTransform("Items2Show").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2Show";
		listBinder.SourceBinding.MemberName = "ItemShows";
		TextBinder textBinder = base.FindTransform("BtnPrivateTalkText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnPrivateTalkName";
		textBinder = base.FindTransform("ChatSendPlaceholder").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ChatSendPlaceholder";
		textBinder = base.FindTransform("ChatNoSendTip").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ChatNoSendTip";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ToggleBinder toggleBinder = base.FindTransform("ChannelAll").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelWorldOn";
		toggleBinder = base.FindTransform("ChannelGuild").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelGuildOn";
		toggleBinder = base.FindTransform("ChannelPrivate").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelPrivateOn";
		toggleBinder = base.FindTransform("ChannelSystem").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelSystemOn";
		toggleBinder = base.FindTransform("ChannelTeam").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelTeamOn";
		toggleBinder = base.FindTransform("ChannelTeamOrg").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelTeamOrgOn";
		toggleBinder = base.FindTransform("BulletCurtainOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "BulletCurtainOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ChannelTipOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelTipOn";
		toggleBinder.OffWhenDisable = false;
		toggleBinder = base.FindTransform("ChannelMaskOn").get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "ChannelMaskOn";
		toggleBinder.OffWhenDisable = false;
		ButtonBinder buttonBinder = base.FindTransform("BtnShow").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnShowUp";
		buttonBinder = base.FindTransform("BtnSend").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnSendUp";
		buttonBinder = base.FindTransform("BtnFace").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnFaceUp";
		buttonBinder = base.FindTransform("BtnPrivateTalk").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnPrivateTalkUp";
		buttonBinder = base.FindTransform("BtnFriend").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnFriendUp";
		buttonBinder = base.FindTransform("BtnSay").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickBtnSay";
		buttonBinder = base.FindTransform("BtnClose").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickBtnClose";
		ButtonValueBinder buttonValueBinder = base.FindTransform("RegionSendSay").get_gameObject().AddComponent<ButtonValueBinder>();
		buttonValueBinder.BindingProxy = base.get_gameObject();
		buttonValueBinder.IsPressedBinding.MemberName = "OnBtnSayIsPressed";
	}

	public void AddChatNew(int dstChannel, ChatManager.ChatInfo chatInfo)
	{
		this.m_ChatChannelViews.get_Item(dstChannel).AddChat2Channel(chatInfo);
	}

	public void SetInput(string content)
	{
		if (this.m_ChatInputUnit != null)
		{
			this.m_ChatInputUnit.ShowInfo(content);
		}
	}

	public void Release()
	{
		using (Dictionary<int, ChatChannelView>.ValueCollection.Enumerator enumerator = this.m_ChatChannelViews.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ChatChannelView current = enumerator.get_Current();
				current.ClearAll();
			}
		}
	}
}
