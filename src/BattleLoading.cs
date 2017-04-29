using System;

public class BattleLoading : UIBase
{
	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = false;
		this.alpha = 0.75f;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
