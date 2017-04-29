using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WingSelectUI : UIBase
{
	public static WingSelectUI Instance;

	private List<wings> m_listDataWings;

	private ListPool m_listPool;

	private void Awake()
	{
		WingSelectUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_listPool = base.FindTransform("Items").GetComponent<ListPool>();
		this.m_listPool.SetItem("WingCell");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.InitScrollRect();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		WingSelectUI.Instance = null;
		base.ReleaseSelf(true);
	}

	public void OnClickOneWing(int wingId)
	{
		if (WingManager.GetWingLv(wingId) == 0)
		{
			if (WingManager.IsCanActiveWing(wingId))
			{
				WingManager.Instance.SendWingComposeReq(wingId);
			}
			else
			{
				WingPreviewOneUI wingPreviewOneUI = UIManagerControl.Instance.OpenUI("WingPreviewOneUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as WingPreviewOneUI;
				wingPreviewOneUI.InitWithNotActive(wingId);
				wingPreviewOneUI.get_transform().SetAsLastSibling();
			}
		}
		else
		{
			WingPreviewOneUI wingPreviewOneUI2 = UIManagerControl.Instance.OpenUI("WingPreviewOneUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as WingPreviewOneUI;
			wingPreviewOneUI2.get_transform().SetAsLastSibling();
			if (wingId == EntityWorld.Instance.EntSelf.Decorations.wingId)
			{
				wingPreviewOneUI2.InitWithUndress(wingId);
				wingPreviewOneUI2.get_transform().SetAsLastSibling();
			}
			else
			{
				wingPreviewOneUI2.InitWithWear(wingId);
				wingPreviewOneUI2.get_transform().SetAsLastSibling();
			}
		}
	}

	public void RefreshWings()
	{
		this.SortWingList();
		int num = 0;
		while (num < this.m_listPool.Items.get_Count() && num < this.m_listDataWings.get_Count())
		{
			wings dataWings = this.m_listDataWings.get_Item(num);
			WingCell component = this.m_listPool.Items.get_Item(num).GetComponent<WingCell>();
			component.RefreshWing(dataWings);
			num++;
		}
	}

	public void PlayActiveSuccess(int wingId)
	{
		for (int i = 0; i < this.m_listPool.Items.get_Count(); i++)
		{
			WingCell component = this.m_listPool.Items.get_Item(i).GetComponent<WingCell>();
			if (component.wingId == wingId)
			{
				component.PlayActiveSuccess();
				return;
			}
		}
	}

	private void InitScrollRect()
	{
		int num = false.CompareTo(true);
		this.SortWingList();
		this.m_listPool.Create(this.m_listDataWings.get_Count(), delegate(int index)
		{
			if (index < this.m_listDataWings.get_Count() && index < this.m_listPool.Items.get_Count())
			{
				wings dataWings = this.m_listDataWings.get_Item(index);
				WingCell component = this.m_listPool.Items.get_Item(index).GetComponent<WingCell>();
				component.RefreshWing(dataWings);
			}
		});
	}

	private void SortWingList()
	{
		this.m_listDataWings = WingManager.GetSelectWingInfos();
		this.m_listDataWings.Sort(delegate(wings a, wings b)
		{
			int wingLv = WingManager.GetWingLv(a.id);
			int wingLv2 = WingManager.GetWingLv(b.id);
			if (wingLv != wingLv2)
			{
				return -wingLv.CompareTo(wingLv2);
			}
			bool flag = WingSelectUI.IsTimeLimitWing(a.id);
			bool flag2 = WingSelectUI.IsTimeLimitWing(b.id);
			if (flag != flag2)
			{
				return -flag.CompareTo(flag2);
			}
			return a.id.CompareTo(b.id);
		});
	}

	public static bool IsWingExpire(int wingId)
	{
		if (!WingSelectUI.IsTimeLimitWing(wingId))
		{
			return false;
		}
		int num = WingSelectUI.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
		return num >= WingManager.wingInfoDict.get_Item(wingId).overdueUtc;
	}

	public static bool IsTimeLimitWing(int wingId)
	{
		return WingManager.GetWingInfo(wingId).time > 0;
	}

	private static int ToTimeStamp(DateTime time)
	{
		DateTime dateTime = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
		return (int)(time - dateTime).get_TotalSeconds();
	}

	public static string GetWingRemainTime(int wingId)
	{
		int num = WingSelectUI.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
		int num2 = Mathf.Max(WingManager.wingInfoDict.get_Item(wingId).overdueUtc - num, 0);
		int num3 = num2 / 3600;
		int num4 = num2 % 3600 / 60;
		return string.Concat(new object[]
		{
			num3,
			"小时",
			num4,
			"分后消失"
		});
	}
}
