using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementUI : UIBase
{
	public ListView2 list;

	private List<HuoYueDu> activis;

	private int getBoxed;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void start()
	{
		this.UpdateDataUI();
		base.FindTransform("ScrollRect").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
	}

	protected override void OnEnable()
	{
		base.FindTransform("ScrollRect").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
		this.list.DoAnimation();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.RefreshAchievementInfo, new Callback(this.UpdateDataUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.RefreshAchievementInfo, new Callback(this.UpdateDataUI));
	}

	public override void UpdateDataUI()
	{
		Dictionary<int, AchievementItemInfo> allIdList = AchievementManager.Instance.AllIdList;
		int count = allIdList.get_Count();
		this.list.CreateRow(count, 0);
		int num = 0;
		using (Dictionary<int, AchievementItemInfo>.KeyCollection.Enumerator enumerator = allIdList.get_Keys().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				this.list.Items.get_Item(num).GetComponent<AchievementItem>().UpdateItem(allIdList.get_Item(current).achievementId);
				num++;
			}
		}
	}
}
