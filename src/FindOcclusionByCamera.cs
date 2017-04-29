using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class FindOcclusionByCamera : TimerInterval
{
	private List<GameObject> m_listLastObj = new List<GameObject>();

	private Vector3 hDirection = Vector3.get_zero();

	private Vector3 targetPosition = Vector3.get_zero();

	private Vector3 direction = Vector3.get_zero();

	private RaycastHit[] hits;

	private float distance;

	private int ihit;

	public Transform target
	{
		get
		{
			if (EntityWorld.Instance != null)
			{
				return EntityWorld.Instance.TraSelf;
			}
			return null;
		}
	}

	private void Start()
	{
		base.Circle1 = 0.5f;
	}

	private void LateUpdate()
	{
		if (!this.target || this == null || base.get_transform() == null)
		{
			return;
		}
		if (base.IsTime1Over())
		{
			this.DealOcclusion();
		}
	}

	private void DealOcclusion()
	{
		this.hDirection = (new Vector3(base.get_transform().get_position().x, 0f, base.get_transform().get_position().z) - new Vector3(this.target.get_position().x, 0f, this.target.get_position().z)).get_normalized();
		this.targetPosition = this.target.get_position() + this.hDirection * 0.01f + new Vector3(0f, 0.5f, 0f);
		this.direction = (base.get_transform().get_position() - this.targetPosition).get_normalized();
		this.hits = null;
		this.distance = Vector3.Distance(this.targetPosition, base.get_transform().get_position());
		this.hits = Physics.RaycastAll(this.targetPosition, this.direction, this.distance);
		if (this.hits != null)
		{
			for (int i = 0; i < this.m_listLastObj.get_Count(); i++)
			{
				this.ihit = 0;
				while (this.ihit < this.hits.Length)
				{
					if (this.IsOcclusion(this.hits[this.ihit]))
					{
						if (this.m_listLastObj.get_Item(i) != null && this.m_listLastObj.get_Item(i) == this.hits[this.ihit].get_collider().get_gameObject())
						{
							break;
						}
					}
					this.ihit++;
				}
				if (this.ihit >= this.hits.Length && this.m_listLastObj.get_Item(i) != null)
				{
					ShaderEffectUtils.SetIsNearCamera(this.m_listLastObj.get_Item(i).get_transform(), false);
				}
			}
			this.m_listLastObj.Clear();
			for (int j = 0; j < this.hits.Length; j++)
			{
				if (this.IsOcclusion(this.hits[j]))
				{
					ShaderEffectUtils.SetIsNearCamera(this.hits[j].get_collider().get_transform(), true);
					this.m_listLastObj.Add(this.hits[j].get_collider().get_gameObject());
				}
			}
		}
		else
		{
			for (int k = 0; k < this.m_listLastObj.get_Count(); k++)
			{
				ShaderEffectUtils.SetIsNearCamera(this.m_listLastObj.get_Item(k).get_transform(), false);
			}
			this.m_listLastObj.Clear();
		}
	}

	private bool IsOcclusion(RaycastHit hit)
	{
		return hit.get_collider() != null && hit.get_collider().get_gameObject() != null && hit.get_collider().GetComponent<ActorSelf>() == null;
	}

	private bool IsOcclusionLayers(int layer)
	{
		return false;
	}
}
