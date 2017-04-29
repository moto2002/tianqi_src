using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class BroadcastManager : BaseSubSystemManager
{
	private const int EVENT_MAX = 5;

	private Queue<string> m_BroadcastQueue = new Queue<string>();

	private bool m_isMoveOver = true;

	private static BroadcastManager instance;

	public static BroadcastManager Instance
	{
		get
		{
			if (BroadcastManager.instance == null)
			{
				BroadcastManager.instance = new BroadcastManager();
			}
			return BroadcastManager.instance;
		}
	}

	private BroadcastManager()
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
		EventDispatcher.AddListener<bool>(EventNames.BroadcastOfTownUI, new Callback<bool>(this.BroadcastOfTownUI));
		NetworkManager.AddListenEvent<NewBroadCastInfoPush>(new NetCallBackMethod<NewBroadCastInfoPush>(this.OnNewBroadCastInfoPush));
	}

	public void OnNewBroadCastInfoPush(short state, NewBroadCastInfoPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int id = down.id;
			GuangBoNeiRong guangBoNeiRong = DataReader<GuangBoNeiRong>.Get(id);
			if (guangBoNeiRong == null)
			{
				return;
			}
			int show = guangBoNeiRong.show;
			switch (show)
			{
			case 0:
				this.AddQueue(this.ContentFormat(down));
				this.BroadcastToChatManager(down);
				break;
			case 1:
				this.AddQueue(this.ContentFormat(down));
				break;
			case 2:
				this.BroadcastToChatManager(down);
				break;
			default:
				Debug.LogError("不存在的显示类型：" + show);
				break;
			}
		}
	}

	private void BroadcastOfTownUI(bool isEnable)
	{
		if (!this.m_isMoveOver)
		{
			BroadcastUI.Instance.Show(true);
		}
	}

	public void MoveOver()
	{
		this.m_isMoveOver = true;
		if (this.m_BroadcastQueue.get_Count() != 0 && this.m_isMoveOver)
		{
			this.m_isMoveOver = false;
			this.OpenBroadcast(this.m_BroadcastQueue.Dequeue());
		}
		else
		{
			TimerHeap.AddTimer(500u, 0, new Action(this.CloseUI));
		}
	}

	public void AddQueue(string content)
	{
		if (!string.IsNullOrEmpty(content))
		{
			this.m_BroadcastQueue.Enqueue(content);
		}
		if (this.m_isMoveOver)
		{
			this.m_isMoveOver = false;
			this.OpenBroadcast(this.m_BroadcastQueue.Dequeue());
		}
	}

	private void CloseUI()
	{
		if (this.m_BroadcastQueue.get_Count() != 0 && this.m_isMoveOver)
		{
			this.m_isMoveOver = false;
			this.OpenBroadcast(this.m_BroadcastQueue.Dequeue());
		}
		else if (this.m_isMoveOver && BroadcastUI.Instance != null)
		{
			BroadcastUI.Instance.Show(false);
		}
	}

	private void OpenBroadcast(string _content)
	{
		BroadcastUI broadcastUI = UIManagerControl.Instance.OpenUI("BroadcastUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as BroadcastUI;
		broadcastUI.ShowBroadcast(_content);
	}

	private void BroadcastToChatManager(NewBroadCastInfoPush down)
	{
		GuangBoNeiRong guangBoNeiRong = DataReader<GuangBoNeiRong>.Get(down.id);
		if (guangBoNeiRong == null)
		{
			return;
		}
		ChineseData chineseData = DataReader<ChineseData>.Get(guangBoNeiRong.desc);
		if (chineseData == null)
		{
			return;
		}
		if (guangBoNeiRong.hitEventId.get_Count() > 5)
		{
			return;
		}
		string text = chineseData.content;
		text = this.FormatDesc(text, guangBoNeiRong.hitEventId.get_Count());
		this.RefreshDetailInfos(down.paramters, guangBoNeiRong.hitEventId);
		ChatManager.Instance.BroadcastMessageReceive(text, down.paramters);
	}

	private void RefreshDetailInfos(List<DetailInfo> detailInfos, List<int> eventIds)
	{
		if (detailInfos == null || detailInfos.get_Count() == 0 || eventIds == null || eventIds.get_Count() == 0)
		{
			return;
		}
		int num = 0;
		while (num < detailInfos.get_Count() && num < eventIds.get_Count())
		{
			DetailInfo detailInfo = detailInfos.get_Item(num);
			if (eventIds.get_Item(num) > 0)
			{
				GuangBoLianJie guangBoLianJie = DataReader<GuangBoLianJie>.Get(eventIds.get_Item(num));
				if (guangBoLianJie != null)
				{
					detailInfo.type = LinkType.GetDetailType(guangBoLianJie.type);
					if (detailInfo.type == DetailType.DT.UI)
					{
						detailInfo.cfgId = guangBoLianJie.link;
						detailInfo.label = GameDataUtils.GetChineseContent(guangBoLianJie.name, false);
					}
					else if (detailInfo.type == DetailType.DT.Interface)
					{
						detailInfo.cfgId = guangBoLianJie.hitEventId;
						detailInfo.label = GameDataUtils.GetChineseContent(guangBoLianJie.name, false);
					}
					else if (detailInfo.type == DetailType.DT.Equipment && eventIds.get_Item(num) == 66)
					{
						detailInfo.label = GameDataUtils.GetItemName(detailInfo.cfgId, false, 0L);
					}
					if (guangBoLianJie.click == 0)
					{
						detailInfo.type = DetailType.DT.Default;
					}
				}
			}
			else
			{
				detailInfo.type = DetailType.DT.Default;
			}
			num++;
		}
	}

	private string ContentFormat(NewBroadCastInfoPush down = null)
	{
		if (down == null)
		{
			return string.Empty;
		}
		int id = down.id;
		GuangBoNeiRong guangBoNeiRong = DataReader<GuangBoNeiRong>.Get(id);
		if (guangBoNeiRong == null)
		{
			return string.Empty;
		}
		string chineseContent = GameDataUtils.GetChineseContent(guangBoNeiRong.desc, false);
		if (string.IsNullOrEmpty(chineseContent))
		{
			return string.Empty;
		}
		List<string> list = new List<string>();
		List<int> hitEventId = DataReader<GuangBoNeiRong>.Get(id).hitEventId;
		int count = hitEventId.get_Count();
		if (count >= 1)
		{
			for (int i = 0; i < count; i++)
			{
				int type = DataReader<GuangBoLianJie>.Get(hitEventId.get_Item(i)).type;
				switch (type)
				{
				case 0:
					list.Add((i >= down.paramters.get_Count()) ? string.Empty : down.paramters.get_Item(i).label);
					break;
				case 1:
					list.Add((i >= down.paramters.get_Count()) ? string.Empty : down.paramters.get_Item(i).label);
					break;
				case 2:
					list.Add((down.paramters.get_Count() >= i) ? string.Empty : GameDataUtils.GetItemName(down.paramters.get_Item(i).cfgId, false, 0L));
					break;
				case 3:
				case 4:
					list.Add(GameDataUtils.GetChineseContent(DataReader<GuangBoLianJie>.Get(hitEventId.get_Item(i)).name, false));
					break;
				default:
					Debug.LogError(string.Concat(new object[]
					{
						"There is no hitType: ",
						type,
						" ,  Broadcast ID: ",
						id
					}));
					list.Add(string.Empty);
					break;
				}
			}
		}
		string result;
		try
		{
			if (count <= 0)
			{
				result = chineseContent;
			}
			else if (count == 1)
			{
				result = string.Format(chineseContent, list.get_Item(0));
			}
			else if (count == 2)
			{
				result = string.Format(chineseContent, list.get_Item(0), list.get_Item(1));
			}
			else if (count == 3)
			{
				result = string.Format(chineseContent, list.get_Item(0), list.get_Item(1), list.get_Item(2));
			}
			else if (count == 4)
			{
				result = string.Format(chineseContent, new object[]
				{
					list.get_Item(0),
					list.get_Item(1),
					list.get_Item(2),
					list.get_Item(3)
				});
			}
			else if (count == 5)
			{
				result = string.Format(chineseContent, new object[]
				{
					list.get_Item(0),
					list.get_Item(1),
					list.get_Item(2),
					list.get_Item(3),
					list.get_Item(4)
				});
			}
			else
			{
				result = string.Empty;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_Message());
			Debug.LogError(string.Concat(new object[]
			{
				"广播Format出错, _id = ",
				id,
				", desc = ",
				chineseContent
			}));
			result = string.Empty;
		}
		return result;
	}

	private string FormatDesc(string desc, int event_count)
	{
		if (event_count <= 0)
		{
			return desc;
		}
		if (event_count == 1)
		{
			return string.Format(desc, ChatManager.ItemPlaceholder);
		}
		if (event_count == 2)
		{
			return string.Format(desc, ChatManager.ItemPlaceholder, ChatManager.ItemPlaceholder);
		}
		if (event_count == 3)
		{
			return string.Format(desc, ChatManager.ItemPlaceholder, ChatManager.ItemPlaceholder, ChatManager.ItemPlaceholder);
		}
		if (event_count == 4)
		{
			return string.Format(desc, new object[]
			{
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder
			});
		}
		if (event_count == 5)
		{
			return string.Format(desc, new object[]
			{
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder,
				ChatManager.ItemPlaceholder
			});
		}
		return string.Empty;
	}
}
