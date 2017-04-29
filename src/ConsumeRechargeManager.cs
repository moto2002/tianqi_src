using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class ConsumeRechargeManager : BaseSubSystemManager
{
	private List<Ac> m_aclist = new List<Ac>();

	private static ConsumeRechargeManager instance;

	public static ConsumeRechargeManager Instance
	{
		get
		{
			if (ConsumeRechargeManager.instance == null)
			{
				ConsumeRechargeManager.instance = new ConsumeRechargeManager();
			}
			return ConsumeRechargeManager.instance;
		}
	}

	private ConsumeRechargeManager()
	{
	}

	private void UpdateAC(Ac ac)
	{
		for (int i = 0; i < this.m_aclist.get_Count(); i++)
		{
			if (this.m_aclist.get_Item(i).typeId == ac.typeId)
			{
				this.m_aclist.set_Item(i, ac);
				return;
			}
		}
		this.m_aclist.Add(ac);
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
		NetworkManager.AddListenEvent<OperateAcPushClient>(new NetCallBackMethod<OperateAcPushClient>(this.OnOperateAcPushClient));
		NetworkManager.AddListenEvent<GetAcPrizeRes>(new NetCallBackMethod<GetAcPrizeRes>(this.OnGetAcPrizeRes));
	}

	public void SendGetAcPrizeReq(int typeId, int targetValue)
	{
		NetworkManager.Send(new GetAcPrizeReq
		{
			typeId = typeId,
			targetId = targetValue
		}, ServerType.Data);
	}

	public void OnOperateAcPushClient(short state, OperateAcPushClient down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.ac.get_Count(); i++)
			{
				this.UpdateAC(down.ac.get_Item(i));
			}
			this.RefreshUI();
			OperateActivityManager.Instance.OnUpdateOperateAcPush();
		}
		else
		{
			Debug.LogError("OperateAcPushClient  is  empty！________________________________________________________________________");
		}
	}

	public void OnGetAcPrizeRes(short state, GetAcPrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText("奖励领取成功");
			this.RefreshUI();
		}
	}

	public Ac GetAC(Ac.AcType.Type type)
	{
		for (int i = 0; i < this.m_aclist.get_Count(); i++)
		{
			if (this.m_aclist.get_Item(i).typeId == type)
			{
				return this.m_aclist.get_Item(i);
			}
		}
		return null;
	}

	public void OpenVIPUIOfRecharge()
	{
		LinkNavigationManager.OpenVIPUI2Recharge();
	}

	public static int SortCompare(AcItemInfo ac1, AcItemInfo ac2)
	{
		int result = 0;
		if (ac1.targetVal > ac2.targetVal)
		{
			result = -1;
		}
		else if (ac1.targetVal < ac2.targetVal)
		{
			result = 1;
		}
		return result;
	}

	public bool GetRechargeGiftPoint()
	{
		bool result = false;
		Ac aC = this.GetAC(Ac.AcType.Type.Recharge);
		if (aC == null || aC.acItemInfo == null)
		{
			return result;
		}
		List<AcItemInfo> acItemInfo = aC.acItemInfo;
		acItemInfo.Sort(new Comparison<AcItemInfo>(ConsumeRechargeManager.SortCompare));
		int num = -1;
		int count = acItemInfo.get_Count();
		if (acItemInfo.get_Item(0).status == 2)
		{
			num = acItemInfo.get_Item(0).targetVal;
		}
		else
		{
			for (int i = count - 1; i >= 0; i--)
			{
				if (acItemInfo.get_Item(i).status != 2)
				{
					num = acItemInfo.get_Item(i).targetVal;
					break;
				}
			}
		}
		if (num == -1)
		{
			return result;
		}
		AcItemInfo acItemInfo2 = null;
		for (int j = 0; j < acItemInfo.get_Count(); j++)
		{
			if (acItemInfo.get_Item(j).targetVal == num)
			{
				acItemInfo2 = acItemInfo.get_Item(j);
				break;
			}
		}
		if (acItemInfo2.status == 1)
		{
			result = true;
		}
		return result;
	}

	public bool GetConsumeGiftPoint()
	{
		bool result = false;
		Ac aC = this.GetAC(Ac.AcType.Type.Cost);
		if (aC == null || aC.acItemInfo == null)
		{
			return result;
		}
		List<AcItemInfo> acItemInfo = aC.acItemInfo;
		acItemInfo.Sort(new Comparison<AcItemInfo>(ConsumeRechargeManager.SortCompare));
		int num = -1;
		int count = acItemInfo.get_Count();
		if (acItemInfo.get_Item(0).status == 2)
		{
			num = acItemInfo.get_Item(0).targetVal;
		}
		else
		{
			for (int i = count - 1; i >= 0; i--)
			{
				if (acItemInfo.get_Item(i).status != 2)
				{
					num = acItemInfo.get_Item(i).targetVal;
					break;
				}
			}
		}
		if (num == -1)
		{
			return result;
		}
		AcItemInfo acItemInfo2 = null;
		for (int j = 0; j < acItemInfo.get_Count(); j++)
		{
			if (acItemInfo.get_Item(j).targetVal == num)
			{
				acItemInfo2 = acItemInfo.get_Item(j);
				break;
			}
		}
		if (acItemInfo2.status == 1)
		{
			result = true;
		}
		return result;
	}

	public void RefreshUI()
	{
		if (RechargeGiftUI.Instance != null && RechargeGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			RechargeGiftUI.Instance.RefreshUI();
		}
		if (ConsumeGiftUI.Instance != null && ConsumeGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			ConsumeGiftUI.Instance.RefreshUI();
		}
	}
}
