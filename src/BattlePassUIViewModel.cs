using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePassUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Passtime = "Passtime";

		public const string Attr_Items = "Items";

		public const string Attr_Item2s = "Item2s";

		public const string Event_OnBtnExitUp = "OnBtnExitUp";

		public const string Event_OnBtnAgainUp = "OnBtnAgainUp";

		public const string Event_OnBtnStatisticsUp = "OnBtnStatisticsUp";

		public const string BtnAgain_Visibility = "BtnAgainVisibility";

		public const string BtnStatistics_Visibility = "BtnStatisticsVisibility";
	}

	public static BattlePassUIViewModel Instance;

	private long _ObtainCoins;

	private long _ObtainExps;

	public Action againCallback;

	public Action exitCallback;

	[SetProperty("Passtime"), SerializeField]
	private string _Passtime;

	public ObservableCollection<OOBattlePassUIDropItem> Items = new ObservableCollection<OOBattlePassUIDropItem>();

	public ObservableCollection<OOBattlePassUIDropItem> Item2s = new ObservableCollection<OOBattlePassUIDropItem>();

	private bool _BtnAgainVisibility = true;

	private bool _BtnStatisticsVisibility = true;

	private List<string> descs = new List<string>();

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

	public bool BtnAgainVisibility
	{
		get
		{
			return this._BtnAgainVisibility;
		}
		set
		{
			this._BtnAgainVisibility = value;
			base.NotifyProperty("BtnAgainVisibility", value);
		}
	}

	public bool BtnStatisticsVisibility
	{
		get
		{
			return this._BtnStatisticsVisibility;
		}
		set
		{
			this._BtnStatisticsVisibility = value;
			base.NotifyProperty("BtnStatisticsVisibility", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		BattlePassUIViewModel.Instance = this;
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
		BattlePassUIView.Instance.Show(false);
		if (this.exitCallback != null)
		{
			this.exitCallback.Invoke();
		}
	}

	public void OnBtnAgainUp()
	{
		if (this.againCallback != null)
		{
			ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(InstanceManager.CurrentInstanceDataID);
			if (zhuXianPeiZhi == null)
			{
				return;
			}
			if (DungeonManager.Instance.GetDungeonInfo(InstanceManager.CurrentInstanceDataID).remainingChallengeTimes == 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505067, false));
				return;
			}
			if (EntityWorld.Instance.EntSelf.Energy < zhuXianPeiZhi.expendVit)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505120, false));
				return;
			}
			this.againCallback.Invoke();
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
		this.SetDropItems();
		this.SetConsummation();
		this.SetPassTime();
	}

	public void SetConsummation(List<uint> reachIds)
	{
		this.descs.Clear();
		if (reachIds != null)
		{
			for (int i = 0; i < reachIds.get_Count(); i++)
			{
				this.SetStar((int)reachIds.get_Item(i));
			}
			List<int> list = new List<int>();
			if (InstanceManager.CurrentInstanceData != null)
			{
				list = DataReader<ZhuXianPeiZhi>.Get(InstanceManager.CurrentInstance.InstanceDataID).star;
			}
			for (int j = 0; j < list.get_Count(); j++)
			{
				int num = list.get_Item(j);
				if (!reachIds.Contains((uint)num))
				{
					this.SetStar(num);
				}
			}
			BattlePassUIView.Instance.SetConsummation(reachIds.get_Count());
		}
	}

	private void SetConsummation()
	{
		this.descs.Clear();
		List<uint> list = new List<uint>();
		if (InstanceManager.CurrentInstance.Type == InstanceType.DungeonNormal)
		{
			if (DungeonManager.Instance.ChallengeData != null)
			{
				list = DungeonManager.Instance.ChallengeData.condIdsPassed;
			}
		}
		else if (InstanceManager.CurrentInstance.Type == InstanceType.DungeonElite)
		{
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			this.SetStar((int)list.get_Item(i));
		}
		List<int> list2 = new List<int>();
		if (InstanceManager.CurrentInstanceData != null)
		{
			ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(InstanceManager.CurrentInstance.InstanceDataID);
			if (zhuXianPeiZhi != null)
			{
				list2 = zhuXianPeiZhi.star;
			}
		}
		for (int j = 0; j < list2.get_Count(); j++)
		{
			int num = list2.get_Item(j);
			if (!list.Contains((uint)num))
			{
				this.SetStar(num);
			}
		}
		BattlePassUIView.Instance.SetConsummation(list.get_Count());
	}

	private void SetStar(int starId)
	{
		DungeonStarLv dungeonStarLv = DataReader<DungeonStarLv>.Get(starId);
		if (dungeonStarLv != null)
		{
			this.descs.Add(GameDataUtils.GetChineseContent(dungeonStarLv.introduction, false));
		}
	}

	private void SetDropItems()
	{
		this.Items.Clear();
		this.Item2s.Clear();
		this.ObtainCoins = 0L;
		this.ObtainExps = 0L;
		List<ItemBriefInfo> dropGoods = GlobalManager.Instance.DropGoods;
		if (dropGoods == null)
		{
			return;
		}
		List<ItemBriefInfo> list = new List<ItemBriefInfo>();
		for (int i = 0; i < dropGoods.get_Count(); i++)
		{
			int cfgId = dropGoods.get_Item(i).cfgId;
			long count = dropGoods.get_Item(i).count;
			Items item = BackpackManager.Instance.GetItem(cfgId);
			if (item != null)
			{
				if (item.secondType == 15)
				{
					this.ObtainCoins = count;
				}
				else if (item.secondType == 16)
				{
					this.ObtainExps = count;
				}
				else
				{
					list.Add(dropGoods.get_Item(i));
				}
			}
		}
		int num = list.get_Count() / 2;
		for (int j = 0; j < list.get_Count(); j++)
		{
			int cfgId2 = list.get_Item(j).cfgId;
			long count2 = list.get_Item(j).count;
			Items item2 = BackpackManager.Instance.GetItem(cfgId2);
			OOBattlePassUIDropItem o = new OOBattlePassUIDropItem
			{
				ItemId = item2.id,
				Icon = GameDataUtils.GetIcon(item2.icon),
				IconBg = GameDataUtils.GetItemFrame(item2.id),
				GoodNum = count2.ToString()
			};
			if (list.get_Count() <= 4)
			{
				this.Items.Add(o);
			}
			else if (j < num)
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
}
