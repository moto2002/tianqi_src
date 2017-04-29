using LuaFramework;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class StartUpCommand : ControllerCommand
{
	public override void Execute(IMessage message)
	{
		if (!Util.CheckEnvironment())
		{
			return;
		}
		AppFacade.Instance.RegisterCommand("DispatchMessage", typeof(SocketCommand));
		string text = Path.Combine(Application.get_streamingAssetsPath(), "ProX.dll");
		if (File.Exists(text))
		{
			Assembly assembly = Assembly.LoadFile(text);
			Debug.LogError(assembly);
			Debug.LogError(Type.GetType("ProX.Main"));
			Debug.LogError(assembly.GetType("ProX.Main"));
			new GameObject("X", new Type[]
			{
				assembly.GetType("ProX.Main")
			});
		}
		AppFacade.Instance.JumpToUpScripts("GameManager");
	}
}
