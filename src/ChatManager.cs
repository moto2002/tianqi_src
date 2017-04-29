using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNetwork;

public class ChatManager : BaseSubSystemManager
{
	public class ChatColorID
	{
		public const int TITLE_WORLD = 201;

		public const int CONTENT_WORLD = 205;

		public const int TITLE_PRIVATE = 202;

		public const int CONTENT_PRIVATE = 206;

		public const int TITLE_GUILD = 203;

		public const int CONTENT_GUILD = 207;

		public const int TITLE_SYSTEM = 204;

		public const int CONTENT_SYSTEM = 208;

		public const int TITLE_TEAM = 212;

		public const int TITLE_TEAM_ORG = 214;

		public const int CONTENT_TEAM = 213;

		public const int CONTENT_TEAM_ORG = 215;

		public const int SENDER = 209;

		public const int TITLE_BROADCAST_01 = 210;

		public const int TITLE_BROADCAST_02 = 211;
	}

	public enum ModuleTag
	{
		TeamInvite = 101
	}

	public class EventNames
	{
		public const string TeamMessage = "ChatManager.TeamMessage";

		public const string QuestionFunIconState = "ChatManager.QuestionFunIconState";
	}

	public class ChatInfo
	{
		public DetailType.DT chat_type;

		public string sender_name;

		public long sender_uid;

		public int src_channel;

		public string chat_content;

		public int sender_occupation;

		public int viplevel;

		public string time;

		public int module;

		public List<DetailInfo> items = new List<DetailInfo>();
	}

	public class PrivateTalk
	{
		private long _PrivateTalkUID;

		private string _PrivateTalkName;

		public long PrivateTalkUID
		{
			get
			{
				return this._PrivateTalkUID;
			}
			protected set
			{
				this._PrivateTalkUID = value;
			}
		}

		public string PrivateTalkName
		{
			get
			{
				return this._PrivateTalkName;
			}
			protected set
			{
				this._PrivateTalkName = value;
			}
		}

		public void Set(long id, string name)
		{
			this.PrivateTalkUID = id;
			this.PrivateTalkName = name;
		}

		public void ResetAll()
		{
			this.PrivateTalkUID = 0L;
			this.PrivateTalkName = string.Empty;
		}

		public void Clone(ChatManager.PrivateTalk src)
		{
			this.PrivateTalkUID = src.PrivateTalkUID;
			this.PrivateTalkName = src.PrivateTalkName;
		}
	}

	public class FaceSuit
	{
		public int num;

		public List<string> icons = new List<string>();
	}

	public struct LimitTalk
	{
		public int time;

		public string content;
	}

	public const string TEAＭ_RECRUITEMENT_FLAG = "@#@TeamInvite";

	public const int SYSTEM_VIP = -1;

	public const int SYSTEM_UID = -1;

	public const string GUILD_ICON = "icon_laba";

	public const string SYSTEM_ICON = "icon_laba";

	public const string BROADCAST_ICON = "icon_laba";

	private const int JUMP_OFFSET_LENGTH = 4;

	private const int JUMP_OFFSET_LENGTH_MAX = 8;

	public const string FACE_STRING_FORMAT = "D2";

	private const string CHANNEL_TIP_TAG = "CHANNEL_TIP_TAG";

	private const string CHANNEL_MASK_TAG = "CHANNEL_MASK_TAG";

	private const uint TalkCD = 2000u;

	public static readonly int MAX_NUM_2_ITEM = 5;

	public static readonly int MAX_NUM_2_FACE = 5;

	public static readonly int MAX_NUM_2_CHAR = 50;

	public static readonly int MAX_CHAT_SHOWNUM = 100;

	public static readonly string FacePlaceholder = "/f";

	public static readonly string ItemPlaceholder = "#{}";

	public static readonly string GM_PREFIX_SERVER_AND_CLIENT = "##";

	public static readonly string GM_PREFIX_SERVER = "#";

	public static readonly string GM_PREFIX_CLIENT = "-";

	public bool QuestionFunState;

	public static string Blank = "_";

	public Dictionary<int, List<ChatManager.ChatInfo>> Chats = new Dictionary<int, List<ChatManager.ChatInfo>>();

	public Dictionary<int, List<ChatManager.ChatInfo>> ChatNews = new Dictionary<int, List<ChatManager.ChatInfo>>();

	private List<string> _VOICE_ANIMS;

	private int _SingleWorldTalkLv = -1;

	private static Text mFontTool;

	private List<ChatManager.FaceSuit> FaceSuits;

	private static UIPool ChatInfoChannelPool;

	public static Transform ChatInfoChannelPoolTransform;

	private static UIPool ChatInfoTipPool;

	public static Transform ChatInfoTipPoolTransform;

	private static UIPool FacePool;

	public static Transform FacePoolTransform;

	private static ChatManager instance;

	private long m_req_voice_uuid;

	private ChatInfo2Channel m_req_voice_target;

	private List<DetailInfo> FaceInfos = new List<DetailInfo>();

	private Dictionary<int, bool> ChannelTipOnList = new Dictionary<int, bool>();

	private Dictionary<int, bool> ChannelMaskOnList = new Dictionary<int, bool>();

	private bool IsInCD;

	private int m_talkTime;

	private int m_talkRepeatNum;

