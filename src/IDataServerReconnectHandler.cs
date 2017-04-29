using System;

public interface IDataServerReconnectHandler
{
	int BeginCount
	{
		get;
	}

	uint NextTime
	{
		get;
	}

	int GetNextCount(int currentCount);

	void Begin();

	void SuccessEnd();
}
