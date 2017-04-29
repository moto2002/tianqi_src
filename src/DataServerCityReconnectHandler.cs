using System;

public class DataServerCityReconnectHandler : IDataServerReconnectHandler
{
	protected static DataServerCityReconnectHandler instance;

	public static DataServerCityReconnectHandler Instance
	{
		get
		{
			if (DataServerCityReconnectHandler.instance == null)
			{
				DataServerCityReconnectHandler.instance = new DataServerCityReconnectHandler();
			}
			return DataServerCityReconnectHandler.instance;
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

	protected DataServerCityReconnectHandler()
	{
	}

	public int GetNextCount(int currentCount)
	{
		return currentCount + 1;
	}

	public void Begin()
	{
	}

	public void SuccessEnd()
	{
		NetworkManager.Instance.SendCacheData(ServerType.Data, null);
	}
}
