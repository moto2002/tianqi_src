using GameData;
using Package;
using System;
using UnityEngine;
using XNetwork;

public class BoxRewardManager : BaseSubSystemManager
{
	public XDict<int, ChapterAwardInfo> m_mapChapterAwards = new XDict<int, ChapterAwardInfo>();

	public int canShowAwardMaxID;

	protected Action getChapterAwardDataCallback;

	protected static BoxRewardManager instance;

	public static BoxRewardManager Instance
	{
		get
		{
			if (BoxRewardManager.instance == null)
			{
				BoxRewardManager.instance = new BoxRewardManager();
			}
			return BoxRewardManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.m_mapChapterAwards.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ChapterAwardLoginPush>(new NetCallBackMethod<ChapterAwardLoginPush>(this.OnGetChapterAwardLoginPush));
		NetworkManager.AddListenEvent<ChapterAwardChangeNty>(new NetCallBackMethod<ChapterAwardChangeNty>(this.OnGetChapterAwardChangeNty));
		NetworkManager.AddListenEvent<ReceiveAwardRes>(new NetCallBackMethod<ReceiveAwardRes>(this.OnGetReceiveAwardRes));
		NetworkManager.AddListenEvent<GetChapterAwardDataRes>(new NetCallBackMethod<GetChapterAwardDataRes>(this.OnGetChapterAwardDataRes));
	}

	public void SendGetChapterAwardDataReq(int chapterID, Action callback = null)
	{
		this.getChapterAwardDataCallback = callback;
		NetworkManager.Send(new GetChapterAwardDataReq
		{
			canReadChapterAwardId = chapterID
		}, ServerType.Data);
	}

	public void SendReceiveAwardReq(int awardID)
	{
		NetworkManager.Send(new ReceiveAwardReq
		{
			chapterAwardId = awardID
		}, ServerType.Data);
	}

	private void OnGetReceiveAwardRes(short state, ReceiveAwardRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.OnGetReceiveAwardRes);
	}

	private void OnGetChapterAwardLoginPush(short state, ChapterAwardLoginPush down)
	{
	}

	private void OnGetChapterAwardChangeNty(short state, ChapterAwardChangeNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			Debuger.Error("down == null OnGetChapterAwardChangeNty", new object[0]);
			return;
		}
		if (this.m_mapChapterAwards.Count == 0)
		{
			return;
		}
		for (int i = 0; i < down.chapterAwards.get_Count(); i++)
		{
			if (this.m_mapChapterAwards.ContainsKey(down.chapterAwards.get_Item(i).chapterAwardId))
			{
				this.m_mapChapterAwards[down.chapterAwards.get_Item(i).chapterAwardId] = down.chapterAwards.get_Item(i);
			}
			else
			{
				this.m_mapChapterAwards.Add(down.chapterAwards.get_Item(i).chapterAwardId, down.chapterAwards.get_Item(i));
			}
		}
		EventDispatcher.Broadcast(EventNames.OnGetChapterAwardChangeNty);
	}

	private void OnGetChapterAwardDataRes(short state, GetChapterAwardDataRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			Debuger.Error("down == null OnGetChapterAwardChangeNty", new object[0]);
			return;
		}
		for (int i = 0; i < down.chapterAwards.get_Count(); i++)
		{
			if (this.m_mapChapterAwards.ContainsKey(down.chapterAwards.get_Item(i).chapterAwardId))
			{
				this.m_mapChapterAwards[down.chapterAwards.get_Item(i).chapterAwardId] = down.chapterAwards.get_Item(i);
			}
			else
			{
				this.m_mapChapterAwards.Add(down.chapterAwards.get_Item(i).chapterAwardId, down.chapterAwards.get_Item(i));
			}
		}
		if (this.getChapterAwardDataCallback != null)
		{
			this.getChapterAwardDataCallback.Invoke();
		}
	}

	public void GetChapterAward(int chapterID, int type, Action callback = null)
	{
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterOrder == chapterID && a.chapterType == type);
		if (zhuXianZhangJiePeiZhi == null)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
			return;
		}
		bool flag = false;
		for (int i = 0; i < zhuXianZhangJiePeiZhi.needStar.get_Count(); i++)
		{
			int key = this.MakeBoxRewardID(zhuXianZhangJiePeiZhi.id, type, zhuXianZhangJiePeiZhi.needStar.get_Item(i));
			if (!this.m_mapChapterAwards.ContainsKey(key))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.SendGetChapterAwardDataReq(zhuXianZhangJiePeiZhi.id, callback);
		}
		else if (callback != null)
		{
			callback.Invoke();
		}
	}

	public int MakeBoxRewardID(int chapter, int type, int needstar)
	{
		return chapter << 16 | type << 8 | needstar;
	}

	public XDict<string, int> ParseAwardId(int awardId)
	{
		XDict<string, int> xDict = new XDict<string, int>();
		xDict.Add("chapter", (awardId & 16711680) >> 16);
		xDict.Add("type", (awardId & 65280) >> 8);
		xDict.Add("needstar", awardId & 255);
		return xDict;
	}

	protected void DebugInfo()
	{
		for (int i = 0; i < this.m_mapChapterAwards.Values.get_Count(); i++)
		{
			ChapterAwardInfo chapterAwardInfo = this.m_mapChapterAwards.Values.get_Item(i);
			XDict<string, int> xDict = this.ParseAwardId(chapterAwardInfo.chapterAwardId);
			Debug.LogError(string.Concat(new object[]
			{
				"cai.canReceive  ",
				chapterAwardInfo.canReceive,
				"  cai.chapterAwardId   ",
				chapterAwardInfo.chapterAwardId,
				"  cai.isReceived  ",
				chapterAwardInfo.isReceived,
				"  dic chapter  ",
				xDict["chapter"],
				"  dic  type  ",
				xDict["type"],
				"  dic needstar  ",
				xDict["needstar"]
			}));
		}
	}

	public bool CheckNormalDungeonBadge()
	{
		for (int i = 0; i < DungeonManager.Instance.NormalData.get_Count(); i++)
		{
			int chapterId = DungeonManager.Instance.NormalData.get_Item(i).chapterId;
			ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(chapterId);
			for (int j = 0; j < zhuXianZhangJiePeiZhi.needStar.get_Count(); j++)
			{
				int key = this.MakeBoxRewardID(zhuXianZhangJiePeiZhi.id, 101, zhuXianZhangJiePeiZhi.needStar.get_Item(j));
				if (this.m_mapChapterAwards.ContainsKey(key))
				{
					ChapterAwardInfo chapterAwardInfo = BoxRewardManager.Instance.m_mapChapterAwards[key];
					if (chapterAwardInfo.canReceive && !chapterAwardInfo.isReceived)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool CheckEliteDungeonBadge()
	{
		for (int i = 0; i < DungeonManager.Instance.EliteData.get_Count(); i++)
		{
			int chapterId = DungeonManager.Instance.EliteData.get_Item(i).chapterId;
			ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(chapterId);
			for (int j = 0; j < zhuXianZhangJiePeiZhi.needStar.get_Count(); j++)
			{
				int key = this.MakeBoxRewardID(zhuXianZhangJiePeiZhi.id, 102, zhuXianZhangJiePeiZhi.needStar.get_Item(j));
				if (this.m_mapChapterAwards.ContainsKey(key))
				{
					ChapterAwardInfo chapterAwardInfo = BoxRewardManager.Instance.m_mapChapterAwards[key];
					if (chapterAwardInfo.canReceive && !chapterAwardInfo.isReceived)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
