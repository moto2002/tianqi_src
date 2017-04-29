using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattlePassUIView : UIBase
{
	public static BattlePassUIView Instance;

	private Text m_lblExpNum;

	private Text m_lblGoldNum;

	private GameObject m_goBtnAgain;

	private Transform m_tranItemsRegion;

	private int m_reachCount;

	private int fx_StarWhite1;

	private int fx_StarWhite2;

	private int fx_StarWhite3;

	private int fx_Starlight1;

	private int fx_Starlight2;

	private int fx_Starlight3;

	private int fx_WinPop;

	private int fx_win;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		BattlePassUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			BattlePassUIView.Instance = null;
			BattlePassUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.FindTransform("BtnExitName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(501002, false));
		base.FindTransform("BtnAgainName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(501001, false));
		this.m_lblExpNum = base.FindTransform("ExpNum").GetComponent<Text>();
		this.m_lblGoldNum = base.FindTransform("GoldNum").GetComponent<Text>();
		this.m_goBtnAgain = base.FindTransform("BtnAgain").get_gameObject();
		this.m_tranItemsRegion = base.FindTransform("ItemsRegion");
		this.m_tranItemsRegion.get_gameObject().SetActive(false);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("PassTimeNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Passtime";
		ListBinder listBinder = base.FindTransform("Items").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "BattlePassUIDropItem";
		listBinder.SourceBinding.MemberName = "Items";
		listBinder = base.FindTransform("Item2s").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "BattlePassUIDropItem";
		listBinder.SourceBinding.MemberName = "Item2s";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnAgainNode").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnAgainVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnStatistics").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnStatisticsVisibility";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnExit").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnExitUp";
		buttonBinder = base.FindTransform("BtnAgain").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnAgainUp";
		buttonBinder = base.FindTransform("BtnStatistics").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnStatisticsUp";
	}

	protected override void OnEnable()
	{
		this.RemoveAllFxs();
		if (EntityWorld.Instance.EntSelf.Lv < 20)
		{
			this.m_goBtnAgain.SetActive(false);
		}
		else
		{
			this.m_goBtnAgain.SetActive(true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.RemoveAllFxs();
		this.m_tranItemsRegion.get_gameObject().SetActive(false);
		base.FindTransform("BtnExitNode").get_gameObject().SetActive(false);
	}

	public void PlayLogicAnim()
	{
	}

	public void SetConsummation(int reachCount)
	{
		this.m_reachCount = reachCount;
	}

	public void anim_ShowStars()
	{
		this.ShowStars();
	}

	public void anim_Starlight1(AnimationEvent e)
	{
		this.Starlight1();
	}

	public void anim_Starlight2(AnimationEvent e)
	{
		this.Starlight2();
	}

	public void anim_Starlight3(AnimationEvent e)
	{
		this.Starlight3();
	}

	public void anim_WinPop()
	{
		this.WinPop();
	}

	public void anim_Items(AnimationEvent e)
	{
		this.anim_RewardNums();
		ListBinder component = base.FindTransform("Items").GetComponent<ListBinder>();
		ListBinder component2 = base.FindTransform("Item2s").GetComponent<ListBinder>();
		this.m_tranItemsRegion.get_gameObject().SetActive(true);
		component.HideAll();
		component2.HideAll();
		int count = component.m_listUse.get_Count();
		for (int i = 0; i < count; i++)
		{
			int num = i;
			if (num < component.m_listUse.get_Count())
			{
				component.m_listUse.get_Item(num).get_gameObject().SetActive(true);
			}
		}
		int count2 = component2.m_listUse.get_Count();
		for (int j = 0; j < count2; j++)
		{
			int num2 = j;
			if (num2 < component2.m_listUse.get_Count())
			{
				component2.m_listUse.get_Item(num2).get_gameObject().SetActive(true);
			}
		}
	}

	private void anim_RewardNums()
	{
		ChangeNumAnim changeNumAnim = this.m_lblExpNum.get_gameObject().AddMissingComponent<ChangeNumAnim>();
		changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_lblExpNum, 0L, BattlePassUIViewModel.Instance.ObtainExps, null, null, null);
		changeNumAnim = this.m_lblGoldNum.get_gameObject().AddMissingComponent<ChangeNumAnim>();
		changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_lblGoldNum, 0L, BattlePassUIViewModel.Instance.ObtainCoins, null, null, null);
	}

	public void anim_End(AnimationEvent e)
	{
		UIQueueManager.Instance.CheckQueue(PopCondition.BattlePassUI_EndAnim);
	}

	public void RemoveAllFxs()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_Starlight1, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_Starlight2, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_Starlight3, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_StarWhite1, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_StarWhite2, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_StarWhite3, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinPop, true);
	}

	private void ShowStars()
	{
		this.fx_StarWhite1 = FXSpineManager.Instance.ReplaySpine(this.fx_StarWhite1, 408, base.FindTransform("Star1"), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_StarWhite2 = FXSpineManager.Instance.ReplaySpine(this.fx_StarWhite2, 408, base.FindTransform("Star2"), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_StarWhite3 = FXSpineManager.Instance.ReplaySpine(this.fx_StarWhite3, 408, base.FindTransform("Star3"), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void Starlight1()
	{
		if (this.m_reachCount >= 1)
		{
			FXSpineManager.Instance.PlaySpine(410, base.FindTransform("Star1"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_Starlight1 = FXSpineManager.Instance.ReplaySpine(this.fx_Starlight1, 406, base.FindTransform("Star1"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void Starlight2()
	{
		if (this.m_reachCount >= 2)
		{
			FXSpineManager.Instance.PlaySpine(410, base.FindTransform("Star2"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_Starlight2 = FXSpineManager.Instance.ReplaySpine(this.fx_Starlight2, 406, base.FindTransform("Star2"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void Starlight3()
	{
		if (this.m_reachCount >= 3)
		{
			FXSpineManager.Instance.PlaySpine(410, base.FindTransform("Star3"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_Starlight3 = FXSpineManager.Instance.ReplaySpine(this.fx_Starlight3, 406, base.FindTransform("Star3"), "BattlePassUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void WinPop()
	{
		FXSpineManager.Instance.PlaySpine(411, base.FindTransform("WinFlag"), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_WinPop = FXSpineManager.Instance.ReplaySpine(this.fx_WinPop, 407, base.FindTransform("WinFlag"), "BattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
