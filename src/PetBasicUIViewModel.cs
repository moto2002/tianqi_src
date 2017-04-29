using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class PetBasicUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_SubPanelPets = "SubPanelPets";

		public const string Attr_SubPanelFormation = "SubPanelFormation";

		public const string Attr_SubFormationBadge = "SubFormationBadge";

		public const string Attr_SubPanelCombination = "SubPanelCombination";

		public const string Attr_SubPanelPetInfo = "SubPanelPetInfo";

		public const string Attr_SubPanelPetInfoRoot = "SubPanelPetInfoRoot";

		public const string Attr_FnBtnLevel = "FnBtnLevel";

		public const string Attr_FnBtnLevelBadge = "FnBtnLevelBadge";

		public const string Attr_FnBtnUpgrade = "FnBtnUpgrade";

		public const string Attr_FnBtnUpgradeBadge = "FnBtnUpgradeBadge";

		public const string Attr_FnBtnSkillEvo = "FnBtnSkillEvo";

		public const string Attr_FnBtnSkillEvoBadge = "FnBtnSkillEvoBadge";

		public const string Attr_SubPanelNaturalEvo = "SubPanelNaturalEvo";

		public const string Attr_SubPanelSkillEvo = "SubPanelSkillEvo";

		public const string Attr_PetTalents = "PetTalents";

		public const string Attr_PetTypeIcon = "PetTypeIcon";

		public const string Attr_PetTypeZi = "PetTypeZi";

		public const string Attr_PetBattlePower = "PetBattlePower";

		public const string Attr_PetName = "PetName";

		public const string Attr_PetLevel = "PetLevel";

		public const string Attr_PetQuality = "PetQuality";

		public const string Attr_MorphBtnText = "MorphBtnText";

		public const string Attr_BasicAttrs = "BasicAttrs";

		public const string Attr_SupportSkillIcon = "SupportSkillIcon";

		public const string Attr_SupportSkillHSV = "SupportSkillHSV";

		public const string Attr_ShowArrow = "ShowArrow";

		public const string Attr_PetEXPs = "PetEXPs";

		public const string Attr_EXPNum = "EXPNum";

		public const string Attr_EXPNumAmount = "EXPNumAmount";

		public const string Attr_BtnResetLevelOn = "BtnResetLevelOn";

		public const string Attr_BtnLevelOn = "BtnLevelOn";

		public const string Attr_SkillPointInfo = "SkillPointInfo";

		public const string Attr_SPBtnBuyEnable = "SPBtnBuyEnable";

		public const string Attr_PetAttackNow = "PetAttackNow";

		public const string Attr_LevelBasicAttrs = "LevelBasicAttrs";

		public const string Attr_FragNumAmount = "FragNumAmount";

		public const string Attr_FragNum = "FragNum";

		public const string Attr_FragmentBtnName = "FragmentBtnName";

		public const string Attr_IsFragmentFull = "IsFragmentFull";

		public const string Attr_UpgradeMax = "UpgradeMax";

		public const string Attr_UpgradePetNowFrame = "UpgradePetNowFrame";

		public const string Attr_UpgradePetNowFramePet = "UpgradePetNowFramePet";

		public const string Attr_UpgradePetNowIcon = "UpgradePetNowIcon";

		public const string Attr_UpgradePetNowQuality = "UpgradePetNowQuality";

		public const string Attr_UpgradePetNowFighting = "UpgradePetNowFighting";

		public const string Attr_UpgradePetUpFrame = "UpgradePetUpFrame";

		public const string Attr_UpgradePetUpFramePet = "UpgradePetUpFramePet";

		public const string Attr_UpgradePetUpIcon = "UpgradePetUpIcon";

		public const string Attr_UpgradePetUpQuality = "UpgradePetUpQuality";

		public const string Attr_UpgradePetUpFighting = "UpgradePetUpFighting";

		public const string Attr_ShowUpNaturalEvo = "ShowUpNaturalEvo";

		public const string Attr_UpNaturalEvoIcon = "UpNaturalEvoIcon";

		public const string Attr_UpNaturalEvoName = "UpNaturalEvoName";

		public const string Attr_UpNaturalEvoDesc = "UpNaturalEvoDesc";

		public const string Attr_BtnUpgradeEnable = "BtnUpgradeEnable";

		public const string Attr_BtnFollowBg = "BtnFollowBg";

		public const string Attr_BtnFollowShow = "BtnFollowShow";

		public const string Event_OnMorphBtnUp = "OnMorphBtnUp";

		public const string Event_OnSupportSkillBtnDown = "OnSupportSkillBtnDown";

		public const string Event_OnSupportSkillBtnUp = "OnSupportSkillBtnUp";

		public const string Event_OnSPBtnBuyUp = "OnSPBtnBuyUp";

		public const string Event_OnBtnUpgradeObtainUp = "OnBtnUpgradeObtainUp";

		public const string Event_OnBtnUpgradeUp = "OnBtnUpgradeUp";

		public const string Event_OnBtnPreview = "OnBtnPreview";

		public const string Event_OnPPIArrowL = "OnPPIArrowL";

		public const string Event_OnPPIArrowR = "OnPPIArrowR";

		public const string Event_OnFollowUp = "OnFollowUp";

		public const string Event_OnBtnResetLevel = "OnBtnResetLevel";

		public const string Event_OnBtnLevel1 = "OnBtnLevel1";

		public const string Event_OnBtnLevel10 = "OnBtnLevel10";
	}

	private const string COLOR_NAME = "78503c";

	private const string COLOR_ATTR = "ff7d4b";

	private const string REQUEST_STRING = "---";

	private const string NO_ACTIVATION_TIP = "激活可见";

	public const string NO_ACTIVATION_FIGHTING = "战力: 激活可见";

	public static PetBasicUIViewModel Instance;

	public static int PetModelUID;

	private static long _PetUID;

	private static int _PetID;

	private bool _InFuse;

	private int m_fragmentId;

	private bool _SubPanelPets;

	private bool _SubPanelFormation;

	private bool _SubFormationBadge;

	private bool _SubPanelCombination;

	private bool _SubPanelPetInfo;

	private bool _SubPanelPetInfoRoot;

	private bool _ShowArrow = true;

	private bool _FnBtnLevel;

	private bool _FnBtnLevelBadge;

	private bool _FnBtnUpgrade;

	private bool _FnBtnUpgradeBadge;

	private bool _FnBtnSkillEvo;

	private bool _FnBtnSkillEvoBadge;

	private bool _SubPanelNaturalEvo;

	private bool _SubPanelSkillEvo;

	private SpriteRenderer _PetTypeIcon;

	private SpriteRenderer _PetTypeZi;

	private string _PetBattlePower;

	private string _PetName;

	private string _PetLevel;

	private SpriteRenderer _PetQuality;

	public ObservableCollection<OOPetBasicAttr> BasicAttrs = new ObservableCollection<OOPetBasicAttr>();

	public ObservableCollection<OOPetBasicAttr> LevelBasicAttrs = new ObservableCollection<OOPetBasicAttr>();

	public ObservableCollection<OOPetEXPUnit> PetEXPs = new ObservableCollection<OOPetEXPUnit>();

	private string _EXPNum;

	private float _EXPNumAmount;

	private bool _BtnResetLevelOn;

	private bool _BtnLevelOn = true;

	private string _SkillPointInfo;

	private bool _SPBtnBuyEnable;

	private string _PetAttackNow;

	private string _FragNum;

	private float _FragNumAmount;

	private string _FragmentBtnName;

	private bool _IsFragmentFull;

	private bool _UpgradeMax;

	private SpriteRenderer _UpgradePetNowFrame;

	private SpriteRenderer _UpgradePetNowFramePet;

	private SpriteRenderer _UpgradePetNowIcon;

	private SpriteRenderer _UpgradePetNowQuality;

	private string _UpgradePetNowFighting;

	private SpriteRenderer _UpgradePetUpFrame;

	private SpriteRenderer _UpgradePetUpFramePet;

	private SpriteRenderer _UpgradePetUpIcon;

	private SpriteRenderer _UpgradePetUpQuality;

	private string _UpgradePetUpFighting;

	private bool _ShowUpNaturalEvo;

	private SpriteRenderer _UpNaturalEvoIcon;

	private string _UpNaturalEvoName;

	private string _UpNaturalEvoDesc;

	private bool _BtnUpgradeEnable;

	private string _MorphBtnText;

	private SpriteRenderer _SupportSkillIcon;

	private int _SupportSkillHSV;

	private bool _BtnFollowShow;

	private SpriteRenderer _BtnFollowBg;

	public int CurrentEXPItemId;

	private List<float> mPetEvaluate = new List<float>();

	public bool lockRefreshEXPUI;

	private float lastEXPNumAmount;

	private float EXPdelta = 0.005f;

	private List<Action> m_expActions = new List<Action>();

	private uint m_expTimerId;

	public static long PetUID
	{
		get
		{
			return PetBasicUIViewModel._PetUID;
		}
		set
		{
			PetBasicUIViewModel._PetUID = value;
		}
	}

	public static int PetID
	{
		get
		{
			return PetBasicUIViewModel._PetID;
		}
		set
		{
			PetBasicUIViewModel._PetID = value;
		}
	}

	public bool InFuse
	{
		get
		{
			return this._InFuse;
		}
		set
		{
			this._InFuse = value;
			if (!this._InFuse)
			{
				this.MorphBtnText = GameDataUtils.GetChineseContent(500005, false);
			}
			else
			{
				this.MorphBtnText = GameDataUtils.GetChineseContent(500006, false);
			}
		}
	}

	public bool SubPanelPets
	{
		get
		{
			return this._SubPanelPets;
		}
		set
		{
			this._SubPanelPets = value;
			base.NotifyProperty("SubPanelPets", value);
			if (value)
			{
				UIManagerControl.Instance.OpenUI("PetChooseUI", PetBasicUIView.Instance.Node2PetChoose, false, UIType.NonPush);
				PetBasicUIView.Instance.ShowPetBackground(false);
				PetBasicUIView.Instance.SetBack(true);
			}
		}
	}

	public bool SubPanelFormation
	{
		get
		{
			return this._SubPanelFormation;
		}
		set
		{
			this._SubPanelFormation = value;
			base.NotifyProperty("SubPanelFormation", value);
			if (value)
			{
				Transform parent = base.get_transform().FindChild("SubPanelFormation");
				UIManagerControl.Instance.OpenUI_Async("PetFormationUI", delegate(UIBase uibase)
				{
					PetFormationUI petFormationUI = uibase as PetFormationUI;
					petFormationUI.RefreshUI(PetManager.Instance.CurrentFormationID);
				}, parent);
			}
		}
	}

	public bool SubFormationBadge
	{
		get
		{
			return this._SubFormationBadge;
		}
		set
		{
			this._SubFormationBadge = value;
			base.NotifyProperty("SubFormationBadge", value);
		}
	}

	public bool SubPanelCombination
	{
		get
		{
			return this._SubPanelCombination;
		}
		set
		{
			this._SubPanelCombination = value;
			base.NotifyProperty("SubPanelCombination", value);
			if (value)
			{
				Transform parent = base.get_transform().FindChild("SubPanelCombination");
				UIManagerControl.Instance.OpenUI("PetFetterUI", parent, false, UIType.NonPush);
				PetBasicUIView.Instance.SetBack(true);
			}
		}
	}

	public bool SubPanelPetInfo
	{
		get
		{
			return this._SubPanelPetInfo;
		}
		set
		{
			this._SubPanelPetInfo = value;
			base.NotifyProperty("SubPanelPetInfo", value);
			if (value)
			{
				this.SubPanelPetInfoRoot = true;
				if (PetChooseUIView.Instance != null)
				{
					PetChooseUIView.Instance.Show(false);
				}
				this.FnBtnLevel = true;
				this.FnBtnUpgrade = false;
				this.FnBtnSkillEvo = false;
				PetBasicUIView.Instance.SetBack(false);
			}
		}
	}

	public bool SubPanelPetInfoRoot
	{
		get
		{
			return this._SubPanelPetInfoRoot;
		}
		set
		{
			this._SubPanelPetInfoRoot = value;
			base.NotifyProperty("SubPanelPetInfoRoot", value);
		}
	}

	public bool ShowArrow
	{
		get
		{
			return this._ShowArrow;
		}
		set
		{
			this._ShowArrow = value;
			base.NotifyProperty("ShowArrow", value);
		}
	}

	public bool FnBtnLevel
	{
		get
		{
			return this._FnBtnLevel;
		}
		set
		{
			this._FnBtnLevel = value;
			base.NotifyProperty("FnBtnLevel", value);
			if (value)
			{
				this.RefreshEXPUI(false, 0);
				this.RemoveEXPBarAnimation();
			}
		}
	}

	public bool FnBtnLevelBadge
	{
		get
		{
			return this._FnBtnLevelBadge;
		}
		set
		{
			this._FnBtnLevelBadge = value;
			base.NotifyProperty("FnBtnLevelBadge", value);
		}
	}

	public bool FnBtnUpgrade
	{
		get
		{
			return this._FnBtnUpgrade;
		}
		set
		{
			this._FnBtnUpgrade = value;
			base.NotifyProperty("FnBtnUpgrade", value);
			if (value)
			{
				this.RefreshUpgradeUI();
			}
		}
	}

	public bool FnBtnUpgradeBadge
	{
		get
		{
			return this._FnBtnUpgradeBadge;
		}
		set
		{
			this._FnBtnUpgradeBadge = value;
			base.NotifyProperty("FnBtnUpgradeBadge", value);
		}
	}

	public bool FnBtnSkillEvo
	{
		get
		{
			return this._FnBtnSkillEvo;
		}
		set
		{
			this._FnBtnSkillEvo = value;
			base.NotifyProperty("FnBtnSkillEvo", value);
			if (value)
			{
				this.SubPanelSkillEvo = true;
				this.SubPanelNaturalEvo = false;
			}
		}
	}

	public bool FnBtnSkillEvoBadge
	{
		get
		{
			return this._FnBtnSkillEvoBadge;
		}
		set
		{
			this._FnBtnSkillEvoBadge = value;
			base.NotifyProperty("FnBtnSkillEvoBadge", value);
		}
	}

	public bool SubPanelNaturalEvo
	{
		get
		{
			return this._SubPanelNaturalEvo;
		}
		set
		{
			this._SubPanelNaturalEvo = value;
			base.NotifyProperty("SubPanelNaturalEvo", value);
			if (value)
			{
				UIManagerControl.Instance.OpenUI("PetEvoNaturalUI", PetBasicUIView.Instance.FindTransform("SubPanelNaturalEvo"), false, UIType.NonPush);
			}
		}
	}

	public bool SubPanelSkillEvo
	{
		get
		{
			return this._SubPanelSkillEvo;
		}
		set
		{
			this._SubPanelSkillEvo = value;
			base.NotifyProperty("SubPanelSkillEvo", value);
			if (value)
			{
				UIManagerControl.Instance.OpenUI("PetEvoSkillUI", PetBasicUIView.Instance.FindTransform("SubPanelSkillEvo"), false, UIType.NonPush);
			}
		}
	}

	public SpriteRenderer PetTypeIcon
	{
		get
		{
			return this._PetTypeIcon;
		}
		set
		{
			this._PetTypeIcon = value;
			base.NotifyProperty("PetTypeIcon", value);
		}
	}

	public SpriteRenderer PetTypeZi
	{
		get
		{
			return this._PetTypeZi;
		}
		set
		{
			this._PetTypeZi = value;
			base.NotifyProperty("PetTypeZi", value);
		}
	}

	public string PetBattlePower
	{
		get
		{
			return this._PetBattlePower;
		}
		set
		{
			this._PetBattlePower = value;
			base.NotifyProperty("PetBattlePower", value);
		}
	}

	public string PetName
	{
		get
		{
			return this._PetName;
		}
		set
		{
			this._PetName = value;
			base.NotifyProperty("PetName", value);
		}
	}

	public string PetLevel
	{
		get
		{
			return this._PetLevel;
		}
		set
		{
			this._PetLevel = value;
			base.NotifyProperty("PetLevel", value);
		}
	}

	public SpriteRenderer PetQuality
	{
		get
		{
			return this._PetQuality;
		}
		set
		{
			this._PetQuality = value;
			base.NotifyProperty("PetQuality", value);
		}
	}

	public string EXPNum
	{
		get
		{
			return this._EXPNum;
		}
		set
		{
			this._EXPNum = value;
			base.NotifyProperty("EXPNum", value);
		}
	}

	public float EXPNumAmount
	{
		get
		{
			return this._EXPNumAmount;
		}
		set
		{
			this._EXPNumAmount = value;
			base.NotifyProperty("EXPNumAmount", value);
		}
	}

	public bool BtnResetLevelOn
	{
		get
		{
			return this._BtnResetLevelOn;
		}
		set
		{
			this._BtnResetLevelOn = value;
			base.NotifyProperty("BtnResetLevelOn", value);
		}
	}

	public bool BtnLevelOn
	{
		get
		{
			return this._BtnLevelOn;
		}
		set
		{
			this._BtnLevelOn = value;
			base.NotifyProperty("BtnLevelOn", value);
		}
	}

	public string SkillPointInfo
	{
		get
		{
			return this._SkillPointInfo;
		}
		set
		{
			this._SkillPointInfo = value;
			base.NotifyProperty("SkillPointInfo", value);
		}
	}

	public bool SPBtnBuyEnable
	{
		get
		{
			return this._SPBtnBuyEnable;
		}
		set
		{
			this._SPBtnBuyEnable = value;
			base.NotifyProperty("SPBtnBuyEnable", value);
		}
	}

	public string PetAttackNow
	{
		get
		{
			return this._PetAttackNow;
		}
		set
		{
			this._PetAttackNow = value;
			base.NotifyProperty("PetAttackNow", value);
		}
	}

	public string FragNum
	{
		get
		{
			return this._FragNum;
		}
		set
		{
			this._FragNum = value;
			base.NotifyProperty("FragNum", value);
		}
	}

	public float FragNumAmount
	{
		get
		{
			return this._FragNumAmount;
		}
		set
		{
			this._FragNumAmount = value;
			base.NotifyProperty("FragNumAmount", value);
		}
	}

	public string FragmentBtnName
	{
		get
		{
			return this._FragmentBtnName;
		}
		set
		{
			this._FragmentBtnName = value;
			base.NotifyProperty("FragmentBtnName", value);
		}
	}

	public bool IsFragmentFull
	{
		get
		{
			return this._IsFragmentFull;
		}
		set
		{
			this._IsFragmentFull = value;
			base.NotifyProperty("IsFragmentFull", value);
		}
	}

	public bool UpgradeMax
	{
		get
		{
			return this._UpgradeMax;
		}
		set
		{
			this._UpgradeMax = value;
			base.NotifyProperty("UpgradeMax", value);
			PetBasicUIView.Instance.RefreshFXOfUpgradeMax(value);
		}
	}

	public SpriteRenderer UpgradePetNowFrame
	{
		get
		{
			return this._UpgradePetNowFrame;
		}
		set
		{
			this._UpgradePetNowFrame = value;
			base.NotifyProperty("UpgradePetNowFrame", value);
		}
	}

	public SpriteRenderer UpgradePetNowFramePet
	{
		get
		{
			return this._UpgradePetNowFramePet;
		}
		set
		{
			this._UpgradePetNowFramePet = value;
			base.NotifyProperty("UpgradePetNowFramePet", value);
		}
	}

	public SpriteRenderer UpgradePetNowIcon
	{
		get
		{
			return this._UpgradePetNowIcon;
		}
		set
		{
			this._UpgradePetNowIcon = value;
			base.NotifyProperty("UpgradePetNowIcon", value);
		}
	}

	public SpriteRenderer UpgradePetNowQuality
	{
		get
		{
			return this._UpgradePetNowQuality;
		}
		set
		{
			this._UpgradePetNowQuality = value;
			base.NotifyProperty("UpgradePetNowQuality", value);
		}
	}

	public string UpgradePetNowFighting
	{
		get
		{
			return this._UpgradePetNowFighting;
		}
		set
		{
			this._UpgradePetNowFighting = value;
			base.NotifyProperty("UpgradePetNowFighting", value);
		}
	}

	public SpriteRenderer UpgradePetUpFrame
	{
		get
		{
			return this._UpgradePetUpFrame;
		}
		set
		{
			this._UpgradePetUpFrame = value;
			base.NotifyProperty("UpgradePetUpFrame", value);
		}
	}

	public SpriteRenderer UpgradePetUpFramePet
	{
		get
		{
			return this._UpgradePetUpFramePet;
		}
		set
		{
			this._UpgradePetUpFramePet = value;
			base.NotifyProperty("UpgradePetUpFramePet", value);
		}
	}

	public SpriteRenderer UpgradePetUpIcon
	{
		get
		{
			return this._UpgradePetUpIcon;
		}
		set
		{
			this._UpgradePetUpIcon = value;
			base.NotifyProperty("UpgradePetUpIcon", value);
		}
	}

	public SpriteRenderer UpgradePetUpQuality
	{
		get
		{
			return this._UpgradePetUpQuality;
		}
		set
		{
			this._UpgradePetUpQuality = value;
			base.NotifyProperty("UpgradePetUpQuality", value);
		}
	}

	public string UpgradePetUpFighting
	{
		get
		{
			return this._UpgradePetUpFighting;
		}
		set
		{
			this._UpgradePetUpFighting = value;
			base.NotifyProperty("UpgradePetUpFighting", value);
		}
	}

	public bool ShowUpNaturalEvo
	{
		get
		{
			return this._ShowUpNaturalEvo;
		}
		set
		{
			this._ShowUpNaturalEvo = value;
			base.NotifyProperty("ShowUpNaturalEvo", value);
		}
	}

	public SpriteRenderer UpNaturalEvoIcon
	{
		get
		{
			return this._UpNaturalEvoIcon;
		}
		set
		{
			this._UpNaturalEvoIcon = value;
			base.NotifyProperty("UpNaturalEvoIcon", value);
		}
	}

	public string UpNaturalEvoName
	{
		get
		{
			return this._UpNaturalEvoName;
		}
		set
		{
			this._UpNaturalEvoName = value;
			base.NotifyProperty("UpNaturalEvoName", value);
		}
	}

	public string UpNaturalEvoDesc
	{
		get
		{
			return this._UpNaturalEvoDesc;
		}
		set
		{
			this._UpNaturalEvoDesc = value;
			base.NotifyProperty("UpNaturalEvoDesc", value);
		}
	}

	public bool BtnUpgradeEnable
	{
		get
		{
			return this._BtnUpgradeEnable;
		}
		set
		{
			this._BtnUpgradeEnable = value;
			base.NotifyProperty("BtnUpgradeEnable", value);
		}
	}

	public string MorphBtnText
	{
		get
		{
			return this._MorphBtnText;
		}
		set
		{
			this._MorphBtnText = value;
			base.NotifyProperty("MorphBtnText", value);
		}
	}

	public SpriteRenderer SupportSkillIcon
	{
		get
		{
			return this._SupportSkillIcon;
		}
		set
		{
			this._SupportSkillIcon = value;
			base.NotifyProperty("SupportSkillIcon", value);
		}
	}

	public int SupportSkillHSV
	{
		get
		{
			return this._SupportSkillHSV;
		}
		set
		{
			this._SupportSkillHSV = value;
			base.NotifyProperty("SupportSkillHSV", value);
		}
	}

	public bool BtnFollowShow
	{
		get
		{
			return this._BtnFollowShow;
		}
		set
		{
			this._BtnFollowShow = value;
			base.NotifyProperty("BtnFollowShow", value);
		}
	}

	public SpriteRenderer BtnFollowBg
	{
		get
		{
			return this._BtnFollowBg;
		}
		set
		{
			this._BtnFollowBg = value;
			base.NotifyProperty("BtnFollowBg", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PetBasicUIViewModel.Instance = this;
		this.InFuse = false;
	}

	private void OnEnable()
	{
		this.ResetAll();
		this.SubPanelPets = true;
		this.SkillPointInfo = PetManager.Instance.SkillPointInfo;
		PetManager.Instance.SetSkillPointBtnBuy();
		this.ShowArrow = PetManager.IsLinkPetMoreOne();
	}

	private void OnDisable()
	{
		this.RemoveEXPBarAnimation();
		PetManager.Instance.IsFormationFromInstance = false;
		PetManager.Instance.IsFromLink = false;
	}

	private void ResetAll()
	{
		this.SubPanelPets = true;
		this.SubPanelFormation = false;
		this.SubPanelCombination = false;
		this.SubPanelPetInfo = false;
	}

	public void SetEXPItemSelected(int itemId, bool isTip)
	{
		this.CurrentEXPItemId = itemId;
		if (this.CurrentEXPItemId <= 0 || BackpackManager.Instance.OnGetGoodCount(this.CurrentEXPItemId) == 0L)
		{
			for (int i = 0; i < PetManager.Instance.ExpItemIds.get_Count(); i++)
			{
				int num = PetManager.Instance.ExpItemIds.get_Item(i);
				if (BackpackManager.Instance.OnGetGoodCount(num) > 0L)
				{
					this.CurrentEXPItemId = num;
					break;
				}
			}
		}
		for (int j = 0; j < this.PetEXPs.Count; j++)
		{
			if (this.PetEXPs[j].ItemId == this.CurrentEXPItemId)
			{
				this.PetEXPs[j].Checked = true;
			}
			else
			{
				this.PetEXPs[j].Checked = false;
			}
		}
	}

	public void OnBtnLevel1()
	{
		PetManager.Instance.LevelUp(this.CurrentEXPItemId, 1);
	}

	public void OnBtnLevel10()
	{
		PetManager.Instance.LevelUp(this.CurrentEXPItemId, 10);
	}

	public void OnBtnUpgradeObtainUp()
	{
		LinkNavigationManager.ItemNotEnoughToLink(this.m_fragmentId, true, null, true);
	}

	public void OnBtnUpgradeUp()
	{
		if (!this.IsFragmentFull)
		{
			this.OnBtnUpgradeObtainUp();
			return;
		}
		PetInfo petInfo = PetManager.Instance.GetPetInfo(PetBasicUIViewModel.PetUID);
		if (petInfo == null)
		{
			Debug.LogError("未找到宠物实例,uid = " + PetBasicUIViewModel.PetUID);
			return;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			Debug.LogError("GameData.Pet未找到宠物,id = " + petInfo.petId);
			return;
		}
		if (pet.needFragment.get_Count() == 0)
		{
			Debug.LogError("数据有问题, 需要的碎片数量列表=0, id = " + petInfo.petId);
			return;
		}
		int num = petInfo.star + 1;
		if (num > pet.needFragment.get_Count())
		{
			UIManagerControl.Instance.ShowToastText("已达最高品质,无法继续提升品质");
			return;
		}
		int num2 = (pet.upstarNeedGold.get_Count() <= num - 1) ? 0 : pet.upstarNeedGold.get_Item(num - 1);
		int fragmentId = pet.fragmentId;
		int num3 = pet.needFragment.get_Item(num - 1);
		string text = num2.ToString();
		if (EntityWorld.Instance.EntSelf.Gold < (long)num2)
		{
			text = TextColorMgr.GetColorByID(text, 1000007);
		}
		string text2 = num3.ToString();
		if (BackpackManager.Instance.OnGetGoodCount(fragmentId) < (long)num3)
		{
			text2 = TextColorMgr.GetColorByID(text2, 1000007);
		}
		if (num2 <= 0)
		{
			PetManager.Instance.SendPetUpStar(PetBasicUIViewModel.PetUID);
		}
		else
		{
			string content = string.Format(GameDataUtils.GetChineseContent(500016, false), text, GameDataUtils.GetItemName(fragmentId, true, 0L), text2);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(500015, false), content, delegate
			{
				Debuger.Info("cancel", new object[0]);
			}, delegate
			{
				PetManager.Instance.SendPetUpStar(PetBasicUIViewModel.PetUID);
			}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500015, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	public void OnBtnPreview()
	{
		UIManagerControl.Instance.OpenUI("UpgradePreviewUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public void OnSPBtnBuyUp()
	{
		int num = DataReader<CChongWuSheZhi>.Get("price").num;
		int num2 = DataReader<CChongWuSheZhi>.Get("buyPoint").num;
		string color = TextColorMgr.GetColor(num + "钻石", "F87D04", string.Empty);
		string content = string.Concat(new string[]
		{
			string.Concat(new object[]
			{
				"是否花费",
				color,
				"购买",
				num2,
				"点技能点"
			})
		});
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("技能点购买", content, delegate
		{
		}, delegate
		{
			PetManager.Instance.SendPurchaseSkillPoint();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	public void OnMorphBtnUp()
	{
		this.InFuse = !this.InFuse;
		this.ShowSelectedPetInfo();
	}

	public void OnSupportSkillBtnDown(GameObject go)
	{
		if (go != null)
		{
			int supportSkill = PetManager.Instance.GetSupportSkill(PetBasicUIViewModel._PetID);
			if (supportSkill > 0)
			{
				Skill skill = DataReader<Skill>.Get(supportSkill);
				if (skill == null)
				{
					return;
				}
				TipsUI.Instance.ShowTip(GameDataUtils.GetChineseContent(skill.name, false), this.GetSkillDesc(GameDataUtils.GetChineseContent(skill.describeId, false)), new Vector3(go.get_transform().get_position().x, go.get_transform().get_position().y + 0.3f, go.get_transform().get_position().z));
			}
		}
	}

	public void OnSupportSkillBtnUp()
	{
		EventDispatcher.Broadcast("UIManagerControl.HideTipsUI");
	}

	public void OnPPIArrowL()
	{
		if (PetBasicUIViewModel.PetUID <= 0L)
		{
			return;
		}
		long linkPet = PetManager.GetLinkPet(PetBasicUIViewModel.PetUID, false);
		PetInfo petInfo = PetManager.Instance.GetPetInfo(linkPet);
		if (petInfo != null)
		{
			this.ShowSelectedPetInfo(linkPet, petInfo.petId);
		}
	}

	public void OnPPIArrowR()
	{
		if (PetBasicUIViewModel.PetUID <= 0L)
		{
			return;
		}
		long linkPet = PetManager.GetLinkPet(PetBasicUIViewModel.PetUID, true);
		PetInfo petInfo = PetManager.Instance.GetPetInfo(linkPet);
		if (petInfo != null)
		{
			this.ShowSelectedPetInfo(linkPet, petInfo.petId);
		}
	}

	public void OnFollowUp()
	{
		PetManager.Instance.SendChangeMainCityFollowPet(PetBasicUIViewModel.PetUID);
	}

	public void OnBtnResetLevel()
	{
		PetManager.Instance.SendPresResetPetLv(PetBasicUIViewModel.PetUID);
	}

	public void ShowSelectedPetInfo(long petUid, int petId)
	{
		PetBasicUIViewModel.PetUID = petUid;
		PetBasicUIViewModel.PetID = petId;
		this.ShowSelectedPetInfo();
		Pet pet = DataReader<Pet>.Get(petId);
		PetBasicUIView.Instance.SetBackground(pet.petType);
	}

	public void ShowSelectedPetInfo()
	{
		this.RefreshShowUI();
		this.RefreshNaturalEvoUI();
		this.RefreshEXPUI(false, 0);
		this.RefreshUpgradeUI();
		this.RefreshSkillUI();
	}

	private void RefreshAttrUI(Pet dataPet, PetInfo petInfo)
	{
		this.SetBasicAttrs(dataPet, petInfo);
	}

	private void SetBasicAttrs(Pet dataPet, PetInfo petInfo)
	{
		this.BasicAttrs.Clear();
		string attr = string.Empty;
		string attr2 = string.Empty;
		bool flag = true;
		if (petInfo != null)
		{
			flag = this.UpgradeMax;
			if (flag)
			{
				attr2 = GameDataUtils.GetChineseContent(505031, false);
			}
		}
		else
		{
			attr2 = "激活可见";
		}
		string basicAttrName = this.GetBasicAttrName(GameData.AttrType.Atk);
		attr = TextColorMgr.GetColor(PetManager.Instance.GetATK(dataPet, petInfo, false, false).ToString(), "ff7d4b", string.Empty);
		if (!flag)
		{
			attr2 = PetManager.Instance.GetATK(dataPet, petInfo, false, true).ToString();
		}
		this.BasicAttrs.Add(this.GetBasic(basicAttrName, attr, attr2));
		basicAttrName = this.GetBasicAttrName(GameData.AttrType.Defence);
		attr = TextColorMgr.GetColor(PetManager.Instance.GetDefence(dataPet, petInfo, false, false).ToString(), "ff7d4b", string.Empty);
		if (!flag)
		{
			attr2 = PetManager.Instance.GetDefence(dataPet, petInfo, false, true).ToString();
		}
		this.BasicAttrs.Add(this.GetBasic(basicAttrName, attr, attr2));
		basicAttrName = this.GetBasicAttrName(GameData.AttrType.Hp);
		attr = TextColorMgr.GetColor(PetManager.Instance.GetHP(dataPet, petInfo, false, false).ToString(), "ff7d4b", string.Empty);
		if (!flag)
		{
			attr2 = PetManager.Instance.GetHP(dataPet, petInfo, false, true).ToString();
		}
		this.BasicAttrs.Add(this.GetBasic(basicAttrName, attr, attr2));
	}

	private void SetPolygen()
	{
		this.mPetEvaluate.Clear();
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (pet == null)
		{
			return;
		}
		if (pet.petEvaluate.get_Count() < 6)
		{
			return;
		}
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(2) / 5f);
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(1) / 5f);
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(0) / 5f);
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(5) / 5f);
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(4) / 5f);
		this.mPetEvaluate.Add(pet.petEvaluate.get_Item(3) / 5f);
		PetBasicUIView.Instance.m_PolygenSytem.SetValues(this.mPetEvaluate);
		PetBasicUIView.Instance.m_PolygenSytem.SetType(GameDataUtils.GetChineseContent(pet.function - 1 + 500100, false));
	}

	private void RefreshShowUI()
	{
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (pet == null)
		{
			return;
		}
		this.PetTypeIcon = PetManagerBase.GetPetType(pet);
		this.PetTypeZi = PetManagerBase.GetPetZi(pet);
		if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID))
		{
			PetInfo petInfo = PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID);
			this.SetPetNameLevel(pet, petInfo.lv.ToString());
			this.SetPetQualityByStar(petInfo.star);
			this.ShowPetModel(pet);
			this.SetFighting(petInfo.publicBaseInfo.simpleInfo.Fighting, false, 0L);
			this.SetFollow(petInfo.id);
			this.BtnResetLevelOn = (petInfo.lv >= this.GetResettingLv());
		}
		else
		{
			this.SetPetNameLevel(pet, 1.ToString());
			this.SetPetQualityByStar(pet.initStar);
			this.ShowPetModel(pet);
			this.SetFighting(0L, false, 0L);
			this.SetFollow(0L);
			this.BtnResetLevelOn = false;
		}
	}

	public void SetFighting(long fightingNow, bool anim = false, long fightingOld = 0L)
	{
		this.PetBattlePower = this.GetFighting(fightingNow.ToString());
		if (PetBasicUIView.Instance != null)
		{
			PetBasicUIView.Instance.ShowFighting(fightingNow, anim, fightingOld);
		}
	}

	public string GetFighting(string fighting)
	{
		return this.GetBasicAttr2Important(AttrUtility.GetAttrName(GameData.AttrType.Fighting), fighting);
	}

	private void SetPetNameLevel(Pet dataPet, string level)
	{
		if (dataPet != null)
		{
			this.PetName = PetManager.GetPetName(dataPet, true) + " Lv" + level;
			this.PetLevel = " Lv" + level;
		}
	}

	private void SetPetQualityByStar(int star)
	{
		this.PetQuality = PetManager.GetPetQualityIcon(star);
	}

	public void ShowPetModel()
	{
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (pet != null)
		{
			this.ShowPetModel(pet);
		}
	}

	private void ShowPetModel(Pet dataPet)
	{
		if (dataPet != null)
		{
			int num;
			if (!this.InFuse)
			{
				num = PetManager.Instance.GetSelfPetModel(dataPet);
			}
			else
			{
				num = dataPet.fuseModle;
			}
			if (num > 0)
			{
				ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(PetBasicUIViewModel.PetModelUID);
				if (uIModel != null && uIModel.get_gameObject() != null && uIModel.get_gameObject().get_activeSelf() && uIModel.resGUID == num)
				{
					return;
				}
				ModelDisplayManager.Instance.ShowModel(num, true, ModelDisplayManager.OFFSET_TO_PETUI, delegate(int uid)
				{
					PetBasicUIViewModel.PetModelUID = uid;
				});
			}
		}
	}

	public void SetFollow(long id)
	{
		PetInfo petInfo = PetManager.Instance.GetPetInfo(id);
		if (petInfo != null)
		{
			this.SetFollow(true, PetManager.Instance.IsFollow(petInfo.id));
		}
		else
		{
			this.SetFollow(false, false);
		}
	}

	private void SetFollow(bool show, bool follow)
	{
		this.BtnFollowShow = show;
		if (!follow)
		{
			this.BtnFollowBg = ResourceManager.GetIconSprite("pet_gensui");
		}
		else
		{
			this.BtnFollowBg = ResourceManager.GetIconSprite("pet_gensui2");
		}
	}

	private void RefreshNaturalEvoUI()
	{
		if (!this.FnBtnSkillEvo)
		{
			return;
		}
		if (!this.SubPanelNaturalEvo)
		{
			return;
		}
		PetManager.Instance.UpdatePetEvoUI();
	}

	public void RefreshEXPUI(bool anim, int uplevel = 0)
	{
		if (this.lockRefreshEXPUI)
		{
			return;
		}
		this.CheckLevelBadge();
		this.CheckPetEvoBadge();
		if (!this.FnBtnLevel)
		{
			return;
		}
		this.SetAttackNow();
		if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID))
		{
			PetInfo petInfo = PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID);
			this.BtnResetLevelOn = (petInfo.lv >= this.GetResettingLv());
			this.SetPetEXPs(!PetManager.Instance.IsPetMaxLevel(petInfo.lv));
			GenericAttribute genericAttribute = DataReader<GenericAttribute>.Get(petInfo.lv);
			if (genericAttribute == null)
			{
				Debug.LogError("GameData.GenericAttribute no exist, level = " + petInfo.lv);
				return;
			}
			uint petExp = (uint)genericAttribute.petExp;
			if (!PetManager.Instance.IsPetMaxLevel(petInfo.lv))
			{
				this.EXPNum = petInfo.exp + "/" + petExp;
				if (petExp > 0u)
				{
					this.SetEXPNumAmount((float)petInfo.exp / petExp, anim, uplevel);
				}
				else
				{
					this.SetEXPNumAmount(0f, anim, uplevel);
				}
				this.BtnLevelOn = true;
			}
			else
			{
				this.EXPNum = "已提升至满级";
				this.SetEXPNumAmount(1f, anim, uplevel);
				this.BtnLevelOn = false;
			}
		}
		else
		{
			this.SetPetEXPs(false);
			this.EXPNum = "未获得该宠物,无法升级";
			this.SetEXPNumAmount(1f, false, 0);
			this.BtnLevelOn = false;
		}
	}

	private void CheckLevelBadge()
	{
		if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID))
		{
			PetInfo petInfo = PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID);
			this.FnBtnLevelBadge = PetManager.Instance.CheckCanLevelUp(petInfo);
		}
		else
		{
			this.FnBtnLevelBadge = false;
		}
	}

	private int GetResettingLv()
	{
		return DataReader<CChongWuSheZhi>.Get("resettingLv").num;
	}

	private void SetEXPNumAmount(float amount, bool anim, int uplevel)
	{
		if (anim)
		{
			this.m_expActions.Add(delegate
			{
				this.lastEXPNumAmount = amount;
				this.SetEXPDelta(uplevel);
				this.JustEXPBarAnimation(uplevel);
			});
			this.CheckEXPBarAnimationAction();
		}
		else
		{
			this.EXPNumAmount = amount;
		}
	}

	private void SetEXPDelta(int uplevel)
	{
		int count = this.m_expActions.get_Count();
		int num = Mathf.Max(2, 10 - count);
		float num2 = (float)(uplevel * 1) + (this.lastEXPNumAmount - this.EXPNumAmount);
		this.EXPdelta = num2 / (float)num;
	}

	public void RemoveMoreEXPBarAnimationAction()
	{
		if (this.m_expActions.get_Count() > 1)
		{
			this.m_expActions.RemoveRange(0, this.m_expActions.get_Count() - 1);
		}
	}

	private void CheckEXPBarAnimationAction()
	{
		if (this.m_expTimerId > 0u)
		{
			return;
		}
		if (this.m_expActions.get_Count() == 0)
		{
			return;
		}
		this.m_expActions.get_Item(0).Invoke();
		this.m_expActions.RemoveAt(0);
	}

	private void RemoveEXPBarAnimation()
	{
		TimerHeap.DelTimer(this.m_expTimerId);
		this.m_expTimerId = 0u;
		this.m_expActions.Clear();
	}

	private void JustEXPBarAnimation(int uplevel)
	{
		TimerHeap.DelTimer(this.m_expTimerId);
		this.m_expTimerId = TimerHeap.AddTimer(0u, 30, delegate
		{
			if (PetBasicUIViewModel.Instance == null)
			{
				this.RemoveEXPBarAnimation();
				return;
			}
			if (uplevel > 0)
			{
				this.EXPNumAmount += this.EXPdelta;
				if (this.EXPNumAmount >= 1f)
				{
					this.EXPNumAmount = 0f;
					this.UpgradeLvFX();
					this.JustEXPBarAnimation(uplevel - 1);
				}
			}
			else if (this.EXPNumAmount < this.lastEXPNumAmount && this.EXPNumAmount + this.EXPdelta <= this.lastEXPNumAmount)
			{
				this.EXPNumAmount += this.EXPdelta;
			}
			else
			{
				this.EXPNumAmount = this.lastEXPNumAmount;
				TimerHeap.DelTimer(this.m_expTimerId);
				this.m_expTimerId = 0u;
				this.CheckEXPBarAnimationAction();
			}
		});
	}

	private void UpgradeLvFX()
	{
		PetManager.Instance.AnimSuccessOfLevelUp();
		FXSpineManager.Instance.PlaySpine(2206, PetBasicUIView.Instance.FindTransform("EXPFXNode"), "PetBasicUI", 2001, null, "UI", 43f, 0f, 0.95f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SetPetEXPs(bool enableUse)
	{
		this.PetEXPs.Clear();
		for (int i = 0; i < PetManager.Instance.ExpItemIds.get_Count(); i++)
		{
			Items items = DataReader<Items>.Get(PetManager.Instance.ExpItemIds.get_Item(i));
			if (items != null)
			{
				if (this.CurrentEXPItemId <= 0 && i == 0)
				{
					this.CurrentEXPItemId = items.id;
				}
				OOPetEXPUnit oOPetEXPUnit = new OOPetEXPUnit();
				oOPetEXPUnit.ItemId = items.id;
				oOPetEXPUnit.ItemFrame = GameDataUtils.GetItemFrame(items);
				oOPetEXPUnit.ItemIcon = GameDataUtils.GetItemIcon(items.id);
				oOPetEXPUnit.ItemNum = string.Empty + BackpackManager.Instance.OnGetGoodCount(items.id);
				oOPetEXPUnit.BtnUseEnable = enableUse;
				this.PetEXPs.Add(oOPetEXPUnit);
			}
		}
		this.SetEXPItemSelected(this.CurrentEXPItemId, false);
	}

	public void RefreshUpgradeUI()
	{
		this.CheckUpgradeBadge();
		if (!this.FnBtnUpgrade)
		{
			return;
		}
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (pet == null)
		{
			return;
		}
		if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID))
		{
			PetInfo petInfo = PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID);
			this.SetFragment(pet, petInfo.star);
			this.SetUpgradePets(pet, petInfo.star, petInfo.publicBaseInfo.simpleInfo.Lv);
			this.BtnUpgradeEnable = true;
			this.RefreshAttrUI(pet, petInfo);
		}
		else
		{
			this.SetFragment(pet, 1);
			this.SetUpgradePets(pet, 1, 1);
			this.BtnUpgradeEnable = false;
			this.RefreshAttrUI(pet, null);
		}
	}

	public void SetUpgradePetUpFighting(long fighting, long afterFighting)
	{
		this.UpgradePetNowFighting = "战力：" + TextColorMgr.GetColor(fighting.ToString(), "ff7d4b", string.Empty);
		this.UpgradePetUpFighting = "战力：" + TextColorMgr.GetColor(afterFighting.ToString(), "ff7d4b", string.Empty);
	}

	private void SetUpgradePets(Pet dataPet, int star, int level)
	{
		this.UpgradePetNowFrame = PetManager.GetPetFrame01(star);
		this.UpgradePetNowFramePet = PetManager.GetPetFrame02(star);
		this.UpgradePetNowIcon = PetManager.Instance.GetSelfPetIcon2(dataPet);
		this.UpgradePetNowQuality = PetManager.GetPetQualityIcon(star);
		this.UpgradePetNowFighting = "---";
		this.UpgradePetUpFrame = PetManager.GetPetFrame01(star + 1);
		this.UpgradePetUpFramePet = PetManager.GetPetFrame02(star + 1);
		this.UpgradePetUpIcon = PetManagerBase.GetPlayerPetIcon2(dataPet, star + 1);
		this.UpgradePetUpQuality = PetManager.GetPetQualityIcon(star + 1);
		this.UpgradePetUpFighting = "---";
		if (PetBasicUIViewModel.PetUID > 0L)
		{
			PetManager.Instance.SendPresPetUpStar(PetBasicUIViewModel.PetUID);
		}
		else
		{
			this.UpgradePetNowFighting = "战力: 激活可见";
			this.UpgradePetUpFighting = "战力: 激活可见";
		}
		ChongWuTianFu activeNatural = PetManager.GetActiveNatural(dataPet, star + 1);
		if (activeNatural != null)
		{
			this.ShowUpNaturalEvo = true;
			this.UpNaturalEvoIcon = ResourceManager.GetIconSprite(activeNatural.picture);
			this.UpNaturalEvoName = GameDataUtils.GetChineseContent(activeNatural.name, false) + "（升阶后激活）";
			this.UpNaturalEvoDesc = GameDataUtils.GetChineseContent(activeNatural.describe, false);
		}
		else
		{
			this.ShowUpNaturalEvo = false;
		}
	}

	private void SetFragment(Pet dataPet, int star)
	{
		if (dataPet != null)
		{
			int num = star + 1;
			if (num <= dataPet.needFragment.get_Count())
			{
				this.UpgradeMax = false;
				int fragmentId = dataPet.fragmentId;
				int num2 = dataPet.needFragment.get_Item(num - 1);
				this.m_fragmentId = fragmentId;
				long num3 = BackpackManager.Instance.OnGetGoodCount(fragmentId);
				if (num3 >= (long)num2)
				{
					this.FragNum = num3 + "/" + num2;
					this.FragNumAmount = 1f;
					this.IsFragmentFull = true;
				}
				else
				{
					this.FragNum = num3 + "/" + num2;
					this.FragNumAmount = (float)num3 / (float)num2;
					this.IsFragmentFull = false;
				}
			}
			else
			{
				this.UpgradeMax = true;
				this.FragNum = GameDataUtils.GetChineseContent(500004, false);
				this.FragNumAmount = 1f;
			}
		}
	}

	private void CheckUpgradeBadge()
	{
		if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID))
		{
			PetInfo petInfo = PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID);
			this.FnBtnUpgradeBadge = PetManager.Instance.CheckCanUpgrade(petInfo);
		}
		else
		{
			this.FnBtnUpgradeBadge = false;
		}
	}

	private void RefreshSkillUI()
	{
		if (!this.FnBtnSkillEvo)
		{
			return;
		}
		if (!this.SubPanelSkillEvo)
		{
			return;
		}
		PetManager.Instance.UpdatePetEvoUI();
		this.SetAttackNow();
	}

	private float GetAttackAll(int start, bool nextlevel)
	{
		int num = Mathf.Max(0, start - 1);
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (num < pet.attributeTemplateID.get_Count() && num < pet.attributeTemplateGrowID.get_Count())
		{
			float attack = this.GetAttack(pet.attributeTemplateID.get_Item(num));
			float attack2 = this.GetAttack(pet.attributeTemplateGrowID.get_Item(num));
			int num2 = PetEvoGlobal.GetPetLv(PetBasicUIViewModel.PetID);
			if (nextlevel)
			{
				num2++;
			}
			return attack + attack2 * (float)num2;
		}
		return 0f;
	}

	public void SetAttackNow()
	{
		int petUpgradeLevel = PetManager.Instance.GetPetUpgradeLevel(PetBasicUIViewModel.PetID);
		float attackAll = this.GetAttackAll(petUpgradeLevel, false);
		if (attackAll > 0f)
		{
			this.PetAttackNow = string.Format("宠物当前攻击力: {0}", attackAll);
			this.LevelBasicAttrs.Clear();
			string basicAttrName = this.GetBasicAttrName(GameData.AttrType.Atk);
			string color = TextColorMgr.GetColor(attackAll.ToString(), "ff7d4b", string.Empty);
			string attr = string.Empty;
			if (PetManager.Instance.MaplistPet.ContainsKey(PetBasicUIViewModel.PetUID) && PetManager.Instance.IsPetMaxLevel(PetManager.Instance.MaplistPet.get_Item(PetBasicUIViewModel.PetUID).lv))
			{
				attr = "已达最大等级";
			}
			else
			{
				attr = this.GetAttackAll(petUpgradeLevel, true).ToString();
			}
			this.LevelBasicAttrs.Add(this.GetBasic("宠物攻击", color, attr));
		}
		else
		{
			this.PetAttackNow = string.Format("宠物当前攻击力: {0}", "配置错误");
			this.LevelBasicAttrs.Clear();
		}
	}

	private float GetAttack(int attrId)
	{
		Attrs attrs = DataReader<Attrs>.Get(attrId);
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			if (attrs.attrs.get_Item(i) == 201)
			{
				return (float)attrs.values.get_Item(i);
			}
		}
		return 0f;
	}

	private void SetSupportSkill(int petId, PetInfo petInfo = null)
	{
		int supportSkill = PetManager.Instance.GetSupportSkill(petId);
		if (supportSkill > 0)
		{
			Skill skill = DataReader<Skill>.Get(supportSkill);
			if (skill == null)
			{
				return;
			}
			this.SupportSkillIcon = GameDataUtils.GetIcon(skill.icon);
			if (petInfo == null)
			{
				this.SupportSkillHSV = 6;
			}
			else
			{
				this.SupportSkillHSV = 0;
			}
		}
	}

	public void CheckPetEvoBadge()
	{
		this.FnBtnSkillEvoBadge = PetManager.Instance.CheckCanSkillUp(PetBasicUIViewModel.PetID);
	}

	private OOPetBasicAttr GetBasic(string name, string attr1, string attr2 = "xxxxxx")
	{
		return new OOPetBasicAttr
		{
			Name = name,
			Attr01 = attr1,
			Attr02 = attr2
		};
	}

	public string GetBasicAttrName(GameData.AttrType type)
	{
		return TextColorMgr.GetColor(AttrUtility.GetAttrName(type), "78503c", string.Empty);
	}

	public string GetBasicAttr2Important(string name, string num)
	{
		name = TextColorMgr.GetColorByID(name + ":", 305);
		num = TextColorMgr.GetColor(num, "ffeb4b", string.Empty);
		return name + " " + num;
	}

	public string GetSkillName(string text)
	{
		return TextColorMgr.GetColorByID(text, 301);
	}

	public string GetSkillDesc(string text)
	{
		return TextColorMgr.GetColorByID(text, 1000002);
	}
}
