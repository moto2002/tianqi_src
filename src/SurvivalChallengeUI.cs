using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalChallengeUI : UIBase
{
	private ButtonCustom SCBtn;

	private ButtonCustom RankListBtn;

	private ButtonCustom ChestBtn;

	private Text BossName;

	private Image BossIcon;

	private Text Times;

	private ButtonCustom ButtonBuyTimes;

	private ButtonCustom ButtonDesc;

	private ScrollRectCustom scrollRect;

	private Transform Select;

	private GameObject TipsHasFinish;

	private GameObject TipsNotFinish;

	private RectTransform UpImage;

	private RectTransform DownImage;

	private RectTransform ListItems;

	private GameObject ItemPrefab;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.ButtonBuyTimes.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuyTimes);
		this.scrollRect.movePage = true;
		this.scrollRect.OnPageChanged = delegate(int pageIndex)
		{
			this.UpdateSelectDetail(SurvivalManager.Instance.StageMax - pageIndex);
		};
	}

	private void Update()
	{
		this.DownImage.set_anchoredPosition(new Vector2(this.ListItems.get_anchoredPosition().x, this.ListItems.get_anchoredPosition().y));
		this.UpImage.set_anchoredPosition(new Vector2(this.ListItems.get_anchoredPosition().x, this.ListItems.get_anchoredPosition().y - this.ListItems.get_sizeDelta().y));
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.BossName = base.FindTransform("BossName").GetComponent<Text>();
		this.BossIcon = base.FindTransform("BossIcon").GetComponent<Image>();
		this.Times = base.FindTransform("TimesNum").GetComponent<Text>();
		this.ButtonBuyTimes = base.FindTransform("ButtonBuyTimes").GetComponent<ButtonCustom>();
		this.ButtonDesc = base.FindTransform("ButtonDesc").GetComponent<ButtonCustom>();
		this.Select = base.FindTransform("SelectRoot");
		this.scrollRect = base.FindTransform("DifficultyList").GetComponent<ScrollRectCustom>();
		this.TipsHasFinish = base.FindTransform("TipsHasFinish").get_gameObject();
		this.TipsNotFinish = base.FindTransform("TipsNotFinish").get_gameObject();
		this.UpImage = base.FindTransform("UpBgImg").GetComponent<RectTransform>();
		this.DownImage = base.FindTransform("DownBgImg").GetComponent<RectTransform>();
		this.ListItems = base.FindTransform("ListItems").GetComponent<RectTransform>();
		this.ItemPrefab = base.FindTransform("Difficulty1").get_gameObject();
		this.SCBtn = base.FindTransform("SCButton").GetComponent<ButtonCustom>();
		this.SCBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.StartSCFight);
		this.ChestBtn = base.FindTransform("ButtonReward").GetComponent<ButtonCustom>();
		this.ChestBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChest);
		this.RankListBtn = base.FindTransform("ButtonRank").GetComponent<ButtonCustom>();
		this.RankListBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRankList);
		this.ButtonDesc.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDesc);
	}

	protected override void OnEnable()
	{
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI != null)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Survival, base.get_transform(), 0);
			changePetChooseUI.Show(true);
		}
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110033), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
		this.scrollRect.OnHasBuilt = delegate
		{
			this.scrollRect.OnHasBuilt = null;
			int index = SurvivalManager.Instance.StageMax - SurvivalManager.Instance.StageCurr;
			this.SetStageSelected(index, true);
		};
	}

	protected override void OnDisable()
	{
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBuyTimes(GameObject go)
	{
		int vipTimesByType = VIPPrivilegeManager.Instance.GetVipTimesByType(8);
		int needDiamond = DataReader<ShengCunMiJingCiShuGouMai>.Get(SurvivalManager.Instance.ScInfo.challengeTimeBuyNum + 1).needDiamond;
		if (SurvivalManager.Instance.ScInfo.challengeTimeBuyNum >= vipTimesByType)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513528, false), 1f, 1f);
			return;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(512058, false), string.Format(GameDataUtils.GetChineseContent(512059, false), needDiamond, DataReader<ShengCunMiJingPeiZhi>.Get("buyNum").value, vipTimesByType - SurvivalManager.Instance.ScInfo.challengeTimeBuyNum + "/" + vipTimesByType), null, null, delegate
		{
			DialogBoxUIViewModel.Instance.BtnRclose = true;
			NetworkManager.Send(new PurchaseSecretAreaTimesReq(), ServerType.Data);
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null);
	}

	private void OnClickDesc(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 512060, 512032);
	}

	private void OnClickChest(GameObject go)
	{
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		TiaoZhanBoCi currentInfo = SurvivalManager.Instance.GetCurrentInfo();
		if (currentInfo == null)
		{
			return;
		}
		for (int i = 0; i < currentInfo.currencyType.get_Count(); i++)
		{
			list.Add(currentInfo.currencyType.get_Item(i));
			list2.Add((long)currentInfo.currencyNum.get_Item(i));
		}
		RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
		rewardUI.SetRewardItem("副本奖励", list, list2, true, false, null, null);
	}

	private void OnClickRankList(GameObject go)
	{
		SurvivalManager.Instance.SendSurvivalChallengeRankingListReq(1);
		UIManagerControl.Instance.OpenUI("SCRankingListUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
	}

	private void StartSCFight(GameObject go)
	{
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		SurvivalManager.Instance.SendChallengeSecretAreaReq();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SCUpdateUI, new Callback(this.RefreshUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SCUpdateUI, new Callback(this.RefreshUI));
	}

	public void RefreshUI()
	{
		for (int i = 0; i < this.ListItems.get_transform().get_childCount(); i++)
		{
			if (this.ItemPrefab != this.ListItems.get_transform().GetChild(i).get_gameObject())
			{
				Object.Destroy(this.ListItems.get_transform().GetChild(i).get_gameObject());
			}
		}
		int num = SurvivalManager.Instance.StageMax - SurvivalManager.Instance.StageMaxHis;
		num = ((num < 0) ? 0 : num);
		for (int j = SurvivalManager.Instance.StageMax; j > num; j--)
		{
			GameObject gameObject = UGUITools.AddChild(this.ListItems.get_gameObject(), this.ItemPrefab, false);
			gameObject.SetActive(true);
			gameObject.GetComponent<DifficultyItem>().updateItem(j);
			gameObject.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickItem);
		}
		this.Times.set_text(SurvivalManager.Instance.ScInfo.challengeTime.ToString());
		this.ButtonBuyTimes.get_gameObject().SetActive(SurvivalManager.Instance.ScInfo.challengeTime <= 0);
	}

	private void OnClickItem(GameObject go)
	{
		int index = int.Parse(go.get_name());
		this.SetStageSelected(index, false);
	}

	private void SetStageSelected(int index, bool bRightNow)
	{
		int num = SurvivalManager.Instance.StageMax - index;
		SurvivalManager.Instance.CurrentSelectId = num;
		this.UpdateSelectDetail(num);
		this.scrollRect.Move2Page(index, bRightNow);
	}

	private void UpdateSelectDetail(int stage)
	{
		if (this.Select == null)
		{
			return;
		}
		this.Select.GetComponent<DifficultyItem>().updateItem(stage);
		int bossId = DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.stage == stage && a.bossId > 0).bossId;
		Monster monster = DataReader<Monster>.Get(bossId);
		int id = 512055;
		SpriteRenderer spriteRenderer;
		if (monster != null)
		{
			spriteRenderer = GameDataUtils.GetIcon(monster.icon2);
			id = monster.name;
		}
		else
		{
			spriteRenderer = ResourceManager.GetIconSprite("role_icon_wenhao");
		}
		if (spriteRenderer != null)
		{
			ResourceManager.SetSprite(this.BossIcon, spriteRenderer);
		}
		this.BossName.set_text(GameDataUtils.GetChineseContent(id, false));
		if (SurvivalManager.Instance.IsPassAll && stage == SurvivalManager.Instance.StageCurr)
		{
			this.TipsNotFinish.SetActive(false);
			this.TipsHasFinish.SetActive(true);
			this.SCBtn.get_gameObject().SetActive(false);
			return;
		}
		if (SurvivalManager.Instance.StageCurr == stage)
		{
			this.TipsNotFinish.SetActive(false);
			this.TipsHasFinish.SetActive(false);
			this.SCBtn.get_gameObject().SetActive(true);
		}
		else if (SurvivalManager.Instance.StageCurr > stage)
		{
			this.TipsNotFinish.SetActive(false);
			this.TipsHasFinish.SetActive(true);
			this.SCBtn.get_gameObject().SetActive(false);
		}
		else
		{
			this.TipsNotFinish.SetActive(true);
			this.TipsNotFinish.GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(512061, false), stage - 1));
			this.TipsHasFinish.SetActive(false);
			this.SCBtn.get_gameObject().SetActive(false);
		}
	}
}
