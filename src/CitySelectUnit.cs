using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitySelectUnit : MonoBehaviour
{
	protected Image icon;

	protected GameObject currentFlag;

	public ButtonCustom btn;

	protected int cityID;

	private void Awake()
	{
		this.icon = base.get_transform().FindChild("CitySelectUnitIcon").GetComponent<Image>();
		this.currentFlag = base.get_transform().FindChild("CitySelectUnitCurrentFlag").get_gameObject();
		this.btn = base.GetComponent<ButtonCustom>();
		this.btn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClick);
	}

	public void Show(bool isShow)
	{
		base.get_gameObject().SetActive(isShow);
	}

	public void SetCurrent(bool isCurrent)
	{
		this.currentFlag.SetActive(isCurrent);
	}

	public void SetData(int id, int iconID, List<int> localPosition)
	{
		this.cityID = id;
		ResourceManager.SetSprite(this.icon, GameDataUtils.GetIcon(iconID));
		float num = base.get_transform().get_localPosition().x;
		float num2 = base.get_transform().get_localPosition().y;
		if (localPosition.get_Count() > 0)
		{
			num = (float)localPosition.get_Item(0);
			if (localPosition.get_Count() > 1)
			{
				num2 = (float)localPosition.get_Item(1);
			}
		}
		base.get_transform().set_localPosition(new Vector3(num, num2, base.get_transform().get_localPosition().z));
	}

	protected void OnClick(GameObject go)
	{
		EventDispatcher.Broadcast<int>(CitySelectUIEvent.OnUnitClick, this.cityID);
	}
}