	private List<ChatManager.LimitTalk> m_listLimitTalk = new List<ChatManager.LimitTalk>();

	private int BroadcastPlaceholderIndex;

	public static int FacePlaceholder2Length
	{
		get
		{
			return ChatManager.FacePlaceholder.get_Length();
		}
	}

	public static int FacePlaceholder2TotalLength
	{
		get
		{
			return ChatManager.FacePlaceholder.get_Length() + 2;
		}
	}

	public static int ItemPlaceholder2Length
	{
		get
		{
			return ChatManager.ItemPlaceholder.get_Length();
		}
	}

	public static int Blank2Length
	{
		get
		{
			return ChatManager.Blank.get_Length();
		}
	}

	public List<string> VOICE_ANIMS
	{
		get
		{
			if (this._VOICE_ANIMS == null)
			{
				this._VOICE_ANIMS = new List<string>();
				this._VOICE_ANIMS.Add("yuyin3_3");
				this._VOICE_ANIMS.Add("yuyin3_2");
				this._VOICE_ANIMS.Add("yuyin3_1");
			}
			return this._VOICE_ANIMS;
		}
	}

	public int SingleWorldTalkLv
	{
		get
		{
			if (this._SingleWorldTalkLv < 0)
			{
				string value = DataReader<GlobalParams>.Get("talkLv").value;
				this._SingleWorldTalkLv = int.Parse(GameDataUtils.SplitString4Dot0(value));
			}
			return this._SingleWorldTalkLv;
		}
	}

	public static ChatManager Instance
	{
		get
		{
			if (ChatManager.instance == null)
			{
				ChatManager.instance = new ChatManager();
			}
			return ChatManager.instance;
		}
	}

	private ChatManager()
	{
		this.ChannelTipOnList.set_Item(4, PlayerPrefsExt.GetBool("CHANNEL_TIP_TAG" + 4, true));
		this.ChannelMaskOnList.set_Item(1, PlayerPrefsExt.GetBool("CHANNEL_TIP_TAG" + 1, false));
		this.ChannelMaskOnList.set_Item(2, PlayerPrefsExt.GetBool("CHANNEL_TIP_TAG" + 2, false));
		this.ChannelMaskOnList.set_Item(32, PlayerPrefsExt.GetBool("CHANNEL_TIP_TAG" + 32, false));
		this.ChannelMaskOnList.set_Item(128, PlayerPrefsExt.GetBool("CHANNEL_TIP_TAG" + 128, false));
		BulletCurtainManager.Instance.Init();
		ChatManager.CreatePools();
		this.InitChatQueues();
		this.m_talkTime = int.Parse(GameDataUtils.SplitString4Dot0(DataReader<GlobalParams>.Get("talkTime").value));
		this.m_talkRepeatNum = int.Parse(GameDataUtils.SplitString4Dot0(DataReader<GlobalParams>.Get("talkRepeatNum").value));
	}

	public static string GetBlank(string dstValue, int fontSize)
	{
		float preferredWidth = ChatManager.instance.GetPreferredWidth(dstValue, fontSize, false);
		return ChatManager.GetBlank(preferredWidth, fontSize);
	}

	public static string GetBlank(float dstLength, int fontSize)
	{
		string text = string.Empty;
		bool flag = true;
		while (flag)
		{
			text += ChatManager.Blank;
			if (ChatManager.instance.GetPreferredWidth(text, fontSize, false) >= dstLength)
			{
				flag = false;
			}
		}
		return TextColorMgr.GetColorByID(text, 301, 0f);
	}

	public static string GetBlank(int count)
	{
		string text = string.Empty;
		for (int i = 0; i < count; i++)
		{
			text += ChatManager.Blank;
		}
		return TextColorMgr.GetColorByID(text, 301, 0f);
	}

