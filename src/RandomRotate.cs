using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
	public bool isRotate = true;

	public int fps = 30;

	public int x = 100;

	public int y = 200;

	public int z = 300;

	private float rangeX;

	private float rangeY;

	private float rangeZ;

	private float deltaTime;

	private bool isVisible;

	private void Start()
	{
		this.deltaTime = 1f / (float)this.fps;
		this.rangeX = (float)Random.Range(0, 10);
		this.rangeY = (float)Random.Range(0, 10);
		this.rangeZ = (float)Random.Range(0, 10);
	}

	private void OnBecameVisible()
	{
		this.isVisible = true;
		base.StartCoroutine(this.UpdateRotation());
	}

	private void OnBecameInvisible()
	{
		this.isVisible = false;
	}

	[DebuggerHidden]
	private IEnumerator UpdateRotation()
	{
		RandomRotate.<UpdateRotation>c__Iterator26 <UpdateRotation>c__Iterator = new RandomRotate.<UpdateRotation>c__Iterator26();
		<UpdateRotation>c__Iterator.<>f__this = this;
		return <UpdateRotation>c__Iterator;
	}
}
