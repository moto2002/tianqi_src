using System;

public class DataServerMixBattleReconnectHandler : IDataServerReconnectHandler
{
	protected static DataServerMixBattleReconnectHandler instance;

	public static DataServerMixBattleReconnectHandler Instance
	{
		get
		{
			if (DataServerMixBattleReconnectHandler.instance == null)
			{
				DataServerMixBattleReconnectHandler.instance = new DataServerMixBattleReconnectHandler();
			}
			return DataServerMixBattleReconnectHandler.instance;
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

	protected DataServerMixBattleReconnectHandler()
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
		NetworkManager.Instance.SendCacheData(ServerType.Data, null);
		InstanceManager.IsAIThinking = true;
		XInputManager.EnabledLogic = true;
	}
}
