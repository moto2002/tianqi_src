using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalDimensionRangeChecker
{
	protected static int LocalDimensionRangeCheckerID;

	public int ID;

	protected Vector2 center = Vector2.get_zero();

	protected float range;

	protected int interval;

	protected Action<int, List<long>> callBack;

	protected uint timer = 4294967295u;

	protected List<long> insider = new List<long>();

	protected LocalDimensionRangeChecker()
	{
	}

	public static LocalDimensionRangeChecker GetLocalDimensionRangeChecker(Vector2 theCenter, float theRange, int theInterval, Action<int, List<long>> theCallBack)
	{
		if (theInterval == 0 || theCallBack == null)
		{
			return null;
		}
		LocalDimensionRangeChecker.LocalDimensionRangeCheckerID++;
		LocalDimensionRangeChecker localDimensionRangeChecker = new LocalDimensionRangeChecker();
		localDimensionRangeChecker.ID = LocalDimensionRangeChecker.LocalDimensionRangeCheckerID;
		localDimensionRangeChecker.center = theCenter;
		localDimensionRangeChecker.range = theRange;
		localDimensionRangeChecker.interval = theInterval;
		localDimensionRangeChecker.callBack = theCallBack;
		localDimensionRangeChecker.timer = TimerHeap.AddTimer(0u, localDimensionRangeChecker.interval, new Action(localDimensionRangeChecker.LoopCheck));
		return localDimensionRangeChecker;
	}

	protected void LoopCheck()
	{
		this.insider.Clear();
		List<EntityParent> values = EntityWorld.Instance.AllEntities.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i) != null && values.get_Item(i).Actor && values.get_Item(i).Actor.FixTransform != null && Vector2.Distance(this.center, new Vector2(values.get_Item(i).Actor.FixTransform.get_position().x, values.get_Item(i).Actor.FixTransform.get_position().z)) < this.range)
			{
				this.insider.Add(values.get_Item(i).ID);
			}
		}
		this.callBack.Invoke(this.ID, this.insider);
	}

	public void StopCheck()
	{
		TimerHeap.DelTimer(this.timer);
	}
}
