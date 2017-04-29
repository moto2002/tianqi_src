using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GemManager : BaseSubSystemManager
{
	public const int EQUIP_COUNT = 10;

	public const int SLOT_COUNT = 4;

	public GemEmbedInfo[,] equipSlots;

	public bool[,] newOpeningSlots;

	public static readonly GemManager Instance = new GemManager();

	public override void Init()
	{
		this.equipSlots = new GemEmbedInfo[10, 4];
		this.newOpeningSlots = new bool[10, 4];
		base.Init();
	}

	public override void Release()
	{
		this.equipSlots = null;
		this.newOpeningSlots = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GemBaseInfoNty>(new NetCallBackMethod<GemBaseInfoNty>(this.NoticeGemBaseInfoNty));
		NetworkManager.AddListenEvent<GemSlotOpenNty>(new NetCallBackMethod<GemSlotOpenNty>(this.NoticeGemSlotOpenNty));
		NetworkManager.AddListenEvent<GemSysEmbedRes>(new NetCallBackMethod<GemSysEmbedRes>(this.RecvGemSysEmbedRes));
		NetworkManager.AddListenEvent<GemSysCompositeRes>(new NetCallBackMethod<GemSysCompositeRes>(this.RecvGemSysCompositeRes));
		NetworkManager.AddListenEvent<GemSysTakeoffRes>(new NetCallBackMethod<GemSysTakeoffRes>(this.RecvGemSysTakeoffRes));
		EventDispatcher.AddListener(EventNames.OnUpdateGoods, new Callback(this.OnUpdateGoods));
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionReadyChange", new Callback(this.OnRoleSelfProfessionChange));
	}

	public void SendGemSysTakeoffReq(EquipLibType.ELT _type, int _hole)
	{
		Debug.Log(string.Concat(new object[]
		{
			"SendGemSysTakeoffReq equip=",
			(int)_type,
			" hole=",
			_hole
		}));
		NetworkManager.Send(new GemSysTakeoffReq
		{
			type = _type,
			hole = _hole
		}, ServerType.Data);
	}

	public void SendGemSysEmbedReq(EquipLibType.ELT _type, int _hole, long _gemId)
	{
		NetworkManager.Send(new GemSysEmbedReq
		{
			type = _type,
			hole = _hole,
			gemId = _gemId
		}, ServerType.Data);
	}

	public void SendGemSysCompositeReq(int _typeId, int _method)
	{
		NetworkManager.Send(new GemSysCompositeReq
		{
			typeId = _typeId,
			method = _method
		}, ServerType.Data);
	}

	private void NoticeGemBaseInfoNty(short state, GemBaseInfoNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg != null)
		{
			List<GemEmbedInfo>[] array = new List<GemEmbedInfo>[msg.gemPartInfos.get_Count()];
			for (int i = 0; i < msg.gemPartInfos.get_Count(); i++)
			{
				array[i] = msg.gemPartInfos.get_Item(i).gemEmbedInfo;
			}
			for (int j = 0; j < 10; j++)
			{
				if (j >= array.Length)
				{
					break;
				}
				List<GemEmbedInfo> list = array[j];
				for (int k = 0; k < list.get_Count(); k++)
				{
					GemEmbedInfo gemEmbedInfo = list.get_Item(k);
					int num = gemEmbedInfo.hole - 1;
					this.equipSlots[j, num] = gemEmbedInfo;
				}
			}
			this.CheckCanShowWearingGemPromoteWay();
		}
	}

	private void NoticeGemSlotOpenNty(short state, GemSlotOpenNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		using (List<GemSlotOpen>.Enumerator enumerator = msg.slotOpen.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GemSlotOpen current = enumerator.get_Current();
				int num = current.type - EquipLibType.ELT.Weapon;
				int num2 = current.hole - 1;
				this.equipSlots[num, num2] = new GemEmbedInfo();
				this.newOpeningSlots[num, num2] = true;
			}
		}
		this.CheckCanShowWearingGemPromoteWay();
	}

	private void RecvGemSysEmbedRes(short state, GemSysEmbedRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg != null)
		{
			EquipLibType.ELT type = msg.type;
			int hole = msg.hole;
			long newGemId = msg.newGemId;
			int newGemTypeId = msg.newGemTypeId;
			GemEmbedInfo gemEmbedInfo = this.equipSlots[type - EquipLibType.ELT.Weapon, hole - 1];
			gemEmbedInfo.id = newGemId;
			gemEmbedInfo.typeId = newGemTypeId;
			GemUI gemUI = UIManagerControl.Instance.GetUIIfExist("GemUI") as GemUI;
			if (gemUI != null)
			{
				gemUI.RefreshEquipSlot(msg.hole - 1, newGemTypeId);
			}
			GemSelectUI gemSelectUI = UIManagerControl.Instance.GetUIIfExist("GemSelectUI") as GemSelectUI;
			if (gemSelectUI != null)
			{
				gemSelectUI.Show(false);
			}
			EventDispatcher.Broadcast<int>(EventNames.UpdateEquipPosGemData, (int)msg.type);
			EventDispatcher.Broadcast(EventNames.EquipDetailedShouldCheckBadge);
			this.CheckCanShowWearingGemPromoteWay();
		}
	}

	private void RecvGemSysCompositeRes(short state, GemSysCompositeRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		GemSelectUI gemSelectUI = UIManagerControl.Instance.GetUIIfExist("GemSelectUI") as GemSelectUI;
		if (gemSelectUI != null && GemUI.instance != null)
		{
			gemSelectUI.Refresh((int)GemUI.instance.equipCurr, GemUI.instance.slotCurr);
		}
		string text = string.Format(GameDataUtils.GetChineseContent(505157, false), msg.count, GameDataUtils.GetItemName(msg.typeId, true, 0L));
		UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
		EventDispatcher.Broadcast(EventNames.EquipDetailedShouldCheckBadge);
		this.CheckCanShowWearingGemPromoteWay();
	}

	private void RecvGemSysTakeoffRes(short state, GemSysTakeoffRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg != null)
		{
			this.equipSlots[msg.type - EquipLibType.ELT.Weapon, msg.hole - 1].typeId = 0;
			this.equipSlots[msg.type - EquipLibType.ELT.Weapon, msg.hole - 1].id = 0L;
			GemUI gemUI = UIManagerControl.Instance.GetUIIfExist("GemUI") as GemUI;
			if (gemUI != null)
			{
				gemUI.RefreshEquipSlot(msg.hole - 1, 0);
			}
			GemSingleUI gemSingleUI = UIManagerControl.Instance.GetUIIfExist("GemSingleUI") as GemSingleUI;
			if (gemSingleUI != null)
			{
				gemSingleUI.Show(false);
			}
			EventDispatcher.Broadcast(EventNames.EquipDetailedShouldCheckBadge);
			EventDispatcher.Broadcast<int>(EventNames.UpdateEquipPosGemData, (int)msg.type);
			this.CheckCanShowWearingGemPromoteWay();
		}
	}

	public bool IsCanWearGem()
	{
		return this.IsCanWearGem(EquipLibType.ELT.Necklace) || this.IsCanWearGem(EquipLibType.ELT.Pant) || this.IsCanWearGem(EquipLibType.ELT.Shirt) || this.IsCanWearGem(EquipLibType.ELT.Shoe) || this.IsCanWearGem(EquipLibType.ELT.Waist) || this.IsCanWearGem(EquipLibType.ELT.Weapon) || this.IsCanWearGem(EquipLibType.ELT.Part7) || this.IsCanWearGem(EquipLibType.ELT.Part8) || this.IsCanWearGem(EquipLibType.ELT.Part9) || this.IsCanWearGem(EquipLibType.ELT.Part10);
	}

	public bool IsCanWearGem(EquipLibType.ELT pos)
	{
		return SystemOpenManager.IsSystemOn(22) && GemGlobal.CheckCanShowTip((int)pos);
	}

	public void CheckCanShowWearingGemPromoteWay()
	{
		bool isShow = this.IsCanWearGem();
		StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.GemCanWear, isShow);
	}

	private void OnUpdateGoods()
	{
		if (BackpackManager.Instance.RuneGoods != null && BackpackManager.Instance.RuneGoods.get_Count() > 0)
		{
			this.CheckCanShowWearingGemPromoteWay();
		}
	}

	private void OnRoleSelfProfessionChange()
	{
		this.equipSlots = null;
		this.equipSlots = new GemEmbedInfo[10, 4];
		this.newOpeningSlots = null;
		this.newOpeningSlots = new bool[10, 4];
	}
}
