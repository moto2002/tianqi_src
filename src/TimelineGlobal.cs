using GameData;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using XEngineActor;

public static class TimelineGlobal
{
	public static int timelineId;

	public static int timelineSection;

	public static Action timelineCallback;

	public static Animator animator;

	private static string prefabPath = "Timeline/timeline";

	public static List<ActorModel> npcs = new List<ActorModel>();

	private static GameObject timeline;

	public static void Init(int timelineId, int timelineSection, Action timelineCallback)
	{
		Debug.Log(string.Concat(new object[]
		{
			"timelineId=",
			timelineId,
			" timelineSection",
			timelineSection
		}));
		TimelineGlobal.timelineId = timelineId;
		TimelineGlobal.timelineSection = timelineSection;
		TimelineGlobal.timelineCallback = timelineCallback;
		TimelineGlobal.ClearNpcs();
		Object.Destroy(TimelineGlobal.timeline);
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(TimelineGlobal.prefabPath, typeof(Object));
		TimelineGlobal.timeline = (Object.Instantiate(@object) as GameObject);
		TimelineGlobal.timeline.AddComponent<TimelineHierarchy>();
		TimelineGlobal.timeline.AddComponent<AudioPlayer>();
	}

	public static void DelNpc(int npcId)
	{
		for (int i = 0; i < TimelineGlobal.npcs.get_Count(); i++)
		{
			if (TimelineGlobal.npcs.get_Item(i).resGUID == npcId)
			{
				Object.Destroy(TimelineGlobal.npcs.get_Item(i));
				break;
			}
		}
	}

	public static ActorModel GetNpc(int npcId)
	{
		for (int i = 0; i < TimelineGlobal.npcs.get_Count(); i++)
		{
			if (TimelineGlobal.npcs.get_Item(i).resGUID == npcId)
			{
				return TimelineGlobal.npcs.get_Item(i);
			}
		}
		ActorModel actorModel = ModelPool.Instance.Get(npcId);
		actorModel.ModelType = ActorModelType.CG;
		actorModel.ShowShadow(true, npcId);
		TimelineGlobal.npcs.Add(actorModel);
		return actorModel;
	}

	public static void ClearNpcs()
	{
		for (int i = 0; i < TimelineGlobal.npcs.get_Count(); i++)
		{
			Object.Destroy(TimelineGlobal.npcs.get_Item(i));
		}
		TimelineGlobal.npcs.Clear();
	}

	public static void InitPrefab()
	{
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(TimelineGlobal.prefabPath, typeof(Object));
		TimelineGlobal.timeline = (Object.Instantiate(@object) as GameObject);
		TimelineGlobal.animator = TimelineGlobal.timeline.GetComponent<Animator>();
		TimelineGlobal.animator.set_enabled(true);
	}

	private static void Play()
	{
		TimelineGlobal.animator.Play("t" + TimelineGlobal.timelineId.ToString());
	}

	public static void GetPartComicIds(int frameLength, List<int> frameRowIds, ref List<int> npcIds)
	{
		for (int i = 0; i < frameRowIds.get_Count(); i++)
		{
			ShiJianShiJianBiao shiJianShiJianBiao = DataReader<ShiJianShiJianBiao>.Get(frameRowIds.get_Item(i));
			if (shiJianShiJianBiao.eventType == 5)
			{
				for (int j = 0; j < frameLength; j++)
				{
					Type typeFromHandle = typeof(ShiJianShiJianBiao);
					PropertyInfo property = typeFromHandle.GetProperty("time" + j);
					int num = (int)property.GetValue(shiJianShiJianBiao, null);
					if (num != 0)
					{
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						npcIds.Add(shiJianCanShuBiao.anime);
					}
				}
			}
		}
	}

	public static void GetPartNpcModelIds(int frameLength, List<int> frameRowIds, ref List<int> npcIds)
	{
		for (int i = 0; i < frameRowIds.get_Count(); i++)
		{
			ShiJianShiJianBiao shiJianShiJianBiao = DataReader<ShiJianShiJianBiao>.Get(frameRowIds.get_Item(i));
			if (shiJianShiJianBiao.eventType == 2)
			{
				for (int j = 0; j < frameLength; j++)
				{
					Type typeFromHandle = typeof(ShiJianShiJianBiao);
					PropertyInfo property = typeFromHandle.GetProperty("time" + j);
					int num = (int)property.GetValue(shiJianShiJianBiao, null);
					if (num != 0)
					{
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						if (shiJianCanShuBiao.modelId != 1)
						{
							npcIds.Add(shiJianCanShuBiao.modelId);
						}
					}
				}
			}
		}
	}