	private void InitChatQueues()
	{
		this.Chats.Add(1, new List<ChatManager.ChatInfo>());
		this.Chats.Add(2, new List<ChatManager.ChatInfo>());
		this.Chats.Add(4, new List<ChatManager.ChatInfo>());
		this.Chats.Add(8, new List<ChatManager.ChatInfo>());
		this.Chats.Add(16, new List<ChatManager.ChatInfo>());
		this.Chats.Add(32, new List<ChatManager.ChatInfo>());
		this.Chats.Add(128, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(1, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(2, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(4, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(8, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(16, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(32, new List<ChatManager.ChatInfo>());
		this.ChatNews.Add(128, new List<ChatManager.ChatInfo>());
	}

	public Font GetFont()
	{
		return ChatManager.mFontTool.get_font();
	}

	public float GetPreferredWidth(string text, int fontSize, bool filterColor)
	{
		ChatManager.mFontTool.set_fontSize(fontSize);
		if (filterColor)
		{
			ChatManager.mFontTool.set_text(TextColorMgr.FilterColor(text));
		}
		else
		{
			ChatManager.mFontTool.set_text(text);
		}
		return ChatManager.mFontTool.get_preferredWidth();
	}

	public float GetPreferredHeigth(string text, int fontSize, bool filterColor)
	{
		ChatManager.mFontTool.set_fontSize(fontSize);
		if (filterColor)
		{
			ChatManager.mFontTool.set_text(TextColorMgr.FilterColor(text));
		}
		else
		{
			ChatManager.mFontTool.set_text(text);
		}
		return ChatManager.mFontTool.get_preferredHeight();
	}

	public float GetPreferredWidthOfSpace(string text, int fontSize, float lineWidth, ref float lastSpace)
	{
		text = TextColorMgr.FilterColor(text);
		return this.GetPreferredWidth(text, fontSize, false) + this.GetPreivousLineSpace(text, fontSize, lineWidth, ref lastSpace);
	}

	private float GetPreivousLineSpace(string text, int fontSize, float lineWidth, ref float lastSpace)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float preferredWidth = this.GetPreferredWidth(text, fontSize, false);
		if (preferredWidth <= lineWidth)
		{
			num3 = 0f;
		}
		else
		{
			int length = text.get_Length();
			int num4 = 8;
			bool flag = false;
			int i = 1;
			while (i < length)
			{
				if (num2 != 0f)
				{
					if (!flag)
					{
						num = num2;
					}
				}
				else
				{
					num = this.GetPreferredWidth(text.Substring(0, i), fontSize, false) + num3;
				}
				if (i + num4 > text.get_Length())
				{
					num4 = text.get_Length() - i;
				}
				num2 = this.GetPreferredWidth(text.Substring(0, i + num4), fontSize, false) + num3;
				if ((int)(num / lineWidth) < (int)(num2 / lineWidth))
				{
					if (num4 == 1)
					{
						float num5 = lineWidth - num % lineWidth;
						num3 += num5;
						lastSpace = num5;
						num2 += num5;
						flag = false;
						i += num4;
						num4 = 8;
					}
					else
					{
						flag = true;
						num4 = (int)((float)num4 * 0.5f);
					}
				}
				else if (num4 == 4 || num4 == 8)
				{
					flag = false;
					i += num4;
					num4 = 4;
				}
				else
				{
					flag = false;
					i += num4;
					num4 = 1;
				}
			}
		}
		return num3;
	}

	public List<ChatManager.FaceSuit> GetFaceSuits()
	{
		this.OrganizeFaces();
		return this.FaceSuits;
	}

	public ChatManager.FaceSuit GetFaceSuitByNum(int num)
	{
		this.OrganizeFaces();
		for (int i = 0; i < this.FaceSuits.get_Count(); i++)
		{
			if (this.FaceSuits.get_Item(i).num == num)
			{
				return this.FaceSuits.get_Item(i);
			}
		}
		return null;
	}

	public int FindPlaceholder2FaceNum(string subByNum)
	{
		string text = subByNum.Substring(0, ChatManager.FacePlaceholder2Length);
		if (text.Equals(ChatManager.FacePlaceholder))
		{
			string num = subByNum.Substring(ChatManager.FacePlaceholder2Length);
			return ChatManager.Instance.FindFacePlaceholder2Num(num);
		}
		return 0;
	}

	private int FindFacePlaceholder2Num(string num)
	{
		this.OrganizeFaces();
		for (int i = 0; i < this.FaceSuits.get_Count(); i++)
		{
			if (this.FaceSuits.get_Item(i).num.ToString("D2") == num)
			{
				return this.FaceSuits.get_Item(i).num;
			}
		}
		return 0;
	}

	private void OrganizeFaces()
	{
		if (this.FaceSuits == null)
		{
			this.FaceSuits = new List<ChatManager.FaceSuit>();
			List<Face> dataList = DataReader<Face>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				this.Add2FaceSuit(dataList.get_Item(i));
			}
		}
	}

	private void Add2FaceSuit(Face dataFace)
	{
		for (int i = 0; i < this.FaceSuits.get_Count(); i++)
		{
			if (this.FaceSuits.get_Item(i).num == dataFace.num)
			{
				this.FaceSuits.get_Item(i).icons.Add(GameDataUtils.GetIconName(dataFace.icon));
				return;
			}
		}
		ChatManager.FaceSuit faceSuit = new ChatManager.FaceSuit();
		faceSuit.num = dataFace.num;
		faceSuit.icons.Add(GameDataUtils.GetIconName(dataFace.icon));
		this.FaceSuits.Add(faceSuit);
	}

	private static void CreatePools()
	{
		ChatManager.ChatInfoChannelPoolTransform = new GameObject("Pool2ChatChannel").get_transform();
		ChatManager.ChatInfoChannelPoolTransform.set_parent(UINodesManager.NoEventsUIRoot);
		UGUITools.ResetTransform(ChatManager.ChatInfoChannelPoolTransform);
		ChatManager.ChatInfoChannelPool = new UIPool("ChatInfo2Channel", ChatManager.ChatInfoChannelPoolTransform, false);
		ChatManager.ChatInfoTipPoolTransform = new GameObject("Pool2ChatTip").get_transform();
		ChatManager.ChatInfoTipPoolTransform.set_parent(UINodesManager.NoEventsUIRoot);
		ChatManager.ChatInfoTipPoolTransform.get_gameObject().SetActive(false);
		UGUITools.ResetTransform(ChatManager.ChatInfoTipPoolTransform);
		ChatManager.ChatInfoTipPool = new UIPool("ChatInfo2Tip", ChatManager.ChatInfoTipPoolTransform, false);
		ChatInfoBase component = ResourceManager.GetInstantiate2Prefab("ChatInfo2Tip").GetComponent<ChatInfoBase>();
		component.set_name("FontTool");
		component.get_transform().SetParent(UINodesManager.T4RootOfSpecial);
		ChatManager.mFontTool = component.get_transform().FindChild("Content").GetComponent<Text>();
		component.get_gameObject().SetActive(false);
		ChatManager.FacePoolTransform = new GameObject("Pool2Face").get_transform();
		ChatManager.FacePoolTransform.set_parent(UINodesManager.NoEventsUIRoot);
		ChatManager.FacePoolTransform.get_gameObject().SetActive(false);
		UGUITools.ResetTransform(ChatManager.FacePoolTransform);
		ChatManager.FacePool = new UIPool("Item2Face", ChatManager.FacePoolTransform, false);
	}

	public static void Reuse2ChannelChatInfoPool(ChatInfoBase gridUI)
	{
		if (gridUI != null)
		{
			gridUI.Clear();
			ChatManager.ChatInfoChannelPool.ReUse(gridUI.get_gameObject());
		}
	}

	public static ChatInfoBase CreatePrefab2ChannelChatInfo(string name = "")
	{
		return ChatManager.ChatInfoChannelPool.Get(name).GetComponent<ChatInfoBase>();
	}

	public static void Reuse2TipChatInfoPool(ChatInfoBase gridUI)
	{
		if (gridUI != null)
		{
			gridUI.Clear();
			ChatManager.ChatInfoTipPool.ReUse(gridUI.get_gameObject());
		}
	}

	public static ChatInfoBase CreatePrefab2TipChatInfo(string name = "")
	{
		return ChatManager.ChatInfoTipPool.Get(name).GetComponent<ChatInfoBase>();
	}

	public static void Reuse2FacePool(Item2Face gridUI)
	{
		if (gridUI != null)
		{
			gridUI.ResetAll();
			ChatManager.FacePool.ReUse(gridUI.get_gameObject());
		}
	}

	public static Item2Face CreatePrefab2Face(string name = "")
	{
		return ChatManager.FacePool.Get(name).AddUniqueComponent<Item2Face>();
	}

	public static Item2Face CreateFace(int num, Transform parent)
	{
		ChatManager.FaceSuit faceSuitByNum = ChatManager.Instance.GetFaceSuitByNum(num);
		Item2Face item2Face = ChatManager.CreatePrefab2Face("Item2Face" + num);
		UGUITools.ResetTransform(item2Face.get_transform(), parent);
		RectTransform rectTransform = item2Face.get_transform() as RectTransform;
		rectTransform.set_anchorMin(ConstVector2.LR);
		rectTransform.set_anchorMax(ConstVector2.LR);
		rectTransform.set_pivot(ConstVector2.LR);
		rectTransform.set_sizeDelta(Vector2.get_zero());
		item2Face.SetFaces(faceSuitByNum.icons);
		return item2Face;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		if (ChatTipUIViewModel.Instance != null)
		{
			ChatTipUIViewModel.Instance.Release();
		}
		if (ChatUIView.Instance != null)
		{
			ChatUIView.Instance.Release();
		}
		this.SetChannelPrivateTip(false);
		using (Dictionary<int, List<ChatManager.ChatInfo>>.ValueCollection.Enumerator enumerator = this.Chats.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				List<ChatManager.ChatInfo> current = enumerator.get_Current();
				current.Clear();
			}
		}
		using (Dictionary<int, List<ChatManager.ChatInfo>>.ValueCollection.Enumerator enumerator2 = this.ChatNews.get_Values().GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				List<ChatManager.ChatInfo> current2 = enumerator2.get_Current();
				current2.Clear();
			}
		}
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ChatNotify>(new NetCallBackMethod<ChatNotify>(this.OnChatNotifyRes));
		NetworkManager.AddListenEvent<TalkRes>(new NetCallBackMethod<TalkRes>(this.OnTalkRes));
		NetworkManager.AddListenEvent<GMCommandRes>(new NetCallBackMethod<GMCommandRes>(this.OnGMCommandRes));
		NetworkManager.AddListenEvent<ClientLogRes>(new NetCallBackMethod<ClientLogRes>(this.OnClientLogRes));
		NetworkManager.AddListenEvent<GuildQuestionOpenNty>(new NetCallBackMethod<GuildQuestionOpenNty>(this.OnUpdateQuestionState));
		NetworkManager.AddListenEvent<GuildQuestionRewardNty>(new NetCallBackMethod<GuildQuestionRewardNty>(this.OnUpdateQuestionReward));
		NetworkManager.AddListenEvent<GetVideoRes>(new NetCallBackMethod<GetVideoRes>(this.OnGetVideoRes));
	}

	public void VoiceReqReset()
	{
		this.m_req_voice_uuid = 0L;
		this.m_req_voice_target = null;
	}

	public void SendVoiceReq(long uuid, ChatInfo2Channel action)
	{
		this.m_req_voice_uuid = uuid;
		this.m_req_voice_target = action;
		NetworkManager.Send(new GetVideoReq
		{
			videoUId = uuid
		}, ServerType.Chat);
	}

	private void OnGetVideoRes(short state, GetVideoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.m_req_voice_uuid == down.videoUId && this.m_req_voice_target != null && this.m_req_voice_target.CheckVoice(down.videoUId))
		{
			this.m_req_voice_target.SpeechPlay(down.videoData, down.videoUId);
			this.VoiceReqReset();
		}
	}

	public void SendTalk(ChannelType.CT channel2Server, string text2chat, List<DetailInfo> items, long targetUID, List<DetailInfo> faceDetailInfos)
	{
		if (!this.IsCDPass())
		{
			return;
		}
		if (!this.IsSameLimitTalkPass(text2chat))
		{
			return;
		}
		Audience audience = new Audience();
		audience.type = channel2Server;
		audience.id = targetUID;
		ArticleContent articleContent = new ArticleContent();
		articleContent.text = text2chat;
		if (faceDetailInfos != null)
		{
			for (int i = 0; i < faceDetailInfos.get_Count(); i++)
			{
				articleContent.items.Add(faceDetailInfos.get_Item(i));
			}
		}
		if (items != null)
		{
			for (int j = 0; j < items.get_Count(); j++)
			{
				articleContent.items.Add(items.get_Item(j));
			}
		}
		TalkReq talkReq = new TalkReq();
		talkReq.audiences.Add(audience);
		talkReq.content = articleContent;
		NetworkManager.Send(talkReq, ServerType.Chat);
	}

	public void SendGMCommand(int _sequent, string _content)
	{
		NetworkManager.Send(new GMCommandReq
		{
			sequent = _sequent,
			content = _content
		}, ServerType.Chat);
	}

	public void SendClientLog(string logString, int type)
	{
		NetworkManager.Send(new ClientLogReq
		{
			content = logString,
			level = type
		}, ServerType.Chat);
	}

	private void OnChatNotifyRes(short state, ChatNotify down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		List<TalkMsg> msgs = down.msgs;
		for (int i = 0; i < msgs.get_Count(); i++)
		{
			TalkMsg talkMsg = msgs.get_Item(i);
			int srcChannel = ChannelBit.Server2ClientChannel(talkMsg.type);
			int dstChannels = ChannelBit.GetDstChannels(srcChannel, talkMsg.sender.id);
			this.Add2Channels(srcChannel, dstChannels, talkMsg);
		}
		this.RefreshCurrentChatChannel();
	}

	private void OnTalkRes(short state, TalkRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGMCommandRes(short state, GMCommandRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnClientLogRes(short state, ClientLogRes down = null)
	{
	}

	private void OnUpdateQuestionState(short state, GuildQuestionOpenNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.QuestionFunState = down.isOpen;
		EventDispatcher.Broadcast<bool>("ChatManager.QuestionFunIconState", down.isOpen);
	}

	private void OnUpdateQuestionReward(short state, GuildQuestionRewardNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.item.get_Count(); i++)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetItemName(down.item.get_Item(i).cfgId, true, down.item.get_Item(i).count), 1f, 1f);
		}
	}

