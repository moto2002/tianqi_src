using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffHookUI : UIBase
{
	public enum Mode
	{
		BeginRegion,
		ResultRegion
	}

	public static OffHookUI Instance;

	private ButtonCustom btnShopBuy;

	private ButtonCustom btnEnsure;

	private Text textBeginTitle;

	private Text textBeginEexplain;

	private Text textBeginEexplainInfo;

	private Text textOffTime;

	private Text textLvStr;

	private Text textNeedExpStr;

	private Text textHourExpStr;

	private Text textPowerExpStr;

	private Text textLvValue;

	private Text textNeedExpValue;

	private Text textHourExpValue;

	private Text textPowerExpValue;

	private Text textResultTitle;

	private Text textBtnEnsureName;

	private Text textResultEexplain;

	private Text textResultEexplainInfo;

	private Text textOffTimeStr;

	private Text textLeftTimeStr;

	private Text textGetExpStr;

	private Text textLvUpStr;

	private Text textOffTimeValue;

	private Text textLeftTimeValue;

	private Text textGetExpValue;

	private Text textLvUpValue1;

	private Text textLvUpValue2;

	private Text textEffectContent;

	private uint t;

	private List<string> infoData = new List<string>();

	private OffHookUI.Mode m_Mode;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		OffHookUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.btnEnsure = base.FindTransform("BtnEnsure").GetComponent<ButtonCustom>();
		this.btnEnsure.set_enabled(true);
		this.btnEnsure.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEnsure);
		this.textResultTitle = base.FindTransform("TextResultTitle").GetComponent<Text>();
		this.textBtnEnsureName = base.FindTransform("TextBtnEnsureName").GetComponent<Text>();
		this.textResultEexplain = base.FindTransform("TextResultEexplain").GetComponent<Text>();
		this.textResultEexplainInfo = base.FindTransform("TextResultEexplainInfo").GetComponent<Text>();
		this.textOffTimeStr = base.FindTransform("TextOffTimeStr").GetComponent<Text>();
		this.textLeftTimeStr = base.FindTransform("TextLeftTimeStr").GetComponent<Text>();
		this.textGetExpStr = base.FindTransform("TextGetExpStr").GetComponent<Text>();
		this.textLvUpStr = base.FindTransform("TextLvUpStr").GetComponent<Text>();
		this.textOffTimeValue = base.FindTransform("TextOffTimeValue").GetComponent<Text>();
		this.textLeftTimeValue = base.FindTransform("TextLeftTimeValue").GetComponent<Text>();
		this.textGetExpValue = base.FindTransform("TextGetExpValue").GetComponent<Text>();
		this.textLvUpValue1 = base.FindTransform("TextLvUpValue1").GetComponent<Text>();
		this.textLvUpValue2 = base.FindTransform("TextLvUpValue2").GetComponent<Text>();
		this.textEffectContent = base.FindTransform("EffectContent").GetComponent<Text>();
		base.FindTransform("CloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEnsure);
		this.InitRegionInfo();
		this.StrMove();
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			OffHookUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		TimerHeap.DelTimer(this.t);
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnDisable()
	{
		TimerHeap.DelTimer(this.t);
		base.OnDisable();
	}

	private void OnClickShopBuy(GameObject go)
	{
		XMarketManager.Instance.OpenShop(1);
	}

	private void OnClickBtnEnsure(GameObject go)
	{
		this.OnClickMaskAction();
	}

	public void SwitchMode(OffHookUI.Mode mode)
	{
		this.m_Mode = mode;
		bool flag = false;
		OffHookUI.Mode mode2 = this.m_Mode;
		if (mode2 != OffHookUI.Mode.BeginRegion)
		{
			if (mode2 != OffHookUI.Mode.ResultRegion)
			{
			}
		}
		else
		{
			flag = true;
		}
		base.FindTransform("BeginRegion").get_gameObject().SetActive(flag);
		base.FindTransform("ResultRegion").get_gameObject().SetActive(!flag);
	}

	private void InitRegionInfo()
	{
		this.textBeginTitle = base.FindTransform("TextBeginTitle").GetComponent<Text>();
		this.textBeginEexplain = base.FindTransform("TextBeginEexplain").GetComponent<Text>();
		this.textBeginEexplainInfo = base.FindTransform("TextBeginEexplainInfo").GetComponent<Text>();
		this.textOffTime = base.FindTransform("TextOffTime").GetComponent<Text>();
		this.textLvStr = base.FindTransform("TextLvStr").GetComponent<Text>();
		this.textNeedExpStr = base.FindTransform("TextNeedExpStr").GetComponent<Text>();
		this.textHourExpStr = base.FindTransform("TextHourExpStr").GetComponent<Text>();
		this.textPowerExpStr = base.FindTransform("TextPowerExpStr").GetComponent<Text>();
		this.textLvValue = base.FindTransform("TextLvValue").GetComponent<Text>();
		this.textNeedExpValue = base.FindTransform("TextNeedExpValue").GetComponent<Text>();
		this.textHourExpValue = base.FindTransform("TextHourExpValue").GetComponent<Text>();
		this.textPowerExpValue = base.FindTransform("TextPowerExpValue").GetComponent<Text>();
		this.btnShopBuy = base.FindTransform("BtnShopBuy").GetComponent<ButtonCustom>();
		this.btnShopBuy.set_enabled(true);
		this.btnShopBuy.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickShopBuy);
		this.btnShopBuy.get_gameObject().SetActive(SystemConfig.IsOpenPay);
		List<LiXianJieMianPeiZhi> dataList = DataReader<LiXianJieMianPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			LiXianJieMianPeiZhi liXianJieMianPeiZhi = dataList.get_Item(i);
			if (i == 0)
			{
				this.textBeginTitle.set_text(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.panelname, false));
				this.textBeginEexplainInfo.set_text(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId, false));
				this.infoData.Add(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId2, false));
				this.infoData.Add(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId3, false));
				this.infoData.Add(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId4, false));
				this.infoData.Add(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId5, false));
				this.infoData.Add(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId6, false));
			}
			else
			{
				this.textResultEexplainInfo.set_text(GameDataUtils.GetChineseContent(liXianJieMianPeiZhi.describeId, false));
			}
		}
		this.textBeginEexplain.set_text(GameDataUtils.GetChineseContent(330014, false));
		this.textLvStr.set_text(GameDataUtils.GetChineseContent(330015, false));
		this.textNeedExpStr.set_text(GameDataUtils.GetChineseContent(330016, false));
		this.textHourExpStr.set_text(GameDataUtils.GetChineseContent(330017, false));
		this.textPowerExpStr.set_text(GameDataUtils.GetChineseContent(330033, false));
		this.textResultTitle.set_text(GameDataUtils.GetChineseContent(330018, false));
		this.textBtnEnsureName.set_text(GameDataUtils.GetChineseContent(330019, false));
		this.textResultEexplain.set_text(GameDataUtils.GetChineseContent(330014, false));
		this.textOffTimeStr.set_text(GameDataUtils.GetChineseContent(330020, false));
		this.textLeftTimeStr.set_text(GameDataUtils.GetChineseContent(330021, false));
		this.textGetExpStr.set_text(GameDataUtils.GetChineseContent(330022, false));
		this.textLvUpStr.set_text(GameDataUtils.GetChineseContent(330023, false));
	}

	public void RefreshUI()
	{
		OffLineLoginPush offHookData = OffHookManager.Instance.GetOffHookData();
		this.SwitchMode(OffHookUI.Mode.BeginRegion);
		OffHookManager.Instance.SendPanelReq();
		if (offHookData != null)
		{
			string time = TimeConverter.GetTime(offHookData.hasTime, TimeFormat.DHHMM_Chinese);
			this.textOffTime.set_text(string.Format(GameDataUtils.GetChineseContent(330024, false), time));
			this.textLeftTimeValue.set_text(time);
			this.textLvValue.set_text(string.Format(GameDataUtils.GetChineseContent(330025, false), EntityWorld.Instance.EntSelf.Lv));
			string time2 = TimeConverter.GetTime(offHookData.offTime, TimeFormat.DHHMM_Chinese);
			this.textOffTimeValue.set_text(time2);
			this.textGetExpValue.set_text(string.Format(GameDataUtils.GetChineseContent(330026, false), AttrUtility.GetExpValueStr(offHookData.addExp)));
			this.textLvUpValue1.set_text(string.Format(GameDataUtils.GetChineseContent(330025, false), offHookData.roleLv));
			this.textLvUpValue2.set_text(string.Format(GameDataUtils.GetChineseContent(330025, false), EntityWorld.Instance.EntSelf.Lv));
		}
	}

	public void RefreshBeginUI(OffLineMsgRes down)
	{
		string time = TimeConverter.GetTime(down.hasTime, TimeFormat.DHHMM_Chinese);
		this.textOffTime.set_text(string.Format(GameDataUtils.GetChineseContent(330024, false), time));
		this.textLeftTimeValue.set_text(time);
		this.textLvValue.set_text(string.Format(GameDataUtils.GetChineseContent(330025, false), EntityWorld.Instance.EntSelf.Lv));
		this.textNeedExpValue.set_text(string.Format(GameDataUtils.GetChineseContent(330026, false), AttrUtility.GetExpValueStr(down.needExp)));
		this.textHourExpValue.set_text(string.Format(GameDataUtils.GetChineseContent(330026, false), AttrUtility.GetExpValueStr(down.hourExp)));
		long value = down.hourExp + down.hourExp * 2L / 10L;
		this.textPowerExpValue.set_text(string.Format(GameDataUtils.GetChineseContent(330026, false), AttrUtility.GetExpValueStr(value)));
	}

	public void ShowResultPanel()
	{
		this.SwitchMode(OffHookUI.Mode.ResultRegion);
	}

	protected void StrMove()
	{
		if (this.infoData == null || this.infoData.get_Count() < 1)
		{
			return;
		}
		int pos = 0;
		this.t = TimerHeap.AddTimer(0u, 5000, delegate
		{
			if (pos >= this.infoData.get_Count())
			{
				pos = 0;
			}
			this.textEffectContent.set_text(this.infoData.get_Item(pos));
			pos++;
		});
	}
}
