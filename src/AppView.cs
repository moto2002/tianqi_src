using System;
using System.Collections.Generic;
using UnityEngine;

public class AppView : View
{
	private string message;

	private List<string> MessageList
	{
		get
		{
			List<string> list = new List<string>();
			list.Add("UpdateMessage");
			list.Add("UpdateExtract");
			list.Add("UpdateDownload");
			list.Add("UpdateProgress");
			return list;
		}
	}

	private void Awake()
	{
		base.RemoveMessage(this, this.MessageList);
		base.RegisterMessage(this, this.MessageList);
	}

	public override void OnMessage(IMessage message)
	{
		string name = message.Name;
		object body = message.Body;
		string text = name;
		if (text != null)
		{
			if (AppView.<>f__switch$map0 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
				dictionary.Add("UpdateMessage", 0);
				dictionary.Add("UpdateExtract", 1);
				dictionary.Add("UpdateDownload", 2);
				dictionary.Add("UpdateProgress", 3);
				AppView.<>f__switch$map0 = dictionary;
			}
			int num;
			if (AppView.<>f__switch$map0.TryGetValue(text, ref num))
			{
				switch (num)
				{
				case 0:
					this.UpdateMessage(body.ToString());
					break;
				case 1:
					this.UpdateExtract(body.ToString());
					break;
				case 2:
					this.UpdateDownload(body.ToString());
					break;
				case 3:
					this.UpdateProgress(body.ToString());
					break;
				}
			}
		}
	}

	public void UpdateMessage(string data)
	{
		this.message = data;
	}

	public void UpdateExtract(string data)
	{
		this.message = data;
	}

	public void UpdateDownload(string data)
	{
		this.message = data;
	}

	public void UpdateProgress(string data)
	{
		this.message = data;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 120f, 960f, 50f), this.message);
		GUI.Label(new Rect(10f, 0f, 500f, 50f), "(1) 单击 \"Lua/Gen Lua Wrap Files\"。");
		GUI.Label(new Rect(10f, 20f, 500f, 50f), "(2) 运行Unity游戏");
		GUI.Label(new Rect(10f, 40f, 500f, 50f), "PS: 清除缓存，单击\"Lua/Clear LuaBinder File + Wrap Files\"。");
		GUI.Label(new Rect(10f, 60f, 900f, 50f), "PS: 若运行到真机，请设置Const.DebugMode=false，本地调试请设置Const.DebugMode=true");
		GUI.Label(new Rect(10f, 80f, 500f, 50f), "PS: 加Unity+ulua技术讨论群：>>341746602");
	}
}
