using System;
using UnityEngine;

public class CameraPathDelay : CameraPathPoint
{
	public float time;

	public float introStartEasePercentage = 0.1f;

	public AnimationCurve introCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public float outroEndEasePercentage = 0.1f;

	public AnimationCurve outroCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
}
