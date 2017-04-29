using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteInstanceDetailUI : UIBase
{
	private int currentInstanceID;

	private static int eliteCfgID;

	public static bool IsOpenFromStack;

	private static EliteDataInfo m_eliteDataInfo;

	private ListPool dropListPool;

	private Image bossIcon;

	private Image eliteDungeonStepImgNum;

	private ListPool scoreViewListPool;

	private ListPool difficultListPool;

	private RectTransform difficultScrRect;

	private EliteDifficultItem lastSelectItem;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.dropListPool = base.FindTransform("Drop").GetComponent<ListPool>();
		this.bossIcon = base.FindTransform("BossIcon").GetComponent<Image>();
		this.eliteDungeonStepImgNum = base.FindTransform("EliteDungeonStepImgNum").GetComponent<Image>();
		this.scoreViewListPool = base.FindTransform("ScoreViewListPool").GetComponent<ListPool>();
		base.FindTransform("QuickEnterBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuickEnterBtn);
		base.FindTransform("FindTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFindTeamBtn);
		base.FindTransform("BeginBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBeginBtn);
		base.FindTransform("MyTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMyTeamBtn);
		base.FindTransform("ScoreTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505400, false));
		this.difficultScrRect = base.FindTransform("EliteDifficultScr").GetComponent<RectTransform>();
		this.difficultListPool = base.FindTransform("DifficultListPool").GetComponent<ListPool>();
		this.scoreViewListPool.Clear();
		this.dropListPool.Clear();
		this.SetScoreText();
		this.difficultListPool.Clear();
	}

	protected override void OnEnable()
	{
		if (EliteInstanceDetailUI.IsOpenFromStack)
		{
			this.RefreshUI(EliteInstanceDetailUI.eliteCfgID);
			if (EliteInstanceDetailUI.m_eliteDataInfo != null)
			{
				this.OnRefreshDifficultyList(EliteInstanceDetailUI.m_eliteDataInfo);
			}
		}
		WaitUI.CloseUI(0u);
		this.CheckShowSEffecFX();
	}

	protected override void OnDisable()
	{
		EliteInstanceDetailUI.IsOpenFromStack = true;
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		this.scoreViewListPool.Clear();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.RefreshBtnState));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.RefreshBtnState));
	}

	protected override void OnClickMaskAction()
	{
		UIStackManager.Instance.PopUIIfTarget("EliteInstanceDetailUI");
		base.OnClickMaskAction();
	}

	private void OnClickQuickEnterBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			return;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), "挑战boss需要组队参加，你确定是否要建立队伍", null, delegate
		{
			List<int> list = new List<int>();
			list.Add(EliteInstanceDetailUI.eliteCfgID);
			EliteDungeonManager.Instance.OnMakeTeam(DungeonType.ENUM.Elite, list, 18);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickFindTeamBtn(GameObject go)
	{
		TeamBasicManager.Instance.OpenSeekTeamUI(DungeonType.ENUM.Elite, EliteInstanceDetailUI.eliteCfgID, null);
	}

	private void OnClickBeginBtn(GameObject go)
	{
		EliteDungeonManager.Instance.CheckCanStarFight(EliteInstanceDetailUI.eliteCfgID);
	}

	private void OnClickMyTeamBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			TeamBasicUI teamBasicUI = UIManagerControl.Instance.OpenUI("TeamBasicUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TeamBasicUI;
			teamBasicUI.get_transform().SetAsLastSibling();
			List<int> list = new List<int>();
			list.Add(EliteInstanceDetailUI.eliteCfgID);
			TeamBasicManager.Instance.SendTeamSettingReq(TeamBasicManager.Instance.MyTeamData.MinLV, TeamBasicManager.Instance.MyTeamData.MaxLV, DungeonType.ENUM.Elite, list);
		}
	}

	private void OnClickSelectDifficult(GameObject go)
	{
		EliteDifficultItem component = go.GetComponent<EliteDifficultItem>();
		if (component != null && this.lastSelectItem != component)
		{
			int eliteID = component.EliteCfgID;
			if (EliteDungeonManager.Instance.CheckCopyIsOpen(eliteID))
			{
				if (this.lastSelectItem != null)
				{
					this.lastSelectItem.Selected = false;
				}
				component.Selected = true;
				this.lastSelectItem = component;
				this.RefreshUI(eliteID);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513600, false));
			}
		}
	}

	public void OnRefreshDifficultyList(EliteDataInfo dataInfo)
	{
		EliteInstanceDetailUI.m_eliteDataInfo = dataInfo;
		if (dataInfo != null && dataInfo.cfgIDList.get_Count() > 0)
		{
			this.difficultListPool.Clear();
			this.difficultListPool.Create(dataInfo.cfgIDList.get_Count(), delegate(int index)
			{
				if (index < dataInfo.cfgIDList.get_Count() && index < this.difficultListPool.Items.get_Count())
				{
					EliteDifficultItem component = this.difficultListPool.Items.get_Item(index).GetComponent<EliteDifficultItem>();
					if (component != null)
					{
						component.RefreshUI(dataInfo.cfgIDList.get_Item(index));
					}
					this.difficultListPool.Items.get_Item(index).GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectDifficult);
					if (index == EliteDungeonManager.Instance.GetCanChallegeRankIndex(dataInfo.cfgIDList))
					{
						this.OnClickSelectDifficult(this.difficultListPool.Items.get_Item(index).get_gameObject());
					}
				}
			});
			this.difficultScrRect.set_sizeDelta(new Vector2(this.difficultScrRect.get_sizeDelta().x, (float)(80 * dataInfo.cfgIDList.get_Count())));
		}
	}

	public void RefreshUI(int eliteID)
	{
		JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(eliteID);
		if (jingYingFuBenPeiZhi == null)
		{
			Debugger.LogError("没有找到ID为" + eliteID + "的精英副本配置表");
			return;
		}
		this.OpenCPC();
		this.currentInstanceID = jingYingFuBenPeiZhi.copyId;
		EliteInstanceDetailUI.eliteCfgID = eliteID;
		EliteDungeonManager.Instance.eliteCfgID = eliteID;
		Icon icon = DataReader<Icon>.Get(jingYingFuBenPeiZhi.bossPic);
		if (icon != null)
		{
			ResourceManager.SetSprite(this.bossIcon, ResourceManager.GetIconSprite(icon.icon));
		}
		base.FindTransform("TextInstance").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(jingYingFuBenPeiZhi.bossName, false));
		base.FindTransform("TextPower").GetComponent<Text>().set_text(jingYingFuBenPeiZhi.power.ToString());
		ResourceManager.SetIconSprite(this.eliteDungeonStepImgNum, "shuzi_jie_" + jingYingFuBenPeiZhi.step);
		List<int> list = new List<int>();
		if (jingYingFuBenPeiZhi.normalShow != null)
		{
			for (int i = 0; i < jingYingFuBenPeiZhi.normalShow.get_Count(); i++)
			{
				list.Add(jingYingFuBenPeiZhi.normalShow.get_Item(i).key);
			}
		}
		list.Sort(new Comparison<int>(EliteInstanceDetailUI.ItemIDSortCompara));
		this.SetDropItemList(list);
		this.RefreshBtnState();
	}

	private void OpenCPC()
	{
		base.get_transform().GetComponent<RectTransform>().SetAsLastSibling();
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Elite, base.get_transform(), 0);
	}

	private void RefreshBtnState()
	{
		if (TeamBasicManager.Instance.IsHaveTeam())
		{
			base.FindTransform("HaveTeamButtons").get_gameObject().SetActive(true);
			base.FindTransform("NoTeamButtons").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("HaveTeamButtons").get_gameObject().SetActive(false);
			base.FindTransform("NoTeamButtons").get_gameObject().SetActive(true);
		}
	}

	private void OnCreateTeamSuccess()
	{
		this.RefreshBtnState();
	}

	private void SetScoreText()
	{
		this.scoreViewListPool.Clear();
		List<string> contentList = new List<string>();
		contentList.Add(GameDataUtils.GetChineseContent(505401, false));
		int count = DataReader<JTongGuanPingJi>.DataList.get_Count();
		for (int i = 0; i < count; i++)
		{
			string text = string.Empty;
			int time = DataReader<JTongGuanPingJi>.DataList.get_Item(i).time;
			text = string.Format(GameDataUtils.GetChineseContent(505402 + i, false), time);
			contentList.Add(text);
		}
		if (contentList != null && contentList.get_Count() > 0)
		{
			this.scoreViewListPool.Create(contentList.get_Count(), delegate(int index)
			{
				if (index < this.scoreViewListPool.Items.get_Count() && index < contentList.get_Count())
				{
					Transform transform = this.scoreViewListPool.Items.get_Item(index).get_transform();
					Text component = transform.FindChild("BossDesc").GetComponent<Text>();
					if (component != null)
					{
						component.set_text(contentList.get_Item(index));
					}
				}
			});
		}
	}

	private void SetDropItemList(List<int> rewardIDs)
	{
		this.dropListPool.Clear();
		if (rewardIDs != null && rewardIDs.get_Count() > 0)
		{
			this.dropListPool.Create(rewardIDs.get_Count(), delegate(int index)
			{
				if (index < rewardIDs.get_Count() && index < this.dropListPool.Items.get_Count())
				{
					RewardItem component = this.dropListPool.Items.get_Item(index).GetComponent<RewardItem>();
					if (component != null)
					{
						component.SetRewardItem(rewardIDs.get_Item(index), -1L, 0L);
					}
				}
			});
		}
	}

	private void CheckShowSEffecFX()
	{
		if (!DataReader<JingYingFuBenPeiZhi>.Contains(EliteInstanceDetailUI.eliteCfgID))
		{
			return;
		}
		JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(EliteInstanceDetailUI.eliteCfgID);
		if (DataReader<JingYingFuBenPeiZhi>.Contains(EliteDungeonManager.Instance.newOpenCfgID))
		{
			JingYingFuBenPeiZhi jingYingFuBenPeiZhi2 = DataReader<JingYingFuBenPeiZhi>.Get(EliteDungeonManager.Instance.newOpenCfgID);
			if (jingYingFuBenPeiZhi2.rank > 1 && jingYingFuBenPeiZhi.bossName == jingYingFuBenPeiZhi2.bossName)
			{
				EliteNewOpenCopyTipUI eliteNewOpenCopyTipUI = UIManagerControl.Instance.OpenUI("EliteNewOpenCopyTipUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as EliteNewOpenCopyTipUI;
				string chineseContent = GameDataUtils.GetChineseContent(jingYingFuBenPeiZhi2.bossName, false);
				string chineseContent2 = GameDataUtils.GetChineseContent(505093 + jingYingFuBenPeiZhi2.rank, false);
				eliteNewOpenCopyTipUI.RefreshUI(string.Format(GameDataUtils.GetChineseContent(513601, false), chineseContent, chineseContent2));
				EliteDungeonManager.Instance.newOpenCfgID = -1;
			}
		}
	}

	private static int ItemIDSortCompara(int A1, int A2)
	{
		if (A1 < A2)
		{
			return -1;
		}
		return 1;
	}
}
