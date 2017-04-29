using GameData;
using Package;
using System;
using UnityEngine;
using XNetwork;

public class GuildWarInstance : BattleInstanceParent<GuildBattleWarResultNty>
{
	private static GuildWarInstance instance;

	protected BattleUI battleUI;

	protected MineAndReportUI mineAndReportUI;

	protected long progressOwnerID = -1L;

	protected int progressPercentage = -1;

	protected int mineCD = -1;

	protected int buffID = -1;

	public static GuildWarInstance Instance
	{
		get
		{
			if (GuildWarInstance.instance == null)
			{
				GuildWarInstance.instance = new GuildWarInstance();
			}
			return GuildWarInstance.instance;
		}
	}

	protected GuildWarInstance()
	{
		base.Type = InstanceType.GuildWar;
	}

	public override void ReleaseData()
	{
		base.ReleaseData();
		this.progressOwnerID = -1L;
		this.progressPercentage = -1;
		this.mineCD = -1;
		this.buffID = -1;
	}

	public override void AddInstanceListeners()
	{
		base.AddInstanceListeners();
		NetworkManager.AddListenEvent<GuildWarGrabProcessNty>(new NetCallBackMethod<GuildWarGrabProcessNty>(this.UpdateOwnProgress));
		NetworkManager.AddListenEvent<GuildWarMultiRewardCountdownNty>(new NetCallBackMethod<GuildWarMultiRewardCountdownNty>(this.UpdateMineCD));
		NetworkManager.AddListenEvent<GuildWarEnterDungeonNty>(new NetCallBackMethod<GuildWarEnterDungeonNty>(this.UpdateBuff));
	}

	public override void RemoveInstanceListeners()
	{
		base.RemoveInstanceListeners();
		NetworkManager.RemoveListenEvent<GuildWarGrabProcessNty>(new NetCallBackMethod<GuildWarGrabProcessNty>(this.UpdateOwnProgress));
		NetworkManager.RemoveListenEvent<GuildWarMultiRewardCountdownNty>(new NetCallBackMethod<GuildWarMultiRewardCountdownNty>(this.UpdateMineCD));
		NetworkManager.RemoveListenEvent<GuildWarEnterDungeonNty>(new NetCallBackMethod<GuildWarEnterDungeonNty>(this.UpdateBuff));
	}

	public override void PlayerInitEnd(EntityPlayer player)
	{
	}

	public override void PlayerHpChange(EntityPlayer player)
	{
	}

	public override void PlayerHpLmtChange(EntityPlayer player)
	{
	}