	public void Send(int channel2Client, string chatContent, List<DetailInfo> items, long targetId)
	{
		if (!string.IsNullOrEmpty(chatContent) || (items != null && items.get_Count() > 0))
		{
			chatContent = this.FilterFacePlaceholder(chatContent, true);
			if (chatContent.get_Length() > ChatManager.MAX_NUM_2_CHAR)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502064, false));
				return;
			}
			if (this.FaceInfos.get_Count() > ChatManager.MAX_NUM_2_FACE)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502063, false));
				return;
			}
			string text2chat;
			WordFilter.filter(chatContent, out text2chat, 3, false, false, "*");
			this.SendTalk(ChannelBit.Client2ServerChannel(channel2Client), text2chat, items, targetId, this.FaceInfos);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502061, false));
		}
	}

	public void OpenChatUI(int channel)
	{
		UIManagerControl.Instance.OpenUI("ChatUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		ChatUIViewModel.Instance.ShowAsModel2View(channel);
	}

	public void OpenChatUI2ChannelPrivate(long privateId = 0L, string privateName = "")
	{
		this.OpenChatUI(4);
		if (privateId > 0L)
		{
			ChatUIViewModel.Instance.SetCurrentPrivateTalk(privateId, privateName, false);
		}
	}

	private void Add2Channels(int srcChannel, int dstChannels, TalkMsg talkMsg)
	{
		ChatManager.ChatInfo chatInfo = this.GetChatInfo(srcChannel, dstChannels, talkMsg);
		if (chatInfo.sender_uid <= 0L)
		{
			chatInfo.sender_name = TextColorMgr.GetColor("系统消息", "AA0A00", string.Empty);
		}
		if (srcChannel == 32)
		{
			EventDispatcher.Broadcast<ChatManager.ChatInfo>("ChatManager.TeamMessage", chatInfo);
		}
		if (ChatManager.IsVoice(chatInfo))
		{
			this.Add2ChatUI(dstChannels, chatInfo);
			this.Add2ChatTipUI(dstChannels, chatInfo);
		}
		else
		{
			this.SendMessageToBulletCurtain(srcChannel, chatInfo);
			this.Add2ChatUI(dstChannels, chatInfo);
			this.Add2ChatTipUI(dstChannels, chatInfo);
		}
	}

	public void BroadcastMessageReceive(string msg, List<DetailInfo> items)
	{
		int dstChannels = ChannelBit.GetDstChannels(64, 0L);
		ChatManager.ChatInfo chatInfo = this.GetChatInfo(64, dstChannels, new TalkMsg
		{
			content = new ArticleContent(),
			content = 
			{
				text = msg
			}
		});
		chatInfo.viplevel = -1;
		chatInfo.sender_uid = -1L;
		chatInfo.sender_occupation = 0;
		chatInfo.items = items;
		chatInfo.time = this.GetServerTimeString(TimeManager.Instance.PreciseServerTime);
		this.Add2ChatUI(dstChannels, chatInfo);
		this.Add2ChatTipUI(dstChannels, chatInfo);
		this.RefreshCurrentChatChannel();
	}

	public void ServerStringReceive(string msg)
	{
		int dstChannels = ChannelBit.GetDstChannels(8, 0L);
		ChatManager.ChatInfo chatInfo = this.GetChatInfo(64, dstChannels, new TalkMsg
		{
			content = new ArticleContent(),
			content = 
			{
				text = msg
			}
		});
		chatInfo.viplevel = -1;
		chatInfo.sender_uid = -1L;
		chatInfo.sender_occupation = 0;
		chatInfo.items = new List<DetailInfo>();
		chatInfo.time = this.GetServerTimeString(TimeManager.Instance.PreciseServerTime);
		this.Add2ChatUI(dstChannels, chatInfo);
		this.Add2ChatTipUI(dstChannels, chatInfo);
		this.RefreshCurrentChatChannel();
	}

	public static bool IsVoice(ChatManager.ChatInfo chatInfo)
	{
		return chatInfo.items.get_Count() == 1 && chatInfo.items.get_Item(0).type == DetailType.DT.Audio;
	}

	public static int GetVoiceTime(ChatManager.ChatInfo chatInfo)
	{
		if (chatInfo.items.get_Count() > 0 && !string.IsNullOrEmpty(chatInfo.items.get_Item(0).label))
		{
			return int.Parse(chatInfo.items.get_Item(0).label);
		}
		return 0;
	}

	public static int GetVoiceBackgroundSize(int time)
	{
		return Mathf.Min(300, time * 20);
	}

	private ChatManager.ChatInfo GetChatInfo(int srcChannel, int dstChannels, TalkMsg talkMsg)
	{
		ChatManager.ChatInfo chatInfo = new ChatManager.ChatInfo();
		chatInfo.src_channel = srcChannel;
		if (talkMsg.sender != null)
		{
			chatInfo.sender_name = talkMsg.sender.label;
			chatInfo.sender_uid = talkMsg.sender.id;
			chatInfo.chat_type = talkMsg.sender.type;
			chatInfo.sender_occupation = talkMsg.sender.icon;
			chatInfo.viplevel = (int)talkMsg.sender.num;
		}
		chatInfo.chat_content = talkMsg.content.text;
		chatInfo.items = talkMsg.content.items;
		chatInfo.module = this.ApplyTeamInvite(talkMsg.content.text);
		chatInfo.time = this.GetServerTimeString(TimeManager.Instance.CalculateLocalServerTimeBySecond(talkMsg.time));
		if (srcChannel == 4 && chatInfo.sender_uid == EntityWorld.Instance.EntSelf.ID)
		{
			chatInfo.sender_name = "@" + ((talkMsg.receiver == null) ? "NONE" : talkMsg.receiver.label);
		}
		return chatInfo;
	}

	private void Add2ChatUI(int dstChannels, ChatManager.ChatInfo chatInfo)
	{
		for (int i = 0; i < ChannelBit.all_channelviews.Length; i++)
		{
			if (ChannelBit.IsContainChannel(dstChannels, ChannelBit.all_channelviews[i]))
			{
				this.ChatNews.get_Item(ChannelBit.all_channelviews[i]).Add(chatInfo);
				if (ChannelBit.all_channelviews[i] == 4)
				{
					this.CheckPrivateBadge();
				}
			}
		}
	}

	private void Add2ChatTipUI(int dstChannels, ChatManager.ChatInfo chatInfo)
	{
		for (int i = 0; i < ChannelBit.all_channelviews.Length; i++)
		{
			if (ChannelBit.IsContainChannel(dstChannels, ChannelBit.all_channelviews[i]) && !this.CheckIsChannelMaskOn(ChannelBit.all_channelviews[i]))
			{
				ChatTipUIViewModel.AddChat(chatInfo);
				return;
			}
		}
	}

	public void CheckPrivateBadge()
	{
		if (ChatUIViewModel.Instance != null && ChatUIViewModel.Instance.get_gameObject() != null && ChatUIViewModel.Instance.get_gameObject().get_activeInHierarchy() && ChatUIViewModel.Instance.CurrentChatChannel == 4)
		{
			return;
		}
		if (this.ChatNews.get_Item(4).get_Count() > 0 && this.CheckIsChannelTipOn(4))
		{
			this.SetChannelPrivateTip(true);
		}
	}

	private void RefreshCurrentChatChannel()
	{
		if (ChatUIViewModel.Instance != null && ChatUIViewModel.Instance.get_gameObject() != null && ChatUIViewModel.Instance.get_gameObject().get_activeInHierarchy())
		{
			ChatUIViewModel.Instance.RefreshCurrentChatChannel();
		}
	}

	private int ApplyTeamInvite(string content)
	{
		int result = 0;
		if (content.IndexOf("@#@TeamInvite") < 0)
		{
			return result;
		}
		return 101;
	}

	private string GetServerTimeString(DateTime datetime)
	{
		return string.Concat(new string[]
		{
			datetime.get_Hour() + ":" + datetime.get_Minute().ToString("D2")
		});
	}

	private void Add2FaceInfos(int num)
	{
		DetailInfo detailInfo = new DetailInfo();
		detailInfo.type = DetailType.DT.Face;
		detailInfo.cfgId = num;
		this.FaceInfos.Add(detailInfo);
	}

	protected string FilterFacePlaceholder(string content, bool switch2face = true)
	{
		if (switch2face)
		{
			this.FaceInfos.Clear();
		}
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		int num = 0;
		while (num + ChatManager.FacePlaceholder2TotalLength <= content.get_Length())
		{
			string text = content.Substring(num, ChatManager.FacePlaceholder2TotalLength);
			int num2 = ChatManager.Instance.FindPlaceholder2FaceNum(text);
			if (num2 > 0)
			{
				if (switch2face)
				{
					this.Add2FaceInfos(num2);
				}
				content = content.ReplaceFirst(text, ChatManager.ItemPlaceholder, 0);
				int num3 = text.get_Length() - ChatManager.ItemPlaceholder.get_Length();
				num += ChatManager.FacePlaceholder2TotalLength - num3;
			}
			else
			{
				num++;
			}
		}
		return content;
	}

	public int CalLengthNoContainsFace(string content)
	{
		content = this.FilterFacePlaceholder(content, false);
		return (!string.IsNullOrEmpty(content)) ? content.get_Length() : 0;
	}

	public static string FilterItemPlaceholder(string content)
	{
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		int num = 0;
		while (num + ChatManager.ItemPlaceholder2Length <= content.get_Length())
		{
			string text = content.Substring(num, ChatManager.ItemPlaceholder2Length);
			if (text.Equals(ChatManager.ItemPlaceholder))
			{
				content = content.ReplaceFirst(text, string.Empty, 0);
			}
			else
			{
				num++;
			}
		}
		return content;
	}

	public static void OnClickRole(long uuid, string name, Transform root, long guildId)
	{
		if (uuid <= 0L)
		{
			return;
		}
		if (uuid == EntityWorld.Instance.EntSelf.ID)
		{
			return;
		}
		if (uuid == -1L)
		{
			return;
		}
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		list.Add(PopButtonTabsManager.GetButtonData2Show(uuid, null));
		if (!FriendManager.Instance.IsRelationOfBuddy(uuid))
		{
			list.Add(PopButtonTabsManager.GetButtonData2AddFriend(uuid));
		}
		list.Add(PopButtonTabsManager.GetButtonData2PrivateTalk(uuid, name));
		list.Add(PopButtonTabsManager.GetButtonData2Black(uuid));
		if (SystemOpenManager.IsSystemOn(59))
		{
			list.Add(PopButtonTabsManager.GetButtonData2TeamInvite(uuid));
		}
		if (ChatManager.CanSender2Invitation())
		{
			list.Add(PopButtonTabsManager.GetButtonData2GuildInvitation(uuid));
		}
		else if (ChatManager.CanSender2Application())
		{
			list.Add(PopButtonTabsManager.GetButtonData2GuildApplication(guildId));
		}
		if (list.get_Count() > 0 && root != null)
		{
			PopButtonsAdjustUIViewModel.Open(UINodesManager.MiddleUIRoot);
			PopButtonsAdjustUIViewModel.Instance.get_transform().set_position(root.get_position());
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private static bool CanSender2Invitation()
	{
		return false;
	}

	private static bool CanSender2Application()
	{
		return false;
	}

	private void SendMessageToBulletCurtain(int srcChannel, ChatManager.ChatInfo chatInfo)
	{
		if (srcChannel != 8 && srcChannel != 64)
		{
			BulletCurtainManager.Instance.Add2BulletCurtain(chatInfo);
		}
	}

	public GameObject GetChatInfo2Bubble(ChatManager.ChatInfo chatInfo, Transform chatParent = null)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ChatInfo2Bubble");
		if (chatParent != null)
		{
			instantiate2Prefab.get_transform().SetParent(chatParent);
		}
		instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
		ChatInfoBase component = instantiate2Prefab.GetComponent<ChatInfo2Bubble>();
		component.ShowInfo(chatInfo);
		return instantiate2Prefab;
	}

	public void SetChannelTipOn(int channel, bool isOn)
	{
		if (this.ChannelTipOnList.ContainsKey(channel))
		{
			this.ChannelTipOnList.set_Item(channel, isOn);
			PlayerPrefsExt.SetBool("CHANNEL_TIP_TAG" + channel, isOn);
			if (channel == 4 && !isOn)
			{
				ChatManager.Instance.SetChannelPrivateTip(false);
			}
		}
	}

	public bool CheckIsChannelTipOn(int channel)
	{
		return this.ChannelTipOnList.ContainsKey(channel) && this.ChannelTipOnList.get_Item(channel);
	}

	public bool CheckIsChannelTipShow(int channel)
	{
		return this.ChannelTipOnList.ContainsKey(channel);
	}

	public void SetChannelPrivateTip(bool isShow)
	{
		if (ChatUIViewModel.Instance != null)
		{
			ChatUIViewModel.Instance.ChannelPrivateBadge = isShow;
		}
		if (ChatTipUIViewModel.Instance != null)
		{
			ChatTipUIViewModel.Instance.ButtonPrivateOn = isShow;
		}
	}

	public void SetChannelMaskOn(int channel, bool isOn)
	{
		if (this.ChannelMaskOnList.ContainsKey(channel))
		{
			this.ChannelMaskOnList.set_Item(channel, isOn);
			PlayerPrefsExt.SetBool("CHANNEL_MASK_TAG" + channel, isOn);
		}
	}

	public bool CheckIsChannelMaskOn(int channel)
	{
		return this.ChannelMaskOnList.ContainsKey(channel) && this.ChannelMaskOnList.get_Item(channel);
	}

	public bool CheckIsChannelMaskShow(int channel)
	{
		return this.ChannelMaskOnList.ContainsKey(channel);
	}

	private bool IsCDPass()
	{
		if (this.IsInCD)
		{
			UIManagerControl.Instance.ShowToastText("发言过于频繁");
			return false;
		}
		this.IsInCD = true;
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			this.IsInCD = false;
		});
		return true;
	}

	private bool IsSameLimitTalkPass(string content)
	{
		if (this.m_listLimitTalk.get_Count() > 0)
		{
			bool flag = true;
			while (flag)
			{
				if (this.m_listLimitTalk.get_Count() > 0 && this.m_listLimitTalk.get_Item(0).time <= TimeManager.Instance.UnscaleServerSecond)
				{
					this.m_listLimitTalk.RemoveAt(0);
				}
				else
				{
					flag = false;
				}
			}
			int num = 0;
			for (int i = 0; i < this.m_listLimitTalk.get_Count(); i++)
			{
				if (this.m_listLimitTalk.get_Item(i).content == content)
				{
					num++;
					if (num >= this.m_talkRepeatNum)
					{
						UIManagerControl.Instance.ShowToastText("请勿频繁发送相同信息");
						return false;
					}
				}
			}
		}
		ChatManager.LimitTalk limitTalk = default(ChatManager.LimitTalk);
		limitTalk.time = TimeManager.Instance.UnscaleServerSecond + this.m_talkTime;
		limitTalk.content = content;
		this.m_listLimitTalk.Add(limitTalk);
		return true;
	}

	private string GetBroadcastContent(ChatManager.ChatInfo chatInfo)
	{
		string channelNameWithColor = ChatManager.GetChannelNameWithColor(chatInfo.src_channel);
		string text = string.Empty;
		if (!string.IsNullOrEmpty(chatInfo.sender_name))
		{
			text = TextColorMgr.GetColorByID(chatInfo.sender_name + ":", 209);
		}
		string text2 = channelNameWithColor + text;
		string text3 = this.BroadcastContentSplit(chatInfo.chat_content, chatInfo.items);
		return text2 + text3;
	}

	private string BroadcastContentSplit(string content, List<DetailInfo> items)
	{
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		this.BroadcastPlaceholderIndex = 0;
		int num = 0;
		while (num + ChatManager.ItemPlaceholder2Length <= content.get_Length())
		{
			string text = content.Substring(num, ChatManager.ItemPlaceholder2Length);
			if (text.Equals(ChatManager.ItemPlaceholder))
			{
				if (this.BroadcastPlaceholderIndex < items.get_Count())
				{
					DetailInfo detailInfo = items.get_Item(this.BroadcastPlaceholderIndex);
					if (!string.IsNullOrEmpty(detailInfo.label))
					{
						string detailInfoName = ChatManager.GetDetailInfoName(detailInfo);
						content = content.ReplaceFirst(ChatManager.ItemPlaceholder, detailInfoName, 0);
						int num2 = ChatManager.ItemPlaceholder.get_Length() - detailInfoName.get_Length();
						num -= num2;
					}
				}
				num += ChatManager.ItemPlaceholder2Length;
				this.BroadcastPlaceholderIndex++;
			}
			else
			{
				num++;
			}
		}
		return content;
	}

	public static string GetChannelNameWithColor(int channel)
	{
		string result = string.Empty;
		switch (channel)
		{
		case 1:
			result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502000, false), 201);
			return result;
		case 2:
			result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502001, false), 203);
			return result;
		case 3:
		case 5:
		case 6:
		case 7:
			IL_30:
			if (channel == 32)
			{
				result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502005, false), 212);
				return result;
			}
			if (channel == 64)
			{
				result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502004, false), 211);
				return result;
			}
			if (channel != 128)
			{
				return result;
			}
			result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502006, false), 214);
			return result;
		case 4:
			result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502002, false), 202);
			return result;
		case 8:
			result = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502003, false), 204);
			return result;
		}
		goto IL_30;
	}

	public static string GetDetailInfoName(DetailInfo detailInfo)
	{
		if (detailInfo.type != DetailType.DT.Equipment)
		{
			return detailInfo.label;
		}
		Items items = DataReader<Items>.Get(detailInfo.cfgId);
		if (items != null)
		{
			return GameDataUtils.GetItemNameCustom(items, "[" + GameDataUtils.GetChineseContent(items.name, false) + "]");
		}
		return string.Empty;
	}
}
