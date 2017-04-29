using System;
using UnityEngine;

public class TickEngine
{
	private TickBehaviour _ticker;

	public TickEngine()
	{
		GameObject gameObject = GameObject.Find("Ticker");
		if (gameObject == null)
		{
			gameObject = new GameObject("Ticker");
			this._ticker = gameObject.AddComponent<TickBehaviour>();
		}
		else
		{
			this._ticker = gameObject.GetComponent<TickBehaviour>();
		}
	}

	public void Add(ITickable tickable)
	{
		this._ticker.Add(tickable);
	}
}