	public static List<int> GetComicIds(int timelineId)
	{
		Debug.LogError("GetNpcModelIds timelineId=" + timelineId);
		List<int> list = new List<int>();
		DongHuaShiJianBiao dongHuaShiJianBiao = DataReader<DongHuaShiJianBiao>.Get(timelineId);
		TimelineGlobal.GetPartComicIds(dongHuaShiJianBiao.beginEventTime, dongHuaShiJianBiao.beginEventId, ref list);
		TimelineGlobal.GetPartComicIds(dongHuaShiJianBiao.endEventTime, dongHuaShiJianBiao.endEventId, ref list);
		using (List<int>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				Debug.LogError("aComicId=" + current);
			}
		}
		return list;
	}

	public static List<int> GetNpcModelIds(int timelineId)
	{
		Debug.LogError("GetNpcModelIds timelineId=" + timelineId);
		List<int> list = new List<int>();
		DongHuaShiJianBiao dongHuaShiJianBiao = DataReader<DongHuaShiJianBiao>.Get(timelineId);
		TimelineGlobal.GetPartNpcModelIds(dongHuaShiJianBiao.beginEventTime, dongHuaShiJianBiao.beginEventId, ref list);
		TimelineGlobal.GetPartNpcModelIds(dongHuaShiJianBiao.endEventTime, dongHuaShiJianBiao.endEventId, ref list);
		using (List<int>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				Debug.LogError("aNpcId=" + current);
			}
		}
		return list;
	}

	public static List<int> GetNpcModelIds(string timelineId)
	{
		List<int> list = new List<int>();
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(TimelineGlobal.prefabPath, typeof(Object));
		GameObject gameObject = Object.Instantiate(@object) as GameObject;
		Animator component = gameObject.GetComponent<Animator>();
		int num = component.get_runtimeAnimatorController().get_animationClips().Length;
		Debug.LogError("length=" + num);
		AnimationClip[] animationClips = component.get_runtimeAnimatorController().get_animationClips();
		AnimationClip animationClip = null;
		for (int i = 0; i < animationClips.Length; i++)
		{
			if (animationClips[i].get_name() == timelineId)
			{
				animationClip = animationClips[i];
				break;
			}
		}
		if (animationClip != null)
		{
			for (int j = 0; j < animationClip.get_events().Length; j++)
			{
				if (!(animationClip.get_events()[j].get_stringParameter() == string.Empty))
				{
					int key = int.Parse(animationClip.get_events()[j].get_stringParameter());
					DongHuaBiao dongHuaBiao = DataReader<DongHuaBiao>.Get(key);
					int npcId = dongHuaBiao.npcId;
					if (npcId > 1)
					{
						list.Add(npcId);
					}
				}
			}
		}
		return list;
	}

	public static void Pause()
	{
		TimelineGlobal.animator.set_speed(0f);
		TimelineGlobal.animator.set_enabled(false);
	}

	public static void Resume()
	{
		TimelineGlobal.animator.set_speed(1f);
		TimelineGlobal.animator.set_enabled(true);
	}

	public static bool IsPause()
	{
		return TimelineGlobal.animator.get_speed() == 0f;
	}

	public static void SetVisible(bool visible)
	{
		TimelineGlobal.SetRoleVisible(visible);
		TimelineGlobal.SetPetVisible(visible);
		UIManagerControl.Instance.FakeHideAllUI(!visible, 7);
	}

	private static void SetRoleVisible(bool visible)
	{
		EntityWorld.Instance.EntSelf.ShowSelf(visible);
	}

	private static void SetPetVisible(bool visible)
	{
		Dictionary<long, EntityPet> entCurPet = EntityWorld.Instance.EntCurPet;
		using (Dictionary<long, EntityPet>.Enumerator enumerator = entCurPet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, EntityPet> current = enumerator.get_Current();
				current.get_Value().IsVisible = visible;
			}
		}
	}

	private static void SetUiVisible(bool visible)
	{
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("BattleUI");
		uIIfExist.Show(visible);
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", !visible);
	}
}
