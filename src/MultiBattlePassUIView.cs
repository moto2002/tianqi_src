using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattlePassUIView : UIBase
{
	public static MultiBattlePassUIView Instance;

	private Text m_lblExpNum;

	private Text m_lblGoldNum;

	private Transform m_tranItemsRegion;

	private int fx_WinPop;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		MultiBattlePassUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			MultiBattlePassUIView.Instance = null;
			MultiBattlePassUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.FindTransform("BtnExitName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(501002, false));
		string colorByID = TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(501003, false), 405);
		base.FindTransform("RewardsName").GetComponent<Text>().set_text(colorByID);
		this.m_lblExpNum = base.FindTransform("ExpNum").GetComponent<Text>();
		this.m_lblGoldNum = base.FindTransform("GoldNum").GetComponent<Text>();
		this.m_tranItemsRegion = base.FindTransform("ItemsRegion");
		this.m_tranItemsRegion.get_gameObject().SetActive(false);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("PassTimeNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Passtime";
		textBinder = base.FindTransform("countDownText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CoundDownText";
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
		visibilityBinder.Target = base.FindTransform("BtnStatistics").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowBtnStatistics";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnNextNode").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowBtnNext";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("countDownText").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowCountDownText";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnExit").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnExitUp";
		buttonBinder = base.FindTransform("BtnStatistics").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnStatisticsUp";
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.m_tranItemsRegion.get_gameObject().SetActive(false);
		base.FindTransform("RewardBgsBoss").get_gameObject().SetActive(false);
	}

	public void PlayLogicAnim()
	{
	}

	public void SetBossRewardItems(Dictionary<int, int> itemIdNum)
	{
		if (itemIdNum.get_Count() > 0)
		{
			base.FindTransform("RewardBgsBoss").get_gameObject().SetActive(true);
			Transform transform = base.FindTransform("BossRewardItems");
			for (int i = 0; i < transform.get_childCount(); i++)
			{
				Object.Destroy(transform.GetChild(i).get_gameObject());
			}
			using (Dictionary<int, int>.Enumerator enumerator = itemIdNum.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					ItemShow.ShowItem(transform, current.get_Key(), (long)current.get_Value(), false, null, 2001);
				}
			}
		}
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
		Debug.LogError(MultiBattlePassUIViewModel.Instance.ObtainCoins + "=====" + MultiBattlePassUIViewModel.Instance.ObtainExps);
		base.FindTransform("Exp").get_gameObject().SetActive(MultiBattlePassUIViewModel.Instance.ObtainExps != 0L);
		base.FindTransform("Gold").get_gameObject().SetActive(MultiBattlePassUIViewModel.Instance.ObtainCoins != 0L);
		ChangeNumAnim changeNumAnim = this.m_lblExpNum.get_gameObject().AddMissingComponent<ChangeNumAnim>();
		changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_lblExpNum, 0L, MultiBattlePassUIViewModel.Instance.ObtainExps, null, null, null);
		changeNumAnim = this.m_lblGoldNum.get_gameObject().AddMissingComponent<ChangeNumAnim>();
		changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_lblGoldNum, 0L, MultiBattlePassUIViewModel.Instance.ObtainCoins, null, null, null);
	}

	public void anim_End(AnimationEvent e)
	{
		UIQueueManager.Instance.CheckQueue(PopCondition.BattlePassUI_EndAnim);
	}

	public void RemoveAllFxs()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_WinPop, true);
	}

	private void WinPop()
	{
		this.fx_WinPop = FXSpineManager.Instance.ReplaySpine(this.fx_WinPop, 407, base.FindTransform("WinFlag"), base.get_gameObject().get_name(), 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
