using System;

public class DataServerClientBattleReconnectHandler : IDataServerReconnectHandler
{
	protected static DataServerClientBattleReconnectHandler instance;

	public static DataServerClientBattleReconnectHandler Instance
	{
		get
		{
			if (DataServerClientBattleReconnectHandler.instance == null)
			{
				DataServerClientBattleReconnectHandler.instance = new DataServerClientBattleReconnectHandler();
			}
			return DataServerClientBattleReconnectHandler.instance;
		}
	}

	public int BeginCount
	{
		get
		{
			return 0;
		}
	}

	public uint NextTime
	{
		get
		{
			return 5000u;
		}
	}

	protected DataServerClientBattleReconnectHandler()
	{
	}

	public int GetNextCount(int currentCount)
	{
		return 0;
	}

	public void Begin()
	{
	}

	public void SuccessEnd()
	{
		NetworkManager.Instance.SendCacheData(ServerType.Data, null);
	}
}
