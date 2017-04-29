using System;
using System.Net;

public struct ReconnectMessage
{
	public IPEndPoint ipEndPoint;

	public ServerType serverType;

	public Action onConnectSuccessCallBack;

	public Action onConnectFailedCallBack;
}
