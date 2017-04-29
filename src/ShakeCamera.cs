using GameData;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
	public static ShakeCamera instance;

	private Transform mainCamera;

	public shakeType type = shakeType.horizontal;

	private float shakedis = 0.2f;

	private float shakeCycle = 45f;

	private float ran;

	private float _shakeTime;

	private float angle;

	private int count;

	private float pre;

	[SerializeField]
	private bool isShake;

	protected static XDict<int, uint> cameraShaderTimerList = new XDict<int, uint>();

	public bool IsShake
	{
		get
		{
			return this.isShake;
		}
	}

	private void Start()
	{
		this.mainCamera = CamerasMgr.MainCameraRoot;
		ShakeCamera.instance = this;
	}

	public void HandleCameraEffect(int id)
	{
		CameraAnimation cameraAnimation = DataReader<CameraAnimation>.Get(id);
		if (cameraAnimation == null)
		{
			return;
		}
		switch (cameraAnimation.aniType)
		{
		case 1:
			this.StartCameraAnimation(cameraAnimation);
			break;
		case 3:
			if (cameraAnimation != null && cameraAnimation.shader != null && cameraAnimation.shader.get_Count() > 0)
			{
				for (int i = 0; i < cameraAnimation.shader.get_Count(); i++)
				{
					int cameraShader = cameraAnimation.shader.get_Item(i);
					EventDispatcher.Broadcast<int, bool>(ShaderEffectEvent.CAMERA_ANIMATION_EFFECT, cameraShader, true);
					if (ShakeCamera.cameraShaderTimerList.ContainsKey(cameraAnimation.shader.get_Item(i)))
					{
						TimerHeap.DelTimer(ShakeCamera.cameraShaderTimerList[cameraShader]);
					}
					uint value = TimerHeap.AddTimer((uint)cameraAnimation.time, 0, delegate
					{
						EventDispatcher.Broadcast<int, bool>(ShaderEffectEvent.CAMERA_ANIMATION_EFFECT, cameraShader, false);
					});
					ShakeCamera.cameraShaderTimerList[cameraShader] = value;
				}
			}
			break;
		}
	}

	protected void StartCameraAnimation(CameraAnimation cameraData)
	{
		if (cameraData == null || this.mainCamera == null || this.isShake)
		{
			return;
		}
		if (cameraData.time > 0)
		{
			this.shakedis = (float)cameraData.swing * 0.01f;
			this._shakeTime = (float)cameraData.time * 0.001f;
			this.shakeCycle = 90f;
			this.type = (shakeType)cameraData.type;
			this.ran = cameraData.rate;
			base.StartCoroutine(this.Shake());
		}
	}

	private void ClearData()
	{
		this.shakedis = 0f;
		this._shakeTime = 0f;
		this.shakeCycle = 0f;
		this.type = (shakeType)0;
		this.ran = 0f;
		this.angle = 0f;
		this.count = 0;
		this.isShake = false;
	}

	[DebuggerHidden]
	private IEnumerator Shake()
	{
		ShakeCamera.<Shake>c__Iterator19 <Shake>c__Iterator = new ShakeCamera.<Shake>c__Iterator19();
		<Shake>c__Iterator.<>f__this = this;
		return <Shake>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator DelayContinuShake(int num)
	{
		ShakeCamera.<DelayContinuShake>c__Iterator1A <DelayContinuShake>c__Iterator1A = new ShakeCamera.<DelayContinuShake>c__Iterator1A();
		<DelayContinuShake>c__Iterator1A.num = num;
		<DelayContinuShake>c__Iterator1A.<$>num = num;
		<DelayContinuShake>c__Iterator1A.<>f__this = this;
		return <DelayContinuShake>c__Iterator1A;
	}

	private void ShakeCorn()
	{
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
			float num2 = this.angle % 360f;
			if (num2 == 180f)
			{
				num2 = 0f;
			}
			vector = vector * Mathf.Sin(num2 * 0.0174532924f) * this.shakedis;
		}
		this.mainCamera.get_transform().set_localPosition(this.mainCamera.get_localPosition() + vector);
	}

	private void StopShake()
	{
		base.StopCoroutine(this.Shake());
		if (this.count % 4 != 0)
		{
			int num = 4 - this.count % 4;
			this.count = 0;
			for (int i = 0; i < num; i++)
			{
				this.ShakeCorn();
			}
		}
	}

	public void ResetData()
	{
		for (int i = 0; i < ShakeCamera.cameraShaderTimerList.Count; i++)
		{
			TimerHeap.DelTimer(ShakeCamera.cameraShaderTimerList.ElementValueAt(i));
		}
		ShakeCamera.cameraShaderTimerList.Clear();
	}
}
