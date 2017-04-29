using System;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
	public static ThreadManager Instance;

	private void Awake()
	{
		ThreadManager.Instance = this;
	}
}
