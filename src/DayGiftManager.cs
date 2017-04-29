using Package;
using System;
using XNetwork;

public class DayGiftManager : BaseSubSystemManager
{
	private MinorLoginNty DataInfo;

	public int State = 2;

	private bool _IsCanGetDayGift;

	private static DayGiftManager instance;

	public bool IsCanGetDayGift
	{
		get
		{
			return this._IsCanGetDayGift;
		}
		set
		{
			this._IsCanGetDayGift = value;
			EventDispatcher.Broadcast<bool>(EventNames.DayGiftBadge, value);
		}
	}

	public static DayGiftManager Instance
	{
		get
		{
			if (DayGiftManager.instance == null)
			{
				DayGiftManager.instance = new DayGiftManager();
			}
			return DayGiftManager.instance;
		}
	}

	private DayGiftManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<MinorLoginNty>(new NetCallBackMethod<MinorLoginNty>(this.OnDayGiftPush));
	}

	public void OnDayGiftPush(short state, MinorLoginNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.State = down.minorLogin;
			this.IsCanGetDayGift = this.CheckIsCanGetDayGift();
			this.DataInfo = down;
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
			if (uIIfExist != null)
			{
				TownUI townUI = uIIfExist as TownUI;
				townUI.ControlSystemOpens(false, 70);
			}
		}
	}

	public bool CheckIsCanGetDayGift()
	{
		return this.State == 1;
	}

	public bool CheckDayGiftOn()
	{
		return this.State != 2;
	}

	public MinorLoginNty GetRewardItems()
	{
		return this.DataInfo;
	}
}
