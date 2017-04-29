using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogManager : BaseSubSystemManager
{
	public int wave;

	private Dictionary<int, Dictionary<int, SortedList<int, JuQingZhiYinBuZou>>> m_map_type_to_list = new Dictionary<int, Dictionary<int, SortedList<int, JuQingZhiYinBuZou>>>();

	private Dictionary<int, int> m_listMaxTriggerCount = new Dictionary<int, int>();

	private float m_lastTime;

	private Dictionary<Transform, List<int>> MonsterDialogQueue = new Dictionary<Transform, List<int>>();

	private Dictionary<Transform, int> MonsterHeightQueue = new Dictionary<Transform, int>();

	private Dictionary<Transform, uint> MonsterTimerQueue = new Dictionary<Transform, uint>();

	private List<string> TipsContentQueue = new List<string>();

	private List<uint> TipsTimeQueue = new List<uint>();

	public GameObject Hint;

	public Text TipsText;

	private uint hintTimer = 4294967295u;

	private List<int> HeadDialogQueue = new List<int>();

	public Image Icon;

	public TextCustom text;

	public TextCustom MonsterName;

	protected static BattleDialogManager instance;

	public static BattleDialogManager Instance
	{
		get
		{
			if (BattleDialogManager.instance == null)
			{
				BattleDialogManager.instance = new BattleDialogManager();
			}
			return BattleDialogManager.instance;
		}
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
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
		EventDispatcher.AddListener<int, EntityMonster, string>("BattleDialogTrigger", new Callback<int, EntityMonster, string>(this.OnDialogTrigger));
		EventDispatcher.AddListener<float>("BattleDialogInstanceEscapeTime", new Callback<float>(this.OnInstanceEscapeTime));
	}

	private void LoadSceneEnd(int sceneId)
	{
		this.ResetSelf();
		if (InstanceManager.CurrentInstanceDataID > 0)
		{
			this.InitData(InstanceManager.CurrentInstanceDataID);
		}
	}

	private void InitData(int InstanceId)
	{
		List<JuQingZhiYinBuZou> list = DataReader<JuQingZhiYinBuZou>.DataList.FindAll((JuQingZhiYinBuZou a) => a.off != 0 && (a.counterpart.Contains(InstanceId) || a.counterpart.Contains(0)));
		for (int i = 0; i < list.get_Count(); i++)
		{
			JuQingZhiYinBuZou juQingZhiYinBuZou = list.get_Item(i);
			Dictionary<int, SortedList<int, JuQingZhiYinBuZou>> dictionary;
			if (!this.m_map_type_to_list.ContainsKey(juQingZhiYinBuZou.triggerType))
			{
				dictionary = new Dictionary<int, SortedList<int, JuQingZhiYinBuZou>>();
				this.m_map_type_to_list.Add(juQingZhiYinBuZou.triggerType, dictionary);
			}
			dictionary = this.m_map_type_to_list.get_Item(juQingZhiYinBuZou.triggerType);
			int num = 0;
			if (juQingZhiYinBuZou.triggerType != 9 && juQingZhiYinBuZou.triggerType != 10 && juQingZhiYinBuZou.triggerType != 11)
			{
				num = (int)float.Parse(juQingZhiYinBuZou.args.get_Item(0));
			}
			SortedList<int, JuQingZhiYinBuZou> sortedList;
			if (!dictionary.ContainsKey(num))
			{
				sortedList = new SortedList<int, JuQingZhiYinBuZou>();
				dictionary.Add(num, sortedList);
			}
			sortedList = dictionary.get_Item(num);
			sortedList.Add(juQingZhiYinBuZou.id, juQingZhiYinBuZou);
		}
		this.m_lastTime = 0f;
	}

	private void ResetSelf()
	{
		this.m_map_type_to_list.Clear();
		this.m_listMaxTriggerCount.Clear();
		this.MonsterDialogQueue.Clear();
		this.MonsterHeightQueue.Clear();
		this.MonsterTimerQueue.Clear();
		this.HeadDialogQueue.Clear();
		this.TipsContentQueue.Clear();
		this.TipsTimeQueue.Clear();
	}

	private void OnInstanceEscapeTime(float time)
	{
		if (time - this.m_lastTime > 1f)
		{
			this.m_lastTime = time;
			this.OnDialogTrigger(9, null, (int)this.m_lastTime + string.Empty);
		}
	}

	private void OnDialogTrigger(int type, EntityMonster entityMonster, string arg)
	{
		if (type == 10 && InstanceManager.CurrentInstanceData != null && InstanceManager.CurrentInstanceData.waveShow > 0)
		{
			this.wave = int.Parse(arg);
			this.AddTipsDialog(string.Format(GameDataUtils.GetChineseContent(InstanceManager.CurrentInstanceData.waveShow, false), arg), 2000u);
		}
		if (!this.m_map_type_to_list.ContainsKey(type))
		{
			return;
		}
		int num = 0;
		if (type != 9 && type != 10 && type != 11)
		{
			num = entityMonster.TypeID;
		}
		Dictionary<int, SortedList<int, JuQingZhiYinBuZou>> dictionary = this.m_map_type_to_list.get_Item(type);
		if (!dictionary.ContainsKey(num))
		{
			return;
		}
		SortedList<int, JuQingZhiYinBuZou> sortedList = dictionary.get_Item(num);
		for (int i = 0; i < sortedList.get_Values().get_Count(); i++)
		{
			JuQingZhiYinBuZou juQingZhiYinBuZou = sortedList.get_Values().get_Item(i);
			if (this.IsTriggerSuccess(type, juQingZhiYinBuZou, arg))
			{
				int num2 = 0;
				if (this.m_listMaxTriggerCount.ContainsKey(juQingZhiYinBuZou.id))
				{
					num2 = this.m_listMaxTriggerCount.get_Item(juQingZhiYinBuZou.id);
				}
				if (num2 < juQingZhiYinBuZou.amount)
				{
					num2++;
					this.m_listMaxTriggerCount.set_Item(juQingZhiYinBuZou.id, num2);
					int num3 = Random.Range(0, 10000);
					if (num3 < juQingZhiYinBuZou.probability)
					{
						if (juQingZhiYinBuZou.type == 1)
						{
							this.AddHeadDialog(juQingZhiYinBuZou.id);
						}
						else if (juQingZhiYinBuZou.type == 2)
						{
							int typeID = juQingZhiYinBuZou.typeArgs.get_Item(0);
							EntityMonster anEntityByTypeID = EntityWorld.Instance.GetAnEntityByTypeID<EntityMonster>(typeID);
							this.AddBubbleDialog(anEntityByTypeID, juQingZhiYinBuZou.id);
						}
						else if (juQingZhiYinBuZou.type == 3)
						{
							this.AddTipsDialog(GameDataUtils.GetChineseContent(juQingZhiYinBuZou.typeArgs.get_Item(0), false), (uint)juQingZhiYinBuZou.delayTime);
						}
					}
				}
			}
		}
	}

	private bool IsTriggerSuccess(int type, JuQingZhiYinBuZou dataBZ, string arg)
	{
		bool result = false;
		switch (type)
		{
		case 1:
			result = GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(1)).Equals(arg);
			break;
		case 2:
			result = GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(1)).Equals(arg);
			break;
		case 3:
			result = GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(1)).Equals(arg);
			break;
		case 4:
			result = GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(1)).Equals(arg);
			break;
		case 5:
			result = true;
			break;
		case 6:
			result = true;
			break;
		case 7:
			result = true;
			break;
		case 8:
			result = (float.Parse(arg) <= float.Parse(GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(1))) * 0.0001f);
			break;
		case 9:
			result = ((float)int.Parse(arg) > float.Parse(GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(0))));
			break;
		case 10:
			result = arg.Equals(GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(0)));
			break;
		case 11:
			result = arg.Equals(GameDataUtils.SplitString4Dot0(dataBZ.args.get_Item(0)));
			break;
		}
		return result;
	}

	private void AddHeadDialog(int id)
	{
		this.HeadDialogQueue.Add(id);
		if (this.HeadDialogQueue.get_Count() == 1)
		{
			this.ShowHeadDialog();
		}
	}

	private void ShowHeadDialog()
	{
	}

	private void HideBossHeadDialog()
	{
		if (this.HeadDialogQueue.get_Count() > 0)
		{
			this.HeadDialogQueue.RemoveAt(0);
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("BattleUI");
			if (uIIfExist != null)
			{
				((BattleUI)uIIfExist).StartCoroutine(uIIfExist.FindTransform("DialogHeaderIcon").GetComponent<RectTransform>().MoveTo(new Vector3(-847.5f, 153f, 0f), 0.3f, EaseType.BackIn));
			}
			if (this.HeadDialogQueue.get_Count() > 0)
			{
				this.ShowHeadDialog();
			}
		}
	}

	public void AddBubbleDialog(EntityMonster entity, int id)
	{
		if (entity == null)
		{
			return;
		}
		this.HideBubbleDialog(entity.Actor.FixTransform, entity.ID);
		List<int> list;
		if (!this.MonsterDialogQueue.ContainsKey(entity.Actor.FixTransform))
		{
			list = new List<int>();
			this.MonsterDialogQueue.Add(entity.Actor.FixTransform, list);
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(entity.FixModelID);
			this.MonsterHeightQueue.Add(entity.Actor.FixTransform, avatarModel.height_HP);
		}
		list = this.MonsterDialogQueue.get_Item(entity.Actor.FixTransform);
		list.Add(id);
		if (list.get_Count() == 1)
		{
			this.ShowBubbleDialog(entity.Actor.FixTransform, entity.ID);
		}
	}

	private void ShowBubbleDialog(Transform targetTransform, long uuid)
	{
		if (this.MonsterDialogQueue.ContainsKey(targetTransform))
		{
			int key = this.MonsterDialogQueue.get_Item(targetTransform).get_Item(0);
			JuQingZhiYinBuZou dataBZ = DataReader<JuQingZhiYinBuZou>.Get(key);
			if (dataBZ.typeArgs.get_Count() < 2)
			{
				Debug.LogError("dataBZ参数错误, id = " + dataBZ.id);
				return;
			}
			if (dataBZ.delayTime > 0)
			{
				TimerHeap.AddTimer((uint)dataBZ.delayTime, 0, delegate
				{
					this.DoAddBubbleDialog(targetTransform, dataBZ, uuid);
				});
			}
			else
			{
				this.DoAddBubbleDialog(targetTransform, dataBZ, uuid);
			}
		}
	}

	private void DoAddBubbleDialog(Transform targetTransform, JuQingZhiYinBuZou dataBZ, long uuid)
	{
		if (targetTransform == null)
		{
			return;
		}
		BubbleDialogueManager.Instance.BubbleDialogue(targetTransform, (float)this.MonsterHeightQueue.get_Item(targetTransform), uuid, GameDataUtils.GetChineseContent(dataBZ.typeArgs.get_Item(1), false));
		uint time = (uint)dataBZ.time;
		this.MonsterTimerQueue.set_Item(targetTransform, TimerHeap.AddTimer(time, 0, delegate
		{
			this.HideBubbleDialog(targetTransform, uuid);
		}));
	}

	private void HideBubbleDialog(Transform targetTransform, long id)
	{
		if (targetTransform == null)
		{
			return;
		}
		if (this.MonsterDialogQueue.ContainsKey(targetTransform))
		{
			BubbleDialogueManager.Instance.RemoveBubbleDialogue(id, targetTransform);
			List<int> list = this.MonsterDialogQueue.get_Item(targetTransform);
			if (list.get_Count() > 0)
			{
				list.RemoveAt(0);
			}
			if (this.MonsterTimerQueue.ContainsKey(targetTransform))
			{
				TimerHeap.DelTimer(this.MonsterTimerQueue.get_Item(targetTransform));
				this.MonsterTimerQueue.Remove(targetTransform);
			}
			if (list.get_Count() > 0)
			{
				this.ShowBubbleDialog(targetTransform, id);
			}
		}
	}

	private void AddTipsDialog(string text, uint delayTime)
	{
		if (delayTime > 0u)
		{
			TimerHeap.AddTimer(delayTime, 0, delegate
			{
				UIManagerControl.Instance.ShowBattleToastText(text, 2f);
			});
		}
		else
		{
			UIManagerControl.Instance.ShowBattleToastText(text, 2f);
		}
	}

	public void ShowTips()
	{
		UIUtils.animToFadeInEnd(this.Hint);
		this.TipsText.set_text(this.TipsContentQueue.get_Item(0));
		this.hintTimer = TimerHeap.AddTimer(this.TipsTimeQueue.get_Item(0), 0, delegate
		{
			this.HideTips();
		});
	}

	public void HideTips()
	{
		TimerHeap.DelTimer(this.hintTimer);
		UIUtils.animToFadeOut(this.Hint);
		this.TipsContentQueue.RemoveAt(0);
		this.TipsTimeQueue.RemoveAt(0);
		if (this.TipsContentQueue.get_Count() > 0)
		{
			this.ShowTips();
		}
	}
}
