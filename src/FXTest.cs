using System;
using UnityEngine;

public class FXTest : MonoBehaviour
{
	private string input = string.Empty;

	private void OnGUI()
	{
		this.input = GUI.TextField(new Rect(100f, 100f, 100f, 100f), this.input);
		if (GUI.Button(new Rect(200f, 100f, 100f, 100f), "play") && !string.IsNullOrEmpty(this.input))
		{
			int num = int.Parse(this.input);
			if (num > 0)
			{
				FXManager.Instance.PlayFX(num, null, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
		}
	}
}
