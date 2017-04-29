using GameData;
using Package;
using System;
using UnityEngine;
using XNetwork;

public class OffHookManager : BaseSubSystemManager
{
	private OffLineLoginPush dataList;

	public bool isInit;

	private static OffHookManager instance;

	public static OffHookManager Instance
	{
		get
		{
			if (OffHookManager.instance == null)
			{
				OffHookManager.instance = new OffHookManager();
			}
			return OffHookManager.instance;
		}
	}

	private OffHookManager()
	{
	}

	public static bool IsNotNull()
	{
		return OffHookManager.instance != null;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<OffLineLoginPush>(new NetCallBackMethod<OffLineLoginPush>(this.OnUpdatePanel));
		NetworkManager.AddListenEvent<OffLineRes>(new NetCallBackMethod<OffLineRes>(this.OnOffLineRes));
		NetworkManager.AddListenEvent<OffLineMsgRes>(new NetCallBackMethod<OffLineMsgRes>(this.OnOffLineMsgRes));
	}

	public void SendBuyPlanReq(int itemId)
	{
		NetworkManager.Send(new OffLineReq
		{
			itemId = itemId
		}, ServerType.Data);
	}

	public void SendPanelReq()
	{
		NetworkManager.Send(new OffLineMsgReq(), ServerType.Data);
	}

	public void OnUpdatePanel(short state, OffLineLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.dataList = down;
			if (OffHookUI.Instance != null && OffHookUI.Instance.get_gameObject().get_activeSelf())
			{
				OffHookUI.Instance.RefreshUI();
			}
		}
	}

	public void OnOffLineRes(short state, OffLineRes down = null)
	{
		if (state != 0)
		{
			if (state == 206)
			{
				Items items = DataReader<Items>.Get(down.itemId);
				if (items != null)
				{
					UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(509004, false), items.minLv));
				}
			}
			else
			{
				StateManager.Instance.StateShow(state, 0);
			}
			return;
		}
		if (down != null)
		{
			if (this.dataList != null)
			{
				this.dataList.hasTime = down.hasTime;
			}
			else
			{
				this.dataList = new OffLineLoginPush();
				this.dataList.hasTime = down.hasTime;
			}
			int getTime = down.GetTime;
			if (getTime % 60 == 0)
			{
				UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(330035, false), getTime / 60));
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(330035, false), Math.Round(getTime / 60m, 1).ToString("F1")));
			}
		}
	}

	public void OnOffLineMsgRes(short state, OffLineMsgRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.dataList != null)
			{
				this.dataList.hasTime = down.hasTime;
			}
			if (OffHookUI.Instance != null && OffHookUI.Instance.get_gameObject().get_activeSelf())
			{
				OffHookUI.Instance.RefreshBeginUI(down);
			}
		}
	}

	public OffLineLoginPush GetOffHookData()
	{
		return this.dataList;
	}

	public int GetOffHookHasTime()
	{
		if (this.dataList != null)
		{
			return this.dataList.hasTime;
		}
		return 0;
	}

	public void GetRecommendItem(int itemCfgID, Transform trans, bool showHaveCount = false)
	{
		Items localItem = DataReader<Items>.Get(itemCfgID);
		if (localItem != null && localItem.function == 2)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
			instantiate2Prefab.get_transform().SetParent(trans);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.set_name("Equip_" + itemCfgID);
			if (showHaveCount)
			{
				instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(localItem.id, "提示", "使 用", delegate
				{
					if (this.dataList != null)
					{
						if (this.dataList.hasTime < 28800)
						{
							OffHookManager.Instance.SendBuyPlanReq(localItem.id);
						}
						else
						{
							UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(330034, false), 1f, 1f);
						}
					}
					else
					{
						OffHookManager.Instance.SendBuyPlanReq(localItem.id);
					}
				}, true, null, 2000);
			}
			else
			{
				instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(localItem.id, "离线挂机", "前 往", delegate
				{
					XMarketManager.Instance.OpenShop(1);
				}, true, null, 2000);
				instantiate2Prefab.GetComponent<EquipRecommendItem>().ItemNameContent = "剩余不足1小时";
			}
			return;
		}
	}

	public bool IsOnLegalCheckHookTime()
	{
		return this.dataList == null || (this.dataList != null && this.dataList.hasTime < 28800);
	}
}
