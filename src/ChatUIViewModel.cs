using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ChannelWorldOn = "ChannelWorldOn";

		public const string Attr_ChannelGuildOn = "ChannelGuildOn";

		public const string Attr_ChannelPrivateOn = "ChannelPrivateOn";

		public const string Attr_ChannelSystemOn = "ChannelSystemOn";

		public const string Attr_ChannelTeamOn = "ChannelTeamOn";

		public const string Attr_ChannelTeamOrgOn = "ChannelTeamOrgOn";

		public const string Attr_SendContent = "SendContent";

		public const string Attr_ItemShows = "ItemShows";

		public const string Attr_BulletCurtainOn = "BulletCurtainOn";

		public const string Attr_BulletCurtainOnShow = "BulletCurtainOnShow";

		public const string Attr_ChannelTipOn = "ChannelTipOn";

		public const string Attr_ChannelTipOnShow = "ChannelTipOnShow";

		public const string Attr_ChannelMaskOn = "ChannelMaskOn";

		public const string Attr_ChannelMaskOnShow = "ChannelMaskOnShow";

		public const string Attr_BtnPrivateTalkVisibility = "BtnPrivateTalkVisibility";

		public const string Attr_BtnPrivateTalkName = "BtnPrivateTalkName";

		public const string Attr_ChatSendPlaceholder = "ChatSendPlaceholder";

		public const string Attr_ChatSendOn = "ChatSendOn";

		public const string Attr_ChannelPrivateBadge = "ChannelPrivateBadge";

		public const string Attr_IsSayOn = "IsSayOn";

		public const string Attr_BtnSayBg = "BtnSayBg";

		public const string Attr_ChatNoSendTip = "ChatNoSendTip";

		public const string Event_OnBtnShowUp = "OnBtnShowUp";

		public const string Event_OnBtnSendUp = "OnBtnSendUp";

		public const string Event_OnBtnSayIsPressed = "OnBtnSayIsPressed";

		public const string Event_OnBtnFaceUp = "OnBtnFaceUp";

		public const string Event_OnBtnPrivateTalkUp = "OnBtnPrivateTalkUp";

		public const string Event_OnBtnFriendUp = "OnBtnFriendUp";

		public const string Event_OnClickBtnSay = "OnClickBtnSay";

		public const string Event_OnClickBtnClose = "OnClickBtnClose";
	}

	private const int MAX_NUM_2_PRIVATE = 6;

	private const string NO_GUILD_TIP = "未加入军团";

	private static ChatUIViewModel m_instance;

	private int _CurrentChatChannel;

	public static List<ChatManager.PrivateTalk> PrivateTalks = new List<ChatManager.PrivateTalk>();

	private ChatManager.PrivateTalk CurrentPrivateTalk = new ChatManager.PrivateTalk();

	public static long GuildTalkUID = 0L;

	private bool _ChannelWorldOn = true;

	private bool _ChannelGuildOn;

	private bool _ChannelPrivateOn;

	private bool _ChannelPrivateBadge;

	private bool _ChannelSystemOn;

	private bool _ChannelTeamOn;

	private bool _ChannelTeamOrgOn;

	private string _SendContent;

	private bool _BulletCurtainOn;

	private bool _BulletCurtainOnShow;

	private bool _ChannelTipOn;

	private bool _ChannelTipOnShow;

	private bool _ChannelMaskOn;

	private bool _ChannelMaskOnShow;

	private bool _BtnPrivateTalkVisibility;

	private string _BtnPrivateTalkName;

	private string _ChatSendPlaceholder;

	private string _ChatNoSendTip;

	private bool _ChatSendOn;

	private bool _IsSayOn;

	private SpriteRenderer _BtnSayBg;

	public ObservableCollection<OOItem2Show> ItemShows = new ObservableCollection<OOItem2Show>();

	private List<int> ItemShowIds = new List<int>();

	public bool IsSayCancel;

	public bool IsSaySendOn;

	public static ChatUIViewModel Instance
	{
		get
		{
			return ChatUIViewModel.m_instance;
		}
	}

	public int CurrentChatChannel
	{
		get
		{
			return this._CurrentChatChannel;
		}
		set
		{
			if (this._CurrentChatChannel != value)
			{
				this.ClearInput();
			}
			this._CurrentChatChannel = value;
			this.BulletCurtainOn = BulletCurtainManager.Instance.CheckIsBulletCurtainOn(this.CurrentChatChannel);
			this.BulletCurtainOnShow = BulletCurtainManager.Instance.CheckIsBulletCurtainShow(this.CurrentChatChannel);
			this.ChannelTipOn = ChatManager.Instance.CheckIsChannelTipOn(this.CurrentChatChannel);
			this.ChannelTipOnShow = ChatManager.Instance.CheckIsChannelTipShow(this.CurrentChatChannel);
			this.ChannelMaskOn = ChatManager.Instance.CheckIsChannelMaskOn(this.CurrentChatChannel);
			this.ChannelMaskOnShow = ChatManager.Instance.CheckIsChannelMaskShow(this.CurrentChatChannel);
		}
	}

	public bool ChannelWorldOn
	{
		get
		{
			return this._ChannelWorldOn;
		}
		set
		{
			this._ChannelWorldOn = value;
			base.NotifyProperty("ChannelWorldOn", value);
			if (value)
			{
				this.ShowAsViwe2Model(1);
				this.ChannelSingleWorldSetting();
			}
		}
	}

	public bool ChannelGuildOn
	{
		get
		{
			return this._ChannelGuildOn;
		}
		set
		{
			this._ChannelGuildOn = value;
			base.NotifyProperty("ChannelGuildOn", value);
			if (value)
			{
				this.ShowAsViwe2Model(2);
				this.ChannelGuildSetting();
			}
		}
	}

	public bool ChannelPrivateOn
	{
		get
		{
			return this._ChannelPrivateOn;
		}
		set
		{
			this._ChannelPrivateOn = value;
			base.NotifyProperty("ChannelPrivateOn", value);
			if (value)
			{
				ChatManager.Instance.SetChannelPrivateTip(false);
				this.ShowAsViwe2Model(4);
				this.ChannelPrivateSettting();
			}
		}
	}

	public bool ChannelPrivateBadge
	{
		get
		{
			return this._ChannelPrivateBadge;
		}
		set
		{
			this._ChannelPrivateBadge = value;
			base.NotifyProperty("ChannelPrivateBadge", value);
		}
	}

	public bool ChannelSystemOn
	{
		get
		{
			return this._ChannelSystemOn;
		}
		set
		{
			this._ChannelSystemOn = value;
			base.NotifyProperty("ChannelSystemOn", value);
			if (value)
			{
				this.ShowAsViwe2Model(8);
				this.ChatSendOn = false;
				this.ChatNoSendTip = "系统消息频道，无法聊天";
			}
		}
	}

	public bool ChannelTeamOn
	{
		get
		{
			return this._ChannelTeamOn;
		}
		set
		{
			this._ChannelTeamOn = value;
			base.NotifyProperty("ChannelTeamOn", value);
			if (value)
			{
				this.ShowAsViwe2Model(32);
				this.ChannelTeamSetting();
			}
		}
	}

	public bool ChannelTeamOrgOn
	{
		get
		{
			return this._ChannelTeamOrgOn;
		}
		set
		{
			this._ChannelTeamOrgOn = value;
			base.NotifyProperty("ChannelTeamOrgOn", value);
			if (value)
			{
				this.ShowAsViwe2Model(128);
				this.ChatSendOn = false;
				this.ChatNoSendTip = "招募消息频道，无法聊天";
			}
		}
	}

	public string SendContent
	{
		get
		{
			return this._SendContent;
		}
		set
		{
			string text = ChatManager.FilterItemPlaceholder(value);
			if (ChatManager.Instance.CalLengthNoContainsFace(text) > ChatManager.MAX_NUM_2_CHAR)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502064, false));
				base.NotifyProperty("SendContent", this._SendContent);
			}
			else
			{
				this._SendContent = text;
				base.NotifyProperty("SendContent", this._SendContent);
				ChatUIView.Instance.SetInput(this._SendContent);
			}
		}
	}

	public bool BulletCurtainOn
	{
		get
		{
			return this._BulletCurtainOn;
		}
		set
		{
			this._BulletCurtainOn = value;
			base.NotifyProperty("BulletCurtainOn", value);
			BulletCurtainManager.Instance.SetBulletCurtainOn(this.CurrentChatChannel, value);
		}
	}

	public bool BulletCurtainOnShow
	{
		get
		{
			return this._BulletCurtainOnShow;
		}
		set
		{
			this._BulletCurtainOnShow = value;
			base.NotifyProperty("BulletCurtainOnShow", value);
		}
	}

	public bool ChannelTipOn
	{
		get
		{
			return this._ChannelTipOn;
		}
		set
		{
			this._ChannelTipOn = value;
			base.NotifyProperty("ChannelTipOn", value);
			ChatManager.Instance.SetChannelTipOn(this.CurrentChatChannel, value);
		}
	}

	public bool ChannelTipOnShow
	{
		get
		{
			return this._ChannelTipOnShow;
		}
		set
		{
			this._ChannelTipOnShow = value;
			base.NotifyProperty("ChannelTipOnShow", value);
		}
	}

	public bool ChannelMaskOn
	{
		get
		{
			return this._ChannelMaskOn;
		}
		set
		{
			this._ChannelMaskOn = value;
			base.NotifyProperty("ChannelMaskOn", value);
			ChatManager.Instance.SetChannelMaskOn(this.CurrentChatChannel, value);
		}
	}

	public bool ChannelMaskOnShow
	{
		get
		{
			return this._ChannelMaskOnShow;
		}
		set
		{
			this._ChannelMaskOnShow = value;
			base.NotifyProperty("ChannelMaskOnShow", value);
		}
	}

	public bool BtnPrivateTalkVisibility
	{
		get
		{
			return this._BtnPrivateTalkVisibility;
		}
		set
		{
			this._BtnPrivateTalkVisibility = false;
			base.NotifyProperty("BtnPrivateTalkVisibility", this._BtnPrivateTalkVisibility);
		}
	}

	public string BtnPrivateTalkName
	{
		get
		{
			return this._BtnPrivateTalkName;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				this._BtnPrivateTalkName = value;
			}
			else
			{
				this._BtnPrivateTalkName = "NONE";
			}
			base.NotifyProperty("BtnPrivateTalkName", value);
		}
	}

	public string ChatSendPlaceholder
	{
		get
		{
			return this._ChatSendPlaceholder;
		}
		set
		{
			this._ChatSendPlaceholder = value;
			base.NotifyProperty("ChatSendPlaceholder", value);
		}
	}

	public string ChatNoSendTip
	{
		get
		{
			return this._ChatNoSendTip;
		}
		set
		{
			this._ChatNoSendTip = value;
			base.NotifyProperty("ChatNoSendTip", value);
		}
	}

	public bool ChatSendOn
	{
		get
		{
			return this._ChatSendOn;
		}
		set
		{
			this._ChatSendOn = value;
			base.NotifyProperty("ChatSendOn", value);
		}
	}

	public bool IsSayOn
	{
		get
		{
			return this._IsSayOn;
		}
		set
		{
			this._IsSayOn = value;
			base.NotifyProperty("IsSayOn", value);
			if (value)
			{
				this.BtnSayBg = ResourceManager.GetIconSprite("yuyin_2");
			}
			else
			{
				this.BtnSayBg = ResourceManager.GetIconSprite("yuyin");
			}
		}
	}

	public SpriteRenderer BtnSayBg
	{
		get
		{
			return this._BtnSayBg;
		}
		set
		{
			this._BtnSayBg = value;
			base.NotifyProperty("BtnSayBg", value);
		}
	}

	private void SetCurrentChatChannel()
	{
		this.CurrentChatChannel = this.CurrentChatChannel;
		int currentChatChannel = this.CurrentChatChannel;
		switch (currentChatChannel)
		{
		case 1:
			this.ChannelWorldOn = true;
			return;
		case 2:
			this.ChannelGuildOn = true;
			return;
		case 3:
		case 5:
		case 6:
		case 7:
			IL_3B:
			if (currentChatChannel == 32)
			{
				this.ChannelTeamOn = true;
				return;
			}
			if (currentChatChannel != 128)
			{
				return;
			}
			this.ChannelTeamOrgOn = true;
			return;
		case 4:
			this.ChannelPrivateOn = true;
			return;
		case 8:
			this.ChannelSystemOn = true;
			return;
		}
		goto IL_3B;
	}

	private void ChannelSingleWorldSetting()
	{
		if (EntityWorld.Instance.EntSelf.Lv >= ChatManager.Instance.SingleWorldTalkLv)
		{
			this.ChatSendOn = true;
			this.ChatSendPlaceholder = GameDataUtils.GetChineseContent(502068, false);
		}
		else
		{
			this.ChatSendOn = false;
			this.ChatNoSendTip = string.Format(GameDataUtils.GetChineseContent(505417, false), ChatManager.Instance.SingleWorldTalkLv);
		}
	}

	private void ChannelPrivateSettting()
	{
		this.ChatSendOn = true;
		this.BtnPrivateTalkVisibility = false;
		if (this.CurrentChatChannel == 4)
		{
			this.BtnPrivateTalkVisibility = true;
			this.SetCurrentPrivateTalk(this.CurrentPrivateTalk.PrivateTalkUID, this.CurrentPrivateTalk.PrivateTalkName, true);
		}
		else
		{
			this.SetCurrentPrivateTalk(0L, string.Empty, true);
		}
	}

	private static void Add2PrivateTalks(ChatManager.PrivateTalk srcPriavteTalk)
	{
		for (int i = 0; i < ChatUIViewModel.PrivateTalks.get_Count(); i++)
		{
			if (ChatUIViewModel.PrivateTalks.get_Item(i).PrivateTalkUID == srcPriavteTalk.PrivateTalkUID)
			{
				return;
			}
		}
		if (ChatUIViewModel.PrivateTalks.get_Count() >= 6)
		{
			ChatUIViewModel.PrivateTalks.RemoveAt(0);
		}
		ChatManager.PrivateTalk privateTalk = new ChatManager.PrivateTalk();
		privateTalk.Clone(srcPriavteTalk);
		ChatUIViewModel.PrivateTalks.Add(privateTalk);
	}

	public void SetCurrentPrivateTalk(long id, string name, bool bReset = false)
	{
		if (!bReset)
		{
			this.CurrentPrivateTalk.Set(id, name);
			ChatUIViewModel.Add2PrivateTalks(this.CurrentPrivateTalk);
			this.ChatSendPlaceholder = "对" + TextColorMgr.GetColor(name, "F87D04", string.Empty) + "说：";
		}
		else
		{
			this.CurrentPrivateTalk.ResetAll();
			if (this.CurrentChatChannel == 4)
			{
				this.ChatSendPlaceholder = "请选择私聊对象";
			}
		}
		if (ChatUIViewModel.Instance != null)
		{
			ChatUIViewModel.Instance.BtnPrivateTalkName = this.CurrentPrivateTalk.PrivateTalkName;
		}
	}

	private void ChannelTeamSetting()
	{
		if (TeamBasicManager.Instance.IsHaveTeam())
		{
			this.ChatSendOn = true;
			this.ChatSendPlaceholder = GameDataUtils.GetChineseContent(502068, false);
		}
		else
		{
			this.ChatSendOn = false;
			this.ChatNoSendTip = "当前没有队伍, 无法聊天";
		}
	}

	private void ChannelGuildSetting()
	{
		if (GuildManager.Instance.IsJoinInGuild())
		{
			this.ChatSendOn = true;
			this.ChatSendPlaceholder = GameDataUtils.GetChineseContent(502068, false);
		}
		else
		{
			this.ChatSendOn = false;
			this.ChatNoSendTip = "未加入军团, 无法聊天";
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ChatUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
		ChatTipUIViewModel.Close();
		this.CurrentChatChannel = 1;
		ChatManager.Instance.CheckPrivateBadge();
		this.IsSayOn = false;
	}

	private void OnDisable()
	{
		if (ChatTalkTipUIView.Instance != null)
		{
			ChatTalkTipUIView.Instance.Show(false);
		}
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			ChatTipUIViewModel.Open(UINodesManager.NormalUIRoot, true);
		}
		else if (TownUI.Instance != null)
		{
			ChatTipUIViewModel.Open(TownUI.Instance.m_ChatTipUIRoot, true);
		}
		this.SendContent = string.Empty;
	}

	public void OnBtnShowUp()
	{
		Debuger.Info("OnBtnShowUp", new object[0]);
		ChatUIView.Instance.Node2RevealPackUI.SetAsLastSibling();
		UIManagerControl.Instance.OpenUI("RevealPackUI", ChatUIView.Instance.Node2RevealPackUI, false, UIType.NonPush);
	}

	public void OnBtnSendUp()
	{
		if (string.IsNullOrEmpty(this.SendContent))
		{
			return;
		}
		if (this.SendContent.StartsWith(ChatManager.GM_PREFIX_SERVER))
		{
			if (ClientGMManager.Instance.IsPermissionOn(this.CurrentChatChannel))
			{
				ChatManager.Instance.SendGMCommand(0, this.SendContent);
				this.SendContent = string.Empty;
				return;
			}
		}
		else if (this.SendContent.StartsWith(ChatManager.GM_PREFIX_CLIENT))
		{
			string text = this.SendContent.Substring(ChatManager.GM_PREFIX_CLIENT.get_Length());
			if (text.StartsWith("roll"))
			{
				string[] array = text.Replace("roll", string.Empty).Split(new char[]
				{
					' '
				});
				if (array.Length >= 2)
				{
					int num;
					bool flag = int.TryParse(array[1], ref num);
					if (flag)
					{
						num = Random.Range(1, num);
						ChatManager.Instance.Send(1, "roll " + num, null, 0L);
					}
				}
				this.SendContent = string.Empty;
				return;
			}
			if (!ClientGMManager.Instance.IsPermissionOn(this.CurrentChatChannel))
			{
				if (this.CurrentChatChannel == 4 && text == ClientGMManager.PermissionCode)
				{
					ClientGMManager.IsPermission = true;
					this.SendContent = string.Empty;
					return;
				}
			}
			else
			{
				if (text.StartsWith("debug"))
				{
					ClientGMManager.Instance.GetGMResult("debug on");
					SystemConfig.IsDebugInfoOn = true;
					ClientApp.Instance.get_gameObject().AddMissingComponent<DebugInfoUIViewManager>();
					this.SendContent = string.Empty;
					return;
				}
				ClientGMManager.Instance.GetGMResult(text);
				this.SendContent = string.Empty;
				return;
			}
		}
		if (this.JustSendMessageOfText())
		{
			this.ClearInput();
		}
	}

	public void OnClickBtnSay()
	{
		this.IsSayOn = !this.IsSayOn;
	}

	public void OnBtnSayIsPressed(bool isPressed)
	{
		if (!NativeCallManager.Native_CheckPermissionRecordAudio())
		{
			return;
		}
		if (isPressed)
		{
			this.IsSayCancel = false;
			this.IsSaySendOn = true;
			UIManagerControl.Instance.OpenUI("ChatTalkTipUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
		else
		{
			this.SendMessageOfVoice();
		}
	}

	public void OnBtnFaceUp()
	{
		Debuger.Info("OnBtnFaceUp", new object[0]);
		ChatUIView.Instance.Node2FaceUI.SetAsLastSibling();
		UIManagerControl.Instance.OpenUI("FaceUI", ChatUIView.Instance.Node2FaceUI, false, UIType.NonPush);
	}

	public void OnBtnPrivateTalkUp()
	{
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		for (int i = 0; i < ChatUIViewModel.PrivateTalks.get_Count(); i++)
		{
			ChatManager.PrivateTalk privateTalk = ChatUIViewModel.PrivateTalks.get_Item(i);
			list.Add(new ButtonInfoData
			{
				buttonName = privateTalk.PrivateTalkName,
				color = "button_yellow_1",
				onCall = delegate
				{
					long privateTalkUID = privateTalk.PrivateTalkUID;
					string privateTalkName = privateTalk.PrivateTalkName;
					this.SetCurrentPrivateTalk(privateTalkUID, privateTalkName, false);
					PopButtonsUIViewModel.Instance.Close();
				}
			});
		}
		if (list.get_Count() > 0)
		{
			ChatUIView.Instance.Node2PrivatesUI.SetAsLastSibling();
			PopButtonsUIViewModel.Open(ChatUIView.Instance.Node2PrivatesUI);
			PopButtonsUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	public void OnBtnFriendUp()
	{
		LinkNavigationManager.OpenFriendUI();
	}

	public void OnClickBtnClose()
	{
		ChatUIView.Instance.Show(false);
	}

	public void ShowAsViwe2Model(int channel)
	{
		this.CurrentChatChannel = channel;
		this.RefreshCurrentChatChannel();
	}

	public void ShowAsModel2View(int channel)
	{
		this.CurrentChatChannel = channel;
		this.SetCurrentChatChannel();
		this.RefreshCurrentChatChannel();
		if (channel == 4)
		{
		}
	}

	public void RefreshCurrentChatChannel()
	{
		this.AddNews2Chats(this.CurrentChatChannel);
	}

	private void RefreshItemShows()
	{
		this.ItemShows.Clear();
		for (int i = 0; i < this.ItemShowIds.get_Count(); i++)
		{
			Items items = DataReader<Items>.Get(this.ItemShowIds.get_Item(i));
			if (items != null)
			{
				OOItem2Show oOItem2Show = new OOItem2Show();
				oOItem2Show.id = items.id;
				oOItem2Show.Frame = GameDataUtils.GetItemFrame(items.id);
				oOItem2Show.Icon = GameDataUtils.GetIcon(items.icon);
				this.ItemShows.Add(oOItem2Show);
			}
		}
	}

	public void Add2ItemShows(int id)
	{
		if (this.IsItemInShow(id))
		{
			return;
		}
		this.ItemShowIds.Add(id);
		this.RefreshItemShows();
	}

	public void Remove4ItemShows(int id)
	{
		if (this.IsItemInShow(id))
		{
			this.ItemShowIds.Remove(id);
			this.RefreshItemShows();
		}
	}

	public bool IsItemInShow(int id)
	{
		return this.ItemShowIds.Contains(id);
	}

	private void Update()
	{
		if (Input.GetKeyUp(13))
		{
			this.OnBtnSendUp();
		}
	}

	private bool JustSendMessageOfText()
	{
		if (!this.CheckChannelIsSendOn())
		{
			return false;
		}
		if (ChatManager.Instance.CalLengthNoContainsFace(this.SendContent) > ChatManager.MAX_NUM_2_CHAR)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502064, false));
			return false;
		}
		List<DetailInfo> list = new List<DetailInfo>();
		for (int i = 0; i < this.ItemShowIds.get_Count(); i++)
		{
			list.Add(new DetailInfo
			{
				type = DetailType.DT.Equipment,
				cfgId = this.ItemShowIds.get_Item(i)
			});
		}
		long targetByChannel = this.GetTargetByChannel();
		ChatManager.Instance.Send(this.CurrentChatChannel, this.SendContent, list, targetByChannel);
		return true;
	}

	public void SendMessageOfVoice()
	{
		if (ChatTalkTipUIView.Instance != null)
		{
			ChatTalkTipUIView.Instance.Show(false);
		}
		if (this.IsSayCancel)
		{
			return;
		}
		if (!this.IsSaySendOn)
		{
			return;
		}
		this.IsSaySendOn = false;
		VoiceSDKManager.Instance.SpeechRecordStop(delegate
		{
			this.OnRecordFinished();
		});
	}

	private void OnRecordFinished()
	{
		Debug.Log("OnRecordFinished");
		byte[] speech = VoiceSDKManager.Instance.GetSpeech();
		if (speech == null)
		{
			UIManagerControl.Instance.ShowToastText("录音数据获取失败");
			return;
		}
		Debug.Log("***record length = " + speech.Length);
		int second = ChatTalkTipUIView.GetSecond();
		if (second < 1)
		{
			UIManagerControl.Instance.ShowToastText("录音时间过短");
			return;
		}
		this.JustSendMessageOfVoice(speech, second);
	}

	private void JustSendMessageOfVoice(byte[] audio, int record_second)
	{
		if (!this.CheckChannelIsSendOn())
		{
			return;
		}
		long targetByChannel = this.GetTargetByChannel();
		DetailInfo detailInfo = new DetailInfo();
		detailInfo.type = DetailType.DT.Audio;
		detailInfo.audio = audio;
		detailInfo.label = record_second.ToString();
		List<DetailInfo> list = new List<DetailInfo>();
		list.Add(detailInfo);
		ChatManager.Instance.Send(this.CurrentChatChannel, string.Empty, list, targetByChannel);
	}

	private void AddNews2Chats(int channel)
	{
		List<ChatManager.ChatInfo> list = ChatManager.Instance.ChatNews.get_Item(channel);
		for (int i = 0; i < list.get_Count(); i++)
		{
			ChatManager.ChatInfo chatInfo = list.get_Item(i);
			ChatUIView.Instance.AddChatNew(channel, chatInfo);
			List<ChatManager.ChatInfo> list2 = ChatManager.Instance.Chats.get_Item(channel);
			if (list2.get_Count() > 0 && list2.get_Count() >= ChatManager.MAX_CHAT_SHOWNUM)
			{
				list2.RemoveAt(0);
			}
			list2.Add(chatInfo);
		}
		ChatManager.Instance.ChatNews.get_Item(channel).Clear();
	}

	private long GetTargetByChannel()
	{
		long result = 0L;
		if (this.CurrentChatChannel == 4)
		{
			result = this.CurrentPrivateTalk.PrivateTalkUID;
		}
		else if (this.CurrentChatChannel == 2)
		{
			result = ChatUIViewModel.GuildTalkUID;
		}
		return result;
	}

	private void ClearInput()
	{
		this.ItemShows.Clear();
		this.SendContent = string.Empty;
	}

	private bool CheckChannelIsSendOn()
	{
		if (this.CurrentChatChannel == 1 && EntityWorld.Instance.EntSelf.Lv < ChatManager.Instance.SingleWorldTalkLv)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(505417, false), ChatManager.Instance.SingleWorldTalkLv);
			UIManagerControl.Instance.ShowToastText(text);
			return false;
		}
		if (this.CurrentChatChannel == 8)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502065, false));
			return false;
		}
		if (this.CurrentChatChannel == 2 && !GuildManager.Instance.IsJoinInGuild())
		{
			UIManagerControl.Instance.ShowToastText("未加入军团");
			return false;
		}
		if (this.CurrentChatChannel == 4 && this.CurrentPrivateTalk.PrivateTalkUID <= 0L)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502060, false));
			return false;
		}
		if (this.CurrentChatChannel == 32 && !TeamBasicManager.Instance.IsHaveTeam())
		{
			UIManagerControl.Instance.ShowToastText("当前没有队伍");
			return false;
		}
		return true;
	}
}
