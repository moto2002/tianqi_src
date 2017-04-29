using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine.UI;

public class BrilliantAttrCheckItem : BaseUIBehaviour
{
	private Text attrText;

	private Text attrRangeText;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.attrText = base.FindTransform("AttrText").GetComponent<Text>();
		this.attrRangeText = base.FindTransform("AttrRangeText").GetComponent<Text>();
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void RefreshUI(int equipCfgID, int attrID)
	{
		this.attrText.set_text(string.Empty);
		if (attrID > 0)
		{
			this.attrText.set_text(AttrUtility.GetAttrName((AttrType)attrID));
			this.attrRangeText.set_text(EquipGlobal.GetExcellentRangeText(equipCfgID, attrID));
		}
	}
}
