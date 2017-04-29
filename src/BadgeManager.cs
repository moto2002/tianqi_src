using System;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : BaseSubSystemManager
{
	private Dictionary<BadgeType, bool> m_dicBadgeType = new Dictionary<BadgeType, bool>();

	private static BadgeManager m_Instance;

	public static BadgeManager Instance
	{
		get
		{
			if (BadgeManager.m_Instance == null)
			{
				BadgeManager.m_Instance = new BadgeManager();
			}
			return BadgeManager.m_Instance;
		}
	}

	private BadgeManager()
	{
	}

	private void InitBadgeType()
	{
		this.m_dicBadgeType.set_Item(BadgeType.Actor, false);
		this.m_dicBadgeType.set_Item(BadgeType.Backpack, false);
		this.m_dicBadgeType.set_Item(BadgeType.Skill, false);
		this.m_dicBadgeType.set_Item(BadgeType.Element, false);
		this.m_dicBadgeType.set_Item(BadgeType.Pet, false);
		this.m_dicBadgeType.set_Item(BadgeType.Rise, false);
		this.m_dicBadgeType.set_Item(BadgeType.Equip, false);
	}

	public override void Init()
	{
		base.Init();
		this.InitBadgeType();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
	}

	public void ResetAllBadgeData()
	{
		this.SetBadgeType(BadgeType.Pet, PetManager.Instance.CheckBadge());
		this.SetBadgeType(BadgeType.Equip, GemManager.Instance.IsCanWearGem() || EquipmentManager.Instance.CheckCanShowEnchantmentTipAllPos() || EquipmentManager.Instance.CheckCanShowStrengthenTipAllPos() || EquipmentManager.Instance.CheckCanShowStarUpTipAllPos());
		this.SetBadgeType(BadgeType.Skill, SkillUIManager.Instance.CheckRoleSkillsCanUpgrade());
		this.SetBadgeType(BadgeType.Actor, TitleManager.Instance.HasNewTitle() || EquipmentManager.Instance.CheckCanChangeEquipAllPos() || WingManager.CheckAllBadge());
		this.SetBadgeType(BadgeType.Backpack, BackpackManager.Instance.IsCanShowRedPoint);
	}

	public void SetBadgeType(BadgeType badgeType, bool show)
	{
		if (!this.m_dicBadgeType.ContainsKey(badgeType))
		{
			Debug.LogError("字典内不存在 badgeType " + badgeType);
			return;
		}
		this.m_dicBadgeType.set_Item(badgeType, show);
	}

	public bool GetBadgeTypeIsShow(BadgeType badgeType)
	{
		return this.m_dicBadgeType.ContainsKey(badgeType) && this.m_dicBadgeType.get_Item(badgeType);
	}
}
