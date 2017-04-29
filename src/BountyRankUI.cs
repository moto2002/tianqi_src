using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyRankUI : UIBase
{
	public ListView2 list;

	public GameObject MyRankTips;

	public Text MyRank;

	public Text MyLevel;

	protected override void Preprocessing()
	{
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	public void UpdateList(BountyRankListInfo self, List<BountyRankListInfo> rank)
	{
		this.list.CreateRow(rank.get_Count(), 0);
		for (int i = 0; i < rank.get_Count(); i++)
		{
			this.list.Items.get_Item(i).GetComponent<BountyRankItem>().UpdateItem(rank.get_Item(i));
		}
		if (self == null || self.rank < 1)
		{
			this.MyRankTips.SetActive(true);
			this.MyRank.get_transform().get_parent().get_gameObject().SetActive(false);
			this.MyLevel.get_transform().get_parent().get_gameObject().SetActive(false);
		}
		else
		{
			Debug.LogError(self == null);
			this.MyRankTips.SetActive(false);
			this.MyRank.set_text(self.rank.ToString());
			this.MyLevel.set_text(self.score.ToString());
			this.MyRank.get_transform().get_parent().get_gameObject().SetActive(true);
			this.MyLevel.get_transform().get_parent().get_gameObject().SetActive(true);
		}
	}
}
