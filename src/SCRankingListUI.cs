using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;

public class SCRankingListUI : UIBase
{
	public ListView2 list;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.SCUpdateList();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SCUpdateList, new Callback(this.SCUpdateList));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SCUpdateList, new Callback(this.SCUpdateList));
	}

	private void SCUpdateList()
	{
		List<SecretAreaRankInfo> scRankingInfo = SurvivalManager.Instance.ScRankingInfo;
		this.list.CreateRow(scRankingInfo.get_Count(), 0);
		for (int i = 0; i < scRankingInfo.get_Count(); i++)
		{
			this.list.Items.get_Item(i).GetComponent<SCRankItem>().UpdateItem(scRankingInfo.get_Item(i));
		}
	}
}
