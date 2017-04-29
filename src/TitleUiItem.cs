using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiItem : MonoBehaviour
{
	public Text Title;

	public Image TitleImage;

	public Text ConditionOwn;

	public Text Condition;

	public GameObject New;

	public GameObject Wear;

	public GameObject[] Bonuses;

	public ButtonCustom GotoBtn;

	public Text ValidityPeriodTitle;

	public Text ValidityPeriod;

	public Image LeftTimebg;

	public GameObject NoHave;

	[HideInInspector]
	public int id;

	private int pathId;

	private bool isOwn;

	private DateTime time;

	private int duration;

	private void Start()
	{
		ButtonCustom expr_06 = this.GotoBtn;
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClick));
	}

	private void Update()
	{
	}

	private void OnClick(GameObject go)
	{
		if (TitleManager.Instance.OwnCurrId != this.id)
		{
			NetworkManager.Send(new ReplaceCurrTitleReq
			{
				titleId = this.id
			}, ServerType.Data);
			this.New.SetActive(false);
		}
	}

	public void UpdateItem(bool isOwn, TitleInfo ti)
	{
		this.isOwn = isOwn;
		ChengHao chengHao = DataReader<ChengHao>.Get(ti.titleId);
		if (chengHao.displayWay == 1)
		{
			this.Title.set_text(GameDataUtils.GetChineseContent(chengHao.icon, false));
			this.TitleImage.get_gameObject().SetActive(false);
			this.Title.get_gameObject().SetActive(true);
		}
		else if (chengHao.displayWay == 2)
		{
			ResourceManager.SetSprite(this.TitleImage, GameDataUtils.GetIcon(chengHao.icon));
			this.TitleImage.get_gameObject().SetActive(true);
			this.Title.get_gameObject().SetActive(false);
		}
		this.id = ti.titleId;
		this.duration = chengHao.duration;
		for (int i = 0; i < 4; i++)
		{
			this.Bonuses[i].SetActive(false);
		}
		List<int> attrs = DataReader<Attrs>.Get(chengHao.gainProperty).attrs;
		List<int> values = DataReader<Attrs>.Get(chengHao.gainProperty).values;
		for (int j = 0; j < attrs.get_Count(); j++)
		{
			this.Bonuses[j].SetActive(true);
			this.Bonuses[j].get_transform().Find("BonusesDesc0").GetComponent<Text>().set_text(AttrUtility.GetAttrName((GameData.AttrType)attrs.get_Item(j)));
			this.Bonuses[j].get_transform().Find("BonusesNum0").GetComponent<Text>().set_text("+" + AttrUtility.GetAttrValueDisplay((GameData.AttrType)attrs.get_Item(j), values.get_Item(j)));
		}
		this.time = TimeManager.Instance.CalculateLocalServerTimeBySecond(ti.remainTime);
		string chineseContent = GameDataUtils.GetChineseContent(chengHao.gainIntroduction, false);
		string text = GameDataUtils.GetChineseContent(chengHao.introduction, false);
		string text2 = null;
		int condition = chengHao.condition;
		if (condition != 2)
		{
			if (condition != 10)
			{
				text2 = chengHao.size.ToString();
			}
			else
			{
				int chapterOrder = DataReader<ZhuXianZhangJiePeiZhi>.Get(DataReader<ZhuXianPeiZhi>.Get(chengHao.size).chapterId).chapterOrder;
				int instance = DataReader<ZhuXianPeiZhi>.Get(chengHao.size).instance;
				string chineseContent2 = GameDataUtils.GetChineseContent(DataReader<ZhuXianPeiZhi>.Get(chengHao.size).name, false);
				text = string.Format(text, chapterOrder, instance, chineseContent2);
			}
		}
		else
		{
			text2 = GameDataUtils.GetChineseContent(DataReader<JingJiChangFenDuan>.Get(chengHao.size).name, false);
		}
		if (text2 != null)
		{
			text2 = "<color=#ff7d4b>" + text2 + "</color>";
			text = string.Format(text, text2);
		}
		if (chengHao.schedule == -1)
		{
			this.ConditionOwn.set_text(chineseContent);
			this.Condition.set_text(text);
		}
		else
		{
			int num = 0;
			if (TitleManager.Instance.idProcessMap.ContainsKey(ti.titleId))
			{
				num = TitleManager.Instance.idProcessMap.get_Item(ti.titleId);
			}
			if (num > chengHao.schedule)
			{
				num = chengHao.schedule;
			}
			this.ConditionOwn.set_text(chineseContent);
			this.Condition.set_text(string.Concat(new object[]
			{
				text,
				"(",
				num,
				"/",
				chengHao.schedule,
				")"
			}));
		}
		this.ConditionOwn.get_transform().get_parent().get_gameObject().SetActive(isOwn);
		this.Condition.get_transform().get_parent().get_gameObject().SetActive(!isOwn);
		if (ti.lookFlag)
		{
			this.New.SetActive(false);
		}
		else
		{
			this.New.SetActive(true);
		}
		if (ti.titleId == TitleManager.Instance.OwnCurrId)
		{
			this.Wear.SetActive(true);
		}
		else
		{
			this.Wear.SetActive(false);
		}
		this.NoHave.SetActive(!isOwn);
		this.RefreshTime();
	}

	public void RefreshTime()
	{
		if (this.id == 21)
		{
		}
		if (this.duration > 0)
		{
			if (this.isOwn)
			{
				if (this.time > TimeManager.Instance.PreciseServerTime)
				{
					this.ValidityPeriod.get_transform().get_parent().get_gameObject().SetActive(true);
					this.LeftTimebg.get_gameObject().SetActive(true);
					TimeSpan timeSpan = this.time - TimeManager.Instance.PreciseServerTime;
					int num = (int)timeSpan.get_TotalHours();
					string text = string.Empty;
					if (num > 1)
					{
						if (num > 24)
						{
							text = text + timeSpan.get_Days() + GameDataUtils.GetChineseContent(509000, false);
							text = text + timeSpan.get_Hours() + GameDataUtils.GetChineseContent(509001, false);
						}
					}
					else
					{
						text = (timeSpan.get_Minutes() + GameDataUtils.GetChineseContent(509002, false)).ToString();
					}
					this.ValidityPeriod.set_text(text);
					this.ValidityPeriodTitle.set_text(GameDataUtils.GetChineseContent(502264, false));
				}
				else
				{
					this.ValidityPeriod.get_transform().get_parent().get_gameObject().SetActive(false);
					this.LeftTimebg.get_gameObject().SetActive(false);
				}
			}
			else
			{
				string text2 = string.Empty;
				int num2 = this.duration / 86400;
				int num3 = this.duration % 86400 / 3600;
				if (num2 > 0)
				{
					text2 = num2 + GameDataUtils.GetChineseContent(509000, false);
					if (num3 > 0)
					{
						text2 += num3.ToString("D2") + GameDataUtils.GetChineseContent(509001, false);
					}
				}
				else if (num3 > 0)
				{
					text2 += num3.ToString("D2") + GameDataUtils.GetChineseContent(509001, false);
				}
				this.ValidityPeriod.set_text(text2);
				this.ValidityPeriodTitle.set_text(GameDataUtils.GetChineseContent(513304, false));
				this.ValidityPeriod.get_transform().get_parent().get_gameObject().SetActive(true);
				this.LeftTimebg.get_gameObject().SetActive(true);
			}
		}
		else
		{
			this.ValidityPeriod.get_transform().get_parent().get_gameObject().SetActive(false);
			this.LeftTimebg.get_gameObject().SetActive(false);
		}
	}
}
