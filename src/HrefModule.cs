using System;

public class HrefModule
{
	public int activityID;

	public string moduleName;

	public Action m_hrefModuleClick;

	public HrefModule(int activityID, string name = "", Action callBack = null)
	{
		this.activityID = activityID;
		this.moduleName = name;
		this.m_hrefModuleClick = callBack;
	}
}
