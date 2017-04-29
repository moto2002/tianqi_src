using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPVPPassUI : UIBase
{
	public Action ExitCallBack;

	private List<Transform> rewardItemsList;

	private Transform fxTrans;

	private ListPool myMultiPvpRoleInfoListPool;

	private ListPool enemyMultiPvpRoleInfoListPool;

	private Text battleTimeText;

	private Text battleExitCDText;

	private Text myTotalKillNumText;

	private Text enemyTotalKillNumText;

	private TimeCountDown timeCountDown;

	private int fx_WinExplode;

	private int fx_WinStart;

	private int fx_WinIdle;

	private int fx_ResultExplode;

	private int fx_ResultStart;

	private int fx_ResultIdle;

	private int fx_LoseExplode;

	private int fx_LoseStart;

	private int fx_LoseIdle;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, false);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.fxTrans = base.FindTransform("FXRoot");
		base.FindTransform("BtnExit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExitBtn);
		this.myMultiPvpRoleInfoListPool = base.FindTransform("MyTeamRoot").FindChild("MultiPvpRoleInfoList").GetComponent<ListPool>();
		this.enemyMultiPvpRoleInfoListPool = base.FindTransform("EnemyTeamRoot").FindChild("MultiPvpRoleInfoList").GetComponent<ListPool>();
		this.battleTimeText = base.FindTransform("BattleTimeText").GetComponent<Text>();
		this.battleExitCDText = base.FindTransform("BattleExitCDText").GetComponent<Text>();
		this.myTotalKillNumText = base.FindTransform("MyTotalKillNumText").GetComponent<Text>();
		this.enemyTotalKillNumText = base.FindTransform("EnemyTotalKillNumText").GetComponent<Text>();
		this.rewardItemsList = new List<Transform>();
		for (int i = 1; i < 4; i++)
		{
			Transform transform = base.FindTransform("Reward" + i);
			if (transform != null)
			{
				this.rewardItemsList.Add(transform);
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.DeleteAllFX();
		this.StopCountDown();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void OnClickExitBtn(GameObject go)
	{
		if (this.ExitCallBack != null)
		{
			this.ExitCallBack.Invoke();
		}
		this.Show(false);
	}

	public void RefreshResultUI(MultiPvpSettleNty result)
	{
		if (result != null)
		{
			this.SetPassTime();
			this.RefrshRewardItems(result.rewards);
			List<MultiPvpRoleInfo> multiPvpRoleInfoListByCamp = MultiPVPManager.Instance.GetMultiPvpRoleInfoListByCamp(result.infoList, true);
			this.RefreshMyBattleResultInfo(multiPvpRoleInfoListByCamp);
			List<MultiPvpRoleInfo> multiPvpRoleInfoListByCamp2 = MultiPVPManager.Instance.GetMultiPvpRoleInfoListByCamp(result.infoList, false);
			this.RefreshEnemyBattleResultInfo(multiPvpRoleInfoListByCamp2);
			this.myTotalKillNumText.set_text(MultiPVPManager.Instance.GetMulitPvpTotalKillNum(result.infoList, true) + string.Empty);
			this.enemyTotalKillNumText.set_text(MultiPVPManager.Instance.GetMulitPvpTotalKillNum(result.infoList, false) + string.Empty);
		}
	}

	private void RefrshRewardItems(List<DropItem> rewardItems)
	{
		int i = 0;
		if (rewardItems != null)
		{
			while (i < rewardItems.get_Count())
			{
				long count = rewardItems.get_Item(i).count;
				int typeId = rewardItems.get_Item(i).typeId;
				if (i < this.rewardItemsList.get_Count())
				{
					GameObject gameObject = this.rewardItemsList.get_Item(i).get_gameObject();
					if (gameObject != null && !gameObject.get_activeSelf())
					{
						gameObject.SetActive(true);
					}
					Text component = this.rewardItemsList.get_Item(i).FindChild("TextReward").GetComponent<Text>();
					component.set_text(Utils.SwitchChineseNumber(count, 0) + string.Empty);
					Image component2 = this.rewardItemsList.get_Item(i).FindChild("ImageReward").GetComponent<Image>();
					ResourceManager.SetSprite(component2, GameDataUtils.GetItemIcon(typeId));
				}
				i++;
			}
		}
		for (int j = i; j < 3; j++)
		{
			GameObject gameObject2 = this.rewardItemsList.get_Item(j).get_gameObject();
			if (gameObject2 != null && gameObject2.get_activeSelf())
			{
				gameObject2.SetActive(false);
			}
		}
	}

	private void RefreshMyBattleResultInfo(List<MultiPvpRoleInfo> roleInfoList = null)
	{
		this.myMultiPvpRoleInfoListPool.Clear();
		if (roleInfoList != null)
		{
			this.myMultiPvpRoleInfoListPool.Create(roleInfoList.get_Count(), delegate(int index)
			{
				if (index < roleInfoList.get_Count() && index < this.myMultiPvpRoleInfoListPool.Items.get_Count())
				{
					this.SetMultiPvpRoleInfoItem(this.myMultiPvpRoleInfoListPool.Items.get_Item(index).get_transform(), roleInfoList.get_Item(index));
				}
			});
		}
	}

	private void RefreshEnemyBattleResultInfo(List<MultiPvpRoleInfo> roleInfoList = null)
	{
		this.enemyMultiPvpRoleInfoListPool.Clear();
		if (roleInfoList != null)
		{
			this.enemyMultiPvpRoleInfoListPool.Create(roleInfoList.get_Count(), delegate(int index)
			{
				if (index < roleInfoList.get_Count() && index < this.enemyMultiPvpRoleInfoListPool.Items.get_Count())
				{
					this.SetMultiPvpRoleInfoItem(this.enemyMultiPvpRoleInfoListPool.Items.get_Item(index).get_transform(), roleInfoList.get_Item(index));
				}
			});
		}
	}

	private void SetMultiPvpRoleInfoItem(Transform multiPvpRoleInfoItemTrans, MultiPvpRoleInfo roleInfo)
	{
		if (multiPvpRoleInfoItemTrans == null || roleInfo == null)
		{
			return;
		}
		Image component = multiPvpRoleInfoItemTrans.FindChild("RoleIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, UIUtils.GetRoleHeadIcon(roleInfo.career));
		Text component2 = multiPvpRoleInfoItemTrans.FindChild("RoleLv").GetComponent<Text>();
		component2.set_text(roleInfo.roleLv + string.Empty);
		Text component3 = multiPvpRoleInfoItemTrans.FindChild("RoleName").GetComponent<Text>();
		component3.set_text(roleInfo.roleName + string.Empty);
		GameObject gameObject = multiPvpRoleInfoItemTrans.FindChild("KillBossMark").get_gameObject();
		gameObject.get_gameObject().SetActive(roleInfo.killBossCount > 0);
		Text component4 = multiPvpRoleInfoItemTrans.FindChild("KillNum").GetComponent<Text>();
		component4.set_text(roleInfo.killCount + string.Empty);
		Text component5 = multiPvpRoleInfoItemTrans.FindChild("DeathNum").GetComponent<Text>();
		component5.set_text(roleInfo.deathCount + string.Empty);
		Text component6 = multiPvpRoleInfoItemTrans.FindChild("ScoreNum").GetComponent<Text>();
		component6.set_text(roleInfo.score + string.Empty);
		Text component7 = multiPvpRoleInfoItemTrans.FindChild("MultiKillNum").GetComponent<Text>();
		component7.set_text((roleInfo.maxCombo <= 1) ? string.Empty : ("连杀x" + roleInfo.maxCombo));
	}

	private void SetPassTime()
	{
		int curUsedTime = InstanceManager.CurUsedTime;
		this.battleTimeText.set_text(GameDataUtils.GetChineseContent(501004, false) + " " + TimeConverter.GetTime(curUsedTime, TimeFormat.HHMMSS));
	}

	public void OnCountDownToExit(int countTime, Action countDownEndAction)
	{
		this.StopCountDown();
		this.timeCountDown = new TimeCountDown(countTime, TimeFormat.SECOND, delegate
		{
			if (this.battleExitCDText != null)
			{
				this.battleExitCDText.set_text(string.Format("<color=green>" + this.timeCountDown.GetSeconds() + "秒</color>", new object[0]) + "后自动离开");
			}
		}, delegate
		{
			this.StopCountDown();
			countDownEndAction.Invoke();
		}, true);
	}

	private void StopCountDown()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}

	public void PlayAnimation(InstanceResultType type)
	{
		this.DeleteAllFX();
		TimerHeap.AddTimer(50u, 0, delegate
		{
			if (type == InstanceResultType.Win)
			{
				this.fx_WinStart = FXSpineManager.Instance.PlaySpine(425, this.fxTrans, "CommonBattlePassUI", 3002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
					this.fx_WinIdle = FXSpineManager.Instance.ReplaySpine(this.fx_WinIdle, 422, this.fxTrans, "CommonBattlePassUI", 3002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_WinExplode = FXSpineManager.Instance.PlaySpine(428, this.fxTrans, "CommonBattlePassUI", 3003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			else if (type == InstanceResultType.Result)
			{
				this.fx_ResultStart = FXSpineManager.Instance.PlaySpine(423, this.fxTrans, "CommonBattlePassUI", 3002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_ResultStart, true);
					this.fx_ResultIdle = FXSpineManager.Instance.ReplaySpine(this.fx_ResultIdle, 420, this.fxTrans, "CommonBattlePassUI", 3002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_ResultExplode = FXSpineManager.Instance.PlaySpine(426, this.fxTrans, "CommonBattlePassUI", 3003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			else if (type == InstanceResultType.Lose)
			{
				this.fx_LoseStart = FXSpineManager.Instance.PlaySpine(424, this.fxTrans, "CommonBattlePassUI", 3002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
					this.fx_LoseIdle = FXSpineManager.Instance.ReplaySpine(this.fx_LoseIdle, 421, this.fxTrans, "CommonBattlePassUI", 3002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_LoseExplode = FXSpineManager.Instance.PlaySpine(427, this.fxTrans, "CommonBattlePassUI", 3003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		});
	}

	private void DeleteAllFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_WinExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinIdle, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultIdle, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseIdle, true);
	}
}
