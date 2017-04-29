using System;
using System.Net;

public interface IReconnectManager
{
	bool EnableReconnect
	{
		get;
		set;
	}

	bool IsReconnectingDataServer
	{
		get;
	}

	IDataServerReconnectHandler DataServerReconnectHandler
	{
		set;
	}

	bool IsSendingDataServerVerify
	{
		get;
	}

	void Init();

	void Release();

	void BeginReconnect(IPEndPoint endPint, ServerType serverType, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null);

	void VerifyReqNotConnected();

	void VerifyResNotConnected();

	void BeginSynchronizeServerBattle();

	void EndSynchronizeServerBattle();
}
