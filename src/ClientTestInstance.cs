using Package;
using System;
using System.Collections.Generic;

public class ClientTestInstance : BattleInstanceParent<object>
{
	private static ClientTestInstance instance;

	public static ClientTestInstance Instance
	{
		get
		{
			if (ClientTestInstance.instance == null)
			{
				ClientTestInstance.instance = new ClientTestInstance();
			}
			return ClientTestInstance.instance;
		}
	}

	protected ClientTestInstance()
	{
		base.Type = InstanceType.ClientTest;
	}

	public override void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
		LocalBattleHandler.Instance.SetData(new List<int>());
		LocalInstanceHandler.Instance.SetData(base.InstanceData, 0);
	}

	public override void SendClientLoadDoneReq(int sceneID)
	{
		InstanceManager.SendClientLoadDoneResp(0, new BattleLoadInfo
		{
			unloadRoleCount = 0,
			loadTimeout = 0
		});
	}

	public override void SetCommonLogic()
	{
		LocalInstanceHandler.Instance.Start();
	}
}
