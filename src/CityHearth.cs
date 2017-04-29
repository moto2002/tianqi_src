using System;

public class CityHearth : RangeChecker
{
	private void Awake()
	{
		this.AddListeners();
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	protected override void EnterRange()
	{
		EventDispatcher.Broadcast(CityManagerEvent.EnterIntegrationHearth);
	}
}
