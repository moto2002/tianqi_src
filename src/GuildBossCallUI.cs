using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;

public class GuildBossCallUI : UIBase
{
	private ListPool bossItemsListPool;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.bossItemsListPool = base.FindTransform("BossItemRegion").GetComponent<ListPool>();
		this.bossItemsListPool.SetItem("GuildBossItem");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.bossItemsListPool.Clear();
		this.UpdateBossItems();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.bossItemsListPool.Clear();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnCallGuildBossRes, new Callback(this.OnCallGuildBossRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnCallGuildBossRes, new Callback(this.OnCallGuildBossRes));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void UpdateBossItems()
	{
		this.bossItemsListPool.Clear();
		List<JunTuanBOSSMoXing> bossCfgList = DataReader<JunTuanBOSSMoXing>.DataList;
		int num = bossCfgList.get_Count();
		this.bossItemsListPool.Create(num, delegate(int index)
		{
			if (index < num && index < this.bossItemsListPool.Items.get_Count())
			{
				GuildBossItem component = this.bossItemsListPool.Items.get_Item(index).GetComponent<GuildBossItem>();
				if (component != null)
				{
					component.UpdateBossItemData(bossCfgList.get_Item(index));
				}
			}
		});
	}

	private void OnCallGuildBossRes()
	{
		this.Show(false);
	}
}
