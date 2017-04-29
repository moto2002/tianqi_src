using System;
using UnityEngine;

public class ControllerTool
{
	public static bool SplitIsCity(Animator animator)
	{
		return animator != null && animator.get_runtimeAnimatorController() != null && animator.get_runtimeAnimatorController().get_name().ToLower().Contains("city");
	}
}
