using System;

public class AppFacade : Facade
{
	private static AppFacade _instance;

	public static AppFacade Instance
	{
		get
		{
			if (AppFacade._instance == null)
			{
				AppFacade._instance = new AppFacade();
			}
			return AppFacade._instance;
		}
	}

	protected override void InitFramework()
	{
		base.InitFramework();
		this.RegisterCommand("StartUp", typeof(StartUpCommand));
	}

	public void StartUp()
	{
		base.SendMessageCommand("StartUp", null);
		base.RemoveMultiCommand(new string[]
		{
			"StartUp"
		});
	}
}
