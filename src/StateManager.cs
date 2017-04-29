using System;
using UnityEngine;

public class StateManager
{
	protected static StateManager instance;

	public static StateManager Instance
	{
		get
		{
			if (StateManager.instance == null)
			{
				StateManager.instance = new StateManager();
			}
			return StateManager.instance;
		}
	}

	private StateManager()
	{
	}

	public void StateShow(short stateID, int itemID = 0)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"状态错误：错误码为: ",
			stateID,
			" ",
			Status.GetStatusDesc((int)stateID)
		}));
		if (!this.CheckHandleSpecialState(stateID, itemID))
		{
			this.HandleNormalState(stateID);
		}
	}

	protected void HandleNormalState(short stateID)
	{
		UIManagerControl.Instance.ShowToastText(Status.GetStatusDesc((int)stateID), 1f, 1f);
	}

	protected bool CheckHandleSpecialState(short stateID, int itemID = 0)
	{
		if ((int)stateID == Status.GOLD_NOT_ENOUGH)
		{
			this.GoldNotEnough();
		}
		else if ((int)stateID == Status.DIAMOND_NOT_ENOUGH || (int)stateID == Status.LESS_RESET_MATERIAL)
		{
			this.DiamondNotEnough();
		}
		else if ((int)stateID == Status.ENERGY_NOT_ENOUGH)
		{
			this.EnergyNotEnough();
		}
		else if ((int)stateID == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			this.ItemNotEnough(itemID);
		}
		else
		{
			if ((int)stateID != Status.REFRESH_ITEM_NOT_ENOUGH)
			{
				return false;
			}
			LinkNavigationManager.ItemNotEnoughToLink(71034, true, null, true);
		}
		return true;
	}

	protected void GoldNotEnough()
	{
		LinkNavigationManager.ItemNotEnoughToLink(2, true, null, true);
	}

	protected void DiamondNotEnough()
	{
		if (SystemConfig.IsOpenPay)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(510106, false), GameDataUtils.GetChineseContent(510107, false), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenVIPUI2Recharge();
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510106, false));
		}
	}

	protected void EnergyNotEnough()
	{
		EnergyManager.Instance.BuyEnergy(delegate
		{
		});
	}

	protected void ItemNotEnough(int itemID)
	{
		switch (itemID)
		{
		case 2:
			this.GoldNotEnough();
			break;
		case 3:
			this.DiamondNotEnough();
			break;
		case 4:
			this.EnergyNotEnough();
			break;
		default:
			UIManagerControl.Instance.OpenSourceReferenceUI(itemID, null);
			break;
		}
	}
}
