using System;

public class WildBossManagerEvent
{
	public static readonly string CreateBoss = "WildBossManagerEvent.CreateBoss";

	public static readonly string RemoveBoss = "WildBossManagerEvent.RemoveBoss";

	public static readonly string ClearBoss = "WildBossManagerEvent.ClearBoss";

	public static readonly string GetCityFakeDrop = "WildBossManagerEvent.GetCityFakeDrop";

	public static readonly string OnSingleWaitingNumChanged = "WildBossManagerEvent.OnSingleWaitingNumChanged";

	public static readonly string OnMultiWaitingNumChanged = "WildBossManagerEvent.OnMultiWaitingNumChanged";

	public static readonly string ShowQueue = "WildBossManagerEvent.ShowQueue";

	public static readonly string UpdateQueue = "WildBossWaitingUIEvent.UpdateQueue";

	public static readonly string QuitQueue = "WildBossWaitingUIEvent.QuitQueue";
}
