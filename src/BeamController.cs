using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BeamController : PostProcessBase
{
	private Material m_matBC;

	private List<Material> EffectMaterials = new List<Material>();

	private string BeanName = "beam";

	private Texture2D m_texBean;

	private float EffectLength;

	private Texture2D TexBean
	{
		get
		{
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texBean, this.BeanName);
			return this.m_texBean;
		}
	}

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hsh(Mobile)/Character/Disintegrate"))
		{
			this.m_shaderNames.Add("Hsh(Mobile)/Character/Disintegrate");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matBC = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
	}

	private void SetMaterialParms(float amount)
	{
		for (int i = 0; i < this.EffectMaterials.get_Count(); i++)
		{
			this.EffectMaterials.get_Item(i).SetFloat(ShaderPIDManager._DisintegrateAmount, amount);
		}
	}

	public void Beam(Transform tran, bool isIn)
	{
		this.EffectLength = 2f;
		if (isIn)
		{
			this.BeamIn(tran);
		}
		else
		{
			this.BeamOut(tran);
		}
	}

	private void BeamOut(Transform tran)
	{
		this.Initialization();
		this.EffectMaterials.Clear();
		Renderer[] componentsInChildren = tran.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			this.m_matBC.SetTexture(ShaderPIDManager._MainTex, renderer.get_material().GetTexture(ShaderPIDManager._MainTex));
			this.m_matBC.SetTexture(ShaderPIDManager._NoiseTex, this.TexBean);
			this.m_matBC.SetColor(ShaderPIDManager._DissolveColor, new Color32(255, 255, 255, 255));
			this.m_matBC.SetFloat(ShaderPIDManager._TileFactor, 4f);
			renderer.set_material(this.m_matBC);
			this.EffectMaterials.Add(renderer.get_material());
		}
		base.StartCoroutine(this.doBeamOut());
	}

	[DebuggerHidden]
	private IEnumerator doBeamOut()
	{
		BeamController.<doBeamOut>c__Iterator37 <doBeamOut>c__Iterator = new BeamController.<doBeamOut>c__Iterator37();
		<doBeamOut>c__Iterator.<>f__this = this;
		return <doBeamOut>c__Iterator;
	}

	private void BeamIn(Transform tran)
	{
		this.Initialization();
		this.EffectMaterials.Clear();
		Renderer[] componentsInChildren = tran.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			this.m_matBC.SetTexture(ShaderPIDManager._MainTex, renderer.get_material().GetTexture(ShaderPIDManager._MainTex));
			this.m_matBC.SetTexture(ShaderPIDManager._NoiseTex, this.TexBean);
			this.m_matBC.SetColor(ShaderPIDManager._DissolveColor, new Color32(255, 255, 255, 0));
			this.m_matBC.SetFloat(ShaderPIDManager._TileFactor, 4f);
			renderer.set_material(this.m_matBC);
			this.EffectMaterials.Add(renderer.get_material());
		}
		base.StartCoroutine(this.doBeamIn());
	}

	[DebuggerHidden]
	private IEnumerator doBeamIn()
	{
		BeamController.<doBeamIn>c__Iterator38 <doBeamIn>c__Iterator = new BeamController.<doBeamIn>c__Iterator38();
		<doBeamIn>c__Iterator.<>f__this = this;
		return <doBeamIn>c__Iterator;
	}
}
