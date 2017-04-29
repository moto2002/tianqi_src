using System;
using UnityEngine;
using XEngineActor;

public class ShadowSlicePlane : MonoBehaviour
{
	public long uuid;

	public Transform m_root;

	public ActorParent m_actorParent;

	public MeshRenderer m_meshRenderer;

	private float shadowY;

	public bool IsActorVisibleOn = true;

	private void Update()
	{
		if (InstanceManager.CurrentInstanceType == InstanceType.None)
		{
			if (this.m_meshRenderer != null)
			{
				this.m_meshRenderer.set_enabled(this.IsActorVisibleOn);
			}
		}
		else if (this.uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(this.uuid, BillboardManager.BillboardInfoOffOption.Shadow))
		{
			if (this.m_meshRenderer != null)
			{
				this.m_meshRenderer.set_enabled(false);
			}
		}
		else if (this.m_meshRenderer != null)
		{
			this.m_meshRenderer.set_enabled(true);
		}
	}

	public void SetShadowY()
	{
		if (this.m_root == null)
		{
			return;
		}
		this.shadowY = this.GetOffsetY();
		if (this.m_actorParent != null)
		{
			base.get_transform().set_position(this.m_actorParent.GetAnimationFootPos());
			base.get_transform().set_localPosition(new Vector3(base.get_transform().get_localPosition().x, this.shadowY, base.get_transform().get_localPosition().z));
		}
		else
		{
			base.get_transform().set_localPosition(new Vector3(0f, this.shadowY, 0f));
		}
	}

	private float GetOffsetY()
	{
		return 0.03f;
	}
}
