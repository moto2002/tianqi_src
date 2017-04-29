using System;
using UnityEngine;

public class BaseOrgan : MonoBehaviour, IOrgan
{
	[Header("速度")]
	public float speed = 4f;

	[Header("开始和结束的点")]
	public Transform startPoint;

	public Transform endPoint;

	[Header("初始化方向")]
	public bool isUp = true;

	[Header("圆盘半径")]
	public float radius = 2.5f;

	[Header("碰撞开关")]
	public Transform switchCollision;

	[HideInInspector]
	private Vector3 lastPosition;

	[HideInInspector]
	private Vector3 firstPosition;

	[HideInInspector]
	public bool isTrigger;

	private Collider col;

	private Action startMove;

	private Action endMove;

	private void Start()
	{
		OrganHandler.Instance.Add(this);
		this.col = base.GetComponentInChildren<Collider>();
	}

	protected void OnTriggerStay(Collider other)
	{
		if (other.get_gameObject().get_layer() == LayerSystem.NameToLayer(LayerSystem.GetGameObjectLayerName(EntityWorld.Instance.EntSelf.Camp, EntityWorld.Instance.EntSelf.LayerEntityNumber, 1)))
		{
			if (this.col != null && this.col.get_isTrigger())
			{
				float num = this.radius - other.get_gameObject().GetComponent<CharacterController>().get_radius() - 0.4f;
				if (Vector3.Distance(other.get_gameObject().get_transform().get_position(), base.get_transform().get_position()) < num)
				{
					this.isTrigger = true;
				}
			}
			this.StayPlatform(other);
		}
	}

	protected void OnTriggerEnter(Collider other)
	{
		Debuger.Error("OnTriggerEnter", new object[0]);
		if (other.get_gameObject().get_layer() == LayerSystem.NameToLayer(LayerSystem.GetGameObjectLayerName(EntityWorld.Instance.EntSelf.Camp, EntityWorld.Instance.EntSelf.LayerEntityNumber, 1)))
		{
			this.firstPosition = base.get_transform().get_position();
			if (this.isUp)
			{
				this.lastPosition = this.endPoint.get_position();
			}
			else
			{
				this.lastPosition = this.startPoint.get_position();
			}
			this.EnterPlatform(other);
		}
	}

	protected void OnTriggerExit(Collider other)
	{
		Debuger.Error("OnTriggerExit", new object[0]);
		if (other.get_gameObject().get_layer() == LayerSystem.NameToLayer(LayerSystem.GetGameObjectLayerName(EntityWorld.Instance.EntSelf.Camp, EntityWorld.Instance.EntSelf.LayerEntityNumber, 1)))
		{
			this.isTrigger = false;
			if (this.lastPosition == base.get_transform().get_position())
			{
				this.isUp = !this.isUp;
			}
			this.lastPosition = Vector3.get_zero();
			this.firstPosition = Vector3.get_zero();
			this.LeavePlatform(other);
			this.startMove = null;
			this.endMove = null;
		}
	}

	private void OnDistroy()
	{
		OrganHandler.Instance.Remove(this);
		this.col = null;
	}

	internal bool UpdatePos()
	{
		if (this.endPoint != null && this.startPoint != null)
		{
			Vector3 vector = this.lastPosition - base.get_transform().get_position();
			if (vector != Vector3.get_zero())
			{
				float sqrMagnitude = vector.get_sqrMagnitude();
				Vector3 vector2 = (this.lastPosition - this.firstPosition).get_normalized().get_normalized() * this.speed * Time.get_smoothDeltaTime();
				if (sqrMagnitude > vector2.get_sqrMagnitude())
				{
					base.get_transform().Translate(vector2, 0);
					if (this.isUp)
					{
						this.UpdateActor(vector2, false);
					}
					if (this.switchCollision != null && !this.switchCollision.get_gameObject().get_activeSelf())
					{
						this.switchCollision.get_gameObject().SetActive(true);
						if (this.startMove != null)
						{
							this.startMove.Invoke();
						}
					}
					return false;
				}
				base.get_transform().Translate(vector, 0);
				if (this.isUp)
				{
					this.UpdateActor(vector, false);
				}
				if (this.switchCollision != null && this.switchCollision.get_gameObject().get_activeSelf())
				{
					this.switchCollision.get_gameObject().SetActive(false);
					if (this.endMove != null)
					{
						this.endMove.Invoke();
					}
				}
				return true;
			}
		}
		return true;
	}

	public virtual void EnterPlatform(Collider other)
	{
	}

	public virtual void LeavePlatform(Collider other)
	{
	}

	public virtual void StayPlatform(Collider other)
	{
	}

	public virtual void UpdateActor(Vector3 delta, bool isEqual = false)
	{
	}

	public void AddStartPlatformEvent(Action start)
	{
		this.startMove = (Action)Delegate.Combine(this.startMove, start);
	}

	public void AddEndPlatformEvent(Action end)
	{
		this.endMove = (Action)Delegate.Combine(this.endMove, end);
	}

	public void RemoveStartPlatformEvent(Action start)
	{
		this.startMove = (Action)Delegate.Remove(this.startMove, start);
	}

	public void RemoveEndPlatformEvent(Action end)
	{
		this.endMove = (Action)Delegate.Remove(this.endMove, end);
	}
}
