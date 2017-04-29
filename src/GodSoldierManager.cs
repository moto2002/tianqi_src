using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class GodSoldierManager : BaseSubSystemManager
{
	private List<GodWeaponInfo> mGodList;

	private Dictionary<int, Dictionary<int, SShenBingDengJi>> mTypeLevelDict;

	private static GodSoldierManager instance;

	public List<GodWeaponInfo> GodList
	{
		get
		{
			return this.mGodList;
		}
	}

	public int LastUpGradeType
	{
		get;
		private set;
	}

	public Dictionary<int, Dictionary<int, SShenBingDengJi>> DengjiDict
	{
		get
		{
			return this.mTypeLevelDict;
		}
	}

	public bool WaitUpGradeRes
	{
		get;
		private set;
	}

	public static GodSoldierManager Instance
	{
		get
		{
			if (GodSoldierManager.instance == null)
			{
				GodSoldierManager.instance = new GodSoldierManager();
			}
			return GodSoldierManager.instance;
		}
	}

	private GodSoldierManager()
	{
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GodWeaponLoginPush>(new NetCallBackMethod<GodWeaponLoginPush>(this.OnGodWeaponLoginPush));
		NetworkManager.AddListenEvent<UpGodWeaponLvRes>(new NetCallBackMethod<UpGodWeaponLvRes>(this.OnUpGodWeaponLvRes));
	}

	private void OnGodWeaponLoginPush(short state, GodWeaponLoginPush down = null)
	{
		this.WaitUpGradeRes = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.mTypeLevelDict == null || this.mTypeLevelDict.get_Count() != down.weaponInfo.get_Count())
			{
				this.mTypeLevelDict = new Dictionary<int, Dictionary<int, SShenBingDengJi>>();
				for (int i = 0; i < down.weaponInfo.get_Count(); i++)
				{
					this.mTypeLevelDict.Add(down.weaponInfo.get_Item(i).Type, new Dictionary<int, SShenBingDengJi>());
				}
				List<SShenBingDengJi> dataList = DataReader<SShenBingDengJi>.DataList;
				for (int j = 0; j < dataList.get_Count(); j++)
				{
					SShenBingDengJi sShenBingDengJi = dataList.get_Item(j);
					if (this.mTypeLevelDict.get_Item(sShenBingDengJi.id).ContainsKey(sShenBingDengJi.level))
					{
						this.mTypeLevelDict.get_Item(sShenBingDengJi.id).set_Item(sShenBingDengJi.level, sShenBingDengJi);
					}
					else
					{
						this.mTypeLevelDict.get_Item(sShenBingDengJi.id).Add(sShenBingDengJi.level, sShenBingDengJi);
					}
				}
			}
			this.mGodList = down.weaponInfo;
			EventDispatcher.Broadcast<int>(EventNames.GodSoldierListNty, this.LastUpGradeType);
		}
	}

	public void SendUpGradeGodSoldier(int type, int itemId)
	{
		if (this.WaitUpGradeRes)
		{
			return;
		}
		this.WaitUpGradeRes = true;
		this.LastUpGradeType = type;
		NetworkManager.Send(new UpGodWeaponLvReq
		{
			Type = type,
			material = itemId
		}, ServerType.Data);
	}

	private void OnUpGodWeaponLvRes(short state, UpGodWeaponLvRes down = null)
	{
		this.WaitUpGradeRes = false;
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			LinkNavigationManager.ItemNotEnoughToLink(7, true, null, true);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public bool CheckAllMaterial()
	{
		int count = DataReader<SShenBingPeiZhi>.DataList.get_Count();
		for (int i = 1; i <= count; i++)
		{
			if (this.CheckMaterialById(i))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckMaterialById(int id)
	{
		if (this.mTypeLevelDict == null || this.mGodList == null)
		{
			return false;
		}
		GodWeaponInfo godWeaponInfo = this.mGodList.Find((GodWeaponInfo e) => e.Type == id);
		if (godWeaponInfo == null)
		{
			return false;
		}
		if (!this.mTypeLevelDict.ContainsKey(godWeaponInfo.Type) || this.mTypeLevelDict.get_Item(godWeaponInfo.Type).get_Count() <= godWeaponInfo.gLevel)
		{
			return false;
		}
		SShenBingPeiZhi sShenBingPeiZhi = DataReader<SShenBingPeiZhi>.Get(id);
		if (sShenBingPeiZhi == null)
		{
			return false;
		}
		for (int i = 0; i < sShenBingPeiZhi.material.get_Count(); i++)
		{
			if (BackpackManager.Instance.OnGetGoodCount(sShenBingPeiZhi.material.get_Item(i)) > 0L)
			{
				return true;
			}
		}
		return false;
	}

	public long GetAllGodSoliderAttrValue()
	{
		long num = 0L;
		if (this.mGodList != null)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < this.mGodList.get_Count(); i++)
			{
				GodWeaponInfo godWeaponInfo = this.mGodList.get_Item(i);
				if (godWeaponInfo.isOpen && godWeaponInfo.gLevel > 0)
				{
					Attrs attrs = DataReader<Attrs>.Get(this.mTypeLevelDict.get_Item(godWeaponInfo.Type).get_Item(godWeaponInfo.gLevel).attrID);
					if (attrs != null)
					{
						for (int j = 0; j < attrs.attrs.get_Count(); j++)
						{
							list.Add(attrs.attrs.get_Item(j));
							list2.Add(attrs.values.get_Item(j));
						}
					}
				}
			}
			num += EquipGlobal.CalculateFightingByIDAndValue(list, list2);
		}
		return num;
	}
}
