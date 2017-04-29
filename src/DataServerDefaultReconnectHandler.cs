using System;

public class DataServerDefaultReconnectHandler : IDataServerReconnectHandler
{
	protected static DataServerDefaultReconnectHandler instance;

	public static DataServerDefaultReconnectHandler Instance
	{
		get
		{
			if (DataServerDefaultReconnectHandler.instance == null)
			{
				DataServerDefaultReconnectHandler.instance = new DataServerDefaultReconnectHandler();
			}
			return DataServerDefaultReconnectHandler.instance;
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
			return 5000u;
		}
	}

	protected DataServerDefaultReconnectHandler()
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
