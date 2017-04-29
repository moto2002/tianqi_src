using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildWarVSInfoUI : UIBase
{
	public enum GuildWarVSType
	{
		GuildWarQualificationType = 1,
		GuildWarLastWeekType,
		GuildWarThisWeekType
	}

	private ListPool warQualificationItemsListPool;

	private ButtonCustom guildWarQualificationBtn;

	private ButtonCustom guildWarWeekVSBtn;

	private GuildWarVSInfoUI.GuildWarVSType guildWarBtnState;

	private Text myGuildActivePointText;

	private Text guildwarBtnStateText;

	private Text guildWarQualificationTip;

	private ButtonCustom btnRewardUI;

	private ButtonCustom btnAddActivePoint;

	private ButtonCustom btnRule;

	private bool isShowThisWeekVS;

	private bool isCanGoToGuildWarScene;

	private GameObject guildWeekVSPanel;

	private GameObject guildWarQualificationPanel;

	private GameObject guildWarOpenTimeRoot;

	private TimeCountDown timeCountDown;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.warQualificationItemsListPool = base.FindTransform("WarQualificationListPool").GetComponent<ListPool>();
		this.myGuildActivePointText = base.FindTransform("MyGuildActivePoint").GetComponent<Text>();
		this.guildwarBtnStateText = base.FindTransform("GuildWarStepText").GetComponent<Text>();
		this.guildWarQualificationTip = base.FindTransform("GuildWarQualificationTip").GetComponent<Text>();
		this.btnRewardUI = base.FindTransform("BtnRewardUI").GetComponent<ButtonCustom>();
		this.btnAddActivePoint = base.FindTransform("BtnAddActivePoint").GetComponent<ButtonCustom>();
		this.btnRule = base.FindTransform("RuleBtn").GetComponent<ButtonCustom>();
		this.guildWarQualificationBtn = base.FindTransform("GuildWarQualificationBtn").GetComponent<ButtonCustom>();
		this.guildWarWeekVSBtn = base.FindTransform("GuildWarWeekVSBtn").GetComponent<ButtonCustom>();
		this.btnRewardUI.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnRewardUI);
		this.btnAddActivePoint.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnAddActivePoint);
		this.btnRule.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnRule);
		this.guildWarQualificationBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.guildWarWeekVSBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.guildWeekVSPanel = base.FindTransform("GuildWeekVSPanel").get_gameObject();
		this.guildWarQualificationPanel = base.FindTransform("GuildWarQualificationPanel").get_gameObject();
		this.guildWarOpenTimeRoot = base.FindTransform("GuildWarOpenTime").get_gameObject();
		this.warQualificationItemsListPool.SetItem("GuildWarQualificationItem");
		if (this.guildWarQualificationTip != null && this.guildWarQualificationTip.get_gameObject().get_activeSelf())
		{
			this.guildWarQualificationTip.get_gameObject().SetActive(false);
		}
		this.SetGuildWarOpenTime();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.warQualificationItemsListPool.Clear();
		this.ResetGuildVSInfo();
		this.RefreshUI();
		this.UpdateMyGuildInfo();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.warQualificationItemsListPool.Clear();
		this.RemoveNextCompleteCD();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnEligibilityGuildInfoRes, new Callback(this.OnEligibilityGuildInfoRes));
		EventDispatcher.AddListener<int>(EventNames.OnWeekVsInfosRes, new Callback<int>(this.OnWeekVsInfosRes));
		EventDispatcher.AddListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.AddListener(EventNames.OnGuildActivityNty, new Callback(this.OnGuildActivityNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnEligibilityGuildInfoRes, new Callback(this.OnEligibilityGuildInfoRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnWeekVsInfosRes, new Callback<int>(this.OnWeekVsInfosRes));
		EventDispatcher.RemoveListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.RemoveListener(EventNames.OnGuildActivityNty, new Callback(this.OnGuildActivityNty));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnRewardUI(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("GuildWarRewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		this.Show(false);
	}

	private void OnClickBtnAddActivePoint(GameObject go)
	{
		if (this.isCanGoToGuildWarScene)
		{
			GuildWarManager.Instance.SendEnterWaitingRoomReq();
		}
		else
		{
			LinkNavigationManager.OpenDailyTaskUI();
			this.Show(false);
		}
	}

	private void OnClickBtnRule(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 515091, 515090);
	}

	private void OnClickTab(GameObject go)
	{
		if (go == this.guildWarQualificationBtn.get_gameObject())
		{
			GuildWarVSInfoUI.GuildWarVSType guildWarVSType = (!this.isShowThisWeekVS) ? GuildWarVSInfoUI.GuildWarVSType.GuildWarQualificationType : GuildWarVSInfoUI.GuildWarVSType.GuildWarThisWeekType;
			if (this.guildWarBtnState == guildWarVSType)
			{
				return;
			}
			this.guildWarBtnState = guildWarVSType;
			this.RefreshUIByType(this.guildWarBtnState);
			this.RefreshTabBtnByType(this.guildWarBtnState);
		}
		else if (go == this.guildWarWeekVSBtn.get_gameObject())
		{
			if (this.guildWarBtnState == GuildWarVSInfoUI.GuildWarVSType.GuildWarLastWeekType)
			{
				return;
			}
			this.RefreshLastWeekVSPanel();
		}
	}

	private void RefreshUI()
	{
		if (GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.ELIGIBILITY)
		{
			this.guildWarBtnState = GuildWarVSInfoUI.GuildWarVSType.GuildWarQualificationType;
			this.isShowThisWeekVS = false;
			this.guildWarQualificationBtn.get_transform().FindChild("btnText").GetComponent<Text>().set_text("入围军团");
		}
		else
		{
			this.guildWarBtnState = GuildWarVSInfoUI.GuildWarVSType.GuildWarThisWeekType;
			this.isShowThisWeekVS = true;
			this.guildWarQualificationBtn.get_transform().FindChild("btnText").GetComponent<Text>().set_text("军团对战");
		}
		this.RefreshGuildWarBtnState();
		this.RefreshUIByType(this.guildWarBtnState);
		this.RefreshTabBtnByType(this.guildWarBtnState);
	}

	private void RefreshUIByType(GuildWarVSInfoUI.GuildWarVSType type)
	{
		if (type == GuildWarVSInfoUI.GuildWarVSType.GuildWarQualificationType)
		{
			if (this.guildWeekVSPanel.get_activeSelf())
			{
				this.guildWeekVSPanel.SetActive(false);
			}
			if (!this.guildWarQualificationPanel.get_activeSelf())
			{
				this.guildWarQualificationPanel.SetActive(true);
			}
			this.RefreshQualificationPanel();
		}
		else if (type == GuildWarVSInfoUI.GuildWarVSType.GuildWarLastWeekType)
		{
			if (!this.guildWeekVSPanel.get_activeSelf())
			{
				this.guildWeekVSPanel.SetActive(true);
			}
			if (this.guildWarQualificationPanel.get_activeSelf())
			{
				this.guildWarQualificationPanel.SetActive(false);
			}
			this.RefreshLastWeekVSPanel();
		}
		else if (type == GuildWarVSInfoUI.GuildWarVSType.GuildWarThisWeekType)
		{
			if (!this.guildWeekVSPanel.get_activeSelf())
			{
				this.guildWeekVSPanel.SetActive(true);
			}
			if (this.guildWarQualificationPanel.get_activeSelf())
			{
				this.guildWarQualificationPanel.SetActive(false);
			}
			this.RefreshThisWeekVSPanel();
		}
	}

	private void RefreshTabBtnByType(GuildWarVSInfoUI.GuildWarVSType type)
	{
		if (type == GuildWarVSInfoUI.GuildWarVSType.GuildWarQualificationType || type == GuildWarVSInfoUI.GuildWarVSType.GuildWarThisWeekType)
		{
			this.SetBtnLightAndDim(this.guildWarQualificationBtn.get_gameObject(), "y_fenye3", true);
			this.SetBtnLightAndDim(this.guildWarWeekVSBtn.get_gameObject(), "y_fenye4", false);
		}
		else if (type == GuildWarVSInfoUI.GuildWarVSType.GuildWarLastWeekType)
		{
			this.SetBtnLightAndDim(this.guildWarWeekVSBtn.get_gameObject(), "y_fenye3", true);
			this.SetBtnLightAndDim(this.guildWarQualificationBtn.get_gameObject(), "y_fenye4", false);
			this.guildwarBtnStateText.set_text(string.Empty);
			this.btnAddActivePoint.get_gameObject().SetActive(false);
		}
		this.RefreshGuildWarBtnState();
	}

	private void RefreshGuildWarBtnState()
	{
		this.RemoveNextCompleteCD();
		if (this.guildWarBtnState == GuildWarVSInfoUI.GuildWarVSType.GuildWarLastWeekType)
		{
			return;
		}
		bool flag = GuildWarManager.Instance.CheckCanJoinInGuildWar();
		this.guildwarBtnStateText.set_text(string.Empty);
		this.btnAddActivePoint.get_transform().FindChild("Text").GetComponent<Text>().set_text("进入军团战");
		this.isCanGoToGuildWarScene = flag;
		if (GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.NORMAL)
		{
			this.btnAddActivePoint.get_gameObject().SetActive(true);
			this.btnAddActivePoint.get_transform().FindChild("Text").GetComponent<Text>().set_text("提升活跃度");
			this.isCanGoToGuildWarScene = false;
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep > GuildWarTimeStep.GWTS.NORMAL && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.HALF_MATCH1_BEG)
		{
			this.btnAddActivePoint.get_gameObject().SetActive(false);
			if (flag)
			{
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515097, false));
			}
			else
			{
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515093, false));
			}
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH1_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH2_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.FINAL_MATCH_BEG)
		{
			if (flag)
			{
				this.btnAddActivePoint.get_gameObject().SetActive(true);
			}
			else
			{
				this.btnAddActivePoint.get_gameObject().SetActive(false);
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515093, false));
			}
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH1_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.HALF_MATCH2_BEG)
		{
			this.btnAddActivePoint.get_gameObject().SetActive(false);
			if (flag)
			{
				this.SetNextCompleteCD();
			}
			else
			{
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515093, false));
			}
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH2_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_BEG)
		{
			this.btnAddActivePoint.get_gameObject().SetActive(false);
			if (flag)
			{
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515094, false));
			}
			else
			{
				this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515093, false));
			}
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.FINAL_MATCH_END)
		{
			this.isCanGoToGuildWarScene = false;
			this.btnAddActivePoint.get_gameObject().SetActive(false);
			this.guildwarBtnStateText.set_text(GameDataUtils.GetChineseContent(515105, false));
		}
	}

	private void UpdateMyGuildInfo()
	{
		this.myGuildActivePointText.set_text(string.Empty);
		if (GuildManager.Instance.MyGuildnfo != null)
		{
			this.myGuildActivePointText.set_text(GuildManager.Instance.MyGuildnfo.activity + string.Empty);
			base.FindTransform("MyGuildActivePointName").GetComponent<Text>().set_text("本军团活跃度：");
		}
		else
		{
			base.FindTransform("MyGuildActivePointName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(34157, false));
		}
	}

	private void RefreshQualificationPanel()
	{
		GuildWarManager.Instance.SendEligibilityGuildInfoReq();
	}

	private void UpdateQualificationList()
	{
		this.warQualificationItemsListPool.Clear();
		if (GuildWarManager.Instance.QualitificationGuildInfoList != null && GuildWarManager.Instance.QualitificationGuildInfoList.get_Count() > 0)
		{
			if (this.guildWarQualificationTip != null && this.guildWarQualificationTip.get_gameObject().get_activeSelf())
			{
				this.guildWarQualificationTip.get_gameObject().SetActive(false);
			}
			int count = GuildWarManager.Instance.QualitificationGuildInfoList.get_Count();
			this.warQualificationItemsListPool.Create(count, delegate(int index)
			{
				if (index < count && index < this.warQualificationItemsListPool.Items.get_Count())
				{
					GuildWarQualificationItem component = this.warQualificationItemsListPool.Items.get_Item(index).GetComponent<GuildWarQualificationItem>();
					GuildParticipantInfo guildParticipantInfo = GuildWarManager.Instance.QualitificationGuildInfoList.get_Item(index);
					component.UpdateItem(guildParticipantInfo);
				}
			});
		}
		else if (this.guildWarQualificationTip != null && !this.guildWarQualificationTip.get_gameObject().get_activeSelf())
		{
			this.guildWarQualificationTip.get_gameObject().SetActive(true);
		}
	}

	private void RefreshLastWeekVSPanel()
	{
		GuildWarManager.Instance.SendWeekVsInfosReq(1);
	}

	private void RefreshThisWeekVSPanel()
	{
		this.ResetGuildVSInfo();
		GuildWarManager.Instance.SendWeekVsInfosReq(0);
	}

	private void UpdateWeekVSData(WeekVsInfosRes weekVSInfoData)
	{
		if (weekVSInfoData != null)
		{
			if (weekVSInfoData.first8Infos != null)
			{
				for (int i = 0; i < weekVSInfoData.first8Infos.get_Count(); i++)
				{
					List<GuildParticipantInfo> guildsInfo = weekVSInfoData.first8Infos.get_Item(i).guildsInfo;
					for (int j = 0; j < guildsInfo.get_Count(); j++)
					{
						GuildParticipantInfo guildParticipantInfo = guildsInfo.get_Item(j);
						base.FindTransform("FirstRound").FindChild("GuildNameText" + guildParticipantInfo.rank).GetComponent<Text>().set_text(guildParticipantInfo.name);
						long num = weekVSInfoData.first8Infos.get_Item(i).winnerId;
						if (guildsInfo.get_Count() <= 1 && GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH1_BEG)
						{
							num = guildParticipantInfo.guildId;
						}
						if (guildParticipantInfo.guildId == num)
						{
							int vSGroupByRank = GuildWarManager.Instance.GetVSGroupByRank(guildParticipantInfo.rank);
							Transform transform = base.FindTransform("SecondRound").FindChild("GuildNameText" + vSGroupByRank);
							if (transform != null)
							{
								transform.GetComponent<Text>().set_text(guildParticipantInfo.name);
							}
							base.FindTransform("FirstRound").FindChild("GuildWin" + guildParticipantInfo.rank).get_gameObject().SetActive(true);
						}
					}
				}
			}
			if (weekVSInfoData.second4Infos != null && weekVSInfoData.second4Infos.get_Count() > 0)
			{
				for (int k = 0; k < weekVSInfoData.second4Infos.get_Count(); k++)
				{
					List<GuildParticipantInfo> guildsInfo2 = weekVSInfoData.second4Infos.get_Item(k).guildsInfo;
					for (int l = 0; l < guildsInfo2.get_Count(); l++)
					{
						GuildParticipantInfo guildParticipantInfo2 = guildsInfo2.get_Item(l);
						int vSGroupByRank2 = GuildWarManager.Instance.GetVSGroupByRank(guildParticipantInfo2.rank);
						base.FindTransform("SecondRound").FindChild("GuildNameText" + vSGroupByRank2).GetComponent<Text>().set_text(guildParticipantInfo2.name);
						base.FindTransform("FirstRound").FindChild("GuildWin" + guildParticipantInfo2.rank).get_gameObject().SetActive(true);
						long num2 = weekVSInfoData.second4Infos.get_Item(k).winnerId;
						if (guildsInfo2.get_Count() <= 1 && GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH2_BEG)
						{
							num2 = guildParticipantInfo2.guildId;
						}
						if (guildParticipantInfo2.guildId == num2)
						{
							int vSGroup2ByRank = GuildWarManager.Instance.GetVSGroup2ByRank(guildParticipantInfo2.rank);
							Transform transform2 = base.FindTransform("ThirdRound").FindChild("GuildNameText" + vSGroup2ByRank);
							if (transform2 != null)
							{
								transform2.GetComponent<Text>().set_text(guildParticipantInfo2.name);
							}
							base.FindTransform("SecondRound").FindChild("GuildWin" + vSGroupByRank2).get_gameObject().SetActive(true);
						}
					}
				}
			}
			if (weekVSInfoData.third2Infos != null && weekVSInfoData.third2Infos.get_Count() > 0)
			{
				for (int m = 0; m < weekVSInfoData.third2Infos.get_Count(); m++)
				{
					List<GuildParticipantInfo> guildsInfo3 = weekVSInfoData.third2Infos.get_Item(m).guildsInfo;
					for (int n = 0; n < guildsInfo3.get_Count(); n++)
					{
						GuildParticipantInfo guildParticipantInfo3 = guildsInfo3.get_Item(n);
						int vSGroup2ByRank2 = GuildWarManager.Instance.GetVSGroup2ByRank(guildParticipantInfo3.rank);
						base.FindTransform("ThirdRound").FindChild("GuildNameText" + vSGroup2ByRank2).GetComponent<Text>().set_text(guildParticipantInfo3.name);
						base.FindTransform("SecondRound").FindChild("GuildWin" + GuildWarManager.Instance.GetVSGroupByRank(guildParticipantInfo3.rank)).get_gameObject().SetActive(true);
						long num3 = weekVSInfoData.third2Infos.get_Item(m).winnerId;
						if (guildsInfo3.get_Count() <= 1 && GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.FINAL_MATCH_BEG)
						{
							num3 = guildParticipantInfo3.guildId;
						}
						if (guildParticipantInfo3.guildId == num3)
						{
							base.FindTransform("GuildWarWinnerNameText").GetComponent<Text>().set_text(guildParticipantInfo3.name);
							base.FindTransform("ThirdRound").FindChild("GuildWin" + vSGroup2ByRank2).get_gameObject().SetActive(true);
						}
					}
				}
			}
		}
	}

	private void SetGuildWarOpenTime()
	{
		for (int i = 2; i <= 4; i++)
		{
			string guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(i, "56cc2d");
			if (i == 2)
			{
				base.FindTransform("EightToFourText").GetComponent<Text>().set_text(guildWarOpenTime);
			}
			else if (i == 3)
			{
				base.FindTransform("HalfText").GetComponent<Text>().set_text(guildWarOpenTime);
			}
			else if (i == 4)
			{
				base.FindTransform("FinalText").GetComponent<Text>().set_text(guildWarOpenTime);
			}
		}
	}

	private void ResetGuildVSInfo()
	{
		for (int i = 0; i < 8; i++)
		{
			Transform transform = base.FindTransform("FirstRound").FindChild("GuildNameText" + (i + 1));
			if (transform != null)
			{
				transform.GetComponent<Text>().set_text(string.Empty);
			}
			base.FindTransform("FirstRound").FindChild("GuildWin" + (i + 1)).get_gameObject().SetActive(false);
			if (i < 4)
			{
				base.FindTransform("SecondRound").FindChild("GuildNameText" + (i + 1)).GetComponent<Text>().set_text(string.Empty);
				base.FindTransform("SecondRound").FindChild("GuildWin" + (i + 1)).get_gameObject().SetActive(false);
			}
			if (i < 2)
			{
				base.FindTransform("ThirdRound").FindChild("GuildNameText" + (i + 1)).GetComponent<Text>().set_text(string.Empty);
				base.FindTransform("ThirdRound").FindChild("GuildWin" + (i + 1)).get_gameObject().SetActive(false);
			}
		}
		base.FindTransform("GuildWarWinnerNameText").GetComponent<Text>().set_text(string.Empty);
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}

	private void SetNextCompleteCD()
	{
		this.RemoveNextCompleteCD();
		int match1ToMatch2RemainTime = GuildWarManager.Instance.GetMatch1ToMatch2RemainTime();
		this.timeCountDown = new TimeCountDown(match1ToMatch2RemainTime, TimeFormat.HHMMSS, delegate
		{
			if (this.timeCountDown != null && this.guildwarBtnStateText != null)
			{
				this.guildwarBtnStateText.set_text(string.Format(GameDataUtils.GetChineseContent(515092, false), this.timeCountDown.GetTime()));
			}
		}, delegate
		{
			this.guildwarBtnStateText.set_text(string.Format(GameDataUtils.GetChineseContent(515092, false), 0));
			this.RemoveNextCompleteCD();
		}, true);
	}

	private void RemoveNextCompleteCD()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}

	private void OnEligibilityGuildInfoRes()
	{
		this.UpdateQualificationList();
	}

	private void OnWeekVsInfosRes(int week)
	{
		if (week == 0)
		{
			if (!this.guildWarOpenTimeRoot.get_activeSelf())
			{
				this.guildWarOpenTimeRoot.SetActive(true);
			}
			this.UpdateWeekVSData(GuildWarManager.Instance.ThisWeekVSInfoData);
			this.RefreshGuildWarBtnState();
		}
		else if (week == 1)
		{
			if (GuildWarManager.Instance.LastWeekVSInfoData == null || GuildWarManager.Instance.LastWeekVSInfoData.first8Infos == null || GuildWarManager.Instance.LastWeekVSInfoData.first8Infos.get_Count() <= 0)
			{
				UIManagerControl.Instance.ShowToastText("没有上周战绩");
				return;
			}
			if (!this.guildWeekVSPanel.get_activeSelf())
			{
				this.guildWeekVSPanel.SetActive(true);
			}
			if (this.guildWarQualificationPanel.get_activeSelf())
			{
				this.guildWarQualificationPanel.SetActive(false);
			}
			if (this.guildWarOpenTimeRoot.get_activeSelf())
			{
				this.guildWarOpenTimeRoot.SetActive(false);
			}
			this.ResetGuildVSInfo();
			this.guildWarBtnState = GuildWarVSInfoUI.GuildWarVSType.GuildWarLastWeekType;
			this.RefreshTabBtnByType(this.guildWarBtnState);
			this.UpdateWeekVSData(GuildWarManager.Instance.LastWeekVSInfoData);
		}
	}

	private void OnGuildWarStepNty()
	{
		this.RefreshUI();
	}

	private void OnGuildActivityNty()
	{
		this.UpdateMyGuildInfo();
	}
}
