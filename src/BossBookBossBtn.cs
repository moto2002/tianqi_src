using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BossBookBossBtn : BaseUIBehaviour
{
	private CanvasGroup m_CanvasGroup;

	private ButtonCustom m_BtnBoss;

	private Image m_BtnBossImage;

	private Text m_TextName;

	private Text m_TextTime;

	private Text m_TextLevel;

	private GameObject m_ImageSelect;

	private Transform m_PanelBtn;

	public int bossId;

	private int refreshTimeStamp;

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_BtnBoss = base.FindTransform("BtnBoss").GetComponent<ButtonCustom>();
		this.m_BtnBossImage = base.FindTransform("ImageBoss").GetComponent<Image>();
		this.m_CanvasGroup = this.m_BtnBossImage.GetComponent<CanvasGroup>();
		this.m_TextName = base.FindTransform("TextName").GetComponent<Text>();
		this.m_TextTime = base.FindTransform("TextTime").GetComponent<Text>();
		this.m_TextLevel = base.FindTransform("TextLevel").GetComponent<Text>();
		this.m_ImageSelect = base.FindTransform("ImageSelect").get_gameObject();
		this.m_PanelBtn = base.FindTransform("PanelBtn");
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<List<int>>(EventNames.BossBookPageUpdate, new Callback<List<int>>(this.OnPageUpdate));
		EventDispatcher.AddListener<int>(EventNames.BossBookItemUpdate, new Callback<int>(this.OnBossUpdate));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<List<int>>(EventNames.BossBookPageUpdate, new Callback<List<int>>(this.OnPageUpdate));
		EventDispatcher.RemoveListener<int>(EventNames.BossBookItemUpdate, new Callback<int>(this.OnBossUpdate));
	}

	public void InitBtn(ButtonCustom.VoidDelegateObj clickCallback)
	{
		this.m_BtnBoss.onClickCustom = clickCallback;
	}

	public void UpdateBtn(int bossId)
	{
		if (this.bossId != bossId)
		{
			BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(bossId);
			if (bossBiaoQian != null)
			{
				this.m_TextName.set_text(GameDataUtils.GetChineseContent(bossBiaoQian.nameId, false));
				this.m_TextTime.set_text(string.Empty);
				this.m_TextLevel.set_text(bossBiaoQian.step.ToString() + GameDataUtils.GetChineseContent(517516, false));
				string iconName = GameDataUtils.GetIconName(bossBiaoQian.icon);
				ResourceManager.SetSprite(this.m_BtnBossImage, ResourceManager.GetIconSprite(iconName));
			}
			this.bossId = bossId;
			this.EndCountDown();
		}
	}

	public void SetSelect(bool isSelect)
	{
		if (isSelect)
		{
			this.m_BtnBoss.set_enabled(false);
			this.m_ImageSelect.SetActive(true);
			this.m_TextName.set_color(new Color32(255, 235, 210, 255));
			this.m_CanvasGroup.set_alpha(1f);
			base.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
		}
		else
		{
			this.m_BtnBoss.set_enabled(true);
			this.m_ImageSelect.SetActive(false);
			this.m_TextName.set_color(new Color32(194, 171, 107, 255));
			this.m_CanvasGroup.set_alpha(0.7f);
			base.get_transform().set_localScale(new Vector3(0.8f, 0.8f, 0.8f));
		}
	}

	private void StratCountDown()
	{
		this.EndCountDown();
		base.StartCoroutine(this.CountDown());
	}

	private void EndCountDown()
	{
		base.StopAllCoroutines();
	}

	[DebuggerHidden]
	private IEnumerator CountDown()
	{
		BossBookBossBtn.<CountDown>c__Iterator3D <CountDown>c__Iterator3D = new BossBookBossBtn.<CountDown>c__Iterator3D();
		<CountDown>c__Iterator3D.<>f__this = this;
		return <CountDown>c__Iterator3D;
	}

	private void ShowTime()
	{
		int num = BossBookManager.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
		if (this.refreshTimeStamp > num)
		{
			this.m_TextTime.set_text(TimeConverter.GetTime(this.refreshTimeStamp - num, TimeFormat.HHMMSS));
		}
		else
		{
			this.EndCountDown();
			BossBookManager.Instance.SendGetBossLabelInfoReq(this.bossId);
		}
	}

	protected void OnPageUpdate(List<int> updataIds)
	{
		if (updataIds.Contains(this.bossId))
		{
			this.UpdateTimeInfo();
		}
	}

	protected void OnBossUpdate(int bossId)
	{
		if (bossId != this.bossId)
		{
			return;
		}
		this.UpdateTimeInfo();
	}

	protected void UpdateTimeInfo()
	{
		this.EndCountDown();
		BossItemInfo bossItemInfo = BossBookManager.Instance.GetBossItemInfo(this.bossId);
		if (bossItemInfo == null)
		{
			return;
		}
		if (bossItemInfo.survivalBossCount > 0)
		{
			this.m_TextTime.set_text(GameDataUtils.GetChineseContent(517515, false));
		}
		else
		{
			int num = BossBookManager.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
			this.refreshTimeStamp = bossItemInfo.nextRefreshSec;
			if (this.refreshTimeStamp > num)
			{
				this.m_TextTime.set_text(TimeConverter.GetTime(this.refreshTimeStamp - num, TimeFormat.HHMMSS));
				this.StratCountDown();
			}
			else
			{
				this.m_TextTime.set_text(string.Empty);
			}
		}
	}
}
