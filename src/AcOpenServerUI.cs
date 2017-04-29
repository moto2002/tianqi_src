using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcOpenServerUI : UIBase
{
	private ListPool taskListPool;

	private ListPool activityTypeListPool;

	private ListPool upWayListPool;

	private RawImage leftPictureImg;

	private Text myRankingText;

	private Text myLevelTextName;

	private Text myLevelText;

	private Text remainTimeCDText;

	private AcOpenServerBtnItem currentSelectBtnItem;

	private TimeCountDown activityRemainCD;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.taskListPool = base.FindTransform("TaskListPool").GetComponent<ListPool>();
		this.activityTypeListPool = base.FindTransform("ActivityTypeListPool").GetComponent<ListPool>();
		this.upWayListPool = base.FindTransform("UpWayListPool").GetComponent<ListPool>();
		this.leftPictureImg = base.FindTransform("LeftPictureImg").GetComponent<RawImage>();
		this.myRankingText = base.FindTransform("MyRankingText").GetComponent<Text>();
		this.myLevelTextName = base.FindTransform("MyLevelTextName").GetComponent<Text>();
		this.myLevelText = base.FindTransform("MyLevelText").GetComponent<Text>();
		this.remainTimeCDText = base.FindTransform("RemainTimeCDText").GetComponent<Text>();
		base.FindTransform("IntroductionBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHelpBtn);
		base.FindTransform("RefreshBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefreshBtn);
		this.taskListPool.Clear();
		this.activityTypeListPool.Clear();
		this.upWayListPool.Clear();
		base.FindTransform("GetRewardTipText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513241, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.taskListPool.Clear();
		this.RemoveActivityCountDown();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetOpenServerActInfoRes, new Callback(this.OnGetOpenServerActInfoRes));
		EventDispatcher.AddListener(EventNames.OnOpenServerActStatusNty, new Callback(this.OnOpenServerActStatusNty));
		EventDispatcher.AddListener(EventNames.OnGetSumRechargeRes, new Callback(this.OnGetSumRechargeRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetOpenServerActInfoRes, new Callback(this.OnGetOpenServerActInfoRes));
		EventDispatcher.RemoveListener(EventNames.OnOpenServerActStatusNty, new Callback(this.OnOpenServerActStatusNty));
		EventDispatcher.RemoveListener(EventNames.OnGetSumRechargeRes, new Callback(this.OnGetSumRechargeRes));
	}

	private void OnClickActivityTypeItemBtn(GameObject go)
	{
		AcOpenServerBtnItem component = go.GetComponent<AcOpenServerBtnItem>();
		if (component == null)
		{
			return;
		}
		if (!AcOpenServerManager.Instance.CheckActivityTypeUnLock(component.ActivityTypeID))
		{
			KaiFuHuoDong kaiFuHuoDong = DataReader<KaiFuHuoDong>.Get(component.ActivityTypeID);
			if (kaiFuHuoDong != null && kaiFuHuoDong.openDay != null && kaiFuHuoDong.openDay.get_Count() > 0)
			{
				int num = kaiFuHuoDong.openDay.get_Item(0);
				if (num > 0)
				{
					UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(513169, false), num) + GameDataUtils.GetChineseContent(508009, false));
				}
			}
			return;
		}
		if (component == this.currentSelectBtnItem)
		{
			return;
		}
		if (this.currentSelectBtnItem != null)
		{
			this.currentSelectBtnItem.Selected = false;
		}
		component.Selected = true;
		this.UpdateSelectBtnItem(component);
	}

	private void UpdateSelectBtnItem(AcOpenServerBtnItem btnItem)
	{
		if (btnItem == null)
		{
			return;
		}
		this.currentSelectBtnItem = btnItem;
		AcOpenServerManager.Instance.SendGetOpenServerActInfoReq(btnItem.ActivityTypeID);
		this.UpdateUIByType(btnItem.ActivityTypeID);
		this.UpdateMyTargetInfo(btnItem.ActivityTypeID);
		this.UpdateActivityCountDown(btnItem.ActivityTypeID);
		this.UpdateUpWayListByType(btnItem.ActivityTypeID);
	}

	private void OnClickHelpBtn(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 513240, 513239);
	}

	private void OnClickRefreshBtn(GameObject go)
	{
		if (this.currentSelectBtnItem != null)
		{
			AcOpenServerManager.Instance.SendGetOpenServerActInfoReq(this.currentSelectBtnItem.ActivityTypeID);
		}
	}

	private void OnClickLinkToSystem(GameObject go)
	{
		int systemId = int.Parse(go.get_name());
		if (SystemOpenManager.IsSystemClickOpen(systemId, 0, true))
		{
			LinkNavigationManager.SystemLink(systemId, true, null);
			this.Show(false);
		}
	}

	private void RefreshUI()
	{
		List<KaiFuHuoDong> activityTypeCfgList = AcOpenServerManager.Instance.GetCanShowActivityTypes();
		if (activityTypeCfgList != null && activityTypeCfgList.get_Count() > 0)
		{
			int tabIndex = AcOpenServerManager.Instance.GetSelectBtnTab();
			tabIndex = ((tabIndex >= 0 && tabIndex < activityTypeCfgList.get_Count()) ? tabIndex : 0);
			this.activityTypeListPool.Create(activityTypeCfgList.get_Count(), delegate(int index)
			{
				if (index < this.activityTypeListPool.Items.get_Count() && index < activityTypeCfgList.get_Count())
				{
					AcOpenServerBtnItem component = this.activityTypeListPool.Items.get_Item(index).GetComponent<AcOpenServerBtnItem>();
					if (component != null)
					{
						component.UpdateUI(activityTypeCfgList.get_Item(index));
						component.Selected = false;
						this.activityTypeListPool.Items.get_Item(index).GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickActivityTypeItemBtn);
						if (this.currentSelectBtnItem == null)
						{
							if (tabIndex == index)
							{
								component.Selected = true;
								this.UpdateSelectBtnItem(component);
							}
						}
						else if (this.currentSelectBtnItem != null && index == this.currentSelectBtnItem.ActivityTypeID - 1)
						{
							this.UpdateSelectBtnItem(this.currentSelectBtnItem);
						}
					}
				}
			});
		}
	}

	private void UpdateMyTargetInfo(int type)
	{
		ResourceManager.SetTexture(this.leftPictureImg, AcOpenServerManager.Instance.GetAcTypeLeftPictureName(type));
		this.myRankingText.set_text((AcOpenServerManager.Instance.MyRoleRankNum <= 0) ? "未上榜" : string.Format("第{0}名", AcOpenServerManager.Instance.MyRoleRankNum));
		this.myLevelTextName.set_text(AcOpenServerManager.Instance.GetAcOpenServerRightText(type));
		this.myLevelText.set_text(AcOpenServerManager.Instance.MyRoleScore + string.Empty);
	}

	private void UpdateUIByType(int type)
	{
		this.taskListPool.Clear();
		List<KaiFuPaiMing> activityTaskList = AcOpenServerManager.Instance.GetActivityTaskListByType(type);
		if (activityTaskList != null && activityTaskList.get_Count() > 0)
		{
			this.taskListPool.Create(activityTaskList.get_Count(), delegate(int index)
			{
				AcOpenServerItem component = this.taskListPool.Items.get_Item(index).GetComponent<AcOpenServerItem>();
				if (component != null)
				{
					component.UpdateUI(activityTaskList.get_Item(index));
				}
			});
		}
	}

	private void UpdateActivityCountDown(int type)
	{
		this.RemoveActivityCountDown();
		this.remainTimeCDText.set_text(string.Empty);
		int remainActivityTime = AcOpenServerManager.Instance.GetRemainActivityTime(type);
		if (remainActivityTime <= 0)
		{
			this.remainTimeCDText.set_text("<color=#ff0000>已结束</color>");
		}
		else
		{
			this.activityRemainCD = new TimeCountDown(remainActivityTime, TimeFormat.HHMMSS, delegate
			{
				if (this.activityRemainCD != null)
				{
					this.remainTimeCDText.set_text("<color=#FFEBD2>" + this.activityRemainCD.GetTime() + "</color>");
				}
			}, delegate
			{
				this.remainTimeCDText.set_text("<color=#ff0000>已结束</color>");
			}, true);
		}
	}

	private void RemoveActivityCountDown()
	{
		if (this.activityRemainCD != null)
		{
			this.activityRemainCD.Dispose();
			this.activityRemainCD = null;
		}
	}

	private void UpdateUpWayListByType(int type)
	{
		List<int> systemIDs = AcOpenServerManager.Instance.GetUpWayListByType(type);
		this.upWayListPool.Clear();
		this.upWayListPool.Create(systemIDs.get_Count(), delegate(int index)
		{
			if (index < systemIDs.get_Count() && index < this.upWayListPool.Items.get_Count())
			{
				Image component = this.upWayListPool.Items.get_Item(index).get_transform().FindChild("SystemImg").GetComponent<Image>();
				Image component2 = this.upWayListPool.Items.get_Item(index).get_transform().FindChild("SystemWordImg").GetComponent<Image>();
				int num = systemIDs.get_Item(index);
				if (DataReader<SystemOpen>.Contains(num))
				{
					ResourceManager.SetSprite(component, GameDataUtils.GetIcon(DataReader<SystemOpen>.Get(num).icon));
					ResourceManager.SetSprite(component2, GameDataUtils.GetIcon(DataReader<SystemOpen>.Get(num).icon2));
					component2.SetNativeSize();
				}
				this.upWayListPool.Items.get_Item(index).set_name(num + string.Empty);
				this.upWayListPool.Items.get_Item(index).GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLinkToSystem);
			}
		});
	}

	private void OnGetOpenServerActInfoRes()
	{
		if (this.currentSelectBtnItem != null && AcOpenServerManager.Instance.ActivityTargetTaskDic != null && AcOpenServerManager.Instance.ActivityTargetTaskDic.ContainsKey(this.currentSelectBtnItem.ActivityTypeID))
		{
			this.UpdateUIByType(this.currentSelectBtnItem.ActivityTypeID);
			this.UpdateMyTargetInfo(this.currentSelectBtnItem.ActivityTypeID);
		}
	}

	private void OnOpenServerActStatusNty()
	{
		this.RefreshUI();
	}

	private void OnGetSumRechargeRes()
	{
		if (!AcOpenServerManager.Instance.IsOpenActivity)
		{
			this.Show(false);
			return;
		}
		if (this.currentSelectBtnItem != null)
		{
			this.UpdateMyTargetInfo(this.currentSelectBtnItem.ActivityTypeID);
		}
	}
}
