using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentLevelZone : MonoBehaviour
{
	public int m_row;

	private ListPool m_ItemPool;

	private Image m_spLevel10;

	private Image m_spLevel1;

	public List<RoleTalentItem> listItems = new List<RoleTalentItem>();

	private void Awake()
	{
		this.m_ItemPool = base.get_transform().FindChild("ItemList").GetComponent<ListPool>();
		this.m_ItemPool.SetItem("RoleTalentItem");
		this.m_spLevel10 = base.get_transform().FindChild("Levels").FindChild("Level10").GetComponent<Image>();
		this.m_spLevel1 = base.get_transform().FindChild("Levels").FindChild("Level1").GetComponent<Image>();
	}

	public void RefreshTalents(List<int> talentIds, Action finishCallback)
	{
		int num = talentIds.get_Count();
		this.listItems.Clear();
		this.m_ItemPool.Create(num, delegate(int index)
		{
			if (index < num && index < this.m_ItemPool.Items.get_Count())
			{
				RoleTalentItem component = this.m_ItemPool.Items.get_Item(index).GetComponent<RoleTalentItem>();
				component.m_row = this.m_row;
				component.m_column = index;
				int cfgId = talentIds.get_Item(index);
				component.SetInfo(RoleTalentManager.Instance.GetDataCurrent(cfgId));
				component.SetLevel(RoleTalentManager.Instance.GetLevel(cfgId), RoleTalentManager.Instance.GetMaxLevel(cfgId));
				this.listItems.Add(component);
			}
			if (index == num - 1 && finishCallback != null)
			{
				finishCallback.Invoke();
			}
		});
	}

	public void SetLevel(int num)
	{
		ResourceManager.SetSprite(this.m_spLevel1, GameDataUtils.GetNumIcon1(num, NumType.jn));
		ResourceManager.SetSprite(this.m_spLevel10, GameDataUtils.GetNumIcon10(num, NumType.jn));
	}
}
