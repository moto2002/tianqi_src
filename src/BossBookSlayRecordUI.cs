using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class BossBookSlayRecordUI : UIBase
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

	public void SetSlayLog(int bossId, List<BossKilledLog> logList)
	{
		BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(bossId);
		ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(bossBiaoQian.scene);
		this.m_TextTitle.set_text(GameDataUtils.GetChineseContent(bossBiaoQian.nameId, false) + GameDataUtils.GetChineseContent(517511, false));
		this.m_ScrollLayout.set_anchoredPosition(new Vector2(0f, 0f));
		this.HideCells();
		for (int i = 0; i < logList.get_Count(); i++)
		{
			BossKilledLog bossKilledLog = logList.get_Item(i);
			Transform transform;
			if (i < this.m_ScrollLayout.get_childCount())
			{
				transform = this.m_ScrollLayout.GetChild(i);
				transform.get_gameObject().SetActive(true);
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("BBSlayRecordCell");
				instantiate2Prefab.SetActive(true);
				transform = instantiate2Prefab.get_transform();
				transform.SetParent(this.m_ScrollLayout, false);
			}
			DateTime dateTime = BossBookManager.StampToDateTime(bossKilledLog.dateTimeSec.ToString());
			string time = TimeConverter.GetTime(dateTime, TimeFormat.MDHHMM);
			string text = string.Format(GameDataUtils.GetChineseContent(517513, false), time, GameDataUtils.GetChineseContent(zhuChengPeiZhi.name, false), bossKilledLog.roleName);
			transform.Find("TextRecord").GetComponent<Text>().set_text(text);
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
}
