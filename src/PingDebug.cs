using System;
using UnityEngine;

public class PingDebug : MonoBehaviour
{
	[HideInInspector]
	public Rect debugPingRect = new Rect(110f, 5f, 500f, 500f);

	private void Awake()
	{
	}

	private void OnGUI()
	{
		this.DrawPingGUI();
	}

	protected void DrawPingGUI()
	{
		if (!SystemConfig.IsDebugPing)
		{
			return;
		}
		GUILayout.BeginArea(this.debugPingRect);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUI.set_color(Color.get_red());
		GUIStyle gUIStyle = new GUIStyle(GUI.get_skin().get_label());
		gUIStyle.set_fontSize(24);
		string text = (NetworkManager.Instance.PingValue != -1) ? ("Ping: " + NetworkManager.Instance.PingValue) : string.Empty;
		GUILayout.Label(text, gUIStyle, new GUILayoutOption[0]);
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void Update()
	{
		if (!NetworkManager.Instance.IsExistPing)
		{
			return;
		}
		if (NetworkManager.Instance.PingValue > 3000)
		{
		}
	}
}
