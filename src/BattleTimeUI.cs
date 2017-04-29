using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimeUI : BaseUIBehaviour
{
	protected static List<Image> TimeImages = new List<Image>();

	protected static List<Vector3> HMSSlotGroup = new List<Vector3>();

	protected static List<Vector3> MSSlotGroup = new List<Vector3>();

	public Text TextBattleTime;

	private int previous_time = -1;

	private static void Clear()
	{
		BattleTimeUI.TimeImages.Clear();
		BattleTimeUI.HMSSlotGroup.Clear();
		BattleTimeUI.MSSlotGroup.Clear();
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		BattleTimeUI.Clear();
		BattleTimeUI.TimeImages.Add(base.FindTransform("Hour10Image").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("HourImage").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("Minute10Image").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("MinuteImage").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("Second10Image").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("SecondImage").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("HMColonImage").GetComponent<Image>());
		BattleTimeUI.TimeImages.Add(base.FindTransform("MSColonImage").GetComponent<Image>());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSHour10Slot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSHourSlot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSMinute10Slot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSMinuteSlot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSSecond10Slot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSSecondSlot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSHMColonSlot").get_localPosition());
		BattleTimeUI.HMSSlotGroup.Add(base.FindTransform("HMSMSColonSlot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSHour10Slot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSHourSlot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSMinute10Slot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSMinuteSlot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSSecond10Slot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSSecondSlot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSHMColonSlot").get_localPosition());
		BattleTimeUI.MSSlotGroup.Add(base.FindTransform("MSMSColonSlot").get_localPosition());
		this.TextBattleTime = base.FindTransform("TextBattleTime").GetComponent<Text>();
	}

	private void OnEnable()
	{
		this.previous_time = -1;
		this.ResetTime();
		BattleTimeManager.Instance.AddCurrentTimeUIAction(new Action<int>(this.UpdateTime));
		TimeManager.Instance.ForceSendSyncServerTime();
	}

	private void OnDisable()
	{
		this.previous_time = -1;
		BattleTimeManager.Instance.RemoveCurrentTimeUIAction(new Action<int>(this.UpdateTime));
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		BattleTimeUI.Clear();
	}

	protected void ResetTime()
	{
		this.UpdateTime(0);
	}

	protected void UpdateTime(int time)
	{
		if (BattleTimeUI.TimeImages.get_Count() == 0)
		{
			return;
		}
		if (time == this.previous_time)
		{
			return;
		}
		this.previous_time = time;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (time >= 0)
		{
			num = time / 3600;
			int num4 = time % 3600;
			num2 = num4 / 60;
			num3 = num4 % 60;
		}
		if (num > 0)
		{
			this.TextBattleTime.set_text(string.Format("{0}:{1}:{2}", num, num2, num3));
			return;
		}
		this.TextBattleTime.set_text(string.Format("{0}:{1}", num2, num3));
	}
}
