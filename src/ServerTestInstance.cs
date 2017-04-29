using System;

public class ServerTestInstance : BattleInstanceParent<object>
{
	private static ServerTestInstance instance;

	public static ServerTestInstance Instance
	{
		get
		{
			if (ServerTestInstance.instance == null)
			{
				ServerTestInstance.instance = new ServerTestInstance();
			}
			return ServerTestInstance.instance;
		}
	}

	protected ServerTestInstance()
	{
		base.Type = InstanceType.ServerTest;
	}
}
