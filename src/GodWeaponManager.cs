using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GodWeaponManager : BaseSubSystemManager
{
	private bool mWeaponLock;

	public bool GodWeaponLock;

	private List<HolyWeaponInfo> mWeaponList;

	private Dictionary<int, List<HolyWeaponInfo>> mWeaponDict;

	public int HasWeaponTaskFinish;

	public int OpenDescId;

	public Queue<int> TownPlayQueue = new Queue<int>();

	public Queue<int> UIPlayQueue = new Queue<int>();

	public readonly int[] EQUIP_TYPE = new int[]
	{
		1,
		4,
		5,
		6
	};

	private static GodWeaponManager instance;

	public bool WeaponLock
	{
		get
		{
			return this.mWeaponLock;
		}
		set
		{
			this.mWeaponLock = value;
		}
	}

	public List<HolyWeaponInfo> WeaponList
	{
		get
		{
			return this.mWeaponList;
		}
	}

	public Dictionary<int, List<HolyWeaponInfo>> WeaponDict
	{
		get
		{
			return this.mWeaponDict;
		}
	}

	public static GodWeaponManager Instance
	{
		get
		{
			if (GodWeaponManager.instance == null)
			{
				GodWeaponManager.instance = new GodWeaponManager();
			}
			return GodWeaponManager.instance;
		}
	}

	private GodWeaponManager()
	{
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<HolyWeaponPush>(new NetCallBackMethod<HolyWeaponPush>(this.OnHolyWeaponPush));
		NetworkManager.AddListenEvent<GetHolyWeaponsInfoRes>(new NetCallBackMethod<GetHolyWeaponsInfoRes>(this.OnGetHolyWeaponsInfoRes));
		EventDispatcher.AddListener<bool>(EventNames.DungeonResultNty, new Callback<bool>(this.OnExitWeaponInstanceNty));
	}

	public void SendGetGodWeaponInfos()
	{
		NetworkManager.Send(new GetHolyWeaponsInfoReq(), ServerType.Data);
		Debug.Log("<请求获取神器列表!!!>");
	}

	private void OnGetHolyWeaponsInfoRes(short state, GetHolyWeaponsInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.mWeaponList = down.Info;
		this.mWeaponDict = new Dictionary<int, List<HolyWeaponInfo>>();
		List<HolyWeaponInfo> list = null;
		for (int i = 0; i < this.mWeaponList.get_Count(); i++)
		{
			HolyWeaponInfo holyWeaponInfo = this.mWeaponList.get_Item(i);
			if (holyWeaponInfo.Id > 0)
			{
				if (!this.mWeaponDict.TryGetValue(holyWeaponInfo.Type, ref list))
				{
					list = new List<HolyWeaponInfo>();
					this.mWeaponDict.Add(holyWeaponInfo.Type, list);
				}
				list.Add(holyWeaponInfo);
			}
		}
		List<int> list2 = new List<int>(this.mWeaponDict.get_Keys());
		for (int j = 0; j < list2.get_Count(); j++)
		{
			this.mWeaponDict.get_Item(list2.get_Item(j)).Sort(new Comparison<HolyWeaponInfo>(this.SortInfo));
		}
		EventDispatcher.Broadcast(EventNames.GetGodWeaponListRes);
	}

	private void OnHolyWeaponPush(short state, HolyWeaponPush down = null)
	{
		GodWeaponManager.<OnHolyWeaponPush>c__AnonStorey112 <OnHolyWeaponPush>c__AnonStorey = new GodWeaponManager.<OnHolyWeaponPush>c__AnonStorey112();
		<OnHolyWeaponPush>c__AnonStorey.down = down;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (<OnHolyWeaponPush>c__AnonStorey.down == null)
		{
			return;
		}
		if (this.mWeaponList == null)
		{
			Debug.LogError("神器总列表还没推送就推送变化了，找后端！！！");
			return;
		}
		int i;
		for (i = 0; i < <OnHolyWeaponPush>c__AnonStorey.down.weapon.get_Count(); i++)
		{
			HolyWeaponInfo holyWeaponInfo = this.mWeaponList.Find((HolyWeaponInfo e) => e.Id == <OnHolyWeaponPush>c__AnonStorey.down.weapon.get_Item(i).Id);
			if (holyWeaponInfo != null)
			{
				holyWeaponInfo.State = <OnHolyWeaponPush>c__AnonStorey.down.weapon.get_Item(i).State;
				if (holyWeaponInfo.State == 2)
				{
					EventDispatcher.Broadcast<HolyWeaponInfo>(EventNames.GotGodWeaponNty, holyWeaponInfo);
					Artifact artifact = DataReader<Artifact>.Get(holyWeaponInfo.Id);
					if (artifact != null)
					{
						if (artifact.areaIndex > 0 && !this.TownPlayQueue.Contains(holyWeaponInfo.Id))
						{
							this.TownPlayQueue.Enqueue(holyWeaponInfo.Id);
						}
						if (UIManagerControl.Instance.IsOpen("TownUI"))
						{
							EventDispatcher.Broadcast(EventNames.PlayTownUIWeaponEffect);
						}
						if (!this.UIPlayQueue.Contains(holyWeaponInfo.Id))
						{
							this.UIPlayQueue.Enqueue(holyWeaponInfo.Id);
						}
						if (UIManagerControl.Instance.IsOpen("GodWeaponUI"))
						{
							EventDispatcher.Broadcast(EventNames.PlayGodWeaponUIEffect);
						}
					}
				}
				Debug.Log(string.Concat(new object[]
				{
					"<神器[<color=white>",
					holyWeaponInfo.Id,
					"</color>]状态变更[<color=white>",
					(GodWeaponItem.State)holyWeaponInfo.State,
					"</color>]>"
				}));
			}
		}
	}

	private int SortInfo(HolyWeaponInfo a, HolyWeaponInfo b)
	{
		Artifact artifact = DataReader<Artifact>.Get(a.Id);
		Artifact artifact2 = DataReader<Artifact>.Get(b.Id);
		if (artifact == null || artifact2 == null)
		{
			return 0;
		}
		if (artifact.priority < artifact2.priority)
		{
			return -1;
		}
		return 1;
	}

	private List<zZhuangBeiPeiZhiBiao> GetEquipData(List<float> list)
	{
		List<zZhuangBeiPeiZhiBiao> list2 = new List<zZhuangBeiPeiZhiBiao>();
		for (int i = 0; i < list.get_Count(); i++)
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get((int)list.get_Item(i));
			if (zZhuangBeiPeiZhiBiao != null)
			{
				list2.Add(zZhuangBeiPeiZhiBiao);
			}
		}
		return list2;
	}

	private void OnExitWeaponInstanceNty(bool isWin)
	{
		if (!isWin && DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.WEAPON)
		{
			TimerHeap.AddTimer(3000u, 0, new Action(this.ExitInstance));
		}
	}

	private void ExitInstance()
	{
		DungeonManager.Instance.SendExitDungeonReq();
	}

	public bool CheckEquipLevelAndQuality(int id)
	{
		TuiJianZhuangBei tuiJianZhuangBei = DataReader<TuiJianZhuangBei>.Get(id);
		return tuiJianZhuangBei != null && (long)tuiJianZhuangBei.recommendedPower <= EntityWorld.Instance.EntSelf.Fighting;
	}

	public List<zZhuangBeiPeiZhiBiao> GetRecommendEquipsData(int id)
	{
		return this.GetRecommendEquipsData(DataReader<TuiJianZhuangBei>.Get(id));
	}

	public List<zZhuangBeiPeiZhiBiao> GetRecommendEquipsData(TuiJianZhuangBei data)
	{
		if (data != null)
		{
			switch (EntityWorld.Instance.EntSelf.TypeID)
			{
			case 4:
				return this.GetEquipData(data.recommended4);
			case 7:
				return this.GetEquipData(data.recommended7);
			case 8:
				return this.GetEquipData(data.recommended8);
			}
		}
		return null;
	}

	public void ChallengeDungeon(int battle, int weaponModelId)
	{
		DungeonManager.Instance.DungeonInstanceType = DungeonManager.InsType.WEAPON;
		DungeonManager.Instance.WeaponModelId = weaponModelId;
		DungeonManager.Instance.SendChallengeDungeonReq(battle);
		UIStackManager.Instance.ClearPush();
	}
}
