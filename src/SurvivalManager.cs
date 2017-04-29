using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class SurvivalManager : BaseSubSystemManager
{
	public bool IsPassAll;

	public int BattleCurrentBatch;

	public int BattleCurrentStage;

	public int StageMax;

	public int StageCurr;

	public int StageMaxHis;

	public int StageTop;

	public SecretAreaLoginPush ScInfo;

	public SurvivalChallengeResultNty ScResultNty;

	public List<SecretAreaRankInfo> ScRankingInfo = new List<SecretAreaRankInfo>();

	private int _currentSelectId;

	private static SurvivalManager instance;

	private bool resultProcessed;

	public int CurrentSelectId
	{
		get
		{
			if (this._currentSelectId <= 0 && this.ScInfo != null)
			{
				if (this.ScInfo.currClearBatch <= 0)
				{
					this._currentSelectId = 1;
				}
				else
				{
					TiaoZhanBoCi tiaoZhanBoCi = DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.id == this.ScInfo.currClearBatch + 1);
					if (tiaoZhanBoCi == null)
					{
						tiaoZhanBoCi = DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.id == this.ScInfo.currClearBatch);
					}
					this._currentSelectId = tiaoZhanBoCi.stage;
				}
			}
			return this._currentSelectId;
		}
		set
		{
			this._currentSelectId = value;
		}
	}

	public static SurvivalManager Instance
	{
		get
		{
			if (SurvivalManager.instance == null)
			{
				SurvivalManager.instance = new SurvivalManager();
			}
			return SurvivalManager.instance;
		}
	}

	protected SurvivalManager()
	{
	}

	public TiaoZhanBoCi GetCurrentInfo()
	{
		return DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.bossId > 0 && a.stage == this.CurrentSelectId);
	}

	public override void Init()
	{
		base.Init();
		int value = DataReader<ShengCunMiJingPeiZhi>.Get("stageNum").value;
		this.StageMaxHis = value;
		this.StageMax = value;
		List<TiaoZhanBoCi> dataList = DataReader<TiaoZhanBoCi>.DataList;
		if (dataList != null && dataList.get_Count() > 0)
		{
			dataList.Sort(new Comparison<TiaoZhanBoCi>(SurvivalManager.StageSorCompare));
			this.StageTop = dataList.get_Item(0).stage;
		}
	}

	public override void Release()
	{
		this.ScRankingInfo.Clear();
		this.IsPassAll = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<SecretAreaLoginPush>(new NetCallBackMethod<SecretAreaLoginPush>(this.OnScPush));
		NetworkManager.AddListenEvent<SecretAreaRankListRes>(new NetCallBackMethod<SecretAreaRankListRes>(this.OnScRankingListRes));
		NetworkManager.AddListenEvent<ChallengeSecretAreaRes>(new NetCallBackMethod<ChallengeSecretAreaRes>(this.OnChallengeSecretAreaRes));
		NetworkManager.AddListenEvent<SecretAreaChallengeResultNty>(new NetCallBackMethod<SecretAreaChallengeResultNty>(this.OnScResultNty));
		NetworkManager.AddListenEvent<SurvivalChallengeExitBTRes>(new NetCallBackMethod<SurvivalChallengeExitBTRes>(this.OnScExitBTRes));
		NetworkManager.AddListenEvent<ChallengeBatchInfoNty>(new NetCallBackMethod<ChallengeBatchInfoNty>(this.OnChallengeBatchInfoNty));
		NetworkManager.AddListenEvent<ClearMonsterBatchNty>(new NetCallBackMethod<ClearMonsterBatchNty>(this.OnClearMonsterBatchNty));
		NetworkManager.AddListenEvent<PurchaseSecretAreaTimesRes>(new NetCallBackMethod<PurchaseSecretAreaTimesRes>(this.OnPurchaseSecretAreaTimesRes));
		NetworkManager.AddListenEvent<ContinueSecretAreaChallengeRes>(new NetCallBackMethod<ContinueSecretAreaChallengeRes>(this.OnContinueSecretAreaChallengeRes));
	}

	public void SendSurvivalChallengeRankingListReq(int _page)
	{
		NetworkManager.Send(new SecretAreaRankListReq
		{
			page = _page
		}, ServerType.Data);
	}

	public void SendScExitBTReq(bool _iaAgain)
	{
		NetworkManager.Send(new SurvivalChallengeExitBTReq
		{
			again = _iaAgain
		}, ServerType.Data);
	}

	public void SendExitSecretAreaChallengeReq()
	{
		NetworkManager.Send(new ExitSecretAreaChallengeReq(), ServerType.Data);
	}

	public void SendChallengeSecretAreaReq()
	{
		InstanceManager.SecurityCheck(delegate
		{
			this.BattleCurrentStage = this.StageCurr;
			NetworkManager.Send(new ChallengeSecretAreaReq(), ServerType.Data);
		}, null);
	}

	private void OnScPush(short state, SecretAreaLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.ScInfo = down;
			TiaoZhanBoCi tiaoZhanBoCi = DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.id == this.ScInfo.currClearBatch + 1);
			if (tiaoZhanBoCi == null)
			{
				this.IsPassAll = true;
				tiaoZhanBoCi = DataReader<TiaoZhanBoCi>.DataList.Find((TiaoZhanBoCi a) => a.id == this.ScInfo.currClearBatch);
			}
			this.StageCurr = tiaoZhanBoCi.stage;
			int value = DataReader<ShengCunMiJingPeiZhi>.Get("lockStageNum").value;
			if (this.StageCurr >= this.StageMaxHis)
			{
				this.StageMax = this.StageCurr + value;
				if (this.StageMax > this.StageTop)
				{
					this.StageMax = this.StageTop;
				}
			}
			EventDispatcher.Broadcast(EventNames.SCUpdateUI);
		}
	}

	private void OnScRankingListRes(short state, SecretAreaRankListRes down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.ScRankingInfo = down.rankInfos;
				this.ScRankingInfo.Sort((SecretAreaRankInfo a, SecretAreaRankInfo b) => a.rank.CompareTo(b.rank));
				EventDispatcher.Broadcast(EventNames.SCUpdateList);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnChallengeSecretAreaRes(short state, ChallengeSecretAreaRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			SurvivalInstance.Instance.InstanceDataID = down.copyId;
			InstanceManager.ChangeInstanceManager(down.copyId, true);
		}
	}

	private void OnPurchaseSecretAreaTimesRes(short state, PurchaseSecretAreaTimesRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnContinueSecretAreaChallengeRes(short state, ContinueSecretAreaChallengeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.ReloadClientBattleState();
		}
	}

	private void OnClearMonsterBatchNty(short state, ClearMonsterBatchNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.isBossBatch)
		{
			TimerHeap.AddTimer(3000u, 0, delegate
			{
				DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(512048, false), GameDataUtils.GetChineseContent(512052, false), delegate
				{
				}, delegate
				{
					NetworkManager.Send(new ContinueSecretAreaChallengeReq
					{
						isContinue = false
					}, ServerType.Data);
				}, delegate
				{
					NetworkManager.Send(new ContinueSecretAreaChallengeReq
					{
						isContinue = true
					}, ServerType.Data);
				}, GameDataUtils.GetChineseContent(512057, false), GameDataUtils.GetChineseContent(512056, false), "button_orange_1", "button_orange_1", null);
				DialogBoxUIView dialogBoxUIView = UIManagerControl.Instance.GetUIIfExist("DialogBoxUI") as DialogBoxUIView;
				if (dialogBoxUIView != null)
				{
					dialogBoxUIView.isClick = false;
					dialogBoxUIView.SetMask();
				}
			});
		}
	}

	private void OnScExitBTRes(short state, SurvivalChallengeExitBTRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.again)
			{
				InstanceManager.ChangeInstanceManager(SurvivalInstance.Instance, false);
			}
			else
			{
				InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
				UIStackManager.Instance.PopUILast_FullScreen();
			}
		}
	}

	private void OnChallengeBatchInfoNty(short state, ChallengeBatchInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.BattleCurrentBatch = down.batch;
			this.BattleCurrentStage = down.stage;
			BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
			if (battleUI != null)
			{
				battleUI.ShowBoss(DataReader<TiaoZhanBoCi>.Get(down.batch).bossId > 0);
			}
			EventDispatcher.Broadcast(EventNames.SetProgress);
		}
	}

	private void OnScResultNty(short state, SecretAreaChallengeResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
		}
		if (down != null)
		{
			this.OnGameOver(down);
		}
	}

	private void OnGameOver(SecretAreaChallengeResultNty downTmp)
	{
		this.resultProcessed = true;
		SurvivalInstance.Instance.GetInstanceResult(downTmp);
		InstanceManager.StopAllClientAI(true);
	}

	private static int StageSorCompare(TiaoZhanBoCi AF1, TiaoZhanBoCi AF2)
	{
		if (AF1.stage >= AF2.stage)
		{
			return -1;
		}
		return 1;
	}
}
