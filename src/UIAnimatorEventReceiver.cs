using System;
using UnityEngine;

public class UIAnimatorEventReceiver : MonoBehaviour
{
	public Action CallBackOfEnd;

	protected void EventEnd()
	{
		if (this.CallBackOfEnd != null)
		{
			this.CallBackOfEnd.Invoke();
		}
	}
}
