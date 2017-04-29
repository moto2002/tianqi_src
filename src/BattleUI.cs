using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
	public class BattleBtnKeys
	{
		public const int Attack = 1;

		public const int Roll = 8;

		public const int Skill1 = 11;

		public const int Skill2 = 12;

		public const int Skill3 = 13;

		public const int Fuse1 = 21;

		public const int Fuse2 = 22;

		public const int Fuse3 = 23;
	}

	public struct TopLeftTabData
	{
		public string name;

		public Action<bool> showAction;

		public GameObject stretchGameObject;
	}

	public enum RankRewardType
	{
		Text,
		Icon
	}

	protected const string BloodBar = "fight_boss_bloodbar";

	protected const int selfMaxActPoint = 10;

	public static BattleUI Instance;

	protected GameObject Self;

	protected Image SelfHead;

	protected Text SelfLv;

	protected Text SelfName;

	protected GameObject SelfVip;

	protected Image SelfVipLevelIcon0;

	protected Image SelfVipLevelIcon1;

	protected Transform SelfFightingIcon;

	protected Text SelfBloodText;

	protected Image SelfBlood;

	protected int selfBloodBlinkFxID;

	protected float selfBloodBlinkPercentage;

	protected GameObject Adversary;

	protected Image AdversaryHead;

	protected Text AdversaryLv;

	protected Image AdversaryBlood;

	protected Text AdversaryBloodText;

	protected GameObject Boss;

	protected Image BossHead;

	protected Text BossLv;

	protected Text BossName;

	protected Image BossBloodDown;

	protected Image BossBloodUp;

	protected Text BossBloodBarNum;

	protected Text BossBloodText;

	protected Transform BossBloodFx;

	protected Transform BossBloodBarNumListIcon;

	protected Image BossTired;

	protected int bossWeakFxID;

	protected XDict<int, SkillButton> skillBtn = new XDict<int, SkillButton>();

	protected List<GameObject> guideBtn = new List<GameObject>();

	protected XDict<int, Image> skillBtnIcon = new XDict<int, Image>();

	protected XDict<int, Image> skillBtnLock = new XDict<int, Image>();

	protected XDict<int, CDControl> skillBtnCD = new XDict<int, CDControl>();

	protected int attackClickFxID;

	protected int rollClickFxID;

	protected int skill1ClickFxID;

	protected int skill2ClickFxID;

	protected int skill3ClickFxID;

	protected uint pressBtnAttackKeyTimer = 4294967295u;

	protected uint pressBtnAttackKeyDelayBegin = 150u;

	protected int pressBtnAttackKeyInterval = 300;

	protected List<PetFightCutDown> skillTimeCountDown = new List<PetFightCutDown>();

	protected XDict<int, Transform> petShell = new XDict<int, Transform>();

	protected XDict<int, PetCDControl> petShellCountDown = new XDict<int, PetCDControl>();

	protected List<PetFightCutDown> petTimeCountDown = new List<PetFightCutDown>();

	protected List<Image> petType = new List<Image>();

	protected PetTipsFX petTipsFX;

	protected ButtonCustom BtnAuto;

	protected Image Auto;

	protected bool isInAuto;

	protected bool isPauseCheck;

	protected bool canAutoSetAuto;

	protected uint autoSetAutoTimerID;

	protected uint autoSetAutoTime = 8000u;

	protected ComboControl comboControl;

	protected GameObject LiveMessage;

	protected Text LiveMessageText;

	protected uint liveMessageTimer = 4294967295u;

	protected GameObject Hint;

	protected Text HintText;

	protected uint hintTimer = 4294967295u;

	protected ButtonCustom BtnQuit;

	public Action BtnQuitAction;

	protected BattleTimeUI battleTimeUI;

	protected GameObject RadarMinimapUIRoot;

	protected GameObject MiniMap;

	protected GameObject TopLeftTabs;

	protected List<TopLeftTab> TopLeftTabList = new List<TopLeftTab>();

	protected GameObject TopLeftTabsBtnLeftImage;

	protected GameObject TopLeftTabsBtnRightImage;

	protected bool isTabsStretchOut = true;

	protected XDict<BaseTweenPostion, Vector3> stretchInfo = new XDict<BaseTweenPostion, Vector3>();

	protected Vector3 StretchOffset = new Vector3(-270f, 0f, 0f);

	public GameObject GlobalRank;

	protected Transform GlobalRankBGFxSlot;

	protected RectTransform GlobalRankInfoIcon;

	protected Transform GlobalRankInfoIconBG;

	protected Image GlobalRankInfoIconFG;

	protected Transform GlobalRankInfoIconExplodeFxSlot;

	protected Text GlobalRankInfoText;

	protected Text GlobalRankRewardText;

	protected Transform GlobalRankReward;

	protected BattleUI.RankRewardType curRankRewardType;

	protected int lastGlobalRank = -1;

	protected bool isShowGlobalRankOpenAnim;

	protected bool isCanShowGlobalRankBGFx;

	protected float globalRankInfoIconVelocity = 20f;

	protected int globalRankBGFxID;

	protected int globalRankInfoIconFxID;

	protected int globalRankExplodeFxID;

	public GameObject TeamMember;

	protected List<TeamBattleMemberItem> playerItem = new List<TeamBattleMemberItem>();

	protected ButtonCustom BtnBackpack;

	protected ButtonCustom BtnBackpackPreview;

	protected GameObject guildBossBattleTimeObj;

	protected Text guildBossBattleTimeText;

	protected GameObject CallBoss;

	protected GameObject BountyBattleUI;

	protected GameObject SpecialInstanceBattleUI;

	protected RewardPreviewUI mRewardPreviewUI;

	protected GameObject mSpecialBuffUI;

	protected RectTransform mTramcarRewardUI;

	protected GameObject GuildWarCityResouce;

	protected Text GuildWarCityResouceLeftTeamName;

	protected Text GuildWarCityResouceLeftTeamNum;

	protected RectTransform GuildWarCityResouceLeftTeamBar;

	protected Text GuildWarCityResouceLeftTeamBarText;

	protected Text GuildWarCityResouceRightTeamName;

	protected Text GuildWarCityResouceRightTeamNum;

	protected RectTransform GuildWarCityResouceRightTeamBar;

	protected Text GuildWarCityResouceRightTeamBarText;

	private Text GuildWarCityTimeCDText;

	protected GameObject GuildWarMineCD;

	protected Text GuildWarMineCDText;

	protected TimeCountDown GuildWarMineTimeCD;

	protected GameObject GuildWarMineState;

	protected GameObject GuildWarMineStateBar;

	protected Image GuildWarMineStateBarFG;

	protected Text GuildWarMineStateBarName;

	protected GameObject GuildWarMineStateInfo;

	protected Text GuildWarMineStateInfoName;

	protected Vector3 GuildWarMineStateSlot0;

	protected Vector3 GuildWarMineStateSlot1;

	protected Vector3 GuildWarMineStateBarSlot0;

	protected Vector3 GuildWarMineStateBarSlot1;

	protected GameObject GuildWarBuff;

	protected Image GuildWarBuffFG;

	protected Action GuildWarBuffCallback;

	public Transform MineAndReportUISlot;

	private Text peakBattleVSLeftText;

	private Text peakBattleVSRightText;

	private Text peakBattleKillNumText;

	private Text peakBattleDeathNumText;

	private GameObject peakBattleInfoSlot;

	private GameObject peakBattleVSInfo;

	protected GameObject Area;

	protected Text AreaText;

	protected GameObject PlayerNum;

	protected Text PlayerNumText;

	protected GameObject BossProbability;

	protected Text BossProbabilityText;

	protected GameObject BattleMode;

	protected ButtonCustom BtnPKMode;

	protected ButtonCustom BtnPeaceMode;

	protected GameObject PKModeCD;

	protected CDControl PKModeCDControl;

	protected Image PKModeCDNum0;

	protected Image PKModeCDNum1;

	protected Image PKModeCDNum2;

	protected Transform PKModeCDNumSlot1;

	protected Transform PKModeCDNumSlot01;

	protected Transform PKModeCDNumSlot10;

	protected Transform PKModeCDNumSlot001;

	protected Transform PKModeCDNumSlot010;

	protected Transform PKModeCDNumSlot100;

	protected GameObject PeaceModeCD;

	protected CDControl PeaceModeCDControl;

	protected Image PeaceModeCDNum0;

	protected Image PeaceModeCDNum1;

	protected Image PeaceModeCDNum2;

	protected Transform PeaceModeCDNumSlot1;

	protected Transform PeaceModeCDNumSlot01;

	protected Transform PeaceModeCDNumSlot10;

	protected Transform PeaceModeCDNumSlot001;

	protected Transform PeaceModeCDNumSlot010;

	protected Transform PeaceModeCDNumSlot100;

	protected GameObject BattleBackpackItem;

	protected BaseTweenPostion BattleBackpackItemTween;

	protected Image BattleBackpackItemFrame;

	protected Image BattleBackpackItemIcon;

	protected Text BattleBackpackItemNum;

	protected GameObject BattleBackpackItemStep;

	protected Text BattleBackpackItemStepNum;

	protected Vector3 BattleBackpackItemTweenDstPos = new Vector3(-115f, 0f, 0f);

	protected int battleBackpackItemFxID;

	protected GameObject BatchText;

	protected Text BatchTextNum;

	protected DamageRankingUI damageRankingUI;

	protected DamageRanking2UI damageRanking2UI;

	protected ButtonCustom BtnSwitch;

	protected ButtonCustom ButtonWin;

	protected ButtonCustom ButtonLose;

	protected ButtonCustom ButtonPetDie;

	protected ButtonCustom ButtonLogOn;

	protected ButtonCustom ButtonHeroDie;

	protected GameObject ActPointBar;

	protected Image ActPointBarProgress;

	protected Image ActPointImage1;

	protected Image ActPointImage2;

	protected List<Transform> normalMode = new List<Transform>();

	protected List<Transform> fuseMode = new List<Transform>();

	protected GameObject FuseTimeBar;

	protected Image FuseTimeBarProgress;

	protected Transform FuseTimeBarBlinkFx;

	protected Transform FuseTimeBarFx;

	protected bool isSelfFuse;

	protected uint fuseTimerID;

	protected float fuseTimeBlinkPercentage = 0.5f;

	public static bool IsOpenAnimationOn;

	private List<long> m_hpTabs = new List<long>();

	private TimeCountDown guildWarCityTimeCD;

	public bool IsInAuto
	{
		get
		{
			return this.isInAuto;
		}
		set
		{
			if (value && InstanceManager.IsAILevelLimit)
			{
				return;
			}
			this.isInAuto = value;
			if (!this.IsPauseCheck)
			{
				this.SetIsInAuto(value);
			}
		}
	}

	public bool IsPauseCheck
	{
		get
		{
			return this.isPauseCheck;
		}
		set
		{
			this.isPauseCheck = (value || InstanceManager.IsAILevelLimit);
			if (!value)
			{
				this.SetIsInAuto(this.IsInAuto);
			}
		}
	}

	protected bool CanAutoSetAuto
	{
		get
		{
			return this.canAutoSetAuto;
		}
		set
		{
			this.canAutoSetAuto = value;
			TimerHeap.DelTimer(this.autoSetAutoTimerID);
			if (this.canAutoSetAuto && !this.IsInAuto)
			{
				this.autoSetAutoTimerID = TimerHeap.AddTimer(this.autoSetAutoTime, 0, delegate
				{
					if (!this.IsInAuto)
					{
						this.IsInAuto = true;
					}
				});
			}
		}
	}

	public RewardPreviewUI RewardPreviewUI
	{
		get
		{
			return this.mRewardPreviewUI;
		}
	}

	protected bool IsSelfFuse
	{
		get
		{
			return this.isSelfFuse;
		}
		set
		{
			this.isSelfFuse = value;
		}
	}

	protected void SetIsInAuto(bool state)
	{
		SelfAIControlManager.Instance.IsUIAuto = state;
		ResourceManager.SetSprite(this.Auto, ResourceManager.GetIconSprite((!InstanceManager.IsAILevelLimit) ? ((!state) ? "new_zd_qxtg" : "new_zd_tuoguan") : "new_zd_tgs"));
	}

	public void Awake()
	{
		BattleUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.Self = base.FindTransform("Self").get_gameObject();
		this.SelfHead = base.FindTransform("SelfHead").GetComponent<Image>();
		this.SelfLv = base.FindTransform("SelfLv").GetComponent<Text>();
		this.SelfName = base.FindTransform("SelfName").GetComponent<Text>();
		this.SelfVip = base.FindTransform("SelfVip").get_gameObject();
		this.SelfVipLevelIcon0 = base.FindTransform("SelfVipLevelIcon0").GetComponent<Image>();
		this.SelfVipLevelIcon1 = base.FindTransform("SelfVipLevelIcon1").GetComponent<Image>();
		this.SelfFightingIcon = base.FindTransform("SelfFightingIcon");
		this.SelfBlood = base.FindTransform("SelfBlood").GetComponent<Image>();
		this.SelfBloodText = base.FindTransform("SelfBloodText").GetComponent<Text>();
		this.selfBloodBlinkPercentage = float.Parse(DataReader<GlobalParams>.Get("hp_blink_pecent").value);
		this.Adversary = base.FindTransform("Adversary").get_gameObject();
		this.AdversaryHead = base.FindTransform("AdversaryHead").GetComponent<Image>();
		this.AdversaryLv = base.FindTransform("AdversaryLv").GetComponent<Text>();
		this.AdversaryBlood = base.FindTransform("AdversaryBlood").GetComponent<Image>();
		this.AdversaryBloodText = base.FindTransform("AdversaryBloodText").GetComponent<Text>();
		this.Boss = base.FindTransform("Boss").get_gameObject();
		this.BossHead = base.FindTransform("BossHead").GetComponent<Image>();
		this.BossLv = base.FindTransform("BossLv").GetComponent<Text>();
		this.BossName = base.FindTransform("BossName").GetComponent<Text>();
		this.BossBloodDown = base.FindTransform("BossBloodDown").GetComponent<Image>();
		this.BossBloodUp = base.FindTransform("BossBloodUp").GetComponent<Image>();
		this.BossBloodBarNum = base.FindTransform("BossBloodBarNum").GetComponent<Text>();
		this.BossBloodText = base.FindTransform("BossBloodText").GetComponent<Text>();
		this.BossBloodFx = base.FindTransform("BossBloodFx");
		this.BossBloodBarNumListIcon = base.FindTransform("BossBloodBarNumListIcon");
		this.BossTired = base.FindTransform("BossTiredImage").GetComponent<Image>();
		SkillButton component = base.FindTransform("BtnAttack").GetComponent<SkillButton>();
		SkillButton component2 = base.FindTransform("BtnRoll").GetComponent<SkillButton>();
		SkillButton component3 = base.FindTransform("BtnSkill1").GetComponent<SkillButton>();
		SkillButton component4 = base.FindTransform("BtnSkill2").GetComponent<SkillButton>();
		SkillButton component5 = base.FindTransform("BtnSkill3").GetComponent<SkillButton>();
		SkillButton component6 = base.FindTransform("BtnFuse1").GetComponent<SkillButton>();
		SkillButton component7 = base.FindTransform("BtnFuse2").GetComponent<SkillButton>();
		SkillButton component8 = base.FindTransform("BtnFuse3").GetComponent<SkillButton>();
		component.Init(1);
		component2.Init(8);
		component3.Init(11);
		component4.Init(12);
		component5.Init(13);
		component6.Init(21);
		component7.Init(22);
		component8.Init(23);
		this.skillBtn.Add(1, component);
		this.skillBtn.Add(8, component2);
		this.skillBtn.Add(11, component3);
		this.skillBtn.Add(12, component4);
		this.skillBtn.Add(13, component5);
		this.skillBtn.Add(21, component6);
		this.skillBtn.Add(22, component7);
		this.skillBtn.Add(23, component8);
		this.guideBtn.Clear();
		this.guideBtn.Add(base.FindTransform("GuideBtnAttack").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnRoll").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnSkill1").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnSkill2").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnSkill3").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnFuse").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnFuse1").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnFuse2").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnFuse3").get_gameObject());
		this.guideBtn.Add(base.FindTransform("GuideBtnAuto").get_gameObject());
		this.skillBtnIcon.Add(1, this.skillBtn[1].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(8, this.skillBtn[8].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(11, this.skillBtn[11].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(12, this.skillBtn[12].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(13, this.skillBtn[13].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(21, this.skillBtn[21].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(22, this.skillBtn[22].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnIcon.Add(23, this.skillBtn[23].get_transform().FindChild("Icon").GetComponent<Image>());
		this.skillBtnLock.Add(1, this.skillBtn[1].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(8, this.skillBtn[8].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(11, this.skillBtn[11].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(12, this.skillBtn[12].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(13, this.skillBtn[13].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(21, this.skillBtn[21].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(22, this.skillBtn[22].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnLock.Add(23, this.skillBtn[23].get_transform().FindChild("Lock").GetComponent<Image>());
		this.skillBtnCD.Add(1, this.skillBtn[1].get_transform().FindChild("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(8, this.skillBtn[8].get_transform().FindChild("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(11, this.skillBtn[11].get_transform().FindChild("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(12, this.skillBtn[12].get_transform().FindChild("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(13, this.skillBtn[13].get_transform().FindChild("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(21, this.skillBtn[21].get_transform().Find("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(22, this.skillBtn[22].get_transform().Find("CD").GetComponent<CDControl>());
		this.skillBtnCD.Add(23, this.skillBtn[23].get_transform().Find("CD").GetComponent<CDControl>());
		this.skillTimeCountDown.Add(this.skillBtn[11].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.skillTimeCountDown.Add(this.skillBtn[12].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.skillTimeCountDown.Add(this.skillBtn[13].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.skillTimeCountDown.get_Item(0).Init(0, this.skillBtnCD[11].get_transform());
		this.skillTimeCountDown.get_Item(1).Init(1, this.skillBtnCD[12].get_transform());
		this.skillTimeCountDown.get_Item(2).Init(2, this.skillBtnCD[13].get_transform());
		this.petShell.Add(0, this.skillBtn[21].get_transform().FindChild("IconShell"));
		this.petShell.Add(1, this.skillBtn[22].get_transform().FindChild("IconShell"));
		this.petShell.Add(2, this.skillBtn[23].get_transform().FindChild("IconShell"));
		this.petShellCountDown.Add(0, this.petShell[0].FindChild("IconShellFG").GetComponent<PetCDControl>());
		this.petShellCountDown.Add(1, this.petShell[1].FindChild("IconShellFG").GetComponent<PetCDControl>());
		this.petShellCountDown.Add(2, this.petShell[2].FindChild("IconShellFG").GetComponent<PetCDControl>());
		this.petTimeCountDown.Add(this.skillBtn[21].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.petTimeCountDown.Add(this.skillBtn[22].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.petTimeCountDown.Add(this.skillBtn[23].get_transform().FindChild("CountDown").GetComponent<PetFightCutDown>());
		this.petTimeCountDown.get_Item(0).Init(0, this.skillBtnCD[21].get_transform());
		this.petTimeCountDown.get_Item(1).Init(1, this.skillBtnCD[22].get_transform());
		this.petTimeCountDown.get_Item(2).Init(2, this.skillBtnCD[23].get_transform());
		this.petType.Add(this.skillBtn[21].get_transform().FindChild("Type").GetComponent<Image>());
		this.petType.Add(this.skillBtn[22].get_transform().FindChild("Type").GetComponent<Image>());
		this.petType.Add(this.skillBtn[23].get_transform().FindChild("Type").GetComponent<Image>());
		this.petTipsFX = base.FindTransform("PetTipsFX").GetComponent<PetTipsFX>();
		this.petTipsFX.PetFx.Clear();
		this.petTipsFX.PetFx.Add(this.skillBtn[21].get_transform());
		this.petTipsFX.PetFx.Add(this.skillBtn[22].get_transform());
		this.petTipsFX.PetFx.Add(this.skillBtn[23].get_transform());
		this.BtnAuto = base.FindTransform("BtnAuto").GetComponent<ButtonCustom>();
		this.BtnSwitch = base.FindTransform("BtnSwitch").GetComponent<ButtonCustom>();
		this.Auto = base.FindTransform("BtnAuto").GetComponent<Image>();
		this.autoSetAutoTime = (uint)float.Parse(DataReader<GlobalParams>.Get("autoSetAutoTime").value);
		this.comboControl = base.FindTransform("ComboNumRoot").GetComponent<ComboControl>();
		this.comboControl.AwakeSelf();
		this.LiveMessage = base.FindTransform("LiveMessage").get_gameObject();
		this.LiveMessageText = base.FindTransform("LiveMessageText").GetComponent<Text>();
		this.Hint = base.FindTransform("Hint").get_gameObject();
		this.HintText = base.FindTransform("HintText").GetComponent<Text>();
		this.BtnQuit = base.FindTransform("BtnQuit").GetComponent<ButtonCustom>();
		this.battleTimeUI = base.FindTransform("BattleTimeUI").GetComponent<BattleTimeUI>();
		this.RadarMinimapUIRoot = base.FindTransform("RadarMinimapUIRoot").get_gameObject();
		this.TopLeftTabs = base.FindTransform("TopLeftTabs").get_gameObject();
		this.TopLeftTabList.Add(base.FindTransform("TopLeftTab0").GetComponent<TopLeftTab>());
		this.TopLeftTabList.Add(base.FindTransform("TopLeftTab1").GetComponent<TopLeftTab>());
		this.TopLeftTabsBtnLeftImage = base.FindTransform("TopLeftTabsBtnLeftImage").get_gameObject();
		this.TopLeftTabsBtnRightImage = base.FindTransform("TopLeftTabsBtnRightImage").get_gameObject();
		this.GlobalRank = base.FindTransform("GlobalRank").get_gameObject();
		this.GlobalRankBGFxSlot = base.FindTransform("GlobalRankBGFxSlot");
		this.GlobalRankInfoIcon = base.FindTransform("GlobalRankInfoIcon").GetComponent<RectTransform>();
		this.GlobalRankInfoIconBG = base.FindTransform("GlobalRankInfoIconBG");
		this.GlobalRankInfoIconFG = base.FindTransform("GlobalRankInfoIconFG").GetComponent<Image>();
		this.GlobalRankInfoIconExplodeFxSlot = base.FindTransform("GlobalRankInfoIconExplodeFxSlot");
		this.GlobalRankInfoText = base.FindTransform("GlobalRankInfoText").GetComponent<Text>();
		this.GlobalRankRewardText = base.FindTransform("GlobalRankRewardText").GetComponent<Text>();
		this.GlobalRankReward = base.FindTransform("GlobalRankReward");
		this.TeamMember = base.FindTransform("TeamMember").get_gameObject();
		for (int i = 0; i < 2; i++)
		{
			this.playerItem.Add(base.FindTransform("TeamMember" + i).GetComponent<TeamBattleMemberItem>());
		}
		for (int j = 0; j < this.playerItem.get_Count(); j++)
		{
			this.playerItem.get_Item(j).Init();
		}
		this.BtnBackpack = base.FindTransform("BtnBackpack").GetComponent<ButtonCustom>();
		this.BtnBackpackPreview = base.FindTransform("BtnBackpackPreview").GetComponent<ButtonCustom>();
		this.guildBossBattleTimeObj = base.FindTransform("GuildBossBattleTime").get_gameObject();
		this.guildBossBattleTimeText = base.FindTransform("GuildBossRemainTimeText").GetComponent<Text>();
		this.CallBoss = base.FindTransform("CallBoss").get_gameObject();
		this.BountyBattleUI = base.FindTransform("BountyBattleUI").get_gameObject();
		this.SpecialInstanceBattleUI = base.FindTransform("SpecialInstanceBattleUI").get_gameObject();
		this.mRewardPreviewUI = base.FindTransform("RewardPreviewUI").GetComponent<RewardPreviewUI>();
		this.mSpecialBuffUI = base.FindTransform("SpecialBuffUI").get_gameObject();
		this.mTramcarRewardUI = base.FindTransform("TramcarRewardUI").GetComponent<RectTransform>();
		this.GuildWarCityResouce = base.FindTransform("GuildWarCityResouce").get_gameObject();
		this.GuildWarCityResouceLeftTeamName = base.FindTransform("GuildWarCityResouceLeftTeamName").GetComponent<Text>();
		this.GuildWarCityResouceLeftTeamNum = base.FindTransform("GuildWarCityResouceLeftTeamNum").GetComponent<Text>();
		this.GuildWarCityResouceLeftTeamBar = base.FindTransform("GuildWarCityResouceLeftTeamBar").GetComponent<RectTransform>();
		this.GuildWarCityResouceLeftTeamBarText = base.FindTransform("GuildWarCityResouceLeftTeamBarText").GetComponent<Text>();
		this.GuildWarCityResouceRightTeamName = base.FindTransform("GuildWarCityResouceRightTeamName").GetComponent<Text>();
		this.GuildWarCityResouceRightTeamNum = base.FindTransform("GuildWarCityResouceRightTeamNum").GetComponent<Text>();
		this.GuildWarCityResouceRightTeamBar = base.FindTransform("GuildWarCityResouceRightTeamBar").GetComponent<RectTransform>();
		this.GuildWarCityResouceRightTeamBarText = base.FindTransform("GuildWarCityResouceRightTeamBarText").GetComponent<Text>();
		this.GuildWarCityTimeCDText = base.FindTransform("GuildWarCityCDText").GetComponent<Text>();
		this.GuildWarMineCD = base.FindTransform("GuildWarMineCD").get_gameObject();
		this.GuildWarMineCDText = base.FindTransform("GuildWarMineCDText").GetComponent<Text>();
		this.GuildWarMineState = base.FindTransform("GuildWarMineState").get_gameObject();
		this.GuildWarMineStateBar = base.FindTransform("GuildWarMineStateBar").get_gameObject();
		this.GuildWarMineStateBarFG = base.FindTransform("GuildWarMineStateBarFG").GetComponent<Image>();
		this.GuildWarMineStateBarName = base.FindTransform("GuildWarMineStateBarName").GetComponent<Text>();
		this.GuildWarMineStateInfo = base.FindTransform("GuildWarMineStateInfo").get_gameObject();
		this.GuildWarMineStateInfoName = base.FindTransform("GuildWarMineStateInfoName").GetComponent<Text>();
		this.GuildWarMineStateSlot0 = base.FindTransform("GuildWarMineStateSlot0").get_localPosition();
		this.GuildWarMineStateSlot1 = base.FindTransform("GuildWarMineStateSlot1").get_localPosition();
		this.GuildWarMineStateBarSlot0 = base.FindTransform("GuildWarMineStateBarSlot0").get_localPosition();
		this.GuildWarMineStateBarSlot1 = base.FindTransform("GuildWarMineStateBarSlot1").get_localPosition();
		this.GuildWarBuff = base.FindTransform("GuildWarBuff").get_gameObject();
		this.GuildWarBuffFG = base.FindTransform("GuildWarBuffFG").GetComponent<Image>();
		this.MineAndReportUISlot = base.FindTransform("MineAndReportUISlot");
		this.peakBattleVSLeftText = base.FindTransform("PeakBattleVSLeftText").GetComponent<Text>();
		this.peakBattleVSRightText = base.FindTransform("PeakBattleVSRightText").GetComponent<Text>();
		this.peakBattleDeathNumText = base.FindTransform("PeakBattleDeathNumText").GetComponent<Text>();
		this.peakBattleKillNumText = base.FindTransform("PeakBattleKillNumText").GetComponent<Text>();
		this.peakBattleInfoSlot = base.FindTransform("PeakBattleInfoSlot").get_gameObject();
		this.peakBattleVSInfo = base.FindTransform("PeakBattleVSInfo").get_gameObject();
		this.Area = base.FindTransform("Area").get_gameObject();
		this.AreaText = base.FindTransform("AreaText").GetComponent<Text>();
		this.PlayerNum = base.FindTransform("PlayerNum").get_gameObject();
		this.PlayerNumText = base.FindTransform("PlayerNumText").GetComponent<Text>();
		this.BossProbability = base.FindTransform("BossProbability").get_gameObject();
		this.BossProbabilityText = base.FindTransform("BossProbabilityText").GetComponent<Text>();
		this.BattleMode = base.FindTransform("BattleMode").get_gameObject();
		this.BtnPKMode = base.FindTransform("BtnPKMode").GetComponent<ButtonCustom>();
		this.BtnPeaceMode = base.FindTransform("BtnPeaceMode").GetComponent<ButtonCustom>();
		this.PKModeCD = base.FindTransform("PKModeCD").get_gameObject();
		this.PKModeCDControl = base.FindTransform("PKModeCDControl").GetComponent<CDControl>();
		this.PKModeCDNum0 = base.FindTransform("PKModeCDNum0").GetComponent<Image>();
		this.PKModeCDNum1 = base.FindTransform("PKModeCDNum1").GetComponent<Image>();
		this.PKModeCDNum2 = base.FindTransform("PKModeCDNum2").GetComponent<Image>();
		this.PKModeCDNumSlot1 = base.FindTransform("PKModeCDNumSlot1");
		this.PKModeCDNumSlot01 = base.FindTransform("PKModeCDNumSlot01");
		this.PKModeCDNumSlot10 = base.FindTransform("PKModeCDNumSlot10");
		this.PKModeCDNumSlot001 = base.FindTransform("PKModeCDNumSlot001");
		this.PKModeCDNumSlot010 = base.FindTransform("PKModeCDNumSlot010");
		this.PKModeCDNumSlot100 = base.FindTransform("PKModeCDNumSlot100");
		this.PeaceModeCD = base.FindTransform("PeaceModeCD").get_gameObject();
		this.PeaceModeCDControl = base.FindTransform("PeaceModeCDControl").GetComponent<CDControl>();
		this.PeaceModeCDNum0 = base.FindTransform("PeaceModeCDNum0").GetComponent<Image>();
		this.PeaceModeCDNum1 = base.FindTransform("PeaceModeCDNum1").GetComponent<Image>();
		this.PeaceModeCDNum2 = base.FindTransform("PeaceModeCDNum2").GetComponent<Image>();
		this.PeaceModeCDNumSlot1 = base.FindTransform("PeaceModeCDNumSlot1");
		this.PeaceModeCDNumSlot01 = base.FindTransform("PeaceModeCDNumSlot01");
		this.PeaceModeCDNumSlot10 = base.FindTransform("PeaceModeCDNumSlot10");
		this.PeaceModeCDNumSlot001 = base.FindTransform("PeaceModeCDNumSlot001");
		this.PeaceModeCDNumSlot010 = base.FindTransform("PeaceModeCDNumSlot010");
		this.PeaceModeCDNumSlot100 = base.FindTransform("PeaceModeCDNumSlot100");
		this.BattleBackpackItem = base.FindTransform("BattleBackpackItem").get_gameObject();
		this.BattleBackpackItemTween = base.FindTransform("BattleBackpackItemTween").GetComponent<BaseTweenPostion>();
		this.BattleBackpackItemFrame = base.FindTransform("BattleBackpackItemFrame").GetComponent<Image>();
		this.BattleBackpackItemIcon = base.FindTransform("BattleBackpackItemIcon").GetComponent<Image>();
		this.BattleBackpackItemNum = base.FindTransform("BattleBackpackItemNum").GetComponent<Text>();
		this.BattleBackpackItemStep = base.FindTransform("BattleBackpackItemStep").get_gameObject();
		this.BattleBackpackItemStepNum = base.FindTransform("BattleBackpackItemStepNum").GetComponent<Text>();
		this.BatchText = base.FindTransform("BatchText").get_gameObject();
		this.BatchText.GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(511610, false));
		this.BatchTextNum = base.FindTransform("BatchTextNum").GetComponent<Text>();
		this.damageRankingUI = base.FindTransform("DamageRankingUI").GetComponent<DamageRankingUI>();
		this.damageRanking2UI = base.FindTransform("DamageRanking2UI").GetComponent<DamageRanking2UI>();
		this.ButtonWin = base.FindTransform("ButtonWin").GetComponent<ButtonCustom>();
		this.ButtonLose = base.FindTransform("ButtonLose").GetComponent<ButtonCustom>();
		this.ButtonPetDie = base.FindTransform("ButtonPetDie").GetComponent<ButtonCustom>();
		this.ButtonLogOn = base.FindTransform("ButtonLogOn").GetComponent<ButtonCustom>();
		this.ButtonHeroDie = base.FindTransform("ButtonHeroDie").GetComponent<ButtonCustom>();
		this.ShowButtonGM(SystemConfig.IsBattleGMOn);
		this.ActPointBar = base.FindTransform("ActPointBar").get_gameObject();
		this.ActPointBarProgress = base.FindTransform("ActPointBarProgress").GetComponent<Image>();
		this.ActPointImage1 = base.FindTransform("ActPointImage1").GetComponent<Image>();
		this.ActPointImage2 = base.FindTransform("ActPointImage2").GetComponent<Image>();
		this.FuseTimeBar = base.FindTransform("FuseTimeBar").get_gameObject();
		this.FuseTimeBarProgress = base.FindTransform("FuseTimeBarProgress").GetComponent<Image>();
		this.FuseTimeBarBlinkFx = base.FindTransform("FuseTimeBarBlinkFx");
		this.FuseTimeBarFx = base.FindTransform("FuseTimeBarFx");
		this.SetTriggerListener();
	}

	private void SetTriggerListener()
	{
		EventTriggerListener.Get(this.skillBtn[1].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnAttackUp);
		EventTriggerListener.Get(this.skillBtn[1].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnAttackDown);
		EventTriggerListener.Get(this.skillBtn[1].get_gameObject()).onExit = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnAttackExit);
		EventTriggerListener.Get(this.skillBtn[8].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnRollUp);
		EventTriggerListener.Get(this.skillBtn[8].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnRollDown);
		EventTriggerListener.Get(this.skillBtn[11].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill1Up);
		EventTriggerListener.Get(this.skillBtn[11].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill1Down);
		EventTriggerListener.Get(this.skillBtn[12].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill2Up);
		EventTriggerListener.Get(this.skillBtn[12].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill2Down);
		EventTriggerListener.Get(this.skillBtn[13].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill3Up);
		EventTriggerListener.Get(this.skillBtn[13].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnSkill3Down);
		EventTriggerListener.Get(this.skillBtn[21].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse1Up);
		EventTriggerListener.Get(this.skillBtn[21].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse1Down);
		EventTriggerListener.Get(this.skillBtn[22].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse2Up);
		EventTriggerListener.Get(this.skillBtn[22].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse2Down);
		EventTriggerListener.Get(this.skillBtn[23].get_gameObject()).onUp = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse3Up);
		EventTriggerListener.Get(this.skillBtn[23].get_gameObject()).onDown = new EventTriggerListener.VoidDelegateGameObject(this.OnClickBtnFuse3Down);
		this.BtnAuto.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnAuto);
		this.BtnSwitch.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnSwitch);
		this.BtnQuit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnQuit);
		this.BtnBackpack.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBackpack);
		this.BtnBackpackPreview.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBackpackPreview);
		this.BtnPKMode.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnPKMode);
		this.BtnPeaceMode.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnPeaceMode);
		this.GuildWarBuffFG.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildWarBuff);
		base.FindTransform("TopLeftTabsBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTabsStretch);
		this.ButtonWin.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWin);
		this.ButtonLose.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnLose);
		this.ButtonPetDie.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPetDie);
		this.ButtonLogOn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLogOn);
		this.ButtonHeroDie.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClicHeroDie);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", false);
		this.ResetPetBtnBgState();
		this.SetUI();
		this.ShowPopAnimation(true);
		this.OpenChatTipUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", true);
		FXSpineManager.Instance.DeleteSpine(this.selfBloodBlinkFxID, true);
		TimerHeap.DelTimer(this.pressBtnAttackKeyTimer);
		this.ShowPopAnimation(false);
		ChatTipUIViewModel.Close();
		this.ClearGuildWarCityTimeCountDown();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<float>(EventNames.FuseTime, new Callback<float>(this.SetFuseTime));
		EventDispatcher.AddListener<int, LiveTextMessage>(BattleUIEvent.LiveText, new Callback<int, LiveTextMessage>(this.ShowLiveMessage));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
		EventDispatcher.AddListener(SelfAIControlManagerEvent.StopAutoSetUIAuto, new Callback(this.StopAutoSetUIAuto));
		EventDispatcher.AddListener(SelfAIControlManagerEvent.StartAutoSetUIAuto, new Callback(this.StartAutoSetUIAuto));
		EventDispatcher.AddListener(EventNames.SetProgress, new Callback(this.SetSpecialBattleStage));
		EventDispatcher.AddListener<int>(EventNames.PetCountDownEnd, new Callback<int>(this.OnPetCountDownEnd));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<float>(EventNames.FuseTime, new Callback<float>(this.SetFuseTime));
		EventDispatcher.RemoveListener<int, LiveTextMessage>(BattleUIEvent.LiveText, new Callback<int, LiveTextMessage>(this.ShowLiveMessage));
		EventDispatcher.RemoveListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
		EventDispatcher.RemoveListener(SelfAIControlManagerEvent.StopAutoSetUIAuto, new Callback(this.StopAutoSetUIAuto));
		EventDispatcher.RemoveListener(SelfAIControlManagerEvent.StartAutoSetUIAuto, new Callback(this.StartAutoSetUIAuto));
		EventDispatcher.RemoveListener(EventNames.SetProgress, new Callback(this.SetSpecialBattleStage));
		EventDispatcher.RemoveListener<int>(EventNames.PetCountDownEnd, new Callback<int>(this.OnPetCountDownEnd));
	}

	private void Update()
	{
		this.UpdateGlobalRankOpenAnim();
	}

	public void FinishAnimationClose()
	{
		base.Show(false);
		BattleUI.IsOpenAnimationOn = false;
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

	protected void ShowPopAnimation(bool isShow)
	{
		Animator component = base.GetComponent<Animator>();
		if (isShow)
		{
			component.set_enabled(true);
			component.Play("BattleUI_open", 0, 0f);
			BattleUI.IsOpenAnimationOn = false;
		}
		else if (BattleUI.IsOpenAnimationOn)
		{
			component.set_enabled(true);
			component.Play("BattleUI_close", 0, 0f);
		}
		else
		{
			this.FinishAnimationClose();
			component.set_enabled(false);
		}
	}

	public void ResetUI()
	{
		this.IsInAuto = false;
		this.CanAutoSetAuto = false;
		this.ResetPlayer();
		this.ShowGuildBossBattleTime(false);
		this.ShowCallBoss(false);
		this.ShowBountyBattleUI(false);
		this.ShowSpecialInstanceBattleUI(false);
		this.ShowBoss(false);
		this.BossBloodDown.get_gameObject().SetActive(true);
		this.ClearPetCountDown();
		this.ClearSkillCountDown();
		this.IsSelfFuse = false;
		this.ActPointBarProgress.set_fillAmount(0f);
		Utils.SetTransformZOn(this.FuseTimeBar.get_transform(), false);
		this.FuseTimeBarProgress.set_fillAmount(1f);
		this.normalMode.Clear();
		this.fuseMode.Clear();
		this.ClearSkill();
		this.ClearSkillCD();
		this.comboControl.ResetAll();
		FXSpineManager.Instance.DeleteSpine(this.selfBloodBlinkFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.rollClickFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.skill1ClickFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.skill2ClickFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.skill3ClickFxID, true);
		this.selfBloodBlinkFxID = 0;
		this.rollClickFxID = 0;
		this.skill1ClickFxID = 0;
		this.skill2ClickFxID = 0;
		this.skill3ClickFxID = 0;
		UIUtils.animToFadeOutEnd(this.LiveMessage);
		UIUtils.animToFadeOutEnd(this.Hint);
		this.petTipsFX.ResetAll();
	}

	public void SetUI()
	{
		this.ResetUI();
		this.IsInAuto = false;
		this.CanAutoSetAuto = true;
		if (BattleBlackboard.Instance.HasSetBoss)
		{
			this.ShowBoss(true);
			this.SetBossHead(BattleBlackboard.Instance.BossHead);
			this.SetBossLevel(BattleBlackboard.Instance.BossLv);
			this.SetBossHp(BattleBlackboard.Instance.BossHp, BattleBlackboard.Instance.BossHpLmt);
		}
		this.SetAllPetCountDown(BattleBlackboard.Instance.PetCD);
		this.SetModeDefaultValue(BattleBlackboard.Instance.SelfPetNum);
		this.SetSelfHead(BattleBlackboard.Instance.SelfHead);
		this.SetSelfLevel(BattleBlackboard.Instance.SelfLv);
		this.SetSelfName(BattleBlackboard.Instance.SelfName);
		this.SetSelfVipLv(BattleBlackboard.Instance.SelfVipLv);
		this.SetSelfFighting(BattleBlackboard.Instance.SelfFighting);
		this.SetSelfHP(BattleBlackboard.Instance.SelfHp, BattleBlackboard.Instance.SelfHpLmt);
		for (int i = 0; i < BattleBlackboard.Instance.PlayerDatas.Count; i++)
		{
			BattleBlackboard.PlayerData playerData = BattleBlackboard.Instance.PlayerDatas.ElementValueAt(i);
			this.SetPlayerID(playerData.Index, playerData.PlayerID);
			this.SetPlayerHead(playerData.Index, playerData.PlayerHead);
			this.SetPlayerName(playerData.Index, playerData.PlayerName);
			this.SetPlayerLevel(playerData.Index, playerData.PlayerLv);
			this.SetPlayerVipLv(playerData.Index, playerData.PlayerVipLv);
			this.SetPlayerHp(playerData.Index, playerData.PlayerHp, playerData.PlayerHpLmt);
		}
		if (BattleBlackboard.Instance.SelfFuse)
		{
			this.SetSelfFuse(true);
		}
		else
		{
			this.IsSelfFuse = false;
			this.SetSkillMode(false);
			Utils.SetTransformZOn(this.FuseTimeBar.get_transform(), false);
		}
		this.SetAllSkill(BattleBlackboard.Instance.SelfSkillIcon);
		this.SetSelfActPoint(BattleBlackboard.Instance.SelfActPoint);
		this.SetAllSkillCD(BattleBlackboard.Instance.SelfSkillCD);
		for (int j = 0; j < BattleBlackboard.Instance.SelfSkillBound.Count; j++)
		{
			this.SetSkillEnable(BattleBlackboard.Instance.SelfSkillBound.ElementKeyAt(j), (float)BattleBlackboard.Instance.SelfActPoint >= BattleBlackboard.Instance.SelfSkillBound.ElementValueAt(j));
		}
		this.SetCombo(false, 0);
		this.SetBossName(BattleBlackboard.Instance.BossName);
		this.SetSpecialBattleStage();
	}

	protected void OpenChatTipUI()
	{
		ChatTipUIViewModel.Open(UINodesManager.NormalUIRoot, true);
	}

	protected void OnUnloadScene(int sceneOld, int sceneNew)
	{
		this.ResetButtonsOfGuide();
	}

	public void ResetAllInstancePart()
	{
		this.ShowBattleTimeUI(false);
		this.ShowMiniMapUI(false);
		this.ShowBountyBattleUI(false);
		this.ShowRewardPreviewUI(false, RewardPreviewUI.CopyType.NONE, 0L, 0L);
		this.ShowTramcarRewardUI(false);
		this.ShowSpecialBuffUI(false);
		this.ShowGuildWarCityResouce(false);
		this.ShowGuildWarMineCD(false, 0);
		this.ShowGuildWarMineState(false);
		this.ShowGuildWarBuff(false, 0, null);
		this.ShowPeakBattleSlot(false);
		this.ShowDamageRankingUI(false);
		this.ShowDamageRanking2UI(false);
		this.ShowArea(false);
		this.ShowPlayerNum(false);
		this.ShowBackpackBtn(false);
		this.ShowBackpackPreviewBtn(false);
		this.ShowBattleBackpackItem(false);
		FXSpineManager.Instance.DeleteSpine(this.battleBackpackItemFxID, true);
		this.ShowBatchText(false);
		this.ShowBossProbability(false);
		this.ShowBattleMode(false);
		this.ShowPKModeBtn(false);
		this.ShowPKModeCD(false);
		this.ShowPeaceModeBtn(false);
		this.ShowPeaceModeCD(false);
		this.ShowTopLeftTabs(false, new BattleUI.TopLeftTabData[0]);
		this.ShowGlobalRank(false, BattleUI.RankRewardType.Text);
		this.ShowTeamMember(false);
	}

	public bool ShowSelf(bool isShow)
	{
		this.Self.SetActive(isShow);
		return isShow;
	}

	public void SetSelfHead(int icon)
	{
		ResourceManager.SetSprite(this.SelfHead, GameDataUtils.GetIcon(icon));
	}

	public void SetSelfLevel(int level)
	{
		this.SelfLv.set_text(level.ToString());
	}

	public void SetSelfName(string name)
	{
		this.SelfName.set_text(name);
	}

	public void SetSelfVipLv(int vipLevel)
	{
		if (vipLevel == 0)
		{
			this.SelfVip.SetActive(false);
		}
		else
		{
			this.SelfVip.SetActive(true);
			ResourceManager.SetSprite(this.SelfVipLevelIcon1, GameDataUtils.GetNumIcon1(vipLevel, NumType.Yellow_light));
		}
	}

	public void SetSelfFighting(long fighting)
	{
		UIUtils.ShowImageText(this.SelfFightingIcon, fighting, "new_z_zln_", string.Empty);
	}

	public void SetSelfHP(long curHp, long hpLmt)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (hpLmt == 0L)
		{
			return;
		}
		if (curHp < 0L)
		{
			curHp = 0L;
		}
		float num = (float)((double)curHp / (double)hpLmt);
		this.SelfBlood.set_fillAmount(num);
		this.SelfBloodText.set_text(string.Format("{0}/{1}", curHp, hpLmt));
		if (curHp > 0L && num < this.selfBloodBlinkPercentage)
		{
			if (EntityWorld.Instance.EntSelf.IsInBattle)
			{
				this.selfBloodBlinkFxID = FXSpineManager.Instance.ReplaySpine(this.selfBloodBlinkFxID, 203, UINodesManager.NormalUIRoot, "BattleUI", 1001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.ScaleXY);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.selfBloodBlinkFxID, true);
		}
	}

	public void SetAdversaryHead(int icon)
	{
		ResourceManager.SetSprite(this.AdversaryHead, GameDataUtils.GetIcon(icon));
	}

	public void SetAdversaryLevel(int level)
	{
		this.AdversaryLv.set_text(level.ToString());
	}

	public void SetAdversaryHp(long hpNow, long hpMax)
	{
		if (hpMax <= 0L)
		{
			return;
		}
		if (hpNow < 0L)
		{
			hpNow = 0L;
		}
		this.BossBloodUp.set_fillAmount((float)((double)hpNow / (double)hpMax));
		this.AdversaryBloodText.set_text(hpNow + "/" + hpMax);
	}

	public void ShowBoss(bool isShow)
	{
		if (this.Boss.get_activeSelf() != isShow)
		{
			this.Boss.SetActive(isShow);
		}
	}

	public void SetBossName(string name)
	{
		this.BossName.set_text(name);
	}

	public void SetBossHead(int icon)
	{
		ResourceManager.SetSprite(this.BossHead, GameDataUtils.GetIcon(icon));
	}

	public void SetBossLevel(int level)
	{
		this.BossLv.set_text(level.ToString());
	}

	public void SetBossHp(long curHp, long hpLmt)
	{
		if (curHp < 0L)
		{
			curHp = 0L;
		}
		if (hpLmt < 0L)
		{
			hpLmt = 0L;
		}
		this.BossBloodText.set_text(string.Format("{0}/{1}", curHp, hpLmt));
		long num = 10000L;
		long num2 = num / 2L;
		int num4;
		if (hpLmt < num + num2)
		{
			this.BossBloodDown.get_gameObject().SetActive(false);
			ResourceManager.SetSprite(this.BossBloodUp, ResourceManager.GetIconSprite("new_boss_blood5"));
			float num3 = (float)((double)curHp / (double)hpLmt);
			if ((double)num3 > 0.99)
			{
				num3 = 1f;
			}
			else if ((double)num3 < 0.01)
			{
				num3 = 0f;
			}
			this.BossBloodUp.set_fillAmount(num3);
			num4 = 1;
		}
		else
		{
			long num5 = hpLmt % num;
			long num6;
			if (num5 > num2)
			{
				num6 = num5;
			}
			else
			{
				num6 = num + num5;
			}
			this.m_hpTabs.Clear();
			for (long num7 = hpLmt - num6; num7 >= 0L; num7 -= num)
			{
				this.m_hpTabs.Add(num7);
			}
			int num8 = 0;
			for (int i = 0; i < this.m_hpTabs.get_Count(); i++)
			{
				if (curHp >= this.m_hpTabs.get_Item(i))
				{
					num8 = i;
					break;
				}
			}
			num4 = this.m_hpTabs.get_Count() - num8;
			if (num8 == this.m_hpTabs.get_Count() - 1)
			{
				this.BossBloodDown.get_gameObject().SetActive(false);
				ResourceManager.SetSprite(this.BossBloodUp, ResourceManager.GetIconSprite("new_boss_blood5"));
				float num9 = (float)((double)curHp / (double)num6);
				if ((double)num9 > 0.99)
				{
					num9 = 1f;
				}
				else if ((double)num9 < 0.01)
				{
					num9 = 0f;
				}
				this.BossBloodUp.set_fillAmount(num9);
			}
			else if (num8 == this.m_hpTabs.get_Count() - 2)
			{
				this.BossBloodDown.get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.BossBloodUp, ResourceManager.GetIconSprite("new_boss_blood4"));
				float num10;
				if (hpLmt - curHp < num6)
				{
					num10 = (float)((double)(curHp - (hpLmt - num6)) / (double)num6);
				}
				else
				{
					num10 = (float)((double)(curHp % num) / (double)num);
				}
				if ((double)num10 > 0.99)
				{
					num10 = 1f;
				}
				else if ((double)num10 < 0.01)
				{
					num10 = 0f;
				}
				this.BossBloodUp.set_fillAmount(num10);
				ResourceManager.SetSprite(this.BossBloodDown, ResourceManager.GetIconSprite("new_boss_blood5"));
			}
			else
			{
				this.BossBloodDown.get_gameObject().SetActive(true);
				int num11 = (this.m_hpTabs.get_Count() - num8) % 4 + 1;
				ResourceManager.SetSprite(this.BossBloodUp, ResourceManager.GetIconSprite("new_boss_blood" + num11));
				float num12;
				if (hpLmt - curHp < num6)
				{
					num12 = (float)((double)(curHp - (hpLmt - num6)) / (double)num6);
				}
				else
				{
					num12 = (float)((double)(curHp % num) / (double)num);
				}
				if ((double)num12 > 0.99)
				{
					num12 = 1f;
				}
				else if ((double)num12 < 0.01)
				{
					num12 = 0f;
				}
				this.BossBloodUp.set_fillAmount(num12);
				num11--;
				if (num11 <= 0)
				{
					num11 = 4;
				}
				ResourceManager.SetSprite(this.BossBloodDown, ResourceManager.GetIconSprite("new_boss_blood" + num11));
			}
		}
		UIUtils.ShowImageText(this.BossBloodBarNumListIcon, long.Parse(num4.ToString()), "new_zd_qixuetanzi_", string.Empty);
	}

	public void SetBossWeak(bool isWeek)
	{
		if (!isWeek && this.bossWeakFxID > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.bossWeakFxID, true);
			FXSpineManager.Instance.PlaySpine(309, this.BossTired.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (isWeek)
		{
			this.bossWeakFxID = FXSpineManager.Instance.ReplaySpine(this.bossWeakFxID, 202, this.BossTired.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.bossWeakFxID, true);
		}
	}

	public void SetBossVp(int curVp, int vpLmt)
	{
		this.BossTired.set_fillAmount((float)((double)curVp / (double)vpLmt));
	}

	public void SetBossDead()
	{
		this.ShowBoss(false);
		this.BountyBattleUI.GetComponent<BountyBattleUI>().removeListeners();
	}

	protected void OnClickBtnAttackUp(GameObject sender)
	{
		TimerHeap.DelTimer(this.pressBtnAttackKeyTimer);
	}

	protected void OnClickBtnAttackDown(GameObject sender)
	{
		TimerHeap.DelTimer(this.pressBtnAttackKeyTimer);
		if (!base.get_gameObject().get_activeSelf())
		{
			return;
		}
		this.pressBtnAttackKeyTimer = TimerHeap.AddTimer(this.pressBtnAttackKeyDelayBegin, this.pressBtnAttackKeyInterval, delegate
		{
			XInputManager.Instance.OnSkillBtnDown(1, false);
		});
	}

	protected void OnClickBtnAttackExit(GameObject sender)
	{
		TimerHeap.DelTimer(this.pressBtnAttackKeyTimer);
	}

	protected void OnClickBtnRollUp(GameObject sender)
	{
	}

	protected void OnClickBtnRollDown(GameObject sender)
	{
	}

	protected void OnClickBtnSkill1Up(GameObject sender)
	{
	}

	protected void OnClickBtnSkill1Down(GameObject sender)
	{
	}

	protected void OnClickBtnSkill2Up(GameObject sender)
	{
	}

	protected void OnClickBtnSkill2Down(GameObject sender)
	{
	}

	protected void OnClickBtnSkill3Up(GameObject sender)
	{
	}

	protected void OnClickBtnSkill3Down(GameObject sender)
	{
	}

	protected void OnClickBtnFuse1Up(GameObject sender)
	{
	}

	protected void OnClickBtnFuse1Down(GameObject sender)
	{
	}

	protected void OnClickBtnFuse2Up(GameObject sender)
	{
	}

	protected void OnClickBtnFuse2Down(GameObject sender)
	{
	}

	protected void OnClickBtnFuse3Up(GameObject sender)
	{
	}

	protected void OnClickBtnFuse3Down(GameObject sender)
	{
	}

	public void AddSkill(int index, int iconID)
	{
		if (this.skillBtnIcon.ContainsKey(index))
		{
			Utils.SetTransformZOn(this.skillBtnIcon[index].get_transform(), true);
			ResourceManager.SetSprite(this.skillBtnIcon[index], GameDataUtils.GetIcon(iconID));
		}
		if (this.skillBtnLock.ContainsKey(index))
		{
			Utils.SetTransformZOn(this.skillBtnLock[index].get_transform(), false);
		}
		if (PetManager.Instance.GetCurrentPetFormation().get_Count() > 0)
		{
			int num = 0;
			switch (index)
			{
			case 21:
				num = 0;
				break;
			case 22:
				num = 1;
				break;
			case 23:
				num = 2;
				break;
			}
			int petId = PetManager.Instance.GetPetInfo(PetManager.Instance.GetCurrentPetFormation().get_Item(num)).petId;
			int function = DataReader<Pet>.Get(petId).function;
			ResourceManager.SetSprite(this.petType.get_Item(num), ResourceManager.GetIconSprite("fight_pet_biaoshi" + function));
		}
	}

	public void RemoveSkill(int index)
	{
		if (this.skillBtnIcon.ContainsKey(index))
		{
			Utils.SetTransformZOn(this.skillBtnIcon[index].get_transform(), false);
		}
		if (this.skillBtnLock.ContainsKey(index))
		{
			Utils.SetTransformZOn(this.skillBtnLock[index].get_transform(), true);
		}
	}

	public void ClearSkill()
	{
		for (int i = 0; i < this.skillBtnIcon.Values.get_Count(); i++)
		{
			if (this.skillBtnIcon.Keys.get_Item(i) != 1)
			{
				Utils.SetTransformZOn(this.skillBtnIcon.Values.get_Item(i).get_transform(), false);
			}
		}
		for (int j = 0; j < this.skillBtnLock.Values.get_Count(); j++)
		{
			Utils.SetTransformZOn(this.skillBtnLock.Values.get_Item(j).get_transform(), true);
		}
	}

	public void SetAllSkill(XDict<int, int> skillIcons)
	{
		for (int i = 0; i < skillIcons.Count; i++)
		{
			this.AddSkill(skillIcons.ElementKeyAt(i), skillIcons.ElementValueAt(i));
		}
	}

	public void SetSkillEnable(int index, bool isEnable)
	{
		if (!this.skillBtnIcon.ContainsKey(index))
		{
			return;
		}
		switch (index)
		{
		case 21:
			this.petTipsFX.PetEnableSetPetFx(isEnable, 0);
			break;
		case 22:
			this.petTipsFX.PetEnableSetPetFx(isEnable, 1);
			break;
		case 23:
			this.petTipsFX.PetEnableSetPetFx(isEnable, 2);
			break;
		}
		if (isEnable)
		{
			ImageColorMgr.SetImageColor(this.skillBtnIcon[index], false);
		}
		else
		{
			ImageColorMgr.SetImageColor(this.skillBtnIcon[index], true);
		}
	}

	public void SetSkillCD(int index, float time, float percentage)
	{
		if (!this.skillBtn.ContainsKey(index))
		{
			return;
		}
		if (!this.skillBtnCD.ContainsKey(index))
		{
			return;
		}
		if (time == 0f || percentage <= 0f)
		{
			this.ResetSkillCD(index);
			return;
		}
		switch (index)
		{
		case 21:
			this.petTipsFX.PetCdStartSetPetFx(0);
			break;
		case 22:
			this.petTipsFX.PetCdStartSetPetFx(1);
			break;
		case 23:
			this.petTipsFX.PetCdStartSetPetFx(2);
			break;
		}
		this.skillBtn[index].set_interactable(false);
		this.SetSkillCountDown(index, time, percentage);
		this.skillBtnCD[index].SetCD(time / 1000f, percentage, delegate
		{
			if (index == 21 || index == 22 || index == 23)
			{
				EventDispatcher.Broadcast("GuideManager.InstanceOfActPoint");
			}
			switch (index)
			{
			case 21:
				this.petTipsFX.PetCdEndSetPetFx(0);
				break;
			case 22:
				this.petTipsFX.PetCdEndSetPetFx(1);
				break;
			case 23:
				this.petTipsFX.PetCdEndSetPetFx(2);
				break;
			}
			FXSpineManager.Instance.PlaySpine(212, this.skillBtn[index].get_transform(), "BattleUI", 2001, delegate
			{
				this.skillBtn[index].set_interactable(true);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		});
	}

	public void ResetSkillCD(int index)
	{
		if (!this.skillBtn.ContainsKey(index))
		{
			return;
		}
		if (!this.skillBtnCD.ContainsKey(index))
		{
			return;
		}
		this.skillBtn[index].set_interactable(true);
		this.skillBtnCD[index].ResetCD();
	}

	public void ClearSkillCD()
	{
		for (int i = 0; i < this.skillBtn.Values.get_Count(); i++)
		{
			this.skillBtn.Values.get_Item(i).set_interactable(true);
		}
		for (int j = 0; j < this.skillBtnCD.Values.get_Count(); j++)
		{
			this.skillBtnCD.Values.get_Item(j).ResetCD();
		}
	}

	protected void SetAllSkillCD(XDict<int, KeyValuePair<float, DateTime>> skillCD)
	{
		for (int i = 0; i < skillCD.Count; i++)
		{
			this.SetSkillCD(skillCD.ElementKeyAt(i), skillCD.ElementValueAt(i).get_Key(), (float)((double)skillCD.ElementValueAt(i).get_Key() - (DateTime.get_Now() - skillCD.ElementValueAt(i).get_Value()).get_TotalMilliseconds()) / skillCD.ElementValueAt(i).get_Key());
		}
	}

	public void SetClick(int skillIndex)
	{
		switch (skillIndex)
		{
		case 8:
			this.rollClickFxID = FXSpineManager.Instance.ReplaySpine(this.rollClickFxID, 211, this.skillBtn[8].get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return;
		case 9:
		case 10:
			IL_22:
			if (skillIndex != 1)
			{
				return;
			}
			if (this.IsSelfFuse)
			{
			}
			return;
		case 11:
			this.skill1ClickFxID = FXSpineManager.Instance.ReplaySpine(this.skill1ClickFxID, 211, this.skillBtn[11].get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return;
		case 12:
			this.skill2ClickFxID = FXSpineManager.Instance.ReplaySpine(this.skill2ClickFxID, 211, this.skillBtn[12].get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return;
		case 13:
			this.skill3ClickFxID = FXSpineManager.Instance.ReplaySpine(this.skill3ClickFxID, 211, this.skillBtn[13].get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return;
		}
		goto IL_22;
	}

	protected void ResetButtonsOfGuide()
	{
		for (int i = 0; i < this.guideBtn.get_Count(); i++)
		{
			this.guideBtn.get_Item(i).SetActive(true);
		}
	}

	public void SetPetCountDown(int index, float time, float percentage)
	{
		if (!this.petShell.ContainsKey(index))
		{
			return;
		}
		if (!this.petShellCountDown.ContainsKey(index))
		{
			return;
		}
		if (time == 0f || percentage <= 0f)
		{
			this.ResetPetCountDown(index);
			return;
		}
		Utils.SetTransformZOn(this.petShell[index], true);
		this.petShellCountDown[index].SetPetCD(time / 1000f, percentage, delegate
		{
		});
		this.UpdatePetBtnBgState(index, true);
		this.petTimeCountDown.get_Item(index).SetCutDown(time, true, true);
	}

	public void ResetPetCountDown(int index)
	{
		if (!this.petShell.ContainsKey(index))
		{
			return;
		}
		if (!this.petShellCountDown.ContainsKey(index))
		{
			return;
		}
		Utils.SetTransformZOn(this.petShell[index], false);
		this.petShellCountDown[index].ResetPetCD();
	}

	public void ClearPetCountDown()
	{
		for (int i = 0; i < this.petShell.Values.get_Count(); i++)
		{
			Utils.SetTransformZOn(this.petShell.Values.get_Item(i), false);
		}
		for (int j = 0; j < this.petShellCountDown.Values.get_Count(); j++)
		{
			this.petShellCountDown.Values.get_Item(j).ResetPetCD();
			this.petTimeCountDown.get_Item(j).Hidden();
		}
	}

	protected void SetAllPetCountDown(XDict<int, KeyValuePair<float, DateTime>> petCountDown)
	{
		for (int i = 0; i < petCountDown.Count; i++)
		{
			this.SetPetCountDown(petCountDown.ElementKeyAt(i), petCountDown.ElementValueAt(i).get_Key(), (float)((double)petCountDown.ElementValueAt(i).get_Key() - (DateTime.get_Now() - petCountDown.ElementValueAt(i).get_Value()).get_TotalMilliseconds()) / petCountDown.ElementValueAt(i).get_Key());
		}
	}

	public void SetSkillCountDown(int index, float time, float percentage)
	{
		if (index != 11 && index != 12 && index != 13)
		{
			return;
		}
		if (time == 0f || percentage <= 0f)
		{
			this.skillTimeCountDown.get_Item(index).Hidden();
			return;
		}
		switch (index)
		{
		case 11:
			this.skillTimeCountDown.get_Item(0).SetCutDown(time, false, false);
			break;
		case 12:
			this.skillTimeCountDown.get_Item(1).SetCutDown(time, false, false);
			break;
		case 13:
			this.skillTimeCountDown.get_Item(2).SetCutDown(time, false, false);
			break;
		}
	}

	public void ClearSkillCountDown()
	{
		for (int i = 0; i < this.skillTimeCountDown.get_Count(); i++)
		{
			this.skillTimeCountDown.get_Item(i).Hidden();
		}
	}

	public void OnClickBtnAuto(GameObject sender)
	{
		if (!this.IsInAuto && InstanceManager.IsAILevelLimit)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(InstanceManager.AILimitTip, false));
		}
		else
		{
			this.IsInAuto = !this.IsInAuto;
		}
	}

	protected void StopAutoSetUIAuto()
	{
		this.CanAutoSetAuto = false;
	}

	protected void StartAutoSetUIAuto()
	{
		this.CanAutoSetAuto = true;
	}

	public void SetCombo(bool isShow, int combo = 0)
	{
		this.comboControl.SetCombo(isShow, combo, true);
	}

	protected void ShowLiveMessage(int type, LiveTextMessage message)
	{
		if (message.contentID == 0)
		{
			return;
		}
		string fixLiveMessage = this.GetFixLiveMessage(type, message);
		CanvasGroup component = this.LiveMessage.GetComponent<CanvasGroup>();
		if (component && component.get_alpha() > 0f && fixLiveMessage.Equals(this.LiveMessageText.get_text()))
		{
			return;
		}
		TimerHeap.DelTimer(this.liveMessageTimer);
		UIUtils.animToFadeInEnd(this.LiveMessage);
		this.LiveMessageText.set_text(fixLiveMessage);
		this.liveMessageTimer = TimerHeap.AddTimer((uint)message.showTime, 0, delegate
		{
			this.HideLiveMessage();
		});
	}

	protected string GetFixLiveMessage(int type, LiveTextMessage message)
	{
		string info = DataReader<ZhanDouXinXi>.Get(message.contentID).info;
		return info.Replace("{0}", message.name);
	}

	public void HideLiveMessage()
	{
		TimerHeap.DelTimer(this.liveMessageTimer);
		if (this.LiveMessage)
		{
			UIUtils.animToFadeOut(this.LiveMessage);
		}
	}

	public void ShowHint(string text, uint time)
	{
		UIUtils.animToFadeInEnd(this.Hint);
		this.HintText.set_text(text);
		if (time != 0u)
		{
			this.hintTimer = TimerHeap.AddTimer(time, 0, delegate
			{
				this.HideHint();
			});
		}
	}

	public void HideHint()
	{
		TimerHeap.DelTimer(this.hintTimer);
		UIUtils.animToFadeOut(this.Hint);
	}

	protected void OnClickBtnQuit(GameObject sender)
	{
		if (this.BtnQuitAction != null)
		{
			EventDispatcher.Broadcast("GuideManager.InstanceExit");
			this.BtnQuitAction.Invoke();
		}
	}

	public void ShowBattleTimeUI(bool isShow)
	{
		this.battleTimeUI.get_gameObject().SetActive(isShow);
	}

	public void ShowMiniMapUI(bool isShow)
	{
		if (isShow)
		{
			if (this.MiniMap)
			{
				if (!this.MiniMap.get_activeSelf())
				{
					this.MiniMap.SetActive(true);
				}
			}
			else
			{
				this.MiniMap = ResourceManager.GetInstantiate2Prefab("RadarMinimapUI");
				this.MiniMap.set_name("MiniMap");
				if (this.MiniMap)
				{
					UGUITools.SetParent(this.RadarMinimapUIRoot, this.MiniMap, true);
				}
			}
		}
		else if (this.MiniMap && this.MiniMap.get_activeSelf())
		{
			this.MiniMap.SetActive(false);
		}
	}

	public void ShowGuildBossBattleTime(bool isShow)
	{
		if (this.guildBossBattleTimeObj.get_activeSelf() == isShow)
		{
			return;
		}
		this.guildBossBattleTimeObj.SetActive(isShow);
	}

	public void SetGuildBossBattleTimeText(string content)
	{
		this.guildBossBattleTimeText.set_text(content);
	}

	public void ShowTopLeftTabs(bool isShow, params BattleUI.TopLeftTabData[] args)
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

	public void ShowGlobalRank(bool isShow, BattleUI.RankRewardType type)
	{
		if (this.GlobalRank.get_activeSelf() != isShow)
		{
			this.GlobalRank.SetActive(isShow);
		}
		if (isShow)
		{
			this.curRankRewardType = type;
			if (type != BattleUI.RankRewardType.Text)
			{
				if (type == BattleUI.RankRewardType.Icon)
				{
					if (this.GlobalRankRewardText.get_gameObject().get_activeSelf())
					{
						this.GlobalRankRewardText.get_gameObject().SetActive(false);
					}
					if (!this.GlobalRankReward.get_gameObject().get_activeSelf())
					{
						this.GlobalRankReward.get_gameObject().SetActive(true);
					}
				}
			}
			else
			{
				if (!this.GlobalRankRewardText.get_gameObject().get_activeSelf())
				{
					this.GlobalRankRewardText.get_gameObject().SetActive(true);
				}
				if (this.GlobalRankReward.get_gameObject().get_activeSelf())
				{
					this.GlobalRankReward.get_gameObject().SetActive(false);
				}
			}
			this.AddUpdateGlobalRankAction();
		}
		else
		{
			this.RemoveUpdateGlobalRankAction();
			this.lastGlobalRank = 0;
			this.isShowGlobalRankOpenAnim = false;
			this.ClearAllGlobalRankFx();
		}
	}

	protected void AddUpdateGlobalRankAction()
	{
		BattleTimeManager.Instance.AddCurrentTimeUIAction(new Action<int>(this.UpdateGlobalRank));
	}

	protected void RemoveUpdateGlobalRankAction()
	{
		BattleTimeManager.Instance.RemoveCurrentTimeUIAction(new Action<int>(this.UpdateGlobalRank));
	}

	protected void UpdateGlobalRank(int secondTime)
	{
		int passTime = (int)(TimeManager.Instance.PreciseServerTime - BattleTimeManager.Instance.StartTime).get_TotalSeconds();
		int rankByPassTime = InstanceManager.CurrentInstance.GetRankByPassTime(passTime);
		int standardTimeByRank = InstanceManager.CurrentInstance.GetStandardTimeByRank(rankByPassTime);
		if (rankByPassTime < 0)
		{
			return;
		}
		this.TryUpdateRank(rankByPassTime);
		this.TryUpdateTime(standardTimeByRank, passTime);
	}

	protected void TryUpdateRank(int curGlobalRank)
	{
		if (curGlobalRank == this.lastGlobalRank)
		{
			return;
		}
		this.lastGlobalRank = curGlobalRank;
		ResourceManager.SetIconSprite(this.GlobalRankInfoIconFG, this.GetRankSpriteName(curGlobalRank));
		this.GlobalRankInfoIconFG.SetNativeSize();
		if (curGlobalRank == 1)
		{
			this.isShowGlobalRankOpenAnim = true;
			this.GlobalRankInfoIcon.set_anchoredPosition(new Vector2(640f, -90f));
			this.PlayGlobalRankInfoIconFx();
			this.UpdateGlobalRankOpenAnim();
		}
		else
		{
			this.ClearGlobalRankInfoIconFx();
			this.PlayGlobalRankExplodeFx();
		}
		BattleUI.RankRewardType rankRewardType = this.curRankRewardType;
		if (rankRewardType != BattleUI.RankRewardType.Text)
		{
			if (rankRewardType == BattleUI.RankRewardType.Icon)
			{
				this.SetRankReward(InstanceManager.CurrentInstance.GetRankReward(curGlobalRank));
			}
		}
		else
		{
			this.SetRankRewardText(InstanceManager.CurrentInstance.GetRankRewardText(curGlobalRank));
		}
	}

	protected string GetRankSpriteName(int rank)
	{
		switch (rank)
		{
		case 1:
			return "lcxs_quality_5";
		case 2:
			return "lcxs_quality_4";
		case 3:
			return "lcxs_quality_3";
		case 4:
			return "lcxs_quality_2";
		default:
			return "lcxs_quality_1";
		}
	}

	protected void TryUpdateTime(int standardRankTime, int passTime)
	{
		int num = standardRankTime - passTime;
		if (num <= 10 && num >= 0 && this.isCanShowGlobalRankBGFx)
		{
			this.PlayGlobalRankBGFx();
			this.isCanShowGlobalRankBGFx = false;
		}
		else if (num > 10 || num < 0)
		{
			this.ClearGlobalRankBGFx();
			this.isCanShowGlobalRankBGFx = true;
		}
		this.SetRankInfoText(InstanceManager.CurrentInstance.GetRankInfoText(this.lastGlobalRank, num));
	}

	protected void SetRankInfoText(string text)
	{
		this.GlobalRankInfoText.set_text(text);
	}

	protected void SetRankRewardText(string text)
	{
		this.GlobalRankRewardText.set_text(text);
	}

	protected void SetRankReward(XDict<int, long> reward)
	{
		if (this.GlobalRankReward)
		{
			for (int i = 0; i < this.GlobalRankReward.get_childCount(); i++)
			{
				Object.Destroy(this.GlobalRankReward.GetChild(i).get_gameObject());
			}
		}
		if (reward != null)
		{
			for (int j = 0; j < reward.Count; j++)
			{
				ItemShow.ShowItem(this.GlobalRankReward, reward.ElementKeyAt(j), reward.ElementValueAt(j), false, null, 2001);
			}
		}
	}

	protected void PlayGlobalRankBGFx()
	{
		this.globalRankBGFxID = FXSpineManager.Instance.ReplaySpine(this.globalRankBGFxID, 4401, this.GlobalRankBGFxSlot, "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1.143f, false, FXMaskLayer.MaskState.None);
	}

	protected void ClearGlobalRankBGFx()
	{
		if (this.globalRankBGFxID > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.globalRankBGFxID, true);
			this.globalRankBGFxID = 0;
		}
	}

	protected void PlayGlobalRankInfoIconFx()
	{
		this.globalRankInfoIconFxID = FXSpineManager.Instance.ReplaySpine(this.globalRankInfoIconFxID, 4402, this.GlobalRankInfoIconFG.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void ClearGlobalRankInfoIconFx()
	{
		if (this.globalRankInfoIconFxID > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.globalRankInfoIconFxID, true);
			this.globalRankInfoIconFxID = 0;
		}
	}

	protected void PlayGlobalRankExplodeFx()
	{
		this.globalRankExplodeFxID = FXSpineManager.Instance.PlaySpine(4403, this.GlobalRankInfoIconExplodeFxSlot, "BattleUI", 2003, new Action(this.ClearGlobalRankExplodeFx), "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void ClearGlobalRankExplodeFx()
	{
		if (this.globalRankExplodeFxID > 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.globalRankExplodeFxID, true);
			this.globalRankExplodeFxID = 0;
		}
	}

	protected void ClearAllGlobalRankFx()
	{
		this.ClearGlobalRankBGFx();
		this.ClearGlobalRankInfoIconFx();
		this.ClearGlobalRankExplodeFx();
	}

	protected void UpdateGlobalRankOpenAnim()
	{
		if (!this.isShowGlobalRankOpenAnim)
		{
			return;
		}
		float num = Vector2.Distance(this.GlobalRankInfoIcon.get_anchoredPosition(), new Vector2(0f, 10f));
		if (num <= 1f)
		{
			this.isShowGlobalRankOpenAnim = false;
			this.GlobalRankInfoIcon.set_anchoredPosition(new Vector2(10f, 10f));
			this.GlobalRankInfoIconBG.set_localScale(new Vector3(0.6f, 0.6f, 1f));
			this.GlobalRankInfoIconFG.get_transform().set_localScale(new Vector3(0.45f, 0.45f, 1f));
			this.PlayGlobalRankExplodeFx();
			return;
		}
		this.GlobalRankInfoIcon.set_anchoredPosition(Vector2.MoveTowards(this.GlobalRankInfoIcon.get_anchoredPosition(), new Vector2(0f, 10f), (float)((int)(this.globalRankInfoIconVelocity + (num / 2f - this.globalRankInfoIconVelocity) * Time.get_deltaTime()))));
		this.GlobalRankInfoIconBG.set_localScale(Vector3.get_one() * Mathf.Lerp(1f, 0.6f, this.globalRankInfoIconVelocity * Time.get_deltaTime()));
		this.GlobalRankInfoIconFG.get_transform().set_localScale(Vector3.get_one() * Mathf.Lerp(1f, 0.45f, this.globalRankInfoIconVelocity * Time.get_deltaTime()));
	}

	public void TabShowGlobalRank(bool isShow)
	{
		if (this.GlobalRank.get_activeSelf() != isShow)
		{
			this.GlobalRank.SetActive(isShow);
		}
	}

	public void ShowTeamMember(bool isShow)
	{
		if (this.TeamMember.get_activeSelf() != isShow)
		{
			this.TeamMember.SetActive(isShow);
		}
	}

	protected void ResetPlayer()
	{
		for (int i = 0; i < this.playerItem.get_Count(); i++)
		{
			this.playerItem.get_Item(i).get_gameObject().SetActive(false);
		}
	}

	public void SetPlayerID(int index, long id)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		if (!this.playerItem.get_Item(index).get_gameObject().get_activeSelf())
		{
			this.playerItem.get_Item(index).get_gameObject().SetActive(true);
		}
		this.playerItem.get_Item(index).SetPlayerID(id);
	}

	public void SetPlayerHead(int index, int icon)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		this.playerItem.get_Item(index).SetPlayerHead(icon);
	}

	public void SetPlayerLevel(int index, int level)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		this.playerItem.get_Item(index).SetPlayerLevel(level);
	}

	public void SetPlayerName(int index, string name)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		this.playerItem.get_Item(index).SetPlayerName(name);
	}

	public void SetPlayerVipLv(int index, int vipLv)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		this.playerItem.get_Item(index).SetPlayerVipLv(vipLv);
	}

	public void SetPlayerHp(int index, long hp, long hpLmt)
	{
		if (index >= this.playerItem.get_Count())
		{
			return;
		}
		this.playerItem.get_Item(index).SetPlayerHp(hp, hpLmt);
	}

	public void TabShowTeamMember(bool isShow)
	{
		if (this.TeamMember.get_activeSelf() != isShow)
		{
			this.TeamMember.SetActive(isShow);
		}
	}

	public void ShowBackpackBtn(bool isShow)
	{
		this.BtnBackpack.get_gameObject().SetActive(isShow);
	}

	protected void OnClickBtnBackpack(GameObject sender)
	{
		BattleBackpackUI battleBackpackUI = UIManagerControl.Instance.OpenUI("BattleBackpackUI", null, false, UIType.NonPush) as BattleBackpackUI;
		battleBackpackUI.SetTitleName(GameDataUtils.GetChineseContent(511612, false));
		battleBackpackUI.ShowTips(false, string.Empty);
		InstanceManager.CurrentInstance.UpdateRealTimeDrop(InstanceManager.RealTimeDropCache);
	}

	public void ShowBackpackPreviewBtn(bool isShow)
	{
		this.BtnBackpackPreview.get_gameObject().SetActive(isShow);
	}

	protected void OnClickBtnBackpackPreview(GameObject sender)
	{
		BattleBackpackUI battleBackpackUI = UIManagerControl.Instance.OpenUI("BattleBackpackUI", null, false, UIType.NonPush) as BattleBackpackUI;
		battleBackpackUI.SetTitleName(GameDataUtils.GetChineseContent(50707, false));
		battleBackpackUI.ShowTips(true, GameDataUtils.GetChineseContent(50708, false));
		InstanceManager.CurrentInstance.UpdateRealTimeDrop(InstanceManager.RealTimeDropCache);
	}

	public void ShowCallBoss(bool isShow)
	{
		this.CallBoss.SetActive(isShow);
	}

	public void SetSpecialBattleStage()
	{
		Text component = base.FindTransform("Stage").GetComponent<Text>();
		if (component != null)
		{
			component.set_text(string.Format(GameDataUtils.GetChineseContent(512050, false), SurvivalManager.Instance.BattleCurrentStage));
		}
	}

	public void ShowBountyBattleUI(bool isShow)
	{
		this.BountyBattleUI.SetActive(isShow);
	}

	public void ShowSpecialInstanceBattleUI(bool isShow)
	{
		this.SpecialInstanceBattleUI.SetActive(isShow);
	}

	public void InitSpecialInstanceBattleUI()
	{
		this.SpecialInstanceBattleUI.GetComponent<SpecialInstanceBattleUI>().init();
	}

	public void ShowRewardPreviewUI(bool isShow, RewardPreviewUI.CopyType type = RewardPreviewUI.CopyType.NONE, long defaultExp = 0L, long defaultBatchExp = 0L)
	{
		this.mRewardPreviewUI.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.SetRewardPreviewText(defaultExp, defaultBatchExp);
			this.mRewardPreviewUI.SetShowType(type);
		}
	}

	public void SetRewardPreviewText(long defaultExp = 0L, long defaultBatchExp = 0L)
	{
		if (this.mRewardPreviewUI != null)
		{
			this.mRewardPreviewUI.SetDefaultExp(defaultExp, defaultBatchExp);
		}
	}

	public void SetRewardPreviewDefaultExpText(long defaultNum = 0L)
	{
		if (this.mRewardPreviewUI != null)
		{
			this.mRewardPreviewUI.SetDefaultExpText(defaultNum);
		}
	}

	public void ShowSpecialBuffUI(bool isShow)
	{
		this.mSpecialBuffUI.SetActive(isShow);
		if (isShow && this.mSpecialBuffUI.get_transform().get_childCount() == 3)
		{
			FXSpineManager.Instance.PlaySpine(902, this.mSpecialBuffUI.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public void ShowTramcarRewardUI(bool isShow)
	{
		this.mTramcarRewardUI.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			if (TramcarManager.Instance.FightInfo.helpRoleId > 0L)
			{
				this.mTramcarRewardUI.set_anchoredPosition(new Vector2(0f, -257f));
			}
			else
			{
				this.mTramcarRewardUI.set_anchoredPosition(new Vector2(0f, -127f));
			}
			this.ShowBattleTimeUI(TramcarManager.Instance.CurLootQulity <= 0);
		}
	}

	public void ShowGuildWarCityResouce(bool isShow)
	{
		if (this.GuildWarCityResouce.get_activeSelf() != isShow)
		{
			this.GuildWarCityResouce.SetActive(isShow);
		}
		this.ShowGuildWarCityTimeCountDown(isShow);
	}

	public void SetGuildWarCityResouceInfo(GuildResourceInfo myGuildInfo, GuildResourceInfo enemyGuildInfo)
	{
		if ((myGuildInfo != null && myGuildInfo.IsLeft) || (enemyGuildInfo != null && !enemyGuildInfo.IsLeft))
		{
			this.SetGuildWarCityResouceLeftInfo(myGuildInfo, "38C6F4");
			this.SetGuildWarCityResouceRightInfo(enemyGuildInfo, "FF4141");
		}
		else
		{
			this.SetGuildWarCityResouceLeftInfo(enemyGuildInfo, "FF4141");
			this.SetGuildWarCityResouceRightInfo(myGuildInfo, "38C6F4");
		}
	}

	protected void SetGuildWarCityResouceLeftInfo(GuildResourceInfo guildInfo, string color = "38C6F4")
	{
		if (guildInfo == null)
		{
			this.SetGuildWarCityResouceLeftTeamName(GameDataUtils.GetChineseContent(515082, false), "38C6F4");
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

	protected void SetGuildWarCityResouceRightInfo(GuildResourceInfo guildInfo, string color = "38C6F4")
	{
		if (guildInfo == null)
		{
			this.SetGuildWarCityResouceLeftTeamName(GameDataUtils.GetChineseContent(515082, false), "38C6F4");
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

	protected void SetGuildWarCityResouceLeftTeamName(string text, string color = "38C6F4")
	{
		if (this.GuildWarCityResouceLeftTeamName != null)
		{
			this.GuildWarCityResouceLeftTeamName.set_text(TextColorMgr.GetColor(text, color, string.Empty));
		}
	}

	protected void SetGuildWarCityResouceLeftTeamNum(string text)
	{
		if (this.GuildWarCityResouceLeftTeamNum != null)
		{
			this.GuildWarCityResouceLeftTeamNum.set_text(text);
		}
	}

	protected void SetGuildWarCityResouceLeftTeamBar(float percentage)
	{
		if (this.GuildWarCityResouceLeftTeamBar != null)
		{
			this.GuildWarCityResouceLeftTeamBar.set_sizeDelta(new Vector2(215f * percentage, this.GuildWarCityResouceLeftTeamBar.get_sizeDelta().y));
		}
	}

	protected void SetGuildWarCityResouceLeftTeamBarText(string text)
	{
		if (this.GuildWarCityResouceLeftTeamBarText != null)
		{
			this.GuildWarCityResouceLeftTeamBarText.set_text(text);
		}
	}

	protected void SetGuildWarCityResouceRightTeamName(string text, string color = "3FCB19")
	{
		if (this.GuildWarCityResouceRightTeamName != null)
		{
			this.GuildWarCityResouceRightTeamName.set_text(TextColorMgr.GetColor(text, color, string.Empty));
		}
	}

	protected void SetGuildWarCityResouceRightTeamNum(string text)
	{
		if (this.GuildWarCityResouceRightTeamNum != null)
		{
			this.GuildWarCityResouceRightTeamNum.set_text(text);
		}
	}

	protected void SetGuildWarCityResouceRightTeamBar(float percentage)
	{
		if (this.GuildWarCityResouceRightTeamBar != null)
		{
			this.GuildWarCityResouceRightTeamBar.set_sizeDelta(new Vector2(215f * percentage, this.GuildWarCityResouceRightTeamBar.get_sizeDelta().y));
		}
	}

	protected void SetGuildWarCityResouceRightTeamBarText(string text)
	{
		if (this.GuildWarCityResouceRightTeamBarText != null)
		{
			this.GuildWarCityResouceRightTeamBarText.set_text(text);
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
			if (this.GuildWarCityTimeCDText != null)
			{
				this.GuildWarCityTimeCDText.set_text(GameDataUtils.GetChineseContent(513538, false));
			}
		}, true);
	}

	private void SetGuildWarCityTimeCountDownText(string second)
	{
		if (this.GuildWarCityTimeCDText != null)
		{
			this.GuildWarCityTimeCDText.set_text("军团争霸剩余： " + second);
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

	public void ShowGuildWarMineCD(bool isShow, int mineCD = 0)
	{
		if (this.GuildWarMineCD.get_activeSelf() != isShow)
		{
			this.GuildWarMineCD.SetActive(isShow);
		}
		if (isShow)
		{
			this.SetGuildWarMineCD(mineCD);
		}
		this.UpdateGuildWarMinePos();
	}

	protected void SetGuildWarMineCD(int mineCD)
	{
		this.ResetGuildWarMineCD();
		this.GuildWarMineCDText.set_text(string.Format(GameDataUtils.GetChineseContent(515120, false), mineCD));
		this.GuildWarMineTimeCD = new TimeCountDown(mineCD, TimeFormat.HHMMSS, delegate
		{
			if (this.GuildWarMineTimeCD != null)
			{
				this.GuildWarMineCDText.set_text(string.Format(GameDataUtils.GetChineseContent(515120, false), this.GuildWarMineTimeCD.GetTime()));
			}
		}, delegate
		{
			this.ResetGuildWarMineCD();
			this.ShowGuildWarMineCD(false, 0);
		}, true);
	}

	protected void ResetGuildWarMineCD()
	{
		if (this.GuildWarMineTimeCD != null)
		{
			this.GuildWarMineTimeCD.Dispose();
			this.GuildWarMineTimeCD = null;
		}
	}

	protected void UpdateGuildWarMinePos()
	{
		if (this.GuildWarMineCD.get_activeSelf())
		{
			this.GuildWarMineState.get_transform().set_localPosition(this.GuildWarMineStateSlot1);
		}
		else
		{
			this.GuildWarMineState.get_transform().set_localPosition(this.GuildWarMineStateSlot0);
		}
	}

	public void ShowGuildWarMineState(bool isShow)
	{
		if (this.GuildWarMineState.get_activeSelf() != isShow)
		{
			this.GuildWarMineState.SetActive(isShow);
		}
	}

	public void UpdateGuildWarMineState(GuildWarManager.MineState mineState, long myGuildID, string myGuildName, long enemyGuildID, string enemyGuildName, long progressOwnerID, float curProgress)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"UpdateGuildWarMineState: ",
			mineState,
			" ",
			myGuildID,
			" ",
			myGuildName,
			" ",
			enemyGuildID,
			" ",
			enemyGuildName,
			" ",
			progressOwnerID,
			" ",
			curProgress
		}));
		if (mineState == GuildWarManager.MineState.My)
		{
			if (!this.GuildWarMineStateInfo.get_activeSelf())
			{
				this.GuildWarMineStateInfo.SetActive(true);
			}
			this.GuildWarMineStateInfoName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515126, false), myGuildName), "6adc32", string.Empty));
			if (progressOwnerID == enemyGuildID)
			{
				if (!this.GuildWarMineStateBar.get_activeSelf())
				{
					this.GuildWarMineStateBar.SetActive(true);
				}
				this.GuildWarMineStateBar.get_transform().set_localPosition(this.GuildWarMineStateBarSlot1);
				this.GuildWarMineStateBarName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515125, false), enemyGuildName), "ff4040", string.Empty));
				ResourceManager.SetCodeSprite(this.GuildWarMineStateBarFG, "new_boss_blood1");
				this.GuildWarMineStateBarFG.set_fillAmount(curProgress);
			}
			else if (this.GuildWarMineStateBar.get_activeSelf())
			{
				this.GuildWarMineStateBar.SetActive(false);
			}
		}
		else if (mineState == GuildWarManager.MineState.Enemy)
		{
			if (!this.GuildWarMineStateInfo.get_activeSelf())
			{
				this.GuildWarMineStateInfo.SetActive(true);
			}
			this.GuildWarMineStateInfoName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515126, false), enemyGuildName), "ff4040", string.Empty));
			if (progressOwnerID == myGuildID)
			{
				if (!this.GuildWarMineStateBar.get_activeSelf())
				{
					this.GuildWarMineStateBar.SetActive(true);
				}
				this.GuildWarMineStateBar.get_transform().set_localPosition(this.GuildWarMineStateBarSlot1);
				this.GuildWarMineStateBarName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515124, false), myGuildName), "6adc32", string.Empty));
				ResourceManager.SetCodeSprite(this.GuildWarMineStateBarFG, "new_boss_blood3");
				this.GuildWarMineStateBarFG.set_fillAmount(curProgress);
			}
			else if (this.GuildWarMineStateBar.get_activeSelf())
			{
				this.GuildWarMineStateBar.SetActive(false);
			}
		}
		else if (progressOwnerID == myGuildID)
		{
			if (!this.GuildWarMineStateBar.get_activeSelf())
			{
				this.GuildWarMineStateBar.SetActive(true);
			}
			this.GuildWarMineStateBar.get_transform().set_localPosition(this.GuildWarMineStateBarSlot0);
			if (this.GuildWarMineStateInfo.get_activeSelf())
			{
				this.GuildWarMineStateInfo.SetActive(false);
			}
			this.GuildWarMineStateBarName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515124, false), myGuildName), "6adc32", string.Empty));
			ResourceManager.SetCodeSprite(this.GuildWarMineStateBarFG, "new_boss_blood3");
			this.GuildWarMineStateBarFG.set_fillAmount(curProgress);
		}
		else if (progressOwnerID == enemyGuildID)
		{
			if (!this.GuildWarMineStateBar.get_activeSelf())
			{
				this.GuildWarMineStateBar.SetActive(true);
			}
			this.GuildWarMineStateBar.get_transform().set_localPosition(this.GuildWarMineStateBarSlot0);
			if (this.GuildWarMineStateInfo.get_activeSelf())
			{
				this.GuildWarMineStateInfo.SetActive(false);
			}
			this.GuildWarMineStateBarName.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515125, false), enemyGuildName), "ff4040", string.Empty));
			ResourceManager.SetCodeSprite(this.GuildWarMineStateBarFG, "new_boss_blood1");
			this.GuildWarMineStateBarFG.set_fillAmount(curProgress);
		}
		else
		{
			if (this.GuildWarMineStateBar.get_activeSelf())
			{
				this.GuildWarMineStateBar.SetActive(false);
			}
			if (!this.GuildWarMineStateInfo.get_activeSelf())
			{
				this.GuildWarMineStateInfo.SetActive(true);
			}
			this.GuildWarMineStateInfoName.set_text(GameDataUtils.GetChineseContent(515118, false));
		}
	}

	public void ShowGuildWarBuff(bool isShow, int icon = 0, Action callback = null)
	{
		if (this.GuildWarBuff.get_activeSelf() != isShow)
		{
			this.GuildWarBuff.SetActive(isShow);
		}
		if (isShow)
		{
			ResourceManager.SetSprite(this.GuildWarBuffFG, GameDataUtils.GetIcon(icon));
			this.GuildWarBuffCallback = callback;
		}
	}

	protected void OnClickGuildWarBuff(GameObject go)
	{
		if (this.GuildWarBuffCallback != null)
		{
			this.GuildWarBuffCallback.Invoke();
		}
	}

	public void ShowPeakBattleSlot(bool isShow)
	{
		if (this.peakBattleVSInfo != null && this.peakBattleVSInfo.get_activeSelf() != isShow)
		{
			this.peakBattleVSInfo.SetActive(isShow);
		}
		if (this.peakBattleInfoSlot != null && this.peakBattleInfoSlot.get_activeSelf() != isShow)
		{
			this.peakBattleInfoSlot.SetActive(isShow);
		}
	}

	public void SetPeakBattleKillAndDeathNumText(int killNum, int DeathNum)
	{
		if (this.peakBattleDeathNumText != null)
		{
			this.peakBattleDeathNumText.set_text(DeathNum + string.Empty);
		}
		if (this.peakBattleKillNumText != null)
		{
			this.peakBattleKillNumText.set_text(killNum + string.Empty);
		}
	}

	public void SetPeakBattleVSText(int myTeamTotalNum, int enemyTeamTotalNum)
	{
		this.SetPeakBattleVSLeftText(myTeamTotalNum);
		this.SetPeakBattleVSRightText(enemyTeamTotalNum);
	}

	private void SetPeakBattleVSLeftText(int teamTotalNum)
	{
		if (this.peakBattleVSLeftText != null)
		{
			this.peakBattleVSLeftText.set_text(teamTotalNum + "/" + MultiPVPManager.Instance.VictoryKillNum);
		}
	}

	private void SetPeakBattleVSRightText(int teamTotalNum)
	{
		if (this.peakBattleVSRightText != null)
		{
			this.peakBattleVSRightText.set_text(teamTotalNum + "/" + MultiPVPManager.Instance.VictoryKillNum);
		}
	}

	public void ShowArea(bool isShow)
	{
		this.Area.SetActive(isShow);
	}

	public void SetAreaName(string text)
	{
		this.AreaText.set_text(text);
	}

	public void ShowPlayerNum(bool isShow)
	{
		this.PlayerNum.SetActive(isShow);
	}

	public void SetPlayerNum(string text)
	{
		this.PlayerNumText.set_text(text);
	}

	public void ShowBossProbability(bool isShow)
	{
		this.BossProbability.SetActive(isShow);
	}

	public void SetBossProbabilityText(string text)
	{
		this.BossProbabilityText.set_text(text);
	}

	public void ShowBattleMode(bool isShow)
	{
		this.BattleMode.SetActive(isShow);
	}

	public void ShowPKModeBtn(bool isShow)
	{
		this.TrueShowPeaceModeBtn(isShow);
	}

	protected void TrueShowPKModeBtn(bool isShow)
	{
		this.BtnPKMode.get_gameObject().SetActive(isShow);
	}

	protected void OnClickBtnPKMode(GameObject sender)
	{
		this.TrueOnClickBtnPeaceMode();
	}

	protected void TrueOnClickBtnPKMode()
	{
		HookInstance.Instance.ChangePKMode();
	}

	public void ShowPKModeCD(bool isShow)
	{
		this.TrueShowPeaceModeCD(isShow);
	}

	protected void TrueShowPKModeCD(bool isShow)
	{
		this.PKModeCD.SetActive(isShow);
	}

	public void SetPKModeCD(float cdTime, float percentage)
	{
		this.TrueSetPeaceModeCD(cdTime, percentage);
	}

	protected void TrueSetPKModeCD(float cdTime, float percentage)
	{
		if (cdTime == 0f || percentage <= 0f)
		{
			this.TrueShowPKModeCD(false);
			this.TrueShowPKModeBtn(true);
			return;
		}
		this.PKModeCDControl.SetCD(cdTime, percentage, delegate
		{
			this.TrueShowPKModeCD(false);
			this.TrueShowPKModeBtn(true);
		});
	}

	public void ShowPeaceModeBtn(bool isShow)
	{
		this.TrueShowPKModeBtn(isShow);
	}

	protected void TrueShowPeaceModeBtn(bool isShow)
	{
		this.BtnPeaceMode.get_gameObject().SetActive(isShow);
	}

	protected void OnClickBtnPeaceMode(GameObject sender)
	{
		this.TrueOnClickBtnPKMode();
	}

	protected void TrueOnClickBtnPeaceMode()
	{
		HookInstance.Instance.ChangePeaceMode();
	}

	public void ShowPeaceModeCD(bool isShow)
	{
		this.TrueShowPKModeCD(isShow);
	}

	protected void TrueShowPeaceModeCD(bool isShow)
	{
		this.PeaceModeCD.SetActive(isShow);
	}

	public void SetPeaceModeCD(float cdTime, float percentage)
	{
		this.TrueSetPKModeCD(cdTime, percentage);
	}

	protected void TrueSetPeaceModeCD(float cdTime, float percentage)
	{
		if (cdTime == 0f || percentage <= 0f)
		{
			this.TrueShowPeaceModeCD(false);
			this.TrueShowPeaceModeBtn(true);
			return;
		}
		this.PeaceModeCDControl.SetCD(cdTime, percentage, delegate
		{
			this.TrueShowPeaceModeCD(false);
			this.TrueShowPeaceModeBtn(true);
		});
	}

	public void PlayBattleBackpackItem(int itemID, long num, Action callback)
	{
		if (!DataReader<Items>.Contains(itemID))
		{
			if (callback != null)
			{
				callback.Invoke();
			}
			return;
		}
		this.ShowBattleBackpackItem(true);
		Items items = DataReader<Items>.Get(itemID);
		ResourceManager.SetSprite(this.BattleBackpackItemFrame, GameDataUtils.GetItemFrame(items));
		ResourceManager.SetSprite(this.BattleBackpackItemIcon, GameDataUtils.GetIcon(items.icon));
		this.BattleBackpackItemNum.set_text((num != 0L) ? num.ToString() : string.Empty);
		if (items.step > 0)
		{
			this.BattleBackpackItemStep.SetActive(true);
			this.BattleBackpackItemStepNum.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
		else
		{
			this.BattleBackpackItemStep.SetActive(false);
		}
		this.BattleBackpackItemTween.Reset(false, true);
		this.battleBackpackItemFxID = FXSpineManager.Instance.PlaySpine(3305, this.BattleBackpackItem.get_transform(), string.Empty, 14999, delegate
		{
			FXSpineManager.Instance.DeleteSpine(this.battleBackpackItemFxID, true);
			this.BattleBackpackItemTween.MoveTo(this.BattleBackpackItemTweenDstPos, 0.5f, delegate
			{
				this.ShowBattleBackpackItem(false);
				this.battleBackpackItemFxID = FXSpineManager.Instance.PlaySpine(3305, this.BtnBackpack.get_transform(), string.Empty, 14999, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.battleBackpackItemFxID, true);
					if (callback != null)
					{
						callback.Invoke();
					}
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			});
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void ShowBattleBackpackItem(bool isShow)
	{
		this.BattleBackpackItem.SetActive(isShow);
	}

	public void ShowBatchText(bool isShow)
	{
		this.BatchText.SetActive(isShow);
	}

	public void SetBatchTextNum(int num)
	{
		this.BatchTextNum.set_text(num.ToString());
	}

	public void ShowDamageRankingUI(bool isShow)
	{
		this.damageRankingUI.get_gameObject().SetActive(isShow);
	}

	public void ShowDamageRanking2UI(bool isShow)
	{
		this.damageRanking2UI.get_gameObject().SetActive(isShow);
	}

	protected void OnClickBtnSwitch(GameObject sender)
	{
		FollowCamera.instance.OnSwitchCamera();
	}

	protected void OnClickBtnWin(GameObject sender)
	{
		Debuger.Info("OnClickBtnWin", new object[0]);
		if (InstanceManager.CurrentInstanceType == DungeonNormalInstance.Instance.Type)
		{
			DungeonManager.Instance.SendSettleDungeonReq(false, !BattleBlackboard.Instance.IsAllMateAlive, true, BattleBlackboard.Instance.IsAllPetAlive, (int)BattleBlackboard.Instance.SelfLowestHPPercentage, (int)BattleBlackboard.Instance.PetLowestHPPercentage, (int)BattleBlackboard.Instance.SelfCurrentHPPercentage, (int)BattleBlackboard.Instance.BossLifeTime, BattleBlackboard.Instance.AllAttendPet, BattleBlackboard.Instance.AllFusePet, BattleBlackboard.Instance.IsAllNPCDead, BattleBlackboard.Instance.IsAnyNPCDead, BattleBlackboard.Instance.IsAllNPCArrived, true, BattleBlackboard.Instance.FinishTime, BattleBlackboard.Instance.DeadMonserCount);
		}
	}

	protected void OnClickBtnLose(GameObject sender)
	{
		ClientApp.Instance.ReInit();
	}

	protected void OnClickPetDie(GameObject sender)
	{
		ClientGMManager.ShowDebugInfo(true);
	}

	protected void OnClicHeroDie(GameObject sender)
	{
	}

	protected void OnClickLogOn(GameObject sender)
	{
		Debuger.Info("OnClickBtnWin", new object[0]);
		ClientApp.Instance.isShowFightLog = !ClientApp.Instance.isShowFightLog;
		if (ClientApp.Instance.isShowFightLog)
		{
			this.ButtonLogOn.get_transform().FindChild("Text").GetComponent<Text>().set_text("Log/Off");
		}
		else
		{
			this.ButtonLogOn.get_transform().FindChild("Text").GetComponent<Text>().set_text("Log/On");
		}
	}

	public void ShowButtonGM(bool isShow)
	{
		base.FindTransform("LeftToGM").get_gameObject().SetActive(isShow);
	}

	public void SetSelfActPoint(int actPoint)
	{
		this.ActPointBarProgress.set_fillAmount((float)(actPoint % 5) / 5f);
		if (actPoint < 5)
		{
			this.ActPointImage1.set_enabled(false);
			this.ActPointImage2.set_enabled(false);
		}
		else if (actPoint >= 5 && actPoint < 10)
		{
			if (!this.ActPointImage1.get_enabled())
			{
				FXSpineManager.Instance.PlaySpine(205, this.ActPointImage1.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			this.ActPointImage1.set_enabled(true);
			this.ActPointImage2.set_enabled(false);
		}
		else if (actPoint == 10)
		{
			if (!this.ActPointImage1.get_enabled())
			{
				FXSpineManager.Instance.PlaySpine(205, this.ActPointImage1.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			if (!this.ActPointImage2.get_enabled())
			{
				FXSpineManager.Instance.PlaySpine(205, this.ActPointImage2.get_transform(), "BattleUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			this.ActPointImage1.set_enabled(true);
			this.ActPointImage2.set_enabled(true);
		}
	}

	protected void SetModeDefaultValue(int num)
	{
		this.normalMode.Add(this.skillBtn[1].get_transform());
		this.normalMode.Add(this.skillBtn[8].get_transform());
		this.normalMode.Add(this.skillBtn[11].get_transform());
		this.normalMode.Add(this.skillBtn[12].get_transform());
		this.normalMode.Add(this.skillBtn[13].get_transform());
		if (num > 0)
		{
			this.normalMode.Add(this.skillBtn[21].get_transform());
		}
		else
		{
			Utils.SetTransformZOn(this.skillBtn[21].get_transform(), false);
		}
		if (num > 1)
		{
			this.normalMode.Add(this.skillBtn[22].get_transform());
		}
		else
		{
			Utils.SetTransformZOn(this.skillBtn[22].get_transform(), false);
		}
		if (num > 2)
		{
			this.normalMode.Add(this.skillBtn[23].get_transform());
		}
		else
		{
			Utils.SetTransformZOn(this.skillBtn[23].get_transform(), false);
		}
		this.fuseMode.Add(this.skillBtn[1].get_transform());
	}

	public void SetSelfFuse(bool isFuse)
	{
		this.SetSkillMode(isFuse);
		if (isFuse)
		{
			this.IsSelfFuse = isFuse;
			Utils.SetTransformZOn(this.FuseTimeBar.get_transform(), true);
		}
		else
		{
			Utils.SetTransformZOn(this.FuseTimeBar.get_transform(), false);
			if (this.IsSelfFuse && !isFuse)
			{
				this.IsSelfFuse = isFuse;
			}
			else
			{
				this.IsSelfFuse = isFuse;
			}
		}
	}

	protected void SetSkillMode(bool isFuse)
	{
		if (isFuse)
		{
			for (int i = 0; i < this.normalMode.get_Count(); i++)
			{
				Utils.SetTransformZOn(this.normalMode.get_Item(i), false);
			}
			for (int j = 0; j < this.fuseMode.get_Count(); j++)
			{
				Utils.SetTransformZOn(this.fuseMode.get_Item(j), true);
			}
		}
		else
		{
			for (int k = 0; k < this.fuseMode.get_Count(); k++)
			{
				Utils.SetTransformZOn(this.fuseMode.get_Item(k), false);
			}
			for (int l = 0; l < this.normalMode.get_Count(); l++)
			{
				Utils.SetTransformZOn(this.normalMode.get_Item(l), true);
			}
		}
	}

	protected void SetFuseTime(float time)
	{
		bool startFuseCD = true;
		float fuseTime = time;
		float fuseTimeTicker = time;
		if (this.fuseTimerID != 0u)
		{
			TimerHeap.DelTimer(this.fuseTimerID);
		}
		this.fuseTimerID = TimerHeap.AddTimer(0u, 25, delegate
		{
			if (!startFuseCD)
			{
				return;
			}
			fuseTimeTicker -= 25f;
			if (fuseTimeTicker < 0f)
			{
				fuseTimeTicker = 0f;
			}
			if (this.FuseTimeBarProgress != null)
			{
				this.FuseTimeBarProgress.set_fillAmount(fuseTimeTicker / fuseTime);
			}
			if (fuseTimeTicker == 0f)
			{
				startFuseCD = false;
				TimerHeap.DelTimer(this.fuseTimerID);
			}
		});
	}

	private void OnPetCountDownEnd(int index)
	{
		this.UpdatePetBtnBgState(index, false);
	}

	private void UpdatePetBtnBgState(int index, bool isShow)
	{
		index++;
		string transformName = "BtnFuse" + index.ToString();
		if (base.FindTransform(transformName) != null && base.FindTransform(transformName).FindChild("NumImageBg") != null)
		{
			base.FindTransform(transformName).FindChild("NumImageBg").get_gameObject().SetActive(isShow);
		}
	}

	private void ResetPetBtnBgState()
	{
		for (int i = 1; i <= 3; i++)
		{
			string transformName = "BtnFuse" + i.ToString();
			if (base.FindTransform(transformName) != null && base.FindTransform(transformName).FindChild("NumImageBg") != null)
			{
				base.FindTransform(transformName).FindChild("NumImageBg").get_gameObject().SetActive(false);
			}
		}
	}
}
