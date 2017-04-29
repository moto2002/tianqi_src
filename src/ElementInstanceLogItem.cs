using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceLogItem : MonoBehaviour
{
	public Text TextLog;

	private void Awake()
	{
		this.TextLog = base.get_transform().FindChild("TextLog").GetComponent<Text>();
	}
}
