using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildWarResultUI : UIBase
{
	private Text countDownText;

	private Transform leftRoot;

	private Transform rightRoot;

	private int cdSeconds;

	private TimeCountDown exitCoundDown;

	private int fx_WinExplode;

	private int fx_WinStart;

	private int fx_WinIdle;

	private int fx_LoseExplode;

	private int fx_LoseStart;

	private int fx_LoseIdle;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = false;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.countDownText = base.FindTransform("CountDownText").GetComponent<Text>();
		this.leftRoot = base.FindTransform("Left");
		this.rightRoot = base.FindTransform("Right");
		this.countDownText.set_text(string.Empty);
		base.FindTransform("QuitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExit);
		this.cdSeconds = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("DelayTime").value);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ClearExitCountDown();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickExit(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			GuildWarManager.Instance.SendLeaveWaitingRoomReq();
		}
		else if (InstanceManager.CurrentInstanceType == InstanceType.GuildWar)
		{
			GuildWarManager.Instance.SendLeaveGuildBattleReq();
		}
	}

	public void RefreshUI(GuildMatchResultNty down)
	{
		this.SetExitCountDown();
		if (down != null)
		{
			bool isMyGuildInSceneLeft = GuildWarManager.Instance.IsMyGuildInSceneLeft;
			for (int i = 0; i < down.guildInfos.get_Count(); i++)
			{
				long guildId = down.guildInfos.get_Item(i).guildId;
				bool flag = down.winnerId == guildId;
				Transform transform = this.leftRoot;
				if (guildId == GuildManager.Instance.MyGuildnfo.guildId)
				{
					transform = ((!isMyGuildInSceneLeft) ? this.rightRoot : this.leftRoot);
				}
				else
				{
					transform = (isMyGuildInSceneLeft ? this.rightRoot : this.leftRoot);
				}
				transform.FindChild("GuildNameText").GetComponent<Text>().set_text(down.guildInfos.get_Item(i).guildName);
				transform.FindChild("GuildFightingRoot").FindChild("FightingNumText").GetComponent<Text>().set_text(down.guildInfos.get_Item(i).totalFighting + string.Empty);
				transform.FindChild("GuildParticipateNumRoot").FindChild("ParticipateNumText").GetComponent<Text>().set_text(down.guildInfos.get_Item(i).teamNum + string.Empty);
				transform.FindChild("GuildResourcesNumRoot").FindChild("ResourcesNumText").GetComponent<Text>().set_text(down.guildInfos.get_Item(i).totalResource + string.Empty);
				transform.FindChild("ImageResultFX").FindChild("WinImg").get_gameObject().SetActive(flag);
				transform.FindChild("ImageResultFX").FindChild("LoseImg").get_gameObject().SetActive(!flag);
			}
		}
	}

	private void PlayWinOrFailAnimation(Transform fxTrans, bool isWin)
	{
		if (isWin)
		{
			this.DeleteWinFXs();
			this.fx_WinStart = FXSpineManager.Instance.PlaySpine(425, fxTrans, "CommonBattlePassUI", 2002, delegate
			{
				FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
				this.fx_WinIdle = FXSpineManager.Instance.ReplaySpine(this.fx_WinIdle, 422, fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_WinExplode = FXSpineManager.Instance.PlaySpine(428, fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			this.DeleteLoseFXs();
			this.fx_LoseStart = FXSpineManager.Instance.PlaySpine(424, fxTrans, "CommonBattlePassUI", 2002, delegate
			{
				FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
				this.fx_LoseIdle = FXSpineManager.Instance.ReplaySpine(this.fx_LoseIdle, 421, fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_LoseExplode = FXSpineManager.Instance.PlaySpine(427, fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void DeleteWinFXs()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_WinExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinIdle, true);
	}

	private void DeleteLoseFXs()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseIdle, true);
	}

	private void SetExitCountDown()
	{
		this.ClearExitCountDown();
		this.exitCoundDown = new TimeCountDown(this.cdSeconds, TimeFormat.SECOND, delegate
		{
			if (this.exitCoundDown != null)
			{
				this.SetExitCountDownText(this.exitCoundDown.GetSeconds());
			}
		}, delegate
		{
			this.ClearExitCountDown();
		}, true);
	}

	private void SetExitCountDownText(int second)
	{
		if (this.countDownText != null)
		{
			this.countDownText.set_text(second + "秒后离开");
		}
	}

	private void ClearExitCountDown()
	{
		if (this.exitCoundDown != null)
		{
			this.exitCoundDown.Dispose();
			this.exitCoundDown = null;
		}
	}
}
