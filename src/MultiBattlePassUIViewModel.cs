using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiBattlePassUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Passtime = "Passtime";

		public const string Attr_CoundDownText = "CoundDownText";

		public const string Attr_Items = "Items";

		public const string Attr_Item2s = "Item2s";

		public const string Attr_ShowBtnStatistics = "ShowBtnStatistics";

		public const string Attr_ShowBtnNext = "ShowBtnNext";

		public const string Attr_ShowCountDownText = "ShowCountDownText";

		public const string Event_OnBtnExitUp = "OnBtnExitUp";

		public const string Event_OnBtnStatisticsUp = "OnBtnStatisticsUp";
	}

	public static MultiBattlePassUIViewModel Instance;

	private long _ObtainCoins;

	private long _ObtainExps;

	public Action againCallback;

	public Action exitCallback;

	[SetProperty("Passtime"), SerializeField]
	private string _Passtime;

	private string _CoundDownText;

	private bool _ShowBtnStatistics;

	private bool _ShowBtnNext;

	private bool _ShowCountDownText;

	public ObservableCollection<OOBattlePassUIDropItem> Items = new ObservableCollection<OOBattlePassUIDropItem>();

	public ObservableCollection<OOBattlePassUIDropItem> Item2s = new ObservableCollection<OOBattlePassUIDropItem>();

	private TimeCountDown timeCountDown;

	public long ObtainCoins
	{
		get
		{
			return this._ObtainCoins;
		}
		set
		{
			this._ObtainCoins = value;
		}
	}

	public long ObtainExps
	{
		get
		{
			return this._ObtainExps;
		}
		set
		{
			this._ObtainExps = value;
		}
	}

	public string Passtime
	{
		get
		{
			return this._Passtime;
		}
		set
		{
			this._Passtime = value;
			base.NotifyProperty("Passtime", value);
		}
	}

	public string CoundDownText
	{
		get
		{
			return this._CoundDownText;
		}
		set
		{
			this._CoundDownText = value;
			base.NotifyProperty("CoundDownText", value);
		}
	}

	public bool ShowBtnStatistics
	{
		get
		{
			return this._ShowBtnStatistics;
		}
		set
		{
			this._ShowBtnStatistics = value;
			base.NotifyProperty("ShowBtnStatistics", value);
		}
	}

	public bool ShowBtnNext
	{
		get
		{
			return this._ShowBtnNext;
		}
		set
		{
			this._ShowBtnNext = value;
			base.NotifyProperty("ShowBtnNext", value);
		}
	}

	public bool ShowCountDownText
	{
		get
		{
			return this._ShowCountDownText;
		}
		set
		{
			this._ShowCountDownText = value;
			base.NotifyProperty("ShowCountDownText", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		MultiBattlePassUIViewModel.Instance = this;
	}

	private void OnEnable()
	{
		Utils.WinSetting(true);
		this.UpdateData();
	}

	private void OnDisable()
	{
		Utils.WinSetting(false);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(DungeonManagerEvent.OnGetDropItems, new Callback(this.UpdateData));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(DungeonManagerEvent.OnGetDropItems, new Callback(this.UpdateData));
	}

	public void OnBtnExitUp()
	{
		LoadingUIView.Open(false);
		MultiBattlePassUIView.Instance.Show(false);
		if (this.exitCallback != null)
		{
			this.exitCallback.Invoke();
		}
	}

	public void OnBtnStatisticsUp()
	{
		InstanceDamageCal instanceDamageCal = UIManagerControl.Instance.OpenUI("InstanceDamageCal", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as InstanceDamageCal;
		instanceDamageCal.ResetUI();
	}

	public void UpdateData()
	{
		if (!base.get_isActiveAndEnabled())
		{
			return;
		}
		this.SetPassTime();
	}

	public void SetRewardItems(PassUICommonDrop commonDrop)
	{
		this.ObtainExps = commonDrop.exp;
		this.ObtainCoins = commonDrop.gold;
		this.SetRewardItem(commonDrop.item);
	}

	protected void SetRewardItem(List<KeyValuePair<int, long>> item)
	{
		this.Items.Clear();
		this.Item2s.Clear();
		if (item == null)
		{
			return;
		}
		int num = item.get_Count() / 2;
		for (int i = 0; i < item.get_Count(); i++)
		{
			int key = item.get_Item(i).get_Key();
			long value = item.get_Item(i).get_Value();
			Items item2 = BackpackManager.Instance.GetItem(key);
			OOBattlePassUIDropItem o = new OOBattlePassUIDropItem
			{
				ItemId = item2.id,
				Icon = GameDataUtils.GetIcon(item2.icon),
				IconBg = GameDataUtils.GetItemFrame(item2.id),
				GoodNum = value.ToString()
			};
			if (item.get_Count() <= 4)
			{
				this.Items.Add(o);
			}
			else if (i < num)
			{
				this.Items.Add(o);
			}
			else
			{
				this.Item2s.Add(o);
			}
		}
	}

	private void SetRewardItem(List<DropItem> itemList)
	{
		this.Items.Clear();
		this.Item2s.Clear();
		if (itemList == null)
		{
			return;
		}
		int num = itemList.get_Count() / 2;
		for (int i = 0; i < itemList.get_Count(); i++)
		{
			int typeId = itemList.get_Item(i).typeId;
			long count = itemList.get_Item(i).count;
			Items item = BackpackManager.Instance.GetItem(typeId);
			OOBattlePassUIDropItem o = new OOBattlePassUIDropItem
			{
				ItemId = item.id,
				Icon = GameDataUtils.GetIcon(item.icon),
				IconBg = GameDataUtils.GetItemFrame(item.id),
				GoodNum = count.ToString()
			};
			if (itemList.get_Count() <= 4)
			{
				this.Items.Add(o);
			}
			else if (i < num)
			{
				this.Items.Add(o);
			}
			else
			{
				this.Item2s.Add(o);
			}
		}
	}

	private void SetPassTime()
	{
		int curUsedTime = InstanceManager.CurUsedTime;
		this.Passtime = GameDataUtils.GetChineseContent(501004, false) + " " + TimeConverter.GetTime(curUsedTime, TimeFormat.HHMMSS);
	}

	public void OnCountDown(int countTime, Action countDownEndAction)
	{
		this.timeCountDown = new TimeCountDown(countTime, TimeFormat.SECOND, delegate
		{
			this.CoundDownText = string.Format("<color=green>" + this.timeCountDown.GetSeconds() + "秒</color>", new object[0]) + "后自动离开";
		}, delegate
		{
			if (countDownEndAction != null)
			{
				countDownEndAction.Invoke();
			}
		}, true);
	}

	public void StopCountDown()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}
}
