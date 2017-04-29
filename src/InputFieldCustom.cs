using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldCustom : InputField
{
	public Action<GameObject> onClickCustom;

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		if (this.onClickCustom != null)
		{
			this.onClickCustom.Invoke(base.get_gameObject());
		}
	}
}
