using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class WaterWaveUvAnimation : MonoBehaviour
{
	public float speed = 1f;

	public int fps = 30;

	public Color color;

	private Material mat;

	private float offset;

	private float offsetHeight;

	private float delta;

	private void Start()
	{
		this.mat = base.GetComponent<Renderer>().get_material();
		this.delta = 1f / (float)this.fps * this.speed;
		base.StartCoroutine(this.updateTiling());
	}

	[DebuggerHidden]
	private IEnumerator updateTiling()
	{
		WaterWaveUvAnimation.<updateTiling>c__Iterator24 <updateTiling>c__Iterator = new WaterWaveUvAnimation.<updateTiling>c__Iterator24();
		<updateTiling>c__Iterator.<>f__this = this;
		return <updateTiling>c__Iterator;
	}
}
