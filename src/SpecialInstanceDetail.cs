using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceDetail : BaseUIBehaviour
{
	protected List<Transform> DifficultyBtn = new List<Transform>();

	protected Transform DifficultyList;

	protected Text TitleText;

	protected RawImage InstanceRawImage;

	protected Transform DropInfoItem;

	protected int selectFxUID;

	protected Text DescribtionContent;

	protected Text InstanceTimesNum;

	protected Text PowerText;

	protected ButtonCustom BuyTimeBtn;

	protected ButtonCustom StartBtn;

	protected Image BuffIcon;

	protected ButtonCustom BuffFrame;

	protected Text TextBuffName;

	protected GameObject BuffNoIcon;

	protected GameObject BuffObj;

	protected SpecialFightMode currentMode;

	protected SpecialFightModeGroup currentGroup;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.DifficultyList = base.FindTransform("Difficulty");
		this.TitleText = base.FindTransform("TitleText").GetComponent<Text>();
		this.InstanceRawImage = base.FindTransform("InstanceRawImage").GetComponent<RawImage>();
		this.DropInfoItem = base.FindTransform("DropInfoItem");
		this.DescribtionContent = base.FindTransform("DescribtionContent").GetComponent<Text>();
		this.InstanceTimesNum = base.FindTransform("InstanceTimesNum").GetComponent<Text>();
		this.PowerText = base.FindTransform("PowerText").GetComponent<Text>();
		this.BuyTimeBtn = base.FindTransform("BuyTimeBtn").GetComponent<ButtonCustom>();
		this.StartBtn = base.FindTransform("StartBtn").GetComponent<ButtonCustom>();
		this.BuffObj = base.FindTransform("Buff").get_gameObject();
		this.BuffIcon = base.FindTransform("BuffIcon").GetComponent<Image>();
		this.BuffFrame = base.FindTransform("BuffFrame").GetComponent<ButtonCustom>();
		this.TextBuffName = base.FindTransform("TextBuffName").GetComponent<Text>();
		this.BuffNoIcon = base.FindTransform("BuffNoIcon").get_gameObject();
		base.FindTransform("TextBuffTips").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502323, false));
	}

	private void Start()
	{
		this.BuyTimeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuyTimes);
		this.StartBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStart);
		this.BuffFrame.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuff);
	}

	private void OnEnable()
	{
		this.OnUpdateSpecialInstanceDetailUI();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateSpecialInstanceDetailUI, new Callback(this.OnUpdateSpecialInstanceDetailUI));
		EventDispatcher.AddListener<bool>(EventNames.TeamAotoMatchSpecial, new Callback<bool>(this.OnTeamAotoMatch));
		EventDispatcher.AddListener<bool>(EventNames.TeamAotoMatchError, new Callback<bool>(this.OnTeamAotoMatchError));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateSpecialInstanceDetailUI, new Callback(this.OnUpdateSpecialInstanceDetailUI));
		EventDispatcher.RemoveListener<bool>(EventNames.TeamAotoMatchSpecial, new Callback<bool>(this.OnTeamAotoMatch));
		EventDispatcher.RemoveListener<bool>(EventNames.TeamAotoMatchError, new Callback<bool>(this.OnTeamAotoMatchError));
	}

	public void SetInit(SpecialFightMode mode)
	{
		this.currentMode = mode;
		this.currentGroup = SpecialFightManager.GetModeCroup(mode);
		this.SetTitleText(this.currentMode);
		SpecialFightModeGroup specialFightModeGroup = this.currentGroup;
		if (specialFightModeGroup != SpecialFightModeGroup.Defend)
		{
			if (specialFightModeGroup != SpecialFightModeGroup.Expericence)
			{
			}
		}
		else
		{
			this.InitDifficulty(mode);
		}
		this.OnUpdateSpecialInstanceDetailUI();
	}

	protected void InitDifficulty(SpecialFightMode mode)
	{
		DefendFightModeInfo defendFightModeInfo = SpecialFightManager.GetSpecialFightInfo(mode) as DefendFightModeInfo;
		this.DifficultyList.get_gameObject().SetActive(false);
		this.BuffObj.SetActive(false);
		this.OnUpdateDetailUI();
		this.InstanceTimesNum.set_text(defendFightModeInfo.todayCanChallengeTimes.ToString());
		this.BuyTimeBtn.get_gameObject().SetActive(defendFightModeInfo.todayCanChallengeTimes == 0);
	}

	protected void InitBuff(SpecialFightMode mode)
	{
		this.DifficultyList.get_gameObject().SetActive(false);
		this.BuffObj.SetActive(true);
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(this.currentMode) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return;
		}
		int buffId = 0;
		if (specialFightInfo.m_BuffIds != null && specialFightInfo.m_BuffIds.get_Count() > 0)
		{
			buffId = specialFightInfo.m_BuffIds.get_Item(0);
		}
		this.OnUpdateBuff(buffId);
	}

	protected void SetTitleText(SpecialFightMode mode)
	{
		switch (mode)
		{
		case SpecialFightMode.Hold:
			this.TitleText.set_text(GameDataUtils.GetChineseContent(513507, false));
			break;
		case SpecialFightMode.Protect:
			this.TitleText.set_text(GameDataUtils.GetChineseContent(513505, false));
			break;
		case SpecialFightMode.Save:
			this.TitleText.set_text(GameDataUtils.GetChineseContent(513506, false));
			break;
		case SpecialFightMode.Expericence:
			this.TitleText.set_text(GameDataUtils.GetChineseContent(502322, false));
			break;
		}
	}

	protected string SetNameText(SpecialFightMode mode, int difficultyIndex, int difficultyLevel)
	{
		switch (mode)
		{
		case SpecialFightMode.Hold:
		case SpecialFightMode.Protect:
			return string.Format(GameDataUtils.GetChineseContent(513500, false), difficultyLevel);
		case SpecialFightMode.Save:
			switch (difficultyIndex)
			{
			case 0:
				return GameDataUtils.GetChineseContent(513501, false);
			case 1:
				return GameDataUtils.GetChineseContent(513502, false);
			case 2:
				return GameDataUtils.GetChineseContent(513503, false);
			}
			break;
		}
		return string.Empty;
	}

	protected void OnUpdateExpericenceInstanceDetailUI()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(this.currentMode) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return;
		}
		this.OnUpdateDetailUI();
		this.InstanceTimesNum.set_text(specialFightInfo.m_RestTimes.ToString());
		this.BuyTimeBtn.get_gameObject().SetActive(specialFightInfo.m_RestTimes == 0);
		int buffId = 0;
		if (specialFightInfo.m_BuffIds != null && specialFightInfo.m_BuffIds.get_Count() > 0)
		{
			buffId = specialFightInfo.m_BuffIds.get_Item(0);
		}
		this.OnUpdateBuff(buffId);
	}

	protected void OnUpdateBuff(int buffId)
	{
		if (!DataReader<FZengYibuffPeiZhi>.Contains(buffId))
		{
			this.BuffNoIcon.SetActive(true);
			this.BuffIcon.get_gameObject().SetActive(false);
			this.TextBuffName.set_text(GameDataUtils.GetChineseContent(502329, false));
		}
		else
		{
			FZengYibuffPeiZhi fZengYibuffPeiZhi = DataReader<FZengYibuffPeiZhi>.Get(buffId);
			this.BuffNoIcon.SetActive(false);
			this.BuffIcon.get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.BuffIcon, GameDataUtils.GetIcon(fZengYibuffPeiZhi.icon));
			this.TextBuffName.set_text(string.Empty);
		}
	}

	protected void OnUpdateDetailUI()
	{
		SpecialFightCommonTableData specialFightCommonTableData = SpecialFightManager.GetSpecialFightCommonTableData(this.currentMode);
		if (specialFightCommonTableData == null)
		{
			return;
		}
		Icon icon = DataReader<Icon>.Get(specialFightCommonTableData.picture);
		if (icon != null)
		{
			ResourceManager.SetTexture(this.InstanceRawImage, icon.icon);
		}
		for (int i = 0; i < this.DropInfoItem.get_childCount(); i++)
		{
			Object.Destroy(this.DropInfoItem.GetChild(i).get_gameObject());
		}
		for (int j = 0; j < specialFightCommonTableData.itemIDs.get_Count(); j++)
		{
			ItemShow.ShowItem(this.DropInfoItem, specialFightCommonTableData.itemIDs.get_Item(j), (long)specialFightCommonTableData.itemNums.get_Item(j), false, null, 2001);
		}
		this.DescribtionContent.set_text(GameDataUtils.GetChineseContent(specialFightCommonTableData.descID, false));
	}

	protected void OnUpdateSpecialInstanceDetailUI()
	{
		SpecialFightModeGroup specialFightModeGroup = this.currentGroup;
		if (specialFightModeGroup != SpecialFightModeGroup.Defend)
		{
			if (specialFightModeGroup == SpecialFightModeGroup.Expericence)
			{
				this.OnUpdateExpericenceInstanceDetailUI();
			}
		}
		else
		{
			this.InitDifficulty(this.currentMode);
		}
	}

	protected void OnClickBuyTimes(GameObject go)
	{
		SpecialFightModeGroup specialFightModeGroup = this.currentGroup;
		if (specialFightModeGroup != SpecialFightModeGroup.Defend)
		{
			if (specialFightModeGroup == SpecialFightModeGroup.Expericence)
			{
				this.OnBuyExperienceTimes();
			}
		}
		else
		{
			this.OnBuyDefendTimes();
		}
	}

	protected void OnBuyDefendTimes()
	{
		DefendFightManager.Instance.OnBuyDefendTimes(this.currentMode);
	}

	protected void OnBuyExperienceTimes()
	{
		SpecialFightManager.Instance.OnBuyExperienceTimes();
	}

	protected void OnClickStart(GameObject go)
	{
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		SpecialFightModeGroup specialFightModeGroup = this.currentGroup;
		if (specialFightModeGroup != SpecialFightModeGroup.Defend)
		{
			if (specialFightModeGroup == SpecialFightModeGroup.Expericence)
			{
				this.StartExperienceFight();
			}
		}
		else
		{
			this.StartDefendFight();
		}
	}

	protected void StartDefendFight()
	{
		DefendFightModeInfo modeInfo = DefendFightManager.Instance.GetModeInfo(DefendFightManager.Instance.SelectDetailMode);
		if (modeInfo == null)
		{
			return;
		}
		if (modeInfo.todayCanChallengeTimes <= 0)
		{
			this.OnBuyDefendTimes();
		}
		else
		{
			DefendFightManager.Instance.StartFight();
		}
	}

	protected void StartExperienceFight()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(this.currentMode) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return;
		}
		if (specialFightInfo.m_RestTimes <= 0)
		{
			this.OnBuyExperienceTimes();
		}
		else
		{
			SpecialFightManager.Instance.StartExperienceFight();
		}
	}

	protected void OnTeamAotoMatch(bool isQuick)
	{
		if (!isQuick)
		{
			TeamManager.Instance.OpenQuickMatchUI();
		}
	}

	protected void OnTeamAotoMatchError(bool isQuick)
	{
		TeamManager.Instance.CloseMatchUI();
		UIManagerControl.Instance.ShowToastText("匹配失败", 2f, 2f);
	}

	protected void OnClickBuff(GameObject go)
	{
		SpecialInstanceBuffUI specialInstanceBuffUI = UIManagerControl.Instance.OpenUI("SpecialInstanceBuffUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as SpecialInstanceBuffUI;
		specialInstanceBuffUI.SetModeInit(this.currentMode);
	}
}
