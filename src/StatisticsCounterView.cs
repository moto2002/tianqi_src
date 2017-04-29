using System;
using UnityEngine;

internal class StatisticsCounterView : MonoBehaviour
{
	private void OnGUI()
	{
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.set_fontSize(30);
		GUI.Label(new Rect(10f, 10f, 100f, 30f), string.Empty, gUIStyle);
	}
}
