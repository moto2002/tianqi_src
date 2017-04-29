using System;
using UnityEngine;

public class LiquidumTrailEmitter : MonoBehaviour
{
	public GameObject TrailDropPrefab;

	public Material DefaultTrialMaterial;

	public bool Emit = true;

	public bool ChangeMaterialAtRunTime;

	private GameObject TrailDrop;

	private float MyTimer;

	private bool randomize;

	private void Start()
	{
		if (!Liquidum.Instance)
		{
			Debuger.Error("WARNING:Liquidum Main Script not found!\nPlease Drag&Drop Liquidum_Effect.prefab under your camera/player/character controller gameobject.\nFound it in /LIQUIDUM main directory\n", new object[0]);
			return;
		}
		this.TrailDropPrefab.GetComponent<Renderer>().set_material(this.DefaultTrialMaterial);
		if (!this.ChangeMaterialAtRunTime)
		{
			TrailRenderer component = this.TrailDropPrefab.GetComponent<TrailRenderer>();
			this.SetShaderAttribute(component);
		}
	}

	private void Update()
	{
		this.TrialEmit();
	}

	private void TrialEmit()
	{
		this.MyTimer += Time.get_deltaTime();
		if (this.Emit && this.MyTimer >= Liquidum.Instance.TrailCreationDelay)
		{
			Vector3 localPosition = new Vector3(Random.Range((-Liquidum.Instance.TrailMaxDistanceFromCenter - Liquidum.Instance.TrailMinDistanceFromCenter) * 2.2f, (Liquidum.Instance.TrailMaxDistanceFromCenter + Liquidum.Instance.TrailMinDistanceFromCenter) * 2f), 1.2f, 2f);
			if (localPosition.x > -2f && localPosition.x < 1.5f && localPosition.get_magnitude() >= Liquidum.Instance.TrailMinDistanceFromCenter * 4.5f)
			{
				this.TrailDrop = (GameObject)Object.Instantiate(this.TrailDropPrefab, base.get_transform().get_position(), base.get_transform().get_rotation());
				this.TrailDrop.get_transform().set_parent(base.get_transform());
				this.TrailDrop.get_transform().set_localPosition(localPosition);
				if (this.ChangeMaterialAtRunTime)
				{
					TrailRenderer component = this.TrailDrop.GetComponent<TrailRenderer>();
					this.SetShaderAttribute(component);
				}
				this.MyTimer = 0f;
			}
		}
	}

	private void SetShaderAttribute(TrailRenderer trailRender)
	{
		if (trailRender != null)
		{
			trailRender.GetComponent<Renderer>().get_material().SetColor("_Color", Liquidum.Instance.TrailsColor / 2f);
			trailRender.GetComponent<Renderer>().get_material().SetFloat("_BumpAmt", Liquidum.Instance.TrailDistortion * 4f);
		}
	}
}
