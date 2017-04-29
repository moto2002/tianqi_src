using System;
using UnityEngine;

public class DissolveMat : MonoBehaviour
{
	public float StartAmount;

	public float EndAmount = 1f;

	private bool IsAnimating;

	private ParticleSystem mParticleSystem;

	private Material mMaterial;

	private float mStartDelay;

	private float mStartLifetime;

	private float mDeltaTime;

	private void OnEnable()
	{
		this.mParticleSystem = base.get_transform().GetComponent<ParticleSystem>();
		this.mMaterial = this.mParticleSystem.GetComponent<Renderer>().get_material();
		this.mStartDelay = this.mParticleSystem.get_startDelay();
		this.mStartLifetime = this.mParticleSystem.get_startLifetime();
		this.Init();
		base.Invoke("StartAnimating", this.mStartDelay);
	}

	private void Init()
	{
		this.IsAnimating = false;
		this.mDeltaTime = 0f;
	}

	private void StartAnimating()
	{
		this.IsAnimating = true;
	}

	private void Update()
	{
		if (this.IsAnimating)
		{
			float num = this.CalAmount();
			this.mMaterial.SetFloat(ShaderPIDManager._Amount, Mathf.Min(num, this.EndAmount));
			if (num > this.EndAmount)
			{
				this.IsAnimating = false;
			}
		}
	}

	private float CalAmount()
	{
		this.mDeltaTime += Time.get_deltaTime();
		float num = this.mDeltaTime / this.mStartLifetime;
		float num2 = (this.EndAmount - this.StartAmount) * num;
		return this.StartAmount + num2;
	}
}
