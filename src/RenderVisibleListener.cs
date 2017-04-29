using System;
using UnityEngine;

public class RenderVisibleListener : MonoBehaviour
{
	public Action actionBecameVisible;

	public Action actionBecameInVisible;

	private void OnBecameVisible()
	{
		if (this.actionBecameVisible != null)
		{
			this.actionBecameVisible.Invoke();
		}
	}

	private void OnBecameInvisible()
	{
		if (this.actionBecameInVisible != null)
		{
			this.actionBecameInVisible.Invoke();
		}
	}
}
