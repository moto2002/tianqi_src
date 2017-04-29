using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class VipTasteCardManager : BaseSubSystemManager
{
	public bool isHaveShow;

	public bool isExpireShow = true;

	public int CheckId;

	public int CardTime;

	private static VipTasteCardManager _Instance;

	public static VipTasteCardManager Instance
	{
		get
		{
			if (VipTasteCardManager._Instance == null)
			{
				VipTasteCardManager._Instance = new VipTasteCardManager();
			}
			return VipTasteCardManager._Instance;
		}
	}

	private VipTasteCardManager()
	{
	}

	public static bool IsNotNull()
	{
		return VipTasteCardManager.Instance != null;
	}

	public override void Init()
	{
		base.Init();
		this.InitCheckId();
	}

	public override void Release()
	{
		this.isHaveShow = false;
		this.isExpireShow = true;
		this.CheckId = 0;
		this.CardTime = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<UseFreeCardRes>(new NetCallBackMethod<UseFreeCardRes>(this.OnUseFreeCardRes));
		NetworkManager.AddListenEvent<FreeCardNty>(new NetCallBackMethod<FreeCardNty>(this.OnFreeCardNty));
	}

	private void InitCheckId()
	{
		List<vipTiYanQia> dataList = DataReader<vipTiYanQia>.DataList;
		vipTiYanQia vipTiYanQia = dataList.get_Item(0);
		if (dataList == null)
		{
			return;
		}
		this.CheckId = vipTiYanQia.ID;
	}

	public void SendUseCard(int itemId)
	{
		NetworkManager.Send(new UseFreeCardReq
		{
			itemId = itemId
		}, ServerType.Data);
	}

	public void OnUseFreeCardRes(short state, UseFreeCardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			Debug.LogError("OnOffLineRes  is  empty！________________________________________________________________________");
		}
	}

	public void OnFreeCardNty(short state, FreeCardNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.times == 0)
			{
				this.isExpireShow = false;
			}
			this.CardTime = down.times;
			EventDispatcher.Broadcast(EventNames.VipTasteCardNty);
		}
	}

	public void GetRecommendItem(int itemCfgID, Transform trans, bool showHaveCount = false, int isTasteType = 0)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
		instantiate2Prefab.get_transform().SetParent(trans);
		instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
		instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(Vector3.get_zero());
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.set_name("TasteCard_" + itemCfgID);
		if (isTasteType == 1)
		{
			instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(itemCfgID, "提示", "使 用", delegate
			{
				LinkNavigationManager.OpenVipTasteCardUI();
			}, true, null, 2000);
		}
		else if (isTasteType == 2)
		{
			instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(itemCfgID, "到期提醒", "查 看", delegate
			{
				LinkNavigationManager.OpenVipExprieCardUI();
			}, true, null, 2000);
		}
		else if (isTasteType == 3)
		{
			instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(itemCfgID, "到期提醒", "查 看", delegate
			{
				LinkNavigationManager.OpenVipLimitardExprieUI();
			}, true, null, 2000);
			instantiate2Prefab.GetComponent<EquipRecommendItem>().ItemNameContent = GameDataUtils.GetChineseContent(505179, true);
		}
	}
}
