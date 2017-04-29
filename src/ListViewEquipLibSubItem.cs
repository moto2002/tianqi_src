using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ListViewEquipLibSubItem : MonoBehaviour
{
	public Image ImageFrame;

	public Image ImageIcon;

	public Image ImageSelect;

	public Image ImageQuality;

	public EquipSimpleInfo equip;

	private void Awake()
	{
		this.ImageFrame = base.get_transform().FindChild("ImageFrame").GetComponent<Image>();
		this.ImageIcon = base.get_transform().FindChild("ImageIcon").GetComponent<Image>();
		this.ImageSelect = base.get_transform().FindChild("ImageSelect").GetComponent<Image>();
		this.ImageQuality = base.get_transform().FindChild("ImageQuality").GetComponent<Image>();
	}
}
