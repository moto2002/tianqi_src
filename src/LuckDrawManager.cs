using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class LuckDrawManager : BaseSubSystemManager
{
	public const int DRAW_ID_EQUIP = 3;

	public const int DRAW_ID_TIMEONE = 5;

	public Dictionary<int, LuckDrawInfo> LuckDrawInfoMap = new Dictionary<int, LuckDrawInfo>();

	public Dictionary<int, DateTime> DrawIdDateTime = new Dictionary<int, DateTime>();

	public int lastSelectMode;

	public bool IsShowLuckDrawTip;

	private static LuckDrawManager instance;

	public int lastDrawType;

	public static LuckDrawManager Instance
	{
		get
		{
			if (LuckDrawManager.instance == null)
			{
				LuckDrawManager.instance = new LuckDrawManager();
			}
			return LuckDrawManager.instance;
		}
	}

	private LuckDrawManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.LuckDrawInfoMap.Clear();
		this.DrawIdDateTime.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DrawChangeNty>(new NetCallBackMethod<DrawChangeNty>(this.OnDrawChangeNty));
		NetworkManager.AddListenEvent<LuckDrawLoginPush>(new NetCallBackMethod<LuckDrawLoginPush>(this.OnLuckDrawLoginPush));
		NetworkManager.AddListenEvent<DrawRes>(new NetCallBackMethod<DrawRes>(this.OnDrawRes));
	}

	public void LuckDrawReq(UIBase ui, int type)
	{
		if (!BackpackManager.Instance.ShowBackpackFull())
		{
			this.lastDrawType = type;
			EventDispatcher.Broadcast<bool>("EventNames.EnableFloating", false);
			NetworkManager.Send(new DrawReq
			{
				drawId = type
			}, ServerType.Data);
		}
	}

	private void OnLuckDrawLoginPush(short state, LuckDrawLoginPush down = null)
	{
		this.OnLuckDrawInfo(down.luckDrawInfos);
	}

	private void OnDrawChangeNty(short state, DrawChangeNty down = null)
	{
		this.OnLuckDrawInfo(down.luckDrawInfos);
	}

	private void OnDrawRes(short state, DrawRes down = null)
	{
		if (LuckDrawUI.Instance != null)
		{
			LuckDrawUI.Instance.SetButtonLuckDrawLock(true);
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, down.resId);
			return;
		}
		if (down != null && down.awardInfos != null && down.awardInfos.get_Count() > 0)
		{
			LuckDrawResult luckDrawResult = UIManagerControl.Instance.OpenUI("LuckDrawResult", UINodesManager.NormalUIRoot, true, UIType.NonPush) as LuckDrawResult;
			luckDrawResult.UpdateUI(down.awardInfos);
		}
		else
		{
			Debug.Log("===========抽奖返回结果为空，找服务端=====");
		}
	}

	private void OnLuckDrawInfo(List<LuckDrawInfo> luckDrawInfoList)
	{
		using (List<LuckDrawInfo>.Enumerator enumerator = luckDrawInfoList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				LuckDrawInfo current = enumerator.get_Current();
				this.LuckDrawInfoMap.Remove(current.drawId);
				this.LuckDrawInfoMap.Add(current.drawId, current);
				this.DrawIdDateTime.Remove(current.drawId);
				this.DrawIdDateTime.Add(current.drawId, TimeManager.Instance.PreciseServerTime.AddSeconds((double)current.refreshTime));
			}
		}
		EventDispatcher.Broadcast(EventNames.OnLuckDrawInfoChangeNty);
		this.CheckTipsInTownUI();
	}

	public void Broadcast()
	{
		EventDispatcher.Broadcast(EventNames.OnLuckDrawInfoChangeNty);
	}

	public bool CheckDrawType(int drawId)
	{
		bool result = false;
		ChouJiangXiaoHao chouJiangXiaoHao = DataReader<ChouJiangXiaoHao>.Get(drawId);
		if (chouJiangXiaoHao == null)
		{
			Debug.Log("GameData.ChouJiangXiaoHao is exist, id = " + drawId);
			return false;
		}
		if (drawId == 1 || drawId == 2)
		{
			if (BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) >= (long)chouJiangXiaoHao.lotteryAmount)
			{
				result = true;
			}
		}
		else if (drawId == 3 || drawId == 5)
		{
			if (this.DrawIdDateTime.ContainsKey(drawId) && TimeManager.Instance.PreciseServerTime > this.DrawIdDateTime.get_Item(drawId))
			{
				result = true;
			}
			if (BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) >= (long)chouJiangXiaoHao.lotteryAmount)
			{
				result = true;
			}
			if (EntityWorld.Instance.EntSelf.Diamond >= chouJiangXiaoHao.amount)
			{
				result = true;
			}
		}
		else if (EntityWorld.Instance.EntSelf.Diamond >= chouJiangXiaoHao.amount)
		{
			result = true;
		}
		return result;
	}

	public void LuckDrawAgain(UIBase ui)
	{
		this.LuckDrawReq(ui, this.lastDrawType);
	}

	public bool CheckTipOfTimeOne()
	{
		return !this.DrawIdDateTime.ContainsKey(5) || this.DrawIdDateTime.get_Item(5) <= TimeManager.Instance.PreciseServerTime;
	}

	public bool CheckTipOfEquip()
	{
		return !this.DrawIdDateTime.ContainsKey(3) || this.DrawIdDateTime.get_Item(3) <= TimeManager.Instance.PreciseServerTime;
	}

	public void CheckTipsInTownUI()
	{
		this.IsShowLuckDrawTip = false;
		if (this.CheckDrawType(1))
		{
			this.IsShowLuckDrawTip = true;
		}
		else if (this.CheckTipOfTimeOne())
		{
			this.IsShowLuckDrawTip = true;
		}
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsTownUiLuckDraw, this.IsShowLuckDrawTip);
	}
}
