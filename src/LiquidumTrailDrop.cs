using System;
using UnityEngine;

public class LiquidumTrailDrop : MonoBehaviour
{
	private float speed;

	private float MyTimer;

	private float YSpeed;

	private float XSpeed;

	private float TmpYSpeed;

	private float Ran;

	private float updateInterval = 0.2f;

	private TrailRenderer ThisTrailRender;

	private TrailRenderer coda;

	private TrailRenderer sfondo;

	private float scale;

	private float RanScale = 1f;

	private float GoYSpeed = 1f;

	private float GoXSpeed = 1f;

	[W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to false for best performance.")]
	public bool ChangeAtRunTime;

	[W_ToolTip("-Use this bool to add a additional background trial.\n-Set to false for best performance.")]
	public bool UsebackgroundTrial;

	private void Start()
	{
		this.ThisTrailRender = base.GetComponent<TrailRenderer>();
		this.scale = Liquidum.Instance.TrailScale / 100f;
		Color color = Liquidum.Instance.TrailsColor / 5f;
		Color color2 = color / 30f + new Color(0.02f, 0.02f, 0.02f, 0.02f);
		this.coda = base.get_transform().GetChild(0).GetComponentInChildren<TrailRenderer>();
		this.coda.GetComponent<Renderer>().get_material().SetColor("_Color", color);
		this.coda.GetComponent<Renderer>().get_material().SetFloat("_BumpAmt", Liquidum.Instance.TrailDistortion * 8f);
		if (this.sfondo != null)
		{
			this.sfondo = base.get_transform().GetChild(1).GetComponentInChildren<TrailRenderer>();
			this.sfondo.GetComponent<Renderer>().get_material().SetColor("_Color", color2);
			this.sfondo.GetComponent<Renderer>().get_material().SetFloat("_BumpAmt", Liquidum.Instance.TrailDistortion * 5f);
		}
		this.TrialUpdate();
	}

	private void TrialUpdate()
	{
		this.ThisTrailRender.set_startWidth(this.scale * this.RanScale);
		this.coda.set_startWidth(this.scale * this.RanScale * 0.6f);
		if (this.sfondo)
		{
			this.sfondo.set_startWidth(this.scale * this.RanScale * 3f);
		}
		this.ThisTrailRender.set_time(Liquidum.Instance.TrialTail / 5f);
		this.coda.set_time(Liquidum.Instance.TrialTail * 1.5f);
		if (this.sfondo)
		{
			this.sfondo.set_time(Liquidum.Instance.TrialTail * 3f);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.set_color(Color.get_cyan());
		Gizmos.DrawWireSphere(base.get_transform().get_position(), 0.001f + base.get_transform().get_localScale().get_magnitude() / 25f);
	}

	private void Update()
	{
		this.speed = Liquidum.Instance.TrailSlipSpeed;
		this.MyTimer += Time.get_deltaTime();
		if (this.MyTimer > this.updateInterval)
		{
			this.TmpYSpeed = Random.Range(1f, this.speed) * Time.get_deltaTime();
			this.XSpeed = Random.Range(-0.05f, 0.05f);
			this.Ran = Random.Range(0f, 1f);
			if ((double)this.YSpeed < 0.001)
			{
				this.RanScale += 0.5f * Time.get_deltaTime();
				this.XSpeed += 1.5f * Time.get_deltaTime();
			}
			else
			{
				this.RanScale -= 0.02f * Time.get_deltaTime();
				this.XSpeed -= 1.5f * Time.get_deltaTime();
			}
			this.RanScale = Mathf.Clamp(this.RanScale, 1f, 2f);
			if (this.ChangeAtRunTime)
			{
				this.TrialUpdate();
			}
			this.MyTimer = 0f;
		}
		this.YSpeed = Mathf.Lerp(this.YSpeed, this.TmpYSpeed, 0.02f * Time.get_deltaTime());
		if (this.Ran < Liquidum.Instance.TrailDropsFriction)
		{
			this.GoYSpeed = this.YSpeed / 2f;
			this.GoXSpeed = this.XSpeed * Time.get_deltaTime();
		}
		else
		{
			this.GoYSpeed = this.YSpeed * 3f;
			this.GoXSpeed = this.XSpeed / 1.5f * Time.get_deltaTime();
		}
		if (Time.get_timeScale() > 0.01f)
		{
			Transform expr_1C9 = base.get_transform();
			expr_1C9.set_position(expr_1C9.get_position() - base.get_transform().get_up() * this.GoYSpeed);
			Transform expr_1F5 = base.get_transform();
			expr_1F5.set_position(expr_1F5.get_position() + base.get_transform().get_right() * (this.GoXSpeed + Liquidum.Instance.TrailConstantAngle / 100f));
		}
		if (base.get_transform().get_localPosition().y < -3f * Liquidum.Instance.TrialTail)
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}
