using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EffectSettings : MonoBehaviour
{
	public float ColliderRadius = 0.2f;

	public float EffectRadius;

	public GameObject Target;

	public float MoveSpeed = 1f;

	public float MoveDistance = 20f;

	public bool IsHomingMove;

	public bool IsVisible = true;

	public bool DeactivateAfterCollision = true;

	public float DeactivateTimeDelay = 4f;

	public LayerMask LayerMask = -1;

	private GameObject[] active_key = new GameObject[100];

	private float[] active_value = new float[100];

	private GameObject[] inactive_Key = new GameObject[100];

	private float[] inactive_value = new float[100];

	private int lastActiveIndex;

	private int lastInactiveIndex;

	private int currentActiveGo;

	private int currentInactiveGo;

	private bool deactivatedIsWait;

	public event EventHandler<CollisionInfo> CollisionEnter
	{
		[MethodImpl(32)]
		add
		{
			this.CollisionEnter = (EventHandler<CollisionInfo>)Delegate.Combine(this.CollisionEnter, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CollisionEnter = (EventHandler<CollisionInfo>)Delegate.Remove(this.CollisionEnter, value);
		}
	}

	public event EventHandler EffectDeactivated
	{
		[MethodImpl(32)]
		add
		{
			this.EffectDeactivated = (EventHandler)Delegate.Combine(this.EffectDeactivated, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.EffectDeactivated = (EventHandler)Delegate.Remove(this.EffectDeactivated, value);
		}
	}

	public void OnCollisionHandler(CollisionInfo e)
	{
		for (int i = 0; i < this.lastActiveIndex; i++)
		{
			base.Invoke("SetGoActive", this.active_value[i]);
		}
		for (int j = 0; j < this.lastInactiveIndex; j++)
		{
			base.Invoke("SetGoInactive", this.inactive_value[j]);
		}
		EventHandler<CollisionInfo> collisionEnter = this.CollisionEnter;
		if (collisionEnter != null)
		{
			collisionEnter.Invoke(this, e);
		}
		if (this.DeactivateAfterCollision && !this.deactivatedIsWait)
		{
			this.deactivatedIsWait = true;
			base.Invoke("Deactivate", this.DeactivateTimeDelay);
		}
	}

	public void OnEffectDeactivatedHandler()
	{
		EventHandler effectDeactivated = this.EffectDeactivated;
		if (effectDeactivated != null)
		{
			effectDeactivated.Invoke(this, EventArgs.Empty);
		}
	}

	public void Deactivate()
	{
		this.OnEffectDeactivatedHandler();
		base.get_gameObject().SetActive(false);
	}

	private void SetGoActive()
	{
		this.active_key[this.currentActiveGo].SetActive(false);
		this.currentActiveGo++;
		if (this.currentActiveGo >= this.lastActiveIndex)
		{
			this.currentActiveGo = 0;
		}
	}

	private void SetGoInactive()
	{
		this.inactive_Key[this.currentInactiveGo].SetActive(true);
		this.currentInactiveGo++;
		if (this.currentInactiveGo >= this.lastInactiveIndex)
		{
			this.currentInactiveGo = 0;
		}
	}

	public void OnEnable()
	{
		for (int i = 0; i < this.lastActiveIndex; i++)
		{
			this.active_key[i].SetActive(true);
		}
		for (int j = 0; j < this.lastInactiveIndex; j++)
		{
			this.inactive_Key[j].SetActive(false);
		}
		this.deactivatedIsWait = false;
	}

	public void OnDisable()
	{
		base.CancelInvoke("SetGoActive");
		base.CancelInvoke("SetGoInactive");
		base.CancelInvoke("Deactivate");
		this.currentActiveGo = 0;
		this.currentInactiveGo = 0;
	}

	public void RegistreActiveElement(GameObject go, float time)
	{
		this.active_key[this.lastActiveIndex] = go;
		this.active_value[this.lastActiveIndex] = time;
		this.lastActiveIndex++;
	}

	public void RegistreInactiveElement(GameObject go, float time)
	{
		this.inactive_Key[this.lastInactiveIndex] = go;
		this.inactive_value[this.lastInactiveIndex] = time;
		this.lastInactiveIndex++;
	}
}
