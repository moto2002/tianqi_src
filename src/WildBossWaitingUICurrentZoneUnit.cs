using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class WildBossWaitingUICurrentZoneUnit : BaseUIBehaviour
{
	protected Image WildBossWaitingUICurrentZoneUnitHeadIcon;

	protected Text WildBossWaitingUICurrentZoneUnitName;

	protected Image WildBossWaitingUICurrentZoneUnitBloodFG;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.WildBossWaitingUICurrentZoneUnitHeadIcon = base.FindTransform("WildBossWaitingUICurrentZoneUnitHeadIcon").GetComponent<Image>();
		this.WildBossWaitingUICurrentZoneUnitName = base.FindTransform("WildBossWaitingUICurrentZoneUnitName").GetComponent<Text>();
		this.WildBossWaitingUICurrentZoneUnitBloodFG = base.FindTransform("WildBossWaitingUICurrentZoneUnitBloodFG").GetComponent<Image>();
	}

	public void SetData(int career, string name, float percentage)
	{
		this.SetHeadIcon(career);
		this.SetName(name);
		this.SetBlood(percentage);
	}

	protected void SetHeadIcon(int career)
	{
		ResourceManager.SetSprite(this.WildBossWaitingUICurrentZoneUnitHeadIcon, UIUtils.GetRoleSmallIcon(career));
	}

	protected void SetName(string name)
	{
		this.WildBossWaitingUICurrentZoneUnitName.set_text(name);
	}

	protected void SetBlood(float percentage)
	{
		this.WildBossWaitingUICurrentZoneUnitBloodFG.set_fillAmount(percentage);
	}
}
