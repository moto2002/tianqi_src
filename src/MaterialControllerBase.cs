using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public abstract class MaterialControllerBase : MonoBehaviour
{
	public Material mat;

	public Dictionary<int, Material[]> m_src_matmap = new Dictionary<int, Material[]>();

	public Dictionary<int, Material[]> m_dst_matmap = new Dictionary<int, Material[]>();

	protected bool m_isInitValue;

	protected bool m_takeEffect;

	private bool m_isVisible;

	protected Transform prefabRoot;

	protected bool UseOriginalPrevious;

	protected virtual void OnEnable()
	{
		if (!this.m_isVisible)
		{
			this.m_isVisible = true;
		}
	}

	protected virtual void OnDisable()
	{
		this.DoOnDisable();
	}

	public void DoOnDisable()
	{
		if (this.m_isVisible)
		{
			this.RecoverRendererToOriginalMaterial();
			this.ClearChangeMaterialMark();
			this.m_takeEffect = false;
			this.m_isInitValue = false;
			this.m_isVisible = false;
			this.prefabRoot = null;
		}
	}

	protected abstract void ChangeRendererToCommonMaterial();

	protected abstract void RecoverRendererToOriginalMaterial();

	protected virtual void Update()
	{
		if (!this.m_isVisible)
		{
			return;
		}
		if (this.mat == null)
		{
			return;
		}
		if (this.prefabRoot == null)
		{
			ActorFX actorFX = MaterialControllerBase.FindActorFX(base.get_transform());
			if (actorFX != null)
			{
				this.prefabRoot = actorFX.HostRoot;
			}
		}
		if (!this.IsCanUpdate())
		{
			return;
		}
		if (!this.m_takeEffect)
		{
			if (this.prefabRoot != null)
			{
				this.SetChangeMaterialMark();
				this.ChangeRendererToCommonMaterial();
				this.m_takeEffect = true;
			}
			else if (Application.get_platform() == 7)
			{
				Debug.LogError("ActorFX属性HostRoot is null");
			}
		}
	}

	protected void SetChangeMaterialMark()
	{
		this.UseOriginalPrevious = false;
		MaterialControllerMark materialControllerMark = this.prefabRoot.get_gameObject().AddUniqueComponent<MaterialControllerMark>();
		if (materialControllerMark.Current != null && materialControllerMark.Current.GetInstanceID() != base.GetInstanceID())
		{
			this.UseOriginalPrevious = true;
			this.SaveMarkMaterials(materialControllerMark);
			materialControllerMark.Current.m_src_matmap.Clear();
			materialControllerMark.Current.DoOnDisable();
		}
		materialControllerMark.Current = this;
	}

	protected void ClearChangeMaterialMark()
	{
		if (this.prefabRoot != null)
		{
			MaterialControllerMark component = this.prefabRoot.get_gameObject().GetComponent<MaterialControllerMark>();
			if (component != null && component.Current != null)
			{
				component.Current = null;
			}
		}
	}

	protected void SaveMarkMaterials(MaterialControllerMark mark)
	{
		if (mark == null)
		{
			return;
		}
		if (mark.Current == null)
		{
			return;
		}
		if (mark.Current.m_src_matmap.get_Count() <= 0)
		{
			return;
		}
		using (Dictionary<int, Material[]>.Enumerator enumerator = mark.Current.m_src_matmap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, Material[]> current = enumerator.get_Current();
				this.m_src_matmap.set_Item(current.get_Key(), current.get_Value());
			}
		}
	}

	private bool IsCanUpdate()
	{
		if (this.prefabRoot == null)
		{
			return false;
		}
		MaterialControllerMark component = this.prefabRoot.get_gameObject().GetComponent<MaterialControllerMark>();
		return !(component != null) || !(component.Current != null) || this is ChangeMaterialBase;
	}

	protected static ActorFX FindActorFX(Transform transform)
	{
		if (transform.GetComponent<ActorFX>() != null)
		{
			return transform.GetComponent<ActorFX>();
		}
		Transform parent = transform.get_parent();
		if (parent == null)
		{
			return null;
		}
		while (parent.GetComponent<ActorFX>() == null)
		{
			parent = parent.get_parent();
			if (parent == null)
			{
				break;
			}
		}
		if (parent != null)
		{
			return parent.GetComponent<ActorFX>();
		}
		return null;
	}

	protected bool IsRendererValid(Renderer item)
	{
		return item.get_gameObject().get_layer() != LayerSystem.NameToLayer("FX") && item.GetType() != typeof(ParticleSystemRenderer) && !item.get_gameObject().get_name().Equals("SP");
	}
}
