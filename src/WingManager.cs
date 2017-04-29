using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class WingManager : BaseSubSystemManager
{
	public const int WINGSYS_WINGID = 1;

	public static Dictionary<int, WingInfo> wingInfoDict = new Dictionary<int, WingInfo>();

	public static bool firstGetWingDetail;

	public static readonly WingManager Instance = new WingManager();

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		WingManager.wingInfoDict.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<WingInfoPush>(new NetCallBackMethod<WingInfoPush>(this.OnWingInfoPush));
		NetworkManager.AddListenEvent<WingInfoChangeNty>(new NetCallBackMethod<WingInfoChangeNty>(this.OnWingInfoChangeNty));
		NetworkManager.AddListenEvent<WingComposeRes>(new NetCallBackMethod<WingComposeRes>(this.RecvWingComposeRes));
		NetworkManager.AddListenEvent<WingUpLvRes>(new NetCallBackMethod<WingUpLvRes>(this.RecvWingUpLvRes));
		NetworkManager.AddListenEvent<WingWearRes>(new NetCallBackMethod<WingWearRes>(this.RecvWingWearRes));
		NetworkManager.AddListenEvent<WingHiddenRes>(new NetCallBackMethod<WingHiddenRes>(this.RecvWingHiddenRes));
		NetworkManager.AddListenEvent<GetWingDetailRes>(new NetCallBackMethod<GetWingDetailRes>(this.RecvGetWingDetailRes));
		EventDispatcher.AddListener<int, int, bool>(ExteriorArithmeticUnitEvent.WingChanged, new Callback<int, int, bool>(this.OnEquipWing));
	}

	protected void OnEquipWing(int wingId, int wingLv, bool isHidden)
	{
		WingUI.wingIdLast = wingId;
		WingUI wingUI = UIManagerControl.Instance.GetUIIfExist("WingUI") as WingUI;
		if (wingUI != null)
		{
			wingUI.Refresh();
		}
		WingUpgradeUI wingUpgradeUI = UIManagerControl.Instance.GetUIIfExist("WingUpgradeUI") as WingUpgradeUI;
		if (wingUpgradeUI != null)
		{
			wingUpgradeUI.Refresh();
		}
		WingSelectUI wingSelectUI = UIManagerControl.Instance.GetUIIfExist("WingSelectUI") as WingSelectUI;
		if (wingSelectUI != null)
		{
			wingSelectUI.RefreshWings();
		}
	}

	public void SendWingComposeReq(int wingCfgId)
	{
		NetworkManager.Send(new WingComposeReq
		{
			wingCfgId = wingCfgId
		}, ServerType.Data);
	}

	public void SendWingUpLvReq(int wingCfgId)
	{
		NetworkManager.Send(new WingUpLvReq
		{
			wingCfgId = wingCfgId
		}, ServerType.Data);
	}

	public void SendWingWearReq(int wingCfgId)
	{
		NetworkManager.Send(new WingWearReq
		{
			wingCfgId = wingCfgId
		}, ServerType.Data);
	}

	public void SendWingHiddenReq(bool hidden)
	{
		NetworkManager.Send(new WingHiddenReq
		{
			hidden = hidden
		}, ServerType.Data);
	}

	public void SendGetWingDetailReq()
	{
		NetworkManager.Send(new GetWingDetailReq(), ServerType.Data);
	}

	private void OnWingInfoPush(short state, WingInfoPush msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		WingManager.firstGetWingDetail = msg.firstGetWingDetail;
		using (List<WingInfo>.Enumerator enumerator = msg.wingInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				WingInfo current = enumerator.get_Current();
				WingManager.wingInfoDict.set_Item(current.cfgId, current);
			}
		}
	}

	private void OnWingInfoChangeNty(short state, WingInfoChangeNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int cfgId = msg.wingInfo.cfgId;
		WingManager.wingInfoDict.set_Item(cfgId, msg.wingInfo);
		if (WingManager.wingInfoDict.get_Item(cfgId).overdueUtc == -1)
		{
			WingManager.wingInfoDict.Remove(cfgId);
			wings wingInfo = WingManager.GetWingInfo(cfgId);
			string text = string.Format(GameDataUtils.GetChineseContent(237031, false), wingInfo.name);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
		}
		WingSelectUI wingSelectUI = UIManagerControl.Instance.GetUIIfExist("WingSelectUI") as WingSelectUI;
		if (wingSelectUI != null)
		{
			wingSelectUI.RefreshWings();
		}
	}

	private void RecvWingComposeRes(short state, WingComposeRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		WingManager.wingInfoDict.set_Item(msg.wingInfo.cfgId, msg.wingInfo);
		WingUpgradeUI wingUpgradeUI = UIManagerControl.Instance.GetUIIfExist("WingUpgradeUI") as WingUpgradeUI;
		if (wingUpgradeUI != null)
		{
			wingUpgradeUI.PlayActiveSuccess();
			wingUpgradeUI.Refresh();
		}
		WingSelectUI wingSelectUI = UIManagerControl.Instance.GetUIIfExist("WingSelectUI") as WingSelectUI;
		if (wingSelectUI != null)
		{
			wingSelectUI.PlayActiveSuccess(msg.wingInfo.cfgId);
		}
		WingUI wingUI = UIManagerControl.Instance.GetUIIfExist("WingUI") as WingUI;
		if (wingUI != null)
		{
			wingUI.CheckBadge();
		}
		ActorUI actorUI = UIManagerControl.Instance.GetUIIfExist("ActorUI") as ActorUI;
		if (actorUI != null)
		{
			actorUI.CheckBadge();
		}
	}

	private void RecvWingUpLvRes(short state, WingUpLvRes msg = null)
	{
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			LinkNavigationManager.ItemNotEnoughToLink(msg.itemId, true, null, true);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, msg.itemId);
			return;
		}
		WingManager.wingInfoDict.set_Item(msg.wingInfo.cfgId, msg.wingInfo);
		WingUpgradeUI wingUpgradeUI = UIManagerControl.Instance.GetUIIfExist("WingUpgradeUI") as WingUpgradeUI;
		if (wingUpgradeUI != null)
		{
			wingUpgradeUI.Refresh();
			wingUpgradeUI.PlayProgressSpine();
			wingUpgradeUI.PlayUpgradeSpine();
		}
		WingUI wingUI = UIManagerControl.Instance.GetUIIfExist("WingUI") as WingUI;
		if (wingUI != null)
		{
			wingUI.CheckBadge();
		}
		ActorUI actorUI = UIManagerControl.Instance.GetUIIfExist("ActorUI") as ActorUI;
		if (actorUI != null)
		{
			actorUI.CheckBadge();
		}
	}

	private void RecvWingWearRes(short state, WingWearRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void RecvWingHiddenRes(short state, WingHiddenRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void RecvGetWingDetailRes(short state, GetWingDetailRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		WingManager.firstGetWingDetail = msg.firstGetWingDetail;
		WingUpgradeUI wingUpgradeUI = UIManagerControl.Instance.GetUIIfExist("WingUpgradeUI") as WingUpgradeUI;
		if (wingUpgradeUI != null)
		{
			wingUpgradeUI.PlayIconSpine();
		}
	}

	public bool IsWingSysOnAndActivation()
	{
		return SystemOpenManager.IsSystemOn(35) && WingManager.GetWingLv(1) > 0;
	}

	public static bool CheckAllBadge()
	{
		return WingManager.CheckPage1Badge() || WingManager.CheckPage2Badge();
	}

	public static bool CheckPage1Badge()
	{
		return WingManager.IsSystemOn() && (WingManager.IsCanActiveWing(1) || WingManager.IsCanUpgradeWing(1));
	}

	public static bool CheckPage2Badge()
	{
		if (!WingManager.IsSystemOn())
		{
			return false;
		}
		List<wings> selectWingInfos = WingManager.GetSelectWingInfos();
		using (List<wings>.Enumerator enumerator = selectWingInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				wings current = enumerator.get_Current();
				if (WingManager.IsCanActiveWing(current.id))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool IsSystemOn()
	{
		return SystemOpenManager.IsSystemOn(35);
	}

	public static bool IsCanActiveWing(int id)
	{
		int wingLv = WingManager.GetWingLv(id);
		if (wingLv != 0)
		{
			return false;
		}
		wings wingInfo = WingManager.GetWingInfo(id);
		int key = wingInfo.activation.get_Item(0).key;
		int value = wingInfo.activation.get_Item(0).value;
		long num = BackpackManager.Instance.OnGetGoodCount(key);
		return num >= (long)value;
	}

	public static bool IsCanUpgradeWing(int wingId)
	{
		int wingLv = WingManager.GetWingLv(wingId);
		if (wingLv == 0)
		{
			return false;
		}
		if (WingManager.IsMaxWingLv(wingId, wingLv))
		{
			return false;
		}
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv + 1);
		int key = wingLvInfo.update.get_Item(0).key;
		int value = wingLvInfo.update.get_Item(0).value;
		long num = BackpackManager.Instance.OnGetGoodCount(key);
		return num >= (long)value;
	}

	public static List<wings> GetSelectWingInfos()
	{
		List<wings> list = new List<wings>();
		using (List<wings>.Enumerator enumerator = DataReader<wings>.DataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				wings current = enumerator.get_Current();
				if (current.id != 1)
				{
					list.Add(current);
				}
			}
		}
		return list;
	}

	public static wings GetWingInfo(int id)
	{
		return DataReader<wings>.Get(id);
	}

	public static List<wingLv> GetWingLvInfos(int id)
	{
		List<wingLv> list = new List<wingLv>();
		using (List<wingLv>.Enumerator enumerator = DataReader<wingLv>.DataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				wingLv current = enumerator.get_Current();
				if (current.id == id)
				{
					list.Add(current);
				}
			}
		}
		return list;
	}

	public static bool IsMaxWingLv(int id, int lv)
	{
		wingLv wingLvInfo = WingManager.GetWingLvInfo(id, lv);
		return wingLvInfo.nextLv == wingLvInfo.lv;
	}

	public static int GetWingLv(int id)
	{
		if (WingManager.wingInfoDict.ContainsKey(id))
		{
			return WingManager.wingInfoDict.get_Item(id).lv;
		}
		return 0;
	}

	public static int GetWingModel(int wingId, int wingLv)
	{
		if (wingId == 0)
		{
			return 0;
		}
		if (wingId == 1)
		{
			return WingManager.GetWingLvInfo(wingId, wingLv).model;
		}
		return WingManager.GetWingInfo(wingId).model;
	}

	public static wingLv GetWingLvInfo(int id, int lv)
	{
		List<wingLv> wingLvInfos = WingManager.GetWingLvInfos(id);
		for (int i = 0; i < wingLvInfos.get_Count(); i++)
		{
			wingLv wingLv = wingLvInfos.get_Item(i);
			if (wingLv.lv == lv)
			{
				return wingLv;
			}
		}
		return null;
	}

	public static wingLv GetWingLvInfoNextDifferent(int wingId, int wingLv)
	{
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv);
		List<wingLv> wingLvInfos = WingManager.GetWingLvInfos(wingId);
		for (int i = 0; i < wingLvInfos.get_Count(); i++)
		{
			wingLv wingLv2 = wingLvInfos.get_Item(i);
			if (wingLv2.lv > wingLv && wingLv2.model != wingLvInfo.model)
			{
				return wingLv2;
			}
		}
		return wingLvInfo;
	}

	public static wingLv GetWingLvInfoPreDifferent(int wingId, int wingLv)
	{
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv);
		List<wingLv> wingLvInfos = WingManager.GetWingLvInfos(wingId);
		int num = 0;
		for (int i = wingLvInfos.get_Count() - 1; i >= 0; i--)
		{
			wingLv wingLv2 = wingLvInfos.get_Item(i);
			if (wingLv2.lv < wingLv && wingLv2.model != wingLvInfo.model)
			{
				num = wingLv2.model;
				break;
			}
		}
		if (num > 0)
		{
			for (int j = 0; j < wingLvInfos.get_Count(); j++)
			{
				wingLv wingLv3 = wingLvInfos.get_Item(j);
				if (wingLv3.model == num)
				{
					return wingLv3;
				}
			}
		}
		return wingLvInfo;
	}
}
