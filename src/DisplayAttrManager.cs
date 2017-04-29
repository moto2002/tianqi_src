using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAttrManager
{
	protected static DisplayAttrManager instance;

	protected static Transform displayAttrUnitRoot;

	protected static UIPool displayAttrUnitPool;

	protected List<DisplayAttrData> dataCashe = new List<DisplayAttrData>();

	protected List<DisplayAttrUnit> playingMoveTo = new List<DisplayAttrUnit>();

	protected List<DisplayAttrUnit> waitingToFadeout = new List<DisplayAttrUnit>();

	protected List<DisplayAttrUnit> playingFadeout = new List<DisplayAttrUnit>();

	protected int intervalTime = 300;

	protected int popOneTime = 150;

	protected int tempTime;

	protected uint waitingFadeoutTimer;

	public static DisplayAttrManager Instance
	{
		get
		{
			if (DisplayAttrManager.instance == null)
			{
				DisplayAttrManager.instance = new DisplayAttrManager();
			}
			return DisplayAttrManager.instance;
		}
	}

	public DisplayAttrManager()
	{
		this.CreatePools();
		TimerHeap.AddTimer(0u, this.intervalTime, delegate
		{
			this.CheckStack();
		});
	}

	protected void CreatePools()
	{
		if (!DisplayAttrManager.displayAttrUnitRoot)
		{
			DisplayAttrManager.displayAttrUnitRoot = new GameObject("DisplayAttrUnitRoot").get_transform();
			DisplayAttrManager.displayAttrUnitRoot.set_parent(UINodesManager.T2RootOfSpecial);
			UGUITools.ResetTransform(DisplayAttrManager.displayAttrUnitRoot);
		}
		if (DisplayAttrManager.displayAttrUnitPool == null)
		{
			DisplayAttrManager.displayAttrUnitPool = new UIPool("DisplayAttrUnit", DisplayAttrManager.displayAttrUnitRoot, false);
		}
	}

	public void AddFloatText(List<int> attrType, List<long> attrValue)
	{
		this.ResetCurrent();
		int num = (attrType.get_Count() >= attrValue.get_Count()) ? attrValue.get_Count() : attrType.get_Count();
		for (int i = 0; i < num; i++)
		{
			this.AddFloatText(i, attrType.get_Item(i), attrValue.get_Item(i), i == num - 1);
		}
	}

	protected void ResetCurrent()
	{
		this.tempTime = 0;
		TimerHeap.DelTimer(this.waitingFadeoutTimer);
		this.dataCashe.Clear();
		for (int i = 0; i < this.playingMoveTo.get_Count(); i++)
		{
			this.playingMoveTo.get_Item(i).Reset();
		}
		this.playingMoveTo.Clear();
		for (int j = 0; j < this.waitingToFadeout.get_Count(); j++)
		{
			this.waitingToFadeout.get_Item(j).Reset();
		}
		this.waitingToFadeout.Clear();
		for (int k = 0; k < this.playingFadeout.get_Count(); k++)
		{
			this.playingFadeout.get_Item(k).Reset();
		}
		this.playingFadeout.Clear();
	}

	public void AddFloatText(int index, int attrType, long attrValue, bool isEnd)
	{
		DisplayAttrData displayAttrData = new DisplayAttrData();
		displayAttrData.index = index;
		displayAttrData.attrType = attrType;
		displayAttrData.attrValue = attrValue;
		displayAttrData.isEnd = isEnd;
		this.dataCashe.Add(displayAttrData);
	}

	protected void CheckStack()
	{
		if (this.dataCashe.get_Count() == 0)
		{
			return;
		}
		this.tempTime += this.intervalTime;
		if (this.tempTime < this.popOneTime)
		{
			return;
		}
		this.tempTime = 0;
		this.DisplayAttr();
	}

	protected void DisplayAttr()
	{
		DisplayAttrData displayAttrData = this.dataCashe.get_Item(0);
		this.dataCashe.RemoveAt(0);
		DisplayAttrUnit displayAttrUnit = DisplayAttrManager.displayAttrUnitPool.Get(string.Empty).AddUniqueComponent<DisplayAttrUnit>();
		this.playingMoveTo.Add(displayAttrUnit);
		displayAttrUnit.MoveTo(displayAttrData.index, AttrUtility.GetStandardAddDesc(displayAttrData.attrType, displayAttrData.attrValue), displayAttrData.isEnd);
	}

	public void MoveToEnd(DisplayAttrUnit displayAttrUnit, bool isEnd)
	{
		this.playingMoveTo.Remove(displayAttrUnit);
		this.waitingToFadeout.Add(displayAttrUnit);
		if (isEnd)
		{
			this.waitingFadeoutTimer = TimerHeap.AddTimer(1000u, 0, delegate
			{
				for (int i = 0; i < this.waitingToFadeout.get_Count(); i++)
				{
					this.waitingToFadeout.get_Item(i).FadeOut();
					this.playingFadeout.Add(this.waitingToFadeout.get_Item(i));
				}
				this.waitingToFadeout.Clear();
			});
		}
	}

	public void FadeOutEnd(DisplayAttrUnit displayAttrUnit)
	{
		this.playingFadeout.Remove(displayAttrUnit);
		displayAttrUnit.Reset();
	}

	public void DisplayEnd(GameObject gameObject)
	{
		DisplayAttrManager.displayAttrUnitPool.ReUse(gameObject);
	}
}
