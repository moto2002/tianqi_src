using System;
using UnityEngine;

public class LineRendererBehaviour : MonoBehaviour
{
	public bool IsVertical;

	public float LightHeightOffset = 0.3f;

	public float ParticlesHeightOffset = 0.2f;

	public float TimeDestroyLightAfterCollision = 4f;

	public float TimeDestroyThisAfterCollision = 4f;

	public float TimeDestroyRootAfterCollision = 4f;

	public GameObject EffectOnHitObject;

	public GameObject Explosion;

	public GameObject StartGlow;

	public GameObject HitGlow;

	public GameObject Particles;

	public GameObject GoLight;

	private EffectSettings effectSettings;

	private Transform tRoot;

	private Transform tTarget;

	private bool isInitializedOnStart;

	private LineRenderer line;

	private int currentShaderIndex;

	private RaycastHit hit;

	private void Start()
	{
		this.GetEffectSettingsComponent(base.get_transform());
		if (this.effectSettings == null)
		{
			Debuger.Info("Prefab root have not script \"PrefabSettings\"", new object[0]);
		}
		this.tRoot = this.effectSettings.get_transform();
		this.line = base.GetComponent<LineRenderer>();
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	private void InitializeDefault()
	{
		base.GetComponent<Renderer>().get_material().SetFloat(ShaderPIDManager._Chanel, (float)this.currentShaderIndex);
		this.currentShaderIndex++;
		if (this.currentShaderIndex == 3)
		{
			this.currentShaderIndex = 0;
		}
		this.line.SetPosition(0, this.tRoot.get_position());
		if (this.IsVertical)
		{
			if (Physics.Raycast(this.tRoot.get_position(), Vector3.get_down(), ref this.hit))
			{
				this.line.SetPosition(1, this.hit.get_point());
				if (this.StartGlow != null)
				{
					this.StartGlow.get_transform().set_position(this.tRoot.get_position());
				}
				if (this.HitGlow != null)
				{
					this.HitGlow.get_transform().set_position(this.hit.get_point());
				}
				if (this.GoLight != null)
				{
					this.GoLight.get_transform().set_position(this.hit.get_point() + new Vector3(0f, this.LightHeightOffset, 0f));
				}
				if (this.Particles != null)
				{
					this.Particles.get_transform().set_position(this.hit.get_point() + new Vector3(0f, this.ParticlesHeightOffset, 0f));
				}
				if (this.Explosion != null)
				{
					this.Explosion.get_transform().set_position(this.hit.get_point() + new Vector3(0f, this.ParticlesHeightOffset, 0f));
				}
			}
		}
		else
		{
			this.tTarget = this.effectSettings.Target.get_transform();
			Vector3 normalized = (this.tTarget.get_position() - this.tRoot.get_position()).get_normalized();
			Vector3 vector = this.tRoot.get_position() + normalized * this.effectSettings.MoveDistance;
			if (Physics.Raycast(this.tRoot.get_position(), normalized, ref this.hit, this.effectSettings.MoveDistance + 1f, this.effectSettings.LayerMask))
			{
				vector = (this.tRoot.get_position() + Vector3.Normalize(this.hit.get_point() - this.tRoot.get_position()) * (this.effectSettings.MoveDistance + 1f)).get_normalized();
			}
			this.line.SetPosition(1, this.hit.get_point() - this.effectSettings.ColliderRadius * vector);
			Vector3 position = this.hit.get_point() - vector * this.ParticlesHeightOffset;
			if (this.StartGlow != null)
			{
				this.StartGlow.get_transform().set_position(this.tRoot.get_position());
			}
			if (this.HitGlow != null)
			{
				this.HitGlow.get_transform().set_position(position);
			}
			if (this.GoLight != null)
			{
				this.GoLight.get_transform().set_position(this.hit.get_point() - vector * this.LightHeightOffset);
			}
			if (this.Particles != null)
			{
				this.Particles.get_transform().set_position(position);
			}
			if (this.Explosion != null)
			{
				this.Explosion.get_transform().set_position(position);
			}
		}
		CollisionInfo e = new CollisionInfo
		{
			Hit = this.hit
		};
		this.effectSettings.OnCollisionHandler(e);
		if (this.hit.get_transform() != null)
		{
			ShieldCollisionBehaviour component = this.hit.get_transform().GetComponent<ShieldCollisionBehaviour>();
			if (component != null)
			{
				component.ShieldCollisionEnter(e);
			}
		}
	}

	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.get_parent();
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.get_transform());
			}
		}
	}
}
