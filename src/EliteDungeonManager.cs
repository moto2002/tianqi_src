using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class EliteDungeonManager : BaseSubSystemManager, ITeamRuleManager
{
	private Dictionary<int, MapInfo> eliteMapInfoDic;

	private List<EliteCopyInfo> eliteCopyInfoList;

	private List<EliteDataInfo> eliteDataList;

	public int RestGetRewardTimes;

	private int matchingLv;

	private static EliteDungeonManager instance;

	public int eliteCfgID;

	public int newOpenCfgID = -1;

	public Dictionary<int, MapInfo> EliteMapInfoDic
	{
		get
		{
			return this.eliteMapInfoDic;
		}
	}

	public List<EliteCopyInfo> EliteCopyInfoList
	{
		get
		{
			return this.eliteCopyInfoList;
		}
	}

	public List<EliteDataInfo> EliteDataList
	{
		get
		{
			return this.eliteDataList;
		}
	}

	public int MatchingLv
	{
		get
		{
			if (this.matchingLv <= 0)
			{
				this.matchingLv = DataReader<JJingYingFuBenPeiZhi>.Get("matchingLv").num;
			}
			return this.matchingLv;
		}
		set
		{
			this.matchingLv = value;
		}
	}

	public static EliteDungeonManager Instance
	{
		get
		{
			if (EliteDungeonManager.instance == null)
			{
				EliteDungeonManager.instance = new EliteDungeonManager();
			}
			return EliteDungeonManager.instance;
		}
	}

	public override void Init()
	{
		this.eliteDataList = new List<EliteDataInfo>();
		this.eliteMapInfoDic = new Dictionary<int, MapInfo>();
		this.eliteCopyInfoList = new List<EliteCopyInfo>();
		base.Init();
	}

	public override void Release()
	{
		this.eliteCopyInfoList = null;
		this.eliteMapInfoDic = null;
		this.eliteDataList = null;
		this.newOpenCfgID = -1;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<EliteCopyPush>(new NetCallBackMethod<EliteCopyPush>(this.OnEliteCopyPush));
		NetworkManager.AddListenEvent<EliteChangeNty>(new NetCallBackMethod<EliteChangeNty>(this.OnEliteChangeNty));
		NetworkManager.AddListenEvent<MapChangeNty>(new NetCallBackMethod<MapChangeNty>(this.OnMapChangeNty));
		NetworkManager.AddListenEvent<GetEliteCopyInfoRes>(new NetCallBackMethod<GetEliteCopyInfoRes>(this.OnGetEliteCopyInfoRes));
		NetworkManager.AddListenEvent<EliteCanGetPrizeTimesNty>(new NetCallBackMethod<EliteCanGetPrizeTimesNty>(this.OnEliteCanGetPrizeTimesNty));
		NetworkManager.AddListenEvent<EliteChallengeRes>(new NetCallBackMethod<EliteChallengeRes>(this.OnEliteChallengeRes));
		NetworkManager.AddListenEvent<EliteExitRes>(new NetCallBackMethod<EliteExitRes>(this.OnEliteExitRes));
		NetworkManager.AddListenEvent<EliteBattleResultNty>(new NetCallBackMethod<EliteBattleResultNty>(this.OnEliteBattleResultNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.VipLvChanged, new Callback(this.OnRoleVipChange));
	}

	public void SendEliteCopyInfoReq(int _id)
	{
		NetworkManager.Send(new GetEliteCopyInfoReq
		{
			mapId = _id
		}, ServerType.Data);
	}

	public void SendEliteChallengeReq(int id)
	{
		WaitUI.OpenUI(0u);
		this.eliteCfgID = id;
		Debug.Log("挑战的精英副本配置ID为--------" + this.eliteCfgID);
		NetworkManager.Send(new EliteChallengeReq
		{
			copyId = id
		}, ServerType.Data);
	}

	public void SendEliteAutoMatchReq(int eliteCopyID)
	{
		this.eliteCfgID = eliteCopyID;
		NetworkManager.Send(new EliteAutoMatchReq
		{
			copyId = eliteCopyID
		}, ServerType.Data);
	}

	public void SendEliteCancelMatchReq()
	{
		NetworkManager.Send(new EliteCancelMatchReq(), ServerType.Data);
	}

	public void SendEliteQuickEnterReq(int eliteCopyID)
	{
		this.eliteCfgID = eliteCopyID;
		NetworkManager.Send(new EliteQuickEnterReq
		{
			copyId = eliteCopyID
		}, ServerType.Data);
	}

	public void SendEliteExitReq()
	{
		NetworkManager.Send(new EliteExitReq(), ServerType.Data);
	}

	public void SendEliteChanllengeFirstReq()
	{
		WaitUI.OpenUI(0u);
		NetworkManager.Send(new EliteChallengeFirstReq(), ServerType.Data);
	}

	private void OnEliteCopyPush(short state, EliteCopyPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.info.get_Count(); i++)
			{
				int mapId = down.info.get_Item(i).mapId;
				if (this.eliteMapInfoDic.ContainsKey(mapId))
				{
					this.eliteMapInfoDic.set_Item(mapId, down.info.get_Item(i));
				}
				else
				{
					this.eliteMapInfoDic.Add(mapId, down.info.get_Item(i));
				}
			}
			this.SetEliteDataList();
		}
	}

	private void OnEliteCanGetPrizeTimesNty(short state, EliteCanGetPrizeTimesNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RestGetRewardTimes = down.remainTimes;
			EventDispatcher.Broadcast(EventNames.EliteCopyMiscChange);
		}
	}

	private void OnEliteChangeNty(short state, EliteChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int id = down.info.copyId;
			this.newOpenCfgID = id;
			int num = this.eliteCopyInfoList.FindIndex((EliteCopyInfo a) => a.copyId == id);
			if (num >= 0)
			{
				this.eliteCopyInfoList.set_Item(num, down.info);
			}
			else
			{
				this.eliteCopyInfoList.Add(down.info);
			}
			this.UpdateToEliteDataList(down.info);
			EventDispatcher.Broadcast(EventNames.UpdataEliteInfo);
		}
	}

	private void OnMapChangeNty(short state, MapChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"OnMapChangeNty,mapId:",
				down.info.mapId,
				"isOpen : ",
				down.info.openFlag
			}));
			int mapId = down.info.mapId;
			if (this.eliteMapInfoDic.ContainsKey(mapId))
			{
				this.eliteMapInfoDic.set_Item(mapId, down.info);
			}
			else
			{
				this.eliteMapInfoDic.Add(mapId, down.info);
			}
			EventDispatcher.Broadcast(EventNames.EliteMapChange);
		}
	}

	private void OnGetEliteCopyInfoRes(short state, GetEliteCopyInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.info.get_Count(); i++)
			{
				int cfgId = down.info.get_Item(i).copyId;
				int num = this.eliteCopyInfoList.FindIndex((EliteCopyInfo a) => a.copyId == cfgId);
				if (num >= 0)
				{
					this.eliteCopyInfoList.set_Item(num, down.info.get_Item(i));
				}
				else
				{
					this.eliteCopyInfoList.Add(down.info.get_Item(i));
				}
				this.UpdateToEliteDataList(down.info.get_Item(i));
			}
			EventDispatcher.Broadcast(EventNames.UpdataEliteInfo);
		}
	}

	private void OnEliteChallengeRes(short state, EliteChallengeRes down = null)
	{
		WaitUI.CloseUI(0u);
		JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(this.eliteCfgID);
		if (jingYingFuBenPeiZhi != null && (int)state == Status.ROLE_LEVEL_LIMIT)
		{
			string text = GameDataUtils.GetChineseContent(505029, false);
			text = string.Format(text, jingYingFuBenPeiZhi.level);
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if ((int)state == Status.ELITE_DUNGEON_LIMIT)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516131, false));
			return;
		}
		if (state != 0)
		{
			if (down != null && down.reasons != null && down.reasons.get_Count() > 0)
			{
				TeamDetailReason.RSType reasonType = down.reasons.get_Item(0).reasonType;
				int num = 0;
				TeamDetailReason.RSType rSType = reasonType;
				switch (rSType)
				{
				case TeamDetailReason.RSType.LvLimit:
					num = 516123;
					goto IL_14D;
				case TeamDetailReason.RSType.TimesLimit:
					num = 516120;
					goto IL_14D;
				case TeamDetailReason.RSType.BagFull:
					num = 516122;
					goto IL_14D;
				case TeamDetailReason.RSType.NotFound:
				case (TeamDetailReason.RSType)6:
				case (TeamDetailReason.RSType)7:
				case (TeamDetailReason.RSType)8:
				case (TeamDetailReason.RSType)9:
				case (TeamDetailReason.RSType)10:
					IL_EA:
					if (rSType != TeamDetailReason.RSType.Others)
					{
						goto IL_14D;
					}
					goto IL_14D;
				case TeamDetailReason.RSType.InFighting:
					num = 516115;
					goto IL_14D;
				case TeamDetailReason.RSType.NotAgree:
					num = 516119;
					goto IL_14D;
				case TeamDetailReason.RSType.NotAnswer:
					num = 516119;
					goto IL_14D;
				case TeamDetailReason.RSType.DungeonLimit:
					num = 516129;
					goto IL_14D;
				}
				goto IL_EA;
				IL_14D:
				if (num != 0)
				{
					string chineseContent = GameDataUtils.GetChineseContent(num, false);
					UIManagerControl.Instance.ShowToastText(chineseContent);
					return;
				}
			}
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OnChallengeRes();
	}

	private void OnEliteAutoMatchRes(short state, EliteAutoMatchRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int countDown = (int)float.Parse(DataReader<MultiCopy>.Get("match_auto_time").value);
		this.OnMatchRes(countDown, false, null);
	}

	private void OnEliteCancelMatchRes(short state, EliteCancelMatchRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		TeamBasicManager.Instance.HideMatchUI();
	}

	private void OnEliteQuickEnterRes(short state, EliteQuickEnterRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int countDown = (int)float.Parse(DataReader<MultiCopy>.Get("match_2_time").value);
		this.OnMatchRes(countDown, false, null);
	}

	private void OnEliteExitRes(short state, EliteExitRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
	}

	private void OnEliteBattleResultNty(short state, EliteBattleResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			InstanceManager.StopAllClientAI(true);
			DungeonEliteInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnEliteChallengeFirstRes(short state, EliteChallengeFirstRes down = null)
	{
		if (state != 0)
		{
			WaitUI.CloseUI(0u);
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void SetEliteDataList()
	{
		List<JingYingFuBenPeiZhi> dataList = DataReader<JingYingFuBenPeiZhi>.DataList;
		if (dataList == null)
		{
			return;
		}
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			JingYingFuBenPeiZhi dataCfg = dataList.get_Item(i);
			if (this.eliteDataList == null)
			{
				this.eliteDataList = new List<EliteDataInfo>();
			}
			int num = this.eliteDataList.FindIndex((EliteDataInfo a) => a.BossID == dataCfg.bossName && a.ArenaID == dataCfg.map);
			if (num < 0)
			{
				EliteDataInfo eliteDataInfo = new EliteDataInfo(dataCfg.bossName);
				eliteDataInfo.cfgIDList.Add(dataCfg.id);
				eliteDataInfo.ArenaID = dataCfg.map;
				eliteDataInfo.BossIconID = dataCfg.bossPic;
				eliteDataInfo.TaskID = dataCfg.mission;
				this.eliteDataList.Add(eliteDataInfo);
			}
			else
			{
				int num2 = this.eliteDataList.get_Item(num).cfgIDList.FindIndex((int a) => a == dataCfg.id);
				if (num2 < 0)
				{
					this.eliteDataList.get_Item(num).cfgIDList.Add(dataCfg.id);
				}
			}
		}
		if (this.eliteCopyInfoList == null)
		{
			return;
		}
		for (int j = 0; j < this.eliteCopyInfoList.get_Count(); j++)
		{
			int copyId = this.eliteCopyInfoList.get_Item(j).copyId;
			JingYingFuBenPeiZhi dataCfg = DataReader<JingYingFuBenPeiZhi>.Get(copyId);
			if (dataCfg != null)
			{
				int num3 = this.eliteDataList.FindIndex((EliteDataInfo a) => a.BossID == dataCfg.bossName && a.ArenaID == dataCfg.map);
				if (num3 >= 0)
				{
					this.eliteDataList.get_Item(num3).isOpen = true;
					if (this.eliteDataList.get_Item(num3).eliteCopyInfoDic.ContainsKey(copyId))
					{
						this.eliteDataList.get_Item(num3).eliteCopyInfoDic.set_Item(copyId, this.eliteCopyInfoList.get_Item(j));
					}
					else
					{
						this.eliteDataList.get_Item(num3).eliteCopyInfoDic.Add(copyId, this.eliteCopyInfoList.get_Item(j));
					}
				}
			}
		}
	}

	private void UpdateToEliteDataList(EliteCopyInfo Info)
	{
		JingYingFuBenPeiZhi cfgData = DataReader<JingYingFuBenPeiZhi>.Get(Info.copyId);
		if (cfgData != null)
		{
			int num = this.eliteDataList.FindIndex((EliteDataInfo a) => a.BossID == cfgData.bossName && a.ArenaID == cfgData.map);
			if (num < 0)
			{
				return;
			}
			this.eliteDataList.get_Item(num).isOpen = true;
			if (!this.eliteDataList.get_Item(num).eliteCopyInfoDic.ContainsKey(Info.copyId))
			{
				this.eliteDataList.get_Item(num).eliteCopyInfoDic.Add(Info.copyId, Info);
			}
			else
			{
				this.eliteDataList.get_Item(num).eliteCopyInfoDic.set_Item(Info.copyId, Info);
			}
		}
	}

	private void OnRoleVipChange()
	{
		EventDispatcher.Broadcast(EventNames.EliteCopyMiscChange);
	}

	public int GetVIPCanGetPrizeTimesMax()
	{
		return VIPPrivilegeManager.Instance.GetPrizeTimes();
	}

	public string GetResetGetPrizeTimesStr()
	{
		string result = string.Empty;
		string value = DataReader<JJingYingFuBenPeiZhi>.Get("recoverAndTime").value;
		if (value != null)
		{
			string[] array = value.Split(new char[]
			{
				';'
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string[] array2 = text.Split(new char[]
				{
					','
				});
				string[] array3 = array2[1].Split(new char[]
				{
					':'
				});
				int num2 = 0;
				if (array3.Length > 0)
				{
					num2 = int.Parse(array3[0]);
				}
				DateTime dateTime = new DateTime(TimeManager.Instance.PreciseServerTime.get_Year(), TimeManager.Instance.PreciseServerTime.get_Month(), TimeManager.Instance.PreciseServerTime.get_Day(), num2, 0, 0);
				if ((TimeManager.Instance.PreciseServerTime - dateTime).get_TotalMinutes() <= 0.0)
				{
					break;
				}
				num = i + 1;
			}
			if (array.Length > 0)
			{
				num %= array.Length;
			}
			if (num < array.Length)
			{
				string text2 = array[num];
				string[] array4 = text2.Split(new char[]
				{
					','
				});
				string text3 = array4[1];
				int num3 = int.Parse(array4[0]);
				string[] array5 = array4[1].Split(new char[]
				{
					':'
				});
				string text4 = string.Empty;
				int num4 = 0;
				if (array5.Length > 0)
				{
					num4 = int.Parse(array5[0]);
				}
				if (num4 <= 12)
				{
					text4 = "上午" + GameDataUtils.GetChineseContent(505054, false);
				}
				else
				{
					num4 -= 12;
					text4 = "下午" + GameDataUtils.GetChineseContent(505054, false);
				}
				result = string.Format(text4, num4, num3);
			}
		}
		return result;
	}

	public int GetEliteScoreRankIndex(int passTime)
	{
		int count = DataReader<JTongGuanPingJi>.DataList.get_Count();
		for (int i = 0; i < count; i++)
		{
			int time = DataReader<JTongGuanPingJi>.DataList.get_Item(i).time;
			if (passTime < time)
			{
				return DataReader<JTongGuanPingJi>.DataList.get_Item(i).rank;
			}
		}
		return DataReader<JTongGuanPingJi>.DataList.get_Item(count - 1).rank;
	}

	public int GetScoreTime(int index)
	{
		int result = 0;
		JTongGuanPingJi jTongGuanPingJi = DataReader<JTongGuanPingJi>.Get(index);
		if (jTongGuanPingJi != null)
		{
			result = jTongGuanPingJi.time;
		}
		return result;
	}

	public string GetScoreText(int index, int time)
	{
		string text = string.Empty;
		text = string.Format(GameDataUtils.GetChineseContent(505407, false), time);
		return text + "\n" + GameDataUtils.GetChineseContent(505407 + index, false);
	}

	public string GetScoreRewardText(int index)
	{
		string empty = string.Empty;
		int num = 0;
		JTongGuanPingJi jTongGuanPingJi = DataReader<JTongGuanPingJi>.Get(index);
		if (jTongGuanPingJi != null)
		{
			num = (int)(jTongGuanPingJi.ratio * 100f);
		}
		return string.Format(GameDataUtils.GetChineseContent(505413, false), num);
	}

	public int GetCanStarFightBossIndex(int AreanaID = 1)
	{
		EliteDungeonManager.<GetCanStarFightBossIndex>c__AnonStorey146 <GetCanStarFightBossIndex>c__AnonStorey = new EliteDungeonManager.<GetCanStarFightBossIndex>c__AnonStorey146();
		int num = 0;
		<GetCanStarFightBossIndex>c__AnonStorey.dataList = DataReader<JingYingFuBenPeiZhi>.DataList;
		if (<GetCanStarFightBossIndex>c__AnonStorey.dataList == null)
		{
			return num;
		}
		List<JingYingFuBenPeiZhi> list = new List<JingYingFuBenPeiZhi>();
		int i;
		for (i = 0; i < <GetCanStarFightBossIndex>c__AnonStorey.dataList.get_Count(); i++)
		{
			if (<GetCanStarFightBossIndex>c__AnonStorey.dataList.get_Item(i).map == AreanaID)
			{
				int num2 = list.FindIndex((JingYingFuBenPeiZhi a) => a.bossName == <GetCanStarFightBossIndex>c__AnonStorey.dataList.get_Item(i).bossName);
				if (num2 < 0)
				{
					list.Add(<GetCanStarFightBossIndex>c__AnonStorey.dataList.get_Item(i));
				}
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			int power = list.get_Item(j).power;
			bool flag = EntityWorld.Instance.EntSelf.Lv >= power;
			if (flag && j >= num)
			{
				num = j;
			}
		}
		return num;
	}

	public bool CheckCopyIsOpen(int eliteCfgID)
	{
		if (this.EliteCopyInfoList != null)
		{
			int num = this.EliteCopyInfoList.FindIndex((EliteCopyInfo a) => a.copyId == eliteCfgID);
			if (num >= 0)
			{
				return true;
			}
		}
		return false;
	}

	public int GetCanChallegeRankIndex(List<int> eliteCfgList)
	{
		if (eliteCfgList != null)
		{
			int result = 0;
			for (int i = 0; i < eliteCfgList.get_Count(); i++)
			{
				if (this.CheckCopyIsOpen(eliteCfgList.get_Item(i)))
				{
					result = i;
				}
			}
			return result;
		}
		return 0;
	}

	public void CheckCanStarFight(int copyCfgID)
	{
		if (EliteDungeonManager.Instance.RestGetRewardTimes <= 0)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), "您的可领取次数已用完，无法作为队长主动开始副本和快速匹配，可加入其他玩家的队伍进行挑战", null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
			return;
		}
		TeamBasicManager.Instance.OnClickStart(DungeonType.ENUM.Elite, null, delegate
		{
			EliteDungeonManager.Instance.SendEliteChallengeReq(copyCfgID);
		});
	}

	public void OnChallengeRes()
	{
		WaitUI.OpenUI(0u);
		TeamBasicManager.Instance.OnChallengeSuccessCallBack(DungeonType.ENUM.Team, 1, TeamBasicManager.Instance.CdTime, null);
	}

	public void OnMakeTeam(DungeonType.ENUM dungeonType, List<int> dungeonParams = null, int systemID = 0)
	{
		TeamBasicManager.Instance.OnMakeTeamByDungeonType(dungeonType, dungeonParams, systemID);
	}

	public void OnMatchFailedCallBack()
	{
	}

	public void OnMatchRes(int countDown, bool isOrder = false, Action callBack = null)
	{
	}
}
