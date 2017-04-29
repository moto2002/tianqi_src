using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorVisibleManager
{
	private static ActorVisibleManager instance;

	private Dictionary<long, ActorVisibleCtrl> m_avclist = new Dictionary<long, ActorVisibleCtrl>();

	private Dictionary<long, bool> m_actorshowlist = new Dictionary<long, bool>();

	private int StatisticsShowPeople;

	public static ActorVisibleManager Instance
	{
		get
		{
			if (ActorVisibleManager.instance == null)
			{
				ActorVisibleManager.instance = new ActorVisibleManager();
			}
			return ActorVisibleManager.instance;
		}
	}

	public void Init()
	{
	}

	public void Add(long uuid, Transform actorTarget, int actorType, long ownerId = 0L)
	{
		ActorVisibleCtrl actorVisibleCtrl = actorTarget.get_gameObject().AddUniqueComponent<ActorVisibleCtrl>();
		this.m_avclist.set_Item(uuid, actorVisibleCtrl);
		actorVisibleCtrl.AwakeSelf(actorType, uuid, ownerId, actorTarget.get_gameObject().GetInterface<IActorVisible>());
		actorVisibleCtrl.set_enabled(true);
	}

	public void Remove(Transform actorTarget)
	{
		ActorVisibleCtrl component = actorTarget.GetComponent<ActorVisibleCtrl>();
		if (component != null)
		{
			component.set_enabled(false);
			component.RemoveAVC();
			this.m_avclist.Remove(component.uuid);
		}
	}

	public void RefreshRenderers(long uuid)
	{
		ActorVisibleCtrl actorVisibleCtrl;
		if (this.m_avclist.TryGetValue(uuid, ref actorVisibleCtrl))
		{
			actorVisibleCtrl.RefreshRenderers();
		}
	}

	public bool IsShow(long uuid)
	{
		ActorVisibleCtrl actorVisibleCtrl;
		return EntityWorld.Instance.EntSelf != null && (EntityWorld.Instance.EntSelf.ID == uuid || !this.m_avclist.TryGetValue(uuid, ref actorVisibleCtrl) || actorVisibleCtrl.IsShow());
	}

	public bool SetShow(long uuid, bool isDistanceShow, bool ignorePeopleShow)
	{
		bool flag;
		if (!isDistanceShow)
		{
			flag = false;
			if (this.m_actorshowlist.ContainsKey(uuid) && this.m_actorshowlist.get_Item(uuid))
			{
				this.StatisticsShowPeople--;
			}
		}
		else if (this.m_actorshowlist.ContainsKey(uuid) && this.m_actorshowlist.get_Item(uuid))
		{
			flag = true;
		}
		else if (this.IsPeopleShowMaximum() && !ignorePeopleShow)
		{
			flag = false;
		}
		else
		{
			flag = true;
			this.StatisticsShowPeople++;
		}
		this.m_actorshowlist.set_Item(uuid, flag);
		return flag;
	}

	public void ClearAVC()
	{
		this.m_actorshowlist.Clear();
		this.StatisticsShowPeople = 0;
	}

	private bool IsPeopleShowMaximum()
	{
		return this.StatisticsShowPeople >= Mathf.Min(GameLevelManager.GameLevelVariable.PeopleNum, 100);
	}

	public void Print()
	{
		UIManagerControl.Instance.ShowToastText("当前显示人物数量=" + this.StatisticsShowPeople);
		int num = 0;
		using (Dictionary<long, bool>.Enumerator enumerator = this.m_actorshowlist.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, bool> current = enumerator.get_Current();
				if (current.get_Value())
				{
					Debug.LogError(string.Concat(new object[]
					{
						"people index = ",
						num,
						", uuid = ",
						current.get_Key()
					}));
					num++;
				}
			}
		}
	}
}
