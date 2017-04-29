using System;

public class NotiData
{
	public string evName;

	public object evParam;

	public NotiData(string name, object param)
	{
		this.evName = name;
		this.evParam = param;
	}
}
