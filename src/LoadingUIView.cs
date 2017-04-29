using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIView : UIBase
{
	public static bool IsFromLogin;

	public static bool IsShowPrompt;

	private Transform Point1;

	private Slider m_spProgressBg;

	private Text m_lblProgressText;

	private Text m_lblTipsDescText;

	private RawImage m_Bg;

	private GameObject mGoPrompt;

	private bool IsInSmooth;

	private float SmoothPercent;

	private static LoadingUIView m_instance;

	private List<int> tipsIds = new List<int>();

	public static LoadingUIView Instance
	{
		get
		{
			if (LoadingUIView.m_instance == null)
			{
				UIManagerControl.Instance.OpenUI("LoadingUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
			}
			return LoadingUIView.m_instance;
		}
	}

	public static void Open(bool isShowPrompt = false)
	{
		LoadingUIView.IsShowPrompt = isShowPrompt;
		if (!UIManagerControl.Instance.IsOpen("LoadingUI"))
		{
			UIManagerControl.Instance.OpenUI("LoadingUI", UINodesManager.T3RootOfSpecial, true, UIType.NonPush);
		}
	}

	public static void Close()
	{
		UIManagerControl.Instance.HideUI("LoadingUI");
	}

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		LoadingUIView.m_instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_Bg = base.FindTransform("RawImage").GetComponent<RawImage>();
		this.m_spProgressBg = base.FindTransform("Slider").GetComponent<Slider>();
		this.m_lblProgressText = base.FindTransform("ProgressText").GetComponent<Text>();
		this.m_lblTipsDescText = base.FindTransform("TipsDesc").GetComponent<Text>();
		this.mGoPrompt = base.FindTransform("PromptDesc").get_gameObject();
		this.Point1 = base.FindTransform("point1");
	}

	protected override void OnEnable()
	{
		base.get_transform().SetAsLastSibling();
		this.PlaySpine();
		LoadingUIView.SetProgress(0f);
		this.RandomTips();
		if (LoadingUIView.IsFromLogin)
		{
			ResourceManager.SetTexture(this.m_Bg, "load_login");
		}
		else
		{
			this.RandomBG();
		}
		this.CheckIsOpenWithPVPBattle();
		this.mGoPrompt.SetActive(LoadingUIView.IsShowPrompt);
		this.m_lblTipsDescText.get_gameObject().SetActive(!LoadingUIView.IsShowPrompt);
		GuideManager.Instance.out_system_lock = true;
		GuideUIView.IsDownOn = false;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		LoadingUIView.IsFromLogin = false;
		this.CheckIsCloseWithPVPBattle();
		GuideManager.Instance.out_system_lock = false;
		GuideManager.Instance.CheckQueue(true, false);
		TimerHeap.AddTimer(5000u, 0, delegate
		{
			GuideUIView.IsDownOn = true;
		});
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		this.DeleteSpine();
	}

	private void CheckIsOpenWithPVPBattle()
	{
		if (InstanceManager.CurrentInstanceType == InstanceType.Arena && PVPManager.Instance.isEnterPVP)
		{
			base.FindTransform("RawImage").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("RawImage").get_gameObject().SetActive(true);
		}
	}

	private void CheckIsCloseWithPVPBattle()
	{
		if (InstanceManager.CurrentInstanceType == InstanceType.Arena)
		{
			UIManagerControl.Instance.HideUI("PVPVSUI");
		}
	}

	private void RandomTips()
	{
		string text = string.Empty;
		if (EntityWorld.Instance.EntSelf != null)
		{
			this.tipsIds.Clear();
			List<DengJiDuan> dataList = DataReader<DengJiDuan>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				if (EntityWorld.Instance.EntSelf.Lv >= dataList.get_Item(i).minLv && EntityWorld.Instance.EntSelf.Lv <= dataList.get_Item(i).maxLv)
				{
					this.tipsIds.Add(dataList.get_Item(i).lvId);
				}
			}
			if (this.tipsIds.get_Count() == 0)
			{
				Debug.Log("tipsIds count == 0");
			}
			if (this.tipsIds.get_Count() > 0)
			{
				int tipsId = this.tipsIds.get_Item(Random.Range(0, this.tipsIds.get_Count()));
				List<Tips> list = DataReader<Tips>.DataList.FindAll((Tips e) => e.lvId == tipsId);
				if (list.get_Count() == 0)
				{
					Debug.Log("tips count == 0");
				}
				if (list.get_Count() > 0)
				{
					text = string.Format("<size=26>Tips:</size><size=22>{0}</size>", GameDataUtils.GetChineseContent(list.get_Item(Random.Range(0, list.get_Count())).tipsId, false));
				}
			}
		}
		this.m_lblTipsDescText.set_text(text);
	}

	private void RandomBG()
	{
		List<JiaZaiJieMianPeiTu> dataList = DataReader<JiaZaiJieMianPeiTu>.DataList;
		if (dataList.get_Count() == 0)
		{
			Debug.LogError("GameData.JiaZaiJieMianPeiTu count = 0");
			return;
		}
		int num = Random.Range(0, dataList.get_Count());
		if (num >= 0 && num < dataList.get_Count())
		{
			string iconName = GameDataUtils.GetIconName(dataList.get_Item(num).loadingPic);
			if (ResourceManager.IsTextureExist(iconName))
			{
				ResourceManager.SetTexture(this.m_Bg, iconName);
			}
		}
	}

	public static void SetProgress(float percent)
	{
		if (LoadingUIView.Instance != null)
		{
			LoadingUIView.Instance.DoSetProgress(percent);
		}
	}

	private void DoSetProgress(float percent)
	{
		this.IsInSmooth = false;
		if (this.m_spProgressBg != null && this.m_lblProgressText != null)
		{
			this.m_spProgressBg.set_value(percent);
			this.m_lblProgressText.set_text("正在加载" + Convert.ToInt32(percent * 100f) + "%");
		}
	}

	public static void SetProgressInSmooth(float percent)
	{
		if (LoadingUIView.Instance != null)
		{
			LoadingUIView.Instance.DoSetProgressInSmooth(percent);
		}
	}

	private void DoSetProgressInSmooth(float percent)
	{
		if (percent < 1f)
		{
			this.IsInSmooth = true;
			this.SmoothPercent = percent;
		}
		else
		{
			this.DoSetProgress(percent);
		}
	}

	private void Update()
	{
		if (this.IsInSmooth && this.m_spProgressBg != null)
		{
			Slider expr_22 = this.m_spProgressBg;
			expr_22.set_value(expr_22.get_value() + (this.SmoothPercent - this.m_spProgressBg.get_value()) * 0.1f);
			this.m_lblProgressText.set_text("正在加载" + Convert.ToInt32(this.m_spProgressBg.get_value() * 100f) + "%");
		}
	}

	private void PlaySpine()
	{
		ImageSequenceFrames component = base.FindTransform("LoadingFX").GetComponent<ImageSequenceFrames>();
		component.IsAnimating = false;
		List<string> list = new List<string>();
		for (int i = 0; i <= 24; i++)
		{
			list.Add("cat000" + i.ToString("D2"));
		}
		component.Scale = 1f;
		component.PlayAnimation2Loop(list, 0.0001f);
	}

	private void DeleteSpine()
	{
	}
}
