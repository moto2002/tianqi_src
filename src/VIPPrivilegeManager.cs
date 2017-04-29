using GameData;
using System;
using System.Collections.Generic;

public class VIPPrivilegeManager
{
	public const string SP_TREASURE = "tequan8";

	public const string SP_BACKGROUND = "kuang";

	public const string SP_VIPDESC_BG = "tequan";

	private static VIPPrivilegeManager instance;

	public static VIPPrivilegeManager Instance
	{
		get
		{
			if (VIPPrivilegeManager.instance == null)
			{
				VIPPrivilegeManager.instance = new VIPPrivilegeManager();
			}
			return VIPPrivilegeManager.instance;
		}
	}

	private VIPPrivilegeManager()
	{
	}

	public void Init()
	{
	}

	public bool IsOpenSweepSeries()
	{
		bool result = false;
		if (EntityWorld.Instance.EntSelf != null)
		{
			VipXiaoGuo vipXiaoGuo = this.CheckOwnEffect(101, EntityWorld.Instance.EntSelf.VipLv);
			if (vipXiaoGuo != null)
			{
				result = true;
			}
		}
		return result;
	}

	public int GetPrizeTimes()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return 0;
		}
		int key = EntityWorld.Instance.EntSelf.VipLv;
		if (!VIPManager.Instance.IsVIPPrivilegeOn())
		{
			key = 0;
		}
		int num = 0;
		List<int> date = DataReader<JJingYingFuBenPeiZhi>.Get("prizeTimes").date;
		if (date != null)
		{
			num = date.get_Item(0);
		}
		VipDengJi vipDengJi = DataReader<VipDengJi>.Get(key);
		if (vipDengJi != null)
		{
			for (int i = 0; i < vipDengJi.effect.get_Count(); i++)
			{
				VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(vipDengJi.effect.get_Item(i));
				if (vipXiaoGuo != null)
				{
					if (vipXiaoGuo.type == 17)
					{
						num += vipXiaoGuo.value1;
					}
				}
			}
		}
		return num;
	}

	public int Effect2TreasureID(int effectId)
	{
		int result = 0;
		VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(effectId);
		if (vipXiaoGuo != null && vipXiaoGuo.type == 99)
		{
			result = vipXiaoGuo.value1;
		}
		return result;
	}

	public VipXiaoGuo VIP2FirstObtainTreasure(int vipLevel)
	{
		VipDengJi vipDengJi = DataReader<VipDengJi>.Get(vipLevel);
		if (vipDengJi == null)
		{
			return null;
		}
		List<int> effect = vipDengJi.effect;
		for (int i = 0; i < effect.get_Count(); i++)
		{
			VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(effect.get_Item(i));
			if (vipXiaoGuo != null && vipXiaoGuo.type == 99)
			{
				return vipXiaoGuo;
			}
		}
		return null;
	}

	public int GetVipTimesByType(int type)
	{
		int result = 0;
		if (EntityWorld.Instance.EntSelf != null)
		{
			VipXiaoGuo vipXiaoGuo = this.CheckOwnEffect(type, EntityWorld.Instance.EntSelf.VipLv);
			if (vipXiaoGuo != null)
			{
				result = vipXiaoGuo.value1;
			}
		}
		return result;
	}

	private VipXiaoGuo CheckOwnEffect(int type, int vipLevel)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return null;
		}
		VipXiaoGuo result = null;
		if (VIPManager.Instance.LimitCardData.Times > TimeManager.Instance.PreciseServerSecond)
		{
			result = this.GetVipEffect(type, vipLevel);
		}
		if (VipTasteCardManager.Instance.CardTime > TimeManager.Instance.PreciseServerSecond)
		{
			List<vipTiYanQia> dataList = DataReader<vipTiYanQia>.DataList;
			if (dataList == null || dataList.get_Count() == 0)
			{
				return result;
			}
			vipTiYanQia vipTiYanQia = dataList.get_Item(0);
			List<int> effect = vipTiYanQia.effect;
			for (int i = 0; i < effect.get_Count(); i++)
			{
				VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(effect.get_Item(i));
				if (vipXiaoGuo != null && vipXiaoGuo.type == type)
				{
					result = vipXiaoGuo;
				}
			}
		}
		return result;
	}

	public VipXiaoGuo GetVipEffect(int type, int vipLevel)
	{
		int num = 0;
		VipXiaoGuo result = null;
		for (int i = vipLevel; i >= 0; i--)
		{
			VipDengJi vipDengJi = DataReader<VipDengJi>.Get(i);
			if (vipDengJi != null)
			{
				List<int> effect = vipDengJi.effect;
				for (int j = 0; j < effect.get_Count(); j++)
				{
					VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(effect.get_Item(j));
					if (vipXiaoGuo != null && vipXiaoGuo.type == type)
					{
						if (num < i)
						{
							result = vipXiaoGuo;
							num = i;
						}
						break;
					}
				}
			}
		}
		return result;
	}

	public int GetMaxVipTimesByType(int type)
	{
		int result = 0;
		if (EntityWorld.Instance.EntSelf != null)
		{
			VipXiaoGuo vipEffect = this.GetVipEffect(type, VIPManager.Instance.MaxVipLevel);
			if (vipEffect != null)
			{
				result = vipEffect.value1;
			}
		}
		return result;
	}
}
