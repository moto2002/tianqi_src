using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : UIBase
{
	public struct TopLeftTabData
	{
		public string name;

		public Action<bool> showAction;

		public GameObject stretchGameObject;
	}

	private const float TopRightButtonXClose = 800f;

	private const float TopRightButtonXOpen = 70f;

	private const float LeftTaskTeamButtonXClose = -270f;

	private const float LeftTaskTeamButtonXOpen = 0f;

	private const float RightMoreButtonXClose = 272f;

	private const float RightMoreButtonXOpen = -68f;

	public const uint FLYSHOE_DELAY = 200u;

	private const int ExtraPackLevel = 30;

	public static TownUI Instance;

	private Transform headObj;

	private ListPool equipRecommendRootListPool;

	private ListPool itemUseRecommendTipListPool;

	private Transform FashionTimeoutRecommendRegion;

	private Transform NewFashionRecommendRegion;

	private Text nameLab;

	private Text level;

	private Text num;

	private Transform fight;

	private Image exp;

	private Slider bar;

	private Image m_spVIPLevelBg;

	private Image m_spVIPTitle;

	private Image m_spVIPLevel1;

	private Image m_spVIPLevel2;

	private int m_goBtnBountyFxId;

	private int m_vipBtnFxId;

	private int mGodWeaponBtnFxId;

	public Transform FXNav;

	private GameObject m_goGodWeaponProgressUI;

	private GameObject m_goMainTaskRootUI;

	private GameObject m_goSystemOpenProgressUI;

	private GameObject m_teamTownUI;

	private GameObject m_goMiniMap;

	[HideInInspector]
	public Transform m_ChatTipUIRoot;

	private GameObject m_goQuit;

	private GameObject m_goFleaMarket;

	private GameObject m_goFleaMarketBadge;

	private GameObject m_goXMarket;

	private GameObject m_goXMarketBadge;

	private GameObject m_goLuckDraw;

	private GameObject m_goAchievement;

	private GameObject m_goFirstPayGiftButton;

	private GameObject m_goButtonTipsTownUiFirstPay2;

	private GameObject mGoPayButton;

	private GameObject mGoBuyGoldButton;

	private GameObject m_goSignIn;

	private GameObject mGoDailyTask;

	private GameObject m_goOperateActivity;

	private GameObject mGoStrongerTips;

	private GameObject m_goBtnBounty;

	private GameObject m_goButtonChangeCareer;

	private GameObject m_goMail;

	private GameObject m_goMailBadge;

	private GameObject m_goBossBook;

	private GameObject m_goBtnGuildWar;

	private GameObject m_goBtnOpenActivity;

	private GameObject m_goBtnNewPeoperGift;

	private GameObject m_RedBagBtn;

	private GameObject m_RankUpChange;

	private Transform activityRecommendRegion;

	private RectTransform mGotoUpdateButton;

	private GameObject mGoSurvival;

	private GameObject mGoPVP;

	private Image mImgSwitchBottom;

	private GameObject mBottomRightPoint;

	private RectTransform mRightBottomPanel;

	private Image m_spBottomRightMask;

	private Mask m_maskBottomRightMask;

	private RectTransform mRightBottomBg;

	private GameObject mGoPackage;

	private GameObject mGoSkill;

	private GameObject mGoGodSoldier;

	private GameObject mGoEquip;

	private GameObject mGoPet;

	private GameObject mGoRole;

	private GameObject mGoGuild;

	private GameObject m_goDayGiftButton;

	private GameObject m_goButtonTipsTownUiDayGift;

	private GameObject m_goButtonTipsTownUiPay;

	private GameObject m_goButtonGuildAnswer;

	private GameObject m_benifitButton;

	private GameObject m_GodWeaponButton;

	private GameObject m_TopRightButtonsMask;

	private GameObject m_BtnSwitchTopRight;

	private GameObject m_CurResourcePoint;

	private Text m_CurResourcePointNum;

	private GameObject m_GuildWarCityResouce;

	private Text m_GuildWarCityResouceLeftTeamName;

	private Text m_GuildWarCityResouceLeftTeamNum;

	private RectTransform m_GuildWarCityResouceLeftTeamBar;

	private Text m_GuildWarCityResouceLeftTeamBarText;

	private Text m_GuildWarCityResouceRightTeamName;

	private Text m_GuildWarCityResouceRightTeamNum;

	private RectTransform m_GuildWarCityResouceRightTeamBar;

	private Text m_GuildWarCityResouceRightTeamBarText;

	private Text m_GuildWarCityTimeCDText;

	private Transform m_MineAndReportUISlot;

	private GameObject m_GuildWarBubbleGo;

	protected Action ClickButtonGuildWarBubbleAction;

	private GameObject wildBossBubbleGo;

	private Action ClickButtonWildBossBubbleAction;

	private GameObject m_transactionBubbleGo;

	private Action ClickButtonTransactionBubbleAction;

	private RectTransform mMorePanel;

	private RectTransform mSwitchMoreArrow;

	private GameObject mMorePoint;

	private RectTransform mTaskAndTeamPanel;

	private Image mSwitchTeamArrow;

	private RectTransform mTopRightButtonPanel;

	private Image m_spTopRightButtonsMask;

	private Mask m_maskTopRightButtonsMask;

	[HideInInspector]
	public Transform FXGodWeaponProgressUI;

	private TaskAndTeamState currentTaskOrTeamState = TaskAndTeamState.TaskState;

	private GameObject wildBossQueue;

	private Text wildBossQueueName;

	private Text wildBossQueueNum;

	private GameObject wildBossQueueInfoBtn;

	private Text wildBossQueueInfoBtnText;

	private Text wildBossQueueInfoBtnNum;

	private Image mFastTransMask;

	private Transform VipTasteRegion;

	private Text TextVipTasteTime;

	private Text TextRedBagTime;

	protected GameObject TopLeftTabs;

	protected List<TopLeftTab> TopLeftTabList = new List<TopLeftTab>();

	protected GameObject TopLeftTabsBtnLeftImage;

	protected GameObject TopLeftTabsBtnRightImage;

	protected bool isTabsStretchOut = true;

	protected XDict<BaseTweenPostion, Vector3> stretchInfo = new XDict<BaseTweenPostion, Vector3>();

	protected Vector3 StretchOffset = new Vector3(-270f, 0f, 0f);

	private static int fxNavID;

	private uint teamBeInviteTimerID;

	private int mStrongerFxId;

	private TimeCountDown timeCoundDown;

	private string lastNewFashionDataID = string.Empty;

	private bool IsFleaMarketOnSelf;

	private bool IsFleaMarketOnSystem;

	private bool IsXMarketOnSystem;

	private int m_FirstPayFxId;

	public static bool IsFirstChargeOnSelf;

	private bool IsFirstChargeOnGuide;

	private int m_DayGiftFxId;

	private bool IsDayGiftOnGuide;

	public static bool IsOpenAnimationOn;

	private bool mIsShowTopRight = true;

	private int GuildAnswerTipFXID;

	private uint mDelayShowFisrtPlayId;

	private bool mIsShowTeam = true;

	private bool mIsShowMore;

	private bool mIsShowRightBottom;

	private Vector2 mRightBottomPanelPos;

	private Vector2 mRightBottomBgPos;

	private bool _IsForceOpenRightBottom;

	private bool mHasFlyShoeReq;

	private bool mHasFlyShoeRes;

	private bool mFlySHoeTimeOut;

	private int bossBookFxID;

	private int guildWarFxID;

	private int redBagBtnFxID;

	private int redBagBtnFxID2;

	private bool IsChangeCareerOnSystem;

	private RectTransform m_posButtonsLeftBase;

	private RectTransform m_posButtonsTopRightBase;

	private TimeCountDown guildWarCityTimeCD;

	private int DailyTaskEffectId;

	private int AcOpenServerFXID;

	private TimeCountDown redBagTimeCoundDown;

	private TimeCountDown redBagTimeCD;

	private bool isCanClickRedBag = true;

	public bool IsForceOpenRightBottom
	{
		get
		{
			return this._IsForceOpenRightBottom;
		}
		set
		{
			this._IsForceOpenRightBottom = value;
		}
	}

	private void Awake()
	{
		TownUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.nameLab = base.FindTransform("NameLab").GetComponent<Text>();
		this.level = base.FindTransform("LevelLab").GetComponent<Text>();
		this.headObj = base.FindTransform("HeadIcon");
		this.num = base.FindTransform("num").GetComponent<Text>();
		this.fight = base.FindTransform("FightLab");
		this.exp = base.FindTransform("BoolIcon").GetComponent<Image>();
		this.bar = base.FindTransform("ExpBar").GetComponent<Slider>();
		base.FindTransform("VIP").get_gameObject().SetActive(SystemConfig.IsOpenPay);
		this.mGoBuyGoldButton.SetActive(false);
		this.m_spVIPLevelBg = base.FindTransform("VIPBtnBg").GetComponent<Image>();
		this.m_spVIPTitle = base.FindTransform("VIPTitle").GetComponent<Image>();
		this.m_spVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<Image>();
		this.m_spVIPLevel2 = base.FindTransform("VIPLevel2").GetComponent<Image>();
		this.FXNav = base.FindTransform("FXNav");
		this.activityRecommendRegion = base.FindTransform("ActivityRecommendRegion");
		this.mGotoUpdateButton = base.FindTransform("BtnGotoUpdate").GetComponent<RectTransform>();
		this.FXGodWeaponProgressUI = base.FindTransform("FXGodWeaponProgressUI");
		this.m_ChatTipUIRoot = base.FindTransform("ChatTipUIRoot");
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.OpenGodWeaponProgressUI();
		this.OpenMainTaskUI();
		this.OpenSystemOpenProgressUI();
		this.OpenTeamUI();
		this.OpenMiniMap();
		this.m_posButtonsLeftBase = (base.FindTransform("ButtonsLeftBase") as RectTransform);
		this.m_posButtonsTopRightBase = (base.FindTransform("ButtonsTopRightBase") as RectTransform);
		this.m_goFleaMarket = base.FindTransform("FleaMarket").get_gameObject();
		this.m_goFleaMarketBadge = base.FindTransform("FleaMarketBadge").get_gameObject();
		this.m_goXMarket = base.FindTransform("XMarket").get_gameObject();
		this.m_goXMarketBadge = base.FindTransform("XMarketBadge").get_gameObject();
		this.m_goLuckDraw = base.FindTransform("LuckDraw").get_gameObject();
		this.m_goAchievement = base.FindTransform("Achievement").get_gameObject();
		this.m_goFirstPayGiftButton = base.FindTransform("FirstPayButton").get_gameObject();
		this.m_goButtonTipsTownUiFirstPay2 = base.FindTransform("ButtonTipsTownUiFirstPay2").get_gameObject();
		this.mGoPayButton = base.FindTransform("PayButton").get_gameObject();
		this.mGoBuyGoldButton = base.FindTransform("BuyGold").get_gameObject();
		this.m_goSignIn = base.FindTransform("SignIn").get_gameObject();
		this.mGoDailyTask = base.FindTransform("DailyTask").get_gameObject();
		this.m_goOperateActivity = base.FindTransform("OperateActivity").get_gameObject();
		this.mGoStrongerTips = base.FindTransform("BtnBeStrongerTip").get_gameObject();
		this.m_goMail = base.FindTransform("Mail").get_gameObject();
		this.m_goMailBadge = base.FindTransform("MailBadge").get_gameObject();
		this.m_goBtnBounty = base.FindTransform("BtnBounty").get_gameObject();
		this.m_goButtonChangeCareer = base.FindTransform("ButtonChangeCareer").get_gameObject();
		this.m_goBossBook = base.FindTransform("BossBook").get_gameObject();
		this.m_goBtnGuildWar = base.FindTransform("GuildWarButton").get_gameObject();
		this.m_goBtnOpenActivity = base.FindTransform("OpenActivityButton").get_gameObject();
		this.m_goBtnNewPeoperGift = base.FindTransform("NewPeoperGiftButton").get_gameObject();
		this.m_RedBagBtn = base.FindTransform("RedBagBtn").get_gameObject();
		this.m_RankUpChange = base.FindTransform("RankUp").get_gameObject();
		this.mGoSurvival = base.FindTransform("SurvivalButton").get_gameObject();
		this.mGoPVP = base.FindTransform("PVPButton").get_gameObject();
		this.mImgSwitchBottom = base.FindTransform("btnSwitchBottom").GetComponent<Image>();
		this.mBottomRightPoint = base.FindTransform("BottomRightPoint").get_gameObject();
		this.mRightBottomPanel = base.FindTransform("BottomRightButtonGrid").GetComponent<RectTransform>();
		this.m_spBottomRightMask = base.FindTransform("BottomRightMask").GetComponent<Image>();
		this.m_maskBottomRightMask = base.FindTransform("BottomRightMask").GetComponent<Mask>();
		this.mRightBottomBg = base.FindTransform("BottomRightBg").GetComponent<RectTransform>();
		this.DealRightBottomOfMask(false);
		this.mGoPackage = base.FindTransform("ItemBackPackButton").get_gameObject();
		this.mGoSkill = base.FindTransform("ItemSkillButton").get_gameObject();
		this.mGoGodSoldier = base.FindTransform("BtnGodSoldier").get_gameObject();
		this.mGoEquip = base.FindTransform("ItemRefiningButton").get_gameObject();
		this.mGoPet = base.FindTransform("ItemPetButton").get_gameObject();
		this.mGoRole = base.FindTransform("ItemRoleButton").get_gameObject();
		this.m_goDayGiftButton = base.FindTransform("DayGiftButton").get_gameObject();
		this.m_goButtonTipsTownUiDayGift = base.FindTransform("ButtonTipsTownUiDayGift").get_gameObject();
		this.mGoGuild = base.FindTransform("ButtonGuild").get_gameObject();
		this.m_GodWeaponButton = base.FindTransform("GodWeaponButton").get_gameObject();
		this.m_benifitButton = base.FindTransform("BenefitsButton").get_gameObject();
		this.m_TopRightButtonsMask = base.FindTransform("TopRightButtonsMask").get_gameObject();
		this.m_BtnSwitchTopRight = base.FindTransform("BtnSwitchTopRight").get_gameObject();
		this.m_CurResourcePoint = base.FindTransform("CurResoucePoint").get_gameObject();
		this.m_CurResourcePointNum = base.FindTransform("CurResoucePointNum").GetComponent<Text>();
		this.m_GuildWarCityResouce = base.FindTransform("GuildWarCityResouce").get_gameObject();
		this.m_GuildWarCityResouceLeftTeamName = base.FindTransform("GuildWarCityResouceLeftTeamName").GetComponent<Text>();
		this.m_GuildWarCityResouceLeftTeamNum = base.FindTransform("GuildWarCityResouceLeftTeamNum").GetComponent<Text>();
		this.m_GuildWarCityResouceLeftTeamBar = base.FindTransform("GuildWarCityResouceLeftTeamBar").GetComponent<RectTransform>();
		this.m_GuildWarCityResouceLeftTeamBarText = base.FindTransform("GuildWarCityResouceLeftTeamBarText").GetComponent<Text>();
		this.m_GuildWarCityResouceRightTeamName = base.FindTransform("GuildWarCityResouceRightTeamName").GetComponent<Text>();
		this.m_GuildWarCityResouceRightTeamNum = base.FindTransform("GuildWarCityResouceRightTeamNum").GetComponent<Text>();
		this.m_GuildWarCityResouceRightTeamBar = base.FindTransform("GuildWarCityResouceRightTeamBar").GetComponent<RectTransform>();
		this.m_GuildWarCityResouceRightTeamBarText = base.FindTransform("GuildWarCityResouceRightTeamBarText").GetComponent<Text>();
		this.m_GuildWarCityTimeCDText = base.FindTransform("GuildWarCityCDText").GetComponent<Text>();
		this.m_MineAndReportUISlot = base.FindTransform("MineAndReportUISlot");
		this.m_GuildWarBubbleGo = base.FindTransform("GuildWarBubble").get_gameObject();
		this.wildBossBubbleGo = base.FindTransform("WildBossBubble").get_gameObject();
		this.m_transactionBubbleGo = base.FindTransform("TransactionBubble").get_gameObject();
		this.mMorePanel = base.FindTransform("More").GetComponent<RectTransform>();
		this.mSwitchMoreArrow = base.FindTransform("arrow").GetComponent<RectTransform>();
		this.mMorePoint = base.FindTransform("MorePoint").get_gameObject();
		this.mTaskAndTeamPanel = base.FindTransform("TaskAndTeamPanel").GetComponent<RectTransform>();
		this.mSwitchTeamArrow = base.FindTransform("BtnTaskTeamSwitch").GetComponent<Image>();
		this.mTopRightButtonPanel = base.FindTransform("TopRightButtonPanel").GetComponent<RectTransform>();
		this.m_spTopRightButtonsMask = base.FindTransform("TopRightButtonsMask").GetComponent<Image>();
		this.m_maskTopRightButtonsMask = base.FindTransform("TopRightButtonsMask").GetComponent<Mask>();
		this.DealTopRightButtonOfMask(false);
		this.m_goQuit = base.FindTransform("BtnQuit").get_gameObject();
		this.wildBossQueue = base.FindTransform("WildBossQueue").get_gameObject();
		this.wildBossQueueName = base.FindTransform("WildBossQueueName").GetComponent<Text>();
		this.wildBossQueueNum = base.FindTransform("WildBossQueueNum").GetComponent<Text>();
		this.wildBossQueueInfoBtn = base.FindTransform("WildBossQueueInfoBtn").get_gameObject();
		this.wildBossQueueInfoBtnText = base.FindTransform("WildBossQueueInfoBtnText").GetComponent<Text>();
		this.wildBossQueueInfoBtnNum = base.FindTransform("WildBossQueueInfoBtnNum").GetComponent<Text>();
		this.equipRecommendRootListPool = base.FindTransform("EquipRecommendRegion").GetComponent<ListPool>();
		this.equipRecommendRootListPool.SetItem("EquipRecommendItem");
		this.itemUseRecommendTipListPool = base.FindTransform("ItemUseTipListPool").GetComponent<ListPool>();
		this.itemUseRecommendTipListPool.SetItem("EquipRecommendItem");
		this.FashionTimeoutRecommendRegion = base.FindTransform("FashionTimeoutRecommendRegion");
		this.NewFashionRecommendRegion = base.FindTransform("NewFashionRecommendRegion");
		this.mFastTransMask = base.FindTransform("FastTransMask").GetComponent<Image>();
		this.m_goButtonTipsTownUiPay = base.FindTransform("ButtonTipsTownUiPay").get_gameObject();
		this.m_goButtonGuildAnswer = base.FindTransform("GuildAnswerBtn").get_gameObject();
		base.FindTransform("CurResoucePointNum").GetComponent<Text>().set_text("我获得的资源");
		this.VipTasteRegion = base.FindTransform("VipTasteRegion");
		base.FindTransform("TextVipTasteTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508101, false));
		base.FindTransform("TextVipTasteStr1").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508104, false));
		base.FindTransform("TextVipTasteStr2").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508110, false));
		base.FindTransform("TextVipTasteStr3").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508107, false));
		this.TextVipTasteTime = base.FindTransform("TextVipTasteTime").GetComponent<Text>();
		this.TextRedBagTime = base.FindTransform("RedBagTimeLab").GetComponent<Text>();
		this.TopLeftTabs = base.FindTransform("TopLeftTabs").get_gameObject();
		this.TopLeftTabList.Add(base.FindTransform("TopLeftTab0").GetComponent<TopLeftTab>());
		this.TopLeftTabList.Add(base.FindTransform("TopLeftTab1").GetComponent<TopLeftTab>());
		this.TopLeftTabsBtnLeftImage = base.FindTransform("TopLeftTabsBtnLeftImage").get_gameObject();
		this.TopLeftTabsBtnRightImage = base.FindTransform("TopLeftTabsBtnRightImage").get_gameObject();
	}

	private void Start()
	{
		base.FindTransform("ButtonNetDisconnect").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonNetDisconnect);
		this.headObj.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHead);
		base.FindTransform("FightType").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFight);
		base.FindTransform("GuildWar").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildWar);
		base.FindTransform("SignIn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSignIn);
		base.FindTransform("Mail").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMessage);
		base.FindTransform("TeamBeInvitedTip").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTeamBeInvited);
		base.FindTransform("guildApplicationTip").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildApplicationTip);
		base.FindTransform("TalkBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTalkBubble);
		base.FindTransform("TaskBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTaskBubble);
		base.FindTransform("UpgradeBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUpgradeBubble);
		base.FindTransform("Achievement").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAchievement);
		base.FindTransform("DailyTask").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTask);
		base.FindTransform("Friend").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFriend);
		base.FindTransform("LuckDraw").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLuckDraw);
		base.FindTransform("OperateActivity").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOperateActivity);
		base.FindTransform("VipButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickVIP);
		base.FindTransform("DiscountActivity").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDiscountActivity);
		base.FindTransform("FleaMarket").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFleaMarket);
		base.FindTransform("XMarket").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickXMarket);
		base.FindTransform("Setting").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSettting);
		base.FindTransform("StrongerButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStrongerButton);
		base.FindTransform("BtnBounty").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBounty);
		base.FindTransform("FirstPayButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnFirstPayButton);
		this.mGoPayButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPayButton);
		this.mGoBuyGoldButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuyGold);
		this.mGotoUpdateButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGotoUpdateButton);
		this.m_goButtonChangeCareer.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonChangeCareer);
		base.FindTransform("ButtonGuild").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildBtn);
		base.FindTransform("GuildWarBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonGuildWarBubble);
		base.FindTransform("WildBossBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonWildBossBubble);
		base.FindTransform("TransactionBubble").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonTransactionBubble);
		base.FindTransform("btnSwitch").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.SwitchRightMoreButton);
		base.FindTransform("ButtonOpenTask").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenTask);
		base.FindTransform("ButtonOpenTeam").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenTeam);
		base.FindTransform("BtnTaskTeamSwitch").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.SwitchTaskTeamButton);
		base.FindTransform("BtnSwitchTopRight").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.SwitchTopRightButton);
		base.FindTransform("GodWeaponButtonGuide").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGodWeapon);
		base.FindTransform("BtnQuit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnQuit);
		base.FindTransform("StrongerUIBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnOpenStrongerUI);
		this.mGoSurvival.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSurvival);
		this.mGoPVP.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPVP);
		this.mImgSwitchBottom.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRightBottom);
		this.mGoPackage.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPackage);
		this.mGoSkill.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkill);
		this.mGoGodSoldier.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGodSoldier);
		this.mGoEquip.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEquip);
		this.mGoPet.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPet);
		this.mGoRole.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRole);
		this.m_goBossBook.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBossBook);
		this.wildBossQueue.GetComponentInChildren<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWildBossQueue);
		this.wildBossQueueInfoBtn.GetComponentInChildren<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWildBossQueue);
		this.m_goBtnGuildWar.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildWarRewardUI);
		this.m_goBtnOpenActivity.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenAcOpenServer);
		this.m_goBtnNewPeoperGift.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenNewPeoperGift);
		this.m_RankUpChange.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRankUpBtn);
		this.m_goButtonGuildAnswer.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildAnswerBtn);
		base.FindTransform("BenefitsButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBenefitsBtn);
		base.FindTransform("VIP").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickVIP);
		base.FindTransform("ButtonOffHook").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonOffHook);
		this.mGoStrongerTips.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickShowPromoteWay);
		base.FindTransform("DayGiftButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDayGiftButton);
		this.SwitchRightMoreButton(null);
		base.FindTransform("GuildWarCityMessageBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnGuildWarCityMessageBtnClick);
		base.FindTransform("Hunt").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHunt);
		this.m_RedBagBtn.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRedBagBtn);
		base.FindTransform("TopLeftTabsBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTabsStretch);
	}

	protected override void OnEnable()
	{
		if (!PushNotificationManager.Instance.OpenPushSystem())
		{
			this.ShowPopAnimation(true);
		}
		Utils.EnableRoleLight(true);
		this.ControlSystemOpens(true, 0);
		this.ShowTopLeftTabs(false, new TownUI.TopLeftTabData[0]);
		BadgeManager.Instance.ResetAllBadgeData();
		this.CheckBadge();
		bool isCurrentGuildFieldScene = MySceneManager.Instance.IsCurrentGuildFieldScene;
		bool isCurrentGuildWarCityScene = MySceneManager.Instance.IsCurrentGuildWarCityScene;
		this.ShowGodWeaponProgressUI(true);
		this.ShowMainTaskUI(true);
		this.ShowSystemOpenProgressUI(true);
		this.ShowTeamUI(true);
		this.ShowChatTipUI(true, !this.mIsShowRightBottom);
		this.ShowMiniMap(!isCurrentGuildFieldScene);
		this.TryShowRankUIChangeUI();
		this.mFastTransMask.get_gameObject().SetActive(false);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(false);
		CurrenciesUIViewModel.Instance.ShowCurrenciesClass = false;
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", false);
		EventDispatcher.Broadcast<UIBase>(EventNames.RefreshTipsButtonStateInUIBase, this);
		this.ShowTaskAndTeam(!isCurrentGuildFieldScene && !isCurrentGuildWarCityScene);
		this.ShowMailBtn(!isCurrentGuildFieldScene, false);
		this.ShowExitBtn(isCurrentGuildFieldScene || isCurrentGuildWarCityScene);
		this.ShowGuildAnswerButton(isCurrentGuildFieldScene);
		this.ShowGodWeaponButton(!isCurrentGuildWarCityScene);
		this.ShowTopRightButtonsInScene(!isCurrentGuildWarCityScene);
		this.ShowCurResoucePoint(false);
		this.ShowMineAndReportUI(isCurrentGuildWarCityScene);
		this.ShowGuildWarCityResouce(isCurrentGuildWarCityScene);
		this.ShowGuildWarCityTimeCountDown(isCurrentGuildWarCityScene);
		this.ShowOrHideAcOpenServer();
		this.CheckAcOpenServerTip();
		if (isCurrentGuildWarCityScene)
		{
			this.SetCurResoucePointNum(GuildWarManager.Instance.RoleTotalResourceNum.ToString());
			this.SetGuildWarCityResouceInfo(GuildWarManager.Instance.MyGuildWarResourceInfo, GuildWarManager.Instance.EnemyGuildWarResourceInfo);
		}
		this.UpdateTalkBubble(false, 0);
		this.UpdateTaskBubble(true);
		this.UpdateUpgradeBubble(true);
		this.UpdateTeamBeInvitedUI();
		this.UpdateShowGuildApplicationTip();
		this.UpdateShowRecommendEquip();
		this.UpdateShowPromoteWay();
		this.UpdateItemUseRecommendTip();
		if (WildBossManager.Instance.IsWaitForUI)
		{
			this.ShowWildBossBubble(true, new Action(WildBossManager.Instance.OnClickChallengeUI));
		}
		else
		{
			this.ShowWildBossBubble(false, null);
		}
		if (GuildWarManager.Instance.IsWaitForUI)
		{
			this.ShowGuildWarBubble(true, new Action(GuildWarManager.Instance.OnClickChallengeUI));
		}
		else
		{
			this.ShowGuildWarBubble(false, null);
		}
		if (TransactionNPCManager.Instance.IsWaitForUI)
		{
			this.ShowTransactionBubble(true, new Action(TransactionNPCManager.Instance.OnClickChallengeUI));
		}
		else
		{
			this.ShowTransactionBubble(false, null);
		}
		this.UpdateTaskOrTeamState();
		this.OnCurrentActivityShow();
		GodWeaponProgressManager.Instance.RefreshUI();
		EquipmentManager.Instance.ShowIfGoOnWashOperate();
		BossBookManager.Instance.ContinueNavToBoss();
		if (RadarManager.Instance.IsNaving)
		{
			this.BeginNav();
		}
		this.CheckExitGuildField();
		this.InitWildBossWaiting();
		this.UpdateCheckOffTime();
		this.UpdateFashionTimeoutRecommend();
		this.UpdateNewFashionRecommend();
		this.CheckGodWeaponEffect();
		this.PlayVipBtnEffect();
		this.CheckVipTasteCard();
		this.CheckVipTasteCardTime();
		this.ShowCanGetGuildWarChampionPrize();
		this.CheckShowBossBookTip();
		this.OnGuildWarStepNty();
		this.RefreshBadge();
		EventDispatcher.Broadcast<bool>(EventNames.BroadcastOfTownUI, true);
		this.OnRedBagFresh();
		this.CheckTramcarInviteMessage();
		this.CheckDownloadExtrPack();
		GuideManager.Instance.CheckQueue(false, false);
	}

	protected override void OnDisable()
	{
		this.ShowGodWeaponProgressUI(false);
		this.ShowMainTaskUI(false);
		this.ShowSystemOpenProgressUI(false);
		this.ShowTeamUI(false);
		this.ShowChatTipUI(false, true);
		this.ShowMiniMap(false);
		CurrenciesUIViewModel.Show(false);
		this.ShowTaskAndTeam(true);
		this.ShowMailBtn(true, true);
		this.ShowExitBtn(false);
		this.SwitchTopRightButtonRightNow(!this.mIsShowTopRight);
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", true);
		EventDispatcher.Broadcast<bool>(EventNames.BroadcastOfTownUI, false);
		TimerHeap.DelTimer(this.teamBeInviteTimerID);
		this.ShowPopAnimation(false);
		if (MainTaskManager.Instance.ShowTalkUINpc <= 0)
		{
			EquipmentManager.Instance.ShowRecommendTip = false;
			if (EquipmentManager.Instance.RecommendDic != null)
			{
				EquipmentManager.Instance.RecommendDic.Clear();
			}
		}
		UIManagerControl.Instance.HideUI("TaskDescUI");
		TimerHeap.DelTimer(this.mDelayShowFisrtPlayId);
		this.RemovePromoteWayFXs();
		this.equipRecommendRootListPool.Clear();
		this.IsForceOpenRightBottom = false;
		this.TryRemoveTopNewFashionRecommend();
		this.ClearTimeCoundDown();
		this.ResetVipTasteRegionState();
		this.ClearGuildWarCityTimeCountDown();
		this.HideBossComeOutTip();
		this.StopGodWeaponEffect();
		this.RemoveGuildWarFX();
		this.itemUseRecommendTipListPool.Clear();
		BackpackManager.Instance.ClearAllItemCanUseTip();
		this.ClearRedBagTimeCountDown();
		this.ClearRedBagClickTimeCountDown();
		this.isCanClickRedBag = true;
		this.RemoveRedBagBtnFx();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
	}

	private void Update()
	{
		if (MultiPVPManager.isOpenQuickBtn && Input.GetKeyDown(98))
		{
			UIManagerControl.Instance.OpenUI("MultiPVPUI", null, true, UIType.FullScreen);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.OnRoleSelfLevelUp));
		EventDispatcher.AddListener<int>("GuideManager.TaskFinish", new Callback<int>(this.OnTaskFinish));
		EventDispatcher.AddListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.UpdateDataUI));
		EventDispatcher.AddListener<bool>("FriendManagerEvents.FriendBadgeTip", new Callback<bool>(this.OnFriendBadgeTip));
		EventDispatcher.AddListener<bool>("MailEvents.IsMailReadTipOn", new Callback<bool>(this.OnMailBadgeTip));
		EventDispatcher.AddListener<bool>("MarketManager.FleaShopBadgeTip", new Callback<bool>(this.OnFleaShopBadgeTip));
		EventDispatcher.AddListener<bool>("MarketManager.FleaShopOpen", new Callback<bool>(this.OnFleaShopOpen));
		EventDispatcher.AddListener<bool>("MarketManager.XMarketBadgeTip", new Callback<bool>(this.OnXMarketBadgeTip));
		EventDispatcher.AddListener<bool>(EventNames.FirstChargeBadge, new Callback<bool>(this.OnFirstChargeBadge));
		EventDispatcher.AddListener(EventNames.BeginNav, new Callback(this.BeginNav));
		EventDispatcher.AddListener(EventNames.EndNav, new Callback(this.EndNav));
		EventDispatcher.AddListener<int>(EventNames.GetDailyTaskPrize, new Callback<int>(this.OnGetDailyTaskPrize));
		EventDispatcher.AddListener(EventNames.UpdateGuildApplication, new Callback(this.UpdateShowGuildApplicationTip));
		EventDispatcher.AddListener(EventNames.ActivityAnnounce, new Callback(this.OnCurrentActivityShow));
		EventDispatcher.AddListener(EventNames.UpdateEquipRecommend, new Callback(this.UpdateShowRecommendEquip));
		EventDispatcher.AddListener(EventNames.UpdateBeInvitedCount, new Callback(this.UpdateTeamBeInvitedUI));
		EventDispatcher.AddListener(EventNames.UpadateAskForJoinList, new Callback(this.UpadateAskForJoinList));
		EventDispatcher.AddListener(EventNames.UpdateTeamBasicInfo, new Callback(this.UpdateTeamBasicInfo));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.UpdateTeamBasicInfo));
		EventDispatcher.AddListener<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, new Callback<ChatManager.ChatInfo>(this.UpdateTeamChatTip));
		EventDispatcher.AddListener(EventNames.UpdatePromoteWayTip, new Callback(this.UpdateShowPromoteWay));
		EventDispatcher.AddListener(EventNames.OnUpdateGoods, new Callback(this.OnUpdateBackPack));
		EventDispatcher.AddListener(EventNames.VipTimeLimitNty, new Callback(this.OnUpdateVipColor));
		EventDispatcher.AddListener<int, string, int>(WildBossManagerEvent.OnSingleWaitingNumChanged, new Callback<int, string, int>(this.OnWildBossWaitingNumChanged));
		EventDispatcher.AddListener<int, string, int>(WildBossManagerEvent.OnMultiWaitingNumChanged, new Callback<int, string, int>(this.OnWildBossWaitingNumChanged));
		EventDispatcher.AddListener(EventNames.PlayTownUIWeaponEffect, new Callback(this.PlayGodWeaponEffect));
		EventDispatcher.AddListener("GuideManager.CheckGodWeaponGuide", new Callback(this.PlayGodWeaponEffect));
		EventDispatcher.AddListener(EventNames.GoldBuyChangedNty, new Callback(this.CheckBeniftRedPoint));
		EventDispatcher.AddListener(EventNames.OnLoginWelfareUpdate, new Callback(this.CheckBeniftRedPoint));
		EventDispatcher.AddListener<bool>(EventNames.FlyShoeTransportRes, new Callback<bool>(this.OnFlyShoeTransportRes));
		EventDispatcher.AddListener("OnRechargeTipChange", new Callback(this.CheckPayTip));
		EventDispatcher.AddListener(EventNames.VipTasteCardNty, new Callback(this.OnUpdateVipTasteCard));
		EventDispatcher.AddListener(EventNames.OnGuildWarMisNty, new Callback(this.ShowCanGetGuildWarChampionPrize));
		EventDispatcher.AddListener(EventNames.BossBookComeOutTipUpdate, new Callback(this.CheckShowBossBookTip));
		EventDispatcher.AddListener(EventNames.RefreshActivityInfo, new Callback(this.ControlSystemOfDayGift));
		EventDispatcher.AddListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.AddListener(EventNames.OnOpenServerActStatusNty, new Callback(this.ShowOrHideAcOpenServer));
		EventDispatcher.AddListener(EventNames.UpdateAcTypeCanGetRewardTip, new Callback(this.CheckAcOpenServerTip));
		EventDispatcher.AddListener<NPC>(EventNames.OpenNPCSystem, new Callback<NPC>(this.OnOpenNPCSystem));
		EventDispatcher.AddListener<NPC>(EventNames.OpenNPCMenu, new Callback<NPC>(this.OnOpenNPCMenu));
		EventDispatcher.AddListener(EventNames.CloseNPCMenu, new Callback(this.OnCloseNPCMenu));
		EventDispatcher.AddListener(EventNames.UpdateItemUseRecommendTip, new Callback(this.UpdateItemUseRecommendTip));
		EventDispatcher.AddListener(EventNames.RedBagFresh, new Callback(this.OnRedBagFresh));
		EventDispatcher.AddListener(EventNames.TramcarInviteFriendNty, new Callback(this.CheckTramcarInviteMessage));
		EventDispatcher.AddListener(EventNames.OnNewPeoperGiftPackage, new Callback(this.OnRefreshNewPeoperGiftButton));
		EventDispatcher.AddListener(EventNames.OnGuildBossStatusNty, new Callback(this.CheckGuildBossTip));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.OnRoleSelfLevelUp));
		EventDispatcher.RemoveListener<int>("GuideManager.TaskFinish", new Callback<int>(this.OnTaskFinish));
		EventDispatcher.RemoveListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.UpdateDataUI));
		EventDispatcher.RemoveListener<bool>("FriendManagerEvents.FriendBadgeTip", new Callback<bool>(this.OnFriendBadgeTip));
		EventDispatcher.RemoveListener<bool>("MailEvents.IsMailReadTipOn", new Callback<bool>(this.OnMailBadgeTip));
		EventDispatcher.RemoveListener<bool>("MarketManager.FleaShopBadgeTip", new Callback<bool>(this.OnFleaShopBadgeTip));
		EventDispatcher.RemoveListener<bool>("MarketManager.FleaShopOpen", new Callback<bool>(this.OnFleaShopOpen));
		EventDispatcher.RemoveListener<bool>("MarketManager.XMarketBadgeTip", new Callback<bool>(this.OnXMarketBadgeTip));
		EventDispatcher.RemoveListener<bool>(EventNames.FirstChargeBadge, new Callback<bool>(this.OnFirstChargeBadge));
		EventDispatcher.RemoveListener(EventNames.BeginNav, new Callback(this.BeginNav));
		EventDispatcher.RemoveListener(EventNames.EndNav, new Callback(this.EndNav));
		EventDispatcher.RemoveListener<int>(EventNames.GetDailyTaskPrize, new Callback<int>(this.OnGetDailyTaskPrize));
		EventDispatcher.RemoveListener(EventNames.ActivityAnnounce, new Callback(this.OnCurrentActivityShow));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildApplication, new Callback(this.UpdateShowGuildApplicationTip));
		EventDispatcher.RemoveListener(EventNames.UpdateEquipRecommend, new Callback(this.UpdateShowRecommendEquip));
		EventDispatcher.RemoveListener(EventNames.UpdateBeInvitedCount, new Callback(this.UpdateTeamBeInvitedUI));
		EventDispatcher.RemoveListener(EventNames.UpadateAskForJoinList, new Callback(this.UpadateAskForJoinList));
		EventDispatcher.RemoveListener(EventNames.UpdateTeamBasicInfo, new Callback(this.UpdateTeamBasicInfo));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.UpdateTeamBasicInfo));
		EventDispatcher.RemoveListener<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, new Callback<ChatManager.ChatInfo>(this.UpdateTeamChatTip));
		EventDispatcher.RemoveListener(EventNames.UpdatePromoteWayTip, new Callback(this.UpdateShowPromoteWay));
		EventDispatcher.RemoveListener(EventNames.OnUpdateGoods, new Callback(this.OnUpdateBackPack));
		EventDispatcher.RemoveListener(EventNames.VipTimeLimitNty, new Callback(this.OnUpdateVipColor));
		EventDispatcher.RemoveListener<int, string, int>(WildBossManagerEvent.OnSingleWaitingNumChanged, new Callback<int, string, int>(this.OnWildBossWaitingNumChanged));
		EventDispatcher.RemoveListener<int, string, int>(WildBossManagerEvent.OnMultiWaitingNumChanged, new Callback<int, string, int>(this.OnWildBossWaitingNumChanged));
		EventDispatcher.RemoveListener(EventNames.PlayTownUIWeaponEffect, new Callback(this.PlayGodWeaponEffect));
		EventDispatcher.RemoveListener("GuideManager.CheckGodWeaponGuide", new Callback(this.PlayGodWeaponEffect));
		EventDispatcher.RemoveListener(EventNames.GoldBuyChangedNty, new Callback(this.CheckBeniftRedPoint));
		EventDispatcher.RemoveListener(EventNames.OnLoginWelfareUpdate, new Callback(this.CheckBeniftRedPoint));
		EventDispatcher.RemoveListener<bool>(EventNames.FlyShoeTransportRes, new Callback<bool>(this.OnFlyShoeTransportRes));
		EventDispatcher.RemoveListener("OnRechargeTipChange", new Callback(this.CheckPayTip));
		EventDispatcher.RemoveListener(EventNames.VipTasteCardNty, new Callback(this.OnUpdateVipTasteCard));
		EventDispatcher.RemoveListener(EventNames.OnGuildWarMisNty, new Callback(this.ShowCanGetGuildWarChampionPrize));
		EventDispatcher.RemoveListener(EventNames.BossBookComeOutTipUpdate, new Callback(this.CheckShowBossBookTip));
		EventDispatcher.RemoveListener(EventNames.RefreshActivityInfo, new Callback(this.ControlSystemOfDayGift));
		EventDispatcher.RemoveListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.RemoveListener(EventNames.OnOpenServerActStatusNty, new Callback(this.ShowOrHideAcOpenServer));
		EventDispatcher.RemoveListener(EventNames.UpdateAcTypeCanGetRewardTip, new Callback(this.CheckAcOpenServerTip));
		EventDispatcher.RemoveListener<NPC>(EventNames.OpenNPCSystem, new Callback<NPC>(this.OnOpenNPCSystem));
		EventDispatcher.RemoveListener<NPC>(EventNames.OpenNPCMenu, new Callback<NPC>(this.OnOpenNPCMenu));
		EventDispatcher.RemoveListener(EventNames.CloseNPCMenu, new Callback(this.OnCloseNPCMenu));
		EventDispatcher.RemoveListener(EventNames.UpdateItemUseRecommendTip, new Callback(this.UpdateItemUseRecommendTip));
		EventDispatcher.RemoveListener(EventNames.RedBagFresh, new Callback(this.OnRedBagFresh));
		EventDispatcher.RemoveListener(EventNames.TramcarInviteFriendNty, new Callback(this.CheckTramcarInviteMessage));
		EventDispatcher.RemoveListener(EventNames.OnNewPeoperGiftPackage, new Callback(this.OnRefreshNewPeoperGiftButton));
		EventDispatcher.RemoveListener(EventNames.OnGuildBossStatusNty, new Callback(this.CheckGuildBossTip));
	}

	private void OnRoleSelfLevelUp()
	{
		this.ControlSystemOpens(true, 0);
		this.OnUpdateBackPack();
		DailyTaskManager.Instance.CheckTownDailyTaskPoint();
		OperateActivityManager.Instance.CheckUpdateMaxLevel();
		this.CheckDownloadExtrPack();
	}

	private void OnTaskFinish(int taskId)
	{
		this.ControlSystemOpens(true, 0);
	}

	private void BeginNav()
	{
		TownUI.fxNavID = FXSpineManager.Instance.ReplaySpine(TownUI.fxNavID, 1204, this.FXNav, "TownUI", 2002, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void EndNav()
	{
		if (TownUI.fxNavID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(TownUI.fxNavID, true);
			TownUI.fxNavID = 0;
		}
	}

	private void UpdateTaskOrTeamState()
	{
		if (this.currentTaskOrTeamState == TaskAndTeamState.TaskState)
		{
			base.FindTransform("MainTaskUIRoot").get_gameObject().SetActive(true);
			base.FindTransform("TeamTownUIRoot").get_gameObject().SetActive(false);
			base.FindTransform("OpenTaskSelect").get_gameObject().SetActive(true);
			base.FindTransform("OpenTeamSelect").get_gameObject().SetActive(false);
		}
		else if (this.currentTaskOrTeamState == TaskAndTeamState.TeamState)
		{
			base.FindTransform("MainTaskUIRoot").get_gameObject().SetActive(false);
			base.FindTransform("TeamTownUIRoot").get_gameObject().SetActive(true);
			base.FindTransform("OpenTaskSelect").get_gameObject().SetActive(false);
			base.FindTransform("OpenTeamSelect").get_gameObject().SetActive(true);
		}
	}

	public override void UpdateDataUI()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		Image component = this.headObj.GetComponent<Image>();
		if (component != null)
		{
			ResourceManager.SetSprite(component, UIUtils.GetRoleHeadIcon(EntityWorld.Instance.EntSelf.TypeID));
		}
		long hpLmt = RoleAttrTool.GetHpLmt(EntityWorld.Instance.EntSelf);
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene && GuildWarManager.Instance.RoleHp != -1L && GuildWarManager.Instance.RoleHp < hpLmt)
		{
			this.num.set_text(string.Format("{0}/{1}", GuildWarManager.Instance.RoleHp, hpLmt));
			this.exp.set_fillAmount((float)((double)GuildWarManager.Instance.RoleHp / (double)hpLmt));
		}
		else
		{
			this.num.set_text(string.Format("{0}/{1}", hpLmt, hpLmt));
			this.exp.set_fillAmount(1f);
		}
		this.bar.set_value((float)EntityWorld.Instance.EntSelf.Exp / (float)EntityWorld.Instance.EntSelf.ExpLmt);
		this.level.set_text(string.Format("{0}", EntityWorld.Instance.EntSelf.Lv));
		UIUtils.ShowImageText(this.fight, EntityWorld.Instance.EntSelf.Fighting, "new_z_zln_", "StrongerUIBtn");
		ResourceManager.SetSprite(this.m_spVIPLevel2, GameDataUtils.GetNumIcon1(EntityWorld.Instance.EntSelf.VipLv, NumType.Yellow_light));
		this.nameLab.set_text(EntityWorld.Instance.EntSelf.Name);
		this.UpdateTeamBasicInfo();
		this.OnUpdateVipColor();
	}

	private void OnUpdateVipColor()
	{
		bool isWhiteBlack = false;
		MonthCardInfoPush limitCardData = VIPManager.Instance.GetLimitCardData();
		if (limitCardData != null && limitCardData.Times < TimeManager.Instance.PreciseServerSecond)
		{
			isWhiteBlack = true;
		}
		ImageColorMgr.SetImageColor(this.m_spVIPLevelBg, isWhiteBlack);
		ImageColorMgr.SetImageColor(this.m_spVIPTitle, isWhiteBlack);
		ImageColorMgr.SetImageColor(this.m_spVIPLevel1, isWhiteBlack);
		ImageColorMgr.SetImageColor(this.m_spVIPLevel2, isWhiteBlack);
		this.OnUpdateLimtCardExpire();
	}

	private void PlayVipBtnEffect()
	{
		this.m_vipBtnFxId = FXSpineManager.Instance.ReplaySpine(this.m_vipBtnFxId, 708, base.FindTransform("VIP"), "TownUI", 2001, null, "UI", 59f, 9f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void UpdateTeamBeInvitedUI()
	{
		base.FindTransform("TeamBeInvitedTip").get_gameObject().SetActive(TeamBasicManager.Instance.BeInviteList.get_Count() > 0);
		if (base.FindTransform("TeamBeInvitedTip").get_gameObject().get_activeSelf())
		{
			TimerHeap.DelTimer(this.teamBeInviteTimerID);
			this.teamBeInviteTimerID = TimerHeap.AddTimer(30000u, 0, delegate
			{
				TeamBasicManager.Instance.TeamInviteTimeOut();
			});
		}
	}

	private void UpadateAskForJoinList()
	{
		Transform transform = base.FindTransform("ButtonOpenTeamBadge");
		if (transform == null)
		{
			return;
		}
		if (TeamBasicManager.Instance.AskForJoinTeamList == null)
		{
			transform.get_gameObject().SetActive(false);
		}
		else
		{
			transform.get_gameObject().SetActive(TeamBasicManager.Instance.AskForJoinTeamList.get_Count() > 0);
		}
	}

	private void UpdateTeamBasicInfo()
	{
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			base.FindTransform("IamLeaderIcon").get_gameObject().SetActive(true);
		}
		else
		{
			this.UpadateAskForJoinList();
			base.FindTransform("IamLeaderIcon").get_gameObject().SetActive(false);
		}
	}

	private void UpdateTeamChatTip(ChatManager.ChatInfo chatInfo)
	{
		Transform transform = base.FindTransform("TeamChatTipRoot");
		int num = 0;
		if (this.currentTaskOrTeamState == TaskAndTeamState.TaskState && chatInfo != null && EntityWorld.Instance.EntSelf.ID != chatInfo.sender_uid)
		{
			if (transform.get_childCount() > num)
			{
				transform.GetChild(num).get_gameObject().SetActive(true);
				ChatInfoBase component = transform.GetChild(num).GetComponent<ChatInfo2Bubble>();
				if (component != null)
				{
					component.ShowInfo(chatInfo);
				}
			}
			else
			{
				GameObject chatInfo2Bubble = ChatManager.Instance.GetChatInfo2Bubble(chatInfo, transform);
				chatInfo2Bubble.set_name("chatInfoBubble");
				chatInfo2Bubble.get_transform().set_localPosition(Vector3.get_zero());
			}
			num++;
		}
		for (int i = num; i < transform.get_childCount(); i++)
		{
			GameObject gameObject = transform.GetChild(i).get_gameObject();
			gameObject.SetActive(false);
		}
	}

	private void UpdateShowGuildApplicationTip()
	{
		if (GuildManager.Instance.ApplicationPlayers != null && GuildManager.Instance.ApplicationPlayers.get_Count() > 0)
		{
			base.FindTransform("guildApplicationTip").get_gameObject().SetActive(true);
		}
		else
		{
			base.FindTransform("guildApplicationTip").get_gameObject().SetActive(false);
		}
	}

	private void UpdateShowPromoteWay()
	{
		if (StrongerManager.Instance.CheckCanShowPromoteWay())
		{
			this.mStrongerFxId = FXSpineManager.Instance.ReplaySpine(this.mStrongerFxId, 610, this.mGoStrongerTips.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mGoStrongerTips.SetActive(true);
		}
		else
		{
			this.RemovePromoteWayFXs();
			this.mGoStrongerTips.SetActive(false);
		}
	}

	private void RemovePromoteWayFXs()
	{
		if (this.mStrongerFxId > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.mStrongerFxId, true);
			this.mStrongerFxId = 0;
		}
	}

	private void UpdateShowRecommendEquip()
	{
		this.equipRecommendRootListPool.Clear();
		if (EquipmentManager.Instance.RecommendDic == null || EquipmentManager.Instance.RecommendDic.get_Count() <= 0 || !EquipmentManager.Instance.ShowRecommendTip)
		{
			return;
		}
		List<KeyValuePair<EquipLibType.ELT, long>> equipRecommendList = EquipGlobal.GetEquipRecommendList(EquipmentManager.Instance.RecommendDic);
		this.equipRecommendRootListPool.Create(1, delegate(int index)
		{
			if (index < 1 && index < this.equipRecommendRootListPool.Items.get_Count())
			{
				EquipRecommendItem component = this.equipRecommendRootListPool.Items.get_Item(index).GetComponent<EquipRecommendItem>();
				EquipLibType.ELT pos = equipRecommendList.get_Item(index).get_Key();
				long equipID = equipRecommendList.get_Item(index).get_Value();
				this.equipRecommendRootListPool.Items.get_Item(index).set_name("Equip_" + (int)pos);
				if (component != null && EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
				{
					int itemID = EquipGlobal.GetEquipCfgIDByEquipID(equipID);
					zZhuangBeiPeiZhiBiao data = DataReader<zZhuangBeiPeiZhiBiao>.Get(itemID);
					Items items = DataReader<Items>.Get(itemID);
					int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos(pos);
					zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByPos);
					Items items2 = DataReader<Items>.Get(equipCfgIDByPos);
					if (data != null && items != null)
					{
						int num = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("checkLv").value);
						bool flag = false;
						if (num <= EntityWorld.Instance.EntSelf.Lv)
						{
							if (data.step > zZhuangBeiPeiZhiBiao.step && items.color >= 4)
							{
								flag = true;
							}
							else if (data.step == zZhuangBeiPeiZhiBiao.step && items.color >= items2.color && items.color >= 4)
							{
								flag = true;
							}
						}
						if (flag)
						{
							flag = false;
							int num2 = 0;
							if (DataReader<zZhuangBeiSheZhi>.Contains("checkNum" + items.color))
							{
								num2 = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("checkNum" + items.color).value);
							}
							int wearingEquipNumByMinColor = EquipGlobal.GetWearingEquipNumByMinColor(items.color);
							if (num2 >= wearingEquipNumByMinColor)
							{
								flag = true;
							}
							if (!flag)
							{
								int num3 = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("rankNum").value);
								int wearingEquipNumByMinStep = EquipGlobal.GetWearingEquipNumByMinStep(data.step);
								if (num3 >= wearingEquipNumByMinStep)
								{
									flag = true;
								}
							}
						}
						if (flag)
						{
							component.UpdateUIData(itemID, "您获得新装备", "查 看", delegate
							{
								LinkNavigationManager.OpenActorUI(delegate
								{
									UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
									EquipDetailedPopUI.Instance.SetSelectEquipTip(pos, false);
								});
							}, false, null, 2000);
						}
						else
						{
							component.UpdateUIData(itemID, "您获得新装备", "替 换", delegate
							{
								if (data.level > EntityWorld.Instance.EntSelf.Lv)
								{
									string text = GameDataUtils.GetChineseContent(510113, false);
									text = text.Replace("{s1}", data.level.ToString());
									UIManagerControl.Instance.ShowToastText(text);
									return;
								}
								EquipmentManager.Instance.SendPutOnEquipmentReq(data.position, equipID, itemID);
							}, false, null, 2000);
						}
						component.FightingContent = EquipmentManager.Instance.GetEquipFightingByEquipID(equipID).ToString();
						component.SetExcellentCount(EquipGlobal.GetExcellentAttrsCountByColor(equipID, 1f));
						EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipID);
						if (equipSimpleInfoByEquipID != null)
						{
							component.SetEquipBinding(equipSimpleInfoByEquipID.binding);
						}
					}
				}
			}
		});
	}

	private void UpdateCheckOffTime()
	{
		this.ClearOffLineRecommendRegion();
		if (!SystemOpenManager.IsSystemOn(64))
		{
			return;
		}
		OffLineLoginPush offHookData = OffHookManager.Instance.GetOffHookData();
		if (offHookData != null)
		{
			if (!OffHookManager.Instance.isInit)
			{
				OffHookManager.Instance.isInit = true;
				bool flag = false;
				if (offHookData.hasTime >= 0 && offHookData.hasTime < 3600)
				{
					flag = true;
				}
				if (offHookData != null && offHookData.daily)
				{
					flag = true;
				}
				if (flag)
				{
					if (!this.CheckLimitCardInBag())
					{
						Transform trans = base.FindTransform("OffLineRecommendRegion");
						OffHookManager.Instance.GetRecommendItem(71005, trans, false);
					}
				}
				else
				{
					this.CheckLimitCardInBag();
				}
				if (offHookData.offTime >= 300)
				{
					LinkNavigationManager.OpenOffHookUI();
					OffHookUI.Instance.ShowResultPanel();
				}
			}
			else
			{
				this.CheckLimitCardInBag();
			}
		}
		else
		{
			this.CheckLimitCardInBag();
		}
	}

	private void ClearOffLineRecommendRegion()
	{
		Transform transform = base.FindTransform("OffLineRecommendRegion");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Transform child = transform.GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
	}

	private bool CheckLimitCardInBag()
	{
		if (!OffHookManager.Instance.IsOnLegalCheckHookTime())
		{
			return true;
		}
		List<int> list = new List<int>();
		list.Add(71005);
		list.Add(71006);
		list.Add(71007);
		for (int i = 0; i < list.get_Count(); i++)
		{
			long num = BackpackManager.Instance.OnGetGoodCount(list.get_Item(i));
			if (num > 0L)
			{
				Transform trans = base.FindTransform("OffLineRecommendRegion");
				OffHookManager.Instance.GetRecommendItem(list.get_Item(i), trans, true);
				return true;
			}
		}
		return false;
	}

	private void CheckVipTasteCard()
	{
		if (!VipTasteCardManager.Instance.isHaveShow)
		{
			int checkId = VipTasteCardManager.Instance.CheckId;
			if (checkId > 0)
			{
				long num = BackpackManager.Instance.OnGetGoodCount(checkId);
				if (num > 0L)
				{
					Transform trans = base.FindTransform("OffLineRecommendRegion");
					VipTasteCardManager.Instance.GetRecommendItem(checkId, trans, true, 1);
					VipTasteCardManager.Instance.isHaveShow = true;
					return;
				}
			}
		}
		this.OnUpdateVipTasteCard();
	}

	private void OnUpdateVipTasteCard()
	{
		this.OnUpdateLimtCardExpire();
		if (!VipTasteCardManager.Instance.isExpireShow)
		{
			int checkId = VipTasteCardManager.Instance.CheckId;
			if (checkId > 0)
			{
				VipTasteCardManager.Instance.isExpireShow = true;
				if (!VIPManager.Instance.IsVIPPrivilegeOn())
				{
					Transform trans = base.FindTransform("OffLineRecommendRegion");
					VipTasteCardManager.Instance.GetRecommendItem(checkId, trans, true, 2);
				}
			}
		}
		this.CheckVipTasteCardTime();
	}

	private void OnUpdateLimtCardExpire()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.VipLv > 0 && !VIPManager.Instance.IsShowLimtCardTimeArrive)
		{
			int checkId = VipTasteCardManager.Instance.CheckId;
			if (checkId > 0)
			{
				Transform trans = base.FindTransform("OffLineRecommendRegion");
				VipTasteCardManager.Instance.GetRecommendItem(checkId, trans, true, 3);
				VIPManager.Instance.IsShowLimtCardTimeArrive = true;
			}
		}
	}

	private void CheckVipTasteCardTime()
	{
		this.ClearTimeCoundDown();
		if (this == null)
		{
			return;
		}
		this.VipTasteRegion.get_gameObject().SetActive(false);
		int num = VipTasteCardManager.Instance.CardTime - TimeManager.Instance.PreciseServerSecond;
		if (num <= 0)
		{
			return;
		}
		this.VipTasteRegion.get_gameObject().SetActive(true);
		this.timeCoundDown = new TimeCountDown(num, TimeFormat.SECOND, delegate
		{
			this.TextVipTasteTime.set_text(TimeConverter.GetTime(this.timeCoundDown.GetSeconds(), TimeFormat.MMSS_Chinese));
		}, delegate
		{
			this.VipTasteRegion.get_gameObject().SetActive(false);
		}, true);
	}

	private void ResetVipTasteRegionState()
	{
		if (this.VipTasteRegion != null)
		{
			this.VipTasteRegion.get_gameObject().SetActive(false);
		}
	}

	private void ClearTimeCoundDown()
	{
		if (this.timeCoundDown != null)
		{
			this.timeCoundDown.Dispose();
			this.timeCoundDown = null;
		}
	}

	private void UpdateFashionTimeoutRecommend()
	{
		FashionManager.Instance.TryShowTimeoutRecommend();
	}

	public void SetFashionTimeoutRecommend(int iconID, string titleName, string itemName = "", string btnName = "", Action BtnCallBack = null, Action BtnCloseCallBack = null)
	{
		Transform transform = this.FashionTimeoutRecommendRegion.FindChild("FashionRecommendItem");
		if (transform == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
			if (!instantiate2Prefab)
			{
				return;
			}
			instantiate2Prefab.get_transform().SetParent(this.FashionTimeoutRecommendRegion);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.set_name("FashionRecommendItem");
			transform = instantiate2Prefab.get_transform();
		}
		else
		{
			transform.get_gameObject().SetActive(true);
		}
		EquipRecommendItem component = transform.GetComponent<EquipRecommendItem>();
		if (!component)
		{
			return;
		}
		component.UpdateUIData(iconID, titleName, itemName, btnName, BtnCallBack, BtnCloseCallBack, true, 2000);
	}

	private void UpdateNewFashionRecommend()
	{
		FashionManager.Instance.TryShowNewFashionRecommend();
	}

	public void SetNewFashionRecommend(string currentNewFashionDataID, int iconID, string titleName, string itemName = "", string btnName = "", Action BtnCallBack = null, Action BtnCloseCallBack = null)
	{
		if (this.lastNewFashionDataID == currentNewFashionDataID)
		{
			if (BtnCloseCallBack != null)
			{
				BtnCloseCallBack.Invoke();
			}
			return;
		}
		this.lastNewFashionDataID = currentNewFashionDataID;
		if (!this.NewFashionRecommendRegion.get_gameObject().get_activeSelf())
		{
			this.NewFashionRecommendRegion.get_gameObject().SetActive(true);
		}
		Transform transform = this.NewFashionRecommendRegion.FindChild("FashionRecommendItem");
		if (transform == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
			if (!instantiate2Prefab)
			{
				return;
			}
			instantiate2Prefab.get_transform().SetParent(this.NewFashionRecommendRegion);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.set_name("FashionRecommendItem");
			transform = instantiate2Prefab.get_transform();
		}
		else
		{
			transform.get_gameObject().SetActive(true);
		}
		EquipRecommendItem component = transform.GetComponent<EquipRecommendItem>();
		if (!component)
		{
			return;
		}
		component.UpdateUIData(iconID, titleName, itemName, btnName, BtnCallBack, BtnCloseCallBack, true, 2000);
	}

	private void TryRemoveTopNewFashionRecommend()
	{
		if (this.NewFashionRecommendRegion && this.NewFashionRecommendRegion.get_gameObject() && this.NewFashionRecommendRegion.get_gameObject().get_activeSelf())
		{
			this.NewFashionRecommendRegion.get_gameObject().SetActive(false);
		}
	}

	private void UpdateItemUseRecommendTip()
	{
		this.itemUseRecommendTipListPool.Clear();
		if (BackpackManager.Instance.ItemCanUseRecommendTipList != null && BackpackManager.Instance.ItemCanUseRecommendTipList.get_Count() > 0)
		{
			List<long> itemList = BackpackManager.Instance.ItemCanUseRecommendTipList;
			this.itemUseRecommendTipListPool.Create(itemList.get_Count(), delegate(int index)
			{
				if (index < itemList.get_Count() && index < this.itemUseRecommendTipListPool.Items.get_Count())
				{
					Goods goods = BackpackManager.Instance.OnGetGood(itemList.get_Item(index));
					EquipRecommendItem component = this.itemUseRecommendTipListPool.Items.get_Item(index).GetComponent<EquipRecommendItem>();
					if (component != null && goods != null)
					{
						int count = BackpackManager.Instance.OnGetGoodCount(itemList.get_Item(index));
						component.UpdateUIData(goods.GetItemId(), GameDataUtils.GetChineseContent(621264, false), "全部使用", delegate
						{
							if (goods.LocalItem.function > 0)
							{
								BackpackManager.Instance.SendUseGood(itemList.get_Item(index), count, goods.LocalItem.promptWay);
							}
						}, true, delegate
						{
							BackpackManager.Instance.ClearItemCanUseTip(itemList.get_Item(index));
						}, 2005);
						if (goods.GetItem().gogok > 0)
						{
							component.SetExcellentCount(goods.GetItem().gogok);
						}
					}
				}
			});
		}
	}

	private void UpdateTalkBubble(bool isShow, int id)
	{
		base.FindTransform("TalkBubble").get_gameObject().SetActive(isShow);
		this.CheckActiveNpcShopButton(isShow, id);
	}

	private void UpdateTaskBubble(bool isShow)
	{
		if (isShow && MainTaskManager.Instance.HasNpcId(MainTaskManager.Instance.ZeroTaskNpcId))
		{
			base.FindTransform("TaskBubble").get_gameObject().SetActive(true);
		}
		else
		{
			base.FindTransform("TaskBubble").get_gameObject().SetActive(false);
		}
	}

	private void UpdateUpgradeBubble(bool isShow)
	{
		base.FindTransform("UpgradeBubble").get_gameObject().SetActive(false);
	}

	private void OnFriendBadgeTip(bool tip)
	{
		base.FindTransform("FriendPoint").get_gameObject().SetActive(tip);
	}

	private void OnMailBadgeTip(bool tip)
	{
		if (this.m_goMailBadge != null)
		{
			this.m_goMailBadge.SetActive(tip);
		}
		this.ShowMailBtn(tip, false);
	}

	private void OnTaskTip(bool tip)
	{
	}

	private void OnBuyGoldBadgeTip()
	{
		base.FindTransform("BuyGoldBadge").get_gameObject().SetActive(GoldBuyManager.Instance.remainingFreeTimes > 0);
	}

	private void OnFleaShopBadgeTip(bool tip)
	{
		if (this.m_goFleaMarketBadge != null)
		{
			this.m_goFleaMarketBadge.SetActive(tip);
		}
	}

	private void OnFleaShopOpen(bool open)
	{
		this.IsFleaMarketOnSelf = open;
		this.ControlSystemOfFleaMarket();
	}

	private void ControlSystemOfFleaMarket()
	{
		if (this.m_goFleaMarket != null)
		{
			this.m_goFleaMarket.SetActive(this.IsFleaMarketOnSelf && this.IsFleaMarketOnSystem);
		}
	}

	private void ControlSystemOfXMarket()
	{
		this.m_goXMarket.SetActive(this.IsXMarketOnSystem);
	}

	private void OnXMarketBadgeTip(bool tip)
	{
		this.m_goXMarketBadge.SetActive(false);
	}

	private void OnFirstChargeBadge(bool tip)
	{
		this.m_goButtonTipsTownUiFirstPay2.SetActive(tip);
	}

	public void ControlSystemOfFirstCharge()
	{
		if (this.m_goFirstPayGiftButton == null)
		{
			return;
		}
		this.m_goFirstPayGiftButton.SetActive(this.IsFirstChargeOnGuide);
		if (this.m_goFirstPayGiftButton.get_activeSelf() && FirstPayManager.Instance.CheckShowSpine())
		{
			this.m_FirstPayFxId = FXSpineManager.Instance.ReplaySpine(this.m_FirstPayFxId, 610, this.m_goFirstPayGiftButton.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.m_FirstPayFxId, true);
			TimerHeap.DelTimer(this.mDelayShowFisrtPlayId);
		}
	}

	public void OnFirstChargeOpen(bool open)
	{
		TownUI.IsFirstChargeOnSelf = open;
		this.ControlSystemOfFirstCharge();
	}

	private void OnDayGiftBadge()
	{
		bool active = SignInManager.Instance.CheckSeverSignBadage();
		this.m_goButtonTipsTownUiDayGift.SetActive(active);
	}

	public void ControlSystemOfDayGift()
	{
		this.m_goDayGiftButton.SetActive(false);
	}

	public void OnGetDailyTaskPrize(int currentDailyID)
	{
	}

	private void SetActivityRecommendVisible(bool isShow = false)
	{
		if (isShow)
		{
			this.activityRecommendRegion.FindChild("ActivityRecommendItem").get_gameObject().SetActive(true);
		}
		if (this.activityRecommendRegion.get_gameObject().get_activeSelf() == isShow)
		{
			return;
		}
		this.activityRecommendRegion.get_gameObject().SetActive(isShow);
	}

	public void OnCurrentActivityShow()
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene || MySceneManager.Instance.IsCurrentGuildFieldScene)
		{
			this.SetActivityRecommendVisible(false);
			return;
		}
		if (ActivityCenterManager.Instance.CurrentACInfoDic != null && ActivityCenterManager.Instance.CurrentACInfoDic.get_Count() > 0)
		{
			ActiveCenterInfo currentActivity = null;
			using (Dictionary<int, ActiveCenterInfo>.Enumerator enumerator = ActivityCenterManager.Instance.CurrentACInfoDic.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, ActiveCenterInfo> current = enumerator.get_Current();
					currentActivity = current.get_Value();
				}
			}
			if (currentActivity == null || currentActivity.status != ActiveCenterInfo.ActiveStatus.AS.Start)
			{
				this.SetActivityRecommendVisible(false);
				return;
			}
			if (!DataReader<HuoDongZhongXin>.Contains(currentActivity.id))
			{
				this.SetActivityRecommendVisible(false);
				return;
			}
			HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(currentActivity.id);
			string text = string.Empty;
			text = ((huoDongZhongXin.activityid != 10007) ? (huoDongZhongXin.activities + "进行中") : GameDataUtils.GetChineseContent(513677, false));
			text = text + "\n" + ActivityCenterManager.Instance.GetFormatOpenTime(huoDongZhongXin, false, false, string.Empty);
			this.SetActivityRecommendVisible(true);
			Transform transform = base.FindTransform("ActivityRecommendItem");
			if (transform == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
				instantiate2Prefab.get_transform().SetParent(this.activityRecommendRegion);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
				instantiate2Prefab.SetActive(true);
				instantiate2Prefab.set_name("ActivityRecommendItem");
				transform = instantiate2Prefab.get_transform();
			}
			EquipRecommendItem component = transform.GetComponent<EquipRecommendItem>();
			component.UpdateUIData(huoDongZhongXin.Icon, huoDongZhongXin.activities, text, GameDataUtils.GetChineseContent(606103, false), delegate
			{
				ActivityCenterManager.Instance.OpenCurrentActivityUI(currentActivity.id);
				ActivityCenterManager.Instance.RemoveCurrentActivityTip(currentActivity.id);
			}, delegate
			{
				ActivityCenterManager.Instance.RemoveCurrentActivityTip(currentActivity.id);
			}, true, 2000);
		}
		else
		{
			this.SetActivityRecommendVisible(false);
		}
	}

	private void RefreshUpdateTips(bool visable, string content = "")
	{
		this.mGotoUpdateButton.GetComponent<ButtonCustom>().set_interactable(visable);
		if (visable)
		{
			this.mGotoUpdateButton.FindChild("Text").GetComponent<Text>().set_text(content);
			OperateActivityManager.Instance.CheckUpdateMaxLevel();
			this.mGotoUpdateButton.get_gameObject().SetActive(visable);
		}
		Vector3 vector = new Vector3((!visable) ? 0f : -372f, 200f, 0f);
		if (base.get_gameObject().get_activeInHierarchy())
		{
			base.StartCoroutine(this.mGotoUpdateButton.MoveTo(vector, 2f, (!visable) ? EaseType.BackIn : EaseType.BackOut, delegate
			{
				if (!visable)
				{
					this.mGotoUpdateButton.get_gameObject().SetActive(visable);
				}
			}));
		}
		else
		{
			this.mGotoUpdateButton.set_localPosition(vector);
			if (!visable)
			{
				this.mGotoUpdateButton.get_gameObject().SetActive(visable);
			}
		}
	}

	private void OnPackageDownloadFinish(int id)
	{
		GengXinYouLi gengXinYouLi = DataReader<GengXinYouLi>.Get(id);
		if (gengXinYouLi != null)
		{
			this.RefreshUpdateTips(true, string.Format(GameDataUtils.GetChineseContent(513159, false), OperateActivityManager.Instance.SwitchChineseNumber(gengXinYouLi.FinishPar)));
		}
	}

	private void OnGetAllUpdateReward()
	{
		this.RefreshUpdateTips(false, string.Empty);
	}

	private void OnUpdateAwardPush()
	{
		if (OperateActivityManager.Instance.LocalUpdateGiftInfos.get_Count() > 0)
		{
			this.RefreshUpdateTips(OperateActivityManager.Instance.IsNeedUpdatePack, GameDataUtils.GetChineseContent(513158, false));
		}
	}

	public void ShowWildBossBubble(bool isShow, Action clickCallback = null)
	{
		if (this.wildBossBubbleGo != null)
		{
			this.wildBossBubbleGo.SetActive(isShow);
		}
		this.ClickButtonWildBossBubbleAction = clickCallback;
	}

	private void OnUpdateBackPack()
	{
		this.CheckBackpackTip();
		if (SystemOpenManager.IsSystemOn(64))
		{
			this.CheckLimitCardInBag();
		}
		this.CheckVipTasteCard();
	}

	protected void OnGuildWarCityMessageBtnClick(GameObject go)
	{
		LinkNavigationManager.OpenGuildWarInfoUI();
	}

	private void CheckTramcarInviteMessage()
	{
		if (!TramcarManager.Instance.IsDontShowAgainBeInvite && base.get_gameObject().get_activeInHierarchy() && TramcarManager.Instance.InviteMessage.get_Count() > 0)
		{
			UIManagerControl.Instance.OpenUI("TramcarBeInviteUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	private void OnClickOpenTask(GameObject go)
	{
		if (this.currentTaskOrTeamState == TaskAndTeamState.TaskState)
		{
			return;
		}
		this.currentTaskOrTeamState = TaskAndTeamState.TaskState;
		this.UpdateTaskOrTeamState();
	}

	private void OnClickOpenTeam(GameObject go)
	{
		if (!SystemOpenManager.IsSystemClickOpen(59, 0, true))
		{
			return;
		}
		if (this.currentTaskOrTeamState != TaskAndTeamState.TeamState)
		{
			this.currentTaskOrTeamState = TaskAndTeamState.TeamState;
			this.UpdateTaskOrTeamState();
			this.UpdateTeamChatTip(null);
		}
		if (TeamBasicManager.Instance.IsHaveTeam())
		{
			TeamBasicUI teamBasicUI = UIManagerControl.Instance.OpenUI("TeamBasicUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TeamBasicUI;
			teamBasicUI.get_transform().SetAsLastSibling();
		}
	}

	private void OnClickGuildBtn(GameObject go)
	{
		LinkNavigationManager.OpenGuildUI(null);
	}

	private void OnClickButtonNetDisconnect(GameObject go)
	{
		NetworkManager.Instance.ShutDownAndReconnectAllServer();
	}

	private void OnClickTask(GameObject go)
	{
		LinkNavigationManager.OpenDailyTaskUI();
	}

	private void OnClickAchievement(GameObject go)
	{
		LinkNavigationManager.OpenRankingUI();
	}

	private void OnClickFight(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("BattleTypeUI", null, true, UIType.FullScreen);
	}

	private void OnClickTeamBeInvited(GameObject go)
	{
		if (TeamBasicManager.Instance.BeInviteList.get_Count() > 0)
		{
			TeamBasicManager.Instance.HandleTeamInvite();
			base.FindTransform("TeamBeInvitedTip").get_gameObject().SetActive(false);
		}
	}

	private void OnClickGuildApplicationTip(GameObject go)
	{
		if (GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AcceptOrRefuseMember))
		{
			UIManagerControl.Instance.OpenUI("GuildApplicationUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	private void OnClickMessage(GameObject go)
	{
		LinkNavigationManager.OpenMailUI();
	}

	private void OnClickSignIn(GameObject go)
	{
		LinkNavigationManager.OpenSignInUI();
	}

	private void OnClickGuildWar(GameObject sender)
	{
	}

	private void OnClickGuildWarRewardUI(GameObject go)
	{
		if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.ELIGIBILITY && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_END)
		{
			GuildWarVSInfoUI guildWarVSInfoUI = UIManagerControl.Instance.OpenUI("GuildWarVSInfoUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarVSInfoUI;
			guildWarVSInfoUI.get_transform().SetAsLastSibling();
		}
		else
		{
			GuildWarRewardUI guildWarRewardUI = UIManagerControl.Instance.OpenUI("GuildWarRewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarRewardUI;
			guildWarRewardUI.get_transform().SetAsLastSibling();
		}
	}

	private void OnClickHead(GameObject go)
	{
		LinkNavigationManager.SystemLink(5, true, null);
	}

	private void OnClickFriend(GameObject go)
	{
		LinkNavigationManager.SystemLink(79, true, null);
	}

	private void OnClickLuckDraw(GameObject go)
	{
		LinkNavigationManager.OpenLuckDrawUI();
	}

	private void OnClickOperateActivity(GameObject go)
	{
		if (!SystemOpenManager.IsSystemClickOpen(12, 0, true))
		{
			return;
		}
		EventDispatcher.Broadcast<int>(EventNames.OpenActivityUI, -1);
	}

	private void OnClickVIP(GameObject go)
	{
		LinkNavigationManager.OpenVIPUI2Privilege();
	}

	private void OnClickDiscountActivity(GameObject go)
	{
		LinkNavigationManager.OpenActivityTossDiscountUI();
	}

	private void OnClickFleaMarket(GameObject sender)
	{
		LinkNavigationManager.OpenFleaMarketUI();
	}

	private void OnClickXMarket(GameObject sender)
	{
		LinkNavigationManager.OpenXMarketUI();
	}

	private void OnClickSettting(GameObject sender)
	{
		LinkNavigationManager.OpenSetttingUI();
	}

	private void OnClickStrongerButton(GameObject sender)
	{
		LinkNavigationManager.OpenStrongerUI();
	}

	private void OnClickBtnFirstPayButton(GameObject go)
	{
		LinkNavigationManager.OpenFirstPayUI(null);
		this.ControlSystemOpens(false, 9);
	}

	private void OnClickPayButton(GameObject go)
	{
		LinkNavigationManager.OpenVIPUI2Recharge();
	}

	private void OnClickBuyGold(GameObject go)
	{
		LinkNavigationManager.SystemLink(29, true, null);
	}

	private void OnClickBtnBounty(GameObject go)
	{
		BountyManager.Instance.isSelectDaily = !BountyManager.Instance.HasOpenedUrgentTask();
		LinkNavigationManager.OpenBountyUI();
	}

	private void OnClickGotoUpdateButton(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("UpdateGiftUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
	}

	private void OnClickButtonChangeCareer(GameObject go)
	{
		LinkNavigationManager.OpenChangeCareerUI();
	}

	private void OnClickRole(GameObject go)
	{
		LinkNavigationManager.SystemLink(5, true, null);
	}

	private void OnClickPet(GameObject go)
	{
		LinkNavigationManager.SystemLink(4, true, null);
	}

	private void OnClickEquip(GameObject go)
	{
		LinkNavigationManager.OpenEquipStrengthenUI(EquipLibType.ELT.Weapon, null);
	}

	private void OnClickGodSoldier(GameObject go)
	{
		LinkNavigationManager.OpenGodSoldierUI();
	}

	private void OnClickGodWeapon(GameObject go)
	{
		LinkNavigationManager.OpenGodWeaponUI();
	}

	private void OnClickSkill(GameObject go)
	{
		LinkNavigationManager.OpenSkillUI(null);
	}

	private void OnClickPackage(GameObject go)
	{
		BackpackManager.Instance.IsCanShowRedPoint = false;
		LinkNavigationManager.OpenBackpackUI(null);
	}

	private void OnClickSurvival(GameObject go)
	{
		LinkNavigationManager.SystemLink(15, true, null);
	}

	private void OnClickPVP(GameObject go)
	{
		LinkNavigationManager.SystemLink(13, true, null);
	}

	private void OnOpenStrongerUI(GameObject go)
	{
		LinkNavigationManager.OpenStrongerUI();
	}

	private void OnClickBossBook(GameObject go)
	{
		LinkNavigationManager.SystemLink(68, true, null);
		BossBookManager.Instance.HideBossBookComeOutTip();
		this.RemoveBossBookFX();
	}

	private void OnClickBtnQuit(GameObject go)
	{
		if (!MySceneManager.Instance.IsCurrentGuildFieldScene && !MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			return;
		}
		if (MySceneManager.Instance.IsCurrentGuildFieldScene)
		{
			EventDispatcher.Broadcast(CityManagerEvent.ExitGuildField);
		}
		else if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			GuildWarManager.Instance.SendLeaveWaitingRoomReq();
		}
	}

	private void OnClickHunt(GameObject go)
	{
		LinkNavigationManager.OpenHuntUI(0);
	}

	private void OnClickOpenAcOpenServer(GameObject go)
	{
		AcOpenServerManager.Instance.IsHideTownFX = true;
		this.RemoveAcOpenServerFX();
		LinkNavigationManager.OpenAcOpenServerUI();
	}

	private void OnClickOpenNewPeoperGift(GameObject go)
	{
		LinkNavigationManager.OpenNewPeoperGiftUI();
	}

	private void OnClickRankUpBtn(GameObject go)
	{
		LinkNavigationManager.OpenRankUpChangeUI();
	}

	private void OnClickGuildAnswerBtn(GameObject go)
	{
		ChatManager.Instance.OpenChatUI(2);
	}

	private void OnClickBenefitsBtn(GameObject go)
	{
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		ButtonInfoData buttonInfoData = new ButtonInfoData();
		buttonInfoData.buttonName = GameDataUtils.GetChineseContent(645009, false);
		buttonInfoData.color = "button_yellow_1";
		buttonInfoData.isShowRedPoint = SignInManager.Instance.ChckeBadage();
		buttonInfoData.onCall = delegate
		{
			LinkNavigationManager.OpenSignInUI();
		};
		ButtonInfoData buttonInfoData2 = new ButtonInfoData();
		buttonInfoData2.buttonName = "点 金";
		buttonInfoData2.color = "button_yellow_1";
		buttonInfoData2.isShowRedPoint = (GoldBuyManager.Instance.remainingFreeTimes > 0);
		buttonInfoData2.onCall = delegate
		{
			LinkNavigationManager.SystemLink(29, true, null);
		};
		ButtonInfoData buttonInfoData3 = new ButtonInfoData();
		buttonInfoData3.buttonName = "离 线";
		buttonInfoData3.color = "button_yellow_1";
		buttonInfoData3.onCall = delegate
		{
			LinkNavigationManager.OpenOffHookUI();
		};
		ButtonInfoData buttonInfoData4 = new ButtonInfoData();
		buttonInfoData4.buttonName = "七日活动";
		buttonInfoData4.color = "button_yellow_1";
		buttonInfoData4.isShowRedPoint = SignInManager.Instance.CheckSeverSignBadage();
		buttonInfoData4.onCall = delegate
		{
			LinkNavigationManager.OpenSevenDayUI();
		};
		if (SystemOpenManager.IsSystemOn(10))
		{
			list.Add(buttonInfoData);
		}
		if (SystemOpenManager.IsSystemOn(29) && SystemConfig.IsOpenPay)
		{
			list.Add(buttonInfoData2);
		}
		if (SystemOpenManager.IsSystemOn(64))
		{
			list.Add(buttonInfoData3);
		}
		if (SystemOpenManager.IsSystemOn(70) && OperateActivityManager.Instance.isSevenDayOn())
		{
			list.Add(buttonInfoData4);
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIView popButtonsAdjustUIView = UIManagerControl.Instance.OpenUI("PopButtonsAdjustUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PopButtonsAdjustUIView;
			popButtonsAdjustUIView.get_transform().set_position(new Vector3(this.m_benifitButton.get_transform().get_position().x, this.m_benifitButton.get_transform().get_position().y, 0f));
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private void OnClickTalkBubble(GameObject go)
	{
		MainTaskManager.Instance.NormalTalkUI();
	}

	private void OnClickTaskBubble(GameObject go)
	{
		LinkNavigationManager.OpenZeroTaskUI();
	}

	private void OnClickUpgradeBubble(GameObject go)
	{
		LinkNavigationManager.OpenRankUpUI();
	}

	private void OnClickButtonWildBossBubble(GameObject go)
	{
		if (this.ClickButtonWildBossBubbleAction != null)
		{
			this.ClickButtonWildBossBubbleAction.Invoke();
		}
	}

	private void OnClickButtonOffHook(GameObject go)
	{
		LinkNavigationManager.OpenOffHookUI();
	}

	private void OnClickShowPromoteWay(GameObject go)
	{
		StrongerManager.Instance.GetPromoteWayButtons();
	}

	private void OnClickBtnDayGiftButton(GameObject go)
	{
		LinkNavigationManager.OpenSevenDayUI();
		this.ControlSystemOpens(false, 75);
	}

	private void OnClickButtonGiftExchangeUI(GameObject go)
	{
		LinkNavigationManager.OpenGiftExchangeUI();
	}

	private void OnClickRedBagBtn(GameObject go)
	{
		if (!this.isCanClickRedBag)
		{
			return;
		}
		this.isCanClickRedBag = false;
		this.SetRedBagTimeCoundDown();
		RedBagManager.Instance.SendGetRedPacketReq();
	}

	public void FinishAnimationClose()
	{
		base.Show(false);
		TownUI.IsOpenAnimationOn = false;
	}

	public override void Show(bool isShow)
	{
		if (isShow)
		{
			base.Show(isShow);
		}
		else
		{
			this.ShowPopAnimation(false);
		}
	}

	private void ShowPopAnimation(bool isShow)
	{
		Animator component = base.GetComponent<Animator>();
		if (!isShow)
		{
			if (TownUI.IsOpenAnimationOn)
			{
				component.set_enabled(true);
				component.Play("TownUI_close", 0, 0f);
				if (CurrenciesUIView.Instance != null)
				{
					CurrenciesUIView.Instance.ShowPopAnimation(false);
				}
				TownUI.IsOpenAnimationOn = false;
			}
			else
			{
				component.set_enabled(false);
				TownUI.IsOpenAnimationOn = false;
				this.FinishAnimationClose();
			}
		}
		else if (TownUI.IsOpenAnimationOn)
		{
			component.set_enabled(true);
			component.Play("TownUI_open", 0, 0f);
			if (CurrenciesUIView.Instance != null)
			{
				CurrenciesUIView.Instance.ShowPopAnimation(true);
			}
			TownUI.IsOpenAnimationOn = false;
		}
		else
		{
			component.set_enabled(true);
			component.Play("TownUI_open", 0, 1f);
			TownUI.IsOpenAnimationOn = false;
		}
	}

	private void SwitchTopRightButton(GameObject go = null)
	{
		this.SwitchTopRightButton(false);
	}

	public void SwitchTopRightButtonToShow()
	{
		if (!this.mIsShowTopRight)
		{
			this.SwitchTopRightButton(true);
		}
	}

	private float GetTopRightButtonX(bool isShow)
	{
		return (!isShow) ? 70f : 800f;
	}

	private void ShowTopRightButtonsInScene(bool isShow)
	{
		if (this.m_BtnSwitchTopRight != null)
		{
			this.m_BtnSwitchTopRight.SetActive(isShow);
		}
		if (this.m_TopRightButtonsMask != null)
		{
			this.m_TopRightButtonsMask.SetActive(isShow);
		}
	}

	private void SwitchTopRightButton(bool rightNow)
	{
		if (base.get_gameObject().get_activeInHierarchy() && !rightNow)
		{
			if (!this.mIsShowTopRight)
			{
				this.mTopRightButtonPanel.get_gameObject().SetActive(true);
			}
			this.DealTopRightButtonOfMask(true);
			Vector3 target = new Vector3(this.GetTopRightButtonX(this.mIsShowTopRight), this.mTopRightButtonPanel.get_anchoredPosition().y);
			base.StartCoroutine(this.mTopRightButtonPanel.MoveTo(target, 0.3f, EaseType.Linear, delegate
			{
				this.mTopRightButtonPanel.get_gameObject().SetActive(this.mIsShowTopRight);
				this.DealTopRightButtonOfMask(false);
			}));
		}
		else
		{
			this.SwitchTopRightButtonRightNow(this.mIsShowTopRight);
		}
		this.m_BtnSwitchTopRight.get_transform().set_localScale(new Vector3((!this.mIsShowTopRight) ? -1f : 1f, 1f, 1f));
		this.DealTopRightButtonOfSpine();
		this.mIsShowTopRight = !this.mIsShowTopRight;
	}

	private void SwitchTopRightButtonRightNow(bool isShow)
	{
		if (this.mTopRightButtonPanel != null)
		{
			Vector3 anchoredPosition3D = new Vector3(this.GetTopRightButtonX(isShow), this.mTopRightButtonPanel.get_anchoredPosition().y);
			this.mTopRightButtonPanel.get_gameObject().SetActive(!isShow);
			this.mTopRightButtonPanel.set_anchoredPosition3D(anchoredPosition3D);
			this.DealTopRightButtonOfMask(false);
		}
	}

	private void DealTopRightButtonOfSpine()
	{
		ActorFXSpine[] componentsInChildren = this.mTopRightButtonPanel.GetComponentsInChildren<ActorFXSpine>(true);
		if (componentsInChildren != null)
		{
			if (!this.mIsShowTopRight)
			{
				this.DelayShowFisrtPlayFX(300u, componentsInChildren);
			}
			else
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].get_gameObject().SetActive(!this.mIsShowTopRight);
				}
			}
		}
	}

	private void DealTopRightButtonOfMask(bool isOn)
	{
		if (this.m_spTopRightButtonsMask != null)
		{
			this.m_spTopRightButtonsMask.set_enabled(isOn);
		}
		if (this.m_maskTopRightButtonsMask != null)
		{
			this.m_maskTopRightButtonsMask.set_enabled(isOn);
		}
	}

	private void ShowMailBtn(bool isShow, bool isForce = false)
	{
		if (isForce)
		{
			if (this.m_goMail != null)
			{
				this.m_goMail.SetActive(isShow);
			}
			return;
		}
		bool isCurrentGuildFieldScene = MySceneManager.Instance.IsCurrentGuildFieldScene;
		bool active = false;
		if (!isCurrentGuildFieldScene)
		{
			active = MailManager.Instance.CheckMailBtnActive();
		}
		if (this.m_goMail != null)
		{
			this.m_goMail.SetActive(active);
		}
	}

	private void ShowExitBtn(bool isShow)
	{
		if (this.m_goQuit != null)
		{
			this.m_goQuit.SetActive(isShow);
		}
		if (isShow && MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			this.m_goQuit.get_transform().set_localPosition(new Vector3(280f, 142.5f, 0f));
		}
		else if (isShow && MySceneManager.Instance.IsCurrentGuildFieldScene)
		{
			this.m_goQuit.get_transform().set_localPosition(new Vector3(430.3f, 134f, 0f));
		}
	}

	private void ShowGodWeaponButton(bool isShow)
	{
		if (this.m_GodWeaponButton != null)
		{
			this.m_GodWeaponButton.SetActive(isShow);
		}
	}

	private void ShowGuildAnswerButton(bool isShow)
	{
		if (this.m_goButtonGuildAnswer != null && this.m_goButtonGuildAnswer.get_activeSelf() != isShow)
		{
			this.m_goButtonGuildAnswer.SetActive(isShow);
		}
		if (isShow)
		{
			this.GuildAnswerTipFXID = FXSpineManager.Instance.ReplaySpine(this.GuildAnswerTipFXID, 610, this.m_goButtonGuildAnswer.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.GuildAnswerTipFXID, true);
		}
	}

	private void DelayShowFisrtPlayFX(uint time, ActorFXSpine[] fxs)
	{
		TimerHeap.DelTimer(this.mDelayShowFisrtPlayId);
		this.mDelayShowFisrtPlayId = TimerHeap.AddTimer(time, 0, delegate
		{
			for (int i = 0; i < fxs.Length; i++)
			{
				fxs[i].get_gameObject().SetActive(true);
			}
		});
	}

	private void SwitchTaskTeamButton(GameObject go = null)
	{
		Vector3 vector = new Vector3(this.GetLeftTeamButtonX(), this.mTaskAndTeamPanel.get_anchoredPosition().y);
		if (base.get_gameObject().get_activeInHierarchy())
		{
			base.StartCoroutine(this.mTaskAndTeamPanel.MoveTo(vector, 0.3f, EaseType.Linear));
		}
		else
		{
			this.mTaskAndTeamPanel.set_localPosition(vector);
		}
		if (!this.mIsShowTeam)
		{
			base.FindTransform("BtnTaskTeamSwitch").FindChild("BtnTaskTeamSwitchRightImage").get_gameObject().SetActive(false);
			base.FindTransform("BtnTaskTeamSwitch").FindChild("BtnTaskTeamSwitchLeftImage").get_gameObject().SetActive(true);
		}
		else
		{
			base.FindTransform("BtnTaskTeamSwitch").FindChild("BtnTaskTeamSwitchRightImage").get_gameObject().SetActive(true);
			base.FindTransform("BtnTaskTeamSwitch").FindChild("BtnTaskTeamSwitchLeftImage").get_gameObject().SetActive(false);
		}
		this.mIsShowTeam = !this.mIsShowTeam;
	}

	private float GetLeftTeamButtonX()
	{
		return (!this.mIsShowTeam) ? 0f : -270f;
	}

	private void ShowTaskAndTeam(bool isShow)
	{
		if (this.mTaskAndTeamPanel != null && this.mTaskAndTeamPanel.get_gameObject() != null)
		{
			this.mTaskAndTeamPanel.get_gameObject().SetActive(isShow);
		}
		if (this.mSwitchTeamArrow != null && this.mSwitchTeamArrow.get_gameObject() != null)
		{
			this.mSwitchTeamArrow.get_gameObject().SetActive(isShow);
		}
	}

	private void SwitchRightMoreButton(GameObject go = null)
	{
		Vector3 vector = new Vector3(this.GetRightMoreButtonX(), this.mMorePanel.get_anchoredPosition().y);
		if (base.get_gameObject().get_activeInHierarchy())
		{
			base.StartCoroutine(this.mMorePanel.MoveTo(vector, 0.3f, EaseType.Linear));
		}
		else
		{
			this.mMorePanel.set_localPosition(vector);
		}
		this.mSwitchMoreArrow.set_localScale(new Vector3((!this.mIsShowMore) ? 1f : -1f, 1f, 1f));
		this.mMorePoint.SetActive(false);
		this.mIsShowMore = !this.mIsShowMore;
	}

	private float GetRightMoreButtonX()
	{
		return (!this.mIsShowMore) ? 272f : -68f;
	}

	public void CloseRightMoreButton()
	{
		this.mIsShowMore = false;
		this.mMorePanel.set_anchoredPosition(new Vector2(272f, this.mMorePanel.get_anchoredPosition().y));
		this.mSwitchMoreArrow.set_localScale(Vector3.get_one());
	}

	private void OnClickRightBottom(GameObject go = null)
	{
		this.IsForceOpenRightBottom = false;
		this.SwitchRightBottom(!this.mIsShowRightBottom, false);
	}

	public void ForceOpenRightBottom()
	{
		this.SwitchRightBottom(true, true);
		this.IsForceOpenRightBottom = true;
		ChatTipUIViewModel.ForceOff();
	}

	public void SwitchRightBottom(bool isShow, bool isRightNow = false)
	{
		if (this.IsForceOpenRightBottom)
		{
			return;
		}
		if (this.mIsShowRightBottom != isShow)
		{
			this.mIsShowRightBottom = isShow;
			this.RefreshRightBottomBgPos(false);
			if (base.get_gameObject().get_activeInHierarchy() && !isRightNow)
			{
				this.DealRightBottomOfMask(true);
				if (isShow)
				{
					this.mRightBottomPanel.get_gameObject().SetActive(true);
				}
				base.StartCoroutine(this.mRightBottomPanel.MoveTo(this.mRightBottomPanelPos, 0.3f, EaseType.Linear, delegate
				{
					this.DealRightBottomOfMask(false);
					this.mRightBottomPanel.get_gameObject().SetActive(isShow);
				}));
				base.StartCoroutine(this.mRightBottomBg.MoveTo(this.mRightBottomBgPos, 0.3f, EaseType.Linear));
			}
			else
			{
				this.mRightBottomPanel.set_anchoredPosition(this.mRightBottomPanelPos);
				this.mRightBottomBg.set_anchoredPosition(this.mRightBottomBgPos);
				this.DealRightBottomOfMask(false);
				this.mRightBottomPanel.get_gameObject().SetActive(isShow);
			}
			base.FindTransform("SwitchBottomLeft").get_gameObject().SetActive(!this.mIsShowRightBottom);
			base.FindTransform("SwitchBottomRight").get_gameObject().SetActive(this.mIsShowRightBottom);
			if (this.mIsShowRightBottom)
			{
				ChatTipUIViewModel.Open(this.m_ChatTipUIRoot, !this.mIsShowRightBottom);
			}
		}
		else
		{
			this.DealRightBottomOfMask(isShow);
			this.mRightBottomPanel.get_gameObject().SetActive(isShow);
		}
	}

	private void DealRightBottomOfMask(bool isOn)
	{
		if (this != null)
		{
			if (this.m_spBottomRightMask != null)
			{
				this.m_spBottomRightMask.set_enabled(isOn);
			}
			if (this.m_maskBottomRightMask != null)
			{
				this.m_maskBottomRightMask.set_enabled(isOn);
			}
		}
	}

	private void RefreshRightBottomBgPos(bool isRightNow = false)
	{
		int num = this.mRightBottomPanel.GetComponentsInChildren<ButtonCustom>().Length * 80;
		this.mRightBottomPanelPos = new Vector2((float)((!this.mIsShowRightBottom) ? 570 : 5), this.mRightBottomPanel.get_anchoredPosition().y);
		this.mRightBottomBgPos = new Vector2((!this.mIsShowRightBottom) ? 570f : (this.mRightBottomBg.get_sizeDelta().x - (float)num - 50f), this.mRightBottomBg.get_anchoredPosition().y);
		if (isRightNow)
		{
			this.mRightBottomPanel.set_anchoredPosition(this.mRightBottomPanelPos);
			this.mRightBottomBg.set_anchoredPosition(this.mRightBottomBgPos);
		}
	}

	private void CheckGodWeaponEffect()
	{
		if (GodWeaponManager.Instance.TownPlayQueue.get_Count() > 0)
		{
			GuideManager.Instance.out_system_lock = true;
			TimerHeap.AddTimer(1500u, 0, new Action(this.PlayGodWeaponEffect));
		}
		this.mGodWeaponBtnFxId = FXSpineManager.Instance.ReplaySpine(this.mGodWeaponBtnFxId, 3903, base.FindTransform("GodWeaponEffect"), "TownUI", 1997, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void PlayGodWeaponEffect()
	{
		if (!GuideManager.Instance.guide_lock && !GodWeaponManager.Instance.WeaponLock)
		{
			if (GodWeaponManager.Instance.TownPlayQueue.get_Count() > 0)
			{
				if (UIManagerControl.Instance.IsOpen("TaskDescUI"))
				{
					UIManagerControl.Instance.HideUI("TaskDescUI");
				}
				if (UIManagerControl.Instance.IsOpen("TalkUI"))
				{
					TalkUI.Instance.CloseNoUnlockGuide();
					UIStackManager.Instance.PopTownUI();
				}
				GuideManager.Instance.out_system_lock = true;
				if (UIManagerControl.Instance.IsOpen("TownUI"))
				{
					GodWeaponManager.Instance.WeaponLock = true;
					GetGodWeaponUI getGodWeaponUI = UIManagerControl.Instance.OpenUI("GetGodWeaponUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as GetGodWeaponUI;
					getGodWeaponUI.SetData(GodWeaponManager.Instance.TownPlayQueue.Dequeue(), new Action(this.PlayGodWeaponEffect));
				}
			}
			else
			{
				this.StopGodWeaponEffect();
				GuideManager.Instance.CheckQueue(true, true);
			}
		}
	}

	private void StopGodWeaponEffect()
	{
		UIManagerControl.Instance.HideUI("GetGodWeaponUI");
		if (GodWeaponManager.Instance.HasWeaponTaskFinish > 0)
		{
			int hasWeaponTaskFinish = GodWeaponManager.Instance.HasWeaponTaskFinish;
			GodWeaponManager.Instance.HasWeaponTaskFinish = 0;
			EventDispatcher.Broadcast<int>("GuideManager.TaskFinish", hasWeaponTaskFinish);
		}
	}

	public void PlayFastTransMask()
	{
		if (!this.mHasFlyShoeReq)
		{
			this.mHasFlyShoeReq = true;
			this.mHasFlyShoeRes = false;
			this.mFlySHoeTimeOut = false;
			this.mFastTransMask.get_gameObject().SetActive(true);
			base.StartCoroutine(this.mFastTransMask.ColorTo(new Color(0f, 0f, 0f, 0.85f), 0.2f, Ease.FromType(EaseType.Linear)));
			TimerHeap.AddTimer(500u, 0, new Action(this.StopFastTransMask));
		}
	}

	private void StopFastTransMask()
	{
		if (this.mHasFlyShoeReq && this.mHasFlyShoeRes)
		{
			this.HideFastTransMask();
			this.mHasFlyShoeReq = false;
			this.mHasFlyShoeRes = false;
			this.mFlySHoeTimeOut = false;
		}
		else
		{
			this.mFlySHoeTimeOut = true;
			TimerHeap.AddTimer(2000u, 0, new Action(this.HideFastTransMask));
		}
	}

	private void HideFastTransMask()
	{
		if (base.get_gameObject() != null && base.get_gameObject().get_activeSelf())
		{
			base.StartCoroutine(this.mFastTransMask.ColorTo(new Color(0f, 0f, 0f, 0f), 0.2f, Ease.FromType(EaseType.Linear)));
			TimerHeap.AddTimer(200u, 0, delegate
			{
				this.mFastTransMask.get_gameObject().SetActive(false);
			});
		}
		else
		{
			this.mFastTransMask.set_color(new Color(0f, 0f, 0f, 0f));
			this.mFastTransMask.get_gameObject().SetActive(false);
		}
	}

	private void OnFlyShoeTransportRes(bool isSuccess)
	{
		this.mHasFlyShoeRes = true;
		if (this.mFlySHoeTimeOut)
		{
			this.StopFastTransMask();
		}
	}

	private void HideBossComeOutTip()
	{
		this.RemoveBossBookFX();
		BossBookManager.Instance.RemoveBossBookCountDown();
	}

	private void CheckShowBossBookTip()
	{
		this.RemoveBossBookFX();
		if (BossBookManager.Instance.CheckBossBookComeOutTip())
		{
			this.AddBossBookFX();
		}
	}

	private void AddBossBookFX()
	{
		this.RemoveBossBookFX();
		this.bossBookFxID = FXSpineManager.Instance.ReplaySpine(this.bossBookFxID, 610, this.m_goBossBook.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveBossBookFX()
	{
		if (this.bossBookFxID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.bossBookFxID, true);
			this.bossBookFxID = 0;
		}
	}

	private void OnGuildWarStepNty()
	{
		this.RemoveGuildWarFX();
		if (GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.FINAL_MATCH_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH1_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH2_BEG)
		{
			this.AddGuildWarFX();
		}
	}

	private void AddGuildWarFX()
	{
		this.RemoveGuildWarFX();
		this.guildWarFxID = FXSpineManager.Instance.ReplaySpine(this.guildWarFxID, 610, this.m_goBtnGuildWar.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveGuildWarFX()
	{
		if (this.guildWarFxID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.guildWarFxID, true);
			this.guildWarFxID = 0;
		}
	}

	private void AddRedBagBtnFx()
	{
		this.RemoveRedBagBtnFx();
		this.redBagBtnFxID = FXSpineManager.Instance.ReplaySpine(this.redBagBtnFxID, 4507, base.FindTransform("RedBagBtnEffect"), "TownUI", 1997, null, "UI", 0f, 0f, 1.5f, 1.5f, false, FXMaskLayer.MaskState.None);
		this.redBagBtnFxID2 = FXSpineManager.Instance.ReplaySpine(this.redBagBtnFxID2, 4506, base.FindTransform("RedBagBtnEffect2"), "TownUI", 2000, null, "UI", 0f, 0f, 3.5f, 3.5f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveRedBagBtnFx()
	{
		if (this.redBagBtnFxID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.redBagBtnFxID, true);
			this.redBagBtnFxID = 0;
		}
		if (this.redBagBtnFxID2 != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.redBagBtnFxID2, true);
			this.redBagBtnFxID2 = 0;
		}
	}

	public void CheckBadge()
	{
		if (BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Actor) || BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Skill) || BackpackManager.Instance.IsCanShowRedPoint || (BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Equip) && !SystemOpenManager.IsSystemHideEntrance(40)) || (BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Pet) && !SystemOpenManager.IsSystemHideEntrance(4)) || (GodSoldierManager.Instance.CheckAllMaterial() && !SystemOpenManager.IsSystemHideEntrance(65)) || GuildBossManager.Instance.IsCallGuildBoss)
		{
			this.mBottomRightPoint.SetActive(true);
		}
		else
		{
			this.mBottomRightPoint.SetActive(false);
		}
		this.mGoRole.get_transform().FindChild("RedPoint").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Actor));
		this.mGoPackage.get_transform().FindChild("RedPoint").get_gameObject().SetActive(BackpackManager.Instance.IsCanShowRedPoint);
		this.mGoSkill.get_transform().FindChild("RedPoint").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Skill));
		this.mGoPet.get_transform().FindChild("RedPoint").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Pet));
		this.mGoEquip.get_transform().FindChild("RedPoint").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Equip));
		this.mGoGodSoldier.get_transform().FindChild("RedPoint").get_gameObject().SetActive(GodSoldierManager.Instance.CheckAllMaterial());
		this.mGoGuild.get_transform().FindChild("RedPoint").get_gameObject().SetActive(GuildBossManager.Instance.IsCallGuildBoss);
		this.CheckInstanceTips();
		this.CheckVipCanGetRewardTip();
		this.CheckDiscountActivityTips();
		InvestFundManager.Instance.CheckCanBuyFlag(false);
		this.CheckPayTip();
		this.SetDailyTaskEffectVisable(DailyTaskManager.Instance.HasLimitTaskOpen);
		DailyTaskManager.Instance.CheckTownDailyTaskPoint();
		this.CheckBeniftRedPoint();
	}

	public void CheckSignBadage()
	{
		Transform transform = base.FindTransform("SignIn");
		if (transform != null)
		{
			transform.FindChild("SignInBadge").get_gameObject().SetActive(SignInManager.Instance.ChckeBadage());
		}
	}

	public void CheckInstanceTips()
	{
		bool isShowTip = DefendFightManager.Instance.IsShowTip;
		base.FindTransform("ButtonTipsTownUiInstanceUI").get_gameObject().SetActive(isShowTip);
	}

	public void CheckDiscountActivityTips()
	{
		base.FindTransform("DiscountActivity").FindChild("ButtonTipsTownUiDiscountActivity").get_gameObject().SetActive(false);
	}

	public void CheckVipCanGetRewardTip()
	{
		bool canShowTips = VIPManager.Instance.GetCanShowTips();
		base.FindTransform("VipButton").FindChild("ButtonTipsTownUiVip").get_gameObject().SetActive(canShowTips);
	}

	public void CheckPayTip()
	{
		bool isShowTipsOfBox = RechargeManager.Instance.IsShowTipsOfBox;
		bool isShowTipsOfCanBuy = InvestFundManager.Instance.IsShowTipsOfCanBuy;
		bool isShowTipsOfCanGet = InvestFundManager.Instance.IsShowTipsOfCanGet;
		bool active = isShowTipsOfBox || isShowTipsOfCanBuy || isShowTipsOfCanGet;
		this.m_goButtonTipsTownUiPay.SetActive(active);
	}

	private void CheckBackpackTip()
	{
		BackpackManager.Instance.CheckBagCanShowRedPoint();
		this.CheckBadge();
	}

	private void CheckAcOpenServerTip()
	{
		base.FindTransform("OpenActivityButtonPoint").get_gameObject().SetActive(AcOpenServerManager.Instance.CheckCanShowAllRedPoint());
	}

	private void CheckGuildBossTip()
	{
		this.CheckBadge();
	}

	public void CheckBeniftRedPoint()
	{
		if (SignInManager.Instance.ChckeBadage() || GoldBuyManager.Instance.remainingFreeTimes > 0 || SignInManager.Instance.CheckSeverSignBadage())
		{
			base.FindTransform("BenefitsButtonPoint").get_gameObject().SetActive(true);
		}
		else
		{
			base.FindTransform("BenefitsButtonPoint").get_gameObject().SetActive(false);
		}
	}

	private void RefreshBadge()
	{
		this.OnFriendBadgeTip(FriendManager.Instance.IsAsksTipOn);
		this.OnMailBadgeTip(MailManager.Instance.IsMailReadTipOn);
		this.OnTaskTip(DailyTaskManager.Instance.IsOperability);
		this.OnFleaShopOpen(MarketManager.Instance.IsFleaShopOpen());
		this.OnFleaShopBadgeTip(MarketManager.Instance.IsFleaShopBadgeTip);
		this.OnFirstChargeBadge(FirstPayManager.Instance.IsPayButRewardNoObtain);
		this.CheckBeniftRedPoint();
	}

	public void ShowTopLeftTabs(bool isShow, params TownUI.TopLeftTabData[] args)
	{
		if (this.TopLeftTabs.get_activeSelf() != isShow)
		{
			this.TopLeftTabs.SetActive(isShow);
		}
		this.ResetStretchInfo();
		if (isShow)
		{
			if (args == null || args.Length == 0)
			{
				for (int i = 0; i < this.TopLeftTabList.get_Count(); i++)
				{
					if (this.TopLeftTabList.get_Item(i).get_gameObject().get_activeSelf())
					{
						this.TopLeftTabList.get_Item(i).get_gameObject().SetActive(false);
					}
				}
			}
			else
			{
				int num = (this.TopLeftTabList.get_Count() >= args.Length) ? args.Length : this.TopLeftTabList.get_Count();
				for (int j = 0; j < this.TopLeftTabList.get_Count(); j++)
				{
					if (j < num)
					{
						if (!this.TopLeftTabList.get_Item(j).get_gameObject().get_activeSelf())
						{
							this.TopLeftTabList.get_Item(j).get_gameObject().SetActive(true);
						}
						this.TopLeftTabList.get_Item(j).SetData(j, args[j].name, new Action<int>(this.OnClickTopLeftTab), args[j].showAction);
						this.AddStretchInfo(args[j].stretchGameObject);
					}
					else if (this.TopLeftTabList.get_Item(j).get_gameObject().get_activeSelf())
					{
						this.TopLeftTabList.get_Item(j).get_gameObject().SetActive(false);
					}
				}
				this.OnClickTopLeftTab(0);
			}
		}
		else
		{
			for (int k = 0; k < this.TopLeftTabList.get_Count(); k++)
			{
				this.TopLeftTabList.get_Item(k).SetData(-1, string.Empty, null, null);
			}
		}
	}

	protected void ResetStretchInfo()
	{
		for (int i = 0; i < this.stretchInfo.Count; i++)
		{
			BaseTweenPostion baseTweenPostion = this.stretchInfo.ElementKeyAt(i);
			if (baseTweenPostion)
			{
				if (baseTweenPostion.get_transform())
				{
					baseTweenPostion.Reset(false, false);
					baseTweenPostion.get_transform().set_localPosition(this.stretchInfo.ElementValueAt(i));
				}
			}
		}
		this.stretchInfo.Clear();
		for (int j = 0; j < this.TopLeftTabList.get_Count(); j++)
		{
			this.AddStretchInfo(this.TopLeftTabList.get_Item(j).get_gameObject());
		}
	}

	protected void AddStretchInfo(GameObject gameObject)
	{
		if (!gameObject)
		{
			return;
		}
		BaseTweenPostion key = gameObject.AddUniqueComponent<BaseTweenPostion>();
		if (!this.stretchInfo.ContainsKey(key))
		{
			this.stretchInfo.Add(key, gameObject.get_transform().get_localPosition());
		}
	}

	protected void OnClickTopLeftTab(int index)
	{
		for (int i = 0; i < this.TopLeftTabList.get_Count(); i++)
		{
			this.TopLeftTabList.get_Item(i).SetClickState(i == index);
		}
	}

	protected void OnClickTabsStretch(GameObject go = null)
	{
		this.isTabsStretchOut = !this.isTabsStretchOut;
		if (this.isTabsStretchOut)
		{
			this.TopLeftTabsBtnLeftImage.SetActive(true);
			this.TopLeftTabsBtnRightImage.SetActive(false);
		}
		else
		{
			this.TopLeftTabsBtnLeftImage.SetActive(false);
			this.TopLeftTabsBtnRightImage.SetActive(true);
		}
		for (int i = 0; i < this.stretchInfo.Count; i++)
		{
			BaseTweenPostion baseTweenPostion = this.stretchInfo.ElementKeyAt(i);
			if (baseTweenPostion)
			{
				if (baseTweenPostion.get_transform())
				{
					if (this.isTabsStretchOut)
					{
						baseTweenPostion.MoveTo(this.stretchInfo.ElementValueAt(i), 0.3f);
					}
					else
					{
						baseTweenPostion.MoveTo(this.stretchInfo.ElementValueAt(i) + this.StretchOffset, 0.3f);
					}
				}
			}
		}
	}

	private void OpenGodWeaponProgressUI()
	{
		this.m_goGodWeaponProgressUI = ResourceManager.GetInstantiate2Prefab("GodWeaponProgressUI");
		UGUITools.SetParent(base.FindTransform("MainTaskUIRoot").get_gameObject(), this.m_goGodWeaponProgressUI, false);
		this.m_goGodWeaponProgressUI.GetComponent<GodWeaponProgressUIView>().AwakeSelf();
	}

	private void ShowGodWeaponProgressUI(bool isShow)
	{
		if (this.m_goGodWeaponProgressUI != null)
		{
			this.m_goGodWeaponProgressUI.SetActive(isShow);
		}
	}

	private void OpenMainTaskUI()
	{
		this.m_goMainTaskRootUI = ResourceManager.GetInstantiate2Prefab("MainTaskUI");
		UGUITools.SetParent(base.FindTransform("MainTaskUIRoot").get_gameObject(), this.m_goMainTaskRootUI, true);
		this.m_goMainTaskRootUI.GetComponent<MainTaskUI>().AwakeSelf();
	}

	private void ShowMainTaskUI(bool isShow)
	{
		if (this.m_goMainTaskRootUI != null)
		{
			this.m_goMainTaskRootUI.SetActive(isShow);
		}
	}

	private void OpenSystemOpenProgressUI()
	{
		this.m_goSystemOpenProgressUI = ResourceManager.GetInstantiate2Prefab("SystemOpenProgressUI");
		UGUITools.SetParent(base.FindTransform("SystemOpenProgressUIRoot").get_gameObject(), this.m_goSystemOpenProgressUI, true);
		this.m_goSystemOpenProgressUI.GetComponent<SystemOpenProgressUIView>().AwakeSelf();
	}

	private void ShowSystemOpenProgressUI(bool isShow)
	{
		if (this.m_goSystemOpenProgressUI != null)
		{
			this.m_goSystemOpenProgressUI.SetActive(isShow);
		}
	}

	private void OpenTeamUI()
	{
		this.m_teamTownUI = ResourceManager.GetInstantiate2Prefab("TeamTownUI");
		UGUITools.SetParent(base.FindTransform("TeamTownUIRoot").get_gameObject(), this.m_teamTownUI, true);
	}

	private void ShowTeamUI(bool isShow)
	{
		if (this.m_teamTownUI != null)
		{
			this.m_teamTownUI.SetActive(isShow);
		}
	}

	private void ShowChatTipUI(bool isShow, bool isUp)
	{
		if (isShow)
		{
			ChatTipUIViewModel.Open(this.m_ChatTipUIRoot, isUp);
		}
		else
		{
			ChatTipUIViewModel.Close();
		}
	}

	private void OpenMiniMap()
	{
		this.m_goMiniMap = ResourceManager.GetInstantiate2Prefab("RadarMinimapUI");
		UGUITools.SetParent(base.FindTransform("RadarMinimapUIRoot").get_gameObject(), this.m_goMiniMap, true);
	}

	private void ShowMiniMap(bool isShow)
	{
		if (this.m_goMiniMap != null)
		{
			this.m_goMiniMap.SetActive(isShow);
		}
	}

	public void ControlSystemOpens(bool refreshAll = true, int refreshSystemId = 0)
	{
		if (refreshAll || refreshSystemId == 6)
		{
			this.IsFleaMarketOnSystem = !SystemOpenManager.IsSystemHideEntrance(6);
			this.ControlSystemOfFleaMarket();
		}
		if (refreshAll || refreshSystemId == 62)
		{
			this.IsXMarketOnSystem = (!SystemOpenManager.IsSystemHideEntrance(62) && SystemConfig.IsOpenPay);
			this.ControlSystemOfXMarket();
		}
		if (refreshAll || refreshSystemId == 7)
		{
			this.m_goLuckDraw.SetActive(!SystemOpenManager.IsSystemHideEntrance(7) && SystemConfig.IsOpenPay);
			LuckDrawManager.Instance.CheckTipsInTownUI();
		}
		if (refreshAll || refreshSystemId == 8)
		{
			this.m_goAchievement.SetActive(!SystemOpenManager.IsSystemHideEntrance(8));
		}
		if (refreshAll || refreshSystemId == 9)
		{
			this.IsFirstChargeOnGuide = (!SystemOpenManager.IsSystemHideEntrance(9) && FirstPayManager.Instance.CheckFirstPayOn() && SystemConfig.IsOpenPay);
			this.ControlSystemOfFirstCharge();
		}
		if (refreshAll || refreshSystemId == 10)
		{
			this.m_goSignIn.SetActive(false);
		}
		if (refreshAll || refreshSystemId == 11)
		{
			this.mGoDailyTask.SetActive(!SystemOpenManager.IsSystemHideEntrance(11) && !MySceneManager.Instance.IsCurrentGuildWarCityScene);
		}
		if (refreshAll || refreshSystemId == 12)
		{
			this.m_goOperateActivity.SetActive(SystemConfig.IsOpenPay && !SystemOpenManager.IsSystemHideEntrance(12));
		}
		if (refreshAll || refreshSystemId == 27)
		{
			this.m_goBtnBounty.SetActive(!SystemOpenManager.IsSystemHideEntrance(27));
			if (this.m_goBtnBounty.get_activeSelf() && BountyManager.Instance.HasOpenedUrgentTask())
			{
				this.m_goBtnBountyFxId = FXSpineManager.Instance.ReplaySpine(this.m_goBtnBountyFxId, 1207, this.m_goBtnBounty.get_transform(), "TownUI", 2001, null, "UI", 21.7f, 2.4f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
		if (refreshAll || refreshSystemId == 30)
		{
		}
		if (refreshAll || refreshSystemId == 32)
		{
		}
		if (refreshAll || refreshSystemId == 34)
		{
			this.IsChangeCareerOnSystem = SystemOpenManager.IsSystemOn(34);
			this.ControlSystemOfChangeCareer();
		}
		if (refreshAll || refreshSystemId == 41)
		{
		}
		if (refreshAll || refreshSystemId == 45)
		{
			base.FindTransform("ButtonGuild").get_gameObject().SetActive(!SystemOpenManager.IsSystemHideEntrance(45));
		}
		if (refreshAll || refreshSystemId == 21)
		{
			this.mGoEquip.SetActive(!SystemOpenManager.IsSystemHideEntrance(21));
		}
		if (refreshAll || refreshSystemId == 3)
		{
			this.mGoSkill.SetActive(!SystemOpenManager.IsSystemHideEntrance(3));
		}
		if (refreshAll || refreshSystemId == 4)
		{
			this.mGoPet.SetActive(!SystemOpenManager.IsSystemHideEntrance(4));
		}
		if (refreshAll || refreshSystemId == 15)
		{
			this.mGoSurvival.SetActive(!SystemOpenManager.IsSystemHideEntrance(15));
		}
		if (refreshAll || refreshSystemId == 13)
		{
			this.mGoPVP.SetActive(!SystemOpenManager.IsSystemHideEntrance(13));
		}
		if (refreshAll || refreshSystemId == 65)
		{
			this.mGoGodSoldier.SetActive(!SystemOpenManager.IsSystemHideEntrance(65));
		}
		if (refreshAll || refreshSystemId == 33)
		{
			this.mGoPayButton.SetActive(SystemConfig.IsOpenPay && !SystemOpenManager.IsSystemHideEntrance(33));
		}
		if (refreshAll || refreshSystemId == 64)
		{
			base.FindTransform("ButtonOffHook").get_gameObject().SetActive(false);
		}
		if (refreshAll || refreshSystemId == 68)
		{
			this.m_goBossBook.SetActive(!SystemOpenManager.IsSystemHideEntrance(68));
		}
		if (refreshAll || refreshSystemId == 75)
		{
			this.ControlSystemOfDayGift();
		}
		if (refreshAll || refreshSystemId == 74)
		{
			base.FindTransform("Hunt").get_gameObject().SetActive(!SystemOpenManager.IsSystemHideEntrance(74));
		}
		if (refreshAll || refreshSystemId == 76)
		{
			this.m_goBtnGuildWar.SetActive(!SystemOpenManager.IsSystemHideEntrance(76));
		}
		if (refreshAll || refreshSystemId == 113)
		{
			this.m_RankUpChange.SetActive(!SystemOpenManager.IsSystemHideEntrance(113));
		}
		if (refreshAll || refreshSystemId == 114)
		{
			base.FindTransform("NewPeoperGiftButton").get_gameObject().SetActive(!NewPeoperGiftPackageManager.Instance.IsBuyFinish);
		}
		this.RefreshRightBottomBgPos(true);
	}

	private void ControlSystemOfChangeCareer()
	{
		bool flag = !ChangeCareerManager.Instance.IsCareerChanged();
		this.m_goButtonChangeCareer.get_gameObject().SetActive(flag && this.IsChangeCareerOnSystem);
	}

	public void MoveAreaButtons(int area, List<Transform> buttons, List<Vector2> offsets)
	{
		if (buttons == null || buttons.get_Count() == 0)
		{
			return;
		}
		Vector2 vector = Vector2.get_zero();
		if (area != 2)
		{
			if (area == 3)
			{
				vector = this.m_posButtonsTopRightBase.get_anchoredPosition();
			}
		}
		else
		{
			vector = this.m_posButtonsLeftBase.get_anchoredPosition();
		}
		EaseType ease = EaseType.Linear;
		for (int i = 0; i < buttons.get_Count(); i++)
		{
			RectTransform rectTransform = buttons.get_Item(i) as RectTransform;
			Vector2 vector2 = vector + offsets.get_Item(i);
			if (base.get_gameObject().get_activeInHierarchy())
			{
				base.StartCoroutine(rectTransform.MoveToAnchoredPosition(vector2, 2f, ease, null));
			}
			else
			{
				rectTransform.set_anchoredPosition(vector2);
			}
		}
	}

	private void CheckExitGuildField()
	{
		if (MySceneManager.Instance.IsCurrentGuildFieldScene && !GuildManager.Instance.IsGuildFieldOpen)
		{
			GuildManager.Instance.ShowForceExitGuildFieldUI();
		}
	}

	protected void InitWildBossWaiting()
	{
		if (WildBossManager.Instance.IsSingleBossWaiting)
		{
			this.OnWildBossWaitingNumChanged(WildBossManager.Instance.SingleBossWaitingBossRank, WildBossManager.Instance.SingleBossWaitingBossName, WildBossManager.Instance.SingleBossWaitingNum);
		}
		else if (WildBossManager.Instance.IsMultiBossWaiting)
		{
			this.OnWildBossWaitingNumChanged(WildBossManager.Instance.MultiBossWaitingBossRank, WildBossManager.Instance.MultiBossWaitingBossName, WildBossManager.Instance.MultiBossWaitingNum);
		}
	}

	protected void OnWildBossWaitingNumChanged(int rank, string name, int curWaitingNum)
	{
		if (curWaitingNum < 0)
		{
			this.HideWildBossWaiting();
		}
		else
		{
			this.SetWildBossWaitingNum(rank, name, curWaitingNum);
		}
	}

	protected void SetWildBossWaitingNum(int rank, string name, int curWaitingNum)
	{
		if (!this.wildBossQueueInfoBtn.get_activeSelf())
		{
			this.wildBossQueueInfoBtn.SetActive(true);
		}
		this.wildBossQueueInfoBtnText.set_text(string.Format(GameDataUtils.GetChineseContent(505172, false), rank, name));
		this.wildBossQueueInfoBtnNum.set_text(string.Format(GameDataUtils.GetChineseContent(505173, false), curWaitingNum));
	}

	protected void HideWildBossWaiting()
	{
		this.wildBossQueueInfoBtn.SetActive(false);
	}

	protected void OnClickWildBossQueue(GameObject go)
	{
		EventDispatcher.Broadcast(WildBossManagerEvent.ShowQueue);
	}

	private void ShowCanGetGuildWarChampionPrize()
	{
		bool isCanGetChampionPrize = GuildWarManager.Instance.IsCanGetChampionPrize;
		if (this.m_goBtnGuildWar.get_transform().FindChild("GuildWarButtonPoint").get_gameObject().get_activeSelf() != isCanGetChampionPrize)
		{
			this.m_goBtnGuildWar.get_transform().FindChild("GuildWarButtonPoint").get_gameObject().SetActive(isCanGetChampionPrize);
		}
	}

	private void ShowCurResoucePoint(bool isShow)
	{
		if (this.m_CurResourcePoint != null)
		{
			this.m_CurResourcePoint.SetActive(isShow);
		}
	}

	public void SetCurResoucePointNum(string text)
	{
		if (this.m_CurResourcePointNum != null)
		{
			this.m_CurResourcePointNum.set_text(text);
		}
	}

	private void ShowGuildWarCityResouce(bool isShow)
	{
		if (this.m_GuildWarCityResouce != null)
		{
			this.m_GuildWarCityResouce.SetActive(isShow);
		}
	}

	public void SetGuildWarCityResouceInfo(GuildResourceInfo myGuildInfo, GuildResourceInfo enemyGuildInfo)
	{
		if ((myGuildInfo != null && myGuildInfo.IsLeft) || (enemyGuildInfo != null && !enemyGuildInfo.IsLeft))
		{
			this.SetGuildWarCityResouceLeftInfo(myGuildInfo, "3FCB19");
			this.SetGuildWarCityResouceRightInfo(enemyGuildInfo, "FF4141");
		}
		else
		{
			this.SetGuildWarCityResouceLeftInfo(enemyGuildInfo, "FF4141");
			this.SetGuildWarCityResouceRightInfo(myGuildInfo, "3FCB19");
		}
	}

	public void SetGuildWarCityResouceLeftInfo(GuildResourceInfo guildInfo, string color = "3FCB19")
	{
		if (guildInfo == null)
		{
			this.SetGuildWarCityResouceLeftTeamName(GameDataUtils.GetChineseContent(515082, false), "3FCB19");
			this.SetGuildWarCityResouceLeftTeamNum(GameDataUtils.GetChineseContent(515078, false));
			this.SetGuildWarCityResouceLeftTeamBar(0f);
			this.SetGuildWarCityResouceLeftTeamBarText(GameDataUtils.GetChineseContent(515082, false));
		}
		else
		{
			this.SetGuildWarCityResouceLeftTeamName(guildInfo.GuildName, color);
			this.SetGuildWarCityResouceLeftTeamNum((guildInfo.GuildMemberNum >= 0) ? string.Format(GameDataUtils.GetChineseContent(515079, false), guildInfo.GuildMemberNum.ToString()) : GameDataUtils.GetChineseContent(515078, false));
			this.SetGuildWarCityResouceLeftTeamBar((float)guildInfo.TotalResourceNum / (float)guildInfo.MaxResourceNum);
			this.SetGuildWarCityResouceLeftTeamBarText(string.Format(GameDataUtils.GetChineseContent(515080, false), guildInfo.TotalResourceNum, guildInfo.MaxResourceNum));
		}
	}

	public void SetGuildWarCityResouceRightInfo(GuildResourceInfo guildInfo, string color = "3FCB19")
	{
		if (guildInfo == null)
		{
			this.SetGuildWarCityResouceLeftTeamName(GameDataUtils.GetChineseContent(515082, false), "3FCB19");
			this.SetGuildWarCityResouceLeftTeamNum(GameDataUtils.GetChineseContent(515078, false));
			this.SetGuildWarCityResouceLeftTeamBar(0f);
			this.SetGuildWarCityResouceLeftTeamBarText(GameDataUtils.GetChineseContent(515082, false));
		}
		else
		{
			this.SetGuildWarCityResouceRightTeamName(guildInfo.GuildName, color);
			this.SetGuildWarCityResouceRightTeamNum((guildInfo.GuildMemberNum >= 0) ? string.Format(GameDataUtils.GetChineseContent(515079, false), guildInfo.GuildMemberNum.ToString()) : GameDataUtils.GetChineseContent(515078, false));
			this.SetGuildWarCityResouceRightTeamBar((float)guildInfo.TotalResourceNum / (float)guildInfo.MaxResourceNum);
			this.SetGuildWarCityResouceRightTeamBarText(string.Format(GameDataUtils.GetChineseContent(515080, false), guildInfo.TotalResourceNum, guildInfo.MaxResourceNum));
		}
	}

	private void SetGuildWarCityResouceLeftTeamName(string text, string color = "3FCB19")
	{
		if (this.m_GuildWarCityResouceLeftTeamName != null)
		{
			this.m_GuildWarCityResouceLeftTeamName.set_text(string.Concat(new string[]
			{
				"<color=#",
				color,
				">",
				text,
				"</color>"
			}));
		}
	}

	private void SetGuildWarCityResouceLeftTeamNum(string text)
	{
		if (this.m_GuildWarCityResouceLeftTeamNum != null)
		{
			this.m_GuildWarCityResouceLeftTeamNum.set_text(text);
		}
	}

	private void SetGuildWarCityResouceLeftTeamBar(float percentage)
	{
		if (this.m_GuildWarCityResouceLeftTeamBar != null)
		{
			this.m_GuildWarCityResouceLeftTeamBar.set_sizeDelta(new Vector2(215f * percentage, this.m_GuildWarCityResouceLeftTeamBar.get_sizeDelta().y));
		}
	}

	private void SetGuildWarCityResouceLeftTeamBarText(string text)
	{
		if (this.m_GuildWarCityResouceLeftTeamBarText != null)
		{
			this.m_GuildWarCityResouceLeftTeamBarText.set_text(text);
		}
	}

	private void SetGuildWarCityResouceRightTeamName(string text, string color = "3FCB19")
	{
		if (this.m_GuildWarCityResouceRightTeamName != null)
		{
			this.m_GuildWarCityResouceRightTeamName.set_text(string.Concat(new string[]
			{
				"<color=#",
				color,
				">",
				text,
				"</color>"
			}));
		}
	}

	private void SetGuildWarCityResouceRightTeamNum(string text)
	{
		if (this.m_GuildWarCityResouceRightTeamNum != null)
		{
			this.m_GuildWarCityResouceRightTeamNum.set_text(text);
		}
	}

	private void SetGuildWarCityResouceRightTeamBar(float percentage)
	{
		if (this.m_GuildWarCityResouceRightTeamBar != null)
		{
			this.m_GuildWarCityResouceRightTeamBar.set_sizeDelta(new Vector2(215f * percentage, this.m_GuildWarCityResouceRightTeamBar.get_sizeDelta().y));
		}
	}

	private void SetGuildWarCityResouceRightTeamBarText(string text)
	{
		if (this.m_GuildWarCityResouceRightTeamBarText != null)
		{
			this.m_GuildWarCityResouceRightTeamBarText.set_text(text);
		}
	}

	public void ShowGuildWarBubble(bool isShow, Action clickCallback = null)
	{
		this.m_GuildWarBubbleGo.SetActive(isShow);
		this.ClickButtonGuildWarBubbleAction = clickCallback;
	}

	private void OnClickButtonGuildWarBubble(GameObject go)
	{
		if (this.ClickButtonGuildWarBubbleAction != null)
		{
			this.ClickButtonGuildWarBubbleAction.Invoke();
		}
	}

	public void ShowTransactionBubble(bool isShow, Action clickCallback = null)
	{
		this.m_transactionBubbleGo.SetActive(isShow);
		this.ClickButtonTransactionBubbleAction = clickCallback;
	}

	private void OnClickButtonTransactionBubble(GameObject go)
	{
		if (this.ClickButtonTransactionBubbleAction != null)
		{
			this.ClickButtonTransactionBubbleAction.Invoke();
		}
	}

	private void ShowGuildWarCityTimeCountDown(bool isShow)
	{
		this.ClearGuildWarCityTimeCountDown();
		if (isShow)
		{
			this.SetGuildWarCityTimeCoundDown();
		}
	}

	private void SetGuildWarCityTimeCoundDown()
	{
		this.ClearGuildWarCityTimeCountDown();
		int guildWarMatchEndSecond = GuildWarManager.Instance.GetGuildWarMatchEndSecond();
		this.guildWarCityTimeCD = new TimeCountDown(guildWarMatchEndSecond, TimeFormat.HHMMSS, delegate
		{
			if (this.guildWarCityTimeCD != null)
			{
				this.SetGuildWarCityTimeCountDownText(this.guildWarCityTimeCD.GetTime());
			}
		}, delegate
		{
			this.ClearGuildWarCityTimeCountDown();
			if (this.m_GuildWarCityTimeCDText != null)
			{
				this.m_GuildWarCityTimeCDText.set_text(GameDataUtils.GetChineseContent(513538, false));
			}
		}, true);
	}

	private void SetGuildWarCityTimeCountDownText(string second)
	{
		if (this.m_GuildWarCityTimeCDText != null)
		{
			this.m_GuildWarCityTimeCDText.set_text("军团争霸剩余： " + second);
		}
	}

	private void ClearGuildWarCityTimeCountDown()
	{
		if (this.guildWarCityTimeCD != null)
		{
			this.guildWarCityTimeCD.Dispose();
			this.guildWarCityTimeCD = null;
		}
	}

	public void SetDailyTaskEffectVisable(bool isVisable)
	{
		if (isVisable)
		{
			this.DailyTaskEffectId = FXSpineManager.Instance.ReplaySpine(this.DailyTaskEffectId, 610, this.mGoDailyTask.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.DailyTaskEffectId, true);
		}
	}

	private void ShowMineAndReportUI(bool isShow)
	{
		if (isShow)
		{
			MineAndReportUI mineAndReportUI = UIManagerControl.Instance.OpenUI("MineAndReportUI", this.m_MineAndReportUISlot, false, UIType.NonPush) as MineAndReportUI;
			if (mineAndReportUI)
			{
				this.ShowTopLeftTabs(true, new TownUI.TopLeftTabData[]
				{
					new TownUI.TopLeftTabData
					{
						name = GameDataUtils.GetChineseContent(515116, false),
						showAction = new Action<bool>(mineAndReportUI.ShowMine),
						stretchGameObject = this.m_MineAndReportUISlot.get_gameObject()
					},
					new TownUI.TopLeftTabData
					{
						name = GameDataUtils.GetChineseContent(515117, false),
						showAction = new Action<bool>(mineAndReportUI.ShowReport),
						stretchGameObject = this.m_MineAndReportUISlot.get_gameObject()
					}
				});
			}
		}
		else if (UIManagerControl.Instance.IsOpen("MineAndReportUI"))
		{
			UIManagerControl.Instance.HideUI("MineAndReportUI");
		}
	}

	private void ShowOrHideAcOpenServer()
	{
		if (this.m_goBtnOpenActivity.get_activeSelf() != AcOpenServerManager.Instance.IsOpenActivity)
		{
			this.m_goBtnOpenActivity.SetActive(AcOpenServerManager.Instance.IsOpenActivity);
		}
		this.CheckAcOpenServerTip();
		if (AcOpenServerManager.Instance.IsOpenActivity && !AcOpenServerManager.Instance.IsHideTownFX)
		{
			this.AddAcOpenServerFX();
		}
		else
		{
			this.RemoveAcOpenServerFX();
		}
	}

	private void AddAcOpenServerFX()
	{
		this.AcOpenServerFXID = FXSpineManager.Instance.ReplaySpine(this.AcOpenServerFXID, 610, this.m_goBtnOpenActivity.get_transform(), "TownUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveAcOpenServerFX()
	{
		if (this.AcOpenServerFXID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.AcOpenServerFXID, true);
		}
	}

	private void OnOpenNPCMenu(NPC npcData)
	{
		if (npcData == null || npcData.function.get_Count() <= 0)
		{
			return;
		}
		int num = npcData.function.get_Item(0);
		if (npcData.function.get_Count() > 1)
		{
			int num2 = npcData.function.get_Item(1);
		}
		if (num == 104)
		{
			this.UpdateTaskBubble(true);
		}
		else if (num == 107)
		{
			MainTaskManager.Instance.UpgradeTaskNpcId = npcData.id;
			this.UpdateUpgradeBubble(true);
		}
		else if (num == TransactionNPCManager.Instance.SystemId)
		{
			if (TransactionNPCManager.Instance.CheckIsNpcRandomShop(TransactionNPCManager.Instance.CurrentShopId))
			{
				this.ShowTransactionBubble(true, new Action(this.OnClickRandomNpcShop));
			}
			else
			{
				this.ShowTransactionBubble(true, new Action(this.OnClickStationaryNpcShop));
			}
		}
	}

	private void OnCloseNPCMenu()
	{
		this.UpdateTaskBubble(false);
		this.UpdateUpgradeBubble(false);
		this.ShowTransactionBubble(false, null);
	}

	private void OnOpenNPCSystem(NPC npcData)
	{
		if (npcData != null && npcData.function.get_Count() > 0)
		{
			int num = npcData.function.get_Item(0);
			if (num == 104)
			{
				if (EntityWorld.Instance.EntSelf.IsNavigating || CityManager.Instance.NeedDelayEnterNPC)
				{
					UIManagerControl.Instance.HideUI("ChatUI");
					LinkNavigationManager.OpenZeroTaskUI();
				}
			}
			else if (num == TransactionNPCManager.Instance.SystemId)
			{
				if (TransactionNPCManager.Instance.CheckIsNpcRandomShop(TransactionNPCManager.Instance.CurrentShopId))
				{
					this.OnClickRandomNpcShop();
				}
				else
				{
					this.OnClickStationaryNpcShop();
				}
			}
		}
	}

	private void OnRedBagFresh()
	{
		bool flag = RedBagManager.Instance.CheckRedBagActive();
		int redBagNum = RedBagManager.Instance.GetRedBagNum();
		this.m_RedBagBtn.SetActive(flag);
		if (flag)
		{
			this.AddRedBagBtnFx();
		}
		else
		{
			this.RemoveRedBagBtnFx();
		}
		this.m_RedBagBtn.get_transform().FindChild("RedBagCountLab").GetComponent<Text>().set_text(redBagNum.ToString());
		this.CheckRedBagTime();
	}

	private void CheckRedBagTime()
	{
		this.ClearRedBagTimeCountDown();
		if (TownUI.Instance == null)
		{
			return;
		}
		int redBagLeftTime = RedBagManager.Instance.GetRedBagLeftTime();
		if (redBagLeftTime <= 0)
		{
			return;
		}
		this.redBagTimeCoundDown = new TimeCountDown(redBagLeftTime, TimeFormat.SECOND, delegate
		{
			this.TextRedBagTime.set_text(TimeConverter.GetTime(this.redBagTimeCoundDown.GetSeconds(), TimeFormat.MMSS_Chinese));
		}, delegate
		{
			this.TextRedBagTime.set_text(string.Empty);
		}, true);
	}

	private void ClearRedBagTimeCountDown()
	{
		if (this.redBagTimeCoundDown != null)
		{
			this.redBagTimeCoundDown.Dispose();
			this.redBagTimeCoundDown = null;
		}
	}

	private void SetRedBagTimeCoundDown()
	{
		this.ClearRedBagClickTimeCountDown();
		int remainTime = 2;
		this.redBagTimeCD = new TimeCountDown(remainTime, TimeFormat.SECOND, delegate
		{
			remainTime--;
			if (remainTime < 1)
			{
				this.isCanClickRedBag = true;
				this.ClearRedBagClickTimeCountDown();
			}
		}, delegate
		{
			this.isCanClickRedBag = true;
			this.ClearRedBagClickTimeCountDown();
		}, true);
	}

	private void ClearRedBagClickTimeCountDown()
	{
		if (this.redBagTimeCD != null)
		{
			this.redBagTimeCD.Dispose();
			this.redBagTimeCD = null;
		}
	}

	public void OnClickRandomNpcShop()
	{
		TransactionNPCManager.Instance.OpenRandomNpcShop();
	}

	public void OnClickStationaryNpcShop()
	{
		TransactionNPCManager.Instance.OpenStationaryNpcShop();
	}

	public void OnRefreshNewPeoperGiftButton()
	{
		this.ControlSystemOpens(false, 114);
	}

	private void CheckActiveNpcShopButton(bool isShow, int npcId)
	{
		if (npcId == 0)
		{
			return;
		}
		NPC nPC = DataReader<NPC>.Get(npcId);
		if (nPC != null && nPC.function != null && nPC.function.get_Count() > 0 && nPC.function.get_Item(0) == TransactionNPCManager.Instance.SystemId)
		{
			this.ShowTransactionBubble(isShow, null);
		}
	}

	protected void TryShowRankUIChangeUI()
	{
		RankUpChangeManager.Instance.TryShowStageFinishUI();
		RankUpChangeManager.Instance.TryShowCareerFinishUI();
	}

	private void CheckDownloadExtrPack()
	{
		if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Lv >= 30 && !GameManager.Instance.CurrentUpdateManager.IsExtendDownloaded())
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621270, false), "继续游戏需下载后续资源，稍等片刻享受更精彩游戏内容。", delegate
			{
				GameManager.Instance.CurrentUpdateManager.Resume(true, null);
			}, null, false, "确定", "button_orange_1", null);
		}
	}
}
