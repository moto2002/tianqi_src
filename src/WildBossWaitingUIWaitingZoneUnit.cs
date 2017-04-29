using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class WildBossWaitingUIWaitingZoneUnit : BaseUIBehaviour
{
	protected Text WildBossWaitingUIWaitingZoneUnitRank;

	protected Image WildBossWaitingUIWaitingZoneUnitHeadIcon;

	protected Text WildBossWaitingUIWaitingZoneUnitName;

	protected Text WildBossWaitingUIWaitingZoneUnitFighting;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.WildBossWaitingUIWaitingZoneUnitRank = base.FindTransform("WildBossWaitingUIWaitingZoneUnitRank").GetComponent<Text>();
		this.WildBossWaitingUIWaitingZoneUnitHeadIcon = base.FindTransform("WildBossWaitingUIWaitingZoneUnitHeadIcon").GetComponent<Image>();
		this.WildBossWaitingUIWaitingZoneUnitName = base.FindTransform("WildBossWaitingUIWaitingZoneUnitName").GetComponent<Text>();
		this.WildBossWaitingUIWaitingZoneUnitFighting = base.FindTransform("WildBossWaitingUIWaitingZoneUnitFighting").GetComponent<Text>();
	}

	public void SetData(int rank, int career, string name, long fighting)
	{
		this.SetRank(rank);
		this.SetHeadIcon(career);
		this.SetName(name);
		this.SetFighting(fighting);
	}

	protected void SetRank(int rank)
	{
		this.WildBossWaitingUIWaitingZoneUnitRank.set_text(rank.ToString());
	}

	protected void SetHeadIcon(int career)
	{
		ResourceManager.SetSprite(this.WildBossWaitingUIWaitingZoneUnitHeadIcon, UIUtils.GetRoleSmallIcon(career));
	}

	protected void SetName(string name)
	{
		this.WildBossWaitingUIWaitingZoneUnitName.set_text(name);
	}

	protected void SetFighting(long fighting)
	{
		this.WildBossWaitingUIWaitingZoneUnitFighting.set_text(string.Format(GameDataUtils.GetChineseContent(502070, false), fighting));
	}
}
