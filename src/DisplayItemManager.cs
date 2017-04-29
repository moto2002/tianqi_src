using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemManager
{
	private static Transform mDisplayItemRoot;

	private static UIPool mDisplayItemUnitPool;

	public XDict<int, long> ItemDataDict = new XDict<int, long>();

	private List<DisplayItemData> mDataCashe = new List<DisplayItemData>();

	private List<DisplayItemUnit> mPlayingMoveTo = new List<DisplayItemUnit>();

	private List<DisplayItemUnit> mWaitingToFadeout = new List<DisplayItemUnit>();

	private List<DisplayItemUnit> mPlayingFadeout = new List<DisplayItemUnit>();

	private int mIntervalTime = 300;

	private int mPopOneTime = 150;

	private int mTempTime;

	private uint waitingFadeoutTimer;

	private static DisplayItemManager mInstance;

	public static DisplayItemManager Instance
	{
		get
		{
			if (DisplayItemManager.mInstance == null)
			{
				DisplayItemManager.mInstance = new DisplayItemManager();
			}
			return DisplayItemManager.mInstance;
		}
	}

	public DisplayItemManager()
	{
		this.CreatePools();
		TimerHeap.AddTimer(0u, this.mIntervalTime, delegate
		{
			this.CheckStack();
		});
	}

	private void CreatePools()
	{
		if (DisplayItemManager.mDisplayItemRoot == null)
		{
			DisplayItemManager.mDisplayItemRoot = new GameObject("DisplayItemRoot").get_transform();
			DisplayItemManager.mDisplayItemRoot.set_parent(UINodesManager.T2RootOfSpecial);
			UGUITools.ResetTransform(DisplayItemManager.mDisplayItemRoot);
		}
		if (DisplayItemManager.mDisplayItemUnitPool == null)
		{
			DisplayItemManager.mDisplayItemUnitPool = new UIPool("DisplayItemUnit", DisplayItemManager.mDisplayItemRoot, false);
		}
	}

	private void CheckStack()
	{
		if (this.mDataCashe.get_Count() == 0)
		{
			return;
		}
		this.mTempTime += this.mIntervalTime;
		if (this.mTempTime < this.mPopOneTime)
		{
			return;
		}
		this.mTempTime = 0;
		this.DisplayItem();
	}

	private void DisplayItem()
	{
		DisplayItemData displayItemData = this.mDataCashe.get_Item(0);
		this.mDataCashe.RemoveAt(0);
		DisplayItemUnit displayItemUnit = DisplayItemManager.mDisplayItemUnitPool.Get(string.Empty).AddUniqueComponent<DisplayItemUnit>();
		this.mPlayingMoveTo.Add(displayItemUnit);
		displayItemUnit.MoveTo(displayItemData.index, displayItemData.key, "+" + displayItemData.value, displayItemData.isEnd);
	}

	private void ResetCurrent()
	{
		this.mTempTime = 0;
		TimerHeap.DelTimer(this.waitingFadeoutTimer);
		this.mDataCashe.Clear();
		for (int i = 0; i < this.mPlayingMoveTo.get_Count(); i++)
		{
			this.mPlayingMoveTo.get_Item(i).Reset();
		}
		this.mPlayingMoveTo.Clear();
		for (int j = 0; j < this.mWaitingToFadeout.get_Count(); j++)
		{
			this.mWaitingToFadeout.get_Item(j).Reset();
		}
		this.mWaitingToFadeout.Clear();
		for (int k = 0; k < this.mPlayingFadeout.get_Count(); k++)
		{
			this.mPlayingFadeout.get_Item(k).Reset();
		}
		this.mPlayingFadeout.Clear();
	}

	public void AddItemBubble()
	{
		if (this.ItemDataDict.Count > 0)
		{
			this.AddItemBubble(this.ItemDataDict.Keys, this.ItemDataDict.Values);
		}
		this.ItemDataDict.Clear();
	}

	public void AddItemBubble(List<int> itemId, List<long> itemValue)
	{
		this.ResetCurrent();
		int num = (itemId.get_Count() >= itemValue.get_Count()) ? itemValue.get_Count() : itemId.get_Count();
		for (int i = 0; i < num; i++)
		{
			this.AddItemBubble(i, itemId.get_Item(i), itemValue.get_Item(i), i == num - 1);
		}
	}

	public void AddItemBubble(int index, int itemId, long itemValue, bool isEnd)
	{
		DisplayItemData displayItemData = new DisplayItemData();
		displayItemData.index = index;
		displayItemData.key = itemId;
		displayItemData.value = itemValue;
		displayItemData.isEnd = isEnd;
		this.mDataCashe.Add(displayItemData);
	}

	public void MoveToEnd(DisplayItemUnit unit, bool isEnd)
	{
		this.mPlayingMoveTo.Remove(unit);
		this.mWaitingToFadeout.Add(unit);
		if (isEnd)
		{
			this.waitingFadeoutTimer = TimerHeap.AddTimer(1000u, 0, delegate
			{
				for (int i = 0; i < this.mWaitingToFadeout.get_Count(); i++)
				{
					this.mWaitingToFadeout.get_Item(i).FadeOut();
					this.mPlayingFadeout.Add(this.mWaitingToFadeout.get_Item(i));
				}
				this.mWaitingToFadeout.Clear();
			});
		}
	}

	public void FadeOutEnd(DisplayItemUnit unit)
	{
		this.mPlayingFadeout.Remove(unit);
		unit.Reset();
	}

	public void DisplayEnd(GameObject go)
	{
		DisplayItemManager.mDisplayItemUnitPool.ReUse(go);
	}
}
