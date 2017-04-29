using System;
using UnityEngine;

public class DepthOfUI : MonoBehaviour
{
	public int SiblingIndex;

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

	private void Start()
	{
		DepthManager.SetGraphicRaycaster(base.get_gameObject());
		this.ResetDepth();
	}

	public void ResetDepth()
	{
		if (Application.get_isPlaying())
		{
			DepthManager.SetDepth(base.get_gameObject(), this.SortingOrder);
		}
	}
}
