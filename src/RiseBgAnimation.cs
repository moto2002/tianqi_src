using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RiseBgAnimation : MonoBehaviour
{
	public Transform bg;

	private void Start()
	{
		base.StartCoroutine(this.MoveBG());
	}

	[DebuggerHidden]
	private IEnumerator MoveBG()
	{
		RiseBgAnimation.<MoveBG>c__Iterator3F <MoveBG>c__Iterator3F = new RiseBgAnimation.<MoveBG>c__Iterator3F();
		<MoveBG>c__Iterator3F.<>f__this = this;
		return <MoveBG>c__Iterator3F;
	}

	private void Update()
	{
	}
}
