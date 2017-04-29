using System;
using System.Reflection;
using UnityEngine;

public class NcParticleSystem : NcEffectBehaviour
{
	public enum ParticleDestruct
	{
		NONE,
		COLLISION,
		WORLD_Y
	}

	protected const int m_nAllocBufCount = 50;

	protected bool m_bDisabledEmit;

	public float m_fStartDelayTime;

	public bool m_bBurst;

	public float m_fBurstRepeatTime = 0.5f;

	public int m_nBurstRepeatCount;

	public int m_fBurstEmissionCount = 10;

	public float m_fEmitTime;

	public float m_fSleepTime;

	public bool m_bScaleWithTransform = true;

	public bool m_bWorldSpace = true;

	public float m_fStartSizeRate = 1f;

	public float m_fStartLifeTimeRate = 1f;

	public float m_fStartEmissionRate = 1f;

	public float m_fStartSpeedRate = 1f;

	public float m_fRenderLengthRate = 1f;

	public float m_fLegacyMinMeshNormalVelocity = 10f;

	public float m_fLegacyMaxMeshNormalVelocity = 10f;

	public float m_fShurikenSpeedRate = 1f;

	protected bool m_bStart;

	protected Vector3 m_OldPos = Vector3.get_zero();

	protected bool m_bLegacyRuntimeScale = true;

	public NcParticleSystem.ParticleDestruct m_ParticleDestruct;

	public LayerMask m_CollisionLayer = -1;

	public float m_fCollisionRadius = 0.3f;

	public float m_fDestructPosY = 0.2f;

	public GameObject m_AttachPrefab;

	public float m_fPrefabScale = 1f;

	public float m_fPrefabSpeed = 1f;

	public float m_fPrefabLifeTime = 2f;

	protected bool m_bSleep;

	protected float m_fStartTime;

	protected float m_fDurationStartTime;

	protected float m_fEmitStartTime;

	protected int m_nCreateCount;

	protected bool m_bScalePreRender;

	protected bool m_bMeshParticleEmitter;

	protected ParticleSystem m_ps;

	protected ParticleEmitter m_pe;

	protected ParticleAnimator m_pa;

	protected ParticleRenderer m_pr;

	protected ParticleSystem.Particle[] m_BufPsParts;

	protected ParticleSystem.Particle[] m_BufColliderOriParts;

	protected ParticleSystem.Particle[] m_BufColliderConParts;

	public void SetDisableEmit()
	{
		this.m_bDisabledEmit = true;
	}

	public bool IsShuriken()
	{
		return base.GetComponent<ParticleSystem>() != null;
	}

	public bool IsLegacy()
	{
		return base.GetComponent<ParticleEmitter>() != null && base.GetComponent<ParticleEmitter>().get_enabled();
	}

	public override int GetAnimationState()
	{
		if (!base.get_enabled() || !NcEffectBehaviour.IsActive(base.get_gameObject()))
		{
			return -1;
		}
		if (this.m_bBurst)
		{
			if (0 >= this.m_nBurstRepeatCount)
			{
				return 1;
			}
			if (this.m_nCreateCount < this.m_nBurstRepeatCount)
			{
				return 1;
			}
			return 0;
		}
		else
		{
			if (0f < this.m_fStartDelayTime)
			{
				return 1;
			}
			if (0f >= this.m_fEmitTime || this.m_fSleepTime > 0f)
			{
				return -1;
			}
			if (this.m_nCreateCount < 1)
			{
				return 1;
			}
			return 0;
		}
	}

	public bool IsMeshParticleEmitter()
	{
		return this.m_bMeshParticleEmitter;
	}

	private void Awake()
	{
		if (this.IsShuriken())
		{
			this.m_ps = base.GetComponent<ParticleSystem>();
		}
		else
		{
			this.m_pe = base.GetComponent<ParticleEmitter>();
			this.m_pa = base.GetComponent<ParticleAnimator>();
			this.m_pr = base.GetComponent<ParticleRenderer>();
			if (this.m_pe != null)
			{
				this.m_bMeshParticleEmitter = this.m_pe.ToString().Contains("MeshParticleEmitter");
			}
		}
	}

