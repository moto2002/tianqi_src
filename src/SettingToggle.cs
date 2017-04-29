using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
	public int m_pushId;

	public bool IsOn
	{
		get
		{
			return base.GetComponent<Toggle>().get_isOn();
		}
	}

	public void SetSettingType(int pushId)
	{
		this.m_pushId = pushId;
	}

	public void SetName(string name)
	{
		base.get_transform().FindChild("SettingToggleText").GetComponent<Text>().set_text(name);
	}

	public void SetToggle(bool isOn)
	{
		base.GetComponent<Toggle>().set_isOn(isOn);
	}
}
