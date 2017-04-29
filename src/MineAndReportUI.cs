using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineAndReportUI : UIBase
{
	protected const int MineCount = 5;

	protected GameObject MineAndReportUIMine;

	protected List<Image> MineAndReportUIMineInfoPoint = new List<Image>();

	protected List<Text> MineAndReportUIMineInfoName = new List<Text>();

	protected List<Text> MineAndReportUIMineInfoGuildName = new List<Text>();

	protected XDict<GameObject, int> MineAndReportUIMineActionInfo = new XDict<GameObject, int>();

	protected Text MineAndReportUIMyInfoText;

	protected GameObject MineAndReportUIReport;

	protected RectTransform MineAndReportUIReportContent;

	protected VerticalLayoutGroup MineAndReportUIReportVLG;

	protected ListPool MineAndReportUIReportPool;

	protected Text MineAndReportUIKillInfoSign0Text;

	protected Text MineAndReportUIKillInfoSign1Text;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.MineAndReportUIMine = base.FindTransform("MineAndReportUIMine").get_gameObject();
		for (int i = 0; i < 5; i++)
		{
			this.MineAndReportUIMineInfoPoint.Add(base.FindTransform("MineAndReportUIMineInfo" + i + "Point").GetComponent<Image>());
			this.MineAndReportUIMineInfoName.Add(base.FindTransform("MineAndReportUIMineInfo" + i + "Name").GetComponent<Text>());
			this.MineAndReportUIMineInfoGuildName.Add(base.FindTransform("MineAndReportUIMineInfo" + i + "GuildName").GetComponent<Text>());
			GameObject gameObject = base.FindTransform("MineAndReportUIMineInfo" + i + "BG").get_gameObject();
			this.MineAndReportUIMineActionInfo.Add(gameObject, i + 1);
			gameObject.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMineBtn);
		}
		this.MineAndReportUIMyInfoText = base.FindTransform("MineAndReportUIMyInfoText").GetComponent<Text>();
		this.MineAndReportUIReport = base.FindTransform("MineAndReportUIReport").get_gameObject();
		this.MineAndReportUIReportContent = base.FindTransform("MineAndReportUIReportContent").GetComponent<RectTransform>();
		this.MineAndReportUIReportVLG = this.MineAndReportUIReportContent.GetComponent<VerticalLayoutGroup>();
		this.MineAndReportUIReportPool = this.MineAndReportUIReportContent.GetComponent<ListPool>();
		this.MineAndReportUIReportPool.SetItem("MineAndReportUIReportInfoUnit");
		this.MineAndReportUIReportPool.isAutoCalcalateLayout = false;
		this.MineAndReportUIKillInfoSign0Text = base.FindTransform("MineAndReportUIKillInfoSign0Text").GetComponent<Text>();
		this.MineAndReportUIKillInfoSign1Text = base.FindTransform("MineAndReportUIKillInfoSign1Text").GetComponent<Text>();
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isEndNav = false;
		this.isInterruptStick = false;
	}

	public void ShowMine(bool isShow)
	{
		if (this.MineAndReportUIMine.get_activeSelf() != isShow)
		{
			this.MineAndReportUIMine.SetActive(isShow);
		}
		if (isShow)
		{
			this.SetMineData();
		}
		this.SetKillData();
	}

	public void SetMineData()
	{
		this.SetMineInfo(GuildWarManager.Instance.GetMineInfo());
		this.SetMyMine(GuildWarManager.Instance.BattleNo);
	}

	public void SetMineInfo(XDict<int, GuildWarManager.MineInfo> mineInfo)
	{
		for (int i = 0; i < 5; i++)
		{
			if (mineInfo.ContainsKey(i + 1))
			{
				GuildWarManager.MineInfo mineInfo2 = mineInfo[i + 1];
				ResourceManager.SetCodeSprite(this.MineAndReportUIMineInfoPoint.get_Item(i), this.GetMinePointImageName(mineInfo2.state));
				this.MineAndReportUIMineInfoName.get_Item(i).set_text(this.FormatMinePointTextColor(mineInfo2.state, GameDataUtils.GetChineseContent(DataReader<JunTuanZhanCaiJi>.Get(mineInfo2.id).Name, false)));
				this.MineAndReportUIMineInfoGuildName.get_Item(i).set_text(this.FormatMinePointTextColor(mineInfo2.state, mineInfo2.ownerName));
			}
			else
			{
				ResourceManager.SetCodeSprite(this.MineAndReportUIMineInfoPoint.get_Item(i), this.GetMinePointImageName(GuildWarManager.MineState.NoData));
				this.MineAndReportUIMineInfoName.get_Item(i).set_text(GameDataUtils.GetChineseContent(515111, false));
				this.MineAndReportUIMineInfoGuildName.get_Item(i).set_text(GameDataUtils.GetChineseContent(515111, false));
			}
		}
	}

	protected string GetMinePointImageName(GuildWarManager.MineState mineState)
	{
		if (mineState == GuildWarManager.MineState.My)
		{
			return "kongxian";
		}
		if (mineState != GuildWarManager.MineState.Enemy)
		{
			return "lixian";
		}
		return "manglu";
	}

	protected string FormatMinePointTextColor(GuildWarManager.MineState mineState, string text)
	{
		if (mineState == GuildWarManager.MineState.My)
		{
			return TextColorMgr.GetColor(text, "6adc32", string.Empty);
		}
		if (mineState != GuildWarManager.MineState.Enemy)
		{
			return TextColorMgr.GetColor(text, "efefef", string.Empty);
		}
		return TextColorMgr.GetColor(text, "ff4040", string.Empty);
	}

	protected void SetMyMine(int mineID)
	{
		if (DataReader<JunTuanZhanCaiJi>.Contains(mineID))
		{
			this.MineAndReportUIMyInfoText.set_text(TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515119, false), GameDataUtils.GetChineseContent(DataReader<JunTuanZhanCaiJi>.Get(mineID).Name, false)), "67ff7d", string.Empty));
		}
		else
		{
			this.MineAndReportUIMyInfoText.set_text(TextColorMgr.GetColor(GameDataUtils.GetChineseContent(515129, false), "67ff7d", string.Empty));
		}
	}

	public void ShowReport(bool isShow)
	{
		if (this.MineAndReportUIReport.get_activeSelf() != isShow)
		{
			this.MineAndReportUIReport.SetActive(isShow);
		}
		if (isShow)
		{
			this.SetReportData();
		}
		this.SetKillData();
	}

	public void SetReportData()
	{
		this.SetReportData(GuildWarManager.Instance.ReportCache);
	}

	public void SetReportData(List<string> reportTexts)
	{
		this.MineAndReportUIReportPool.Create(reportTexts.get_Count(), delegate(int index)
		{
			if (index < reportTexts.get_Count() && index < this.MineAndReportUIReportPool.Items.get_Count())
			{
				MineAndReportUIReportInfoUnit component = this.MineAndReportUIReportPool.Items.get_Item(index).GetComponent<MineAndReportUIReportInfoUnit>();
				component.SetData(reportTexts.get_Item(index));
			}
			if (index == reportTexts.get_Count() - 1)
			{
				float num = 0f;
				for (int i = 0; i < this.MineAndReportUIReportPool.Items.get_Count(); i++)
				{
					num += this.MineAndReportUIReportPool.Items.get_Item(i).GetComponent<LayoutElement>().get_preferredHeight();
				}
				if (num < 190f)
				{
					num = 190f;
				}
				this.MineAndReportUIReportVLG.get_padding().set_top(0);
				this.MineAndReportUIReportContent.set_sizeDelta(new Vector2(this.MineAndReportUIReportContent.get_sizeDelta().x, num));
			}
		});
	}

	public void SetKillData()
	{
		this.SetKillData(GuildWarManager.Instance.RoleKillCount, GuildWarManager.Instance.RoleTotalResourceNum);
	}

	public void SetKillData(int killNum, int resourceNum)
	{
		this.MineAndReportUIKillInfoSign0Text.set_text(killNum.ToString());
		this.MineAndReportUIKillInfoSign1Text.set_text(resourceNum.ToString());
	}

	protected void OnClickMineBtn(GameObject go)
	{
		if (!this.MineAndReportUIMineActionInfo.ContainsKey(go))
		{
			return;
		}
		GuildWarManager.Instance.NavToMine(this.MineAndReportUIMineActionInfo[go]);
	}
}
