using LuaFramework;
using System;
using System.Collections.Generic;

public class SocketCommand : ControllerCommand
{
	public override void Execute(IMessage message)
	{
		object body = message.Body;
		if (body == null)
		{
			return;
		}
		KeyValuePair<int, ByteBuffer> keyValuePair = (KeyValuePair<int, ByteBuffer>)body;
		int key = keyValuePair.get_Key();
		Util.CallMethod("Network", "OnSocket", new object[]
		{
			keyValuePair.get_Key(),
			keyValuePair.get_Value()
		});
	}
}
