using System;
using UnityEngine;

public class Liquidum_TriggerDistortion : MonoBehaviour
{
	private Liquidum LiquidumScript;

	private bool fadeO = true;

	private bool fadeI;

	private Color TargetColor;

	private void Start()
	{
		this.LiquidumScript = Liquidum.Instance;
		this.TargetColor = new Color(0f, 0f, 0f, 0f);
		this.fadeO = true;
		if (!this.LiquidumScript.TriggerDistortionActive)
		{
			Object.Destroy(base.get_gameObject());
		}
	}

	public void FadeOut()
	{
		this.fadeO = true;
		this.fadeI = false;
		this.TargetColor = Color.Lerp(base.GetComponent<Renderer>().get_material().GetColor("_Color"), new Color(0f, 0f, 0f, 0f), this.LiquidumScript.TriggerDistortionFadeOutSpeed * Time.get_deltaTime() * 2f);
		if (this.TargetColor.a < 0.01f)
		{
			this.fadeO = false;
		}
	}

	public void FadeIn()
	{
		this.fadeI = true;
		this.fadeO = false;
		this.TargetColor = Color.Lerp(base.GetComponent<Renderer>().get_material().GetColor("_Color"), this.LiquidumScript.TriggerDistortionColor, this.LiquidumScript.TriggerDistortionFadeInSpeed * Time.get_deltaTime() * 2f);
		if (this.TargetColor.a >= this.LiquidumScript.TriggerDistortionColor.a - 0.01f)
		{
			this.fadeI = false;
		}
	}

	private void Update()
	{
		float num = Time.get_time() / 3f;
		base.GetComponent<Renderer>().get_materials()[0].set_mainTextureOffset(new Vector2(0f, -num));
		base.GetComponent<Renderer>().get_materials()[0].SetTextureOffset("_BumpMap", new Vector2(0f, -num));
		if (this.fadeI || this.fadeO)
		{
			base.GetComponent<Renderer>().get_material().SetColor("_Color", this.TargetColor);
		}
		else if (this.TargetColor.a < 0.01f)
		{
			base.GetComponent<Renderer>().get_material().SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		}
		else
		{
			base.GetComponent<Renderer>().get_material().SetColor("_Color", this.LiquidumScript.TriggerDistortionColor);
		}
		base.GetComponent<Renderer>().get_material().SetFloat("_BumpAmt", this.LiquidumScript.TriggerDistortionPower);
		base.GetComponent<Renderer>().get_materials()[0].set_mainTextureOffset(new Vector2(0f, -num));
		base.GetComponent<Renderer>().get_materials()[0].SetTextureOffset("_BumpMap", new Vector2(0f, -num));
		if (this.fadeO)
		{
			this.FadeOut();
		}
		if (this.fadeI)
		{
			this.FadeIn();
		}
	}
}
