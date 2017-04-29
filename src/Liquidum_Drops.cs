using System;
using UnityEngine;

public class Liquidum_Drops : MonoBehaviour
{
	private Liquidum LiquidumScript;

	private float FadeSpeed;

	private int NumDropFrames;

	private float frame;

	private Vector3 DropsScale;

	private float RandomS;

	private Color DropsColor;

	private float Distortion;

	private bool ClearingNow;

	private void Awake()
	{
	}

	private void Start()
	{
		this.LiquidumScript = Liquidum.Instance;
		this.NumDropFrames = this.LiquidumScript.NumDropFrames;
		this.DropsScale = new Vector3(this.LiquidumScript.DropsScale.x * 10f, 1f, this.LiquidumScript.DropsScale.y * 10f);
		this.DropsColor = this.LiquidumScript.DropsColor;
		this.Distortion = this.LiquidumScript.Distortion * 5f;
		this.frame = (float)Random.Range(0, this.NumDropFrames);
		base.GetComponent<Renderer>().get_material().SetTextureOffset("_MainTex", new Vector2(this.frame / 10f, 0.1f));
		base.GetComponent<Renderer>().get_material().SetTextureOffset("_BumpMap", new Vector2(this.frame / 10f, 0.1f));
		base.GetComponent<Renderer>().get_material().SetColor("_Color", this.DropsColor);
		base.GetComponent<Renderer>().get_material().SetFloat("_BumpAmt", this.Distortion);
		base.get_transform().set_localScale(this.DropsScale);
		if (this.LiquidumScript.RandomScale)
		{
			float num = Random.Range(0.8f, 2f);
			base.get_transform().set_localScale(this.DropsScale * num);
		}
		if (this.LiquidumScript.RandomSpeed)
		{
			this.RandomS = Random.Range(1f, 15f);
		}
		else
		{
			this.RandomS = 1f;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.set_color(Color.get_gray());
		Gizmos.DrawWireSphere(base.get_transform().get_position(), 0.001f + base.get_transform().get_localScale().get_magnitude() / 200f);
	}

	private void Randomization()
	{
		base.get_transform().set_localScale(new Vector3(base.get_transform().get_localScale().x - Time.get_deltaTime(), base.get_transform().get_localScale().y, base.get_transform().get_localScale().z + Time.get_deltaTime()));
	}

	private void Update()
	{
		this.FadeDrops();
		if (this.LiquidumScript.DropSlipSpeed > 0f)
		{
			this.SlipDrops();
		}
		if (this.ClearingNow)
		{
			this.ClearImmediate();
		}
	}

	private void FadeDrops()
	{
		Color color = Color.Lerp(base.GetComponent<Renderer>().get_material().GetColor("_Color"), Color.get_clear(), this.LiquidumScript.DropFadeSpeed * Time.get_deltaTime());
		base.GetComponent<Renderer>().get_material().SetColor("_Color", color);
		if ((double)color.a < 0.001 || base.get_transform().get_localPosition().y < -1.2f)
		{
			Object.Destroy(base.get_gameObject());
		}
	}

	private void SlipDrops()
	{
		base.get_transform().set_position(Vector3.Lerp(base.get_transform().get_position(), new Vector3(base.get_transform().get_position().x, -0.1f, base.get_transform().get_position().z), this.RandomS * this.LiquidumScript.DropSlipSpeed * Time.get_deltaTime() / 20000f));
	}

	public void ClearImmediate()
	{
		this.ClearingNow = true;
		Color color = Color.Lerp(base.GetComponent<Renderer>().get_material().GetColor("_Color"), Color.get_clear(), 2f * Time.get_deltaTime());
		base.GetComponent<Renderer>().get_material().SetColor("_Color", color);
		if ((double)color.a < 0.001)
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}
