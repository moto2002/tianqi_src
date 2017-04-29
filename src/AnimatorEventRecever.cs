using System;
using UnityEngine;

public class AnimatorEventRecever : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnActionStart(string _actName)
	{
	}

	public void OnActionEnd(string _actName)
	{
		EventDispatcher.Broadcast<string>(EventNames.OnAnimationEnd, _actName);
	}
}
