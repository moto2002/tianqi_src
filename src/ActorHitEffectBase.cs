using System;
using UnityEngine;

public abstract class ActorHitEffectBase : MonoBehaviour
{
	private const string AttributeName = "_HitStrength";

	protected float SrcPower;

	protected float DstPower;

	protected float Rate;

	protected float m_fCurrentPower;

	protected bool m_bPlaying;

	protected bool m_bForward = true;

	public Renderer m_hitRenderer;

	private float Circle;

	private float m_fCurrentTime;

	public virtual void Init(Renderer hitRenderer)
	{
		if (hitRenderer == null)
		{
			return;
		}
		this.m_hitRenderer = hitRenderer;
		this.Circle = 1f / this.Rate;
		this.SetHitValue(this.SrcPower);
	}

	public void SetActorHitEffect()
	{
		if (this.m_hitRenderer == null)
		{
			return;
		}
		if (this.m_bPlaying)
		{
			return;
		}
		this.m_bPlaying = true;
		this.m_bForward = true;
		this.m_fCurrentPower = this.SrcPower;
	}

	private void SetHitValue(float value)
	{
		if (this.m_hitRenderer == null && this.m_hitRenderer.get_material())
		{
			return;
		}
		ActorHitEffectBase.SetHitValue(this.m_hitRenderer.get_material(), value);
	}

	public static void SetHitValue(Material hitMaterial, float value)
	{
		if (hitMaterial.HasProperty("_HitStrength"))
		{
			hitMaterial.SetFloat("_HitStrength", value);
		}
	}

	private void Update()
	{
		if (this.m_bPlaying)
		{
			if (this.m_bForward)
			{
				this.SrcToDst();
			}
			else
			{
				this.DstToSrc();
			}
		}
	}

	private void SrcToDst()
	{
		this.m_fCurrentPower = Mathf.Lerp(this.m_fCurrentPower, this.DstPower, Time.get_deltaTime() * this.Rate);
		if (this.IsTimeOver())
		{
			this.m_bForward = false;
			this.SetHitValue(this.DstPower);
		}
		else
		{
			this.SetHitValue(this.m_fCurrentPower);
		}
	}

	private void DstToSrc()
	{
		this.m_fCurrentPower = Mathf.Lerp(this.m_fCurrentPower, this.SrcPower, Time.get_deltaTime() * this.Rate);
		if (this.IsTimeOver())
		{
			this.m_bPlaying = false;
			this.SetHitValue(this.SrcPower);
		}
		else
		{
			this.SetHitValue(this.m_fCurrentPower);
		}
	}

	private bool IsTimeOver()
	{
		this.m_fCurrentTime += Time.get_deltaTime();
		if (this.m_fCurrentTime >= this.Circle)
		{
			this.m_fCurrentTime = 0f;
			return true;
		}
		return false;
	}
}
