using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageRankingUI : BaseUIBehaviour
{
	private List<Transform> rankingItemList;

	private float totalDamageValue;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.rankingItemList = new List<Transform>();
		for (int i = 1; i <= 3; i++)
		{
			Transform transform = base.FindTransform("rankingItem" + i);
			this.rankingItemList.Add(transform);
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < this.rankingItemList.get_Count(); i++)
		{
			Transform transform = this.rankingItemList.get_Item(i);
			transform.get_gameObject().SetActive(false);
		}
		this.AddListeners();
	}

	private void OnDisable()
	{
		this.RemoveListeners();
	}

	protected override void AddListeners()
	{
		if (!this.IsAddListenersSuccess)
		{
			this.IsAddListenersSuccess = true;
			EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		}
	}

	protected override void RemoveListeners()
	{
		if (this.IsAddListenersSuccess)
		{
			this.IsAddListenersSuccess = false;
			EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		}
	}

	private void OnSecondsPast()
	{
		InstanceManager.QueryBattleSituation(BattleHurtInfoType.RoleMakeTotalHurt, delegate(XDict<long, BattleSituationInfo> battleSituationInfoDic)
		{
			this.totalDamageValue = 0f;
			List<BattleSituationInfo> list = new List<BattleSituationInfo>();
			for (int i = 0; i < battleSituationInfoDic.Count; i++)
			{
				long key = battleSituationInfoDic.ElementKeyAt(i);
				if (battleSituationInfoDic[key].num > 0L)
				{
					this.totalDamageValue += (float)battleSituationInfoDic[key].num;
					list.Add(battleSituationInfoDic[key]);
				}
			}
			list.Sort(new Comparison<BattleSituationInfo>(DamageRankingUI.BattleSituationSortCompare));
			this.RefreshUI(list);
		});
	}

	public void RefreshUI(List<BattleSituationInfo> infoList)
	{
		int i = 0;
		if (infoList == null)
		{
			return;
		}
		if (this.totalDamageValue <= 0f)
		{
			return;
		}
		int num = 0;
		while (i < infoList.get_Count())
		{
			if (i >= this.rankingItemList.get_Count())
			{
				break;
			}
			if (!this.rankingItemList.get_Item(i).get_gameObject().get_activeSelf())
			{
				this.rankingItemList.get_Item(i).get_gameObject().SetActive(true);
			}
			this.rankingItemList.get_Item(i).FindChild("roleName").GetComponent<Text>().set_text(infoList.get_Item(i).name + "：");
			int num2;
			if (i != infoList.get_Count() - 1)
			{
				num2 = Mathf.RoundToInt((float)infoList.get_Item(i).num / this.totalDamageValue * 100f);
				num += num2;
			}
			else
			{
				num2 = ((100 - num < 0) ? 0 : (100 - num));
			}
			this.rankingItemList.get_Item(i).FindChild("damage").FindChild("damageText").GetComponent<Text>().set_text(infoList.get_Item(i).num + string.Empty);
			this.rankingItemList.get_Item(i).FindChild("percentImg").GetComponent<RectTransform>().set_sizeDelta(new Vector2((float)(288 * num2) / 100f, 20f));
			i++;
		}
		for (int j = i; j < 3; j++)
		{
			this.rankingItemList.get_Item(j).get_gameObject().SetActive(false);
		}
	}

	private static int BattleSituationSortCompare(BattleSituationInfo AF1, BattleSituationInfo AF2)
	{
		int result = 0;
		if (AF1.num > AF2.num)
		{
			return -1;
		}
		return result;
	}
}
