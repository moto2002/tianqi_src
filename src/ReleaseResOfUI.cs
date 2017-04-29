using System;

public class ReleaseResOfUI
{
	public static void ReleaseResInInstance()
	{
	}

	public static void ReleaseResInCity()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("RoleUI");
		UIManagerControl.Instance.UnLoadUIPrefab("TalkUI");
		UIManagerControl.Instance.UnLoadUIPrefab("TaskDescUI");
	}
}
