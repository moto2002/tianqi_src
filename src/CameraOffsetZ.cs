using System;
using UnityEngine;

public class CameraOffsetZ : MonoBehaviour
{
	public float zOffset;

	private GameObject m_temp;

	private Camera mainCamera
	{
		get
		{
			if (CamerasMgr.CameraMain != null)
			{
				return CamerasMgr.CameraMain;
			}
			return Camera.get_main();
		}
	}

	private void Awake()
	{
		this.m_temp = new GameObject("zOffset");
		this.m_temp.get_transform().set_parent(base.get_transform().get_parent());
		this.m_temp.get_transform().set_localPosition(base.get_transform().get_localPosition());
		this.m_temp.get_transform().set_localRotation(base.get_transform().get_localRotation());
		this.m_temp.get_transform().set_localScale(base.get_transform().get_localScale());
	}

	private void Update()
	{
		if (this.mainCamera != null && this.mainCamera.get_enabled() && base.get_transform() != null)
		{
			Vector3 vector = this.mainCamera.WorldToViewportPoint(this.m_temp.get_transform().get_position());
			base.get_transform().set_position(this.mainCamera.ViewportToWorldPoint(vector + new Vector3(0f, 0f, this.zOffset)));
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(this.m_temp);
	}
}
