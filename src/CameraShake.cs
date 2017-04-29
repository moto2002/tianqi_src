using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private const bool checkForMinimumValues = true;

	private const float minShakeValue = 0.001f;

	public Camera mainCamera;

	public int time = 2;

	public Vector3 shakeDir = Vector3.get_one();

	public float distance = 0.1f;

	public float speed = 50f;

	public float decay = 0.2f;

	public float guiShakeModifier = 1f;

	public bool multiplyByTimeScale;

	private bool shaking;

	private bool cancelling;

	public static CameraShake instance;

	private Vector3 offsetCache = default(Vector3);

	public event Action cameraShakeStarted
	{
		[MethodImpl(32)]
		add
		{
			this.cameraShakeStarted = (Action)Delegate.Combine(this.cameraShakeStarted, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.cameraShakeStarted = (Action)Delegate.Remove(this.cameraShakeStarted, value);
		}
	}

	public static bool isShaking
	{
		get
		{
			return CameraShake.instance.IsShaking();
		}
	}

	public static bool isCancelling
	{
		get
		{
			return CameraShake.instance.IsCancelling();
		}
	}

	private void OnEnable()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = CamerasMgr.CameraMain;
		}
		if (this.mainCamera == null)
		{
			Debuger.Info("Error:No camera", new object[0]);
		}
		CameraShake.instance = this;
	}

	public void Shake()
	{
		CameraShake.instance.DoShake((float)this.time, this.shakeDir, this.distance, this.speed, this.decay, this.multiplyByTimeScale);
	}

	public static void Shake(float time, Vector3 shakedir, float distance, float speed, float decay, bool multiplyByTimeScale = false)
	{
		CameraShake.instance.DoShake(time, shakedir, distance, speed, decay, multiplyByTimeScale);
	}

	public static void CancelShake()
	{
		CameraShake.instance.DoCancelShake();
	}

	public static void CancelShake(float time)
	{
		CameraShake.instance.DoCancelShake(time);
	}

	public bool IsShaking()
	{
		return this.shaking;
	}

	public bool IsCancelling()
	{
		return this.cancelling;
	}

	public void DoShake(float time, Vector3 shakeDir, float distance, float speed, float decay, bool multiplyByTimeScale = false)
	{
		Vector3 insideUnitSphere = Random.get_insideUnitSphere();
		base.StartCoroutine(this.DoShake_Internal(insideUnitSphere, time, shakeDir, distance, speed, decay, multiplyByTimeScale));
	}

	public void DoCancelShake()
	{
		if (this.shaking && !this.cancelling)
		{
			this.shaking = false;
			base.StopAllCoroutines();
			this.mainCamera.ResetWorldToCameraMatrix();
		}
	}

	public void DoCancelShake(float time)
	{
		if (this.shaking && !this.cancelling)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.DoResetState(time));
		}
	}

	[DebuggerHidden]
	private IEnumerator DoShake_Internal(Vector3 seed, float time, Vector3 dir, float distance, float speed, float decay, bool multiplyByTimeScale = false)
	{
		CameraShake.<DoShake_Internal>c__Iterator17 <DoShake_Internal>c__Iterator = new CameraShake.<DoShake_Internal>c__Iterator17();
		<DoShake_Internal>c__Iterator.seed = seed;
		<DoShake_Internal>c__Iterator.time = time;
		<DoShake_Internal>c__Iterator.distance = distance;
		<DoShake_Internal>c__Iterator.multiplyByTimeScale = multiplyByTimeScale;
		<DoShake_Internal>c__Iterator.dir = dir;
		<DoShake_Internal>c__Iterator.speed = speed;
		<DoShake_Internal>c__Iterator.decay = decay;
		<DoShake_Internal>c__Iterator.<$>seed = seed;
		<DoShake_Internal>c__Iterator.<$>time = time;
		<DoShake_Internal>c__Iterator.<$>distance = distance;
		<DoShake_Internal>c__Iterator.<$>multiplyByTimeScale = multiplyByTimeScale;
		<DoShake_Internal>c__Iterator.<$>dir = dir;
		<DoShake_Internal>c__Iterator.<$>speed = speed;
		<DoShake_Internal>c__Iterator.<$>decay = decay;
		<DoShake_Internal>c__Iterator.<>f__this = this;
		return <DoShake_Internal>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator DoResetState(float time)
	{
		CameraShake.<DoResetState>c__Iterator18 <DoResetState>c__Iterator = new CameraShake.<DoResetState>c__Iterator18();
		<DoResetState>c__Iterator.time = time;
		<DoResetState>c__Iterator.<$>time = time;
		<DoResetState>c__Iterator.<>f__this = this;
		return <DoResetState>c__Iterator;
	}
}
