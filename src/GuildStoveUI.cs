using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuildStoveUI : UIBase
{
	public static GuildStoveUI Instance;

	private GameObject m_goEquipSmeltUI;

	private GameObject m_goEquipBuildUI;

	private Transform m_FXSpine01;

	private Text m_lblCoin01Num;

	private Image m_spCoin01Icon;

	private Text m_lblCoin02Num;

	private Image m_spCoin02Icon;

	private GameObject m_goRootNoEquips;

	private GameObject m_goRootHasEquips;

	private Text m_lblEquipSmeltUITips01;

	private Text m_lblEquipSmeltUITips02;

	private Text m_lblEquipBuildUITips01;

	private Text m_lblEquipBuildUITips02;

	private Text m_lblEquipBuildUITips03;

	private Text m_lblBuildCoin01Num;

	private Image m_spBuildCoin01Icon;

	private Text m_lblBuildCoin02Num;

	private Image m_spBuildCoin02Icon;

	private int m_spine_normal_id;

	private int m_spine_smelt_id;

	private int m_spine_build_id;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		GuildStoveUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_goEquipSmeltUI = base.FindTransform("EquipSmeltUI").get_gameObject();
		this.m_goEquipBuildUI = base.FindTransform("EquipBuildUI").get_gameObject();
		this.m_FXSpine01 = base.FindTransform("FXSpine01");
		this.m_lblCoin01Num = base.FindTransform("Coin01Num").GetComponent<Text>();
		this.m_spCoin01Icon = base.FindTransform("Coin01Icon").GetComponent<Image>();
		this.m_lblCoin02Num = base.FindTransform("Coin02Num").GetComponent<Text>();
		this.m_spCoin02Icon = base.FindTransform("Coin02Icon").GetComponent<Image>();
		this.m_goRootNoEquips = base.FindTransform("RootNoEquips").get_gameObject();
		this.m_goRootHasEquips = base.FindTransform("RootHasEquips").get_gameObject();
		this.m_lblEquipSmeltUITips01 = base.FindTransform("EquipSmeltUITips01").GetComponent<Text>();
		this.m_lblEquipSmeltUITips02 = base.FindTransform("EquipSmeltUITips02").GetComponent<Text>();
		this.m_lblEquipBuildUITips01 = base.FindTransform("EquipBuildUITips01").GetComponent<Text>();
		this.m_lblEquipBuildUITips02 = base.FindTransform("EquipBuildUITips02").GetComponent<Text>();
		this.m_lblEquipBuildUITips03 = base.FindTransform("EquipBuildUITips03").GetComponent<Text>();
		this.m_lblBuildCoin01Num = base.FindTransform("BuildCoin01Num").GetComponent<Text>();
		this.m_spBuildCoin01Icon = base.FindTransform("BuildCoin01Icon").GetComponent<Image>();
		this.m_lblBuildCoin02Num = base.FindTransform("BuildCoin02Num").GetComponent<Text>();
		this.m_spBuildCoin02Icon = base.FindTransform("BuildCoin02Icon").GetComponent<Image>();
		base.FindTransform("BtnClose").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnClose));
		base.FindTransform("BtnSmeltOneKey").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnSmeltOneKey));
		base.FindTransform("BtnSmelt").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnSmelt));
		base.FindTransform("BtnBuild").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnBuild));
		base.FindTransform("BtnSmeltDesc").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnSmeltDesc));
		base.FindTransform("BtnBuildDesc").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnBuildDesc));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.PlaySpineNormal();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.RemoveAllSpine();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			GuildStoveUI.Instance = null;
			GuildStoveUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateGuildInfo, new Callback(this.RefreshGuildContribution));
		EventDispatcher.AddListener(EventNames.OnGuildEquipEssenceNty, new Callback(this.RefreshEqiupEssence));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateGuildInfo, new Callback(this.RefreshGuildContribution));
		EventDispatcher.RemoveListener(EventNames.OnGuildEquipEssenceNty, new Callback(this.RefreshEqiupEssence));
	}

	private void OnBtnClose()
	{
		this.Show(false);
	}

	private void OnBtnSmeltOneKey()
	{
		GuildStoveUIViewModel.Instance.DoSmeltOneKey();
	}

	private void OnBtnSmelt()
	{
		GuildStoveUIViewModel.Instance.DoSmelt();
	}

	private void OnBtnBuild()
	{
		GuildStoveUIViewModel.Instance.DoBuild();
	}

	private void OnBtnSmeltDesc()
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 40053, 40054);
	}

	private void OnBtnBuildDesc()
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 40051, 40052);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("ButtonSubs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.SourceBinding.MemberName = "GuildStoveSubs";
		listBinder.PrefabName = "ButtonToggle2GuildStoveSub";
		listBinder = base.FindTransform("EquipList").get_gameObject().GetComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "BackpackItem";
		listBinder.LoadNumberFrame = 5;
		listBinder.SourceBinding.MemberName = "SmeltEquips";
		listBinder.ITEM_NAME = "_SmeltEquip";
		listBinder = base.FindTransform("EquipPosList").get_gameObject().GetComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "StoveBuildEquipPartCheck";
		listBinder.LoadNumberFrame = 5;
		listBinder.SourceBinding.MemberName = "EqiupParts";
		listBinder.ITEM_NAME = "_EqiupPart";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public void ShowSubUI(int index)
	{
		if (index == 0)
		{
			this.m_goEquipSmeltUI.SetActive(true);
			this.m_goEquipBuildUI.SetActive(false);
		}
		else
		{
			this.m_goEquipSmeltUI.SetActive(false);
			this.m_goEquipBuildUI.SetActive(true);
		}
	}

	public void RefreshGuildContribution()
	{
		this.m_lblCoin01Num.set_text(MoneyType.GetNum(5).ToString());
		ResourceManager.SetSprite(this.m_spCoin01Icon, MoneyType.GetIcon(5));
	}

	public void RefreshEqiupEssence()
	{
		this.m_lblCoin02Num.set_text(MoneyType.GetNum(7).ToString());
		ResourceManager.SetSprite(this.m_spCoin02Icon, MoneyType.GetIcon(7));
	}

	public void SetIsEquipSmeltsEmpty(bool isEmpty)
	{
		this.m_goRootNoEquips.SetActive(isEmpty);
		this.m_goRootHasEquips.SetActive(!isEmpty);
	}

	public void SetEquipSmeltUITips01(int today_fund, int max_fund)
	{
		this.m_lblEquipSmeltUITips01.set_text(GameDataUtils.GetChineseContent(40047, false));
		this.m_lblEquipSmeltUITips02.set_text(string.Format("{0}/{1}", today_fund, max_fund));
	}

	public void SetEquipBuildUITips01(int maxStep)
	{
		this.m_lblEquipBuildUITips01.set_text(string.Format(GameDataUtils.GetChineseContent(40048, false), maxStep));
	}

	public void SetEquipBuildUITips02(int guildMaxStep)
	{
		this.m_lblEquipBuildUITips02.set_text(string.Format(GameDataUtils.GetChineseContent(40049, false), guildMaxStep));
	}

	public void SetEquipBuildUITips03(int levelMaxStep)
	{
		this.m_lblEquipBuildUITips03.set_text(string.Format(GameDataUtils.GetChineseContent(40050, false), levelMaxStep));
	}

	public void SetEquipBuildCost(int cost_contribute, int const_essence)
	{
		this.m_lblBuildCoin01Num.set_text(cost_contribute.ToString());
		ResourceManager.SetSprite(this.m_spBuildCoin01Icon, MoneyType.GetIcon(5));
		this.m_lblBuildCoin02Num.set_text(const_essence.ToString());
		ResourceManager.SetSprite(this.m_spBuildCoin02Icon, MoneyType.GetIcon(7));
	}

	public void PlaySpineNormal()
	{
		this.m_spine_normal_id = FXSpineManager.Instance.ReplaySpine(this.m_spine_normal_id, 4601, this.m_FXSpine01, "GuildStoveUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void PlaySpineSmelt(Action action)
	{
		this.m_spine_smelt_id = FXSpineManager.Instance.ReplaySpine(this.m_spine_smelt_id, 4602, this.m_FXSpine01, "GuildStoveUI", 2001, action, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void PlaySpineBuild(Action action)
	{
		this.m_spine_build_id = FXSpineManager.Instance.ReplaySpine(this.m_spine_build_id, 4603, this.m_FXSpine01, "GuildStoveUI", 2001, action, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveAllSpine()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_spine_normal_id, true);
		FXSpineManager.Instance.DeleteSpine(this.m_spine_smelt_id, true);
		FXSpineManager.Instance.DeleteSpine(this.m_spine_build_id, true);
	}
}
