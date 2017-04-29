using System;
using System.Collections.Generic;
using UnityEngine;

public class TickBehaviour : MonoBehaviour
{
	private List<ITickable> _ticked;

	private void Awake()
	{
		this._ticked = new List<ITickable>();
	}

	public void Add(ITickable tickable)
	{
		this._ticked.Add(tickable);
	}

	private void Update()
	{
		using (List<ITickable>.Enumerator enumerator = this._ticked.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ITickable current = enumerator.get_Current();
				current.Tick(Time.get_deltaTime());
			}
		}
	}
}
