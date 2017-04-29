using System;
using UnityEngine;

public class DepthOfUINoCollider : MonoBehaviour
{
	[SetProperty("SortingOrder"), SerializeField]
	public int SortingOrder;

	private void Start()
	{
		this.RefreshDepth(this.SortingOrder);
	}

	public void RefreshDepth(int sortingOrder)
	{
		this.SortingOrder = sortingOrder;
		DepthManager.SetDepth(base.get_gameObject(), this.SortingOrder);
	}
}
