using System;
using UnityEngine;

public class NcDetachParent : NcEffectBehaviour
{
	public bool m_bFollowParentTransform = true;

	public bool m_bParentHideToStartDestroy = true;

	public float m_fSmoothDestroyTime = 2f;

	public bool m_bDisableEmit = true;

	public bool m_bSmoothHide = true;

	public bool m_bMeshFilterOnlySmoothHide;

	protected bool m_bStartDetach;

	protected float m_fStartDestroyTime;

	protected GameObject m_ParentGameObject;

	protected NcTransformTool m_OriginalPos = new NcTransformTool();

	public void SetDestroyValue(bool bParentHideToStart, bool bStartDisableEmit, float fSmoothDestroyTime, bool bSmoothHide, bool bMeshFilterOnlySmoothHide)
	{
		this.m_bParentHideToStartDestroy = bParentHideToStart;
		this.m_bDisableEmit = bStartDisableEmit;
		this.m_bSmoothHide = bSmoothHide;
		this.m_fSmoothDestroyTime = fSmoothDestroyTime;
		this.m_bMeshFilterOnlySmoothHide = bMeshFilterOnlySmoothHide;
	}

	private void Update()
	{
		if (!this.m_bStartDetach)
		{
			this.m_bStartDetach = true;
			if (base.get_transform().get_parent() != null)
			{
				this.m_ParentGameObject = base.get_transform().get_parent().get_gameObject();
				NcDetachObject.Create(this.m_ParentGameObject, base.get_transform().get_gameObject());
			}
			GameObject rootInstanceEffect = NcEffectBehaviour.GetRootInstanceEffect();
			if (this.m_bFollowParentTransform)
			{
				this.m_OriginalPos.SetLocalTransform(base.get_transform());
				base.ChangeParent(rootInstanceEffect.get_transform(), base.get_transform(), false, null);
				this.m_OriginalPos.CopyToLocalTransform(base.get_transform());
			}
			else
			{
				base.ChangeParent(rootInstanceEffect.get_transform(), base.get_transform(), false, null);
			}
			if (!this.m_bParentHideToStartDestroy)
			{
				this.StartDestroy();
			}
		}
		if (0f < this.m_fStartDestroyTime)
		{
			if (0f < this.m_fSmoothDestroyTime)
			{
				if (this.m_bSmoothHide)
				{
					float num = 1f - (NcEffectBehaviour.GetEngineTime() - this.m_fStartDestroyTime) / this.m_fSmoothDestroyTime;
					if (num < 0f)
					{
						num = 0f;
					}
					if (this.m_bMeshFilterOnlySmoothHide)
					{
						MeshFilter[] componentsInChildren = base.get_transform().GetComponentsInChildren<MeshFilter>(true);
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Color[] colors = componentsInChildren[i].get_mesh().get_colors();
							for (int j = 0; j < colors.Length; j++)
							{
								Color color = colors[j];
								color.a -= num;
								colors[j] = color;
							}
							componentsInChildren[i].get_mesh().set_colors(colors);
						}
					}
					else
					{
						Renderer[] componentsInChildren2 = base.get_transform().GetComponentsInChildren<Renderer>(true);
						for (int k = 0; k < componentsInChildren2.Length; k++)
						{
							Renderer renderer = componentsInChildren2[k];
							string materialColorName = NcEffectBehaviour.GetMaterialColorName(renderer.get_material());
							if (materialColorName != null)
							{
								Color color2 = renderer.get_material().GetColor(materialColorName);
								color2.a -= num;
								renderer.get_material().SetColor(materialColorName, color2);
							}
						}
					}
				}
				if (this.m_fStartDestroyTime + this.m_fSmoothDestroyTime < NcEffectBehaviour.GetEngineTime())
				{
					Object.Destroy(base.get_gameObject());
				}
			}
		}
		else if (this.m_bParentHideToStartDestroy && (this.m_ParentGameObject == null || !NcEffectBehaviour.IsActive(this.m_ParentGameObject)))
		{
			this.StartDestroy();
		}
		if (this.m_bFollowParentTransform && this.m_ParentGameObject != null && this.m_ParentGameObject.get_transform() != null)
		{
			NcTransformTool ncTransformTool = new NcTransformTool();
			ncTransformTool.SetTransform(this.m_OriginalPos);
			ncTransformTool.AddTransform(this.m_ParentGameObject.get_transform());
			ncTransformTool.CopyToLocalTransform(base.get_transform());
		}
	}

	private void StartDestroy()
	{
		this.m_fStartDestroyTime = NcEffectBehaviour.GetEngineTime();
		if (this.m_bDisableEmit)
		{
			base.DisableEmit();
		}
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		this.m_fSmoothDestroyTime /= fSpeedRate;
	}
}
