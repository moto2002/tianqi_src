using System;
using UnityEngine;

public class ListViewEquipLibItem : MonoBehaviour
{
	public ListViewEquipLibSubItem item1;

	public ListViewEquipLibSubItem item2;

	public ListViewEquipLibSubItem item3;

	public ListViewEquipLibSubItem item4;

	public ListViewEquipLibSubItem item5;

	private void Awake()
	{
		this.item1 = base.get_transform().FindChild("ListViewEquipLibSubItem1").GetComponent<ListViewEquipLibSubItem>();
		this.item2 = base.get_transform().FindChild("ListViewEquipLibSubItem2").GetComponent<ListViewEquipLibSubItem>();
		this.item3 = base.get_transform().FindChild("ListViewEquipLibSubItem3").GetComponent<ListViewEquipLibSubItem>();
		this.item4 = base.get_transform().FindChild("ListViewEquipLibSubItem4").GetComponent<ListViewEquipLibSubItem>();
		this.item5 = base.get_transform().FindChild("ListViewEquipLibSubItem5").GetComponent<ListViewEquipLibSubItem>();
	}
}
