using System;
using UnityEngine;

public class CameraRange : MonoBehaviour
{
	public static CameraRange intance;

	public Transform target;

	private Transform MainCamera;

	private static string[] CameraRangeLayers = new string[]
	{
		"CameraRange"
	};

	private void Start()
	{
		CameraRange.intance = this;
		this.MainCamera = CamerasMgr.MainCameraRoot;
	}

	public void SetCameraPos(Vector3 dstPos)
	{
		this.MainCamera.set_position(this.GetRangePoint(dstPos));
	}

	public Vector3 GetRangePoint(Vector3 dstPos)
	{
		return this.GetRangePointBy1(dstPos);
	}

	private Vector3 GetRangePointBy1(Vector3 dstPos)
	{
		if (this.target != null)
		{
			RaycastHit raycastHit;
			bool flag = Physics.Linecast(this.target.get_position(), dstPos, ref raycastHit, LayerSystem.GetMask(CameraRange.CameraRangeLayers));
			if (flag)
			{
				return raycastHit.get_point();
			}
		}
		return dstPos;
	}

	private Vector3 GetRangePointBy2(Vector3 dstPos)
	{
		if (this.target != null)
		{
			RaycastHit raycastHit;
			bool flag = Physics.Linecast(this.target.get_position(), dstPos, ref raycastHit, LayerSystem.GetMask(CameraRange.CameraRangeLayers));
			if (flag)
			{
				float num = Mathf.Abs(this.target.get_transform().get_position().x - dstPos.x);
				float num2 = Mathf.Abs(this.target.get_transform().get_position().y - dstPos.y);
				float num3 = Mathf.Abs(this.target.get_transform().get_position().x - raycastHit.get_point().x);
				float num4 = Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f) - Mathf.Pow(num3, 2f);
				num4 = Mathf.Pow(num4, 0.5f) + this.target.get_transform().get_position().y;
				return new Vector3(raycastHit.get_point().x, num4, raycastHit.get_point().z);
			}
		}
		return dstPos;
	}
}
