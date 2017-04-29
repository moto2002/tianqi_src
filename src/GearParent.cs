using GameData;
using System;
using UnityEngine;

public class GearParent : MonoBehaviour
{
	public int ID;

	public bool active;

	public bool state;

	public static void TriggerGearEvent(int eventID)
	{
		JiGuanShiJianBiao jiGuanShiJianBiao = DataReader<JiGuanShiJianBiao>.Get(eventID);
		if (jiGuanShiJianBiao == null)
		{
			return;
		}
		if (jiGuanShiJianBiao.active.get_Count() > 0)
		{
			for (int i = 0; i < jiGuanShiJianBiao.active.get_Count(); i++)
			{
				JiGuanShiJianBiao.ActivePair activePair = jiGuanShiJianBiao.active.get_Item(i);
				TimerHeap.AddTimer<int>((uint)activePair.value, 0, new Action<int>(GearParent.SetGearControlActive), activePair.key);
			}
		}
		if (jiGuanShiJianBiao.deactive.get_Count() > 0)
		{
			for (int j = 0; j < jiGuanShiJianBiao.deactive.get_Count(); j++)
			{
				JiGuanShiJianBiao.DeactivePair deactivePair = jiGuanShiJianBiao.deactive.get_Item(j);
				TimerHeap.AddTimer<int>((uint)deactivePair.value, 0, new Action<int>(GearParent.SetGearControlDeactive), deactivePair.key);
			}
		}
		if (jiGuanShiJianBiao.stateUp.get_Count() > 0)
		{
			for (int k = 0; k < jiGuanShiJianBiao.stateUp.get_Count(); k++)
			{
				JiGuanShiJianBiao.StateupPair stateupPair = jiGuanShiJianBiao.stateUp.get_Item(k);
				TimerHeap.AddTimer<int>((uint)stateupPair.value, 0, new Action<int>(GearParent.SetGearControlStateUp), stateupPair.key);
			}
		}
		if (jiGuanShiJianBiao.stateDown.get_Count() > 0)
		{
			for (int l = 0; l < jiGuanShiJianBiao.stateDown.get_Count(); l++)
			{
				JiGuanShiJianBiao.StatedownPair statedownPair = jiGuanShiJianBiao.stateDown.get_Item(l);
				TimerHeap.AddTimer<int>((uint)statedownPair.value, 0, new Action<int>(GearParent.SetGearControlStateDown), statedownPair.key);
			}
		}
	}

	protected static void SetGearControlActive(int gearID)
	{
		EventDispatcher.Broadcast<int>(GearEvent.Active, gearID);
	}

	protected static void SetGearControlDeactive(int gearID)
	{
		EventDispatcher.Broadcast<int>(GearEvent.Deactive, gearID);
	}

	protected static void SetGearControlStateUp(int gearID)
	{
		EventDispatcher.Broadcast<int>(GearEvent.StateUp, gearID);
	}

	protected static void SetGearControlStateDown(int gearID)
	{
		EventDispatcher.Broadcast<int>(GearEvent.StateDown, gearID);
	}

	private void Awake()
	{
		this.AddListeners();
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	public virtual void AddListeners()
	{
		EventDispatcher.AddListener<int>(GearEvent.Active, new Callback<int>(this.Active));
		EventDispatcher.AddListener<int>(GearEvent.Deactive, new Callback<int>(this.Deactive));
		EventDispatcher.AddListener<int>(GearEvent.StateUp, new Callback<int>(this.StateUp));
		EventDispatcher.AddListener<int>(GearEvent.StateDown, new Callback<int>(this.StateDown));
	}

	public virtual void RemoveListeners()
	{
		EventDispatcher.RemoveListener<int>(GearEvent.Active, new Callback<int>(this.Active));
		EventDispatcher.RemoveListener<int>(GearEvent.Deactive, new Callback<int>(this.Deactive));
		EventDispatcher.RemoveListener<int>(GearEvent.StateUp, new Callback<int>(this.StateUp));
		EventDispatcher.RemoveListener<int>(GearEvent.StateDown, new Callback<int>(this.StateDown));
	}

	protected virtual void Active(int activeID)
	{
		if (activeID == this.ID)
		{
			this.active = true;
		}
	}

	protected virtual void Deactive(int deactiveID)
	{
		if (deactiveID == this.ID)
		{
			this.active = false;
		}
	}

	protected virtual void StateUp(int stateUpID)
	{
		if (stateUpID == this.ID)
		{
			this.state = true;
		}
	}

	protected virtual void StateDown(int stateDownID)
	{
		if (stateDownID == this.ID)
		{
			this.state = false;
		}
	}
}
