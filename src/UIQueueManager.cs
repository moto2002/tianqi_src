using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIQueueManager
{
	public class EventNames
	{
	}

	private static UIQueueManager instance;

	public bool Islocked;

	public List<QueueUnit> m_actionlist = new List<QueueUnit>();

	public static UIQueueManager Instance
	{
		get
		{
			if (UIQueueManager.instance == null)
			{
				UIQueueManager.instance = new UIQueueManager();
			}
			return UIQueueManager.instance;
		}
	}

	public void Init()
	{
	}

	public bool CheckQueue(PopCondition condition)
	{
		if (!this.Islocked)
		{
			for (int i = 0; i < this.m_actionlist.get_Count(); i++)
			{
				if (this.m_actionlist.get_Item(i).condition == condition)
				{
					QueueUnit queueUnit = this.m_actionlist.get_Item(i);
					this.m_actionlist.RemoveAt(i);
					queueUnit.JustCall();
					this.Islocked = true;
					return true;
				}
			}
		}
		return false;
	}

	public void Push(Action action, PopPriority priority, PopCondition condition)
	{
		QueueUnit queueUnit = new QueueUnit
		{
			action = action,
			priority = (uint)priority,
			condition = condition
		};
		this.m_actionlist.Add(queueUnit);
		this.m_actionlist = Enumerable.ToList<QueueUnit>(Enumerable.OrderByDescending<QueueUnit, uint>(this.m_actionlist, (QueueUnit t) => t.priority));
	}

	public void PrintDebug()
	{
		Debug.LogError("=>m_actionlist count = " + this.m_actionlist.get_Count());
	}
}
