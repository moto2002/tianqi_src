using System;
using UnityEngine;

public class NcAutoDestruct : NcEffectBehaviour
{
	public enum CollisionType
	{
		NONE,
		COLLISION,
		WORLD_Y
	}

	public float m_fLifeTime = 2f;

	public float m_fSmoothDestroyTime;

	public bool m_bDisableEmit = true;

	public bool m_bSmoothHide = true;

	public bool m_bMeshFilterOnlySmoothHide;

	protected bool m_bEndNcCurveAnimation;

	public NcAutoDestruct.CollisionType m_CollisionType;

	public LayerMask m_CollisionLayer = -1;

	public float m_fCollisionRadius = 0.3f;

	public float m_fDestructPosY = 0.2f;

	protected float m_fStartTime;

	protected float m_fStartDestroyTime;

	protected NcCurveAnimation m_NcCurveAnimation;

	public static NcAutoDestruct CreateAutoDestruct(GameObject baseGameObject, float fLifeTime, float fDestroyTime, bool bSmoothHide, bool bMeshFilterOnlySmoothHide)
	{
		NcAutoDestruct ncAutoDestruct = baseGameObject.AddComponent<NcAutoDestruct>();
		ncAutoDestruct.m_fLifeTime = fLifeTime;
		ncAutoDestruct.m_fSmoothDestroyTime = fDestroyTime;
		ncAutoDestruct.m_bSmoothHide = bSmoothHide;
		ncAutoDestruct.m_bMeshFilterOnlySmoothHide = bMeshFilterOnlySmoothHide;
		if (NcEffectBehaviour.IsActive(baseGameObject))
		{
			ncAutoDestruct.Start();
			ncAutoDestruct.Update();
		}
		return ncAutoDestruct;
	}

	private void Awake()
	{
		this.m_bEndNcCurveAnimation = false;
		this.m_fStartTime = 0f;
		this.m_NcCurveAnimation = null;
	}

	private void Start()
	{
		this.m_fStartTime = NcEffectBehaviour.GetEngineTime();
		if (this.m_bEndNcCurveAnimation)
		{
			this.m_NcCurveAnimation = base.GetComponent<NcCurveAnimation>();
		}
	}

	private void Update()
	{
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
					this.AutoDestruct();
				}
			}
		}
		else
		{
			if (0f < this.m_fStartTime && this.m_fStartTime + this.m_fLifeTime <= NcEffectBehaviour.GetEngineTime())
			{
				this.StartDestroy();
			}
			if (this.m_bEndNcCurveAnimation && this.m_NcCurveAnimation != null && 1f <= this.m_NcCurveAnimation.GetElapsedRate())
			{
				this.StartDestroy();
			}
		}
	}

	private void FixedUpdate()
	{
		if (0f < this.m_fStartDestroyTime)
		{
			return;
		}
		bool flag = false;
		if (this.m_CollisionType == NcAutoDestruct.CollisionType.NONE)
		{
			return;
		}
		if (this.m_CollisionType == NcAutoDestruct.CollisionType.COLLISION)
		{
			if (Physics.CheckSphere(base.get_transform().get_position(), this.m_fCollisionRadius, this.m_CollisionLayer))
			{
				flag = true;
			}
		}
		else if (this.m_CollisionType == NcAutoDestruct.CollisionType.WORLD_Y && base.get_transform().get_position().y <= this.m_fDestructPosY)
		{
			flag = true;
		}
		if (flag)
		{
			this.StartDestroy();
		}
	}

	private void StartDestroy()
	{
		if (this.m_fSmoothDestroyTime <= 0f)
		{
			this.AutoDestruct();
		}
		else
		{
			this.m_fStartDestroyTime = NcEffectBehaviour.GetEngineTime();
			if (this.m_bDisableEmit)
			{
				base.DisableEmit();
			}
		}
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		this.m_fLifeTime /= fSpeedRate;
		this.m_fSmoothDestroyTime /= fSpeedRate;
	}

	private void AutoDestruct()
	{
		Object.DestroyObject(base.get_gameObject());
	}
}
