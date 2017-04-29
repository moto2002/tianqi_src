using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangFightUI : UIBase
{
	private Text Text1Num;

	private Text Text2Num;

	private Text Text3Num;

	private ButtonCustom BtnStart;

	private Text TextNumGold;

	private Text TextNumSilver;

	private Text TextNumCopper;

	private ButtonCustom BtnGangFightReport;

	private Text TextRow1;

	private Text TextRow2;

	private Text TextRow3;

	private Text Text1Unit;

	private Text Text2Unit;

	private Text Text3Unit;

	private Text TextGoldTitle;

	private Text TextSilverTitle;

	private Text TextCopperTitle;

	private Text TextTimeLessDes;

	private Text TextGoldName;

	private Text TextSilverName;

	private Text TextCopperName;

	private Image ImageHeadGold;

	private Image ImageHeadSilver;

	private Image ImageHeadCopper;

	private Text TextGoldLv;

	private Text TextSilverLv;

	private Text TextCopperLv;

	private Transform HavePersonGold;

	private Transform NoPersonGold;

	private Transform HavePersonSilver;

	private Transform NoPersonSilver;

	private Transform HavePersonCopper;

	private Transform NoPersonCopper;

	private TimeCountDown remainTimeCD;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.hideMainCamera = true;
		this.Text1Num = base.FindTransform("Text1Num").GetComponent<Text>();
		this.Text2Num = base.FindTransform("Text2Num").GetComponent<Text>();
		this.Text3Num = base.FindTransform("Text3Num").GetComponent<Text>();
		this.BtnStart = base.FindTransform("BtnStart").GetComponent<ButtonCustom>();
		this.TextNumGold = base.FindTransform("TextNumGold").GetComponent<Text>();
		this.TextNumSilver = base.FindTransform("TextNumSilver").GetComponent<Text>();
		this.TextNumCopper = base.FindTransform("TextNumCopper").GetComponent<Text>();
		this.BtnGangFightReport = base.FindTransform("BtnGangFightReport").GetComponent<ButtonCustom>();
		this.TextRow1 = base.FindTransform("TextRow1").GetComponent<Text>();
		this.TextRow2 = base.FindTransform("TextRow2").GetComponent<Text>();
		this.TextRow3 = base.FindTransform("TextRow3").GetComponent<Text>();
		this.Text1Unit = base.FindTransform("Text1Unit").GetComponent<Text>();
		this.Text2Unit = base.FindTransform("Text2Unit").GetComponent<Text>();
		this.Text3Unit = base.FindTransform("Text3Unit").GetComponent<Text>();
		this.TextGoldTitle = base.FindTransform("TextGoldTitle").GetComponent<Text>();
		this.TextSilverTitle = base.FindTransform("TextSilverTitle").GetComponent<Text>();
		this.TextCopperTitle = base.FindTransform("TextCopperTitle").GetComponent<Text>();
		this.TextTimeLessDes = base.FindTransform("TextTimeLessDes").GetComponent<Text>();
		this.TextGoldName = base.FindTransform("TextGoldName").GetComponent<Text>();
		this.TextSilverName = base.FindTransform("TextSilverName").GetComponent<Text>();
		this.TextCopperName = base.FindTransform("TextCopperName").GetComponent<Text>();
		this.ImageHeadGold = base.FindTransform("ImageHeadGold").GetComponent<Image>();
		this.ImageHeadSilver = base.FindTransform("ImageHeadSilver").GetComponent<Image>();
		this.ImageHeadCopper = base.FindTransform("ImageHeadCopper").GetComponent<Image>();
		this.TextGoldLv = base.FindTransform("TextGoldLv").GetComponent<Text>();
		this.TextSilverLv = base.FindTransform("TextSilverLv").GetComponent<Text>();
		this.TextCopperLv = base.FindTransform("TextCopperLv").GetComponent<Text>();
		this.HavePersonGold = base.FindTransform("HavePersonGold");
		this.NoPersonGold = base.FindTransform("NoPersonGold");
		this.HavePersonSilver = base.FindTransform("HavePersonSilver");
		this.NoPersonSilver = base.FindTransform("NoPersonSilver");
		this.HavePersonCopper = base.FindTransform("HavePersonCopper");
		this.NoPersonCopper = base.FindTransform("NoPersonCopper");
		this.BtnStart.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnStart);
		this.BtnGangFightReport.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGangFightReport);
		this.ResetChineseDataUI();
	}

	protected override void OnEnable()
	{
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI != null)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Gang, base.get_transform(), 0);
			changePetChooseUI.Show(true);
		}
		GangFightManager.Instance.QueryCombatWinRankingsInfo();
		GangFightManager.Instance.QueryFightRecordInfo();
		this.ResetGangFightPersonalInfo();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110016), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshGangFightActivityTime();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		this.RemoveRemainTimeCD();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetCancelGangFightingRes, new Callback(this.OnGetCancelGangFightingRes));
		EventDispatcher.AddListener(EventNames.OnGetGangFightPersonalInfo, new Callback(this.OnGetGangFightPersonalInfo));
		EventDispatcher.AddListener(EventNames.OnGetCombatWinRankingsInfo, new Callback(this.OnGetCombatWinRankingsInfo));
		EventDispatcher.AddListener(EventNames.CloseGangfight, new Callback(this.OnGangFightActivityClose));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetCancelGangFightingRes, new Callback(this.OnGetCancelGangFightingRes));
		EventDispatcher.RemoveListener(EventNames.OnGetGangFightPersonalInfo, new Callback(this.OnGetGangFightPersonalInfo));
		EventDispatcher.RemoveListener(EventNames.OnGetCombatWinRankingsInfo, new Callback(this.OnGetCombatWinRankingsInfo));
		EventDispatcher.RemoveListener(EventNames.CloseGangfight, new Callback(this.OnGangFightActivityClose));
	}

	private void OnGetCancelGangFightingRes()
	{
	}

	private void OnGetGangFightPersonalInfo()
	{
		this.ResetGangFightPersonalInfo();
	}

	private void OnGetCombatWinRankingsInfo()
	{
		this.ResetCombatWinRankingsInfo();
	}

	private void OnGangFightActivityClose()
	{
		this.RemoveRemainTimeCD();
		this.RefreshGangFightActivityTime();
	}

	private void OnClickBtnStart(GameObject sender)
	{
		if (GangFightManager.Instance.CheckGangFightIsOpen())
		{
			GangFightManager.Instance.SendStartGangFight();
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513526, false));
		}
	}

	private void OnClickBtnGangFightReport(GameObject sender)
	{
		BattleLogUI battleLogUI = UIManagerControl.Instance.OpenUI("BattleLogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BattleLogUI;
		battleLogUI.RefreshUI();
	}

	private void RefreshGangFightActivityTime()
	{
		if (GangFightManager.Instance.CheckGangFightIsOpen())
		{
			int num = (int)(GangFightManager.Instance.dateTimeClose - GangFightManager.Instance.severTime).get_TotalSeconds();
			this.SetRemainTimeCD(num);
		}
		else
		{
			this.SetGangFightOpenTime();
		}
	}

	private void SetRemainTimeCD(int remainTime)
	{
		this.RemoveRemainTimeCD();
		if (remainTime > 0)
		{
			this.remainTimeCD = new TimeCountDown(remainTime, TimeFormat.HHMMSS, delegate
			{
				string time = TimeConverter.GetTime(remainTime, TimeFormat.HHMMSS);
				if (this.remainTimeCD != null)
				{
					time = this.remainTimeCD.GetTime();
				}
				string text = GameDataUtils.GetChineseContent(510116, false);
				text = text.Replace("{s1}", time + GameDataUtils.GetChineseContent(509003, false));
				this.TextTimeLessDes.set_text(text);
			}, delegate
			{
				this.RemoveRemainTimeCD();
				this.SetGangFightOpenTime();
			}, true);
		}
		else
		{
			this.SetGangFightOpenTime();
		}
	}

	private void RemoveRemainTimeCD()
	{
		if (this.remainTimeCD != null)
		{
			this.remainTimeCD.Dispose();
			this.remainTimeCD = null;
		}
	}

	private void SetGangFightOpenTime()
	{
		string text = GameDataUtils.GetChineseContent(510115, false);
		text = text.Replace("{s1}", GangFightManager.Instance.openTime + "-" + GangFightManager.Instance.closeTime);
		this.TextTimeLessDes.set_text(text);
	}

	private void ResetChineseDataUI()
	{
		this.TextRow1.set_text(GameDataUtils.GetChineseContent(505004, false));
		this.TextRow2.set_text(GameDataUtils.GetChineseContent(505005, false));
		this.TextRow3.set_text(GameDataUtils.GetChineseContent(505006, false));
		this.Text1Unit.set_text(GameDataUtils.GetChineseContent(505008, false));
		this.Text2Unit.set_text(GameDataUtils.GetChineseContent(505008, false));
		this.Text3Unit.set_text(GameDataUtils.GetChineseContent(505008, false));
		this.TextGoldTitle.set_text(GameDataUtils.GetChineseContent(505014, false));
		this.TextSilverTitle.set_text(GameDataUtils.GetChineseContent(505014, false));
		this.TextCopperTitle.set_text(GameDataUtils.GetChineseContent(505014, false));
		this.TextTimeLessDes.set_text(GameDataUtils.GetChineseContent(505007, false));
	}

	private void ResetGangFightPersonalInfo()
	{
		this.Text1Num.set_text(GangFightManager.Instance.historyCombatWin.ToString());
		this.Text2Num.set_text(GangFightManager.Instance.topCombatWin.ToString());
		this.Text3Num.set_text(GangFightManager.Instance.totalWin.ToString());
	}

	private void ResetCombatWinRankingsInfo()
	{
		ResourceManager.SetSprite(this.ImageHeadGold, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.ImageHeadSilver, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.ImageHeadCopper, ResourceManagerBase.GetNullSprite());
		List<CombatWinRankingsItem> combatWinRankingsItems = GangFightManager.Instance.CombatWinRankingsItems;
		for (int i = 0; i < combatWinRankingsItems.get_Count(); i++)
		{
			CombatWinRankingsItem combatWinRankingsItem = combatWinRankingsItems.get_Item(i);
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(combatWinRankingsItem.modelId);
			if (i == 0)
			{
				this.TextGoldName.set_text(combatWinRankingsItem.name);
				this.TextNumGold.set_text(combatWinRankingsItem.winCount.ToString() + GameDataUtils.GetChineseContent(510108, false));
				this.TextGoldLv.set_text("Lv." + combatWinRankingsItem.lv);
				ResourceManager.SetSprite(this.ImageHeadGold, GameDataUtils.GetIcon(avatarModel.pic));
				this.HavePersonGold.get_gameObject().SetActive(true);
				this.NoPersonGold.get_gameObject().SetActive(false);
			}
			else if (i == 1)
			{
				this.TextSilverName.set_text(combatWinRankingsItem.name);
				this.TextNumSilver.set_text(combatWinRankingsItem.winCount.ToString() + GameDataUtils.GetChineseContent(510108, false));
				this.TextSilverLv.set_text("Lv." + combatWinRankingsItem.lv);
				ResourceManager.SetSprite(this.ImageHeadSilver, GameDataUtils.GetIcon(avatarModel.pic));
				this.HavePersonSilver.get_gameObject().SetActive(true);
				this.NoPersonSilver.get_gameObject().SetActive(false);
			}
			else if (i == 2)
			{
				this.TextCopperName.set_text(combatWinRankingsItem.name);
				this.TextNumCopper.set_text(combatWinRankingsItem.winCount.ToString() + GameDataUtils.GetChineseContent(510108, false));
				this.TextCopperLv.set_text("Lv." + combatWinRankingsItem.lv);
				ResourceManager.SetSprite(this.ImageHeadCopper, GameDataUtils.GetIcon(avatarModel.pic));
				this.HavePersonCopper.get_gameObject().SetActive(true);
				this.NoPersonCopper.get_gameObject().SetActive(false);
			}
		}
	}
}
