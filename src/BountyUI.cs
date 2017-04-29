using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyUI : UIBase
{
	public BountyProductionItem[] ProductionItems;

	public Text Score;

	private DetailDaily detailDaily;

	private DetailUrgent detailUrgent;

	private List<int> FxUidList = new List<int>();

	public bool GettingReward;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.detailDaily = base.FindTransform("DetailDaily").GetComponent<DetailDaily>();
		this.detailUrgent = base.FindTransform("DetailUrgent").GetComponent<DetailUrgent>();
	}

	private void Start()
	{
		base.FindTransform("ButtonRank").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRank);
		base.FindTransform("ButtonExecute").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExecutTask);
		base.FindTransform("ButtonDaily").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonDaily);
		base.FindTransform("ButtonUrgent").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonUrgent);
		base.FindTransform("ButtonDesc").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonDesc);
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetIconSprite("nav_font_lcxs"), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI != null)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Bounty, base.get_transform(), 0);
			Canvas canvas = changePetChooseUI.get_gameObject().AddComponent<Canvas>();
			canvas.set_overrideSorting(true);
			canvas.set_sortingOrder(2020);
			changePetChooseUI.get_gameObject().AddComponent<GraphicRaycaster>();
		}
		this.RefreshUI();
		this.GettingReward = false;
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1706, base.get_transform(), "BountyUI", 2002, null, "UI", -127f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1707, base.get_transform(), "BountyUI", 2002, null, "UI", -127f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1708, base.get_transform(), "BountyUI", 2003, null, "UI", -127f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		CurrenciesUIView currenciesUIView = UIManagerControl.Instance.GetUIIfExist("CurrenciesUI") as CurrenciesUIView;
		if (currenciesUIView != null)
		{
			Canvas canvas2 = currenciesUIView.get_gameObject().AddComponent<Canvas>();
			canvas2.set_overrideSorting(true);
			canvas2.set_sortingOrder(2020);
			currenciesUIView.get_gameObject().AddComponent<GraphicRaycaster>();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		for (int i = 0; i < this.FxUidList.get_Count(); i++)
		{
			FXSpineManager.Instance.DeleteSpine(this.FxUidList.get_Item(i), true);
		}
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI != null)
		{
			Object.Destroy(changePetChooseUI.GetComponent<GraphicRaycaster>());
			Object.Destroy(changePetChooseUI.GetComponent<Canvas>());
		}
		CurrenciesUIView currenciesUIView = UIManagerControl.Instance.GetUIIfExist("CurrenciesUI") as CurrenciesUIView;
		if (currenciesUIView != null)
		{
			Object.Destroy(currenciesUIView.GetComponent<GraphicRaycaster>());
			Object.Destroy(currenciesUIView.GetComponent<Canvas>());
		}
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			ModelPool.Instance.Clear();
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		EventDispatcher.AddListener(EventNames.BountyRefreshUI, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<bool>(EventNames.TeamAotoMatchSpecial, new Callback<bool>(this.OnTeamAotoMatch));
		EventDispatcher.AddListener<bool>(EventNames.TeamAotoMatchError, new Callback<bool>(this.OnTeamAotoMatchError));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		EventDispatcher.RemoveListener(EventNames.BountyRefreshUI, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<bool>(EventNames.TeamAotoMatchSpecial, new Callback<bool>(this.OnTeamAotoMatch));
		EventDispatcher.RemoveListener<bool>(EventNames.TeamAotoMatchError, new Callback<bool>(this.OnTeamAotoMatchError));
		base.RemoveListeners();
	}

	public void RefreshUI()
	{
		BountyProductionItem[] productionItems = this.ProductionItems;
		for (int i = 0; i < productionItems.Length; i++)
		{
			BountyProductionItem bountyProductionItem = productionItems[i];
			bountyProductionItem.get_gameObject().SetActive(false);
		}
		for (int j = 0; j < BountyManager.Instance.Info.productions.get_Count(); j++)
		{
			ProductionInfo info = BountyManager.Instance.Info.productions.get_Item(j);
			this.ProductionItems[j].get_gameObject().SetActive(true);
			this.ProductionItems[j].GetComponent<BountyProductionItem>().UpdateData(info);
		}
		this.Score.set_text(string.Format(GameDataUtils.GetChineseContent(513631, false), BountyManager.Instance.Info.score.ToString()));
		this.detailDaily.UpdateUI();
		this.detailUrgent.UpdateUI();
		this.OnClickTagButton();
		this.OnSecondsPast();
	}

	private void OnClickTagButton()
	{
		bool isSelectDaily = BountyManager.Instance.isSelectDaily;
		if (isSelectDaily)
		{
			ResourceManager.SetSprite(base.FindTransform("ButtonDaily").GetComponent<Image>(), ResourceManager.GetIconSprite("lcxs_btdaily_1"));
			ResourceManager.SetSprite(base.FindTransform("ButtonUrgent").GetComponent<Image>(), ResourceManager.GetIconSprite("lcxs_bturgent_2"));
		}
		else
		{
			ResourceManager.SetSprite(base.FindTransform("ButtonDaily").GetComponent<Image>(), ResourceManager.GetIconSprite("lcxs_btdaily_2"));
			ResourceManager.SetSprite(base.FindTransform("ButtonUrgent").GetComponent<Image>(), ResourceManager.GetIconSprite("lcxs_bturgent_1"));
		}
		this.detailDaily.get_gameObject().SetActive(isSelectDaily);
		this.detailUrgent.get_gameObject().SetActive(!isSelectDaily);
	}

	private void OnSecondsPast()
	{
		this.detailDaily.OnSecondPass();
		this.detailUrgent.OnSecondPass();
	}

	private void OnTeamAotoMatch(bool arg1)
	{
		if (!arg1)
		{
			MatchUI matchUI = TeamManager.Instance.OpenMatchUI();
			matchUI.Title.set_text(GameDataUtils.GetChineseContent(513632, false));
			matchUI.ButtonText.set_text(GameDataUtils.GetChineseContent(513637, false));
			ResourceManager.SetSprite(matchUI.TipsImage, ResourceManager.GetIconSprite("zhengzaiqianwangmudidi"));
			matchUI.time = 9999;
		}
	}

	private void OnTeamAotoMatchError(bool arg1)
	{
		UIManagerControl.Instance.ShowToastText("匹配失败", 2f, 2f);
		TeamManager.Instance.CloseMatchUI();
	}

	public void OnClickRank(GameObject go)
	{
		NetworkManager.Send(new BountyRankListReq(), ServerType.Data);
	}

	private void OnClickButtonDaily(GameObject go)
	{
		BountyManager.Instance.isSelectDaily = true;
		this.OnClickTagButton();
	}

	private void OnClickButtonUrgent(GameObject go)
	{
		BountyManager.Instance.isSelectDaily = false;
		this.OnClickTagButton();
	}

	private void OnClickButtonDesc(GameObject go)
	{
		int titleID = 513655;
		int descID = 513653;
		if (!BountyManager.Instance.isSelectDaily)
		{
			titleID = 513656;
			descID = 513654;
		}
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, titleID, descID);
	}

	public void OnClickExecutTask(GameObject go)
	{
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		if (!BackpackManager.Instance.ShowBackpackFull())
		{
			if (BountyManager.Instance.isSelectDaily)
			{
				if (BountyManager.Instance.Info.productions.get_Count() >= 4)
				{
					DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(513640, false), GameDataUtils.GetChineseContent(513641, false), null, null, delegate
					{
						NetworkManager.Send(new BountyAcceptTaskReq
						{
							taskId = BountyManager.Instance.Info.taskId
						}, ServerType.Data);
					}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null);
				}
				else
				{
					NetworkManager.Send(new BountyAcceptTaskReq
					{
						taskId = BountyManager.Instance.Info.taskId
					}, ServerType.Data);
				}
			}
			else if (BountyManager.Instance.Info.productions.get_Count() >= 4)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513641, false), 1f, 2f);
			}
			else
			{
				NetworkManager.Send(new BountyAcceptTaskReq
				{
					taskId = (!BountyManager.Instance.isSelectDaily) ? BountyManager.Instance.Info.urgentTaskId : BountyManager.Instance.Info.taskId
				}, ServerType.Data);
			}
		}
	}
}
