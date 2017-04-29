using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkTrialUI : UIBase
{
	protected ListPool chapterListPool;

	protected List<DarkTrialUIChapterUnit> chapterUnitList = new List<DarkTrialUIChapterUnit>();

	protected ListPool missionListPool;

	protected List<DarkTrialUIMissionUnit> missionUnitList = new List<DarkTrialUIMissionUnit>();

	protected Image DarkTrialUIMapNormal;

	protected Image DarkTrialUIMapHard;

	protected Image DarkTrialUIMapHero;

	protected RawImage DarkTrialUIMapBG;

	protected Text DarkTrialUIMapRankText;

	protected Image DarkTrialUIMapRankImage;

	protected Transform DarkTrialUIRewardSlot;

	protected Text DarkTrialUIRemainTimeText;

	protected Text DarkTrialUIMatchTeamBtnImageName;

	protected int curChapter;

	protected int curMission;

	protected int curInstance;

	protected int CurChapter
	{
		get
		{
			return this.curChapter;
		}
		set
		{
			this.curChapter = value;
		}
	}

	protected int CurMission
	{
		get
		{
			return this.curMission;
		}
		set
		{
			this.curMission = value;
		}
	}

	protected int CurInstance
	{
		get
		{
			return this.curInstance;
		}
		set
		{
			this.curInstance = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.chapterListPool = base.FindTransform("DarkTrialUIChapterUnitList").GetComponent<ListPool>();
		this.chapterListPool.SetItem("DarkTrialUIChapterUnit");
		this.missionListPool = base.FindTransform("DarkTrialUIMissionUnitList").GetComponent<ListPool>();
		this.missionListPool.SetItem("DarkTrialUIMissionUnit");
		this.DarkTrialUIMapNormal = base.FindTransform("DarkTrialUIMapNormal").GetComponent<Image>();
		this.DarkTrialUIMapHard = base.FindTransform("DarkTrialUIMapHard").GetComponent<Image>();
		this.DarkTrialUIMapHero = base.FindTransform("DarkTrialUIMapHero").GetComponent<Image>();
		this.DarkTrialUIMapBG = base.FindTransform("DarkTrialUIMapBG").GetComponent<RawImage>();
		this.DarkTrialUIMapRankText = base.FindTransform("DarkTrialUIMapRankText").GetComponent<Text>();
		this.DarkTrialUIMapRankImage = base.FindTransform("DarkTrialUIMapRankImage").GetComponent<Image>();
		this.DarkTrialUIRewardSlot = base.FindTransform("DarkTrialUIRewardTextSlot");
		this.DarkTrialUIRemainTimeText = base.FindTransform("DarkTrialUIRemainTimeText").GetComponent<Text>();
		this.DarkTrialUIMatchTeamBtnImageName = base.FindTransform("DarkTrialUIMatchTeamBtnImageName").GetComponent<Text>();
		base.FindTransform("DarkTrialUIMapNormalText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50718, false));
		base.FindTransform("DarkTrialUIMapHardText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50719, false));
		base.FindTransform("DarkTrialUIMapHeroText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50720, false));
		base.FindTransform("DarkTrialUIStartBtnImageName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50717, false));
		this.DarkTrialUIMapNormal.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickNormalBtn);
		this.DarkTrialUIMapHard.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHardBtn);
		this.DarkTrialUIMapHero.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHeroBtn);
		base.FindTransform("DarkTrialUIMatchTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMatchBtn);
		base.FindTransform("DarkTrialUIStartBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStartBtn);
		base.FindTransform("DarkTrialUIDescBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDescBtn);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		base.hideMainCamera = true;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.AddListener(EventNames.CreateTeamSuccess, new Callback(this.UpdateBtn));
		EventDispatcher.AddListener(EventNames.UpdateTeamBasicInfo, new Callback(this.UpdateBtn));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.UpdateBtn));
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110037), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.MultiPve, base.get_transform(), 0);
		}
		this.UpdateRemainTimes();
		this.UpdateBtn();
		WaitUI.OpenUI(5000u);
		this.InitChapter(delegate
		{
			this.OnClickChapterBtn(0);
		});
	}

	protected override void OnDisable()
	{
		EventDispatcher.RemoveListener(EventNames.CreateTeamSuccess, new Callback(this.UpdateBtn));
		EventDispatcher.RemoveListener(EventNames.UpdateTeamBasicInfo, new Callback(this.UpdateBtn));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.UpdateBtn));
		base.OnDisable();
	}

	protected void OnClickDescBtn(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 50703, 50701);
	}

	protected void OnClickChapterBtn(int index)
	{
		this.SetChapter(index);
		WaitUI.OpenUI(5000u);
		this.InitMission(delegate
		{
			WaitUI.CloseUI(0u);
			this.OnClickMissionBtn(0);
		});
	}

	protected void OnClickMissionBtn(int index)
	{
		this.SetMission(index);
		this.OnClickNormalBtn(null);
	}

	protected void OnClickNormalBtn(GameObject go)
	{
		this.SetInstance(0);
	}

	protected void OnClickHardBtn(GameObject go)
	{
		this.SetInstance(1);
	}

	protected void OnClickHeroBtn(GameObject go)
	{
		this.SetInstance(2);
	}

	protected void OnClickMatchBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			DarkTrialManager.Instance.SeekTeam(this.CurInstance);
		}
		else
		{
			DarkTrialManager.Instance.ShowMyTeam(this.CurInstance);
		}
	}

	protected void OnClickStartBtn(GameObject go)
	{
		DarkTrialManager.Instance.StartDarkTrial(this.CurInstance);
	}

	public void UpdateData()
	{
		this.SetInstance(this.CurInstance);
	}

	protected void UpdateRemainTimes()
	{
		this.DarkTrialUIRemainTimeText.set_text(string.Format(GameDataUtils.GetChineseContent(50714, false), DarkTrialManager.Instance.RemainTimes, DarkTrialManager.Instance.MaxTimes));
	}

	protected void UpdateBtn()
	{
		this.DarkTrialUIMatchTeamBtnImageName.set_text(GameDataUtils.GetChineseContent((TeamBasicManager.Instance.MyTeamData != null) ? 50715 : 50716, false));
	}

	public void InitChapter(Action callback = null)
	{
		List<DarkChapter> dataList = DataReader<DarkChapter>.DataList;
		this.chapterUnitList.Clear();
		this.chapterListPool.Create(dataList.get_Count(), delegate(int index)
		{
			if (index < dataList.get_Count() && index < this.chapterListPool.Items.get_Count())
			{
				DarkTrialUIChapterUnit component = this.chapterListPool.Items.get_Item(index).GetComponent<DarkTrialUIChapterUnit>();
				component.SetData(index, dataList.get_Item(index).Name, new Action<int>(this.OnClickChapterBtn));
				this.chapterUnitList.Add(component);
			}
			if (index == dataList.get_Count() - 1 && callback != null)
			{
				callback.Invoke();
			}
		});
	}

	protected void SetChapter(int index)
	{
		this.CurChapter = index + 1;
		for (int i = 0; i < this.chapterUnitList.get_Count(); i++)
		{
			this.chapterUnitList.get_Item(i).SetClickState(i == index);
		}
	}

	protected void InitMission(Action callback = null)
	{
		if (!DataReader<DarkChapter>.Contains(this.CurChapter))
		{
			return;
		}
		DarkChapter darkChapter = DataReader<DarkChapter>.Get(this.CurChapter);
		List<DarkGroup> dataList = new List<DarkGroup>();
		for (int i = 0; i < darkChapter.MissionGroup.get_Count(); i++)
		{
			if (DataReader<DarkGroup>.Contains(darkChapter.MissionGroup.get_Item(i)))
			{
				dataList.Add(DataReader<DarkGroup>.Get(darkChapter.MissionGroup.get_Item(i)));
			}
		}
		if (dataList.get_Count() == 0)
		{
			return;
		}
		this.missionUnitList.Clear();
		this.missionListPool.Create(dataList.get_Count(), delegate(int index)
		{
			if (index < dataList.get_Count() && index < this.missionListPool.Items.get_Count())
			{
				DarkTrialUIMissionUnit component = this.missionListPool.Items.get_Item(index).GetComponent<DarkTrialUIMissionUnit>();
				component.SetData(index, dataList.get_Item(index).Icon, dataList.get_Item(index).Name, dataList.get_Item(index).Lv, new Action<int>(this.OnClickMissionBtn));
				this.missionUnitList.Add(component);
			}
			if (index == dataList.get_Count() - 1 && callback != null)
			{
				callback.Invoke();
			}
		});
	}

	protected void SetMission(int index)
	{
		if (!DataReader<DarkChapter>.Contains(this.CurChapter))
		{
			return;
		}
		DarkChapter darkChapter = DataReader<DarkChapter>.Get(this.CurChapter);
		int num = (index >= 0) ? ((index <= darkChapter.MissionGroup.get_Count() - 1) ? index : (darkChapter.MissionGroup.get_Count() - 1)) : 0;
		this.CurMission = darkChapter.MissionGroup.get_Item(num);
		for (int i = 0; i < this.missionUnitList.get_Count(); i++)
		{
			this.missionUnitList.get_Item(i).SetClickState(i == index);
		}
	}

	protected void SetInstance(int index)
	{
		if (!DataReader<DarkChapter>.Contains(this.CurChapter))
		{
			return;
		}
		DarkChapter darkChapter = DataReader<DarkChapter>.Get(this.CurChapter);
		if (!DataReader<DarkGroup>.Contains(this.CurMission))
		{
			return;
		}
		DarkGroup darkGroup = DataReader<DarkGroup>.Get(this.CurMission);
		int num = (index >= 0) ? ((index <= darkGroup.FubenId.get_Count() - 1) ? index : (darkGroup.FubenId.get_Count() - 1)) : 0;
		this.CurInstance = darkGroup.FubenId.get_Item(num);
		ResourceManager.SetSprite(this.DarkTrialUIMapNormal, ResourceManager.GetIconSprite((index != 0) ? "y_fenye2" : "y_fenye1"));
		ResourceManager.SetSprite(this.DarkTrialUIMapHard, ResourceManager.GetIconSprite((index != 1) ? "y_fenye2" : "y_fenye1"));
		ResourceManager.SetSprite(this.DarkTrialUIMapHero, ResourceManager.GetIconSprite((index != 2) ? "y_fenye2" : "y_fenye1"));
		if (!DataReader<DarkFuben>.Contains(this.CurInstance))
		{
			return;
		}
		DarkFuben darkFuben = DataReader<DarkFuben>.Get(this.CurInstance);
		this.UpdateMap(darkFuben.mapID, darkFuben.coordinate);
		this.UpdateRank(darkFuben.FubenId);
		this.UpdateReward(darkFuben.rewardID);
	}

	protected void UpdateMap(int iconID, List<int> coordinate)
	{
		ResourceManager.SetTexture(this.DarkTrialUIMapBG, GameDataUtils.GetIconName(iconID));
		if (coordinate.get_Count() > 1)
		{
			this.DarkTrialUIMapBG.get_transform().set_localPosition(new Vector3((float)coordinate.get_Item(0), (float)coordinate.get_Item(1), this.DarkTrialUIMapBG.get_transform().get_localPosition().z));
		}
	}

	protected void UpdateRank(int instanceID)
	{
		int instanceRank = DarkTrialManager.Instance.GetInstanceRank(instanceID);
		if (instanceRank > 0)
		{
			this.DarkTrialUIMapRankText.set_text(GameDataUtils.GetChineseContent(50712, false));
			if (!this.DarkTrialUIMapRankImage.get_gameObject().get_activeSelf())
			{
				this.DarkTrialUIMapRankImage.get_gameObject().SetActive(true);
			}
			ResourceManager.SetSprite(this.DarkTrialUIMapRankImage, ResourceManager.GetIconSprite(this.GetRankImageName(instanceRank)));
		}
		else
		{
			this.DarkTrialUIMapRankText.set_text(GameDataUtils.GetChineseContent(50728, false));
			if (this.DarkTrialUIMapRankImage.get_gameObject().get_activeSelf())
			{
				this.DarkTrialUIMapRankImage.get_gameObject().SetActive(false);
			}
		}
	}

	protected string GetRankImageName(int rank)
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

	protected void UpdateReward(List<int> reward)
	{
		for (int i = 0; i < this.DarkTrialUIRewardSlot.get_childCount(); i++)
		{
			Object.Destroy(this.DarkTrialUIRewardSlot.GetChild(i).get_gameObject());
		}
		for (int j = 0; j < reward.get_Count(); j++)
		{
			ItemShow.ShowItem(this.DarkTrialUIRewardSlot, reward.get_Item(j), -1L, false, null, 2001);
		}
	}
}
