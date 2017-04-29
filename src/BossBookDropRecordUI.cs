using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class BossBookDropRecordUI : UIBase
{
	private RectTransform m_ScrollLayout;

	private Text m_TextTitle;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExit);
		this.m_ScrollLayout = (base.FindTransform("ScrollLayout") as RectTransform);
		this.m_TextTitle = base.FindTransform("TextTitle").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
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
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected void OnClickExit(GameObject go)
	{
		this.Show(false);
	}

	public void SetDropLog(List<BossDropLog> logList)
	{
		this.m_TextTitle.set_text(GameDataUtils.GetChineseContent(517512, false));
		this.m_ScrollLayout.set_anchoredPosition(new Vector2(0f, 0f));
		this.HideCells();
		for (int i = 0; i < logList.get_Count(); i++)
		{
			BossDropLog bossDropLog = logList.get_Item(i);
			Transform transform;
			if (i < this.m_ScrollLayout.get_childCount())
			{
				transform = this.m_ScrollLayout.GetChild(i);
				transform.get_gameObject().SetActive(true);
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("BBDropRecordCell");
				instantiate2Prefab.SetActive(true);
				transform = instantiate2Prefab.get_transform();
				transform.SetParent(this.m_ScrollLayout, false);
			}
			transform.get_gameObject().SetActive(true);
			BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(bossDropLog.labelId);
			ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(bossBiaoQian.scene);
			Text component = transform.Find("TextRecord").GetComponent<Text>();
			Text component2 = transform.Find("TextDrop").GetComponent<Text>();
			DateTime dateTime = BossBookManager.StampToDateTime(bossDropLog.dateTimeSec.ToString());
			string time = TimeConverter.GetTime(dateTime, TimeFormat.MDHHMM);
			string text = string.Format(GameDataUtils.GetChineseContent(517514, false), new object[]
			{
				time,
				bossDropLog.roleName,
				GameDataUtils.GetChineseContent(zhuChengPeiZhi.name, false),
				GameDataUtils.GetChineseContent(bossBiaoQian.nameId, false)
			});
			component.set_text(text);
			if (bossDropLog.items.get_Count() > 0)
			{
				int num = this.GetBestQualityItem(bossDropLog.items);
				if (num < 0 && num >= bossDropLog.items.get_Count())
				{
					num = 0;
				}
				ItemBriefInfo itemBriefInfo = bossDropLog.items.get_Item(num);
				Items items = DataReader<Items>.Get(itemBriefInfo.cfgId);
				if (items != null)
				{
					component2.set_text(GameDataUtils.GetChineseContent(items.name, false));
					Dictionary<string, Color> textColorByQuality = GameDataUtils.GetTextColorByQuality(items.color);
					component2.set_color(textColorByQuality.get_Item("TextColor"));
					component2.GetComponent<Outline>().set_effectColor(textColorByQuality.get_Item("TextOutlineColor"));
					component2.GetComponent<RectTransform>().set_anchoredPosition(new Vector2(component.get_preferredWidth() + 50f, 0f));
				}
				else
				{
					component2.set_text(string.Empty);
				}
			}
			else
			{
				component2.set_text(string.Empty);
			}
		}
	}

	public void HideCells()
	{
		for (int i = 0; i < this.m_ScrollLayout.get_childCount(); i++)
		{
			Transform child = this.m_ScrollLayout.GetChild(i);
			child.get_gameObject().SetActive(false);
		}
	}

	private int GetBestQualityItem(List<ItemBriefInfo> items)
	{
		if (items == null || items.get_Count() <= 0)
		{
			return -1;
		}
		int num = 0;
		for (int i = 1; i < items.get_Count(); i++)
		{
			Items items2 = DataReader<Items>.Get(items.get_Item(num).cfgId);
			Items items3 = DataReader<Items>.Get(items.get_Item(i).cfgId);
			if (items2 != null && items3 != null)
			{
				if (items2.color >= 6)
				{
					return num;
				}
				if (items3.color >= 6)
				{
					return i;
				}
				if (items2.color < items3.color)
				{
					num = i;
				}
			}
		}
		return num;
	}
}
