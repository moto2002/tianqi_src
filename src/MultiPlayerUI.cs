using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerUI : UIBase
{
	private RawImage AdBg;

	private Transform dropTrans;

	private Transform haveTeamBtns;

	private Transform noTeamBtns;

	private Text beginBtnText;

	private Text joinTimesText;

	private Text activityTimeText;

	private Text introductionText;

	private int joinTimes;

	private int introductionID;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.dropTrans = base.FindTransform("Drop");
		this.joinTimesText = base.FindTransform("TextTimes").GetComponent<Text>();
		this.activityTimeText = base.FindTransform("activityTime").GetComponent<Text>();
		this.introductionText = base.FindTransform("introductionText").GetComponent<Text>();
		this.beginBtnText = base.FindTransform("beginBtnText").GetComponent<Text>();
		this.haveTeamBtns = base.FindTransform("haveTeamButtons");
		this.noTeamBtns = base.FindTransform("noTeamButtons");
		this.AdBg = base.FindTransform("ImageMap").GetComponent<RawImage>();
	}

	private void Start()
	{
		base.FindTransform("quickEnterBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuickEnterTeamBtn);
		base.FindTransform("createTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCreateTeamBtn);
		base.FindTransform("beginButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStarBtn);
		base.FindTransform("MyTeamButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMyTeamBtn);
		this.introductionText.set_text(GameDataUtils.GetChineseContent(50701, false));
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.AddListener<int>("RefreshChallengeCount", new Callback<int>(this.OnRefreshChallengeCount));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.RefreshUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.RemoveListener<int>("RefreshChallengeCount", new Callback<int>(this.OnRefreshChallengeCount));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.RefreshUI));
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		this.SetTitleIcon();
		this.OpenChangePetChooseUI();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(destroy);
		}
	}

	private void OnClickQuickEnterTeamBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			return;
		}
		if (!ActivityCenterManager.Instance.CheckActivityIsOpen(10002))
		{
			string chineseContent = GameDataUtils.GetChineseContent(513526, false);
			UIManagerControl.Instance.ShowToastText(chineseContent);
			return;
		}
		if (MultiPlayerManager.Instance.remainChallengeTimes <= 0)
		{
			string text = "您的参与次数不足";
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		MultiPlayerManager.Instance.SendDarkTrainQuickEnterReq();
	}

	private void OnClickStarBtn(GameObject go)
	{
		MultiPlayerManager.Instance.CheckCanStartFight();
	}

	private void OnClickCreateTeamBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			MultiPlayerManager.Instance.OnMakeTeam(DungeonType.ENUM.Team, new List<int>(), 19);
		}
	}

	private void OnClickMyTeamBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			TeamBasicUI teamBasicUI = UIManagerControl.Instance.OpenUI("TeamBasicUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TeamBasicUI;
			teamBasicUI.get_transform().SetAsLastSibling();
			TeamBasicManager.Instance.SendTeamSettingReq(TeamBasicManager.Instance.MyTeamData.MinLV, TeamBasicManager.Instance.MyTeamData.MaxLV, DungeonType.ENUM.Team, new List<int>());
		}
	}

	private void OnClickIntroduction(GameObject go)
	{
		int titleID = (int)float.Parse(DataReader<MultiCopy>.Get("Rule").value);
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, titleID, this.introductionID);
	}

	private void RefreshUI()
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			this.noTeamBtns.get_gameObject().SetActive(true);
			this.haveTeamBtns.get_gameObject().SetActive(false);
		}
		else
		{
			this.noTeamBtns.get_gameObject().SetActive(false);
			this.haveTeamBtns.get_gameObject().SetActive(true);
		}
	}

	private void OpenChangePetChooseUI()
	{
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.PVE, base.get_transform(), 0);
		changePetChooseUI.Show(true);
		changePetChooseUI.get_transform().SetAsLastSibling();
	}

	private void SetTitleIcon()
	{
		int id = (int)float.Parse(DataReader<MultiCopy>.Get("title").value);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(id), "BACK", delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	private void SetItemGrid(List<int> items)
	{
		if (items == null)
		{
			return;
		}
		for (int i = 0; i < this.dropTrans.get_childCount(); i++)
		{
			Object.Destroy(this.dropTrans.GetChild(i).get_gameObject());
		}
		for (int j = 0; j < items.get_Count(); j++)
		{
			ItemShow.ShowItem(this.dropTrans, items.get_Item(j), -1L, false, null, 2001);
		}
	}

	private void SetActivityInfo(string activityBgIcon, string activityTime, int descID, int challegeCount = 0, List<int> items = null, Action beginCallBack = null)
	{
		this.joinTimes = challegeCount;
		this.introductionID = descID;
		ResourceManager.SetTexture(this.AdBg, activityBgIcon);
		this.activityTimeText.set_text(activityTime);
		this.SetItemGrid(items);
		this.OnRefreshChallengeCount(MultiPlayerManager.Instance.remainChallengeTimes);
	}

	private void OnCreateTeamSuccess()
	{
		this.RefreshUI();
		if (UIManagerControl.Instance.IsOpen("MultiPlayerUI"))
		{
			this.OnClickMyTeamBtn(null);
		}
	}

	private void OnRefreshChallengeCount(int count)
	{
		int num = this.joinTimes - count;
		num = ((num < 0) ? 0 : num);
		this.joinTimesText.set_text(string.Format("{0}/{1}", num, this.joinTimes));
	}

	public void SettingUI(int id, string arg)
	{
		if (id == 10002)
		{
			string value = DataReader<MultiCopy>.Get("Background").value;
			string activityOpenTimeByActivityType = ActivityCenterManager.Instance.GetActivityOpenTimeByActivityType(ActivityType.MultiPeople);
			List<int> list = new List<int>();
			List<int> itemIds = DataReader<TongGuanDiaoLuo>.DataList.get_Item(0).ItemIds;
			if (itemIds != null)
			{
				list.AddRange(itemIds);
			}
			int descID = (int)float.Parse(DataReader<MultiCopy>.Get("introduction").value);
			int challegeCount = 3;
			HuoDongZhongXin activityCfgData = ActivityCenterManager.Instance.GetActivityCfgData(ActivityType.MultiPeople);
			if (activityCfgData != null)
			{
				challegeCount = activityCfgData.num;
			}
			this.SetActivityInfo(value, activityOpenTimeByActivityType, descID, challegeCount, list, null);
		}
	}

	public void SetCountDownText(int time)
	{
		if (this.beginBtnText != null)
		{
			if (time <= 0)
			{
				this.beginBtnText.set_text(GameDataUtils.GetChineseContent(513146, false));
			}
			else
			{
				this.beginBtnText.set_text(time.ToString());
			}
		}
	}
}
