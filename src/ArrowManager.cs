using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class ArrowManager
{
	private static ArrowManager instance;

	private List<EnemyInScreenUnit> EnemyArrows = new List<EnemyInScreenUnit>();

	private static UIPool EnemyArrowPool;

	public static Transform Pool2EnemyArrow;

	public static ArrowManager Instance
	{
		get
		{
			if (ArrowManager.instance == null)
			{
				ArrowManager.instance = new ArrowManager();
			}
			return ArrowManager.instance;
		}
	}

	private ArrowManager()
	{
		ArrowManager.CreatePools();
	}

	private static void CreatePools()
	{
		Transform transform = new GameObject("Pool2EnemyArrow").get_transform();
		transform.set_parent(UINodesManager.NoEventsUIRoot);
		transform.get_gameObject().set_layer(LayerSystem.NameToLayer("UI"));
		ArrowManager.Pool2EnemyArrow = transform;
		UGUITools.ResetTransform(ArrowManager.Pool2EnemyArrow);
		ArrowManager.EnemyArrowPool = new UIPool("EnemyInScreenUnit", ArrowManager.Pool2EnemyArrow, false);
	}

	public void Init()
	{
	}

	public void AddEnemy(int actorType, Transform actorRoot, ActorParent actorParent, float height, long uuid, bool isTarget, bool isShowOfLogic)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		this.RemoveEnemy(uuid, actorRoot);
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (!EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		if (actorType == 51 || actorType == 5 || actorType == 6 || actorType == 61)
		{
			EnemyInScreenUnit enemyInScreenUnit = this.Create2Enemy(actorRoot, height, uuid);
			enemyInScreenUnit.SetUUID(uuid);
			enemyInScreenUnit.SetTargetPosition(actorRoot, actorParent);
			enemyInScreenUnit.SetFlag(isTarget, false);
			enemyInScreenUnit.Show(actorRoot.get_gameObject().get_activeInHierarchy() && isShowOfLogic);
		}
		else if (actorType == 53 || actorType == 3 || actorType == 4 || actorType == 62)
		{
			EnemyInScreenUnit enemyInScreenUnit2 = this.Create2Enemy(actorRoot, height, uuid);
			enemyInScreenUnit2.SetUUID(uuid);
			enemyInScreenUnit2.SetTargetPosition(actorRoot, actorParent);
			enemyInScreenUnit2.SetFlag(isTarget, true);
			enemyInScreenUnit2.Show(actorRoot.get_gameObject().get_activeInHierarchy() && isShowOfLogic);
		}
	}

	public void RemoveEnemy(long uuid, Transform actorRoot)
	{
		for (int i = 0; i < this.EnemyArrows.get_Count(); i++)
		{
			EnemyInScreenUnit enemyInScreenUnit = this.EnemyArrows.get_Item(i);
			if (enemyInScreenUnit.uuid == uuid && enemyInScreenUnit.get_transform() == actorRoot)
			{
				if (enemyInScreenUnit != null && enemyInScreenUnit.GetBillboardTransform() != null)
				{
					ArrowManager.EnemyArrowPool.ReUse(enemyInScreenUnit.GetBillboardTransform().get_gameObject());
					enemyInScreenUnit.ResetAll();
				}
				this.EnemyArrows.RemoveAt(i);
				return;
			}
		}
	}

	public void Show(long uuid, bool isShow)
	{
		EnemyInScreenUnit unitInActive = this.GetUnitInActive(uuid);
		if (unitInActive != null)
		{
			unitInActive.set_enabled(isShow);
		}
	}

	private EnemyInScreenUnit Create2Enemy(Transform parent, float height, long uuid)
	{
		GameObject gameObject = ArrowManager.EnemyArrowPool.Get(string.Empty);
		gameObject.set_name(uuid.ToString());
		EnemyInScreenUnit enemyInScreenUnit = parent.get_gameObject().AddMissingComponent<EnemyInScreenUnit>();
		enemyInScreenUnit.AwakeSelf(gameObject.get_transform());
		this.EnemyArrows.Add(enemyInScreenUnit);
		enemyInScreenUnit.set_enabled(true);
		return enemyInScreenUnit;
	}

	private EnemyInScreenUnit GetUnitInActive(long uuid)
	{
		for (int i = 0; i < this.EnemyArrows.get_Count(); i++)
		{
			EnemyInScreenUnit enemyInScreenUnit = this.EnemyArrows.get_Item(i);
			if (enemyInScreenUnit.uuid == uuid && enemyInScreenUnit.get_gameObject().get_activeSelf())
			{
				return enemyInScreenUnit;
			}
		}
		return null;
	}
}