	private void OnEnable()
	{
		if (this.m_bScaleWithTransform)
		{
			this.AddRenderEventCall();
		}
	}

	private void OnDisable()
	{
		if (this.m_bScaleWithTransform)
		{
			this.RemoveRenderEventCall();
		}
	}

	private void Start()
	{
		this.m_bStart = true;
		if (this.m_bDisabledEmit)
		{
			return;
		}
		this.m_OldPos = base.get_transform().get_position();
		this.m_fDurationStartTime = (this.m_fEmitStartTime = (this.m_fStartTime = NcEffectBehaviour.GetEngineTime()));
		if (this.IsShuriken())
		{
			this.ShurikenInitParticle();
		}
		else
		{
			this.LegacyInitParticle();
		}
		if (this.m_bBurst || 0f < this.m_fStartDelayTime)
		{
			this.SetEnableParticle(false);
		}
	}

	private void Update()
	{
		if (this.m_bDisabledEmit)
		{
			return;
		}
		if (0f < this.m_fStartDelayTime)
		{
			if (this.m_fStartTime + this.m_fStartDelayTime <= NcEffectBehaviour.GetEngineTime())
			{
				this.m_fEmitStartTime = NcEffectBehaviour.GetEngineTime();
				this.m_fDurationStartTime = NcEffectBehaviour.GetEngineTime();
				this.m_fStartDelayTime = 0f;
				this.SetEnableParticle(true);
			}
			return;
		}
		if (this.m_bBurst)
		{
			if (this.m_fDurationStartTime <= NcEffectBehaviour.GetEngineTime())
			{
				if (this.m_nBurstRepeatCount == 0 || this.m_nCreateCount < this.m_nBurstRepeatCount)
				{
					this.m_fDurationStartTime = this.m_fBurstRepeatTime + NcEffectBehaviour.GetEngineTime();
					this.m_nCreateCount++;
					if (this.IsShuriken())
					{
						this.m_ps.Emit(this.m_fBurstEmissionCount);
					}
					else if (this.m_pe != null)
					{
						this.m_pe.Emit(this.m_fBurstEmissionCount);
					}
				}
			}
		}
		else if (this.m_bSleep)
		{
			if (this.m_fEmitStartTime + this.m_fEmitTime + this.m_fSleepTime < NcEffectBehaviour.GetEngineTime())
			{
				this.SetEnableParticle(true);
				this.m_fEmitStartTime = NcEffectBehaviour.GetEngineTime();
				this.m_bSleep = false;
			}
		}
		else if (0f < this.m_fEmitTime && this.m_fEmitStartTime + this.m_fEmitTime < NcEffectBehaviour.GetEngineTime())
		{
			this.m_nCreateCount++;
			this.SetEnableParticle(false);
			if (0f < this.m_fSleepTime)
			{
				this.m_bSleep = true;
			}
			else
			{
				this.m_fEmitTime = 0f;
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.m_ParticleDestruct != NcParticleSystem.ParticleDestruct.NONE)
		{
			bool flag = false;
			if (this.IsShuriken())
			{
				if (this.m_ps != null)
				{
					this.AllocateParticleSystem(ref this.m_BufColliderOriParts);
					this.AllocateParticleSystem(ref this.m_BufColliderConParts);
					this.m_ps.GetParticles(this.m_BufColliderOriParts);
					this.m_ps.GetParticles(this.m_BufColliderConParts);
					this.ShurikenScaleParticle(this.m_BufColliderConParts, this.m_ps.get_particleCount(), this.m_bScaleWithTransform, true);
					for (int i = 0; i < this.m_ps.get_particleCount(); i++)
					{
						bool flag2 = false;
						Vector3 vector;
						if (this.m_bWorldSpace)
						{
							vector = this.m_BufColliderConParts[i].get_position();
						}
						else
						{
							vector = base.get_transform().TransformPoint(this.m_BufColliderConParts[i].get_position());
						}
						if (this.m_ParticleDestruct == NcParticleSystem.ParticleDestruct.COLLISION)
						{
							if (Physics.CheckSphere(vector, this.m_fCollisionRadius, this.m_CollisionLayer))
							{
								flag2 = true;
							}
						}
						else if (this.m_ParticleDestruct == NcParticleSystem.ParticleDestruct.WORLD_Y && vector.y <= this.m_fDestructPosY)
						{
							flag2 = true;
						}
						if (flag2 && 0f < this.m_BufColliderOriParts[i].get_lifetime())
						{
							this.m_BufColliderOriParts[i].set_lifetime(0f);
							flag = true;
							this.CreateAttachPrefab(vector, this.m_BufColliderConParts[i].get_startSize() * this.m_fPrefabScale);
						}
					}
					if (flag)
					{
						this.m_ps.SetParticles(this.m_BufColliderOriParts, this.m_ps.get_particleCount());
					}
				}
			}
			else if (this.m_pe != null)
			{
				Particle[] particles = this.m_pe.get_particles();
				Particle[] particles2 = this.m_pe.get_particles();
				this.LegacyScaleParticle(particles2, this.m_bScaleWithTransform, true);
				for (int j = 0; j < particles2.Length; j++)
				{
					bool flag3 = false;
					Vector3 vector;
					if (this.m_bWorldSpace)
					{
						vector = particles2[j].get_position();
					}
					else
					{
						vector = base.get_transform().TransformPoint(particles2[j].get_position());
					}
					if (this.m_ParticleDestruct == NcParticleSystem.ParticleDestruct.COLLISION)
					{
						if (Physics.CheckSphere(vector, this.m_fCollisionRadius, this.m_CollisionLayer))
						{
							flag3 = true;
						}
					}
					else if (this.m_ParticleDestruct == NcParticleSystem.ParticleDestruct.WORLD_Y && vector.y <= this.m_fDestructPosY)
					{
						flag3 = true;
					}
					if (flag3 && 0f < particles[j].get_energy())
					{
						particles[j].set_energy(0f);
						flag = true;
						this.CreateAttachPrefab(vector, particles2[j].get_size() * this.m_fPrefabScale);
					}
				}
				if (flag)
				{
					this.m_pe.set_particles(particles);
				}
			}
		}
	}

	private void OnPreRender()
	{
		if (!this.m_bStart)
		{
			return;
		}
		if (this.m_bScaleWithTransform)
		{
			this.m_bScalePreRender = true;
			if (this.IsShuriken())
			{
				this.ShurikenSetRuntimeParticleScale(true);
			}
			else
			{
				this.LegacySetRuntimeParticleScale(true);
			}
		}
	}

	private void OnPostRender()
	{
		if (!this.m_bStart)
		{
			return;
		}
		if (this.m_bScalePreRender)
		{
			if (this.IsShuriken())
			{
				this.ShurikenSetRuntimeParticleScale(false);
			}
			else
			{
				this.LegacySetRuntimeParticleScale(false);
			}
		}
		this.m_OldPos = base.get_transform().get_position();
		this.m_bScalePreRender = false;
	}

	private void CreateAttachPrefab(Vector3 position, float size)
	{
		if (this.m_AttachPrefab == null)
		{
			return;
		}
		GameObject gameObject = base.CreateGameObject(this.m_AttachPrefab, this.m_AttachPrefab.get_transform().get_position() + position, this.m_AttachPrefab.get_transform().get_rotation());
		if (gameObject == null)
		{
			return;
		}
		base.ChangeParent(NcEffectBehaviour.GetRootInstanceEffect().get_transform(), gameObject.get_transform(), false, null);
		NcTransformTool.CopyLossyToLocalScale(gameObject.get_transform().get_lossyScale() * size, gameObject.get_transform());
		NcEffectBehaviour.AdjustSpeedRuntime(gameObject, this.m_fPrefabSpeed);
		if (0f < this.m_fPrefabLifeTime)
		{
			NcAutoDestruct ncAutoDestruct = gameObject.GetComponent<NcAutoDestruct>();
			if (ncAutoDestruct == null)
			{
				ncAutoDestruct = gameObject.AddComponent<NcAutoDestruct>();
			}
			ncAutoDestruct.m_fLifeTime = this.m_fPrefabLifeTime;
		}
	}

	private void AddRenderEventCall()
	{
		Camera[] allCameras = Camera.get_allCameras();
		for (int i = 0; i < allCameras.Length; i++)
		{
			Camera camera = allCameras[i];
			NsRenderManager nsRenderManager = camera.GetComponent<NsRenderManager>();
			if (nsRenderManager == null)
			{
				nsRenderManager = camera.get_gameObject().AddComponent<NsRenderManager>();
			}
			nsRenderManager.AddRenderEventCall(this);
		}
	}

	private void RemoveRenderEventCall()
	{
		Camera[] allCameras = Camera.get_allCameras();
		for (int i = 0; i < allCameras.Length; i++)
		{
			Camera camera = allCameras[i];
			NsRenderManager component = camera.GetComponent<NsRenderManager>();
			if (component != null)
			{
				component.RemoveRenderEventCall(this);
			}
		}
	}

	private void SetEnableParticle(bool bEnable)
	{
		if (this.m_ps != null)
		{
			this.m_ps.set_enableEmission(bEnable);
		}
		if (this.m_pe != null)
		{
			this.m_pe.set_emit(bEnable);
		}
	}

	public float GetScaleMinMeshNormalVelocity()
	{
		return this.m_fLegacyMinMeshNormalVelocity * ((!this.m_bScaleWithTransform) ? 1f : NcTransformTool.GetTransformScaleMeanValue(base.get_transform()));
	}

	public float GetScaleMaxMeshNormalVelocity()
	{
		return this.m_fLegacyMaxMeshNormalVelocity * ((!this.m_bScaleWithTransform) ? 1f : NcTransformTool.GetTransformScaleMeanValue(base.get_transform()));
	}

	private void LegacyInitParticle()
	{
		if (this.m_pe != null)
		{
			this.LegacySetParticle();
		}
	}

	private void LegacySetParticle()
	{
		ParticleEmitter pe = this.m_pe;
		ParticleAnimator pa = this.m_pa;
		ParticleRenderer pr = this.m_pr;
		if (pe == null || pr == null)
		{
			return;
		}
		if (this.m_bLegacyRuntimeScale)
		{
			Vector3 vector = Vector3.get_one() * this.m_fStartSpeedRate;
			float fStartSpeedRate = this.m_fStartSpeedRate;
			ParticleEmitter expr_53 = pe;
			expr_53.set_minSize(expr_53.get_minSize() * this.m_fStartSizeRate);
			ParticleEmitter expr_66 = pe;
			expr_66.set_maxSize(expr_66.get_maxSize() * this.m_fStartSizeRate);
			ParticleEmitter expr_79 = pe;
			expr_79.set_minEnergy(expr_79.get_minEnergy() * this.m_fStartLifeTimeRate);
			ParticleEmitter expr_8C = pe;
			expr_8C.set_maxEnergy(expr_8C.get_maxEnergy() * this.m_fStartLifeTimeRate);
			ParticleEmitter expr_9F = pe;
			expr_9F.set_minEmission(expr_9F.get_minEmission() * this.m_fStartEmissionRate);
			ParticleEmitter expr_B2 = pe;
			expr_B2.set_maxEmission(expr_B2.get_maxEmission() * this.m_fStartEmissionRate);
			pe.set_worldVelocity(Vector3.Scale(pe.get_worldVelocity(), vector));
			pe.set_localVelocity(Vector3.Scale(pe.get_localVelocity(), vector));
			pe.set_rndVelocity(Vector3.Scale(pe.get_rndVelocity(), vector));
			ParticleEmitter expr_FB = pe;
			expr_FB.set_angularVelocity(expr_FB.get_angularVelocity() * fStartSpeedRate);
			ParticleEmitter expr_10A = pe;
			expr_10A.set_rndAngularVelocity(expr_10A.get_rndAngularVelocity() * fStartSpeedRate);
			ParticleEmitter expr_119 = pe;
			expr_119.set_emitterVelocityScale(expr_119.get_emitterVelocityScale() * fStartSpeedRate);
			if (pa != null)
			{
				pa.set_rndForce(Vector3.Scale(pa.get_rndForce(), vector));
				pa.set_force(Vector3.Scale(pa.get_force(), vector));
			}
			ParticleRenderer expr_158 = pr;
			expr_158.set_lengthScale(expr_158.get_lengthScale() * this.m_fRenderLengthRate);
		}
		else
		{
			Vector3 vector2 = ((!this.m_bScaleWithTransform) ? Vector3.get_one() : pe.get_transform().get_lossyScale()) * this.m_fStartSpeedRate;
			float num = ((!this.m_bScaleWithTransform) ? 1f : NcTransformTool.GetTransformScaleMeanValue(pe.get_transform())) * this.m_fStartSpeedRate;
			float num2 = ((!this.m_bScaleWithTransform) ? 1f : NcTransformTool.GetTransformScaleMeanValue(pe.get_transform())) * this.m_fStartSizeRate;
			ParticleEmitter expr_1EF = pe;
			expr_1EF.set_minSize(expr_1EF.get_minSize() * num2);
			ParticleEmitter expr_1FE = pe;
			expr_1FE.set_maxSize(expr_1FE.get_maxSize() * num2);
			ParticleEmitter expr_20D = pe;
			expr_20D.set_minEnergy(expr_20D.get_minEnergy() * this.m_fStartLifeTimeRate);
			ParticleEmitter expr_220 = pe;
			expr_220.set_maxEnergy(expr_220.get_maxEnergy() * this.m_fStartLifeTimeRate);
			ParticleEmitter expr_233 = pe;
			expr_233.set_minEmission(expr_233.get_minEmission() * this.m_fStartEmissionRate);
			ParticleEmitter expr_246 = pe;
			expr_246.set_maxEmission(expr_246.get_maxEmission() * this.m_fStartEmissionRate);
			pe.set_worldVelocity(Vector3.Scale(pe.get_worldVelocity(), vector2));
			pe.set_localVelocity(Vector3.Scale(pe.get_localVelocity(), vector2));
			pe.set_rndVelocity(Vector3.Scale(pe.get_rndVelocity(), vector2));
			ParticleEmitter expr_292 = pe;
			expr_292.set_angularVelocity(expr_292.get_angularVelocity() * num);
			ParticleEmitter expr_2A1 = pe;
			expr_2A1.set_rndAngularVelocity(expr_2A1.get_rndAngularVelocity() * num);
			ParticleEmitter expr_2B0 = pe;
			expr_2B0.set_emitterVelocityScale(expr_2B0.get_emitterVelocityScale() * num);
			if (pa != null)
			{
				pa.set_rndForce(Vector3.Scale(pa.get_rndForce(), vector2));
				pa.set_force(Vector3.Scale(pa.get_force(), vector2));
			}
			ParticleRenderer expr_2F1 = pr;
			expr_2F1.set_lengthScale(expr_2F1.get_lengthScale() * this.m_fRenderLengthRate);
		}
	}

	private void LegacyParticleSpeed(float fSpeed)
	{
		ParticleEmitter pe = this.m_pe;
		ParticleAnimator pa = this.m_pa;
		ParticleRenderer pr = this.m_pr;
		if (pe == null || pr == null)
		{
			return;
		}
		Vector3 vector = Vector3.get_one() * fSpeed;
		ParticleEmitter expr_3B = pe;
		expr_3B.set_minEnergy(expr_3B.get_minEnergy() / fSpeed);
		ParticleEmitter expr_49 = pe;
		expr_49.set_maxEnergy(expr_49.get_maxEnergy() / fSpeed);
		pe.set_worldVelocity(Vector3.Scale(pe.get_worldVelocity(), vector));
		pe.set_localVelocity(Vector3.Scale(pe.get_localVelocity(), vector));
		pe.set_rndVelocity(Vector3.Scale(pe.get_rndVelocity(), vector));
		ParticleEmitter expr_8D = pe;
		expr_8D.set_angularVelocity(expr_8D.get_angularVelocity() * fSpeed);
		ParticleEmitter expr_9B = pe;
		expr_9B.set_rndAngularVelocity(expr_9B.get_rndAngularVelocity() * fSpeed);
		ParticleEmitter expr_A9 = pe;
		expr_A9.set_emitterVelocityScale(expr_A9.get_emitterVelocityScale() * fSpeed);
		if (pa != null)
		{
			pa.set_rndForce(Vector3.Scale(pa.get_rndForce(), vector));
			pa.set_force(Vector3.Scale(pa.get_force(), vector));
		}
	}

	private void LegacySetRuntimeParticleScale(bool bScale)
	{
		if (!this.m_bLegacyRuntimeScale)
		{
			return;
		}
		if (this.m_pe != null)
		{
			Particle[] particles = this.m_pe.get_particles();
			this.m_pe.set_particles(this.LegacyScaleParticle(particles, bScale, true));
		}
	}

	public Particle[] LegacyScaleParticle(Particle[] parts, bool bScale, bool bPosUpdate)
	{
		float num;
		if (bScale)
		{
			num = NcTransformTool.GetTransformScaleMeanValue(base.get_transform());
		}
		else
		{
			num = 1f / NcTransformTool.GetTransformScaleMeanValue(base.get_transform());
		}
		for (int i = 0; i < parts.Length; i++)
		{
			if (!this.IsMeshParticleEmitter())
			{
				if (this.m_bWorldSpace)
				{
					if (bPosUpdate)
					{
						Vector3 vector = this.m_OldPos - base.get_transform().get_position();
						if (bScale)
						{
							int expr_70_cp_1 = i;
							parts[expr_70_cp_1].set_position(parts[expr_70_cp_1].get_position() - vector * (1f - 1f / num));
						}
					}
					int expr_9A_cp_1 = i;
					parts[expr_9A_cp_1].set_position(parts[expr_9A_cp_1].get_position() - base.get_transform().get_position());
					int expr_BC_cp_1 = i;
					parts[expr_BC_cp_1].set_position(parts[expr_BC_cp_1].get_position() * num);
					int expr_D4_cp_1 = i;
					parts[expr_D4_cp_1].set_position(parts[expr_D4_cp_1].get_position() + base.get_transform().get_position());
				}
				else
				{
					int expr_FB_cp_1 = i;
					parts[expr_FB_cp_1].set_position(parts[expr_FB_cp_1].get_position() * num);
				}
			}
			int expr_113_cp_1 = i;
			parts[expr_113_cp_1].set_angularVelocity(parts[expr_113_cp_1].get_angularVelocity() * num);
			int expr_127_cp_1 = i;
			parts[expr_127_cp_1].set_velocity(parts[expr_127_cp_1].get_velocity() * num);
			int expr_13F_cp_1 = i;
			parts[expr_13F_cp_1].set_size(parts[expr_13F_cp_1].get_size() * num);
		}
		return parts;
	}

	private void ShurikenInitParticle()
	{
		if (this.m_ps != null)
		{
			ParticleSystem expr_17 = this.m_ps;
			expr_17.set_startSize(expr_17.get_startSize() * this.m_fStartSizeRate);
			ParticleSystem expr_2F = this.m_ps;
			expr_2F.set_startLifetime(expr_2F.get_startLifetime() * this.m_fStartLifeTimeRate);
			ParticleSystem expr_47 = this.m_ps;
			expr_47.set_emissionRate(expr_47.get_emissionRate() * this.m_fStartEmissionRate);
			ParticleSystem expr_5F = this.m_ps;
			expr_5F.set_startSpeed(expr_5F.get_startSpeed() * this.m_fStartSpeedRate);
			ParticleSystemRenderer component = base.GetComponent<ParticleSystemRenderer>();
			if (component != null)
			{
				float num = (float)NcParticleSystem.Ng_GetProperty(component, "lengthScale");
				NcParticleSystem.Ng_SetProperty(component, "lengthScale", num * this.m_fRenderLengthRate);
			}
		}
	}

	private void AllocateParticleSystem(ref ParticleSystem.Particle[] tmpPsParts)
	{
		if (tmpPsParts == null || tmpPsParts.Length < this.m_ps.get_particleCount())
		{
			tmpPsParts = new ParticleSystem.Particle[this.m_ps.get_particleCount() + 50];
		}
	}

	private void ShurikenSetRuntimeParticleScale(bool bScale)
	{
		if (this.m_ps != null)
		{
			this.AllocateParticleSystem(ref this.m_BufPsParts);
			this.m_ps.GetParticles(this.m_BufPsParts);
			this.m_BufPsParts = this.ShurikenScaleParticle(this.m_BufPsParts, this.m_ps.get_particleCount(), bScale, true);
			this.m_ps.SetParticles(this.m_BufPsParts, this.m_ps.get_particleCount());
		}
	}

	public ParticleSystem.Particle[] ShurikenScaleParticle(ParticleSystem.Particle[] parts, int nCount, bool bScale, bool bPosUpdate)
	{
		float num;
		if (bScale)
		{
			num = NcTransformTool.GetTransformScaleMeanValue(base.get_transform());
		}
		else
		{
			num = 1f / NcTransformTool.GetTransformScaleMeanValue(base.get_transform());
		}
		for (int i = 0; i < nCount; i++)
		{
			if (this.m_bWorldSpace)
			{
				if (bPosUpdate)
				{
					Vector3 vector = this.m_OldPos - base.get_transform().get_position();
					if (bScale)
					{
						int expr_66_cp_1 = i;
						parts[expr_66_cp_1].set_position(parts[expr_66_cp_1].get_position() - vector * (1f - 1f / num));
					}
				}
				int expr_90_cp_1 = i;
				parts[expr_90_cp_1].set_position(parts[expr_90_cp_1].get_position() - base.get_transform().get_position());
				int expr_B2_cp_1 = i;
				parts[expr_B2_cp_1].set_position(parts[expr_B2_cp_1].get_position() * num);
				int expr_CA_cp_1 = i;
				parts[expr_CA_cp_1].set_position(parts[expr_CA_cp_1].get_position() + base.get_transform().get_position());
			}
			else
			{
				int expr_F1_cp_1 = i;
				parts[expr_F1_cp_1].set_position(parts[expr_F1_cp_1].get_position() * num);
			}
			int expr_109_cp_1 = i;
			parts[expr_109_cp_1].set_startSize(parts[expr_109_cp_1].get_startSize() * num);
		}
		return parts;
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		this.m_fStartDelayTime /= fSpeedRate;
		this.m_fBurstRepeatTime /= fSpeedRate;
		this.m_fEmitTime /= fSpeedRate;
		this.m_fSleepTime /= fSpeedRate;
		this.m_fShurikenSpeedRate *= fSpeedRate;
		this.LegacyParticleSpeed(fSpeedRate);
		this.m_fPrefabLifeTime /= fSpeedRate;
		this.m_fPrefabSpeed *= fSpeedRate;
	}

	public static void Ng_SetProperty(object srcObj, string fieldName, object newValue)
	{
		PropertyInfo property = srcObj.GetType().GetProperty(fieldName, 52);
		if (property != null && property.get_CanWrite())
		{
			property.SetValue(srcObj, newValue, null);
		}
		else
		{
			Debuger.Warning(property.get_Name() + " could not be write.", new object[0]);
		}
	}

	public static object Ng_GetProperty(object srcObj, string fieldName)
	{
		object result = null;
		PropertyInfo property = srcObj.GetType().GetProperty(fieldName, 52);
		if (property != null && property.get_CanRead() && property.GetIndexParameters().Length == 0)
		{
			result = property.GetValue(srcObj, null);
		}
		else
		{
			Debuger.Warning(property.get_Name() + " could not be read.", new object[0]);
		}
		return result;
	}
}
