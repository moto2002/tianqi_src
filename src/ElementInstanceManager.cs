using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class ElementInstanceManager : BaseSubSystemManager
{
	public ElementCopyLoginPush m_elementCopyLoginPush;

	public List<CopyReward> m_listCopyReward = new List<CopyReward>();

	public MineInfoRes m_mineInfoRes = new MineInfoRes();

	private Action actionOnGetMineInfoRes;

	private Action actionOnGetExitElementCopyRes;

	private Action actionSendFightReq;

	public bool m_isActorMoving;

	public bool m_shouldShow;

	public string m_currentFightBlockID;

	private Dictionary<string, TimeEnty> m_mapTimeCal = new Dictionary<string, TimeEnty>();

	private static ElementInstanceManager m_Instance;

	public static ElementInstanceManager Instance
	{
		get
		{
			if (ElementInstanceManager.m_Instance == null)
			{
				ElementInstanceManager.m_Instance = new ElementInstanceManager();
			}
			return ElementInstanceManager.m_Instance;
		}
	}

	protected ElementInstanceManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.m_elementCopyLoginPush = null;
		this.m_listCopyReward.Clear();
		this.m_mapTimeCal.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ElementCopyLoginPush>(new NetCallBackMethod<ElementCopyLoginPush>(this.OnGetElementCopyLoginPush));
		NetworkManager.AddListenEvent<ExploreEnergyChangedNty>(new NetCallBackMethod<ExploreEnergyChangedNty>(this.OnGetExploreEnergyChangedNty));
		NetworkManager.AddListenEvent<ExploreBlockRes>(new NetCallBackMethod<ExploreBlockRes>(this.OnGetExploreBlockRes));
		NetworkManager.AddListenEvent<RecoveryToFullEnergyRes>(new NetCallBackMethod<RecoveryToFullEnergyRes>(this.OnGetRecoveryToFullEnergyRes));
		NetworkManager.AddListenEvent<AccepttDebrisRes>(new NetCallBackMethod<AccepttDebrisRes>(this.OnGetAccepttDebrisRes));
		NetworkManager.AddListenEvent<SelectPetToMiningRes>(new NetCallBackMethod<SelectPetToMiningRes>(this.OnGetSelectPetToMiningRes));
		NetworkManager.AddListenEvent<StartToFightRes>(new NetCallBackMethod<StartToFightRes>(this.OnGetStartToFightRes));
		NetworkManager.AddListenEvent<BlockStatusChangedNty>(new NetCallBackMethod<BlockStatusChangedNty>(this.OnGetBlockStatusChangedNty));
		NetworkManager.AddListenEvent<ExploreLogChangedNty>(new NetCallBackMethod<ExploreLogChangedNty>(this.OnGetExploreLogChangedNty));
		NetworkManager.AddListenEvent<MineInfoRes>(new NetCallBackMethod<MineInfoRes>(this.OnGetMineInfoRes));
		NetworkManager.AddListenEvent<EvacuatePetRes>(new NetCallBackMethod<EvacuatePetRes>(this.OnGetEvacuatePetRes));
		NetworkManager.AddListenEvent<ElementCopyBattleResultNty>(new NetCallBackMethod<ElementCopyBattleResultNty>(this.OnGetElementCopyBattleResultNty));
		NetworkManager.AddListenEvent<ExitElementCopyRes>(new NetCallBackMethod<ExitElementCopyRes>(this.OnGetExitElementCopyRes));
		NetworkManager.AddListenEvent<MineInfoChangedNty>(new NetCallBackMethod<MineInfoChangedNty>(this.OnGetMineInfoChangedNty));
		NetworkManager.AddListenEvent<LastBlockChangedNty>(new NetCallBackMethod<LastBlockChangedNty>(this.OnGetLastBlockChangedNty));
		NetworkManager.AddListenEvent<MineInfoResidueTimeChangeNty>(new NetCallBackMethod<MineInfoResidueTimeChangeNty>(this.OnGetMineInfoResidueTimeChangeNty));
		NetworkManager.AddListenEvent<PropertyChangedNty>(new NetCallBackMethod<PropertyChangedNty>(this.OnGetPropertyChangedNty));
		TimerHeap.AddTimer(0u, 1000, delegate
		{
			using (Dictionary<string, TimeEnty>.Enumerator enumerator = this.m_mapTimeCal.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, TimeEnty> current = enumerator.get_Current();
					current.get_Value().time--;
					if (current.get_Value().time < 0)
					{
						current.get_Value().time = 0;
					}
				}
			}
		});
	}

	public void SendExploreBlockReq(string source, string target)
	{
		if (this.m_elementCopyLoginPush.exploreEnergy == 0)
		{
			this.BuyRecovery();
		}
		else
		{
			EventDispatcher.Broadcast<bool>("EventNames.EnableFloating", false);
			NetworkManager.Send(new ExploreBlockReq
			{
				sourceBlockId = source,
				targetBlockId = target
			}, ServerType.Data);
		}
	}

	public void SendRecoveryToFullEnergyReq()
	{
		NetworkManager.Send(new RecoveryToFullEnergyReq(), ServerType.Data);
	}

	public void SendAcceptDebrisReq(string blockID)
	{
		Debug.LogError("SendAcceptDebrisReq  " + blockID);
		NetworkManager.Send(new AcceptDebrisReq
		{
			mineBlockId = blockID
		}, ServerType.Data);
	}

	public void SendSelectPetToMiningReq(string blockID, long petID)
	{
		NetworkManager.Send(new SelectPetToMiningReq
		{
			mineBlockId = blockID,
			petId = petID
		}, ServerType.Data);
	}

	public void SendStartToFightReq(string blockID, Action action)
	{
		this.actionSendFightReq = action;
		this.SendStartToFightReq(blockID);
	}

	public void SendStartToFightReq(string blockID)
	{
		if (DataReader<YBanKuaiSuoYin>.Get(this.m_elementCopyLoginPush.lastBlock).around.Contains(blockID))
		{
			this.m_currentFightBlockID = blockID;
			NetworkManager.Send(new StartToFightReq
			{
				blockId = blockID
			}, ServerType.Data);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502307, false));
		}
	}

	public void SendMineInfoReq(Action action)
	{
		if (this.actionOnGetMineInfoRes != null)
		{
			return;
		}
		Debuger.Error("--------------------SendMineInfoReq", new object[0]);
		this.actionOnGetMineInfoRes = action;
		NetworkManager.Send(new MineInfoReq(), ServerType.Data);
	}

	public void SendEvacuatePetReq(string blockID)
	{
		NetworkManager.Send(new EvacuatePetReq
		{
			mineBlockId = blockID
		}, ServerType.Data);
	}

	public void SendExitElementCopyReq(Action action)
	{
		if (this.actionOnGetExitElementCopyRes != null)
		{
			return;
		}
		this.actionOnGetExitElementCopyRes = action;
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		NetworkManager.Send(new ExitElementCopyReq(), ServerType.Data);
	}

	private void OnGetElementCopyLoginPush(short state, ElementCopyLoginPush down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_elementCopyLoginPush = down;
			this.DebugBlocks();
		}
	}

	private void OnGetExploreEnergyChangedNty(short state, ExploreEnergyChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.IsNeedReplaceFlag)
			{
				this.m_elementCopyLoginPush.residueRecoverTime = down.residueRecoverTime;
			}
			this.m_elementCopyLoginPush.exploreEnergy = down.exploreEnergy;
			EventDispatcher.Broadcast(EventNames.OnGetExploreEnergyChangedNty);
		}
	}

	private void OnGetExploreBlockRes(short state, ExploreBlockRes down)
	{
		EventDispatcher.Broadcast<bool>("EventNames.EnableFloating", true);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_elementCopyLoginPush.activateBlocks.AddRange(down.newOpenBlocks);
			EventDispatcher.Broadcast(EventNames.OnGetExploreBlockRes);
		}
	}

	private void OnGetRecoveryToFullEnergyRes(short state, RecoveryToFullEnergyRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EventDispatcher.Broadcast(EventNames.OnGetRecoveryToFullEnergyRes);
			this.m_elementCopyLoginPush.purchaseNum = down.purchaseNum;
		}
	}

	private void OnGetAccepttDebrisRes(short state, AccepttDebrisRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int num = this.m_mineInfoRes.mineInfos.FindIndex((MineInfo a) => a.blockId.Equals(down.mineInfo.blockId));
			this.m_mineInfoRes.mineInfos.RemoveAt(num);
			this.m_mineInfoRes.mineInfos.Insert(num, down.mineInfo);
			Debug.LogError("OnGetAccepttDebrisRes");
			EventDispatcher.Broadcast(EventNames.OnGetAccepttDebrisRes);
		}
	}

	private void OnGetSelectPetToMiningRes(short state, SelectPetToMiningRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.OnGetSelectPetToMiningRes);
	}

	private void OnGetStartToFightRes(short state, StartToFightRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EventDispatcher.Broadcast(EventNames.OnGetStartToFightRes);
			if (BallElement.Instance.lastBlockChche.get_Length() != 0)
			{
				Transform transform = BallElement.Instance.get_transform().FindChild(BallElement.Instance.lastBlockChche);
				BallElementItem component = transform.GetComponent<BallElementItem>();
				component.isActor = false;
			}
			BallElement.Instance.shouldChangePosImmediately = true;
			InstanceManager.ChangeInstanceManager(down.copyId, false);
			if (this.actionSendFightReq != null)
			{
				this.actionSendFightReq.Invoke();
				this.actionSendFightReq = null;
			}
		}
	}

	private void OnGetBlockStatusChangedNty(short state, BlockStatusChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.blockInfo.get_Count(); i++)
			{
				BlockInfo bi = down.blockInfo.get_Item(i);
				if (bi.incidentType == RandomIncidentType.IncidentType.PETPROPERTY)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						YChongWuJiaChengKu yChongWuJiaChengKu = DataReader<YChongWuJiaChengKu>.Get(bi.incidentTypeId);
						string text = yChongWuJiaChengKu.depict;
						text = text.Replace("{s1}", yChongWuJiaChengKu.addNum.ToString());
						UIManagerControl.Instance.ShowBattleToastText(text, 2f);
					});
				}
				else if (bi.incidentType == RandomIncidentType.IncidentType.PLAYERPROPERTY)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						YJiaoSeJiaChengKu yJiaoSeJiaChengKu = DataReader<YJiaoSeJiaChengKu>.Get(bi.incidentTypeId);
						string text = yJiaoSeJiaChengKu.depict;
						text = text.Replace("{s1}", yJiaoSeJiaChengKu.addNum.ToString());
						UIManagerControl.Instance.ShowBattleToastText(text, 1f);
					});
				}
				else if (bi.incidentType == RandomIncidentType.IncidentType.RECOVRYENERGY)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						YNengLiangHuiFu yNengLiangHuiFu = DataReader<YNengLiangHuiFu>.Get(bi.incidentTypeId);
						string text = yNengLiangHuiFu.powerName;
						text = text.Replace("{s1}", yNengLiangHuiFu.powerPoint.ToString());
						UIManagerControl.Instance.ShowBattleToastText(text, 2f);
					});
				}
				else if (bi.incidentType == RandomIncidentType.IncidentType.TOOL)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						ElementInstanceRewardUI elementInstanceRewardUI = UIManagerControl.Instance.OpenUI("ElementInstanceRewardUI", null, false, UIType.NonPush) as ElementInstanceRewardUI;
						elementInstanceRewardUI.RefreshUI(bi.blockId);
					});
				}
				int num = this.m_elementCopyLoginPush.activateBlocks.FindIndex((BlockInfo a) => a.blockId == bi.blockId);
				if (num != -1)
				{
					this.m_elementCopyLoginPush.activateBlocks.set_Item(num, bi);
				}
			}
			EventDispatcher.Broadcast(EventNames.OnGetBlockStatusChangedNty);
		}
	}

	private void OnGetExploreLogChangedNty(short state, ExploreLogChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_elementCopyLoginPush.exploreLogs.AddRange(down.exploreLogs);
			EventDispatcher.Broadcast(EventNames.OnGetExploreLogChangedNty);
		}
	}

	private void OnGetMineInfoRes(short state, MineInfoRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EventDispatcher.Broadcast(EventNames.OnGetMineInfoRes);
			this.m_mineInfoRes = down;
			for (int i = 0; i < this.m_mineInfoRes.mineInfos.get_Count(); i++)
			{
				MineInfo mineInfo = this.m_mineInfoRes.mineInfos.get_Item(i);
				if (this.m_mapTimeCal.ContainsKey(mineInfo.blockId))
				{
					this.m_mapTimeCal.get_Item(mineInfo.blockId).time = mineInfo.lastModifyTime;
				}
				else
				{
					this.m_mapTimeCal.Add(mineInfo.blockId, new TimeEnty(mineInfo.lastModifyTime));
				}
			}
		}
		if (this.actionOnGetMineInfoRes != null)
		{
			this.actionOnGetMineInfoRes.Invoke();
			this.actionOnGetMineInfoRes = null;
		}
	}

	private void OnGetEvacuatePetRes(short state, EvacuatePetRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.OnGetEvacuatePetRes);
	}

	private void OnGetElementCopyBattleResultNty(short state, ElementCopyBattleResultNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		ElementInstance.Instance.GetInstanceResult(down);
		EventDispatcher.Broadcast(EventNames.OnGetElementCopyBattleResultNty);
		if (this.GetBlockInfo(this.m_currentFightBlockID).incidentType == RandomIncidentType.IncidentType.MINE)
		{
			this.m_shouldShow = down.result;
		}
	}

	private void OnGetExitElementCopyRes(short state, ExitElementCopyRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.actionOnGetExitElementCopyRes != null)
		{
			this.actionOnGetExitElementCopyRes.Invoke();
			this.actionOnGetExitElementCopyRes = null;
		}
	}

	private void OnGetMineInfoChangedNty(short state, MineInfoChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.m_elementCopyLoginPush.minePetInfos.Clear();
		if (down != null)
		{
			this.m_elementCopyLoginPush.minePetInfos.AddRange(down.minePetinfos);
		}
		EventDispatcher.Broadcast(EventNames.OnGetMineInfoChangedNty);
	}

	private void OnGetLastBlockChangedNty(short state, LastBlockChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_elementCopyLoginPush.lastBlock = down.blockId;
			EventDispatcher.Broadcast(EventNames.OnGetLastBlockChangedNty);
		}
	}

	private void OnGetMineInfoResidueTimeChangeNty(short state, MineInfoResidueTimeChangeNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.mineInfoResidueTimes.get_Count(); i++)
			{
				MineInfoResidueTime mineInfoResidueTime = down.mineInfoResidueTimes.get_Item(i);
				if (this.m_mapTimeCal.ContainsKey(mineInfoResidueTime.blockId))
				{
					this.m_mapTimeCal.get_Item(mineInfoResidueTime.blockId).time = mineInfoResidueTime.residueTime;
				}
				else
				{
					this.m_mapTimeCal.Add(mineInfoResidueTime.blockId, new TimeEnty(mineInfoResidueTime.residueTime));
				}
			}
			EventDispatcher.Broadcast(EventNames.OnGetMineInfoResidueTimeChangeNty);
		}
	}

	private void OnGetPropertyChangedNty(short state, PropertyChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.@object == ObjectType.GameObject.PET)
			{
				this.m_elementCopyLoginPush.petProperty.Clear();
				this.m_elementCopyLoginPush.petProperty.AddRange(down.propertyInfo);
			}
			else
			{
				this.m_elementCopyLoginPush.playerProperty.Clear();
				this.m_elementCopyLoginPush.playerProperty.AddRange(down.propertyInfo);
			}
		}
	}

	public int GetTimeCal(string blockID)
	{
		return this.m_mapTimeCal.get_Item(blockID).time;
	}

	private void DebugBlocks()
	{
	}

	public BlockInfo GetBlockInfo(string blockID)
	{
		return this.m_elementCopyLoginPush.activateBlocks.Find((BlockInfo a) => a.blockId == blockID);
	}

	public bool CheckIsAround(string blockID)
	{
		return DataReader<YBanKuaiSuoYin>.Get(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock).around.Contains(blockID);
	}

	public void BuyRecovery()
	{
		string text = GameDataUtils.GetChineseContent(502310, false);
		string value = DataReader<YWanFaSheZhi>.Get("powerMoney").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		int num = ElementInstanceManager.Instance.m_elementCopyLoginPush.purchaseNum;
		if (num >= array.Length)
		{
			num = array.Length - 1;
		}
		string text2 = array[num];
		text = text.Replace("{s1}", text2);
		text = text.Replace("{s2}", DataReader<YWanFaSheZhi>.Get("powerNum").num.ToString());
		string[] array2 = DataReader<YWanFaSheZhi>.Get("buyTimes").value.Split(new char[]
		{
			';'
		});
		int num2;
		if (EntityWorld.Instance.EntSelf.VipLv >= array2.Length)
		{
			num2 = int.Parse(array2[array2.Length - 1]);
		}
		else
		{
			num2 = int.Parse(array2[EntityWorld.Instance.EntSelf.VipLv]);
		}
		string text3 = num2 - ElementInstanceManager.Instance.m_elementCopyLoginPush.purchaseNum + "/" + num2;
		text = text.Replace("{s3}", text3);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(502309, false), text, delegate
		{
		}, delegate
		{
			ElementInstanceManager.Instance.SendRecoveryToFullEnergyReq();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}
}
