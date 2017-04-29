using System;
using UnityEngine;

public class NcDuplicator : NcEffectBehaviour
{
	public float m_fDuplicateTime = 0.1f;

	public int m_nDuplicateCount = 3;

	public float m_fDuplicateLifeTime;

	public Vector3 m_AddStartPos = Vector3.get_zero();

	public Vector3 m_AccumStartRot = Vector3.get_zero();

	public Vector3 m_RandomRange = Vector3.get_zero();

	protected int m_nCreateCount;

	protected float m_fStartTime;

	protected GameObject m_ClonObject;

	protected bool m_bInvoke;

	public override int GetAnimationState()
	{
		if (base.get_enabled() && NcEffectBehaviour.IsActive(base.get_gameObject()) && (this.m_nDuplicateCount == 0 || (this.m_nDuplicateCount != 0 && this.m_nCreateCount < this.m_nDuplicateCount)))
		{
			return 1;
		}
		return 0;
	}

	public GameObject GetCloneObject()
	{
		return this.m_ClonObject;
	}

	private void Awake()
	{
		this.m_nCreateCount = 0;
		this.m_fStartTime = -this.m_fDuplicateTime;
		this.m_ClonObject = null;
		this.m_bInvoke = false;
		if (!base.get_enabled())
		{
			return;
		}
		if (base.get_transform().get_parent() != null && base.get_enabled() && NcEffectBehaviour.IsActive(base.get_gameObject()) && base.GetComponent<NcDontActive>() == null)
		{
			this.InitCloneObject();
		}
	}

	private void OnDestroy()
	{
		if (this.m_ClonObject != null)
		{
			Object.Destroy(this.m_ClonObject);
		}
	}

	private void Start()
	{
		if (this.m_bInvoke)
		{
			this.m_fStartTime = NcEffectBehaviour.GetEngineTime();
			this.CreateCloneObject();
			base.InvokeRepeating("CreateCloneObject", this.m_fDuplicateTime, this.m_fDuplicateTime);
		}
	}

	private void Update()
	{
		if (this.m_bInvoke)
		{
			return;
		}
		if ((this.m_nDuplicateCount == 0 || this.m_nCreateCount < this.m_nDuplicateCount) && this.m_fStartTime + this.m_fDuplicateTime <= NcEffectBehaviour.GetEngineTime())
		{
			this.m_fStartTime = NcEffectBehaviour.GetEngineTime();
			this.CreateCloneObject();
		}
	}

	private void InitCloneObject()
	{
		if (this.m_ClonObject == null)
		{
			this.m_ClonObject = base.CreateGameObject(base.get_gameObject());
			NcEffectBehaviour.HideNcDelayActive(this.m_ClonObject);
			NcDuplicator component = this.m_ClonObject.GetComponent<NcDuplicator>();
			if (component != null)
			{
				Object.Destroy(component);
			}
			NcDelayActive component2 = this.m_ClonObject.GetComponent<NcDelayActive>();
			if (component2 != null)
			{
				Object.Destroy(component2);
			}
			Component[] components = base.get_transform().GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
			{
				if (!(components[i] is Transform) && !(components[i] is NcDuplicator))
				{
					Object.Destroy(components[i]);
				}
			}
			NcEffectBehaviour.RemoveAllChildObject(base.get_gameObject(), false);
			return;
		}
	}

	private void CreateCloneObject()
	{
		if (this.m_ClonObject == null)
		{
			return;
		}
		GameObject gameObject;
		if (base.get_transform().get_parent() == null)
		{
			gameObject = base.CreateGameObject(base.get_gameObject());
		}
		else
		{
			gameObject = base.CreateGameObject(base.get_transform().get_parent().get_gameObject(), this.m_ClonObject);
		}
		if (0f < this.m_fDuplicateLifeTime)
		{
			NcAutoDestruct ncAutoDestruct = gameObject.GetComponent<NcAutoDestruct>();
			if (ncAutoDestruct == null)
			{
				ncAutoDestruct = gameObject.AddComponent<NcAutoDestruct>();
			}
			ncAutoDestruct.m_fLifeTime = this.m_fDuplicateLifeTime;
		}
		Vector3 position = gameObject.get_transform().get_position();
		gameObject.get_transform().set_position(new Vector3(Random.Range(-this.m_RandomRange.x, this.m_RandomRange.x) + position.x, Random.Range(-this.m_RandomRange.y, this.m_RandomRange.y) + position.y, Random.Range(-this.m_RandomRange.z, this.m_RandomRange.z) + position.z));
		Transform expr_11B = gameObject.get_transform();
		expr_11B.set_position(expr_11B.get_position() + this.m_AddStartPos);
		Transform expr_137 = gameObject.get_transform();
		expr_137.set_localRotation(expr_137.get_localRotation() * Quaternion.Euler(this.m_AccumStartRot.x * (float)this.m_nCreateCount, this.m_AccumStartRot.y * (float)this.m_nCreateCount, this.m_AccumStartRot.z * (float)this.m_nCreateCount));
		GameObject expr_186 = gameObject;
		expr_186.set_name(expr_186.get_name() + " " + this.m_nCreateCount);
		this.m_nCreateCount++;
		if (this.m_bInvoke && this.m_nDuplicateCount <= this.m_nCreateCount)
		{
			base.CancelInvoke("CreateCloneObject");
		}
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		this.m_fDuplicateTime /= fSpeedRate;
		this.m_fDuplicateLifeTime /= fSpeedRate;
		if (bRuntime && this.m_ClonObject != null)
		{
			NcEffectBehaviour.AdjustSpeedRuntime(this.m_ClonObject, fSpeedRate);
		}
	}
}
