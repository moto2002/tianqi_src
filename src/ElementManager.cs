using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class ElementManager : BaseSubSystemManager
{
	public List<ElementInfo> elementInfos = new List<ElementInfo>();

	private static ElementManager m_Instance;

	public static ElementManager Instance
	{
		get
		{
			if (ElementManager.m_Instance == null)
			{
				ElementManager.m_Instance = new ElementManager();
			}
			return ElementManager.m_Instance;
		}
	}

	private ElementManager()
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
		NetworkManager.AddListenEvent<ElementLoginPush>(new NetCallBackMethod<ElementLoginPush>(this.OnGetElementLoginPush));
		NetworkManager.AddListenEvent<ElementChangedNty>(new NetCallBackMethod<ElementChangedNty>(this.OnGetElementChangedNty));
		NetworkManager.AddListenEvent<ElementUpRes>(new NetCallBackMethod<ElementUpRes>(this.OnGetElementUpRes));
	}

	public void SendElementUpReq(int elemTypeParm, int subElemTypeParm)
	{
		Debuger.Error(string.Concat(new object[]
		{
			"SendElementUpReq elemTypeParm  ",
			elemTypeParm,
			"  subElemTypeParm  ",
			subElemTypeParm
		}), new object[0]);
		NetworkManager.Send(new ElementUpReq
		{
			elemId = this.MakeElementID(elemTypeParm, subElemTypeParm)
		}, ServerType.Data);
	}

	private void OnGetElementLoginPush(short state, ElementLoginPush down)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.elementInfos = down.elems;
			}
			else
			{
				Debuger.Error("down == null OnGetElementLoginPush", new object[0]);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetElementChangedNty(short state, ElementChangedNty down)
	{
		if (state == 0)
		{
			if (down != null)
			{
				Debuger.Error("OnGetElementChangedNty", new object[0]);
				for (int i = 0; i < down.elems.get_Count(); i++)
				{
					ElementInfo elementInfo = down.elems.get_Item(i);
					for (int j = 0; j < this.elementInfos.get_Count(); j++)
					{
						ElementInfo elementInfo2 = this.elementInfos.get_Item(j);
						if (elementInfo.elemId == elementInfo2.elemId)
						{
							if (elementInfo2.elemLv != elementInfo.elemLv)
							{
								KeyValuePair<int, int> keyValuePair = this.ParseElementID(elementInfo.elemId);
								EventDispatcher.Broadcast<int, int>(EventNames.ElementUpgrade, keyValuePair.get_Key(), keyValuePair.get_Value());
								Debuger.Error(string.Concat(new object[]
								{
									"ParseElementID ",
									keyValuePair.get_Key(),
									"  ",
									keyValuePair.get_Value()
								}), new object[0]);
							}
							if (elementInfo2.upgradable != elementInfo.upgradable)
							{
								KeyValuePair<int, int> keyValuePair2 = this.ParseElementID(elementInfo.elemId);
								if (elementInfo.elemLv == 0)
								{
									EventDispatcher.Broadcast<int, int>(EventNames.ElementUnLock, keyValuePair2.get_Key(), keyValuePair2.get_Value());
								}
								Debuger.Error(string.Concat(new object[]
								{
									"ParseElementID ",
									keyValuePair2.get_Key(),
									"  ",
									keyValuePair2.get_Value()
								}), new object[0]);
							}
							elementInfo2.elemLv = elementInfo.elemLv;
							elementInfo2.upgradable = elementInfo.upgradable;
							break;
						}
					}
				}
			}
			else
			{
				Debuger.Error("down == null OnGetElementChangedNty", new object[0]);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetElementUpRes(short state, ElementUpRes down)
	{
		if (state == 0)
		{
			EventDispatcher.Broadcast(EventNames.OnGetElementChangedNty);
			if (down == null)
			{
				Debuger.Error("down == null OnGetElementUpRes", new object[0]);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	public Dictionary<int, bool> CheckElementTypesCanUpdate()
	{
		Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
		dictionary.Add(1, false);
		dictionary.Add(2, false);
		dictionary.Add(3, false);
		dictionary.Add(4, false);
		for (int i = 0; i < this.elementInfos.get_Count(); i++)
		{
			ElementInfo ei = this.elementInfos.get_Item(i);
			if (ei.upgradable)
			{
				KeyValuePair<int, int> kp = this.ParseElementID(ei.elemId);
				if (!dictionary.get_Item(kp.get_Key()))
				{
					YuanSuShuXing yuanSuShuXing = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == kp.get_Key() && e.subType == kp.get_Value() && e.lv == ei.elemLv + 1);
					if (yuanSuShuXing != null)
					{
						if (BackpackManager.Instance.OnGetGoodCount(yuanSuShuXing.itemId.get_Item(0)) >= (long)yuanSuShuXing.itemNum.get_Item(0))
						{
							dictionary.set_Item(kp.get_Key(), true);
						}
					}
				}
			}
		}
		return dictionary;
	}

	public bool CheckAllCanUpdate()
	{
		for (int i = 0; i < this.elementInfos.get_Count(); i++)
		{
			ElementInfo ei = this.elementInfos.get_Item(i);
			if (ei.upgradable)
			{
				KeyValuePair<int, int> kp = this.ParseElementID(ei.elemId);
				YuanSuShuXing yuanSuShuXing = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == kp.get_Key() && e.subType == kp.get_Value() && e.lv == ei.elemLv + 1);
				if (yuanSuShuXing != null)
				{
					if (BackpackManager.Instance.OnGetGoodCount(yuanSuShuXing.itemId.get_Item(0)) >= (long)yuanSuShuXing.itemNum.get_Item(0))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public int MakeElementID(int elementType, int elementSubType)
	{
		return elementType << 16 | elementSubType;
	}

	public KeyValuePair<int, int> ParseElementID(int elementid)
	{
		int num = (int)(((long)elementid & (long)((ulong)-65536)) >> 16);
		int num2 = elementid & 65535;
		KeyValuePair<int, int> result = new KeyValuePair<int, int>(num, num2);
		return result;
	}
}