	public override void ShowBattleUI()
	{
		if (base.InstanceResult != null)
		{
			return;
		}
		this.battleUI = LinkNavigationManager.OpenBattleUI();
		this.battleUI.BtnQuitAction = delegate
		{
			GuildWarManager.Instance.RemoveDieCountDown();
			GuildWarManager.Instance.SendLeaveGuildBattleReq();
		};
		this.battleUI.ResetAllInstancePart();
		this.battleUI.ShowGuildWarCityResouce(true);
		this.battleUI.SetGuildWarCityResouceInfo(GuildWarManager.Instance.MyGuildWarResourceInfo, GuildWarManager.Instance.EnemyGuildWarResourceInfo);
		this.mineAndReportUI = (UIManagerControl.Instance.OpenUI("MineAndReportUI", this.battleUI.MineAndReportUISlot, false, UIType.NonPush) as MineAndReportUI);
		if (this.mineAndReportUI)
		{
			this.battleUI.ShowTopLeftTabs(true, new BattleUI.TopLeftTabData[]
			{
				new BattleUI.TopLeftTabData
				{
					name = GameDataUtils.GetChineseContent(515116, false),
					showAction = new Action<bool>(this.mineAndReportUI.ShowMine),
					stretchGameObject = this.battleUI.MineAndReportUISlot.get_gameObject()
				},
				new BattleUI.TopLeftTabData
				{
					name = GameDataUtils.GetChineseContent(515117, false),
					showAction = new Action<bool>(this.mineAndReportUI.ShowReport),
					stretchGameObject = this.battleUI.MineAndReportUISlot.get_gameObject()
				}
			});
		}
		this.TryUpdateOwnProgressUI();
		this.TryUpdateDoubleTime();
		this.TryUpdateBuff();
		this.battleUI.IsPauseCheck = false;
		this.battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GetInstanceResult(GuildBattleWarResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		UIManagerControl.Instance.HideUI("BattleUI");
	}

	public override void ExitBattleField()
	{
		base.ExitBattleField();
		if (UIManagerControl.Instance.IsOpen("MineAndReportUI"))
		{
			UIManagerControl.Instance.HideUI("MineAndReportUI");
		}
	}

	protected void UpdateOwnProgress(short state, GuildWarGrabProcessNty down = null)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"UpdateOwnProgress: ",
			down.grabGuildId,
			" ",
			down.grabProcess
		}));
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.progressOwnerID = down.grabGuildId;
		this.progressPercentage = down.grabProcess;
		this.TryUpdateOwnProgressUI();
	}

	public void TryUpdateOwnProgressUI()
	{
		if (this.progressOwnerID == -1L)
		{
			return;
		}
		if (this.progressPercentage == -1)
		{
			return;
		}
		if (!this.battleUI)
		{
			return;
		}
		this.battleUI.ShowGuildWarMineState(true);
		this.battleUI.UpdateGuildWarMineState(GuildWarManager.Instance.GetCurBattleMineState(), GuildWarManager.Instance.MyGuildID, GuildWarManager.Instance.MyGuildName, GuildWarManager.Instance.EnemyGuildID, GuildWarManager.Instance.EnemyGuildName, this.progressOwnerID, (float)this.progressPercentage * 0.001f);
	}

	protected void UpdateMineCD(short state, GuildWarMultiRewardCountdownNty down = null)
	{
		Debug.LogError("UpdateDoubleTime: " + down.countdownSec);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.mineCD = down.countdownSec;
		this.TryUpdateDoubleTime();
	}

	protected void TryUpdateDoubleTime()
	{
		if (this.mineCD == -1)
		{
			return;
		}
		if (!this.battleUI)
		{
			return;
		}
		this.battleUI.ShowGuildWarMineCD(true, this.mineCD);
	}

	protected void UpdateBuff(short state, GuildWarEnterDungeonNty down = null)
	{
		Debug.LogError("UpdateBuff: " + down.buffId);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.buffID = down.buffId;
		this.TryUpdateBuff();
	}

	protected void TryUpdateBuff()
	{
		if (!DataReader<Buff>.Contains(this.buffID))
		{
			return;
		}
		if (!this.battleUI)
		{
			return;
		}
		this.battleUI.ShowGuildWarBuff(true, 5619, delegate
		{
			if (!DataReader<Buff>.Contains(this.buffID))
			{
				return;
			}
			Buff buff = DataReader<Buff>.Get(this.buffID);
			string effectText = string.Empty;
			if (DataReader<Attrs>.Contains(buff.targetPropId))
			{
				Attrs attrs = DataReader<Attrs>.Get(buff.targetPropId);
				if (attrs.attrs.get_Count() > 0 && attrs.values.get_Count() > 0)
				{
					effectText = AttrUtility.GetStandardDesc(attrs.attrs.get_Item(0), attrs.values.get_Item(0));
				}
			}
			BattleBuffDetailUI battleBuffDetailUI = UIManagerControl.Instance.OpenUI("BattleBuffDetailUI", null, false, UIType.NonPush) as BattleBuffDetailUI;
			if (battleBuffDetailUI)
			{
				battleBuffDetailUI.SetData(5619, 515122, 515123, effectText, 515121);
			}
		});
	}
}
