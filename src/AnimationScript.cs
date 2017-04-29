using System;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
	public EaseType moveEase;

	public EaseType stopEase;

	public float moveDuration;

	public float stopTweenDuration;

	public Vector3 stopScale = Vector3.get_one();

	public Vector3 endPos;

	public int startIndex;

	private void Start()
	{
	}
}
