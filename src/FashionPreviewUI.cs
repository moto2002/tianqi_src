using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FashionPreviewUI : UIBase
{
	protected Transform FashionPreviewCellSlot;

	protected FashionPreviewCell fashionPreviewCell;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FashionPreviewCellSlot = base.FindTransform("FashionPreviewCellSlot");
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetDress(List<string> allFashionDataID, string fashionDataID)
	{
		this.SetPreview(allFashionDataID, fashionDataID, FashionPreviewCell.FashionPreviewCellType.Dress);
	}

	public void SetUndress(List<string> allFashionDataID, string fashionDataID)
	{
		this.SetPreview(allFashionDataID, fashionDataID, FashionPreviewCell.FashionPreviewCellType.Undress);
	}

	public void SetRenewal(List<string> allFashionDataID, string fashionDataID)
	{
		this.SetPreview(allFashionDataID, fashionDataID, FashionPreviewCell.FashionPreviewCellType.Renewal);
	}

	public void SetBuy(List<string> allFashionDataID, string fashionDataID)
	{
		this.SetPreview(allFashionDataID, fashionDataID, FashionPreviewCell.FashionPreviewCellType.Buy);
	}

	public void SetDisplay(List<string> allFashionDataID, string fashionDataID)
	{
		this.SetPreview(allFashionDataID, fashionDataID, FashionPreviewCell.FashionPreviewCellType.None);
	}

	protected void SetPreview(List<string> allFashionDataID, string fashionDataID, FashionPreviewCell.FashionPreviewCellType type)
	{
		if (this.fashionPreviewCell != null && this.fashionPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.fashionPreviewCell.get_gameObject());
		}
		this.fashionPreviewCell = FashionPreviewManager.Instance.GetPreview(this.FashionPreviewCellSlot);
		this.fashionPreviewCell.Bind(this);
		this.fashionPreviewCell.SetData(allFashionDataID, fashionDataID, type, true);
	}

	private void OnApplicationPause(bool isPause)
	{
		if (this.fashionPreviewCell)
		{
			this.fashionPreviewCell.DoOnApplicationPause();
		}
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy() && this.fashionPreviewCell)
			{
				this.fashionPreviewCell.DoOnApplicationPause();
			}
		});
	}
}
