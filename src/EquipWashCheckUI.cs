using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;

public class EquipWashCheckUI : UIBase
{
	private ListPool brilliantAttrListPool;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
		this.brilliantAttrListPool = base.FindTransform("BrilliantAttrGrid").GetComponent<ListPool>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.brilliantAttrListPool.Clear();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.brilliantAttrListPool.Clear();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void RefreshUI(int equipCfgID)
	{
		List<int> attrIDList = EquipGlobal.GetExcellentCheckList(equipCfgID);
		if (attrIDList != null && attrIDList.get_Count() > 0)
		{
			this.brilliantAttrListPool.Create(attrIDList.get_Count(), delegate(int index)
			{
				if (index < attrIDList.get_Count() && index < this.brilliantAttrListPool.Items.get_Count())
				{
					BrilliantAttrCheckItem component = this.brilliantAttrListPool.Items.get_Item(index).GetComponent<BrilliantAttrCheckItem>();
					if (component != null)
					{
						component.RefreshUI(equipCfgID, attrIDList.get_Item(index));
					}
				}
			});
		}
	}
}
