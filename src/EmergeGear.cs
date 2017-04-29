using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergeGear : GearParent
{
	protected List<Transform> result = new List<Transform>();

	private void Awake()
	{
		this.GetTramformList();
		this.AddListeners();
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	protected override void StateUp(int stateUpID)
	{
		base.StateUp(stateUpID);
		if (stateUpID == this.ID)
		{
			this.Emerge();
		}
	}

	protected override void StateDown(int stateDownID)
	{
		base.StateDown(stateDownID);
		if (stateDownID == this.ID)
		{
			this.Stealth();
		}
	}

	protected void GetTramformList()
	{
		Queue<Transform> queue = new Queue<Transform>();
		this.result.Add(base.get_transform());
		queue.Enqueue(base.get_transform());
		while (queue.get_Count() > 0)
		{
			IEnumerator enumerator = queue.Dequeue().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.get_Current();
					this.result.Add(transform);
					queue.Enqueue(transform);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public void Emerge()
	{
		this.state = true;
		for (int i = 0; i < this.result.get_Count(); i++)
		{
			this.result.get_Item(i).get_gameObject().SetActive(true);
		}
	}

	public void Stealth()
	{
		this.state = false;
		for (int i = 0; i < this.result.get_Count(); i++)
		{
			this.result.get_Item(i).get_gameObject().SetActive(false);
		}
	}
}
