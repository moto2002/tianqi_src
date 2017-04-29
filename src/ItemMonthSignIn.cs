using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemMonthSignIn : MonoBehaviour
{
	public List<MonthSignInChildItem> childItems = new List<MonthSignInChildItem>();

	private void Awake()
	{
		this.childItems.Add(base.get_transform().FindChild("Item1").GetComponent<MonthSignInChildItem>());
		this.childItems.Add(base.get_transform().FindChild("Item2").GetComponent<MonthSignInChildItem>());
		this.childItems.Add(base.get_transform().FindChild("Item3").GetComponent<MonthSignInChildItem>());
		this.childItems.Add(base.get_transform().FindChild("Item4").GetComponent<MonthSignInChildItem>());
		this.childItems.Add(base.get_transform().FindChild("Item5").GetComponent<MonthSignInChildItem>());
	}
}
