using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TalentActivation : MonoBehaviour
{
	private int m_itemId;

	private Image m_spItemIcon;

	private Text m_lblNum;

	public void AwakeSelf()
	{
		this.m_spItemIcon = base.get_transform().FindChild("ItemIcon").GetComponent<Image>();
		this.m_lblNum = base.get_transform().FindChild("Num").GetComponent<Text>();
		base.get_transform().FindChild("ItemIcon").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickItem));
	}

	private void OnClickItem()
	{
		UIManagerControl.Instance.OpenSourceReferenceUI(this.m_itemId, null);
	}

	public void SetItem(int itemId, int needNum)
	{
		this.m_itemId = itemId;
		ResourceManager.SetSprite(this.m_spItemIcon, GameDataUtils.GetItemLitterIcon(itemId));
		this.m_lblNum.set_text(BackpackManager.Instance.OnGetGoodCount(itemId) + "/" + needNum);
	}
}
