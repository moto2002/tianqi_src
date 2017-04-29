using System;
using UnityEngine;

public class UIAnimatorDisableWhenNoactive : MonoBehaviour
{
	public Action DisableCallBack;

	private void OnDisable()
	{
		if (this.DisableCallBack != null)
		{
			this.DisableCallBack.Invoke();
		}
		Animator component = base.get_gameObject().GetComponent<Animator>();
		if (component != null)
		{
			component.set_enabled(false);
		}
	}
}
