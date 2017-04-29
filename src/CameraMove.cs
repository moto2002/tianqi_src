using GameData;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public static CameraMove intance;

	private Transform mainCamera;

	private CameraAnimation cameraData;

	public shakeType type = shakeType.horizontal;

	private float shakedis = 0.2f;

	private float shakeCycle = 45f;

	private float ran;

	private float shakeTime;

	private float angle = 90f;

	private float pre;

	private bool isShake;

	public bool IsShake
	{
		get
		{
			return this.isShake;
		}
		set
		{
			this.isShake = value;
		}
	}

	private void Awake()
	{
		CameraMove.intance = this;
		this.mainCamera = CamerasMgr.MainCameraRoot;
	}

	private void Start()
	{
	}

	[DebuggerHidden]
	private IEnumerator UpdateShake()
	{
		CameraMove.<UpdateShake>c__Iterator16 <UpdateShake>c__Iterator = new CameraMove.<UpdateShake>c__Iterator16();
		<UpdateShake>c__Iterator.<>f__this = this;
		return <UpdateShake>c__Iterator;
	}

	public void UpdateMove()
	{
		if (this.isShake)
		{
			this.ShakeCamera();
		}
	}

	private void ShakeCamera()
	{
		if (this.shakeTime > 0f)
		{
			this.shakeTime -= Time.get_deltaTime();
			Vector3 vector;
			switch (this.type)
			{
			case shakeType.horizontal:
				vector = Vector3.get_right();
				break;
			case shakeType.vertical:
				vector = Vector3.get_up();
				break;
			case shakeType.zoom:
				vector = Vector3.get_forward();
				break;
			default:
				vector = Vector3.get_zero();
				break;
			}
			if (this.ran > 0f)
			{
				this.ran *= this.shakedis;
				float num = Random.Range(-this.ran, this.ran);
				if ((num > 0f && this.pre > 0f) || (num < 0f && this.pre < 0f))
				{
					num = -num;
				}
				this.pre = num;
				vector *= num;
			}
			else
			{
				this.angle += this.shakeCycle;
				vector = vector * Mathf.Sin(this.angle * 0.0174532924f) * this.shakedis;
			}
			this.mainCamera.get_transform().set_localPosition(this.mainCamera.get_localPosition() + vector * 10f);
		}
		else
		{
			this.mainCamera.get_transform().set_localPosition(this.mainCamera.get_localPosition());
			this.angle = 90f;
			this.isShake = false;
		}
	}

	public void StartShake(CameraAnimation cameraData)
	{
		if (this.isShake)
		{
			return;
		}
		Debuger.Info("cameraData.time:" + cameraData.time, new object[0]);
		if (cameraData.time > 0)
		{
			this.shakedis = (float)cameraData.swing / 100f;
			this.shakeTime = (float)cameraData.time / 1000f;
			this.shakeCycle = cameraData.revolution * 90f;
			this.type = (shakeType)cameraData.type;
			this.ran = cameraData.rate;
			this.isShake = true;
		}
	}
}
