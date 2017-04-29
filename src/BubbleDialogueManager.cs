using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDialogueManager
{
	public class BubbleType
	{
		public const int NPCBorn = 1;

		public const int MonsterRefreshTime = 2;

		public const int TargetMonsterBorn = 3;

		public const int TargetMonsterSkill = 4;
	}

	private static BubbleDialogueManager instance;

	private List<BubbleDialogueUnit> BubbleDialogues = new List<BubbleDialogueUnit>();

	private static UIPool BubbleDialoguePool;

	public static Transform Pool2BubbleDialogue;

	public static BubbleDialogueManager Instance
	{
		get
		{
			if (BubbleDialogueManager.instance == null)
			{
				BubbleDialogueManager.instance = new BubbleDialogueManager();
			}
			return BubbleDialogueManager.instance;
		}
	}

	private BubbleDialogueManager()
	{
		BubbleDialogueManager.CreatePools();
	}

	private static void CreatePools()
	{
		Transform transform = new GameObject("Pool2BubbleDialogue").get_transform();
		transform.set_parent(UINodesManager.NoEventsUIRoot);
		transform.SetAsLastSibling();
		transform.get_gameObject().set_layer(LayerSystem.NameToLayer("UI"));
		BubbleDialogueManager.Pool2BubbleDialogue = transform;
		UGUITools.ResetTransform(BubbleDialogueManager.Pool2BubbleDialogue);
		BubbleDialogueManager.BubbleDialoguePool = new UIPool("BubbleDialogueUnit", BubbleDialogueManager.Pool2BubbleDialogue, false);
		TimerHeap.AddTimer(10000u, 2500, delegate
		{
			BillboardManager.ResortOfZ(BubbleDialogueManager.BubbleDialoguePool);
		});
	}

	public void Init()
	{
	}

	public bool AddBubbleDialogueLimit(Transform actorRoot, float height, long uuid, int dialogueMonster)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return false;
		}
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return false;
		}
		this.RemoveBubbleDialogue(uuid, actorRoot);
		if (dialogueMonster > 0 && this.GetCountInShow() >= dialogueMonster)
		{
			return false;
		}
		this.AddBubbleDialogue(actorRoot, height, uuid);
		return true;
	}

	public void BubbleDialogueTriggerNPCBorn(MonsterRefresh dataMR, long uuid)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return;
		}
		this.BubbleDialogueByuuid(uuid, dataMR, 1);
	}

	public void BubbleDialogueTrigger(int bubbleType, int arg = 0)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		Dictionary<long, MonsterRefresh> currentNPCList = LocalInstanceHandler.Instance.GetCurrentNPCList();
		switch (bubbleType)
		{
		case 2:
			arg++;
			using (Dictionary<long, MonsterRefresh>.Enumerator enumerator = currentNPCList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<long, MonsterRefresh> current = enumerator.get_Current();
					if (current.get_Value() != null && current.get_Value().refreshBatch.Contains(arg))
					{
						this.BubbleDialogueByuuid(current.get_Key(), current.get_Value(), bubbleType * 10 + arg);
					}
				}
			}
			break;
		case 3:
		case 4:
			using (Dictionary<long, MonsterRefresh>.Enumerator enumerator2 = currentNPCList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<long, MonsterRefresh> current2 = enumerator2.get_Current();
					if (current2.get_Value() != null)
					{
						this.BubbleDialogueByuuid(current2.get_Key(), current2.get_Value(), bubbleType);
					}
				}
			}
			break;
		}
	}

	public void BubbleDialogueByuuid(long uuid, MonsterRefresh dataMR, int bubbleType2Key)
	{
		if (dataMR == null)
		{
			return;
		}
		if (uuid <= 0L)
		{
			return;
		}
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return;
		}
		EntityMonster entity = EntityWorld.Instance.GetEntity<EntityMonster>(uuid);
		if (entity != null)
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(entity.FixModelID);
			if (avatarModel != null)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				for (int i = 0; i < dataMR.dialogueNpc.get_Count(); i++)
				{
					if (dataMR.dialogueNpc.get_Item(i).key == bubbleType2Key)
					{
						string[] array = dataMR.dialogueNpc.get_Item(i).value.Split(",".ToCharArray());
						if (array != null)
						{
							for (int j = 0; j < array.Length; j++)
							{
								list.Add(Convert.ToInt32(array[j]));
							}
						}
						break;
					}
				}
				for (int k = 0; k < dataMR.dialogueLastTime.get_Count(); k++)
				{
					if (dataMR.dialogueLastTime.get_Item(k).key == bubbleType2Key)
					{
						string[] array2 = dataMR.dialogueLastTime.get_Item(k).value.Split(",".ToCharArray());
						if (array2 != null)
						{
							for (int l = 0; l < array2.Length; l++)
							{
								list2.Add(Convert.ToInt32(array2[l]));
							}
						}
						break;
					}
				}
				if (list.get_Count() > 0 && list2.get_Count() > 0)
				{
					this.AddBubbleDialogue(entity.Actor.FixTransform, (float)avatarModel.height_HP, uuid);
					this.SetContentsBySequence(uuid, list, list2, 0);
				}
			}
		}
	}

	public void AddBubbleDialogue(Transform actorRoot, float height, long uuid)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		this.RemoveBubbleDialogue(uuid, actorRoot);
		BubbleDialogueUnit bubbleDialogueUnit = this.Create2BubbleDialogue(actorRoot, height, uuid);
		bubbleDialogueUnit.SetUUID(uuid);
		bubbleDialogueUnit.SetTargetPositionNode(BillboardManager.AddHeadInfoPosition(actorRoot, height));
	}

	public void RemoveBubbleDialogue(long uuid, Transform actorRoot)
	{
		for (int i = 0; i < this.BubbleDialogues.get_Count(); i++)
		{
			BubbleDialogueUnit bubbleDialogueUnit = this.BubbleDialogues.get_Item(i);
			if (bubbleDialogueUnit.uuid == uuid && bubbleDialogueUnit.get_transform() == actorRoot)
			{
				if (bubbleDialogueUnit != null && bubbleDialogueUnit.GetBillboardTransform() != null)
				{
					BubbleDialogueManager.BubbleDialoguePool.ReUse(bubbleDialogueUnit.GetBillboardTransform().get_gameObject());
					bubbleDialogueUnit.ResetAll();
				}
				this.BubbleDialogues.RemoveAt(i);
				return;
			}
		}
	}

	public void SetContentsBySequence(long uuid, List<int> contentIds, List<int> dialogueTimes, int index = 0)
	{
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return;
		}
		if (contentIds.get_Count() > index && dialogueTimes.get_Count() > index)
		{
			BubbleDialogueUnit gridUI = this.GetBubbleDialogueUnit(uuid);
			if (gridUI != null)
			{
				gridUI.SetContent(GameDataUtils.GetChineseContent(contentIds.get_Item(index), false));
				uint start = (uint)Mathf.Max(3000, dialogueTimes.get_Item(index) * 1000);
				index++;
				TimerHeap.AddTimer(start, 0, delegate
				{
					if (contentIds.get_Count() <= index)
					{
						if (gridUI != null)
						{
							Transform transform = gridUI.get_transform();
							this.RemoveBubbleDialogue(uuid, transform);
						}
					}
					else
					{
						this.SetContentsBySequence(uuid, contentIds, dialogueTimes, index);
					}
				});
			}
		}
	}

	public void SetContentsByRandom(long uuid, List<int> contentIds, int dialogueTimes)
	{
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return;
		}
		if (contentIds == null || contentIds.get_Count() == 0)
		{
			return;
		}
		int num = Random.Range(0, contentIds.get_Count());
		string chineseContent = GameDataUtils.GetChineseContent(contentIds.get_Item(num), false);
		BubbleDialogueUnit gridUI = this.GetBubbleDialogueUnit(uuid);
		if (gridUI != null)
		{
			gridUI.SetContent(chineseContent);
			uint start = (uint)Mathf.Max(3000, dialogueTimes * 1000);
			TimerHeap.AddTimer(start, 0, delegate
			{
				if (gridUI != null)
				{
					Transform transform = gridUI.get_transform();
					this.RemoveBubbleDialogue(uuid, transform);
				}
			});
		}
	}

	public void SetContentsByShopNpc(long uuid, int shopId, int dialogueTimes = 9999999)
	{
		if (uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(uuid, BillboardManager.BillboardInfoOffOption.BubbleDialogue))
		{
			return;
		}
		BubbleDialogueUnit gridUI = this.GetBubbleDialogueUnit(uuid);
		if (gridUI != null)
		{
			int nextUpdateTime = 0;
			string str = GameDataUtils.GetChineseContent(505602, false);
			bool flag = false;
			List<NpcShopSt> transactionNpcData = TransactionNPCManager.Instance.TransactionNpcData;
			if (transactionNpcData != null)
			{
				for (int i = 0; i < transactionNpcData.get_Count(); i++)
				{
					if (transactionNpcData.get_Item(i).shopId == shopId)
					{
						nextUpdateTime = transactionNpcData.get_Item(i).nextUpdateTime;
						flag = transactionNpcData.get_Item(i).sellOut;
						break;
					}
				}
			}
			if (flag)
			{
				TimerHeap.AddTimer(0u, 1000, delegate
				{
					if (gridUI != null)
					{
						int num = nextUpdateTime - TimeManager.Instance.PreciseServerSecond;
						if (num > 0)
						{
							string text = string.Empty;
							text = TimeConverter.GetTime(num, TimeFormat.HHMMSS);
							gridUI.SetContent(string.Format(str, text));
						}
						else
						{
							List<NPCShangChengBiao> dataList2 = DataReader<NPCShangChengBiao>.DataList;
							for (int k = 0; k < dataList2.get_Count(); k++)
							{
								if (dataList2.get_Item(k).shopId == shopId)
								{
									string chineseContent2 = GameDataUtils.GetChineseContent(dataList2.get_Item(k).shopBewrite, false);
									gridUI.SetContent(chineseContent2);
									break;
								}
							}
						}
					}
				});
			}
			else
			{
				List<NPCShangChengBiao> dataList = DataReader<NPCShangChengBiao>.DataList;
				for (int j = 0; j < dataList.get_Count(); j++)
				{
					if (dataList.get_Item(j).shopId == shopId)
					{
						string chineseContent = GameDataUtils.GetChineseContent(dataList.get_Item(j).shopBewrite, false);
						gridUI.SetContent(chineseContent);
						break;
					}
				}
			}
			uint start = (uint)Mathf.Max(3000, dialogueTimes * 1000);
			TimerHeap.AddTimer(start, 0, delegate
			{
				if (gridUI != null)
				{
					Transform transform = gridUI.get_transform();
					this.RemoveBubbleDialogue(uuid, transform);
				}
			});
		}
	}

	public void BubbleDialogue(Transform actorRoot, float height, long uuid, string content)
	{
		this.AddBubbleDialogue(actorRoot, height, uuid);
		BubbleDialogueUnit bubbleDialogueUnit = this.GetBubbleDialogueUnit(uuid);
		if (bubbleDialogueUnit != null)
		{
			bubbleDialogueUnit.SetContent(content);
		}
	}

	private BubbleDialogueUnit Create2BubbleDialogue(Transform parent, float height, long uuid)
	{
		GameObject gameObject = BubbleDialogueManager.BubbleDialoguePool.Get(string.Empty);
		gameObject.set_name(uuid.ToString());
		BubbleDialogueUnit bubbleDialogueUnit = parent.get_gameObject().AddMissingComponent<BubbleDialogueUnit>();
		bubbleDialogueUnit.AwakeSelf(gameObject.get_transform());
		this.BubbleDialogues.Add(bubbleDialogueUnit);
		bubbleDialogueUnit.set_enabled(true);
		return bubbleDialogueUnit;
	}

	private BubbleDialogueUnit GetBubbleDialogueUnit(long uuid)
	{
		for (int i = 0; i < this.BubbleDialogues.get_Count(); i++)
		{
			BubbleDialogueUnit bubbleDialogueUnit = this.BubbleDialogues.get_Item(i);
			if (bubbleDialogueUnit.uuid == uuid)
			{
				return bubbleDialogueUnit;
			}
		}
		return null;
	}

	private int GetCountInShow()
	{
		return BubbleDialogueManager.BubbleDialoguePool.m_useds.get_Count();
	}

	public bool IsAVCOn(long uuid)
	{
		return ActorVisibleManager.Instance.IsShow(uuid);
	}
}
