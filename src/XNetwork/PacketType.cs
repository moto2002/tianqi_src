using System;

namespace XNetwork
{
	public enum PacketType
	{
		Data,
		Error,
		Disconnect,
		Ping,
		ReconnectResult,
		Verify,
		Heartbeat,
		Test = 100
	}
}
