using System;
using UnityEngine;

public class TurnOffCamera : MonoBehaviour
{
	private void Awake()
	{
		base.get_gameObject().SetActive(false);
	}
}
