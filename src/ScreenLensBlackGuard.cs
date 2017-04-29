using System;
using UnityEngine;

public class ScreenLensBlackGuard : MonoBehaviour
{
	private void Awake()
	{
		this.CameraInStroy(false);
		EventDispatcher.AddListener<bool>(ShaderEffectEvent.CAMERA_IN_STORY, new Callback<bool>(this.CameraInStroy));
	}

	private void CameraInStroy(bool bEnable)
	{
		this.EnableRenders(bEnable);
	}

	private void EnableRenders(bool bEnable)
	{
		if (this != null && base.get_transform() != null)
		{
			Renderer component = base.get_transform().GetComponent<Renderer>();
			if (component != null)
			{
				component.set_enabled(bEnable);
			}
		}
	}
}
