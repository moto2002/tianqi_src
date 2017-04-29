using System;
using UnityEngine;

public class DepthOfFieldOfUI_ctrl : MonoBehaviour
{
	public float maxBlurSize = 3.5f;

	private DepthOfFieldOfUI m_DepthOfFieldOfUI;

	private void OnEnable()
	{
		if (GameLevelManager.IsPostProcessReachQuality(1000) && this.m_DepthOfFieldOfUI == null)
		{
			this.m_DepthOfFieldOfUI = CamerasMgr.CameraUI.get_gameObject().AddComponent<DepthOfFieldOfUI>();
		}
	}

	private void OnDisable()
	{
		Object.Destroy(this.m_DepthOfFieldOfUI);
	}

	private void Update()
	{
		if (this.m_DepthOfFieldOfUI != null)
		{
			this.m_DepthOfFieldOfUI.maxBlurSize = this.maxBlurSize;
		}
	}
}
