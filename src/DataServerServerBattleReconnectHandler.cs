using System;

public class DataServerServerBattleReconnectHandler : IDataServerReconnectHandler
{
	protected static DataServerServerBattleReconnectHandler instance;

	public static DataServerServerBattleReconnectHandler Instance
	{
		get
		{
			if (DataServerServerBattleReconnectHandler.instance == null)
			{
				DataServerServerBattleReconnectHandler.instance = new DataServerServerBattleReconnectHandler();
			}
			return DataServerServerBattleReconnectHandler.instance;
		}
	}

	public int BeginCount
	{
		get
		{
			return 1;
		}
	}

	public uint NextTime
	{
		get
		{
			return 2000u;
		}
	}

	protected DataServerServerBattleReconnectHandler()
	{
	}

	public int GetNextCount(int currentCount)
	{
		return currentCount + 1;
	}

	public void Begin()
	{
		InstanceManager.IsAIThinking = false;
		XInputManager.EnabledLogic = false;
	}

	public void SuccessEnd()
	{
		ReconnectManager.Instance.BeginSynchronizeServerBattle();
	}
}
