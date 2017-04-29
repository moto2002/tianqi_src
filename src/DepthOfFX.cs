using System;
using UnityEngine;

public class DepthOfFX : MonoBehaviour
{
	[SetProperty("SortingOrder"), SerializeField]
	private int _SortingOrder;

	public int SortingOrder
	{
		get
		{
			return this._SortingOrder;
		}
		set
		{
			this._SortingOrder = Mathf.Max(2, value);
			this.ResetDepth();
		}
	}

	private void OnEnable()
	{
		this.ResetDepth();
	}

	private void ResetDepth()
	{
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].set_sortingOrder(this.SortingOrder);
		}
	}
}
