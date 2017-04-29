using System;
using UnityEngine;

public class RangeChecker : GearParent
{
	public float range = 2f;

	public float interval = 0.5f;

	protected float currentDeltaTime;

	protected bool isInRange;

	private void Awake()
	{
		this.AddListeners();
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	public void Update()
	{
		if (!MySceneManager.Instance.IsSceneExist)
		{
			return;
		}
		this.currentDeltaTime += Time.get_unscaledDeltaTime();
		if (this.currentDeltaTime < this.interval)
		{
			return;
		}
		this.currentDeltaTime -= this.interval;
		if (this.isInRange)
		{
			if (!this.InRange(EntityWorld.Instance.EntSelf))
			{
				this.isInRange = false;
				this.ExitRange();
			}
		}
		else if (this.InRange(EntityWorld.Instance.EntSelf))
		{
			this.isInRange = true;
			this.EnterRange();
		}
	}

	protected virtual bool InRange(EntityParent entity)
	{
		return entity != null && entity.Actor && XUtility.DistanceNoY(base.get_transform().get_position(), entity.Actor.FixTransform.get_position()) <= this.range;
	}

	protected virtual void EnterRange()
	{
	}

	protected virtual void ExitRange()
	{
	}
}
