using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class StrongerUI : UIBase
{
	private ListView2 listView;

	private Text myPowerText;

	private Text standardPowerText;

	private ButtonCustom closeBtn;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isClick = true;
		this.isMask = true;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.listView = base.FindTransform("ListItems").GetComponent<ListView2>();
		this.myPowerText = base.FindTransform("TextPowerNum").GetComponent<Text>();
		this.standardPowerText = base.FindTransform("TextMaxPowerNum").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		this.listView.DoAnimation();
		base.FindTransform("ListScrollRect").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		this.myPowerText.set_text(EntityWorld.Instance.EntSelf.Fighting.ToString());
		int lv = EntityWorld.Instance.EntSelf.Lv;
		DangQianDengJiLiLunZhanLi dangQianDengJiLiLunZhanLi = DataReader<DangQianDengJiLiLunZhanLi>.Get(lv);
		if (dangQianDengJiLiLunZhanLi != null)
		{
			this.standardPowerText.set_text(dangQianDengJiLiLunZhanLi.idealStrength.ToString());
		}
		this.UpdateStrongerListUI();
	}

	private void UpdateStrongerListUI()
	{
		List<StrongerInfoData> canShowStrongerDataList = StrongerManager.Instance.GetCanShowStrongerDataList();
		int count = canShowStrongerDataList.get_Count();
		this.listView.CreateRow(count, 0);
		for (int i = 0; i < canShowStrongerDataList.get_Count(); i++)
		{
			this.listView.Items.get_Item(i).GetComponent<StrongerUiItem>().UpdateItem(canShowStrongerDataList.get_Item(i));
		}
	}
}
