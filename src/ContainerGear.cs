using System;
using UnityEngine;

public class ContainerGear : GearParent
{
	public static XDict<Transform, ContainerGear> containers = new XDict<Transform, ContainerGear>();

	public ContainerGearType type;

	public bool isSelfAttackable = true;

	public bool isPlayerAttackable = true;

	public bool isPetAttackable = true;

	public bool isMonsterAttackable = true;

	public int life = 1;

	public float hitRange;

	public bool isDieDelete = true;

	protected Transform theTransform;

	protected Animator theAnimator;

	public static void ClearAllContainers()
	{
		ContainerGear.containers.Clear();
	}

	private void Start()
	{
		if (base.get_transform().get_parent() == null)
		{
			this.theTransform = base.get_transform();
		}
		else
		{
			this.theTransform = base.get_transform().get_parent();
		}
		ContainerGear.containers.Add(this.theTransform, this);
		this.theAnimator = base.GetComponent<Animator>();
		this.OnActivate();
	}

	private void OnDestroy()
	{
		ContainerGear.containers.Remove(this.theTransform);
		this.theTransform = null;
		this.theAnimator = null;
	}

	public void OnActionStart(string actionName)
	{
	}

	public void OnActionEnd(string actionName)
	{
		if (actionName == "born" && this.theAnimator)
		{
			this.theAnimator.Play("idle");
		}
		else if (actionName == "die")
		{
			this.DeleteContainer();
		}
	}

	public void fx(AnimationEvent arg)
	{
		int intParameter = arg.get_intParameter();
		if (intParameter != 0)
		{
			FXManager.Instance.PlayFX(intParameter, this.theTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		}
	}

	public virtual void OnActivate()
	{
		this.active = true;
		if (this.theAnimator)
		{
			this.theAnimator.Play("born");
		}
	}

	public virtual void OnHit(int effectID)
	{
		if (this.life <= 0)
		{
			return;
		}
		this.life--;
		if (this.life > 0)
		{
			this.PlayHit(effectID);
		}
		else
		{
			this.PlayDie(effectID);
		}
	}

	public virtual void PlayHit(int effectID)
	{
		if (this.theAnimator)
		{
			this.theAnimator.Play("hit");
		}
	}

	public virtual void PlayDie(int effectID)
	{
		if (this.theAnimator)
		{
			this.theAnimator.Play("die");
		}
		ContainerGear.containers.Remove(this.theTransform);
		ContainerGearType containerGearType = this.type;
		if (containerGearType != ContainerGearType.Obstacle)
		{
			Vector3 position = this.theTransform.get_position();
			EventDispatcher.BroadcastAsync<ContainerGearType, Vector3>(ContainerGearEvent.Broken, this.type, position);
		}
	}

	protected void DeleteContainer()
	{
		if (this && this.theTransform && this.theTransform.get_gameObject())
		{
			Object.Destroy(this.theTransform.get_gameObject());
		}
	}
}
