using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class ActorVisibleCtrl : MonoBehaviour
{
	private const float DISTANCE_HIDE = 6f;

	private const float DISTANCE_SHOW = 5f;

	public long uuid;

	private long ownerId;

	private int m_objType = 31;

	private IActorVisible interfaceGroup;

	private Actor m_actor;

	private ActorParent m_actorParent;

	private List<Renderer> m_listSmr = new List<Renderer>();

	private ShadowSlicePlane m_shadowSlicePlane;

	private List<AdjustTransparency> m_alphaControls = new List<AdjustTransparency>();

	private List<Animation> m_listAnimation = new List<Animation>();

	private List<Animator> m_listAnimator = new List<Animator>();

	private bool m_isInit;

	private bool m_isPeopleShow = true;

	private List<AdjustTransparency> alphaControls
	{
		get
		{
			if (this.IsSelfManagerType())
			{
				return this.m_alphaControls;
			}
			if (this.m_actorParent != null && this.m_actorParent.GetEntity() != null)
			{
				return this.m_actorParent.GetEntity().alphaControls;
			}
			return this.m_alphaControls;
		}
	}

	private bool IsSelfManagerType()
	{
		return this.m_objType == 31 || this.m_objType == 22 || this.m_objType == 23 || this.m_objType == 24 || this.m_objType == 25;
	}

	private List<Renderer> GetRenderers()
	{
		if (this.IsSelfManagerType())
		{
			return this.m_listSmr;
		}
		if (this.m_actorParent != null && this.m_actorParent.GetEntity() != null)
		{
			return this.m_actorParent.GetEntity().shaderRenderers;
		}
		return this.m_listSmr;
	}

	private ShadowSlicePlane GetShadowSlicePlane()
	{
		if (this.IsSelfManagerType())
		{
			return this.m_shadowSlicePlane;
		}
		if (this.m_actorParent != null && this.m_actorParent.GetEntity() != null)
		{
			return this.m_actorParent.GetEntity().shadowSlicePlane;
		}
		return this.m_shadowSlicePlane;
	}

	public void AwakeSelf(int _type, long _uuid, long _ownerId, IActorVisible _interfaceGroup)
	{
		this.uuid = _uuid;
		this.ownerId = _ownerId;
		this.m_objType = _type;
		this.interfaceGroup = _interfaceGroup;
		if (this.IsSelfManagerType())
		{
			this.m_actor = base.GetComponent<Actor>();
			Renderer renderer = null;
			ShaderEffectUtils.InitShaderRenderers(base.get_transform(), this.m_listSmr, ref renderer, ref this.m_shadowSlicePlane);
			ShaderEffectUtils.InitTransparencys(this.m_listSmr, this.m_alphaControls);
		}
		else
		{
			this.m_actorParent = base.GetComponent<ActorParent>();
		}
		this.m_listAnimation.AddRange(base.get_transform().GetComponentsInChildren<Animation>(true));
		this.m_listAnimator.AddRange(base.get_transform().GetComponentsInChildren<Animator>(true));
		this.UpdateShow(true);
	}

	private void LateUpdate()
	{
		if (Time.get_frameCount() % 10 != 0)
		{
			return;
		}
		this.UpdateShow(false);
	}

	private void OnDisable()
	{
		this.m_isPeopleShow = ActorVisibleManager.Instance.SetShow(this.uuid, false, false);
	}

	private void OnDestroy()
	{
		this.m_isPeopleShow = ActorVisibleManager.Instance.SetShow(this.uuid, false, false);
	}

	private void UpdateShow(bool isInited = false)
	{
		if (EntityWorld.Instance.ActSelf == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		if (CamerasMgr.CameraMain == null)
		{
			return;
		}
		if (!CamerasMgr.CameraMain.get_enabled())
		{
			if (isInited)
			{
				this.ShowByDistance(false, true);
			}
			return;
		}
		if (this.m_objType == 22)
		{
			this.ShowByDistance(ActorVisibleManager.Instance.IsShow(this.ownerId), isInited);
		}
		else
		{
			Vector3 vector = CamerasMgr.CameraMain.WorldToScreenPoint(base.get_transform().get_position());
			Vector3 vector2 = CamerasMgr.CameraMain.WorldToScreenPoint(EntityWorld.Instance.ActSelf.FixTransform.get_position());
			if (Vector3.Distance(base.get_transform().get_position(), EntityWorld.Instance.ActSelf.FixTransform.get_position()) > BillboardManager.GetDistanceOfAVC(this.m_objType))
			{
				this.ShowByDistance(false, isInited);
			}
			else if (vector2.z - vector.z >= 6f)
			{
				this.ShowByDistance(false, isInited);
			}
			else if (vector2.z - vector.z <= 5f)
			{
				this.ShowByDistance(true, isInited);
			}
		}
	}

	public void RemoveAVC()
	{
		ShaderEffectUtils.SetFadeRightNow(this.alphaControls, false);
		this.ShowAnims(true);
		this.ShowSMRs(true);
	}

	public void ShowByDistance(bool isShow, bool isRightNow)
	{
		if (isShow)
		{
			this.WhenObjBecameVisible(isRightNow);
		}
		else
		{
			this.WhenObjBecameInvisible(isRightNow);
		}
	}

	public void RefreshRenderers()
	{
		if (!this.m_isPeopleShow)
		{
			this.ShowSMRs(false);
		}
	}

	public bool IsShow()
	{
		return this.m_isPeopleShow;
	}

	private void WhenObjBecameVisible(bool isRightNow = false)
	{
		bool flag = ActorVisibleManager.Instance.SetShow(this.uuid, true, this.IsIgnorePeopleShowMaximumType());
		if (!this.m_isInit)
		{
			this.m_isInit = true;
			this.m_isPeopleShow = flag;
		}
		else
		{
			if (this.m_isPeopleShow == flag)
			{
				return;
			}
			this.m_isPeopleShow = flag;
		}
		this.ShowAnims(this.m_isPeopleShow);
		this.ShowSMRs(this.m_isPeopleShow);
		if (isRightNow)
		{
			ShaderEffectUtils.SetFadeRightNow(this.alphaControls, false);
			this.ShowHeadInfo(this.m_isPeopleShow);
			this.ShowWeaponFX(this.m_isPeopleShow);
		}
		else
		{
			ShaderEffectUtils.SetFade(this.alphaControls, false, delegate
			{
				this.ShowHeadInfo(this.m_isPeopleShow);
				this.ShowWeaponFX(this.m_isPeopleShow);
			});
		}
	}

	private void WhenObjBecameInvisible(bool isRightNow = false)
	{
		ActorVisibleManager.Instance.SetShow(this.uuid, false, this.IsIgnorePeopleShowMaximumType());
		if (!this.m_isInit)
		{
			this.m_isInit = true;
			this.m_isPeopleShow = false;
		}
		else
		{
			if (!this.m_isPeopleShow)
			{
				return;
			}
			this.m_isPeopleShow = false;
		}
		this.ShowHeadInfo(false);
		this.ShowWeaponFX(false);
		if (isRightNow)
		{
			ShaderEffectUtils.SetFadeRightNow(this.alphaControls, true);
			this.ShowAnims(false);
			this.ShowSMRs(false);
		}
		else
		{
			ShaderEffectUtils.SetFade(this.alphaControls, true, delegate
			{
				this.ShowAnims(false);
				this.ShowSMRs(false);
			});
		}
	}

	private void ShowSMRs(bool isPeopleShow)
	{
		List<Renderer> renderers = this.GetRenderers();
		ShadowSlicePlane shadowSlicePlane = this.GetShadowSlicePlane();
		if (renderers != null)
		{
			for (int i = 0; i < renderers.get_Count(); i++)
			{
				if (renderers.get_Item(i) != null)
				{
					renderers.get_Item(i).set_enabled(isPeopleShow);
				}
			}
		}
		if (shadowSlicePlane != null)
		{
			shadowSlicePlane.IsActorVisibleOn = isPeopleShow;
		}
	}

	private void ShowAnims(bool isPeopleShow)
	{
		if (!InstanceManager.IsActorAnimatorOn)
		{
			return;
		}
		for (int i = 0; i < this.m_listAnimation.get_Count(); i++)
		{
			if (this.m_listAnimation.get_Item(i) != null)
			{
				this.m_listAnimation.get_Item(i).set_enabled(isPeopleShow);
			}
		}
		for (int j = 0; j < this.m_listAnimator.get_Count(); j++)
		{
			if (this.m_listAnimator.get_Item(j) != null)
			{
				this.m_listAnimator.get_Item(j).set_enabled(isPeopleShow);
			}
		}
		if (this.interfaceGroup != null)
		{
			this.interfaceGroup.OnAnimatorBecameVisiable();
		}
	}

	private void ShowHeadInfo(bool isPeopleShow)
	{
		switch (this.m_objType)
		{
		case 21:
			if (this.m_actorParent != null)
			{
				HeadInfoManager.Instance.show_control_actorvisible(this.uuid, isPeopleShow);
			}
			break;
		case 22:
			if (this.m_actor != null)
			{
				HeadInfoManager.Instance.show_control_actorvisible(this.uuid, isPeopleShow);
			}
			break;
		case 23:
		case 24:
		case 25:
			if (this.m_actor != null)
			{
				HeadInfoManager.Instance.show_control_actorvisible(this.uuid, isPeopleShow);
			}
			break;
		case 31:
			if (this.m_actor != null)
			{
				HeadInfoManager.Instance.show_control_actorvisible(this.uuid, isPeopleShow);
			}
			break;
		}
	}

	private void ShowWeaponFX(bool isPeopleShow)
	{
		int objType = this.m_objType;
		if (objType == 21)
		{
			if (this.m_actorParent != null)
			{
				this.m_actorParent.GetEntity().ShowEquipFX(isPeopleShow);
			}
		}
	}

	private bool IsIgnorePeopleShowMaximumType()
	{
		return this.m_objType == 31 || this.m_objType == 23 || this.m_objType == 22;
	}
}
