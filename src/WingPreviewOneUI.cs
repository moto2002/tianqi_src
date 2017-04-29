using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;

public class WingPreviewOneUI : UIBase
{
	public static WingPreviewOneUI instance;

	private WingPreviewCell mWingPreviewCell;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		WingPreviewOneUI.instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	private void OnClickBtnUndress()
	{
		WingManager.Instance.SendWingWearReq(0);
		this.Show(false);
	}

	private void OnClickBtnGet(int itemId)
	{
		LinkNavigationManager.ItemNotEnoughToLink(itemId, true, delegate
		{
			LinkNavigationManager.OpenXMarketUI2();
			this.Show(false);
		}, true);
	}

	private void OnClickBtnWear(int wingId)
	{
		WingManager.Instance.SendWingWearReq(wingId);
		this.Show(false);
	}

	public void InitWithMaxLv(int wingId)
	{
		this.Init(wingId);
		this.mWingPreviewCell.SetCondition(true, "已获得最高级");
		wings wingInfo = WingManager.GetWingInfo(wingId);
		int wingLv = WingManager.GetWingLv(wingId);
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv);
		this.mWingPreviewCell.SetName(TextColorMgr.GetColorByQuality(wingLvInfo.name, wingLvInfo.color));
	}

	public void InitWithNotActive(int wingId)
	{
		this.Init(wingId);
		wings wingInfo = WingManager.GetWingInfo(wingId);
		this.mWingPreviewCell.ShowButtonGet(true);
		this.mWingPreviewCell.actionButtonGet = delegate
		{
			this.OnClickBtnGet(wingInfo.activation.get_Item(0).key);
		};
	}

	public void InitWithWear(int wingId)
	{
		this.Init(wingId);
		this.mWingPreviewCell.ShowButtonWear(true);
		this.mWingPreviewCell.actionButtonWear = delegate
		{
			this.OnClickBtnWear(wingId);
		};
	}

	public void InitWithUndress(int wingId)
	{
		this.Init(wingId);
		this.mWingPreviewCell.ShowButtonUndress(true);
		this.mWingPreviewCell.actionButtonUndress = delegate
		{
			this.OnClickBtnUndress();
		};
	}

	private void Init(int wingId)
	{
		WingGlobal.ResetRawImage();
		if (this.mWingPreviewCell != null && this.mWingPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.mWingPreviewCell.get_gameObject());
		}
		wings wingInfo = WingManager.GetWingInfo(wingId);
		int wingLv = WingManager.GetWingLv(wingId);
		int wingModel = WingManager.GetWingModel(wingId, wingLv);
		this.mWingPreviewCell = WingGlobal.GetOneWingPreview(base.get_transform());
		this.mWingPreviewCell.SetRawImage(wingModel);
		this.mWingPreviewCell.SetName(TextColorMgr.GetColorByQuality(wingInfo.name, wingInfo.color));
	}

	private void OnApplicationPause(bool bPause)
	{
		WingGlobal.ResetRawImage();
		this.mWingPreviewCell.DoOnApplicationPause();
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy())
			{
				WingGlobal.ResetRawImage();
				this.mWingPreviewCell.DoOnApplicationPause();
			}
		});
	}
}
