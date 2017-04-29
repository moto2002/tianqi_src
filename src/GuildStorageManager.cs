using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class GuildStorageManager : BaseSubSystemManager
{
	private GuildStorageInfo guildStorageInfo;

	private PersonalInfo guildStoragePersonalInfo;

	private static GuildStorageManager instance;

	public GuildStorageInfo GuildStorageInfo
	{
		get
		{
			return this.guildStorageInfo;
		}
		set
		{
			this.guildStorageInfo = value;
		}
	}

	public PersonalInfo GuildStoragePersonalInfo
	{
		get
		{
			return this.guildStoragePersonalInfo;
		}
		set
		{
			this.guildStoragePersonalInfo = value;
		}
	}

	public static GuildStorageManager Instance
	{
		get
		{
			if (GuildStorageManager.instance == null)
			{
				GuildStorageManager.instance = new GuildStorageManager();
			}
			return GuildStorageManager.instance;
		}
	}

	private GuildStorageManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.guildStorageInfo = null;
		this.guildStoragePersonalInfo = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuildStorageChangeNty>(new NetCallBackMethod<GuildStorageChangeNty>(this.OnGuildStorageChangeNty));
		NetworkManager.AddListenEvent<QueryGuildStorageInfoRes>(new NetCallBackMethod<QueryGuildStorageInfoRes>(this.OnQueryGuildStorageInfoRes));
		NetworkManager.AddListenEvent<GuildStorageExchangeRes>(new NetCallBackMethod<GuildStorageExchangeRes>(this.OnGuildStorageExchangeRes));
		NetworkManager.AddListenEvent<GuildStorageDonateEquipRes>(new NetCallBackMethod<GuildStorageDonateEquipRes>(this.OnGuildStorageDonateEquipRes));
		NetworkManager.AddListenEvent<GuildStorageDecomposeEquipRes>(new NetCallBackMethod<GuildStorageDecomposeEquipRes>(this.OnGuildStorageDecomposeEquipRes));
	}

	private void OnGuildStorageChangeNty(short state, GuildStorageChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.GuildStorageInfo = down.storageInfos;
			this.GuildStoragePersonalInfo = down.personalInfo;
			EventDispatcher.Broadcast(EventNames.OnQueryGuildStorageInfoRes);
		}
	}

	private void OnQueryGuildStorageInfoRes(short state, QueryGuildStorageInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.GuildStorageInfo = down.storageInfos;
			this.GuildStoragePersonalInfo = down.personalInfo;
			EventDispatcher.Broadcast(EventNames.OnQueryGuildStorageInfoRes);
		}
	}

	private void OnGuildStorageExchangeRes(short state, GuildStorageExchangeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText("兑换成功！");
	}

	private void OnGuildStorageDonateEquipRes(short state, GuildStorageDonateEquipRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText("捐献成功！");
		if (down != null && down.donateEquipIds != null)
		{
			EquipmentManager.Instance.RemoveGuildStorageDonateEquips(down.donateEquipIds);
		}
	}

	private void OnGuildStorageDecomposeEquipRes(short state, GuildStorageDecomposeEquipRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText("分解成功！");
	}

	public void SendQueryGuildStorageInfoReq()
	{
		NetworkManager.Send(new QueryGuildStorageInfoReq(), ServerType.Data);
	}

	public void SendGuildStorageExchangeReq(EquipBriefInfo m_equip, ItemBriefInfo m_item)
	{
		GuildStorageExchangeReq guildStorageExchangeReq = new GuildStorageExchangeReq();
		if (m_equip != null)
		{
			guildStorageExchangeReq.equip = m_equip;
		}
		if (m_item != null)
		{
			guildStorageExchangeReq.item = m_item;
		}
		NetworkManager.Send(guildStorageExchangeReq, ServerType.Data);
	}

	public void SendGuildStorageDonateEquipReq(List<EquipBriefInfo> m_equips)
	{
		GuildStorageDonateEquipReq guildStorageDonateEquipReq = new GuildStorageDonateEquipReq();
		if (m_equips != null)
		{
			guildStorageDonateEquipReq.equips.AddRange(m_equips);
		}
		NetworkManager.Send(guildStorageDonateEquipReq, ServerType.Data);
	}

	public void SendGuildStorageDecomposeEquipReq(List<EquipBriefInfo> m_equips)
	{
		if (m_equips == null || m_equips.get_Count() <= 0)
		{
			return;
		}
		GuildStorageDecomposeEquipReq guildStorageDecomposeEquipReq = new GuildStorageDecomposeEquipReq();
		guildStorageDecomposeEquipReq.equips.AddRange(m_equips);
		NetworkManager.Send(guildStorageDecomposeEquipReq, ServerType.Data);
	}

	public List<GuildLogTrace> GetGuildStorageLogList(List<GuildLogTrace> guildStorageLogList)
	{
		List<GuildLogTrace> result = new List<GuildLogTrace>();
		if (guildStorageLogList != null)
		{
			guildStorageLogList.Sort(new Comparison<GuildLogTrace>(GuildStorageManager.GuildStorageLogListSort));
			return guildStorageLogList;
		}
		return result;
	}

	private static int GuildStorageLogListSort(GuildLogTrace GL1, GuildLogTrace GL12)
	{
		if (GL1.logTimeUtc > GL12.logTimeUtc)
		{
			return -1;
		}
		return 1;
	}

	public List<EquipSimpleInfo> GetSelectDecomposeEquips(List<int> ids)
	{
		List<EquipSimpleInfo> list = new List<EquipSimpleInfo>();
		for (int i = 0; i < ids.get_Count(); i++)
		{
			list.AddRange(this.GetSelectDecomposeEquipByID(ids.get_Item(i)));
		}
		return list;
	}

	public List<EquipSimpleInfo> GetSelectDecomposeEquipByID(int id)
	{
		int num = 3;
		int num2 = 6;
		switch (id)
		{
		case 1:
			num = 2;
			num2 = 5;
			break;
		case 2:
			num = 3;
			num2 = 5;
			break;
		case 3:
			num = 2;
			num2 = 6;
			break;
		case 4:
			num = 3;
			num2 = 6;
			break;
		}
		List<EquipSimpleInfo> list = new List<EquipSimpleInfo>();
		if (this.GuildStorageInfo != null && this.GuildStorageInfo.equipsInfo != null)
		{
			for (int i = 0; i < this.GuildStorageInfo.equipsInfo.get_Count(); i++)
			{
				EquipSimpleInfo equipSimpleInfo = this.GuildStorageInfo.equipsInfo.get_Item(i);
				int num3 = 0;
				int num4 = 0;
				if (equipSimpleInfo.excellentAttrs != null)
				{
					for (int j = 0; j < equipSimpleInfo.excellentAttrs.get_Count(); j++)
					{
						if (equipSimpleInfo.excellentAttrs.get_Item(j).attrId > 0 && equipSimpleInfo.excellentAttrs.get_Item(j).color >= 1f)
						{
							num3++;
						}
					}
				}
				Items items = DataReader<Items>.Get(equipSimpleInfo.cfgId);
				if (items != null)
				{
					num4 = items.color;
				}
				if (num3 == num && num4 == num2)
				{
					list.Add(equipSimpleInfo);
				}
			}
		}
		return list;
	}

	public int GetDecomposeEquipTotalGuildFund(List<int> equipCfgList)
	{
		int num = 0;
		if (equipCfgList != null && equipCfgList.get_Count() > 0)
		{
			for (int i = 0; i < equipCfgList.get_Count(); i++)
			{
				if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgList.get_Item(i)))
				{
					zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgList.get_Item(i));
					num += zZhuangBeiPeiZhiBiao.breakDownValue;
				}
			}
		}
		return num;
	}

	public int GetEquipsTotalPoint(List<int> equipCfgList, bool isDonation = true)
	{
		int num = 0;
		if (equipCfgList != null && equipCfgList.get_Count() > 0)
		{
			for (int i = 0; i < equipCfgList.get_Count(); i++)
			{
				if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgList.get_Item(i)))
				{
					zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgList.get_Item(i));
					if (isDonation && zZhuangBeiPeiZhiBiao.donationAndExchange != null && zZhuangBeiPeiZhiBiao.donationAndExchange.get_Count() >= 1)
					{
						num += zZhuangBeiPeiZhiBiao.donationAndExchange.get_Item(0);
					}
					else if (!isDonation && zZhuangBeiPeiZhiBiao.donationAndExchange != null && zZhuangBeiPeiZhiBiao.donationAndExchange.get_Count() >= 2)
					{
						num += zZhuangBeiPeiZhiBiao.donationAndExchange.get_Item(1);
					}
				}
			}
		}
		return num;
	}

	public int GetSpecialItemChangePoint()
	{
		if (DataReader<GongHuiXinXi>.Contains("wareHouseProps"))
		{
			string value = DataReader<GongHuiXinXi>.Get("wareHouseProps").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			if (value != null && value.get_Length() >= 3)
			{
				return (int)float.Parse(array[2]);
			}
		}
		return 0;
	}
}
