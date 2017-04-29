using System;
using System.Collections.Generic;
using UnityEngine;

public class OutLogManager : MonoBehaviour
{
	private static OutLogManager instance;

	protected bool _LogOpen;

	protected static GUIStyle style;

	private List<string> mLines = new List<string>();

	private List<string> mWriteText = new List<string>();

	public static OutLogManager Instance
	{
		get
		{
			if (OutLogManager.instance == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("GM");
				OutLogManager.instance = gameObject.AddUniqueComponent<OutLogManager>();
			}
			return OutLogManager.instance;
		}
	}

	public bool LogOpen
	{
		get
		{
			return this._LogOpen;
		}
		set
		{
			this._LogOpen = value;
		}
	}

	protected OutLogManager()
	{
		Application.add_logMessageReceived(new Application.LogCallback(this.HandleLog));
	}

	private void OnGUI()
	{
		if (!this.LogOpen)
		{
			return;
		}
		OutLogManager.style = GUI.get_skin().get_textField();
		OutLogManager.style.get_onFocused().set_textColor(Color.get_red());
		OutLogManager.style.set_wordWrap(true);
		if (GUI.Button(new Rect((float)(Screen.get_width() - 50), 0f, 50f, 30f), "log cls"))
		{
			this.mLines.Clear();
		}
		GUILayout.Label(this.GetLogs(), new GUILayoutOption[0]);
	}

	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		this.mWriteText.Add(logString);
		if ((type == null || type == 4 || type == 3) && Application.get_isPlaying())
		{
			this.Add2Log(logString);
			this.Add2Log(stackTrace);
		}
	}

	private void Add2Log(string log)
	{
		if (this.mLines.get_Count() > 60)
		{
			this.mLines.RemoveAt(0);
		}
		this.mLines.Add(this.ToLog(new object[]
		{
			log
		}));
	}

	private string ToLog(params object[] objs)
	{
		string text = string.Empty;
		for (int i = 0; i < objs.Length; i++)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text = text + ", " + objs[i].ToString();
			}
		}
		return text;
	}

	private string GetLogs()
	{
		string text = string.Empty;
		for (int i = 0; i < this.mLines.get_Count(); i++)
		{
			text += this.mLines.get_Item(i);
		}
		return text;
	}
}
