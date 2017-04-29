using System;
using UnityEngine;

public class FXSorting : MonoBehaviour
{
	[SetProperty("SortingOrder"), SerializeField]
	private int _SortingOrder;

	[SetProperty("IsContainChildren"), SerializeField]
	private bool _IsContainChildren;

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

	public bool IsContainChildren
	{
		get
		{
			return this._IsContainChildren;
		}
		set
		{
			this._IsContainChildren = value;
			this.ResetDepth();
		}
	}

	private void Start()
	{
		this.ResetDepth();
	}

	private void ResetDepth()
	{
		if (this.IsContainChildren)
		{
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_sortingOrder(this.SortingOrder);
			}
		}
		else
		{
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				component.set_sortingOrder(this.SortingOrder);
			}
		}
	}
}
