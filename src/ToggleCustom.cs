using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleCustom : Toggle
{
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		base.OnPointerClick(eventData);
		SoundManager.PlayUI(base.get_gameObject());
		UIStateSystem.LockOfClickInterval(0u);
	}
}
